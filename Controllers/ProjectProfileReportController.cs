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
    public class ProjectProfileReportController : Controller
    {
        // GET: ProjectProfileReport
        [CheckSessionTimeOut]
        public ActionResult ProjectProfile(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.mtd = mtd;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;
            return View();
        }
        public ActionResult LoadProjectProfileReportForm(string mtd)
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


            ProjectProfileReportModel Accntrpt = new ProjectProfileReportModel();
            ProjectProfileReportModel.ProfileList statusobj = new ProjectProfileReportModel.ProfileList();
            var branch = Common.GetDataViaQuery<ProjectProfileReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;



           

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return PartialView("_AddProjectProfileReportForm", statusobj);
        }

        public ActionResult Getprojectreportdetail(ProjectProfileReportModel.ProfileList Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectProfileReportModel objfld = new ProjectProfileReportModel();

            var data = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput
            {
                
                FK_Project = Data.FK_Project,
                TableCount = Data.TableCount,
               
            },

            companyKey: _userLoginInfo.CompanyKey);

            var ProjectDetails = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount =1  }, companyKey: _userLoginInfo.CompanyKey);
            var TeamDetails = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 2 }, companyKey: _userLoginInfo.CompanyKey);
            var StageDetails = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 3 }, companyKey: _userLoginInfo.CompanyKey);
            var MaterAllocation = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 4 }, companyKey: _userLoginInfo.CompanyKey);
            var MaterUsage = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 5 }, companyKey: _userLoginInfo.CompanyKey);


            var MaterWastage = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 6 }, companyKey: _userLoginInfo.CompanyKey);
            var MaterDamage = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 7 }, companyKey: _userLoginInfo.CompanyKey);
            var MaterExtraWork = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 8 }, companyKey: _userLoginInfo.CompanyKey);
            var MaterProjectT = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 9 }, companyKey: _userLoginInfo.CompanyKey);
            var ResorceDet = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 10 }, companyKey: _userLoginInfo.CompanyKey);
            var projectBilling = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 11 }, companyKey: _userLoginInfo.CompanyKey);
            var Details = objfld.GetProjectDetailsData(input: new ProjectProfileReportModel.ProjectDetailsinput { FK_Project = Data.FK_Project, TableCount = 12 }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { ProjectDetails, TeamDetails, StageDetails , MaterAllocation ,
            MaterUsage,
                MaterWastage,
                MaterDamage,
                MaterExtraWork,
                MaterProjectT,
                ResorceDet,
                projectBilling,
                Details
            }, JsonRequestBehavior.AllowGet);

           // return Json(data, JsonRequestBehavior.AllowGet);


        }
    }
}