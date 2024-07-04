using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using static PerfectWebERP.Models.AuthorizationReportModel;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class AuthorizationReportController : Controller
    {
        // GET: AuthorizationReport
        public ActionResult AuthorizationReport()

        {
            return View();
        }
        public ActionResult LoadAuthorizationReportForm()
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
            AuthorizationReportModel.Customerlist statusobj = new AuthorizationReportModel.Customerlist();
            var branch = Common.GetDataViaQuery<AuthorizationReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;






            var compname = Common.GetDataViaQuery<AuthorizationReportModel.Customerlist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;

            AuthorizationReportModel objAuth = new AuthorizationReportModel();
            var ModuleList = objAuth.GetModuleList(input: new AuthorizationReportModel.ModeLead { Mode = 108 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.ModuleList = ModuleList.Data;

            //var statusmodeList = objAuth.GeLeadStatusList(input: new AuthorizationReportModel.ModeLead { Mode = 11 }, companyKey: _userLoginInfo.CompanyKey);

            // statusobj.StatusList = statusmodeList.Data;


            return PartialView("_AddAuthorizationReport", statusobj);
        }

        public ActionResult GetSubModule(AuthorizationReportModel.customerview cr)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            #endregion ::  Check User Session to verifyLogin  ::
            AuthorizationReportModel.customerview Acctview = new AuthorizationReportModel.customerview();
            AuthorizationReportModel objact = new AuthorizationReportModel();
            var SubcategoryInfo = objact.GetSubModule(input: new AuthorizationReportModel.SubModule { Mode = 109, Group = cr.ID_MenuGroup }, companyKey: _userLoginInfo.CompanyKey);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult GetReportData(Reportdto input)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AuthorizationReportModel objfld = new AuthorizationReportModel();
            if (input.ReportMode == 1)
            {
                var data = objfld.GetAuthorizationReportdata(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_Module = input.FK_Module,
                    FK_SubModule = input.FK_SubModule,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                }, companyKey: _userLoginInfo.CompanyKey);
                
                
                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = objfld.GetStatusListReportData(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}