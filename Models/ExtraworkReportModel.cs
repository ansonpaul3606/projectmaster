using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ExtraworkReportModel
    {
        public class ExtraworkReportView
        {
         
            public List<StageList> StageList { get; set; }
            public List<Popupview_common> ProjectList { get; set; }
            public List<Popupview_subContarctor> SubcontractorList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public List<SupplierMode> listMode { get; set; }
        }
        public class Popupview_subContarctor
        {
            public string SuppName { get; set; }
            public Int64 ID_Supplier { get; set; }
        }
        public class Popupinput
        {
            public Int64 Pagemode { get; set; }
            public Int64 ID { get; set; }
            public Int64 Critrea1 { get; set; }
            public Int64 Critrea2 { get; set; }
            public string Critrea3 { get; set; }
            public string Critrea4 { get; set; }
            public Int64 FK_Company { get; set; }

        }
        public class Popupview_common
        {
            public string ID_Name { get; set; }
            public Int64 ID_FIELD { get; set; }
        }
        public class Reportdto
        {
            public  Int64 FK_WorkType { get; set; }
            public Int64 FK_Stage { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime FromDate { get; set; }
            public Int64 FK_Project { get; set; }
            public Int64 FK_Supplier { get; set; }
            public Int32 PageSize { get; set; }
            public Int32 PageIndex { get; set; }
            public Int64 FK_Company { get; set; }
   
        }
        public class ReportOutData
        {
            public Int64 SlNo { get; set; }
            public Int64 TotalCount { get; set; }
           
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Project { get; set; }
            public string Worktype { get; set; }
            public string Stage { get; set; }
            public string Subcontractor { get; set; }
            public string Description1 { get; set; }
            public decimal Amount { get; set; }
        }
        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }
        public class StageList
        {
            public string Mode { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }
        public class SupplierMode
        {
            public int ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class Modetype
        {
            public int Mode { get; set; }
        }

        public APIGetRecordsDynamic<Popupview_common> GetProjectData(Popupinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Popupview_common, Popupinput>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);

        }
        public APIGetRecordsDynamic<Popupview_common> GetStageData(Popupinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Popupview_common, Popupinput>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);

        }
        public APIGetRecordsDynamic<SupplierMode> GetSupplierModeData(Modetype input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierMode, Modetype>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<ReportOutData> GetReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportOutData, Reportdto>(companyKey: companyKey, procedureName: "ProProjectExtraWorkReport", parameter: input);

        }
        public APIGetRecordsDynamic<Popupview_subContarctor> GetSubcontractorpopup(Popupinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Popupview_subContarctor, Popupinput>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);

        }
    }
}