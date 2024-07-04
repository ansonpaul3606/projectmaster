using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class IncentiveSettingsMasterModel
    {
        public class IncentiveSettingsViewlist
        {
            public List<Category> CategoryList { get; set; }
            public List<IncentiveType> IncentiveType { get; set; }
            public List<CalculationBased> CalculationBased { get; set; }
            public List<CalculationType> CalculationType { get; set; }
            public List<IncentiveActivity> IncentiveActivity { get; set; }
        }
        public class IncentiveSettingsView
        {
            public long ID_IncentiveSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_IncentiveType { get; set; }
            public DateTime INSEffectDate { get; set; }
            public Int32 INSCalcBasedon { get; set; }
            public Int32 INSCalcType { get; set; }
            public string INSPeriod { get; set; }
            public Int32 INSActivity { get; set; }
            public bool INSActive { get; set; }
            public List<IncSettingsDetails> IncSettingsDetails { get; set; }
            public int Mode { get; set; }
            public int Dependency { get; set; }
        }
        public class IncSettingsDetails
        {
            public long Category { get; set; }
            public string ProductName { get; set; }
            public long FK_Product { get; set; }
            public long Product { get; set; }
            public string INSDIncentive { get; set; }
            public decimal INSDFrom { get; set; }
            public decimal INSDTo { get; set; }
            public bool INSDPercentage { get; set; }
        }
        public class IncentiveSettingsUpdate
        {
            public long UserAction { get; set; }
            public long ID_IncentiveSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_IncentiveType { get; set; }
            public DateTime INSEffectDate { get; set; }
            public Int32 INSCalcBasedon { get; set; }
            public Int32 INSCalcType { get; set; }
            public string INSPeriod { get; set; }
            public Int32 INSActivity { get; set; }
            public bool INSActive { get; set; }
            public string IncSettingsDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Branch { get; set; }
            public int Dependency { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class IncentiveType
        {
            public Int32 FK_IncentiveType { get; set; }
            public string Incentivetype { get; set; }
            public Int32 IncTModule { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
            public long Group { get; set; }
        }
        public class CalculationBased
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class CalculationType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class IncentiveActivity
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class GetIncentivesettings
        {
            public Int64 ID_IncentiveSettings { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Branch { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public string Name { get; set; }
            public int Mode { get; set; }
        }
        public class GetIncentivesettingsdata
        {
            public long SlNo { get; set; }
            public long FK_IncentiveSettings { get; set; }
            public long FK_IncentiveType { get; set; }
            public string INSTypeName { get; set; }
            public DateTime INSEffectDate { get; set; }
            public string TransMode { get; set; }
            public long INSCalcBasedon { get; set; }
            public string CalcBasedon { get; set; }
            public long INSCalcType { get; set; }
            public string CalcType { get; set; }
            public string INSPeriod { get; set; }
            public long INSActivity { get; set; }
            public string ActivityName { get; set; }
            public bool INSActive { get; set; }
            public long TotalCount { get; set; }
            public long Dependency { get; set; }
        }
        public class DeleteIncentiveSettings
        {
            public string TransMode { get; set; }
            public long ID_IncentiveSettings { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
        }
        public class IncSelectSettingsDetails
        {
            public long Category { get; set; }
            public string ProductName { get; set; }
            public long FK_Product { get; set; }
            public long Product { get; set; }
            public string INSDIncentive { get; set; }
            public string INSDFrom { get; set; }
            public string INSDTo { get; set; }
            public bool INSDPercentage { get; set; }
        }
        public APIGetRecordsDynamic<CalculationBased> GetCalculationBasedList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<CalculationBased, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<CalculationType> GetCalculationTypeList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<CalculationType, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<IncentiveActivity> GetIncentiveActivityList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentiveActivity, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateIncentiveSettingsData(IncentiveSettingsUpdate input, string companyKey)
        {
            return Common.UpdateTableData<IncentiveSettingsUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveSettingsUpdate");
        }

        public APIGetRecordsDynamic<GetIncentivesettingsdata> GetIncentivesettingsList(GetIncentivesettings input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetIncentivesettingsdata, GetIncentivesettings>(companyKey: companyKey, procedureName: "ProIncentiveSettingsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<IncSelectSettingsDetails> GetIncentivesettingsDetails(GetIncentivesettings input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncSelectSettingsDetails, GetIncentivesettings>(companyKey: companyKey, procedureName: "ProIncentiveSettingsSelect", parameter: input);

        }

        public Output DeleteIncentiveSettingsData(DeleteIncentiveSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIncentiveSettings>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveSettingsDelete");
        }

    }
}