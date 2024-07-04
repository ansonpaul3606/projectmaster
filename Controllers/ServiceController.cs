using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;
using System.Data;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ServiceController : Controller
    {

        #region ::  Service Register  ::
        // GET: Service
        public ActionResult Index()
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

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            return View();
        }

        public ActionResult Registration(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
             ViewBag.ID_Country=_userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            CustomerserviceregisterModel cstrmdl = new CustomerserviceregisterModel();
            var genSettings = cstrmdl.GetSettingsInfo(input: new CustomerserviceregisterModel.SettingsInput
            {
                FK_Company = _userLoginInfo.FK_Company,
                PSModule = "SERVICE",
                PSPage = "CUSV",
                Mode = 3
            },
          companyKey: _userLoginInfo.CompanyKey);

            if (genSettings.Data != null)
            {
                foreach (var item in genSettings.Data)
                {
                    if (item.PSField == 10 && ViewBag.IsSubCategory == null)
                    {
                        ViewBag.IsSubCategory = item.PSValue;
                    }
                    else if (item.PSField == 11 && ViewBag.IsBrand == null)
                    {
                        ViewBag.IsBrand = item.PSValue;
                    }
                    else if (item.PSField == 12 && ViewBag.IsProduct == null)
                    {
                        ViewBag.IsProduct = item.PSValue;
                    }
                }
            }

            return View();

        }



        public ActionResult getBillList(int FK_Customer, int FK_CustomerOthers, Int32 FK_Product, int Mode, Int32 FK_Category)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerserviceregisterModel customerserviceregisterModel = new CustomerserviceregisterModel();

            var data = customerserviceregisterModel.GetBillList(input: new CustomerserviceregisterModel.BillFld
            {
                FK_Customer = FK_Customer,
                FK_CustomerOthers = FK_CustomerOthers,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Product = FK_Product,
                FK_Category = FK_Category,
                EntrBy = _userLoginInfo.EntrBy,
                Mode = Mode


            },

            companyKey: _userLoginInfo.CompanyKey);
           
            return Json(data ,JsonRequestBehavior.AllowGet);


        }
        public ActionResult getGetCustomerAccountBalance(int FK_Customer , DateTime TransDate)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerserviceregisterModel customerserviceregisterModel = new CustomerserviceregisterModel();

            var data = customerserviceregisterModel.GetCustomerAccountBalance(input: new CustomerserviceregisterModel.AccountbalFill
            {
                FK_Customer = FK_Customer,
               TransDate=TransDate
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getwarrantyList(int Mode, int MasterID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerserviceregisterModel customerserviceregisterModel = new CustomerserviceregisterModel();

            var data = customerserviceregisterModel.GetBillList(input: new CustomerserviceregisterModel.BillFld
            {

                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                Mode = Mode,
                MasterID = MasterID

            },

            companyKey: _userLoginInfo.CompanyKey);
            return Json(data, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteServiceRegisterInfo(CustomerserviceregisterModel.CustomerserviceregisterID data)
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

            StateModel stateModel = new StateModel();
            CustomerserviceregisterModel customerserviceregisterModel = new CustomerserviceregisterModel();

            var datresponse = customerserviceregisterModel.DeleteCustomerserviceregisterData
                (input: new CustomerserviceregisterModel.DeleteCustomerserviceregister
                {
                    FK_Customerserviceregister = data.ID_Customerserviceregister,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    TransMode = "",
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Reason = data.ReasonID
                }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceRegisterDeleteReasonList()
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

            CustomerserviceregisterModel model = new CustomerserviceregisterModel();

       
            var outputList = model.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.DeleteReasonInput
            {   FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetRegistrations(int pageSize, int pageIndex, string Name, string TransModes)
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

           // string transMode = "";

            CustomerserviceregisterModel objPrdServ = new CustomerserviceregisterModel();
            var data = objPrdServ.GetservregData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.IDCustomerRegister
            {
                 
                FK_CustomerServiceRegister = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = TransModes

            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetRegistrations1()
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

            var data = Common.GetDataViaQuery<CustomerserviceregisterModel.InclusiveRegistration>(parameters: new APIParameters
            {
                TableName = "Customerserviceregister",
                SelectFields = "ROW_NUMBER() OVER (ORDER BY ID_Customerserviceregister) SINO,ID_Customerserviceregister,CSRTickno TicketNo" +
                ",CusName CustomerName,CusMobile Mobile",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Branch=" + _userLoginInfo.FK_Branch + "",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetRegistrationsinfo(CustomerserviceregisterModel.IDCustomerRegister data)
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

            CustomerserviceregisterModel customerserviceregisterModel = new CustomerserviceregisterModel();


            CustomerserviceregisterModel objprd = new CustomerserviceregisterModel();
            var datainfo = objprd.GetservregData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.IDCustomerRegister
            {
                FK_CustomerServiceRegister = data.FK_CustomerServiceRegister,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                CheckList=0,
            });
            var checkinfo = objprd.GetservregcheckData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.IDCustomerRegister
            {
                FK_CustomerServiceRegister = data.FK_CustomerServiceRegister,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                CheckList = 1,
            });
            var statusinfo = objprd.GetservregcheckData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.IDCustomerRegister
            {
                FK_CustomerServiceRegister = data.FK_CustomerServiceRegister,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                CheckList = 2,
            });
            return Json(new { datainfo, checkinfo , statusinfo }, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult GetExistancy(CustomerserviceregisterModel.ExistCustomer dt)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerserviceregisterModel objprd = new CustomerserviceregisterModel();
            var data = objprd.Getexistreg(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.ExistCustomer
            {
                FK_CustomerServiceRegister=dt.FK_CustomerServiceRegister,
                FK_Product = dt.FK_Product,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                Mobile = dt.Mobile,
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadRegistrationForm(string mtd)
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

            CustomerserviceregisterModel.CustomerserviceregisterView RegisterData = new CustomerserviceregisterModel.CustomerserviceregisterView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List


            CustomerserviceregisterModel cstrmdl = new CustomerserviceregisterModel();

            var catglist = cstrmdl.GetCategorylist(input: new CustomerserviceregisterModel.ModeLead { Mode = 20 },
                companyKey: _userLoginInfo.CompanyKey);

            RegisterData.categorytyps = catglist.Data;


            var NetAction = Common.GetDataViaQuery<CustomerserviceregisterModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = " ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },

           
        companyKey: _userLoginInfo.CompanyKey
       );
            RegisterData.NextActionMode = NetAction.Data;
            var othercompany = Common.GetDataViaQuery<CustomerserviceregisterModel.othercompany>(parameters: new APIParameters
            {
                TableName = "OtherCompany",                       
                SelectFields = "ID_OtherCompany,OCName",
                Criteria = "Cancelled=0 AND Passed=1 AND  FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            RegisterData.othercompanies = othercompany.Data;


            var Category = Common.GetDataViaQuery<CustomerserviceregisterModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            RegisterData.CategoryList = Category.Data;


            var Brand = Common.GetDataViaQuery<CustomerserviceregisterModel.BrandList>(parameters: new APIParameters
            {
                TableName = "Brand",
                SelectFields = "ID_Brand AS BrandID ,BrName AS BrandName",
                Criteria = "Cancelled=0 AND Passed=1 And Mode='P' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Brand",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey

           );
            RegisterData.BrandList = Brand.Data;

         
            var genSettings = cstrmdl.GetSettingsInfo(input: new CustomerserviceregisterModel.SettingsInput {

                FK_Company = _userLoginInfo.FK_Company,
                PSModule = "SERVICE",
                PSPage = "CUSV",
                Mode = 3


            },
           companyKey: _userLoginInfo.CompanyKey);

            if (genSettings.Data != null)
            {
                foreach (var item in genSettings.Data)
                {
                    if (item.PSField == 10 && ViewBag.IsSubCategory == null)
                    {
                        ViewBag.IsSubCategory = item.PSValue;
                    }
                    else if (item.PSField == 11 && ViewBag.IsBrand == null)
                    {
                        ViewBag.IsBrand = item.PSValue;
                    }
                    else if (item.PSField == 12 && ViewBag.IsProduct == null)
                    {
                        ViewBag.IsProduct = item.PSValue;
                    }
                }
            }




            var channelList = cstrmdl.GetChannel(input: new CustomerserviceregisterModel.ModeLead { Mode =22 },
                companyKey: _userLoginInfo.CompanyKey);
           
            RegisterData.ChannelTypes = channelList.Data;
            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;


            #endregion :: Fill List

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddCustomerServiceReg", RegisterData);
        }

       
        public ActionResult GetComplaintList(Int32 prdid)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerserviceregisterModel.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintCheckList CCL join ComplaintList CL on CCL.FK_Complaint= CL.ID_ComplaintList  ",
                SelectFields = "DISTINCT CL.CompntName AS ComplaintName,CL.ID_ComplaintList AS ID_ComplaintList",
                Criteria = "CCL.Cancelled =0 AND CCL.Passed=1 AND CCL.FK_Category='" + prdid + "'" + " AND CCL.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComplaintwiseCategorylist(Int32 categrory)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerserviceregisterModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category ",
                SelectFields = "ID_Category CategoryID, CatName CategoryName",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Company='" + _userLoginInfo.FK_Company + "'" + " AND Mode= CASE WHEN " + categrory + " IN (1,3) THEN 'P' ELSE 'O' END", /*AND Project = 0",  commented for get project category for service project items*/
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetServiceList(int prdid,int Catid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var Criteria = "";
            if (prdid == 0)
            {
                
                Criteria = "PWS.FK_ProductWiseServices in (SELECT ID_ProductWiseServices from ProductWiseServices where (FK_Category = " + Catid + " AND FK_Product = 0) AND Cancelled =0 AND Passed=1)";
            }
            else
            {
                Criteria = "PWS.FK_ProductWiseServices in (SELECT ID_ProductWiseServices from ProductWiseServices where (FK_Product = "+ prdid + ") AND Cancelled =0 AND Passed=1)";
                Criteria += "UNION SELECT S.ID_Services AS ID_Services,S.ServicesName AS ServicesName FROM ProductWiseServicedetails PWS join Services  S on S.ID_Services = PWS.FK_Services ";
                Criteria += "WHERE PWS.FK_ProductWiseServices in (SELECT ID_ProductWiseServices from ProductWiseServices where(FK_Category = "+ Catid + " and FK_Product = 0) ";
                Criteria += "AND Cancelled = 0 AND Passed = 1)";
            }

            var data = Common.GetDataViaQuery<CustomerserviceregisterModel.Service>(parameters: new APIParameters
            {
                TableName = "ProductWiseServicedetails PWS  join Services  S on S.ID_Services = PWS.FK_Services",
                SelectFields = "DISTINCT S.ID_Services AS ID_Services,S.ServicesName AS ServicesName",
                Criteria = Criteria,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddCustomerservicerequest(CustomerserviceregisterModel.CustomerserviceregisterView data)
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

            ModelState.Clear();

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

            CustomerserviceregisterModel customerserviceregister = new CustomerserviceregisterModel();

            var datresponse = customerserviceregister.UpdateCustomerserviceregisterData(input: new CustomerserviceregisterModel.UpdateCustomerserviceregister
            {
                UserAction = 1,
                Debug = 0,
                TransMode =data.TransMode,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Customer = data.FK_Customer,
                FK_CustomerOthers = data.FK_CustomerOthers,
                CSRTickno = data.TicketNumber,
                CusName = data.CustomeName,
                CusMobile = data.CustomerMobile,
                TicketDate = data.TicketDate,
             
                CusAddress = data.CustomerAddress,
                CSRContactNo = data.OtherMobile,
                CSRLandmark = data.Landmark,
                CSRServiceFromDate = data.Servicefromdate,
                CSRServiceToDate = data.Servicetodate,
                CSRServicefromtime = data.Servicefromtime,
                CSRServicetotime = data.Servicetotime,
               CSRChannelID=data.CSRChannelID,
               CSRChannelSubID=data.CSRChannelSubID,
                CSRPriority = data.Priority,
                CSRCurrentStatus =0,
                CSRPCategory = data.Category,
                FK_OtherCompany = data.company,
                FK_Product = data.FK_Product,
                FK_ComplaintList = data.ID_ComplaintList,
                CSRODescription = data.CSRDescription,
                FK_ServiceList = data.ID_ServiceList,
                Status = data.NextAction,
                AttendedBy = data.EmpId,
                TicketTime=data.TicketTime,
                FK_Category=data.CategoryID,
                FK_District=data.DistrictID,
                FK_States=data.StatesID, 
                FK_Country=data.CountryID,
                Address2=data.Address2,
                FK_Area = data.AreaID,
                FK_Post = data.PostID,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                CheckList = data.CheckList is null ? "" : Common.xmlTostring(data.CheckList),
                FK_Brand = data.FK_Brand,
                FK_SubCategory = data.FK_SubCategory


            }, companyKey: _userLoginInfo.CompanyKey);
            CustomerserviceregisterModel.UpdateCustomerserviceregisterResult obData = new CustomerserviceregisterModel.UpdateCustomerserviceregisterResult();
            obData = datresponse.Data[0];
            //sendMail objMail = new sendMail();
            //objMail.sendMailData(Convert.ToInt32(obData.FK_CustomerServiceRegister), "TICKET", _userLoginInfo.FK_Company, 1, 3, _userLoginInfo.CompanyKey, "");
            List<string> objMsg = new List<string>();
            objMsg.Add(obData.Success);
            datresponse.Process.Message = objMsg;
            return Json(new { Process = datresponse.Process }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustomerservicerequest(CustomerserviceregisterModel.CustomerserviceregisterView data)
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
            ModelState.Clear();
            #endregion :: End ::

            CustomerserviceregisterModel update = new CustomerserviceregisterModel();
            var datresponse = update.UpdateCustomerserviceregisterDatas(input: new CustomerserviceregisterModel.UpdateCustomerserviceregister
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_CustomerServiceRegister= data.ID_Customerserviceregister,
                FK_Customerserviceregister =data.ID_Customerserviceregister,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Customer = data.FK_Customer,
                FK_CustomerOthers = data.FK_CustomerOthers,
                CSRTickno = data.TicketNumber,
                CusName = data.CustomeName,
                CusMobile = data.CustomerMobile,
                TicketDate = data.TicketDate,
                CusAddress = data.CustomerAddress,
                CSRContactNo = data.OtherMobile,
                CSRLandmark = data.Landmark,
                CSRServiceFromDate = data.Servicefromdate,
                CSRServiceToDate = data.Servicetodate,
                CSRServicefromtime = data.Servicefromtime,
                CSRServicetotime = data.Servicetotime,
                CSRChannelID = data.CSRChannelID,
                CSRChannelSubID = data.CSRChannelSubID,
                CSRPriority = data.Priority,
                CSRCurrentStatus = 0,
                CSRPCategory = data.Category,
                FK_OtherCompany = data.company,
                FK_Product = data.FK_Product,
                FK_ComplaintList = data.ID_ComplaintList,
                CSRODescription = data.CSRDescription,
                FK_ServiceList  = data.ID_ServiceList,
                Status = data.NextAction,
                AttendedBy = data.EmpId,
                TicketTime = data.TicketTime,
                FK_Category = data.CategoryID,
                FK_District = data.DistrictID,
                FK_States = data.StatesID,
                FK_Country = data.CountryID,
                Address2 = data.Address2,
                FK_Area = data.AreaID,
                FK_Post = data.PostID,
                CheckList = data.CheckList is null ?"": Common.xmlTostring(data.CheckList),
                FK_Brand = data.FK_Brand,
                FK_SubCategory = data.FK_SubCategory


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }



        #region ::  Get Data  ::



        public JsonResult GetFilter()
        {

            List<FilterKeyValuePair> keyValuePairs = new List<FilterKeyValuePair>();
            for (int i = 0; i < 10; i++)
            {
                keyValuePairs.Add(new FilterKeyValuePair { FilterJSONKey = "key " + i, FilterJSONvalue = "value " + i });
            }

            SearchFilter customerNameSearch = new SearchFilter()
            {
                FilterNameKey = "customerNameSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "input",
                FilterData = new FilterData_input
                {
                    FilterLabel = "Customer Name",
                    FilterName = "CusName",
                    Placeholder = "Enter customer name"
                }
            };
            SearchFilter customerMobileSearch = new SearchFilter()
            {
                FilterNameKey = "customerMobileSearch",
                //FilterDataURL = "/filter/cu",
                FilterType = "input",
                FilterData = new FilterData_input
                {
                    FilterLabel = "Mobile Number",
                    FilterName = "CusMobile",
                    Placeholder = "Enter mobile no."
                }
            };

            List<SearchFilter> GetALLFilter2 = new List<SearchFilter>
                            {
                                customerNameSearch,
                                customerMobileSearch,

                            };

            return Json(new { GetALLFilter2 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerInfo(long MobileNumber)
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

            DataTable dtbl = new DataTable();


            var data = Common.GetDataViaQuery<CustomerModel.CustomerView>(parameters: new APIParameters
            {
                TableName = "Customer C LEFT JOIN Country AS CN ON CN.ID_Country=C.FK_Country AND CN.Cancelled=0 AND CN.Passed=1" +
                "LEFT JOIN States AS S ON S.ID_States=C.FK_State AND S.Cancelled=0 AND S.Passed=1" +
                "LEFT JOIN District AS D ON D.ID_District=C.FK_District AND D.Cancelled=0 AND D.Passed=1" +
                "LEFT JOIN Place AS P ON P.ID_Place=C.FK_Place  AND P.Cancelled=0 AND P.Passed=1" +
                "LEFT JOIN Area AS A ON A.ID_Area=C.FK_Area AND A.Cancelled=0 AND A.Passed=1" +
                "LEFT JOIN Post AS PO ON PO.ID_Post=C.FK_Post AND PO.Cancelled=0 AND PO.Passed=1",
                SelectFields = "ID_Customer CustomerID,CusName CustomerName,CusMobile CustomerMobile,C.CusAddress1+','+ISNULL(D.DtName,'')+','+ISNULL(P.PlcName,'')+','+ISNULL(A.AreaName,'')+','+ISNULL(PO.PostName,'')+' '+'- '+ISNULL(PO.PinCode,'') CustomerAddress1," +
                "CusEmail CustomerEmail,CusNumber CustomerNumber",
                Criteria = "C.Cancelled=0 AND C.Passed=1 AND C.FK_Company=" + _userLoginInfo.FK_Company + "AND CusMobile=" + MobileNumber + "",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    

     

        public ActionResult GetTicketNumber()
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

            CustomerserviceregisterModel Ticket = new CustomerserviceregisterModel();

            var data = Ticket.GetTikcetNumber(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.Ticket

            {
                FK_Branch = _userLoginInfo.FK_Branch,
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubcategory(CustomerserviceregisterModel.CustomerserviceregisterView cr)
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
            CustomerserviceregisterModel.CustomerserviceregisterView list = new CustomerserviceregisterModel.CustomerserviceregisterView();
            CustomerserviceregisterModel Model = new CustomerserviceregisterModel();
            APIParameters apiSub = new APIParameters
            {
                TableName = "[SubCategory]",
                SelectFields = "[ID_SubCategory] AS ID_SubCategory,[SubCatName] AS SubCatName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Category ='" + cr.CategoryID + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<CustomerserviceregisterModel.SubCategoryList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            list.SubCategoryList = SubcategoryInfo.Data;

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductList(CustomerserviceregisterModel.ProductInput Data)
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

            CustomerserviceregisterModel Model = new CustomerserviceregisterModel();

            var data = Model.GetProducts(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceregisterModel.ProductInput

            {
                FK_Customer = Data.FK_Customer,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = Data.PageIndex,
                PageSize = 10,
                Mode = Data.Mode
            });

            //   var data = Common.GetDataViaQuery<ProductModel.ProductView>(parameters: new APIParameters
            //   {
            //       TableName = "Product",
            //       SelectFields = "ID_Product ProductID,ProdName ProdName,ProdHSNCode ProdHSNCode",
            //       Criteria = "Cancelled=0",
            //       SortFields = "",
            //       GroupByFileds = ""
            //   },
            // companyKey: _userLoginInfo.CompanyKey
            //);


            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductInfo(long ID_Product)
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

            DataTable dtbl = new DataTable();


            var data = Common.GetDataViaQuery<ProductModel.ProductView>(parameters: new APIParameters
            {
                TableName = "Product",
                SelectFields = "ID_Product ProductID,ProdName ProdName from Product",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Branch=" + 1 + "AND ID_Product=" + ID_Product + "",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion ::  Get Data  ::

        public class CustomerList
        {
            public int ID_Customer { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
        }

        #endregion :: End Service Register  ::

        #region :: Service Assign ::

        public ActionResult Serviceassign(string mtd,string mgrp)
         {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            CommonMethod objCmnMethod = new CommonMethod();

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.mtd = mtd;

            return View();
        }


        public ActionResult LoadServiceassignForm(string mtd)
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


            CustomerserviceassignModel.CustomerserviceassignView Assign = new CustomerserviceassignModel.CustomerserviceassignView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var BranchList = Common.GetDataViaQuery<CustomerserviceassignModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = " ID_Branch,BrName BranchName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            var ComplaintList = Common.GetDataViaQuery<CustomerserviceassignModel.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList,CompntName ComplaintName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );
            var OpDepartmentList = Common.GetDataViaQuery<CustomerserviceassignModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey
            );
            var LatestDepartment = Common.GetDataViaQuery<CustomerserviceassignModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "Top 1 ID_Department AS DepartmentID",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "order by 1 desc",
                SortFields = "",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey
);
            var NetAction = Common.GetDataViaQuery<CustomerserviceassignModel.Status>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = " ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            Assign.StatusList = NetAction.Data;
            CustomerserviceassignModel objpaymode = new CustomerserviceassignModel();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new CustomerserviceassignModel.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);

            Assign.ActionStatusList = rolemodeList.Data;
            Assign.DepartmentList = OpDepartmentList.Data;
            Assign.BranchList = BranchList.Data;
            Assign.ComplaintList = ComplaintList.Data;
            //5-Service
            //DepartmentModel objDept = new DepartmentModel();
            //var defaultDep = objDept.GetDefault(input: new DepartmentModel.GetDefaultDepartment { Mode = 5, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            Assign.ID_DefaultDept = LatestDepartment.Data[0].DepartmentID;


            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            //return PartialView("_AddServiceassignNew", Assign);
            return PartialView("_LoadServiceAssign", Assign);
        }
     





        #region :: Serice Assign Save ::


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddCustomerserviceAssign(CustomerserviceassignModel.UpdateCustomerserviceassign Data)
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

            ModelState.Clear();

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



            CustomerserviceassignModel customerserviceassignModel = new CustomerserviceassignModel();

            var datresponse = customerserviceassignModel.UpdateCustomerserviceassignData(input: new CustomerserviceassignModel.UpdateCustomerserviceassign
            {
                UserAction = 1,
                TransMode = "WEB",
                Debug = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Employee = Data.FK_Employee,
                CSAVisitdate = Data.CSAVisitdate,
                CSAVisittime = Data.CSAVisittime,
                CSAPriority = Data.CSAPriority,
                CSARemarks = Data.CSARemarks,

            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }

        #endregion :: Serice Assign Save ::

        #region :: Get Data ::

        public ActionResult GetSearchResult(CustomerserviceassignModel.TicketInput Data)
        {
            Log logfile = new Log();
            logfile.WriteLog("12", false);

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

                CustomerserviceassignModel Model = new CustomerserviceassignModel();

                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::
            logfile.WriteLog("13", false);
            var data = Model.GetTickets(input: new CustomerserviceassignModel.TicketInput
                {
                    FK_Branch = Data.FK_Branch==null?0: Data.FK_Branch,
                    Product = Data.Product == null ? "" : Data.Product,
                EntrBy = _userLoginInfo.EntrBy,
                    FK_ComplaintType = Data.FK_ComplaintType == null ? 0 : Data.FK_ComplaintType,
                Status = Data.Status ,
                    FromDate = Data.FromDate == null ? null : Data.FromDate,
                    ToDate = Data.ToDate == null ? null : Data.ToDate,
                    TicketNumber = Data.TicketNumber==null ? "" : Data.TicketNumber,
                    Customer = Data.Content == null ? "" : Data.Content,
                    Mobile = Data.Content == null ? "" : Data.Content,
                Content = Data.Content == null ? "" : Data.Content,
                FK_Company = _userLoginInfo.FK_Company
                }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::


            // return Json(new APIGetRecordsDynamic<AreaModel.Area> { Process = output, Data = data.Data }, JsonRequestBehavior.AllowGet);
            
            logfile.WriteLog("45", false);
           
           // logfile.WriteLog(data.Data[0].Mobile, false);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployee(CustomerserviceassignModel.EmployeeInput Data)
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

            CustomerserviceassignModel Model = new CustomerserviceassignModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetEmployee(input: new CustomerserviceassignModel.EmployeeInput
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = Data.PageIndex,
                PageSize = Data.PageSize,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetEmployeeInfo(CustomerserviceassignModel.EmployeeInput Data)
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

            CustomerserviceassignModel Model = new CustomerserviceassignModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetEmployee(input: new CustomerserviceassignModel.EmployeeInput
            {
                FK_Employee = Data.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = Data.PageIndex,
                PageSize = Data.PageSize,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            return Json(data, JsonRequestBehavior.AllowGet);

        }
   
        public ActionResult GetTicketDetails(CustomerserviceassignModel.CustomerserviceregisterInput Data)
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


            CustomerserviceassignModel.CustomerserviceregisterView Model = new CustomerserviceassignModel.CustomerserviceregisterView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var ServiceRegister = Common.GetDataViaQuery<CustomerserviceassignModel.ServiceAssignCustomerInfo>(parameters: new APIParameters
            {
                TableName = " Customerserviceregister CSR",
                SelectFields = "ID_Customerserviceregister FK_Customerserviceregister,CSRServicefromdate FromDate,CSRServicetodate ToDate,CSRServicefromtime FromTime,CSRServicetotime ToTime," +
                               "CSR.CSRCustomerName Customer,CSR.CSRCustomerAddress Address,CSRCustomerMobile Mobile,CSROtherMobile OtherMobile,CSRLandmark Landmark,csr.CSRTickno Ticket",
                Criteria = "CSR.Cancelled=0 AND CSR.Passed=1 AND ID_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND CSR.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            #endregion :: Fill List ::

            var ProductList = Common.GetDataViaQuery<CustomerserviceassignModel.Products>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterproductdetails CP JOIN Product AS P ON P.ID_Product=CP.FK_Product AND P.Cancelled=0 AND P.Passed=1" +
                "JOIN ComplaintList CL ON CL.ID_ComplaintList=CP.FK_ComplaintList AND CL.Cancelled=0 AND CL.Passed=1",
                SelectFields = "P.ProdName Productname,CL.CompntName ProductComplaint,CP.CSRODescription ProductDescription",
                Criteria = "CP.Cancelled=0  AND CP.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            var OtherProductList = Common.GetDataViaQuery<CustomerserviceassignModel.OtherProducts>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterothproductdetails cp join ComplaintList cl on cl.ID_ComplaintList=cp.FK_ComplaintList and cl.Cancelled=0 and cl.Passed=1",
                SelectFields = " CSROCompany Company,CSROProduct Product,CSRODescription Description,cl.CompntName Complaint",
                Criteria = "cp.Cancelled=0  AND cp.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            Model.Product = ProductList.Data;
            Model.OtherProducts = OtherProductList.Data;
          
            if (ServiceRegister.Data!= null)
            {
                Model.FK_Customerserviceregister = ServiceRegister.Data[0].FK_Customerserviceregister;
                Model.Customer = ServiceRegister.Data[0].Customer;
                Model.Address = ServiceRegister.Data[0].Address;
                Model.Mobile = ServiceRegister.Data[0].Mobile;
                Model.OtherMobile = ServiceRegister.Data[0].OtherMobile;
                Model.Landmark = ServiceRegister.Data[0].Landmark;
                Model.Ticket = ServiceRegister.Data[0].Ticket;
                Model.FromDate =Convert.ToDateTime(ServiceRegister.Data[0].FromDate);
                Model.ToDate = String.IsNullOrEmpty(ServiceRegister.Data[0].ToDate)==null ? DateTime.Now.Date :Convert.ToDateTime(ServiceRegister.Data[0].ToDate) ;
                Model.FromTime = ServiceRegister.Data[0].FromTime;
                Model.ToTime = ServiceRegister.Data[0].ToTime;
            }

            return Json(Model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTicketDetailsById(CustomerserviceassignModel.CustomerserviceregisterView Data)
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


            CustomerserviceassignModel.CustomerserviceassignView Model = new CustomerserviceassignModel.CustomerserviceassignView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Common.GetDataViaQuery<CustomerserviceassignModel.CustomerserviceassignView>(parameters: new APIParameters
            {
                TableName = " Customerserviceassign cs join Employee as e on e.ID_Employee=cs.FK_Employee and e.Cancelled=0",
                SelectFields = "CSAVisitdate Visitdate,CSAVisittime Visittime,csapriority Priority,CSARemarks Remarks,E.EmpFName+' '+E.EmpLName AS Employee,E.ID_Employee FK_Employee",
                Criteria = "cs.Cancelled=0 AND cs.Passed=1 AND FK_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND cs.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            var CustomerData = Common.GetDataViaQuery<CustomerserviceassignModel.CustomerDetails>(parameters: new APIParameters
            {
                TableName = " Customerserviceregister CSR",
                SelectFields = "ID_Customerserviceregister,CSRServicefromdate FromDate,CSRServicetodate ToDate,CSRServicefromtime FromTime,CSRServicetotime ToTime," +
                             "CSR.CSRCustomerName Customer,CSR.CSRCustomerAddress Address,CSRCustomerMobile Mobile,CSROtherMobile OtherMobile,CSRLandmark Landmark,csr.CSRTickno Ticket",
                Criteria = "CSR.Cancelled=0 AND CSR.Passed=1 AND ID_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND CSR.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
          );

            #endregion :: Fill List ::

            var ProductList = Common.GetDataViaQuery<CustomerserviceassignModel.Products>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterproductdetails CP JOIN Product AS P ON P.ID_Product=CP.FK_Product AND P.Cancelled=0 AND P.Passed=1" +
                "JOIN ComplaintList CL ON CL.ID_ComplaintList=CP.FK_ComplaintList AND CL.Cancelled=0 AND CL.Passed=1",
                SelectFields = "P.ProdName Productname,CL.CompntName ProductComplaint,CP.CSRPDescription ProductDescription",
                Criteria = "CP.Cancelled=0  AND CP.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            var OtherProductList = Common.GetDataViaQuery<CustomerserviceassignModel.OtherProducts>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterothproductdetails cp join ComplaintList cl on cl.ID_ComplaintList=cp.FK_ComplaintList and cl.Cancelled=0 and cl.Passed=1",
                SelectFields = " CSROCompany Company,CSROProduct Product,CSRODescription Description,cl.CompntName Complaint",
                Criteria = "cp.Cancelled=0  AND cp.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            Model.Product = ProductList.Data;
            Model.OtherProducts = OtherProductList.Data;
            Model.customerDetails = CustomerData.Data;

            if (data != null)
            {
                Model.Employee = data.Data[0].Employee;
                Model.FK_Employee = data.Data[0].FK_Employee;
                Model.Visitdate = data.Data[0].Visitdate;
                Model.Visittime = data.Data[0].Visittime;
                Model.Priority = data.Data[0].Priority;
                Model.Remarks = data.Data[0].Remarks;

            }

            return Json(Model, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GetCustomerList(CustomerserviceregisterModel.CustomerSearchInput Data)
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

        //    DataTable dtbl = new DataTable();

        //    CustomerserviceregisterModel customerserviceregisterModel = new CustomerserviceregisterModel();

        //    var data = customerserviceregisterModel.GetCustomer(input: new CustomerserviceregisterModel.CustomerSearchInput
        //    {
        //        TransMode =string.Empty,
        //        FK_Customer = 0,
        //        EntrBy = _userLoginInfo.EntrBy,
        //        PageIndex = Data.PageIndex,
        //        PageSize = Data.PageSize,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        FK_Machine = _userLoginInfo.FK_Machine,
        //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,              
        //    }, companyKey: _userLoginInfo.CompanyKey);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetProductCheckList(CustomerserviceregisterModel.CheckList Data)
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

        //    CustomerserviceregisterModel customerserviceregister = new CustomerserviceregisterModel();

        //    var datresponse = customerserviceregister.GetCheckList(input: new CustomerserviceregisterModel.CheckList
        //    {
        //        CheckMode = Data.CheckMode

        //    }, companyKey: _userLoginInfo.CompanyKey);


        //    return Json(datresponse, JsonRequestBehavior.AllowGet);
        //}

        #endregion ::Get Data ::


        #endregion :: Service Assign ::

        [HttpPost]
        public ActionResult GetSearchResults(CustomerserviceassignModel.TicketInputNew Data)
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

            CustomerserviceassignModel Model = new CustomerserviceassignModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetTicketsnew(input: new CustomerserviceassignModel.TicketInputNew
            {
                FK_Branch = Data.FK_Branch == null ? 0 : Data.FK_Branch,
                FK_Product = Data.FK_Product == null ? 0 : Data.FK_Product,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ComplaintType = Data.FK_ComplaintType == null ? 0 : Data.FK_ComplaintType,
                Status = Data.Status,
                FromDate = Data.FromDate == null ? "" : Data.FromDate,
                ToDate = Data.ToDate == null ? "" : Data.ToDate,
                TicketNumber = Data.TicketNumber == null ? "" : Data.TicketNumber,
                Customer = Data.Customer == null ? "" : Data.Customer,
                Mobile = Data.Mobile == null ? "" : Data.Mobile,
                SortOrder = Data.SortOrder == null ? 0 : Data.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = Data.PageIndex + 1,
                PageSize = Data.PageSize,
                Mode = Data.Mode,
                FK_Area=Data.FK_Area == null ? 0 : Data.FK_Area,
                FK_Post= Data.FK_Post == null ? 0 : Data.FK_Post,
                FK_Employee=Data.FK_Employee==null?0: Data.FK_Employee,
                DueDays = Data.DueDays == null ? 0 : Data.DueDays,
            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            CustomerserviceassignModel.ServiceAssignDataCount obj = new  CustomerserviceassignModel.ServiceAssignDataCount();

            if (data.Data!= null)
            {
                foreach (var row in data.Data)
                {
                    switch (Convert.ToInt32(row.MasterID))
                    {
                        case 2:
                            obj.New = Convert.ToInt32(row.Value);
                            obj.NewStatus = "display:block";
                            break;
                        case 3:
                            obj.Completed = Convert.ToInt32(row.Value);
                            obj.CompletedStatus = "display:block"; 
                            break;
                        case 4:
                            obj.Pending = Convert.ToInt32(row.Value);
                            obj.PendingStatus = "display:block"; 
                            break;
                        case 5:
                            obj.PickupRequest = Convert.ToInt32(row.Value);
                            obj.PickupRequestStatus = "display:block"; 
                            break;
                        case 6:
                            obj.ReplacementRequest = Convert.ToInt32(row.Value);
                            obj.ReplacementRequestStatus = "display:block"; 
                            break;
                        case 7:
                            obj.DeliveryRequest = Convert.ToInt32(row.Value);
                            obj.DeliveryRequestStatus = "display:block"; 
                            break;
                        case 8:
                            obj.FactoryService = Convert.ToInt32(row.Value);
                            obj.FactoryServiceStatus = "display:block";
                            break;
                    }
                }
            }

            return Json(new { data.Process, data.Data, Data.PageSize, Data.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount , obj}, JsonRequestBehavior.AllowGet);
        }

//#endregion ::Get Data ::

        #region :: Serice Assign Save ::


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustomerserviceAssign(CustomerserviceassignModel.UpdateCustomerserviceassign Data)
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

            ModelState.Clear();

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



            CustomerserviceassignModel customerserviceassignModel = new CustomerserviceassignModel();

            var datresponse = customerserviceassignModel.UpdateCustomerserviceassignData(input: new CustomerserviceassignModel.UpdateCustomerserviceassign
            {
                UserAction = 2,
                TransMode = "WEB",
                Debug = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Employee = Data.FK_Employee,
                CSAVisitdate = Data.CSAVisitdate,
                CSAVisittime = Data.CSAVisittime,
                CSAPriority = Data.CSAPriority,
                CSARemarks = Data.CSARemarks,

            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }

        #endregion :: Serice Assign Save ::

        #region :: Serice Assign pop up ticket select old ::
        public ActionResult GetTicketDetailsNewOLD(CustomerserviceassignModel.CustomerserviceregisterInput Data)
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


            CustomerserviceassignModel.CustomerserviceregisterViewNew Model = new CustomerserviceassignModel.CustomerserviceregisterViewNew();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var assign = Common.GetDataViaQuery<CustomerserviceassignModel.ServiceAssignCustomerInfo>(parameters: new APIParameters
            {
                TableName = " CustomerServiceAssign CSA JOIN CustomerServiceRegister CSR ON CSA.FK_Customerserviceregister=CSR.ID_CustomerServiceRegister",
                SelectFields = "ID_Customerserviceassign ID_Customerserviceassign",
                Criteria = "CSR.Cancelled=0 AND CSR.Passed=1 AND FK_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND CSR.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            CustomerserviceassignModel Models = new CustomerserviceassignModel();
            var ServiceRegister = Models.GetCustomerserviceData1(input: new CustomerserviceassignModel.CustomerserDataInputs
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "WEB",
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);
            #endregion :: Fill List ::

            //var ProductList = Common.GetDataViaQuery<CustomerserviceassignModel.Products>(parameters: new APIParameters
            //{
            //    TableName = "Customerserviceregisterproductdetails CP LEFTJOIN Product AS P ON P.ID_Product=CP.FK_Product AND P.Cancelled=0 AND P.Passed=1" +
            //    "LEFT JOIN ComplaintList CL ON CL.ID_ComplaintList=CP.FK_ComplaintList AND CL.Cancelled=0 AND CL.Passed=1 LEFT JOIN Services S ON S.ID_Services=CP.FK_ServiceList",
            //    SelectFields = "P.ProdName Productname,ISNULL(CL.CompntName,S.ServicesName) ProductComplaint,CP.CSRODescription ProductDescription",
            //    Criteria = "CP.Cancelled=0  AND CP.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
            //    GroupByFileds = "",
            //    SortFields = ""
            //},
            // companyKey: _userLoginInfo.CompanyKey
            //);

            var OtherProductList = Common.GetDataViaQuery<CustomerserviceassignModel.OtherProducts>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterothproductdetails cp join ComplaintList cl on cl.ID_ComplaintList=cp.FK_ComplaintList and cl.Cancelled=0 and cl.Passed=1",
                SelectFields = " CSROCompany Company,CSROProduct Product,CSRODescription Description,cl.CompntName Complaint",
                Criteria = "cp.Cancelled=0  AND cp.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            //var NetAction = Common.GetDataViaQuery<CustomerserviceassignModel.Status>(parameters: new APIParameters
            //{
            //    TableName = "NextAction",
            //    SelectFields = " ID_NextAction,NxtActnName,FK_ActionStatus",
            //    Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
            //    GroupByFileds = "",
            //    SortFields = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey
            //);
            //  Model.StatusList = NetAction.Data;


            //Model.Product = ProductList.Data;
            //Model.OtherProducts = OtherProductList.Data;

            if (ServiceRegister.Data != null)
            {
                Model.FK_Customerserviceregister = ServiceRegister.Data[0].FK_Customerserviceregister;
                Model.Customer = ServiceRegister.Data[0].Customer;
                Model.Address = ServiceRegister.Data[0].Address;
                Model.Mobile = ServiceRegister.Data[0].Mobile;
                Model.OtherMobile = ServiceRegister.Data[0].OtherMobile;
                Model.Landmark = ServiceRegister.Data[0].Landmark;
                Model.Ticket = ServiceRegister.Data[0].Ticket;
                Model.FromDate = ServiceRegister.Data[0].FromDate;
                Model.ToDate = ServiceRegister.Data[0].ToDate;/*String.IsNullOrEmpty(ServiceRegister.Data[0].ToDate) == null ? DateTime.Now.Date : Convert.ToDateTime(ServiceRegister.Data[0].ToDate);*/
                Model.FromTime = ServiceRegister.Data[0].FromTime;
                Model.ToTime = ServiceRegister.Data[0].ToTime;
                Model.Priority = ServiceRegister.Data[0].Priority;
                Model.ID_Customer = ServiceRegister.Data[0].ID_Customer;
                Model.Productname = ServiceRegister.Data[0].Productname;
                Model.ProductDescription = ServiceRegister.Data[0].ProductDescription;
                Model.ProductComplaint = ServiceRegister.Data[0].ProductComplaint;
            }
            //if (ProductList.Data != null)
            //{
                //Model.Productname = ProductList.Data[0].Productname;
                //Model.ProductDescription = ProductList.Data[0].ProductDescription;
                //Model.ProductComplaint = ProductList.Data[0].ProductComplaint;
            //}
            if(assign.Data!=null)
            {
                Model.ID_Customerserviceassign = assign.Data[0].ID_Customerserviceassign;
            }
           
            return Json(Model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTicketDetailsByIdNewOLD(CustomerserviceassignModel.CustomerserviceregisterView Data)
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


            CustomerserviceassignModel.CustomerserviceassignViewNew Model = new CustomerserviceassignModel.CustomerserviceassignViewNew();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Common.GetDataViaQuery<CustomerserviceassignModel.CustomerserviceassignViewNew>(parameters: new APIParameters
            {
                TableName = " Customerserviceassign cs ",
                SelectFields = "TOP 1 ID_Customerserviceassign ID_Customerserviceassign,CSAVisitdate Visitdate,CSAVisittime Visittime,csapriority Priority,CSARemarks Remarks",
                Criteria = "cs.Cancelled=0  AND FK_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND  CSAASsignMode=1 AND cs.FK_Company=" + _userLoginInfo.FK_Company + "ORDER BY ID_Customerserviceassign DESC",
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            CustomerserviceassignModel Models = new CustomerserviceassignModel();
            var CustomerData = Models.GetCustomerserviceData(input: new CustomerserviceassignModel.CustomerserDataInputs
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "WEB",
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);
            //  var CustomerData = Common.GetDataViaQuery<CustomerserviceassignModel.CustomerDetails>(parameters: new APIParameters
            //  {
            //      TableName = " Customerserviceregister CSR  JOIN CustomerOthers C ON C.ID_CustomerOthers=CSR.FK_CustomerOthers",
            //      SelectFields = "ID_CustomerServiceRegister FK_Customerserviceregister,CSRServicefromdate FromDate,CSRServicetodate ToDate,CSRServicefromtime FromTime,CSRServicetotime ToTime," +
            //                     "C.CusName Customer,C.CusAddress1 Address,C.CusMobile Mobile ,CSRContactNo CSROtherMobile,CSRLandmark Landmark,csr.CSRTickno Ticket",
            //      Criteria = "CSR.Cancelled=0 AND CSR.Passed=1 AND ID_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND CSR.FK_Company=" + _userLoginInfo.FK_Company,
            //      GroupByFileds = "",
            //      SortFields = ""
            //  },
            // companyKey: _userLoginInfo.CompanyKey
            //);

            #endregion :: Fill List ::

          
            var AssignEmployee = Models.GetEmployeeData(input: new CustomerserviceassignModel.EmployeeGetInput
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                PageMode=3
            }, companyKey: _userLoginInfo.CompanyKey);

            var ProductList = Common.GetDataViaQuery<CustomerserviceassignModel.Products>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterproductdetails CP LEFT JOIN Product AS P ON P.ID_Product=CP.FK_Product AND P.Cancelled=0 AND P.Passed=1" +
                "LEFT JOIN ComplaintList CL ON CL.ID_ComplaintList=CP.FK_ComplaintList AND CL.Cancelled=0 AND CL.Passed=1 LEFT JOIN Services S ON S.ID_Services=CP.FK_ServiceList", 
                SelectFields = "ISNULL(P.ProdName,'') Productname,ISNULL(CL.CompntName,S.ServicesName) ProductComplaint,CP.CSRODescription ProductDescription",
                Criteria = "CP.Cancelled=0  AND CP.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            var OtherProductList = Common.GetDataViaQuery<CustomerserviceassignModel.OtherProducts>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterothproductdetails cp join ComplaintList cl on cl.ID_ComplaintList=cp.FK_ComplaintList and cl.Cancelled=0 and cl.Passed=1",
                SelectFields = " CSROCompany Company,CSROProduct Product,CSRODescription Description,cl.CompntName Complaint",
                Criteria = "cp.Cancelled=0  AND cp.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

           // Model.Product = ProductList.Data;
            Model.OtherProducts = OtherProductList.Data;
            //Models.customerDetails = CustomerData.Data;

            if (data.Data != null)
            {
                Model.ID_Customerserviceassign = data.Data[0].ID_Customerserviceassign;
                //Model.Employee = data.Data[0].Employee;
                //Model.FK_Employee = data.Data[0].FK_Employee;
                Model.Visitdate = data.Data[0].Visitdate;
                Model.Visittime = data.Data[0].Visittime;
                Model.Priority = data.Data[0].Priority;
                Model.Remarks = data.Data[0].Remarks;
               
            }

         
                //Model.Productname = ProductList.Data[0].Productname == null ? "" : ProductList.Data[0].Productname;
           
                //Model.ProductDescription = ProductList.Data[0].ProductDescription == null ? "" : ProductList.Data[0].ProductDescription;
                //Model.ProductComplaint = ProductList.Data[0].ProductComplaint == null ? "" : ProductList.Data[0].ProductComplaint;
            

            return Json(new { Model, CustomerData, AssignEmployee }, JsonRequestBehavior.AllowGet);
        }
        #endregion :: Serice Assign ticket select ::

        #region :: Serice Assign pop up ticket select New ::

        public ActionResult GetTicketDetailsNew(CustomerserviceassignModel.CustomerserDataSelectInputs Data)
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


            CustomerserviceassignModel.CustomerserviceregisterViewNew Model = new CustomerserviceassignModel.CustomerserviceregisterViewNew();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var assign = Common.GetDataViaQuery<CustomerserviceassignModel.ServiceAssignCustomerInfo>(parameters: new APIParameters
            {
                TableName = " CustomerServiceAssign CSA JOIN CustomerServiceRegister CSR ON CSA.FK_Customerserviceregister=CSR.ID_CustomerServiceRegister",
                SelectFields = "ID_Customerserviceassign ID_Customerserviceassign",
                Criteria = "CSR.Cancelled=0 AND CSR.Passed=1 AND FK_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND CSR.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            CustomerserviceassignModel Models = new CustomerserviceassignModel();
            var ServiceRegister = Models.GetCustomerserviceDataSelect(input: new CustomerserviceassignModel.CustomerserDataSelectInputs
            {
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_CustomerserviceregisterProductDetails = Data.FK_CustomerserviceregisterProductDetails,
                TransMode = Data.TransMode,                
            }, companyKey: _userLoginInfo.CompanyKey);
            #endregion :: Fill List ::
            
            var OtherProductList = Common.GetDataViaQuery<CustomerserviceassignModel.OtherProducts>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterothproductdetails cp join ComplaintList cl on cl.ID_ComplaintList=cp.FK_ComplaintList and cl.Cancelled=0 and cl.Passed=1",
                SelectFields = " CSROCompany Company,CSROProduct Product,CSRODescription Description,cl.CompntName Complaint",
                Criteria = "cp.Cancelled=0  AND cp.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            

            if (ServiceRegister.Data != null)
            {
                Model.FK_Customerserviceregister = ServiceRegister.Data[0].FK_Customerserviceregister;
                Model.Customer = ServiceRegister.Data[0].Customer;
                Model.Address = ServiceRegister.Data[0].Address;
                Model.Mobile = ServiceRegister.Data[0].Mobile;
                Model.OtherMobile = ServiceRegister.Data[0].OtherMobile;
                Model.Landmark = ServiceRegister.Data[0].Landmark;
                Model.Ticket = ServiceRegister.Data[0].Ticket;
                Model.FromDate = ServiceRegister.Data[0].FromDate;
                Model.ToDate = ServiceRegister.Data[0].ToDate;
                Model.FromTime = ServiceRegister.Data[0].FromTime;
                Model.ToTime = ServiceRegister.Data[0].ToTime;
                Model.Priority = ServiceRegister.Data[0].Priority;
                Model.ID_Customer = ServiceRegister.Data[0].ID_Customer;
                Model.Productname = ServiceRegister.Data[0].Productname;
                Model.ProductDescription = ServiceRegister.Data[0].ProductDescription;
                Model.ProductComplaint = ServiceRegister.Data[0].ProductComplaint;
            }
            
            if (assign.Data != null)
            {
                Model.ID_Customerserviceassign = assign.Data[0].ID_Customerserviceassign;
            }

            return Json(Model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTicketDetailsByIdNew(CustomerserviceassignModel.CustomerserviceregisterView Data)
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


            CustomerserviceassignModel.CustomerserviceassignViewNew Model = new CustomerserviceassignModel.CustomerserviceassignViewNew();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Common.GetDataViaQuery<CustomerserviceassignModel.CustomerserviceassignViewNew>(parameters: new APIParameters
            {
                TableName = " Customerserviceassign cs ",
                SelectFields = "TOP 1 ID_Customerserviceassign ID_Customerserviceassign,CSAVisitdate Visitdate,CSAVisittime Visittime,csapriority Priority,CSARemarks Remarks",
                Criteria = "cs.Cancelled=0  AND FK_Customerserviceregister=" + Data.FK_Customerserviceregister + " AND  CSAASsignMode=1 AND cs.FK_Company=" + _userLoginInfo.FK_Company + "ORDER BY ID_Customerserviceassign DESC",
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            CustomerserviceassignModel Models = new CustomerserviceassignModel();
            var CustomerData = Models.GetCustomerserviceNewDataSelect(input: new CustomerserviceassignModel.CustomerserDataSelectInputs
            {                
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_CustomerserviceregisterProductDetails = Data.FK_CustomerserviceregisterProductDetails,                
                TransMode = Data.TransMode,
               
            }, companyKey: _userLoginInfo.CompanyKey);


            #endregion :: Fill List ::


            var AssignEmployee = Models.GetEmployeeData(input: new CustomerserviceassignModel.EmployeeGetInput
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                PageMode = 3
            }, companyKey: _userLoginInfo.CompanyKey);

            var ProductList = Common.GetDataViaQuery<CustomerserviceassignModel.Products>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterproductdetails CP LEFT JOIN Product AS P ON P.ID_Product=CP.FK_Product AND P.Cancelled=0 AND P.Passed=1" +
                "LEFT JOIN ComplaintList CL ON CL.ID_ComplaintList=CP.FK_ComplaintList AND CL.Cancelled=0 AND CL.Passed=1 LEFT JOIN Services S ON S.ID_Services=CP.FK_ServiceList",
                SelectFields = "ISNULL(P.ProdName,'') Productname,ISNULL(CL.CompntName,S.ServicesName) ProductComplaint,CP.CSRODescription ProductDescription",
                Criteria = "CP.Cancelled=0  AND CP.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            var OtherProductList = Common.GetDataViaQuery<CustomerserviceassignModel.OtherProducts>(parameters: new APIParameters
            {
                TableName = "Customerserviceregisterothproductdetails cp join ComplaintList cl on cl.ID_ComplaintList=cp.FK_ComplaintList and cl.Cancelled=0 and cl.Passed=1",
                SelectFields = " CSROCompany Company,CSROProduct Product,CSRODescription Description,cl.CompntName Complaint",
                Criteria = "cp.Cancelled=0  AND cp.FK_Customerserviceregister=" + Data.FK_Customerserviceregister,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            // Model.Product = ProductList.Data;
            Model.OtherProducts = OtherProductList.Data;
            //Models.customerDetails = CustomerData.Data;

            if (data.Data != null)
            {
                Model.ID_Customerserviceassign = data.Data[0].ID_Customerserviceassign;
                //Model.Employee = data.Data[0].Employee;
                //Model.FK_Employee = data.Data[0].FK_Employee;
                Model.Visitdate = data.Data[0].Visitdate;
                Model.Visittime = data.Data[0].Visittime;
                Model.Priority = data.Data[0].Priority;
                Model.Remarks = data.Data[0].Remarks;

            }



            return Json(new { Model, CustomerData, AssignEmployee }, JsonRequestBehavior.AllowGet);
        }
        #endregion :: Serice Assign ticket select NEW::

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddCustomerserviceAssignnew(CustomerserviceassignModel.Customerserviceassignviewnew Data)
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

            ModelState.Clear();

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



            CustomerserviceassignModel customerserviceassignModel = new CustomerserviceassignModel();

            var datresponse = customerserviceassignModel.UpdateCustomerserviceassignDatanew(input: new CustomerserviceassignModel.UpdateCustomerserviceassignnew
            {
                UserAction = 1,
                TransMode = "WEB",
                Debug = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_CustomerServiceAssign = Data.ID_Customerserviceassign,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Employee = Data.FK_Employee,
                CSAVisitdate = Data.CSAVisitdate,
                CSAVisittime = Data.CSAVisittime,
                CSAPriority = Data.CSAPriority,
                CSARemarks = Data.CSARemarks,
                Assignees = Data.Assignees is null ? "" : Common.xmlTostring(Data.Assignees),

                ID_CustomerServiceRegisterProductDetails = Data.ID_CustomerServiceRegisterProductDetails,
                LastID = (Data.LastID.HasValue) ? Data.LastID.Value : 0,
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustomerserviceAssignNew(CustomerserviceassignModel.Customerserviceassignviewnew Data)
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

            ModelState.Clear();

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



            CustomerserviceassignModel customerserviceassignModel = new CustomerserviceassignModel();

            var datresponse = customerserviceassignModel.UpdateCustomerserviceassignData(input: new CustomerserviceassignModel.UpdateCustomerserviceassign
            {
                UserAction = 1,
                TransMode = "WEB",
                Debug = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_CustomerServiceAssign = Data.ID_Customerserviceassign,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Employee = Data.FK_Employee,
                CSAVisitdate = Data.CSAVisitdate,
                CSAVisittime = Data.CSAVisittime,
                CSAPriority = Data.CSAPriority,
                CSARemarks = Data.CSARemarks,
                Assignees = Data.Assignees is null ? "" : Common.xmlTostring(Data.Assignees),
                ID_CustomerServiceRegisterProductDetails = Data.ID_CustomerServiceRegisterProductDetails,
                LastID = (Data.LastID.HasValue) ? Data.LastID.Value : 0,
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult GetEmployeeHistory(CustomerserviceassignModel.EmployeeHistoryInput dt)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerserviceassignModel emphis = new CustomerserviceassignModel();
            var data = emphis.GetEmployeeHistory(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceassignModel.EmployeeHistoryInput
            {
                FK_Employee = dt.FK_Employee,
                VisitDate = dt.VisitDate,
                Visittime = dt.Visittime,
                Mode = dt.Mode
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmployeeTicketHistory(CustomerserviceassignModel.EmployeeHistoryInput dt)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerserviceassignModel empTickethis = new CustomerserviceassignModel();
            var data = empTickethis.GetEmployeeTicketHistory(companyKey: _userLoginInfo.CompanyKey, input: new CustomerserviceassignModel.EmployeeHistoryInput
            {
                FK_Employee = dt.FK_Employee,
                VisitDate = dt.VisitDate,
                Visittime = dt.Visittime,
                Mode = dt.Mode
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustomerserviceAssignPriority(CustomerserviceassignModel.Customerserviceassignviewnew Data)
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

            ModelState.Clear();

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



            CustomerserviceassignModel customerserviceassignModel = new CustomerserviceassignModel();

            var datresponse = customerserviceassignModel.UpdateCustomerserviceassignData(input: new CustomerserviceassignModel.UpdateCustomerserviceassign
            {
                UserAction = 2,
                TransMode = "WEB",
                Debug = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_CustomerServiceAssign = Data.ID_Customerserviceassign,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Employee = Data.FK_Employee,
                CSAVisitdate = Data.CSAVisitdate,
                CSAVisittime = Data.CSAVisittime,
                CSAPriority = Data.CSAPriority,
                CSARemarks = Data.CSARemarks,
                Status=Data.Status,
                FK_Attendedby=Data.FK_Attendedby,
                ID_CustomerServiceRegisterProductDetails = Data.ID_CustomerServiceRegisterProductDetails,
                //Assignees = Data.Assignees is null ? "" : Common.xmlTostring(Data.Assignees)
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult UpdateCustomerserviceDeliveryAssign(CustomerserviceassignModel.Customerserviceassignviewnew Data)
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

            ModelState.Clear();

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


            CustomerserviceassignModel customerserviceassignModel = new CustomerserviceassignModel();

            var dataresponse = customerserviceassignModel.UpdateCustomerserviceDeliveryassignData(input: new CustomerserviceassignModel.UpdateCustomerserviceassign
            {

                UserAction = 1,
                TransMode = "CUSA",
                Debug = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_CustomerServiceAssign = Data.ID_Customerserviceassign,
                FK_Customerserviceregister = Data.FK_Customerserviceregister,
                FK_Employee = Data.FK_Employee,
                CSAVisitdate = Data.CSAVisitdate,
                CSAVisittime = Data.CSAVisittime,
                CSAPriority = Data.CSAPriority,
                CSARemarks = Data.CSARemarks,
                ID_CustomerServiceRegisterProductDetails = Data.ID_CustomerServiceRegisterProductDetails,
                LastID = (Data.LastID.HasValue) ? Data.LastID.Value : 0,
                FK_Vehicle=Data.FK_Vehicle,
            },

           companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);

        }
    }
}


