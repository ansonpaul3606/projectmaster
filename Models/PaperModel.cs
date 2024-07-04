using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PaperModel
    {
        public static string _deletePaperMasterProcedureName = "ProPaperDelete";
        public static string _updatePaperMasterProcedureName = "ProPaperUpdate";
        public static string _selectPaperMasterProcedureName = "ProPaperSelect";
        public class PaperNew
        {
            public int Sort { get; set; }
            public IEnumerable<ModeType> TypeList { get; set; }
            public List<ModeList> modeList { get; set; }
        }

        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class ModeType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }
        public class PaperMaster
        {
            public long ID_Paper { get; set; }
            public string PaperName { get; set; }
            public string PaperShortName { get; set; }
            public int SortOrder { get; set; }
            public long FK_Type { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }

            public long FK_Paper { get; set; }
            public IEnumerable<ModeType> TypeList { get; set; }
            public string AccountHead { get; set; }
            public string AccountSubHead { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string Mode { get; set; }

        }
        public class Provider
        {
            public long ID_Provider { get; set; }
           
            public string ProvName { get; set; }
        }
        public class AccountHeadView
        {

            public long AccountHeadID { get; set; }

            public string AHeadName { get; set; }

        }

        public class AccountSubHeadView
        {
            public long AccountHeadSubID { get; set; }
            public string ASHeadName { get; set; }

        }

        public class UpdatePaper
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Paper { get; set; }
            public string PaperName { get; set; }
            public string PaperShortName { get; set; }
            public int SortOrder { get; set; }
            public long FK_Type { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Paper { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string Mode { get; set; }




        }
        public class GetPaperDetails
        {
            public Int64 FK_Paper { get; set; }

            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            //public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }

        }
        public class GetPaperbyIdDetails
        {
            public Int64 FK_Paper { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class PaperSelectDetails
        {
            public long SlNo { get; set; }
            public long ID_Paper { get; set; }
            public string PaperName { get; set; }
            public string PaperShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Type { get; set; }
            public Int64 TotalCount { get; set; }
            public string Mode { get; set; }

            public string AccountHead { get; set; }
            public string AccountSubHead { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }

        }

        public class DeletePaper
        {
            public string TransMode { get; set; }
            public Int64 FK_Paper { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_Paper { get; set; }
            public Int64 ReasonID { get; set; }
        }





        public class PaperRenewalView
        {

            public long ID_PaperMaintenance { get; set; }
            public long FK_Master { get; set; }
            public string MasterName { get; set; }
            public long FK_PaperMaintenance { get; set; }
            public decimal PmTotalAmount { get; set; }            
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public List<SubRenewalpaperview> PaperMaintenanceDetails { get; set; }
            public List<VehicleList> VehicleList { get; set; }
            public List<PaperList> PaperList { get; set; }
            public List<ProviderList> ProviderList { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<TaxAmountDetails> TaxAmountDetails { get; set; }
            //public Int64 PmTotalAmount { get; set; }
            public string TransMode { get; set; }
            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }
            public decimal OtherCharge { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public long? LastID { get; set; }


        }
        public class VehicleList
        {
            public long FK_Master { get; set; }
            public string MasterName { get; set; }

        }
        public class Products
        {
            public int PrdID { get; set; }
            public string Product { get; set; }


        }
        public class TaxAmountDetails
        {
            public long FK_TaxType { get; set; }
            public long FK_Paper { get; set; }
            public string TaxTyName { get; set; }
            public decimal? TaxAmount { get; set; }
            public long ID_TaxType
            {
                get
                {
                    return this.FK_TaxType;
                }
            }
        }

        public class TaxAmountDetailsout
        {
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal? TaxAmount { get; set; }
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
        
       

        public class ProviderList
        {
            public long FK_Provider { get; set; }
            public string PaperProviderName { get; set; }

        }
        public class PaperList
        {
            public long FK_Paper { get; set; }
            public string PaperName { get; set; }
        }

        public class SubRenewalpaperview
        {
            private string _pmdPaperAmount;
            private string _pmdPaperTaxAmount;
            private string _pmdPaperNetAmount;
            private string _pmdWarningBefore;
           
            public long ID_PaperMaintenanceDetails { get; set; }
            public long FK_PaperMaintenance { get; set; }
            public long FK_Paper { get; set; }
            public long FK_Provider { get; set; }
            public string PmdPaperNo { get; set; }
            public DateTime PmdIssueDate { get; set; }
            public DateTime PmdExpireDate { get; set; }
            public bool PmIncludeTax { get; set; }
            public string PmdPaperAmount { get { return this._pmdPaperAmount; } set { this._pmdPaperAmount = (value is null)?"0":value; } }           
            public string PmdPaperTaxAmount { get { return this._pmdPaperTaxAmount; } set { this._pmdPaperTaxAmount = (value is null) ? "0" : value; } }
            public string PmdPaperNetAmount { get { return this._pmdPaperNetAmount; } set { this._pmdPaperNetAmount = (value is null) ? "0" : value; } }
            public string PmdWarningBefore { get { return this._pmdWarningBefore; } set { this._pmdWarningBefore = (value is null) ? "0" : value; } }
          


        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class PaperRenewalUpdate
        {


            public long ID_PaperMaintenance { get; set; }
            public long FK_Master { get; set; }

            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public decimal PmTotalAmount { get; set; }
            public long FK_PaperMaintenance { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }

            public string PaperMaintenanceDetails { get; set; }
            public string ImageList { get; set; }
            public string OtherChgDetails { get; set; }
            public string TaxDetails { get; set; }
            public string PaymentDetail { get; set; }
            public string TaxAmountDetails { get; set; }

            public long? LastID { get; set; }


        }
        public class Selectrenewalpaperdata
        {

            public string TransMode { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_PaperMaintenance { get; set; }
            public bool Detailed { get; set; }
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
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }

        public class GetrenewalIdDetails
        {
            public long FK_PaperMaintenance { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public bool Detailed { get; set; }
            public string TransMode { get; set; }




        }
        public class RenewalSelectDetails
        {
            public long SlNo { get; set; }
            public long FK_PaperMaintenance { get; set; }
            public long FK_Master { get; set; }
            public string MasterName { get; set; }           
            public DateTime EffectDate { get; set; }       

            public Int64 TotalCount { get; set; }
            public DateTime TransDate { get; set; }

            public long ID_PaperMaintenance { get; set; }
            public long? LastID { get; set; }
        }

        public class DeletePaperRenewal
        {
            public string TransMode { get; set; }
            public long FK_PaperMaintenance { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }          
	   
           public class DeleteRenewalView
        {
            public long ID_PaperMaintenance { get; set; }
            public string TransMode { get; set; }

            public Int64 ReasonID { get; set; }
        }
        public class BindTaxAmount
        {
            public Int64 FK_Company { get; set; }

        }

        public class Datapaperfill
        {
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class TaxAmount
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_Transaction { get; set; }          
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
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
        public class TaxTypeDetget
        {
            public long SlNo { get; set; }
            public long UID { get; set; }
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
        }

        public static string _deleteProcedureName = "ProPaperMaintenanceDelete";
        public static string _updateProcedureName = "ProPaperMaintenanceUpdate";
        public static string _selectProcedureName = "ProPaperMaintenanceSelect";


        public class DropDownListModel
        {
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }

        }
        public APIGetRecordsDynamic<ModeType> GetModeList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeType, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }

        public Output UpdatePaperMaster(UpdatePaper input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePaper>(parameter: input, companyKey: companyKey, procedureName: "ProPaperUpdate");
        }
        public APIGetRecordsDynamic<PaperSelectDetails> GetPaperSelect(GetPaperDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaperSelectDetails, GetPaperDetails>(companyKey: companyKey, procedureName: "ProPaperSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PaperSelectDetails> GetPaperSelectData(GetPaperbyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaperSelectDetails, GetPaperbyIdDetails>(companyKey: companyKey, procedureName: "ProPaperSelect", parameter: input);

        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output DeletePaperData(DeletePaper input, string companyKey)
        {
            return Common.UpdateTableData<DeletePaper>(parameter: input, companyKey: companyKey, procedureName: "ProPaperDelete");
        }

        public Output UpdatePaperrenewal(PaperRenewalUpdate input, string companyKey)
        {
            return Common.UpdateTableData<PaperRenewalUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProPaperMaintenanceUpdate");
        }
        public APIGetRecordsDynamic<RenewalSelectDetails> GetRenewalPaperSelect(Selectrenewalpaperdata input, string companyKey)
        {
            return Common.GetDataViaProcedure<RenewalSelectDetails, Selectrenewalpaperdata>(companyKey: companyKey, procedureName: "ProPaperMaintenanceSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PaperRenewalView> GetRenewalSelectData(GetrenewalIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaperRenewalView, GetrenewalIdDetails>(companyKey: companyKey, procedureName: "ProPaperMaintenanceSelect", parameter: input);

            }

        public APIGetRecordsDynamic<SubRenewalpaperview> GetRenewalSelectDatanew(GetrenewalIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubRenewalpaperview, GetrenewalIdDetails>(companyKey: companyKey, procedureName: "ProPaperMaintenanceSelect", parameter: input);

        }
        public Output DeletePaperRenewalData(DeletePaperRenewal input, string companyKey)
            {
                return Common.UpdateTableData<DeletePaperRenewal>(parameter: input, companyKey: companyKey, procedureName: "ProPaperMaintenanceDelete");
            }

        public APIGetRecordsDynamic<SubRenewalpaperview> GetRenewalfill(Datapaperfill input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubRenewalpaperview, Datapaperfill>(companyKey: companyKey, procedureName: "ProPaperMaintenanceDataFill", parameter: input);

        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDetget> FillTax(BindTaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDetget, BindTaxAmount>(companyKey: companyKey, procedureName: "ProTaxtypeEquipment", parameter: input);

        }
        public APIGetRecordsDynamic<TaxAmountDetails> GetTaxDetails(TaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxAmountDetails, TaxAmount>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }



    }

        
    }
