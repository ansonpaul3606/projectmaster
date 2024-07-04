//----------------------------------------------------------------------
//    Created By    : Kavya K
//    Created On    : 30/01/2023
//    Purpose		: AMC Warranty Mapping
//    ------------------------------------------------------------------------- 
//    Modification On
//    By
//    -------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;
using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using PerfectWebERP.Filters;
using System.IO;

namespace PerfectWebERP.Models
{
    public class AMCWarrantyMappingModel
    {
        public class AMCWarrantyView
        {
            public Int16 SlNo { get; set; }
            public string BillNo { get; set; }          
            public DateTime BillDate { get; set; }
            public string Product { get; set; }
            //public List<AMCType> AMCType { get; set; }
            public string CustomerName { get; set; }
            public string Address { get; set; }
            public string MobileNo { get; set; }
            public long ID_Sales { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public decimal Amount { get; set; }

        }
        public class GetAMCWarrantyCustomer
        {
         
            public long FK_Customer { get; set; }
            public long FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int64 Mode { get; set; }

        }
        public class WarrantyDetails
        {
            public long SlNo { get; set; }
            public long FK_Master { get; set; }
            public Int32 prodtid { get; set; }
            public Int32 stkid { get; set; }
            public Int32 subProductID { get; set; }
            public long WarrantyType { get; set; }
            public DateTime Replcwardt { get; set; }
            public DateTime Serwardt { get; set; }
            public string WarrantyTaxAmount { get; set; }
            public string WarrantyAmount { get; set; }
            public string WarrantyNetAmount { get; set; }
            

        }
        public class DropDownListModel
        {
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }
            public List<AMCType> AMCType { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }

        public class AMCType
        {
            public Int32 AMCTypeID { get; set; }
            public string AMCName { get; set; }
           
        }
        public class GetAMCTypeID
        {
            public long FK_Company { get; set; }
        }

        public class AMCDetailsView
        {
            public Int64 Mode { get; set; }
            public long FK_AMCType { get; set; }
            public Int16 NoOfServices { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime AMCDuedate { get; set; }
            public DateTime AMCRenewduedate { get; set; }
            public decimal Quantity { get; set; }
            public decimal AmcAmount { get; set; }
            public decimal AMCTaxAmount { get; set; }
            public decimal AMCNetAmount { get; set; }
            public long ID_Sales { get; set; }
        }
      
        public class GetAMCDetails
        {
            public Int64 Mode { get; set; }
            public long FK_Type { get; set; }
            public DateTime Date { get; set; }
            public decimal ProductAmount { get; set; }
            public decimal Quantity { get; set; }
        }

        public class GetWarDetails
        {
            public Int64 Mode { get; set; }
            public long FK_Type { get; set; }
            public string Date { get; set; }
            public decimal ProductAmount { get; set; }
            public decimal Quantity { get; set; }
        }
        public class AMCDetailsViewData
        {
            public long ID_AMCDetails { get; set; }
            public long FK_AMCDetails { get; set; }
            public string TransMode { get; set; }
            public List<AMCDetails> AMCDetails { get; set; }
            public List<PaymentDetails> PaymentDetails { get; set; }
            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public string AMCAccountNo { get; set; }
            public long FK_Customer { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Master { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 AMCMode { get; set; }
        }
        public class AMCDetails
        {
            public long AMCFK_Master { get; set; }
            public long AMCMType { get; set; }
            public Int64 AMCNoOfServices { get; set; }    
            public long FK_Product { get; set; }
            public DateTime AMCMRenewduedate { get; set; }
            public DateTime AMCMDuedate { get; set; }
            public string AmcTotalAmount { get; set; }
            public string AMCTaxTotalAmt { get; set; }
            public string AMCNetTotalAmt { get; set; }
            public string AMCRemarks { get; set; }
        }
        public class AMCDetailsViewlabel
        {
           
        
            public Int16 NoOfServices { get; set; }
          
            public string DueDate { get; set; }
            public string RenewDuedate { get; set; }
           
            public decimal Amount { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal NetAmount { get; set; }
           
        }

        public class UpdateAMCDetails
        {
            public Int16 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_AMCDetails { get; set; }
            public long FK_Customer { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public string AMCDetails { get; set; }
            public string PaymentDetails { get; set; }
            public string WarrantyDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_AMCDetails { get; set; }
            public Int64 AMCMode { get; set; }
        }
        public class PaymentDetails
        {
            public Int64 SlNo { get; set; }
            public long PaymentMethod { get; set; }
            public decimal PAmount { get; set; }
        }

        public class WarrantyDetailsView
        {
            public Int64 Mode { get; set; }
            public long FK_WarrantyType { get; set; }
            public string WarrantyTypeName { get; set; }
            public string EffectDate { get; set; }            
            public DateTime ReplaceWarrantyDate { get; set; }
            public DateTime ServiceWarrantyDate { get; set; }
            public long FK_Product { get; set; }
            public decimal Amount { get; set; }
            public decimal Quantity { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal NetAmount { get; set; }
        }


        public class GetAMCWarrantyData
        {
            public Int64 FK_AMCDetails { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int Details { get; set; }
        }

        public class GetWarrantyData
        {
            public Int64 FK_AMCDetails { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int Details { get; set; }
        }

        public class AMCViewDetails
        {
            public long SlNo { get; set; }
            public long ID_AMCDetails { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public long AMCType { get; set; }
            public string AMCTypeName { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public DateTime AMCDueDate { get; set; }
            public DateTime AMCRenewDueDate { get; set; }
            public string AMCAvailableServices { get; set; }
            public string AmcAmount { get; set; }
            public string AmcTaxAmount { get; set; }
            public string AmcNetAmount { get; set; }
            public Int64 TotalCount { get; set; }
            public string AMCDate { get; set; }
            public Int64 Mode { get; set; }
            public string DueDate { get; set; }
            public string RenewalDate { get; set; }
            public string TransMode { get; set; }
            public DateTime ServiceWarrantyDate { get; set; }
            public DateTime ReplaceWarrantyDate { get; set; }
        }

        public class DeleteAMCWarranty
        {
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_AMCDetails { get; set; }
            public Int64 Mode { get; set; }
        }

        public class AMCWarrantyViewList
        {
            public long SlNo { get; set; }
            public long ID_AMCDetails { get; set; }
            public long ID_WarrantyDetails { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public Int64 Mode { get; set; }
            public Int64 AMCMode { get; set; }
            public long AMCType { get; set; }
            public long  FK_AMCType { get; set; }
            public string AMCTypeName { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string InvoiceNo { get; set; }
            public DateTime AMCDueDate { get; set; }
            public DateTime AMCRenewDueDate { get; set; }
            public string AMCAvailableServices { get; set; }
            public decimal AmcAmount { get; set; }
            public string AmcTaxAmount { get; set; }
            public string AmcNetAmount { get; set; }         
            public DateTime AMCDate { get; set; }
            public long FK_WarrantyType { get; set; }
            public string AMCEffectDate { get; set; }
            public string AMCTransDate { get; set; }
            public DateTime ReplaceWarrantyDate { get; set; }
            public DateTime ServiceWarrantyDate { get; set; }
            public string TransMode { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string Name { get; set; }
            public int Details { get; set; }
          public DateTime DueDate { get; set; }
          public DateTime RenewalDate { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_Master { get; set; }
            public string AMCAccountNo { get; set; }

            public string AmcTransRemarks { get; set; }
        }

        public class GetSubTable
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

        public class GetRenwalData
        {
            public long FK_AMCDetails { get; set; }
            public long FK_Company { get; set; }
            public DateTime RenewDate { get; set; }
            public string TransMode { get; set; }
            public Int64 Mode { get; set; }

        }


        public class RenewalDetailsViewData
        {
            public long ID_AMCTransaction { get; set; }
            public long FK_AMCTransaction { get; set; }
            public string TransMode { get; set; }
            public long FK_AMCDetails { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime TransDate { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 AMCMode { get; set; }
        }
        public class UpdateRenewalDetails
        {
            public Int16 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_AMCTransaction { get; set; }
            public long FK_AMCDetails { get; set; }
            public DateTime TransDate { get; set; } 
            public DateTime EffectDate { get; set; }
            public Int64   AMCMode {get;set;}
            public DateTime DueDate { get; set; }
            public DateTime RenewDueDate { get; set; }
            public string AmcAmount { get;set;}
            public string AmcTaxAmount { get;set;}
            public string AmcNetAmount { get;set;}
		    public long FK_Company { get;set;}
            public long FK_BranchCodeUser { get;set;}
            public string EntrBy { get; set; }
            public long FK_Machine { get;set;}
		    public long FK_AMCTransaction {get;set;}

        }
        //    public APIGetRecordsDynamic<AMCWarrantyView> GetAMCWarrantyInvoiceCustomer(GetAMCWarrantyCustomer input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<AMCWarrantyView, GetAMCWarrantyCustomer>(companyKey: companyKey, procedureName: "ProAMCWarrantyCustomer", parameter: input);
        //}
        public static string _deleteProcedureName = "ProAMCDetailsDelete";
        public static string _updateProcedureName = "ProAMCDetailsUpdate";
        public static string _selectProcedureName = "ProAMCDetailsSelect";


        public APIGetRecordsDynamicdn<dynamic> GetAMCWarrantyInvoiceCustomer(GetAMCWarrantyCustomer input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetAMCWarrantyCustomer>(companyKey: companyKey, procedureName: "ProAMCWarrantyCustomer", parameter: input);

        }
        public APIGetRecordsDynamic<AMCType> GetAMType(GetAMCTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCType, GetAMCTypeID>(companyKey: companyKey, procedureName: "ProAMCWarrantyAMCType", parameter: input);
        }

        public APIGetRecordsDynamic<AMCDetailsView> GetAMCWarrantyAMCDetails(GetAMCDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCDetailsView, GetAMCDetails>(companyKey: companyKey, procedureName: "ProGetAMCandWarrantyDetails", parameter: input);
        }

        public APIGetRecordsDynamic<WarrantyDetailsView> GetAMCWarrantyWarrantyDetails(GetWarDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetailsView, GetWarDetails>(companyKey: companyKey, procedureName: "ProGetAMCandWarrantyDetails", parameter: input);
        }

        public Output UpdateAMCData(UpdateAMCDetails input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAMCDetails>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<AMCViewDetails> GetAMCWarrantyDetails(GetAMCWarrantyData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCViewDetails, GetAMCWarrantyData>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }               

              public Output DeleteAMCWarrantyData(DeleteAMCWarranty input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAMCWarranty>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<AMCWarrantyViewList> GetAMCDetailedData(GetAMCWarrantyData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCWarrantyViewList, GetAMCWarrantyData>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<AMCWarrantyViewList> GetWarrantyDetailedData(GetWarrantyData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCWarrantyViewList, GetWarrantyData>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTable input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTable>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }

        //public APIGetRecordsDynamicdn<dynamic> GetRenewalDetail(GetRenwalData input, string companyKey)
        //{
        //    return Common.GetDataViaProcedureDynamic<GetRenwalData>(companyKey: companyKey, procedureName: "ProAMCDetailsRenewalData", parameter: input);

        //}
        public APIGetRecordsDynamic<AMCDetailsViewlabel> GetRenewalDetail(GetRenwalData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AMCDetailsViewlabel, GetRenwalData>(companyKey: companyKey, procedureName: "ProAMCDetailsRenewalData", parameter: input);

        }
        public Output UpdateRenewalData(UpdateRenewalDetails input, string companyKey)
        {
            return Common.UpdateTableData<UpdateRenewalDetails>(parameter: input, companyKey: companyKey, procedureName: "ProAMCDetailsRenewalUpdate");
        }
    }
}