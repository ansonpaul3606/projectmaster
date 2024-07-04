using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class OtherChargesModel
    {
        public class GetOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Transaction { get; set; }
        }
        public class OtherChargeList
        {
            public string SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public long OctyTransType { get; set; }
            public long OctyTransTypeCus { get; set; }
            public long FK_TaxGroup { get; set; }
            public decimal OctyAmount { get; set; }
            public decimal OctyTaxAmount { get; set; }
            public bool OctyIncludeTaxAmount { get; set; }
            public long OctyTransTypeActive { get; set; }
            public string OctranRemarks { get; set; }
            
        }
       
        public class GetOtherChargeTax
        {          
            public decimal Amount { get; set; }          
            public long IncludeTax { get; set; }
            public long FK_TaxGroup { get; set; }
            public long FK_Transaction { get; set; }
            public string TransMode { get; set; }
            public long FK_OtherChargeType { get; set; } = 0;          
        }
        
        public class OtherChargeTaxList
        {
            public string SlNo { get; set; }
            public long ID_TaxSettings { get; set; }
            public string FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
            public decimal TaxPercentage { get; set; }
            public string TaxtyInterstate { get; set; }
            public string TaxUpto { get; set; }
            public string TaxUptoPercentage { get; set; }
            public decimal Amount { get; set; }            
        }
        public class GetOtherChargeTaxData
        {
            public long FK_Transaction { get; set; }
            public string TransMode { get; set; }
        }
        public class GetOtherChargeTaxSavedList
        {
           
            public long ID_OtherChargeType { get; set; }
            public long ID_TaxSettings { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal TaxPercentage { get; set; }
            public long TaxGrpID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxtyName { get; set; }
           
        }
        public class UpdateOtherChargeList
        {
            public string TransMode { get; set; }
            public List<UpdateOtherCharge> OtherChargeList { get; set; }
            public List<UpdateOtherChargeTax> OtherChargeTaxList { get; set; }
        }
        public class UpdateOtherCharge
        {
          
            public long ID_OtherChargeType { get; set; }           
            public long OctyTransType { get; set; }
            public long FK_TaxGroup { get; set; }
            public decimal OctyAmount { get; set; }
            public decimal OctyTaxAmount { get; set; }
            public bool OctyIncludeTaxAmount { get; set; }
            public string OctranRemarks { get; set; }
        }
        public class UpdateOtherChargeTax
        {
            public decimal Amount { get; set; }
            public long ID_OtherChargeType { get; set; }
            public long ID_TaxSettings { get; set; }
            public decimal TaxPercentage { get; set; }
            public long TaxGrpID { get; set; }
            public long FK_TaxType { get; set; }
            public string TaxTyName { get; set; }
        }
        public class UpdateOtherChargeTaxList
        {
            public string TransMode { get; set; }
            public List<UpdateOtherChargeTax> OtherChargeTaxList { get; set; }
        }
        
        public APIGetRecordsDynamic<OtherChargeList> GetOtherChargeList(GetOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherChargeList, GetOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelectData", parameter: input);
        }
        public APIGetRecordsDynamic<OtherChargeTaxList> GetOtherChargeTaxList(GetOtherChargeTax input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherChargeTaxList, GetOtherChargeTax>(companyKey: companyKey, procedureName: "ProOtherChargeTaxCalculation", parameter: input);
        }
        public APIGetRecordsDynamic<GetOtherChargeTaxSavedList> GetOtherChargeTaxDataList(GetOtherChargeTaxData input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetOtherChargeTaxSavedList, GetOtherChargeTaxData>(companyKey: companyKey, procedureName: "ProOtherChargeTaxData", parameter: input);
        }
    }
    public class pssOtherCharge
    {
        public long ID_OtherChargeType { get; set; }
        public long OctyTransType { get; set; }
        public long FK_TaxGroup { get; set; }
        public decimal OctyAmount { get; set; }
        public decimal OctyTaxAmount { get; set; }
        public bool OctyIncludeTaxAmount { get; set; }
        public string OctranRemarks { get; set; }
    }
    public class pssOtherChargeTax
    {
        public long ID_OtherChargeType { get; set; }
        public long ID_TaxSettings { get; set; }
        public decimal Amount { get; set; }     
        public decimal TaxPercentage { get; set; }
        public long TaxGrpID { get; set; }
        public long FK_TaxType { get; set; }
        public string TaxTyName { get; set; }
    }
}
