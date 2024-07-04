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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ServicesController : Controller
    {
        // GET: Services
            public ActionResult Index()
            {
                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;


            return View();
            }

            public ActionResult LoadServicesForm()
            {


                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            ServicesModel.ServicesListModel servicesList = new ServicesModel.ServicesListModel();


                var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                  companyKey: _userLoginInfo.CompanyKey,
                  procedureName: "ProGetNextNo",
                  parameter: new NextSortOrder
                  {
                      TableName = "Services",
                      FieldName = "SortOrder",
                      Debug = 0
                  });

                servicesList.SortOrder = a.Data[0].NextNo;
            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<ServicesModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);
            servicesList.TaxgroupList= taxgroup.Data;


            return PartialView("_AddServicesForm", servicesList);
            }


            [HttpGet]
            public ActionResult GetServicesList()
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


                ServicesModel services = new ServicesModel();
                var data = services.GetServicesData(input: new ServicesModel.GetServices { FK_Services = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);

                return Json(data, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult GetServicesInfoByID(ServicesModel.ServicesInfoView data)
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

                ServicesModel services = new ServicesModel();
                var servicesInfo = services.GetServicesData(companyKey: _userLoginInfo.CompanyKey, input: new ServicesModel.GetServices { FK_Services = data.ServicesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

                return Json(servicesInfo, JsonRequestBehavior.AllowGet);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddNewServicesDetails(ServicesModel.ServicesInputFromViewModel data)
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
            ModelState.Remove("Paid");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
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

                ServicesModel services = new ServicesModel();


               

                var dataresponse = services.UpdateServicesData(input: new ServicesModel.UpdateServices
                {
                    UserAction = 1,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = "",

                    FK_Services=0,
                    ID_Services = 0,
                    ServicesName = data.Services,
                    ServicesShortName = data.ShortName,
                    ServicesDescription = data.Description,
                    ServicesPaid=data.Paid,
                    FK_AccountHead = data.AccountHead,
                    FK_AccountHeadSub = data.AccountHeadSub,
                    SortOrder = data.SortOrder,
                    FK_TaxGroup = (data.TaxGroupID.HasValue) ? data.TaxGroupID.Value : 0,
                    ServiceChargeIncludeTax = data.IncludeTax,
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult UpdateServicesDetails(ServicesModel.ServicesInputFromViewModel data)
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
            ModelState.Remove("Paid");
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

                ServicesModel services = new ServicesModel();


               

                var dataresponse = services.UpdateServicesData(input: new ServicesModel.UpdateServices
                {
                    UserAction = 2,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = "",

                    FK_Services= data.ServicesID,
                    ID_Services = data.ServicesID,
                    ServicesName = data.Services,
                    ServicesShortName = data.ShortName,
                    ServicesDescription = data.Description,
                    ServicesPaid = data.Paid,
                    FK_AccountHead = data.AccountHead,
                    FK_AccountHeadSub = data.AccountHeadSub,
                    SortOrder = data.SortOrder,
                    FK_TaxGroup = (data.TaxGroupID.HasValue) ? data.TaxGroupID.Value : 0,
                    ServiceChargeIncludeTax = data.IncludeTax,

                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            [ValidateAntiForgeryToken()]
            public ActionResult DeleteServicesInfo(ServicesModel.ServicesInfoView data)
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

                ServicesModel services = new ServicesModel();
                var datresponse = services.DeleteServicesData(input: new ServicesModel.DeleteServices {FK_Services = data.ServicesID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode="" }, companyKey: _userLoginInfo.CompanyKey);
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }



        public ActionResult GetServicesDeleteReasonList()
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


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountHeadDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<ServicesModel.AccountHeadView>(parameters: new APIParameters
            {
                TableName = "AccountHead AH",
                SelectFields = "AH.ID_AccountHead AS AccountHead,AH.AHCode AS AccountCode,AH.AHName AS AHeadName",
                Criteria = "AH.Cancelled =0 AND AH.Passed=1",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAccountSubHeadDetails(int AccountCode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<ServicesModel.AccountSubHeadView>(parameters: new APIParameters
            {
                TableName = "AccountSubHead ASH",
                SelectFields = "ASH.ID_AccountSubHead AS AccountHeadSub,ASH.AHCode AS AccountSHCode,ASH.ASHName AS ASHeadName",
                Criteria = "ASH.Cancelled =0 AND ASH.Passed=1 AND ASH.AHCode=" + AccountCode,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
        );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


    }
}