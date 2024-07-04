using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SLASettingsModel
    {
        public class SLASettingsView
        {
            public DateTime EffectDate { get; set; }
            public long FK_SLASettings { get; set; }
            public long ID_SLASettings { get; set; }
            public string TransMode { get; set; }
            public long FK_UserRole { get; set; }
            public int Chck_Notification { get; set; }
            public int Chck_WhtsAp { get; set; }
            public int Chck_Email { get; set; }
            public int Chck_Sms { get; set; }
            public List<ActionStatus> PeriodType { get; set; }
            public List<ComplaintList> complaintLists { get; set; }
            public List<CustomerCategory> customerCategories { get; set; }
            public List<SLASubTableView> SLASettingsDetails { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
            public List<Userrole> UserRolelists { get; set; }
            public List<User> UsersList{ get; set; }
            public List<AlertSettings> AlerSettingstData { get; set; }
        }

        public class SLASubTableView
        {

            public long FK_Product { get; set; }
            public long FK_Complaint { get; set; }
            public long FK_CustomerCategory { get; set; }
            public long FK_PeriodType { get; set; }
            public long FK_Period { get; set; }


        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ComplaintList
        {
            public long ID_ComplaintList { get; set; }
            public string CompntName { get; set; }

        }
        public class Userrole
        {
            public int UserRoleID { get; set; }
            public string UserRole { get; set; }
        }
        public class User
        {
            public long FK_User { get; set; }
            public string Users { get; set; }
        }


        public class CustomerCategory
        {
            public long ID_CustomerCategory { get; set; }
            public string CuscattyName { get; set; }

        }

        public class UpdateSLASettings
        {
            public DateTime EffectDate { get; set; }
            public long FK_SLASettings { get; set; }
            public long ID_SLASettings { get; set; }
            public string TransMode { get; set; }
            public Int32 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string SLASettingsDetails { get; set; }
            public string AlerSettingstData { get; set; }

            public int Chck_Notification { get; set; }
            public int Chck_WhtsAp { get; set; }
            public int Chck_Email { get; set; }
            public int Chck_Sms { get; set; }

        }
        public class SLAViewInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_SLASettings { get; set; }
            public int Detailed { get; set; }
        }
        public class SLAoutPutView
        {
            public long SlNo { get; set; }
            public long SLAGroupID { get; set; }

            public DateTime EffectDate { get; set; }
            public long FK_Product { get; set; }
            public long TotalCount { get; set; }
            public long FK_Complaint { get; set; }
            public long FK_CustomerCategory { get; set; }
            public string ProdName { get; set; }
            public long FK_PeriodType { get; set; }
            public long FK_Period { get; set; }

            public long BranchTypeID { get; set; }
            public long FK_UserGroup { get; set; }
            public long FK_User { get; set; }
            public long FK_AlertPeriodType { get; set; }
            public long FK_AlertPeriod { get; set; }
            public string Users { get; set; }

            public bool SASNotification { get; set; }
            public bool  SASWhatsApp { get; set; }
            public bool SASEmail { get; set; }
            public bool SASSMS { get; set; }
        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ID_SLASettings { get; set; }
        }
        public class DeleteSLASettingsInput
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_SLASettings { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }


        }
        public class CategoryList
        {
            public Int32? CategoryID { get; set; }
            public string CategoryName { get; set; }

        }

        public class SLAReportView
        {
            public List<ActionStatus> ModeList { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public List<ComplaintList> complaintLists { get; set; }
            public List<CustomerCategory> customerCategories { get; set; }
            public string TransMode { get; set; }
            public int ReportMode { get; set; }
        }
        public class ModeInput
        {
            public int Mode { get; set; }
            public long Group { get; set; } = 0;

        }
        public class BranchTypes
        {
            public long BranchTypeID { get; set; }
            public string BranchType { get; set; }
        }

        public class AlertSettings
        {

            public long BranchTypeID { get; set; }
            public long FK_UserGroup { get; set; }
            public long FK_User { get; set; }
            public long FK_AlertPeriodType { get; set; }
            public long FK_AlertPeriod { get; set; }


        }
        public APIGetRecordsDynamic<SLAoutPutView> GetSLASettingslist(SLAViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<SLAoutPutView, SLAViewInput>(companyKey: companyKey, procedureName: "ProSLASettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ActionStatus> GetPeriodType(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateSlaSettingsdetails(UpdateSLASettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSLASettings>(parameter: input, companyKey: companyKey, procedureName: "ProSLASettingsUpdate");
        }
        public Output DeleteSLASData(DeleteSLASettingsInput input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSLASettingsInput>(parameter: input, companyKey: companyKey, procedureName: "ProSLASettingsDelete");
        }
        public APIGetRecordsDynamic<ActionStatus> GetSlaModeReport(ModeInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeInput>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
    }
}