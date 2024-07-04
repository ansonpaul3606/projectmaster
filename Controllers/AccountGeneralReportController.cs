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
    public class AccountGeneralReportController : Controller
    {
        // GET: AccountGeneralReport
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            //CommonMethod objCmnMethod = new CommonMethod();
            //ViewBag.PageTitle = objCmnMethod.Decrypt(mtd);
            return View();
        }
       
        public ActionResult LoadAccountGeneralReportForm(string mtd)
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
            if(_userLoginInfo.UsrrlAdmin==true)
            {
                userrole = 1;
            }
            else
            {
                userrole = 0;
            }

            AccountGeneralReportModel.AccountGeneralReportView statusobj = new AccountGeneralReportModel.AccountGeneralReportView();
            var branch = Common.GetDataViaQuery<AccountGeneralReportModel.Branchs>(parameters: new APIParameters
            { 
                TableName = "Branch B LEFT JOIN  Users U ON U.FK_Branch = B.ID_Branch  AND U.ID_Users=" + _userLoginInfo.ID_Users  , 
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "B.cancelled=0 AND B.Passed =1 AND B.FK_Company=" + _userLoginInfo.FK_Company + "AND  B.ID_Branch = CASE WHEN "+ userrole + "= 1 THEN B.ID_Branch ELSE U.FK_Branch END  ", 
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.BranchList = branch.Data;

            var Transtypelist = Common.GetDataViaQuery<AccountGeneralReportModel.TransactionType>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            statusobj.TransactionTypeList = Transtypelist.Data;
       
            ViewBag.UsrrlAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
          //  ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAccountGeneralReport", statusobj);
        }


        public ActionResult GetAccountgeneralReportgridViewList(AccountGeneralReportModel.AccountGeneralReportView Data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountGeneralReportModel objfld = new AccountGeneralReportModel();

            var data = objfld.GetAccountGeneralReportView(input: new AccountGeneralReportModel.AccountGeneralViewInput
            {
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                BranchID=Data.BranchID,
                TransTypeID= (Data.TransTypeID.HasValue) ? Data.TransTypeID.Value : 0,
                TransModeID =Data.TransModeID,
                SortByID=Data.SortByID,
                GroupByID = Data.GroupByID,
                FK_AccountHead = (Data.AccountHeadID.HasValue) ? Data.AccountHeadID.Value : 0,
                FK_AccountHeadSub = (Data.AccountHeadSubID.HasValue) ? Data.AccountHeadSubID.Value : 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ReferenceNumber = Data.ReferenceNumber
            },

            companyKey: _userLoginInfo.CompanyKey);
            JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = int.MaxValue;
            return json;
           
        }

        public ActionResult Gettrialbalanceamount(AccountGeneralReportModel.AccountGeneralReportView Data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountGeneralReportModel objfld = new AccountGeneralReportModel();

            var data = objfld.GetBalanceDetailsData(input: new AccountGeneralReportModel.AccountbalanceInput
            {
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                BranchID = Data.BranchID,
               
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