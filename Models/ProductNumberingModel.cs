using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductNumberingModel
    {
        public static string _deleteProcedureName = "ProProductNumberingDelete";
        public static string _updateProcedureName = "ProProductNumberingUpdate";
        public static string _selectProcedureName = "ProProductNumberingSelect";
        public class ProductNumbering
        {
            public IEnumerable<ModeData> ImportFromList { get; set; }           
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class ModeData
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ProductNumberingNew
        {
            public long ID_ProductNumbering { get; set; }
            public string TransMode { get; set; }
            public int ProdNumModule { get; set; }
            public long FK_Master { get; set; }            
            public List<ProductNumberingDetails> ProductNumberingDetails { get; set; }
        }
        public class ProductNumberingDetails
        {
            public long FK_Product { get; set; }
            public long FK_Stock { get; set; }
            public string BatchNo { get; set; }
            //public decimal ProdNumNoOfItems { get; set; }
            public decimal ProdNumItems { get; set; }
            public string ProdNumPrefix { get; set; }
            public int ProdNumFromNo { get; set; }
            public int ProdNumToNo { get; set; }
            public string ProdNumSuffix { get; set; }
        }
        public class ProductNumberingUpdate
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }                  
            public string TransMode { get; set; }
            public int ProdNumModule { get; set; }
            public long FK_Master { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string ProductNumberingDetails { get; set; }
            public long FK_ProductNumbering { get; set; }
        }
        public class DeleteProductNumbering
        {
            public string TransMode { get; set; }
            public long ProdNumGroupID { get; set; }           
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class GetProductNumbering
        {
            public string TransMode { get; set; }
            public long ProdNumGroupID { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public int Detailed { get; set; }

        }
        public class ProductNumberingView
        {
            public int SlNo { get; set; }
            public long ProdNumGroupID { get; set; }
            public string TransMode { get; set; }
            public long ProdNumModule { get; set; }
            public string Module { get; set; }
            public long FK_Master { get; set; }
            public string ReferenceNo { get; set; }
            public DateTime Date { get; set; }
            public string RefDate { get; set; }
            public int TotalCount { get; set; }
        }
        public class ProductNumberingDtlView
        {           
            public long FK_Product { get; set; }
            public string Product { get; set; }
            public long FK_Stock { get; set; }
            public string BatchNo { get; set; }
            public decimal ProdNumNoOfItems { get; set; }
            public decimal ProdNumItems { get; set; }

            public string ProdNumPrefix { get; set; }
            public int ProdNumFromNo { get; set; }
            public int ProdNumToNo { get; set; }
            public string ProdNumSuffix { get; set; }
        }
        public class GetProductSerialNumbers
        {
            public long FK_Company { get; set; }
            public long FK_Product { get; set; }
            public long FK_Stock { get; set; }
        }
        public class ProductSerialNumbers
        {
            public long ID_ProductNumberingDetails { get; set; }
            public string ProdSerielNo { get; set; }           
        }
        public APIGetRecordsDynamic<ModeData> GetModeList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeData, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateProductNumberingData(ProductNumberingUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ProductNumberingUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<ProductNumberingView> GetProductNumberingData(GetProductNumbering input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductNumberingView, GetProductNumbering>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ProductNumberingDtlView> GetProductNumberingDtlData(GetProductNumbering input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductNumberingDtlView, GetProductNumbering>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public Output DeleteProductNumberingData(DeleteProductNumbering input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductNumbering>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProductSerialNumbers> GetProductSerialNumber(GetProductSerialNumbers input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductSerialNumbers, GetProductSerialNumbers>(companyKey: companyKey, procedureName: "ProGetProductSerialNumbers", parameter: input);
        }
    }
}