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

namespace PerfectWebERP.Controllers
{
    public class ShortageMarkingController : Controller
    {
        // GET: ShortageMarking
        public ActionResult ShortageMarking(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
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

        [HttpGet]
        public ActionResult LoadShortageMarkingForm(string mtd)
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

            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            ShortageMarkingModel.ShortageMarkingView list = new ShortageMarkingModel.ShortageMarkingView();
            ShortageMarkingModel Shortage = new ShortageMarkingModel();
            
            var catglist = Common.GetDataViaQuery<ShortageMarkingModel.Categorydata>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = " ID_Category AS ID_Category,CatName AS CategoryName",
                Criteria = "Cancelled=0  AND Passed=1 AND Project=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            list.CategoryList = catglist.Data;

            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "ID_Unit AS ID_Unit,UnitName AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var unit = Common.GetDataViaQuery<ShortageMarkingModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);

            list.UnitList = unit.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser;
            return PartialView("_LoadShortageMarkingForm", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddShortageDetails(ShortageMarkingModel.ShortageView data)
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



            ShortageMarkingModel shortage = new ShortageMarkingModel();

            var datresponse = shortage.UpdateShortageDetails(input: new ShortageMarkingModel.UpdateShortage
            {
                UserAction = 1,                
                TransMode = data.TransMode,
                ShTransDate = data.ShTransDate,
                FK_Supplier = data.FK_Supplier,
                FK_Purchase = data.FK_Purchase,
                ShortageProductDetails = data.ShortageProductDetails is null ? "" : Common.xmlTostring(data.ShortageProductDetails),
                ShNetAmount = data.ShNetAmount,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,                
                ID_Shortage = 0,
                LastID=(data.LastID.HasValue) ? data.LastID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetShortageList(int pageSize, int pageIndex, string Name)
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

            ShortageMarkingModel shortage = new ShortageMarkingModel();
            var ShortageInfo = shortage.GetShortageData(companyKey: _userLoginInfo.CompanyKey, input: new ShortageMarkingModel.GetShortage
            {
                FK_Shortage = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            return Json(new { ShortageInfo.Process, ShortageInfo.Data, pageSize, pageIndex, totalrecord = (ShortageInfo.Data is null) ? 0 : ShortageInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetShortageInfoByID(ShortageMarkingModel.ShortageView data)
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
            ShortageMarkingModel shortage = new ShortageMarkingModel();


            var ShortageInfo = shortage.GetShortageData(companyKey: _userLoginInfo.CompanyKey, input: new ShortageMarkingModel.GetShortage
            {
                FK_Shortage = data.ShortageID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = 0,
            });

            var ShortagePdtDetails = shortage.GetShortageProductDetails(companyKey: _userLoginInfo.CompanyKey, input: new ShortageMarkingModel.GetShortage
            {
                FK_Shortage = data.ShortageID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = 1,
            });


            return Json(new { ShortageInfo, ShortagePdtDetails }, JsonRequestBehavior.AllowGet);
        }
    }
}