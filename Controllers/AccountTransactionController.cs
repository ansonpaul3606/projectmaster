using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;
using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using PerfectWebERP.Filters;
using System.IO;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class AccountTransactionController : Controller
    {
        // GET: AccountTransaction
        public ActionResult AccountTransaction(string mtd,string mgrp)
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
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;// objCmnMethod.Decrypt(mtd);
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;
            ViewBag.Fk_Branch = _userLoginInfo.FK_Branch;
            return View();
        }

        public ActionResult LoadFormAccountTransaction(string mtd)
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
            AccountTransactionModel.AccountTransactionView Acctview = new AccountTransactionModel.AccountTransactionView();
            AccountTransactionModel objact = new AccountTransactionModel();
        
            var Accttypelst = objact.GetAccountType(input: new AccountTransactionModel.ModeLead { Mode = 49 }, companyKey: _userLoginInfo.CompanyKey);

            Acctview.ActTypes = Accttypelst.Data;
            var Transtypelist = Common.GetDataViaQuery<AccountTransactionModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            Acctview.TransTypes = Transtypelist.Data;

            //var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            //{
            //    TableName = "PaymentMethod",
            //    SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
            //    Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);
            //Acctview.PaymentView = PaymentView.Data;

            var PaymentView = objact.GetpaymentMethodDetails(new AccountTransactionModel.InputBranch
            {
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);
            Acctview.PaymentView = PaymentView.Data;


            var branchdata = objact.GetBranchHead(input: new AccountTransactionModel.InputBranch
            {
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            Acctview.branchHeads = branchdata.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAccountTransaction", Acctview);
        }

        [HttpPost]
        public ActionResult FillActhead(AccountTransactionModel.Accttyp obj)
        {
            AccountTransactionModel.AccountTransactionView Acctview = new AccountTransactionModel.AccountTransactionView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<AccountTransactionModel.AccountHead>(parameters: new APIParameters
            {
               
                TableName = "AccountHead",
                SelectFields = " ID_AccountHead ID_Account,AHName AccountHd",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company+ "AND AHAccountMode="+obj.Acctypval,
                GroupByFileds = "",
                SortFields = " ID_AccountHead"
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            Acctview.AccountHeads = data.Data;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FillActSubhead(AccountTransactionModel.AcctHeads obj)
        {
            AccountTransactionModel.AccountTransactionView Acctview = new AccountTransactionModel.AccountTransactionView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<AccountTransactionModel.AccountSubHead>(parameters: new APIParameters
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
        public ActionResult GetAccountSubHead(AccountTransactionModel.AcctSubHeads obj)
        {
            AccountTransactionModel.AccountTransactionView Acctview = new AccountTransactionModel.AccountTransactionView();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<AccountTransactionModel.AccountSubHead>(parameters: new APIParameters
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
        public ActionResult AddNewAccountTransaction(AccountTransactionModel.AccountHeadSubTransactionView data)
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
            AccountTransactionModel objTrans = new AccountTransactionModel();
        //    List<AccountTransactionModel.AccountTransaction> lst = new List<AccountTransactionModel.AccountTransaction>();
        //    if (data.AccountTransactionItems.Count > 0)
        //        {
        //           int  Count = (int)data.ID_AccountHeadSubTransaction;

        //        foreach (var temp in data.AccountTransactionItems)
        //        {
        //            AccountTransactionModel.AccountTransaction obj = temp;
        //            obj.ID_AccountHeadSubTransaction = Count;
        //            lst.Add(obj);
        //            Count++;
        //        }
        //}


                var datresponse = objTrans.UpdateAccountHeadSubTransactionData(input: new AccountTransactionModel.UpdateAccountHeadSubTransaction
            {                
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                ID_AccountHeadSubTransaction = 0,           
                Transfer = data.Transfer,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                AhstGroupID = 0,               
                AccountTransactionItems = data.AccountTransactionItems is null ? "" : Common.xmlTostring(data.AccountTransactionItems),
                FK_AccountHeadSubTransaction = 0,
                TransDate = data.TransDate,
                EffectDate = data.EffectDate,
                LastID = data.LastID,
                TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails)
            }, companyKey: _userLoginInfo.CompanyKey);

            if (datresponse.Data[0].ErrCode == "-200")
            {
                string errMsg = datresponse.Data[0].ErrMsg;
                var errorResponse = new
                {
                    Process = new { IsProcess = false, Message = new List<string> { errMsg } }
                };
                return Json(errorResponse, JsonRequestBehavior.AllowGet);
            }

            var successResponse = new
            {
                Process = new { IsProcess = true, Message = new List<string>() }
            };
            
            return Json(successResponse, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdatAccountTransaction(AccountTransactionModel.AccountHeadSubTransactionView data)
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
            AccountTransactionModel objTrans = new AccountTransactionModel();

            List<AccountTransactionModel.AccountTransaction> lst = new List<AccountTransactionModel.AccountTransaction>();
            //if (data.AccountTransactionItems.Count > 0)
            //{
            //   // int Count = 1;

            //    foreach (var temp in data.AccountTransactionItems)
            //    {
            //        AccountTransactionModel.AccountTransaction obj = temp;
            //        //  obj.ID_AccountHeadSubTransaction = Count;
            //        obj.ID_AccountHeadSubTransaction = data.ID_AccountHeadSubTransaction;
            //        obj.ID_AccountHeadSubTransaction++;
            //        lst.Add(obj);
            //       // Count++;
            //    }
            //}

            //if (data.AccountTransactionItems.Count > 0)
            //{
            //    int Count = (int)data.ID_AccountHeadSubTransaction;

            //    foreach (var temp in data.AccountTransactionItems)
            //    {
            //        AccountTransactionModel.AccountTransaction obj = temp;
            //        obj.ID_AccountHeadSubTransaction = Count;
            //        lst.Add(obj);
            //        Count++;
            //    }
            //}

            var datresponse = objTrans.UpdateAccountHeadSubTransactionData(input: new AccountTransactionModel.UpdateAccountHeadSubTransaction
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_AccountHeadSubTransaction = data.ID_AccountHeadSubTransaction,
                Transfer = data.Transfer,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                AhstGroupID = data.AhstGroupID,
                 LastID = data.LastID,
                AccountTransactionItems = data.AccountTransactionItems is null ? "" : Common.xmlTostring(data.AccountTransactionItems),
                FK_AccountHeadSubTransaction = data.ID_AccountHeadSubTransaction,
                TransDate = data.TransDate,
                EffectDate = data.EffectDate,
                TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails)
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
            AccountTransactionModel AccountHeadSubTransaction = new AccountTransactionModel();

            var AccoInfo = AccountHeadSubTransaction.GetAccountHeadSubTransactionData(companyKey: _userLoginInfo.CompanyKey, input: new AccountTransactionModel.GetAccountHeadSubTransaction
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                GroupID = 0,
                Detailed=0,
                TransMode=TransMode
            });


            return Json(new { AccoInfo.Process, AccoInfo.Data, pageSize, pageIndex, totalrecord = (AccoInfo.Data is null) ? 0 : AccoInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        
  [HttpPost]
        public ActionResult GetAccountTransactionInfo(AccountTransactionModel.AccountHeadSubTransactionID data)
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
            AccountTransactionModel objAcTrans = new AccountTransactionModel();
          

            var AccountTransactionItem = objAcTrans.GetAccountHeadSubTransactionItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new AccountTransactionModel.GetAccountHeadSubTransaction
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                Name = data.Name,
                GroupID = data.AhstGroupID,
                Detailed = 1,
                TransMode = data.TransMode
            });



            var datares = objAcTrans.GetAccounttransactionTax(companyKey: _userLoginInfo.CompanyKey, input: new AccountTransactionModel.GetAccountHeadSubTransaction
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = data.PageIndex,
                PageSize = data.PageSize,
                Name = data.Name,
                GroupID = data.AhstGroupID,
                Detailed = 2,
                TransMode = data.TransMode
            });



            return Json(new { AccountTransactionItem,datares }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteAccountTransaction(AccountTransactionModel.DeleteAccountHeadSubTransaction data)
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

            AccountTransactionModel DelAccTrans = new AccountTransactionModel();

            var datresponse = DelAccTrans.DeleteAccountHeadSubTransactionData(input: new AccountTransactionModel.DeleteAccountHeadSubTransaction
            {
                GroupID = data.GroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode=data.TransMode,
                Debug = 0
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAccountTransactionReasonList()
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

        #region[GetTaxCalculation]
        public ActionResult GetTaxCalculation(AccountTransactionModel.TaxGroup tax)
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
            AccountTransactionModel account = new AccountTransactionModel();

            var data = account.FillTaxgroup(new AccountTransactionModel.TaxGroup
            {
                FK_Product = 0,
                FK_TaxGroup = tax.FK_TaxGroup,
                Amount = tax.Amount,
                IncludeTax = tax.IncludeTax,
                Quantity = 0,
                Sales = 0

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new {data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}