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
    public class PayRollReportController : Controller
    {
        // GET: PayRollReport
        public ActionResult Index()

        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.DepartmentID = _userLoginInfo.FK_Department;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return View();

        }


        public ActionResult LoadFormPayRollReport()

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


            PayRollReportModel payroll = new PayRollReportModel();
            PayRollReportModel.PayRollReportView payrollview = new PayRollReportModel.PayRollReportView();


            var branch = Common.GetDataViaQuery<PayRollReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            payrollview.BranchList = branch.Data;
         
            var Companyname = Common.GetDataViaQuery<SalesReportModel.salelist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName Companyname",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            payrollview.Companyname = Companyname.Data[0].Companyname;
           

            var departmentlist = Common.GetDataViaQuery<PayRollReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            payrollview.DepartmentList = departmentlist.Data;

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;


            return PartialView("_AddPayrollreport", payrollview);
        }

        public ActionResult IndexPayroll()

        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.DepartmentID = _userLoginInfo.FK_Department;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            return View();

        }
        public ActionResult LoadFormPayRollAttendanceReport()

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


            PayRollReportModel payroll = new PayRollReportModel();
            PayRollReportModel.PayRollReportView payrollview = new PayRollReportModel.PayRollReportView();


            var branch = Common.GetDataViaQuery<PayRollReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            payrollview.BranchList = branch.Data;

        

            var departmentlist = Common.GetDataViaQuery<PayRollReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            payrollview.DepartmentList = departmentlist.Data;

            var Allowancetype = Common.GetDataViaQuery<PayRollReportModel.AllowanceTypeList>(parameters: new APIParameters
            {
                TableName = "AllowanceType",
                SelectFields = "ID_AllowanceType AS FK_AllowanceType,ALWName AS Allowancetype",
                Criteria = "cancelled=0 AND Passed=1  AND ALWMode=2 AND ALWType=11 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey

    );
            payrollview.AllowanceTypeList = Allowancetype.Data;

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;

            return PartialView("_AddPayrollattendancereport", payrollview);
        }

        public ActionResult GetPayrollattendancereportdetail(PayRollReportModel.PayRollReportView Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            PayRollReportModel objfld = new PayRollReportModel();

            var data = objfld.GetPayRollAttendanceDetailsData(input: new PayRollReportModel.Payrollattendancereportinput
            {
                ReportMode = Data.ID_Report,
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                FK_Branch = Data.BranchID,
                FK_Department = Data.DepartmentID,
                FK_Employee=Data.EmployeeID,
                FK_AllowanceType= (Data.FK_AllowanceType.HasValue) ? Data.FK_AllowanceType.Value : 0,
               
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }
    }
}