using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.IncentiveReportModel;

namespace PerfectWebERP.Controllers
{
    public class IncentiveReportController : Controller
    {
        // GET: IncentiveReport
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            return View();
        }

        public ActionResult LoadIncetiveReport()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_UserRole = _userLoginInfo.FK_UserRole;
            ViewBag.IsAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.IsManger = _userLoginInfo.UsrrlManager;
            IncentiveReportModel objMg = new IncentiveReportModel();
            IncentiveReportModel.IncentiveReportView locationView = new IncentiveReportModel.IncentiveReportView();


            var branch = Common.GetDataViaQuery<IncentiveReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            locationView.BranchList = branch.Data;

            var departmentlist = Common.GetDataViaQuery<IncentiveReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );

            locationView.DepartmentList = departmentlist.Data;

            //DesignationList

            var designationList = Common.GetDataViaQuery<IncentiveReportModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );
            locationView.DesignationList = designationList.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            return PartialView("_AddIncentiveReport", locationView);
        }

        [HttpPost]
        public ActionResult GetReportData(IncentiveReportdto input)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            IncentiveReportModel objfld = new IncentiveReportModel();
            

            var data = objfld.GetReportdata(input:new IncentiveReportdto {
                FK_company=_userLoginInfo.FK_Company,
                BranchID=input.BranchID,
                TransMode=input.TransMode,
                DesignationID=input.DesignationID,
                EmployeeID=input.EmployeeID,
                FromDate=input.FromDate,
                ToDate=input.ToDate,
                Mode=input.Mode,
                PageIndex=input.PageIndex+1,
                PageSize=input.PageSize,
                ASonDate=input.ASonDate
               
            } , companyKey: _userLoginInfo.CompanyKey);


            // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
    }
}