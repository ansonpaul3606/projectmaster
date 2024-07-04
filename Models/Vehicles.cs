using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class Vehicles
    {
        public class VehicleView
        {
            public long SlNo { get; set; }
            public long ID_Vehicle { get; set; }
            public string VehVehicleNo { get; set; }
            public string VehChasisNo { get; set; }
            public DateTime VehRegDate { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public List<ManufacturerList> ManufacturerList { get; set; }
            public List<BrandList> BrandList { get; set; }
            public List<ModelList> ModelList { get; set; }
            public List<MakeList> MakeList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }
            public List<TaxTypeDet> TaxDetails { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public long FK_Category { get; set; }
            public long FK_Maker { get; set; }
            public long FK_Model { get; set; }
            public long FK_Brand { get; set; }
            public long FK_Manufacturer { get; set; }
            public long FK_Vehicle { get; set; }
            public long FK_Fuel { get; set; }
            public string TransMode { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }

            public decimal VehNetAmount { get; set; }
            public decimal VehAmount { get; set; }
            public decimal OtherCharge { get; set; }
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }        
            public decimal TaxAmount { get; set; }

            public List<PaymentDetails> PaymentDetail { get; set; }

            public long FK_BillType { get; set; }
            public bool IncludeTax { get; set; }

            public long FK_Supplier { get; set; }
            public string Supplier { get; set; }

            public DateTime TransDate { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }

        public class MakeList
        {
            public long FK_Maker { get; set; }
            public string MakeName { get; set; }
           

        }
        public class ModelList
        {
            public long FK_Model { get; set; }
            public string ModelName { get; set; }

        }
        public class BrandList
        {
            public long FK_Brand { get; set; }
            public string BrandName { get; set; }

        }
        public class ManufacturerList
        {
            public long FK_Manufacturer { get; set; }
            public string ManufactureName { get; set; }

        }
        public class CategoryList
        {
            public long FK_Category { get; set; }
            public string CategoryName { get; set; }

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


        public class BindTaxAmount   {

            public Int64 FK_Company { get; set; }

        }

        public class UpdateVehicle
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Vehicle { get; set; }
            public string VehVehicleNo { get; set; }
            public string VehChasisNo { get; set; }
            public DateTime VehRegDate { get; set; }
            public long FK_Model { get; set; }
            public long FK_Maker { get; set; }
            public long FK_Brand { get; set; }
            public long FK_Category { get; set; }
            public long FK_Manufacturer { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Vehicle { get; set; }
            public string ImageList { get; set; }
            public long FK_Fuel { get; set; }
            public string OtherChgDetails { get; set; }
            public decimal VehNetAmount { get; set; }
            public decimal VehAmount { get; set; }
            public string TaxDetails { get; set; }
            public bool IncludeTax { get; set; }
            public string PaymentDetail { get; set; }
            public long FK_BillType { get; set; }
            public long FK_Supplier { get; set; }
            public DateTime TransDate { get; set; }
        }
        public class GetVehicleDetails
        {
            public string TransMode { get; set; }
            public long FK_Vehicle { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }            
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

        }
        public class GetVehiclebyIdDetails
        {
            public long FK_Vehicle { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
        }
        public class VehicleSelectDetails
        {
            public long SlNo { get; set; }
            public long FK_Manufacturer { get; set; }
            public long ID_Vehicle { get; set; }
            public string VehVehicleNo { get; set; }
            public DateTime VehRegDate { get; set; }
            
            public string ManufactureName { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class DeleteVehicle
        {
            public string TransMode { get; set; }
            public Int64 FK_Vehicle { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_Vehicle { get; set; }
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }
        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

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
        public class GetSubTableSales { 
        
             
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class PurchaseNewTax
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

            public string TaxTyName { get; set; }

            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long ID_TaxType
            {
                get
                {
                    return this.FK_TaxType;
                }
            }

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
        public class GetPaymentin
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64? FK_Master { get; set; }

        }
        public Output UpdateVehicleData(UpdateVehicle input, string companyKey)
        {
            return Common.UpdateTableData<UpdateVehicle>(parameter: input, companyKey: companyKey, procedureName: "ProVehicleUpdate");
        }
        public APIGetRecordsDynamic<VehicleSelectDetails> GetVehicleSelect(GetVehicleDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<VehicleSelectDetails, GetVehicleDetails>(companyKey: companyKey, procedureName: "ProVehicleSelect", parameter: input);

        }
        public APIGetRecordsDynamic<VehicleView> GetVehicleSelectData(GetVehiclebyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<VehicleView,GetVehiclebyIdDetails>(companyKey: companyKey, procedureName: "ProVehicleSelect", parameter: input);

        }
        public Output DeleteVehicleData(DeleteVehicle input, string companyKey)
        {
            return Common.UpdateTableData<DeleteVehicle>(parameter: input, companyKey: companyKey, procedureName: "ProVehicleDelete");
        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDetget> FillTax(BindTaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDetget, BindTaxAmount>(companyKey: companyKey, procedureName: "ProTaxtypeEquipment", parameter: input);

        }
        public APIGetRecordsDynamic<TaxTypeDet> GetTaxDetailsNew(PurchaseNewTax input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxTypeDet, PurchaseNewTax>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
    }
}
    
