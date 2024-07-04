using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class IncentiveSettingsMasterController : Controller
    {
        // GET: IncentiveSettingsMaster
        public ActionResult IncentiveSettingsMaster(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadFormIncentiveSettingsMaster(string mtd)
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
            CommonMethod objCmnMethod = new CommonMethod();
            IncentiveSettingsMasterModel.IncentiveSettingsViewlist statusobj = new IncentiveSettingsMasterModel.IncentiveSettingsViewlist();
            var Category = Common.GetDataViaQuery<IncentiveSettingsMasterModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.CategoryList = Category.Data;

            IncentiveSettingsMasterModel objpaymode = new IncentiveSettingsMasterModel();
            var CalculationBasedList = objpaymode.GetCalculationBasedList(input: new IncentiveSettingsMasterModel.ModeLead { Mode = 130, Group = 1 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.CalculationBased = CalculationBasedList.Data;

            var CalculationTypeList = objpaymode.GetCalculationTypeList(input: new IncentiveSettingsMasterModel.ModeLead { Mode = 130, Group = 2 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.CalculationType = CalculationTypeList.Data;

            var IncentiveActivityList = objpaymode.GetIncentiveActivityList(input: new IncentiveSettingsMasterModel.ModeLead { Mode = 130, Group = 3 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.IncentiveActivity = IncentiveActivityList.Data;

            var IncentiveType = Common.GetDataViaQuery<IncentiveSettingsMasterModel.IncentiveType>(parameters: new APIParameters
            {
                TableName = "IncentiveType",
                SelectFields = "ID_IncentiveType AS FK_IncentiveType ,IncTName AS Incentivetype, IncTModule",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_IncentiveType",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.IncentiveType = IncentiveType.Data;

            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddIncentiveSettingsMaster", statusobj);
        }
        public ActionResult GetIncentiveActivityList(IncentiveSettingsMasterModel.ModeLead data)
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

            IncentiveSettingsMasterModel objpaymode = new IncentiveSettingsMasterModel();

            var IncentiveActivityList = objpaymode.GetIncentiveActivityList(input: new IncentiveSettingsMasterModel.ModeLead { Mode = 130, Group = 3 }, companyKey: _userLoginInfo.CompanyKey);


            return Json(IncentiveActivityList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddIncentiveSettings(IncentiveSettingsMasterModel.IncentiveSettingsView data)
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


            IncentiveSettingsMasterModel incentive = new IncentiveSettingsMasterModel();

            var datresponse = incentive.UpdateIncentiveSettingsData(input: new IncentiveSettingsMasterModel.IncentiveSettingsUpdate
            {
                UserAction = 1,
                TransMode = data.TransMode,
                ID_IncentiveSettings = 0,
                FK_IncentiveType = data.FK_IncentiveType,
                INSEffectDate = data.INSEffectDate,
                INSCalcBasedon = data.INSCalcBasedon,
                INSCalcType = data.INSCalcType,
                INSPeriod = data.INSPeriod,
                INSActivity = data.INSActivity,
                INSActive = data.INSActive,
                IncSettingsDetails = data.IncSettingsDetails is null ? "" : Common.xmlTostring(data.IncSettingsDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = _userLoginInfo.FK_Branch,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateIncentiveSettings(IncentiveSettingsMasterModel.IncentiveSettingsView data)
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


            IncentiveSettingsMasterModel incentive = new IncentiveSettingsMasterModel();

            var datresponse = incentive.UpdateIncentiveSettingsData(input: new IncentiveSettingsMasterModel.IncentiveSettingsUpdate
            {
                UserAction = 2,
                TransMode = data.TransMode,
                ID_IncentiveSettings = data.ID_IncentiveSettings,
                FK_IncentiveType = data.FK_IncentiveType,
                INSEffectDate = data.INSEffectDate,
                INSCalcBasedon = data.INSCalcBasedon,
                INSCalcType = data.INSCalcType,
                INSPeriod = data.INSPeriod,
                INSActivity = data.INSActivity,
                INSActive = data.INSActive,
                IncSettingsDetails = data.IncSettingsDetails is null ? "" : Common.xmlTostring(data.IncSettingsDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = _userLoginInfo.FK_Branch,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Dependency = data.Dependency,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncentiveSettingsList(int pageSize, int pageIndex, string Name, string TransModes)
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
            IncentiveSettingsMasterModel Inctv = new IncentiveSettingsMasterModel();

            var data = Inctv.GetIncentivesettingsList(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveSettingsMasterModel.GetIncentivesettings
            {
                ID_IncentiveSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = _userLoginInfo.FK_Branch,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode,
                //Mode=0,

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetIncentiveSettingsReasonList()
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
                EntrBy = _userLoginInfo.EntrBy
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncentivesettingsListbyId(IncentiveSettingsMasterModel.IncentiveSettingsView data)
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
            IncentiveSettingsMasterModel incentive = new IncentiveSettingsMasterModel();


            var IncentiveInfo = incentive.GetIncentivesettingsList(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveSettingsMasterModel.GetIncentivesettings
            {
                ID_IncentiveSettings = data.ID_IncentiveSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch=_userLoginInfo.FK_Branch,
                Mode = 0,
            });
            var IncentiveDetailsInfo = incentive.GetIncentivesettingsDetails(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveSettingsMasterModel.GetIncentivesettings
            {
                ID_IncentiveSettings = data.ID_IncentiveSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = _userLoginInfo.FK_Branch,
                Mode = 1,
            });
            return Json(new { IncentiveInfo, IncentiveDetailsInfo }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteIncentiveSettings(IncentiveSettingsMasterModel.DeleteIncentiveSettings data)
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

            IncentiveSettingsMasterModel obj = new IncentiveSettingsMasterModel();

            var datresponse = obj.DeleteIncentiveSettingsData(input: new IncentiveSettingsMasterModel.DeleteIncentiveSettings
            {
                ID_IncentiveSettings = data.ID_IncentiveSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch= _userLoginInfo.FK_Branch,
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}