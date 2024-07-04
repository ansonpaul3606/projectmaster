using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ServiceListModel
    {
        public class ServiceListModelDetailsInput
        {
            public long ReferenceID { get; set; }
            public int CheckList { get; set; }
            public int Detailed { get; set; }
            public string TransMode { get; set; }
        }
        public class PageSettingsDetalsInput
        {
            public string PSModule { get; set; }
            public string PSPage { get; set; }
            //public Int16 PSField { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 Mode { get; set; }

        }
        public class ServiceListModelDetailsData
        {
            public string TransMode { get; set; }
            public DataTable DtList { get; set; }
            public DataTable DtDetailsList { get; set; }
            public DataTable DtProjectList1 { get; set; }
            public DataTable DtProjectList2 { get; set; }
            public DataTable DtProjectList3 { get; set; }
            public IEnumerable<TimeLineChartList> TimeLineData { get; set; }
        }
        public class ServiceListModelDetails
        {
            public long ID_NextActionLead { get; set; }
            public string Name { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Post { get; set; }
            public string Area { get; set; }
            public string District { get; set; }
            public string State { get; set; }
            public string PinCode { get; set; }
            public string Landmark { get; set; }
            public string GSTIN { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string PhoneNo { get; set; }
            public string TransMode { get; set; }
            public DataTable DtTransHistryList { get; set; }
            public DataTable DtEMIList { get; set; }

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


        public class ServiceListView
        {
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Brand { get; set; }
           
            public long ID_ServiceFollowUp { get; set; }
            public long FK_ServiceFollowUp { get; set; }
            public long SlNo { get; set; }
            public string TicketNo { get; set; }
            public string CusName { get; set; }
            public string Mobile { get; set; }
            public string NxtActnName { get; set; }
            public Int32 ID_NextAction { get; set; }
            public string CurrentStatus { get; set; }
            public long Branch { get; set; }
            public int Priority { get; set; }

            public long FK_Customer { get; set; }
            public long FK_CustomerOthers { get; set; }
            public string Service { get; set; }
            public string Complaint { get; set; }

            public string EmployeeNotes { get; set; }
            public string RegNo { get; set; }

            public string ClosedDate { get; set; }
            public long FK_Employee { get; set; }
            public List<Branch> BranchList { get; set; }

            
            public List<ChangeMode> StatusModeList { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }

            public long FK_District { get; set; }
            public string District { get; set; }
            public long? FK_Place { get; set; }

            public string Place { get; set; }
            public long? FK_Post { get; set; }
            public string Post { get; set; }


            public string Location { get; set; }
            public string Email { get; set; }

            public List<ComplaintproductDetails> ComplaintproductDetails { get; set; }
            public List<AttendedEmployeeDetails> AttendedEmployeeDetails { get; set; }

            public string Product { get; set; }
            public long ProductID { get; set; }
            public long ID_Product { get; set; }
            public long FK_Product { get; set; }
          // [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]

            public string VisitedDateAfter { get; set; }
            public string VisitedDate { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
            public DateTime StartingDate { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
            public string RegisteredOn { get; set; }

            public string LastFollowUpDate { get; set; }
            public string CustomerNote { get; set; }
            public long FK_Services { get; set; }
            public string EmployeeNote { get; set; }

            public List<AttendedMode> AtMode { get; set; }
            public List<ServiceDetails> ServiceDetails { get; set; }

            public List<ProductDetails> ProductDetails { get; set; }
            //   public List<ImageUpladDetails> ImageUpladDetails { get; set; }

            public List<DocUploadDetails> DocUploadDetails { get; set; }
            public string Servicefromtime { get; set; }
            public string Servicetotime { get; set; }
            public Int32 AttendedMode { get; set; }

            public decimal ServiceAmount { get; set; }
            public decimal ReplaceAmount { get; set; }

            public decimal ProductAmount { get; set; }
            public decimal TotalAmount { get; set; }

            public string SubProduct { get; set; }
            public string Remarks { get; set; }
            public decimal DiscountAmount { get; set; }

            public decimal NetAmount { get; set; }

            public List<NextAction> NextActionMode { get; set; }

            public Int32 NextAcMode { get; set; }

            public long FK_Customerserviceregister { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int64 TotalCount { get; set; }
            public List<ChangeMode> ChangeMode { get; set; }
            public byte? Attend { get; set; }
            public long ReasonID { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
            public String EntrOn { get; set; }
            // public List<PaymentDetails> PaymentDetail { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }

            public List<PaymentMethodModel.PaymentMethodView> PaymentViewpayment { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public List<Category> CategoryDetailsList { get; set; }
            public List<SubCategory> SubCategoryList { get; set; }
            public List<Complaints> ComplaintsList { get; set; }
            public List<Brand> BrandList { get; set; }

            public List<Complaint> ComplaintList { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public Int32 CategoryNameID { get; set; }
            public string ProductName { get; set; }
            public Int64 ProductNameID { get; set; }

            public List<Servicetype> Servicetype { get; set; }
            public string TransMode { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public long BillTypeID { get; set; }
            public string BillType { get; set; }
            public List<ActionTypes> Actntyplists { get; set; }
            public List<LeadNextAction> LeadNextActionlists { get; set; }
            public List<EmployeeInfo> EmployeeInfoList { get; set; }

            public long ID_NextActionLead { get; set; }
            public int FK_ActionType { get; set; }

            public long FK_EmployeeAssign { get; set; }
            public string AssignEmp { get; set; }
            public string NextActionDateLead { get; set; }
            public int FK_ActionStatus { get; set; }
            public long NxtActnStageLead { get; set; }

            public List<ProductSubDetails> ProductSubDetails { get; set; }
            public decimal ComponentCharge { get; set; }
            public decimal ServiceCharge { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<Actionproductdetails> Actionproductdetails { get; set; }
            public decimal OtherCharge { get; set; }

            public long ID_CustomerServiceRegisterProductDetails { get; set; }
            public decimal TotalSecurityAmount { get; set; }
            public string CusNumber { get; set; }
            public string SerielNo { get; set; }
            public DateTime? TicketDate { get; set; }
            public List<ServiceIncentive> ServiceIncentiveDetails { get; set; }
            public long? LastID { get; set; }
        }

        public class LeadNextAction
        {
            public long ID_NextActionLead { get; set; }
            public string NxtActnNameLead { get; set; }
            public long NxtActnStageLead { get; set; }


        }
        public class ServiceIncentive
        {
            public long ID_Service { get; set; }
            public long ID_MasterProduct { get; set; }
            public long ServiceMod { get; set; }
            public decimal ServiceCost { get; set; }
            public decimal ServiceTax { get; set; }
            public decimal ServiceNetAmount { get; set; }
            public string ServiceRemarks { get; set; }
        }
        public class ActionTypes
        {
            public int FK_ActionType { get; set; }
            public string ActnTypeName { get; set; }
        }
        public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }


        }
        public class PaymentDetailsOLD
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public long ID_TransactionDetails { get; set; }
            public string TransType { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string PaymentMethodS { get; set; }
            public decimal PAmount { get; set; }
            public string Refno { get; set; }
        }
        public class Category
        {
            public Int64 CategoryID { get; set; }
            public string CategoryName { get; set; }

        }
        public class SubCategory
        {
            public Int64 SubCategoryID { get; set; }
            public string SubCategoryName { get; set; }

        } public class Brand
        {
            public Int64 BrandID { get; set; }
            public string BrandName { get; set; }

        } 
        public class CategoryList
        {
            public Int32 CategoryNameID { get; set; }
            public string CategoryName { get; set; }

        }

        public class Servicetype
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }

        public class ChangeMode
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class ServiceDetails
        {

            public long? ID_Product { get; set; }

            public long ID_Services { get; set; }

            public string Product { get; set; }
            public string Warranty { get; set; }
            public long ID_ComplaintList { get; set; }
            public string Description { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            public long FK_ProductNumberingDetails { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Brand { get; set; }

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


        public class ProductDetails
        {
            public long ID_MasterProduct { get; set; }
            public long ID_Product { get; set; }
            public string Componant { get; set; }
            public long ID_WarrantyMode { get; set; }
            public long ID_ReplaceMode { get; set; }
            public decimal Quantity { get; set; }

            public decimal ProductAmount { get; set; }

            public long FK_Stock { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }



        }




        public class ImageUpladDetails
        {
            public string ImagePath { get; set; }

            public string ImagePathName { get; set; }

            public long ID_Product { get; set; }
        }
        public class DocUploadDetails
        {
            public string DocPath { get; set; }

            public string DocPathName { get; set; }

            public long ID_Product { get; set; }
        }
        public class AttendedMode
        {
            public long ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }


        }

        public class NextAction
        {
            public string NxtActnName { get; set; }
            public Int32 ID_NextAction { get; set; }
            public long FK_ActionStatus { get; set; }


        }
        public class ComplaintproductDetails
        {
            public long SLNO { get; set; }
            public string Product { get; set; }
            public string Category { get; set; }
            public string ProductComplaint { get; set; }
            public string ServiceName { get; set; }
            public string ProductDescription { get; set; }
            public string ProductStatus { get; set; }
            public long ID_Product { get; set; }
            public long FK_Category { get; set; }
            public string SerielNo { get; set; }
            public long? ID_ComplaintList { get; set; }            
            public long FK_Brand { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Complaint { get; set; }
            public string SubCategory { get; set; }
            public string Brand { get; set; }
        }


        public class CustomerHistoryOut
        {
            public long SLNO { get; set; }

            public string Product { get; set; }
            public string ProductComplaint { get; set; }

            public string ProductDescription { get; set; }
            public string ProductStatus { get; set; }
            public string TicketDate { get; set; }
            public string FollowUpDate { get; set; }
            public string TicketNo { get; set; }
            public long ID_Product { get; set; }
        }
        public class ProductHistoryOut
        {
            public long SLNO { get; set; }

            public string Product { get; set; }
            public string ProductComplaint { get; set; }

            public string ProductDescription { get; set; }
            public string ProductStatus { get; set; }
            public long ID_Product { get; set; }

            public string Warrenty { get; set; }

            public string ModelNo { get; set; }
            public string WarrentyDate { get; set; }

            public string ProductImage { get; set; }

            public string ProductImageName { get; set; }


            public HttpPostedFileBase ProImg { get; set; }
        }
        public class ProductHistoryOut1
        {
            public long SLNO { get; set; }
            public string CurrentStatus { get; set; }
            public string Service { get; set; }
            public string Complaint { get; set; }

            public string EmployeeNotes { get; set; }
            public string RegNo { get; set; }
            public string TicketNo { get; set; }
            public string ClosedDate { get; set; }
            public long FK_Employee { get; set; }
        }
        public class InputData
        {
            public string TicketNo { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long ID_Customerserviceregister { get; set; }
            public long ID_CustomerserviceregisterProductDetails { get; set; }
        }
        public class InputDataComplaint
        {
            public string TicketNo { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long ID_Customerserviceregister { get; set; }
        }
        public class GetData
        {
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_Customerserviceregister { get; set; }
            public long PageMode { get; set; }
        }

        public class GetDataProductComponent
        {
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long? FK_Product { get; set; }
            public long? FK_Category { get; set; }
            //public long FK_Customerserviceregister { get; set; }
            //public long PageMode { get; set; }
        }

        public class GetDataservices
        {
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_Services { get; set; }

            //public long PageMode { get; set; }
        }
        public class ChangeModeInput
        {
            public int Mode { get; set; }

        }

        public class ServiceModeInput
        {
            public int Mode { get; set; }

        }
        public class ProductInputData
        {
            public long FK_Product { get; set; }
            public long FK_Customer { get; set; }
            public long FK_CustomerOthers { get; set; }
            public long Mode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class CustomerInputData
        {
            public long FK_Customer { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class ProductServicesInput
        {
            public string TransMode { get; set; }
            public long FK_ProductWiseServices { get; set; }
            public long FK_Product { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_ServiceFollowUp { get; set; }

        }
        public class ProductServicesPutput
        {
            public long FK_Customer { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Machine { get; set; }
            public string Service { get; set; }
            public decimal ServiceCost { get; set; }
            public decimal ServiceTaxAmount { get; set; }
            public decimal ServiceNetAmount { get; set; }
            public string Remarks { get; set; }
            public string SubProduct { get; set; }
            public long? ID_Product { get; set; }
            public long ID_Services { get; set; }

        }
        public class MoreServicesPutput
        {

            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Machine { get; set; }
            public string Service { get; set; }
            public decimal ServiceCost { get; set; }
            public string Remarks { get; set; }
            public long? ID_Product { get; set; }
            public long? FK_Customer { get; set; }
            public long ID_Services { get; set; }

        }
        public class ProductDetailsInput
        {
            public string TransMode { get; set; }
            public long FK_ServiceFollowUpProductDetails { get; set; }
            public long FK_Product { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_ServiceFollowUp { get; set; }


        }
        public class ProductDetailsOutPut
        {
            public string OLD_Product { get; set; }
            public long FK_ServiceFollowUpProductDetails { get; set; }
            public long FK_Product { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_ServiceFollowUp { get; set; }
            public long ID_OLD_Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }

            public decimal ReplaceAmount { get; set; }
            public long ID_ActionType { get; set; }
            public long? EmpCode { get; set; }
            public long? StockID { get; set; }
            public string Product { get; set; }
            public long ID_Product { get; set; }
            public decimal SPDOldQuantity { get; set; }

            public decimal SPDMRP { get; set; }
            public decimal SPDTaxAmount { get; set; }
            public long Stock { get; set; }
        }

        public class MoreProductDetailsOutPut
        {
            public string Product { get; set; }
            // public long FK_ServiceFollowUpProductDetails { get; set; }
            public long FK_Product { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            // public Int64 FK_ServiceFollowUp { get; set; }
            public long ID_Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }
            //  public string ModelNo { get; set; }
            public decimal ReplaceAmount { get; set; }
            public long ID_ActionType { get; set; }
            public long? EmpCode { get; set; }
        }
        public class Branch
        {
            public long ID_Branch { get; set; }

            public string BranchName { get; set; }
        }

        
        public class TicketView
        {
            public long SlNo { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_Customerserviceregister { get; set; }
            // public string Branch { get; set; }
            public string Ticket { get; set; }
            public string TicketDate { get; set; }
            public string Customer { get; set; }
            public string Mobile { get; set; }
            public string Priority { get; set; }

            public string CurrentStatus { get; set; }
            public string Product { get; set; }
            // public string Complaint { get; set; }

            public bool Assigned { get; set; }

            public string AssignedDate { get; set; }
            public string DueDays { get; set; }
            public long ProductID { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long ID_ServiceFollowUp { get; set; }
            public long ID_Customerserviceregister { get; set; }
            public long ID_Master { get; set; }
            public string TransMode { get; set; }
            public long ID_CustomerserviceregisterProductDetails { get; set; }
        }

        public class TicketInput
        {
            public long FK_Branch { get; set; }
            public string Product { get; set; }
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            // public long FK_ComplaintType { get; set; }
            public Int16 Status { get; set; }
            public string CurrentStatus { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string TicketNumber { get; set; }
            public string Customer { get; set; }
            // public string Mobile { get; set; }
            public string Content { get; set; }
            public long FK_Company { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Machine { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }

        public class ServicefollowupselectID
        {
            public long FK_ServiceFollowUp { get; set; }

            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int PageMode { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }

        }




        public class UpdateServiceList
        {
            public byte UserAction { get; set; }

            public long ID_ServiceFollowUp { get; set; }
            public long FK_Customerserviceregister { get; set; }
            public long ID_CustomerServiceRegisterProductDetails { get; set; }


            //public string CustomerNotes { get; set; }

            //public string EmployeeNote { get; set; }
            public DateTime StartingDate { get; set; }

            public decimal TotalSecurityAmount { get; set; }
            public decimal ComponentCharge { get; set; }
            public decimal ServiceCharge { get; set; }
            public decimal OtherCharge { get; set; }
            public decimal TotalAmount { get; set; }

            public decimal DiscountAmount { get; set; }

            public long FK_Company { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }


            public long FK_Machine { get; set; }
            public string TransMode { get; set; }

            public bool Debug { get; set; }

            public string CustomerProductdetails { get; set; }
            public string Subproductreplacedetails { get; set; }
            public string AttendedEmployeeDetails { get; set; }
            public string Actionproductdetails { get; set; }
            public string OtherChgDetails { get; set; }

            public long FK_BillType { get; set; }
            public string PaymentDetail { get; set; }
            public string ServiceIncentiveDetails { get; set; }
            public long? LastID { get; set; }
        }


        public class DeleteServiceFollowUp
        {
            public Int64 ID_ServiceFollowUp { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }


        public static string _SelectTickets = "ProEmployeeWiseTicketSelect";
        public APIGetRecordsDynamic<TicketView> GetTicketDetails(TicketInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<TicketView, TicketInput>(companyKey: companyKey, procedureName: _SelectTickets, parameter: input);
        }
        public static string _selectProcedureName = "ProEmployeeWiseTicketSelectDetailed";
        public APIGetRecordsDynamic<ServiceListView> GetCustomerData(InputData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceListView, InputData>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<ServiceListView> GetCustomerProductData(InputData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceListView, InputData>(companyKey: companyKey, procedureName: "ProEmployeeProductDetailes", parameter: input);

        }
        public APIGetRecordsDynamic<ProductHistoryOut> GetProductPreviousData(ProductInputData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductHistoryOut, ProductInputData>(companyKey: companyKey, procedureName: "ProProductWisePreviousComplaintHistory", parameter: input);

        }

        public APIGetRecordsDynamic<ProductHistoryOut1> GetProductPreviousData1(ProductInputData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductHistoryOut1, ProductInputData>(companyKey: companyKey, procedureName: "ProProductWisePreviousComplaintHistory", parameter: input);

        }
        public APIGetRecordsDynamic<CustomerHistoryOut> GetCustomerPreviousData(CustomerInputData input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerHistoryOut, CustomerInputData>(companyKey: companyKey, procedureName: "ProCustomerWisePreviousComplaintHistory", parameter: input);

        }
        public APIGetRecordsDynamic<MoreServicesPutput> GetProductServiceDetails1(GetDataservices input, string companyKey)
        {
            return Common.GetDataViaProcedure<MoreServicesPutput, GetDataservices>(companyKey: companyKey, procedureName: "proServicesSelectserviceclose", parameter: input);

        }

        public APIGetRecordsDynamic<ProductServicesPutput> GetProductServiceDetails(GetData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductServicesPutput, GetData>(companyKey: companyKey, procedureName: "ProServiceFollowUpDetailSelect", parameter: input);

        }

        public APIGetRecordsDynamic<ProductDetailsOutPut> GetProductDetails(GetData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductDetailsOutPut, GetData>(companyKey: companyKey, procedureName: "ProServiceFollowUpDetailSelect", parameter: input);

        }
        public APIGetRecordsDynamic<MoreProductDetailsOutPut> GetMoreProductDetails(GetDataProductComponent input, string companyKey)
        {
            return Common.GetDataViaProcedure<MoreProductDetailsOutPut, GetDataProductComponent>(companyKey: companyKey, procedureName: "ProMoreProductDetailSelect", parameter: input);

        }

        public APIGetRecordsDynamic<AttendedEmployeeDetails> GetAttendedEmployeeDetails(GetData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AttendedEmployeeDetails, GetData>(companyKey: companyKey, procedureName: "ProServiceFollowUpDetailSelect", parameter: input);

        }
        public Output UpdateServiceListData(UpdateServiceList input, string companyKey)
        {
            return Common.UpdateTableData<UpdateServiceList>(parameter: input, companyKey: companyKey, procedureName: "ProServiceFollowUpUpdate");
        }

        public APIGetRecordsDynamic<ServiceListModel.ServiceListView> GetServicefollowupData(ServicefollowupselectID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceListModel.ServiceListView, ServicefollowupselectID>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);
        }

        public class SubtabledataSelect
        {
            public long FK_ServiceFollowUp { get; set; }

            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int PageMode { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
        }

        public class GetPaymentin
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64? FK_Master { get; set; }


        }
        public class ServiceNetAndTax
        {
            public long FK_ServiceType { get; set; }
            public decimal TaxableAmount { get; set; }
        }
        public class ServiceNetAndTaxInput
        {
            public long FK_ServiceType { get; set; }
            public decimal TaxableAmount { get; set; }
        }
        public class ServiceNetAndTaxOutput
        {
            public decimal TaxAmount { get; set; }
            public decimal NetAmount { get; set; }
        }

        public APIGetRecordsDynamic<ServiceDetails> GetServicecostData(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceDetails, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }

        public APIGetRecordsDynamic<ProductDetails> GetReplacedProductData(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductDetails, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }

        public APIGetRecordsDynamic<AttendedEmployeeDetails> GetAttendedEmployeeData(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<AttendedEmployeeDetails, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<ServiceNetAndTaxOutput> GetServicetaxAndNetData(ServiceNetAndTaxInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceNetAndTaxOutput, ServiceNetAndTaxInput>(companyKey: companyKey, procedureName: "ProServiceTaxCalculation", parameter: input);
        }
        public class GetDataRefernceNo
        {
            public string ReferenceNo { get; set; }
            public Int32 Mode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Company { get; set; }

        }
        public class CustomerProductdeatil
        {
            public long FK_Product { get; set; }
            public long FK_Category { get; set; }
            public string Product { get; set; }
            public string SLNo { get; set; }
            public string MasterProduct { get; set; }
            public string ServiceWarrantyExpireDate { get; set; }
            public string ReplacementWarrantyExpireDate { get; set; }
            public string Warranty { get; set; }
            //public long FK_ProductComplaint { get; set; }
            public int Check { get; set; }
            // public string Description { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
        }
        public class Complaint
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set; }
        }
        public class Complaints
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set; }
        }
        public APIGetRecordsDynamic<CustomerProductdeatil> GetCustomerProductDetails(GetDataRefernceNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerProductdeatil, GetDataRefernceNo>(companyKey: companyKey, procedureName: "ProGetCustomerSubProductInfo", parameter: input);

        }

        public class CustomerSubproductViewTable
        {

            public string ReferenceNo { get; set; }
            public long SNo { get; set; }
            public Int32 Mode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Category { get; set; }
            public string Product { get; set; }
            public Int64 FK_Company { get; set; }
            public string MasterProduct { get; set; }
            public string SLNo { get; set; }

            public string ServiceWarrantyExpireDate { get; set; }
            public string ReplacementWarrantyExpireDate { get; set; }
            public string Warranty { get; set; }
            //public List<CustomerProductdetails> CustomerProductdetails { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }

            public int ReplacementWarrantyExpired { get; set; }
            public int ServiceWarrantyExpired { get; set; }
            public long FK_ProductNumberingDetails { get; set; }

        }
        public class CustomerSubproductView
        {

            public string ReferenceNo { get; set; }
            public Int32 Mode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Company { get; set; }
            public string MasterProduct { get; set; }
            public List<CustomerProductdetails> CustomerProductdetails { get; set; }
        }
        public class CustomerProductdetails
        {
            public string MasterProduct { get; set; }
            public long FK_Product { get; set; }
            public long FK_Category { get; set; }
            public string Product { get; set; }
            public string SLNo { get; set; }
            public long SNo { get; set; }

            public string ServiceWarrantyExpireDate { get; set; }
            public string ReplacementWarrantyExpireDate { get; set; }
            public string Warranty { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            //public long FK_ProductComplaint { get; set; }
            public int Check { get; set; }
            // public string Description { get; set; }
            public int ReplacementWarrantyExpired { get; set; }
            public int ServiceWarrantyExpired { get; set; }
            public string Description { get; set; }
            public long FK_ComplaintList { get; set; }
            public long FK_Subcategory { get; set; }
            public long FK_Brand { get; set; }
            public long FK_ProductNumberingDetails { get; set; }
        }

        public APIGetRecordsDynamic<CustomerSubproductViewTable> GetCustomerProductDetailss(GetDataRefernceNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerSubproductViewTable, GetDataRefernceNo>(companyKey: companyKey, procedureName: "ProGetCustomerSubProductInfo", parameter: input);

        }

        public class ProductSubDetailstobind
        {
            public List<ProductSubDetails> ProductSubDetails { get; set; }


        }
        public class ProductSubDetails
        {
            public long FK_Product { get; set; }
            public DateTime? ServiceWarrantyExpireDate { get; set; }
            public DateTime? ReplacementWarrantyExpireDate { get; set; }

        }
        public class ProductSubDetailstobindinput
        {
            public string ProductSubDetails { get; set; }
            public long FK_Employee { get; set; }
        }

        //public Output ProductSubdetailsData(ProductSubDetailstobindinput input, string companyKey)
        //{
        //    return Common.UpdateTableData<ProductSubDetailstobindinput>(parameter: input, companyKey: companyKey, procedureName: "ProProductsubdetails");
        //}

        public class Subproductreplacedetails
        {
            public long ID_MasterProduct { get; set; }
            public string MainProduct { get; set; }
            public List<Subproductreplacedetailss> Subproductreplacedetailss { get; set; }

        }

        public class Subproductreplacedetailss
        {
            public long ID_MasterProduct { get; set; }
            public string MainProduct { get; set; }
            public long ID_Product { get; set; }
            public string Componant { get; set; }
            public decimal ProductAmount { get; set; }

            public long FK_Stock { get; set; }
            public long WarrantyMode { get; set; }
            public decimal Quantity { get; set; }

            public long ReplceMode { get; set; }
            public long WarrantyType { get; set; }
            public long ReplceType { get; set; }


        }

        public class Subproductrepalcedetail
        {
            public long ID_MasterProduct { get; set; }

            public long ID_Product { get; set; }

            public decimal ProductAmount { get; set; }

            public long FK_Stock { get; set; }
            public long ID_WarrantyMode { get; set; }

            public long ReplceMode { get; set; }
        }
        public class WarrantMode
        {
            public long ID_WarrantyMode { get; set; }
            public string WarrantyModeName { get; set; }


        }

        public class ReplaceMode
        {
            public long ID_ReplaceMode { get; set; }
            public string ReplaceModeName { get; set; }


        }
        public class OtherCharges
        {
            public long SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public Int64 OctyTransType { get; set; }
            public string OctyName { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransType { get; set; }
            public Int64 TransTypeID { get; set; }
        }

        public class Actionproductdetails
        {
            public long ID_Product { get; set; }
            public long ID_NextAction { get; set; }

            public long ID_NextActionLead { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_EmployeeAssign { get; set; }

            public DateTime? NextActionDate { get; set; }
            public bool ProvideStandBy { get; set; }
            public string CustomerNote { get; set; }
            public string EmployeeNote { get; set; }
            public decimal SecurityAmount { get; set; }
            public decimal OfferPrice { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            public long FK_ProductNumberingDetails { get; set; }
            public decimal? PQuantity { get; set; }
            public long EmployeeID { get; set; }
        }
        public class FollowpReturnDetails
        {
            public string ReferenceNo { get; set; }
            public Int32 Mode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Category { get; set; }
            public string Product { get; set; }
            public Int64 FK_Company { get; set; }
            public string MasterProduct { get; set; }
            public string SLNo { get; set; }
            public long SNo { get; set; }
            public string ServiceWarrantyExpireDate { get; set; }
            public string ReplacementWarrantyExpireDate { get; set; }
            public string Warranty { get; set; }
            //public List<CustomerProductdetails> CustomerProductdetails { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            public long FK_ProductNumberingDetails { get; set; }

            public int ReplacementWarrantyExpired { get; set; }
            public int ServiceWarrantyExpired { get; set; }
            public string Description { get; set; }
            public long FK_ComplaintList { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Brand { get; set; }
        }
        public class ActionTakenReturnResult
        {
            public long ID_Product { get; set; }
            public string Product { get; set; }
            public long ID_NextAction { get; set; }

            public long ID_NextActionLead { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_EmployeeAssign { get; set; }
            public string EmpName { get; set; }

            public DateTime? NextActionDate { get; set; }
            public bool ProvideStandBy { get; set; }
            public string CustomerNote { get; set; }
            public string EmployeeNote { get; set; }
            public decimal SecurityAmount { get; set; }
            public decimal OfferPrice { get; set; }
            public long FK_CustomerWiseProductDetails { get; set; }
            public decimal? PQuantity { get; set; }
            public long EmployeeID { get; set; }
            public string EmployeeName { get; set; }



        }
        public class GetSubTableSales
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class InputMaindetails
        {
            public long? FK_ServiceFollowUp { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int PageMode { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
        }

        public class ProductServiceCostdetails
        {
            public long ID_MasterProduct { get; set; }
            public string MainProduct { get; set; }
            public List<ProductServiceCostdetailss> ProductServiceCostdetailss { get; set; }

        }

        public class ProductServiceCostdetailss
        {
            public long ID_ServiceFollowUp { get; set; }
            public long ID_MasterProduct { get; set; }
            public long FK_Service { get; set; }
            public string MainProduct { get; set; }
            public string Service { get; set; }
            public long ServiceType { get; set; }
            public decimal ServiceCost { get; set; }
            public decimal ServiceTax { get; set; }
            public decimal ServiceNetAmount { get; set; }
            public string ServiceRemarks { get; set; }
        }

        public APIGetRecordsDynamic<Subproductreplacedetailss> ProductSubdetailsData(ProductSubDetailstobindinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Subproductreplacedetailss, ProductSubDetailstobindinput>(companyKey: companyKey, procedureName: "ProProductsubdetails", parameter: input);

        }


        public APIGetRecordsDynamic<FollowpReturnDetails> GetServiceProductReturnData(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<FollowpReturnDetails, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }
        public APIGetRecordsDynamic<Subproductreplacedetailss> GetServiceProductReturnTab2Data(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<Subproductreplacedetailss, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ActionTakenReturnResult> GetServiceActionTakenResultData(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionTakenReturnResult, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ServiceListView> GetServiceMainReturnData(InputMaindetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceListView, InputMaindetails>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ProductServiceCostdetailss> GetServiceProductReturnService(SubtabledataSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductServiceCostdetailss, SubtabledataSelect>(companyKey: companyKey, procedureName: "ProServiceFollowUpSelect", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetUniversalSearchDetals(ServiceListModelDetailsInput input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<ServiceListModelDetailsInput>(companyKey: companyKey, procedureName: "ProUniversalSearchDetals", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetPageSettingsDetals(PageSettingsDetalsInput input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<PageSettingsDetalsInput>(companyKey: companyKey, procedureName: "ProGetPageSettingsDetails", parameter: input);

        }
    }

}