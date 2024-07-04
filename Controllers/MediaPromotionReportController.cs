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
    public class MediaPromotionReportController : Controller
    {
        // GET: MediaPromotionReport
        public ActionResult MediaPromotionReport(string mtd, string mgrp)
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
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return View();
        }

        [HttpGet]
        public ActionResult MediaPromotionReportForm(string mtd)
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

            MediaPromotionReportModel.MediaPromotionList Promlist = new MediaPromotionReportModel.MediaPromotionList();

            var branch = Common.GetDataViaQuery<MediaPromotionReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Promlist.BranchList = branch.Data;

            var Media = Common.GetDataViaQuery<MediaPromotionReportModel.MediaList>(parameters: new APIParameters
            {
                TableName = "MediaMaster",
                SelectFields = "ID_MediaMaster AS FK_MediaMaster ,MdaName AS MediaName",
                Criteria = "Cancelled=0 AND Passed=1 AND  FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MediaMaster",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Promlist.MediaList = Media.Data;

            //var SubMedia = Common.GetDataViaQuery<MediaPromotionReportModel.SubMediaList>(parameters: new APIParameters
            //{
            //    TableName = "MediaSubMaster",
            //    SelectFields = "ID_MediaSubMaster as FK_MediaSubMaster,SubMdaName as SubMediaName",
            //    Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);
            //Promlist.SubMediaList = SubMedia.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;

            return PartialView("_LoadMediaPromotionReportForm", Promlist);
        }

        public ActionResult GetSubmediamaster(Int64 FK_MediaMaster)
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

            var data = Common.GetDataViaQuery<MediaPromotionReportModel.SubMediaList>(parameters: new APIParameters
            {
                TableName = "[MediaSubMaster]",
                SelectFields = "[ID_MediaSubMaster] AS FK_MediaSubMaster,[SubMdaName] AS SubMediaName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Media ='" + FK_MediaMaster + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}