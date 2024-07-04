using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ServiceReportController : Controller
    {
        // GET: CustomerServiceReport
        public ActionResult ServiceReport()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return View();
        }
        public ActionResult LoadServiceReportForm()
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
            ServiceReportModel.Customerlist statusobj = new ServiceReportModel.Customerlist();
            var branch = Common.GetDataViaQuery<ServiceReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
         



            statusobj.BranchList = branch.Data;

            var product = Common.GetDataViaQuery<ServiceReportModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "ID_Product,ProdName",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.ProductList = product.Data;

            var CompntTypeList = Common.GetDataViaQuery<ServiceReportModel.ComplaintType>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList,CompntName",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

            companyKey: _userLoginInfo.CompanyKey);
            statusobj.ComplaintTypeList = CompntTypeList.Data;

            var Service = Common.GetDataViaQuery<ServiceReportModel.ServiceType>(parameters: new APIParameters
            {
                TableName = "Services",
                SelectFields = "ID_Services AS ID_Service,ServicesName AS SeviceName",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

           companyKey: _userLoginInfo.CompanyKey);
            statusobj.ServiceTypeList = Service.Data;

            var actiontypelist = Common.GetDataViaQuery<ServiceReportModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);
            statusobj.Actntyplists = actiontypelist.Data;
            var compname = Common.GetDataViaQuery<ServiceReportModel.Customerlist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;

            var NetAction = Common.GetDataViaQuery<ServiceReportModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = " ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
           );
            statusobj.NextActionList = NetAction.Data;

            ServiceReportModel objpaymode = new ServiceReportModel();
            //var statusmodeList = objpaymode.GeLeadStatusList(input: new CustomerTicketsModel.ModeLead { Mode = 6 }, companyKey: _userLoginInfo.CompanyKey);

            //statusobj.StatusList = statusmodeList.Data;

            var statusmodeList = objpaymode.GeLeadStatusList(input: new ServiceReportModel.ModeLead { Mode = 11 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.StatusList = statusmodeList.Data;

            var ReplacementmodeList = objpaymode.GetReplacemntList(input: new ServiceReportModel.RplcmntMode { Mode = 8 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.ReplacementList = ReplacementmodeList.Data;

            var BillModemodeList = objpaymode.GetBillTypeList(input: new ServiceReportModel.RBillTypeMode { Mode = 57 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.BillModeList = BillModemodeList.Data;

            var catglist = Common.GetDataViaQuery<ServiceReportModel.Categorydata>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = " ID_Category,CatName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
           );

            //var catglist = objpaymode.GetCategorylist(input: new ServiceReportModel.ModeLead2 { Mode = 20 },
            //   companyKey: _userLoginInfo.CompanyKey);

            var CriteriaList = objpaymode.GetGroupCriteriaList(input: new ServiceReportModel.CriteriaMode { Mode = 131, Group=1 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.CriteriaList = CriteriaList.Data;

            statusobj.categorytyps = catglist.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            return PartialView("_ServiceReportForm", statusobj);
        }

        public ActionResult getModeList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ServiceReportModel.Customerlist statusobj = new ServiceReportModel.Customerlist();
            ServiceReportModel objpaymode = new ServiceReportModel();
            var statusmodeList = objpaymode.GeLeadStatusList(input: new ServiceReportModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.StatusList = statusmodeList.Data;
            return Json(statusmodeList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getCriteriaList(Int32 mode,long group)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ServiceReportModel.Customerlist statusobj = new ServiceReportModel.Customerlist();
            ServiceReportModel objpaymode = new ServiceReportModel();
            var CriteriaList = objpaymode.GetGroupCriteriaList(input: new ServiceReportModel.CriteriaMode { Mode = mode, Group=group }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.CriteriaList = CriteriaList.Data;
            return Json(CriteriaList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetEmployeeLeadDefault()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadGenerateModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode, BT.FK_BranchMode FK_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department, BT.BTName,B.BrName,D.DeptName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

);


           

            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}