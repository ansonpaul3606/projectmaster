using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class SLASettingsController : Controller
    {
        // GET: SLASettings
        #region[Index]
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.TransMode = mGrp;
            return View();
            
        }
        #endregion

        #region[SLASettings]
        public ActionResult LoadSLASettings(string mtd, string TransMode)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            SLASettingsModel model = new SLASettingsModel();
            SLASettingsModel.SLASettingsView view = new SLASettingsModel.SLASettingsView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            var TypeList = model.GetPeriodType(input: new SLASettingsModel.ModeLead { Mode = 93 },

            companyKey: _userLoginInfo.CompanyKey);

            view.PeriodType = TypeList.Data;

            var complaintListData = Common.GetDataViaQuery<SLASettingsModel.ComplaintList>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList AS ID_ComplaintList ,CompntName AS CompntName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey
            );
            view.complaintLists = complaintListData.Data;


            var customerCatData = Common.GetDataViaQuery<SLASettingsModel.CustomerCategory>(parameters: new APIParameters
            {
                TableName = "CustomerCategory   ",
                SelectFields = "ID_CustomerCategory AS ID_CustomerCategory ,CuscattyName AS CuscattyName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey
            );
            view.customerCategories = customerCatData.Data;

            var branchtypelist = Common.GetDataViaQuery<SLASettingsModel.BranchTypes>(parameters: new APIParameters
            {
                TableName = "BranchType",
                SelectFields = "ID_BranchType AS BranchTypeID,BTName AS BranchType,FK_BranchMode AS BranchModeID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            view.BranchTypelists = branchtypelist.Data;

            var userrolelist = Common.GetDataViaQuery<SLASettingsModel.Userrole>(parameters: new APIParameters
            {
                TableName = "UserRole",
                SelectFields = "ID_UserRole AS UserRoleID,UsrrlName AS UserRole",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );
            view.UserRolelists = userrolelist.Data;

            return PartialView("_AddSLASettings", view);
        }
        #endregion

        #region[GetSLASettingsList]
        public ActionResult GetSLASettingsList(int pageSize, int pageIndex, string Name, string TransMode)
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
            SLASettingsModel model = new SLASettingsModel();

            var data = model.GetSLASettingslist(input: new SLASettingsModel.SLAViewInput
            {

                TransMode = TransMode,
                FK_SLASettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
                Detailed = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[AddSLASettings]
        public ActionResult AddSLASettings(SLASettingsModel.SLASettingsView view)
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

            SLASettingsModel model = new SLASettingsModel();

            var data = model.UpdateSlaSettingsdetails(new SLASettingsModel.UpdateSLASettings
            {
                UserAction = 1,
                Debug = 0,
                EffectDate = view.EffectDate,
                TransMode = view.TransMode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_SLASettings = 0,
                ID_SLASettings = 0,
                SLASettingsDetails = view.SLASettingsDetails is null ? "" : Common.xmlTostring(view.SLASettingsDetails),
                AlerSettingstData = view.AlerSettingstData is null ? "" : Common.xmlTostring(view.AlerSettingstData),
                Chck_Email = view   .Chck_Email,
                Chck_Notification = view.Chck_Notification,
                Chck_Sms = view.Chck_Sms,
                Chck_WhtsAp = view.Chck_WhtsAp


            },companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetSLAInfoByID]
        public ActionResult GetSLAInfoByID(SLASettingsModel.SLAViewInput viewInput)
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
            #region :: Model validation  ::

            if (!ModelState.IsValid)
            {

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

            SLASettingsModel model = new SLASettingsModel();

            var data = model.GetSLASettingslist(companyKey: _userLoginInfo.CompanyKey, input: new SLASettingsModel.SLAViewInput
            {
                TransMode = viewInput.TransMode,
                PageSize = 0,
                PageIndex = 0,
                FK_SLASettings = viewInput.FK_SLASettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1

            });
            var result = model.GetSLASettingslist(companyKey: _userLoginInfo.CompanyKey, input: new SLASettingsModel.SLAViewInput
            {
                TransMode = viewInput.TransMode,
                PageSize = 0,
                PageIndex = 0,
                FK_SLASettings = viewInput.FK_SLASettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 2

            });

            return Json(new { data,result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetItemCsReasonList]
        public ActionResult GetItemCsReasonList()
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
            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
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
        #endregion

        #region[DeleteSLASettings]
        public ActionResult DeleteSLASettings(SLASettingsModel.DeleteInput delete)
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
            if (!ModelState.IsValid)
            {
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
            SLASettingsModel model = new SLASettingsModel();

            var datresponse = model.DeleteSLASData(input: new SLASettingsModel.DeleteSLASettingsInput
            {
                TransMode = delete.TransMode,
                FK_SLASettings = delete.ID_SLASettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = delete.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetUser]
        public ActionResult GetUser(SLASettingsModel.SLASettingsView input)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "Please login to continue" }, Status = "Session Timeout" }, }, JsonRequestBehavior.AllowGet);
            }
                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                SLASettingsModel model = new SLASettingsModel();
                SLASettingsModel.SLASettingsView view = new SLASettingsModel.SLASettingsView();

            var result = Common.GetDataViaQuery<SLASettingsModel.User>(parameters: new APIParameters
            {
                TableName = "Users",
                SelectFields = "ID_Users AS FK_User,UserCode AS Users",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_UserRole = "+ input.FK_UserRole+" ",

                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            view.UsersList = result.Data;

            return Json(new { result.Process, result.Data, });
        
        }
        #endregion
    }
}