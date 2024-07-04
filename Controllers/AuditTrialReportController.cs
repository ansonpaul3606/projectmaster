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
    public class AuditTrialReportController : Controller
    {
        // GET: AuditTrialReport
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadAuditTrialReportForm(string mtd, string TransMode)
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


            AuditTrialReportModel trialrpt = new AuditTrialReportModel();
            AuditTrialReportModel.auditlist statusobj = new AuditTrialReportModel.auditlist();
            var branch = Common.GetDataViaQuery<AuditTrialReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;
            var deplst = Common.GetDataViaQuery<AuditTrialReportModel.Department>(parameters: new APIParameters
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
            var MenuGroup = Common.GetDataViaQuery<AuditTrialReportModel.MenuGroup>(parameters: new APIParameters
            {
                TableName = "MenuGroup ",
                SelectFields = "ID_MenuGroup AS ID_MenuGroup,MnuGrpName AS MnuGrpName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            statusobj.MenuGroup = MenuGroup.Data;
            var MenuList = Common.GetDataViaQuery<AuditTrialReportModel.MenuList>(parameters: new APIParameters
            {
                TableName = "MenuList ML join MenuGroup MG on ML.FK_MenuGroup=MG.ID_MenuGroup",
                SelectFields = "ID_MenuList as ID_MenuList,MnuLstName as MnuLstName",
                //Criteria = "ML.Cancelled=0 AND ML.Passed=1 AND MG.Cancelled=0 AND MG.Passed=1 AND FK_Company=" + " AND ML.FK_Company=" + _userLoginInfo.FK_Company,
                Criteria = "ML.Cancelled=0  AND ML.Passed=1 AND ML.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MenuList",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey);
            statusobj.MenuList = MenuList.Data;
            var UserRole = Common.GetDataViaQuery<AuditTrialReportModel.UserRole>(parameters: new APIParameters
            {
                TableName = "UserRole",
                SelectFields = "ID_UserRole AS ID_UserRole,UsrrlName AS UsrrlName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);
            statusobj.UserRole = UserRole.Data;
            //   var User = Common.GetDataViaQuery<AuditTrialReportModel.Users>(parameters: new APIParameters
            //   {
            //       TableName = "Users",
            //       SelectFields = "ID_Users AS ID_Users,UserCode AS UserCode",
            //       Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
            //       SortFields = "",
            //       GroupByFileds = ""AddMaterial
            //   },
            //companyKey: _userLoginInfo.CompanyKey);
            //   statusobj.Users = User.Data;
            var User= Common.GetDataViaQuery<AuditTrialReportModel.Users>(parameters: new APIParameters
            {
                TableName = "Users U join Employee E on U.FK_Employee=E.ID_Employee",
                SelectFields = "ID_Users as ID_Users,UserCode as UserCode,UserCode+'-'+EmpFName+EmpLName as EmpFName",
                //Criteria = "ML.Cancelled=0 AND ML.Passed=1 AND MG.Cancelled=0 AND MG.Passed=1 AND FK_Company=" + " AND ML.FK_Company=" + _userLoginInfo.FK_Company,
                Criteria = "U.Cancelled=0  AND U.Passed=1 AND U.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);
            statusobj.Users = User.Data;


            var Actionlists = trialrpt.GetActionListData(input: new AuditTrialReportModel.ModeValue { Mode = 123 },
          companyKey: _userLoginInfo.CompanyKey);
            statusobj.ActiotypeList = Actionlists.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AuditTrialReport", statusobj);
        }

  

        public ActionResult GetUserList(Int64 ID_UserRole)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<AuditTrialReportModel.Users>(parameters: new APIParameters
            {
                TableName = "Users U join Employee E on U.FK_Employee=E.ID_Employee join UserRole UR on UR.ID_UserRole=U.FK_UserRole ",
                SelectFields = "ID_Users as ID_Users ,UserCode as UserCode,UserCode+'-'+EmpFName+EmpLName as EmpFName",
                Criteria = "U.Cancelled=0  AND U.Passed=1 AND U.FK_Company='" + _userLoginInfo.FK_Company + "'" + " AND UR.ID_UserRole='" + ID_UserRole + "'",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSubModule(Int64 ID_MenuGroup)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<AuditTrialReportModel.SubModule>(parameters: new APIParameters
            {
                TableName = "MenuList ML join MenuGroup MG on ML.FK_MenuGroup=MG.ID_MenuGroup",
                SelectFields = "ID_MenuList as ID_MenuList,MnuLstName as MnuLstName",
                Criteria = "ML.Cancelled=0  AND ML.Passed=1 AND ML.FK_Company='" + _userLoginInfo.FK_Company + "'" + " AND MG.ID_MenuGroup='" + ID_MenuGroup + "'",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
    }
}