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
    public class ProjectReportsController : Controller
    {
        // GET: ProjectReports
        public ActionResult ProjectReports()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_User = _userLoginInfo.ID_Users;
            return View();
        }
        public ActionResult LoadProjectReportsForm()
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
            ProjectReportsModel.Customerlist statusobj = new ProjectReportsModel.Customerlist();
            var branch = Common.GetDataViaQuery<ProjectReportsModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;

            var product = Common.GetDataViaQuery<ProjectReportsModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "ID_Product,ProdName",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.ProductList = product.Data;

            var NextAcList = Common.GetDataViaQuery<ProjectReportsModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ,NxtActnName",
                Criteria = "FK_ActionModule=1 AND Cancelled=0 AND Passed=1 AND FK_ActionModule=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },

            companyKey: _userLoginInfo.CompanyKey);
            statusobj.NextActionList = NextAcList.Data;

            var Category = Common.GetDataViaQuery<ProjectReportsModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey);
            statusobj.CategoryList = Category.Data;

            var actiontypelist = Common.GetDataViaQuery<ProjectReportsModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);
            statusobj.Actntyplists = actiontypelist.Data;
            var compname = Common.GetDataViaQuery<ProjectReportsModel.Customerlist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;

            ProjectReportsModel objpaymode = new ProjectReportsModel();
            //var statusmodeList = objpaymode.GeLeadStatusList(input: new ProjectReportsModel.ModeLead { Mode = 6 }, companyKey: _userLoginInfo.CompanyKey);

            //statusobj.StatusList = statusmodeList.Data;

            var statusmodeList = objpaymode.GeLeadStatusList(input: new ProjectReportsModel.ModeLead { Mode = 11 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.StatusList = statusmodeList.Data;
            var othercharge = Common.GetDataViaQuery<ProjectReportsModel.Othercharge>(parameters: new APIParameters
            {
                TableName = "OtherChargeType ",
                SelectFields = "ID_OtherChargeType AS OtherchargeID,OctyName AS OtherchargeName ",
                Criteria = "cancelled=0 AND Passed =1 AND OctyOther=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
    companyKey: _userLoginInfo.CompanyKey);
            statusobj.OtherchargeList = othercharge.Data;

            var   statusPettyCashierList = objpaymode.GePettyCashierList(input: new ProjectReportsModel.PettyCashierInput { Mode = 1, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.PettyCashierList = statusPettyCashierList.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.FK_User = _userLoginInfo.ID_Users;

            return PartialView("_AddProjectReports", statusobj);
        }

        public ActionResult getPettyCashierList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
             ProjectReportsModel objpaymode = new ProjectReportsModel();
            var statusPettyCashierList = objpaymode.GePettyCashierList(input: new ProjectReportsModel.PettyCashierInput { Mode = mode , FK_Company= _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
          
            return Json(statusPettyCashierList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getModeList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectReportsModel.Customerlist statusobj = new ProjectReportsModel.Customerlist();
            ProjectReportsModel objpaymode = new ProjectReportsModel();
            var statusmodeList = objpaymode.GeLeadStatusList(input: new ProjectReportsModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.StatusList = statusmodeList.Data;
            return Json(statusmodeList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetCustomerList()
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

            ProjectReportsModel.Customerlist LeadFromListObj = new ProjectReportsModel.Customerlist();

            var data = Common.GetDataViaQuery<ProjectReportsModel.Customerlist>(parameters: new APIParameters
            {
                TableName = "Employee ",
                SelectFields = "ID_Employee AS EmployeeID,EmpFName AS Employeename ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            var branch = Common.GetDataViaQuery<ProjectReportsModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            LeadFromListObj.BranchList = branch.Data;


            var empleadfrm = Common.GetDataViaQuery<ProjectReportsModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

  );
            LeadFromListObj.EmployeeLeadInfoList = empleadfrm.Data;

            return Json(data, JsonRequestBehavior.AllowGet);



        }
    }
}