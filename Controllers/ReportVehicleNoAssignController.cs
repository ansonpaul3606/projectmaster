using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    public class ReportVehicleNoAssignController : Controller
    {
        
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
            
            ViewBag.mtd = mtd;

            return View();
        }



        public ActionResult LoadVehicleNoassignreport(string mtd)
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
            ViewBag.BranchID = _userLoginInfo.FK_Branch;

            ReportVehicleNoAssignModel DeliveryReport = new ReportVehicleNoAssignModel();
            ReportVehicleNoAssignModel.VehicleNoassignreportview list = new ReportVehicleNoAssignModel.VehicleNoassignreportview();




            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddReportVehicleNoAssign", list);
        }





        #region[GetDeliveryModelList]
        public ActionResult GetVehicleNoassignModelList(ReportVehicleNoAssignModel.VehiclelistNoassignReportInput Data)
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
            ReportVehicleNoAssignModel reportModel = new ReportVehicleNoAssignModel();

            var datares = reportModel.GetVehiclelistNoassignReport(new ReportVehicleNoAssignModel.VehiclelistNoassignReportInput
            {
                //FK_Mode = Data.FK_Mode,

                AsonDate=Data.AsonDate,
                FK_Vehicle = Data.FK_Vehicle,
                FK_Branch =Data.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}