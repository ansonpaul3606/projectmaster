/*----------------------------------------------------------------------
Created By	: Athul M
Created On	: 03/02/2022
Purpose		: WarrantySetting
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
    public class WarrantySettingModel
    {

        public class WarrantySettingView
        {

            public Int64 WarrantySettingID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Warranty Type")]
            public long FK_WarrantyType { get; set; }
            [Required(ErrorMessage = "Please Enter Effect Date")]
            public DateTime EffectDate { get; set; }
            [Required(ErrorMessage = "Please Enter Effect Date")]
            public byte RepDurType { get; set; }
            //[Required(ErrorMessage = "Please Enter Rep Dur Prd")]
            public Int32 ReplacementDuration { get; set; }
            [Required(ErrorMessage = "Please Enter Ser Durtype")]
            public byte SerDurType{ get; set; }
            //[Required(ErrorMessage = "Please Enter Ser Dur Prd")]
            public decimal WsAmount { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public Int32 ServiceDuration{ get; set; }
            public string WarrantyName { get; set; }
            public string ServiceType { get; set; }
            public string ReplacementType { get; set; }
            public string AccountHeadName { get; set; }
            public string AccountSubHeadName { get; set; }
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
            public bool IncludeTax { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public class UpdateWarrantySetting
        {
            public byte UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_WarrantySetting { get; set; }
            public long FK_WarrantyType { get; set; }
            public DateTime WsEffectDate { get; set; }
            public byte WsRepDurType { get; set; }
            public Int32 WsRepDurPrd { get; set; }
            public byte WsSerDurtype { get; set; }
            public Int32 WsSerDurPrd { get; set; }
            public decimal WsAmount { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public Int64 FK_WarrantySetting { get; set; }
            public long? FK_TaxGroup { get; set; }
            public bool WsAmountIncludeTax { get; set; }

        }
        public static string _deleteProcedureName = "ProWarrantySettingDelete";
        public static string _updateProcedureName = "ProWarrantySettingUpdate";
        public static string _selectProcedureName = "ProWarrantySettingSelect";

        public class WarrantyList
        {
            public int ID_WarrantyTypeSetting { get; set; }
            public string WarrantyTypeSetting { get; set; }
        }

        public class WarrantyType
        {
            public int FK_WarrantyType { get; set; }
            public string Warranty { get; set; }

        }
        public class TaxGroup
        {
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class WarrantySettingList
        {
            public List<WarrantyList> warrlist { get; set; }
            public List<WarrantyType> warrtype { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
        }
        public class DeleteWarrantySetting
        {
            public string TransMode { get; set; }
            public Int64 FK_WarrantySetting { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class DeleteView
        {

            public long WarrantySettingID { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }

        public class WarrantySettingID
        {
            public Int64 FK_WarrantySetting { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }
        public Output UpdateWarrantySettingData(UpdateWarrantySetting input, string companyKey)
        {
            return Common.UpdateTableData<UpdateWarrantySetting>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteWarrantySettingData(DeleteWarrantySetting input, string companyKey)
        {
            return Common.UpdateTableData<DeleteWarrantySetting>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<WarrantySettingView> GetWarrantySettingData(WarrantySettingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantySettingView, WarrantySettingID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}


