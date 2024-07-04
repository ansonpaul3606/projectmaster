using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class GeneralModuleSettingsController : Controller
    {
        // GET: GeneralModuleSettings
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
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }

        #region[LoadGeneralModuleSettings]
        public ActionResult LoadGeneralModuleSettings(string mtd, string TransMode)
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

            GeneralModuleSettingsModel.GenModSettingsView view = new GeneralModuleSettingsModel.GenModSettingsView();
           
            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

           // var StandbyChk = Common.GetDataViaQuery<GeneralModuleSettingsModel.GeneralSettingsModel>(parameters: new APIParameters
           // {
           //     TableName = "GeneralSettings",
           //     SelectFields = "GsModule as GsModule,GsValue AS GsValue,GsField AS GsField",
           //     Criteria = "GsModule='LFLR'AND FK_Company=" + _userLoginInfo.FK_Company,
           //     SortFields = "",
           //     GroupByFileds = ""
           // },
           //companyKey: _userLoginInfo.CompanyKey);



            return PartialView("_AddGenModSettings", view);
        }
        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveGeneralModuleSettings(GeneralModuleSettingsModel.UpdateGeneralModuleSettings data)
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

            GeneralModuleSettingsModel GeneralModuleSettings = new GeneralModuleSettingsModel();
            var datresponse = GeneralModuleSettings.UpdateGeneralModuleSettingsDetails(input: new GeneralModuleSettingsModel.UpdateGeneralModuleSettingInput
            {

                EffectDate = data.EffectDate,
                SecModule = data.SecModule,
                FK_Company = _userLoginInfo.FK_Company,
                SettingsDetails = data.SettingsDetails is null ? "" : Common.xmlTostring(data.SettingsDetails),
                FK_Branch= _userLoginInfo.FK_Branch,
                FK_Machine= _userLoginInfo.FK_Machine,
                EntrBy=_userLoginInfo.EntrBy,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetGeneralModuleSettingsDetails(GeneralModuleSettingsModel.GetGeneralModuleSettingsDetails data)
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

           

            GeneralModuleSettingsModel GeneralModuleSettingsModel = new GeneralModuleSettingsModel();
            var GeneralModuleSettingsModelDtls = GeneralModuleSettingsModel.GetGeneralModuleSettingsDetailsList(companyKey: _userLoginInfo.CompanyKey, input: new GeneralModuleSettingsModel.GetGeneralModuleSettingsDetails
            {
                AsOnDate = DateTime.Now,
                SecModule = data.SecModule,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            });


            return Json(GeneralModuleSettingsModelDtls, JsonRequestBehavior.AllowGet);
        }
    }
}