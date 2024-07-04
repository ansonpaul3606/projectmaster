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
    public class ProductionWorkFlowRptController : Controller
    {
        // GET: ProductionWorkFlowRpt
        public ActionResult ProductionWorkFlowRpt(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;

            return View();
        }

        public ActionResult LoadProductionWorkFlowRptForm(string mtd)
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

            SalesReportModel salesrpt = new SalesReportModel();
            SalesReportModel.salelist statusobj = new SalesReportModel.salelist();


            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_LoadProductionWorkFlowRpt", statusobj);
        }

        public ActionResult GetProductionWorkFlowdetailReport(ProductionWorkFlowRptModel.ProductionWorkFlowRptlist Data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProductionWorkFlowRptModel objfld = new ProductionWorkFlowRptModel();

            var InputMaterials = objfld.GetMaterialsDetailsData(input: new ProductionWorkFlowRptModel.ProductionWorkFlowRptInput
            {
                Mode = 1,
                FK_Product = Data.FK_Product,
                FK_Stage = Data.FK_Stage,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);

            var OutputProducts = objfld.GetProductDetailsData(input: new ProductionWorkFlowRptModel.ProductionWorkFlowRptInput
            {
                Mode = 2,
                FK_Product = Data.FK_Product,
                FK_Stage = Data.FK_Stage,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);

            var ResourceDetails = objfld.GetResourceDetailsData(input: new ProductionWorkFlowRptModel.ProductionWorkFlowRptInput
            {
                Mode = 3,
                FK_Product = Data.FK_Product,
                FK_Stage = Data.FK_Stage,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);

            var StageDetails = objfld.GetStageDetailsData(input: new ProductionWorkFlowRptModel.ProductionWorkFlowRptInput
            {
                Mode = 4,
                FK_Product = Data.FK_Product,
                FK_Stage = Data.FK_Stage,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);

            var AmountData = objfld.GetCostDetailsData(input: new ProductionWorkFlowRptModel.ProductionWorkFlowRptInput
            {
                Mode = 5,
                FK_Product = Data.FK_Product,
                FK_Stage = Data.FK_Stage,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);

            //return Json(AmountData, JsonRequestBehavior.AllowGet);
            return Json(new { InputMaterials, OutputProducts, ResourceDetails, StageDetails, AmountData }, JsonRequestBehavior.AllowGet);

        }
    }
}