using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AllowanceSettingModel
    {

        public class AllowanceSettingView
        {
            public Int64 SlNo { get; set; }
            public Int64 ID_AllowanceSettings { get; set; }
            public Int64 AllowancetypeID { get; set; }
            public string Allowancetype { get; set; }
            public Int64 FK_EmployeeType { get; set; }
            public Int64 FK_Designation { get; set; }
            public Int64 FK_AllowanceType { get; set; }
            public Int64 ALWMode { get; set; }
            public string Mode { get; set; }
            public Int64 ? FK_Employee { get; set; }

           public string EmployeeName { get; set; }
            public Int64 EmployeeTypeID { get; set; }
            public string EmployeeType { get; set; }
           
          
            public string AllowanceTypeName { get; set; }
            public DateTime EffectDate { get; set; }
            public Int64 AlsAmountCriteria { get; set;}
            public decimal? AlsAmount { get; set; }
            public short PercentAllowancetypeID { get; set; }
            public string PercentAllowancetype { get; set; }
            public List<AllowanceSettingsDetails> AllowanceSettingsDetails { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public List<ModeList> ModeList { get; set; }
            public string Designation { get; set; }
        }

        public class AllowanceSettingsDetails
        {
           //public Int64 ID_AllowanceSettingsDetails { get; set; }
           // public Int64 FK_AllowanceSettings { get; set; }
            public Int64 FK_AllowanceType { get; set; }
          
            public decimal AlsDetAmount { get; set; }

        }
    
        public class AllowanceSettingsDetailsout
        {
          
            public Int64 FK_AllowanceType { get; set; }
            public string AllowanceTypeName { get; set; }
            public decimal AlsDetAmount { get; set; }

        }
        public class AllowancesettingsListModel
        {

          
            public List<PercentageAllowancetypeList> PercentageAllowancetypeList { get; set; }
            public List<AllowancetypeList> AllowanceList { get; set; }
            public List<AllowancetypeamountList> AllowancetypeamountList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }
            public List<DesignationList> DesignationList { get; set; }
        }
        public class EmployeeTypeList
        {
            public short EmployeeTypeID { get; set; }
            public string EmployeeType { get; set; }

        }
        public class AllowancetypeList
        {
            public short AllowancetypeID { get; set; }
            public string Allowancetype { get; set; }

        }
        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }

        }
        public class AllowancetypeamountList
        {
            public short FK_AllowanceType { get; set; }
            public string AllowanceTypeName { get; set; }

        }
        public class PercentageAllowancetypeList
        {
            public short PercentAllowancetypeID { get; set; }
            public string PercentAllowancetype { get; set; }

        }
        public class AllowanceSettingsUpdate
        {
            public long ID_AllowanceSettings { get; set; }
            public long ALWMode { get; set; }
            public long FK_AllowanceType { get; set; }
            public long FK_EmployeeType { get; set; }
            public long FK_Designation { get; set; }
            public Int64 FK_Employee { get; set; }
            public Int64 AlsAmountCriteria { get; set; }
            public DateTime EffectDate { get; set; }
            public decimal AlsAmount { get; set; }
            public string AllowanceSettingsDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public long FK_AllowanceSettings { get; set; }
          
        }
        public class AllowanceSettingsRsnView
        {
            public long ID_AllowanceSettings { get; set; }

            public long ReasonID { get; set; }
        }
        public class AllowanceSettingsDelete
        {
            public long FK_AllowanceSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public class AllowanceSettingsID
        {
            public long FK_AllowanceSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }
        }
        public static string _updateProcedureName = "ProAllowanceSettingsUpdate";
        public static string _deleteProcedureName = "ProAllowanceSettingsDelete";
        public static string _selectProcedureName = "ProAllowanceSettingsSelect";
        public class ModeList
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class ModeSelect
        {
            public int Mode { get; set; }
        }

        public class AllowanceSettingsDetailsSubSelect
        {
            public Int64 FK_AllowanceSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Byte Detailed { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public APIGetRecordsDynamic<AllowanceSettingsDetails> GetAllowanceSettingsSubtabledata(AllowanceSettingsDetailsSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<AllowanceSettingsDetails, AllowanceSettingsDetailsSubSelect>(companyKey: companyKey, procedureName: "ProAllowanceSettingsSelect", parameter: input);

        }
        public Output UpdateAllowanceSettingsData(AllowanceSettingsUpdate input, string companyKey)
        {
            return Common.UpdateTableData<AllowanceSettingsUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<AllowanceSettingView> GetAllowanceSettingData(AllowanceSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AllowanceSettingView, AllowanceSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}