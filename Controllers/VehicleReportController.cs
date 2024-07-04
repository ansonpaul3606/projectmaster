using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class VehicleReportController : Controller
    {
        // GET: VehicleReport
        #region[Index]
        public ActionResult Index(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
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

            
            return View();
        }
        #endregion

        #region[LoadVehicleRepo]
        public ActionResult LoadVehicleRepo(string mtd, string TransMode)
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

            VehicleReportModel vehicleReport = new VehicleReportModel();
            VehicleReportModel.VehicleReportView reportView = new VehicleReportModel.VehicleReportView();

            var Category = Common.GetDataViaQuery<VehicleReportModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS FK_Category ,CatName AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode='V' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey

           );
            reportView.CategoryList = Category.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddVehicleReport", reportView );
        }
        #endregion

        #region[GetVehicleReportList]
        public ActionResult GetVehicleReportList(VehicleReportModel.VehicleReportInput vehicleModel)
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
            VehicleReportModel reportModel = new VehicleReportModel();

            var datares = reportModel.GetVehicleGeneralReport(new VehicleReportModel.VehicleReportInput
            {
                ToDate = vehicleModel.ToDate,
                FromDate = vehicleModel.FromDate,
                FK_Category = vehicleModel.FK_Category,
                FK_Mode = vehicleModel.FK_Mode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }       
        #endregion
    }
}