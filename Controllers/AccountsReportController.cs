using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class AccountsReportController : Controller
    {
        // GET: AccountsReport
        public ActionResult AccountsReport(string mtd)
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
        public ActionResult LoadAccountsReportForm(string mtd)
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


            AccountsReportModel Accntrpt = new AccountsReportModel();
            AccountsReportModel.Accountslist statusobj = new AccountsReportModel.Accountslist();
            var branch = Common.GetDataViaQuery<AccountsReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;
           
      
           var AccountType = Accntrpt.GetAccountTypeList(input: new AccountsReportModel.AccountTypeMode { Mode = 47 }, companyKey: _userLoginInfo.CompanyKey);
            
            statusobj.AccountType = AccountType.Data;
            //Test 
            var AccountGroup = Common.GetDataViaQuery<AccountsReportModel.AccountGroup>(parameters: new APIParameters
            {
                TableName = "AccountGroup AG",
                SelectFields = "AG.ID_AccountGroup ,AG.AGName ",
                Criteria = "AG.Cancelled=0 AND AG.Passed=1 AND AG.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey
             );

            statusobj.AccountGroup = AccountGroup.Data;

            var AccountSubGroup = Common.GetDataViaQuery<AccountsReportModel.AccountSubGroup>(parameters: new APIParameters
            {
                TableName = "AccountSubGroup ASG",
                SelectFields = "ASG.ID_AccountSubGroup ,ASG.ASGName ",
                Criteria = "ASG.Cancelled=0 AND ASG.Passed=1 AND ASG.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            statusobj.AccountSubGroup = AccountSubGroup.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            return PartialView("_AddAccountsReportForm", statusobj);
        }

       

       public ActionResult GetAccountreportdetail(AccountsReportModel.Accountslist Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountsReportModel objfld = new AccountsReportModel();

            var data = objfld.GetAccountDetailsData(input: new AccountsReportModel.Accountdetailsinput
            {
                ReportMode=Data.ReportMode,
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                FK_Branch = Data.BranchID,
                FK_AccountHead=Data.AccountHead,
                FK_AccountSubHead=Data.AccountSubHead,
                TableCount=1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetTransactionreportdetail(AccountsReportModel.Accountslist Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountsReportModel objfld = new AccountsReportModel();

            var data = objfld.GetTransactionDetailsData(input: new AccountsReportModel.Accountdetailsinput
            {
                ReportMode = Data.ReportMode,
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                FK_Branch = Data.BranchID,
                FK_AccountHead = Data.AccountHead,
                FK_AccountSubHead = Data.AccountSubHead,
                TableCount = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetRDreportdetail(AccountsReportModel.RAndDReoportInput Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountsReportModel objfld = new AccountsReportModel();

            var data = objfld.GetAccountDetailsData(input: new AccountsReportModel.Accountdetailsinput
            {
                ReportMode = Data.ReportMode,
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                FK_Branch = Data.FK_Branch,
                FK_AccountHead = Data.AccountHead,
                FK_AccountSubHead = Data.AccountSubHead,
                TableCount = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                AccountGroup= Data.AccountGroup,
                AccountSubGroup=Data.AccountSubGroup,
                FK_Machine = _userLoginInfo.FK_Machine
            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult FillAccountSubGroup(AccountHeadModel.AccountHeadViewData obj)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountHeadModel objMl = new AccountHeadModel();
            var AccountSubList = objMl.GetSubGroupList(input: new AccountHeadModel.AccountHeadViewData
            {
                FK_AccountGroup = obj.FK_AccountGroup,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                FK_AccountSubGroup = obj.FK_AccountSubGroup
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(AccountSubList.Data, JsonRequestBehavior.AllowGet);

        }

    }
}