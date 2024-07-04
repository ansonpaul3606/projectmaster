using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class DashboardSettingsModel
    {
        public static string _updateProcedureName = "ProDashboardSettingsUpdate";
        public static string _selectProcedureName = "ProDashboardSettingsSelect";
        public static string _deleteProcedureName = "ProDashboardSettingsDelete";
        public class DashboardSettingsInit
        {          
            public IEnumerable<Userrole> UserRoleList { get; set; }
            public IEnumerable<PeriodDetails> PeriodDetails { get; set; }
        }
        public class Userrole
        {
            public int UserRoleID { get; set; }
            public string UserRole { get; set; }
            public long FK_UserRole { get; set; }
        }
        public class Usersrelated
        {
            public long FK_BranchMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_UserRole { get; set; }
        }
        public class Users
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
        }       
        public class ModeInput
        {
            public int Mode { get; set; }
            public long Group { get; set; } = 0;
        }
        public class PeriodDetails
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class UpdateDashboardSettings
        {
            public long UserAction { get; set; }
            public long Debug { get; set; }
            public long ID_DashboardSettings { get; set; }           
            public long FK_UserGroup { get; set; }
            public long FK_User { get; set; }
            public long FK_Mode { get; set; }
            public DateTime EffectDate { get; set; }
            public string ModuleList { get; set; }
            public string TileList { get; set; }
            public string ChartList { get; set; }
            public string TileMode { get; set; }
            public string ChartMode { get; set; }
            public long FK_Company { get; set; }        
            public string EntrBy { get; set; }
            public long FK_DashboardSettings { get; set; }          
          
        }
        public class GetDashboardSettingsSelect
        {
            public long FK_DashboardSettings { get; set; }           
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }  
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }          
        }
        public class SelectDashboardSettings
        {
            public long SlNo { get; set; }
            public long DashboardSettingsID { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_UserGroup { get; set; }
            public string UserGroup { get; set; }
            public long FK_User { get; set; }
            public string UserName { get; set; }
            public long FK_Mode { get; set; }
            public string ModeName { get; set; }
            public string ModuleList { get; set; }
            public string TileList { get; set; }
            public string ChartList { get; set; }
            public string TileMode { get; set; }
            public string ChartMode { get; set; }
            public int TotalCount { get; set; }     
        }
        public class DeleteDashboardSettings
        {
            public long FK_DashboardSettings { get; set; }
            public int Debug { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
        }
        public APIGetRecordsDynamic<Users> GetDashboardSettingsUsersnamelist(Usersrelated input, string companyKey)
        {
            return Common.GetDataViaProcedure<Users, Usersrelated>(companyKey: companyKey, procedureName: "ProAuthorizationlevelUsersnamelist", parameter: input);
        }
        public APIGetRecordsDynamic<PeriodDetails> GetPeriodType(ModeInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodDetails, ModeInput>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateDashboardSettingsData(UpdateDashboardSettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateDashboardSettings>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<SelectDashboardSettings> GetDashboardSettingsData(GetDashboardSettingsSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<SelectDashboardSettings, GetDashboardSettingsSelect>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public Output DeleteDashboardSettingsData(DeleteDashboardSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDashboardSettings>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}