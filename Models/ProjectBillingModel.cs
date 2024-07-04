using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class ProjectBillingModel
    {
        public class ProjectBillingView
        {
            public long Project { get; set; }
            public long BillType { get; set; }
            public string TransMode { get; set; }
            public DateTime PrBillDate { get; set; }
            public int PrBillMode { get; set; }
            public decimal PrBillAmount { get; set; }
            public decimal PrTaxAmount { get; set; }
            public decimal PrOtherCharges { get; set; }
            public decimal PrAdvAmount { get; set; }
            public decimal PrNetAmount { get; set; }
            public List<BillModeList> PrBillModeList { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<ProjectBillingModel.SettingNameList> SettingNameList { get; set; }
            //othercharge
            public Int64 SlNo { get; set; }
            public Int64 ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public Int64 OctyTransType { get; set; }
            public decimal OctyAmount { get; set; }
            //tax
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal Amount { get; set; }

            public Int64 TotalCount { get; set; }
            public long PrBalanceAmount { get; set; }
            public long PrDueAmount { get; set; }
            public decimal Roundoff { get; set; }
            public long? LastID { get; set; }
            public bool Performaview { get; set; }

        }
        public class ProjectBillingViewList
        {

        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

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
        public class BillModeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeValue
        {
            public Int32 Mode { get; set; }
        }
        //public class BuyBack
        //{
        //    public bool GsValue { get; set; }
        //    public string GsField { get; set; }

        //}

        public class UpdateProjectBilling
        {
            public int UserAction { get; set; }
            public bool PrPerforma { get; set; }
            public long? LastID { get; set; }
            public long ID_ProjectBilling { get; set; }
            public long FK_Project { get; set; }
            public long FK_BillType { get; set; }
            public string TransMode { get; set; }
            public DateTime PrBillDate { get; set; }
            public int PrBillMode { get; set; }
            public decimal PrBillAmount { get; set; }
            public decimal PrTaxAmount { get; set; }
            public decimal PrOtherCharges { get; set; }
            public decimal PrAdvAmount { get; set; }
            public decimal PrNetAmount { get; set; }
            public string TaxList { get; set; }
            public string OtherChgDetails { get; set; }
            public string OtherChgTaxDetails { get; set; }
            public string PaymentDetail { get; set; }
            //public string buyback { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public decimal PrBillRoundOff { get; set; }
            public decimal PrDownPayment { get; set; }
            public decimal PrAddAmount { get; set; }
        }
        public class InputProjectBilling
        {
            public bool PrPerforma { get; set; }
            public int UserAction { get; set; }

            public long ID_ProjectBilling { get; set; }
            public long FK_Project { get; set; }
            public long FK_BillType { get; set; }
            public string TransMode { get; set; }
            public DateTime PrBillDate { get; set; }
            public int PrBillMode { get; set; }
            public decimal PrBillAmount { get; set; }
            public decimal PrTaxAmount { get; set; }
            public decimal PrOtherCharges { get; set; }
            public decimal PrAdvAmount { get; set; }
            public decimal PrNetAmount { get; set; }
            public List<TaxTypeDet> TaxList { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            //public List<BuyBackDetails> buyback { get; set; }
            public long ReasonID { get; set; }
            public decimal RoundOff { get; set; }
            public decimal PrDownPayment { get; set; }
            public decimal PrAddAmount { get; set; }
            public long? LastID { get; set; }
        }

        public class TaxTypeDet
        {
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
            public decimal Amount { get; set; }
            public decimal TaxPerc { get; set; }
        }
        public class TaxTypeDetails
        {
            public long SlNo { get; set; }
            public long ID_TaxSettings { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public long TaxtyInterstate { get; set; }
            public decimal TaxUpto { get; set; }
            public long TaxUptoPercentage { get; set; }
            public decimal Amount { get; set; }
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
        public class DeleteProjectBilling
        {
            public long FK_ProjectBilling { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class BindTaxAmountNew
        {
            public long FK_TaxGroup { get; set; }
            public bool Includetax { get; set; }
            public decimal Amount { get; set; }
        }

        public class ProjectBillingID
        {
            public Int64 ID_ProjectBilling { get; set; }
        }
        public class GetProjectBillingDetails
        {
            public string TransMode { get; set; }
            public long FK_ProjectBilling { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

        }
        public class ProjectBillingViewData
        {
            public Int64 SlNo { get; set; }
            public long ID_ProjectBilling { get; set; }
            public long FK_Project { get; set; }
            public string ProjectName { get; set; }
            public long FK_TaxGroup { get; set; }
            public bool IncludeTax { get; set; }
            public string ProjNumber { get; set; }
            public string ProjAmount { get; set; }
            public long FK_BillType { get; set; }
            public string TransMode { get; set; }
            public DateTime PrBillDate { get; set; }
            public int PrBillMode { get; set; }
            public decimal PrBillAmount { get; set; }
            public decimal PrTaxAmount { get; set; }
            public decimal PrOtherCharges { get; set; }
            public decimal PrAdvAmount { get; set; }
            public decimal PrNetAmount { get; set; }
            public string CusNumber { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public decimal AdvAmount { get; set; }
            public decimal DueAmount { get; set; }
            public decimal Balance { get; set; }
            public decimal RoundOff { get; set; }
            public decimal TaxableAmount { get; set; }

            public List<BillModeList> PrBillModeList { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }

            public Int64 TotalCount { get; set; }
            public long? LastID { get; set; }
            public long PrPerforma { get; set; }
            public decimal PrDownPayment { get; set; }
            public decimal PrAddnAmount { get; set; }

            public long FK_FinancePlanType { get; set; }

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
        public class TaxCalculation
        {
            public long FK_Product { get; set; }
            public decimal Amount { get; set; }
            public bool IncludeTax { get; set; }
            public long Sales { get; set; }
            public long Quantity { get; set; }
            public long FK_TaxGroup { get; set; }
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
        //public class GetBuyBack
        //{

        //    public string TransMode { get; set; }
        //    public string EntrBy { get; set; }
        //    public Int64 FK_Company { get; set; }
        //    public Int64 FK_Machine { get; set; }

        //    public Int64 FK_BranchCodeUser { get; set; }
        //    public Int64 FK_Master { get; set; }


        //}
        public class ProjectBillingInvoiceIP
        {
            public Int64 FK_Master { get; set; }
            public int TableCount { get; set; }
        }
        public class projectInvoice_table2
        {
            //public string ProductDesc { get; set; }

            public string Description { get; set; }
            //**public string ProductName { get; set; }
            public string Rate { get; set; }
            //**public string MRP { get; set; }
            public string TaxAmount { get; set; }
            public string Total { get; set; }
        }
        public class projectInvoice_table1
        {
            public string InvoiceDate { get; set; }
            public string CusAddress { get; set; }
            public string Customer { get; set; }
            public string Place { get; set; }
            public string GSTINNo { get; set; }
            public string PinCode { get; set; }
            public string CusAddress2 { get; set; }
            public string Address1 { get; set; }
            public string Company { get; set; }
            public string Inwords { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Branch { get; set; }
            public string GST { get; set; }
            public Int64 NetAmount { get; set; }
            public string ProjNumber { get; set; }
            public Int64 ID_Project { get; set; }
            public string RefNo { get; set; }
        }
        public class projectInvoice_table3
        {
            public string Product_HSN  { get; set; }
            public string TaxableAmount { get; set; }
            public string Tax1 { get; set; }
            public string Column1 { get; set; }
            public string Tax2 { get; set; }
            public string Column2 { get; set; }
            public string Tax3 { get; set; }
            public string Column3 { get; set; }
            public string Tax4 { get; set; }
            public string Column4 { get; set; }
            public string Tax5 { get; set; }
            public string Column5 { get; set; }
            public string TotalAmount { get; set; }
            public string TaxType1 { get; set; }
            public string TaxType2 { get; set; }
            public string TaxType3 { get; set; }
            public string TaxType4 { get; set; }
            public string TaxType5 { get; set; }
        }
        public class SettingNameList
        {
            public long ID_CommonPrintingSettings { get; set; }
            public string SettingsName { get; set; }
        }
        public static string _deleteProcedureName = "ProProjectBillingDelete";
        public static string _updateProcedureName = "ProProjectBillingUpdate";
        public static string _selectProcedureName = "ProProjectBillingSelect";

        public Output UpdateProjectBillingData(UpdateProjectBilling input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProjectBilling>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProjectBillingData(DeleteProjectBilling input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProjectBilling>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProjectBillingViewData> GetProjectBillingData(GetProjectBillingDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectBillingViewData, GetProjectBillingDetails>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<BillModeList> GetBillModeList(ModeValue input, string companyKey)
        {
            return Common.GetDataViaProcedure<BillModeList, ModeValue>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<ProjectBillingView> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectBillingView, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ProjectBillingView> FillTaxNew(BindTaxAmountNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectBillingView, BindTaxAmountNew>(companyKey: companyKey, procedureName: "ProProjectTaxCalculation", parameter: input);
        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<TaxTypeDetails> GetTaxDetailsNew(TaxCalculation input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDetails, TaxCalculation>(companyKey: companyKey, procedureName: "ProProjectTaxCalculation", parameter: input);
        }

        //public APIGetRecordsDynamic<BuyBackDetails> GetBuyBackSelect(GetBuyBack input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<BuyBackDetails, GetBuyBack>(companyKey: companyKey, procedureName: "ProBuyBackDetailsSelect", parameter: input);

        //}
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<projectInvoice_table1> Pro_ProjectBillingInvoice_table1(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedure<projectInvoice_table1, ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProProjectInvoice", parameter: input);
        }
        public APIGetRecordsDynamic<projectInvoice_table2> Pro_ProjectBillingInvoice_table2(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedure<projectInvoice_table2, ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProProjectInvoice", parameter: input);
        }
        public APIGetRecordsDynamic<projectInvoice_table3> Pro_ProjectBillingInvoice_table3(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedure<projectInvoice_table3, ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProProjectInvoice", parameter: input);
        }
    }
}