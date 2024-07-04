using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AccountsReportModel
    {
        public class Accountslist
        {
            public string Companyname { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Reports> ReportsList { get; set; }            
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
              
           public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public int BranchID { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int TableCount { get; set; }
            public string Branch { get; set; }
            public long AccountHead { get; set; }
            public string AccountHeadName { get; set; }
            public long AccountSubHead { get; set; }
            public string AccountSubHeadName { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
            public string OpeningBalance { get; set; }
            public string BalanceType { get; set; }
            public List<AccountGroup> AccountGroup { get; set; }
            public List<AccountSubGroup> AccountSubGroup { get; set; }
            public List<AccountType> AccountType { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class Reports
        {
            public Int32 ID_Report { get; set; }
            public string ReportsName { get; set; }
        }
       
        public class Accountdetailsview
        {
            public string Branch { get; set; }
            public long ID_AccountHead { get; set; }
            public long AccountHead { get; set; }
            public string AccountHeadName { get; set; }
            public long? AccountSubHead { get; set; }
            public string AccountSubHeadName { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
            public string OpeningBalance { get; set; }
            public string BalanceType { get; set; }
            //test 
            public string AccountGroup { get; set; }
            public string AccountSubGroup { get; set; }
            public string Ahname { get; set; }
            public string Accountcode { get; set; }
            public string Subhead { get; set; }
            public string Accountmode { get; set; }
            public string CreditOPBalance { get; set; }
            public string DebitOPBalance { get; set; }
            public string Receipt { get; set; }
            public string Payment { get; set; }
            public string CreditClosingBal { get; set; }
            public string DebitClosingBal { get; set; }

        }
        public class Accountdetailsinput {
            public int ReportMode { get; set; }
            public long FK_Branch { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_AccountHead { get; set; }
            public long ?FK_AccountSubHead { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int TableCount { get; set; }
            public long AccountGroup { get; set; }
            public long AccountSubGroup { get; set; }

        }

        public class Transctiondetailsview
        {
            public string SlNo { get; set; }
            public string Voucher{ get; set; }
           
            public string TransDate { get; set; }
            public string EffectDate { get; set; }
            public string Party{ get; set; }
            public string Remarks { get; set; }
            public string Payment { get; set; }
            public string Receipt { get; set; }
            public string Balance { get; set; }
        }
        public class AccountGroup
        {
            public Int32 ID_AccountGroup { get; set; }
            public string AGName { get; set; }
        }

        public class AccountSubGroup
        {
            public Int32 ID_AccountSubGroup { get; set; }
            public string ASGName { get; set; }

        }
        public class AccountHeadViewData
        {
            public long FK_AccountGroup { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public long FK_AccountSubGroup { get; set; }
        }
        public class AccountSubGroupView
        {
            public int SlNo { get; set; }
            public long ID_AccountSubGroup { get; set; }
            public long FK_AccountGroup { get; set; }
            public string ASGName { get; set; }
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
        }
        public class RAndDReoportInput
        {
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
           // public int BranchID { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long AccountHead { get; set; }
            public long AccountSubHead { get; set; }
            public int ReportMode { get; set; }
            public int AccountGroup { get; set; }
            public int AccountSubGroup { get; set; }


            //
         
            public long FK_Branch { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
   
            //public long FK_BranchCodeUser { get; set; }
      
            //public long FK_Machine { get; set; }
            public int TableCount { get; set; }
        }
        public class AccountTypeMode
        {
            public Int32 Mode { get; set; }
        }
        public class AccountType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public APIGetRecordsDynamic<Accountdetailsview> GetAccountDetailsData(Accountdetailsinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Accountdetailsview, Accountdetailsinput>(companyKey: companyKey, procedureName: "ProRptAccounts", parameter: input);

        }
        public APIGetRecordsDynamic<Transctiondetailsview> GetTransactionDetailsData(Accountdetailsinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Transctiondetailsview, Accountdetailsinput>(companyKey: companyKey, procedureName: "ProRptAccounts", parameter: input);

        }
        public APIGetRecordsDynamic<AccountSubGroupView> GetSubGroupList(AccountHeadViewData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountSubGroupView, AccountHeadViewData>(companyKey: companyKey, procedureName: "ProAccountSubGroupSelectByGroup", parameter: input);
        }
        public APIGetRecordsDynamic<AccountType> GetAccountTypeList(AccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountType, AccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}