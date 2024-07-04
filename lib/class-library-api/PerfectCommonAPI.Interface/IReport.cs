using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IReport
    {
        string TransMode { get; set; }
        string ReqMode { get; set; }
        string PageIndex { get; set; }
        string PageSize { get; set; }
        string Critrea1 { get; set; }
        string Critrea2 { get; set; }
        string Critrea3 { get; set; }
        string Critrea4 { get; set; }
        string ID { get; set; }
        string Critrea5 { get; set; }
        string Critrea6 { get; set; }
        string Project { get; set; }
        string FK_Company { get; set; }
        string BankKey { get; set; }
        string Token { get; set; }
        //string XMLdata { get; set; }
        string OTP { get; set; }
       
        string MPIN { get; set; }
        string OldMPIN { get; set; }
        //string BankHeader { get; set; }
        //string ID_CustomerWiseProductDetails { get; set; }
        string MobileNumber { get; set; }
        string FK_Employee { get; set; }
        string ComplaintService { get; set; }
        string ComplaintType { get; set; }

        string Name { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        string ID_LeadFrom { get; set; }
        string ID_Category { get; set; }
        string ID_BranchType { get; set; }
        string ID_Department { get; set; }
        string SubMode { get; set; }
        string FromDate { get; set; }
        string Todate { get; set; }
        string ID_ReportSettings { get; set; }
        string ID_LeadGenerateProduct { get; set; }
        string PrductOnly { get; set; }
        string TrnsDate { get; set; }
        string ID_RiskType { get; set; }
        string CustomerNote { get; set; }
        string EmployeeNote { get; set; }
        string Id_Status { get; set; }
        string CusMensDate { get; set; }
        string LocLatitude { get; set; }
        string LocLongitude { get; set; }
        string Debug { get; set; }
        string TableCount { get; set; }
        string Category { get; set; }
        string FK_Machine { get; set; }
        byte[] LocationLandMark1 { get; set; }
        byte[] LocationLandMark2 { get; set; }
        string ID_FollowUpType { get; set; }
        string Pincode { get; set; }
        string FK_Country { get; set; }
        string FK_States { get; set; }
        string FK_District { get; set; }
        string FK_Area { get; set; }
        string FK_ToEmployee { get; set; }
        string FK_Departement { get; set; }
        string FK_Priority { get; set; }
        string FollowupDate { get; set; }
        string FK_ActionType { get; set; }
        string FK_Action { get; set; }
        string FK_Product { get; set; }
        string FK_Category { get; set; }
        string NextActionDate { get; set; }
        string ID_LeadGenerate { get; set; }
        string Remark { get; set; }
        string LeadNo { get; set; }
        string FK_MediaMaster { get; set; }
        string LgCollectedBy { get; set; }
        string ID_NotificationDetails { get; set; }
        string EntrBy { get; set; }
        string FK_BranchCodeUser { get; set; }
        string BranchCode { get; set; }
        string ID_ActionType { get; set; }
        string IsOnline { get; set; }
        string ID_LeadDocumentDetails { get; set; }
        string DocImageFormat { get; set; }
        string Status { get; set; }
        string Criteria { get; set; }
        string FK_Designation { get; set; }
        string LgCusNameTitle { get; set; }
        string CusMobileAlternate { get; set; }
        string FK_LeadByName { get; set; }
        string FK_SubMedia { get; set; }
        string LastID { get; set; }
        string FK_Reason { get; set; }
        string FK_User { get; set; }
        string ID_Branch { get; set; }
        string GroupId { get; set; }
        string ReportMode { get; set; }
        string LgFollowUpTime { get; set; }
        string LgFollowUpStatus { get; set; }
        string LgFollowupDuration { get; set; }
        string FollowUpAction { get; set; }
        string FollowUpType { get; set; }
        string LocationName { get; set; }
        string FK_Branch { get; set; }
        //string ComplaintType{ get; set; }
        
        string TicketNo { get; set; }
        string DueDaysFrom { get; set; }
        string DueDaysTo { get; set; }
        string DueCriteria { get; set; }
        string ReplacementType { get; set; }
        string FK_NextAction { get; set; }
        string ID_User { get; set; }
        string ID_TokenUser { get; set; }



    }
    #region Response Interface Model Objects
    //public class CommonAPIR
    //{
    //    public string ResponseCode { get; set; }
    //    public string ResponseMessage { get; set; }
    //}
    public class ProjectReportNameDetails : CommonAPIR
    {
        public List<ProjectReportNameDetailsList> ProjectReportNameDetailsList { get; set; }
    }
    public class ProjectReportNameDetailsList
    {
        public string ReportName { get; set; }
        public string ReportMode { get; set; }

    }
    public class ProjectReport : CommonAPIR
    {
        public List<SiteVisitList> SiteVisitList { get; set; }
        
    }
    public class SiteVisitList
    {
        public string SlNo{ get; set; }
        public string SiteVisitID { get; set; }
        public string LeadGenerationID { get; set; }
        public string LeadNo { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string CusNote { get; set; }
        public string ExpenseAmount { get; set; }
        public string Remarks { get; set; }
    }
    public class ProjectReportDetail : CommonAPIR
    {
        public List<ProjectStatusLists> ProjectStatusList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class ProjectStatusLists
    {
        public string SlNo { get; set; }
        public string ID_ProjectFollowUp { get; set; }
        public string FollowupDate { get; set; }
        public string StatusDate { get; set; }
        public string FK_Project { get; set; }
        public string Project { get; set; }
        public string FK_Stage { get; set; }
        public string Stage { get; set; }
        public string Remarks { get; set; }
        public string ID_CurrentStatus { get; set; }
        public string CurrentStatus { get; set; }
        public string hdn_EntrOn { get; set; }
        public string hdn_Groupby { get; set; }

    }
    public class ServiceNewList: CommonAPIR
    {
        public List<NewList> NewList{ get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class NewList
    {
        public string SlNo { get; set; }
        public string TicketNo{ get; set; }
        public string TicketDate { get; set; }
        public string Customer{ get; set; }
        public string Product { get; set; }
        public string Complaint { get; set; }
        public string CurrentStatus { get; set; }
        public string Description{ get; set; }
        public string Criteria { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
        public string Post { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Pincode { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
    }
    public class CategoryNameDetails : CommonAPIR
    {
        public List<CategoryNameList> CategoryNameList { get; set; }
    }
    public class CategoryNameList
    {
        public string CategoryMode { get; set; }
        public string CategoryName { get; set; }
        

    }
    public class ProjectListDetail : CommonAPIR
    {
        public List<ProjectLists> ProjectLists { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class ProjectLists
    {
        public string SlNo { get; set; }
        public string ProjectName { get; set; }
        public string Category { get; set; }
        public string LeadNumber{ get; set; }
        public string FinalAmount_R { get; set; }
        public string StartDate{ get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string Duration{ get; set; }
        public string SubCategory { get; set; }
        public string ShortName { get; set; }
        public string CreateDate { get; set; }
        public string DurationType { get; set; }

    }
    public class OutStanding : CommonAPIR
    {
        public List<OutStandingList> OutStandingList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class OutStandingList
    {
        public string SLNo { get; set; }
        public string TicketNo { get; set; }
        public string TicketDate { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public string ComplaintorService{ get; set; }
        public string Description { get; set; }
        public string Mobile { get; set; }
        public string Area { get; set; }
        public string CurrentStatus { get; set; }
        public string Criteria { get; set; }
       
        public string Address { get; set; }
        public string Place { get; set; }
        public string Post { get; set; }
     
        public string District { get; set; }
        public string Pincode { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public string Due { get; set; }

    }
    public class LeadDetailedReport : CommonAPIR
    {
        public List<DetailedList> DetailedList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class DetailedList
    {
        //public int ID { get; set; }
        public string EmployeeName{ get; set; }
        public string LeadNo { get; set; }
        public string  LeadDate{ get; set; }
        public string CategoryName { get; set; }
        public string ProductName{ get; set; }
        public string Quantity { get; set; }
        public string NextAction { get; set; }
        public string ActionType { get; set; }
        public string AssignedTo { get; set; }
        public string CustomerName{ get; set; }
        public string LeadStatusOn{ get; set; }
        public string LeadStatus{ get; set; }
        public string LeadStatusName { get; set; }
    }
    public class EmployeeWiseReport : CommonAPIR
    {
        public List<EmployeeWiseList> EmployeeWiseList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class EmployeeWiseList
    {
        //public int ID { get; set; }
        public string Employee { get; set; }
        public string Opening{ get; set; }
        public string New { get; set; }
        public string Closed { get; set; }
        public string Lost { get; set; }
        public string Balance { get; set; }
       
    }
    public class AssignedWiseReport : CommonAPIR
    {
        public List<AssignedList> AssignedList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class AssignedList
    {
        //public int ID { get; set; }
        public string AssignedTo{ get; set; }
        public string Opening { get; set; }
        public string New { get; set; }
        public string Closed { get; set; }
        public string Lost { get; set; }
        public string Balance { get; set; }

    }
    public class CategoryWiseReport : CommonAPIR
    {
        public List<CategoryWiseList> CategoryWiseList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class CategoryWiseList
    {
        //public int ID { get; set; }
        public string CategoryName { get; set; }
        public string Opening { get; set; }
        public string New { get; set; }
        public string Closed { get; set; }
        public string Lost { get; set; }
        public string Balance { get; set; }

    }
    public class ProductWiseReport : CommonAPIR
    {
        public List<ProductsList> ProductsList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class ProductsList
    {
        //public int ID { get; set; }
        public string ProductName { get; set; }
        public string Opening { get; set; }
        public string New { get; set; }
        public string Closed { get; set; }
        public string Lost { get; set; }
        public string Balance { get; set; }

    }
    public class Grouping: CommonAPIR
    {
        public List<GroupList> GroupList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class GroupList
    {
        //public int ID { get; set; }
        public int GroupMode { get; set; }
        public string GroupName { get; set; }
      

    }


    public class SummaryLead
    {
        public Int64 ID { get; set; }
        public string GroupName { get; set; }
        public string Opening { get; set; }
        public string New { get; set; }
        public string Closed { get; set; }
        public string Lost { get; set; }
        public string Balance { get; set; }

    }
    public class SummaryWiseLeadReport : CommonAPIR
    {
        public List<SummaryLead> SummaryLeadList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }

    public class ComplaintService : CommonAPIR
    {
        public List<ComplaintList> ComplaintList { get; set; }
        public List<ServiceList> ServiceList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class ComplaintList
    {
        //public int ID { get; set; }
        public string ID_ComplaintList { get; set; }
        public string CompntName { get; set; }
      

    }

  
    public class ServiceList
    {
        //public int ID { get; set; }
        public string ID_Service { get; set; }
        public string ServiceName { get; set; }


    }
    public class TicketNo : CommonAPIR
    {
        public List<TicketList> TicketList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class TicketList
    {
        //public int ID { get; set; }
        public string TicketNo { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string Priority { get; set; }
       
    }
    public class ServiceProfile : CommonAPIR
    {
        public List<TicketLedgerList> TicketLedgerList { get; set; }
        //public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class TicketLedgerList
    {
        //public int ID { get; set; }
        public string TicketNo { get; set; }
        public string TicketDate { get; set; }
        public string Branch { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string Place { get; set; }
        public string District { get; set; }
        public string Area { get; set; }
        public string Mobile { get; set; }
        public string Landmark { get; set; }
        public string OtherMobile { get; set; }
        public string Product { get; set; }
        public string Complaint { get; set; }
        public string CurrentStatus { get; set; }
        public string Post { get; set; }

    }
    public class IntimationCategory : CommonAPIR
    {
        public List<IntimationCategoryList> IntimationCategoryList { get; set; }
        
    }
    public class IntimationCategoryList
    {

        public string ID_Catg { get; set; }
        public string CatgName { get; set; }
        public string Project { get; set; }
      

    }
    public class LeadSummaryDetailsReport:CommonAPIR
    {
        public List<LeadSummaryDetailsList> LeadSummaryDetailsList { get; set; }
    }
    public class LeadSummaryDetailsList
    {
        public Int64 FK_Employee { get; set; }
        public string Employee { get; set; }
        public Int64 FK_Lead { get; set; }
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string Nextaction { get; set; }
        public string ActionType { get; set; }
        public string AssignedTo { get; set; }
        public string Customer { get; set; }
        public string LeadStatusOn { get; set; }
        public string LeadStatusName { get; set; }
    }
    
}
#endregion

