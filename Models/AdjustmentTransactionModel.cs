using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AdjustmentTransactionModel
    {
            
        public class AdjustmentTransactionView
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
            public DateTime ChequeDate { get; set; }
            public string Party { get; set; }
            public string AhstNarration { get; set; }
            public List<ActType> ActTypes { get; set; }
            public List<AccountHead> AccountHeads { get; set; }
            public List<AccountSubHead> AccountSubHeads { get; set; }
            public List<TransTypes> TransTypes { get; set; }
            public List<Branchs> Branch { get; set; }
            public Int64? LastID { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public decimal AdjAmount { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

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
        public class AdjustmentTransaction
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
            public string AhstChequeDate { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public long AhstGroupID { get; set; }
           
        }

        public class AdjustmentTransactionViewData
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
            public DateTime AhstChequeDate { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public long AhstGroupID { get; set; }

        }
        public class AdjustmentTransactionData
        {
            public long SlNo { get; set; }
            public long ID_AdjustingHeadTransaction { get; set; }            
            public DateTime ProcessDate { get; set; }
            public string FK_Branch { get; set; }
            public long AccountType { get; set; }
            public long FK_AccountHead { get; set; }
           public Int32 TransType { get; set; }
            public string AccountHead { get; set; }
            public decimal AdjAmount { get; set; }
            public int TotalCount { get; set; }
            public Int64? LastID { get; set; }
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

        public class UpdateAccountAdjustment
        {                    
            public Int16 UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_AdjustingHeadTransaction { get; set; }
            public DateTime ProcessDate { get; set; }
            public string TransType { get; set; }
            public long FK_Branch { get; set; }
            public int AccountType { get; set; }
            public long FK_AccountHead { get; set; }
            public decimal AdjAmount { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_AdjustingHeadTransaction { get; set; }
        }

        public class AccountAdjustmentView
        {           
            public string TransMode { get; set; }
            public long ID_AdjustingHeadTransaction { get; set; }
            public DateTime ProcessDate { get; set; }
            public string TransType { get; set; }
            public long FK_Branch { get; set; }
            public int AccountType { get; set; }
            public long FK_AccountHead { get; set; }
            public decimal AdjAmount { get; set; }                
            public Int64 TotalCount { get; set; }
        }
        public class DeleteAccountHeadSubTransaction
        {
            public string TransMode { get; set; }           
            public long Debug { get; set; }
            public long FK_AdjustingHeadTransaction { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int FK_Company { get; set; }
        }
        public class GetAdjustmentTransaction
        {
            public string TransMode { get; set; }
            public long FK_AdjustingHeadTransaction { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public string Name { get; set; }         
            //public Int32 Detailed { get; set; }
           
        }
        public class AdjustmentTransactionSelectDetails
        {
            public Int64 ID_AccountHeadSubTransaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 Detailed { get; set; }
        }
        public class AdjustmentTransactionItemDetails
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
        public static string _deleteProcedureName = "ProAdjustingHeadTransactionDelete";
        public static string _updateProcedureName = "ProAdjustingHeadTransactionUpdate";
        public static string _selectProcedureName = "ProAdjustingHeadTransactionSelect";

        public Output UpdateTransactionData(UpdateAccountAdjustment input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAccountAdjustment>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteAccountHeadSubTransactionData(DeleteAccountHeadSubTransaction input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAccountHeadSubTransaction>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AdjustmentTransactionData> GetAdjustmentTransactionData(GetAdjustmentTransaction input, string companyKey)
        {
            return Common.GetDataViaProcedure<AdjustmentTransactionData, GetAdjustmentTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        //public APIGetRecordsDynamic<AdjustmentTransactionViewData> GetAccountHeadSubTransactionSelectDetails(GetAccountHeadSubTransaction input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<AdjustmentTransactionViewData, GetAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        //}
        //public APIGetRecordsDynamic<AdjustmentTransaction> GetAccountHeadSubTransactionItemDetailsSelect(GetAccountHeadSubTransaction input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<AdjustmentTransaction, GetAccountHeadSubTransaction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        //}
    }
}