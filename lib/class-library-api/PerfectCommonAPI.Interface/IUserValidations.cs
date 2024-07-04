using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    
    public interface IUserValidations
    {
      

        string ReqMode { get; set; }
        string Channel{ get; set; }
        string SheduledType { get; set; }
        string DlId { get; set; }
        string Unicode { get; set; }
        byte[] Attachment{ get; set; }
        string Date { get; set; }
        string SheduledTime{ get; set; }
        string SheduledDate { get; set; }
        string FK_CommonIntimation { get; set; }
        string ID_CommonIntimation { get; set; }
        string TransDate { get; set; }
        string XMLdata { get; set; }
        string OTP { get; set; }
        string Token { get; set; }
        string MPIN { get; set; }
        string OldMPIN { get; set; }
        string BankHeader { get; set; }
        string BankKey { get; set; }
        string MobileNumber { get; set; }
        string FK_Employee { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Collectedby_ID { get; set; }
        string Area_ID { get; set; }
        string Address { get; set; }
        string ID_LeadFrom { get; set; }
        string FK_LeadThrough { get; set; }
        string ID_Category { get; set; }
        string ID_BranchType { get; set; }
        string ID_Department { get; set; }
        string SubMode { get; set; }
        string FromDate { get; set; }
        string Todate { get; set; }
        string ToDate { get; set; }
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
        string SearchBy { get; set; }
        string LeadDetails { get; set; }
        string GridData { get; set; }
        string SearchBydetails { get; set; }
        string FollowupDate { get; set; }
        string FK_ActionType { get; set; }
        string FK_Action { get; set; }
        string FK_Product { get; set; }
        string FK_Category { get; set; }
        string ProdType { get; set; }
        string NextActionDate { get; set; }
        string ID_LeadGenerate { get; set; }
        string Remark { get; set; }
        
        string ID_ActionType { get; set; }
        string IsOnline { get; set; }

        string LocationName { get; set; }

        string UserAction { get; set; }
        string LgLeadDate { get; set; }
        string LgCollectedBy { get; set; }
        string FK_LeadFrom { get; set; }
        string FK_LeadBy { get; set; }
        string FK_Customer { get; set; }
        string FK_CustomerOthers { get; set; }
        string LgCusName { get; set; }
        string LgCusAddress { get; set; }
        string LgCusAddress2 { get; set; }
        string LgCusMobile { get; set; }
        string LgCusEmail { get; set; }
        string CusCompany { get; set; }
        string CusPhone { get; set; }
        string FK_MediaMaster { get; set; }
        string FK_State { get; set; }
        string FK_Post { get; set; }
        string ID_Product { get; set; }
        string ProdName { get; set; }
        string ProjectName { get; set; }
        string LgpPQuantity { get; set; }
        string LgpDescription { get; set; }
        string ActStatus { get; set; }
        string FK_NetAction { get; set; }
        string BranchID { get; set; }
        string BranchTypeID { get; set; }
        string AssignEmp { get; set; }
        string LocationAddress { get; set; }
        string selectProcedureName { get; set; }
        string FK_Company { get; set; }
        string FK_BranchCodeUser { get; set; }
        string SubProductDetails { get; set; }
        Int64 PreviousID { get; set; }
        string ID_Branch { get; set; }
        string GroupId { get; set; }
        string ReportMode { get; set; }
        string ID_NotificationDetails { get; set; }
        string EntrBy { get; set; }
        string CusPerson { get; set; }
        string LgActMode { get; set; }
        string ID_FollowUpBy { get; set; }
        byte[] DocumentImage { get; set; }
        string Doc_Subject { get; set; }
        string Doc_Description { get; set; }
        string ID_LeadDocumentDetails { get; set; }
        string DocImageFormat { get; set; }
        string TransMode { get; set; }
        string LgCusNameTitle { get; set; }
        string CusMobileAlternate { get; set; }
        string FK_LeadByName { get; set; }
        string FK_SubMedia { get; set; }
        string LastID { get; set; }
        string FK_Reason { get; set; }
        string FK_User { get; set; }
        string LgFollowUpTime { get; set; }
        string LgFollowUpStatus { get; set; }
        string LgFollowupDuration { get; set; }        
        string FollowUpAction { get; set; }
        string FollowUpType { get; set; }
        string LeadNo { get; set; }
        string Criteria { get; set; }
        string Status { get; set; }
        string FK_CollectedBy { get; set; }
        string Category { get; set; }
        string BranchCode { get; set; }
        string ID_TodoListLeadDetails { get; set; }

        string ID_CustomerServiceRegister { get; set; }
        string CSRChannelID { get; set; }
        string CSRPriority { get; set; }
        string CSRCurrentStatus { get; set; }
        string FK_Branch { get; set; }
        string CSRPCategory { get; set; }
        string FK_OtherCompany { get; set; }
        string FK_ComplaintList { get; set; }
        string FK_ServiceList { get; set; }
      
        string EnteredDate { get; set; }


        string CSRChannelSubID { get; set; }
        string AttendedBy { get; set; }
        string CSRTickno { get; set; }
        string CusName { get; set; }
        string CusMobile { get; set; }
        string CusAddress { get; set; }
        string CSRContactNo { get; set; }
        string CSRLandmark { get; set; }
        string CSRServiceFromDate { get; set; }
        string CSRServiceToDate { get; set; }
        string CSRServicefromtime { get; set; }
        string CSRServicetotime { get; set; }
        string CSRODescription { get; set; }
        string TicketDate { get; set; }  
        string FK_ComplaintType { get; set; }
        string SortOrder { get; set; }
        string DueDays { get; set; }
        string LgpExpectDate { get; set; }
        string FK_Customerserviceregister { get; set; }
        string Visitdate { get; set; }
        //string LastID { get; set; }
        string Visittime { get; set; }
        string Assignees { get; set; }
        string EmployeeType { get; set; }
        string FK_AttendedBy { get; set; }
        string TicketTime { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        string CaAssignedDate { get; set; }
        string CaDescription { get; set; }
        string ID_CustomerAssignment { get; set; }
        string ID_CustomerWiseProductDetails { get; set; }        
         string CustomerNotes { get; set; }        
         string StartingDat { get; set; }
         string ServiceAmount { get; set; }
         string ProductAmount { get; set; }
         string NetAmount { get; set; }
         string TotalAmount { get; set; }
         string ReplaceAmount { get; set; }
         string DiscountAmount { get; set; }        
         string FK_NextAction { get; set; }
       
        string FK_BillType { get; set; }
        string FK_NextActionLead { get; set; }
        string FK_ActionTypeLead { get; set; }
        string FK_EmployeeLead { get; set; }
        string NextActionDateLead { get; set; }
        string StartingDate { get; set; }
        string LocationEnteredDate { get; set; }
        string LocationEnteredTime { get; set; }
        string ID_ImageLocation { get; set; }
        string FK_Master { get; set; }
        string ID_EmployeeAttanceMarking { get; set; }
       string ID_EmployeeLocation { get; set; }
        string ChargePercentage { get; set; }
        string Reciever { get; set; }
        string Title { get; set; }
        string Message { get; set; }
        string ID_User { get; set; }
        string FK_Designation { get; set; }
        string ToEmail { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        string Offer { get; set; }       
       string ForAllProduct { get; set; }
        string FK_UserGroup { get; set; }
        string Module { get; set; }
        string AuthID { get; set; }
        string Reason { get; set; }
      
        string PageIndex { get; set; }
        string PageSize { get; set; }
        string Critrea1 { get; set; }
        string Critrea2 { get; set; }
        string Critrea3 { get; set; }
        string Critrea4 { get; set; }
        string ID { get; set; }
        string Critrea5 { get; set; }
        string Critrea6 { get; set; }
        string SkipPrev { get; set; }
        string FK_AuthorizationData { get; set; }
        string VoiceData { get; set; }
        string VoiceDataName { get; set; }
        string FK_CustomerserviceregisterProductDetails { get; set; }
        string OsType { get; set; }
        string versionNo { get; set; }
        string Branch{ get; set; }
        string ConfigCode { get; set; }
        string URLKey { get; set; }
         string FK_SubCategory { get; set; }
         string FK_Brand { get; set; }
        string ID_Master { get; set; }
        string LandNumber { get; set; }
        string IdFliter { get; set; }
        string ID_TokenUser { get; set; }

    }
    #region Response Interface Model Objects
    public class CommonAPIR
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class ProdProjDTL
    {
        public string ID_Product { get; set; }
        public string FK_Category { get; set; }
        public string ProdName { get; set; }
        //   public long ProjectName { get; set; }
        public string ProjectName { get; set; }
        public string AssignEmp { get; set; }
        public string LgpPQuantity { get; set; }
        public string LgpDescription { get; set; }
        public string ActStatus { get; set; }
        public string FK_NetAction { get; set; }
        public string BranchID { get; set; }
        public string BranchTypeID { get; set; }
        public string FK_ActionType { get; set; }
        public string NextActionDate { get; set; }
        public string FK_Departement { get; set; }
        public string FK_Employee { get; set; }
        public string FK_Priority { get; set; }
        public string EntrBy { get; set; }
        public string LgpExpectDate { get; set; }
        public string LgpMRP { get; set; }
        public string LgpSalesPrice { get; set; }
        public string FK_ProductLocation { get; set; }
        public string LastID { get; set; }
      
    }
    public class ResellerDetails : CommonAPIR
    {
        public string ResellerName { get; set; }
        public string AppIconImageCode { get; set; }
        public string TechnologyPartnerImage { get; set; }
        public string ProductName { get; set; }
        public string PlayStoreLink { get; set; }
        public string AppStoreLink { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactAddress { get; set; }
        public string CertificateName { get; set; }
        public string TestingURL { get; set; }
        public string TestingMachineId { get; set; }
        public string TestingImageURL { get; set; }
        public string TestingMobileNo { get; set; }
        public string TestingBankKey { get; set; }
        public string TestingBankHeader { get; set; }
        public string AboutUs { get; set; }
        public Boolean AudioClipEnabled { get; set; }
        public Boolean IsLocationDistanceShowing { get; set; }
        public Boolean EditMRPLead { get; set; }
      

    }
    public class PageDetails
    {
        public byte PSValue { get; set; }
        public string PSLabelName { get; set; }
        public Int32 PSField { get; set; }
    }
    public class UserLoginDetails : CommonAPIR
    {
        public Int64 FK_Employee { get; set; }
       // public Int64 LoggedFK_Employee { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserCode { get; set; }
        public Int64 FK_Branch { get; set; }
        public string BranchName { get; set; }

        public Int64 FK_BranchType { get; set; }
        public Int64 FK_Company { get; set; }
        public Int64 FK_BranchCodeUser { get; set; }
        public Int64 FK_UserRole { get; set; }
        public string UserRole { get; set; }
        public int IsAdmin { get; set; }
        public int IsManager { get; set; }
        public Int64 ID_User { get; set; }
        public Int64 FK_Department { get; set; }
        public string Department { get; set; }
        public Int64 CompanyCategory { get; set; }
        public string LocLongitude { get; set; }
        public string LocLattitude { get; set; }
        public string LocLocationName { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTime { get; set; }
        public int Status { get; set; }
        public Int16 PSValue { get; set; }
        public ModuleList ModuleList { get; set; }
        public UtilityList UtilityList { get; set; }
        public List<PageDetails> CRMDetails { get; set; }
        public List<PageDetails> FollowUpDetails { get; set; }
        public Int64 ID_TokenUser { get; set; }

    }
    public class ModuleList
    {

        public bool WATSAPP { get; set; }
        public bool EMAIL { get; set; }
        public bool LEAD { get; set; }
        public bool SERVICE { get; set; }
        public bool COLLECTION { get; set; }
        public bool PROJECT { get; set; }
        public bool DELIVERY { get; set; }


    }
    //public class ModuleList
    //{
    //    public bool MASTER { get; set; }
    //    public bool SERVICE { get; set; }
    //    public bool LEAD { get; set; }
    //    public bool INVENTORY { get; set; }
    //    public bool SETTINGS { get; set; }
    //    public bool SECURITY { get; set; }
    //    public bool REPORT { get; set; }
    //    public bool PROJECT { get; set; }
    //    public bool OTHER { get; set; }
    //    public bool PRODUCTION { get; set; }
    //    public bool ACCOUNTS { get; set; }
    //    public bool ASSET { get; set; }
    //    public bool TOOL { get; set; }
    //    public bool VEHICLE { get; set; }
    //    public bool DELIVERY { get; set; }
    //    public bool HR { get; set; }

    //}
    public class UtilityList
    {
        public bool ATTANCE_MARKING { get; set; }
        public bool LOCATION_TRACKING { get; set; }
        public int LOCATION_INTERVAL { get; set; }
        public bool LOCATION_ROOTE { get; set; }
    }
    public class MPINDetails : CommonAPIR
    {
        public string MPIN { get; set; }
    }
    public class CustomerDetailsList : CommonAPIR
    {
        public List<CustomerDetails> CustomerDetails { get; set; }
    }
    public class CustomerDetails
    {
        public Int64 ID_Customer { get; set; }
        public string CusNameTitle { get; set; }
        public string CusName { get; set; }
        public string CusAddress1 { get; set; }
        public string CusAddress2 { get; set; }
        public string CusEmail { get; set; }
        public string CusPhnNo { get; set; }
        public string Company { get; set; }
        public Int64 CountryID { get; set; }
        public string CntryName { get; set; }
        public Int64 StatesID { get; set; }
        public string StName { get; set; }
        public Int64 DistrictID { get; set; }
        public string DtName { get; set; }
        public Int64 PostID { get; set; }
        public string PostName { get; set; }
        public Int64 FK_Area { get; set; }
        public string Area { get; set; }        
        public string CusMobileAlternate { get; set; }       
        public string Pincode { get; set; }
        public Int64 Customer_Type { get; set; }
        public string LandNumber { get; set; }
    }
    public class LeadFromDetailsList : CommonAPIR
    {
        public List<LeadFromDetails> LeadFromDetails { get; set; }
    }
    public class LeadFromDetails
    {
        public int ID_LeadFrom { get; set; }
        public string LeadFromName { get; set; }
        public int LeadFromType { get; set; }

    }
   
    public class LeadThroughDetailsList : CommonAPIR
    {
        public List<LeadThroughDetails> LeadThroughDetails { get; set; }
    }
    public class LeadThroughDetails
    {
        public int ID_LeadThrough { get; set; }
        public string LeadThroughName { get; set; }
        public int HasSub { get; set; }

    }
    public class AddNewCustomer : CommonAPIR
    {
        public Int64 ID_Customer { get; set; }
    }
    public class MaintenanceMessage : CommonAPIR
    {
        public string Description { get; set; }
        public byte Type { get; set; }
    }
    public class BannerDetails : CommonAPIR
    {
        public List<BannerDetailsList> BannerDetailsList { get; set; }
    }
    public class BannerDetailsList
    {
        public Int64 ID_Banner { get; set; }
        public string ImagePath { get; set; }
    }
    public class CollectedByUsersList : CommonAPIR
    {
        public List<CollectedByUsers> CollectedByUsers { get; set; }
    }
    public class CollectedByUsers
    {
        public Int64 ID_CollectedBy { get; set; }
        public string Name { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
    }
    public class CategoryDetailsList : CommonAPIR
    {
        public List<CategoryList> CategoryList { get; set; }
    }
    public class CategoryList
    {
        public Int64 ID_Category { get; set; }
        public string CategoryName { get; set; }
        public int Project { get; set; }
    }
    public class ProductDetailsList : CommonAPIR
    {
        public List<ProductList> ProductList { get; set; }
    }
    public class ProductList
    {
        public Int64 ID_Product { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string MRP { get; set; }
        public string SalPrice { get; set; }
        public string FK_Category { get; set; }
        public string ProdBarcode { get; set; }

    }
    public class StatusDetailsList : CommonAPIR
    {
        public List<StatusList> StatusList { get; set; }
    }
    public class StatusList
    {
        public Int64 ID_Status { get; set; }
        public string StatusName { get; set; }
        public int IsEnable { get; set; }
    }
    public class PriorityDetailsList : CommonAPIR
    {
        public List<PriorityList> PriorityList { get; set; }
    }
    public class PriorityList
    {
        public Int64 ID_Priority { get; set; }
        public string PriorityName { get; set; }
    }
    public class FollowUpActionDetails : CommonAPIR
    {
        public List<FollowUpActionDetailsList> FollowUpActionDetailsList { get; set; }
    }
    public class FollowUpActionDetailsList
    {
        public Int64 ID_NextAction { get; set; }
        public string NxtActnName { get; set; }
        public int Status { get; set; }
    }
    public class FollowUpTypeDetails : CommonAPIR
    {
        public List<FollowUpTypeDetailsList> FollowUpTypeDetailsList { get; set; }
    }
    public class FollowUpTypeDetailsList
    {
        public Int64 ID_ActionType { get; set; }
        public string ActnTypeName { get; set; }
        public Int64 ActionMode { get; set; }
    }
    public class MediaTypeDetails : CommonAPIR
    {
        public List<MediaTypeDetailsList> MediaTypeDetailsList { get; set; }
    }
    public class MediaTypeDetailsList
    {
        public Int64 ID_MediaMaster { get; set; }
        public string MdaName { get; set; }
        public Int64 HasSubMedia { get; set; }
    }
    public class SubMediaTypeDetails : CommonAPIR
    {
        public List<SubMediaTypeDetailsList> SubMediaTypeDetailsList { get; set; }
    }
    public class SubMediaTypeDetailsList
    {
        public Int64 ID_MediaSubMaster { get; set; }
        public string SubMdaName { get; set; }       
    }
    public class ReasonDetails : CommonAPIR
    {
        public List<ReasonDetailsList> ReasonDetailsList { get; set; }
    }
    public class ReasonDetailsList
    {
        public Int64 ID_Reason { get; set; }
        public string ResnName { get; set; }
    }
    public class BranchTypeDetails : CommonAPIR
    {
        public List<BranchTypeDetailsList> BranchTypeDetailsList { get; set; }
    }
    public class BranchTypeDetailsList
    {
        public Int64 ID_BranchType { get; set; }
        public string BranchTypeName { get; set; }
    }
    public class BranchDetails : CommonAPIR
    {
        public List<BranchDetailsList> BranchDetailsList { get; set; }
    }
    public class BranchDetailsList
    {
        public Int64 ID_Branch { get; set; }
        public string BranchName { get; set; }
    }
    public class DepartmentDetails : CommonAPIR
    {
        public List<DepartmentDetailsList> DepartmentDetailsList { get; set; }
    }
    public class DepartmentDetailsList
    {
        public Int64 ID_Department { get; set; }
        public string DeptName { get; set; }
    }
    public class EmployeeDetails : CommonAPIR
    {
        public List<EmployeeDetailsList> EmployeeDetailsList { get; set; }
    }
    public class EmployeeDetailsList
    {
        public Int64 ID_Employee { get; set; }
        public Int64 FK_Branch { get; set; }
        public string EmpName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string Branch { get; set; }
    }
    public class EmployeeAllDetails : CommonAPIR
    {
        public List<EmployeeAllDetailsList> EmployeeAllDetailsList { get; set; }

    }
    public class EmployeeAllDetailsList
    {
        public Int64 ID_Employee { get; set; }
        public string EmpName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string Branch { get; set; }
    }
    public class LeadManagementDetailsList : CommonAPIR
    {
        public List<LeadManagementDetails> LeadManagementDetails { get; set; }
    }
    public class LeadManagementDetails
    {
        public int SlNo { get; set; }
        public Int64 ID_LeadGenerate { get; set; }
        public Int64 ID_LeadGenerateProduct { get; set; }
        public string LgLeadDate { get; set; }
        public string LgCusName { get; set; }
        public string LgCusMobile { get; set; }
        public string LgCollectedBy { get; set; }
        public string NextActionDate { get; set; }
        public int DueDays { get; set; }
        public string ProdName { get; set; }
        public string ProjectName { get; set; }
        public string LgpDescription { get; set; }
        public string FK_Employee { get; set; }
        public string LeadNo { get; set; }
        public string Preference { get; set; }
        public string AssignedTo { get; set; }
        public string CusAddress { get; set; }
        public Int64 ID_NextAction { get; set; }
        public string Action { get; set; }
        public string ID_Users { get; set; }
        public string Email { get; set; }

    }
    public class RoportSettingsList : CommonAPIR
    {
        public List<RoportSettings> RoportSettings { get; set; }
    }
    public class RoportSettings
    {
        public Int64 ID_ReportSettings { get; set; }
        public string SettingsName { get; set; }
    }
    public class GeneralReport : CommonAPIR
    {
        public List<Dictionary<string, object>> Data { get; set; }
    }
    public class DataDetails
    {
        public Dictionary<string, object> datadet { get; set; }

    }
    public class LeadHistoryDetails : CommonAPIR
    {
        public List<LeadHistoryDetailsList> LeadHistoryDetailsList { get; set; }
    }
    public class LeadHistoryDetailsList
    {
        public string Product { get; set; }
        public string EnquiryAbout { get; set; }
        public string LgpDescription { get; set; }
        public string Action { get; set; }
        public string ActionDate { get; set; }
        public string Status { get; set; }
        public string CustomerReamrks { get; set; }
        public string Agentremarks { get; set; }
        public string FollowedBy { get; set; }
    }
    public class LeadInfoDetails : CommonAPIR
    {
        public List<LeadInfoDetailsList> LeadInfoDetailsList { get; set; }
    }
    public class LeadInfoDetailsList
    {       
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string LeadSource { get; set; }
        public string LeadFrom { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public string Action { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string CollectedBy { get; set; }
        public string AssignedTo { get; set; }
        public string TargetDate { get; set; }
        public string ExpectDate { get; set; }
        public string ID_Users { get; set; }
        public string LgpMRP { get; set; }
        public string LgpSalesPrice { get; set; }
    }
    public class LeadImageDetails : CommonAPIR
    {
        public byte[] LocationLandMark1 { get; set; }
        public byte[] LocationLandMark2 { get; set; }
        public string LocationLatitude { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationName { get; set; }
    }
    public class NotificationDetails : CommonAPIR
    {
        public List<NotificationInfo> NotificationInfo { get; set; }
        public Int32 count { get; set; }
    }
    public class NotificationInfo
    {

        public Int64 ID_Notification { get; set; }
        public string SendOn { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string EmpImgValue { get; set; }
        public int IsRead { get; set; }
        public string EmpFName { get; set; }
      
    
    }
    public class UpdateLeadGenerateAction : CommonAPIR
    {
        public Int64 FK_LeadGenerateAction { get; set; }
    }
    public class PincodeDetails : CommonAPIR
    {
        public List<PincodeDetailsList> PincodeDetailsList { get; set; }
    }

    public class PincodeDetailsList 
    {

        public string Post { get; set; }
        public Int64 FK_Post { get; set; }
        public string Place { get; set; }
        public Int64 FK_Place { get; set; }
        public string Area { get; set; }
        public Int64 FK_Area { get; set; }
        public string District { get; set; }
        public Int64 FK_District { get; set; }
        public string States { get; set; }
        public Int64 FK_States { get; set; }
        public string Country { get; set; }
        public Int64 FK_Country { get; set; }
    }
    public class CountryDetails : CommonAPIR
    {
        public List<CountryDetailsList> CountryDetailsList { get; set; }
    }
    public class CountryDetailsList
    {
        public Int64 FK_Country { get; set; }
        public string Country { get; set; }


    }
    public class StatesDetails : CommonAPIR
    {
        public List<StatesDetailsList> StatesDetailsList { get; set; }
    }
    public class StatesDetailsList
    {
        public Int64 FK_States { get; set; }
        public string States { get; set; }

    }
    public class DistrictDetails : CommonAPIR
    {
        public List<DistrictDetailsList> DistrictDetailsList { get; set; }
    }
    public class DistrictDetailsList
    {

        public Int64 FK_District { get; set; }
        public string District { get; set; }

    }
    public class AreaDetails : CommonAPIR
    {
        public List<AreaDetailsList> AreaDetailsList { get; set; }
    }
    public class AreaDetailsList
    {

        public Int64 FK_Area { get; set; }
        public string Area { get; set; }

    }
    public class PostDetails : CommonAPIR
    {
        public List<PostDetailsList> PostDetailsList { get; set; }
    }
    public class PostDetailsList
    {

        public Int64 FK_Post { get; set; }
        public string PostName { get; set; }
        public string PinCode { get; set; }

    }
    public class AddAgentCustomerRemarks : CommonAPIR
    {
        public string AgentNote { get; set; }
        public string CustomerNote { get; set; }
    }
    public class UpdateLeadGeneration : CommonAPIR
    {
        public Int64 FK_LeadGenerate { get; set; }
        public string LeadNumber { get; set; }
    }
    public class LeadGenerationDetails : CommonAPIR
    {
        public List<LeadGenerationDetailsList> LeadGenerationDetailsList { get; set; }
    }
    public class LeadGenerationDetailsList
    {
        public Int64 LeadGenerateID { get; set; }
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string LeadFrom { get; set; }
        public string LeadBy { get; set; }
        public Int64 ID_MediaMaster { get; set; }
        public Int64 SubMedia { get; set; }
        public string PinCode { get; set; }
        public Int64 CountryID { get; set; }
        public Int64 StatesID { get; set; }
        public Int64 DistrictID { get; set; }
        public Int64 PostID { get; set; }
        public string Company { get; set; }
        public string CntryName { get; set; }
        public string StName { get; set; }
        public string DtName { get; set; }
        public string PostName { get; set; }
        public string CusPhnNo { get; set; }
        public string LeadByName { get; set; }
        public Int64 ID_Customer { get; set; }
        public Int64 FK_CustomerOthers { get; set; }
        public string CollectedBy { get; set; }
        public string CollectedByName { get; set; }     
        public string CusName { get; set; }
        public string CusMobile { get; set; }
        public string CusEmail { get; set; }
        public string CusAddress { get; set; }
        public string CusAddress2 { get; set; }
        public string CusNameTitle { get; set; }
        public string CusMobileAlternate { get; set; }
        public string Area { get; set; }
        public Int64 AreaID { get; set; }
       

    }
    public class LeadGenerationListDetails : CommonAPIR
    {      
        public Int64 LeadGenerateID { get; set; }
        public string LeadNo { get; set; }
        public string LeadID { get; set; }
        public string LeadDate { get; set; }
        public string LeadFrom { get; set; }
        public string LeadBy { get; set; }
        public Int64 ID_MediaMaster { get; set; }
        public Int64 SubMedia { get; set; }
        public string PinCode { get; set; }
        public Int64 CountryID { get; set; }
        public Int64 StatesID { get; set; }
        public Int64 DistrictID { get; set; }
        public Int64 PostID { get; set; }
        public string Company { get; set; }
        public string CntryName { get; set; }
        public string StName { get; set; }
        public string DtName { get; set; }
        public string PostName { get; set; }
        public string CusPhnNo { get; set; }
        public string LeadByName { get; set; }
        public Int64 ID_Customer { get; set; }
        public Int64 FK_CustomerOthers { get; set; }
        public Int64 CollectedBy { get; set; }
        public string CollectedByName { get; set; }
        public string CusName { get; set; }
        public string CusMobile { get; set; }
        public string CusEmail { get; set; }
        public string CusAddress { get; set; }
        public string CusAddress2 { get; set; }
        public string CusNameTitle { get; set; }
        public string CusMobileAlternate { get; set; }
        public string Area { get; set; }
        public Int64 AreaID { get; set; }
        //public string ResponseCode { get; set; }
        //public string ResponseMessage { get; set; }
       

    }

    public class LeadsDashBoardDetails : CommonAPIR
    {      
        public decimal TotalCount { get; set; }
        public decimal TotalVAl { get; set; }
        public List<LeadsDashBoardDetailsList> LeadsDashBoardDetailsList { get; set; }

    }
    public class LeadsDashBoardDetailsList
    {
        public string Fileds { get; set; }
        public string Count { get; set; }
        public string Value { get; set; }
    }
    public class AddQuatation : CommonAPIR
    {
        public Int64 ID_LeadGenerateProduct { get; set; }
    }

    public class DateWiseExpenseDetails : CommonAPIR
    {
        public string Total { get; set; }
        public List<DateWiseExpenseDetailsList> DateWiseExpenseDetailsList { get; set; }
    }
    public class DateWiseExpenseDetailsList
    {
        public string TrnsDate { get; set; }
        public Int64 ID_Expense { get; set; }
        public string ExpenseName { get; set; }
        public string Amount { get; set; }
    }

    public class ExpenseType : CommonAPIR
    {
        public List<ExpenseTypeList> ExpenseTypeList { get; set; }
    }
    public class ExpenseTypeList
    {
        public Int64 ID_Expense { get; set; }
        public string ExpenseName { get; set; }
    }
    public class UpdateExpenseDetails : CommonAPIR
    {
        public Int64 ID_Expense { get; set; }
    }
    public class ActionType : CommonAPIR
    {
        public List<ActionTypeList> ActionTypeList { get; set; }
    }
    public class ActionTypeList
    {
        public Int64 ID_ActionType { get; set; }
        public string ActionTypeName { get; set; }
    }
    public class PendingCountDetails : CommonAPIR
    {

        public Int32 Todolist { get; set; }
        public Int32 OverDue { get; set; }
        public Int32 UpComingWorks { get; set; }
        public Int32 MyLeads { get; set; }
        public Int32 LeadRequest { get; set; }

    }
    public class AgendaDetails : CommonAPIR
    {
        public List<AgendaDetailsList> AgendaDetailsList { get; set; }
    }
    public class AgendaDetailsList
    {
        public Int64 ID_LeadGenerate { get; set; }
        public Int64 ID_LeadGenerateProduct { get; set; }
        public Int64 ID_ActionType { get; set; }
        public string ActionTypeName { get; set; }
        public string TrnsDate { get; set; }
        public string EnquiryAbout { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string FollowedBy { get; set; }
        public string CustomerRemark { get; set; }
        public string EmployeeRemark { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public Int32 SubMode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationName { get; set; }
        public Int64 FK_Priority { get; set; }
        public string PriorityName { get; set; }
    }
    public class EmployeeProfileDetails : CommonAPIR
    {
        public Int64 ID_Employee { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string EmpDOB { get; set; }
        public string LoginDate { get; set; }
        public string LoginTime { get; set; }
        public Int32 LoginMode { get; set; }
        public string LoginStatus { get; set; }
    }
    public class UpdateUserLoginStatus : CommonAPIR
    {
        public string LocLatitude { get; set; }
        public string LocLongitude { get; set; }
        public string LocationName { get; set; }
        public Int64 FK_Employee { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LoginDate { get; set; }
        public string LoginTime { get; set; }
        public Int32 LoginMode { get; set; }
        public string LoginStatus { get; set; }
        public string DutyStatus { get; set; }
    }
    public class ActivitiesDetails : CommonAPIR
    {
        public List<ActivitiesDetailsList> ActivitiesDetailsList { get; set; }
    }
    public class ActivitiesDetailsList
    {
        public string ID_LeadGenerateProduct { get; set; }
        public string Product { get; set; }
        public string EnquiryAbout { get; set; }
        public string LgpDescription { get; set; }
        public string Action { get; set; }
        public string ActionDate { get; set; }
        public string Status { get; set; }
        public string Agentremarks { get; set; }
        public string FollowedBy { get; set; }
        public string ActionType { get; set; }
       
    }
    public class UpdateLeadGenerate
    {
        public byte UserAction { get; set; }
        public Int16 Debug { get; set; }
        public string TransMode { get; set; }
        public long ID_LeadGenerate { get; set; }
        //public string LgLeadNo { get; set; }
        public DateTime LgLeadDate { get; set; }
        public long FK_Customer { get; set; }
        public long FK_CustomerOthers { get; set; }
        public string LgCusName { get; set; }
        public string LgCusAddress { get; set; }
        public string LgCusMobile { get; set; }
        public string LgCusEmail { get; set; }
        public string CusCompany { get; set; }
        public string CusPhone { get; set; }
        public long FK_Country { get; set; }
        public long FK_State { get; set; }
        public long FK_District { get; set; }
        public long FK_Post { get; set; }

        public long FK_LeadFrom { get; set; }

        public long FK_LeadBy { get; set; }

        public long FK_MediaMaster { get; set; }

        public long LgCollectedBy { get; set; }
        public long FK_Company { get; set; }
        public long FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public long FK_Machine { get; set; }
        public long PreviousID { get; set; }
        public string SubProductDetails { get; set; }
        public long FK_LeadGenerate { get; set; }
    }
    public class LeadGenerateReport : CommonAPIR
    {
        public List<LeadGenerateReportList> LeadGenerateReportList { get; set; }
    }
    public class LeadGenerateReportList
    {
        public string LeadNo { get; set; }
        public string ProjectName { get; set; }
        public string NextActionDate { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string ProductName { get; set; }
        public string Department { get; set; }
        public string LeadBY { get; set; }
        public string ActionModuleName { get; set; }
        public string ActionStatusname { get; set; }
        public string PriorityType { get; set; }
        public string Branch { get; set; }
        public string Company { get; set; }
    }
    public class ProductWiseLeadReport : CommonAPIR
    {
        public List<ProductWiseLeadReportList> ProductWiseLeadReportList { get; set; }
    }
    public class ProductWiseLeadReportList
    {
        public string ProductName { get; set; }
        public string NextActionDate { get; set; }
        public string LeadBy { get; set; }
        public string LeadNo { get; set; }
    }
    public class PriorityWiseLeadReport : CommonAPIR
    {
        public List<PriorityWiseLeadReportList> PriorityWiseLeadReportList { get; set; }
    }
    public class PriorityWiseLeadReportList
    {
        public string PriorityType { get; set; }
        public string LeadNo { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string ActionStatusName { get; set; }
    }
    public class ReportNameDetails : CommonAPIR
    {
        public List<ReportNameDetailsList> ReportNameDetailsList { get; set; }
    }
    public class ReportNameDetailsList
    {
        public Int64 ReportMode { get; set; }
        public string ReportName { get; set; }

    }
    public class GroupingDetails : CommonAPIR
    {
        public List<GroupingDetailsList> GroupingDetailsList { get; set; }
    }
    public class GroupingDetailsList
    {
        public Int64 GroupId { get; set; }
        public string GroupName { get; set; }

    }
    public class ActionListDetailsReport : CommonAPIR
    {
        public List<ActionList> ActionList { get; set; }
    }
    public class ActionList
    {
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string Customer { get; set; }
        public string ProductORProject { get; set; }
        public string Action { get; set; }
        public string ActionType { get; set; }
        public string FollowupDt { get; set; }
        public string Assignee { get; set; }
        public Int32 DueDays { get; set; }
        public string Remarks { get; set; }
    }
    public class StatusListDetailsReport : CommonAPIR
    {
        public List<StatusListDetails> StatusListDetails { get; set; }
    }
    public class StatusListDetails
    {
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string Customer { get; set; }
        public string ProductORProject { get; set; }
        public string Assignee { get; set; }
        public string CompletedDate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
    public class NewListDetailsReport : CommonAPIR
    {
        public List<NewListDetails> NewListDetails { get; set; }
    }
    public class NewListDetails
    {
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string Customer { get; set; }
        public string PhnNo { get; set; }
        public string Product { get; set; }
        public string CollectedBy { get; set; }
        public string Priority { get; set; }
        public string CurrentStatus { get; set; }
        public string Criteria { get; set; }
        public string Branch { get; set; }
        public string Remarks { get; set; }
        public string LgCollectedBy { get; set; }
        public string Area { get; set; }
        public string LeadFrom { get; set; }
        public string LeadByName { get; set; }
        public string AssignedTo { get; set; }
        public string Mobile { get; set; }
        public string MRP { get; set; }
        public string Address { get; set; }

    }
    public class FollowUpListDetailsReport : CommonAPIR
    {
        public List<FollowUpListDetails> FollowUpListDetails { get; set; }
    }
    public class FollowUpListDetails
    {
        public string ID_LeadGenerateProduct { get; set; }
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string Customer { get; set; }
        public string PhnNo { get; set; }
        public string Product { get; set; }
        public string NextAction { get; set; }
        public string NextActionDate { get; set; }
        public string FollowUpMethod { get; set; }
        public string AssignedTo { get; set; }
        public string CompletedDate { get; set; }
        public string DueDays { get; set; }
        public string Status { get; set; }
        public string BRANCH { get; set; }
        public string PriorityID { get; set; }
        public string Priority { get; set; }
        public string criteria { get; set; }
        public string Remarks { get; set; }
        public string LgCollectedBy { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
       

    }
    public class LeadGenerationDefaultvalueSettings : CommonAPIR
    {
        public string ID_Employee { get; set; }
        public string EmpFName { get; set; }
        public string ID_BranchType { get; set; }
        public string BranchType { get; set; }
        public string ID_Branch { get; set; }
        public string Branch { get; set; }
        public string FK_Department { get; set; }
        public string Department { get; set; }
        public string FK_Country { get; set; }
        public string Country { get; set; }
        public string FK_States { get; set; }
        public string StateName { get; set; }
        public string FK_District { get; set; }
        public string DistrictName { get; set; }      
    }
    public class DeleteLeadGenerate : CommonAPIR
    {
        public string Message { get; set; }
    }
    public class AgendaType : CommonAPIR
    {
        public Int32 Count { get; set; }
        public List<AgendaTypeList> AgendaTypeList { get; set; }
    }
    public class AgendaTypeList
    {
        public Int32 Id_Agenda { get; set; }
        public string AgendaName { get; set; }
        public string ImageCode { get; set; }
        public Int32 Count { get; set; }
    }
    public class UpdateLeadManagement : CommonAPIR
    {
        public string LeadNo { get; set; }
    }
  
    public class AddDocument : CommonAPIR
    {
        public Int64 ID_LeadDocumentDetails { get; set; }
    }
    public class DocumentDetails : CommonAPIR
    {
        public List<DocumentDetailsList> DocumentDetailsList { get; set; }
    }
    public class DocumentDetailsList
    {
        public Int64 ID_LeadDocumentDetails { get; set; }
        public string DocumentDate { get; set; }
        public string DocumentSubject { get; set; }
        public string DocumentDescription { get; set; }
        public string DocumentImageFormat { get; set; }
    }
    public class DocumentImageDetails : CommonAPIR
    {
        public byte[] DocumentImage { get; set; }
    }
    public class AddRemark : CommonAPIR
    {
        public Int64 ID_LeadGenerateProduct { get; set; }

    }
    public class TodoListLeadDetails : CommonAPIR
    {
        public List<TodoListLeadDetailsList> TodoListLeadDetailsList { get; set; }

    }
    public class TodoListLeadDetailsList
    {
        public Int32 ID_TodoListLeadDetails { get; set; }
        public string TodoListLeadDetailsName { get; set; }
    }
    public class ServiceCustomerDetails:CommonAPIR
    {
        public List<ServiceCustomerList> ServiceCustomerList { get; set; }
    }
    public class ServiceCustomerList
    {
        public string Customer_ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string CusMode { get; set; }
        public string OtherMobile { get; set; }
    }
    public class CommonPopupDetails : CommonAPIR
    {
        public List<CommonPopupList> CommonPopupList { get; set; }
    }
  
    public class CommonPopupList
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public class ServiceProductDetails : CommonAPIR
    {
        public List<ServiceProductDetailsList> ServiceProductDetailsList { get; set; }

    }
    public class ServiceProductDetailsList
    {
        public string ID_Product { get; set; }
        public string ProductName { get; set; }
    }
    public class ServiceDet : CommonAPIR
    {
        public List<ServiceDetailsList> ServiceDetailsList { get; set; }
    }
    public class ServiceDetailsList
    {
        public string ID_Services { get; set; }
        public string ServicesName { get; set; }
    }
    public class ComplaintDetails : CommonAPIR
    {
        public List<ComplaintDetailsList> ComplaintDetailsList { get; set; }
    }
    public class ComplaintDetailsList
    {
        public string ID_ComplaintList { get; set; }
        public string ComplaintName { get; set; }
    }
    public class WarrantyDetails : CommonAPIR
    {
        public List<WarrantyDetailsList> WarrantyDetailsList { get; set; }
    }
    public class WarrantyDetailsList
    {
        public string ID_CustomerWiseProductDetails { get; set; }
        public string ID_WarrantyDetails { get; set; }
        public string WarrantyType { get; set; }
        public string ReplacementWarrantyExpireDate { get; set; }
        public string ServiceWarrantyExpireDate { get; set; }
        public string ReplacementWarrantyStatus { get; set; }
        public string ServiceWarrantyStatus { get; set; }
    }
    public class ProductHistoy : CommonAPIR
    {
        public List<ProductHistoyList> ProductHistoyList { get; set; }
    }
    public class ProductHistoyList
    {
        public string TicketNo { get; set; }
        public string RegOn { get; set; }
        public string Complaint { get; set; }
        public string Status { get; set; }
        public string AttendedBy { get; set; }
        public string EmployeeRemarks { get; set; }
      
    }
    public class SalesHistory : CommonAPIR
    {
        public List<SalesHistoryList> SalesHistoryList { get; set; }
    }
    public class SalesHistoryList
    {
        public Int64 ID_CustomerWiseProductDetails { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string Dealer { get; set; }
        public string Product { get; set; }
        public string Quantity { get; set; }       

    }
    public class UpdateCustomerServiceRegister : CommonAPIR
    {
        public string CSRTickno { get; set; }
    }
    public class CompanyLogDetails : CommonAPIR
    {
        public string DisplayType { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public byte[] CompanyLogo { get; set; }        
    }
    public class MediaDetails : CommonAPIR
    {
        public List<MediaDetailsList> MediaList { get; set; }
    }
    public class MediaDetailsList
    {
        public string ID_FIELD { get; set; }
        public string Name { get; set; }
    }
    public class ServiceCountDetails : CommonAPIR
    {
        public List<ServiceCountListDetails> ServiceCountList { get; set; }
    }
    public class ServiceCountListDetails
    {
        public string MasterID { get; set; }
        public string Field { get; set; }
        public string value { get; set; }
    }


    public class ServiceAssignNewDetails : CommonAPIR
    {
        public List<ServiceAssignNewList> ServiceAssignNewList { get; set; }
    }
    public class ServiceAssignNewList
    {
        public string ID_CustomerServiceRegister { get; set; }
        public string TicketNo { get; set; }
        public string TicketDate { get; set; }
        public string Branch { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string PriorityCode { get; set; }
        public string Priority { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Assign { get; set; }
        public string ProductStatus { get; set; }
        public string Area { get; set; }
        public string Due { get; set; }
        public string Employee { get; set; }
        public string Channel { get; set; }
        public string ID_CustomerServiceRegisterProductDetails { get; set; }
        public string ID_Master { get; set; }
        public string TransMode { get; set; }
        public string Product { get; set; }
        public Int16 TicketStatus { get; set; } 
    }
    public class CustomerBalanceDetails : CommonAPIR
    {
        public List<CustomerBalanceList> CustomerBalanceList { get; set; }
    }
    public class CustomerBalanceList
    {
      
        public string SlNo { get; set; }
        public string AccountDetails { get; set; }
        public string Balance { get; set; }
        public string Due { get; set; }
       
    }
    public class ServiceAssignOnGoingDetails : CommonAPIR
    {
        public List<ServiceAssignOnGoingList> ServiceAssignOnGoingList { get; set; }
    }
    public class ServiceAssignOnGoingList
    {
        public string ID_CustomerServiceRegister { get; set; }
        public string TicketNo { get; set; }
        public string TicketDate { get; set; }
        public string Branch { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string PriorityCode { get; set; }
        public string Priority { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string Assign { get; set; }
        public string ProductStatus { get; set; }
        public string Area { get; set; }
        public string Due { get; set; }
        public string Employee { get; set; }
        public string Channel { get; set; }
    }
    public class RoleDetails : CommonAPIR
    {
        public List<RoleDetailsList> RoleDetailsList { get; set; }
    }
    public class RoleDetailsList
    {
        public Int32 ID_Role { get; set; }
        public string RoleName { get; set; }
    }
    public class  ServiceAssignDetails:CommonAPIR
    {
       public Int64 ID_Customerserviceregister { get; set; }
       public string  FromDate { get; set; }
       public string  ToDate { get; set; }
       public string FromTime { get; set; }
       public string  ToTime { get; set; }
       public string Customer { get; set; }
       public string Address { get; set; }
       public string Remarks { get; set; }
       public string Mobile { get; set; }
       public string OtherMobile { get; set; }
       public string Landmark { get; set; }
       public string Ticket { get; set; }
       public byte Priority { get; set; }
        public string PriorityName { get; set; }
        public byte CurrentStatus { get; set; }
       public string  Productname { get; set; }
       public string ProductComplaint { get; set; }
       public string ProductDescription { get; set; }
      //  public string FK_Customer{ get; set; }
        public long ID_Customer { get; set; }
        public List<EmployeeRoleDetailsList> EmployeeRoleDetailsList { get; set; }
    }
    public class EmployeeRoleDetailsList
    {
        public string ID_Employee { get; set; }
        public string EmpCode { get; set; }
        public string Employee { get; set; }
        public string ID_CSAEmployeeType { get; set; }
        public string EmployeeType { get; set; }
        public string Attend { get; set; }
        public string DepartmentID { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
    }
    public class ServiceAsign
    {
        public Int64 FK_Customerserviceregister { get; set; }       
        public Int64 FK_OdMonUsers { get; set; }
        public string Visitdate { get; set; }
        public string Visittime { get; set; }
        public string LastID { get; set; }
        public List<OffLineDataProcessList> OffLineData { get; set; }
    }
    public class ServiceAssignedWork : CommonAPIR
    {
        public List<ServiceAssignedWorkList> ServiceAssignedWorkList { get; set; }
    }
    public class ServiceAssignedWorkList
    {
        public string TicketNo { get; set; }

        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
    }
   
    public class OffLineDataProcessList
    {
        public Int64 FK_OdMonUsers { get; set; }
        public string TransDate { get; set; }
        public string SubModule { get; set; }
        public Int64 FK_AccountCode { get; set; }
        public string OdmdtlRemarksByParty { get; set; }
        public string OdmdtlRemarksByUser { get; set; }
        public string OdmdtlNextActDate { get; set; }
        public byte OdmdtlActionMode { get; set; }
        public byte OdmdtlActionReqNotice { get; set; }
        public byte OdmdtlActionReqSitevisit { get; set; }
        public string OdmdtlActTime { get; set; }
        public string OdmdtlActDur { get; set; }
        public byte OdmdtlCallStatus { get; set; }
        public byte[] OdMonactdtlLandMark { get; set; }
        public string OdMonactdtlVoice { get; set; }
        public byte OdmdtlRiskType { get; set; }
        public decimal OdmdtlCollectionAmount { get; set; }
        public string CuslocLongitude { get; set; }
        public string CuslocLatitude { get; set; }
        public string CuslocLocName { get; set; }
        public byte[] CuslocLandMark1 { get; set; }
        public byte[] CuslocLandMark2 { get; set; }

        //public binary CuslocLandMark1 { get; set; }
        //public byte[] CuslocLandMark2 { get; set; }
        public byte OdmdtlAckMenthod { get; set; }
        public int RemarksMode { get; set; }
        public int RemarksTypeUser { get; set; }
        public int RemarksTypeParty { get; set; }
        public bool SaveNoteUser { get; set; }
        public bool SaveNoteParty { get; set; }

    }
    public class Customerserviceassignview
    {
        public string FK_Branch { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string Visitdate { get; set; }
        public string LastID { get; set; }
        public string Visittime { get; set; }
        public string FK_Priority { get; set; }
        public string Remark { get; set; }       
        public List<Assignees> Assignees { get; set; }      
        public string BankKey { get; set; }
        public string EntrBy { get; set; }
        
        public string FK_Customerserviceregister { get; set; }
        public string FK_CustomerserviceregisterProductDetails { get; set; }
    }
    public class Assignees
    {

        public long FK_Employee { get; set; }
        public Int32 EmployeeType { get; set; }
    }
    public class CustomerserviceassignUpdate : CommonAPIR
    {      
        public string Message { get; set; }
    }
    public class CustomerserviceassignEdit : CommonAPIR
    {
        public string Message { get; set; }
    }
    public class CustomerServiceRegisterCount:CommonAPIR
    {
        public Int32 WarrantyCount { get; set; }
        public Int32 ServiceHistoryCount { get; set; }
        public Int32 SalesCount { get; set; }
        public Int32 CustomerDueCount { get; set; }
    }
    public class ServiceFollowUpdetails:CommonAPIR
    {
        public List<ServiceFollowUpdetailsList> ServiceFollowUpdetailsList { get; set; }
    }
    public class ServiceFollowUpdetailsList
    {
        public Int64 ID_CustomerserviceregisterProductDetails { get; set; }
        public Int64 ID_Customerserviceregister { get; set; }
       public Int64 ID_ServiceFollowup { get; set; }
        public string Ticket { get; set; }
       public string TicketDate { get; set; }
        public string AssignedDate { get; set; }
        public Int32 DueDays { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string CurrentStatus { get; set; }
        public Int32 PriorityCode { get; set; }
        public string Priority { get; set; }
        public Int32 TotalCount { get; set; }
        public string Area { get; set; }
        public string FK_ImageLocation { get; set; }
        public string LocLongitude { get; set; }
        public string LocLattitude { get; set; }
        public string LocLocationName { get; set; }
        public string FK_Status { get; set; }
        public string Status_Longitude { get; set; }
        public string Status_Lattitude { get; set; }
        public string Status_LocationName { get; set; }
        public string Status_Date { get; set; }
        public string Status_Time { get; set; }        
    }
    public class ComplaintListDetails : CommonAPIR
    {
        public List<ComplaintListDetailsList> ComplaintListDetailsList { get; set; }

    }
    public class ComplaintListDetailsList
    {
        public Int64  ID_ComplaintList { get; set; }
        public string ComplaintName { get; set; }
    }
    public class WarrantyAMCDetails : CommonAPIR
    {
        public List<WarrantyAMCDetailsList> WarrantyAMCDetailsList { get; set; }

    }
    public class WarrantyAMCDetailsList
    {
      
       public string SlNo { get; set; }
       public string InvoiceNo { get; set; }
       public string InvoiceDate { get; set; }
       public string Dealer { get; set; }


    }
    public class EmployeeWiseTicketSelect:CommonAPIR
    {
        public string Ticket { get; set; }
        public string RegisterdOn { get; set; }
        public string VisitOn { get; set; }
        public string CustomerName{ get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public Int64 FK_Customer { get; set; }
        public Int64 FK_CustomerOthers { get; set; }
        public Int64 FK_Product { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Service { get; set; }
        public string Description { get; set; }
        public string CusNumber { get; set; }
    }
    #endregion
   
    public class MenuGroupDetails : CommonAPIR
    {
        public List<MenuGroupDetailsList> MenuGroupList { get; set; }
    }
    public class MenuGroupDetailsList
    {
        public long MenuGroupID { get; set; }
        public string MenuGroupName { get; set; }
        public bool MenuGroupVisible { get; set; }
        public long SortOrder { get; set; }
    }
    public class GenralSettingsDetails : CommonAPIR
    {
        public bool GsValue { get; set; }
    }
    public class UpdateWalkingCustomer : CommonAPIR
    {
        public string FK_CustomerAssignment { get; set; }
    }
    
    public class PickupandDeliveryCountDetails : CommonAPIR
    {
        public Int32 PickUp { get; set; }
        public Int32 Delivery { get; set; }
    }
    public class WalkingCustomerDetailsList : CommonAPIR
    {
        public List<WalkingCustomerDetails> WalkingCustomerDetails { get; set; }
    }  
    public class WalkingCustomerDetails
    {
        public int SlNo { get; set; }
        public Int64 ID_CustomerAssignment { get; set; }       
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string AssignedDate { get; set; }
        public string Employee { get; set; }
        public Int64 FK_Employee { get; set; }
        public string DESCRIPTION { get; set; }
        public string VoiceName { get; set; }
        public string VoiceData { get; set; }
        public int blnVoiceData { get; set; }
    }
   
    public class  ServiceDashBoardDetails:CommonAPIR
    {
        public string TotalCount { get; set; }
        public List<ServiceStages> ServiceStages { get; set; }
        public List<Services> Services { get; set; }
    }
    public class ServiceStages
    {
        public string Fileds { get; set; }
        public string Count { get; set; }
        public string Value { get; set; }
    }
    public class Services
    {
        public string Fileds { get; set; }
        public string Count { get; set; }
        public string Value { get; set; }
    }    
    public class AgendaCount : CommonAPIR
    {
        public string LEAD { get; set; }
        public string SERVICE { get; set; }
        public string EMI { get; set; }
        public List<LeadManagementDetails> LeadData { get;set; }
        public List<ServiceFollowUpdetailsList> ServiceData { get; set; }
        public List<EMICollectionReportList> EMIData { get; set; }
          
    }
    public class UpdateLocation : CommonAPIR
    {
        public string FK_Location { get; set; }
    }
    public class UpdateFollowupStatus:CommonAPIR
    {
        public string FK_FollowUpStatus { get; set; }
        public string FK_Status { get; set; }
    }

    public class UpdateAttanceMarking : CommonAPIR
    {
        public string FK_EmployeeAttanceMarking { get; set; }
    }
    public class UpdateEmployeeLocation : CommonAPIR
    {
        public string FK_EmployeeLocation { get; set; }
    }
    public class UpdateNotification : CommonAPIR
    {
        public string ID_Notification { get; set; }
    }
    public class EmployeeLocationList : CommonAPIR
    {
        public List<EmployeeLocation> EmployeeLocationListData { get; set; }
    }
    public class EmployeeLocation
    {      
        public string FK_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string LocLongitude { get; set; }
        public string LocLattitude { get; set; }
        public string LocLocationName { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTime { get; set; }
        public string ChargePercentage { get; set; }
        public string FK_Department { get; set; }
        public string DeptName { get; set; }
        public string FK_Designation { get; set; }
        public string DesName { get; set; }
        public int Attance { get; set; }
    }
    public class EmployeeWiseLocationList : CommonAPIR
    {
        public List<EmployeeWiseLocation> EmployeeWiseLocationListData { get; set; }
    }
    public class EmployeeWiseLocation
    {
        public string FK_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string LocLongitude { get; set; }
        public string LocLattitude { get; set; }
        public string LocLocationName { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTime { get; set; }
        public string ChargePercentage { get; set; }
       
    }

    public class DesignationList : CommonAPIR
    {
        public List<DesignationDetails> DesignationDetails { get; set; }
    }
    public class DesignationDetails
    {
        public string ID_Designation { get; set; }
        public string DesignationName { get; set; }
       
    }

    public class MailResult : CommonAPIR
    {
        public string Result { get; set; }
    }
    public class LeadGenerateData
    {
        public string BankKey { get; set; }
        public string Token { get; set; }
        public string UserAction { get; set; }
        public string TransMode { get; set; }
        public string ID_LeadGenerate { get; set; }
        public string LgLeadDate { get; set; }
        public string FK_Customer { get; set; }
        public string FK_SubMedia { get; set; }
        public string FK_CustomerOthers { get; set; }
        public string LgCusNameTitle { get; set; }
        public string LgCusName { get; set; }
        public string LgCusAddress { get; set; }
        public string LgCusAddress2 { get; set; }
        public string LgCusMobile { get; set; }
        public string LgCusEmail { get; set; }
        public string CusCompany { get; set; }
        public string CusPhone { get; set; }
        public string FK_Country { get; set; }       
        public string FK_States { get; set; }
        public string FK_District { get; set; }
        public string FK_Area { get; set; }
        public string FK_Post { get; set; }
        public string LastID { get; set; }
        public string FK_LeadFrom { get; set; }
        public string FK_LeadBy { get; set; }
        public string FK_LeadByName { get; set; }
        public string FK_MediaMaster { get; set; }
        public string LgCollectedBy { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public long PreviousID { get; set; }
        public string CusMobileAlternate { get; set; }
        public string LocLatitude { get; set; }
        public string LocLongitude { get; set; }
        public byte[] LocationLandMark1 { get; set; }
        public byte[] LocationLandMark2 { get; set; }
        public string ID_CustomerAssignment { get; set; }
        public string FK_AuthorizationData { get; set; }
        public List<ProdProjDTL> ProductDetails { get; set; }
        public string ProductsDetails { get; set; }
        public  string LandNumber { get; set; }
        public string ID_TokenUser { get; set; }
      
    }
  
    public class ItemSearchList : CommonAPIR
    {
        public List<ProductListData> ItemSearchListData { get; set; }
    }
    public class ProductListData
    {
        public string ID_Product { get; set; }
        public string ProductName { get; set; }
        public string ProdShortName { get; set; }
        public string MRP { get; set; }
        public string Price { get; set; }
        public string FK_Category { get; set; }
        public string CategoryName { get; set; }
        public string Project { get; set; }     
    }
    public class ProductEnquiryList : CommonAPIR
    {
        public List<ProductEnquiry> ProductEnquiryListData { get; set; }
    }
    public class ProductEnquiry
    {
        public string ID_Product { get; set; }
        public string ID_Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MRP { get; set; }
        public string SalPrice { get; set; }
        public string CurrentQuantity { get; set; }
        public string ProductDescription { get; set; }
        public string Offer { get; set; }
        public string OfrName { get; set; }
        public string OfrDescription { get; set; }
        public string OfrExpireDate { get; set; }
        public string Image { get; set; } 
    }
    public class ProductEnquiryDetails : CommonAPIR
    {
        public string ID_Product { get; set; }
        public string ID_Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ProductDescription { get; set; }
        public List<ProductEnquiryImageData> ImageList { get; set; }
        public List<ProductEnquiry> RelatedItem { get; set; }
    }
    public class ProductEnquiryImageData
    {           
        public string Image { get; set; }
    }
    public class AuthorizationModuleData : CommonAPIR
    {       
        public List<AuthorizationModuleDetails> AuthorizationModuleDetails { get; set; }
    }
    public class AuthorizationModuleDetails
    {
        public string Module { get; set; }
        public string Module_Name { get; set; }
        public string NoofRecords { get; set; }
        public string MobImage { get; set; }
        public string MobColor { get; set; }
    }
    
    public class DynamicData : CommonAPIR
    {
        public IEnumerable<dynamic> ListData { get; set; }
    }
    public class AuthorizationActionDetails : CommonAPIR
    {
     
        public string TransactionDetails { get; set; }
        public string PartyDetails { get; set; }
        public int SubDetails { get; set; }
        public string SubTitle { get; set; }
        public string SubTitleHTML { get; set; }
        public IEnumerable<dynamic> SubDetailsData { get; set; }
        public string FooterLeft { get; set; }
        public string FooterRight { get; set; }
        public string FK_Master { get; set; }
        public string TransMode { get; set; }
        public string Mode { get; set; }
        public string AuthID { get; set; }
        public string ActiveCorrectionOption { get; set; }

    }
    public class UpdateAuthorization : CommonAPIR
    {
        public string ID_AuthorizationLevel { get; set; }
    }
    public class CorrectionAuthorization : CommonAPIR
    {
        public string ID_AuthorizationLevel { get; set; }
    }
    public class ProductLocationList : CommonAPIR
    {
        public List<ProductLocation> ProductLocationListData { get; set; }
    }
    public class ProductLocation
    {
        public string ID_ProductLocation { get; set; }
        public string LocationName { get; set; }
    }
    public class UserTaskList : CommonAPIR
    {
        public List<TaskList> UserTaskListData { get; set; }
    }
    public class TaskList
    {
        public string Task { get; set; }
        public int Value { get; set; }
        public int ModuleCount { get; set; }
        public string ID_AuthorizationData { get; set; }
        public string TransMode { get; set; }
        public string Mode { get; set; }
        public string FK_TransMaster { get; set; }
        public string MobImage { get; set; }
        public string MobColor { get; set; }
        public string Criteria{ get; set; }
    }
    public class WalkingCustomerList : CommonAPIR
    {
        public List<WalkingCustomerListDetails> WalkingCustomerDetails { get; set; }
    }
    public class WalkingCustomerListDetails
    {       
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string Product { get; set; }
        public string Action { get; set; }
        public string AssignedTo { get; set; }
        public string ID_LeadGenerateProduct { get; set; }
        public string FollowUpDate { get; set; }
        public string FK_Employee { get; set; }
        public string ID_Users { get; set; }
        public string LeadNo { get; set; }
    }
    public class WalkingCustomerData
    {
        public string BankKey { get; set; }
        public string UserAction { get; set; }     
        public string TransMode { get; set; }
        public string ID_CustomerAssignment { get; set; }
        public string CusName { get; set; }
        public string CusMobile { get; set; }
        public string CaAssignedDate { get; set; }
        public string FK_Employee { get; set; }
        public string CaDescription { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string FK_Machine { get; set; }
        public List<leadByMobileNo> leadByMobileNo { get; set; }
        public string leadByMobileNos { get; set; }
        public string FK_Product { get; set; }
        public string FK_Category { get; set; }
        public string ProjectName{ get; set; }
        public string CaCusEmail { get; set; }
        public string VoiceData { get; set; }
        public string VoiceLabel { get; set; }
        public string Token { get; set; }
        public string ID_TokenUser { get; set; }
    }

    public class leadByMobileNo
    {
        public string ID_LeadGenerateProduct { get; set; }
        public string FK_Employee { get; set; }
        public string FollowUpDate { get; set; }     
       
    }
    public class AuthorizationCorrectionModuleData : CommonAPIR
    {     
        public List<AuthorizationCorrectionModuleDetails> AuthorizationModuleCorrectionDetails { get; set; }
    }
    public class AuthorizationCorrectionModuleDetails
    {
        public string Module { get; set; }
        public string Module_Name { get; set; }
        public string NoofRecords { get; set; }
        public string MobImage { get; set; }
        public string MobColor { get; set; }
        public string ID_AuthorizationData { get; set; }
        public string TransMode { get; set; }
        public string Mode { get; set; }
        public string FK_TransMaster { get; set; }
    }
    public class AuthorizationCorrectionDetailsData : CommonAPIR
    {
        public List<AuthorizationCorrectionDetails> AuthorizationCorrectionDetails { get; set; }
    }
    public class AuthorizationCorrectionDetails
    {
        public string ID_AuthorizationData { get; set; }
        public string TransMode { get; set; }
        public string Mode { get; set; }
        public string TransTitle { get; set; }
        public string TransNo { get; set; }
        public string FK_TransMaster { get; set; }
        public string CorrectionPassBy { get; set; }
        public string CorrectionPassOn { get; set; }
        public string ContactNum { get; set; }
    }
  
    public class CorrectionLeadGenerate : CommonAPIR
    {
        public string LeadGenerateID { get; set; }
        public string LeadNo { get; set; }
        public string LeadID { get; set; }
        public string LeadDate { get; set; }
        public string LeadFrom { get; set; }
        public string LeadBy { get; set; }
        public string ID_MediaMaster { get; set; }
        public string SubMedia { get; set; }
        public string PinCode { get; set; }
        public string CountryID { get; set; }
        public string StatesID { get; set; }
        public string DistrictID { get; set; }
        public string PostID { get; set; }
        public string Company { get; set; }
        public string CntryName { get; set; }
        public string StName { get; set; }
        public string DtName { get; set; }
        public string PostName { get; set; }
        public string CusPhnNo { get; set; }
        public string LeadByName { get; set; }
        public string ID_Customer { get; set; }
        public string FK_CustomerOthers { get; set; }
        public string CollectedBy { get; set; }
        public string CollectedByName { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string CusName { get; set; }
        public string CusMobile { get; set; }
        public string CusEmail { get; set; }
        public string CusAddress { get; set; }
        public string CusAddress2 { get; set; }
        public string CusNameTitle { get; set; }
        public string CusMobileAlternate { get; set; }
        public string Area { get; set; }
        public string AreaID { get; set; }
        public string FK_Quotation { get; set; }
        public List<CorrectionLeadGenerateProduct> ProductDetails { get; set; }
        public List<CorrectionSenderDtls> SenderDetails { get; set; }
    }
    public class CorrectionLeadGenerateProduct
    {
        public string ID_LeadGenerateProduct { get; set; }
        public string FK_LeadGenerate { get; set; }
        public string ID_Product { get; set; }
        public string ProdName { get; set; }
        public string ProjectName { get; set; }
        public string LgpPQuantity { get; set; }
        public string LgpDescription { get; set; }
        public string FK_NetAction { get; set; }
        public string NextActionDate { get; set; }
        public string FK_Departement { get; set; }
        public string FK_Employee { get; set; }
        public string LgActStatus { get; set; }
        public string LgActDate { get; set; }
        public string LgActCusComment { get; set; }
        public string LgActLeadComment { get; set; }
        public string FK_Priority { get; set; }
        public string BranchID { get; set; }
        public string AssignEmp { get; set; }
        public string BranchTypeID { get; set; }
        public string FK_ActionType { get; set; }
        public string ActStatus { get; set; }
        public string FK_Category { get; set; }
        public string CategoryName { get; set; }
        public string Project { get; set; }
        public string LgpExpectDate { get; set; }
        public string LgpMRP { get; set; }
        public string LgpSalesPrice { get; set; }
        public string FK_ProductLocation { get; set; }       
    }


    public class CorrectionSenderDtls
    {
        public string Sender { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
    }
    public class WalkingCustomerVoiceDetails : CommonAPIR
    {
        public string VoiceName { get; set; }
        public string VoiceData { get; set; }
    }
    public class AuthorizationDetails : CommonAPIR
    {
        //public String label  { get; set; }
        //public String color { get; set; }
        //public String count { get; set; }
        //public String icon { get; set; }
        //public String mode { get; set; }
        public List<AuthorizationDetailsList> AuthorizationDetailsList { get; set; }

    }
    public class AuthorizationDetailsList
    {
        public String label { get; set; }
        public String color { get; set; }
        public String count { get; set; }
        public String icon { get; set; }
        public String mode { get; set; }
    }

    public class ModuleDetails : CommonAPIR
    {
        public List<ModuleDetailsList> ModuleDetailsList { get; set; }

    }
    public class ModuleDetailsList
    {
        public Int64 ID_MenuGroup { get; set; }
        public string MnuGrpName { get; set; }

    }
    public class ModuleWiseSearchDetails : CommonAPIR
    {
        public List<ModuleWiseSearchDetailsList> ModuleWiseSearchDetailsList { get; set; }

    }
    public class ModuleWiseSearchDetailsList
    {
        public Int64 Mode { get; set; }
        public string ModeName{ get; set; }
        public Int64 SubMode { get; set; }
        public string SubModeName { get; set; }

    }
    public class AuthorizationModuleCountDetails : CommonAPIR
    {
        
        public List<AuthorizationModuleCountDetailsList> AuthorizationModuleCountDetailsList { get; set; }

    }
    public class AuthorizationModuleCountDetailsList
    {
        public String label { get; set; }
        public String color { get; set; }
        public String count { get; set; }
        public String icon { get; set; }
        public String mode { get; set; }
    }
    public class AuthorizationPendingDetails : CommonAPIR
    {

        public List<AuthorizationPendingDetailsList> AuthorizationPendingDetailsList { get; set; }

    }
    public class AuthorizationPendingDetailsList
    {
        public String label { get; set; }
        public String color { get; set; }
        public String count { get; set; }
        public String icon { get; set; }
        public String mode { get; set; }
        public String SlNo { get; set; }
        public String ID_FIELD { get; set; }
        public String Action { get; set; }
        public String TicketNo { get; set; }
        public String TicketDate { get; set; }
        public String drank { get; set; }
        public String EnteredBy { get; set; }
        public String EnteredOn { get; set; }
        public String TotalCount { get; set; }
    }
    public class OtherChargeDetails : CommonAPIR
    {
        public List<OtherChargeDetailsList> OtherChargeDetailsList { get; set; }
    }
    public class OtherChargeDetailsList
    {
        public string ID_OtherChargeType { get; set; }
        //  public string Service { get; set; }
        public string OctyName { get; set; }
        public string OctyTransType { get; set; }
       

    }
    public class DashBoardModule:CommonAPIR
    {
        public List<DashBoardModuleList> DashBoardModuleList { get; set; }
    }
    public class DashBoardModuleList
    {
        public Int32 ModuleMode { get; set; }
        public string ModuleName { get; set; }
    }
    public class AuthorizationDataList : CommonAPIR
    {
        public List<DataItems> DataItems { get; set; }
    }
    public class DataItems
    {
        public string Module { get; set; }
        public List<Details> Details { get; set; }
    }
    public class Details
    {
        public long FK_Transaction { get; set; }
        public string TransNumber { get; set; }
        public string TransDate { get; set; }
        public string CusName { get; set; }
        public string CusMobile { get; set; }
        public string CusAddress { get; set; }
        public string Priority { get; set; }
        public string EnteredBy { get; set; }
        public string Reason { get; set; }
        public string Amount { get; set; }
    }


    public class UserTaskListDetails : CommonAPIR
    {
        //public List<TaskListDetails> ListData { get; set; }
        public List<DataItemsList> DataItemsList { get; set; }
        

    }
    public class DataItemsList
    {
        public string ModuleName { get; set; }
        public List<ListData> ListData { get; set; }
    }
    public class ListData
    {
         public int SlNo { get; set; }
        public string LeadNo { get; set; }
        public string CustomerName { get; set; }
         public string Address { get; set; }
         public string Product { get; set; }
         public string AuthorizedBy { get; set; }
         public string Date { get; set; }
         public string Time { get; set; }
         public string Rejection { get; set; }
    }
    public class SendIntimationModule : CommonAPIR
    {
        public List<IntimationModuleList> IntimationModuleList { get; set; }
    }
    public class IntimationModuleList
    {
        public string ID_Mode { get; set; }
        public string ModeName { get; set; }

    }
    public class CheckVersionCode
    {
        public int ChkStatusOutput { get; set; }
    }
    public class ScheduleType : CommonAPIR
    {
        public List<ScheduleTypeList> ScheduleTypeList { get; set; }
    }
    public class ScheduleTypeList
    {
        public int ID_SheduledType { get; set; }
        public string SheduledTypeName { get; set; }

    }
    public class Channel : CommonAPIR
    {
        public List<ChannelList> ChannelList { get; set; }
    }
    public class ChannelList
    {
        public int ID_Channel { get; set; }
        public string ChannelName { get; set; }

    }
    public class UpdateSendIntimation: CommonAPIR
    {
        public string Message { get; set; }
    }

    public class SendIntimationData
    {
        public string BankKey { get; set; }
        public string UserAction { get; set; }
        public string ID_CommonIntimation { get; set; }
        public string TransMode { get; set; }
        public string Module { get; set; }
        public string Branch { get; set; }
        public string Status{ get; set; }
        public string Channel { get; set; }
        public string SheduledType { get; set; }
        public string DlId { get; set; }
        public string Subject { get; set; }
        public string Unicode { get; set; }
        public string Debug { get; set; }
        //public byte[] Attachment { get; set; }
        public string Attachment { get; set; }
        public string Message { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string FK_Machine { get; set; }

        public string Date { get; set; }
        public string SheduledTime { get; set; }
        public string SheduledDate { get; set; }
        public string FK_CommonIntimation { get; set; }
        public string ID_LeadFrom { get; set; }
        public string FK_LeadThrough { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FK_Category { get; set; }
        public string ProdType { get; set; }
        public string ID_Product { get; set; }
        public string FK_Employee { get; set; }
        public string Collectedby_ID { get; set; }
        public string Area_ID { get; set; }

        public string FK_NetAction { get; set; }
        public string FK_ActionType { get; set; }
        public string FK_Priority { get; set; }
        public string SearchBy { get; set; }
        public string SearchBydetails { get; set; }
        public string LeadDetails { get; set; }
        public string GridData { get; set; }
        public List<SendIntimationList> LeadCusDetails { get; set; }
        public string LeadCusDetailss { get; set; }


    }
    public class SendIntimationList
    {
        public string FK_Lead { get; set; }
        public string LeadNo { get; set; }
        public string FK_Customer { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string LeadDate { get; set; }
    }
        public class AppConfigurationSettings:CommonAPIR
    {
        public string CERT_NAME { get; set; }
        public string BASE_URL { get; set; }
        public string IMAGE_URL { get; set; }
        public string BANK_KEY { get; set; }
    }
    public class EmployeeLocationDistanceList
    {
        public Int64 FK_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string StartingPoint { get; set; }
        public  string EndingPoint { get; set; }
        public dynamic JSonData { get; set; }

    }
    public class EmployeeLocationDistance: CommonAPIR
    {
        public List<EmployeeLocationDistanceList> EmployeeLocationDistanceList { get; set; }
    }
    public class SubCategoryDetailsList : CommonAPIR
    {
        public List<SubCategoryList> SubCategoryList { get; set; }
    }
    public class SubCategoryList
    {
        public Int64 ID_SubCategory { get; set; }
        public string SubCatName { get; set; }
      
    }
    public class BrandDetails : CommonAPIR
    {
        public List<BrandDetailsList> BrandDetailsList { get; set; }
    }
    public class BrandDetailsList
    {
        public Int64 ID_Brand { get; set; }
        public string BrandName { get; set; }

    }
    public class LeadSourceDetails : CommonAPIR
    {
        public List<LeadSourceDetailsList> LeadSourceDetailsList { get; set; }
    }
    public class LeadSourceDetailsList
    {
        public Int64 ID_LeadFrom { get; set; }
        public string LeadFromName { get; set; }

    }
    public class LeadFromInfo : CommonAPIR
    {
        //public List<LeadFromList> LeadFromList { get; set; }
        public DataTable LeadFromList { get; set; }
    }
    public class LeadFromList
    {
        public Int64 ID_FIELD { get; set; }
        public Int64 Code { get; set; }
        public string Name { get; set; }
        public string HasSub { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }


    }
    public class LeadHistory : CommonAPIR
    {
        public List<LeadHistoryList> LeadHistoryList { get; set; }
    }
    public class LeadHistoryList
    {
        public string SlNo { get; set; }
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Email{ get; set; }


    }
    public class ProductType : CommonAPIR
    {
        public List<ProductTypeList> ProductTypeList { get; set; }
    }
    public class ProductTypeList
    {
        public Int64 ID_ProductType { get; set; }
        public string ProductTypeName { get; set; }

    }
    public class TimeLineDetails : CommonAPIR
    {
        public List<TimeLineList> TimeLineList { get; set; }

    }
    public class TimeLineList
    {
        public string Title1 { get; set; }
        public List<DataList> DataList { get; set; }
    }
    public class DataList
    {
        public int SLNo { get; set; }
        public string Head { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public List<string> Description{ get; set; }
        //public string Description{ get; set; }
        public string ID_Transaction { get; set; }
        public string MoreData { get; set; }
        public string EntrOn { get; set; }
        public string EntrBy { get; set; }
    }
    public class AttendanceDetails : CommonAPIR
    {
        public List<AttendanceDetailsList> AttendanceDetailsList { get; set; }
    }
    public class AttendanceDetailsList
    {
        public Int64 ID_EmployeeAttanceMarking { get; set; }
        public Int64 FK_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string Longitude { get; set; }
        public string Lattitude { get; set; }
        public string LocationName { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTime { get; set; }
        public string Status { get; set; }
        public string TimeDuration { get; set; }



    }
    public class AttendancePunchDetails : CommonAPIR
    {
        public List<AttendanceFirstPunchdetails> AttendanceFirstPunchdetails { get; set; }
        public List<AttendanceLastPunchdetails> AttendanceLastPunchdetails { get; set; }
    }
    public class AttendanceFirstPunchdetails
    {
        public string ID_EmployeeAttanceMarking { get; set; }
        public string FK_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Longitude { get; set; }
        public string Lattitude { get; set; }
        public string LocationName { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTime { get; set; }
        public string PunchStatus { get; set; }




    }
    public class AttendanceLastPunchdetails
    {
        public string ID_EmployeeAttanceMarking { get; set; }
        public string FK_Employee { get; set; }
        public string Designation { get; set; }
        public string EmployeeName { get; set; }
        public string Longitude { get; set; }
        public string Lattitude { get; set; }
        public string LocationName { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredTime { get; set; }
       public string PunchStatus { get; set; }




    }
    public class UserDetails: CommonAPIR
    {
        public List<UserList> UserList { get; set; }
    }
    public class UserList
    {
        public Int64 ID_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string EmpCode { get; set; }
        public string PhoneNo{ get; set; }
        public string PnUserToken { get; set; }
        public string FK_Branch{ get; set; }
        public string BranchName{ get; set; }
        public string EntrBy { get; set; }




    }
    public class LicenceList
    {
        public string Field { get; set; }
        public string value { get; set; }
    }
    public class MyActivityCount : CommonAPIR
    {
        public List<MyActivityCountList> MyActivityCountList { get; set; }
    }
    public class MyActivityCountList
    {
        public Int32 SubMode { get; set; }
        public string Name { get; set; }
        public Int32 Count { get; set; }
      
    
    }
    public class MyActivitysActionCountDetails : CommonAPIR
    {
        public List<MyActivitysActionCountList> MyActivitysActionCountList { get; set; }
    }
    public class MyActivitysActionCountList
    {
        public Int64 ID_ActionType { get; set; }
        public string ActionName { get; set; }
        public Int32 count { get; set; }
    }


    public class MyActivitysActionDetails : CommonAPIR
    {
        public List<MyActivitysActionList> MyActivitysActionList { get; set; }
    }
    public class MyActivitysActionList
    {
        public string ID_LeadGenerateProduct { get; set; }
        public string Product { get; set; }
        public string EnquiryAbout { get; set; }
        public string LgpDescription { get; set; }
        public string Action { get; set; }
        public string ActionDate { get; set; }
        public string Status { get; set; }
        public string Agentremarks { get; set; }
        public string FollowedBy { get; set; }
        public string ActionType { get; set; }
        public string CustomerRemarks { get; set; }
        public string CustomerName { get; set; }
    }
    public class MyActivitysFliters : CommonAPIR
    {
        public List<MyActivitysFlitersList> MyActivitysFlitersList { get; set; }
    }
    public class MyActivitysFlitersList
    {
        public Int32 IdFliter { get; set; }
        public string FliterType { get; set; }
      
    }
    public class ClosedLeadReport : CommonAPIR
    {
        public List<ClosedLeadReportList> ClosedLeadReportList { get; set; }
    }
    public class ClosedLeadReportList
    {
        public string Category { get; set; }
        public string Employee { get; set; }
        public string Product { get; set; }
        public string Branch { get; set; }
        public string Count { get; set; }
        public string Criteria { get; set; }
        public string FK_ComCategory { get; set; }

    }
}

