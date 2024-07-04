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
    public class StockReportController : Controller
    {
        // GET: StockReport
        public ActionResult StockReportIndex()
        {
         
                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                ViewBag.Username = _userLoginInfo.UserName;
                ViewBag.UserRole = _userLoginInfo.UserRole;
                ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return View();
          
        }


        public ActionResult LoadFormStockReport()
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

          
            StockReportModel stock = new StockReportModel();
            StockReportModel.StockReportView stockview =  new StockReportModel.StockReportView();
           

            var branch = Common.GetDataViaQuery<StockReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            stockview.BranchList = branch.Data;
            var branchTo = Common.GetDataViaQuery<StockReportModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchIDTo,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            stockview.BranchListTo = branchTo.Data;
            var Companyname = Common.GetDataViaQuery<SalesReportModel.salelist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName Companyname",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            stockview.Companyname = Companyname.Data[0].Companyname;
            var categorylist = Common.GetDataViaQuery<StockReportModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = " ID_Category AS CategoryID,CatName AS CategoryName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );


            stockview.CategoryList = categorylist.Data;

            var departmentlist = Common.GetDataViaQuery<StockReportModel.Department>(parameters: new APIParameters
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
         var departmentlistTo = Common.GetDataViaQuery<StockReportModel.DepartmentTo>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentIDTo,DeptName as DepartmentNameTo",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
             );


            stockview.DepartmentListTo = departmentlistTo.Data;

            var ModeLists = Common.GetDataViaQuery<StockReportModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeNames,M.Mode Modes",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            stockview.ModeList = ModeLists.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            return PartialView("_AddStockReportview", stockview);
        }

        public ActionResult getModeCriteriaList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            StockReportModel.StockReportView stockview = new StockReportModel.StockReportView();
            StockReportModel jpaymode = new StockReportModel();
            var criteriamodeList = jpaymode.GroupingCriteriaList(input: new StockReportModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
           // stockview.CriteriaList = criteriamodeList.Data;
            return Json(criteriamodeList.Data, JsonRequestBehavior.AllowGet);

        }

    }
}