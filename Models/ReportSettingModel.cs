using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ReportSettingModel
    {
        public class ReportSettingModelView
        {
            public Int64 SLNO { get; set; }
            public long ID_ReportSettings { get; set; }
            public string RpName { get; set; }
            public string SbName { get; set; }
            public Int32 SortOrd { get; set; }
            public long FrmtID { get; set; }
            public string FrmtName { get; set; }
            public long MasterID { get; set; }
            public string Masters { get; set; }
            public long FldId { get; set; }
            public string FldName { get; set; }
            public string AliaName { get; set; }
            public string SortFldID { get; set; }
            public string SortFld { get; set; }
            public string SortArea { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int32 Report { get; set; }
            public Int64 TotalCount { get; set; }
            public List<ReportSettingDetailsList> ReportSettingsDetailsType { get; set; }

        }
        public class ReportSettingList
        {
            public List<Field> FieldNam { get; set; }
            public List<Format> FrmtNames { get; set; }
            public List<Mstr> Masters { get; set; }
            public List<sortfield> Sortfields { get; set; }
            public List<ReportNames> reportNames { get; set; }
            public Int32 SortOrd { get; set; }
        }

        public class ReportSettingDetailsList
        {
            public Int64 SLNO { get; set; }
            public long ID_ReportSettingsDetails { get; set; }
            public long FK_ReportSettings { get; set; }
            public long FK_ReportFields { get; set; }
            public string ReportField { get; set; }
            public string AliasName { get; set; }
            public string Formula { get; set; }
            public string FormulaValue { get; set; }
            public bool Cancelled { get; set; }
            public int SortOrder { get; set; }
           
        }
        public class ReportSettingDetailsView
        {
            public Int64 SLNO { get; set; }
            public long ID_ReportSettingsDetails { get; set; }
            public long FK_ReportSettings { get; set; }
            public long FK_ReportFields { get; set; }
            public string ReportField { get; set; }
            public string AliasName { get; set; }
            public string Formula { get; set; }
            public string FormulaValue { get; set; }
            public bool Cancelled { get; set; }
            public int SortOrder { get; set; }

        }

        public class ReptSettRsnView
        {
            public long ID_ReportSettings { get; set; }

            public long ReasonID { get; set; }
        }

        public class ReportNames
        {
            public Int32 ReptId { get; set; }
            public string RptName { get; set; }
          

           
        }
        public  class ReportNamesval
        {
           
            public dynamic countryy { get; set; }
        }
        public class UpdateReportSettingList
        {
            public long ID_ReportSettings { get; set; }
            public string RpsName { get; set; }
            public string RpsSubName { get; set; }
            public long FK_ReportFormat { get; set; }
            public string SortFields { get; set; }
            public string GroupByFields { get; set; }
            public long FK_ReportMasterTable { get; set; }
            public bool IsGrouping { get; set; }
            public int SortOrder { get; set; }
            public bool IsActive { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }          
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string ReportSettingsDetailsType { get; set; }

        }
      

        public class ReportSettingID
        {
            public long FK_ReportSettings { get; set; }

            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }

        public class ReptSettingDelete
        {
            public long FK_ReportSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }

        public class Field
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class Format
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Mstr
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class sortfield
        {
            public int SortFldID { get; set; }
            public string SortFld { get; set; }


        }
        public class ModeLead
        {
            public Int32 ReportFormat { get; set; }
            
        }
        public class FldLead
        {
            public Int32 ReportMasterTable { get; set; }

        }
        public class RptFld
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int32 FK_ReportSettings { get; set; }
            public long FK_Company { get; set; }

        }
        public static string _deleteProcedureName = "ProReportSettingsDelete";
        public static string _updateProcedureName = "ProReportSettingsUpdate";
        public static string _selectProcedureName = "ProReportSettingsSelect";
        public static string _selectdetProcedureName = "ProReportSettingsDetailsSelect";

        public APIGetRecordsDynamic<Format> GetReportFormat(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Format, ModeLead>(companyKey: companyKey, procedureName: "ProCmnReportDashBoardSelect", parameter: input);

        }
        public APIGetRecordsDynamic<Mstr> GetReportMastr(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Mstr, ModeLead>(companyKey: companyKey, procedureName: "ProCmnReportDashBoardSelect", parameter: input);

        }
        public APIGetRecordsDynamic<Field> GetReportField(FldLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Field, FldLead>(companyKey: companyKey, procedureName: "ProCmnReportDashBoardSelect", parameter: input);

        }

        public APIGetRecordsDynamicdn<dynamic> GetReportView(RptFld input, string companyKey)
        {
          return Common.GetDataViaProcedureDynamic<RptFld>(companyKey: companyKey, procedureName: "proReportView", parameter: input);

        }
        public Output UpdateReportSettingData(UpdateReportSettingList input, string companyKey)
        {
            return Common.UpdateTableData<UpdateReportSettingList>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<ReportSettingModelView> GetReportSettingData(ReportSettingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportSettingModelView, ReportSettingID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ReportSettingDetailsView> GetReportSettingdetailsData(ReportSettingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReportSettingDetailsView, ReportSettingID>(companyKey: companyKey, procedureName: _selectdetProcedureName, parameter: input);
        }
        public Output DeleteReptSetData(ReptSettingDelete input, string companyKey)
        {
            return Common.UpdateTableData<ReptSettingDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}