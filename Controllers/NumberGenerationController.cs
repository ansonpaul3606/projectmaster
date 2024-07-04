using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class NumberGenerationController : Controller
    {
       
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult NumberGenerationCommonLoadForm()
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

            NumberGenerateModel objNGM = new NumberGenerateModel();
            NumberGenerateModel.NumberGenerateNew objList = new NumberGenerateModel.NumberGenerateNew();
            var generationCriteria= objNGM.GetModulesList(input: new NumberGenerateModel.NumberGenData { Mode = 16 }, companyKey: _userLoginInfo.CompanyKey);
            objList.GenerationCriteria = generationCriteria.Data.AsEnumerable();           
            var subModule = objNGM.GetModulesList(input: new NumberGenerateModel.NumberGenData { Mode = 31 }, companyKey: _userLoginInfo.CompanyKey);
            objList.SubModuleList = subModule.Data.AsEnumerable();
            var numRstPeriod = objNGM.GetModulesList(input: new NumberGenerateModel.NumberGenData { Mode = 14 }, companyKey: _userLoginInfo.CompanyKey);
            objList.ResetPeriod = numRstPeriod.Data.AsEnumerable();
            var typeList = objNGM.GetModulesList(input: new NumberGenerateModel.NumberGenData { Mode = 30 }, companyKey: _userLoginInfo.CompanyKey);
            objList.Type = typeList.Data.AsEnumerable();

            MenuGroupModel MenuGroup = new MenuGroupModel();
            var MenuGroupInfo = MenuGroup.GetMenuGroupData(companyKey: _userLoginInfo.CompanyKey, input: new MenuGroupModel.GetMenuGroup
            {
                FK_MenuGroup = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = ""
            });
            objList.ModuleList = MenuGroupInfo.Data.AsEnumerable();

            return PartialView("_AddNumberGenerate", objList);
        }
        [HttpPost]
        public ActionResult GetDataSearch()
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
            NumberGenerateModel objNGM = new NumberGenerateModel();
            var data = objNGM.GetModulesList(input: new NumberGenerateModel.NumberGenData { Mode = 32 }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddCommonNumberGeneration(NumberGenerateModel.NumberGeneration data)
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

            NumberGenerateModel num = new NumberGenerateModel();
            var datresponse = num.UpdateCommonNumberingSettings(input: new NumberGenerateModel.NumberGenerationUpdate
            {
                UserAction = 1,
                Debug = 0,
                ID_CommonSettings =data.ID_CommonSettings,
                EffectDate=data.EffectDate,
                CSNoGenCriteria = data.CSNoGenCriteria,
                CSNoResetPeriod = data.CSNoResetPeriod,
                FK_Module = data.FK_Module,
                FK_SubModule=data.FK_SubModule,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CommonSettingsDetails = data.CommonSettingsDetails is null ? "" : Common.xmlTostring(data.CommonSettingsDetails),
                FK_CommonSettings = 0
              
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateNewCommonNumbergeneration(NumberGenerateModel.NumberGeneration data)
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
            NumberGenerateModel num = new NumberGenerateModel();
            var datresponse = num.UpdateCommonNumberingSettings(input: new NumberGenerateModel.NumberGenerationUpdate
            {
                UserAction = 2,
                Debug = 0,
                ID_CommonSettings = data.ID_CommonSettings,
                EffectDate = data.EffectDate,
                CSNoGenCriteria = data.CSNoGenCriteria,
                CSNoResetPeriod = data.CSNoResetPeriod,
                FK_Module = data.FK_Module,
                FK_SubModule = data.FK_SubModule,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CommonSettingsDetails = data.CommonSettingsDetails is null ? "" : Common.xmlTostring(data.CommonSettingsDetails),
                FK_CommonSettings = 0

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetNumbergenerationList(int pageSize, int pageIndex, string Name)
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

            NumberGenerateModel numbergenerate = new NumberGenerateModel();

            var data = numbergenerate.GetNumbergenerateData(companyKey: _userLoginInfo.CompanyKey, input: new NumberGenerateModel.GetNumbergenerate
            {
                FK_CommonSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = "",
                Name = Name
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getModeList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            NumberGenerateModel.CommonsettingsViewList CmnstListObj = new NumberGenerateModel.CommonsettingsViewList();
            NumberGenerateModel objpaymode = new NumberGenerateModel();
            var statusmodeList = objpaymode.GetModulesList(input: new NumberGenerateModel.NumberGenData { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            CmnstListObj.ProcessList = statusmodeList.Data;
            return Json(statusmodeList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteNumbergen(NumberGenerateModel.NumberGenerateDelete data)
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
            NumberGenerateModel model = new NumberGenerateModel();
            Output dataresponse = model.DeleteNumbergenerateData(companyKey: _userLoginInfo.CompanyKey, input: new NumberGenerateModel.DeletenumgenMethod
            {
                FK_CommonSettings = data.ID_CommonSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.FK_Reason,
                TransMode = ""

            });

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetNumgenenerationInfoByID(NumberGenerateModel.NumberGenerateModelView data)
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

            NumberGenerateModel numgenmodel = new NumberGenerateModel();
            var ngInfo = numgenmodel.GetNumbergenerateDataByID(input: new NumberGenerateModel.GetNumbergenerateById
            {
                FK_CommonSettings = data.ID_CommonSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);
            var ngDtlsInfo = numgenmodel.GetNumbergenerateDetailsDataByID(input: new NumberGenerateModel.GetNumbergenerateById
            {
                FK_CommonSettings = data.ID_CommonSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { ngInfo, ngDtlsInfo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNumberGenerateReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
    }
}