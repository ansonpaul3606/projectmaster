using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EmployeeWiseTargetSettingsModel
    {
        public class EmployeeWiseTargetSettingsViewList
        {
            public Int16 SlNo { get; set; }
            public long ID_EmployeeWiseTargetSettings { get; set; }
            public Int16 Module { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Designation { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public decimal TargetAmount { get; set; }
            public Int16 TargetCount { get; set; }
            public Int16 PeriodType { get; set; }
            public Int16 Period { get; set; }
            public Int16 Mode { get; set; }
            public Int16 Deactivate { get; set; } = 0;
            public List<ModeDetails> ModeList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<DesignationList> DesignationList { get; set; }
            public List<PeriodDetails> PerioTypeList { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }

        }
        public class ModeDetails
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class PeriodDetails
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ModeInput
        {
            public int Mode { get; set; }
            public long Group { get; set; } = 0;

        }
        public class UpdateEmployeeWiseTargetSettings
        {
            public Int16 UserAction { get; set; }
            public long ID_EmployeeWiseTargetSettings { get; set; }
            public Int16 EtsModule { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_Designation { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public decimal EtsAmount { get; set; }
            public Int16 EtsCount { get; set; }
            public Int16 EtsPeriodType { get; set; }
            public long EtsPeriod { get; set; }
            public Int16 EtsMode { get; set; }
            public Int16 Deactivate { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

        }
        public class GetEmpTargetSettings
        {
            public Int64 FK_EmployeeWiseTargetSettings { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class GetEmployeeWiseTargetSettings
        {
            public Int16 SlNo { get; set; }
            public long ID_EmployeeWiseTargetSettings { get; set; }
            public Int16 Module { get; set; }
            public string ModuleName { get; set; }
            public DateTime EffectDate { get; set; }
            public long? FK_Designation { get; set; }
            public long? FK_Department { get; set; }
            public long? FK_Employee { get; set; }
            public string Employee { get; set; }
            public decimal TargetAmount { get; set; }
            public Int16? TargetCount { get; set; }
            public Int16 PeriodType { get; set; }
            public Int16 Period { get; set; }
            public Int16 Mode { get; set; }
            public Int64? Deactivate { get; set; }            
            public Int64? TotalCount { get; set; }
        }
        public class DeleteEmployeeWiseTargetSettings
        {
            public long FK_EmployeeWiseTargetSettings { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_Company { get; set; }
            public int FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public APIGetRecordsDynamic<ModeDetails> GetModeList(ModeInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeDetails, ModeInput>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<PeriodDetails> GetPeriodType(ModeInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodDetails, ModeInput>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdateEmployeeEmployeeWiseTargetData(UpdateEmployeeWiseTargetSettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeWiseTargetSettings>(parameter: input, companyKey: companyKey, procedureName: "ProEmployeeWiseTargetSettingsUpdate");
        }
        public APIGetRecordsDynamic<GetEmployeeWiseTargetSettings> GetEmpWiseTargetSettingsData(GetEmpTargetSettings input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetEmployeeWiseTargetSettings, GetEmpTargetSettings>(companyKey: companyKey, procedureName: "ProEmployeeWiseTargetSettingsSelect", parameter: input);
        }
        public Output DeleteEmployeeWiseTargetSettingsData(DeleteEmployeeWiseTargetSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEmployeeWiseTargetSettings>(parameter: input, companyKey: companyKey, procedureName: "ProEmployeeWiseTargetSettingsDelete");
        }
        //public APIGetRecordsDynamic<FinancePlanList> GetFinancePlanData(ID input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<FinancePlanList, ID>(companyKey: companyKey, procedureName: "ProFinancePlanTypeSelect", parameter: input);

        //}
        //ProEmployeeWiseTargetSettingsUpdate
        //ProEmployeeWiseTargetSettingsSelect
        //ProEmployeeWiseTargetSettingsDelete

    }
}