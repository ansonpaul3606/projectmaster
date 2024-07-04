using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PickUpandDelivery
    {
        public class DeliveryPickupassignView
        {

            public long Customerserviceregister { get; set; }
            public long ID_ProductDeliveryFollowUp { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            //public string Employee { get; set; }

            [Required(ErrorMessage = "Please Enter Pickup date")]
            public DateTime Pickupdate { get; set; }

            [Required(ErrorMessage = "Please select date")]
            public DateTime PdAssignedDate { get; set; }
            [Required(ErrorMessage = "Please select time")]
            public string PdAssignedTime { get; set; }

            [Required(ErrorMessage = "Please Enter Pickup time")]
            public string Pickuptime { get; set; }

            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long Branch { get; set; }
            public int Priority { get; set; }
            public string Remarks { get; set; }
            public string CSAstatus { get; set; }
            public string UserCode { get; set; }
            public long ID_NextAction { get; set; }

            public long FK_Employee { get; set; }
            //public string TransMode { get; set; }
            ////public long FK_ProductDelivery { get; set; }
            //public long Mode { get; set; }
            //public bool Details { get; set; }


            public Int64 TotalCount { get; set; }
            public List<Products> Product { get; set; }

            public List<OtherProducts> OtherProducts { get; set; }

            public List<CustomerDetails> customerDetails { get; set; }
            public List<Employeedetails> employeedetails { get; set; }

            public List<Branch> BranchList { get; set; }

            public List<Complaint> ComplaintList { get; set; }

            public List<Department> DepartmentList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<Status> StatusList { get; set; }
            public List<DeliverydetailsOutput> delout { get; set; }
            public List<ProdDetails> Productdetails { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<LeadGenerateModel.EmployeeInfo> EmployeeInfoList { get; set; }
        }
        public class ProductDeliveryFollowUp
        {
            //public long Customerserviceregister { get; set; }
            public long FK_ProductDeliveryAssign { get; set; }

            [Required(ErrorMessage = "Please select Date")]
            public DateTime PdfDeliveryDate { get; set; }

            [Required(ErrorMessage = "Please Select Time")]
            public string PdfDeliveryTime { get; set; }

            public string PdfCustomerNotes { get; set; }

            public string PdfEmployeeNotes { get; set; }

            public int PdfDeliveryCharge { get; set; }

            public int PdfNetAmount { get; set; }
            public long PdfAction { get; set; }


            public string Remarks { get; set; }
            public string CSAstatus { get; set; }
            public long ID_NextAction { get; set; }
            public long FK_Employee { get; set; }
            public List<ProdDetails> Productdetails { get; set; }
            public List<UpdateProductDeliveryFollowUp> UpdateProdel { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public decimal StandByTotalAmount { get; set; }
            public long FK_BillType { get; set; }
            public string TransMode { get; set; }
            public long Pickupstatus { get; set; }
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
        public class DeliveryInput
        {

            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Area { get; set; }
            public string FK_Category { get; set; }

            public long FK_Product { get; set; }
            public string Priority { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public string TicketNo { get; set; }
            public long Mode { get; set; }
            public string Module { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public Int16 Status { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }


        }


        public class UpdateProductDeliveryFollowUp
        {
            public long ID_ProductDeliveryFollowUp { get; set; }
            //public long FK_Customerserviceregister { get; set; }
            public long FK_ProductDeliveryAssign { get; set; }
            
            public DateTime PdfDeliveryDate { get; set; }
            public string PdfDeliveryTime { get; set; }
            public string PdfCustomerNotes { get; set; }
            public string PdfEmployeeNotes { get; set; }
            public int PdfDeliveryCharge { get; set; }
            public int PdfNetAmount { get; set; }
            public long PdfAction { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int16 UserAction { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public string productReplace { get; set; }

            public string CSAstatus { get; set; }
            public long ID_NextAction { get; set; }
            public long FK_Employee { get; set; }
            public decimal StandByAmount { get; set; }
            public string PaymentDetail { get; set; }
            public long FK_BillType { get; set; }
            public string TransMode { get; set; }
            public long Pickupstatus { get; set; }
        }


        

        public class Deliverydetails
        {
            public string TransMode { get; set; }
            public long FK_ProductDelivery { get; set; }
            public long Mode { get; set; }
            public int Details { get; set; }


        }
        public class DeliverydetailsOutput
        {
            public int FK_Master { get; set; }
            public long ID_ProductDelivery { get; set; }
            public long FK_Product { get; set; }
            public string CSRTickno { get; set; }
            public string CSRRegDate { get; set; }
            public string Customer { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Post { get; set; }
            public string Area { get; set; }
            public string District { get; set; }
            public string State { get; set; }
            public string Category { get; set; }
            public string Product { get; set; }
        }

        public class ProdDetails
        {
            public long ID_Product { get; set; }
            public string Selected { get; set; }
            public string ProvideStandBy { get; set; }
            public string ProdName { get; set; }
            public decimal Quantity { get; set; }
            public string Product { get; set; }
            public string SPQuantity { get; set; }
            public string SPAmount { get; set; }
            public string Remarks { get; set; }
            public long Stock { get; set; }
            public long FK_StandByProduct { get; set; }
            public decimal StandByQuantity { get; set; }
            public decimal StandByAmount { get; set; }
            public long FK_EmployeeStock { get; set; }
        }

        public class DeliverydetailsOutput_stby
        {
            public int FK_Master { get; set; }
            public long ID_ProductDelivery { get; set; }
            public long FK_Product { get; set; }
            public string SalBillNo { get; set; }
            public string SalBillDate { get; set; }
            public string Customer { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Post { get; set; }
            public string Area { get; set; }
            public string District { get; set; }
            public string State { get; set; }
            public string Category { get; set; }
            public string Product { get; set; }

            public DateTime AssignedOn { get; set; }


                      
        }

        public class Complaint
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set; }
        }

        public class Products
        {
            public long FK_Customerserviceregisterproductdetails { get; set; }
            public string Productname { get; set; }
            public string ProductComplaint { get; set; }
            public string ProductDescription { get; set; }

        }

        public class OtherProducts
        {
            public long FK_Customerserviceregisterothproductdetails { get; set; }
            public string Company { get; set; }
            public string Product { get; set; }
            public string Complaint { get; set; }
            public string Description { get; set; }

        }

        public class CustomerDetails
        {
            public long FK_Customerserviceregister { get; set; }

            public string FromDate { get; set; }

            public string ToDate { get; set; }

            public string FromTime { get; set; }

            public string ToTime { get; set; }

            public string Customer { get; set; }

            public string Address { get; set; }

            public string Mobile { get; set; }

            public string OtherMobile { get; set; }

            public string Landmark { get; set; }

            public string Ticket { get; set; }
        }


        public static string _SelectEmployee = "ProEmployeeServiceAssign";
        public class EmployeeInput
        {
            public string TransMode { get; set; }
            public long FK_Employee { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class Employee
        {
            public long ID_Employee { get; set; }

            public long EmpID { get; set; }

            [DisplayName("Employee Code")]
            public string EmployeeCode { get; set; }
            [DisplayName("Employee Name")]
            public string EmployeeName { get; set; }
            public string EmpStatus { get; set; }


        }

        public class DeliveryTicket
        {
            public long Slno { get; set; }
            public long ID_ProductDeliveryAssign { get; set; }
            public string Module { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Master { get; set; }
            public long FK_Employee { get; set; }
            public string ReferenceNo { get; set; }
            public string PickUpTime { get; set; }
            public string AssignedOn { get; set; }
            public string CustomerName { get; set; }
            public string Mobile { get; set; }
            public string Area { get; set; }
            public string EMPName { get; set; }
            public string Priority { get; set; }
            public Int32 PriorityCode { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 Count { get; set; }
            public Int32 pageSize { get; set; }
            public Int32 pageIndex { get; set; }
            public string Channel { get; set; }
            public long Mode { get; set; }
            public string TicketNo { get; set; }
            public long CSAssignStatus { get; set; }

        }
        public class Employeedetails
        {
            public Int32 ID_Employee { get; set; }
            public string Employee { get; set; }
            public Int32 ID_CSAEmployeeType { get; set; }
            public string EmployeeType { get; set; }
            public Int32 SLNO { get; set; }
            public Int32 DepartmentID { get; set; }

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
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
        }
        public class Branch
        {
            public long ID_Branch { get; set; }

            public string BranchName { get; set; }
        }
        public class Status
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
            public long FK_ActionStatus { get; set; }
        }

        public class DelPickfollowupselectID
        {
            public long FK_ProductDeliveryFollowUp { get; set; }

            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public int Detailed { get; set; }

        }

        public class SaleProductOut
        {
         public long ID_ProductDelivery { get; set; }
         public long FK_Product { get; set; }
         public string Product { get; set; }
         public decimal Quantity { get; set; }
            public long FK_Category { get; set; }
        }
        public class SaleDeliveryout
        {
            public long ID_ProductDelivery { get; set; }
            public string ReferenceNo { get; set; }
            public string BillDate { get; set; }
            public string Customer { get; set; }
            public string MobileNo { get; set; }
            public string DelName { get; set; }
            public string DelAddress1 { get; set; }
            public string DelAddress2 { get; set; }
            public string Post { get; set; }
            public string Area { get; set; }
            public string Country { get; set; }
            public string District { get; set; }
            public string State { get; set; }
            public string PinCode { get; set; }
            public string DelMobileNo { get; set; }
            public string DelVehicleNo { get; set; }
            public string DelDriverName { get; set; }
            public string DelDriverMobileNo { get; set; }
            public string PdEWayBillNo { get; set; }


        }
        public class UpdateSalesdelivery
        {
            public long ID_ProductDeliveryFollowUp { get; set; }

            public long FK_ProductDeliveryAssign { get; set; }

            public DateTime PdfDeliveryDate { get; set; }
            public string PdfDeliveryTime { get; set; }

            public string PdfEmployeeNotes { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int16 UserAction { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public string productReplace { get; set; }

            public string CSAstatus { get; set; }
    
            public long FK_Employee { get; set; }
      
            public string TransMode { get; set; }

            public long ID_NextAction { get; set; }
            public decimal StandByAmount { get; set; }
            public string PaymentDetail { get; set; }
            public long FK_BillType { get; set; }
            public string PdfCustomerNotes { get; set; }



         
            public int PdfDeliveryCharge { get; set; }
            public int PdfNetAmount { get; set; }
            public long PdfAction { get; set; }
            public string DeliveryComplaint { get; set; }

        }

        public class SaleDeliveryInput
        {
            public List<SaleProductOut> productOuts { get; set; }
            public long FK_ProductDeliveryAssign { get; set; }
            public long FK_Employee { get; set; }
            public DateTime PdfDeliveryDate { get; set; }
            public string PdfDeliveryTime { get; set; }
            public string Remarks { get; set; }
            public string CSAstatus { get; set; }
            public string TransMode { get; set; }
            public List<DeliveryComplaints> DeliveryComplaints { get; set; }

        }
        public class DeliveryComplaints
        {
            public string FK_Product { get; set; }
            public string Qty { get; set; }
            public string ComplaintID { get; set; }
            public string Description { get; set; }
            public string Priority { get; set; }
        }

        public static string _deleteProcedureName = "ProProductDeliveryFollowUpDelete";
        public static string _updateProcedureName = "ProProductDeliveryFollowUpUpdate";
        public static string _selectProcedureName = "ProProductDeliveryFollowUpSelect";
        public class GetProductComplaintList
        {          
            public long FK_Company { get; set; }            
        }
        public class ProductComplaintList
        {
            public string ComplaintName { get; set; }
            public long ID_ComplaintList { get; set; }
            public long FK_Category { get; set; }
         
        }


        public class ProdDetails_stby
        {
            public long ID_Product { get; set; }
            public string Selected { get; set; }
            public string ProvideStandBy { get; set; }
            public string ProdName { get; set; }
            public decimal Quantity { get; set; }
            public string Product { get; set; }
            public decimal SPQuantity { get; set; }
            public string SPAmount { get; set; }
            public string Remarks { get; set; }
            public long Stock { get; set; }
            public long FK_StandByProduct { get; set; }
            public decimal StandByQuantity { get; set; }
            public decimal StandByAmount { get; set; }
            public long FK_EmployeeStock { get; set; }
            public long FK_StandByStock { get; set; }


                            
        }

        public class StandbyDeliveryInput
        {
            public long FK_ProductDeliveryAssign { get; set; }
            [Required(ErrorMessage = "Please select Date")]
            public DateTime PdfDeliveryDate { get; set; }
            [Required(ErrorMessage = "Please Select Time")]
            public string PdfDeliveryTime { get; set; }
            public string PdfCustomerNotes { get; set; }
            public string PdfEmployeeNotes { get; set; }
            public int PdfDeliveryCharge { get; set; }
            public int PdfNetAmount { get; set; }
            public long PdfAction { get; set; }
            public string Remarks { get; set; }
            public string CSAstatus { get; set; }
            public long FK_Employee { get; set; }
            public List<ProductStby> Productdetails { get; set; }
            public List<UpdateProductDeliveryFollowUp> UpdateProdel { get; set; }
            public long ID_NextAction { get; set; }
            public string TransMode { get; set; }
        }
        public class ProductStby
        {
            
                public long ID_Product { get; set; }
                public string Selected { get; set; }
                public string ProdName { get; set; }
                public string StandByProduct { get; set; }
                public decimal ReQuantity { get; set; }
                public string SPQuantity { get; set; }
                public string Remarks { get; set; }
                public long FK_StandByProduct { get; set; }
                public decimal StandByQuantity { get; set; }
                public decimal StandByAmount { get; set; }
                public long FK_StandbyStock { get; set; }

        }

        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<DeliverydetailsOutput> GetDeliverydetails(Deliverydetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliverydetailsOutput, Deliverydetails>(companyKey: companyKey, procedureName: "ProGetDeliveryDetails", parameter: input);
        }

        public APIGetRecordsDynamic<ProdDetails> GetProductdetails(Deliverydetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProdDetails, Deliverydetails>(companyKey: companyKey, procedureName: "ProGetDeliveryDetails", parameter: input);
        }

        public Output UpdateDeliverypickupData(UpdateProductDeliveryFollowUp input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductDeliveryFollowUp>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }


        public APIGetRecordsDynamic<DeliveryTicket> GetDeliveryresult(DeliveryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliveryTicket, DeliveryInput>(companyKey: companyKey, procedureName: "ProProductDeliveryList", parameter: input);

        }

        public APIGetRecordsDynamic<DeliveryPickupassignView> GetDelPickfollowupData(DelPickfollowupselectID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliveryPickupassignView, DelPickfollowupselectID>(companyKey: companyKey, procedureName: "ProProductDeliveryFollowUpSelect", parameter: input);
        }

        public APIGetRecordsDynamic<SaleProductOut> GetSaleProductdetails(Deliverydetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<SaleProductOut, Deliverydetails>(companyKey: companyKey, procedureName: "ProGetDeliveryDetails", parameter: input);
        }

        public APIGetRecordsDynamic<SaleDeliveryout> GetSaleDelivery(Deliverydetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<SaleDeliveryout, Deliverydetails>(companyKey: companyKey, procedureName: "ProGetDeliveryDetails", parameter: input);
        }

        public Output UpdateSalesDelivery(UpdateSalesdelivery input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSalesdelivery>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<ProductComplaintList> GetComplaintList(GetProductComplaintList input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductComplaintList, GetProductComplaintList>(companyKey: companyKey, procedureName: "ProProductComplaintList", parameter: input);
        }

        public APIGetRecordsDynamic<ProdDetails_stby> GetProductdetails_stby(Deliverydetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProdDetails_stby, Deliverydetails>(companyKey: companyKey, procedureName: "ProGetDeliveryDetails", parameter: input);
        }

        public APIGetRecordsDynamic<DeliverydetailsOutput_stby> GetDeliverydetails_stby(Deliverydetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliverydetailsOutput_stby, Deliverydetails>(companyKey: companyKey, procedureName: "ProGetDeliveryDetails", parameter: input);
        }
    }
}