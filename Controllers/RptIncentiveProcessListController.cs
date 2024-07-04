using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class RptIncentiveProcessListController : Controller
    {
        // GET: RptIncentiveProcessList

        string Reportname = "";
        string TransMode = "RptTransMode";
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
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            return View();
        }
        public ActionResult LoadIncentiveprocesslistReport(string mtd, string TransMode)
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


            RptIncentiveProcessListModel viewlist = new RptIncentiveProcessListModel();
            RptIncentiveProcessListModel.RptIncentiveProcessListModelview stockview = new RptIncentiveProcessListModel.RptIncentiveProcessListModelview();

          

            var branch = Common.GetDataViaQuery<RptIncentiveProcessListModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            stockview.BranchList = branch.Data;
         
        
            var departmentlist = Common.GetDataViaQuery<RptIncentiveProcessListModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            stockview.DepartmentList = departmentlist.Data;


            var Incentivetypelist = Common.GetDataViaQuery<RptIncentiveProcessListModel.Incentivetype>(parameters: new APIParameters
            {

                TableName = "IncentiveType",
                SelectFields = "ID_IncentiveType as IncentiveTypeID,IncTName as IncentiveType",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey
        );


            stockview.IncentivetypeList = Incentivetypelist.Data;

            var modeList = viewlist.GetIncentivePayModeList(input: new RptIncentiveProcessListModel.CriteriaMode { Mode = 133,Group=2 }, companyKey: _userLoginInfo.CompanyKey);
            stockview.PaymentModeList = modeList.Data;

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.DepartmentID = _userLoginInfo.FK_Department;
            return PartialView("_AddRptIncentiveProcessList", stockview);
        }

        [HttpPost]
        public ActionResult GetIncentiveProcessReportList(RptIncentiveProcessListModel.RptIncentiveProcessinput input)
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
            string TranSMode = input.TransMode;
          
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            RptIncentiveProcessListModel objfld = new RptIncentiveProcessListModel();
            var datares = objfld.GetIncentiveProcessReportdataTab(new RptIncentiveProcessListModel.RptIncentiveProcessinputview
            {
                ToDate = input.ToDate,
                FromDate = input.FromDate,
               FK_Branch=input.FK_Branch,
               FK_Department=input.FK_Department,
               FK_Employee=input.FK_Employee,
               FK_IncentiveType=input.FK_IncentiveType,
               ReportType=input.ReportType,
                GroupBy=input.GroupBy,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
              
            }, companyKey: _userLoginInfo.CompanyKey);
            Session[TransMode] = TranSMode;
            Reportname = (_userLoginInfo.UserName + TranSMode).Replace(" ", ""); ;
            Session[Reportname] = datares.Data;

            return Json(true, JsonRequestBehavior.AllowGet);

        }
      
        public ActionResult getReportModeList( long group)
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
            RptIncentiveProcessListModel.RptIncentiveProcessListModelview stockview = new RptIncentiveProcessListModel.RptIncentiveProcessListModelview();
            RptIncentiveProcessListModel viewlist = new RptIncentiveProcessListModel();
            var ReportModeList = viewlist.GetIncentiveReportList(input: new RptIncentiveProcessListModel.CriteriaMode { Mode = 133, Group = group }, companyKey: _userLoginInfo.CompanyKey);            
            return Json(ReportModeList, JsonRequestBehavior.AllowGet);

        }
    }
}