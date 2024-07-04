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
    public class GSTReportController : Controller
    {
        // GET: GSTReport
        public ActionResult GSTReportIndex()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();

        }

        public ActionResult LoadFormGSTReport()
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


            GSTReportModel GST = new GSTReportModel();
            GSTReportModel.GSTReportView gstview = new GSTReportModel.GSTReportView();


            var branch = Common.GetDataViaQuery<GSTReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            gstview.BranchList = branch.Data;

            var TaxType = Common.GetDataViaQuery<GSTReportModel.TaxType>(parameters: new APIParameters
            {
                TableName = "TaxType ",
                SelectFields = "ID_TaxType AS TaxTypeID,TaxtyName AS TaxTypeName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            gstview.TaxTypeList = TaxType.Data;

            var Companyname = Common.GetDataViaQuery<GSTReportModel.GSTReportView>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName Companyname",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            gstview.Companyname = Companyname.Data[0].Companyname;

            var ModeList = Common.GetDataViaQuery<GSTReportModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            gstview.ModeList = ModeList.Data;
            var departmentlist = Common.GetDataViaQuery<GSTReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            gstview.DepartmentList = departmentlist.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
           
            return PartialView("_AddGSTReportview", gstview);
        }
    }
}