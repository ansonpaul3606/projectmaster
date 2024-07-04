using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class HomeModel
    {
        public class DashboardDetails
        {
            public List<ChartListData> ChartData { get; set; }
        }
        public class ChartListData
        {
            public long ChartID { get; set; }
            public string ChartName { get; set; }
            public string xValues { get; set; }
            public string yValues { get; set; }
            public string yColor { get; set; }
            public string ySecondValues { get; set; }
            public string ySecondColor { get; set; }
            public string yThirdValues { get; set; }
            public string yThirdColor { get; set; }
            public string XAxis { get; set; }
            public string YAxis { get; set; }
            public string ChartModalName { get; set; }
            public bool ShowYInhndreds { get; set; }
            public bool ShowY2Inhndreds { get; set; }
            public bool ShowY3Inhndreds { get; set; }
            public bool ShowXInhndreds { get; set; }
            public long DevideXAmnt { get; set; }
            public long DevideYAmnt { get; set; }
            public long DevideY2Amnt { get; set; }
            public long DevideY3Amnt { get; set; }

        }
        public class DashboardData
        {
            public int Leads { get; set; }
            public int Complaints { get; set; }
            public int Services { get; set; }
            public int Projects { get; set; }
            public int Products { get; set; }
            public decimal LeadHot { get; set; }
            public decimal LeadWarm { get; set; }          
            public decimal LeadCold { get; set; }
            public int LeadColdCount { get; set; }
            public int LeadHotCount { get; set; }
            public int LeadWarmCount { get; set; }
            public decimal LeadFollowupPendingDue { get; set; }
            public decimal LeadFollowupPendingTodays { get; set; }
            public decimal LeadFollowupPendingUpComing { get; set; }

            public int LeadFollowupPendingDueCount { get; set; }
            public int LeadFollowupPendingTodaysCount { get; set; }
            public int LeadFollowupPendingUpComingCount { get; set; }
            public decimal ProjectStart { get; set; }
            public decimal ProjectPause { get; set; }
            public decimal ProjectCompleted { get; set; }
            public decimal ProjectStartCount { get; set; }
            public decimal ProjectPauseCount { get; set; }
            public decimal ProjectCompletedCount { get; set; }
            public decimal ServiceTodaysPending { get; set; }
            public decimal ServiceOverDue { get; set; }
            public decimal ServiceUpcoming { get; set; }
            public int ServiceTodaysPendingCount { get; set; }
            public int ServiceOverDueCount { get; set; }
            public int ServiceUpcomingCount { get; set; }
            public IEnumerable<ListData> MonthList { get; set; }
            public IEnumerable<ListData> YearList { get; set; }
            public ModuleList ModuleListData { get; set; }           
        }
        public class StageData
        {
            public string Stage { get; set; }
            public Decimal Percent { get; set; }
            public long Count { get; set; }

        }
        public class DashboardListData
        {
            public string Field { get; set; }
            public decimal Value { get; set; }
            public string Module { get; set; }
            public int DashBoard { get; set; }
            public int Count { get; set; }    
            
        }
        public class GetDashboardData
        {
            public long FK_Employee { get; set; }
            public long FK_Departmnet { get; set; }
            public long FK_Branch { get; set; }
            public int FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int Mode { get; set; }
            public int Period { get; set; }
        }
        public class GetDashboardInData
        {
            public long FK_Employee { get; set; }
            public long FK_Department { get; set; }
            public long FK_Branch { get; set; }
            public int FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }     
            public DateTime AsOnDate { get; set; }
        }
        public class DashboardOutData
        {
            public long ID_DashBoardDetails { get; set; }
            public string ChartName { get; set; }
            public dynamic Datafile { get; set; }
            public long ChartList { get; set; }
            public long ChartType { get; set; }
            public string XAxis { get; set; }
            public string YAxis { get; set; }
            public string Remarks { get; set; }
        }
        public class ProjectData
        {
            public string Label { get; set; }
            public decimal Data { get; set; }
            public string Color { get; set; }
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class ListData
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GetInventoryDashboardData
        {
            public int FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long UserCode { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }
        public class InventoryDashboardListData
        {
            public string Field { get; set; }
            public decimal Value { get; set; }
            public int DashBoard { get; set; }
        }
        public class GetNotificationData
        {
            public long FK_Company { get; set; }
            public long RecievedBy { get; set; }
        }
        public class NotificationDataList
        {
            public long ID_Notification { get; set; }
            public string SendOn { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
            public string EmpImgValue { get; set; }
            public bool IsRead { get; set; }
            public string EmpFName { get; set; }
            public int TotalCount { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public bool Redirect { get; set; }

        }

        public class NotificationUpdate
        {
            public long FK_Company { get; set; }
            public long ID_Notification { get; set; }
        }

        public class NotificationDelete
        {
            public long FK_Company { get; set; }
            public long ID_Notification { get; set; }
            public string EnterBy { get; set; }
        }
        public class GetNumberGeneration
        {
            public string Submodule { get; set; }
            public DateTime TransDate { get; set; }
            public int fk_Company { get; set; }
            public int fk_Branch { get; set; }
            public int FK_Type { get; set; }
        }
        public class NumberGeneration
        {
            public string AccountNo { get; set; }
        }
        public class CreateNotificationData
        {
            public long FK_Company { get; set; }
            public long Reciever { get; set; }
            public long Sender { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
        }
        public class GetTimeLineChart
        {
            public string TransMode { get; set; }
            public long ID_Master { get; set; }
        }
        public class GetFileUploadDocs
        {
            public string TransMode { get; set; }
            public long ID_Master { get; set; }
        }
        public class FileUploadDocsList
        {
            public long SLNo { get; set; }
            public string Docs { get; set; }
            public string Description { get; set; }


        }
        public class TimeLineChartList
        {
            public long SLNo { get; set; }
            public string Title1 { get; set; }
            public string Title2 { get; set; }
            public string Description { get; set; }
            public string EntrOn { get; set; }
            public string EntrBy { get; set; }
            public string MoreData { get; set; }

        }

        public class GetLedgerInfo
        {
            public long FK_Customer { get; set; }
            public long FK_CustomerOthers { get; set; }
            public long BillNo { get; set; }
            public string TransMode { get; set; }
            public DateTime AsOnDate { get; set; }
        }
        public class LedgerInfoList
        {
            public string CustomerDetails { get; set; }
            public string BillDetails { get; set; }
            public string ProductDetailsTitle { get; set; }
            public bool ProductDetails { get; set; }
            public string LoanDetails { get; set; }
            public string TransactionDetailsTitle { get; set; }
            public bool TransactionDetails { get; set; }
            public long FK_Master { get; set; }

        }

        public class GetLedgerProductDetails
        {
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public long Mode { get; set; }
        }
        public class GetModuleBaseData
        {
            public long FK_UserGroup { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class SetModuleBaseData
        {
            public long MenuGroupID { get; set; }
            public string MenuGroupName { get; set; }
            public bool MenuGroupVisible { get; set; }
            public long SortOrder { get; set; }
            public string Module { get; set; }
        }

        public class ModuleList
        {
            public bool MASTER { get; set; }
            public bool SERVICE { get; set; }
            public bool LEAD { get; set; }
            public bool INVENTORY { get; set; }
            public bool SETTINGS { get; set; }
            public bool SECURITY { get; set; }
            public bool REPORT { get; set; }
            public bool PROJECT { get; set; }
            public bool OTHER { get; set; }
            public bool PRODUCTION { get; set; }
            public bool ACCOUNTS { get; set; }
            public bool ASSET { get; set; }
            public bool TOOL { get; set; }
            public bool VEHICLE { get; set; }
            public bool DELIVERY { get; set; }
            public bool HR { get; set; }

        }
        public class TimeLineVM
        {
            public IEnumerable<TimeLineChartList> TimeLineData { get; set; }
        }
        public class FileUploadDocs
        {
            public IEnumerable<FileUploadDocsList> FileUploadDocsData { get; set; }
        }
        public class FileUploads
        {
            public List<FileUploadDocsList> FileUploadDocList { get; set; }
        }
        public class LedgerVM
        {
            public IEnumerable<LedgerInfoList> LedgerData { get; set; }
        }
        public enum CustomColorClass
        {
            bg_primary,
            bg_secondary,
            bg_success,
            bg_info,
            bg_warning,
            bg_danger

        }
      
        public APIGetRecordsDynamic<DashboardListData> GetDashboardDataList(GetDashboardData input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashboardListData, GetDashboardData>(companyKey: companyKey, procedureName: "ProERPDashBoard", parameter: input);
        }
        public APIGetRecordsDynamic<ListData> GetModeList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ListData, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<InventoryDashboardListData> GetInventoryDashBoardList(GetInventoryDashboardData input, string companyKey)
        {
            return Common.GetDataViaProcedure<InventoryDashboardListData, GetInventoryDashboardData>(companyKey: companyKey, procedureName: "ProInventoryDashBoard", parameter: input);
        }
        public APIGetRecordsDynamic<NotificationDataList> GetNotificationDataList(GetNotificationData input, string companyKey)
        {
            return Common.GetDataViaProcedure<NotificationDataList, GetNotificationData>(companyKey: companyKey, procedureName: "ProNotificationSelect", parameter: input);
        }
        public APIGetRecordsDynamic<NotificationDataList> GetNotificationDataListWithRead(GetNotificationData input, string companyKey)
        {
            return Common.GetDataViaProcedure<NotificationDataList, GetNotificationData>(companyKey: companyKey, procedureName: "ProNotificationList", parameter: input);
        }
        public Output UpdateNotificationData(NotificationUpdate input, string companyKey)
        {
            return Common.UpdateTableData<NotificationUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProNotificationReadUpdate");
        }
        public Output NotificationDataDelete(NotificationDelete input, string companyKey)
        {
            return Common.UpdateTableData<NotificationDelete>(parameter: input, companyKey: companyKey, procedureName: "ProNotificationDelete");
        }
        public APIGetRecordsDynamic<NumberGeneration> GetAccountNo(GetNumberGeneration input, string companyKey)
        {
            return Common.GetDataViaProcedure<NumberGeneration, GetNumberGeneration>(companyKey: companyKey, procedureName: "ProLoadAccountNo", parameter: input);
        }
        public Output CreateNotification(CreateNotificationData input, string companyKey)
        {
            return Common.UpdateTableData<CreateNotificationData>(parameter: input, companyKey: companyKey, procedureName: "ProNotificationUpdate");
        }
        public APIGetRecordsDynamic<TimeLineChartList> GetTrackInfo(GetTimeLineChart input, string companyKey)
        {
            return Common.GetDataViaProcedure<TimeLineChartList, GetTimeLineChart>(companyKey: companyKey, procedureName: "ProTimeLineChartData", parameter: input);
        }

        public APIGetRecordsDynamic<LedgerInfoList> GetLedgerInfoData(GetLedgerInfo input, string companyKey)
        {
            return Common.GetDataViaProcedure<LedgerInfoList, GetLedgerInfo>(companyKey: companyKey, procedureName: "ProCustomerLedger", parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> GetLedgerProductDetailsData(GetLedgerProductDetails input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetLedgerProductDetails>(companyKey: companyKey, procedureName: "ProCustomerLedgerDetails", parameter: input);
        }
        public APIGetRecordsDynamic<FileUploadDocsList> GetFileUpload(GetFileUploadDocs input, string companyKey)
        {
            return Common.GetDataViaProcedure<FileUploadDocsList, GetFileUploadDocs>(companyKey: companyKey, procedureName: "ProTimeLineChartData", parameter: input);
        }
        public APIGetRecordsDynamic<SetModuleBaseData> GetModuleBaseDataList(GetModuleBaseData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetModuleBaseData, GetModuleBaseData>(companyKey: companyKey, procedureName: "ProMenuGroupForAppMenuSelect", parameter: input);
        }
        public APIGetRecordsDynamic<DashboardOutData> GetDashboard(GetDashboardInData input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashboardOutData, GetDashboardInData>(companyKey: companyKey, procedureName: "ProERPDashBoardDetails", parameter: input);
        }
    }
}