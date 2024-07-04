using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EquipmentServiceModel
    {
        public class EquipmentServiceView
        {
            public long SlNo { get; set; }

            public long ID_EquipmentService { get; set; }
            public long FK_Mode { get; set; }
            public long ?FK_Master { get; set; }
            public string Name { get; set; }
            public int ?AMCService { get; set; }
            // public Int32? WarrentyService { get; set; }
            public long ID_Names { get; set; }

            public int ?WarrentyService { get; set; }

            public string BookingNo { get; set; }
            public string VehicleNo { get; set; }
            public long SerPickDel { get; set; }

            public string TransactionName { get; set; }
            public string Mode { get; set; }

            public long FK_Transaction { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long ?FK_ServiceBooking { get; set; }
            public long? FK_EquipmentServiceType { get; set; }

            //public string ServiceName { get; set; }
            public string ServiceCentre { get; set; }
            public long ReasonID { get; set; }
            public long FK_Company { get; set; }
            public long FK_EquipmentService { get; set; }
            public List<EquipmentServiceDetails> EquipmentServiceDetails { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }

            public List<PaymentDetails> PaymentDetail { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }

            public string FromTime { get; set; }
            public string ToTime { get; set; }

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public List<MaintenancetypeList> MaintenancetypeList { get; set; }
            public List<IncidencetypeList> IncidencetypeList { get; set; }
            public List<TaxAmountDetails> TaxAmountDetails { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public decimal SubTotalAmount { get; set; }
            public decimal ?DiscountAmount { get; set; }
            public decimal NetAmount { get; set; }
            public List<ModeList> ModeList { get; set; }
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal ?OtherCharge { get; set; }

            public bool IncludeTax { get; set; }

            public decimal ?TaxAmount { get; set; }
            public DateTime TransDate { get; set; }
            public long? LastID { get; set; }


        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public long ID_TransactionDetails { get; set; }
            public string TransType { get; set; }
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
        public class TaxAmountDetails
        {
            public long FK_TaxType { get; set; }
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
        public class ModeSelect
        {
            public int Mode { get; set; }
        }
        public class ModeList
        {
            public long FK_Transaction { get; set; }
            public string ModeName { get; set; }


        }
        public class EquipmentServiceDetails
        {
            public Int64 ID_EquipmentServiceDetails { get; set; }
            public Int64 FK_EquipmentService { get; set; }


            public string Description { get; set; }
            public decimal Amount { get; set; }

        }
        public class BindTaxAmount
        {

           
           
            
            public Int64 FK_Company { get; set; }
           
        }

        public class EquipmentServiceDelete
        {
            public long FK_EquipmentService { get; set; }
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
        public class EquipmentServiceUpdate
        {
                public byte UserAction { get; internal set; }

            public long ID_EquipmentService { get; set; }
            //public long ID_Names { get; set; }
            public long SerPickDel { get; set; }

            public long FK_Mode { get; set; }
            public long ?FK_Master { get; set; }
            public long FK_Transaction { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            //public Int32? WarrentyService { get; set; }
            //public Int32? AMCService { get; set; }
            //public Int32? WarrentyService { get; set; }
            public int? AMCService { get; set; }

            public int ?WarrentyService { get; set; }
            public decimal OtherCharge { get; set; }

            public long FK_ServiceBooking { get; set; }
            public long FK_EquipmentServiceType { get; set; }
            public string ServiceCentre { get; set; }

            //public string BookingNo { get; set; }
            //public string VehicleNo { get; set; }
            //public string TransactionName { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long FK_Machine { get; set; }
            public decimal SubTotalAmount { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal NetAmount { get; set; }
            public decimal TaxAmount { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
            public string EquipmentServiceDetails { get; set; }                                                                    
            public string TaxAmountDetails { get; set; }
            public string OtherChgDetails { get; set; }

            public bool IncludeTax { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
           
            public string PaymentDetail { get; set; }
            public string ImageList { get; set; }

            public long FK_EquipmentService { get; set; }
            public DateTime TransDate { get; set; }
            public long? LastID { get; set; }

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
        public class MaintenancetypeList
        {
            public Int32 FK_Master { get; set; }
            public string Maintenancetype { get; set; }

        }
        public class IncidencetypeList
        {
            public Int32 FK_Master { get; set; }
            public string Incidencetype { get; set; }

        }
        public class EquipmentServiceRsnView
        {
            public long ID_EquipmentService { get; set; }

            public long ReasonID { get; set; }
       
        }
        public class Equipmentby
        {
            public long SlNo { get; set; }

            public long ID_EquipmentService { get; set; }
            public long FK_Mode { get; set; }
            public long FK_Master { get; set; }
            public string Name { get; set; }
            public Int32? AMCService { get; set; }
            //  public Int32? WarrentyService { get; set; }
            public Int32? WarrentyService { get; set; }
            public long ID_Names { get; set; }
            public decimal OtherCharge { get; set; }

            public string BookingNo { get; set; }
            public string VehicleNo { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime TransDate { get; set; }
            public string TransactionName { get; set; }
            public string Mode { get; set; }

            public long FK_Transaction { get; set; }
            public long ReasonID { get; set; }
            public long FK_Company { get; set; }

            public long FK_ServiceBooking { get; set; }
            public Int64 TotalCount { get; set; }
            public long? LastID { get; set; }
        }

        public class EquipmentServiceViewout
        {
            public long SlNo { get; set; }

            public long ID_EquipmentService { get; set; }
            public long FK_Mode { get; set; }
            public long FK_Master { get; set; }
            public string Name { get; set; }
            public Int32? AMCService { get; set; }
            public Int32? WarrentyService { get; set; }

            //public Int32? WarrentyService { get; set; }
            public long ID_Names { get; set; }

            public string BookingNo { get; set; }
           
            public string TransactionName { get; set; }
            public string Mode { get; set; }

            public long FK_Transaction { get; set; }
            public long FK_ServiceBooking { get; set; }


            public decimal OtherCharge { get; set; }


            public string FromTime { get; set; }
            public string ToTime { get; set; }

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime TransDate { get; set; }
            public string TransMode { get; set; }
         
            public decimal SubTotalAmount { get; set; }
            public decimal? DiscountAmount { get; set; }
            public decimal NetAmount { get; set; }
         
           
            public bool IncludeTax { get; set; }

            public decimal? TaxAmount { get; set; }

            public List<EquipmentServiceDetails> EquipmentServiceDetails { get; set; }
            public long? LastID { get; set; }
        }
        public class EquipmentServiceID
        {
            public long FK_EquipmentService { get; set; }


         

            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public byte Detailed { get; set; }
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

        public class EquipmentTax
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_EquipmentService { get; set; }
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
        public class GetSubTableEquipment
        {

        }
        public Output UpdateEquipmentServiceData(EquipmentServiceUpdate input, string companyKey)
        {
            return Common.UpdateTableData<EquipmentServiceUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<Equipmentby> GetEquipmentServiceData(EquipmentServiceID input, string companyKey)
        {
            return Common.GetDataViaProcedure<Equipmentby, EquipmentServiceID>(companyKey: companyKey, procedureName: "ProEquipmentServiceSelect", parameter: input);
        }
        public Output DeleteEquipmentService(EquipmentServiceDelete input, string companyKey)
        {
            return Common.UpdateTableData<EquipmentServiceDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<EquipmentServiceViewout> GetEquipmentServiceDatainfoid(EquipmentServiceID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EquipmentServiceViewout, EquipmentServiceID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        //public APIGetRecordsDynamic<TaxTypeDet> GetTaxDetails(GetSubTableEquipment input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<TaxTypeDet, GetSubTableEquipment>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        //}

        public APIGetRecordsDynamic<TaxAmountDetails> GetTaxDetails(GetSubTableEquipment input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxAmountDetails, GetSubTableEquipment>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        public static string _updateProcedureName = "ProEquipmentServiceUpdate";
        public static string _deleteProcedureName = "ProEquipmentServiceDelete";
        public static string _selectProcedureName = "ProEquipmentServiceSelect";


        public class EquipmentServiceDetailsSubSelect
        {
            public Int64 FK_EquipmentService { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public byte Detailed { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        
        public APIGetRecordsDynamic<EquipmentServiceDetails> GetEquipmentServiceSubtabledata(EquipmentServiceDetailsSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<EquipmentServiceDetails, EquipmentServiceDetailsSubSelect>(companyKey: companyKey, procedureName: "ProEquipmentServiceSelect", parameter: input);

        }
        
        public APIGetRecordsDynamic<EquipmentServiceView> FillTax(BindTaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<EquipmentServiceView, BindTaxAmount>(companyKey: companyKey, procedureName: "ProTaxtypeEquipment", parameter: input);

        }
        public APIGetRecordsDynamic<TaxAmountDetails> GetTaxDetails(TaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxAmountDetails, TaxAmount>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }        
    }
}
