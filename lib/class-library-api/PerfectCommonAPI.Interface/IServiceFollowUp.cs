using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{

    public interface IServiceFollowUp
    {
        string ReqMode { get; set; }
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

        string NameCriteria { get; set; }
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
        string Criteria4 { get; set; }
        string Criteria3{ get; set; }
        string ID_CustomerServiceRegister { get; set; }
        string CSRChannelID { get; set; }
        string CSRPriority { get; set; }
        string CSRCurrentStatus { get; set; }
        string FK_Branch { get; set; }
        string CSRPCategory { get; set; }
        string FK_OtherCompany { get; set; }
        string FK_ComplaintList { get; set; }
        string FK_ServiceList { get; set; }
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
        string Visittime { get; set; }
        string Assignees { get; set; }
        string EmployeeType { get; set; }
        string FK_AttendedBy { get; set; }
        string TicketTime { get; set; }
        string Address1 { get; set; }
        string Address2 { get; set; }
        
        string CustomerNotes { get; set; }
        string StartingDate { get; set; }
        string ServiceAmount { get; set; }
        string ProductAmount { get; set; }
        string NetAmount { get; set; }
        string ComponentCharge { get; set; }
        string ServiceCharge { get; set; }
        string OtherCharge { get; set; }
        string TotalSecurityAmount { get; set; }
        string TotalAmount { get; set; }
        string ReplaceAmount { get; set; }
        string DiscountAmount { get; set; }
        string  ServiceDetails { get; set; }
        string ProductDetails { get; set; }
        string AttendedEmployeeDetails { get; set; }

        string ServiceIncentive { get; set; }
        string Actionproductdetails { get; set; }
        string OtherCharges { get; set; }
        string PaymentDetail { get; set; }
        string NextActionDateLead { get; set; }
        string FK_BillType { get; set; }
        string FK_NextActionLead { get; set; }
        string FK_ActionTypeLead { get; set; }
        string FK_EmployeeLead { get; set; }
        string FK_NextAction { get; set; }
        string LocationEnteredDate { get; set; }
        string LocationEnteredTime { get; set; }
        string ProductSubDetails { get; set; }
        string  ID_CustomerServiceRegisterProductDetails { get; set; }
        string ID_CustomerWiseProductDetails { get; set; }

    }
    #region Response Interface Model Objects
    public class Attendancedetails : CommonAPIR
    {
        public List<AttendancedetailsList> AttendancedetailsList { get; set; }
    }
    public class AttendancedetailsList
    {
        public Int64 ID_Employee { get; set; }
        public string EmployeeName { get; set; }
        public Int64 ID_CSAEmployeeType { get; set; }
        public Int32 Attend { get; set; }
        public Int64 DepartmentID { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string Designation { get; set; }


    }
    public class ServiceHistoryDetails : CommonAPIR
    {
        public List<ServiceHistoryDetailsList> ServiceHistoryDetailsList { get; set; }
    }
    public class ServiceHistoryDetailsList
    {
        public string TicketNo { get; set; }
        //  public string Service { get; set; }
        public string Complaint { get; set; }
        public string CurrentStatus { get; set; }
        public string ClosedDate { get; set; }
        public string EmployeeNotes { get; set; }
        public string RegOn { get; set; }
        public string AttendedBy { get; set; }
        public string TransMode { get; set; }
        public long ID_Master { get; set; }

    }
    public class ServiceAttendedDetails : CommonAPIR
    {
        public List<ServiceAttendedDetailsList> ServiceAttendedDetailsList { get; set; }

    }
    public class ServiceAttendedDetailsList
    {
        public Int64 ID_ProductWiseServiceDetails { get; set; }
        public string SubProduct { get; set; }
        public Int64 ID_Product { get; set; }
        public Int64 ID_Services { get; set; }
        public string Service { get; set; }
        public string ServiceCost { get; set; }
        public string ServiceTaxAmount { get; set; }
        public string ServiceNetAmount { get; set; }
        public string Remarks { get; set; }


    }
    public class ServiceType : CommonAPIR
    {
        public List<ServiceTypeList> ServiceTypeList { get; set; }
    }
    public class ServiceTypeList
    {
        public Int32 ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }

    }
    public class ServiceInvoice : CommonAPIR
    {
        public List<TicketDetails> TicketDetails { get; set; }
        public List<ServiceInformation> ServiceInformation { get; set; }
        public List<ProductInformation> ProductInformation { get; set; }
        public List<AmountDetails> AmountDetails { get; set; }
        //public List<NetDetails> NetDetails { get; set; }
    }
    public class TicketDetails
    {
        public string TicketNo { get; set; }
        public string Customer { get; set; }
        public string CusAddress { get; set; }
        public string ClosedDate { get; set; }
        public string RegDate { get; set; }
        public long ID_CustomerServiceRegister { get; set; }

    }
    public class ServiceInformation
    {
        public Int64 ServiceId { get; set; }
        public string Service { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }

    }
    public class ProductInformation
    {
        public string Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal SalePrice { get; set; }
        public decimal NetAmount { get; set; }

    }
    public class AmountDetails
    {
        public decimal AdvanceAmount { get; set; }
        public decimal SecurityAmount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ProductCharge { get; set; }
        public decimal  NetAmount { get; set; }
    }
    public class NetDetails
    {
        public decimal NetAmount { get; set; }
        public decimal ProductCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal SecurityAmount { get; set; }
        public decimal DiscountAmount { get; set; }

    }

    public class AddedService : CommonAPIR
    {
        public List<AddedServiceList> AddedServiceList { get; set; }
    }
    public class AddedServiceList
    {
        public Int64 ID_Services { get; set; }
        public string Service { get; set; }
        public Int64 FK_TaxGroup { get; set; }
        public decimal TaxPercentage { get; set; }
        public Boolean ServiceChargeIncludeTax { get; set; }
    }
    public class ServiceSerach : CommonAPIR
    {
        public List<ServiceSerachList> ServiceSerachList { get; set; }
    }
    public class ServiceSerachList
    {
        public Int64 SNo { get; set; }
        public Int64 FK_Category { get; set; }
        public string MasterProduct { get; set; }
        public Int64 FK_Product { get; set; }
        public string Product { get; set; }
        public string SLNo { get; set; }
        public string BindProduct { get; set; }
        public string ComplaintProduct { get; set; }
        public string Warranty { get; set; }
        public string ServiceWarrantyExpireDate { get; set; }
        public string ReplacementWarrantyExpireDate { get; set; }
        public Int64 ID_CustomerWiseProductDetails { get; set; }
        public string ServiceWarrantyExpired { get; set; }
        public string ReplacementWarrantyExpired { get; set; }

    }
    public class ProductListDetails : CommonAPIR
    {
        public List<ProductSearchList> ProductSearchList { get; set; }
    }
    public class ProductSearchList
    {
        public Int64 SlNo { get; set; }
        public Int64 ID_Product { get; set; }
        public string  Name { get; set; }

        public decimal PurRate { get; set; }

        public decimal ProductAmount { get; set; }

        public decimal CurrentStock { get; set; }
        public decimal Stock { get; set; }
        public decimal StandbyStock { get; set; }
        public decimal StandbyQuantity { get; set; }
        public Int64 ID_Stock { get; set; }


    }

        public class SerDetails : CommonAPIR
    {
        public List<ServiceAttendedList> ServiceAttendedList { get; set; }

        //  public List<PartservicereplacedList> PartservicereplacedList { get; set; }
        // public List<AddedServiceList> ServiceAttendedList { get; set; }
    }
    public class ServiceAttendedListDet
    {
        public Int64 SNo { get; set; }
        public Int64 FK_Category { get; set; }
        public string MasterProduct { get; set; }
        public Int64 FK_Product { get; set; }
        public string Product { get; set; }
        public string SLNo { get; set; }
        public string BindProduct { get; set; }
        public string SerchSerialNo { get; set; }
        public string ComplaintProduct { get; set; }
        public string Warranty { get; set; }
        public string ServiceWarrantyExpireDate { get; set; }
        public string ReplacementWarrantyExpireDate { get; set; }
        public Int64 ID_CustomerWiseProductDetails { get; set; }
        public string ServiceWarrantyExpired { get; set; }
        public string ReplacementWarrantyExpired { get; set; }
    }
    public class ServiceAttendedList
    {
        public Int64 SNo { get; set; }
        // public Int64 FK_Category { get; set; }
        public string MasterProduct { get; set; }
        public Int64 FK_Product { get; set; }
        public Int64 FK_Category { get; set; }
    public string Product { get; set; }
        public string Mode { get; set; }
        public string BindProduct { get; set; }
        public Int64 bFK_Category { get; set; }
        public string SerchSerialNo { get; set; }
        public string ComplaintProduct { get; set; }
        public string Warranty { get; set; }
        public string ServiceWarrantyExpireDate { get; set; }
        public string ReplacementWarrantyExpireDate { get; set; }
        public Int64 ID_CustomerWiseProductDetails { get; set; }
        public string ServiceWarrantyExpired { get; set; }
        public string ReplacementWarrantyExpired { get; set; }
        public List<ServiceAttendedListDet> ServiceAttendedListDet { get; set; }
        //public string SLNo { get; set; }

    }
    public class ProductInfo : CommonAPIR
    {
        public List<ProductInfoList> ProductInfoList { get; set; }

        //  public List<PartservicereplacedList> PartservicereplacedList { get; set; }
        // public List<AddedServiceList> ServiceAttendedList { get; set; }
    }
    public class ProductInfoList
    {
        public Int64 SNo { get; set; }
        // public Int64 FK_Category { get; set; }
        public string MasterProduct { get; set; }
        public Int64 FK_Product { get; set; }
        public Int64 FK_Category { get; set; }
        public string Product { get; set; }
        public string Mode { get; set; }
        public string BindProduct { get; set; }
        public Int64 bFK_Category { get; set; }
        public string SerchSerialNo { get; set; }
        public string ComplaintProduct { get; set; }
        public string Warranty { get; set; }
        public string ServiceWarrantyExpireDate { get; set; }
        public string ReplacementWarrantyExpireDate { get; set; }
        public Int64 ID_CustomerWiseProductDetails { get; set; }
        public string ServiceWarrantyExpired { get; set; }
        public string ReplacementWarrantyExpired { get; set; }
        public List<ProductInfoListDet> ProductInfoListDet { get; set; }
        //public string SLNo { get; set; }

    }
    public class ProductInfoListDet
    {
        public Int64 SNo { get; set; }
        public Int64 FK_Category { get; set; }
        public string MasterProduct { get; set; }
        public Int64 FK_Product { get; set; }
        public string Product { get; set; }
        public string SLNo { get; set; }
        public string BindProduct { get; set; }
        public string SerchSerialNo { get; set; }
        public string ComplaintProduct { get; set; }
        public string Warranty { get; set; }
        public string ServiceWarrantyExpireDate { get; set; }
        public string ReplacementWarrantyExpireDate { get; set; }
        public Int64 ID_CustomerWiseProductDetails { get; set; }
        public string ServiceWarrantyExpired { get; set; }
        public string ReplacementWarrantyExpired { get; set; }
    }

    public class PartservicereplacedList
    {
        public Int64 ID_Services { get; set; }
        public string Service { get; set; }

    }
    public class ChangemodeDetails : CommonAPIR
    {
        public List<ChangemodeDetailsList> ChangemodeDetailsList { get; set; }
    }
    public class ChangemodeDetailsList
    {
        public Int64 ID_Mode { get; set; }
        public string ModeName { get; set; }

    }
    public class ReplaceProductdetails : CommonAPIR
    {
        public List<ReplaceProductdetailsList> ReplaceProductdetailsList { get; set; }
    }
    public class ReplaceProductdetailsList
    {
        public Int64 ID_OLD_Product { get; set; }
        public string OLD_Product { get; set; }
        public Int32 SPDOldQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal ReplaceAmount { get; set; }
        public string Remarks { get; set; }
        public Int32 ID_Product { get; set; }
        public string Product { get; set; }

    }
    public class PopUpProductdetails : CommonAPIR
    {
        public List<PopUpProductdetailsList> PopUpProductdetailsList { get; set; }
    }
    public class PopUpProductdetailsList
    {
        public Int64 ID_Product { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MRPs { get; set; }
        public string MRP1R { get; set; }
        public string SalesPrice1R { get; set; }
        public string SalePrice { get; set; }
        public string CurrentStock1R { get; set; }
        public string StockId { get; set; }
        public string TaxAmount { get; set; }
        public string StandbyStock { get; set; }
        public string TotalCount { get; set; }

    }
    public class PopUpSerchCritrea : CommonAPIR
    {
        public List<PopUpSerchCritreaList> PopUpSerchCritreaList { get; set; }
    }
    public class PopUpSerchCritreaList
    {
        public string CritreaValue { get; set; }
        public string Name { get; set; }


    }
    public class FollowUpPaymentMethod : CommonAPIR
    {
        public List<FollowUpPaymentMethodList> FollowUpPaymentMethodList { get; set; }
    }
    public class FollowUpPaymentMethodList
    {
        public Int64 ID_PaymentMethod { get; set; }
        public string PaymentName { get; set; }

    }
    public class SubproductDetails : CommonAPIR
    {
        
        public List<SubproductDetailsList> SubproductDetailsList { get; set; }
    }
    public class ProductSubDetails
    {
        public Int64 FK_Product { get; set; } 
        public string ReplacementWarrantyExpireDate { get; set; }
        public string ID_CustomerWiseProductDetails { get; set; }
    }
    public class SubproductDetailsList
    {
        public Int64 ID_MasterProduct { get; set; }
        public string MainProduct { get; set; }
        public Int64 ID_Product { get; set; }

        public string Componant { get; set; }
        public string WarrantyMode { get; set; }
        public decimal ProductAmount { get; set; }

        public string ReplceMode { get; set; }

        public Int64 FK_Stock { get; set; }
        public Int64 ID_CustomerWiseProductDetails { get; set; }

    }

    public class UpdateServiceFollowUp:CommonAPIR
    {
        public string Message { get; set; }
    }
    public class ServiceFollowUpList
    {
        public string BankKey { get; set; }
        public string FK_Customerserviceregister { get; set; }
        public string UserAction { get; set; }
        public string CustomerNotes { get; set; }
        public string EmployeeNote { get; set; }
        public string StartingDate { get; set; }
        public string ServiceAmount { get; set; }
        public string ProductAmount { get; set; }
        public string NetAmount { get; set; }
        public string TotalAmount { get; set; }
        public string ComponentCharge { get; set; }
        public string OtherCharge { get; set; }
        public string ReplaceAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string  ServiceCharge { get; set; }
        public string FK_ActionType { get; set; }
        public string FK_NextAction { get; set; }
        public string  TotalSecurityAmount { get; set; }
        public List<ServiceDetails> ServiceDetails { get; set; }
        public List<ProductDetails> ProductDetails { get; set; }
        public List<AttendedEmployeeDetails> AttendedEmployeeDetails { get; set; }
        public List<PaymentDetails> PaymentDetail { get; set; }
        public List<Actionproductdetails> Actionproductdetails { get; set; }
        public List<ServiceIncentive> ServiceIncentive { get; set; }
        public string FK_Employee { get; set; }
        public List<OtherCharges> OtherCharges { get; set; }
        public string FK_BillType { get; set; }
        public string FK_NextActionLead { get; set; }
        public string FK_ActionTypeLead { get; set; }
        public string FK_EmployeeLead { get; set; }
        public string NextActionDateLead { get; set; }
       public List<ProductSubDetails> ProductSubDetails { get; set; }
       public string ID_CustomerServiceRegisterProductDetails { get; set; }
       
    }
    public class OtherCharges

    {
        public long ID_OtherChargeType { get; set; }

        public Int16 OctyTransType { get; set; }
        public string OctyName { get; set; }
        public decimal OctyAmount { get; set; }

        public long TransTypeID { get; set; }
    }
        public class ServiceIncentive

    {
        public long ID_Service { get; set; }
        public long ID_MasterProduct { get; set; }
        public Int16 ServiceMod  { get; set; }
         public decimal ServiceCost  { get; set; }
        public decimal ServiceTax  { get; set; }
         public decimal ServiceNetAmount  { get; set; }
    }

        public class Actionproductdetails

    {
       public long ID_Product { get; set; }
        public long ID_NextAction { get; set; }
        public long ID_NextActionLead { get; set; }
        public long FK_ActionType { get; set; }
        public long FK_EmployeeAssign { get; set; }
        public string NextActionDate { get; set; }
        public decimal SecurityAmount { get; set; }
        public decimal OfferPrice { get; set; }
        public long ID_CustomerWiseProductDetails { get; set; }
        public long FK_ProductNumberingDetails { get; set; }

    }

        public class ServiceDetails
    {
        public long SlNo { get; set; }
        public long? ID_Product { get; set; }
        public long? FK_Product { get; set; }
        public long ID_Services { get; set; }
        public long ID_ComplaintList { get; set; }
        public long ID_CustomerWiseProductDetails { get; set; }
        public long FK_ProductNumberingDetails { get; set; }
        public string SubProduct { get; set; }
        public string Product { get; set; }
        public string Remarks { get; set; }
        public string Service { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal ServiceTaxAmount { get; set; }
        public decimal ServiceNetAmount { get; set; }
        public Int32 ServiceType { get; set; }
        public long ID_Mode { get; set; }
        public string ModeName { get; set; }
        public Int64 FK_Brand { get; set; }
        public Int64 FK_SubCategory { get; set; }
        public Int64 FK_Category { get; set; }

    }
    public class ProductDetails
    {
        public long SlNo { get; set; }
        public long ID_MasterProduct { get; set; }
        public long ID_Product { get; set; }

        public long ID_WarrantyMode { get; set; }
        public long ID_ReplaceMode { get; set; }
        public decimal ProductAmount { get; set; }

        public long FK_Stock { get; set; }

        public long ID_CustomerWiseProductDetails { get; set; }
        public long StockID { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReplaceAmount { get; set; }
        public short ID_Mode { get; set; }
        public string ModeName { get; set; }

        public long RpdProductId { get; set; }
        public decimal RpdMRP { get; set; }
        public decimal RpdSalePrice { get; set; }
        public long RpdStockId { get; set; }
        public decimal RpdTaxAmount { get; set; }
        public string RpdProductName { get; set; }
        public decimal RpdProductQty { get; set; }
        public decimal RpdProductAmount { get; set; }

        public decimal Amount { get; set; }
        public string Product { get; set; }
        public string Remarks { get; set; }
        public long? EmpCode { get; set; }
        public long ID_OLD_Product { get; set; }
        public string OLD_Product { get; set; }
        public decimal SPDOldQuantity { get; set; }
        public decimal SPDSalesPrice { get; set; }

        public decimal SPDMRP { get; set; }
        public decimal SPDTaxAmount { get; set; }
        public long Stock { get; set; }
    }
    public class AttendedEmployeeDetails
    {
        public long ID_CustomerServiceAssignee { get; set; }

        public long ID_Employee { get; set; }
        public string Employee { get; set; }
        public string EmployeeType { get; set; }
        public string Department { get; set; }
        public int Attend { get; set; }

        public long ID_CSAEmployeeType { get; set; }


    }
    public class MoreComponentDetails:CommonAPIR
    {
        public List<MoreComponentDetailsList> MoreComponentDetailsList { get; set; }
    }
    public class MoreComponentDetailsList
    {
        public string FK_Product { get; set; }
        public string ProductName { get; set; }
    }
    public class MainProductDetails : CommonAPIR
    {
        public List<MainProductDetailsList> MainProductDetailsList { get; set; }
    }
    public class MainProductDetailsList
    {
        public long ID_Product { get; set; }
        public string ProdName { get; set; }
        public string ProdShortName { get; set; }

    }
    #endregion
}
