using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class Renewalreportmodel
    {
        public class RenewalReportView
        {

            public String Vehicle { get; set; }           
           
            public long ID_Name { get; set; }
            public List<PaperList> PaperList { get; set; }
            public List<ProviderList> ProviderList { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }

            public long FK_ModuleType { get; set; }
            public long FK_Provider { get; set; }
            public long FK_Papper { get; set; }
            public DateTime AsonDate { get; set; }
            public long DemandDays { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public string PmdPaperNo { get; set; }
            public long Mode { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }



        }
        public class PaperList
        {
            public long FK_Paper { get; set; }
            public string PaperName { get; set; }
        }
        public class ProviderList
        {
            public long FK_Provider { get; set; }
            public string PaperProviderName { get; set; }

        }
    
        public class renewalreport
        {
         
            public string TransMode { get; set; }
           
		    public long FK_ModuleType { get; set; }
            public long FK_Provider { get; set; }
            public long FK_Papper { get; set; }
            public string PmdPaperNo { get; set; }
            public DateTime AsonDate { get; set; }
            public long DemandDays { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }

            public long Mode { get; set; }
            public Int32 Pageindex { get; set; }
            public Int32 PageSize { get; set; }




        }
        public class reportview
        {
            public string ID { get; set; }
            public string NAME { get; set; }
            public DateTime VehRegDate { get; set; }
            public string PapperName { get; set; }
            public string PapperNo { get; set; }
            public long ID_Papper { get; set; }
            public long FK_Master { get; set; }
            public long ID_Provider { get; set; }
            public string ProviderName { get; set; }
            public DateTime IssueDate { get; set; }
            public DateTime ExpireDATE { get; set; }
           public long RemainingDays { get; set; }

            public decimal Amount { get; set; }
        
            public int MasterID { get; set; }
            public string Field { get; set; }
            public int Value { get; set; }
            public Int64 TotalCount { get; set; }

        }

        public APIGetRecordsDynamic<reportview> GetRenewData(renewalreport input, string companyKey)
        {
            return Common.GetDataViaProcedure<reportview, renewalreport>(companyKey: companyKey, procedureName: "ProRptPapperMaintananceReport", parameter: input);

        }

    }
}