using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class RentandReturnReportController : Controller
    {
        // GET: RentandReturnReport

        #region[Index]
        public ActionResult Index(string mtd, string mgrp)
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

        #region[LoadRentRtnRepo]
        public ActionResult LoadRentRtnRepo(string mtd, string TransMode)
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

            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string lblpro = "";
          

            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    lblpro = "Vehicle";

                    break;
                case "TO":
                    modeData = "'T'";
                    lblpro = "Tool";

                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            ViewBag.lblpro = lblpro;

            RentandReturnModel.RentReturnView rent = new RentandReturnModel.RentReturnView();

            RentandReturnModel repair = new RentandReturnModel();
            var TypeList = repair.GeLeadStatusList(input: new RentandReturnModel.ModeLead { Mode = 68 },

            companyKey: _userLoginInfo.CompanyKey);

            rent.ActionStatusList = TypeList.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddRentReturnReport", rent);
        }
        #endregion

        #region[GetRentandReturnList]
        public ActionResult GetRentandReturnList(RentandReturnModel.RentRtnReportInput rent)
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
            RentandReturnModel model = new RentandReturnModel();

            var datares = model.GetVehicleGeneralReport(new RentandReturnModel.RentRtnReportInput
            {
                ToDate = rent.ToDate,
                FromDate = rent.FromDate,
                ID_FIELD = rent.ID_FIELD,
                FK_Mode = rent.FK_Mode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = rent.TransMode

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { datares }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}