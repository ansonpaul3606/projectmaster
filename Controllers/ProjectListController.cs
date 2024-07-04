using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using System.IO;
using System.Text;
using System.Data;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ProjectListController : Controller
    {
        // GET: ProjectList
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
           
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadProjectListForm(string mgrp, string mtd)
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


            ProjectListModel.ProjectListData Assign = new ProjectListModel.ProjectListData();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CommonMethod objCmnMethod = new CommonMethod();
            var BranchList = Common.GetDataViaQuery<ProjectListModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = " ID_Branch,BrName BranchName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            var SiteVisitTransMode = Common.GetDataViaQuery<ProjectListModel.SiteVisitTransModes>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode ,MnuLstName as MenulistName",
                Criteria = "ControllerName='SiteVisit'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            ViewBag.SiteVisitTranMode = objCmnMethod.EncryptString(SiteVisitTransMode.Data[0].TransMode.ToString());
            ViewBag.SiteVisitMenu = objCmnMethod.EncryptString(SiteVisitTransMode.Data[0].MenulistName.ToString());
            var ProjectFlwUpTransMode = Common.GetDataViaQuery<ProjectListModel.ProjectFlwUpTransModes>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode,MnuLstName as MenulistName",
                Criteria = "ControllerName='ProjectFollowUp'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            ViewBag.ProjectFolupTranMode = objCmnMethod.EncryptString(ProjectFlwUpTransMode.Data[0].TransMode.ToString());
            ViewBag.ProjectFolupMenu = objCmnMethod.EncryptString(ProjectFlwUpTransMode.Data[0].MenulistName.ToString());

            var ProjectTransactionMode = Common.GetDataViaQuery<ProjectListModel.ProjectTransactionTransModes>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode,MnuLstName as MenulistName",
                Criteria = "ControllerName='ProjectTransaction'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            ViewBag.ProjectTransationMode= objCmnMethod.EncryptString(ProjectTransactionMode.Data[0].TransMode.ToString());
            ViewBag.ProjectTransactionMenu = objCmnMethod.EncryptString(ProjectTransactionMode.Data[0].MenulistName.ToString());

            var MaterialAllocationTransMode = Common.GetDataViaQuery<ProjectListModel.MaterialAllocationTransModes>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode,MnuLstName as MenulistName",
                Criteria = "ControllerName='MaterialAllocation'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            ViewBag.MatAllocationTranMode = objCmnMethod.EncryptString(MaterialAllocationTransMode.Data[0].TransMode.ToString());
            ViewBag.MatAllocationMenu = objCmnMethod.EncryptString(MaterialAllocationTransMode.Data[0].MenulistName.ToString());

            var MaterialUsageTransModes = Common.GetDataViaQuery<ProjectListModel.MaterialUsageTransModes>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode,MnuLstName as MenulistName",
                Criteria = "ControllerName='MaterialUsage'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            ViewBag.MatUsageTranMode = objCmnMethod.EncryptString(MaterialUsageTransModes.Data[0].TransMode.ToString());
            ViewBag.MatUsageMenu = objCmnMethod.EncryptString(MaterialUsageTransModes.Data[0].MenulistName.ToString());


            Assign.BranchList = BranchList.Data;
            ViewBag.TransMode = mgrp;
           
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddProjectList", Assign);
        }
      
        public ActionResult GetProjectList(ProjectListModel.ProjectListInput Data, int pageSize, int pageIndex)
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

            ProjectListModel Model = new ProjectListModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetProjectList(input: new ProjectListModel.ProjectListInput
            {
                FK_Branch = _userLoginInfo.FK_Branch,
               // EntrBy = _userLoginInfo.EntrBy,
               // FromDate = Data.FromDate == null ? "" : Data.FromDate,
                FK_Employee = _userLoginInfo.FK_Employee,
                Mode = Data.Mode,
                FK_Project= Data.FK_Project,
                Detailed=Data.Detailed,
                // ToDate = Data.ToDate == null ? "" : Data.ToDate,
                // Mobile = Data.Content,
                //  Content = Data.Content,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            //return Json(data, JsonRequestBehavior.AllowGet);

            return Json(new { data.Process, data.Data, pageIndex, pageSize, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
    }
}