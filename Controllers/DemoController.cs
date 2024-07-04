using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.ViewModel;

namespace PerfectWebERP.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult DemoIndex()
        {
            UserLoginInfo setUserLoginInfo = new UserLoginInfo
            {
                UserCode = "code",
                FK_Machine = 100,
                CompanyKey = "",
                FK_Company = 1,
                BranchCode = 2,
                BranchUserCode = 2,
                UserName = "Perfect",
                UserRole = "Super Admin",
                UserAvatar = "/assets/images/profile/17.jpg"
            };
            Session["UserLoginInfo"] = setUserLoginInfo;//<set session

            ViewBag.Username = setUserLoginInfo.UserName;
            ViewBag.UserRole = setUserLoginInfo.UserRole;
            ViewBag.UserAvatar = setUserLoginInfo.UserAvatar;

            return View();


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
            var cusInfo = Common.GetDataViaProcedure<Tables.Customer, InputCustomer>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCustomerSelect", parameter: new InputCustomer { ID_Customer = (short)customerID });
            //var customer= Common.GetProcedureData<Tables.Customer, int>(string companyKey, string procedureName, ID_CUSTOMER)
            return Json(new APIGetRecordsDynamic<Customer>
            {
                Process = cusInfo.Process,
                Data = cusInfo.Data.Select(a => new Customer
                {
                    CustomerName = a.CusName,
                    CustomerID = a.ID_Customer,
                    CustomerTitle = a.CusTitle,
                    CustomerDate = a.CusDate,
                    DateOfBirth = a.CusDOB,
                    DistrictID = (int)a.FK_District,
                    PlaceID = (int)a.FK_Place,
                    StateID = (int)a.FK_State,
                    Phone = a.CusPhone,
                    Mobile = a.cusMobile

                }).ToList()

            }, JsonRequestBehavior.AllowGet);
        }

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

            var customerTitle = Common.GetDataViaQuery<CustomerTitle>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameters);


            SampleCustomerView sampleCustomerView = new SampleCustomerView
            {
                CustomerTitlesList = customerTitle.Data,
                PlaceList = placelist,
                StateList = stateList.Data
            };


            return PartialView("_DemoView", sampleCustomerView);
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

            var outputData = Common.UpdateTableData<Tables.Customer>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCustomerUpdate", parameter: new Tables.Customer
            {
                FK_Company = _userLoginInfo.FK_Company,
                BranchCode = _userLoginInfo.BranchCode,
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
                UserCode = _userLoginInfo.UserCode,
                CusDate = newCutomerDetails.CustomerDate,
                AuditData = "",
                BranchCodeUser = _userLoginInfo.BranchUserCode,
                SqlUpdateQuery = ""

            });


            return Json(outputData, JsonRequestBehavior.AllowGet);
        }


    }
}