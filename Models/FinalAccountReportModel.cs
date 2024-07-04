using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class FinalAccountReportModel
    {
        public class FinalAccountReportview
        {
            public DataTable MyDataTable { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<ReportType> ReportType { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }


        public class FinalAccountReportDto
        {
            public int ReportName { get; set; }
            //public long Branch { get; set; }
            public DateTime AsonDate { get; set; }
            public long BranchID { get; set; }
            public long FK_Company { get; set; }
           // public string EntrBy { get; set; }


        }
        public class Toatal
        {
            public decimal TotalA { get; set; }
            public decimal TotalB { get; set; }
        }

        public class ReportType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class AccountGridview
        {
            public long ID_FinalAccount { get; set; }
            public string FaPlName { get; set; }
            public int SortOrder { get; set; }

            public decimal Amount { get; set; }
            public long LevelID { get; set; }
            public long MLevelID { get; set; }
            public string Mode { get; set; }
          
        }
        public class ReportTypeMode
        {
            public Int32 Mode { get; set; }
        }

        public APIGetRecordsDynamic<AccountGridview> GetFinalAccountReportView(FinalAccountReportDto input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountGridview, FinalAccountReportDto>(companyKey: companyKey, procedureName: "proFinalAccountReport", parameter: input);

        }
        public APIGetRecordsDynamic<ReportType> FillReportType(ReportTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportType, ReportTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        } 

    }
}