/*----------------------------------------------------------------------
Created By	: Anson Paul
Created On	: 01/09/2022
Purpose		: PurchaseReturn
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
    public class PurchaseReturnModel
    {

        public class PurchaseReturnView
        {
            public long SlNo { get; set; }
            public long ID_PurchaseReturn { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Bill No")]
            public string PrBillNo { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Reference No")]
            public string PrReferenceNo { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Bill Date")]
            public DateTime PrBillDate { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Invoice No")]
            public string Supplier { get; set; }
            public string PrInvoiceNo { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Invoice Date")]
            public DateTime PrInvoiceDate { get; set; }
           // [Required(ErrorMessage = "Please Enter Pr Bill Total")]
            public decimal PrBillTotal { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Discount")]
            
            public decimal PrDiscount { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Othercharges")]
            public decimal PrOthercharges { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Roundoff")]
            public decimal PrRoundoff { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Net Amount")]
            public decimal TotalTax { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Remarks")]
            public string PrRemarks { get; set; }
            //[Required(ErrorMessage = "Please Enter Taxty Intrastate")]
           // public bool TaxtyIntrastate { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Supplier")]
            public long FK_Supplier { get; set; }
            
            // [Range(1, long.MaxValue, ErrorMessage = "Select Bill Type")]
            public long BillType { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            //public long Branch { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Purchase")]
            public long FK_Purchase { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public string Department { get; set; }
            public long FK_Department { get; set; }
            public decimal PrNetAmount { get; set; }
            public List<PurchaseReturnDetailsNew> PurchaseReturnDetails { get; set; }
            public List<TaxTypeSplit> TaxDetails { get; set; }
            public List<OtherchargeDetails> OtherChgDetails { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public long ReasonID { get; set; }
            public Int64? LastID { get; set; }
            public List<Unit> UnitList { get; set; }
            public long PRReturnType { get; set; }
            public long Statecode { get; set; }
        }
        public class PurchaseReturnViewList
        {
            public long SlNo { get; set; }
            public long ID_PurchaseReturn { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Bill No")]
            public string PrBillNo { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Reference No")]
            public string PrReferenceNo { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Bill Date")]
            public DateTime PrBillDate { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Invoice No")]
            public string Supplier { get; set; }
            public string PrInvoiceNo { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Invoice Date")]
            public DateTime PrInvoiceDate { get; set; }
            // [Required(ErrorMessage = "Please Enter Pr Bill Total")]
            public decimal PrBillTotal { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Discount")]

            public decimal PrDiscount { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Othercharges")]
            public decimal PrOthercharges { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Roundoff")]
            public decimal PrRoundoff { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Net Amount")]
            public decimal TotalTax { get; set; }
            //[Required(ErrorMessage = "Please Enter Pr Remarks")]
            public string PrRemarks { get; set; }
            //[Required(ErrorMessage = "Please Enter Taxty Intrastate")]
            // public bool TaxtyIntrastate { get; set; }
            // [Range(1, long.MaxValue, ErrorMessage = "Select Supplier")]
            public long FK_Supplier { get; set; }

            // [Range(1, long.MaxValue, ErrorMessage = "Select Bill Type")]
            public long BillType { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            //public long Branch { get; set; }
            // [Range(1, long.MaxValue, ErrorMessage = "Select Purchase")]
            public long FK_Purchase { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public string Department { get; set; }
            public long FK_Department { get; set; }
            public string PrNetAmount { get; set; }
            public List<PurchaseReturnDetailsNew> PurchaseReturnDetails { get; set; }
            public List<TaxTypeSplit> TaxDetails { get; set; }
            public List<OtherchargeDetails> OtherChgDetails { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public long ReasonID { get; set; }
            public Int64? LastID { get; set; }

        }
        public class OtherchargeDetails
        {
            public long SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public Int64 OctyTransType { get; set; }
            public string OctyName { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransType { get; set; }
            public Int64 TransTypeID { get; set; }
        }
        public class UpdatePurchaseReturn
        {
            public byte UserAction { get; set; }
            public long ID_PurchaseReturn { get; set; }
            public string PrBillNo { get; set; }
            public string PrReferenceNo { get; set; }
            public DateTime PrBillDate { get; set; }
            public string PrInvoiceNo { get; set; }
            public DateTime PrInvoiceDate { get; set; }
            //public decimal TotalTax { get; set; }
            public decimal PrBillTotal { get; set; }
            public decimal PrDiscount { get; set; }
            public decimal PrOthercharges { get; set; }
            public decimal PrRoundoff { get; set; }
            public decimal PrNetAmount { get; set; }
            public string PrRemarks { get; set; }
            public byte TaxtyIntrastate { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_BillType { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Purchase { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            //public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
           //public DateTime EntrOn { get; set; }
            //public string CancelBy { get; set; }
            //public DateTime CancelOn { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            //public long BackupId { get; set; }
            public string PurchaseReturnDetails { get; set; }
            public string TransMode { get; set; }
            public string TaxDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public string ApprovedBy { get; set; }
            //public DateTime ApprovedOn { get; set; }
            //public long FK_PurchaseReturn { get; set; }
            public Int32 Debug { get; set; }
            public Int64? LastID { get; set; } = 0;
            public long PRReturnType { get; set; }
        }
        public class PurchaseReturnDetailsNew
        {
            public long FK_Product { get; set; }
            public string Product { get; set; }
            public string Unit { get; set; }
            public long UnitID { get; set; }
            public decimal Quantity { get; set; }
            public decimal free { get; set; }
            public decimal ActualQty { get; set; }
            public decimal PurchaseRate { get; set; }
            public long FK_Purchase { get; set; }
            public long FK_Stock { get; set; }
            public long UID { get; set; }
            public decimal Tax { get; set; }
            public decimal TotalRate { get; set; }
            public Int32 IncludeTax { get; set; }
            public decimal Discount { get; set; }
        }
        public class GetPurchaseReturn
        {
            public Int64 FK_PurchaseReturn { get; set; }
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


        //public class PurchaseReturnInfo
        //{
        //    public Int64 PurchaseReturnID { get; set; }
        //}
        public class TaxTypeSplit
        {
            public long SlNo { get; set; }
            public long UID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long ProductID { get; set; }
        }
        public class PurchaseReturnfill
        {
            public int Mode { get; set; }
            public long FK_Master { get; set; }
        }
        public class GetOtherchargeDetails
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class DeletePurchaseReturn
        {
            public long FK_PurchaseReturn { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public class PurchaseTaxGet
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
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
      
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class GetProductUnitDtls
        {
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class ProductUnitDetails
        {
            public long FK_Unit { get; set; }

            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class PurchaseReturnID
        {
            public Int64 ID_PurchaseReturn { get; set; }
        }

        public static string _deleteProcedureName = "ProPurchaseReturnDelete";
        public static string _updateProcedureName = "ProPurchaseReturnUpdate";
        public static string _selectProcedureName = "ProPurchaseReturnSelect";

        public Output UpdatePurchaseReturnData(UpdatePurchaseReturn input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePurchaseReturn>(parameter: input, companyKey: companyKey, procedureName: "ProPurchaseReturnUpdate");
        }
        public Output DeletePurchaseReturnData(DeletePurchaseReturn input, string companyKey)
        {
            return Common.UpdateTableData<DeletePurchaseReturn>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<PurchaseReturnView> GetPurchaseReturnData(GetPurchaseReturn input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseReturnView, GetPurchaseReturn>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<PurchaseReturnViewList> GetPurchaseReturnDataForList(GetPurchaseReturn input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseReturnViewList, GetPurchaseReturn>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> GetPurchaseReturnProductData(GetPurchaseReturn input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetPurchaseReturn>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> FilPurchaseReturn(PurchaseReturnfill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<PurchaseReturnfill>(companyKey: companyKey, procedureName: "ProPurchaseDataFill", parameter: input);

        }
        public APIGetRecordsDynamic<OtherchargeDetails> GetOthrChargeDetails(GetOtherchargeDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherchargeDetails, GetOtherchargeDetails>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDet> GetTaxDetailsNew(PurchaseTaxGet input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDet, PurchaseTaxGet>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ProductUnitDetails> GetProductUnitData(GetProductUnitDtls input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductUnitDetails, GetProductUnitDtls>(companyKey: companyKey, procedureName: "ProProductUnitSelect", parameter: input);

        }
    }
}

