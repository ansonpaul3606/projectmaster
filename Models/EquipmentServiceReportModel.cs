using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EquipmentServiceReportModel
    { 



    

        public class ServiceReportView
        {
          


             public long FK_Type { get; set; }
            public long FK_Mode { get; set; }
            public long FK_Transaction { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public List<MaintenancetypeList> MaintenancetypeList { get; set; }
            public List<IncidencetypeList> IncidencetypeList { get; set; }

            public List<ModeList> ModeList { get; set; }



        }
        public class ModeSelect
        {
            public int Mode { get; set; }
        }
        public class MaintenancetypeList
        {
            public Int32 FK_Master { get; set; }
            public string Maintenancetype { get; set; }

        }
        public class IncidencetypeList
        {
            public Int32 FK_Master { get; set; }
            public string Incidencetype { get; set; }

        }
        public class ModeList
        {
            public long FK_Transaction { get; set; }
            public string ModeName { get; set; }


        }

        public class servicereport
        {


            public long FK_Type { get; set; }
            public long FK_Mode { get; set; }
            public long FK_Transaction { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }





        }
      
        public class reportview
        {
            public string ID { get; set; }
            public string Mode { get; set; }
            public string Transation { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Type { get; set; }
            public decimal Amount { get; set; }
            public int MasterID { get; set; }
            public string Field { get; set; }
            public int Value { get; set; }
            public Int64 TotalCount { get; set; }

        }
        public class ReportOutview
        {
            public long SLNo { get; set; }
            public string Vehicle { get; set; }
            public string MaintenanceIncidence { get; set; }
            public string StartsOn { get; set; }
            public string EndsOn { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal NetAmount { get; set; }
        }
        public APIGetRecordsDynamic<reportview> GetServiceData(servicereport input, string companyKey)
        {
            return Common.GetDataViaProcedure<reportview, servicereport>(companyKey: companyKey, procedureName: "ProRptEquipmentServiceReport", parameter: input);

        }

        public APIGetRecordsDynamic<ReportOutview> GetEqServiceGeneralReport(servicereport input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportOutview, servicereport>(companyKey: companyKey, procedureName: "ProRptEquipmentServiceReport", parameter: input);

        }
    }


}
