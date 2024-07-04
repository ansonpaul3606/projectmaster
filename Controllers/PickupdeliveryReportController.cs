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
    public class PickupdeliveryReportController : Controller
    {
        // GET: PickupdeliveryReport
        public ActionResult Index()

        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.DepartmentID = _userLoginInfo.FK_Department;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return View();

        }
        public ActionResult LoadFormPickupdeliveryReport()

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


            PickupdeliveryReportModel pickup = new PickupdeliveryReportModel();
            PickupdeliveryReportModel.PickupdeliveryReportView pickupdeliveryview = new PickupdeliveryReportModel.PickupdeliveryReportView();


            var branch = Common.GetDataViaQuery<PickupdeliveryReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            pickupdeliveryview.BranchList = branch.Data;



            var departmentlist = Common.GetDataViaQuery<PickupdeliveryReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            pickupdeliveryview.DepartmentList = departmentlist.Data;

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            return PartialView("_AddPickupdeliveryReportView", pickupdeliveryview);
        }

        public ActionResult GetPickupdeliveryreportdetail(PickupdeliveryReportModel.PickupdeliveryReportView Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            PickupdeliveryReportModel objfld = new PickupdeliveryReportModel();

            var data = objfld.GetPayRollAttendanceDetailsData(input: new PickupdeliveryReportModel.Pickupdeliveryreportinput
            {
                ReportMode = Data.ID_Report,
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                FK_Branch = Data.BranchID,
                FK_Department = Data.DepartmentID,
                FK_Employee = Data.EmployeeID,
                //FK_AllowanceType = (Data.FK_AllowanceType.HasValue) ? Data.FK_AllowanceType.Value : 0,

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