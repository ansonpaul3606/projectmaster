using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class NumberGenerateModel
    {
        public class NumberGenerateNew
        {
            public IEnumerable<ActionStatus> GenerationCriteria { get; set; }
            public IEnumerable<MenuGroupModel.MenuGroupView> ModuleList { get; set; }
            public IEnumerable<ActionStatus> SubModuleList { get; set; }
            public IEnumerable<ActionStatus> ResetPeriod { get; set; }
            public IEnumerable<ActionStatus> Type { get; set; }
        }
        public class NumberGeneration
        {          
            public long ID_CommonSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public long CSNoGenCriteria { get; set; }
            public long CSNoResetPeriod { get; set; }
            public long FK_Module { get; set; }
            public long FK_SubModule { get; set; }
            public List<NumberGenerationDtl> CommonSettingsDetails  { get; set; }
        }
        public class NumberGenerationDtl
        {
            public long ID_CommonSettingsDetails { get; set; }
            public Int64 CSDetType { get; set; }
            public string CSDetPreference { get; set; }
            public Int64 CSDetMinLength { get; set; } = 0;
            public string CSDetValue { get; set; } = "";
            public Int64 FK_CSDetValue { get; set; } = 0;
        } 
        public class NumberGenerationUpdate
        {
            public int UserAction { get; set; }
            public int Debug { get; set; } 
            public long ID_CommonSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public Int64 CSNoGenCriteria { get; set; }
            public Int64 CSNoResetPeriod { get; set; }
            public Int64 FK_Module { get; set; }
            public Int64 FK_SubModule { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string CommonSettingsDetails { get; set; }
            public long FK_CommonSettings { get; set; }

        }
        public class NumberGenerateModelView
        {
            public long SlNo { get; set; }
            public long ID_CommonSettings { get; set; }
            public string SubModule { get; set; }
            public string Module { get; set; }
            public string CSNoGenCriteria { get; set; }
            public string CSNoResetPeriod { get; set; }
            public DateTime Effectdate { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class NumberGenerateDelete
        {           
            public long ID_CommonSettings { get; set; }
            public int FK_Reason { get; set; }
        }
        public class NumberGenerateModelViewByID
        {          
            public long ID_CommonSettings { get; set; }
            public string SubModule { get; set; }        
            public Int64 CSNoGenCriteria { get; set; }
            public Int64 CSNoResetPeriod { get; set; }
            public DateTime Effectdate { get; set; }
            public Int64 FK_Module { get; set; }
            public Int64 FK_SubModule { get; set; }           
        }
        public class UpdatecommonNumbering
        {
            public Int16 UserAction { get; set; }
            public short Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_CommonSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public long SubModule { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long CmnstAclen { get; set; }
            public long CmnstBrlen { get; set; }
            public long CmnstTylen { get; set; }
            public long CmnstRstprd { get; set; }
            public string CmnstFixcode { get; set; }
            public long CmnstFixpost { get; set; }
            public long CmnstCriteria { get; set; }
            public long FK_CommonSettings { get; set; }
        }

        public class GetNumbergenerate
        { 
            public Int64 FK_CommonSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }
        public class GetNumbergenerateById
        {
            public Int64 FK_CommonSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
      
        public class CommonsettingsViewList
        {
            public List<ActionStatus> Modulelist { get; set; }
            public List<ActionStatus> ResetperiodList { get; set; }
            public List<ActionStatus> SuffixprefixList { get; set; }
            public List<ActionStatus> AccgencriteriaList { get; set; }
            public List<ActionStatus> ProcessList { get; set; }


        }
        public class NumberGenData
        {
            public Int32 Mode { get; set; }
        }
         
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class DeletenumgenMethod
        {
            public long FK_CommonSettings { get; set; }
            //public long PaymentmethodID { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public static string _updateProcedureName = "ProCommonSettingsUpdate";
        public static string _selectProcedureName = "ProCommonSettingsSelect";
        public static string _deleteProcedureName = "ProCommonSettingsDelete";
        public Output UpdateCommonNumberingSettings(NumberGenerationUpdate input, string companyKey)
        {
            return Common.UpdateTableData<NumberGenerationUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<NumberGenerateModelView> GetNumbergenerateData(GetNumbergenerate input, string companyKey)
        {
            return Common.GetDataViaProcedure<NumberGenerateModelView, GetNumbergenerate>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<NumberGenerateModelViewByID> GetNumbergenerateDataByID(GetNumbergenerateById input, string companyKey)
        {
            return Common.GetDataViaProcedure<NumberGenerateModelViewByID, GetNumbergenerateById>(companyKey: companyKey, procedureName: "ProCommonSettingsSelectByID", parameter: input);
        }
        public APIGetRecordsDynamic<NumberGenerationDtl> GetNumbergenerateDetailsDataByID(GetNumbergenerateById input, string companyKey)
        {
            return Common.GetDataViaProcedure<NumberGenerationDtl, GetNumbergenerateById>(companyKey: companyKey, procedureName: "ProCommonSettingsDetailsSelectByID", parameter: input);
        }
        
        public APIGetRecordsDynamic<ActionStatus> GetModulesList(NumberGenData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, NumberGenData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

        public Output DeleteNumbergenerateData(DeletenumgenMethod input, string companyKey)
        {
            return Common.UpdateTableData<DeletenumgenMethod>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

    }
}