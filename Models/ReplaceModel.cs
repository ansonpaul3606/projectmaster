using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class ReplaceModel
    {
        public class PurchaseReturnView
        {
            public Int64 ID_PurchaseReturn { get; set; }
            
        }
        public class ReplaceInput
        {
            public Int64 ReplaceID { get; set; }
            public string TransMode { get; set; }
            public DateTime TransDate { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_PurchaseReturn { get; set; }
            public bool PurchaseReturn { get; set; }
            public decimal RepFromTotal { get; set; }
            public decimal RepFromRoundOff { get; set; }
            public decimal RepFromNetAmount { get; set; }
            public List<ReplaceFromProductDetails> ReplaceFromDetails { get; set; }
            public List<TaxTypeRet> RepFTaxDetails { get; set; }

            public decimal RepToTotal { get; set; }
            public decimal RepToDiscountAmount { get; set; }
            public decimal RepToDiscountPer { get; set; }
            public decimal RepToRoundOff { get; set; }
            public decimal RepToNetAmount { get; set; }
            public List<TaxTypeTo> RepTTaxDetails { get; set; }
            public List<ReplaceToProductDetails> ReplaceToDetails { get; set; }

            public Int64 LastID { get; set; }
            
        }
        public class GetPurchaseReturn
        {
            public Int64 FK_PurchaseReturn { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
            public int Details { get; set; }
            public int FK_BranchCodeUser { get; set; }
        }
        public class PurchaseReturnDetails
        {
            public long SLNo { get; set; }
            public Int64 FK_PurchaseReturn { get; set; }
            public long ProductID { get; set; }
            public long FK_Stock { get; set; }
            public long StockId {
                get
                {
                    return this.FK_Stock;
                }               
            }
            public string ProName { get; set; }
            public string Unit { get; set; }
            public long UnitID { get; set; }
            public string PpdQuantity { get; set; }
            public string FreeQuantity { get; set; }
            public string PpdActualQuantity { get; set; }
            public string PpdRate { get; set; }
            public string TaxAmount { get; set; }
            public string NetPurchase { get; set; }
            public bool PrdIncludeTaxOnPurRate { get; set; }
            public bool IncludeTax
            {
                get
                {
                    return this.PrdIncludeTaxOnPurRate;
                }
            }
            public long UID { get; set; }            
        }
        public class PurchaseReturnDetailsList
        {
            public long SLNo { get; set; }
            public Int64 FK_PurchaseReturn { get; set; }
            public long ProductID { get; set; }
            public long FK_Stock { get; set; }
            public long StockId { get; set; }
            
            public string ProName { get; set; }
            public string Unit { get; set; }
            public long UnitID { get; set; }
            public string PpdQuantity { get; set; }
            public string FreeQuantity { get; set; }
            public string PpdActualQuantity { get; set; }
            public string PpdRate { get; set; }
            public string TaxAmount { get; set; }
            public string NetPurchase { get; set; }            
            public bool IncludeTax { get; set; }            
            public long UID { get; set; }
        }
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class BindTaxAmountNew
        {

            public long FK_Product { get; set; }
            public decimal Amount { get; set; }
            public decimal Quantity { get; set; }
            public int Sales { get; set; }
            public long Includetax { get; set; }
            
        }
        public class TaxView
        {

            public long SlNo { get; set; }
            public long ProductID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal Amount { get; set; }
            public decimal TaxPercentage { get; set; }
            public int Sales { get; set; }
            public long Includetax { get; set; }

        }
        public class ReplaceFromProductDetails
        {
            public long SLNo { get; set; }
            public Int64 FK_PurchaseReturn { get; set; }
            public long ProductID { get; set; }
            public long StockId { get; set; }

            public string ProName { get; set; }
            public string Unit { get; set; }
            public long UnitID { get; set; }
            public decimal RpdMRP { get; set; }
            public decimal RpdSalesPrice { get; set; }
            public string PpdQuantity { get; set; }
            public string FreeQuantity { get; set; }
            public string PpdActualQuantity { get; set; }
            public string PpdRate { get; set; }
            public string TaxAmount { get; set; }
            public string NetPurchase { get; set; }
            public bool IncludeTax { get; set; }
            public long UID { get; set; }
        }
        public class ReplaceToProductDetails
        {
            public long SLNo { get; set; }
            public Int64 PurProductID { get; set; }
            public string PurProduct { get; set; }
            public long PurUnitID { get; set; }
            public string PpdBarCode { get; set; }
            public string PpdQRCode { get; set; }

            public decimal PurQty { get; set; }
            public decimal Purless { get; set; }
            public decimal PurFreeQuantity { get; set; }
            public decimal PurActQty { get; set; }
            public decimal PurRate { get; set; }
            public decimal PurDisc { get; set; }
            public decimal PurDiscper { get; set; }
            public decimal PurTaxAmount { get; set; }
            public decimal PurTotalAmt { get; set; }
            public decimal PurSalesPrice { get; set; }
            public decimal PurMRP { get; set; }
            public bool PurIncludeTax { get; set; }
            public long UID { get; set; }
        }
        public class UpdateReplace
        {
            public long UserAction { get; set; }
            public string TransMode { get; set; }
            public long ID_Replace { get; set; }
            public DateTime RepDate { get; set; }
            public long FK_Supplier { get; set; }
            public bool RepIsPurchaseReturn { get; set; }
            public long FK_PurchaseReturn { get; set; } = 0;
            public decimal RepFromTotal { get; set; }
            public decimal RepFromRoundOff { get; set; }
            public decimal RepFromNetAmount { get; set; }
            public string ReplaceFromDetails { get; set; }
            public string RepFTaxDetails { get; set; }
            

            public decimal RepToTotal { get; set; }
            public decimal RepToDiscountAmount { get; set; }
            public decimal RepToDiscountPer { get; set; }
            public decimal RepToRoundOff { get; set; }
            public decimal RepToNetAmount { get; set; }
            public string RepTTaxDetails { get; set; }
            public string ReplaceToDetails { get; set; }
            //public long LastID { get; set; }
            public string EntrBy { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_ReplaceMaster { get; set; }

        }
        public class TaxTypeRet
        {
            public long SlNo { get; set; }
            public long FK_Stock { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long ProductID { get; set; }
            public long UID { get; set; }
        }
        public class TaxTypeTo
        {
            public long SlNo { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long FK_Product { get; set; }
            public long UID { get; set; }
        }
        public class TaxListData
        {
            public long SlNo { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long FK_Product { get; set; }
            public long UID { get; set; }
            public long TransType { get; set; }
        }
        public class GetReplace
        {            
            public long FK_ReplaceMaster { get; set; }
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
        public class ReplaceMasterView
        {
            public long SlNo { get; set; }
            public long ID_Replace { get; set; }
            public long FK_Supplier { get; set; }
            public string Supplier { get; set; }
            public DateTime RepDate { get; set; }
            public long FK_PurchaseReturn { get; set; }
            public string PurRetRefereNo { get; set; }
            public bool RepIsPurchaseReturn { get; set; }
            public string RepFromTotal { get; set; }
            public string RepFromRoundOff { get; set; }
            public string RepFromNetAmount { get; set; }

            public string RepToTotal { get; set; }
            public string RepToDiscountPer { get; set; }
            public string RepToDiscountAmount { get; set; }
            public string RepToRoundOff { get; set; }
            public string RepToNetAmount { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class GetReplaceByID
        {
            public long ID_Replace { get; set; }            
            public string TransMode { get; set; }
        }

        public class GetToProductDetails
        {
            public long SLNo { get; set; }
            public Int64 PurProductID { get; set; }
            public string PurProduct { get; set; }
            public long PurUnitID { get; set; }
            public string PpdBarCode { get; set; }
            public string PpdQRCode { get; set; }

            public string PurQty { get; set; }
            public string Purless { get; set; }
            public string PurFreeQuantity { get; set; }
            public string PurActQty { get; set; }
            public string PurRate { get; set; }
            public string PurDisc { get; set; }
            public string PurDiscper { get; set; }
            public string PurTaxAmount { get; set; }
            public string PurTotalAmt { get; set; }
            public string PurSalesPrice { get; set; }
            public string PurMRP { get; set; }
            public bool PurIncludeTax { get; set; }
            public long UID { get; set; }
        }
        public class GetReplaceTax
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class ReplaceDeleteInput
        {
            public long ID_Replace { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
        }
        public class DeleteReplace
        {
            public long FK_Replacemaster { get; set; }
            public string TransMode { get; set; }

            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }
        public APIGetRecordsDynamic<PurchaseReturnDetails> GetPurchaseReturnProductData(GetPurchaseReturn input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseReturnDetails, GetPurchaseReturn>(companyKey: companyKey, procedureName: "ProPurchaseReturnSelect", parameter: input);
        }
        public APIGetRecordsDynamic<TaxView> FillTaxNew(BindTaxAmountNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxView, BindTaxAmountNew>(companyKey: companyKey, procedureName: "proTaxCalculation", parameter: input);

        }
        public Output UpdateReplaceData(UpdateReplace input, string companyKey)
        {
            return Common.UpdateTableData<UpdateReplace>(parameter: input, companyKey: companyKey, procedureName: "ProReplaceMasterUpdate");
        }

        public APIGetRecordsDynamic<ReplaceMasterView> GetReplaceSelect(GetReplace input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReplaceMasterView, GetReplace>(companyKey: companyKey, procedureName: "ProReplaceMasterSelect", parameter: input);
        }

        public APIGetRecordsDynamic<ReplaceFromProductDetails> GetReplaceFromSelect(GetReplace input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReplaceFromProductDetails, GetReplace>(companyKey: companyKey, procedureName: "ProReplaceMasterSelect", parameter: input);
        }

        public APIGetRecordsDynamic<GetToProductDetails> GetReplaceToSelect(GetReplace input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetToProductDetails, GetReplace>(companyKey: companyKey, procedureName: "ProReplaceMasterSelect", parameter: input);
        }

        public APIGetRecordsDynamic<TaxListData> GetTaxDetails(GetReplaceTax input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxListData, GetReplaceTax>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }
        public Output DeleteReplaceData(DeleteReplace input, string companyKey)
        {
            return Common.UpdateTableData<DeleteReplace>(parameter: input, companyKey: companyKey, procedureName: "ProReplacemasterDelete");
        }
    }
}