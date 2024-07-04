using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class StageWiseCollectionReportController : Controller
    {
        // GET: StageWiseCollectionReport
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.DecryptString(mgrp);
            //ViewBag.TransMode = mGrp;
            return View();
        }

        public ActionResult LoadProjectwiseStagesReportForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            StageWiseCollectionReportModel.StageWiseCollectionReportView SVList = new StageWiseCollectionReportModel.StageWiseCollectionReportView();

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Project=1 AND Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<StageWiseCollectionReportModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            SVList.CategoryList = Category.Data;


            return PartialView("_StageWiseCollectionReport", SVList);
        }
        #region[GetStageWiseReportList]
        public ActionResult GetStageWiseReportList(StageWiseCollectionReportModel.StageWisereport StageWisereport)
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

            StageWiseCollectionReportModel stagewise = new StageWiseCollectionReportModel();

            var data = stagewise.GetStageWiseCollectionReport(input: new StageWiseCollectionReportModel.StageWisereport
            {
                AsOnDate = StageWisereport.AsOnDate,
                FK_Project = StageWisereport.FK_Project,
                FK_Category = StageWisereport.FK_Category,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}