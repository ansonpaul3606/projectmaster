using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.EMICollectionReportModel;

namespace PerfectWebERP.Controllers
{
    public class EMICollectionReportController : Controller
    {
        string Reportname = "";
        string TransMode = "RptTransMode";
        // GET: EMICollectionReport
        public ActionResult EMICollectionReport()
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

            return View();
        }

        public ActionResult LoadEMICollectionReport()
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
            EMICollectionReportModel.ReportViewlist statusobj = new EMICollectionReportModel.ReportViewlist();
            var branch = Common.GetDataViaQuery<EMICollectionReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;

            EMICollectionReportModel objpaymode = new EMICollectionReportModel(); 

            var CommonListAmount = objpaymode.GetCommonList(input: new EMICollectionReportModel.ModeLead { Mode = 122,Group = 1 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.CommonListAmount = CommonListAmount.Data;

            var CommonListDate = objpaymode.GetCommonList(input: new EMICollectionReportModel.ModeLead { Mode = 122, Group = 2 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.CommonListDate = CommonListDate.Data;

            var GroupByList = objpaymode.GetCommonList(input: new EMICollectionReportModel.ModeLead { Mode = 122, Group = 3 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.GroupByList = GroupByList.Data;

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            ViewBag.CompCategory = _userLoginInfo.CompCategory;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;

            return PartialView("_LoadEMICollectionReport", statusobj);
        }

        [HttpPost]
        public ActionResult GetEMIReportData(EMICollectionReportModel.InputEMICollectionReport input)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            EMICollectionReportModel objfld = new EMICollectionReportModel();
            string TranSMode = "EMIRPT";

            if (input.ReportMode == 1)
            {
                var data = objfld.GetEMICollectionListReportdata(input: new GetEMICollectionReport
                {
                    ReportMode = input.ReportMode,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    FK_Branch = input.FK_Branch,
                    AmountCriteria = input.AmountCriteria,
                    AmountFrom = input.AmountFrom,
                    AmountTo = input.AmountTo,
                    FK_Employee = input.Employee_ID,
                    DateCriteria = input.DateCriteria,
                    GroupCriteria = input.GroupCriteria,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Area = input.FK_Area

                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Handle the case when input.ReportMode is not equal to 1
                // You can return an appropriate response, error message, or handle it as needed.
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}