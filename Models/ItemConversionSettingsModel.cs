using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ItemConversionSettingsModel
    {
        public class ItemConversionSettingsView
        {
            public DateTime EffectDate { get; set; }
            public long FK_ItemConversionSettings { get; set; }
            public long ID_ItemConversionSettings { get; set; }
            public string TransMode { get; set; }
            public List<ItemConversionSettings> ItemDataDetails { get; set; }
        }
        public class ItemConversionSettings
        {
            public long FK_ProductFrom { get; set; }
            public long FK_ProductTo { get; set; }
        }
        public class ItemUpdate
        {
            public Int32 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string ItemConversionSettingsDetails { get; set; }
            public long FK_ItemConversionSettings { get; set; }
            public long ID_ItemConversionSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public string TransMode { get; set; }
        }
        public class ItemCSViewInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_ItemConversionSettings { get; set; }
            public int Detailed { get; set; }
        }
        public class ItemCSOutPut
        {
            public long SlNo { get; set; }
            public string TransMode { get; set; }
            public long TotalCount { get; set; }
            public long ID_ItemConversionSettings { get; set; }
            public long ICSGroupID { get; set; }
            public DateTime EffectDate { get; set; }
            public long FK_ProductTo { get; set; }
            public long FK_ProductFrom { get; set; }
            public string FromProdName { get; set; }
            public string ToProdName { get; set; }
        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ID_ItemConversionSettings { get; set; }
        }
        public class DeleteItemCs
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_ItemConversionSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }


        }
        public Output UpdateConversiondetails(ItemUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ItemUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProItemConversionSettingsUpdate");
        }
        public APIGetRecordsDynamic<ItemCSOutPut> GetItemCnSettingslist(ItemCSViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ItemCSOutPut, ItemCSViewInput>(companyKey: companyKey, procedureName: "ProItemConversionSettingsSelect", parameter: input);
        }
        public Output DeleteItemCsData(DeleteItemCs input, string companyKey)
        {
            return Common.UpdateTableData<DeleteItemCs>(parameter: input, companyKey: companyKey, procedureName: "ProItemConversionSettingsDelete");
        }
    }
}