using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(string mtd,string mgrp)
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

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
          
            ViewBag.mtd = mtd;

            ViewBag.TransMode = mGrp;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            var MenuList = Common.GetDataViaQuery<QuotationModel.Menulist>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode",
                Criteria = "Cancelled=0 AND Passed=1 AND ControllerName='Customer'AND Url='Index' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MenuList",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            ViewBag.SharePagetransmode = MenuList.Data;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }


        public ActionResult LoadCustomerForm(string mtd)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            CustomerCModel.CustomerListModel CustomerList = new CustomerCModel.CustomerListModel();

            //BranchTypeList

            var branchTypeList = Common.GetDataViaQuery<CustomerCModel.BranchTypelist>(parameters: new APIParameters
            {
                TableName = "BranchType B",
                SelectFields = "B.ID_BranchType as BranchTypeID,B.BTName AS BranchType",
              //  Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company,
                Criteria= "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );

            CustomerList.BranchTypeList = branchTypeList.Data;


            //CustomerTypeList

          //  var customerList = Common.GetDataViaQuery<CustomerCModel.CustomerTypelist>(parameters: new APIParameters
          //  {
          //      TableName = "CustomerType C",
          //      SelectFields = "C.ID_CustomerType AS  CustomerTypeID,C.CustyName AS CustomerType",
          //      Criteria = "C.Cancelled=0 AND C.Passed=1 AND C.FK_Company=" + _userLoginInfo.FK_Company,
          //      GroupByFileds = "",
          //      SortFields = ""
          //  },
          //companyKey: _userLoginInfo.CompanyKey

          //);

          //  CustomerList.CustomerTypeList = customerList.Data;

            //OccupationList

            var occupationList = Common.GetDataViaQuery<CustomerCModel.Occupationlist>(parameters: new APIParameters
            {
                TableName = "Occupation OC",
                SelectFields = "OC.ID_Occupation AS OccupationID,OC.OccpName AS[Occupation]",
                Criteria = "OC.Cancelled=0 AND OC.Passed=1 AND OC.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            CustomerList.OccupationList = occupationList.Data;

            //var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            //companyKey: _userLoginInfo.CompanyKey,
            //procedureName: "ProGetNextNo",
            //parameter: new NextSortOrder
            //{
            //    TableName = "Customer",
            //    FieldName = "SortOrder",
            //    Debug = 0
            //});

            //CustomerCModel model = new CustomerCModel();
            //var CusNo = model.GetCustomerNo(input:new CustomerCModel.inputGetCustomerNo {FK_Company=_userLoginInfo.FK_Company, Submodule= "MSCUS"},companyKey:_userLoginInfo.CompanyKey );
            //CustomerList.CustomerNumber = CusNo.Data[0].CustomerNumber;
            //CustomerList.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddCustomerCreationForm", CustomerList);

        }


        [HttpPost]
        public ActionResult GetPgCustomerList(int pageSize, int pageIndex, string Name)
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

            string transMode = "";

            CustomerCModel custType = new CustomerCModel();
            var data = custType.GetCustomerData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerCModel.CustomerID
            {

                FK_Customer = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode

            });

           // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetCustomerInfoByID(CustomerCModel.CustomerIDView data)
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


            CustomerCModel customer = new CustomerCModel();

            var customerInfo = customer.GetCustomerData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerCModel.CustomerID
            {
                TransMode = "",
                PageSize = 0,
                PageIndex = 0,
                FK_Customer = data.CustomerID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            });

            return Json(customerInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewCustomer(CustomerCModel.CustomerView data)
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
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("Occupation");
            ModelState.Remove("CustomerType");
            ModelState.Remove("LeadGenerate");
            ModelState.Remove("LeadGenerateID");
            ModelState.Remove("LeadGenerateNo");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("SlNo");
           ModelState.Remove("Individual");
            if (data.Individual)
            {
                ModelState.Remove("ContactPerson");
                ModelState.Remove("ContactMobile");
                ModelState.Remove("ContactEmail");
            }
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


            CustomerCModel customer = new CustomerCModel();



            var datresponse = customer.UpdateCustomerData(input: new CustomerCModel.UpdateCustomer
            {
                UserAction = 1,
                CusAddress1 = data.Address1,
               
                CusContactEmail = data.ContactEmail,
                CusContactMobile = data.ContactMobile,
                CusContactPerson = data.ContactPerson,
                CusEmail = data.Email,
                CusGSTINNo = data.GSTINNo,
              
                CusMobile = data.Mobile,
               
                CusName = data.Name,
                CusNumber = data.Number,
                CusPhone = data.Phone,
              
               
                FK_Branch = data.BranchID,
               
                FK_CustomerType = data.CustomerTypeID,
                //FK_Occupation = data.OccupationID
                 FK_Occupation = (data.OccupationID.HasValue) ? data.OccupationID.Value : 0,
                FK_LeadGenerate = data.LeadGenerateID,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
               // FK_Area = data.AreaID,
                //FK_Post = data.PostID,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,

                ID_Customer = 0,
                FK_Customer = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CusLandmark=data.Landmark,
                TransMode = data.TransMode,
                CusReferenceNo = data.CusReferenceNo,
                //IGSTCust=data.IGSTCust

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustomer(CustomerCModel.CustomerView data)
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
            ModelState.Remove("ReasonID");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("Occupation");
            ModelState.Remove("CustomerType");
            ModelState.Remove("LeadGenerate");
            ModelState.Remove("LeadGenerateID");
            ModelState.Remove("LeadGenerateNo");
            ModelState.Remove("SlNo");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("Individual");
            if (data.Individual)
            {
                ModelState.Remove("ContactPerson");
                ModelState.Remove("ContactMobile");
                ModelState.Remove("ContactEmail");
            }
          
            //ModelState.Remove("CustomerSector");


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

            CustomerCModel customer = new CustomerCModel();



            var datresponse = customer.UpdateCustomerData(input: new CustomerCModel.UpdateCustomer
            {
                UserAction = 2,
               
                CusAddress1 = data.Address1,

                CusContactEmail = data.ContactEmail,
                CusContactMobile = data.ContactMobile,
                CusContactPerson = data.ContactPerson,
                CusEmail = data.Email,
                CusGSTINNo = data.GSTINNo,

                CusMobile = data.Mobile,

                CusName = data.Name,
                CusNumber =data.Number,
                CusPhone = data.Phone,


                FK_Branch = data.BranchID,
               
                FK_CustomerType = data.CustomerTypeID,
                FK_Occupation = (data.OccupationID.HasValue) ? data.OccupationID.Value : 0,
                FK_LeadGenerate = data.LeadGenerateID,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
                // FK_Area = data.AreaID,
                //FK_Post = data.PostID
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,

                ID_Customer = data.CustomerID,
                FK_Customer  =data.CustomerID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CusLandmark = data.Landmark,
                TransMode = data.TransMode,
                CusReferenceNo=data.CusReferenceNo,
                //IGSTCust=data.IGSTCust
               
                //EntrOn = DateTime.UtcNow

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCustomerOthers(CustomerCModel.CustomerView data)
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
            ModelState.Remove("ReasonID");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("Occupation");
            ModelState.Remove("CustomerType");
            ModelState.Remove("LeadGenerate");
            ModelState.Remove("LeadGenerateID");
            ModelState.Remove("LeadGenerateNo");
            ModelState.Remove("SlNo");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("BranchID");
            ModelState.Remove("Individual");
            if (data.Individual)
            {
                ModelState.Remove("ContactPerson");
                ModelState.Remove("ContactMobile");
                ModelState.Remove("ContactEmail");
            }

            //ModelState.Remove("CustomerSector");


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

            CustomerCModel customer = new CustomerCModel();



            var datresponse = customer.UpdateCustomerOthersData(input: new CustomerCModel.UpdateCustomer
            {
                UserAction = 2,

                CusAddress1 = data.Address1,

                CusContactEmail = data.ContactEmail,
                CusContactMobile = data.ContactMobile,
                CusContactPerson = data.ContactPerson,
                CusEmail = data.Email,
                CusGSTINNo = data.GSTINNo,

                CusMobile = data.Mobile,

                CusName = data.Name,
                CusNumber = data.Number,
                CusPhone = data.Phone,


                FK_Branch = data.BranchID,

                FK_CustomerType = data.CustomerTypeID,
                FK_Occupation = (data.OccupationID.HasValue) ? data.OccupationID.Value : 0,
                FK_LeadGenerate = data.LeadGenerateID,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
                // FK_Area = data.AreaID,
                //FK_Post = data.PostID
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,

                ID_Customer = data.CustomerID,
                FK_Customer = data.CustomerID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CusLandmark = data.Landmark,
                TransMode = data.TransMode,
                CusReferenceNo = data.CusReferenceNo,
               // IGSTCust = data.IGSTCust

                //EntrOn = DateTime.UtcNow

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCustomer(CustomerCModel.CustomerIDView data)
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

            CustomerCModel customer = new CustomerCModel();

            var datresponse = customer.DeleteCustomerData(input: new CustomerCModel.DeleteCustomer
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_Customer = data.CustomerID,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID


            }, companyKey: _userLoginInfo.CompanyKey);
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


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView> {
                Process = outputList.Process,
                Data = outputList.Data.Where(a=> a.ModeID==1).OrderBy(a=> a.SortOrder).ToList()
                };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetBranchDetails(long BranchTypeID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.Branchlist>(parameters: new APIParameters
            {

                TableName = "Branch B",
                SelectFields = "B.ID_Branch as BranchID,B.BrName AS Branch",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company + " AND B.FK_BranchType = " + BranchTypeID,
                GroupByFileds = "",
                SortFields = ""

              
            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPinCodedetails(string Pincode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CustomerCModel dataModel = new CustomerCModel();
            var outputData = dataModel.GetDetailsByPincodeData(input: new CustomerCModel.InputPincode { FK_Company = _userLoginInfo.FK_Company, Pincode = Pincode }, companyKey: _userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCountryList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.Countrylist>(parameters: new APIParameters
            {
                TableName = "Country",
                SelectFields = " CntryName AS Country,ID_Country AS CountryID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateList(Int64 countryID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            StateModel dataModel = new StateModel();
            var outputData = dataModel.GetStateData(input: new StateModel.GetState
            {
                FK_States = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Country = countryID,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictList(Int64 statesid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.Districtlist>(parameters: new APIParameters
            {
                TableName = "District",
                SelectFields = " DtName AS District,ID_District AS DistrictID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_States='" + statesid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAreaList(Int64 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.Arealist>(parameters: new APIParameters
            {
                TableName = "Area",
                SelectFields = " AreaName AS Area,ID_Area AS AreaID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCustomerTypeList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<CustomerCModel.CustomerTypelist>(
             parameters: new APIParameters
             {
                 TableName = " CustomerType CT JOIN CustomerCategory  CC ON CC.ID_CustomerCategory = CT.FK_CustomerCategory AND CC.Cancelled = 0 AND CC.Passed = 1 AND CC.FK_Company=" + _userLoginInfo.FK_Company +
                 "JOIN CustomerSector CS ON CS.ID_CustomerSector = CT.FK_CustomerSector AND CS.Cancelled = 0 AND CS.Passed = 1 AND CS.FK_Company = " + _userLoginInfo.FK_Company,
                 SelectFields = "CT.ID_CustomerType AS CustomerTypeID,CT.CustyName AS CustomerType,CC.CuscattyName AS CustomerCategory,CS.CussectyName AS CustomerSector,CC.CusCatIndividual AS Individual",
                 //Individual
                 Criteria = "CT.Cancelled =0 AND CT.Passed=1 AND CT.FK_Company=" + _userLoginInfo.FK_Company,
                 SortFields = "",
                 GroupByFileds = ""
             },
             companyKey: _userLoginInfo.CompanyKey
         );


            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetLeadGenerateList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.LeadGenerateListModel>(parameters: new APIParameters
            {

                SelectFields = "L.ID_LeadGenerate AS LeadGenerateID,L.LgLeadNo AS LeadGenerateNo,C.CusName AS LGCustomer",
                TableName = "LeadGenerate L JOIN CustomerOthers C ON L.FK_CustomerOthers=C.ID_CustomerOthers AND C.Cancelled =0 AND C.Passed=1  AND L.FK_Customer=0 AND C.FK_Company=" + _userLoginInfo.FK_Company,
                Criteria = "L.Cancelled =0 AND L.Passed=1  AND C.FK_Customer=0 AND L.FK_Company=" + _userLoginInfo.FK_Company ,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetPlaceList(Int64 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerCModel.Placelist>(parameters: new APIParameters
            {

                TableName = "Place",
                SelectFields = " PlcName AS Place,ID_Place AS PlaceID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPostList(CustomerCModel.InputPlace datas)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<CustomerCModel.Postlist>(parameters: new APIParameters
            {
                TableName = "Post",
                SelectFields = "PostName AS Post,ID_Post AS PostID,PinCode AS PinCode",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District=" + datas.DistrictID + " AND FK_Place=" + datas.PlaceID + " AND FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillDetailsByGSTNo(string GSTINNo)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerCModel model = new CustomerCModel();


            var data = model.GetCustomerByGST(input:
             new CustomerCModel.CustomerGSTINView
             {
                 FK_Company = _userLoginInfo.FK_Company,
                 GSTINNo = GSTINNo

             },

              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);


        }



    }
}