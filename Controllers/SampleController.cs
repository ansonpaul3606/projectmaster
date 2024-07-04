using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.ViewModel;

using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;

using PerfectWebERP.ReportModel;
using System.Net;

namespace PerfectWebERP.Controllers
{
    public class SampleController : Controller
    {
        // GET: Sample
        public ActionResult Index()
        {
            UserLoginInfo setUserLoginInfo = new UserLoginInfo
            {
                EntrBy = "code",
                FK_Machine = 100,
                CompanyKey = "",
                FK_Company = 1,
                FK_Branch = 2,
                FK_BranchCodeUser = 2,
                UserName = "Amith",
                UserRole = "Super Admin",
                UserAvatar = "/assets/images/profile/17.jpg"
            };
            Session["UserLoginInfo"] = setUserLoginInfo;//<set session

            ViewBag.Username = setUserLoginInfo.UserName;
            ViewBag.UserRole = setUserLoginInfo.UserRole;
            ViewBag.UserAvatar = setUserLoginInfo.UserAvatar;

            return View();
        }


        public ActionResult LoadFormSample()
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



            List<Place> placelist = new List<Place>();
            for (int i = 0; i < 20; i++)
            {
                placelist.Add(new Place
                {
                    PlaceID = i,
                    PlaceName = $"Place{i}"
                });
            };


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


            APIParameters apiParameters = new APIParameters
            {
                TableName = "[CustomerType]",
                SelectFields = "[ID_CustomerType] AS CustomerTitleID,[CtName] AS CustomerTitleName",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            };

            var customerTitle=Common.GetDataViaQuery<CustomerTitle>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameters);


            SampleCustomerView sampleCustomerView = new SampleCustomerView
            {
                CustomerTitlesList = customerTitle.Data,
                PlaceList = placelist,
                StateList = stateList.Data
            };


            return PartialView("_AddSampleCustomer", sampleCustomerView);
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

            ModelState.Remove("CustomerID");
            ModelState.Remove("Place");
            ModelState.Remove("State");
            ModelState.Remove("District");
            ModelState.Remove("CustomerTitle");
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

            //save Customer

           var outputData= Common.UpdateTableData<Tabless.CustomerSample>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCustomerUpdate",parameter:new Tabless.CustomerSample
           {
                FK_Company = _userLoginInfo.FK_Company,
                BranchCode = _userLoginInfo.FK_Branch,
                UserAction = 1,//since insert set to 1 | update=2
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_Customer = 0,
                CusTitle = newCutomerDetails.CustomerTitle,
                CusName = newCutomerDetails.CustomerName,
                FK_Place = newCutomerDetails.PlaceID,
                FK_District = newCutomerDetails.DistrictID,
                FK_State = newCutomerDetails.StateID,
                CusPhone = newCutomerDetails.Phone,
                cusMobile = newCutomerDetails.Mobile,
                CusDOB = newCutomerDetails.DateOfBirth,
                UserCode = _userLoginInfo.EntrBy,
                CusDate = newCutomerDetails.CustomerDate,
                AuditData = "",
                BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                SqlUpdateQuery = ""

            });


            return Json(outputData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDistrict()
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

            var data = Common.GetDataViaQuery<Customer>(parameters: new APIParameters
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
        public ActionResult GetCustomerByID(int customerID)
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
            short ID_CUSTOMER = (short)customerID;
            var cusInfo = Common.GetDataViaProcedure<Tabless.CustomerSample, InputCustomer>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCustomerSelect", parameter: new InputCustomer { ID_Customer = (short)customerID });
            //var customer= Common.GetProcedureData<Tables.Customer, int>(string companyKey, string procedureName, ID_CUSTOMER)
            return Json(new APIGetRecordsDynamic<Customer> {
                Process= cusInfo.Process,
                Data= cusInfo.Data.Select(a=> new Customer {
                    CustomerName =a.CusName,
                    CustomerID =a.ID_Customer,
                    CustomerTitle =a.CusTitle,
                    CustomerDate =a.CusDate,
                    DateOfBirth =a.CusDOB,
                    DistrictID=(int)a.FK_District,
                    PlaceID=(int)a.FK_Place,
                    StateID=(int)a.FK_State,
                    Phone=a.CusPhone,
                    Mobile=a.cusMobile

                }).ToList()

            }, JsonRequestBehavior.AllowGet);
        }




        public ActionResult GetRepPDF()
        {


            List<SampleTable> samples = new List<SampleTable>();
            for (int i = 0; i < 10; i++)
            {
                samples.Add(new SampleTable
                {
                    id = i,
                    Field1 = "Field Name A" + i,
                    Field2 = "Field Name b" + i,
                    Field3 = "Field Name c" + i,
                    Field4 = "Field Name d" + i,
                    Field5 = "Field Name e" + i,
                    Field6 = "Field Name f" + i,
                    Field7 = "Field Name g" + i,
                    Field8 = "Field Name h" + i,
                    Field9 = "Field Name i" + i
                });
            }
            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Sample1.rpt"));
            rd.SetDataSource(samples);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "sample.pdf");



        }

        public ActionResult GetSampleReport(int countryID)
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


            StateModel state = new StateModel();

            var outputData = state.GetStateData(input: new StateModel.GetState
            {
                FK_States = 0,
                FK_Country = countryID,

                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""

            }, companyKey: _userLoginInfo.CompanyKey);

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SampleState.rpt"));
            rd.SetDataSource(outputData.Data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "SatateList.pdf");


        }

        ///////   Edit   //////
        
        public ActionResult GetCountrySampleReport(string Rptype)
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


            CountryModel country = new CountryModel();

            var outputData = country.GetCountryData(input: new CountryModel.ID
            {
                FK_Country = 0,
                //FK_Country = countryID,

                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                TransMode = "",


            }, companyKey: _userLoginInfo.CompanyKey);

            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Country.rpt"));
            //rd.SetDataSource(outputData.Data);
            Session["Reportname"] = "Country.rpt";
            Session["Rptype"] = Rptype;
            Session["Reportdata"] = outputData.Data;
            Session["filename"] = "Samplenew";
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            return Redirect("~/Reports/LoadRpt.aspx?Rptype="+Rptype);



        }

        ///////////////////
       
        //public ActionResult GetCustomerTicketsReport(/*CustomerTicketsModel.customerview data*/ string FK_Employee, string Rptype,string FromDate, string ToDate)
        //{
            
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::


        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    try
        //    {

        //        CustomerTicketsModel country = new CustomerTicketsModel();

        //        var outputData = country.GetCustomerReport(input: new CustomerTicketsModel.customerid
        //        {

        //            FK_Company = _userLoginInfo.FK_Company,
        //            FK_Branch = _userLoginInfo.FK_Branch,
        //            FK_Employee = /*data.*/FK_Employee,
        //            FromDate = Convert.ToDateTime(/*data.*/FromDate),
        //            ToDate = Convert.ToDateTime(/*data.*/ToDate),
        //            Status = 0

        //        }, companyKey: _userLoginInfo.CompanyKey);

        //        //ReportDocument rd = new ReportDocument();
        //        //rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Country.rpt"));
        //        //rd.SetDataSource(outputData.Data);
        //        Session["Reportname"] = "CustTicket.rpt";
        //        Session["Rptype"] = /*data.*/Rptype;
        //        Session["Reportdata"] = outputData.Data;
        //        Session["filename"] = "CustomerTicket";
        //        Response.Buffer = false;
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        if(outputData.Data==null)
        //        {
        //            // new HttpStatusCodeResult(HttpStatusCode.BadRequest)
        //            return Content("No Data Found");                
        //        }

        //        return Redirect("~/Reports/LoadRpt.aspx?Rptype=" + /*data.*/Rptype);

        //    }
        //    catch (Exception ex)
        //    {
        //         throw;
        //    }

        //}



    }
}
