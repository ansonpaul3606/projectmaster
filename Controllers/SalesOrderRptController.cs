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
    public class SalesOrderRptController : Controller
    {
        // GET: SalesOrderRpt
        public ActionResult SalesOrderReport()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            return View();
        }
        public ActionResult LoadsalesodReportForm()
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


            SalesReportModel salesrpt = new SalesReportModel();
            SalesReportModel.salelist statusobj = new SalesReportModel.salelist();
            var branch = Common.GetDataViaQuery<SalesReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.BranchList = branch.Data;

            var catglist = Common.GetDataViaQuery<SalesReportModel.Categorydata>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = " ID_Category,CatName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );


            statusobj.categorytyps = catglist.Data;
            var Companyname = Common.GetDataViaQuery<SalesReportModel.salelist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName Companyname",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            statusobj.Companyname = Companyname.Data[0].Companyname;
           
            var deplst = Common.GetDataViaQuery<SalesReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepId,DeptName as Depname",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            statusobj.deprtmnt = deplst.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;

            return PartialView("_AddSalesOrderReport", statusobj);
        }
    }
}