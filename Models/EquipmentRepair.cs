using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class EquipmentRepair
    {
        public class RepairListModel
        {
            public long ID_EquipmentRentalDetails { get; set; }


            public Int32 EquRentType { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }

            public  DateTime EquRentalDate { get; set; }
            public string TransMode { get; set; }
            public string EquEquipmentNo { get; set; }            
            public decimal EquEquipmentDistance { get; set; }
            public decimal EquEquipmentStrtDistance { get; set; }
            public decimal PaymentAmount { get; set; }
            public string EquDescription { get; set; }
            public decimal EquSecurityAmount { get; set; }
            public decimal EquRentAmount { get; set; }
            public decimal EquTaxAmount { get; set; }
            public decimal EquNetAmount { get; set; }
            public string EquOperator { get; set; }
            public string EquOperatorMobile { get; set;            }
            public  string EquOperatorNo { get; set; }
            public Int16 EquReturndays { get; set; }
            public DateTime? EquOperatorExpDate { get; set; }

            public List<PaymentDetails> PaymentDetail { get; set; }

            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }

            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public long FK_EquipmentRentalDetails { get; set; }

            public long FK_MasterID { get; set; }
            public long ID_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxAmount { get; set; }
            public List<TaxAmountDetails> TaxAmountDetails { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public decimal EquPayAmount { get; set; }
            public long FK_BillType { get; set; }

            public bool IncludeTax { get; set; }
            public decimal OtherCharges { get; set; }
            public long? LastID { get; set; }

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
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class GetRentbyIdDetails
        {
            public long FK_EquipmentRentalDetails { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
        }
       
       
        public class InputProductStageSettingsID
        {
            public Int64 FK_ProductStageSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int CheckList { get; set; }
            public string Name { get; set; }

        }
     
             
        
		
        public class UpdateVehicleAndToolRepair
        {
    
            public long ID_EquipmentRentalDetails { get; set; }

            public Int32 EquRentType { get; set; }

            public DateTime EquRentalDate { get; set; }
            public string TransMode { get; set; }

            public string EquDescription { get; set; }
            public decimal EquSecurityAmount { get; set; }
            public string EquEquipmentNo { get; set; }

            public decimal EquEquipmentDistance { get; set; }

            public bool IncludeTax { get; set; }

            public string PaymentDetail { get; set; }

            public string ImageList { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_BillType { get; set; }

            public long FK_Machine { get; set; }
            public Int64 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public long FK_MasterID { get; set; }
            public long FK_EquipmentRentalDetails { get; set; }
            public decimal EquTaxAmount { get; set; }
            public decimal EquNetAmount { get; set; }
            public decimal EquRentAmount { get; set; }
            public string TaxAmountDetails { get; set; }
            public decimal EquPayAmount { get; set; }

            public string EquOperator { get; set; }
            public string EquOperatorMobile { get; set; }
            public string EquOperatorNo { get; set; }
            public Int16 EquReturndays { get; set; }
            public DateTime? EquOperatorExpDate { get; set; }           
            public string OtherChDetails { get; set; }
            public long? LastID { get; set; }
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
       
        public class GetRentDetails
        {  		   
            public string TransMode { get; set; }
            public long FK_EquipmentRentalDetails { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
       

        }
        public class RentSelectDetails
        {
            public long SlNo { get; set; }
            public long FK_EquipmentRentalDetails { get; set; }
            public string EquEquipmentNo { get; set; }
            public DateTime EquRentalDate { get; set; }
            public long EquRentType { get; set; }

            public string RentType{ get; set; }
            public Int64 TotalCount { get; set; }         
            public long ID_EquipmentRentalDetails { get; set; }
            public long? LastID { get; set; }
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
        public class GetSubTableSales
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }

        public class DeleteRent
        {
            public string TransMode { get; set; }
            public Int64 FK_EquipmentRentalDetails { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {
            public long ID_EquipmentRentalDetails { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public class BindTaxAmount
        {
            public Int64 FK_Company { get; set; }

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
        public class TaxAmount
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 ID_Transaction { get; set; }
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


        public class DropDownListModel
        {       
        public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }          
              
        }
         public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
        
       
        public Output UpdateVehicleAndToolrepairData(UpdateVehicleAndToolRepair input, string companyKey)
        {
            return Common.UpdateTableData<UpdateVehicleAndToolRepair>(parameter: input, companyKey: companyKey, procedureName: "ProEquipmentRentalDetailsUpdate");
        }
        public APIGetRecordsDynamic<RepairListModel> FillTax(BindTaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<RepairListModel, BindTaxAmount>(companyKey: companyKey, procedureName: "ProTaxtypeEquipment", parameter: input);

        }
        public APIGetRecordsDynamic<TaxAmountDetails> GetTaxDetails(TaxAmount input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxAmountDetails, TaxAmount>(companyKey: companyKey, procedureName: "ProTaxDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<RepairListModel>GetRentSelectData(GetRentbyIdDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<RepairListModel, GetRentbyIdDetails>(companyKey: companyKey, procedureName: "ProEquipmentRentalDetailsSelect", parameter: input);

        }
        public APIGetRecordsDynamic<RentSelectDetails> GetRentSelect(GetRentDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<RentSelectDetails, GetRentDetails>(companyKey: companyKey, procedureName: "ProEquipmentRentalDetailsSelect", parameter: input);

        }
        public Output DeleteRentData(DeleteRent input, string companyKey)
        {
            return Common.UpdateTableData<DeleteRent>(parameter: input, companyKey: companyKey, procedureName: "ProEquipmentRentalDetailsDelete");
        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetSubTableSales input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, GetSubTableSales>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);

        }

    }
}