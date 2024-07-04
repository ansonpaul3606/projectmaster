using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    public class MaterialRequestReportsController : Controller
    {
        // GET: MaterialRequestReports
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.DecryptString(mgrp);
            //ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            return View();
        }

        #region[LoadVehicleRepo]
        public ActionResult LoadMaterialRequestRepo(string mtd)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            MaterialRequestReportsModel vehicleReport = new MaterialRequestReportsModel();
            MaterialRequestReportsModel.MaterialRequestReportView Materiallist = new MaterialRequestReportsModel.MaterialRequestReportView();
            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND TransMode='PRST' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<MaterialRequestReportsModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            Materiallist.StageList = Stages.Data;


            //ViewBag.TransMode = TransMode;



            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("MaterialRequestReports", Materiallist);
        }
        #endregion
        public ActionResult GetProjectTeam(MaterialRequestReportsModel.MaterialRequestReportView cr)
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
            #region :: Model validation  ::

            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters

            {
                TableName = "[ProjectTeam]",
                SelectFields = "[ID_ProjectTeam] AS TeamID,[ProjTeamName] AS TeamName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Project ='" + cr.FK_Project + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialRequestReportsModel.TeamList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProjectStages(MaterialRequestReportsModel.MaterialRequestReportView cr)
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
            #region :: Model validation  ::


            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
                SelectFields = "distinct [ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 And FK_Project ='" + cr.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialRequestReportsModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetEmployees(MaterialRequestReportsModel.MaterialRequestReportView cr)
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


            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectAttendanceDetails PAD join ProjectTeam on ProjectTeam.TeamID = PAD.FK_Master join ProjectWiseStages on ProjectWiseStages.FK_ProjectTeam = PAD.FK_Master  join Employee on PAD.FK_Employee = Employee.ID_Employee",
                SelectFields = "distinct ID_Employee as EmployeeID,EmpFName as EmployeeName",
                Criteria = "PAD.Passed=1 And PAD.Cancelled=0 And PAD.TransMode='PRTC' AND ProjectWiseStages.FK_Project ='" + cr.FK_Project + "'" + "And FK_ProjectStages = '" + cr.FK_Stages + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialRequestReportsModel.EmployeeList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }

        #region[GetMaterialModelList]
        public ActionResult GetMaterialModelList(MaterialRequestReportsModel.MaterialRequestReportInput MaterialModel)
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
            MaterialRequestReportsModel reportModel = new MaterialRequestReportsModel();

            var datares = reportModel.GetMaterialGeneralReport(new MaterialRequestReportsModel.MaterialRequestReportInput
            {
                ToDate = MaterialModel.ToDate,
                FromDate = MaterialModel.FromDate,
                FK_Project = MaterialModel.FK_Project,
                FK_Mode = MaterialModel.FK_Mode,
                FK_Stages = MaterialModel.FK_Stages,
                FK_Team = MaterialModel.FK_Team,
                FK_Employee = MaterialModel.FK_Employee,
                ProductID=MaterialModel.ProductID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetProjectwiseMaterialDetails
        public ActionResult GetProjectwiseMaterialDetails(MaterialRequestReportsModel.ProjectWiseMaterialDetailsInput MaterialModel)
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
            MaterialRequestReportsModel reportModel = new MaterialRequestReportsModel();

            var datares = reportModel.GetProjectWiseMaterialDetailsReport(new MaterialRequestReportsModel.ProjectWiseMaterialDetailsInput
            {
                AsOnDate = MaterialModel.AsOnDate,
               
                FK_Project = MaterialModel.FK_Project,
                 FK_Stage = MaterialModel.FK_Stage,
              
                FK_Company = _userLoginInfo.FK_Company,
               

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

