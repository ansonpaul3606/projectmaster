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
    public class ProjectWiseDuelistReportController : Controller
    {
        // GET: ProjectWiseDuelistReport
        public ActionResult Index(string mtd)

        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.DepartmentID = _userLoginInfo.FK_Department;
            CommonMethod objCmnMethod = new CommonMethod();
           

            ViewBag.mtd = mtd;
            return View();

        }
        public ActionResult LoadProjectwiseduelistReport(string mtd)

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



            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddProjectWiseDuelistReport"); 
        }

        public ActionResult GetProjectwiseduelistreportdetail(ProjectWiseDuelistReportModel.Projectwiseduelistview Data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectWiseDuelistReportModel objfld = new ProjectWiseDuelistReportModel();

            var data = objfld.GetProjectWiseDuelistDetailsData(input: new ProjectWiseDuelistReportModel.Projectwiseduelistreportinput
            {
              
                AsonDate = Data.AsonDate,
                FK_Project=Data.ProjectID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }
        
    }
}