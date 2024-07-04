using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AccountTransactionModel
    {
            
        public class AccountTransactionView
        {
            public long SlNo { get; set; }
            public long Voucher { get; set; }
            public long AcctType { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public long AcctHead { get; set; }
            public long AcctsubHead { get; set; }
            public decimal Amount { get; set; }
            public string ChequeNo { get; set; }
            public DateTime ?ChequeDate { get; set; }
            
            public string Party { get; set; }
            public string AhstNarration { get; set; }
            public List<ActType> ActTypes { get; set; }
            public List<AccountHead> AccountHeads { get; set; }
            public List<AccountSubHead> AccountSubHeads { get; set; }
            public List<TransTypes> TransTypes { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<BranchHead> branchHeads { get; set; }
            public bool Transfer { get; set; }
            public List<PaymentView> paymentViews { get; set; }
        }
        public class UpdateAccount
        {
            public int TransTypeID { get; set; }
            public string TransType { get; set; }

            public long FK_TransType { get; set; }
        }
        public class TransTypes
        {
            public long TransTypeID { get; set; }
            public string TransType { get; set; }
           
        }
        public class AccountTransaction
        {
            public Int64 UID { get; set; }
            public long Voucher { get; set; }
            public string AccountHead { get; set; }
            public string AccountSubHead { get; set; }  
            public long AccountType { get; set; }
            public string TransType { get; set; }
            public decimal AhstAmount { get; set; }       
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string AhstNarration { get; set; }
            public string AhstParty { get; set; }
            public string AhstChequeNo { get; set; }
            public DateTime ?AhstChequeDate { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public long AhstGroupID { get; set; }
            public decimal TaxAmount { get; set; }
            public Boolean IncludeTax { get; set; }
            public string AhstReferenceNo { get; set; }
            public int AhstPaymentMode { get; set; }
            public long AhstBranch { get; set; }
            public decimal NetAmount { get; set; }
        }

        public class AccountTransactionupdateview
        {
            public long Voucher { get; set; }
            public string AccountHead { get; set; }
            public string AccountSubHead { get; set; }
            public long AccountType { get; set; }
            public string TransType { get; set; }
            public string TransactionType { get; set; }
            public decimal AhstAmount { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string AhstNarration { get; set; }
            public string AhstParty { get; set; }
            public string AhstChequeNo { get; set; }
            public DateTime? AhstChequeDate { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public long AhstGroupID { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public bool Transfer { get; set; }
            public decimal TaxAmount { get; set; }
            public long TaxGroupID { get; set; }
            public long UID { get; set; }
            public string AhstReferenceNo { get; set; }
            public string AhstPaymentMode { get; set; }
            public int FK_AhstPaymentMode { get; set; }
            public int PmMode { get; set; }
            public string AhstBranch { get; set; }
        }

        public class AccountTransactionViewData
        {
            public long Voucher { get; set; }
            public string AccountHead { get; set; }
            public string AccountSubHead { get; set; }
            public long AccountType { get; set; }
            public string TransType { get; set; }
            public decimal AhstAmount { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string AhstNarration { get; set; }
            public string AhstParty { get; set; }
            public string AhstChequeNo { get; set; }
            public DateTime ?AhstChequeDate { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public long AhstGroupID { get; set; }

        }
        public class AccountTransactionData
        {
            public long SlNo { get; set; }
            public long Voucher { get; set; }
            public long AccountType { get; set; }
            public string AccountHead { get; set; }
            public string AccountSubHead { get; set; }
            public DateTime TransDate { get; set; }
            public string TransType { get; set; }
            public decimal AhstAmount { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public DateTime EffectDate { get; set; }
            public string AhstNarration { get; set; }
            public string AhstParty { get; set; }
            public string AhstChequeNo { get; set; }
            public DateTime ?AhstChequeDate { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 LastID { get; set; } = 0;
            public long AhstGroupID { get; set; }
            public decimal TaxAmount { get; set; }
           
        }
            public class ActType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class AccountHead
        {
            public Int32 ID_Account { get; set; }
            public string AccountHd { get; set; }
        }
        public class AccountSubHead
        {
            public int ID_AccountHd { get; set; }
            public string AccountsHd { get; set; }


        }
        public class Accttyp
        {
            public int Acctypval { get; set; }
        }
        public class AcctHeads
        {
            public int AcctHead { get; set; }
        }
        public class AcctSubHeads
        {
            public int AcctSubHead { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public APIGetRecordsDynamic<ActType> GetAccountType(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActType, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public class UpdateAccountHeadSubTransaction
        {
            public long ID_AccountHeadSubTransaction { get; set; }
            public long FK_AccountHeadSubTransaction { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public bool Transfer { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }         
            public long FK_Machine { get; set; }
            public Int16 UserAction { get; set; }
            //  public Int64? LastID { get; set; } = 0;
            public Int64 LastID { get; set; } = 0;
            public string AccountTransactionItems { get; set; }
            public long AhstGroupID { get; set; }
            public long Debug { get; set; }
            public string TaxDetails { get; set; }
        }

        public class AccountHeadSubTransactionView
        {
             public long ID_AccountHeadSubTransaction { get; set; }
            public bool Transfer { get; set; }
            public string TransMode { get; set; }
            public Int64 LastID { get; set; } = 0;
            public List<AccountTransaction> AccountTransactionItems { get; set; }
            public long FK_AccountHeadSubTransaction { get; set; }
            public long AhstGroupID { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public Int64 TotalCount { get; set; }
            public List<TaxTypeDet> TaxDetails { get; set; }
        }
        public class DeleteAccountHeadSubTransaction
        {
            public string TransMode { get; set; }           
            public long Debug { get; set; }
            public long GroupID { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int FK_Company { get; set; }
        }
        public class GetAccountHeadSubTransaction
        {
            public string TransMode { get; set; }
            public long GroupID { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }         
            public Int32 Detailed { get; set; }          
        
        }
        public class AccountTransactionSelectDetails
        {
            public Int64 ID_AccountHeadSubTransaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 Detailed { get; set; }
        }
        public class AccountTransactionItemDetails
        {
            public Int64 ID_AccountHeadSubTransaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 Detailed { get; set; }

        }

        public class AccountHeadSubTransactionID
        {
            public Int64 ID_AccountHeadSubTransaction { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public long AhstGroupID { get; set; }

        }

        public class TaxGroup
        {
            public long FK_Product { get; set; }
            public decimal Amount { get; set; }
            public int IncludeTax { get; set; }
            public int Sales { get; set; }
            public long Quantity { get; set; }
            public long FK_TaxGroup { get; set; }
        }
        public class TaxgroupViewOut
        {
            public long SlNo { get; set; }
            public long ID_TaxSettings { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public long TaxtyInterstate { get; set; }
            public decimal TaxUpto { get; set; }
            public long TaxUptoPercentage { get; set; }
            public decimal Amount { get; set; }

        }

        public class TaxTypeDet
        {
            public long SlNo { get; set; }
            public long UID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal Amount { get; set; }
           // public long ProductID { get; set; }
        }
        public class AccountTaxView
        {
            public long UID { get; set; }
            public string TaxtyName { get; set; }
            public long FK_TaxType { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal Amount { get; set; }
        }
        public class BranchHead
        {
            public long FK_Branch { get; set; }
            public string BranchName { get; set; }
        }
        public class InputBranch
        {
            public long FK_Company { get; set; }
        }
        public class PaymentView
        {
            public long PaymentmethodID { get; set; }
            public string Name { get; set; }
        }

        public static string _deleteProcedureName = "ProAccountHeadSubTransactionDelete";
        public static string _updateProcedureName = "ProAccountHeadSubTransactionUpdate";
        public static string _selectProcedureName = "ProAccountHeadSubTransactionSelect";


        public class GetAccOutput
        {
            public string ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }

        //public Output UpdateAccountHeadSubTransactionData(UpdateAccountHeadSubTransaction input, string companyKey)
        //{
        //    return Common.UpdateTableData<UpdateAccountHeadSubTransaction>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        //}

        public APIGetRecordsDynamic<GetAccOutput> UpdateAccountHeadSubTransactionData(UpdateAccountHeadSubTransaction input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetAccOutput, UpdateAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _updateProcedureName, parameter: input);
        }

        public Output DeleteAccountHeadSubTransactionData(DeleteAccountHeadSubTransaction input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAccountHeadSubTransaction>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AccountTransactionData> GetAccountHeadSubTransactionData(GetAccountHeadSubTransaction input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountTransactionData, GetAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<AccountTransactionViewData> GetAccountHeadSubTransactionSelectDetails(GetAccountHeadSubTransaction input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountTransactionViewData, GetAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<AccountTransactionupdateview> GetAccountHeadSubTransactionItemDetailsSelect(GetAccountHeadSubTransaction input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountTransactionupdateview, GetAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<TaxgroupViewOut> FillTaxgroup (TaxGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxgroupViewOut, TaxGroup>(companyKey: companyKey, procedureName: "proTaxCalculation", parameter: input);

        }

        public APIGetRecordsDynamic<AccountTaxView> GetAccounttransactionTax(GetAccountHeadSubTransaction input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountTaxView, GetAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<BranchHead> GetBranchHead(InputBranch input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchHead, InputBranch>(companyKey: companyKey, procedureName: "ProSelectAccountHeadBranch", parameter: input);
        }

        public APIGetRecordsDynamic<PaymentMethodModel.PaymentMethodView> GetpaymentMethodDetails(InputBranch input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentMethodModel.PaymentMethodView, InputBranch>(companyKey: companyKey, procedureName: "ProPaymentMethodWithoutCreditSelect", parameter: input);
        }
    }
}