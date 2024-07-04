using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.CustomerTicketsModel;

namespace PerfectWebERP.Controllers
{
    public class StockReportListController : Controller
    {
        // GET: StockReportList
        string Reportname = "";
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
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            return View();
        } 

                   

        public ActionResult LoadFormStocklistReport()
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


            StockReportList stock = new StockReportList();
            StockReportList.StockReportListView stockview = new StockReportList.StockReportListView();

            var TypeList = stock.GeLeadStatusList(input: new StockReportList.ModeLead { Mode = 92 },

            companyKey: _userLoginInfo.CompanyKey);

            stockview.ActionStatusList = TypeList.Data;

            var branch = Common.GetDataViaQuery<StockReportList.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            stockview.BranchList = branch.Data;
            var branchTo = Common.GetDataViaQuery<StockReportList.BranchTo>(parameters: new APIParameters
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
            var categorylist = Common.GetDataViaQuery<StockReportList.Category>(parameters: new APIParameters
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

            var departmentlist = Common.GetDataViaQuery<StockReportList.Department>(parameters: new APIParameters
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
            var departmentlistTo = Common.GetDataViaQuery<StockReportList.DepartmentTo>(parameters: new APIParameters
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

            var ModeLists = Common.GetDataViaQuery<StockReportList.ModeList>(parameters: new APIParameters
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

            return PartialView("_AddStockReportList", stockview);
        }

        public ActionResult getModeCriteriaList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            StockReportList.StockReportListView stockview = new StockReportList.StockReportListView();
            StockReportList jpaymode = new StockReportList();
            var criteriamodeList = jpaymode.GroupingCriteriaList(input: new StockReportList.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            // stockview.CriteriaList = criteriamodeList.Data;
            return Json(criteriamodeList.Data, JsonRequestBehavior.AllowGet);

        }

        //public ActionResult GetProductlistlList(StockReportList.StockReportListView Data)
        //{
        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    StockReportList reportModel = new StockReportList();

        //    //var datares = reportModel.GetProductlistlList(new StockReportList.Productlistviewinput
        //    //{
        //    //     ReportMode=Data.ReportMode, 
        //    //     FromDate=Data.FromDate,
        //    //     ToDate = Data.FromDate,
        //    //     FK_Company = _userLoginInfo.FK_Company,
        //    //     FK_Machine = _userLoginInfo.FK_Machine,
        //    //     FK_Department = Data.FK_Department,
        //    //     FK_Product = Data.FK_Product,
        //    //     FK_Branch = Data.FK_Branch,
        //    //     FK_Supplier = Data.FK_Supplier,
        //    //     FK_Category = Data.FK_Category,
        //    //     Criteria = Data.Criteria,
        //    //     FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //    //     Debug = Data.Debug,                 
        //    //     EntrBy = Data.EntrBy,
        //    //    FK_BillType = Data.FK_BillType,
        //    //    TableCount = Data.TableCount,
        //    //    FK_BranchTo = Data.FK_BranchTo,
        //    //    FK_Employee = Data.FK_Employee,
        //    //    FK_BranchFrom = Data.FK_BranchFrom,
        //    //    ID_Master = Data.ID_Master,
        //    //    BillNo = Data.BillNo,
        //    //    TranSMode = Data.TranSMode,
        //    //    FK_DepartmentFrom = Data.FK_DepartmentFrom,
        //    //    FK_DepartmentTo = Data.FK_DepartmentTo,
        //    //    FK_EmployeeFrom = Data.FK_EmployeeFrom,
        //    //    FK_EmployeeTo = Data.FK_EmployeeTo,
        //    //    FK_PaymentMethod = Data.FK_PaymentMethod,
        //    //    FK_ProductNew = Data.FK_ProductOld,
        //    //    HeaderColor = Data.HeaderColor,
        //    //    FK_ProductOld = Data.FK_ProductOld,
        //    //    ActionStatus = Common.xmlTostring(Data.ActionlistArray.Select(a => new StockReportList.Actionlist { FK_Mode = a }).ToList()),
        //    //}, companyKey: _userLoginInfo.CompanyKey);


          







        //}






    }
}