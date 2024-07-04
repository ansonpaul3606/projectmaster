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
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CheckListController : Controller
    {
        //test for TFS
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

        public ActionResult LoadFormCheckList(string mtd,string trns)
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
            CheckListModel.CheckListView sortno = new CheckListModel.CheckListView();
            CheckListModel model = new CheckListModel();
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "CheckList",
                FieldName = "SortOrder",
                Debug = 0
            });

            sortno.SortOrder = a.Data[0].NextNo;
           
            var ChecklistCategorylist= model.GetChecklistCategorylist(companyKey: _userLoginInfo.CompanyKey, input: new CheckListModel.GetChecklistCategory
            {
                Pagemode = 125,
                Critrea1 = 0,
                Critrea2 = 0,
                Critrea3 = "",
                Critrea4 = "",
                ID = 0,
                TransMode= "PRCLC",
                FK_Company = _userLoginInfo.FK_Company

            });

            sortno.ChecklistCategorylist = ChecklistCategorylist.Data;

            var InspectionList = model.GetInspectionListData(input: new CheckListModel.ModeValue { Mode = 106 },
            companyKey: _userLoginInfo.CompanyKey);
            sortno.InspectionListData = InspectionList.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            if (trns == "PRCL")
            {
                ViewBag.showig = true;
            }
            else
            {
                ViewBag.showig = false;
            }

            return PartialView("_AddCheckListForm", sortno);
        }

        [HttpPost]
        public ActionResult GetCheckListList(int pageSize, int pageIndex, string Name, string TransModes)
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
            CheckListModel CheckList = new CheckListModel();

            var ChecInfo = CheckList.GetCheckListData(companyKey: _userLoginInfo.CompanyKey, input: new CheckListModel.GetCheckList
            {
                FK_CheckList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransModes,

            });

            return Json(new { ChecInfo.Process, ChecInfo.Data, pageSize, pageIndex, totalrecord = (ChecInfo.Data is null) ? 0 : ChecInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetChecklistInfo(CheckListModel.CheckListID data)
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
            CheckListModel checkListModel = new CheckListModel();
            var checkInfo = checkListModel.GetCheckListData(companyKey: _userLoginInfo.CompanyKey, input: new CheckListModel.GetCheckList
            {
                FK_CheckList = data.ID_CheckList,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode=data.TransMode
            });
            return Json(checkInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewCheckList(CheckListModel.CheckListView data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("CheckListID");
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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

            CheckListModel CheckList = new CheckListModel();

            var datresponse = CheckList.UpdateCheckListData(input: new CheckListModel.UpdateCheckList
            {
                UserAction = 1,
               
                CkLstName = data.CkLstName,
                CkLstShortName = data.CkLstShortName,
                SortOrder = data.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                //checklistcategory= JsonConvert.SerializeObject(data.ChecklistArray),
                checklistcategory = Common.xmlTostring(data.ChecklistArray.Select(a => new CheckListModel.CheckListType { FK_CheckListType = a }).ToList()),
                ID_CheckList = 0,
                InspectionType=data.CkLstInspectionType,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCheckList(CheckListModel.CheckListView data)
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
            ModelState.Remove("SortOrder");
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

            CheckListModel CheckList = new CheckListModel();
            var datresponse = CheckList.UpdateCheckListData(input: new CheckListModel.UpdateCheckList
            {
                UserAction = 2,
                ID_CheckList = data.ID_CheckList,
                CkLstName = data.CkLstName,
                CkLstShortName = data.CkLstShortName,
                SortOrder = data.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                //checklistcategory = JsonConvert.SerializeObject(data.ChecklistArray),
                checklistcategory = Common.xmlTostring(data.ChecklistArray.Select(a => new CheckListModel.CheckListType { FK_CheckListType = a }).ToList()),
                InspectionType = data.CkLstInspectionType,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCheckList(CheckListModel.DeleteCheckList data)
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

            CheckListModel CheckList = new CheckListModel();

            var datresponse = CheckList.DeleteCheckListData(input: new CheckListModel.DeleteCheckList
            {
                FK_CheckList = data.FK_CheckList,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason},
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCheckListReasonList()
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
                EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

    }
}