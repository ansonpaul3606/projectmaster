using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;
using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    public class EmployeeWiseTargetSettingsController : Controller
    {
        // GET: EmployeeWiseTargetSettings
        public ActionResult EmployeeWiseTargetSettings(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }
        [HttpGet]
        public ActionResult LoadEmployeeWiseTargetSettingsForm(string mtd)
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

            EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList list = new EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList();
            EmployeeWiseTargetSettingsModel EmpWise = new EmployeeWiseTargetSettingsModel();            

            var modeList = EmpWise.GetModeList(input: new EmployeeWiseTargetSettingsModel.ModeInput { Mode = 90, Group = 0 }, companyKey: _userLoginInfo.CompanyKey);
            list.ModeList = modeList.Data;

            var DepartmentList = Common.GetDataViaQuery<EmployeeWiseTargetSettingsModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            list.DepartmentList = DepartmentList.Data;

            var designationList = Common.GetDataViaQuery<EmployeeWiseTargetSettingsModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            list.DesignationList = designationList.Data;

            var periodTypeList = EmpWise.GetPeriodType(input: new EmployeeWiseTargetSettingsModel.ModeInput { Mode = 112 }, companyKey: _userLoginInfo.CompanyKey);
            list.PerioTypeList = periodTypeList.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddEmployeeWiseTargetSettingsForm", list);
        }

        public ActionResult GetModeDetails(EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList data)
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

            EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList list = new EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList();
            EmployeeWiseTargetSettingsModel EmpWise = new EmployeeWiseTargetSettingsModel();

            var modeList = EmpWise.GetModeList(input: new EmployeeWiseTargetSettingsModel.ModeInput { Mode = 90, Group = 1 }, companyKey: _userLoginInfo.CompanyKey);
            list.ModeList = modeList.Data;

            return Json(modeList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddEmployeeWiseTargetSettings(EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList data)
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

            EmployeeWiseTargetSettingsModel empWiseTarget = new EmployeeWiseTargetSettingsModel();           

            var datresponse = empWiseTarget.UpdateEmployeeEmployeeWiseTargetData(input: new EmployeeWiseTargetSettingsModel.UpdateEmployeeWiseTargetSettings
            {
                UserAction=1,
                ID_EmployeeWiseTargetSettings= data.ID_EmployeeWiseTargetSettings,
                EtsModule=data.Module,
                EffectDate=data.EffectDate,
                FK_Designation=data.FK_Designation,
                FK_Department=data.FK_Department,
                FK_Employee=data.FK_Employee,
                EtsAmount=data.TargetAmount,
                EtsCount=data.TargetCount,
                EtsPeriodType=data.PeriodType,
                EtsPeriod=data.Period,
                EtsMode=data.Mode,
                Deactivate=data.Deactivate,
                FK_Company=_userLoginInfo.FK_Company,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                EntrBy=_userLoginInfo.EntrBy,
                FK_Machine=_userLoginInfo.FK_Machine,
            }, companyKey: _userLoginInfo.CompanyKey);           

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEmployeeWiseTargetSettingsList(int pageSize, int pageIndex, string Name)
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

            EmployeeWiseTargetSettingsModel empWiseTarget = new EmployeeWiseTargetSettingsModel();
            var EmpInfo = empWiseTarget.GetEmpWiseTargetSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeWiseTargetSettingsModel.GetEmpTargetSettings
            {
                FK_EmployeeWiseTargetSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            return Json(new { EmpInfo.Process, EmpInfo.Data, pageSize, pageIndex, totalrecord = (EmpInfo.Data is null) ? 0 : EmpInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmpWiseTargetReasonList()
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

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetEmpWiseTargetSettingsInfo(EmployeeWiseTargetSettingsModel.EmployeeWiseTargetSettingsViewList data)
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
            EmployeeWiseTargetSettingsModel empWiseTarget = new EmployeeWiseTargetSettingsModel();
            

            var EmpInfo = empWiseTarget.GetEmpWiseTargetSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeWiseTargetSettingsModel.GetEmpTargetSettings
            {
                FK_EmployeeWiseTargetSettings = data.ID_EmployeeWiseTargetSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            return Json(EmpInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteEmpWiseTargetSettings(EmployeeWiseTargetSettingsModel.DeleteEmployeeWiseTargetSettings data)
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


            EmployeeWiseTargetSettingsModel empWiseTarget = new EmployeeWiseTargetSettingsModel();

            var datresponse = empWiseTarget.DeleteEmployeeWiseTargetSettingsData(input: new EmployeeWiseTargetSettingsModel.DeleteEmployeeWiseTargetSettings
            {
                FK_EmployeeWiseTargetSettings = data.FK_EmployeeWiseTargetSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}