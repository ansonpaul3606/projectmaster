/*----------------------------------------------------------------------
Created By	: Aiswarya
Created On	: 15/02/2022
Purpose		: Sales
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class SaleModel
    {

        public class SalesView
        {

            public long SalesID { get; set; }
            public long ID_Hold { get; set; }
            public long ReasonID { get; set; }
            public Int64? LastID { get; set; }
            public long SlNo { get; set; }
            public string TransMode { get; set; }
            public long BillType { get; set; }
            public string SalBillNo { get; set; }
            public string SalesFrom { get; set; }

            public DateTime SalBillDate { get; set; }
            public DateTime? SalEnterDate { get; set; }
            public Int32 ID_Customer { get; set; }
            public Int32 FK_Lead { get; set; }
            public Int32 FK_CustomerOthers { get; set; }
            public Int32 FK_Customer { get; set; }
            public Int32 FK_SalesOrder { get; set; }

            public string CustomeName { get; set; }
            public string MobileNo { get; set; }

            public decimal SalBillTotal { get; set; }

            public decimal SalDiscount { get; set; }
            public decimal OtherCharge { get; set; }
            public decimal SalRoundoff { get; set; }

            public decimal SalNetAmount { get; set; }
            public int Hold { get; set; }
            public int StockadjonHold { get; set; }
            public decimal AdvAmount { get; set; }
            public Int64 TotalCount { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public List<ProductDetails> ProductDetail { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public List<BuyBackDetails> buyback { get; set; }

            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public List<TransporttypeMode> TransporttypeModeList { get; set; }
            public decimal DownPayment { get; set; }
            public decimal AdditionalAmount { get; set; }
            public DateTime? StartDate { get; set; }
            public long FK_FinancePlanType { get; set; }

            public string Area { get; set; }
            public long? AreaID { get; set; }
            public long? CountryID { get; set; }
            public long? StatesID { get; set; }
            public long? DistrictID { get; set; }
            public long? PostID { get; set; }
            public string Country { get; set; }
            public string States { get; set; }
            public string District { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string DrvPhoneno { get; set; }
            public string DrvName { get; set; }
            public string Vehicleno { get; set; }
            public string ShpMobile { get; set; }
            public string ShpContactName { get; set; }
            public int? Transporttype { get; set; }
            public string EWaybillNo { get; set; }
            public long FK_Employee { get; set; }
            public List<ProductSerialNumbers> ProductSerialNumbers { get; set; }
            public List<SubProductSerialNumbers> SubproductDetails { get; set; }
            public List<ImageListView> ImageList { get; set; }
            public long? BankID { get; set; }
            public string CusTransMode { get; set; }
            public int ChekStandBy { get; set; }
            public int ImportID { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_Vehicle { get; set; }
            public decimal Discount { get; set; }
            public Boolean ItemWise { get; set; }
            public List<SalebillDetails> SalebillDetails { get; set; }

        }
        public class SalebillDetails
        {
            public string Header { get; set; }
            public string Existing { get; set; }
            public string New { get; set; }
        }
        public class Custransmodes
        {
            public string CusTransMode { get; set; }
        }
        public class ImageListView
        {
            public long SLNo { get; set; }
            public string TransMode { get; set; }
            public long stockid { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public long FK_SubProduct { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public string ProdImageMode { get; set; }
            public string ProdImageDescription { get; set; }
            public long ID_ProductImage { get; set; }
            public long WarrantyType { get; set; }
            public string WarTypName { get; set; }
            public string ProdImageType { get; set; }
        }
        public class TransporttypeMode
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class ProductSerialNumbers
        {
            public long FK_Stock { get; set; }
            public long FK_MasterID { get; set; }
            public long ID_ProductNumberingDetails { get; set; }
            public string UID { get; set; }
        }
        public class SubProductSerialNumbers
        {
            public long Master_ID { get; set; }
            public long SubProductID { get; set; }
            public string SubProdName { get; set; }
            public long SubStockId { get; set; }
            public long Fixed { get; set; }
            public long ProductNumbering { get; set; }
            public decimal SubQty { get; set; }
            public decimal CurrentStock { get; set; }
        }
        public class ChangeModeInput
        {
            public int Mode { get; set; }

        }
        public class Invoice_ip
        {
            public Int64 FK_Master { get; set; }
            public Int64 FK_Company { get; set; }
            public int Mode { get; set; }

        }
        public class SalesViewels
        {

            public long SalesID { get; set; }
            public long ID_Hold { get; set; }
            public long ReasonID { get; set; }
            public DateTime? SalEnterDate { get; set; }
            public long SlNo { get; set; }
            public string TransMode { get; set; }
            public long BillType { get; set; }
            public string SalBillNo { get; set; }
            public string SalesFrom { get; set; }
            public Int64? LastID { get; set; }
            public DateTime SalBillDate { get; set; }
            public Int32 ID_Customer { get; set; }
            public Int32 FK_Lead { get; set; }
            public Int32 FK_CustomerOthers { get; set; }
            public Int32 FK_Customer { get; set; }
            public Int32 FK_SalesOrder { get; set; }

            public string CustomeName { get; set; }
            public string MobileNo { get; set; }

            public string SalBillTotal { get; set; }

            public string SalDiscount { get; set; }
            public string OtherCharge { get; set; }
            public string SalRoundoff { get; set; }

            public string SalNetAmount { get; set; }
            public int Hold { get; set; }
            public int StockadjonHold { get; set; }
            public string AdvAmount { get; set; }
            public Int64 TotalCount { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public List<ProductDetails> ProductDetail { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }

            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public List<TransporttypeMode> TransporttypeModeList { get; set; }
            public decimal SalDownPayment { get; set; }
            public decimal SalAddnAmount { get; set; }
            public DateTime? SalStartDate { get; set; }
            public long FK_FinancePlanType { get; set; }

            public string Area { get; set; }
            public long? AreaID { get; set; }
            public long? CountryID { get; set; }
            public long? StatesID { get; set; }
            public long? DistrictID { get; set; }
            public long? PostID { get; set; }
            public string Country { get; set; }
            public string States { get; set; }
            public string District { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string DrvPhoneno { get; set; }
            public string DrvName { get; set; }
            public string Vehicleno { get; set; }
            public string ShpMobile { get; set; }
            public string ShpContactName { get; set; }
            public int? Transporttype { get; set; }
            public string EWaybillNo { get; set; }
            public string Salesman { get; set; }
            public long FK_Salesman { get; set; }
            public long BankID { get; set; }
            public long ID_Branch { get; set; }
            public bool DelSameASCusAddress { get; set; }
            public long? State { get; set; }
        }
        public class SalesEwaybill
        {
            public long ID_Sales { get; set; }
            public string EWaybillNo { get; set; }
        }
        public class SalesEwaybillupdate
        {
            public long FK_Sales { get; set; }
            public string SalEWayBillNo { get; set; }
            public string PaymentDetail { get; set; }
            public string EntrBy { get; set; }
            public string OtherChgDetails { get; set; }
            public string OtherChgTaxDetails { get; set; }
            public decimal Discount  { get; set; }
            public Boolean ItemWise { get; set; }
            public string SalesBillDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class UpdateSales
        {
            public byte UserAction { get; set; }
            public long ID_Sales { get; set; }
            public long ID_Hold { get; set; }
            public Int64? LastID { get; set; }
            public string TransMode { get; set; }
            public string SalBillNo { get; set; }
            public DateTime SalBillDate { get; set; }
            public DateTime? SalEnterDate { get; set; }
            public decimal SalBillTotal { get; set; }
            public decimal SalDiscount { get; set; }
            public decimal SalOthercharges { get; set; }
            public decimal SalRoundoff { get; set; }
            public decimal SalNetAmount { get; set; }
            public decimal AdvAmount { get; set; }
            public long FK_BillType { get; set; }
            public long FK_Customer { get; set; }
            public long FK_CustomerL { get; set; }
            public long FK_CustomerOthersL { get; set; }
            public long FK_Department { get; set; }

            public int Hold { get; set; }
            public int StockadjonHold { get; set; }
            public string CustomeName { get; set; }
            public String MobileNo { get; set; }
            public long FK_LeadGenerate { get; set; }
            public long FK_SalesOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BrachCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string ProductDetail { get; set; }
            public string PaymentDetail { get; set; }
            public string OtherChgDetails { get; set; }
            public string WarrantyDetails { get; set; }
            public string WarrantyImgDetails { get; set; }
            public string buyback { get; set; }

            public decimal DownPayment { get; set; }
            public decimal AdditionalAmount { get; set; }
            public DateTime? StartDate { get; set; }
            public long FK_FinancePlanType { get; set; }

            public long? FK_Area { get; set; }
            public long? FK_Country { get; set; }
            public long? FK_State { get; set; }
            public long? FK_District { get; set; }
            public long? FK_Post { get; set; }



            public string DelAddress1 { get; set; }
            public string DelAddress2 { get; set; }
            public string DelDriverMobileNo { get; set; }
            public string DelDriverName { get; set; }
            public string DelVehicleNo { get; set; }
            public string DelMobileNo { get; set; }
            public string DelName { get; set; }
            public int? DelTransportType { get; set; }
            public long FK_Employee { get; set; }
            public string ProductSerialNumbers { get; set; }
            public string SubproductDetails { get; set; }
            public string OtherChgTaxDetails { get; set; }
            public long FK_Bank { get; set; }
            public int ChekStandBy { get; set; }
            public int ImportID { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_Vehicle { get; set; }


        }

        public static string _deleteProcedureName = "ProSalesHoldDelete";
        public static string _updateProcedureName = "ProSalesUpdate";
        public static string _selectProcedureName = "ProSalesSelect";

        public class DeleteSales
        {

            public Int64 FK_Company { get; set; }
            public long FK_SalesHold { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Machine { get; set; }
        }

        public class DropDownListModel
        {

            public List<BillType> BillTypeList { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }
            public List<AMCTypeModel.AMCTypeView> AMCtype { get; set; }
            public List<ModuleType> ModuleTypeList { get; set; }
            public List<TransporttypeMode> TransporttypeModeList { get; set; }
            public List<DepartmentList> DepartmentList { get; set; }
            public List<ProductUnitDetails> ProductUnitList { get; set; }
            public long SalesID { get; set; }
            public string EWaybillNo { get; set; }
            public Int32 ID_Buyer { get; set; }

            public Int32 FK_Buyer { get; set; }
            public string BuyerName { get; set; }
            public string BuyerMobileNo { get; set; }
            public List<BankList> BankList { get; set; }
            public bool BtoB { get; set; }
        }

        public class ProductDetails
        {
            public long SlNo { get; set; }
            public Int32 ProductID { get; set; }
            public decimal SpdSalQuantity { get; set; }
            public decimal SalePrice { get; set; }
            public decimal MRPs { get; set; }
            public decimal Discp { get; set; }
            public decimal Discamt { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal NetAmt { get; set; }
            public long StockId { get; set; }

            public long FK_Master { get; set; }
            public long FK_FinancePlanType { get; set; }
            public bool EMIProduct { get; set; }
            public decimal AdditionalPay { get; set; }
            public decimal downpay { get; set; }
            public string Description { get; set; }

            public decimal WarrantyTotalAmt { get; set; }
            public long AMCFK_Master { get; set; }
            public long AMCMType { get; set; }
            public long AMCNoOfServices { get; set; }
            public DateTime? AMCMRenewduedate { get; set; }
            public DateTime? AMCMDuedate { get; set; }
            public decimal AmcTotalAmount { get; set; }
            public decimal AMCTaxTotalAmt { get; set; }
            public decimal AMCNetTotalAmt { get; set; }
            public string AMCRemarks { get; set; }
            public decimal SpdSalFreeQuantity { get; set; }
            public long StandByProduct { get; set; }
            public decimal StandByQuantity { get; set; }
            public long StandByStockId { get; set; }
            public long UnitID { get; set; }
           // public string Unit { get; set; }

        }
        public class ProductDetailslst
        {
            public long SlNo { get; set; }
            public Int32 ProductID { get; set; }
            public string ProName { get; set; }

            public string SpdSalQuantity { get; set; }
            public string SalePrice { get; set; }
            public string MRPs { get; set; }
            public string Discp { get; set; }
            public string Discamt { get; set; }
            public string TaxAmount { get; set; }
            public string NetAmt { get; set; }
            public long ?StockId { get; set; }

            public long FK_FinancePlanType { get; set; }
            public string downpay { get; set; }
            public string AdditionalPay { get; set; }
            public bool EMIProduct { get; set; }
            public long FK_Master { get; set; }

            public string Description { get; set; }

            public string WarrantyTotalAmt { get; set; }
            public long AMCFK_Master { get; set; }
            public long AMCMType { get; set; }
            public long AMCNoOfServices { get; set; }
            public DateTime? AMCMRenewduedate { get; set; }
            public DateTime? AMCMDuedate { get; set; }
            public string AmcTotalAmount { get; set; }
            public string AMCTaxTotalAmt { get; set; }
            public string AMCNetTotalAmt { get; set; }
            public string AMCRemarks { get; set; }
            public string SpdSalFreeQuantity { get; set; }
            public long BankID { get; set; }
            public long StandByProduct { get; set; }
            public decimal StandByQuantity { get; set; }
            public long StandByStockId { get; set; }
            public string StandByProdName { get; set; }
            public long UnitID { get; set; }
            //public string Unit { get; set; }
            public string CrntQnty { get; set; }

        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }
            public long PMMode { get; set; }

        }
        public class BuyBackDetails
        {
            public long SlNo { get; set; }
            public long ID_BuyBackDetails { get; set; }

            public long FK_Product { get; set; }
            public string ProdName { get; set; }

            public decimal Rate { get; set; }
            public decimal Quantity { get; set; }
            public string SerialNo { get; set; }
        }
        public class WarrantyDetails
        {
            public long SlNo { get; set; }
            public Int32 prodtid { get; set; }
            public Int32 stkid { get; set; }
            public Int32 subProductID { get; set; }
            public string subProName { get; set; }
            public long WarrantyType { get; set; }
            public string Replcwardt { get; set; }
            public string Serwardt { get; set; }
            public decimal WarrantyTaxAmount { get; set; }
            public decimal WarrantyAmount { get; set; }
            public decimal WarrantyNetAmount { get; set; }
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
            public long OctyTransTypeActive { get; set; }
        }
        public class ModuleType
        {
            public long ID_ModuleType { get; set; }
            public string ModuleName { get; set; }

            public string Mode { get; set; }
        }

        public class GetProduct
        {
            public Int64 ID_Product { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Manufacturer { get; set; }
            public Int64 FK_Supplier { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }

        }
        public class GetPaymentin
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }


        }
        public class GetBuyBack
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }


        }
        public class GetImagein
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 StockId { get; set; }
            public Int64 FK_Master { get; set; }


        }
        public class BillType
        {
            public long ID_BillType { get; set; }
            public string BTName { get; set; }

            public string Mode { get; set; }
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
        public class GetSalesdetail
        {
            public Int64 ID_Sales { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }

        }
        public class GetSales
        {
            public Int64 FK_Sales { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int AllSales { get; set; }

        }




        public class Leadfill
        {

            public int IsLead { get; set; }

            public long FK_Master { get; set; }

        }

        public class WarProductImage
        {

            public Int64 ID_ProductImage { get; set; }
            public string TransMode { get; set; }
            public int ProdImageMode { get; set; }
            public long StockId { get; set; }
            public long stockiddata { get; set; }
            public long FK_Product { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public long WarrantyType { get; set; }
            public string WarTypName { get; set; }
        }
        public class EMIInstallmentDetails
        {
            public long SLNo { get; set; }
            public DateTime EMIDate { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }
        }
        public class GetEMIDetailsSelect
        {
            public long Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Master { get; set; }
        }
        public class SalesOrderID
        {
            public Int64 FK_Master { get; set; }
        }
        public class GetCustomerDetails
        {
            public int FK_Master { get; set; }
            public int ImportId { get; set; }
        }
        public class Getcustomerdata
        {
            public string Area { get; set; }
            public long? AreaID { get; set; }
            public long? CountryID { get; set; }
            public long? StatesID { get; set; }
            public long? DistrictID { get; set; }
            public long? PostID { get; set; }
            public string Country { get; set; }
            public string States { get; set; }
            public string District { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }

            public string Address { get; set; }
            public string Address2 { get; set; }

            public string Mobile { get; set; }
            public string CusName { get; set; }



        }
        public class Bindshippingdetails
        {
            public long FK_Master { get; set; }
            //public int Mode { get; set; }
            public string TransMode { get; set; }
        }
        public class Getcustomerdatashippingdetails
        {
            public string Area { get; set; }
            public long? FK_Area { get; set; }
            public long? FK_Country { get; set; }
            public long? FK_States { get; set; }
            public long? FK_District { get; set; }
            public long? FK_Post { get; set; }
            public string Country { get; set; }
            public string States { get; set; }
            public string District { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }

            public string DelAddress1 { get; set; }
            public string DelAddress2 { get; set; }
            public string DelDriverMobileNo { get; set; }
            public string DelDriverName { get; set; }
            public string DelVehicleNo { get; set; }
            public string DelMobileNo { get; set; }
            public string DelName { get; set; }
            public int? DelTransportType { get; set; }
            public string FK_Vehicle { get; set; }
        }

        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class AmcWarrantyDetailsInput
        {
            public byte Mode { get; set; }
            public long FK_Type { get; set; }
            public DateTime Date { get; set; }
            public decimal Quantity { get; set; }
        }
        public class GetAmcWarrantyDetails
        {
            public byte Mode { get; set; }
            public long FK_Type { get; set; }
            public DateTime Date { get; set; }
            public decimal Quantity { get; set; }
        }
        public class AmcWarrantyDetails
        {
            public long FK_WarrantyType { get; set; }
            public DateTime ReplaceWarrantyDate { get; set; }
            public DateTime ServiceWarrantyDate { get; set; }
            public string Amount { get; set; }
            public string TaxAmount { get; set; }
            public string NetAmount { get; set; }

            public long FK_AMCType { get; set; }
            public Int64 NoOfServices { get; set; }
            public DateTime AMCDuedate { get; set; }
            public DateTime AMCRenewduedate { get; set; }
            public string AmcAmount { get; set; }
            public string AMCTaxAmount { get; set; }
            public string AMCNetAmount { get; set; }
            public string ErrMsg { get; set; }
            public long ErrCode { get; set; }
        }
        public class AccountbalFill
        {
            public Int32 FK_Customer { get; set; }
            public DateTime TransDate { get; set; }

        }
        public class Leadfilldetails
        {
            public int IsLead { get; set; }
            public long FK_Master { get; set; }
            public int mod { get; set; }
            public string Transmode { get; set; }
        }



        public class GeneralSettingsModel
        {

            public string GsModule { get; set; }
            public string GsField { get; set; }
            public bool GsValue { get; set; }
        }
        public class BusinessToBusinessCustomer
        {
            public bool BtoB { get; set; }
        }
        public class  GetEinvoiceData
        {
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Sales { get; set; }
        }
        public class GenerateEinvoiceData
        {
            public string JsonData { get; set; }
        }
        public class EInvoiceSettingsModel
        {

            public string Username { get; set; }
            public string Password { get; set; }
            public long FK_Company { get; set; }
            public string Url { get; set; }

        }

        public class APIResponse_TockenInfo
        {
            public bool Status { get; set; }
            public string Message { get; set; }
            public string Token { get; set; }
        }
        public class IrnResponse
        {
            public bool status { get; set; }
            public dynamic data { get; set; }
            public List<errorType> error { get; set; }

        }
        public class jsonResponseDecrypted
        {         
            public dynamic data { get; set; }
        }

        public class errorType {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }



        public Output UpdateSalesData(UpdateSales input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSales>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output UpdateSalEwaybill(SalesEwaybillupdate input, string companyKey)
        {
            return Common.UpdateTableData<SalesEwaybillupdate>(parameter: input, companyKey: companyKey, procedureName: "ProSalEwayBillUpdate");
        }
        public Output DeleteSalesData(DeleteSales input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSales>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<SalesViewels> GetSalesData(GetSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesViewels, GetSales>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ProductDetailslst> GetSalesDetails(GetSalesdetail input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductDetailslst, GetSalesdetail>(companyKey: companyKey, procedureName: "ProSalesDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> OtherChargeTaxData(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOtherChargeTaxData", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<BuyBackDetails> GetBuyBackSelect(GetBuyBack input, string companyKey)
        {
            return Common.GetDataViaProcedure<BuyBackDetails, GetBuyBack>(companyKey: companyKey, procedureName: "ProBuyBackDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ImageListView> GetImageSelect(GetImagein input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageListView, GetImagein>(companyKey: companyKey, procedureName: "ProProductImageSelect", parameter: input);

        }

        public APIGetRecordsDynamic<WarrantyDetailsReturn> GetWarrantySelect(GetImagein input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetailsReturn, GetImagein>(companyKey: companyKey, procedureName: "ProWarrantyDetailSelect", parameter: input);

        }

        public APIGetRecordsDynamicdn<dynamic> Fillead(Leadfill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<Leadfill>(companyKey: companyKey, procedureName: "ProSalesDataFill", parameter: input);

        }
        public APIGetRecordsDynamic<EMIInstallmentDetails> GetEMIInstallmentDetailsSelects(GetEMIDetailsSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIInstallmentDetails, GetEMIDetailsSelect>(companyKey: companyKey, procedureName: "ProEMIDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<Getcustomerdata> GetCustomerdetailsfill(GetCustomerDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<Getcustomerdata, GetCustomerDetails>(companyKey: companyKey, procedureName: "ProGetCustomerAddress", parameter: input);

        }

        public APIGetRecordsDynamic<Getcustomerdatashippingdetails> Getshippingdetailsfill(Bindshippingdetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<Getcustomerdatashippingdetails, Bindshippingdetails>(companyKey: companyKey, procedureName: "ProDeliveryAddressSelect", parameter: input);

        }

        public APIGetRecordsDynamic<AmcWarrantyDetails> GetAmcWarrantyfill(GetAmcWarrantyDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<AmcWarrantyDetails, GetAmcWarrantyDetails>(companyKey: companyKey, procedureName: "ProGetAMCandWarrantyDetails", parameter: input);

        }

        public APIGetRecordsDynamicdn<dynamic> GetCustomerAccountBalance(AccountbalFill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<AccountbalFill>(companyKey: companyKey, procedureName: "ProGetCustomerAccountBalance", parameter: input);

        }
        public APIGetRecordsDynamic<GenerateEinvoiceData> GenerateEinvoice(GetEinvoiceData input, string companyKey)
        {
            return Common.GetDataViaProcedure<GenerateEinvoiceData, GetEinvoiceData>(companyKey: companyKey, procedureName: "ProEInvoice", parameter: input);
        }

        public class ScrapSaleView
        {
            public long ScrapSalesID { get; set; }

            public long ReasonID { get; set; }
            public Int64? LastID { get; set; }
            public long SlNo { get; set; }
            public string TransMode { get; set; }
            public long BillType { get; set; }
            public string SalScrapBillNo { get; set; }
            public string ReferenceNo { get; set; }


            public DateTime SalScrapBillDate { get; set; }
            public DateTime SalScrapEnterDate { get; set; }


            public Int32 FK_Buyer { get; set; }
            public string BuyerName { get; set; }
            public string BuyerMobileNo { get; set; }

            public decimal SalScrapBillTotal { get; set; }

            public decimal SalScrapDiscount { get; set; }
            public decimal OtherCharge { get; set; }
            public decimal TaxAmount { get; set; }

            public decimal SalScrapNetAmount { get; set; }
            public bool Auction { get; set; }
            public string AuctionRemarks { get; set; }


            public Int64 TotalCount { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public List<ScrapProductDetails> ScrapProductDetails { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public List<TaxAmountDetails> TaxAmountDetails { get; set; }
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }

            public long DepartmentID { get; set; }
            public bool IncludeTax { get; set; }
            public Int32 ID_Product { get; set; }
            public long ID_Stock { get; set; }
        }
        public class TaxAmountDetails
        {
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal? TaxAmount { get; set; }
            public long ID_TaxType
            {
                get
                {
                    return this.FK_TaxType;
                }
            }
        }

        public class TaxAmountDetailsout
        {
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal? TaxAmount { get; set; }
        }
        public class ScrapProductDetails
        {
            public long SlNo { get; set; }
            public Int32 ID_Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal MRPs { get; set; }
            public long ID_Stock { get; set; }
            public long? StockMode { get; set; }

        }
        public class ScrapProductDetailsout
        {
            public long SlNo { get; set; }
            public Int32 ID_Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal MRPs { get; set; }
            public long ID_Stock { get; set; }
            public long? StockMode { get; set; }
            public string Product { get; set; }
            public string ProName { get; set; }

        }
        public class UpdateScrapSales
        {
            public byte UserAction { get; set; }
            public long ID_ScrapSales { get; set; }


            public Int64? LastID { get; set; }

            public string TransMode { get; set; }
            public long? FK_BillType { get; set; }
            public string SalScarpBillNo { get; set; }


            public DateTime SalScarpBillDate { get; set; }
            public DateTime SalScarpEnterDate { get; set; }
            public Int32 FK_Buyer { get; set; }
            public long FK_Department { get; set; }

            public Int32 FK_Branch { get; set; }

            public decimal SalScarpBillTotal { get; set; }

            public bool IncludeTax { get; set; }

            public decimal SalScarpOthercharges { get; set; }
            public decimal SalScarpTaxamount { get; set; }


            public decimal SalScarpNetAmount { get; set; }
            public Boolean Auction { get; set; }
            public string Auctiondetails { get; set; }
            public string ScrapProductDetails { get; set; }
            public string PaymentDetail { get; set; }
            public string OtherChgDetails { get; set; }
            public string TaxAmountDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BrachCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }


        }
        public class BindTaxAmount
        {
            public Int64 FK_Company { get; set; }

        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string Department { get; set; }

        }


        public class BankList
        {
            public long BankID { get; set; }
            public string BankName { get; set; }

        }
        public Output UpdateScrapSalesData(UpdateScrapSales input, string companyKey)
        {
            return Common.UpdateTableData<UpdateScrapSales>(parameter: input, companyKey: companyKey, procedureName: "ProScrapSalesUpdate");
        }
        public APIGetRecordsDynamic<ScrapSaleView> FillTax(BindTaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<ScrapSaleView, BindTaxAmount>(companyKey: companyKey, procedureName: "ProTaxtypeEquipment", parameter: input);

        }
        public class DeleteSalesscrap
        {

            public Int64 FK_Company { get; set; }
            public long FK_ScrapSales { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Machine { get; set; }
        }
        public Output DeleteSalesScrapData(DeleteSalesscrap input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSalesscrap>(parameter: input, companyKey: companyKey, procedureName: "ProScrapSalesDelete");
        }

        public class SalesScrapID
        {
            public Int64 ID_SalesScrap { get; set; }
        }
        public class GetScrapSales
        {
            public Int64 FK_ScrapSales { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public Byte Detailed { get; set; }
        }

        public APIGetRecordsDynamic<ScrapSaleView> GetScrapSalesData(GetScrapSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<ScrapSaleView, GetScrapSales>(companyKey: companyKey, procedureName: "ProScrapSalesSelect", parameter: input);
        }

        public class GetscrapSalesdetail
        {
            public Int64 ID_ScrapSales { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }


        }
        public class BuyBack
        {
            public bool GsValue { get; set; }
            public string GsField { get; set; }

        }

        public APIGetRecordsDynamic<ScrapProductDetailsout> GetScarpSalesDetails(GetScrapSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<ScrapProductDetailsout, GetScrapSales>(companyKey: companyKey, procedureName: "ProScrapSalesSelect", parameter: input);

        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargescrapDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentScrapselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        public class TaxAmount
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public APIGetRecordsDynamic<TaxAmountDetails> GetTaxDetails(TaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxAmountDetails, TaxAmount>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }
        public class InputProduct
        {
            public Int64 ID_Sales { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }
            public Int64 StockID { get; set; }
            public int Mode { get; set; }
            public decimal Quantity { get; set; }
        }
        public class GetSubProduct
        {
            public Int64 ID_Sales { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }
            public Int64 StockID { get; set; }
            public int Mode { get; set; }
            public decimal Quantity { get; set; }
        }
        public class InclusiveProduct
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            public decimal SprodQuanity { get; set; }
            public decimal CurrentStock { get; set; }
            public Boolean SprodQtyFixed { get; set; }
            public long Number { get; set; }

            public long ID_ProductNumberingDetails { get; set; }
            public string ProdSerielNo { get; set; }
            public long FK_Stock { get; set; }


        }
        public class SelectSubProduct
        {
            public Int64 ID_Sales { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public int Mode { get; set; }
        }
        public class GetSubproductDetails
        {
            public long Master_ID { get; set; }
            public long SubProductID { get; set; }
            public string SubProdName { get; set; }
            public long SubStockId { get; set; }
            public long Fixed { get; set; }
            public long ProductNumbering { get; set; }
            public decimal SubQty { get; set; }
            public decimal CurrentStock { get; set; }

        }
        public class GetSerialNumbers
        {
            public long FK_Stock { get; set; }
            public long FK_MasterID { get; set; }
            public long ID_ProductNumberingDetails { get; set; }
            public string ProdSerielNo { get; set; }
            public string UID { get; set; }
            public long ProductId { get; set; }
        }
        public class GetWarrantyDtls
        {
            public long FK_Product { get; set; }
            public DateTime TransDate { get; set; }
            public long stockId { get; set; } = 0;
        }
        public class GetProductUnitDtls
        {
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long  FK_BranchCodeUser { get; set; }
        }
        public class GetProductWisePrice
        {
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public long FK_Stock { get; set; }
            public long Branch { get; set; }
            public long FK_Company { get; set; }
        }
        public class ProductWiseTable1
        {
            public string CompanyName { get; set; }
            public string PCustomerGSTIN { get; set; }
            public string PAddress1 { get; set; }
            public string PAddress2 { get; set; }
            public string PAddress3 { get; set; }
            public string PBankIFSCCode { get; set; }
            public string PBankAccNo { get; set; }
            public string PBranchName { get; set; }
            public string PBankName { get; set; }
            public DateTime PBillDate { get; set; }
            public string PBillNo { get; set; }
            public string PCustomerName { get; set; }
            public string RefNo { get; set; }
            public string EwayBillNo { get; set; }
            

        }
        public class ProductWiseTable2
        {
            public string ProductName { get; set; }
            public string HSNCode { get; set; }
            public string Quantity { get; set; }
            public string FreeQuantity { get; set; }
            public string MRP { get; set; }
            public string Disc { get; set; }
            public string Rate { get; set; }
            public string TaxAmount { get; set; }
            public string TotalAmount { get; set; }
            public bool subline { get; set; }
            public bool IsTotal { get; set; }
            public string AmountInWords { get; set; }
            public string image { get; set; }

        }
        public class ProductWiseTable3
        {
            public string ProductName { get; set; }
           


        }
        public class ProductWisePrice
        {
             
            public string Code { get; set; }
            public decimal MRPs { get; set; }
            public decimal SalePrice { get; set; }
            public decimal CurrentStock { get; set; }
            public long StockId { get; set; }
            public string Department { get; set; }
            public decimal CrntQnty { get; set; }

        
        }
        public class WarrantyDetailsReturn
        {
            public long SlNo { get; set; }
            public Int32 prodtid { get; set; }
            public Int32 stkid { get; set; }
            public Int32 subProductID { get; set; }
            public string subProName { get; set; }
            public long WarrantyType { get; set; }
            public string Replcwardt { get; set; }
            public string Serwardt { get; set; }
            public string WarrantyTaxAmount { get; set; } = "0";
            public string WarrantyAmount { get; set; } = "0";
            public string WarrantyNetAmount { get; set; } = "0";
        }
        public class ProductUnitDetails
        {
            public long FK_Unit { get; set; }

            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class SalsRecalculateInfoInput
        {
           
            public long FK_Sales { get; set; }
            public long FK_Company { get; set; }
            public decimal Discount  { get; set; }
            public Boolean Itemwise { get; set; }
            public string  OtherChgDetails { get; set; }
            public string OtherChgTaxDetails { get; set; }
            public string TransMode { get; set; }

        }
        public class SalsRecalculateInfoview
        {
            public string Header { get; set; }

            public decimal Existing { get; set; }

            public decimal New { get; set; }
        }

            public APIGetRecordsDynamic<InclusiveProduct> GetSubProducts(GetSubProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<InclusiveProduct, GetSubProduct>(companyKey: companyKey, procedureName: "ProGetSubProductInfo", parameter: input);

        }
        public APIGetRecordsDynamic<GetSubproductDetails> GetSubProductSelect(SelectSubProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetSubproductDetails, SelectSubProduct>(companyKey: companyKey, procedureName: "ProSalesSubProductSelect", parameter: input);

        }
        public APIGetRecordsDynamic<GetSerialNumbers> GetSerialNumberSelect(SelectSubProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetSerialNumbers, SelectSubProduct>(companyKey: companyKey, procedureName: "ProSalesSubProductSelect", parameter: input);

        }

        public APIGetRecordsDynamic<WarrantyDetailsReturn> GetWarrantyDtlsData(GetWarrantyDtls input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetailsReturn, GetWarrantyDtls>(companyKey: companyKey, procedureName: "ProGetProductwarrantyDetails", parameter: input);

        }
        public APIGetRecordsDynamic<ProductWisePrice> GetProductwisePrice(GetProductWisePrice input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductWisePrice, GetProductWisePrice>(companyKey: companyKey, procedureName: "ProGetProductwisePrice", parameter: input);

        }
        public APIGetRecordsDynamic<ProductWiseTable1> GetSalesInvoiceTable1(Invoice_ip input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductWiseTable1, Invoice_ip>(companyKey: companyKey, procedureName: "ProCmnSalesInvoicedata", parameter: input);

        }
        public APIGetRecordsDynamic<ProductWiseTable2> GetSalesInvoiceTable2(Invoice_ip input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductWiseTable2, Invoice_ip>(companyKey: companyKey, procedureName: "ProCmnSalesInvoicedata", parameter: input);

        }
        public APIGetRecordsDynamic<ProductWiseTable3> GetSalesInvoiceTable3(Invoice_ip input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductWiseTable3, Invoice_ip>(companyKey: companyKey, procedureName: "ProCmnSalesInvoicedata", parameter: input);

        }

        public class LeadfillQuoation
        {           

            public long FK_Master { get; set; }

        }
       
        public APIGetRecordsDynamicdn<dynamic> FilleadQuotation(LeadfillQuoation input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<LeadfillQuoation>(companyKey: companyKey, procedureName: "ProSalesQuotationDataFill", parameter: input);

        }
        public APIGetRecordsDynamic<ProductUnitDetails> GetProductUnitData(GetProductUnitDtls input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductUnitDetails, GetProductUnitDtls>(companyKey: companyKey, procedureName: "ProProductUnitSelect", parameter: input);

        }
        public APIGetRecordsDynamic<SalsRecalculateInfoview> GetSalesRecalculateInfo(SalsRecalculateInfoInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalsRecalculateInfoview, SalsRecalculateInfoInput>(companyKey: companyKey, procedureName: "ProSalesCalculationInfo", parameter: input);

        }
         public class EinvoiceData
        {
            public string jsonData { get; set; }
            public long FK_Sales { get; set; }
        }
        public Output AddEinviceData(EinvoiceData input, string companyKey)
        {
            return Common.UpdateTableData<EinvoiceData>(parameter: input, companyKey: companyKey, procedureName: "proEinvoicedetailsUpdate");
        }

    }
}



