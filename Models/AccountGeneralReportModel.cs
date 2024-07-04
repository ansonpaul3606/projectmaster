using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AccountGeneralReportModel
    {

        public class AccountGeneralReportView
        {

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; } 
            public List<Branchs> BranchList { get; set; }
            public List<TransactionType> TransactionTypeList { get; set; }
            public List<TransactionMode> TransactionModeList{ get; set; }
            public string Companyname { get; set; }
            public long FK_Company { get; set; }
            public int BranchID { get; set; }
            public long ?TransTypeID { get; set; }
            public string TransModeID { get; set; }
            public Int64 SortByID { get; set; }
            public long? AccountHeadSubID { get; set; }
            public long? AccountHeadID { get; set; }
            public string OpeningCashBalance { get; set; }
            public string ClosingCashBalance { get; set; }
            public string ReferenceNumber { get; set; }
            public Int64 GroupByID { get; set; }
        }

      public class AccountGeneralViewInput
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long BranchID { get; set; }
            public long TransTypeID { get; set; }
            public string TransModeID { get; set; }
            public Int64 SortByID { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string ReferenceNumber { get; set; }            
            public long FK_AccountHead { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public Int64 GroupByID { get; set; }

        }

        public class Branchs
        {
            public long BranchID { get; set; }
            public string Branch { get; set; }

        }

        public class TransactionType
        {
            public string TransType { get; set; }
            public long TransTypeID { get; set; }

        }
        public class TransactionMode
        {
            public string TransMode { get; set;}
            public string TransModeID { get; set; }
        }

        public class AccountHeadView
        {

            public long AccountHeadID { get; set; }

            public string AHeadName { get; set; }

        }
        public class AccountSubHeadView
        {
            public long AccountHeadSubID { get; set; }
            public string ASHeadName { get; set; }

        }

        public class AccountbalanceInput
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long BranchID { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }

        public class AccountGridview
        {
            public string Voucher { get; set;}
            public string AccountCode { get; set; }
            public string SubHead { get; set; }
            public string Party { get; set; }
            public string TransType { get; set; }
            public string TransDate { get; set; }
           
            public string Transfer { get; set;}
            public string Branch { get; set; }
            public string Payment { get; set; }
            public string Receipt { get; set; }
            public string Narration { get; set; }
            public string ReferenceNumber { get; set; }
        }

        public class ShowBalanceCash
        {
            public string OpeningCashBalance { get; set; }
            public string ClosingCashBalance { get; set; }
            public string Receipt { get; set; }
            public string Payment { get; set; }

        }
        
     
        public APIGetRecordsDynamic<AccountGridview> GetAccountGeneralReportView(AccountGeneralViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountGridview, AccountGeneralViewInput>(companyKey: companyKey, procedureName: "proAccountgeneralReportView", parameter: input);
        }
        public APIGetRecordsDynamic<ShowBalanceCash> GetBalanceDetailsData(AccountbalanceInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ShowBalanceCash, AccountbalanceInput>(companyKey: companyKey, procedureName:"proAccountBalance", parameter: input);
        }
    }
}