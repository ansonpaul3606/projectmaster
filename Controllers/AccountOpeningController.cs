
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class AccountOpeningController : Controller
    {
        // GET: AccountOpening
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
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.TransMode = mGrp;
            return View();
        }
        #endregion

        #region[LoadAccountOpening]
        public ActionResult LoadAccountOpening(string mtd, string TransMode)
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
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            AccountOpeningModel model = new AccountOpeningModel();
            AccountOpeningModel.AccountOPeningView view = new AccountOpeningModel.AccountOPeningView();

            var FinalAccountGroup = model.GetAccountGroupList(input: new AccountOpeningModel.FinalAccountTypeMode { Mode = 41 }, companyKey: _userLoginInfo.CompanyKey);
            view.FinalAccountGroup = FinalAccountGroup.Data;

            var branchdata = Common.GetDataViaQuery<AccountOpeningModel.BranchHead>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS FK_Branch ,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            view.branchHeads = branchdata.Data;


            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            return PartialView("_AddAccountOpening", view);
        }
        #endregion

        #region[GetDataAccountGroup]
        public ActionResult GetDataAccountGroup(AccountOpeningModel.inputAccGrp input)
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

            AccountOpeningModel model = new AccountOpeningModel();

            var result = model.GetAccountGroupDetails(new AccountOpeningModel.inputAccGrp
            {
                AccGroupType = input.AccGroupType,
                FK_Company =_userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new {result, data = result.Data, result.Process}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetOpeningAccountData]
        public ActionResult GetOpeningAccountData(AccountOpeningModel.AccountOpeningInput input)
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

            AccountOpeningModel model = new AccountOpeningModel();

            var result = model.GetOpeningAccountDetails(new AccountOpeningModel.AccountOpeningInput
            {
                FK_AccGroupType = input.FK_AccGroupType,
                FK_Branch = input.FK_Branch,
                AccGroupType = input.AccGroupType,
                FinYear = input.FinYear,
                FK_Company = _userLoginInfo.FK_Company,
               

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { result, data = result.Data, result.Process }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[AddAccountOpeningdata]
        public ActionResult AddAccountOpeningdata(AccountOpeningModel.UpdateDtainput input)
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

            AccountOpeningModel account = new AccountOpeningModel();

            var result = account.UpdateAccountOpeningdetails(new AccountOpeningModel.UpdateAccountOpening
            {
                UserAction =1 ,
                Debug = 0,
                ID_AccountHeadBalance = 0,
                TransMode = input.TransMode,
                EntrBy = _userLoginInfo.EntrBy,
                AccountOpeningDetails = input.AccountOpeningDetails is null ? "" : Common.xmlTostring(input.AccountOpeningDetails),
                LastID = (input.LastID.HasValue) ? input.LastID.Value : 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company= _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FinYear = input.FinYear

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}