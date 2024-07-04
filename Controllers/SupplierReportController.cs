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
    public class SupplierReportController : Controller
    {
        // GET: SupplierReport
        public ActionResult Index(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadSupplierreportLoadForm(string mtd, string mgrp)
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

            int userrole = 0;
            if (_userLoginInfo.UsrrlAdmin == true)
            {
                userrole = 1;
            }
            else
            {
                userrole = 0;
            }

            SupplierReportModel.Supplierreportview statusobj = new SupplierReportModel.Supplierreportview();
            var branch = Common.GetDataViaQuery<SupplierReportModel.Branchs>(parameters: new APIParameters
            {
                //TableName = "Branch B LEFT JOIN  Users U ON U.FK_Branch = B.ID_Branch  AND U.ID_Users=" + _userLoginInfo.ID_Users,
                //SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                //Criteria = "B.cancelled=0 AND B.Passed =1 AND B.FK_Company=" + _userLoginInfo.FK_Company + "AND  B.ID_Branch = CASE WHEN " + userrole + "= 1 THEN B.ID_Branch ELSE U.FK_Branch END  ",
                //SortFields = "",
                //GroupByFileds = ""
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.BranchList = branch.Data;

            var SupplierTypeName = Common.GetDataViaQuery<SupplierReportModel.SupplierTypeNameList>(parameters: new APIParameters
            {
                TableName = "SupplierType ST",
                SelectFields = "ST.ID_SupplierType AS ID_SupplierType,ST.STName AS[STName]",
                Criteria = "ST.Cancelled=0 AND ST.Passed=1 AND ST.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );

            statusobj.SupplierType = SupplierTypeName.Data;

            ViewBag.UsrrlAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
        
          

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddSupplierReportView", statusobj);
        }
     
        public ActionResult GetSupplierReportgridViewList(SupplierReportModel.Suppliergridinput Data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SupplierReportModel objfld = new SupplierReportModel();

            var data = objfld.GetSupplierOutStandingReportView(input: new SupplierReportModel.Suppliergridinput
            {
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                AsonDate = Data.AsonDate,
                BranchID = Data.BranchID,
                SupplierTypeID = Data.SupplierTypeID,
                 IncludeAdvance = Data.IncludeAdvance,
                FK_Mode=Data.FK_Mode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode=Data.TransMode,
               
            },

            companyKey: _userLoginInfo.CompanyKey);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;

        }
        public ActionResult IndexSuppLedger(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadSupplierLedgerreportLoadForm(string mtd)
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

            int userrole = 0;
            if (_userLoginInfo.UsrrlAdmin == true)
            {
                userrole = 1;
            }
            else
            {
                userrole = 0;
            }

            SupplierReportModel.Supplierreportview statusobj = new SupplierReportModel.Supplierreportview();
            var branch = Common.GetDataViaQuery<SupplierReportModel.Branchs>(parameters: new APIParameters
            {
                //TableName = "Branch B LEFT JOIN  Users U ON U.FK_Branch = B.ID_Branch  AND U.ID_Users=" + _userLoginInfo.ID_Users,
                //SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                //Criteria = "B.cancelled=0 AND B.Passed =1 AND B.FK_Company=" + _userLoginInfo.FK_Company + "AND  B.ID_Branch = CASE WHEN " + userrole + "= 1 THEN B.ID_Branch ELSE" + _userLoginInfo.FK_Branch +" END  ",
                //SortFields = "",
                //GroupByFileds = ""
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.BranchList = branch.Data;

        

            ViewBag.UsrrlAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddSupplierledgerReportView", statusobj);
        }
       
        public ActionResult GetSupplierledgerreportdetailtopsection(SupplierReportModel.Supplierreportview Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SupplierReportModel objfld = new SupplierReportModel();

            var data = objfld.GetSupplierledgertopDetailsData(input: new SupplierReportModel.SupplierLedgerinput
            {
                FK_Supplier = Data.SuppID,
                FK_Branch = Data.BranchID,

                FK_Company = _userLoginInfo.FK_Company,
                TableClount = 1,
                //FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                //EntrBy = _userLoginInfo.EntrBy,
                //FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }
      
        public ActionResult GetSupplierledgerreportdetail(SupplierReportModel.Supplierreportview Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SupplierReportModel objfld = new SupplierReportModel();

            var data = objfld.GetSupplierledgerDetailsData(input: new SupplierReportModel.SupplierLedgerinput
            {
                FK_Supplier = Data.SuppID,
                FK_Branch = Data.BranchID,
                TableClount = 2,
                FK_Company = _userLoginInfo.FK_Company,
               
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }

    }
}