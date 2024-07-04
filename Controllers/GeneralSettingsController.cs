using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class GeneralSettingsController : Controller
    {
        // GET: GeneralSettings
        #region[Index]
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();

        }
        #endregion

        #region[LoadGeneralsettings]
        public ActionResult LoadGeneralsettings(string mtd)
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

            GeneralSettingsModel general = new GeneralSettingsModel();
            GeneralSettingsModel.GeneralSettingsView view = new GeneralSettingsModel.GeneralSettingsView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            return PartialView("_AddGeneralSettings", view);
        }
        #endregion

        #region[AddGeneralSettings]
        public ActionResult AddGeneralSettings(GeneralSettingsModel.GeneralSettingsView view)
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
            GeneralSettingsModel general = new GeneralSettingsModel();

            var data = general.Updategeneralsettingsdetails(new GeneralSettingsModel.GeneralSettingsUpdate
            {
                GsData = view.GsData,
                GsField = view.GsField,
                GsModule = view.GsModule,
                GsValue = view.GsValue,
                FK_Company = _userLoginInfo.FK_Company,
                ID_GeneralSettings = 0,
                FK_GeneralSettings = 0,
                UserAction = 1,
                Debug = 0,
                FK_Branch = 0,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[UpdateGeneralSettings]
        public ActionResult UpdateGeneralSettings(GeneralSettingsModel.GeneralSettingsView settingsView)
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
            GeneralSettingsModel general = new GeneralSettingsModel();

            var datares = general.Updategeneralsettingsdetails(new GeneralSettingsModel.GeneralSettingsUpdate
            {
                GsData = settingsView.GsData,
                GsField = settingsView.GsField,
                GsModule = settingsView.GsModule,
                GsValue = settingsView.GsValue,
                FK_Branch = 0,
                FK_Company = _userLoginInfo.FK_Company,
                ID_GeneralSettings = settingsView.ID_GeneralSettings,
                FK_GeneralSettings = settingsView.ID_GeneralSettings,
                UserAction = 2,
                Debug = 0,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region[GetGeneralSettingsList]
        public ActionResult GetGeneralSettingsList(int pageSize, int pageIndex, string Name)
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
            GeneralSettingsModel general = new GeneralSettingsModel();


            var datares = general.Getgenerealsettingsdetails(input: new GeneralSettingsModel.GeneralSettingsInput
            {

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name = Name,
                FK_GeneralSettings = 0,


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares.Process, datares.Data, pageIndex, pageSize, totalrecord = (datares.Data is null) ? 0 : datares.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
    
        #endregion

    }
}