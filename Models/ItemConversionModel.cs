using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ItemConversionModel
    {
        public class ItemConversionView
        {
            public List<ActionStatus> ConversionMode { get; set; }
            public List<DepartmentList> DepartmentList { get; set; }
            public long FK_Product { get; set; }
            public DateTime ICDate { get; set; }
            public long FK_Department { get; set; }
            public string TransMode { get; set; }
            public int ICMode { get; set; }
            public long FK_ItemConversion { get; set; }
            public List<ItemSubTable> ItemConversionSubDetails { get; set; }
            public long? LastID { get; set; }

        }
        public class ItemSubTable
        {
            //public int CheckProduct { get; set; }
            public long ICProduct { get; set; }
            public decimal ICActualStock { get; set; }
            public decimal ICConversionStock { get; set; }
            public long FK_StockFrom { get; set; }
            public string ICRemark { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class GetItemInput
        {
            public long FK_Product { get; set; }
            public DateTime ICDate { get; set; }
            public long FK_Department { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class GetConvertedItemView
        {
            public List<ItemTableData> itemDatas { get;set; }
        }
        public class ItemTableData
        {
            public long FK_Product { get; set; }   
            public string Product { get; set; }
            public decimal Stock { get; set; }
            public long FK_Stock { get; set; }
            public string Department { get; set; }
        }
        public class UpdateItemConversion
        {
            public long ID_ItemConversion { get; set; }
            public int UserAction { get; set; }
            public string TransMode { get; set; }
            public int Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public DateTime ICDate { get; set; }
            public long FK_ItemConversion { get; set; }
            public int ICMode { get; set; }
            public string ItemConversionSubDetails { get; set; }
            public long FK_Department { get; set; }
            public long FK_Product { get; set; }
            public long? LastID { get; set; }
        }
        public class ICViewInput
        {

            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            public byte Detailed { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_ItemConversion { get; set; }
        }

        public class ICViewOutPut
        {
            public long TotalCount { get; set; }
            public long SlNo { get; set; }
            public long ID_ItemConversion{ get; set; }
            public string TransMode { get; set; }
            public string ICDate { get; set; }
            public int ICMode { get; set; }
            public long FK_Product { get; set; }
            public long DepartmentID { get; set; }
            public string ProductName { get; set; }
            public long? LastID { get; set; }
            public string ICModeName { get; set; }
        }

        public class GetItemdataGrid
        {
            public long ICProduct { get; set; }
            public decimal ICActualStock { get; set; }
            public decimal ICConversionStock { get; set; }
            public long FK_StockFrom { get; set; }
            public long FK_StockTo { get; set; }
            public string ICRemark { get; set; }
            public long FK_ItemConversion { get; set; }
            public string ProductName { get; set; }
            public long FK_Department { get; set; }
            public string Department { get; set; }

        }

        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ID_ItemConversion { get; set; }
        }
        public class DeleteItemdata
        {
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_ItemConversion { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<ItemTableData> GetItemlist(GetItemInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ItemTableData, GetItemInput>(companyKey: companyKey, procedureName: "ProGetAllConvertedItemSelect", parameter: input);
        }
        public Output UpdateItemConversiondetails(UpdateItemConversion input, string companyKey)
        {
            return Common.UpdateTableData<UpdateItemConversion>(parameter: input, companyKey: companyKey, procedureName: "ProItemConversionUpdate");
        }
        public APIGetRecordsDynamic<ICViewOutPut> GetICList(ICViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ICViewOutPut, ICViewInput>(companyKey: companyKey, procedureName: "ProItemConversionSelect", parameter: input);
        }
        public APIGetRecordsDynamic<GetItemdataGrid> GetItemGrid(ICViewInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetItemdataGrid, ICViewInput>(companyKey: companyKey, procedureName: "ProItemConversionSelect", parameter: input);
        }
        public Output DeleteItemData(DeleteItemdata input, string companyKey)
        {
            return Common.UpdateTableData<DeleteItemdata>(parameter: input, companyKey: companyKey, procedureName: "ProItemConversionDelete");
        }
    }
}