using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class BankReconciliationController : Controller
    {
        // GET: BankReconciliation
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadBankReconciliation()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            BankReconciliationModel objbr = new BankReconciliationModel();
            BankReconciliationModel.BankReconciliationNew objBrnew = new BankReconciliationModel.BankReconciliationNew();
            var branch = Common.GetDataViaQuery<BankReconciliationModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            objBrnew.Branch = branch.Data;      
            return PartialView("_LoadBankReconciliation", objBrnew);
        }
        public ActionResult GetAccountReconcilData(BankReconciliationModel.GetAccountReconcilData obj)
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
            #region :: Fill List ::


            #endregion :: Fill List ::           
            BankReconciliationModel data = new BankReconciliationModel();


            var Lits = data.GetAccountReconcilDataFill(input: new BankReconciliationModel.GetAccountReconcilData {
                FK_AccountHead=obj.FK_AccountHead,
                FK_Branch = obj.FK_Branch,
                FromDate = obj.FromDate,
                ToDate = obj.ToDate,
                Mode=obj.Mode,
                FK_Company = _userLoginInfo.FK_Company }, 
                companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Lits }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateAccountReconcilData(BankReconciliationModel.UpdateBankReconciliationData data)
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
            BankReconciliationModel objBank = new BankReconciliationModel();

            var datresponse = objBank.UpdateAccountReconcil(input: new BankReconciliationModel.UpdateBankReconciliation
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                ReconciliationDtls = data.ReconciliationDtls is null ? "" : Common.xmlTostring(data.ReconciliationDtls),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}