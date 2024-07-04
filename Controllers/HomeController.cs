using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;

using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using PerfectWebERP.Filters;
using System.Reflection;
using System.Text;
using System.Net;
using System.Configuration;
using System.Net.Security;
using System.Net.Http.Headers;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]

    public class HomeController : Controller
    {
        Random randomColor = new Random();
        //private IDALCustomer _customer = new DALCustomer();




        #region :: Views ::

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your Contact Page. ";

            return View();
        }

        #endregion :: Views ::


        #region :: Customer Sample ::

        #region :: Customer Section partial and some other common section


        public JsonResult GetFilter()
        {

            List<FilterKeyValuePair> keyValuePairs = new List<FilterKeyValuePair>();
            for (int i = 0; i < 10; i++)
            {
                keyValuePairs.Add(new FilterKeyValuePair { FilterJSONKey = "key " + i, FilterJSONvalue = "value " + i });
            }

            SearchFilter customerNameSearch = new SearchFilter()
            {
                FilterNameKey = "employeeNameSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "input",
                FilterData = new FilterData_input
                {
                    FilterLabel = "employee Name",
                    FilterName = "EmployeeName",
                    Placeholder = "Enter employee name"
                }
            };
            SearchFilter employeeBranchSearch = new SearchFilter()
            {
                FilterNameKey = "employeeBranchSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "dropdown",
                FilterData = new FilterData_dropdown
                {
                    FilterLabel = "branch",
                    FilterName = "branchID",
                    //FilterValue = "",
                    MultipleSelect = false,
                    SelectOptions = keyValuePairs
                }
            };
            SearchFilter employeeRoleSearch = new SearchFilter()
            {
                FilterNameKey = "employeeRoleSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "dropdown",
                FilterData = new FilterData_dropdown
                {
                    FilterLabel = "Role",
                    FilterName = "EmployeeRoleID",
                    //FilterValue = "",
                    MultipleSelect = true,
                    SelectOptions = keyValuePairs
                }
            };
            SearchFilter employeejoinDate = new SearchFilter()
            {
                FilterNameKey = "employeeJoinDate",
                //FilterDataURL = "/filter/cu",
                FilterType = "date",
                FilterData = new FilterData_datepicker
                {
                    FilterLabel = "Join Date",
                    FilterName = "EmployeeJoinDate",
                    FilterValue = "",
                    DateRangePicker = false,
                }
            };
            SearchFilter employeeDaterange = new SearchFilter()
            {
                FilterNameKey = "employeeDaterange",
                //FilterDataURL = "/filter/cu",
                FilterType = "date",
                FilterData = new FilterData_datepicker
                {
                    FilterLabel = "Date Rnage",
                    FilterName = "employeeDaterange",
                    FilterValue = "",
                    DateRangePicker = true,
                }
            };
            SearchFilter employeeCheck = new SearchFilter()
            {
                FilterNameKey = "employeeCkeckSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "check",
                FilterData = new FilterData_check
                {
                    FilterLabel = "Role",
                    FilterName = "EmployeeCheck",
                    FilterValue = "",
                    SelectOptions = new FilterKeyValuePair { FilterJSONKey = "I agree something ", FilterJSONvalue = "ok" }
                }
            };
            List<SearchFilter> GetALLFilter = new List<SearchFilter>();
            List<SearchFilter> GetALLFilter2 = new List<SearchFilter>
                            {
                                customerNameSearch,
                                employeeBranchSearch,
                                employeeRoleSearch,
                                employeejoinDate,
                                employeeDaterange,

                            };

            return Json(new { GetALLFilter2 }, JsonRequestBehavior.AllowGet);
        }

        #endregion :: Customer Section partial and some other common section

        #region ::  Manage Customer  ::
        public ActionResult Customer(string transMode)
        {

            ViewBag.transMode = transMode;


            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

            CountryModel model = new CountryModel();

            var country = model.GetCountryData(companyKey: _userLoginInfo.CompanyKey,
                input: new CountryModel.ID { FK_Country = 0 }
                );


            return View(country.Data);
        }
        [HttpPost]
        public ActionResult UpdateCustomerDetails(UpdateCustomer newCustomerInputData)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            ModelState.Remove("NewData.State");
            ModelState.Remove("NewData.Place");
            ModelState.Remove("NewData.District");
            ModelState.Remove("NewData.CustomerTitle");
            ModelState.Remove("CurrentData.CustomerTitle");
            ModelState.Remove("CurrentData.State");
            ModelState.Remove("CurrentData.Place");
            ModelState.Remove("CurrentData.District");
            ModelState.Remove("CurrentData.CustomerID");
            ModelState.Remove("CurrentData.CustomerDate");
            ModelState.Remove("CurrentData.DateOfBirth");
            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::


            // BLCommon.Output output = _customer.Update(data: newCustomerInputData);




            byte userAction = 2;//update : 2 | Add : 1 
            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string companyKey = _userLoginInfo.CompanyKey;

            Tabless.CustomerSample customer = new Tabless.CustomerSample
            {
                FK_Company = OrgnCode,
                BranchCode = branchCode,
                UserAction = 2,//since update set to 2  | insert=1
                FK_Machine = FK_Machine,
                ID_Customer = (short)newCustomerInputData.NewData.CustomerID,
                CusTitle = newCustomerInputData.NewData.CustomerTitle,
                CusName = newCustomerInputData.NewData.CustomerName,
                FK_Place = newCustomerInputData.NewData.PlaceID,
                FK_District = newCustomerInputData.NewData.DistrictID,
                FK_State = newCustomerInputData.NewData.StateID,
                CusPhone = newCustomerInputData.NewData.Phone,
                cusMobile = newCustomerInputData.NewData.Mobile,
                CusDOB = newCustomerInputData.NewData.DateOfBirth,
                UserCode = userCode,
                CusDate = newCustomerInputData.NewData.CustomerDate,
                AuditData = "",
                BranchCodeUser = branchUserCode,
                SqlUpdateQuery = ""
            };

            Output datresponse = Common.UpdateTableData<Tabless.CustomerSample>(companyKey: companyKey, procedureName: "proCustomerUpdate", parameter: customer);

            return Json(new
            {
                Process = datresponse
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddNewCustomerDetails(Customer newCutomerDetails)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            Output _output = new Output();

            ModelState.Remove("State");
            ModelState.Remove("Place");
            ModelState.Remove("District");
            ModelState.Remove("CustomerID");

            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            //BLCommon.Output output = _customer.Add(newData: newCutomerDetails);

            //Check model validation
            if (false)
            {

            }



            int userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            newCutomerDetails.CustomerDate = DateTime.UtcNow;
            newCutomerDetails.DateOfBirth = DateTime.UtcNow;


            Tabless.CustomerSample customer = new Tabless.CustomerSample
            {
                FK_Company = OrgnCode,
                BranchCode = branchCode,
                UserAction = 1,//since insert set to 1 | update=2
                FK_Machine = FK_Machine,
                ID_Customer = 0,
                CusTitle = newCutomerDetails.CustomerTitle,
                CusName = newCutomerDetails.CustomerName,
                FK_Place = newCutomerDetails.PlaceID,
                FK_District = newCutomerDetails.DistrictID,
                FK_State = newCutomerDetails.StateID,
                CusPhone = newCutomerDetails.Phone,
                cusMobile = newCutomerDetails.Mobile,
                CusDOB = newCutomerDetails.DateOfBirth,
                UserCode = userCode,
                CusDate = newCutomerDetails.CustomerDate,
                AuditData = "",
                BranchCodeUser = branchUserCode,
                SqlUpdateQuery = ""
            };

            Output datresponse = Common.UpdateTableData<Tabless.CustomerSample>(companyKey: companyKey, procedureName: "proCustomerUpdate", parameter: customer);



            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteCustomerInfo(int customerID)
        {
            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::
            // HttpResponseMessage datresponse = APIServices.CallAPiService(bldash, "api/DashBoardApi/UserAccountSelect/");

            int OrgnCode = 1;
            int FK_Machine = 100;
            string userCode = "code";

            string Bankkey = "";
            BlServices blServices = new BlServices();
            blServices.DbName = "PERFECTERP" + Bankkey;

            blServices.ObjectName = "[proCustomerDelete]";
            //blServices.ObjectParameters = "@OrgnCode|@FK_Machine|@ID_Customer|@UserCode";
            //blServices.ObjectArguments = $"{OrgnCode}|{FK_Machine}|{customerID}|{userCode}";
            blServices.ObjectParameters = "@ID_Customer|@UserCode|@FK_Machine";
            blServices.ObjectArguments = $"{customerID}|{userCode}|{FK_Machine}";

            blServices.ObjectDataTypes = "int16|string|int16";
            blServices.ObjectSplitChar = "|";
            HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsNonQuery");


            rootProcedureOutputAsNonQuery root = new rootProcedureOutputAsNonQuery();
            if (datresponse.IsSuccessStatusCode)
            {
                root = JsonConvert.DeserializeObject<rootProcedureOutputAsNonQuery>(datresponse.Content.ReadAsStringAsync().Result);

                // Need to check the value of *root* and then determine whether its success or failure then build curresponding response to front end
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = true,
                        Message = new List<string> { "Deleted Successfully" },
                        Status = "OK"
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Process = new Output
                {
                    IsProcess = false,
                    Message = new List<string> { "Something Went Wrong" },
                    Status = "API Error"
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #region GetProcedure
        [HttpGet]
        public ActionResult GetCustomerList2()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            #region commented code--
            //Random randomGen = new Random();
            //List<Customer> output = new List<Customer>();

            //int maxRow = 40;
            //for (int i = 0; i < maxRow; i++)
            //{
            //    int ID_States = randomGen.Next(1, 5);
            //    int placeID = randomGen.Next(1, 20);
            //    int districtID = randomGen.Next(1, 10);
            //    int customerTitleID = randomGen.Next(1, 5);
            //    output.Add(new Customer
            //    {
            //        CustomerID = i,
            //        CustomerDate = DateTime.UtcNow,
            //        CustomerName = $"Customer{i}",
            //        CustomerTitleID=customerTitleID,
            //        CustomerTitle = $"Customer{customerTitleID} Title",
            //        DateOfBirth = DateTime.UtcNow.AddYears(-randomGen.Next(1, 100)),
            //        DistrictID=districtID,
            //        District = $"District{districtID}",
            //        Mobile = randomGen.Next(111111111, 999999999).ToString(),
            //        Phone = $"2{randomGen.Next(000000, 999999)}",
            //        PlaceID=placeID,
            //        Place = $"Place{placeID}",
            //        StateID=ID_States,
            //        State = $"State{ID_States}"
            //    });
            //};


            //var abb = output.Select(a => new { RandomID = a.CustomerID, RandomTitle = a.CustomerTitle, RandomName = a.CustomerName, CratedDate = a.DateOfBirth.ToString("dd/MM/yyyy") }).ToList();

            //return Json(new
            //{
            //    output,
            //    BaseInfo = output.Select(a => new { RandomID = a.CustomerID, RandomTitle = a.CustomerTitle, RandomName = a.CustomerName, CratedDate=a.DateOfBirth.ToString("dd/MM/yyyy") }).ToList(),//// in ajax success(data){  data.BaseInfo contain BaseInfo node   }
            //    OtherInfo = output.Select(a => new { RandomID = a.CustomerID, RandomDistrict = a.District, RandomState = a.State }).ToList(),//// in ajax success(data){  data.BaseInfo contain BaseInfo node   }

            //}, JsonRequestBehavior.AllowGet);
            ////   return Json(output.Select(a => new { aaa = a.District, bbb = a.CustomerTitle, ccc = a.CustomerID }).ToList(), JsonRequestBehavior.AllowGet);  // in ajax success(data){ data contain all data   }
            ///

            //---------Using api

            #endregion

            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            var data = Common.GetDataViaQuery<Customer>(
                parameters: new APIParameters
                {
                    TableName = "[Customer] AS C Left JOIN State AS S ON S.ID_State=C.FK_State Left JOIN District AS D ON D.ID_District=C.FK_District",
                    SelectFields = "C.[ID_Customer] AS CustomerID,C.[CusTitle] AS CustomerTitle,C.[CusName] AS CustomerName,C.[FK_Place] AS PlaceID,C.[FK_District] AS DistrictID,C.[FK_State] AS StateID,C.[CusPhone] AS Phone,C.[CusMobile] AS Mobile,C.[CusDOB] AS DateOfBirth,C.[CusDate] AS CustomerDate,S.StaName AS State,D.[DisName] AS District",
                    Criteria = "C.cancelled=0",
                    SortFields = "",
                    GroupByFileds = ""
                },
                companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult GetCustomerInfo([Required(ErrorMessage = "Please Select a valid customer")]int? customerID)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            var data = Common.GetDataViaQuery<Customer>(parameters: new APIParameters
            {
                TableName = "[Customer]",
                SelectFields = "[ID_Customer] AS CustomerID,[CusTitle] AS CustomerTitle,[CusName] AS CustomerName,[FK_Place] AS PlaceID,[FK_District] AS DistrictID,[FK_State] AS StateID,[CusPhone] AS Phone,[CusMobile] AS Mobile,[CusDOB] AS DateOfBirth,[CusDate] AS CustomerDate",
                Criteria = $"cancelled=0 AND ID_Customer={customerID}",
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            var cusInfo = Common.GetDataViaProcedure<Tabless.CustomerSample, InputCustomer>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCustomerSelect", parameter: new InputCustomer { ID_Customer = (short)customerID });

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetDistrictList()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            //    blServices.ObjectName = "ProCmnGetRecords";
            //    blServices.ObjectParameters = "@TableName|@ListFields|@SortField|@Criteria|@Groupby";
            //    blServices.ObjectArguments = "District|ID_District AS DistrictID,Disname AS DistrictName||cancelled=0|";
            //    blServices.ObjectDataTypes = "VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)";
            //    blServices.ObjectSplitChar = "|";
            //    HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Common/GetCommonProcedureOutputAsDataTable");


            var data = Common.GetDataViaQuery<District>(parameters: new APIParameters
            {
                TableName = "District",
                SelectFields = "ID_District AS DistrictID,Disname AS DistrictName",
                Criteria = "cancelled=0",
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }

        public ActionResult GetCustomerForm()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ViewModel.LocationLists output = new ViewModel.LocationLists();

            List<Place> placelist = new List<Place>();
            for (int i = 0; i < 20; i++)
            {
                placelist.Add(new Place
                {
                    PlaceID = i,
                    PlaceName = $"Place{i}"
                });
            };

            output.PlaceList = placelist;


            var stateList = Common.GetDataViaQuery<State>(parameters: new APIParameters
            {
                TableName = "State",
                SelectFields = "ID_State AS StateID,StaName AS StateName",
                Criteria = "cancelled=0",
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            output.StateList = stateList.Data;

            List<District> distcictList = new List<District>();
            for (int i = 0; i < 10; i++)
            {
                distcictList.Add(new District
                {
                    DistrictID = i,
                    DistrictName = $"District{i}"
                });
            };
            output.DistrictList = distcictList;



            var customerList = Common.GetDataViaQuery<CustomerTitle>(parameters: new APIParameters
            {
                TableName = "[CustomerType]",
                SelectFields = "ID_CustomerType AS CustomerTitleID,[CtName] AS CustomerTitleName",
                Criteria = "cancelled=0",
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );


            output.CustomerTitlesList = customerList.Data;





            return PartialView("_AddCustomerFormS", output);
        }

        #endregion GetProcedure

        #endregion ::  Manage Customer  ::

        #endregion :: Customer Sample ::

        #region :: Sample Section  ::

        [HttpGet]
        public ActionResult GetSampletable()
        {
            //---------------------------------------

            List<SampleTable> sampleTable = new List<SampleTable>();

            int maxRow = 40;
            for (int i = 0; i < maxRow; i++)
            {
                sampleTable.Add(new SampleTable { id = i, Field1 = "Dummy Field1-" + i, Field2 = "Dummy Field2-" + i, Field3 = "Dummy Field3-" + i, Field4 = "Field4-" + i, Field5 = "Field5-" + i, Field6 = "Field6-" + i, Field7 = "Field7-" + i, Field8 = "Field8-" + i, Field9 = "Field9-" + i });
            }
            return Json(new { sampleTable }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertSampleData(sampleinput input)
        {
            var newVale = input.newValue;
            var oldvalue = input.oldValue;
            return Json(new { newVale, oldvalue }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertSampleDataN(SampleTable input)
        {

            return Json(new { input }, JsonRequestBehavior.AllowGet);
        }
        #region extraregion
        [HttpPost]
        public ActionResult GetCustomerForm2(int customerID)
        {


            List<Place> placelist = new List<Place>();
            for (int i = 0; i < 20; i++)
            {
                placelist.Add(new Place
                {
                    PlaceID = i,
                    PlaceName = $"Place{i}"
                });
            };
            List<State> statelist = new List<State>();
            for (int i = 0; i < 5; i++)
            {
                statelist.Add(new State
                {
                    StateID = i,
                    StateName = $"State{i}"
                });
            };

            List<District> distcictList = new List<District>();
            for (int i = 0; i < 10; i++)
            {
                distcictList.Add(new District
                {
                    DistrictID = i,
                    DistrictName = $"District{i}"
                });
            };

            List<CustomerTitle> customerList = new List<CustomerTitle>();
            for (int i = 0; i < 10; i++)
            {
                customerList.Add(new CustomerTitle
                {
                    CustomerTitleID = i,
                    CustomerTitleName = $"CustomerTitle{i}"
                });
            };

            ViewModel.LocationLists output = new ViewModel.LocationLists();
            output.DistrictList = distcictList;
            output.PlaceList = placelist;
            output.CustomerTitlesList = customerList;
            output.StateList = statelist;

            return PartialView("_AddCustomerFormS", output);
        }
        public ActionResult GetStateList()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<State>(parameters: new APIParameters
            {

                TableName = "District",
                SelectFields = "ID_District AS DistrictID,Disname AS DistrictName",
                Criteria = "cancelled=0",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            return Json(data, JsonRequestBehavior.AllowGet);



            //return Json(new { data = output.Select(a => new { StateID = a.StateID, State = a.StateName }).ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPlaceList()
        {
            Random randomGen = new Random();
            List<Place> output = new List<Place>();

            int maxRow = 20;
            for (int i = 0; i < maxRow; i++)
            {
                output.Add(new Place
                {
                    PlaceID = i,
                    PlaceName = $"State{i}"
                });
            };

            return Json(new { data = output.Select(a => new { StateID = a.PlaceID, State = a.PlaceName }).ToList() }, JsonRequestBehavior.AllowGet);
        }

        #endregion extraregion

        #endregion :: Sample Section  ::



        // GET: Customer

        public ActionResult Index(string transMode)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            ViewBag.transMode = transMode;

            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.Companyname = _userLoginInfo.Company;

            return View();
        }

        public ActionResult LoadCustomerForm()
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            CustomerModel.CustomerFormViewModel formListData = new CustomerModel.CustomerFormViewModel();

            StateModel state = new StateModel();

            var stateList = state.GetStateData(input: new StateModel.GetState { FK_States = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);

            formListData.StateList = stateList.Data;

            DistrictModel district = new DistrictModel();

            return PartialView("_CustomerForm", formListData);
        }

        public ActionResult GetCustomerList()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CustomerModel customer = new CustomerModel();

            var outputList = customer.GetCustomertData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerModel.GetCustomer { ID_Customer = 0, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy });

            for (int i = 0; i < 10; i++)
            {
                outputList.Data.Add(outputList.Data[0]);
            }


            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSampleListIndexinit()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            StateModel model = new StateModel();

            var outputList = model.GetStateData(companyKey: _userLoginInfo.CompanyKey, input: new StateModel.GetState
            {
                FK_States = 0,
                FK_Country = 0,

                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 1,
                PageSize = 2,
                TransMode = ""
            });



            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSampleListIndex(int pageSize, int pageIndex)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            StateModel model = new StateModel();

            var outputList = model.GetStateData(companyKey: _userLoginInfo.CompanyKey, input: new StateModel.GetState
            {
                FK_States = 0,
                FK_Country = 0,

                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = ""
            });


            return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = 50 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetCustomerInfo(CustomerModel.CustomerInfoView customerInfo)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::

            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::


            CustomerModel customer = new CustomerModel();

            var cusInfo = customer.GetCustomertData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerModel.GetCustomer { ID_Customer = customerInfo.CustomerID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy });

            return Json(cusInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewCustomer(CustomerModel.CustomerFormInputViewModel data)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("CustomerID");
            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                //errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                        .Select(e => e.ErrorMessage)
                //                        .ToList();

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::


            CustomerModel customer = new CustomerModel();



            var datresponse = customer.UpdateCustomerData(input: new CustomerModel.UpdateCustomer
            {
                UserAction = 1,
                CusAddress1 = data.CustomerAddress1,
                CusAddress2 = data.CustomerAddress2,
                CusCanvasBy = data.CustomerCanvasBy,
                CusCanvasMode = data.CustomerCanvasMode,
                CusContactEmail = data.CustomerContactEmail,
                CusContactMobile = data.CustomerContactMobile,
                CusContactPerson = data.CustomerContactPerson,
                CusEmail = data.CustomerEmail,
                CusGSTINNo = data.CustomerGSTINNo,
                CusLocation = data.CustomerLocation,
                CusMobile = data.CustomerMobile,
                CusMode = 1,
                CusName = data.CustomerName,
                CusNumber = data.CustomerNumber.ToString(),
                CusPhone = data.CustomerPhone,
                CusStatus = true,
                CusStatusOn = null,
                CusWebsite = data.CustomerWebsite,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = data.BranchID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Category = data.CategoryID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Country = data.CountryID,
                FK_CustomerType = data.CustomerTypeID,
                FK_District = data.DistrictID,
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_LeadGenerate = data.LeadGenerateID,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Post = data.PostID,
                FK_State = data.StateID,
                ID_Customer = 0,
                // EntrOn=DateTime.UtcNow
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustomer(CustomerModel.CustomerFormInputViewModel data)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 

            #region :: Model validation  ::

            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return


                return Json(new
                {


                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            CustomerModel customer = new CustomerModel();



            var datresponse = customer.UpdateCustomerData(input: new CustomerModel.UpdateCustomer
            {
                UserAction = 2,
                CusAddress1 = data.CustomerAddress1,
                CusAddress2 = data.CustomerAddress2,
                CusCanvasBy = data.CustomerCanvasBy,
                CusCanvasMode = data.CustomerCanvasMode,
                CusContactEmail = data.CustomerContactEmail,
                CusContactMobile = data.CustomerContactMobile,
                CusContactPerson = data.CustomerContactPerson,
                CusEmail = data.CustomerEmail,
                CusGSTINNo = data.CustomerGSTINNo,
                CusLocation = data.CustomerLocation,
                CusMobile = data.CustomerMobile,
                CusMode = 1,
                CusName = data.CustomerName,
                CusNumber = data.CustomerNumber.ToString(),
                CusPhone = data.CustomerPhone,
                CusStatus = true,
                CusStatusOn = null,
                CusWebsite = data.CustomerWebsite,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = data.BranchID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Category = data.CategoryID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Country = data.CountryID,
                FK_CustomerType = data.CustomerTypeID,
                FK_District = data.DistrictID,
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_LeadGenerate = data.LeadGenerateID,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Post = data.PostID,
                FK_State = data.StateID,
                ID_Customer = data.CustomerID,
                //EntrOn = DateTime.UtcNow

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCustomer(CustomerModel.CustomerInfoView data)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 

            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            CustomerModel customer = new CustomerModel();

            var datresponse = customer.DeleteCustomerData(input: new CustomerModel.DeleteCustomer { ID_Customer = data.CustomerID, CancelBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.ReasonID, CancelOn = DateTime.UtcNow }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCustomerReasonList()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            if (!outputList.Process.IsProcess)
            {
                return Json(new { Process = outputList.Process }, JsonRequestBehavior.AllowGet);
            }

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Customerpage()
        {

            return View();
        }




        public ActionResult SampleReport()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

            CountryModel model = new CountryModel();

            var country = model.GetCountryData(companyKey: _userLoginInfo.CompanyKey,
                input: new CountryModel.ID { FK_Country = 0 }
                );


            return View(country.Data);
        }











        public ActionResult Dashboard_old()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.GetDashboardDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetDashboardData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = 1,
                Period = 1
            });
            HomeModel.DashboardData obj = new HomeModel.DashboardData();
            if (objInfo != null)
            {
                List<HomeModel.DashboardListData> DataMaster = objInfo.Data.Where(m => m.Module == "Master" && m.DashBoard == 0).ToList();
                obj.Complaints = Convert.ToInt32(DataMaster[0].Value);
                obj.Services = Convert.ToInt32(DataMaster[1].Value);
                obj.Projects = Convert.ToInt32(DataMaster[2].Value);
                obj.Leads = Convert.ToInt32(DataMaster[3].Value);
                obj.Products = Convert.ToInt32(DataMaster[4].Value);

                List<HomeModel.DashboardListData> DataLead = objInfo.Data.Where(m => m.Module == "Lead" && m.DashBoard == 1).ToList();
                obj.LeadHot = Convert.ToDecimal(DataLead[0].Value);
                obj.LeadWarm = Convert.ToDecimal(DataLead[1].Value);
                obj.LeadCold = Convert.ToDecimal(DataLead[2].Value);
                obj.LeadHotCount = Convert.ToInt32(DataLead[0].Count);
                obj.LeadWarmCount = Convert.ToInt32(DataLead[1].Count);
                obj.LeadColdCount = Convert.ToInt32(DataLead[2].Count);

                List<HomeModel.DashboardListData> DataLeadFollowup = objInfo.Data.Where(m => m.Module == "Lead" && m.DashBoard == 2).ToList();
                obj.LeadFollowupPendingTodays = Convert.ToDecimal(DataLeadFollowup[0].Value);
                obj.LeadFollowupPendingDue = Convert.ToDecimal(DataLeadFollowup[1].Value);
                obj.LeadFollowupPendingUpComing = Convert.ToDecimal(DataLeadFollowup[2].Value);
                obj.LeadFollowupPendingTodaysCount = Convert.ToInt32(DataLeadFollowup[0].Count);
                obj.LeadFollowupPendingDueCount = Convert.ToInt32(DataLeadFollowup[1].Count);
                obj.LeadFollowupPendingUpComingCount = Convert.ToInt32(DataLeadFollowup[2].Count);


                List<HomeModel.DashboardListData> DataProject = objInfo.Data.Where(m => m.Module == "Project" && m.DashBoard == 1).ToList();
                obj.ProjectStart = Convert.ToDecimal(DataProject[0].Value);
                obj.ProjectPause = Convert.ToDecimal(DataProject[1].Value);
                obj.ProjectCompleted = Convert.ToDecimal(DataProject[2].Value);
                obj.ProjectStartCount = Convert.ToInt32(DataProject[0].Count);
                obj.ProjectPauseCount = Convert.ToInt32(DataProject[1].Count);
                obj.ProjectCompletedCount = Convert.ToInt32(DataProject[2].Count);

                List<HomeModel.DashboardListData> DataService = objInfo.Data.Where(m => m.Module == "Service" && m.DashBoard == 1).ToList();
                obj.ServiceTodaysPending = Convert.ToDecimal(DataService[0].Value);
                obj.ServiceOverDue = Convert.ToDecimal(DataService[1].Value);
                obj.ServiceUpcoming = Convert.ToDecimal(DataService[2].Value);
                obj.ServiceTodaysPendingCount = Convert.ToInt32(DataService[0].Count);
                obj.ServiceOverDueCount = Convert.ToInt32(DataService[1].Count);
                obj.ServiceUpcomingCount = Convert.ToInt32(DataService[2].Count);
            }
            var objMonth = objHome.GetModeList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetModeData
            {
                Mode = 44
            });
            var objYear = objHome.GetModeList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetModeData
            {
                Mode = 45
            });
            obj.MonthList = objMonth.Data.AsEnumerable();
            obj.YearList = objYear.Data.AsEnumerable();

            var objModuleBase = objHome.GetModuleBaseDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetModuleBaseData
            {
                FK_UserGroup = _userLoginInfo.FK_UserRole,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            HomeModel.ModuleList log = new HomeModel.ModuleList();
            if (objModuleBase.Data != null)
            {
                foreach (var row in objModuleBase.Data)
                {
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "MASTER")
                    {
                        log.MASTER = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "SERVICE")
                    {
                        log.SERVICE = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "LEAD")
                    {
                        log.LEAD = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "INVENTORY")
                    {
                        log.INVENTORY = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "SETTINGS")
                    {
                        log.SETTINGS = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "SECURITY")
                    {
                        log.SECURITY = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "REPORT")
                    {
                        log.REPORT = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "PROJECT")
                    {
                        log.PROJECT = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "OTHER")
                    {
                        log.OTHER = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "PRODUCTION")
                    {
                        log.PRODUCTION = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "ACCOUNTS")
                    {
                        log.ACCOUNTS = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "ASSET")
                    {
                        log.ASSET = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "TOOL")
                    {
                        log.TOOL = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "VEHICLE")
                    {
                        log.VEHICLE = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "DELIVERY")
                    {
                        log.DELIVERY = true;
                    }
                    if (Convert.ToBoolean(row.MenuGroupVisible) && Convert.ToString(row.Module) == "HR")
                    {
                        log.HR = true;
                    }
                }
            }
            obj.ModuleListData = log;
            return View(obj);
        }
        public ActionResult Dashboard()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            HomeModel.DashboardDetails obj = new HomeModel.DashboardDetails();
            var objDashboardInfo = objHome.GetDashboard(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetDashboardInData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Department = _userLoginInfo.FK_Department,
                EntrBy = _userLoginInfo.EntrBy,
                AsOnDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy"))
            });
            ViewBag.RemainingDays = _userLoginInfo.RemainingDays;
            HomeModel.DashboardData objData = new HomeModel.DashboardData();
            if (objDashboardInfo.Data != null)
            {
                List<HomeModel.DashboardOutData> TileData = objDashboardInfo.Data.Where(m => m.ChartType == 1).ToList();
                List<HomeModel.DashboardOutData> ChartData = objDashboardInfo.Data.Where(m => m.ChartType == 2).ToList();
                List<HomeModel.ChartListData> chartList = new List<HomeModel.ChartListData>();
                StringBuilder sbModal = new StringBuilder();
                ViewBag.DashboardTile = CreateTile(TileData);
                ViewBag.DashboardChart = CreateChart(ChartData, ref chartList, ref sbModal);
                ViewBag.DashboardChartModal = sbModal.ToString();
                if (chartList != null)
                {
                    obj.ChartData = chartList;
                }
            }
            else
            {
                ViewBag.DashboardWarning = @"Dashboard settings are missing!";
            }
            return View(obj);
        }
        private string CreateTile(List<HomeModel.DashboardOutData> objData)
        {
            StringBuilder sb = new StringBuilder();
            Type type = typeof(HomeModel.CustomColorClass);
            Array Colorvalues = type.GetEnumValues();
            Random random = new Random();
            for (int i = 0; i < objData.Count; i++)
            {
                int index = random.Next(Colorvalues.Length);
                HomeModel.CustomColorClass cValue = (HomeModel.CustomColorClass)Colorvalues.GetValue(index);

                if (!string.IsNullOrEmpty(objData[i].Datafile))
                {
                    DataTable CountData = JsonConvert.DeserializeObject<DataTable>(objData[i].Datafile);
                    switch (objData[i].ChartList)
                    {
                        case 1:
                        case 3:
                        case 4:
                        case 6:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                    <div class='card' title='" + objData[i].Remarks + @"'>
                                        <div class='social-graph-wrapper " + cValue + @"'>
                                            <div class='media align-items-center'>
                                                <div class='media-body text-left'>
                                                    <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>
                                                <div class='media-body text-right'>
                                                    <span class='fs-18 text-white mb-2 font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='row'>
                                            <div class='col-4 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f96922'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-4 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f5a732'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-4'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][3].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[3].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>");
                            break;
                        case 12:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                            <div class='card' title='" + objData[i].Remarks + @"'>
                                                <div class='card-body' style='margin-top:4%;'>
                                                        <div class='media'>
                                                       <svg class='mr-3' width='80' height='80' viewBox ='0 0 80 80' fill ='none' xmlns ='http://www.w3.org/2000/svg' >
<g clip - path = 'url(#clip0_35_3)' >
<path d = 'M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill = '#D3D3D3' />
<path d = 'M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill = '#40C7CF' />
<g clip-path = 'url(#clip1_35_3)' >
<path d = 'M29.3625 33.2563C29.0545 33.2721 28.7523 33.1683 28.519 32.9665C28.2858 32.7647 28.1396 32.4805 28.1109 32.1734H26.4375C26.4586 32.7595 26.671 33.3225 27.0422 33.7766C27.405 34.2053 27.8994 34.5019 28.4484 34.6203V35.6891H30.0094V34.6484C30.5959 34.5586 31.1276 34.2526 31.5 33.7906C31.8779 33.316 32.077 32.7237 32.0625 32.1172C32.0842 31.7466 32.0133 31.3764 31.8562 31.0401C31.6991 30.7037 31.4607 30.4118 31.1625 30.1906C30.7339 29.9151 30.2566 29.7242 29.7562 29.6281C29.3122 29.5673 28.8858 29.414 28.5047 29.1781C28.3578 29.0306 28.2754 28.8308 28.2754 28.6227C28.2754 28.4145 28.3578 28.2148 28.5047 28.0672C28.6073 27.982 28.7267 27.9192 28.855 27.8829C28.9834 27.8465 29.1179 27.8375 29.25 27.8562C29.4764 27.836 29.7019 27.9035 29.8798 28.0449C30.0578 28.1862 30.1747 28.3906 30.2063 28.6156H31.8094C31.8065 28.1256 31.621 27.6543 31.2891 27.2938C30.9626 26.9023 30.5188 26.6262 30.0234 26.5063V25.4375H28.4906V26.4781C27.9393 26.5727 27.4403 26.8622 27.0844 27.2938C26.7328 27.7367 26.5486 28.2894 26.5641 28.8547C26.5414 29.2065 26.6098 29.5582 26.7625 29.8759C26.9152 30.1936 27.1471 30.4667 27.4359 30.6688C28.0467 30.9993 28.6995 31.2453 29.3766 31.4C29.6524 31.4601 29.9116 31.5802 30.1359 31.7516C30.2047 31.8326 30.2563 31.9267 30.2877 32.0282C30.3191 32.1297 30.3297 32.2365 30.3188 32.3422C30.3255 32.4694 30.3052 32.5966 30.2591 32.7154C30.2131 32.8342 30.1423 32.9418 30.0516 33.0312C29.8589 33.1912 29.6124 33.2716 29.3625 33.2563Z' fill = 'white' />
<path d = 'M58.7812 36.6875H48.9375V21.2188C48.9375 20.8458 48.7893 20.4881 48.5256 20.2244C48.2619 19.9607 47.9042 19.8125 47.5312 19.8125H22.2188C21.8458 19.8125 21.4881 19.9607 21.2244 20.2244C20.9607 20.4881 20.8125 20.8458 20.8125 21.2188V54.9688C20.8125 55.3417 20.9607 55.6994 21.2244 55.9631C21.4881 56.2268 21.8458 56.375 22.2188 56.375H43.3125V57.7812C43.3125 58.1542 43.4607 58.5119 43.7244 58.7756C43.9881 59.0393 44.3458 59.1875 44.7188 59.1875H58.7812C59.1542 59.1875 59.5119 59.0393 59.7756 58.7756C60.0393 58.5119 60.1875 58.1542 60.1875 57.7812V38.0938C60.1875 37.7208 60.0393 37.3631 59.7756 37.0994C59.5119 36.8357 59.1542 36.6875 58.7812 36.6875ZM43.3125 38.0938V53.5625H23.625V22.625H46.125V36.6875H44.7188C44.3458 36.6875 43.9881 36.8357 43.7244 37.0994C43.4607 37.3631 43.3125 37.7208 43.3125 38.0938ZM57.375 56.375H46.125V39.5H57.375V56.375Z' fill = 'white' />
<path d = 'M55.9688 40.9062H47.5312V43.7188H55.9688V40.9062Z' fill = 'white' />
<path d = 'M50.3438 45.125H47.5312V47.9375H50.3438V45.125Z' fill = 'white' />
<path d = 'M50.3438 49.3438H47.5312V52.1562H50.3438V49.3438Z' fill = 'white' />
<path d = 'M55.9688 45.125H53.1562V47.9375H55.9688V45.125Z' fill = 'white' />
<path d = 'M55.9688 49.3438H53.1562V52.1562H55.9688V49.3438Z' fill = 'white' />
<path d = 'M43.3125 26.8438H34.875V29.6562H43.3125V26.8438Z' fill = 'white' />
<path d = 'M39.0938 31.0625H34.875V33.875H39.0938V31.0625Z' fill = 'white' />
<path d = 'M40.5 39.5H26.4375V42.3125H40.5V39.5Z' fill = 'white' />
<path d = 'M40.5 43.7188H26.4375V46.5312H40.5V43.7188Z' fill = 'white' />
</g>
</g>
<defs>
<clipPath id ='clip0_35_3' >
< rect width='80' height ='80' fill= 'white' />
</clip Path >
<clipPath id = 'clip1_35_3' >
<rect width = '45'height ='45' fill= 'white' transform ='translate(18 17)' />
</clipPath>
</defs>
</svg>

                                                        <div class='media-body'>
                                                            <h6 class='fs-16 text-black font-w600'>" + objData[i].ChartName + @"</h6>
                                                            <span class='fs-16 text-primary font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                        </div>
                                                    </div>                       
                                                </div>

                                            </div>
                                        </div>");
                            break;
                        case 2:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                            <div class='card' title='" + objData[i].Remarks + @"'>
                                                <div class='card-body' style='margin-top:4%;'>
                                                        <div class='media'>
                                                        <svg class='mr-3' width='80' height='80' viewBox='0 0 80 80' fill='none'>
                                                            <path d='M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill='#D3D3D3'></path>
                                                            <path d='M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill='#40C7CF'></path>
                                                            <path d='M20.6216 20.6219C23.142 18.1015 26.1342 16.1022 29.4273 14.7381C32.7205 13.374 36.25 12.672 39.8145 12.672C43.3789 12.672 46.9085 13.374 50.2016 14.7381C53.4947 16.1022 56.4869 18.1015 59.0074 20.6219C61.5278 23.1424 63.5271 26.1346 64.8912 29.4277C66.2552 32.7208 66.9573 36.2504 66.9573 39.8148C66.9573 43.3793 66.2552 46.9088 64.8912 50.202C63.5271 53.4951 61.5278 56.4873 59.0074 59.0077L49.4109 49.4113C50.6711 48.1511 51.6708 46.6549 52.3528 45.0084C53.0348 43.3618 53.3859 41.5971 53.3859 39.8148C53.3859 38.0326 53.0348 36.2678 52.3528 34.6213C51.6708 32.9747 50.6711 31.4786 49.4109 30.2184C48.1507 28.9582 46.6546 27.9585 45.008 27.2765C43.3615 26.5944 41.5967 26.2434 39.8145 26.2434C38.0322 26.2434 36.2675 26.5944 34.6209 27.2765C32.9743 27.9585 31.4782 28.9582 30.218 30.2184L20.6216 20.6219Z' fill='#8FD7FF'></path>
                                                            <path d='M20.6215 59.0077C15.5312 53.9174 12.6715 47.0135 12.6715 39.8148C12.6715 32.6161 15.5312 25.7122 20.6215 20.6219C25.7118 15.5316 32.6157 12.6719 39.8144 12.6719C47.0131 12.6719 53.917 15.5316 59.0073 20.6219L49.4108 30.2183C46.8657 27.6732 43.4138 26.2434 39.8144 26.2434C36.215 26.2434 32.7631 27.6732 30.2179 30.2183C27.6728 32.7635 26.243 36.2154 26.243 39.8148C26.243 43.4141 27.6728 46.8661 30.2179 49.4112L20.6215 59.0077Z' fill='white'></path>
                                                        </svg>
                                                        <div class='media-body'>
                                                            <h6 class='fs-16 text-black font-w600'>" + objData[i].ChartName + @"</h6>
                                                            <span class='fs-16 text-primary font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                        </div>
                                                    </div>                       
                                                </div>

                                            </div>
                                        </div>");
                            break;
                        case 11:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                            <div class='card' title='" + objData[i].Remarks + @"'>
                                                <div class='card-body' style='margin-top:4%;'>
                                                <div class='media'>
                                                    <svg class='mr-3' width='80' height='80' viewBox ='0 0 80 80' fill='none' xmlns='http://www.w3.org/2000/svg'>
                                                            <g clip -path ='url(#clip0_7_670' >
                                                            <path d='M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill = '#D3D3D3' />

                                                            <path d='M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill = '#40C7CF/' />

                                                            <g clip-path='url(#clip1_7_670)'>
                                                            <g clip-path ='url(#clip2_7_670)'>
                                                            <path d = 'M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill = '#D3D3D3' />
                                                            <path d = 'M0 11.6364C0 5.20978 5.20978 0 11.6364 0H68.3636C74.7902 0 80 5.20978 80 11.6364V68.3636C80 74.7902 74.7902 80 68.3636 80H11.6364C5.20978 80 0 74.7902 0 68.3636V11.6364Z' fill = '#40C7CF' />
                                                            <path d = 'M60.6563 55.3437H20.3438C19.3125 55.3437 18.4688 54.5 18.4688 53.4687V19.3437C18.4688 18.3125 19.3125 17.4688 20.3438 17.4688H60.75C61.7813 17.4688 62.625 18.3125 62.625 19.3437V53.5625C62.5313 54.5 61.6875 55.3437 60.6563 55.3437ZM22.125 51.6875H58.875V21.125H22.125V51.6875Z' fill = 'white' />
                                                            <path d = 'M32.5313 61.5312C31.9688 61.5312 31.4063 61.25 31.0313 60.7812C30.4688 59.9375 30.5626 58.8125 31.4063 58.25L39.3751 52.4375C40.2188 51.875 41.3438 51.9687 41.9063 52.8125C42.4688 53.6562 42.3751 54.7812 41.5313 55.3437L33.5626 61.1562C33.2813 61.4375 32.9063 61.5312 32.5313 61.5312Z' fill = 'white' />
                                                            <path d = 'M48.4687 61.5313C48.0937 61.5313 47.7187 61.4375 47.3437 61.1562L39.375 55.3438C38.5312 54.7813 38.3437 53.5625 39 52.8125C39.5625 51.9688 40.7812 51.7813 41.5312 52.4375L49.5 58.25C50.3437 58.8125 50.5312 60.0313 49.875 60.7813C49.5937 61.25 49.0312 61.5313 48.4687 61.5313Z' fill = 'white' />
                                                            <path d = 'M32.625 34.0625C31.5938 34.0625 30.75 33.2188 30.75 32.1875V24.2188C30.75 23.1875 31.5938 22.3438 32.625 22.3438C33.6563 22.3438 34.5 23.1875 34.5 24.2188V32.1875C34.5 33.2188 33.6563 34.0625 32.625 34.0625Z' fill = 'white' />
                                                            <path d = 'M36.0937 28.9063C35.625 28.9063 35.1562 28.7188 34.875 28.4375L32.625 26.375L30.375 28.4375C29.625 29.0938 28.5 29.0938 27.75 28.3438C27.0937 27.5938 27.0937 26.4688 27.8437 25.7188L31.3125 22.5313C32.0625 21.875 33.0937 21.875 33.8437 22.5313L37.3125 25.7188C38.0625 26.375 38.0625 27.5938 37.4062 28.3438C37.125 28.7188 36.6562 28.9063 36.0937 28.9063Z' fill = 'white' />
                                                            <path d = 'M32.3438 48.9687V47.8438C30.9375 47.6563 29.7188 47.1875 28.5938 46.1563L29.7187 44.8438C30.5625 45.5938 31.4063 46.0625 32.3438 46.1563V43.1563C31.2188 42.875 30.375 42.5 29.8125 42.0313C29.25 41.5625 29.0625 40.9063 29.0625 40.0625C29.0625 39.2188 29.3438 38.4687 30 37.9062C30.6563 37.3437 31.4063 37.0625 32.3438 36.9687V36.2188H33.2812V36.9687C34.4062 37.0625 35.4375 37.4375 36.4688 38.0938L35.4375 39.5C34.7813 39.0313 34.0312 38.75 33.2812 38.6563V41.5625C34.4062 41.8438 35.25 42.2188 35.8125 42.6875C36.375 43.1563 36.6562 43.8125 36.6562 44.75C36.6562 45.6875 36.375 46.3438 35.7187 46.9063C35.0625 47.4688 34.3125 47.75 33.2812 47.8438V48.9687H32.3438ZM31.2188 39.125C30.9375 39.3125 30.8437 39.5938 30.8437 39.9688C30.8437 40.3438 30.9375 40.625 31.125 40.8125C31.3125 41 31.6875 41.1875 32.25 41.375V38.75C31.875 38.75 31.5 38.8438 31.2188 39.125ZM34.4063 45.7813C34.6875 45.5938 34.875 45.2187 34.875 44.9375C34.875 44.5625 34.7813 44.2813 34.5 44.0938C34.3125 43.9063 33.8437 43.7187 33.2812 43.5312V46.25C33.75 46.1563 34.125 46.0625 34.4063 45.7813Z' fill = 'white' />
                                                            <path d = 'M48.375 33.6875C47.3438 33.6875 46.5 32.8438 46.5 31.8125V23.8438C46.5 22.8125 47.3438 21.9688 48.375 21.9688C49.4063 21.9688 50.25 22.8125 50.25 23.8438V31.8125C50.1563 32.8438 49.4063 33.6875 48.375 33.6875Z' fill = 'white' />
                                                            <path d = 'M48.375 34.0625C47.9062 34.0625 47.4375 33.875 47.1562 33.5938L43.6875 30.4063C42.9375 29.75 42.9375 28.5313 43.5937 27.7813C44.25 27.0313 45.4687 27.0313 46.2187 27.6875L48.4687 29.75L50.7187 27.6875C51.4687 27.0313 52.5937 27.0313 53.3437 27.7813C54 28.5313 54 29.6563 53.25 30.4063L49.7812 33.5938C49.2187 33.875 48.8437 34.0625 48.375 34.0625Z' fill = 'white' />
                                                            <path d = 'M48 48.9687V47.8438C46.5938 47.6563 45.375 47.1875 44.25 46.1562L45.375 44.8438C46.2188 45.5938 47.0625 46.0625 48 46.1562V43.1562C46.875 42.875 46.0312 42.5 45.4687 42.0313C44.9062 41.5625 44.7188 40.9063 44.7188 40.0625C44.7188 39.2188 45 38.4687 45.6563 37.9062C46.3125 37.3437 47.0625 37.0625 48 36.9687V36.2188H48.9375V36.9687C50.0625 37.0625 51.0938 37.4375 52.125 38.0937L51.0937 39.5C50.4375 39.0312 49.6875 38.75 48.9375 38.6563V41.5625C50.0625 41.8438 50.9063 42.2187 51.4688 42.6875C52.0313 43.1562 52.3125 43.8125 52.3125 44.75C52.3125 45.6875 52.0312 46.3437 51.375 46.9062C50.7187 47.4687 49.9688 47.75 48.9375 47.8438V48.9687H48ZM46.9687 39.125C46.6875 39.3125 46.5938 39.5937 46.5938 39.9687C46.5938 40.3437 46.6875 40.625 46.875 40.8125C47.0625 41 47.4375 41.1875 48 41.375V38.75C47.5313 38.75 47.25 38.8438 46.9687 39.125ZM50.1562 45.7813C50.4375 45.5938 50.625 45.2187 50.625 44.9375C50.625 44.5625 50.5313 44.2812 50.25 44.0937C50.0625 43.9062 49.5937 43.7187 49.0312 43.5312V46.25C49.5 46.1563 49.875 46.0625 50.1562 45.7813Z' fill = 'white' />


                                                            </ svg >
                                                        <div class='media-body'>
                                                            <h6 class='fs-16 text-black font-w600'>" + objData[i].ChartName + @"</h6>
                                                            <span class='fs-16 text-primary font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                        </div>
                                                        </div>   
                                                    </div>                       
                                                </div>

                                            </div>
                                        </div>");
                            break;
                        case 5:
                        case 7:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                    <div class='card' title='" + objData[i].Remarks + @"'>
                                        <div class='social-graph-wrapper " + cValue + @"'>
                                            <div class='media align-items-center'>
                                                <div class='media-body text-left'>
                                                    <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>
                                                <div class='media-body text-right'>
                                                    <span class='fs-18 text-white mb-2 font-w600'>" + CountData.Rows[0][0].ToString() + @"</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class='row'>
                                            <div class='col-6 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f96922'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-6'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f5a732'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                </div>
                                            </div>                                           
                                        </div>
                                    </div>
                                </div>");
                            break;
                        case 9:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                                        <div class='card'  title='" + objData[i].Remarks + @"'>
                                                            <div class='social-graph-wrapper " + cValue + @"'>
                                                                <div class='media align-items-center'>
                                                                    <div class='media-body text-left'>
                                                                        <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                                    </div>
                                                                    <div class='media-body text-right' style='display:none;'>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class='row'>
                                                                
                                                                <div class='col-4 border-right'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][0].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#f5a732'>" + CountData.Columns[0].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-4'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-4'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#ff0000'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            
                                                        </div>
                                                    </div>");

                            break;
                        case 10:
                            sb.Append(@"<div class='col-xl-4 col-xxl-3 col-sm-12'>
                                                        <div class='card'  title='" + objData[i].Remarks + @"'>
                                                            <div class='social-graph-wrapper " + cValue + @"'>
                                                                <div class='media align-items-center'>
                                                                    <div class='media-body text-left'>
                                                                        <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                                    </div>
                                                                    <div class='media-body text-right' style='display:none;'>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class='row'>
                                                                <div class='col-3 border-right'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][0].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#f96922'>" + CountData.Columns[0].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-3 border-right'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#f5a732'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-3'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                                <div class='col-3'>
                                                                    <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                                        <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][3].ToString() + @"</span></h4>
                                                                        <p class='m-0' style='color:#ff0000'>" + CountData.Columns[3].Caption.ToString() + @"</p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             
                                                        </div>
                                                    </div>");

                            break;
                        case 14:
                            sb.Append(@"<div class='col-xl-3 col-xxl-3 col-sm-12'>
                                    <div class='card' title='" + objData[i].Remarks + @"'>
                                        <div class='social-graph-wrapper " + cValue + @"'>
                                            <div class='media align-items-center'>
                                                <div class='media-body text-left'>
                                                    <p class='fs-18 text-white mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>
                                               
                                            </div>
                                        </div>
                                        <div class='row'>
                                            <div class='col-4 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][0].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f96922'>" + CountData.Columns[0].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-4 border-right'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][1].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#f5a732'>" + CountData.Columns[1].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                            <div class='col-4'>
                                                <div class='pt-3 pb-3 pl-0 pr-0 text-center'>
                                                    <h4 class='m-1'><span class='counter'>" + CountData.Rows[0][2].ToString() + @"</span></h4>
                                                    <p class='m-0' style='color:#1ca4fc'>" + CountData.Columns[2].Caption.ToString() + @"</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>");
                            break;

                       
                    }
                }
            }
            return sb.ToString();
        }
        private string CreateChart(List<HomeModel.DashboardOutData> objData, ref List<HomeModel.ChartListData> chartList, ref StringBuilder sbModal)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < objData.Count; i++)
            {
                if (!string.IsNullOrEmpty(objData[i].Datafile))
                {
                    string PanelChartID = "PanelChart" + objData[i].ChartList;
                    string ChartID = "canChart" + objData[i].ChartList;
                    string ModalChartID = "ModCanChart" + objData[i].ChartList;
                    sbModal.Append(@"<div class='modal fade' id='modal" + PanelChartID + @"' data-backdrop='static' data-keyboard='false' tabindex='1000'>
                                        <div class='modal-dialog modal-dialog-centered modal-lg' role='dialog'>
                                            <div class='modal-content'>
                                                <div class='modal-header border-0'>
                                                    <h5 class='modal-title' style='text-transform: inherit;'>" + objData[i].ChartName + @"</h5>
                                                    <button type='button' class='close' data-dismiss='modal' aria-label='Close'>
                                                        <span aria-hidden='true'>&times;</span>
                                                    </button>
                                                </div>
                                                <div class='modal-body' perfect-class='formGroup' style='max-height: calc(100vh - 200px); overflow-y: auto;padding: 1rem;'>
                                                    <div class='row'>");

                    DataTable ChartData = JsonConvert.DeserializeObject<DataTable>(objData[i].Datafile);
                    switch (objData[i].ChartList)
                    {
                        case 1:
                            sb.Append(@"<div class='col-xl-4 col-xxl-4 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>                                                    
                                                </div>     
                                                <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                        <div class='row'>");
                            foreach (DataRow row in ChartData.Rows)
                            {
                                var percentage = ((Convert.ToDecimal(row[1]) * 100) / Convert.ToDecimal(row[2]));
                                var chartdata = @"<div class='col-md-6' style='margin-top: 15px;'>
                                                                                                                            <div class='widget-content'>
                                                                                                                                <div class='widget-content-outer'>
                                                                                                                                    <div class='widget-content-wrapper'>
                                                                                                                                        <div class='widget-content-right'>
                                                                                                                                            <div class='text-muted opacity-6'>" + row[0].ToString() + "  (" + row[1].ToString() + "/" + row[2].ToString() + @")</div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                    <div class='widget-progress-wrapper mt-1'>
                                                                                                                                        <div class='progress-bar-sm progress-bar-animated-alt progress' style='height: 18px;'>
                                                                                                                                            <div class='progress-bar' role='progressbar'  aria-valuemin='0' aria-valuemax='100' style='width:" + percentage + "%;background-color:" + String.Format("#{0:X6}", randomColor.Next(0x1000000)) + @";'></div>
                                                                                                                                        </div>
                                                                                                                                    </div>
                                                                                                                                </div>
                                                                                                                            </div>
                                                                                                                        </div>";
                                sb.Append(chartdata);
                                sbModal.Append(chartdata);
                            }
                            sb.Append(@"</div>                                                        
                                                    </div>
                                                </div>
                                            </div>  
                                            <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div>                                                 
                                        </div>
                                    </div>");
                            break;

                        case 4:
                            sb.Append(@"<div class='col-xl-4 col-xxl-4 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>    
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                <div class='col-sm-12 col-12'>
                                                    <div id='" + ChartID + @"' style='width: 100%; height: 250px; background-color: #FFFFFF;'></div>
                                                </div>
                                            </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                    <div id='" + ModalChartID + @"' style='width: 100%; height: 500px; background-color: #FFFFFF;'></div>
                                                </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList);
                            break;
                        case 2:
                        case 3:
                        case 5:
                        case 6:
                        //case 7:
                        case 8:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 28:
                        case 16:
                        case 26:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                      
                            sb.Append(@"<div class='col-xl-4 col-xxl-4 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                         <canvas id='" + ChartID + @"' style='width:100%;'></canvas>     
                                                    </div>
                                                </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <canvas id='" + ModalChartID + @"' style='width:100%;'></canvas>   
                                                    </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList);
                            break;
                        case 7:

                        case 9:
                        case 17:
                        case 18:
                        case 19:
                        case 37:
                            sb.Append(@"<div class='col-xl-4 col-xxl-4 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                         <canvas id='" + ChartID + @"' style='width:100%;'></canvas>     
                                                    </div>
                                                </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <canvas id='" + ModalChartID + @"' style='width:100%;'></canvas>   
                                                    </div>");
                            CreateCompareChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList);
                            break;
                        case 25:                            sb.Append(@"<div class='col-xl-4 col-xxl-4 col-sm-12' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>
                                                         <canvas id='" + ChartID + @"' style='width:100%;'></canvas>     
                                                    </div>
                                                </div>
                                            </div>
                                             <div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> 
                                        </div>
                                    </div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <canvas id='" + ModalChartID + @"' style='width:100%;'></canvas>   
                                                    </div>");
                            CreateSalesCompareChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList);
                            break;

                        case 27:
                            string tablebodyhtml = ""; var SNo = 0;
                            foreach (DataRow r in ChartData.Rows)
                            {
                                SNo++;

                                tablebodyhtml = tablebodyhtml + ("<tr><td>" + SNo + "</td><td>" + r["ProdName"].ToString() + "</td><td>" + r["ReorderLevel"].ToString() + "</td><td>" + r["CurrentQuantity"].ToString() + "</td></tr>");
                            }

                            sb.Append(@"<div class=' col-xl-4 col-xxl-4 col-sm-12 gridChart' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>" + "<table class='rwd-table '>"

                                                                           + " <tbody id='" + PanelChartID + "'><tr><th>Sl No.</th><th style='width:500px;'>Product</th><th>ReOrder Level</th><th>Current Stock</th></tr>" + tablebodyhtml +


                             "</tbody>" +
                                                                       "</table>" +

                                                    "</div>" +
                                                "</div>" +
                                           " </div> " +
                                             "<div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> " +
                                       " </div> " +
                                    "</div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <table class='rwd-table'>" +

                                                                            "<tbody id='" + PanelChartID + "'><tr><th  width='10%'>Sl No.</th><th>Product</th> <th>ReOrder Level</th><th>Current Stock</th></tr>" +
                                                                            tablebodyhtml +
                                                                             "</tbody>" +
                                                                        "</table>   </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList);
                            break;
                        case 20:
                            string tblbodyhtml = ""; var slno = 0;
                            foreach (DataRow r in ChartData.Rows)
                            {
                                slno++;

                                tblbodyhtml = tblbodyhtml + ("<tr style='border:solid " + r["Color"].ToString() + ";background:" + r["Color"].ToString() + "'><td>" + slno + "</td><td>" + r["Project"].ToString() + "</td><td>" + r["Stages"].ToString() + "</td><td>" + r["DueDate"].ToString() + "</td><td style='text-align:right;'>" + r["Remaining"].ToString() + "</td></tr>");
                            }
                            sb.Append(@"<div class=' col-xl-4 col-xxl-4 col-sm-12 gridChart' id='" + PanelChartID + @"'>
                                        <div class='card flex-lg-column flex-md-row w-100'>
                                            <div class='card-header border-0 pb-0 flex-wrap' style='background-color:#e1e7e7;'>
                                                <div class='card-header-title cht' modalid='modal" + PanelChartID + @"'>
                                                    <p class='fs-18 mb-2'>" + objData[i].ChartName + @"</p>
                                                </div>   
                                                 <ul class='nav'>
                                                    <button type='button' class='close chartClose' data-dismiss='alert' title='Close' style='margin: -4px -19px 10px 0px;' chartID='" + PanelChartID + @"'><span class='float-right'><i class='fa fa-remove'></i></span></button>
                                                </ul>
                                            </div>
                                            <div class='card-body card-body text-sm-left'>
                                                <div class='row'>
                                                    <div class='col-sm-12 col-12'>" + "<table class='rwd-table'>"

                                                                           + " <tbody id='" + PanelChartID + "'><tr><th>Sl No.</th><th>Project</th> <th>Stage</th><th>Due Date</th> <th style='text-align:right;'>Remaining</th></tr>" + tblbodyhtml +


                             "</tbody>" +
                                                                       "</table>" +

                                                    "</div>" +
                                                "</div>" +
                                           " </div> " +
                                             "<div class='card-footer' style='padding: 0.1rem 0.5rem 0.1rem;'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div> " +
                                       " </div> " +
                                    "</div>");
                            sbModal.Append(@"<div class='col-sm-12 col-12'>
                                                         <table class='rwd-table'>" +

                                                                            "<tbody id='" + PanelChartID + "'><tr><th  width='10%'>Sl No.</th><th>Project</th> <th>Stage</th><th>Due Date</th> <th style='text-align:right;'>Remaining</th></tr>" +
                                                                            tblbodyhtml +
                                                                             "</tbody>" +
                                                                        "</table>   </div>");
                            CreateChartData(ChartData, objData[i].ChartList, ChartID, ModalChartID, objData[i].XAxis, objData[i].YAxis, ref chartList);
                            break;
                      
                    }
                    sbModal.Append(@"</div><br/><div class='text-center'><span style='font-size:x-small;color: gray;'>" + objData[i].Remarks + @"</span></div></div></div></div></div>");
                }
            }

            return sb.ToString();
        }
        private void CreateChartData(DataTable dt, long chartID, string elementID, string ChartModalID, string XAxis, string YAxis, ref List<HomeModel.ChartListData> chartList)
        {
            bool showYinHundreds = false;
            bool showY3inHundreds = false;
            bool showXinHundreds = false;
            long DivXAmnt = 0;
            long DivYAmnt = 0;
            long DivY2Amnt = 0;
            long DivY3Amnt = 0;

            bool showY2inHundreds = false;
            bool AnyStringY = false; bool AnyStringY2 = false; bool AnyStringY3 = false;
            bool AnyStringX = false;
            foreach (DataRow row in dt.Rows)
            {
                HomeModel.ChartListData obj = new HomeModel.ChartListData();
                obj.ChartID = chartID;
                obj.ChartName = elementID;



                obj.xValues = row[0].ToString();
                obj.yValues = row[1].ToString();
                obj.yColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                Double num;
                var isnumber = Double.TryParse(row[1].ToString(),out num);
                if (!isnumber && !AnyStringY)
                {
                    AnyStringY = true;
                }
                if (chartID != 9)
                {
                    if (!AnyStringY)
                    {
                        if (!string.IsNullOrEmpty(row[1].ToString()) && isnumber)
                    {
                        var yVal = Math.Abs(Convert.ToDouble(row[1]));
                        if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 10000;
                        }
                        else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 1000;
                        }
                        else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 100;
                        }
                    }
                }
                }
                isnumber = Double.TryParse(row[0].ToString(), out num);
                if (!isnumber && !AnyStringX)
                {
                    AnyStringX = true;

                }
                if (!string.IsNullOrEmpty(row[0].ToString()) && isnumber)
                {
                    if (!AnyStringX)
                    {
                        var XVal = Math.Abs(Convert.ToDouble(row[0]));

                        if (XVal > 100000 && (DivXAmnt >= 100000 || DivXAmnt == 0))
                        {
                            obj.ShowXInhndreds = true; showXinHundreds = true; DivXAmnt = 10000;
                        }
                        else if (XVal > 10000 && (DivXAmnt >= 10000 || DivXAmnt == 0))
                        {
                            obj.ShowXInhndreds = true; showXinHundreds = true; DivXAmnt = 1000;
                        }
                        else if (XVal > 1000 && (DivXAmnt >= 1000 || DivXAmnt == 0))
                        {
                            obj.ShowXInhndreds = true; showXinHundreds = true; DivXAmnt = 100;
                        }
                    }
                }
                if (chartID != 2)
                {
                    if (dt.Columns.Count > 2)
                    {
                        isnumber = Double.TryParse(row[2].ToString(), out num);
                        if (!isnumber && !AnyStringY2)
                        {
                            AnyStringY2 = true;
                        }
                        if (!AnyStringY2)
                        {
                            if (!string.IsNullOrEmpty(row[2].ToString()) && isnumber)
                            {
                                var yVal = Math.Abs(Convert.ToDouble(row[2]));
                                if (yVal > 100000 && (DivY2Amnt >= 100000 || DivY2Amnt == 0))
                                {
                                    obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 10000;
                                }
                                else if (yVal > 10000 && (DivY2Amnt >= 10000 || DivY2Amnt == 0))
                                {
                                    obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 1000;
                                }
                                else if (yVal > 1000 && (DivY2Amnt >= 1000 || DivY2Amnt == 0))
                                {
                                    obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 100;
                                }
                            }
                        }
                        obj.ySecondValues = row[2].ToString();
                        obj.ySecondColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    }
                    if (dt.Columns.Count > 3)
                    {
                        isnumber = Double.TryParse(row[3].ToString(), out num);
                        if (!isnumber && !AnyStringY3)
                        {
                            AnyStringY3 = true;
                        }
                        if (!AnyStringY3)
                        {
                            if (!string.IsNullOrEmpty(row[3].ToString()) && isnumber)
                            {
                                var yVal = Math.Abs(Convert.ToDouble(row[3]));
                                if (yVal > 100000 && (DivY3Amnt >= 100000 || DivY3Amnt == 0))
                                {
                                    obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 10000;
                                }
                                else if (yVal > 10000 && (DivY3Amnt >= 10000 || DivY3Amnt == 0))
                                {
                                    obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 1000;
                                }
                                else if (yVal > 1000 && (DivY3Amnt >= 1000 || DivY3Amnt == 0))
                                {
                                    obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 100;
                                }
                            }
                        }
                        obj.yThirdValues = row[3].ToString();
                        obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    }
                }
                if (AnyStringX)
                {
                    showXinHundreds = false;
                }
                if (AnyStringY)
                {
                    showYinHundreds = false;
                }
                if (AnyStringY2)
                {
                    showY2inHundreds = false;
                }
                if (showYinHundreds)
                {
                    switch (DivYAmnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;

                    }

                }
                else if (showY2inHundreds)
                {
                    switch (DivY2Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else if (showY3inHundreds)
                {
                    switch (DivY3Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else
                {
                    obj.YAxis = YAxis;
                }
                if (showXinHundreds)
                {
                    switch (DivXAmnt)
                    {
                        case 100: obj.XAxis = XAxis + " (In Hundreds)"; break;
                        case 1000: obj.XAxis = XAxis + " (In Thousands)"; break;
                        case 10000: obj.XAxis = XAxis + " (In Ten Thousands)"; break;
                        case 0: obj.XAxis = XAxis; break;
                    }

                }
                else
                {
                    obj.XAxis = XAxis;
                }
                obj.ChartModalName = ChartModalID;
                chartList.Add(obj);
            }
            
            foreach (var item in chartList.Where(w => w.ChartID == chartID))
            {
                item.ShowYInhndreds = showYinHundreds;
                item.ShowY2Inhndreds = showY2inHundreds;
                item.ShowXInhndreds = showXinHundreds;
                item.DevideXAmnt = DivXAmnt;
                item.DevideYAmnt = DivYAmnt;
                item.DevideY2Amnt = DivY2Amnt;
                item.DevideY3Amnt = DivY3Amnt;
            }
        }


        private void CreateCompareChartData(DataTable dt, long chartID, string elementID, string ChartModalID, string XAxis, string YAxis, ref List<HomeModel.ChartListData> chartList)
        {
            bool showYinHundreds = false;
            bool showY2inHundreds = false;
            bool showY3inHundreds = false;
            bool showXinHundreds = false;
            long DivYAmnt = 0;
            long DivY2Amnt = 0;
            long DivY3Amnt = 0;
            long DivXAmnt = 0;

            foreach (DataRow row in dt.Rows)
            {
                HomeModel.ChartListData obj = new HomeModel.ChartListData();
                obj.ChartID = chartID;
                obj.ChartName = elementID;
                obj.xValues = row[0].ToString();
                obj.yValues = row[1].ToString();
                obj.yColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                obj.XAxis = XAxis;
                //if (!string.IsNullOrEmpty(row[1].ToString()) && row[1].ToString().All(char.IsDigit))
                //{
                //    if (Convert.ToInt64(row[1]) > 1000)
                //    {
                //        obj.ShowYInhndreds = true; showYinHundreds = true;
                //    }
                //}
                double num;
                var isnumber = Double.TryParse(row[0].ToString(), out num);

                if (!string.IsNullOrEmpty(row[0].ToString()) && isnumber)
                {
                    var xVal = Math.Abs(Convert.ToDouble(row[0]));

                    
                    if (xVal > 100000 && DivXAmnt >= 100000)
                    {
                        obj.ShowYInhndreds = true; showYinHundreds = true; DivXAmnt = 10000;
                    }
                    else if (xVal > 10000 && DivXAmnt >= 10000)
                    {
                        obj.ShowYInhndreds = true; showYinHundreds = true; DivXAmnt = 1000;
                    }
                    else if (xVal > 1000 && DivXAmnt >=1000)
                    {
                        obj.ShowYInhndreds = true; showYinHundreds = true; DivXAmnt = 100;
                    }







                }

                //if (showXinHundreds)
                //{
                //    switch (DivXAmnt)
                //    {
                //        case 100: obj.XAxis = XAxis + "(In Hundreds)"; break;
                //        case 1000: obj.XAxis = XAxis + "(In Thousands)"; break;
                //        case 10000: obj.XAxis = XAxis + "(In Ten Thousands)"; break;

                //    }

                //}
                //else
                //{
                //    obj.XAxis = XAxis;
                //}
                if (dt.Columns.Count > 1&&chartID!=9)
                {
                    isnumber = Double.TryParse(row[1].ToString(), out num);

                    if (!string.IsNullOrEmpty(row[1].ToString()) && isnumber)
                    {
                        var yVal = Math.Abs(Convert.ToDouble(row[1]));
                         
                        if (yVal > 100000 && (DivYAmnt >= 100000|| DivYAmnt==0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 10000;
                        }
                        else if (yVal > 10000 &&( DivYAmnt >= 10000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 1000;
                        }
                        else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 100;
                        }





                    }
                    obj.yValues = row[1].ToString();
                    obj.ySecondColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                }
                if (dt.Columns.Count > 2)
                {
                    isnumber = Double.TryParse(row[2].ToString(), out num);


                    if (!string.IsNullOrEmpty(row[2].ToString()) && isnumber)
                    {
                        var yVal = Math.Abs(Convert.ToDouble(row[2]));

                        if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 10000;
                        }
                        else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 1000;
                        }
                        else if (yVal > 1000 && (DivYAmnt >=1000 || DivYAmnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 100;
                        }
                         

                    }
                    obj.ySecondValues = row[2].ToString();
                    obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                }
                if (dt.Columns.Count > 3)
                {
                    if (chartID == 9)
                    {
                        isnumber = Double.TryParse(row[3].ToString(), out num);

                        if (!string.IsNullOrEmpty(row[3].ToString()) && isnumber)
                        {
                            var yVal = Math.Abs(Convert.ToDouble(row[3]));

                            if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 10000;
                            }
                            else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 1000;
                            }
                            else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 100;
                            }





                        }
                        obj.yValues = row[3].ToString();
                        obj.ySecondColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));

                    }
                    else
                    {
                        isnumber = Double.TryParse(row[3].ToString(), out num);

                        if (!string.IsNullOrEmpty(row[3].ToString()) && isnumber)
                        {
                            var yVal = Math.Abs(Convert.ToDouble(row[3]));
                            if (yVal > 100000 && (DivY3Amnt >= 100000 || DivY3Amnt == 0))
                            {
                                obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 10000;
                            }
                            else if (yVal > 10000 && (DivY3Amnt >= 10000 || DivY3Amnt == 0))
                            {
                                obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 1000;
                            }
                            else if (yVal > 1000 && (DivY3Amnt >= 1000 || DivY3Amnt == 0))
                            {
                                obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 100;
                            }
                        }
                        obj.yThirdValues = row[3].ToString();
                        obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    }
                }

                if (showYinHundreds)
                {
                    switch (DivYAmnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;

                    }

                }
                else if (showY2inHundreds)
                {
                    switch (DivY2Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else if (showY3inHundreds)
                {
                    switch (DivY3Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else
                {
                    obj.YAxis = YAxis;
                }
                if (showXinHundreds)
                {
                    switch (DivXAmnt)
                    {
                        case 100: obj.XAxis = XAxis + " (In Hundreds)"; break;
                        case 1000: obj.XAxis = XAxis + " (In Thousands)"; break;
                        case 10000: obj.XAxis = XAxis + " (In Ten Thousands)"; break;
                        case 0: obj.XAxis = XAxis; break;
                    }

                }
                else
                {
                    obj.XAxis = XAxis;
                }
                obj.ChartModalName = ChartModalID;
                chartList.Add(obj);
            }
            foreach (var item in chartList.Where(w => w.ChartID == chartID))
            {
                item.ShowYInhndreds = showYinHundreds;
                item.ShowY2Inhndreds = showY2inHundreds;
                item.ShowXInhndreds = showXinHundreds;
                item.DevideXAmnt = DivXAmnt;
                if (showYinHundreds)
                {
                    item.DevideYAmnt = DivYAmnt;
                }
                else if (showY2inHundreds)
                {
                    item.DevideYAmnt = DivY2Amnt;
                }
                else if (showY3inHundreds)
                {
                    item.DevideYAmnt = DivY3Amnt;
                }
            }
        }

        private void CreateSalesCompareChartData(DataTable dt, long chartID, string elementID, string ChartModalID, string XAxis, string YAxis, ref List<HomeModel.ChartListData> chartList)
        {
            bool showYinHundreds = false;
            bool showY2inHundreds = false;
            bool showY3inHundreds = false;
            bool showXinHundreds = false;
            long DivYAmnt = 0;
            long DivY2Amnt = 0;
            long DivY3Amnt = 0;
            long DivXAmnt = 0;
            foreach (DataRow row in dt.Rows)
            {
                HomeModel.ChartListData obj = new HomeModel.ChartListData();
                obj.ChartID = chartID;
                obj.ChartName = elementID;
                obj.xValues = "Period";
                obj.yValues = row[1].ToString();
                obj.yColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                obj.XAxis = XAxis;
                    double num;
                var isnumber = Double.TryParse(row[0].ToString(), out num);

                if (!string.IsNullOrEmpty(row[0].ToString()) && isnumber)
                {
                    var xVal = Math.Abs(Convert.ToDouble(row[0]));


                    if (xVal > 100000 && DivXAmnt >= 100000)
                    {
                        obj.ShowYInhndreds = true; showYinHundreds = true; DivXAmnt = 10000;
                    }
                    else if (xVal > 10000 && DivXAmnt >= 10000)
                    {
                        obj.ShowYInhndreds = true; showYinHundreds = true; DivXAmnt = 1000;
                    }
                    else if (xVal > 1000 && DivXAmnt >= 1000)
                    {
                        obj.ShowYInhndreds = true; showYinHundreds = true; DivXAmnt = 100;
                    }
                    

                }


                if (dt.Columns.Count > 1 && chartID != 9)
                {
                    isnumber = Double.TryParse(row[0].ToString(), out num);

                    if (!string.IsNullOrEmpty(row[0].ToString()) && isnumber)
                    {
                        var yVal = Math.Abs(Convert.ToDouble(row[1]));

                        if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 10000;
                        }
                        else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 1000;
                        }
                        else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                        {
                            obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 100;
                        }





                    }
                    obj.yValues = row[0].ToString();
                    obj.ySecondColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    //    obj.yValues = row[1].ToString();
                    //    obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    //}
                }
                //if (dt.Columns.Count > 2)
                //{
                    isnumber = Double.TryParse(row[1].ToString(), out num);


                    if (!string.IsNullOrEmpty(row[1].ToString()) && isnumber)
                    {
                        var yVal = Math.Abs(Convert.ToDouble(row[1]));

                        if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 10000;
                        }
                        else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 1000;
                        }
                        else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                        {
                            obj.ShowY2Inhndreds = true; showY2inHundreds = true; DivY2Amnt = 100;
                        }


                    }
                    obj.ySecondValues = row[1].ToString();
                    obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                //}
                if (dt.Columns.Count > 3)
                {
                    if (chartID == 9)
                    {
                        isnumber = Double.TryParse(row[3].ToString(), out num);

                        if (!string.IsNullOrEmpty(row[3].ToString()) && isnumber)
                        {
                            var yVal = Math.Abs(Convert.ToDouble(row[3]));

                            if (yVal > 100000 && (DivYAmnt >= 100000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 10000;
                            }
                            else if (yVal > 10000 && (DivYAmnt >= 10000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 1000;
                            }
                            else if (yVal > 1000 && (DivYAmnt >= 1000 || DivYAmnt == 0))
                            {
                                obj.ShowYInhndreds = true; showYinHundreds = true; DivYAmnt = 100;
                            }





                        }
                        obj.yValues = row[3].ToString();
                        obj.ySecondColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));

                    }
                    else
                    {
                        isnumber = Double.TryParse(row[3].ToString(), out num);

                        if (!string.IsNullOrEmpty(row[3].ToString()) && isnumber)
                        {
                            var yVal = Math.Abs(Convert.ToDouble(row[3]));
                            if (yVal > 100000 && (DivY3Amnt >= 100000 || DivY3Amnt == 0))
                            {
                                obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 10000;
                            }
                            else if (yVal > 10000 && (DivY3Amnt >= 10000 || DivY3Amnt == 0))
                            {
                                obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 1000;
                            }
                            else if (yVal > 1000 && (DivY3Amnt >= 1000 || DivY3Amnt == 0))
                            {
                                obj.ShowY3Inhndreds = true; showY3inHundreds = true; DivY3Amnt = 100;
                            }
                        }
                        obj.yThirdValues = row[3].ToString();
                        obj.yThirdColor = String.Format("#{0:X6}", randomColor.Next(0x1000000));
                    }
                }

                if (showYinHundreds)
                {
                    switch (DivYAmnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;

                    }

                }
                else if (showY2inHundreds)
                {
                    switch (DivY2Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else if (showY3inHundreds)
                {
                    switch (DivY3Amnt)
                    {
                        case 100: obj.YAxis = YAxis + " (In Hundreds)"; break;
                        case 1000: obj.YAxis = YAxis + " (In Thousands)"; break;
                        case 10000: obj.YAxis = YAxis + " (In Ten Thousands)"; break;
                        case 0: obj.YAxis = YAxis; break;
                    }

                }
                else
                {
                    obj.YAxis = YAxis;
                }
                if (showXinHundreds)
                {
                    switch (DivXAmnt)
                    {
                        case 100: obj.XAxis = XAxis + " (In Hundreds)"; break;
                        case 1000: obj.XAxis = XAxis + " (In Thousands)"; break;
                        case 10000: obj.XAxis = XAxis + " (In Ten Thousands)"; break;
                        case 0: obj.XAxis = XAxis; break;
                    }

                }
                else
                {
                    obj.XAxis = XAxis;
                }
                obj.ChartModalName = ChartModalID;
                chartList.Add(obj);
            }
            foreach (var item in chartList.Where(w => w.ChartID == chartID))
            {
                item.ShowYInhndreds = showYinHundreds;
                item.ShowY2Inhndreds = showY2inHundreds;
                item.ShowXInhndreds = showXinHundreds;
                item.DevideXAmnt = DivXAmnt;
                if (showYinHundreds)
                {
                    item.DevideYAmnt = DivYAmnt;
                }
                else if (showY2inHundreds)
                {
                    item.DevideYAmnt = DivY2Amnt;
                }
                else if (showY3inHundreds)
                {
                    item.DevideYAmnt = DivY3Amnt;
                }
            }
        }
        [HttpPost]
        public ActionResult LeadStages()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.GetDashboardDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetDashboardData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = 2,
                Period = 1
            });

            return Json(objInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ServiceStages()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.GetDashboardDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetDashboardData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = 3,
                Period = 1
            });

            return Json(objInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ProjectStatus()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.GetDashboardDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetDashboardData
            {
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = 4,
                Period = 1
            });

            List<HomeModel.ProjectData> objList = new List<HomeModel.ProjectData>();
            if (objInfo.Data != null)
            {
                if (objInfo.Data.Count > 0)
                {
                    var random = new Random();
                    foreach (var data in objInfo.Data)
                    {
                        HomeModel.ProjectData obj = new HomeModel.ProjectData();
                        obj.Label = data.Field.ToString();
                        obj.Data = Convert.ToDecimal(data.Value);
                        obj.Color = String.Format("#{0:X6}", random.Next(0x1000000));
                        objList.Add(obj);
                    }
                }
            }


            return Json(new { objInfo.Process, objList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InventoryStatus(int month, int year, int mode)
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.GetInventoryDashBoardList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetInventoryDashboardData
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                UserCode = _userLoginInfo.ID_Users,
                Month = month,
                Year = year
            });

            List<HomeModel.ProjectData> objList = new List<HomeModel.ProjectData>();
            if (objInfo.Data != null)
            {
                if (objInfo.Data.Count > 0)
                {
                    var random = new Random();
                    foreach (var data in objInfo.Data.Where(m => m.DashBoard == mode).ToList())
                    {
                        HomeModel.ProjectData obj = new HomeModel.ProjectData();
                        obj.Label = data.Field.ToString();
                        obj.Data = Convert.ToDecimal(data.Value);
                        obj.Color = String.Format("#{0:X6}", random.Next(0x1000000));
                        objList.Add(obj);
                    }
                }
            }


            return Json(new { objInfo.Process, objList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MenuList(long m, long s = 0)
        {
            Session["MenuGroupID"] = m;
            ViewBag.MenuGroupID = m;
            ViewBag.SubMenuGroupID = s;
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            long[] sr = new long[2];
            sr[0] = m;
            sr[1] = s;
            if (m != 0)
            {
                return View(sr);
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        public ActionResult GetDashBoardDetails()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CommonModel product = new CommonModel();

            var outputList = product.GetCountData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.DashBoardList { UserCode = _userLoginInfo.EntrBy });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDashBoardGraphDetails(Int32 Mode)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CommonModel product = new CommonModel();


            var outputGraphList = product.GetGraphData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.DashBoardGraphList { EntrBy = _userLoginInfo.EntrBy, Mode = Mode, FK_Employee = _userLoginInfo.FK_Employee, FK_Company = _userLoginInfo.FK_Company, FK_Branch = _userLoginInfo.FK_Branch, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(outputGraphList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProjectDashBoardDetails(Int32 Mode)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CommonModel product = new CommonModel();


            var outputGraphList = product.GetProjectGraphData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.DashBoardGraphList { EntrBy = _userLoginInfo.EntrBy, Mode = Mode, FK_Employee = _userLoginInfo.FK_Employee, FK_Company = _userLoginInfo.FK_Company, FK_Branch = _userLoginInfo.FK_Branch, FK_Machine = _userLoginInfo.FK_Machine });


            return Json(outputGraphList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProjectDashBoardGraphDetails(Int32 Mode)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CommonModel product = new CommonModel();


            var outputGraphList = product.GetProjectGraphData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.DashBoardGraphList { EntrBy = _userLoginInfo.EntrBy, Mode = Mode, FK_Employee = _userLoginInfo.FK_Employee, FK_Company = _userLoginInfo.FK_Company, FK_Branch = _userLoginInfo.FK_Branch, FK_Machine = _userLoginInfo.FK_Machine });


            var tst = new List<CommonModel.MainPrjectDash>();

            foreach (var mn in outputGraphList.Data.GroupBy(v => v.Project).ToList())
            {
                decimal sum = 0;
                var tstt = new CommonModel.MainPrjectDash();
                tstt.StageDetails = new List<CommonModel.ProjectDashBoardGraphOut>();
                tstt.ProjectName = mn.FirstOrDefault().Project;
                tstt.ShortName = mn.FirstOrDefault().ShortName;

                if (tstt.ProjectName != null)
                {
                    foreach (var stg in outputGraphList.Data.Where(v => v.Project == tstt.ProjectName).ToList())
                    {
                        sum += stg.Percentage;
                        tstt.StageDetails.Add(new CommonModel.ProjectDashBoardGraphOut { Stage = stg.Stage, Percentage = stg.Percentage });
                    }
                }
                tstt.Count = 0;// sum;

                tst.Add(tstt);
            }


            return Json(tst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadQuickMenu()
        {


            List<CommonModel.MenuList> menuList = new List<CommonModel.MenuList>();

            //call load menu list function here



            // menuList = getDummyData().Where(a => a.MenuGroupID == 2 || a.MenuGroupID==1 || a.MenuListID<20).ToList();//<-----this is adummy datat which is same as link give in the previous menu

            menuList = getDummyData().Where(a => a.MenuListID < 6).ToList();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            return PartialView("_QuickAccessBar", menuList);
        }

        public ActionResult UserProfile()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ProfileModel.ProfileInput Profile = new ProfileModel.ProfileInput();
            ProfileModel profilemodel = new ProfileModel();

            DataTable ProfileData = new DataTable();

            //        var Data = Common.GetDataViaQuery<ProfileModel.ProfileDetails>(parameters: new APIParameters
            //        {
            //            TableName = "Employee E JOIN EmployeeType ET ON ET.ID_EmployeeType=E.FK_EmployeeType AND ET.Cancelled=0" +
            //            "JOIN  Users U ON U.FK_Employee=E.ID_Employee AND U.Cancelled=0" +
            //            "LEFT JOIN Country AS C ON C.ID_Country=E.FK_Country AND C.Cancelled=0" +
            //            "LEFT JOIN States AS S ON S.ID_States=E.FK_State AND S.Cancelled=0" +
            //            "LEFT JOIN District AS D ON D.ID_District=E.FK_District AND D.Cancelled=0" +
            //            "LEFT JOIN Post AS P ON P.ID_Post=E.FK_Post AND P.Cancelled=0" +
            //            "LEFT JOIN Area AS A ON A.ID_Area=P.FK_Area AND A.Cancelled=0" +
            //            "JOIN Designation DE ON DE.ID_Designation=E.FK_Designation " +
            //            "JOIN Department DP ON DP.ID_Department=E.FK_Department",
            //            SelectFields = "EmpCode,EmpFName+' '+ISNULL(EmpLName,'') as EmpName,E.EmpEmrgContactNum,ET.EmptyName,ISNULL(D.DtName,'')+','+ISNULL(P.PostName,'')+','+ISNULL(A.AreaName,'')+','+ISNULL(P.PinCode,'') Address,DE.DesName," +
            //            "convert(varchar(10), UserLoginAttemptDate, 103) LastonlineDate,RIGHT(CONVERT(CHAR(20), U.UserLoginAttemptDate, 22), 11) LastOnlineTime, DP.DeptName",
            //            Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company + " AND U.UserCode='" + _userLoginInfo.EntrBy + "'",
            //            SortFields = "ID_Employee",
            //            GroupByFileds = ""
            //        },
            //companyKey: _userLoginInfo.CompanyKey

            //   );

            var Data = profilemodel.GetUserProfileData(companyKey: _userLoginInfo.CompanyKey, input: new ProfileModel.GetProfileData
            {
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_User = _userLoginInfo.ID_Users,
                EntrBy = _userLoginInfo.EntrBy,

            });
            List<ProfileModel.ProfileDetails> List = new List<ProfileModel.ProfileDetails>();
            if (Data.Data[0] != null)
            {
                //ProfileData.Columns.Add("EmpCode");
                //ProfileData.Columns.Add("EmpName");
                //ProfileData.Columns.Add("EmpEmrgContactNum");
                //ProfileData.Columns.Add("EmptyName");
                //ProfileData.Columns.Add("Address");
                //ProfileData.Columns.Add("DesName");
                //DataRow row = null;

                //foreach (var rowObj in Data.Data)
                //{
                //    row = ProfileData.NewRow();
                //    ProfileData.Rows.Add(rowObj.EmpCode, rowObj.EmpName, rowObj.EmpEmrgContactNum, rowObj.EmptyName, rowObj.Address, rowObj.DesName);
                //}
                List = Data.Data;

            }

            return PartialView("_Userprofile", List);
        }





        List<CommonModel.MenuList> getDummyData()
        {
            List<CommonModel.MenuList> menuList = new List<CommonModel.MenuList>();
            // fiv a ="<div class="col"><div class="widget-icon"><button class="notif-btn"><i class="fa fa-tachometer"></i> </button></div></div>"
            //call load menu list function here

            menuList.Add(new CommonModel.MenuList { MenuListID = 47, MenuListName = "Organization", MenuListLink = Url.Action("Index", "Organization"), TransMode = "Organization", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-address-book", SubList = 9, MenuGroupSubName = "Security" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 46, MenuListName = "Company", MenuListLink = Url.Action("CompanyIndex", "Company"), TransMode = "Company", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home", SubList = 9, MenuGroupSubName = "Security" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 44, MenuListName = "Branch Type", MenuListLink = Url.Action("BranchtypeIndex", "Branchtype"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-bars", SubList = 9, MenuGroupSubName = "Security" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 43, MenuListName = "Branch", MenuListLink = Url.Action("Index", "Branch"), TransMode = "Branch", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-university", SubList = 9, MenuGroupSubName = "Security" });




            menuList.Add(new CommonModel.MenuList { MenuListID = 1, MenuListName = "Country", MenuListLink = Url.Action("Index", "Country"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\country.png", MenuIconImage = "fa fa-globe", SubList = 1, MenuGroupSubName = "Region" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 2, MenuListName = "State", MenuListLink = Url.Action("Index", "State"), TransMode = "State", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-map-o", SubList = 1, MenuGroupSubName = "Region" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 42, MenuListName = "District", MenuListLink = Url.Action("DistrictIndex", "District"), TransMode = "District", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-location-arrow", SubList = 1, MenuGroupSubName = "Region" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 33, MenuListName = "Post", MenuListLink = Url.Action("Post", "Post"), TransMode = "Post", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-pencil-square", SubList = 1, MenuGroupSubName = "Region" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 4, MenuListName = "Place", MenuListLink = Url.Action("Place", "Place"), TransMode = "Place", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 4, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-map-marker", SubList = 1, MenuGroupSubName = "Region" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 56, MenuListName = "Area", MenuListLink = Url.Action("Index", "Area"), TransMode = "", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-area-chart", SubList = 1, MenuGroupSubName = "Region", });

            menuList.Add(new CommonModel.MenuList { MenuListID = 5, MenuListName = "Customer Type", MenuListLink = Url.Action("CustomerType", "CustomerType"), TransMode = "CustomerType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 5, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-address-card-o", SubList = 2, MenuGroupSubName = "Customer" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 3, MenuListName = "Customer", MenuListLink = Url.Action("Index", "Customer"), TransMode = "Customer", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 3, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-users", SubList = 2, MenuGroupSubName = "Customer" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 16, MenuListName = "Customer Category", MenuListLink = Url.Action("Index", "CustomerCategory"), TransMode = "CustomerCategory", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-address-card", SubList = 2, MenuGroupSubName = "Customer" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 45, MenuListName = "Customer Sector", MenuListLink = Url.Action("CustomerSectorIndex", "CustomerSector"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user-circle-o", SubList = 2, MenuGroupSubName = "Customer" });




            menuList.Add(new CommonModel.MenuList { MenuListID = 7, MenuListName = "Employee Type", MenuListLink = Url.Action("EmployeeType", "EmployeeType"), TransMode = "EmployeeType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 7, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-sitemap", SubList = 3, MenuGroupSubName = "Employee" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 6, MenuListName = "Designation", MenuListLink = Url.Action("Designation", "Designation"), TransMode = "Designation", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 6, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-map-marker", SubList = 3, MenuGroupSubName = "Employee" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 67, MenuListName = "Qualification", MenuListLink = Url.Action("Index", "Qualification"), TransMode = "Qualification", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 6, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-map-marker", SubList = 3, MenuGroupSubName = "Employee" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 71, MenuListName = "Occupation", MenuListLink = Url.Action("Index", "Occupation"), TransMode = "Occupation", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 6, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-map-marker", SubList = 3, MenuGroupSubName = "Employee" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 21, MenuListName = "Employee Level", MenuListLink = Url.Action("Index", "EmployeeLevel"), TransMode = "EmployeeLevel", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user-plus", SubList = 3, MenuGroupSubName = "Employee" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 54, MenuListName = "Employee", MenuListLink = Url.Action("Index", "EmployeeCreation"), TransMode = "Employee", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 3, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user", SubList = 3, MenuGroupSubName = "Employee" });


            menuList.Add(new CommonModel.MenuList { MenuListID = 8, MenuListName = "ProcessLevel", MenuListLink = Url.Action("ProcessLevel", "ProcessLevel"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-podcast" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 9, MenuListName = "Supplier", MenuListLink = Url.Action("Index", "Supplier"), TransMode = "Supplier", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-truck", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 10, MenuListName = "Language", MenuListLink = Url.Action("Language", "Language"), TransMode = "Language", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-language", SubList = 6, MenuGroupSubName = "Others" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 11, MenuListName = "Category", MenuListLink = Url.Action("Category", "Category"), TransMode = "Category", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-list-alt", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 20, MenuListName = "Sub Category", MenuListLink = Url.Action("SubCategory", "SubCategory"), TransMode = "SubCategory", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-object-group", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 66, MenuListName = "Product", MenuListLink = Url.Action("Index", "Product"), TransMode = "Product", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-product-hunt", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 34, MenuListName = "Product Unit", MenuListLink = Url.Action("Index", "Unit"), TransMode = "Unit", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-filter" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 28, MenuListName = "Project Category", MenuListLink = Url.Action("Index", "ProjectCategory"), TransMode = "ProjectCategory", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-th-list", SubList = 4, MenuGroupSubName = "Production" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 29, MenuListName = "Measurement Unit", MenuListLink = Url.Action("Index", "MeasurementUnit"), TransMode = "MeasurementUnit", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-thermometer-half", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 30, MenuListName = "Measurement Type", MenuListLink = Url.Action("Index", "MeasurementType"), TransMode = "MeasurementType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-hourglass-end", SubList = 5, MenuGroupSubName = "Inventory" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 12, MenuListName = "BillType", MenuListLink = Url.Action("BillType", "BillType"), TransMode = "BillType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-list-alt", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 13, MenuListName = "IdentityProof", MenuListLink = Url.Action("IdentityProof", "IdentityProof"), TransMode = "IdentityProof", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-id-card", SubList = 3, MenuGroupSubName = "Employee" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 14, MenuListName = "Manufacturer", MenuListLink = Url.Action("Manufacturer", "Manufacturer"), TransMode = "Manufacturer", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-industry", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 15, MenuListName = "Warranty Setting", MenuListLink = Url.Action("warrantysettingindex", "warrantysettings"), TransMode = "warrantysettingindex", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs", SubList = 5, MenuGroupSubName = "Inventory" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 16, MenuListName = "Customer Category", MenuListLink = Url.Action("Index", "CustomerCategory"), TransMode = "CustomerCategory", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 17, MenuListName = "Media Master", MenuListLink = Url.Action("Index", "MediaMaster"), TransMode = "MediaMaster", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-camera-retro", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 18, MenuListName = "Department", MenuListLink = Url.Action("Department", "Department"), TransMode = "Department", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-th-large", SubList = 3, MenuGroupSubName = "Employee" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 19, MenuListName = "Security Type", MenuListLink = Url.Action("SecurityTypeIndex", "SecurityType"), TransMode = "SecurityType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-key", SubList = 4, MenuGroupSubName = "Production" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 20, MenuListName = "Sub Category", MenuListLink = Url.Action("SubCategory", "SubCategory"), TransMode = "SubCategory", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-newspaper-o" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 21, MenuListName = "Employee Level", MenuListLink = Url.Action("Index", "EmployeeLevel"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user-plus" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 22, MenuListName = "Request Type", MenuListLink = Url.Action("Index", "RequestType"), TransMode = "RequestType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-question-circle", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 23, MenuListName = "User Role", MenuListLink = Url.Action("Index", "UserRole"), TransMode = "UserRole", MenuGroupID = 6, MenuGroupName = "Security", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 24, MenuListName = "Reason", MenuListLink = Url.Action("Index", "Reason"), TransMode = "Reason", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-wpforms", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 25, MenuListName = "Tax Type", MenuListLink = Url.Action("Index", "TaxType"), TransMode = "TaxType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-percent", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 39, MenuListName = "Extra Work Type", MenuListLink = Url.Action("ExtraWorkIndex", "ExtraWorkType"), TransMode = "ExtraWorkType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-briefcase", SubList = 4, MenuGroupSubName = "Production" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 41, MenuListName = "Document Type", MenuListLink = Url.Action("DocumentIndex", "DocumentType"), TransMode = "DocumentType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-file-text", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 48, MenuListName = "Other Charge Type", MenuListLink = Url.Action("Index", "OtherChargeType"), TransMode = "OtherChargeType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-money", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 35, MenuListName = "Warranty Type", MenuListLink = Url.Action("WarrantyTypeIndex", "WarrantyType"), TransMode = "WarrantyType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-certificate", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 37, MenuListName = "Followup  Action", MenuListLink = Url.Action("Index", "NextAction"), TransMode = "NextAction", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-forward", SubList = 8, MenuGroupSubName = "Lead" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 38, MenuListName = "Followup Type", MenuListLink = Url.Action("Index", "ActionType"), TransMode = "ActionType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-plus-square-o", SubList = 8, MenuGroupSubName = "Lead" });




            menuList.Add(new CommonModel.MenuList { MenuListID = 40, MenuListName = "Complaint List", MenuListLink = Url.Action("ComplaintList", "ComplaintList"), TransMode = "ComplaintList", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-th-list", SubList = 7, MenuGroupSubName = "Services" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 32, MenuListName = "Complaint Check List", MenuListLink = Url.Action("ComplaintCheckListIndex", "ComplaintCheckList"), TransMode = "ComplaintCheckList", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-list-ol", SubList = 7, MenuGroupSubName = "Services" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 26, MenuListName = "Services", MenuListLink = Url.Action("Index", "Services"), TransMode = "Services", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cog", SubList = 7, MenuGroupSubName = "Services" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 27, MenuListName = "Maintenance", MenuListLink = Url.Action("Index", "Maintenance"), TransMode = "Maintenance", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-wrench", SubList = 7, MenuGroupSubName = "Services" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 31, MenuListName = "Incidence", MenuListLink = Url.Action("Index", "Incidence"), TransMode = "Incidence", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-hourglass-end", SubList = 7, MenuGroupSubName = "Services" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 32, MenuListName = "Complaint Check List", MenuListLink = Url.Action("ComplaintCheckListIndex", "ComplaintCheckList"), TransMode = "ComplaintCheckList", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-list-ol", SubList = 7, MenuGroupSubName = "Services" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 33, MenuListName = "Post", MenuListLink = Url.Action("Post", "Post"), TransMode = "Post", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-pencil-square" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 34, MenuListName = "Unit", MenuListLink = Url.Action("Index", "Unit"), TransMode = "Unit", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-filter", SubList = 5, MenuGroupSubName = "Inventory" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 35, MenuListName = "Warranty Type", MenuListLink = Url.Action("WarrantyTypeIndex", "WarrantyType"), TransMode = "WarrantyType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-wrench", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 36, MenuListName = "Model", MenuListLink = Url.Action("ModelIndex", "Model"), TransMode = "Model", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-share-alt", SubList = 5, MenuGroupSubName = "Inventory" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 71, MenuListName = "Finance Plan Type", MenuListLink = Url.Action("FinancePlanType", "FinancePlanType"), TransMode = "FinancePlanType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-money", SubList = 5, MenuGroupSubName = "Inventory" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 37, MenuListName = "Next Action", MenuListLink = Url.Action("Index", "NextAction"), TransMode = "NextAction", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-forward", SubList = 8, MenuGroupSubName = "Lead" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 38, MenuListName = "Action Type", MenuListLink = Url.Action("Index", "ActionType"), TransMode = "ActionType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-plus-square-o", SubList = 8, MenuGroupSubName = "Lead" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 39, MenuListName = "Extra Work Type", MenuListLink = Url.Action("ExtraWorkIndex", "ExtraWorkType"), TransMode = "ExtraWorkType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-briefcase" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 40, MenuListName = "Complaint List", MenuListLink = Url.Action("ComplaintList", "ComplaintList"), TransMode = "ComplaintList", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-th-list", SubList = 7, MenuGroupSubName = "Services" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 41, MenuListName = "Document Type", MenuListLink = Url.Action("DocumentIndex", "DocumentType"), TransMode = "DocumentType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-file-text", SubList = 6, MenuGroupSubName = "Others" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 42, MenuListName = "District", MenuListLink = Url.Action("DistrictIndex", "District"), TransMode = "District", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-location-arrow" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 43, MenuListName = "Branch", MenuListLink = Url.Action("Index", "Branch"), TransMode = "Branch", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-university" });
            //  menuList.Add(new CommonModel.MenuList { MenuListID = 44, MenuListName = "Branch Type", MenuListLink = Url.Action("BranchtypeIndex", "Branchtype"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 45, MenuListName = "Customer Sector", MenuListLink = Url.Action("CustomerSectorIndex", "CustomerSector"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user-circle-o" });
            //  menuList.Add(new CommonModel.MenuList { MenuListID = 46, MenuListName = "Company", MenuListLink = Url.Action("CompanyIndex", "Company"), TransMode = "Company", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            //  menuList.Add(new CommonModel.MenuList { MenuListID = 47, MenuListName = "Organization", MenuListLink = Url.Action("Index", "Organization"), TransMode = "Organization", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 48, MenuListName = "Other Charge Type", MenuListLink = Url.Action("Index", "OtherChargeType"), TransMode = "OtherChargeType", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 49, MenuListName = "Service Mapping", MenuListLink = Url.Action("Index", "ServiceMapping"), TransMode = "ServiceMapping", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-money", SubList = 7, MenuGroupSubName = "Services" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 15, MenuListName = "Warranty Setting", MenuListLink = Url.Action("warrantysettingindex", "warrantysettings"), TransMode = "warrantysettingindex", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-certificate", SubList = 5 });
            menuList.Add(new CommonModel.MenuList { MenuListID = 8, MenuListName = "Process Level", MenuListLink = Url.Action("ProcessLevel", "ProcessLevel"), TransMode = "Country", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-podcast", SubList = 4, MenuGroupSubName = "Production" });


            //menuList.Add(new CommonModel.MenuList { MenuListID = 23, MenuListName = "User Role", MenuListLink = Url.Action("Index", "UserRole"), TransMode = "UserRole", MenuGroupID = 6, MenuGroupName = "Security", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });


            menuList.Add(new CommonModel.MenuList { MenuListID = 50, MenuListName = "Customer Service", MenuListLink = Url.Action("Registration", "Service"), TransMode = "Service", MenuGroupID = 2, MenuGroupName = "Service", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 51, MenuListName = "Service Assign", MenuListLink = Url.Action("Serviceassign", "Service"), TransMode = "Country", MenuGroupID = 2, MenuGroupName = "Service", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 52, MenuListName = "Lead Generation", MenuListLink = Url.Action("LeadGen", "LeadGeneration"), TransMode = "LeadGeneration", MenuGroupID = 3, MenuGroupName = "Lead", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-puzzle-piece" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 53, MenuListName = "Lead Management", MenuListLink = Url.Action("Index", "LeadManagement"), TransMode = "LeadManagement", MenuGroupID = 3, MenuGroupName = "Lead", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-handshake-o" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 54, MenuListName = "Purchase", MenuListLink = Url.Action("Index", "Purchase"), TransMode = "Inventory", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-shopping-cart" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 55, MenuListName = "Sales", MenuListLink = Url.Action("Index", "Sales"), TransMode = "Inventory", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-shopping-bag" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 56, MenuListName = "Area", MenuListLink = Url.Action("Index", "Area"), TransMode = "", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-area-chart" });
            //menuList.Add(new CommonModel.MenuList { MenuListID = 57, MenuListName = "Area", MenuListLink = Url.Action("Index", "Users"), TransMode = "", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-area-chart" });
            // menuList.Add(new CommonModel.MenuList { MenuListID = 58, MenuListName = "Product Wise Services", MenuListLink = Url.Action("ProductWiseService", "ProductWiseService"), TransMode = "ProductWiseService", MenuGroupID = 2, MenuGroupName = "Service", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 59, MenuListName = "Payment Method", MenuListLink = Url.Action("PaymentMethod", "PaymentMethod"), TransMode = "PaymentMethod", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-area-chart", SubList = 6, MenuGroupSubName = "Others" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 60, MenuListName = "Report Settings", MenuListLink = Url.Action("ReportSetting", "ReportSetting"), TransMode = "Settings", MenuGroupID = 5, MenuGroupName = "Settings", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 61, MenuListName = "Report View", MenuListLink = Url.Action("ReportView", "ReportSetting"), TransMode = "Report", MenuGroupID = 7, MenuGroupName = "Report", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 62, MenuListName = "Lead Management", MenuListLink = Url.Action("CustomerTickets", "CustomerTickets"), TransMode = "Report", MenuGroupID = 7, MenuGroupName = "Report", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });

            //  menuList.Add(new CommonModel.MenuList { MenuListID = 66, MenuListName = "Ledger Report", MenuListLink = Url.Action("Ledger", "CustomerTickets"), TransMode = "Report", MenuGroupID = 7, MenuGroupName = "Report", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 63, MenuListName = "User  Creation", MenuListLink = Url.Action("Index", "Users"), TransMode = "User", MenuGroupID = 6, MenuGroupName = "Security", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 64, MenuListName = "User Password Reset", MenuListLink = Url.Action("Index", "UserPasswordreset"), TransMode = "UserPasswordreset", MenuGroupID = 6, MenuGroupName = "Security", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 65, MenuListName = "Opening Stock", MenuListLink = Url.Action("Index", "OpeningStock"), TransMode = "Opening Stock", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-shopping-cart" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 66, MenuListName = "User Ban / Un Ban", MenuListLink = Url.Action("Index", "UserBanned"), TransMode = "UserBanned", MenuGroupID = 6, MenuGroupName = "Security", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 72, MenuListName = "User Policy Settings", MenuListLink = Url.Action("UserPolicySettings", "UserPolicySettings"), TransMode = "Settings", MenuGroupID = 5, MenuGroupName = "Settings", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 68, MenuListName = "Other Company", MenuListLink = Url.Action("Index", "OtherCompany"), TransMode = "OtherCompany", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-user", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 69, MenuListName = "Stock Transfer", MenuListLink = Url.Action("Index", "EmployeeStockTransfer"), TransMode = "Inventory", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-shopping-cart" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 70, MenuListName = "Common Numbering Settings", MenuListLink = Url.Action("NumberGenerationCommon", "NumberGenerationCommon"), TransMode = "Settings", MenuGroupID = 5, MenuGroupName = "Settings", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 58, MenuListName = "Product Wise Services", MenuListLink = Url.Action("ProductWiseService", "ProductWiseService"), TransMode = "Service", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home", SubList = 7, MenuGroupSubName = "Services" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 73, MenuListName = "Service Follow Up", MenuListLink = Url.Action("Index", "Servicelist"), TransMode = "Service", MenuGroupID = 2, MenuGroupName = "Service", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-home" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 78, MenuListName = "Customer Wise Product Mapping", MenuListLink = Url.Action("Index", "CustomerProductMapping"), TransMode = "CustomerProductMapping", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-sliders", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 70, MenuListName = "Service", MenuListLink = Url.Action("ServiceReport", "ServiceReport"), TransMode = "Report", MenuGroupID = 7, MenuGroupName = "Report", SortOrder = 3, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 74, MenuListName = "Tax Group", MenuListLink = Url.Action("Index", "TaxGrouping"), TransMode = "TaxGroup", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-percent", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 75, MenuListName = "Project Stages", MenuListLink = Url.Action("Index", "ProjectStages"), TransMode = "ProjectStages", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cubes", SubList = 5, MenuGroupSubName = "Inventory" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 1, MenuListName = "Site Visit", MenuListLink = Url.Action("Index", "SiteVisit"), TransMode = "SiteVisit", MenuGroupID = 10, MenuGroupName = "Project", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-male" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 77, MenuListName = "Sales Order", MenuListLink = Url.Action("Index", "SalesOrder"), TransMode = "", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 6, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-shopping-basket" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 79, MenuListName = "Finance Plan", MenuListLink = Url.Action("Index", "Finance"), TransMode = "Finance", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-calculator", SubList = 5, MenuGroupSubName = "Inventory" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 80, MenuListName = "Priority", MenuListLink = Url.Action("Index", "Priority"), TransMode = "Priority", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-th-large", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 81, MenuListName = "Local Supplier", MenuListLink = Url.Action("Index", "LocalSupplier"), TransMode = "LocalSupplier", MenuGroupID = 1, MenuGroupName = "Master", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-truck", SubList = 6, MenuGroupSubName = "Others" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 2, MenuListName = "Cost Preparation", MenuListLink = Url.Action("Index", "CostPreparation"), TransMode = "CostPreparation", MenuGroupID = 10, MenuGroupName = "Project", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-calculator" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 3, MenuListName = "Project Creation", MenuListLink = Url.Action("Index", "ProjectCreation"), TransMode = "ProjectCreation", MenuGroupID = 10, MenuGroupName = "Project", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-newspaper-o" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 4, MenuListName = "Team Creation", MenuListLink = Url.Action("Index", "TeamCreation"), TransMode = "TeamCreation", MenuGroupID = 10, MenuGroupName = "Project", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-users" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 5, MenuListName = "Project wise Stages", MenuListLink = Url.Action("Index", "ProjectwiseStages"), TransMode = "ProjectwiseStages", MenuGroupID = 10, MenuGroupName = "Project", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-sort-amount-asc" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 82, MenuListName = "Purchase Order", MenuListLink = Url.Action("Index", "PurchaseOrder"), TransMode = "", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 6, MenuListVisible = true, MenuIcon = "\\Assets\\images\\order-placed-purchased-icon.png", MenuIconImage = "fa fa-list-ul" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 62, MenuListName = "Project", MenuListLink = Url.Action("ProjectReports", "ProjectReports"), TransMode = "Report", MenuGroupID = 7, MenuGroupName = "Report", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-newspaper-o" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 83, MenuListName = "Quotation", MenuListLink = Url.Action("MenuList", "Home", new { m = 83, s = 83 }), TransMode = "", MenuGroupID = 4, MenuGroupName = "Inventory", SortOrder = 7, MenuListVisible = true, MenuIcon = "\\Assets\\images\\order-placed-purchased-icon.png", MenuIconImage = "fa fa-cart-arrow-down" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 84, MenuListName = "Inward", MenuListLink = Url.Action("Inward", "Quotation"), TransMode = "", MenuGroupID = 83, MenuGroupName = "Inventory", SortOrder = 5, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cart-arrow-down", SubList = 83, MenuGroupSubName = "Quotation" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 85, MenuListName = "Outward", MenuListLink = Url.Action("Outward", "Quotation"), TransMode = "", MenuGroupID = 83, MenuGroupName = "Inventory", SortOrder = 3, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cart-arrow-down", SubList = 83, MenuGroupSubName = "Quotation" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 86, MenuListName = "Menu", MenuListLink = Url.Action("MenuList", "Home", new { m = 86, s = 86 }), TransMode = "", MenuGroupID = 5, MenuGroupName = "Settings", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 87, MenuListName = "Menu Group", MenuListLink = Url.Action("MenuGroup", "Menu"), TransMode = "", MenuGroupID = 86, MenuGroupName = "Settings", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs", SubList = 86, MenuGroupSubName = "Menu Settings" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 88, MenuListName = "Menu List", MenuListLink = Url.Action("MenuList", "Menu"), TransMode = "", MenuGroupID = 86, MenuGroupName = "Settings", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs", SubList = 86, MenuGroupSubName = "Menu Settings" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 89, MenuListName = "Menu Access", MenuListLink = Url.Action("Index", "UserMenuAccess"), TransMode = "", MenuGroupID = 86, MenuGroupName = "Settings", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs", SubList = 86, MenuGroupSubName = "Menu Settings" });
            menuList.Add(new CommonModel.MenuList { MenuListID = 90, MenuListName = "tree", MenuListLink = Url.Action("AJAXDemo", "jsTree3"), TransMode = "", MenuGroupID = 86, MenuGroupName = "Settings", SortOrder = 2, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-cogs", SubList = 86, MenuGroupSubName = "Menu Settings" });

            menuList.Add(new CommonModel.MenuList { MenuListID = 6, MenuListName = "Material Allocation", MenuListLink = Url.Action("Index", "MaterialAllocation"), TransMode = "MaterialAllocation", MenuGroupID = 10, MenuGroupName = "Project", SortOrder = 1, MenuListVisible = true, MenuIcon = "\\Assets\\images\\profile\\17.jpg", MenuIconImage = "fa fa-balance-scale" });

            return menuList;
        }

        public ActionResult LoadMenuList(long menuID, long subid)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CommonModel obj = new CommonModel();
            var mainMenu = obj.GetMainMenuData(input: new CommonModel.GetMainMenu
            {
                FK_UserGroup = _userLoginInfo.FK_UserRole,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_MenuGroup = menuID,
                FK_SubMenu = subid
            }, companyKey: _userLoginInfo.CompanyKey);


            CommonModel.MainMenuList objList = new CommonModel.MainMenuList();
            List<CommonModel.MenuList> ListData = new List<CommonModel.MenuList>();
            CommonMethod objCmnMethod = new CommonMethod();
            if (mainMenu.Data != null)
            {
                foreach (var temp in mainMenu.Data)
                {
                    CommonModel.MenuList objMenuList = temp;
                    if (temp.MenuListPara.Length > 0)
                    {
                        if ((temp.TransMode.Length) > 0)
                        {
                            objMenuList.MenuListLink = Url.Action(temp.ActionData, temp.ControllerName, new { mgrp = objCmnMethod.EncryptString(temp.TransMode) }) + "?" + temp.MenuListPara;
                        }
                        else
                        {
                            objMenuList.MenuListLink = Url.Action(temp.ActionData, temp.ControllerName) + "?" + temp.MenuListPara;
                        }
                    }
                    else
                    {
                        if ((temp.TransMode.Length) > 0)
                        {
                            objMenuList.MenuListLink = Url.Action(temp.ActionData, temp.ControllerName, new { mtd = objCmnMethod.EncryptString(temp.MenuListName), mgrp = objCmnMethod.EncryptString(temp.TransMode) });
                        }
                        else
                        {
                            objMenuList.MenuListLink = Url.Action(temp.ActionData, temp.ControllerName, new { mtd = objCmnMethod.EncryptString(temp.MenuListName) });
                        }

                    }

                    ListData.Add(objMenuList);
                }
                objList.MainMenu = ListData.AsEnumerable();
                if (objList.MainMenu.FirstOrDefault().SubList > 0)
                {
                    objList.HeaderName = objList.MainMenu.FirstOrDefault().MenuGroupSubName;
                }
                else
                {

                    objList.HeaderName = objList.MainMenu.FirstOrDefault().MenuGroupName;
                }
            }
            ViewBag.Companyname = _userLoginInfo.Company;
            return PartialView("_MenuList", objList);
        }
        public ActionResult OpenPage(string ctrl, string act, string grp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            Session["MenuGroupID"] = grp;
            return RedirectToAction(act, ctrl);
        }
        public string CompanyInfo()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            return _userLoginInfo.Company;
        }
        public string CompanyLogoInfo()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;

            if (_userLoginInfo.CompanyLogo == null || _userLoginInfo.CompanyLogo == "" || _userLoginInfo.CompanyLogo == "AAAAAA==")
            {
                return null;
            }
            else
            {
                string image = "data:image /; base64," + _userLoginInfo.CompanyLogo;
                return image;
            }
           
        }
        public string CompanyLogonameInfo()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
           // ViewBag.Companyname = _userLoginInfo.Company;

            //if (_userLoginInfo.Companyname == null || _userLoginInfo.CompanyLogo == "" || _userLoginInfo.CompanyLogo == "AAAAAA==")
            //{
            //    return null;
            //}
            //else
            //{
            //    string image = "data:image /; base64," + _userLoginInfo.CompanyLogo;
            //    return image;
            //}
            return _userLoginInfo.Company;

        }
        public string BranchInfo()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            return _userLoginInfo.Branch;
        }
        public string Userinfo()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            return _userLoginInfo.UserName;
        }

        public string UserImage()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            if (_userLoginInfo.UserImage == null || _userLoginInfo.UserImage == "")
            {
                return null;
            }
            else
            {
                string image = "data:image /; base64," + _userLoginInfo.UserImage;
                return image;
            }

        }

        public string UserRole()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            return _userLoginInfo.UserRole;
        }

        public ActionResult LoadMenuListV(long menuID, long SubList)
        {

            List<CommonModel.MenuList> menuList = new List<CommonModel.MenuList>();


            //call the database funciton to get the menu list of the curresponding menugroup and aother access right and stuff 
            menuList = getDummyData().Where(a => a.MenuGroupID == menuID && a.SubList == SubList).ToList();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];//<-----this is adummy datat which is same as link give in the previous menu
            ViewBag.Companyname = _userLoginInfo.Company;
            return PartialView("_MenuList", menuList);
        }

        public ActionResult LoadPage(string menuName)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            CommonModel com = new CommonModel();
            List<CommonModel.MenuList> menuList = new List<CommonModel.MenuList>();
            var objSearchMenu = com.GetPages(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.PageInput { FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_UserGroup = _userLoginInfo.FK_UserRole, MenuName = menuName, ID_MenuList = 0 });

            List<CommonModel.SearchMenuList> ListData = new List<CommonModel.SearchMenuList>();
            CommonMethod objCmnMethod = new CommonMethod();
            if (objSearchMenu.Data != null)
            {
                foreach (var temp in objSearchMenu.Data)
                {
                    CommonModel.SearchMenuList objMenuList = temp;
                    if (temp.TransMode.Length > 0)
                    {
                        objMenuList.MenuListLink = Url.Action(temp.ActionData, temp.ControllerName, new { mtd = objCmnMethod.EncryptString(temp.MenuListName), mgrp = objCmnMethod.EncryptString(temp.TransMode) });
                    }
                    else
                    {
                        objMenuList.MenuListLink = Url.Action(temp.ActionData, temp.ControllerName, new { mtd = objCmnMethod.EncryptString(temp.MenuListName) });
                    }
                    //objMenuList.MenuListLink = Url.Action("GetMenuData", "Home",new { gid = temp.MenuListID });
                    ListData.Add(objMenuList);
                }
            }

            return Json(new { ListData }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetMenuData(int gid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CommonModel com = new CommonModel();
            List<CommonModel.MenuList> menuList = new List<CommonModel.MenuList>();
            var objSearchMenu = com.GetPages(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.PageInput { FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_UserGroup = _userLoginInfo.FK_UserRole, MenuName = "", ID_MenuList = gid });

            List<CommonModel.SearchMenuList> ListData = new List<CommonModel.SearchMenuList>();
            CommonMethod objCmnMethod = new CommonMethod();
            if (objSearchMenu.Data != null)
            {
                string key = "MenuGroupID";
                Session.Remove(key);
                Session[key] = objSearchMenu.Data[0].FK_MenuGroup;
                return RedirectToAction(objSearchMenu.Data[0].ActionData, objSearchMenu.Data[0].ControllerName);

            }
            ViewBag.Companyname = _userLoginInfo.Company;
            return RedirectToAction("Dashboard");
        }

        public ActionResult LoadMenuGroup()
        {

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            CommonModel obj = new CommonModel();
            var menuGroups = obj.GetMenuGroupData(input: new CommonModel.GetMenuGroup
            {
                FK_UserGroup = _userLoginInfo.FK_UserRole,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            }, companyKey: _userLoginInfo.CompanyKey);
            CommonModel.SideMenuList objList = new CommonModel.SideMenuList();
            objList.SideMenu = menuGroups.Data.AsEnumerable();
            return PartialView("_MenuGroup", objList);

        }

        public ActionResult GetServiceDashBoardGraphDetails(Int32 Mode)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            CommonModel productservice = new CommonModel();


            var outputGraphList = productservice.GetServiceGraphData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.DashBoardGraphList { EntrBy = _userLoginInfo.EntrBy, Mode = Mode, FK_Employee = _userLoginInfo.FK_Employee, FK_Company = _userLoginInfo.FK_Company, FK_Branch = _userLoginInfo.FK_Branch, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(outputGraphList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetInventroyDashBoardGraphDetails(Int32 month, Int32 year)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            CommonModel inventory = new CommonModel();


            var outputGraphList = inventory.GetInventoryGraphData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.DashBoardInventoryGraphList { UserCode = _userLoginInfo.EntrBy, Month = month, Year = year, FK_Company = _userLoginInfo.FK_Company, FK_Branch = _userLoginInfo.FK_Branch, FK_Department = _userLoginInfo.FK_Department });

            return Json(outputGraphList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetInventroyDashBoard()
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            CommonModel inventory = new CommonModel();


            return PartialView("DashboardInventroy");
        }

        [HttpPost]
        public void SetSessionTransModeData(string key, string value)
        {
            Session.Remove(key);
            Session[key] = value;
        }
        [HttpPost]
        public void SetSessionData(string key, string value)
        {
            Session.Remove(key);
            CommonMethod objCmnMethod = new CommonMethod();
            Session[key] = objCmnMethod.Encrypt(value);
        }
        [HttpGet]
        public ActionResult GetSessionData(string key)
        {

            string result = "";
            CommonMethod objCmnMethod = new CommonMethod();
            if (Session[key] != null)
            {
                result = objCmnMethod.Decrypt(Session[key].ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void clearSessionData(string key)
        {
            Session.Remove(key);
        }
        public string NotificationCount()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            HomeModel objHome = new HomeModel();
            string result = "";
            var objInfo = objHome.GetNotificationDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetNotificationData
            {
                FK_Company = _userLoginInfo.FK_Company,
                RecievedBy = _userLoginInfo.ID_Users

            });
            if (objInfo.Data != null)
            {
                result = Convert.ToString(objInfo.Data[0].TotalCount);
            }

            return result;
        }
        public ActionResult Notification()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.GetNotificationDataList(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetNotificationData
            {
                FK_Company = _userLoginInfo.FK_Company,
                RecievedBy = _userLoginInfo.ID_Users

            });
            List<HomeModel.NotificationDataList> ListData = new List<HomeModel.NotificationDataList>();
            if (objInfo.Data != null)
            {
                foreach (var temp in objInfo.Data)
                {
                    HomeModel.NotificationDataList objNList = temp;
                    if (temp.EmpImgValue == null || temp.EmpImgValue == "")
                    {
                        objNList.EmpImgValue = "";
                    }
                    else
                    {
                        objNList.EmpImgValue = "data:image /; base64," + temp.EmpImgValue;
                    }
                    ListData.Add(objNList);
                }
            }
            var jsonResult = Json(new { objInfo.Process, ListData }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;

            //return Json(new { objInfo.Process, ListData }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult NotificationList()
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            var objInfo = objHome.GetNotificationDataListWithRead(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetNotificationData
            {
                FK_Company = _userLoginInfo.FK_Company,
                RecievedBy = _userLoginInfo.ID_Users

            });
            List<HomeModel.NotificationDataList> ListData = new List<HomeModel.NotificationDataList>();
            if (objInfo.Data != null)
            {
                foreach (var temp in objInfo.Data)
                {
                    HomeModel.NotificationDataList objNList = temp;
                    if (temp.EmpImgValue == null || temp.EmpImgValue == "")
                    {
                        objNList.EmpImgValue = "";
                    }
                    else
                    {
                        objNList.EmpImgValue = "data:image /; base64," + temp.EmpImgValue;
                    }
                    ListData.Add(objNList);
                }
            }
           
            var jsonResult = Json(new { objInfo.Process, ListData }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
           return jsonResult;
           // return Json(new { objInfo.Process, ListData }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string NotificationUpdate(int ID)
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            var objInfo = objHome.UpdateNotificationData(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.NotificationUpdate
            {
                FK_Company = _userLoginInfo.FK_Company,
                ID_Notification = ID
            });
            string result = NotificationCount();
            return result;
        }
        [HttpPost]
        public string NotificationDelete(int ID)
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            var objInfo = objHome.NotificationDataDelete(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.NotificationDelete
            {
                FK_Company = _userLoginInfo.FK_Company,
                ID_Notification = ID,
                EnterBy = _userLoginInfo.EntrBy
            });
            string result = NotificationCount();
            return result;
        }
        [HttpPost]
        public ActionResult GetAccountNo(string submodule, int type)
        {
            HomeModel objHome = new HomeModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            var objInfo = objHome.GetAccountNo(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetNumberGeneration
            {
                Submodule = submodule,
                TransDate = DateTime.Now,
                fk_Company = _userLoginInfo.FK_Company,
                fk_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Type = type
            });

            return Json(new { objInfo });
        }
        [HttpPost]
        public ActionResult NotificationCreate(int reciverID, int leadID, string transMode, string title, string msg)
        {
            HomeModel objHome = new HomeModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var objInfo = objHome.CreateNotification(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.CreateNotificationData
            {
                FK_Company = _userLoginInfo.FK_Company,
                Reciever = reciverID,
                Sender = _userLoginInfo.ID_Users,
                Title = title,
                Message = msg,
                TransMode = transMode,
                FK_Master = leadID
            });
            try
            {
                Common.SendMobileNotification(companyKey: _userLoginInfo.CompanyKey);
            }
            catch (Exception ex)
            {
            }
            ViewBag.Companyname = _userLoginInfo.Company;
            return Json(new { Process = objInfo }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCompanyDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            return Json(new { _userLoginInfo.Company, _userLoginInfo.Branch, _userLoginInfo.EntrBy }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LoadTrackInfo(long masterID, string tmode)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            HomeModel objHome = new HomeModel();

            var timeModule = objHome.GetTrackInfo(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetTimeLineChart
            {
                TransMode = tmode,
                ID_Master = masterID
            });
            HomeModel.TimeLineVM objVMData = new HomeModel.TimeLineVM();
            objVMData.TimeLineData = timeModule.Data.AsEnumerable();
            return PartialView("_LoadTrackInfo", objVMData);
        }

        public ActionResult UploadDocs(long masterID, string tmode)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            HomeModel objHome = new HomeModel();

            var timeModule = objHome.GetFileUpload(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetFileUploadDocs
            {
                TransMode = tmode,
                ID_Master = masterID
            });
            HomeModel.FileUploadDocs objVMData = new HomeModel.FileUploadDocs();
            objVMData.FileUploadDocsData = timeModule.Data.AsEnumerable();
            return PartialView("_LoadFileUpload", objVMData);
        }

        public ActionResult SetUploadedFiles(HomeModel.FileUploads data)
        {
            if (data.FileUploadDocList != null)
            {

                Session["DocsImage"] = Common.xmlTostring(data.FileUploadDocList);

            }
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            return Json(JsonRequestBehavior.AllowGet);
        }



        public ActionResult LoadLedgerInfo(long FK_Customer, long FK_OtherCustomer, long ID_Bill, string Transmode, DateTime Reg_Date)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Companyname = _userLoginInfo.Company;
            HomeModel objHome = new HomeModel();

            var LedgerModule = objHome.GetLedgerInfoData(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetLedgerInfo
            {
                FK_Customer = FK_Customer,
                FK_CustomerOthers = FK_OtherCustomer,
                BillNo = ID_Bill,
                TransMode = Transmode,
                AsOnDate = Reg_Date
            });
            ViewBag.CustomerDetails = LedgerModule.Data[0].CustomerDetails;
            ViewBag.BillDetails = LedgerModule.Data[0].BillDetails;
            ViewBag.ProductDetailsTitle = LedgerModule.Data[0].ProductDetailsTitle;
            ViewBag.ProductDetails = Convert.ToBoolean(LedgerModule.Data[0].ProductDetails);
            ViewBag.LoanDetails = LedgerModule.Data[0].LoanDetails;
            ViewBag.TransactionDetailsTitle = LedgerModule.Data[0].TransactionDetailsTitle;
            ViewBag.TransactionDetails = Convert.ToBoolean(LedgerModule.Data[0].TransactionDetails);
            ViewBag.FK_Master = LedgerModule.Data[0].FK_Master;

            if (Convert.ToBoolean(LedgerModule.Data[0].ProductDetails))
            {
                var LedgProductInfo = objHome.GetLedgerProductDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetLedgerProductDetails
                {
                    FK_Master = ViewBag.FK_Master,
                    TransMode = Transmode,
                    Mode = 1
                });
                ViewBag.ProductData = LedgProductInfo.Data;
            }
            if (Convert.ToBoolean(LedgerModule.Data[0].TransactionDetails))
            {
                var LedgTransInfo = objHome.GetLedgerProductDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new HomeModel.GetLedgerProductDetails
                {
                    FK_Master = ViewBag.FK_Master,
                    TransMode = Transmode,
                    Mode = 2
                });
                ViewBag.TransactionData = LedgTransInfo.Data;
            }
            return PartialView("_LoadLedgerInfo");
        }
        public ActionResult LoadPages(string mid, string tmode)
        {
            #region ::  Check User Session to verifyLogin  ::
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ViewBag.Companyname = _userLoginInfo.Company;
            CommonModel obj = new CommonModel();
            var Menu = obj.GetMenuFromNotification(input: new CommonModel.GetFromNotficationMenu
            {
                FK_Company = _userLoginInfo.FK_Company,
                TransMode = tmode
            }, companyKey: _userLoginInfo.CompanyKey);
            string MnuLstName = "", ControllerName = "", url = "";
            if (Menu.Data != null)
            {
                MnuLstName = Menu.Data[0].MnuLstName;
                ControllerName = Menu.Data[0].ControllerName;
                url = Menu.Data[0].Url;
                Common.SetCorrectionData(tmode, mid);
            }
            CommonMethod objCmnMethod = new CommonMethod();
            if (MnuLstName != "" && tmode != "")
            {
                return RedirectToAction(Menu.Data[0].Url, Menu.Data[0].ControllerName, new { mtd = objCmnMethod.EncryptString(MnuLstName), mgrp = objCmnMethod.EncryptString(tmode) });
            }
            return RedirectToAction("Dashboard");

        }
    }






}
