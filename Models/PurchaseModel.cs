/*----------------------------------------------------------------------
Created By	: Aiswarya
Created On	: 31/01/2022
Purpose		: Purchase
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
    public class PurchaseModel
    {
        public class Custransmodes
        {
            public string CusTransMode { get; set; }
        }
        public class PurchaseView
        {

            [Required(ErrorMessage = "Please Enter Invoice No")]
            public string ReferenceNo { get; set; }
            [Required(ErrorMessage = "Please Enter Invoice No")]
            public Int32 InvoiceNo { get; set; }
            [Required(ErrorMessage = "Please Enter Invoice Date")]
            public DateTime InvoiceDate { get; set; }
           
            public decimal BillTotal { get; set; }
           
            public decimal Discount { get; set; }
           
            public decimal OtherCharge { get; set; }
          
            public decimal RoundOff { get; set; }
         
            public decimal NetAmount { get; set; }
           
            public decimal PayableAmount { get; set; }
          
            public bool TaxtyIntrastate { get; set; }
           
            public long Quotation { get; set; }
           
            public long BillType { get; set; }
           
            public long PurchaseOrder { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Supplier")]
            public long SupplierID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long BranchID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public long DepartmentID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public Int64 SlNo { get; set; }
            public Int64 ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public Int64 OctyTransType { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransMode { get; set; }
            public Int64 PurchaseID { get; set; }
            public List<PurchaseDetails> PurchaseDetails { get; set; }
            public List<TaxTypeDet> TaxDetails { get; set; }
            public List<TaxTypeDet> PTaxDetails { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public string SupplierName { get; set; }
            public string BranchName { get; set; }
            public string DepartmentName { get; set; }

            public string Mode { get; set; }

        }
        public class PurchaseViewNew
        {

            [Required(ErrorMessage = "Please Enter Invoice No")]
            public string ReferenceNo { get; set; }
            [Required(ErrorMessage = "Please Enter Invoice No")]
            public string InvoiceNo { get; set; }
            [Required(ErrorMessage = "Please Enter Invoice Date")]
            public DateTime InvoiceDate { get; set; }

            public decimal BillTotal { get; set; }

            public decimal Discount { get; set; }
           

            public decimal PurOtherCharge { get; set; }

            public decimal RoundOff { get; set; }

            public decimal NetAmount { get; set; }

            public decimal PayableAmount { get; set; }

            public bool TaxtyIntrastate { get; set; }

            public long Quotation { get; set; }

            public long BillType { get; set; }

            public long PurchaseOrder { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Supplier")]
            public long SupplierID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long BranchID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public long DepartmentID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public Int64 SlNo { get; set; }
            public Int64 ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public Int64 OctyTransType { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransMode { get; set; }
            public Int64 PurchaseID { get; set; }
            public List<PurchaseDetailsNew> PurchaseDetails { get; set; }
            public List<TaxTypeDet> TaxDetails { get; set; }
            public List<TaxTypeDet> PTaxDetails { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public string SupplierName { get; set; }
            public string BranchName { get; set; }
            public string DepartmentName { get; set; }

            public string Mode { get; set; }
            public decimal Amount { get; set; }

            public Int64 TotalCount { get; set; }


            public long ID_Purchase { get; set; }

            public long PurBillNo { get; set; }

            public string PurReferenceNo { get; set; }

            public string PurInvoiceNo { get; set; }

            public DateTime PurInvoiceDateSel { get; set; }
            public string PurInvoiceDate { get; set; }
            public long FK_Supplier { get; set; }
            public string SuppName { get; set; }
            public long FK_Department { get; set; }
            public string DeptName { get; set; }
            public decimal PurNetAmount { get; set; }
            public decimal PurBillTotal { get; set; }
            public decimal PurDiscount { get; set; }
            public decimal PurDiscountPer { get; set; }
            public decimal PurRoundOff { get; set; }
            public string PurNetAmountDec { get; set; }
            //public bool TaxtyIntrastate { get; set; }

            //public long Quotation { get; set; }

            //public long BillType { get; set; }
            public long ReasonID { get; set; }
            public long FK_PurchaseOrder { get; set; }
            public decimal PurAdvAmount { get; set; }
            public Int64? LastID { get; set; }
            public Int32 Statecode { get; set; }


        }


        public class TaxTypeDet
        {
            public long SlNo { get; set; }
            public long UID { get; set; }
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
            public class GetPurchase
        {
            public Int64 ID_Purchase { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class GetPurchaseNew
        {
            public Int64 ID_Purchase { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public int Details { get; set; }
            public int FK_BranchCodeUser { get; set; }


        }
        public class GetSubTablePurchase
        {
            //public string Mode { get; set; }
            //public string TransMode { get; set; }
            public Int64 ID_Purchase { get; set; }
            //public string EntrBy { get; set; }
            //public Int64 FK_Company { get; set; }
            //public Int64 FK_Machine { get; set; }
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 GroupID { get; set; }
        }

        public class GetSubTablePurchaseNew
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }

        public class PurchaseNewTax
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }

        public class PurchaseDetails
        {
            public decimal PpdQuantity { get; set; }
            public decimal PpdActualQuantity { get; set; }
            public decimal PpdFreeQuantity { get; set; }
            public decimal PpdShortage { get; set; }
            public decimal Ppdless { get; set; }
            public string PpdQRCode { get; set; }
            public string PpdBarCode { get; set; }
            public decimal PpdRate { get; set; }
            public decimal PpdActualRate { get; set; }
            public decimal MRP { get; set; }
            public decimal PpdTaxAmount { get; set; }
            public decimal PpdDiscount { get; set; }
            public decimal PpdProPer { get; set; }
            public decimal PpdProAmnt { get; set; }
            public decimal PpdDisper { get; set; }
            public decimal PpdTotalAmount { get; set; }
            public decimal SalPrice { get; set; }
            public decimal SalTaxAmount { get; set; }
            public decimal SalDiscount { get; set; }
            public decimal NetPurchase { get; set; }
            public Int64 FK_Purchase { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_SubProduct { get; set; }
            public Int64 UnitID { get; set; }
            public Boolean Includetax { get; set; }
            public DateTime ExpiryDate { get; set; }
            public Boolean InterState { get; set; }
            public string Description { get; set; }
           public string PpdBatchNo { get; set; }




        }

        public class PurchaseDetailsNew
        {
            public Int64 UID { get; set; }
            public Int64 ProductID { get; set; }
            public Int64 UnitID { get; set; }
            public string PpdQRCode { get; set; }
            public string PpdBarCode { get; set; }
            public decimal PpdQuantity { get; set; }
            public decimal PpdActualQuantity { get; set; }
            public decimal PpdFreeQuantity { get; set; }
            public decimal PpdShortage { get; set; }
            public decimal Ppdless { get; set; }
            public decimal PpdRate { get; set; }
            public decimal PpdActualRate { get; set; }
            public decimal PpdpurDiscper { get; set; }
            public decimal PpdpurDis { get; set; }
            public decimal MRP { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal PpdDiscount { get; set; }
            public decimal PpdProPer { get; set; }
            public decimal PpdProAmnt { get; set; }
            public decimal PpdDisper { get; set; }
            public decimal PpdTotalAmount { get; set; }
            public decimal SalPrice { get; set; }
            public decimal SalTaxAmount { get; set; }
            public decimal SalDiscount { get; set; }
            public decimal NetPurchase { get; set; }
            public Int64 FK_Purchase { get; set; }
            
            public Int64 FK_SubProduct { get; set; }
            
            public Boolean IncludeTax { get; set; }
            //public Boolean IncludeTax { get; set; }

            public Boolean InterState { get; set; }

            public string ProName { get; set; }
            public Int64 FK_Unit { get; set; }
            public string UnitName { get; set; }

            public DateTime? ExpiryDate { get; set; }
            public string Description { get; set; }
            public string PpdBatchNo { get; set; }
        }
        public class PurchaseDetailsNewn
        {
            public Int64 UID { get; set; }
            public Int64 ProductID { get; set; }
            public Int64 UnitID { get; set; }
            public string PpdQRCode { get; set; }
            public string PpdBarCode { get; set; }
            public string PpdQuantity { get; set; }
            public string PpdActualQuantity { get; set; }
            public decimal PpdFreeQuantity { get; set; }
            public decimal PpdShortage { get; set; }
            public decimal Ppdless { get; set; }
            public string PpdRate { get; set; }
            public decimal PpdActualRate { get; set; }
            public decimal PpdpurDiscper { get; set; }
            public decimal PpdpurDis { get; set; }
            public string MRP { get; set; }
            public string TaxAmount { get; set; }
            public string PpdDiscount { get; set; }
            public decimal PpdProPer { get; set; }
            public decimal PpdProAmnt { get; set; }
            public decimal PpdDisper { get; set; }
            public decimal PpdTotalAmount { get; set; }
            public string  SalPrice { get; set; }
            public decimal SalTaxAmount { get; set; }
            public decimal SalDiscount { get; set; }
            public string  NetPurchase { get; set; }
            public Int64 FK_Purchase { get; set; }

            public Int64 FK_SubProduct { get; set; }

            public Boolean IncludeTax { get; set; }
            //public Boolean IncludeTax { get; set; }

            public Boolean InterState { get; set; }

            public string ProName { get; set; }
            public Int64 FK_Unit { get; set; }
            public string UnitName { get; set; }

            public DateTime ?ExpiryDate { get; set; }
            public string Description { get; set; }
            public string PpdBatchNo { get; set; }
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
            public bool TaxtyInterstate { get; set; }
           
        }

        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }

            public class UpdatePurchase
        {
            public byte UserAction { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Purchase { get; set; }
            //public Int32 PurBillNo { get; set; }
            //public string PurReferenceNo { get; set; }
            public Int32 PurInvoiceNo { get; set; }
            public DateTime PurInvoiceDate { get; set; }
            public decimal PurBillTotal { get; set; }
            public decimal PurDiscount { get; set; }
            public decimal PurOtherCharge { get; set; }
            public decimal PurRoundOff { get; set; }
            public decimal PurNetAmount { get; set; }
            public decimal PurPayableAmount { get; set; }
            public bool TaxtyIntrastate { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_BillType { get; set; }
            public long FK_PurchaseOrder { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
          //  public bool Passed { get; set; }
           
            public string EntrBy { get; set; }
            
            
            public long FK_Machine { get; set; }
         
            public string PurchaseDetails { get; set; }
            public string TaxDetails { get; set; }
            public string PTaxDetails { get; set; }
            
            public string OtherChargeDetails { get; set; }
            public long FK_Purchase { get; set; }
        }

        public class UpdatePurchaseNew
        {
            public byte UserAction { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Purchase { get; set; }
            public string PurInvoiceNo { get; set; }
            public DateTime PurInvoiceDate { get; set; }
            public decimal PurBillTotal { get; set; }
            public decimal PurDiscount { get; set; }
            public decimal PurDiscountPer { get; set; }
            public decimal PurOtherCharge { get; set; }
            public decimal PurRoundOff { get; set; }
            public decimal PurNetAmount { get; set; }
            public decimal PurPayableAmount { get; set; }
            public bool TaxtyIntrastate { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_BillType { get; set; }
            public long FK_PurchaseOrder { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //  public bool Passed { get; set; }

            public string EntrBy { get; set; }


            public long FK_Machine { get; set; }

            public string PurchaseDetails { get; set; }
            public string TaxDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public long FK_Purchase { get; set; }
            public decimal PurAdvAmount { get; set; }
            public Int64? LastID { get; set; } = 0;
            public string PTaxDetails { get; set; }
        }


        public static string _deleteProcedureName = "ProPurchaseDelete";
        public static string _updateProcedureName = "ProPurchaseUpdate";
        public static string _selectProcedureName = "ProPurchaseSelect";

        public class DeletePurchaseold
        {
            public long ID_Purchase { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
        }
        public class Supplierlist
        {
            public long SupplierID { get; set; }
            public string SupplierName { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
            public string GSTIN { get; set; }
            public Int32 Statecode { get; set; }


        }
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class BranchList
        {
            public long BranchID { get; set; }
            public string BranchName { get; set; }
            public string BranchCode { get; set; }
            public string ShortName { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string DepartmentName { get; set; }

             public string ShortName { get; set; }
        }
        public class DropDownListModel
        {
            public List<Supplierlist> SupplierView { get; set; }
            public List<Unit> UnitList { get; set; }
            public List<ModuleType> ModuleTypeList { get; set; }
        }
        public class ModuleType
        {
            public long ID_ModuleType { get; set; }
            public string ModuleName { get; set; }

            public string Mode { get; set; }
        }
        public class DeletePurchase
        {
            public long FK_Purchase { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public class Purchasefill
        {

            public int Mode { get; set; }

            public long FK_Master { get; set; }

        }
        public class PurchaseID
        {
            public Int64 ID_Purchase { get; set; }
        }
        public Output UpdatePurchaseData(UpdatePurchase input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePurchase>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeletePurchaseData(DeletePurchase input, string companyKey)
        {
            return Common.UpdateTableData<DeletePurchase>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<PurchaseViewNew> GetPurchaseData(GetPurchase input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseViewNew, GetPurchase>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<PurchaseViewNew> FillTax(BindTaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseViewNew, BindTaxAmount>(companyKey: companyKey, procedureName: "proTaxCalculation", parameter: input);

        }
        public APIGetRecordsDynamic<PurchaseViewNew> FillTaxNew(BindTaxAmountNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseViewNew, BindTaxAmountNew>(companyKey: companyKey, procedureName: "proTaxCalculation", parameter: input);

        }
        public APIGetRecordsDynamic<PurchaseViewNew> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseViewNew, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PurchaseDetails> GetPurchaseDetails(GetPurchase input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseDetails, GetPurchase>(companyKey: companyKey, procedureName: "ProPurchaseDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDet> GetTaxDetails(GetSubTablePurchase input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDet, GetSubTablePurchase>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDet> GetTaxDetailsNew(PurchaseNewTax input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDet, PurchaseNewTax>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTablePurchaseNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTablePurchaseNew>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public Output UpdatePurchaseDataNew(UpdatePurchaseNew input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePurchaseNew>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<PurchaseViewNew> GetPurchaseDataNew(GetPurchaseNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseViewNew, GetPurchaseNew>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<PurchaseDetailsNewn> GetPurchaseDetailsNew(GetPurchaseNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseDetailsNewn, GetPurchaseNew>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }


        public APIGetRecordsDynamicdn<dynamic> FilPurchase(Purchasefill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<Purchasefill>(companyKey: companyKey, procedureName: "ProPurchaseDataFill", parameter: input);

        }
        public class PurchaseProducttopview
        {
            public long  ProductID { get; set; }
            public long DepartmentID { get; set; }
            public long BranchID { get; set; }
            public long FK_Company { get; set; }
        }
        public class PurchaseTopRateDetails
        {
            public decimal SalePrice { get; set; }
            public decimal MRP { get; set; }

        }
        public APIGetRecordsDynamic<PurchaseTopRateDetails> GetProductTopRate(PurchaseProducttopview input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseTopRateDetails, PurchaseProducttopview>(companyKey: companyKey, procedureName: "ProGetPurchaseTopProductRate", parameter: input);

        }

    }
}


