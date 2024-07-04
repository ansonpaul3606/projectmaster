using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class DashboardSettingsController : Controller
    {
        // GET: DashboardSettings
        public ActionResult Index(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadFormDashboardSettings(string mtd)
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

            DashboardSettingsModel objAl = new DashboardSettingsModel();
            DashboardSettingsModel.DashboardSettingsInit objDSInit = new DashboardSettingsModel.DashboardSettingsInit();
            

            var userrolelist = Common.GetDataViaQuery<DashboardSettingsModel.Userrole>(companyKey: _userLoginInfo.CompanyKey, parameters: new APIParameters
            {
                TableName = "UserRole",
                SelectFields = "ID_UserRole AS UserRoleID,UsrrlName AS UserRole",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",
                SortFields = "",
                GroupByFileds = ""
            });
            objDSInit.UserRoleList = userrolelist.Data.AsEnumerable();

            var periodTypeList = objAl.GetPeriodType(input: new DashboardSettingsModel.ModeInput { Mode = 91 }, companyKey: _userLoginInfo.CompanyKey);
            objDSInit.PeriodDetails = periodTypeList.Data.AsEnumerable();

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddDashboardSettings", objDSInit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult getUserList(AuthorizationLevelModel.Usersrelated data)
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
            DashboardSettingsModel objDS = new DashboardSettingsModel();
            var dataresponse = objDS.GetDashboardSettingsUsersnamelist(companyKey: _userLoginInfo.CompanyKey, input: new DashboardSettingsModel.Usersrelated
            {
                FK_BranchMode = 0,               
                FK_UserRole = data.FK_UserRole,
                FK_Company = _userLoginInfo.FK_Company
            });
            return Json(dataresponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewDashboardSettings(DashboardSettingsModel.UpdateDashboardSettings data)
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
            DashboardSettingsModel DashboardSettings = new DashboardSettingsModel();

            var datresponse = DashboardSettings.UpdateDashboardSettingsData(input: new DashboardSettingsModel.UpdateDashboardSettings
            {
                UserAction = 1,
                Debug = 0,               
                ID_DashboardSettings=data.ID_DashboardSettings,
                EffectDate = data.EffectDate,
                FK_UserGroup = data.FK_UserGroup,
                FK_User = data.FK_User,
                FK_Mode=data.FK_Mode,
                FK_Company = _userLoginInfo.FK_Company,
                ModuleList = data.ModuleList,
                TileList = data.TileList,
                ChartList = data.ChartList,
                TileMode = data.TileMode,
                ChartMode = data.ChartMode,
                EntrBy = _userLoginInfo.EntrBy,                
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetDashboardSettingsList(int pageSize, int pageIndex, string Name)
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
            DashboardSettingsModel objDS = new DashboardSettingsModel();
            var data = objDS.GetDashboardSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new DashboardSettingsModel.GetDashboardSettingsSelect
            {
                FK_DashboardSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,              
                EntrBy = _userLoginInfo.EntrBy,              
                PageIndex = pageIndex + 1,
                PageSize = pageSize,               
                Name = Name
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetDashboardSettingsInfo(DashboardSettingsModel.GetDashboardSettingsSelect DashboardSettingsInfo)
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
            DashboardSettingsModel objDS = new DashboardSettingsModel();
            var alInfo = objDS.GetDashboardSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new DashboardSettingsModel.GetDashboardSettingsSelect
            {
                FK_DashboardSettings = DashboardSettingsInfo.FK_DashboardSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = 0,
                PageSize = 0,
                Name = ""
            });

            return Json(new { alInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateDashboardSettings(DashboardSettingsModel.UpdateDashboardSettings data)
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
            DashboardSettingsModel DashboardSettings = new DashboardSettingsModel();

            var datresponse = DashboardSettings.UpdateDashboardSettingsData(input: new DashboardSettingsModel.UpdateDashboardSettings
            {
                UserAction = 2,
                Debug = 0,
                ID_DashboardSettings = data.ID_DashboardSettings,
                EffectDate = data.EffectDate,
                FK_UserGroup = data.FK_UserGroup,
                FK_User = data.FK_User,
                FK_Mode = data.FK_Mode,
                FK_Company = _userLoginInfo.FK_Company,
                ModuleList = data.ModuleList,
                TileList = data.TileList,
                ChartList = data.ChartList,
                TileMode = data.TileMode,
                ChartMode = data.ChartMode,
                EntrBy = _userLoginInfo.EntrBy,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteDashboardSettings(DashboardSettingsModel.DeleteDashboardSettings data)
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
            DashboardSettingsModel obj = new DashboardSettingsModel();
            Output datresponse = obj.DeleteDashboardSettingsData(input: new DashboardSettingsModel.DeleteDashboardSettings
            {
                FK_DashboardSettings = data.FK_DashboardSettings,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,              
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDashboardSettingsReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
    }
}