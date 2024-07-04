using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IPickUpDelivery
    {
        string ReqMode { get; set; }
        string Token { get; set; }
        string BankKey { get; set; }
        string SubMode { get; set; }
        string FK_Company { get; set; }
        string FK_BranchCodeUser { get; set; }
        string EntrBy { get; set; }
        string FK_Employee { get; set; }
        string FromDate { get; set; }
        string ToDate { get; set; }
        string FK_Area { get; set; }
        string FK_Product { get; set; }
        string CusName { get; set; }
        string CusMobile { get; set; }
        string TicketNo { get; set; }
       string Status { get; set; }
        string ID_ProductDelivery { get; set; }
        string ID_ProductDeliveryFollowUp { get; set; }
        string FK_ProductDeliveryAssign { get; set; }
        string PdfDeliveryDate { get; set; }
        string PdfDeliveryTime { get; set; }
        string EmployeeNotes { get; set; }
        string CustomerNotes { get; set; }
        string DeliveryCharge { get; set; }
        string NetAmount { get; set; }
        string Action { get; set; }
        string FK_Reason { get; set; }
        string ID_NextAction { get; set; }
        string StandByAmount { get; set; }
        string FK_BillType { get; set; }
        string productReplace { get; set; }
        string PaymentDetail { get; set; }
        string LocLatitude { get; set; }
        string LocLongitude { get; set; }
        string Address { get; set; }
        string DeliveryComplaints { get; set; }
        string FK_Category { get; set; }
        string TransMode { get; set; }
        string ID_TokenUser { get; set; }

    }
    #region Response Interface Model Objects
    public class PickUpDeliveryDetails : CommonAPIR
    {
        public List<PickUpDeliveryDetailsList> PickUpDeliveryDetailsList { get; set; }
    }
    public class PickUpDeliveryDetailsList
    {
        public string ID_ProductDelivery { get; set; }
        public string Module { get; set; }
        public string TransMode { get; set; }
        public string FK_Master { get; set; }
        public string FK_Employee { get; set; }
        public string ReferenceNo { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string Area { get; set; }
        public string EMPName { get; set; }
        public string PriorityCode { get; set; }
        public string Priority { get; set; }
        public string PickUpTime { get; set; }
        public string AssignedOn { get; set; }
        public string TotalCount { get; set; }
        public string Mode { get; set; }
        public string AssignedDate { get; set; }
        public string ProductName { get; set; }
        public string FK_ImageLocation { get; set; }
        public string LocLongitude { get; set; }
        public string LocLattitude { get; set; }
      
    }
    public class PickupandDeliveryCount : CommonAPIR
    {
        public Int32 PickUp { get; set; }
        public Int32 Delivery { get; set; }
    }
  
    public class UpdateDeliverStatusDetails:CommonAPIR
    {
        public string FK_Master { get; set; }
        public string ID_ProductDelivery { get; set; }
        public string FK_Product { get; set; }
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
        public string MobileNo { get; set; }
        public string DelName { get; set; }
         public string Country { get; set; }
        public string PinCode { get; set; }
        public string DelMobileNo { get; set; }
        public string DelVehicleNo { get; set; }
        public string DelDriverName { get; set; }
        public string DelDriverMobileNo { get; set; }
        public string PdEWayBillNo { get; set; }
        public string AssignedOn { get; set; }
        public string TransMode { get; set; }
        public string SalBillDate { get; set; }
        public string SalBillNo { get; set; }
        public string ReferenceNo { get; set; }
    }
    public class PickUPProductInformationDetails:CommonAPIR
    {
        public List<PickUPProductInformationDetailsList> PickUPProductInformationDetailsList { get; set; }
    }
    public class PickUPProductInformationDetailsList
    {
        public string ID_Product { get; set; }
        public string ProdName { get; set; }
        public string ProvideStandBy { get; set; }
        public string Quantity { get; set; }
        public string Product { get; set; }
        public string SPQuantity { get; set; }
        public string SPAmount { get; set; }
        public string Remarks { get; set; }
        public string ID_ProductDelivery { get; set; }
        public string FK_Category { get; set; }
        public string FK_StandByProduct { get; set; }
        public string FK_StandByStock { get; set; }
    }
    public class StandByProductDetails : CommonAPIR
    {
        public List<StandByProductDetailsList> StandByProductDetailsList { get; set; }
    }
    public class StandByProductDetailsList
    {
        public string ID_Product { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string MRP { get; set; }
        public string SalesPrice { get; set; }
        public string CurrentStock { get; set; }
        public string StandByStock { get; set; }    
    }
    public class UpdatePickUpAndDelivery:CommonAPIR
    {
        public Int64 FK_ProductDeliveryFollowUp { get; set; }
      
    }
    public class ProductDeliveryFollowUp
    {
        public string BankKey { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string FK_ProductDeliveryAssign { get; set; }        
        public string PdfDeliveryDate { get; set; }      
        public string PdfDeliveryTime { get; set; }
        public string Remark { get; set; }
        public string PdfEmployeeNotes { get; set; }
        public string PdfDeliveryCharge { get; set; }
        public string PdfNetAmount { get; set; }
        public string PdfAction { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string ID_NextAction { get; set; }
        public string FK_Employee { get; set; }
        public List<ProdDetails> Productdetails { get; set; }      
        public List<PaymentDetails> PaymentDetail { get; set; }
        public string StandByAmount { get; set; }
        public string FK_BillType { get; set; }
        public List<DeliveryComplaints> DeliveryComplaints { get; set; }
    }
    public class StandbyDeliveryInput
    {
        public string FK_ProductDeliveryAssign { get; set; }     
        public string PdfDeliveryDate { get; set; }      
        public string PdfDeliveryTime { get; set; }
        public string PdfCustomerNotes { get; set; }
        public string PdfEmployeeNotes { get; set; }
        public string PdfDeliveryCharge { get; set; }
        public string PdfNetAmount { get; set; }
        public long PdfAction { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string FK_Employee { get; set; }
        public List<ProductStby> Productdetails { get; set; }     
        public long ID_NextAction { get; set; }
        public string TransMode { get; set; }
        public string EntrBy { get; set; }
        public string BankKey { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
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
    public class BillType : CommonAPIR
    {
        public List<BillTypeList> BillTypeList { get; set; }
    }
    public class BillTypeList
    {
        public string ID_BillType { get; set; }
        public string BTName { get; set; }

    }
    public class DeliveryComplaints
    {
        public string FK_Product { get; set; }
        public string Qty { get; set; }
        public string ComplaintID { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }
    public class DeliveryProductInformationDetails : CommonAPIR
    {
        public List<DeliveryProductInformationDetailsList> DeliveryProductInformationDetailsList { get; set; }
    }
    public class DeliveryProductInformationDetailsList
    {
        public string ID_ProductDelivery { get; set; }
        public string FK_Product { get; set; }
        public string FK_Category { get; set; }
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string TransMode { get; set; }
        public string ProvideStandBy { get; set; }
        public string StandByProduct { get; set; }
        public string FK_StandByProduct { get; set; }
        public string SPQuantity { get; set; }
        public string SPAmount { get; set; }
        public string Remarks { get; set; }
        public string FK_StandByStock { get; set; }


    }
    public class ProductComplaintList : CommonAPIR
    {
        public List<ProductComplaintListDetails> ProductComplaintsList{ get; set; }
    }
    public class ProductComplaintListDetails
    {
        public string ID_ComplaintList { get; set; }
        public string ComplaintName { get; set; }       

    }
    public class PickUpEMIRecoveryDetails : CommonAPIR
    {
        public string FK_Master { get; set; }
        public string ID_ProductDelivery { get; set; }
        public string AssignedOn { get; set; }
        public string Customer { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Post { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string TransMode { get; set; }
    }

    #endregion
}
