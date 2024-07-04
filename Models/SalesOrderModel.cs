


/*----------------------------------------------------------------------
Created By	: Riyas
Created On	: 20/07/2022
Purpose		: SalesOrder
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
    public class SalesOrderModel
    {
        //public long ID_SalesOrder { get; set; }
        public class SalesOrderView
        {
            public Int64 ID_SalesOrder { get; set; }
            public Int64 SlNo { get; set; }
            public Int64 ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public Int64 OctyTransType { get; set; }
            public decimal OctyAmount { get; set; }
            [Required(ErrorMessage = "Please Enter Mode")]
            public string Mode { get; set; }
            [Required(ErrorMessage = "Please Enter So No")]
            public string SoNo { get; set; }
            [Required(ErrorMessage = "Please Enter So Reference No")]
            public string SoReferenceNo { get; set; }
            [Required(ErrorMessage = "Please Enter So Date")]
            public DateTime SoDate { get; set; }
          
            [Required(ErrorMessage = "Please Enter So Deliverydate")]
            public DateTime? SoDeliverydate { get; set; }
            [Required(ErrorMessage = "Please Enter So Advc Amount")]
            public decimal SoAdvcAmount { get; set; }
            [Required(ErrorMessage = "Please Enter So Remarks")]
            public string SoRemarks { get; set; }
            [Required(ErrorMessage = "Please Enter So Bill Total")]
            public decimal SoBillTotal { get; set; }
            [Required(ErrorMessage = "Please Enter So Discount")]
            public decimal SoDiscount { get; set; }
            [Required(ErrorMessage = "Please Enter So Othercharges")]
            public decimal SoOthercharges { get; set; }
            [Required(ErrorMessage = "Please Enter So Roundoff")]
            public decimal SoRoundoff { get; set; }
            [Required(ErrorMessage = "Please Enter So Net Amount")]
            public decimal SoNetAmount { get; set; }
            [Required(ErrorMessage = "Please Enter So Quotation")]
            public bool SoQuotation { get; set; }
            [Required(ErrorMessage = "Please Enter Taxty Intrastate")]
            public bool TaxtyIntrastate { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Customer")]
            public string Customer { get; set; }
            public long FK_Customer { get; set; }
            public long FK_LeadGenerate { get; set; }
            public long FK_Quotation { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public string SoAddress { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead Generate")]
            public long LeadGenerate { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Quotation")]
            public long Quotation { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long Branch { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public long Department { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Brach Code User")]
            public long BrachCodeUser { get; set; }
            public Int64 TotalCount { get; set; }
          
            public int SortOrder { get; set; }
            public List<ModuleType> ModuleTypeList { get; set; }
            public Int64? LastID { get; set; }

            public long FK_FinancePlan { get; set; }
            public decimal SoAddnAmount { get; set; }
            public decimal SoDownPayment { get; set; }
            public DateTime? SoStartDate { get; set; }
            public Int64? FK_AssignedTo { get; set; }
            public string AssignedEmployee { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public long FK_BillType { get; set; }
            public List<CostCenter> CostCenterList { get; set; }
            public string Costcenter { get; set; }
            public long ?FK_CostCenterDetails { get; set; }
            public List<ProductLocationList> ProductLocationList { get; set; }
            public long ?FK_ProductLocation { get; set; }

            public string SODescription { get; set; }
            public string CCName { get; set; }
          
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string FSoDeliverydate { get; set; }
            public string FSoDate { get; set; }
            public long FK_Employee { get; set; }
            public string Employee { get; set; }


        }
        public class SalesOrderShortView
        {
            public Int64 ID_SalesOrder { get; set; }
            public Int64 SlNo { get; set; }
            public string SoNo { get; set; }
            public DateTime SoDate { get; set; }
            public string Contact_Name { get; set; }
            public string Contact_No { get; set; }
            public decimal SoAdvcAmount { get; set; }
            public decimal SoNetAmount { get; set; }          
            public Int64 TotalCount { get; set; }
            public Int64? LastID { get; set; }

        }
        public class SalesOrderViewNew
        {
            public long ID_SalesOrder { get; set; }
            public string TransMode { get; set; }
            [Required(ErrorMessage = "Please Enter Mode")]
            public string Mode { get; set; }
            [Required(ErrorMessage = "Please Enter So No")]
            public Int32 SoNo { get; set; }
            [Required(ErrorMessage = "Please Enter So Reference No")]
            public string SoReferenceNo { get; set; }
            [Required(ErrorMessage = "Please Enter So Date")]
            public DateTime SoDate { get; set; }
            [Required(ErrorMessage = "Please Enter So Deliverydate")]
            public DateTime? SoDeliverydate { get; set; }
            [Required(ErrorMessage = "Please Enter So Advc Amount")]
            public decimal SoAdvcAmount { get; set; }
            [Required(ErrorMessage = "Please Enter So Remarks")]
            public string SoRemarks { get; set; }
            [Required(ErrorMessage = "Please Enter So Bill Total")]
            public decimal SoBillTotal { get; set; }
            [Required(ErrorMessage = "Please Enter So Discount")]
            public decimal SoDiscount { get; set; }
            [Required(ErrorMessage = "Please Enter So Othercharges")]
            public decimal SoOthercharges { get; set; }
            [Required(ErrorMessage = "Please Enter So Roundoff")]
            public decimal SoRoundoff { get; set; }
            [Required(ErrorMessage = "Please Enter So Net Amount")]
            public decimal SoNetAmount { get; set; }
            [Required(ErrorMessage = "Please Enter So Quotation")]
            public bool SoQuotation { get; set; }
            [Required(ErrorMessage = "Please Enter Taxty Intrastate")]
            public bool TaxtyIntrastate { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Customer")]
            public long Customer { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead Generate")]
            public long LeadGenerate { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Quotation")]
            public long Quotation { get; set; }
            public long FK_Customer { get; set; }
            public long FK_LeadGenerate { get; set; }
            public long FK_Quotation { get; set; }
           
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long Branch { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public long Department { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Brach Code User")]
            public long BrachCodeUser { get; set; }

            public int SortOrder { get; set; }
            public string SoAddress { get; set; }
            public List<SalesOrderDetailsView> SalesOrderDetail { get; set; }
            public List<TaxTypeDet> TaxDetails { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public Int64? LastID { get; set; }

            public DateTime? EMIDate { get; set; }
            public long FK_FinancePlan { get; set; }
            public List<ProductDetailsEMICalc> ProductDetails { get; set; }
            public List<InstallmenetDetails> InstallmentDetails { get; set; }
            public Int32 ProductWise { get; set; }
            public decimal AdditionalAmount { get; set; }
            public decimal DownPayment { get; set; }
            public string MobileNo { get; set; }
            public long FK_AssignedTo { get; set; }
            public long BillType { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public long ?CodeCenter_ID { get; set; }
            public long FK_ProductLocation { get; set; }
            public string SODescription { get; set; }
            public long FK_Employee { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class SalesOrderDetailsView
        {
           
            public long ID_SalesOrderDetails { get; set; }
            public long FK_SalesOrder { get; set; }
            public long FK_Product { get; set; }
            public decimal SodSalQuantity { get; set; }
            public decimal SodSalFreeQuantity { get; set; }
            public decimal SodSalActualQuantity { get; set; }
            public decimal SodMRP { get; set; }
            public decimal SodSalPrice { get; set; }
            public decimal SodSalActualPrice { get; set; }
            public decimal SodSalTaxAmount { get; set; }
            public decimal SodSalDiscountPercent { get; set; }
            public decimal SodSalDiscount { get; set; }
            public decimal SodSalTotalAmount { get; set; }
            public string SodBatchNo { get; set; }
            public string SodRemarks { get; set; }
            public long FK_Stock { get; set; }
            public bool Cancelled { get; set; }
            public string CancelBy { get; set; }
            public DateTime? CancelOn { get; set; }
            public string ProdName { get; set; }
            public string SoAddress { get; set; }
            public decimal Sprice { get; set; }

            public DateTime? Emidate { get; set; }
            public decimal AdditionalPay { get; set; }
            public decimal Downpay { get; set; }
            public decimal InstallmentAmount { get; set; }
            public long FK_FinancePlan { get; set; }
            public long FK_ProductLocation { get; set; }

        }
        public class SalesOrderProductDetails
        {
           
            public Int32 SlNo { get; set; }
            public long FK_Product { get; set; }
            public string ProdName { get; set; }
            public string SodSalQuantity { get; set; }
            public string SodMRP { get; set; }
            public string SodSalPrice { get; set; }
            public string SodSalTaxAmount { get; set; }
            public string SodSalDiscountPercent { get; set; }
            public string SodSalDiscount { get; set; }
            public string SodSalTotalAmount { get; set; }
            public string SodRemarks { get; set; }          
            public string Sprice { get; set; }

            public string FK_FinancePlan { get; set; }
            public DateTime? Emidate { get; set; }
            public string Downpay { get; set; }
            public string AdditionalPay { get; set; }
            public string InstallmentAmount { get; set; }
            public bool EMIProduct { get; set; }
            public long FK_ProductLocation { get; set; }
            public string Location { get; set; }
            public string ProductDescription { get; set; }
            public string ProdCode { get; set; }
            public string MinRate { get; set; }
            public string MaxRate { get; set; }
        }

        public class UpdateSalesOrderDetails
        {
            public long ID_SalesOrderDetails { get; set; }
            public long FK_SalesOrder { get; set; }
            public long FK_Product { get; set; }
            public decimal SodSalQuantity { get; set; }
            public decimal SodSalFreeQuantity { get; set; }
            public decimal SodSalActualQuantity { get; set; }
            public decimal SodMRP { get; set; }
            public decimal SodSalPrice { get; set; }
            public decimal SodSalActualPrice { get; set; }
            public decimal SodSalTaxAmount { get; set; }
            public decimal SodSalDiscountPercent { get; set; }
            public decimal SodSalDiscount { get; set; }
            public decimal SodSalTotalAmount { get; set; }
            public string SodBatchNo { get; set; }
            public string SodRemarks { get; set; }
            public long FK_Stock { get; set; }
            public bool Cancelled { get; set; }
            public string CancelBy { get; set; }
            public DateTime CancelOn { get; set; }
            public string SODescription { get; set; }
        }
        public class TaxTypeDet
        {
            public long SlNo { get; set; }
            public long FK_TaxType { get; set; }

            public string TaxtyName { get; set; }

            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long ProductID { get; set; }
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
        public class GetOtherCharge
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }

        public class UpdateSalesOrder
        {

             public int UserAction { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_SalesOrder { get; set; }
            public string SoNo { get; set; }
            public string SoReferenceNo { get; set; }
            public DateTime SoDate { get; set; }
            public DateTime? SoDeliverydate { get; set; }
            public decimal SoAdvcAmount { get; set; }         
            public decimal SoBillTotal { get; set; }
            public decimal SoDiscount { get; set; }
            public decimal SoOthercharges { get; set; }
            public decimal SoRoundoff { get; set; }
            public decimal SoNetAmount { get; set; }
            public bool TaxtyIntrastate { get; set; }
            public long FK_Customer { get; set; }
            public long FK_LeadGenerate { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_BrachCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long PreviousID { get; set; }
            public string SalesOrderDetails { get; set; }
            public string TaxDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public long FK_SalesOrder { get; set; }
            public Int64? LastID { get; set; } = 0;

            //public DateTime? EMIDate { get; set; }
            //public long FK_FinancePlan { get; set; }
            public string ProductDetails { get; set; }
            public string InstallmenetDetails { get; set; }
            //public Int32 ProductWise { get; set; }
            public decimal AdditionalAmount { get; set; }
            public decimal DownPayment { get; set; }
            public string MobileNo { get; set; }
            public long FK_AssignedTo { get; set; }
            public long FK_BillType { get; set; }
            public string PaymentDetail { get; set; }
            public long FK_CostCenterDetails { get; set; }
            public string SODescription { get; set; }
            public long FK_Employee { get; set; }

        }
        public static string _deleteProcedureName = "ProSalesOrderDelete";
        public static string _updateProcedureName = "ProSalesOrderUpdate"; 
        public static string _selectProcedureName = "ProSalesOrderSelect";

        public class DeleteSalesOrder
        {
            public long ID_SalesOrder { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public int FK_Company { get; set; }

            public int FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class SalesOrderInfoView
        {
            public Int64 SalesOrderID { get; set; }
            public int ReasonID { get; set; }
            public string TransMode { get; set; }
        }
        public class GetSalesOrder
        {
            public Int64 ID_SalesOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class GetQuotation
        {
            public Int64 ID_SalesOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
        }
        public class QuotationView
        {           
            public Int64 ID_SalesOrder { get; set; }
            public Int64 SlNo { get; set; }
           
            public Int32 QuotationNo { get; set; }
            public string Date { get; set; }
           
            public decimal NetAmount { get; set; }

        }
        public class SalesOrderSelectDetails
        {
            public Int64 ID_SalesOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
        }
        public class SalesOrderItemDetails
        {
            public Int64 ID_SalesOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
           
        }
        public class SalesOrderID
        {
            public Int64 ID_SalesOrder { get; set; }
        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
       
        public class ModuleType
        {
            public long ID_ModuleType { get; set; }
            public string ModuleName { get; set; }

            public string Mode { get; set; }
        }
        public class Floor
        {
            public long ID_Floor { get; set; }
            public string FloorName { get; set; }

          
        }

        public class DropDownListModel
        {
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }

            public List<ModuleType> ModuleTypeList { get; set; }
        }
        public class BindTaxAmount
        {
            public long FK_Product { get; set; }
            public decimal PurRate { get; set; }
            public decimal Quantity { get; set; }
            public Boolean Includetax { get; set; }
            public Boolean InterState { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class BindTaxAmountNew
        {
            public long FK_Product { get; set; }
            public decimal Amount { get; set; }
            public decimal Quantity { get; set; }
            public int Sales { get; set; }
            public long Includetax { get; set; }

        }
        public class OutputGetSalesOrderNo
        {
            public int SalesOrderNumber { get; set; }
        }
        public class InputGetSalesOrderNo
        {
            public long FK_Company { get; set; }
        }
        public class SalesOrderAll
        {
            

            public List<SalesOrderView> SalesOrderViewData { get; set; }
            public List<SalesOrderDetailsView> SalesOrderDetailsViewData { get; set; }
            
        }
        public class GetProductEMIPlans
        {
            public long ProductID { get; set; }
            public decimal Amount { get; set; }
            public long FK_Company { get; set; }
        }
        public class CalculateEMI
        {
            public DateTime Date { get; set; }
            public long FK_FinancePlan { get; set; }
            public decimal Downpayment { get; set; }
            public decimal AdditionalAmount { get; set; }
            public decimal Installment { get; set; }
            public decimal BillAmount { get; set; }
        }
        public class ProductEMI
        {
            public List<ProductDetails> ProductDetails { get; set; }
        }
        public class ProductEMICalc
        {
            public DateTime EMIDate { get; set; }
            public long FK_FinancePlan { get; set; }
            public Int32 ProductWise { get; set; }
            public decimal Downpayment { get; set; }
            public decimal AdditionalAmount { get; set; }
            public decimal Installment { get; set; }
            public decimal BillAmount { get; set; }
        }
        public class ProductDetails
        {
            public long ID_Product { get; set; }
            public decimal Amount { get; set; }
        }
        public class ProductDetailsEMICalc
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            public decimal Amount { get; set; }
        }
        public class InstallmenetDetails
        {
            public long SLNO { get; set; }
            public long FK_FinancePlan { get; set; }
            public long FK_Product { get; set; }
            public DateTime EMIDate { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }
        }
        public class EMIPlans
        {
            public long SLNo { get; set; }
            public long PlanID { get; set; }
            public string PlanName { get; set; }
            public long Duration { get; set; }
            public long Period { get; set; }
            public decimal DownPayment { get; set; }
            public decimal InstalmentAmt { get; set; }
            public decimal AddnAmount { get; set; }
            public decimal BillAmount { get; set; }
            public string Errmsg { get; set; }

        }
        public class EMICalcuationdata
        {
            public Int64 SLNo { get; set; }
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
        public class EMIDetails
        {
            public long SLNo { get; set; }
            public long ID_Product { get; set; }
            public long ID_SalesOrderDetails { get; set; }
            public string ProdName { get; set; }
            public Int64 SodEMISale { get; set; }
        }
        public class EMIInstallmentDetails
        {
            public long SLNO { get; set; }
            public long FK_FinancePlan { get; set; }
            public long FK_Product { get; set; }
            public string EMIDate { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }
        }
        public class Leadfill
        {
            public int IsLead { get; set; }
            public long FK_Master { get; set; }
        }
        public class LeadFillSalesOrder
        {
            public long SLNo { get; set; }
            public Int32 ProductID { get; set; }
            public string ProName { get; set; }
            public string SpdSalQuantity { get; set; }
            public string SalePrice { get; set; }
            public string MRPs { get; set; }
            public string Discp { get; set; }
            public string Discamt { get; set; }
            public string TaxAmount { get; set; }
            public string NetAmt { get; set; }
            public long StockId { get; set; }
            public long FK_ProductLocation { get; set; }
        }
        public class LeadFillReSalesOrder
        {
            public long SlNo { get; set; }
            public Int32 FK_Product { get; set; }
            public string ProdName { get; set; }
            public string SodSalQuantity { get; set; }
            public string SodSalPrice { get; set; }
            public string SodMRP { get; set; }
            public string SodSalDiscountPercent { get; set; }
            public string SodSalDiscount { get; set; }
            public string SodSalTaxAmount { get; set; }
            public string SodSalTotalAmount { get; set; }
            public string Sprice { get; set; }
            public long StockId { get; set; }
            public long FK_ProductLocation { get; set; } 
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
        public class CostCenter
        {
            public string CCName { get; set; }
            public Int64 FK_Employee { get; set; }
            public Int64 ID_CostCenterDetails { get; set; }
        }
        public class Fk_companyModel
        {
           public Int64 FK_Company { get; set; }
        }
        public class ProductLocationList
        {
            public long FK_ProductLocation { get; set; }
            public string ProductLocation { get; set; }
           
        }

        public APIGetRecordsDynamic<CostCenter> GetCostCenterDropdownData(Fk_companyModel input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostCenter, Fk_companyModel>(companyKey: companyKey, procedureName: "proGetCostCenterDropdown", parameter: input);
        }
        public Output UpdateSalesOrderData(UpdateSalesOrder input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSalesOrder>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSalesOrderData(DeleteSalesOrder input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSalesOrder>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<SalesOrderShortView> GetSalesOrderData(GetSalesOrder input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderShortView, GetSalesOrder>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<SalesOrderView> GetQuotationData(GetSalesOrder input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderView, GetSalesOrder>(companyKey: companyKey, procedureName: "ProQuotationSelect", parameter: input);
        }
        public APIGetRecordsDynamic<QuotationView> GetQuotationListData(GetQuotation input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationView, GetQuotation>(companyKey: companyKey, procedureName: "ProQuotationList", parameter: input);
        }
       
        public APIGetRecordsDynamic<SalesOrderView> GetSalesOrderSelectDetails(SalesOrderSelectDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderView, SalesOrderSelectDetails>(companyKey: companyKey, procedureName: "ProSalesOrderSelectDetails", parameter: input);
        }
        public APIGetRecordsDynamic<SalesOrderProductDetails> GetSalesOrderItemDetailsSelect(SalesOrderItemDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderProductDetails, SalesOrderItemDetails>(companyKey: companyKey, procedureName: "ProSalesOrderItemDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<SalesOrderView> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderView, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelect", parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetOtherCharge>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<SalesOrderView> FillTaxNew(BindTaxAmountNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderView, BindTaxAmountNew>(companyKey: companyKey, procedureName: "proTaxCalculation", parameter: input);
        }

        //public APIGetRecordsDynamicMulti<SalesOrderAll> GetSalesOrderSelectDetails1(SalesOrderSelectDetails input, string companyKey)
        //{
        //    return Common.GetDatasViaProcedure<SalesOrderAll, SalesOrderSelectDetails>(companyKey: companyKey, procedureName: "ProSalesOrderSelectDetailsMulti", parameter: input);
        //}
        public APIGetRecordsDynamic<EMIPlans> GetProductEMIData(GetProductEMIPlans input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIPlans, GetProductEMIPlans>(companyKey: companyKey, procedureName: "ProFindEMIPlans", parameter: input);
        }
        public APIGetRecordsDynamic<EMICalcuationdata> EMICalculate(CalculateEMI input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMICalcuationdata, CalculateEMI>(companyKey: companyKey, procedureName: "ProEMICalculate", parameter: input);
        }
        public APIGetRecordsDynamic<EMIDetails> GetEMIDetailsSelects(GetEMIDetailsSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIDetails, GetEMIDetailsSelect>(companyKey: companyKey, procedureName: "ProEMIDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<EMIInstallmentDetails> GetEMIInstallmentDetailsSelects(GetEMIDetailsSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIInstallmentDetails, GetEMIDetailsSelect>(companyKey: companyKey, procedureName: "ProEMIDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<LeadFillSalesOrder> Fillead(Leadfill input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadFillSalesOrder, Leadfill>(companyKey: companyKey, procedureName: "ProSalesDataFill", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
       
    }
}