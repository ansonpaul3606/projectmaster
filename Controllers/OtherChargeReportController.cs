using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class OtherChargeReportController : Controller
    {
        // GET: OtherChargeReport
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

        #region[LoadOtherChargerep]
        public ActionResult LoadOtherChargeReport(string mtd, string TransMode)
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

            OtherChargeReportModel vehicleReport = new OtherChargeReportModel();
            //OtherChargeReportModel.OtherChargeReportView reportView = new OtherChargeReportModel.OtherChargeReportView();

            OtherChargeReportModel.OtherChargeListModel MUList = new OtherChargeReportModel.OtherChargeListModel();

            var ModuleList = Common.GetDataViaProcedure<OtherChargeReportModel.ModuleList, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 125 });


            MUList.ModuleList = ModuleList.Data;

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[OtherChargeType]",
                SelectFields = "[ID_OtherChargeType] AS OtherChargeTypeID,[OctyName] AS OtherChargeTypeName",
                Criteria = "Passed=1  and Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var OtherChargeType = Common.GetDataViaQuery<OtherChargeReportModel.OtherChargeType>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            MUList.OtherChargeTypeList = OtherChargeType.Data;

            APIParameters apiParamebranch = new APIParameters
            {

                TableName = "Branch B",
                SelectFields = "B.ID_Branch as BranchID,B.BrName AS Branch",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""


            };
            var branch = Common.GetDataViaQuery<OtherChargeReportModel.BranchList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParamebranch);

            MUList.BranchList = branch.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddOtherChargeReport", MUList);
        }
        #endregion
        #region[GetOtherChargeReportList]
        public ActionResult GetOtherChargeReportList(OtherChargeReportModel.OtherChargeReportInput OtherChargeModel)
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
            OtherChargeReportModel reportModel = new OtherChargeReportModel();

            var datares = reportModel.GetOtherChargeGeneralReport(new OtherChargeReportModel.OtherChargeReportInput
            {
                ToDate = OtherChargeModel.ToDate,
                FromDate = OtherChargeModel.FromDate,
                FK_Branch=OtherChargeModel.FK_Branch,
                OtherChargeType = OtherChargeModel.OtherChargeType,
                Module = OtherChargeModel.Module,
                ImportID = OtherChargeModel.ImportID,
                SupplierID=OtherChargeModel.SupplierID,
                ID_Customer=OtherChargeModel.ID_Customer,
                ProdRptCriteria = OtherChargeModel.ProdRptCriteria,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}