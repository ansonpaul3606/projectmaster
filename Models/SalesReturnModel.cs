/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 23/01/2023
Purpose		: Sales Return
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
    public class SalesReturnModel
    {
        public class SalesReturnView
        {
            public long SlNo { get; set; }
            public long ID_SalesReturn { get; set; }
            public string ReturnType { get; set; }
            public string SrBillNo { get; set; }
            public string SrReferenceNo { get; set; }
            public DateTime? SrBillDate { get; set; }
            public string Customer { get; set; }
            public DateTime SrReturnDate { get; set; }
            public decimal SrBillTotal { get; set; }
            public decimal SrDiscount { get; set; }
            public decimal SrOthercharges { get; set; }
            public decimal SrRoundoff { get; set; }
            public decimal TotalTax { get; set; }
            public string SrRemarks { get; set; }
            public long? FK_Customer { get; set; } = 0;

            public long BillType { get; set; } = 0;
            public long FK_Sales { get; set; } = 0;
            public string Department { get; set; }
            public long FK_Department { get; set; }
            public decimal SrNetAmount { get; set; }
            public List<SalesReturnDetails> SalesReturnDetails { get; set; }
            public List<TaxTypeSplit> TaxDetails { get; set; }
            public List<OtherchargeDetails> OtherChgDetails { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public long ReasonID { get; set; }
            public Int64? LastID { get; set; }
            public string Mode { get; set; }
            public string CustomerName { get; set; }
            public int SrReturnType { get; set; }
            public List<Unit> UnitList { get; set; }
        }

        public class SalesReturnDetails
        {
            public long FK_Product { get; set; }
            public string Product { get; set; }
            public string SerialNumber { get; set; }
            public decimal Quantity { get; set; }
            public decimal free { get; set; }
            public decimal ActualQty { get; set; }
            public decimal MRP { get; set; }
            public decimal SalesPrice { get; set; }
            public decimal ActualPrice { get; set; }     
            public decimal Tax { get; set; }
            public decimal Discount { get; set; }
            public decimal TotalAmount { get; set; }
            public long FK_Stock { get; set; }
            public int ProductCriteria { get; set; }
            public int UID { get; set; }
            public long FK_Unit { get; set; }
        }

        public class TaxTypeSplit
        {
            public long SlNo { get; set; }
            public long UID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long ProductID { get; set; }
            public long FK_Transaction { get; set; }
            public long FK_Stock { get; set; }
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

        public class SalesReturnfil
        {
            public int Mode { get; set; }
            public long FK_Master { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
        }

        public class SalesReturnfill
        {
            public int Mode { get; set; }
            public long SalesID { get; set; }
            public long FK_Master { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
        }

        public class SalesTaxGet
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
            public long FK_Transaction { get; set; }
            public long FK_Stock { get; set; }
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

        public class GetSalesData
        {
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Branch { get; set; }
            public string TransMode { get; set; }

        }

        public class SalesView
        {
            public int SlNo { get; set; }
            public long SalesID { get; set; }
            public long ID_SalesReturn { get; set; }
            public DateTime SrReturnDate { get; set; }
            public int SrReturnType { get; set; }
            public string InvoiceNo { get; set; }
            public string ReturnType { get; set; }
            public DateTime SalInvoiceDate { get; set; }
            public string SalBillDate { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Sales { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string MobileNo { get; set; }
            public Int64 TotalCount { get; set; }     
            public DateTime SrBillDate { get; set; }
            public string SrBillNo { get; set; }
            public string SrReferenceNo { get; set; }
            public decimal SrBillTotal { get; set; }
            public string SrRemarks { get; set; }
            public decimal SrOthercharges { get; set; }
            public decimal SrDiscount { get; set; }
            public long LastID { get; set; }
            
        }
        public class SalesItemDetails
        {
            public long ID_Sales { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class SalesProductDetailsView
        {
            public long ID_SalesProductDetails { get; set; }
            public long SlNo { get; set; }
            public long FK_Sales { get; set; }
            public long FK_Product { get; set; }
            public string SpdSalQuantity { get; set; }
            public string SpdMRP { get; set; }
            public string SpdSalPrice { get; set; }
            public string SpdSalDiscount { get; set; }
            public string ProdName { get; set; }
            public string SpdSalTotalAmount { get; set; }
            public string SpdSalTaxAmount { get; set; }
        }
        public class SalesGetInfo
        {
            public string InvoiceNo { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public string TransMode { get; set; }
        }

        public class SalesReturnGetInfo
        {
            public long ID_Sales { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public string TransMode { get; set; }
        }
        public class UpdateSalesReturn
        {
            public Int16 UserAction { get; set; }
            public long ID_SalesReturn { get; set; }
            public string SrBillNo { get; set; }
            public string Mode { get; set; }
            public string SrReferenceNo { get; set; }
            public DateTime SrReturnDate { get; set; }
            public DateTime? SrBillDate { get; set; }
            public decimal SrBillTotal { get; set; }
            public decimal SrDiscount { get; set; }
            public decimal SrOthercharges { get; set; }
            public decimal SrRoundoff { get; set; }
            public decimal SrNetAmount { get; set; }
            public string SrRemarks { get; set; }
            public Int16 TTIntrastate { get; set; }
            public long? FK_Customer { get; set; } = 0;
            public long FK_BillType { get; set; } = 0;
            public long FK_Branch { get; set; }
            public long? FK_Sales { get; set; } = 0;
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string SalesReturnDetails { get; set; }
            public string TransMode { get; set; }
            public string TaxDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public Int32 Debug { get; set; }
            public Int64? LastID { get; set; } = 0;
            public long FK_SalesReturn { get; set; }
            public string CustomerName { get; set; }
            public int SrReturnType { get; set; }
        }

        public class GetSalesReturn
        {
            public Int64 FK_SalesReturn { get; set; }
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

        public class DeleteSalesReturn
        {
            public long FK_SalesReturn { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }

        public class SalesDetailView
        {
            public int SlNo { get; set; }
            public long SalesID { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string MobileNo { get; set; }
            public string InvoiceNo { get; set; }
            public long FK_Customer { get; set; }
            public DateTime SalInvoiceDate { get; set; }
            public string SalBillDate { get; set; }
        }
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class ProductWiseUnit
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public long ProductID { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_Transaction { get; set; }
            public int SalesReturnMode { get; set; }
        }

        //public class TaxTypeDet
        //{
        //    public long SlNo { get; set; }
        //    public long UID { get; set; }
        //    public long FK_TaxType { get; set; }
        //    public string TaxtyName { get; set; }
        //    public decimal TaxPercentage { get; set; }
        //    public decimal TaxAmount { get; set; }
        //    public long ProductID { get; set; }
        //}

        public static string _deleteProcedureName = "ProSalesReturnDelete";
        public static string _updateProcedureName = "ProSalesReturnUpdate";
        public static string _selectProcedureName = "ProSalesReturnSelect";
        public APIGetRecordsDynamicdn<dynamic> FilSales(SalesReturnfil input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<SalesReturnfil>(companyKey: companyKey, procedureName: "ProSalesReturnSalesDataFill", parameter: input);

        }
        public APIGetRecordsDynamic<OtherchargeDetails> GetOthrChargeDetails(GetOtherchargeDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherchargeDetails, GetOtherchargeDetails>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDet> GetTaxDetailsNew(SalesTaxGet input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDet, SalesTaxGet>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<SalesDetailView> GetSalesSelectForSalesReturn(GetSalesData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesDetailView, GetSalesData>(companyKey: companyKey, procedureName: "ProSalesReturnSaleSelect", parameter: input);
        }
     
        public Output UpdateSalesReturnData(UpdateSalesReturn input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSalesReturn>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<SalesView> FillSales(SalesGetInfo input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesView, SalesGetInfo>(companyKey: companyKey, procedureName: "ProSalesReturnSalesSelect", parameter: input);
        }

        public APIGetRecordsDynamic<SalesView> GetSalesReturnDataForList(GetSalesReturn input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesView, GetSalesReturn>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<SalesView> GetSalesReturnData(GetSalesReturn input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesView, GetSalesReturn>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamicdn<dynamic> GetSalesReturnProductData(GetSalesReturn input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetSalesReturn>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public Output DeleteSalesReturnData(DeleteSalesReturn input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSalesReturn>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<TaxTypeDet> FilSalesTax(SalesReturnfil input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDet, SalesReturnfil>(companyKey: companyKey, procedureName: "ProSalesReturnSalesDataFill", parameter: input);
        }
        public APIGetRecordsDynamic<Unit> GetMultiunit(ProductWiseUnit input, string companyKey)
        {
            return Common.GetDataViaProcedure<Unit, ProductWiseUnit>(companyKey: companyKey, procedureName: "ProProductWiseMultiUnitSelect", parameter: input);
        }
    } 
}