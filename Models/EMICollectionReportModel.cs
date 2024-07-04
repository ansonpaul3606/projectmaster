using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class EMICollectionReportModel
    {
        public class ReportViewlist
        {
            public List<Branchs> BranchList { get; set; }
            public List<CommonList> CommonListAmount { get; set; }
            public List<CommonList> CommonListDate { get; set; }
            public List<CommonList> GroupByList { get; set; }
            
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
            public long Group { get; set; }
        }
        public class CommonList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
            public int selected { get; set; } = 0;
        }
        public class InputEMICollectionReport
        {
            public Int32 ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public int AmountCriteria { get; set; }
            public decimal AmountFrom { get; set; }
            public decimal AmountTo { get; set; }
            public long Employee_ID { get; set; }
            public int DateCriteria { get; set; }
            public int GroupCriteria { get; set; }
            public string CompName { get; set; }
            public long FK_Area { get; set; }
        }
        public class GetEMICollectionReport
        {
            public Int32 ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public int AmountCriteria { get; set; }
            public decimal AmountFrom { get; set; }
            public decimal AmountTo { get; set; }
            public long FK_Employee { get; set; }
            public int DateCriteria { get; set; }
            public int GroupCriteria { get; set; }
            public string EntrBy { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int FK_Machine { get; set; }
            public int FK_Company { get; set; }
            public long FK_Area { get; set; }
        }
        public APIGetRecordsDynamic<CommonList> GetCommonList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<CommonList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetEMICollectionListReportdata(GetEMICollectionReport input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<GetEMICollectionReport>(companyKey: companyKey, procedureName: "ProRptCustomerwiseEMI"/*"ProRptCustomerwiseEMI"*/, parameter: input);
        }
    }
}