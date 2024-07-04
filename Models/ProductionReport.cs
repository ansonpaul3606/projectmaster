using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductionReport
    {

        public class ProductionReportView
        {
            public List<ReportType>ReportType { get; set; }
        }

        public class JobcartReportView
        {

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime AsonDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_product { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Company { get; set; }
            public string Status { get; set; }
            public int ReportMode { get; set; }
            public int GroupBy { get; set; }
            public long ID_JobCard { get; set; }
            public long FK_Material { get; set; }

        }
        public class JobcardReoprt
        {
            public string StartDate { get; set; }
            public string TargetDate { get; set; }
            //public long SLNo { get; set; }
            public int ID_JobCard { get; set; }
            public string JobCardNo { get; set; }
            public string Employee { get; set; }
            public string Priority { get; set; }
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Stock { get; set; }
            public decimal Completed { get; set; }
            public string Stage { get; set; }
            public string Status { get; set; }
            public string DueDays { get; set; }
            public string Material { get; set; }
            public decimal PendingQty { get; set; }
            public string Headers { get; set; }
        }
        public class CommonDropdown
        {
            public int ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ReportType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ReportTypeMode
        {
            public Int32 Mode { get; set; }
        }

        public APIGetRecordsDynamic<JobcardReoprt> GetJobCardReport(JobcartReportView input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobcardReoprt, JobcartReportView>(companyKey: companyKey, procedureName: "ProRptProduction", parameter: input);
        }
        public APIGetRecordsDynamic<ReportType> FillReportType(ReportTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportType, ReportTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}