using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ItemConversionSettingsController : Controller
    {
        // GET: ItemConversionSettings
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

        #region[LoadItemConversionSettings]
        public ActionResult LoadItemConversionSettings(string mtd, string TransMode)
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

            ItemConversionSettingsModel item = new ItemConversionSettingsModel();
            ItemConversionSettingsModel.ItemConversionSettingsView view = new ItemConversionSettingsModel.ItemConversionSettingsView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            return PartialView("_AddItemConversionSettings", view);

        }
        #endregion

        #region[AddItemConversionSettings]
        public ActionResult AddItemConversionSettings(ItemConversionSettingsModel.ItemConversionSettingsView view)
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

            ItemConversionSettingsModel item = new ItemConversionSettingsModel();

            var data = item.UpdateConversiondetails(new ItemConversionSettingsModel.ItemUpdate
            {

                ItemConversionSettingsDetails = view.ItemDataDetails is null ? "" : Common.xmlTostring(view.ItemDataDetails),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserAction = 1,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                FK_ItemConversionSettings = 0,
                ID_ItemConversionSettings = 0,
                EffectDate = view.EffectDate,
                TransMode = view.TransMode


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateItemConversionSettings]
        public ActionResult UpdateItemConversionSettings(ItemConversionSettingsModel.ItemConversionSettingsView item)
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

            ItemConversionSettingsModel model = new ItemConversionSettingsModel();

            var data = model.UpdateConversiondetails(new ItemConversionSettingsModel.ItemUpdate
            {

                ItemConversionSettingsDetails = item.ItemDataDetails is null ? "" : Common.xmlTostring(item.ItemDataDetails),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserAction = 2,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                FK_ItemConversionSettings = item.FK_ItemConversionSettings,
                ID_ItemConversionSettings = item.ID_ItemConversionSettings,
                EffectDate = item.EffectDate,
                TransMode = item.TransMode


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetItemList]
        [HttpPost]
        public ActionResult GetConversionList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ItemConversionSettingsModel model = new ItemConversionSettingsModel();

            var data = model.GetItemCnSettingslist(input: new ItemConversionSettingsModel.ItemCSViewInput
            {

                TransMode = TransMode,
                FK_ItemConversionSettings = 0,
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

        #region[GetItemCSInfoByID]
        public ActionResult GetItemCSInfoByID(ItemConversionSettingsModel.ItemCSViewInput item)
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

            ItemConversionSettingsModel model = new ItemConversionSettingsModel();

            var data = model.GetItemCnSettingslist(companyKey: _userLoginInfo.CompanyKey, input: new ItemConversionSettingsModel.ItemCSViewInput
            {
                TransMode = item.TransMode,
                PageSize = 0,
                PageIndex = 0,
                FK_ItemConversionSettings = item.FK_ItemConversionSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1

            });

            return Json(new { data }, JsonRequestBehavior.AllowGet);
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

        #region[DeleteItemCS]
        public ActionResult DeleteItemCS(ItemConversionSettingsModel.DeleteInput delete)
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
            ItemConversionSettingsModel model = new ItemConversionSettingsModel();

            var datresponse = model.DeleteItemCsData(input: new ItemConversionSettingsModel.DeleteItemCs
            {
                TransMode = delete.TransMode,
                FK_ItemConversionSettings = delete.ID_ItemConversionSettings,
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