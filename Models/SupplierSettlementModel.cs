using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PerfectWebERP.Models
{
    public class SupplierSettlementModel
    {
        public class DropDownListModelView
        {
            public long SlNo { get; set; }
            public long SupplierSettlementID { get; set; }

            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentViewpayment { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public List<SupplierPayment> SupplierPaymentDetails { get; set; }

            public List<PaymentDetails> PaymentDetail { get; set; }
          
            public List<BillTypes> BillTypeListView { get; set; }
            public List<LeadFrom> BranchNameList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
            public List<SupplierMode> listMode { get; set; }

            public int ddlimportfrom { get; set; }


            public int ddltransType { get; set; }
            public Int32 SortOrder { get; set; }
            public string transType { get; set; }
            public string TransDate { get; set; }
            public string SuppName { get; set; }
            public int SuppID { get; set; }
            public int ImportID { get; set; }
            public Int32 DepartmentID { get; set; }
            public int SupplierMode { get; set; }
            public int BranchID { get; set; }
        
            public string SSTransMode { get; set; }
            public decimal Discount { get; set; }
            public decimal Supproundoff { get; set; }
            public decimal SuppNetAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal NetAmounts { get; set; }
            public long BillTypeID { get; set; }
            public decimal AdvanceAmount { get; set; }
            public decimal StOtherCharge { get; set; }
            public string TransMode { get; set; }
            public Int64? LastID { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64? GroupID { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TotalDiscount { get; set; }
            public long FK_PaymentMethod { get; set; }
            public long ID_TransactionDetails { get; set; }
            public int  ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Otherchargeview
        {

        }

        public class ViewInfo
        {
            public long SlNo { get; set; }
            public long FK_Master { get; set; }
            public long SupplierSettlementID { get; set; }

            public int ImportID { get; set; }

            public int ddlimportfrom { get; set; }

            public string ddlimportfroms { get; set; }
            public int ddltransType { get; set; }

            public string transType { get; set; }
            public string TransDate { get; set; }
            public string InvoiceDate { get; set; }
            public string EnteredOn { get; set; }
            public string InvoiceNo { get; set; }

             public decimal InvoiceItemCount { get; set; }

            public string SuppName { get; set; }
            public int SuppID { get; set; }
            public int ID_Mode { get; set; }
           
            public Int32 DepartmentID { get; set; }

            public int BranchID { get; set; }
            public string EnteredBy { get; set; }
            
            public decimal? Discount { get; set; }
            public decimal Supproundoff { get; set; }
            public decimal SuppNetAmount { get; set; }
            public string NetAmount { get; set; }
            public decimal NetAmounts { get; set; }
            public long BillTypeID { get; set; }
            public decimal AdvanceAmount { get; set; }
            public decimal OtherCharge { get; set; }
            public string TransMode { get; set; }
            public Int64? LastID { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64? GroupID { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TotalDiscount { get; set; }
            public int Paymentmethod { get; set; }
            public decimal Balance { get; set; }
            public decimal TrsType { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentViewpayment { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public List<SupplierPayment> SupplierPaymentDetails { get; set; }

            public List<PaymentDetails> PaymentDetail { get; set; }

            public List<BillTypes> BillTypeListView { get; set; }
            public List<LeadFrom> BranchNameList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }

        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string PaymentMethodS { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

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
        public class BillTypes
        {
            public long BillTypeID { get; set; }
            public string BillType { get; set; }
           
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
            
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class BranchTypes
        {
            public int BranchTypeID { get; set; }
            public string BranchType { get; set; }
            public long BranchModeID { get; set; }
            public long FK_BranchMode { get; set; }
            public long FK_BranchType { get; set; }
        }
        public class Modetype
        {
            public int Mode { get; set; }
        }
        public class SupplierMode
        {
            public int ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class LeadFrom
        {
            public Int32 ID_LeadFrom { get; set; }
            public string LeadFromName { get; set; }
        }
        public class SupplierPayment
        {
            public string Mode { get; set; }
            public string RefNo { get; set; }
            public string InvoiceDate { get; set; }
            public long ID_Purchase { get; set; }
            public long ID_SupplierTransaction { get; set; }
            public long FK_BillType { get; set; }
            public long InvoiceType { get; set; }
          //  public decimal Balance { get; set; }
            public decimal Amount { get; set; }
           // public decimal Discount { get; set; }
            public decimal BalanceToPay { get; set; }

            public string TransType { get; set; }
        }
        public class UpdateSupplierSettlement
        {
            public byte UserAction { get; set; }
            public long ID_SuppSettlement { get; set; }
            public long StPaymentMode { get; set; }
            public long StImportfrom { get; set; }
            public string TransDate { get; set; }
            public long FK_Supplier { get; set; }
            public long StInvoiceType { get; set; }
            public decimal StBillTotal { get; set; }
            public decimal StDiscount { get; set; }
            public decimal StOtherCharge { get; set; }
            public decimal StAdvanceamount { get; set; }
            public decimal StRoundOff { get; set; }
            public decimal StNetAmount { get; set; }
            public Int64? LastID { get; set; }
            public string TransMode { get; set; }
            public int FK_Master { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal TotalDiscount { get; set; }
            public long FK_PaymentMethod { get; set; }
            public string PaymentDetail { get; set; }
            public string OtherChgDetails { get; set; }
            public string SupplierPaymentDetails { get; set; }
            public long FK_Suppliertransaction { get; set; }
            

        }

        public class SupplierSettlementViewID
        {
           // public Int64 FK_SupplierTransaction { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public String TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
            public Int64? GroupID { get; set; }
            public Byte Detailed { get; set; }
        }
        public class PurchaseorderSubSelect
        {
            public Int64 FK_PurchaseOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public String TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
            public Byte Detailed { get; set; }


        }
        public class PaymentdatafillbySuppID
        {

            public Int64 FK_Supplier { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public String EntrBy { get; set; }
            public decimal PayAmount { get; set; }



        }
        public class SupplierSettlementInfoView
        {
            public long GroupID { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
        }

        public class DeleteSupplierSettlement
        {
            public long GroupID { get; set; }
            public string TransMode { get; set; }

            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }

        public class Paymentdatabind
        {
            public string Mode { get; set; }
            public string RefNo { get; set; }
            public DateTime InvoiceDate { get; set; }
            public long ID_Purchase { get; set; }
            public long ID_SupplierTransaction { get; set; }
            public long FK_BillType { get; set; }
            public long InvoiceType { get; set; }
            public decimal Amount { get; set; }
            public decimal Discount { get; set; }
            public decimal BalanceToPay { get; set; }
            public decimal TransType { get; set; }
            public decimal PayableAmount { get; set; }



        }

        public class Getotherchargelist
        {
          
            public Int64 InvoiceType { get; set; }
            public Int64? FK_Master { get; set; }
            public Int64 FK_Company { get; set; }
           // public string TransMode { get; set; }

        }

        public class GetPaymentin
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 ?FK_Master { get; set; }


        }

        public static string _updateProcedureName = "ProSupplierTransactionUpdate";
        public static string _selectProcedureName = "ProSupplierTransactionSelect";
        public static string _deleteProcedureName = "ProSupplierTransactionDelete";
        public Output UpdateSuppStlmentData(UpdateSupplierSettlement input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSupplierSettlement>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
           public APIGetRecordsDynamic<ViewInfo> GetSupplierSettlementData(SupplierSettlementViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ViewInfo, SupplierSettlementViewID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<PaymentDetails> GetSubTablePaymentDetailsData(SupplierSettlementViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, SupplierSettlementViewID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetSubTableOtherChargesDetailsData(SupplierSettlementViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, SupplierSettlementViewID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public Output DeleteSupplierSettlementData(DeleteSupplierSettlement input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSupplierSettlement>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<Paymentdatabind> FillSupplierpaymentdata(PaymentdatafillbySuppID input, string companyKey)
        {
            return Common.GetDataViaProcedure<Paymentdatabind, PaymentdatafillbySuppID>(companyKey: companyKey, procedureName: "ProSupplierTransactionDataFill", parameter: input);
        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(Getotherchargelist input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, Getotherchargelist>(companyKey: companyKey, procedureName: "ProOtherChargeForSuppSettlement", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<SupplierMode> GetSupplierModeData(Modetype input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierMode, Modetype>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

    }
}
