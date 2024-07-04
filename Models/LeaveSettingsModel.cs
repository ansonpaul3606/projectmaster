using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class LeaveSettingsModel
    {
        public class LeaveSettingsView
        {
            public List<LeaveTypeList> leaveTypeLists { get; set; }
            public List<AllowanceTypeList> recoveryTypeLists { get; set; }
        }

        public class LeaveTypeList
        {
            public long ID_LeaveType { get; set; }

            public string TypeName { get; set; }
        }

        public class AllowanceTypeList
        {
            public long ID_AllowanceType { get; set; }
            public string AllowName { get; set; }
        }

        public class LeaveSettingsInput
        {
            // public long ID_LeaveSettingsDetails { get; set; }
            public long FK_LeaveSettings { get; set; }
            public long FK_AllowanceType { get; set; }
            public decimal LsdDeductPercent { get; set; }

            public long ID_LeaveSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public int LsSalCalculateDays { get; set; }
            public long LsGroupID { get; set; }
            public long FK_LeaveType { get; set; }


            public List<LeaveSettingDetails> LeaveDetails { get; set; }

            public List<string> LeaveType { get; set; }

        }
        public class LeaveSettingDetails
        {
            public long FK_AllowanceType { get; set; }
            public long FK_LeaveSettings { get; set; }
            public decimal LsdDeductPercent { get; set; }
        }
        public class LeaveType
        {

            public string FK_LeaveType { get; set; }
        }

        public class UpdateLeaveSetting
        {
            public long FK_LeaveSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public int LsSalCalculateDays { get; set; }
            ////public long FK_LeaveType { get; set; }

            public long UserAction { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public string LeaveDetails { get; set; }
            ////public string leave { get; set; }
            public string LeaveType { get; set; }
            public long LsGroupID { get; set; }
        }

        public class LeaveSettingsViewInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_LeaveSettings { get; set; }
            public byte Detailed { get; set; }
        }

        public class LeaveSettingViewOutput
        {
            public long SlNo { get; set; }
            public long TotalCount { get; set; }
            public string LeaveType { get; set; }
            public string EffectDate { get; set; }
            public long FK_LeaveType { get; set; }
            public int LsSalCalculateDays { get; set; }
            public long LsGroupID { get; set; }
            public long ID_LeaveSettings { get; set; }

        }
        public class DeleteLeaveSetting
        {
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
            public long FK_LeaveSettings { get; set; }
            public long Debug { get; set; }


        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public long FK_LeaveSettings { get; set; }
        }

        public class GetLeaveDetailsGrid
        {
            public long FK_AllowanceType { get; set; }
            public long FK_LeaveSettings { get; set; }
            public string LsdDeductPercent { get; set; }
            public string AllowanceType { get; set; }
        }
        public Output UpdateLeaveSettingData(UpdateLeaveSetting input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLeaveSetting>(companyKey: companyKey, procedureName: "ProLeaveSettingsUpdate", parameter: input);
        }
        public APIGetRecordsDynamic<LeaveSettingViewOutput> GetLeaveSettinglist(LeaveSettingsViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeaveSettingViewOutput, LeaveSettingsViewInput>(companyKey: companyKey, procedureName: "ProLeaveSettingsSelect", parameter: input);
        }
        public Output DeleteLeaveSettings(DeleteLeaveSetting input, string companyKey)
        {
            return Common.UpdateTableData<DeleteLeaveSetting>(parameter: input, companyKey: companyKey, procedureName: "ProLeaveSettingsDelete");
        }

        public APIGetRecordsDynamic<GetLeaveDetailsGrid> GetLeaveGrid(LeaveSettingsViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetLeaveDetailsGrid, LeaveSettingsViewInput>(companyKey: companyKey, procedureName: "ProLeaveSettingsSelect", parameter: input);
        }
    }

}
