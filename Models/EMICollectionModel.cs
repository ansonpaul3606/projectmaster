/*----------------------------------------------------------------------
    Created By	: Kavya 
    Created On	: 31/10/2022
    Purpose		: EMI Collection
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
    public class EMICollectionModel
    {
        public class EMICollectionViewList
        {
            public string InvoiceNumber { get; set; }
        }
        public class DropDownListModel
        {        
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<OtherChargeTypeModel.TransTypes> Transtypelist { get; set; }
        }
        public class EMICollectionview
        {
            public long FK_Master { get; set; }
            public Int64 AccountMode { get; set; }
            public DateTime CollectionDate { get; set; }
            public Int64 Advance { get; set; }
            
        }
        public class EMICollectionviewDetails
        {
            public long ID_EMICollection { get; set; }
            public long CusTrCusType { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime CollectDate { get; set; }
            public long TransType { get; set; }
            public decimal CusTrAmount { get; set; }
            public decimal CusTrFineAmount { get; set; }
            public decimal CusTrFineWaiveAmount { get; set; }
            public long CusTrCollectedBy { get; set; }
            public long FK_PaymentMethod { get; set; }
            public List<EMIDetails> EMIDetail { get; set; }
            public decimal NetAmount { get; set; }
            public Int64? LastID { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class EMIDetails
        {
            public long FK_CustomerWiseEMI { get; set; }
            public string BillNo { get; set; }
            public string CusTrDetDemandDate { get; set; }
            public decimal CusTrDetPayAmount { get; set; }
            public decimal CusTrDetFineAmount { get; set; }
            public decimal Total { get; set; }
            public decimal Balance { get; set; }
            public long FK_Closed { get; set; }
        }
        public class EMICollectionDetails
        {
            public long SlNo { get; set; }
            public long ID_Sales { get; set; }
            public long FK_Customer { get; set; }
            public long FK_SalesOrder { get; set; }
            public string SalBillNo { get; set; }
            public string EMINo { get; set; }
            public decimal Amount { get; set; }
            public DateTime BillDate { get; set; }
            public decimal Fine { get; set; }
            public decimal Balance { get; set; }
            public decimal SalBillTotal { get; set; }
            public long FK_FinancePlanType { get; set; }
            public long FK_CustomerWiseEMI { get; set; }
            public string Product { get; set; }
        }
        public class GetEMICollectionview
        {
            public long FK_Master { get; set; }
            public Int64 AccountMode { get; set; }
            public DateTime CollectionDate { get; set; }
            public Int64 Advance { get; set; }

        }
        public class UpdateEMICollection
        {
            public long UserAction { get; set; }
            public long ID_EMICollection { get; set; }
            public long AccountMode { get; set; }
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime CollectDate { get; set; }
            public long TransType { get; set; }
            public decimal CusTrAmount { get; set; }
            public decimal CusTrFineAmount { get; set; }
            public decimal CusTrFineWaiveAmount { get; set; }
            public decimal NetAmount { get; set; }
            public long CusTrCollectedBy { get; set; }
            public long FK_PaymentMethod { get; set; }
            public string EMIDetails { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy{ get; set; }         
            public long FK_Machine{ get; set; }
            public Int64? LastID { get; set; } = 0;
            public string PaymentDetail { get; set; }
        }
        public class GetEMICollection
        {
            public Int64 FK_CustomerTransaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 Detailed { get; set; }            
        }
        public class SalesOrderShortView
        {
            public Int64 ID_CustomerTransaction { get; set; }
            public Int64 SlNo { get; set; }
            public long FK_Customer { get; set; }
            public string Customer { get; set; }
            public DateTime CollectDate { get; set; }
            public string CusTrAmount { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64? LastID { get; set; }
        }
        public class EMiCollectionInfoView
        {
            public Int64 CustomerTransactionID { get; set; }
            public int ReasonID { get; set; }
        }
        public class DeleteEMICollection
        {
            public long FK_CustomerTransaction { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_Company { get; set; }
            public int FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class EmiDataFetch
        {
            public Int64 FK_CustomerTransaction { get; set; }
            public Int64 Detailed { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class EMIDetailsFill
        {
            public long SLNo { get; set; }
            public long ID_CustomerTransactionDetails { get; set; }
            public long FK_SalesOrder { get; set; }
            public string EMINo { get; set; }
            public long FK_CustomerWiseEMI { get; set; }
            public string Product { get; set; }
            public DateTime SalBillDate { get; set; }
            public decimal Amount { get; set; }
            public decimal Fine { get; set; }
            public decimal Total { get; set; }
            public decimal Balance { get; set; }
            //public long ID_Sales { get; set; } = 0;
            public long FK_Closed { get; set; }
        }
        public class EMIDetailsFillFields
        {
            public long ID_CustomerTransaction { get; set; }
            public long CusTrCusType { get; set; }
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime CollectDate { get; set; }
            public long TransType { get; set; }
            public string CusTrAmount { get; set; }
            public string CusTrFineAmount { get; set; }
            public string CusTrFineWaiveAmount { get; set; }
            public long CusTrCollectedBy { get; set; }
            public long FK_PaymentMethod { get; set; }
            public decimal NetAmount { get; set; }

            public string CollectedBy { get; set; }
            public string CusAddress { get; set; }
            public string CusMobile { get; set; }
            public string Customer { get; set; }
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
        public APIGetRecordsDynamic<EMICollectionDetails> GetEMIDetails(GetEMICollectionview input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMICollectionDetails, GetEMICollectionview>(companyKey: companyKey, procedureName: "ProCustomerTransactionDataFill", parameter: input);
        }
        public Output UpdateEMICollectionData(UpdateEMICollection input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEMICollection>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSalesOrderData(DeleteEMICollection input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEMICollection>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<SalesOrderShortView> GetEMICollectionData(GetEMICollection input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderShortView, GetEMICollection>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EMIDetailsFill> GetEMICollectionDataFill(GetEMICollection input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIDetailsFill, GetEMICollection>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EMIDetailsFillFields> GetEMICollectionDataFillFields(GetEMICollection input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIDetailsFillFields, GetEMICollection>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        public static string _deleteProcedureName = "ProCustomerTransactionDelete";
        public static string _updateProcedureName = "ProCustomerTransactionUpdate";
        public static string _selectProcedureName = "ProCustomerTransactionSelect";
    }
}