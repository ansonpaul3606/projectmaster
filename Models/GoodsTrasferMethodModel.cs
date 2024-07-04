using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;


namespace PerfectWebERP.Models
{
    public class GoodsTrasferMethodModel
    {

        public class GoodsTrasferView
        {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Country Name")]
            public long ID_GoodsTransferMethod { get; set; }
            public string GoodsTransName { get; set; }
            [Required(ErrorMessage = "Please Enter Country Code")]
            public string GoodsTransShortName { get; set; }
            public int Sort { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public DateTime? EntrOn { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
        }
        public class Locationlist
        {
            public int Sort { get; set; }
        }
        public class GoodsTrasferInputView
        {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Country Name")]
            public string GoodsTransName { get; set; }
            [Required(ErrorMessage = "Please Enter Country Code")]
            public string GoodsTransShortName { get; set; }
            public int Sort { get; set; }
            public long ReasonID { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public Int16 ID_GoodsTransferMethod { get; set; }
        }
        public class GoodsTransferUpdate
        {
            public Int16 ID_GoodsTransferMethod { get; set; }
            public string GoodsTransName { get; set; }
            public string GoodsTransShortName { get; set; }
            public int SortOrder { get; set; }
            public Int16 FK_GoodsTransferMethod { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }
        public class SelectGoodstransfer
        {
            public Int64 FK_GoodsTransferMethod { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }
        public class DeleteGoodstransfer
        {
            public Int64 FK_GoodsTransferMethod { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public static string _updateProcedureName = "ProGoodsTransferMethodUpdate";
        public static string _deleteProcedureName = "ProGoodsTransferMethodDelete";
        public Output UpdateGoodsTransferData(GoodsTransferUpdate input, string companyKey)
        { 
            return Common.UpdateTableData<GoodsTransferUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<GoodsTrasferView> GetGoodsTransferData(SelectGoodstransfer input, string companyKey)
        {
            return Common.GetDataViaProcedure<GoodsTrasferView, SelectGoodstransfer>(companyKey: companyKey, procedureName: "ProGoodsTransferMethodSelect", parameter: input);
        }
        public Output DeleteGoodsTransferData(DeleteGoodstransfer input, string companyKey)
        {
            return Common.UpdateTableData<DeleteGoodstransfer>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}