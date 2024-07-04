using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class RptIncentiveProcessListModel
    {
        public class Branchs
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class Incentivetype
        {
            public Int32 IncentiveTypeID { get; set; }
            public string IncentiveType { get; set; }

        }

        public class RptIncentiveProcessListModelview
        {
            public long FK_Employee { get; set; }
            public long FK_IncentiveType { get; set; }

            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<Incentivetype> IncentivetypeList { get; set; }
            public List<PaymentMode> PaymentModeList { get; set; }
            public List<ReportMode> ReportModeList { get; set; }
        }
        public class RptIncentiveProcessinputview
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long ReportType { get; set; }
            public long GroupBy { get; set; }
            public long FK_IncentiveType { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            //public long FK_Machine { get; set; }
        }
        public class CriteriaMode
        {
            public Int32 Mode { get; set; }
            public long Group { get; set; }
        }
        public class PaymentMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ReportMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GetRptIncentivePaid
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long ReportType { get; set; }
            public long GroupBy { get; set; }
            public long FK_IncentiveType { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long PaymentMode { get; set; }
            //public string TransMode { get; set; }
            //public long FK_Machine { get; set; }
        }
        public class RptIncentiveProcessinput
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long ReportType { get; set; }
            public long GroupBy { get; set; }
            public long FK_IncentiveType { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            //public long FK_Machine { get; set; }
            public long PaymentMode { get; set; }
            public long ReportMode { get; set; }
            public string AsOndate { get; set; }
        }
        public class GetLedgerIncentive
        {
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Company { get; set; }
            public long TableCount { get; set; }
          
        }
        public class GetRptIncentiveOutstand
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long ReportType { get; set; }
            public long FK_IncentiveType { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public string AsOndate { get; set; }
        }

        public class LedgerIncentiveData
        {
            public string EmployeeName { get; set; }
            public string BranchName { get; set; }
            public string DepartmentName { get; set; }

        }
        public class LedgerIncentiveDetails
        {
            public string SlNo { get; set; }
            public string IncentiveType { get; set; }
            public string PaymentMethod { get; set; }
            public string Payable { get; set; }
            public string Paid { get; set; }
            public string Balance { get; set; }

        }
        public class LedgerIncentivefooter
        {
            public string TotalPayable { get; set; }
            public string TotalPaid { get; set; }
            public string Balance { get; set; }

        }
        public APIGetRecordsDynamicdn<DataTable> GetIncentiveProcessReportdataTab(RptIncentiveProcessinputview input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<RptIncentiveProcessinputview>(companyKey: companyKey, procedureName: "ProRptIncentiveProcesslist", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetIncentivePaidList(GetRptIncentivePaid input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<GetRptIncentivePaid>(companyKey: companyKey, procedureName: "ProRptIncentivePaidlist", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetIncentiveOutstandList(GetRptIncentiveOutstand input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<GetRptIncentiveOutstand>(companyKey: companyKey, procedureName: "ProRptIncentiveOutstanding", parameter: input);
        }
        public APIGetRecordsDynamic<LedgerIncentiveData> GetIncentiveLedger(GetLedgerIncentive input, string companyKey)
        {
            return Common.GetDataViaProcedure<LedgerIncentiveData, GetLedgerIncentive>(companyKey: companyKey, procedureName: "ProRptIncentiveLedger", parameter: input);
        }
        public APIGetRecordsDynamic<LedgerIncentiveDetails> GetIncentiveLedgerDetails(GetLedgerIncentive input, string companyKey)
        {
            return Common.GetDataViaProcedure<LedgerIncentiveDetails, GetLedgerIncentive>(companyKey: companyKey, procedureName: "ProRptIncentiveLedger", parameter: input);
        }
        public APIGetRecordsDynamic<LedgerIncentivefooter> GetIncentiveLedgerFooter(GetLedgerIncentive input, string companyKey)
        {
            return Common.GetDataViaProcedure<LedgerIncentivefooter, GetLedgerIncentive>(companyKey: companyKey, procedureName: "ProRptIncentiveLedger", parameter: input);
        }
        public APIGetRecordsDynamic<PaymentMode> GetIncentivePayModeList(CriteriaMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentMode, CriteriaMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<ReportMode> GetIncentiveReportList(CriteriaMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportMode, CriteriaMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
    }
}