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
    public class BuyBackReportController : Controller
    {
        string Reportname = "";
        string TransMode = "RptTransMode";
        // GET: BuyBackReport
        public ActionResult BuyBackReport(string mtd)
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
            ViewBag.CompCategory = _userLoginInfo.CompCategory;
            ViewBag.Company = _userLoginInfo.Company;
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadBuyBackReportForm(string mtd)
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
            BuyBackReportModel.BuyBacklist statusobj = new BuyBackReportModel.BuyBacklist();

            var branch = Common.GetDataViaQuery<BuyBackReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;

            var Category = Common.GetDataViaQuery<BuyBackReportModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey);
            statusobj.CategoryList = Category.Data;

            var compname = Common.GetDataViaQuery<BuyBackReportModel.BuyBacklist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;


            BuyBackReportModel objpaymode = new BuyBackReportModel();            

            var BuybackModeList = objpaymode.GetBuyBackModeList(input: new BuyBackReportModel.ModeLead { Mode = 128 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.ModeList = BuybackModeList.Data;         

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            ViewBag.CompCategory = _userLoginInfo.CompCategory;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_LoadBuyBackReport", statusobj);
        }
        [HttpPost]
        public ActionResult GetBuybackReport(BuyBackReportModel.InputBuybackReport input)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            BuyBackReportModel objfld = new BuyBackReportModel();
            string TranSMode = "BYBRPT";

            if (input.ReportMode == 1)
            {
                var data = objfld.GetBuybackReportdata(input: new BuyBackReportModel.GetBuybackReport
                {
                    ReportMode = input.ReportMode,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    FK_Branch = input.FK_Branch,
                    FK_Category = input.FK_Category,
                    FK_Product = input.FK_Product,
                    Mode = input.Mode,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Company = _userLoginInfo.FK_Company,

                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}