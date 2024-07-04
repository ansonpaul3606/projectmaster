using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class StageWiseCollectionReportModel
    {

        public class StageWiseCollectionReportView
        {

            public long FK_Category { get; set; }
            public DateTime AsOnDate { get; set; }
            public long FK_Project { get; set; }
            public List<Category> CategoryList { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
        }

       
        public class Category
        {
            public string Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class StageWisereport
        {


            public long FK_Category { get; set; }
            public DateTime AsOnDate { get; set; }
            public long FK_Project { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }




        }

        public class ReportOutview
        {

            public long SLNo { get; set; }
            public string ProjectName { get; set; }
            public decimal ProjectFinalAmount { get; set; }

            public string CurrentStage { get; set; }
            public string ProjectStageStartDate { get; set; }

            public string ProjectStageDueDate { get; set; }
            public string ProjectCurrentStageCompleteDate { get; set; }
            public decimal ProjectTargetAmount { get; set; }
            public decimal ProjectCollectionAmount { get; set; }
            public decimal ProjectDueAmount { get; set; }
            public decimal DueDays { get; set; }


        }
        public APIGetRecordsDynamic<ReportOutview> GetStageWiseCollectionReport(StageWisereport input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportOutview, StageWisereport>(companyKey: companyKey, procedureName: "ProRptProjectStageWiseReport", parameter: input);

        }


    }
}