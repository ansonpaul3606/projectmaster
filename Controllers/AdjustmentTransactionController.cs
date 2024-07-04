using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class AdjustmentTransactionController : Controller
    {      
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
           
            ViewBag.mtd = mtd;
           
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        public ActionResult LoadAdjustmentTransaction(string mtd)
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
            AdjustmentTransactionModel.AdjustmentTransactionView Acctview = new AdjustmentTransactionModel.AdjustmentTransactionView();
            AdjustmentTransactionModel objact = new AdjustmentTransactionModel();

            var Accttypelst = objact.GetAccountType(input: new AdjustmentTransactionModel.ModeLead { Mode = 72 }, companyKey: _userLoginInfo.CompanyKey);
            Acctview.ActTypes = Accttypelst.Data;

            var Transtypelist = Common.GetDataViaQuery<AdjustmentTransactionModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            Acctview.TransTypes = Transtypelist.Data;


            var branch = Common.GetDataViaQuery<AdjustmentTransactionModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            Acctview.Branch = branch.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAdjustmentTransaction", Acctview);
        }

        [HttpPost]
        public ActionResult FillActhead(AdjustmentTransactionModel.Accttyp obj)
        {
            AdjustmentTransactionModel.AdjustmentTransactionView Acctview = new AdjustmentTransactionModel.AdjustmentTransactionView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<AdjustmentTransactionModel.AccountHead>(parameters: new APIParameters
            {

                TableName = "AccountHead",
                SelectFields = " ID_AccountHead ID_Account,AHName AccountHd",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND AHAccountMode=" + obj.Acctypval,
                GroupByFileds = "",
                SortFields = " ID_AccountHead"
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            Acctview.AccountHeads = data.Data;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FillActSubhead(AdjustmentTransactionModel.AcctHeads obj)
        {
            AdjustmentTransactionModel.AdjustmentTransactionView Acctview = new AdjustmentTransactionModel.AdjustmentTransactionView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<AdjustmentTransactionModel.AccountSubHead>(parameters: new APIParameters
            {

                TableName = "AccountSubHead",
                SelectFields = " ID_AccountSubHead ID_AccountHd,ASHName AccountsHd",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_AccountHead=" + obj.AcctHead,
                GroupByFileds = "",
                SortFields = " ID_AccountSubHead"
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            Acctview.AccountSubHeads = data.Data;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAccountSubHead(AdjustmentTransactionModel.AcctSubHeads obj)
        {
            AdjustmentTransactionModel.AdjustmentTransactionView Acctview = new AdjustmentTransactionModel.AdjustmentTransactionView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<AdjustmentTransactionModel.AccountSubHead>(parameters: new APIParameters
            {

                TableName = "AccountSubHead",
                SelectFields = " ID_AccountSubHead ID_AccountHd,ASHName AccountsHd",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_AccountSubHead=" + obj.AcctSubHead,
                GroupByFileds = "",
                SortFields = " ID_AccountSubHead"
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            Acctview.AccountSubHeads = data.Data;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewAdjustmentTransaction(AdjustmentTransactionModel.AccountAdjustmentView data)
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
            AdjustmentTransactionModel objTrans = new AdjustmentTransactionModel();           

            var datresponse = objTrans.UpdateTransactionData(input: new AdjustmentTransactionModel.UpdateAccountAdjustment
            {
                UserAction = 1,
                Debug = 0,
                TransMode = "",
                ID_AdjustingHeadTransaction = 0,
                ProcessDate = data.ProcessDate,
                TransType = data.TransType,              
                FK_Branch = data.FK_Branch,
                AccountType = data.AccountType,
                FK_AccountHead = data.FK_AccountHead,
                AdjAmount = data.AdjAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdatAdjustmentTransaction(AdjustmentTransactionModel.AccountAdjustmentView data)
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
            AdjustmentTransactionModel objTrans = new AdjustmentTransactionModel();

            var datresponse = objTrans.UpdateTransactionData(input: new AdjustmentTransactionModel.UpdateAccountAdjustment
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_AdjustingHeadTransaction =data.ID_AdjustingHeadTransaction,
                ProcessDate = data.ProcessDate,
                TransType = data.TransType,
                FK_Branch = data.FK_Branch,
                AccountType = data.AccountType,
                FK_AccountHead = data.FK_AccountHead,
                AdjAmount = data.AdjAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAccountHeadSubTransactionList(int pageSize, int pageIndex, string Name, string TransMode)
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

            ModelState.Remove("ReasonID");

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            AdjustmentTransactionModel AccountHeadSubTransaction = new AdjustmentTransactionModel();

            var AccoInfo = AccountHeadSubTransaction.GetAdjustmentTransactionData(companyKey: _userLoginInfo.CompanyKey, input: new AdjustmentTransactionModel.GetAdjustmentTransaction
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,              
                TransMode = TransMode
            });


            return Json(new { AccoInfo.Process, AccoInfo.Data, pageSize, pageIndex, totalrecord = (AccoInfo.Data is null) ? 0 : AccoInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAdjustmentTransactionInfo(AdjustmentTransactionModel.AccountHeadSubTransactionID data)
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
            AdjustmentTransactionModel objAcTrans = new AdjustmentTransactionModel();
            var AccountTransInfo = objAcTrans.GetAdjustmentTransactionData(companyKey: _userLoginInfo.CompanyKey, input: new AdjustmentTransactionModel.GetAdjustmentTransaction
            {

                FK_AdjustingHeadTransaction=data.ID_AccountHeadSubTransaction,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,                       
                TransMode = "",

            });

            return Json(new { AccountTransInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteAdjustmentTransaction(AdjustmentTransactionModel.DeleteAccountHeadSubTransaction data)
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
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            AdjustmentTransactionModel DelAccTrans = new AdjustmentTransactionModel();

            var datresponse = DelAccTrans.DeleteAccountHeadSubTransactionData(input: new AdjustmentTransactionModel.DeleteAccountHeadSubTransaction
            {
                FK_AdjustingHeadTransaction = data.FK_AdjustingHeadTransaction,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Debug = 0
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAdjustmentTransactionReasonList()
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

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
    }
}