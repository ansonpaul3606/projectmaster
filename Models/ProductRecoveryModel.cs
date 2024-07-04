using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductRecoveryModel
    {
        public class ProductRecoveryView
        {
            public long FK_Customer { get; set; }
            public DateTime CollectionDate { get; set; }
            
        }
        public class InsertInputView
        {
            public long FK_Customer { get; set; }
            public DateTime ProdRecoveryDate { get; set; }
            public string TransMode { get; set; }
            public DateTime? PickUpDate { get; set; }
            public string PickUpTime { get; set; }
            public long FK_Employee { get; set; }
            public string VehicleDet { get; set; }
            public string ProdRecNarration { get; set; }
            public List<CustomerProductDueView> ProductRecoveryDetails { get; set; }
            public long? LastID { get; set; }
        }
        public class CustomerProductDueView
        {
            public int FK_CustomerWiseEMI { get; set; }
            public string BillDate { get; set; }
            public string EMINo { get; set; }
            public string Product { get; set; }
            public decimal Amount { get; set; }
            public decimal Fine { get; set; }
            public decimal Balance { get; set; }
            public long FK_Sales { get; set; }
            public long FK_Product { get; set; }
            public string Remark { get; set; }
              
                          
        }
        public class UpdateProductRecovery
        {
            public long ID_ProductRecovery { get; set; }
            public int UserAction { get; set; }
            public string TransMode { get; set; }
            public int Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public DateTime ProdRecoveryDate { get; set; }
            //public long FK_ProductRecovery { get; set; }
            //public long FK_Customer { get; set; }
            public string ProductRecoveryDetails { get; set; }
            public DateTime? PickUpDate { get; set; }
            public string PickUpTime { get; set; }
            public long FK_Employee { get; set; }
            public string VehicleDet { get; set; }
            public string ProdRecNarration { get; set; }
            public long? LastID { get; set; }
        }
        public class ProductRecoveryInput
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
            //public long FK_ProductRecovery { get; set; }
            public long ProdGroupID { get; set; }
        }
        public class ProductRecoveryOutPut
        {
            public long TotalCount { get; set; }
            public long SlNo { get; set; }
            public long ProdGroupID { get; set; }
            public string TransMode { get; set; }
            public long FK_Customer { get; set; }
            public string PickUpTime { get; set; }
            public long FK_Employee { get; set; }
            public string VehicleDet { get; set; }
            public DateTime ProdRecoveryDate { get; set; }
            public string CusName { get; set; }
            public DateTime? PickUpDate { get; set; }
            public string Employee { get; set; }
            public long FK_CustomerWiseEMI { get; set; }
            public string BillDate { get; set; }
            public string EMINo { get; set; }
            public string Product { get; set; }
            public decimal Amount { get; set; }
            public decimal Fine { get; set; }
            public decimal Balance { get; set; }
            public long FK_Sales { get; set; }
            public long FK_Product { get; set; }
            public string ProdRecNarration { get; set; }
            public string ProdRecProductRemarks { get; set; }
            public long? LastID { get; set; }

        }

        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long ProdGroupID { get; set; }
        }
        public class DeleteProRecovery
        {
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long ProdGroupID { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
        }
        public APIGetRecordsDynamic<CustomerProductDueView> GetCustomerProductDetails(ProductRecoveryView input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerProductDueView, ProductRecoveryView>(companyKey: companyKey, procedureName: "ProGetCustomerTransactionDue", parameter: input);
        }
        public Output UpdateProductRecoverydetails(UpdateProductRecovery input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductRecovery>(parameter: input, companyKey: companyKey, procedureName: "ProProductRecoveryUpdate");
        }
        public APIGetRecordsDynamic<ProductRecoveryOutPut> GetProRecoList(ProductRecoveryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductRecoveryOutPut, ProductRecoveryInput>(companyKey: companyKey, procedureName: "ProProductRecoverySelect", parameter: input);
        }
        public APIGetRecordsDynamic<ProductRecoveryOutPut> GetProRecoveryGrid(ProductRecoveryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductRecoveryOutPut, ProductRecoveryInput>(companyKey: companyKey, procedureName: "ProProductRecoverySelect", parameter: input);
        }
        public Output DeleteProRecoveryData(DeleteProRecovery input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProRecovery>(parameter: input, companyKey: companyKey, procedureName: "ProProductRecoveryDelete");
        }
    }
}