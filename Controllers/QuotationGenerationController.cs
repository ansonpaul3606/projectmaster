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
    public class QuotationGenerationController : Controller
    {
       
        public ActionResult Index(string mtd,string mgrp)
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
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            ViewBag.mtd = mtd;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }
        public ActionResult LoadFormQuotationGeneration(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddQuotationGeneration");
        }


        [HttpPost]
        public ActionResult GetQuotationGenerationList(int pageSize, int pageIndex, string Name, string TransModes)
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
            QuotationGenerationModel QuotationGeneration = new QuotationGenerationModel();

            var QuotInfo = QuotationGeneration.GetQuotationGenerationData(companyKey: _userLoginInfo.CompanyKey, input: new QuotationGenerationModel.GetQuotationGen
            {
                ID_QuotationGen = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                TransMode = TransModes
            });

            return Json(new { QuotInfo.Process, QuotInfo.Data, pageSize, pageIndex, totalrecord = (QuotInfo.Data is null) ? 0 : QuotInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetQuotationGenerationItemList(QuotationGenerationModel.QuotationItemDetails data)
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
            QuotationGenerationModel QuotationGeneration = new QuotationGenerationModel();
            var quoItem = QuotationGeneration.GetQuotationItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationGenerationModel.QuotationItemDetails
            {
                ID_QuotationGen = data.ID_QuotationGen,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(quoItem, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewQuotationGeneration(QuotationGenerationModel.QuotationGenerationNew data)
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

            QuotationGenerationModel QuotationGeneration = new QuotationGenerationModel();
            var datresponse = QuotationGeneration.UpdateQuotationGenerationData(input: new QuotationGenerationModel.UpdateQuotationGeneration
            {
                UserAction = 1,
                TransMode = data.TransMode,
                ID_QuotationGen=0,
                QuoNO="",
                QuoDate = data.QuoDate,
                QuoExpireDate = data.QuoExpireDate,
                QuoTerms = data.QuoTerms,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuotationDetails = data.QuotationDetails is null ? "" : Common.xmlTostring(data.QuotationDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                FK_QuotationGen = 0,               
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateQuotationGeneration(QuotationGenerationModel.QuotationGenerationNew data)
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
            
            QuotationGenerationModel QuotationGeneration = new QuotationGenerationModel();
            var datresponse = QuotationGeneration.UpdateQuotationGenerationData(input: new QuotationGenerationModel.UpdateQuotationGeneration
            {
                UserAction = 2    ,
                TransMode = data.TransMode,
                ID_QuotationGen = data.ID_QuotationGen,
                QuoNO = "",
                QuoDate = data.QuoDate,
                QuoExpireDate = data.QuoExpireDate,
                QuoTerms = data.QuoTerms,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuotationDetails = data.QuotationDetails is null ? "" : Common.xmlTostring(data.QuotationDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                FK_QuotationGen = 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteQuotationGeneration(QuotationGenerationModel.DeleteQuotationGeneration data)
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

            QuotationGenerationModel QuotationGeneration = new QuotationGenerationModel();

            var datresponse = QuotationGeneration.DeleteQuotationGenerationData(input: new QuotationGenerationModel.DeleteQuotationGeneration
            {
                ID_QuotationGen = data.@ID_QuotationGen,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetQuotationGenerationReasonList()
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