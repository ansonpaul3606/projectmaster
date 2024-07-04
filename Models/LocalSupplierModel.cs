using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class LocalSupplierModel
    {

        public class SupplierView
        {
            public long SlNo { get; set; }
            public long SupplierID { get; set; }
            public string GSTINNo { get; set; }
            public string Name { get; set; }
            public string ContactPerson { get; set; }
            public string Mobile { get; set; }           
            public Int64 TotalCount { get; set; }
            public List<ProductDetailsView> ProductDetails { get; set; }
            public string TransMode { get; set; }
        }
        public class ProductDetailsView
        {
            public long ID_SupplierProductDetails { get; set; }
            public long SupplierID { get; set; }
            public long FK_Product { get; set; }
            public string SPDetRemarks { get; set; }
            public bool Cancelled { get; set; }
            public string CancelledBy { get; set; }
            public DateTime CancelledOn { get; set; }
            public string ProductName { get; set; } 
        }

            public class SupplierGSTINView
        {
            public string GSTINNo { get; set; }
            public long FK_Company { get; set; }
        }
        public class UpdateSupplier
        {
            public long ID_Supplier { get; set; }
            public long FK_Supplier { get; set; }
            public string SuppName { get; set; }
            public string SuppContactPerson { get; set; }
            public string SuppMobile { get; set; }
            public string SuppGSTINNo { get; set; }
            public Int64 IsSupplier { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public bool Debug { get; set; }
            public string ProductDetails { get; set; }
            public string TransMode { get; set; }
        }

        public class UpdateLocalSupplier
        {
            public long FK_Supplier { get; set; }
            public Int64 IsSupplier { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }
        public class ProductDetailsViewList
        {
            public long FK_Supplier { get; set; }
            public long FK_Product { get; set; }
            public string SPDetRemarks { get; set; }
            public string ProductName { get; set; }
            public Int64 IsSupplier { get; set; }
        }

        public class LocalSupplierView
        {
            public long FK_Supplier { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public List<ProductDetailsView> ProductDetails { get; set; }
        }

        public class SupplierID
        {
            public string TransMode { get; set; }
            public long FK_Supplier { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }

        }

        public class LocalSupplierSelectDetails
        {
            public Int64 FK_Supplier { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class LocalSupplierItemDetails
        {
            public Int64 FK_Supplier { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }

        public class DeleteLocalSupplier
        {
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Supplier { get; set; }
        }


        public APIGetRecordsDynamic<SupplierView> GetSupplierByGST(SupplierGSTINView input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierView, SupplierGSTINView>(companyKey: companyKey, procedureName: "proSupplierGSTSelect", parameter: input);
        }

        public Output UpdateLocalSupplierData(UpdateSupplier input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSupplier>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public static string _updateProcedureName = "ProLocalSupplierUpdate";

        public static string _selectProcedureName = "ProLocalSupplierSelect";

        public APIGetRecordsDynamic<SupplierView> GetSupplierData(SupplierID input, string companyKey)
        {
            return Common.GetDataViaProcedure<SupplierView, SupplierID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ProductDetailsViewList> GetLocalSupplierProductDetailsSelect(LocalSupplierItemDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductDetailsViewList, LocalSupplierItemDetails>(companyKey: companyKey, procedureName: "ProLocalSupplierProductDetailsSelect", parameter: input);
        }
        public Output UpdateLocalSupplierToSupplier(UpdateLocalSupplier input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLocalSupplier>(companyKey: companyKey, procedureName: "ProLocalSupplierToSupplierUpdate", parameter: input);
        }
        
    }
}