using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ServiceBillDetailsModel
    {
        public class Servicebillview
        {
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<BankList> BankList { get; set; }
        }
        public class BankList
        {
            public long BankID { get; set; }
            public string BankName { get; set; }

        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class ServiceProductViewInput
        {
            public long Mode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_TransactionID { get; set; }
        }
        public class GetServiceProductView
        {
           public string Product { get; set; }
            public decimal MRP { get; set; }
            public string SalePrice { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal BuyBackAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal Quantity { get; set; }

            public long ServiceId { get; set; }
            public string Service { get; set; }
            public string ServiceCost { get; set; }
           

        }

        public class ServiceBill
        {
            public long ServiceId { get; set; }
            public string Service { get; set; }
            public string ServiceCost { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal NetAmount { get; set; }

        }
        public class Productdetail
        {
            public string Product { get; set; }
            public decimal MRP { get; set; }
            public string SalePrice { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal BuyBackAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal Quantity { get; set; }

        }
        public class ServicebillProductview 
        {
            public string TransMode { get; set; }
            public long BillType { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public long TicketNo { get; set; }
            public string CustomerName { get; set; }
            public List<ServiceBill> Servicedetails { get; set; }
            public List<Productdetail> Productdetails { get; set; }
            public decimal SalBillTotal { get; set; }
            public decimal SalProductBillTotal { get; set; }
            public decimal SalDiscount { get; set; }
            public decimal OtherCharge { get; set; }
            public decimal SalRoundoff { get; set; }
            public decimal SalNetAmount { get; set; }
            public decimal AdvancedAmount { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }

            public decimal SecurityAmount { get; set; }
            public decimal BalCusAmount { get; set; }
            public long ?BankID { get; set; }
        }
        public  class UpdateServicebill
        {
            public long UserAction { get; set; }
            public string TransMode { get; set; }
            public long FK_BillType { get; set; }
            public DateTime SerBillDate { get; set; }
            public DateTime SerEffectDate { get; set; }
            public long FK_CustomerServiceRegister { get; set; }
            public string  SerCustomerName { get; set; }
            public decimal SerBillTotal { get; set; }
            public decimal SerProductBillTotal { get; set; }
            public decimal SerDiscount { get; set; }
            public decimal SerOtherCharge { get; set; }
            public decimal SerRoundoff { get; set; }
            public decimal SerNetAmount { get; set; }
            public string Servicelist { get; set; }
            public string Productlist { get; set; }
            public string OtherChDetails { get; set; }
            public string PaymentDetail { get; set; }
            public long FK_Company { get; set; }
            public long BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_ServiceBill { get; set; }

            public decimal SecurityAmount { get; set; }
            public decimal BalCusAmount { get; set; }
            public long FK_Bank { get; set; }
            public decimal AdvancedAmount { get; set; }

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
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }

        public class GetServicebilldetails
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 BranchCodeUser { get; set; }
            public long FK_ServiceBill { get; set; }
        }



        public class ServicebillViewOut
        {
            public long ID_ServiceBill { get; set; }
            public string TransMode { get; set; }
            public long SlNo { get; set; }
            public long FK_ServiceBillType { get; set; }
            public string FK_CustomerServiceRegister { get; set; }
            public string SerCustomerName { get; set; }

            public DateTime SerBillDate { get; set; }
            public DateTime SerEffectDate { get; set; }
            public decimal SerBillTotal { get; set; }
            public decimal SerProductBillTotal { get; set; }

            public string SerDiscount { get; set; }
             public string TicketNumber { get; set; }

           // public List<ServiceBill> Servicedetails { get; set; }
           // public List<Productdetail> Productdetails { get; set; }
          
            public string SerOtherCharge { get; set; }
            public string SerRoundoff { get; set; }

            public string SerNetAmount { get; set; }

            public Int64 TotalCount { get; set; }

            public decimal BalCusAmount { get; set; }
            public decimal SecurityAmount { get; set; }
            public long BankID { get; set; }
            // public List<OtherCharges> OtherChgDetails { get; set; }

            // public List<PaymentDetails> PaymentDetail { get; set; }

        }

        public class DeleteService
        {
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_ServiceBill { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 BranchCodeUser { get; set; }
        }

        public class ServiceInputDel
        {
            public Int64 ID_ServiceBill { get; set; }
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }
        }

        public class Getbillinput
        {
            public Int64 ID_ServiceBill { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_CustomerServiceRegister { get; set; }
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
        public class GetPaymentin
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }


        }
        public class GetServicedetailsView
        {
            public decimal ProductsCharge { get; set; }
            public decimal ServiceCharge { get; set; }

            public decimal AdvanceAmount { get; set; }
            public decimal SecurityAmount { get; set; }
            public decimal DiscountAmount { get; set; }
           
        }

        public static string _selectProcedureName = "ProServiceBillSelect";
        public static string _deleteProcedureName = "ProServiceBillDelete";

        public APIGetRecordsDynamic<GetServiceProductView> GetDetailsServiceProduct(ServiceProductViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetServiceProductView, ServiceProductViewInput>(companyKey: companyKey, procedureName: "ProGetServiceBillDetails", parameter: input);

        }
        public APIGetRecordsDynamic<GetServicedetailsView> GetServiceDetails(ServiceProductViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetServicedetailsView, ServiceProductViewInput>(companyKey: companyKey, procedureName: "ProGetServiceBillDetails", parameter: input);

        }

        public Output UpdateService(UpdateServicebill input, string companyKey)
        {
            return Common.UpdateTableData<UpdateServicebill>(parameter: input, companyKey: companyKey, procedureName: "ProServiceBillUpdate");
        }

        public APIGetRecordsDynamic<ServicebillViewOut> GetbillData(GetServicebilldetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServicebillViewOut, GetServicebilldetails>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public Output DeleteServiceData(DeleteService input, string companyKey)
        {
            return Common.UpdateTableData<DeleteService>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }

    }
}