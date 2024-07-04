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

using System.IO;
using System.Web.Hosting;



namespace PerfectWebERP.Controllers
{
    public class FeedbackquestionSettingsController : Controller
    {
        // GET: FeedbackquestionSettings
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
        public ActionResult LoadFeedbackquestionSettings(string mtd)
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
            ViewBag.BranchID = _userLoginInfo.FK_Branch;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            FeedbackquestionSettingsModel fedback = new FeedbackquestionSettingsModel();
            FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview view = new FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview();

            var TypeList = fedback.GetPagemodeList(input: new FeedbackquestionSettingsModel.ModeLead { Mode = 118 },

           companyKey: _userLoginInfo.CompanyKey);

            view.ModuleList = TypeList.Data;

            var TypeLists = fedback.GetPagemodeList(input: new FeedbackquestionSettingsModel.ModeLead { Mode = 119 },

            companyKey: _userLoginInfo.CompanyKey);

            view.PageList = TypeLists.Data;

            return PartialView("_AddFeedbackquestionSettings",view);
        }
        
        [HttpPost]
        public ActionResult FillPagelist(int ModuleID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            FeedbackquestionSettingsModel fedback = new FeedbackquestionSettingsModel();
            FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview data = new FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview();

            var TypeLists = fedback.GetPagemodeList(input: new FeedbackquestionSettingsModel.ModeLead { Mode = 119,Group= ModuleID },
            companyKey: _userLoginInfo.CompanyKey);
            data.PageList = TypeLists.Data;
            return Json(data.PageList, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult GetQuestionsAddDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            FeedbackquestionSettingsModel fedback = new FeedbackquestionSettingsModel();
            FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview data = new FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview();


            APIParameters apiSub = new APIParameters
            {
                TableName = "Feedback",
                SelectFields = "ID_Feedback ID_Question,Question AS Question, CASE WHEN Mode=1 THEN 'Customer Portal' ELSE 'Portal' END AS ShowIn, CASE WHEN FeedbackType=1 THEN 'Rating' ELSE 'Review' END AS FeedBackType,CASE WHEN QuestionMode=1 THEN 'Employee' ELSE CASE WHEN QuestionMode=2 THEN 'Product' ELSE  'InfraStructure' END END AS QuestionMode",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Lits = Common.GetDataViaQuery<FeedbackquestionSettingsModel.Questiontablepopup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            data.QuestiontablepopupList = Lits.Data;
            return Json(data.QuestiontablepopupList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFeedbackquestionSettings(FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview data)
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





            #endregion :: Model validation  ::

            FeedbackquestionSettingsModel feed = new FeedbackquestionSettingsModel();


          
            var dataresponse = feed.UpdatefeedbackQuestionsettingsData(input: new FeedbackquestionSettingsModel.UpdateFeedbackQuestion
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_FeedBack = 0,
                ID_FeedbackQuestionSettings = 0,
                FK_FeedbackQuestionSettings=0,
                EffectFrom = data.EffectFrom,
                EffectTo = data.EffectTo,
                Module = data.Module,
                Page = data.Page,
                QuestionDetails = data.QuestionDetails is null ? "" : Common.xmlTostring(data.QuestionDetails),
                Active=data.Active,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0
              
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFeedbackquestionSettings(FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview data)
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





            #endregion :: Model validation  ::

            FeedbackquestionSettingsModel feedbck = new FeedbackquestionSettingsModel();



            var dataresponse = feedbck.UpdatefeedbackQuestionsettingsData(input: new FeedbackquestionSettingsModel.UpdateFeedbackQuestion
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_FeedbackQuestionSettings = data.ID_FeedbackQuestionSettings,
                ID_FeedbackQuestionSettings = data.ID_FeedbackQuestionSettings,
                EffectFrom = data.EffectFrom,
                EffectTo = data.EffectTo,
                Module = data.Module,
                Page = data.Page,
                QuestionDetails = data.QuestionDetails is null ? "" : Common.xmlTostring(data.QuestionDetails),
                Active = data.Active,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetFeedbackquestionSettingsList(int pageSize, int pageIndex, string Name, string TransModes)
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

            ModelState.Remove("ReasonID");

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
            FeedbackquestionSettingsModel feedbcks = new FeedbackquestionSettingsModel();

            var FeedBackupSettingInfo = feedbcks.GetFeedbackquestionSettingsListviewData(companyKey: _userLoginInfo.CompanyKey, input: new FeedbackquestionSettingsModel.FeedBackupQuestionSettingViewID

            {
                FK_FeedbackQuestionSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,

                TransMode = TransModes

            });

            return Json(new { FeedBackupSettingInfo.Process, FeedBackupSettingInfo.Data, pageSize, pageIndex, totalrecord = (FeedBackupSettingInfo.Data is null) ? 0 : FeedBackupSettingInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult GetFeedBackupQuestionSettingInfoByID(FeedbackquestionSettingsModel.FeedBackupQuestionSettingViewID data)
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


            FeedbackquestionSettingsModel Model = new FeedbackquestionSettingsModel();

            var SettingInfo = Model.GetFeedbackquestionSettingsListviewDatabyID(companyKey: _userLoginInfo.CompanyKey, input: new FeedbackquestionSettingsModel.FeedBackupQuestionSettingViewID

            {
                FK_FeedbackQuestionSettings = data.FK_FeedbackQuestionSettings,
              
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,


            });


            return Json(SettingInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFeedBackupQuestionSettingsReasonList()
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


        public ActionResult DeleteFeedBackupQuestionSettings(FeedbackquestionSettingsModel.FeedbackquestionSettingsModelview data)
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

            FeedbackquestionSettingsModel modelfeed = new FeedbackquestionSettingsModel();

            var datresponse = modelfeed.DeleteFeedBackQuestionSettingData(input: new FeedbackquestionSettingsModel.DeleteFeedBackQuestionSetting
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_FeedbackQuestionSettings = data.FK_FeedBack,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}