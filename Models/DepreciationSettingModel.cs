using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class DepreciationSettingModel
    {
        public class DepreciationSettingView
        {
            public long SlNo { get; set; }
            public long ID_DepreciationSettings { get; set; }
            //public long FK_PriceFixingType { get; set; }
            public DateTime EffectDate { get; set; }
            //public string DsPeriodType { get; set; }
            //public decimal DsPeriod { get; set; }
            //public Int16 DsAmountType { get; set; }
            //public decimal DsAmount { get; set; }
            public List<PeriodType> PeriodList { get; set; }
            public List<ModeType> ModeList { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public List<DepreciationSettingsDetails> DepreciationSettingsDetails { get; set; }
            public string TransMode { get; set; }
            public string Mode { get; set; }
            public string ModeType { get; set; }

            public string Product { get; set; }
            public Int16? FK_Product { get; set; }
            public Int16 FK_Category { get; set; }
            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }


        }
        public class CategoryList
        {
            public Int32 FK_Category { get; set; }
            public string Category { get; set; }

        }
        public class PeriodType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ModeType
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Modes
        {
            public Int32 Mode { get; set; }
        }
        public class AddDepreciation
        {
            public long ID_DepreciationSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public string Mode { get; set; }
        
            public string DepreciationSettingsDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_BranchType { get; set; }

            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_Depreciationsettings { get; set; }



        }
        public class DepreciationsettingsDelete
        {
            public long FK_DepreciationSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }
        public class DepreciationsettingsID
        {
            public long FK_DepreciationSettings { get; set; }



            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Byte Details { get; set; }
        }

        public class DepreciationSettingsDetails
        {
            public Int16 ID_DepreciationSettings { get; set; }

            public Int16 FK_Category { get; set; }

            public Int16? FK_Product { get; set; }
            public Int16 DsAmountType { get; set; }
            public Int16 ?DsPeriodType { get; set; }

            public decimal DsAmount { get; set; }
            public Int16? DsPeriod { get; set; }

            public string FK_DepreciationSettings { get; set; }
            public string Product { get; set; }
            public string Category { get; set; }





        }

        public class DepreciationSettingDetailsSubSelect
        {
            public Int64 FK_DepreciationSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Byte Details { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public APIGetRecordsDynamic<PeriodType> GetPerdTypList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodType, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<ModeType> GetModeTypList(Modes input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeType, Modes>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<DepreciationSettingView> GetDepreciationsettingsData(DepreciationsettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepreciationSettingView, DepreciationsettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<DepreciationSettingView> GetDepreciationsettingsDatainfoid(DepreciationsettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepreciationSettingView, DepreciationsettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<DepreciationSettingsDetails> GetDepreciationsettingsSubtabledata(DepreciationSettingDetailsSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepreciationSettingsDetails, DepreciationSettingDetailsSubSelect>(companyKey: companyKey, procedureName: "ProDepreciationSettingsSelect", parameter: input);

        }
        public Output AddDepreciationData(AddDepreciation input, string companyKey)
        {
            return Common.UpdateTableData<AddDepreciation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteDepreciationSettings(DepreciationsettingsDelete input, string companyKey)
        {
            return Common.UpdateTableData<DepreciationsettingsDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public static string _updateProcedureName = "ProDepreciationSettingsUpdate";
        public static string _deleteProcedureName = "ProDepreciationSettingsDelete";
        public static string _selectProcedureName = "ProDepreciationSettingsSelect";
    }
}