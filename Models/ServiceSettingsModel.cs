using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ServiceSettingsModel
    {
        public static string _updateProcedureName = "ProPeriodicServiceSettingsUpdate";
        public static string _selectProcedureName = "ProPeriodicServiceSettingsSelect";
        public static string _deleteProcedureName = "ProPeriodicServiceSettingsDelete";
        public class ServiceSettings
        {
            public List<CategoryList> CategoryLists { get; set; }
            public List<Services> ServiceList { get; set; }
            public List<PeriodType> PeriodTypes { get; set; }
        }
        public class CategoryList
        {
            public Int32? CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class Services
        {
            public Int16 ServiceID { get; set; }
            public string ServiceList { get; set; }
        }
        public class PeriodType
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class PeriodTypeInput
        {
            public int Mode { get; set; }
        }
        public class GetServiceSettings
        {
            public Int64 FK_PeriodicServiceSettings { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public int Detailed { get; set; }

        }
        public class ServiceSettingsView
        {           
            public int SlNo { get; set; }
            public long ID_PeriodicServiceSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Category { get; set; }
            public string Category { get; set; }
            public long FK_Product { get; set; }
            public string Product { get; set; }
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
        }
        public class ServiceSettingsNew
        {
            public long ID_PeriodicServiceSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public string TransMode { get; set; }
            public  List<PeriodicServiceSettingsDetails> PeriodicServiceSettingsDetails  { get; set; }
        }
        public class PeriodicServiceSettingsDetails
        {
            public long FK_Service { get; set; }
            public int PssdPeriodType { get; set; }
            public long PssdPeriod { get; set; }
            public decimal PssdServiceCost { get; set; }
            public int PssdGeneratebefore { get; set; }
        }
        public class ServiceSettingsUpdate
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public long ID_PeriodicServiceSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string PeriodicServiceSettingsDetails { get; set; }
            public long FK_PeriodicServiceSettings { get; set; }
        }
        public class DeleteServiceSettings
        {
            public long FK_PeriodicServiceSettings { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public Output UpdateServiceSettingsData(ServiceSettingsUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ServiceSettingsUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<ServiceSettingsView> GetServiceSettingsData(GetServiceSettings input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceSettingsView, GetServiceSettings>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<PeriodicServiceSettingsDetails> GetServiceSettingsDtlsData(GetServiceSettings input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodicServiceSettingsDetails, GetServiceSettings>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public Output DeleteServiceSettingsData(DeleteServiceSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteServiceSettings>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}