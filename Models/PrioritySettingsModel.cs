using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class PrioritySettingsModel
    {
        public class PrioritySettingsModelView
        {
            public long SlNo { get; set; }
            public string EffectDate { get; set; }
           
          
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
            public long ProductID { get; set; }
            public string ProductName { get; set; }
            public List<CriteriaDetails> CriteriaDetails { get; set; }
            public List<PriorityList> PriorityList { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<CriteriaList> CriteriaLists { get; set; }
            public long Days { get; set; }
            public long Count { get; set; }
            public long PriorityID { get; set; }
            public long CriteriaID { get; set; }
            public bool Warning { get; set; }
            public long Warningbefore { get; set; }
            public long AmountCriteria { get; set; }
            public long Amount { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public Int64? LastID { get; set; }
            public long ID_PrioritySettings { get; set; }
        }

        public class PrioritySettingsviewinput
        {
            public string EffectDate { get; set; }
            public long ProductID { get; set; }    
            public long CategoryID { get; set; }
            public List<CriteriaDetails> CriteriaDetails { get; set; }
        }
        public class CriteriaDetails
        {
            public long PriorityID { get; set; }
            public long CriteriaID { get; set; }
            public long Days { get; set; }
            public long Count { get; set; }
            public bool Warning { get; set; }
            public long Warningbefore { get; set; }
            public long AmountCriteria { get; set; }
            public long Amount { get; set; }
            public long FK_PrioritySetting { get; set; }
        }

        public class UpdatePrioritySettings
        {
            public byte UserAction { get; set; }
            public long ID_PrioritySetting { get; set; }
            public string PSEffectDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Category { get; set; }
            public string CriteriaDetails { get; set; }
         
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_PrioritySetting { get; set; }
        }
        public APIGetRecordsDynamic<PriorityList> GetPriorityList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<PriorityList, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<CriteriaList> GetCriteriaList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<CriteriaList, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

        public Output AddPrioritysetting(UpdatePrioritySettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePrioritySettings>(companyKey: companyKey, procedureName: "ProPrioritySettingUpdate", parameter: input);
        }
        public class PrioritySettingViewID
        {
            public Int64 FK_PrioritySetting { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public String TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
            public Byte Detailed { get; set; }

        }
        public APIGetRecordsDynamic<PrioritySettingsModelView> GetPrioritSettinglistviewData(PrioritySettingViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<PrioritySettingsModelView, PrioritySettingViewID>(companyKey: companyKey, procedureName: "ProPrioritySettingSelect", parameter: input);
        }
        public class PriorityList
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class CriteriaList
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class Category
        {
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
          
        }

        public APIGetRecordsDynamic<PrioritySettingsModelView> GetProPrioritySettingData(PrioritySettingViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<PrioritySettingsModelView, PrioritySettingViewID>(companyKey: companyKey, procedureName: "ProPrioritySettingSelect", parameter: input);
        }
        public APIGetRecordsDynamic<CriteriaDetails> GetProPrioritySettingSubtableData(PrioritySettingViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CriteriaDetails, PrioritySettingViewID>(companyKey: companyKey, procedureName: "ProPrioritySettingSelect", parameter: input);
        }

        public class PrioritySettingsdeleteInfoView
        {
            public long PrioritySettingID { get; set; }
            public long ReasonID { get; set; }
        }
        public class DeletePrioritySettings
        {
            public long FK_PrioritySetting { get; set; }
            public string TransMode { get; set; }

            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }
        public Output DeletePrioritySettingsData(DeletePrioritySettings input, string companyKey)
        {
            return Common.UpdateTableData<DeletePrioritySettings>(parameter: input, companyKey: companyKey, procedureName: "ProPrioritySettingDelete");
        }
    }
}