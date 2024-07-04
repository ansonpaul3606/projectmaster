using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class FeedBackSettingsController : Controller
    {
        // GET: FeedBackSettings
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

        #region[LoadFeedBackSettigs]
        public ActionResult LoadFeedBackSettigs(string mtd)
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

            FeedBackSettingsModel feed = new FeedBackSettingsModel();
            FeedBackSettingsModel.FeedBackSettingsView view = new FeedBackSettingsModel.FeedBackSettingsView();
            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            var TypeList = feed.GeLeadStatusList(input: new FeedBackSettingsModel.ModeLead { Mode = 77 },

            companyKey: _userLoginInfo.CompanyKey);

            view.ActionStatusList = TypeList.Data;

            var TypeLists = feed.GeLeadStatusList(input: new FeedBackSettingsModel.ModeLead { Mode = 78 },

            companyKey: _userLoginInfo.CompanyKey);

            view.FeedStatus = TypeLists.Data;

            var Reviewlist = feed.GeLeadStatusList(input: new FeedBackSettingsModel.ModeLead { Mode = 96 },

            companyKey: _userLoginInfo.CompanyKey);

            view.ReviewStatus = Reviewlist.Data;

            var quesModes = feed.GeLeadStatusList(input: new FeedBackSettingsModel.ModeLead { Mode = 113},
            companyKey:_userLoginInfo.CompanyKey);

            view.quesMode = quesModes.Data;

            var emoj = feed.GetemojisList(input: new FeedBackSettingsModel.ModeLead { Mode = 114 },
                companyKey: _userLoginInfo.CompanyKey);
            view.Emojilist = emoj.Data;

            return PartialView("_AddFeedBackSettings", view);

        }
        #endregion

        #region[AddFeedbackSettings]
        public ActionResult AddFeedbackSettings(FeedBackSettingsModel.FeedBackSettingsView view )
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

            FeedBackSettingsModel feedBack = new FeedBackSettingsModel();
            var data = feedBack.Updatefeedbacksettingsdetails(new FeedBackSettingsModel.FeedbackSettingsUpdate
            {
                Question = view.Question,
                Mode = view.Mode,
                FeedbackType = view.FeedbackType,
                FeedbackSettingsdetails = view.FeedbackSettingsdetails is null ? "" : Common.xmlTostring(view.FeedbackSettingsdetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserAction = 1,
                Debug = 0,
                ID_Feedback = 0,
                FK_Feedback= 0,
                EntrBy = _userLoginInfo.EntrBy,
                QueMode = view.QueMode


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateFeedbackSettings]
        public ActionResult UpdateFeedbackSettings(FeedBackSettingsModel.FeedBackSettingsView view)
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

            FeedBackSettingsModel feedBack = new FeedBackSettingsModel();

            var datares = feedBack.Updatefeedbacksettingsdetails(new FeedBackSettingsModel.FeedbackSettingsUpdate
            {
                Question = view.Question,
                Mode = view.Mode,
                FeedbackType = view.FeedbackType,
                FeedbackSettingsdetails = view.FeedbackSettingsdetails is null ? "" : Common.xmlTostring(view.FeedbackSettingsdetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserAction = 2,
                Debug = 0,
                ID_Feedback = view.ID_Feedback,
                FK_Feedback = view.ID_Feedback,
                EntrBy = _userLoginInfo.EntrBy,
                QueMode = view.QueMode


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetFeedbackSettingsList]
        public ActionResult GetFeedbackSettingsList(int pageSize, int pageIndex, string Name)
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

            FeedBackSettingsModel feed = new FeedBackSettingsModel();

            var datares = feed.GetFeedbackSettingsdetails(input: new FeedBackSettingsModel.FeedbackSettingsInput
            {

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name = Name,
                FK_Feedback = 0,
                TransMode = "",
                Detailed = 0
                


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares.Process, datares.Data, pageIndex, pageSize, totalrecord = (datares.Data is null) ? 0 : datares.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetFeedbackSetrInfoByID]
        public ActionResult GetFeedbackSetrInfoByID(FeedBackSettingsModel.FeedbackSettingsInput input)
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

            FeedBackSettingsModel feed = new FeedBackSettingsModel();

            var data = feed.GetFeedbackSettingsSublist(new FeedBackSettingsModel.FeedbackSettingsInput
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name = "",
                FK_Feedback = input.FK_Feedback,
                TransMode = "",
                Detailed = 1

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { data}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetFeedBackSettingsReasonList]
        public ActionResult GetFeedBackSettingsReasonList()
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

        #region[DeleteFeedbackSettings]
        public ActionResult DeleteFeedbackSettings(FeedBackSettingsModel.DeleteInput delete)
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
            FeedBackSettingsModel model = new FeedBackSettingsModel();

            var datresponse = model.DeleteItemCsData(input: new FeedBackSettingsModel.DeleteFeedbk
            {
                TransMode = delete.TransMode,
                FK_Feedback = delete.FK_Feedback,
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


    }
}