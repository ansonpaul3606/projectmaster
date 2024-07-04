using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ItemConversionController : Controller
    {
        // GET: ItemConversion
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

        #region[LoadItemConversion]
        public ActionResult LoadItemConversion(string mtd, string TransMode)
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

            ItemConversionModel item = new ItemConversionModel();
            ItemConversionModel.ItemConversionView view = new ItemConversionModel.ItemConversionView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            var TypeList = item.GeLeadStatusList(input: new ItemConversionModel.ModeLead { Mode = 84 },

            companyKey: _userLoginInfo.CompanyKey);

            view.ConversionMode = TypeList.Data;


            var OpDepartmentList = Common.GetDataViaQuery<ItemConversionModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                    companyKey: _userLoginInfo.CompanyKey
            );
            view.DepartmentList = OpDepartmentList.Data;

            return PartialView("_AddItemConversion", view);

        }
        #endregion

        #region[GetAllConvertedItem]
        public ActionResult GetAllConvertedItem(ItemConversionModel.ItemConversionView item)
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
            ItemConversionModel model = new ItemConversionModel();

            var result = model.GetItemlist(new ItemConversionModel.GetItemInput
            {
                FK_Product=item.FK_Product,
                ICDate=item.ICDate,
                FK_Department = item.FK_Department,
                EntrBy =_userLoginInfo.EntrBy,
                FK_Company =_userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { result, data = result.Data, result.Process }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[AddItemConversiondata]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddItemConversiondata(ItemConversionModel.ItemConversionView item)
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
            ItemConversionModel model = new ItemConversionModel();

            var datares = model.UpdateItemConversiondetails(input: new ItemConversionModel.UpdateItemConversion
            {
                UserAction = 1,
                TransMode = item.TransMode,
                ID_ItemConversion = 0,
                ICDate = item.ICDate,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ItemConversion = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                ICMode = item.ICMode,
                Debug = 0,
                FK_Department = item.FK_Department,
                FK_Product = item.FK_Product,
                LastID = (item.LastID.HasValue) ? item.LastID.Value : 0,

                ItemConversionSubDetails = item.ItemConversionSubDetails is null ? "" : Common.xmlTostring(item.ItemConversionSubDetails)

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetItemConversionList]
        public ActionResult GetItemConversionList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ItemConversionModel item = new ItemConversionModel();

            var data = item.GetICList(input: new ItemConversionModel.ICViewInput
            {

                TransMode = TransMode,
                FK_ItemConversion = 0,
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
        
        #region[GetItemInfoByID]
        public ActionResult GetItemInfoByID(ItemConversionModel.ICViewInput input)
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
            ItemConversionModel item = new ItemConversionModel();

            var datares = item.GetItemGrid(input: new ItemConversionModel.ICViewInput
            {
                TransMode = input.TransMode,
                PageSize = 0,
                PageIndex = 0,
                FK_ItemConversion = input.FK_ItemConversion,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetItemReasonList]
        public ActionResult GetItemReasonList()
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

        #region[DeleteItemData]
        public ActionResult DeleteItemData(ItemConversionModel.DeleteInput input)
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

            ItemConversionModel item = new ItemConversionModel();

            var dltdata = item.DeleteItemData(input: new ItemConversionModel.DeleteItemdata
            {
                TransMode = input.TransMode,
                FK_ItemConversion = input.ID_ItemConversion,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = input.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = dltdata }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[UpdateItemConversiondata]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateItemConversiondata(ItemConversionModel.ItemConversionView item)
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
            ItemConversionModel model = new ItemConversionModel();

            var datares = model.UpdateItemConversiondetails(input: new ItemConversionModel.UpdateItemConversion
            {
                UserAction = 2,
                TransMode = item.TransMode,
                ID_ItemConversion = item.FK_ItemConversion,
                ICDate = item.ICDate,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ItemConversion = item.FK_ItemConversion,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                ICMode = item.ICMode,
                Debug = 0,
                FK_Department = item.FK_Department,
                FK_Product = item.FK_Product,
                LastID = (item.LastID.HasValue) ? item.LastID.Value : 0,

                ItemConversionSubDetails = item.ItemConversionSubDetails is null ? "" : Common.xmlTostring(item.ItemConversionSubDetails)

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}