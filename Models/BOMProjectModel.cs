using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class BOMProjectModel
    {
        public class BOMProjectModelview
        {
            public long SlNo { get; set; }
            public DateTime EffectDate { get; set; }
            public DateTime BOMDate { get; set; }
            public long ID_BOMProject { get; set; }
            public string BOMName { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<BOMProjectProductDetails> BOMProjectProductDetails { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public Int64? LastID { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public string SubProdName { get; set; }

            
        }

        public class Category
        {
            public string Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class BOMProjectProductDetails
        {
            public long ID_Product { get; set; }
            public string SubProdName { get; set; }
            public bool CustomerMap { get; set; }
           public decimal Quantity { get; set; }
            public decimal Rate { get; set; }

        }


        public class UpdateBOMProject
        {
            public byte UserAction { get; set; }
            public long ID_BOMProject { get; set; }
            public DateTime BOMDate { get; set; }
          
            public long FK_Category { get; set; }
            public string BOMProjectProductDetails { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_BOMProject { get; set; }

            public string BOMName { get; set; }
        }
        public Output AddBOMProject(UpdateBOMProject input, string companyKey)
        {
            return Common.UpdateTableData<UpdateBOMProject>(companyKey: companyKey, procedureName: "ProBOMProjectUpdate", parameter: input);
        }
        public class BOMProjectViewID
        {
            public Int64 FK_BOMProject { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
            public Byte Detailed { get; set; }

        }
        public APIGetRecordsDynamic<BOMProjectModelview> GetBOMProjectlistviewData(BOMProjectViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<BOMProjectModelview, BOMProjectViewID>(companyKey: companyKey, procedureName: "ProBOMProjectSelect", parameter: input);
        }
        public APIGetRecordsDynamic<BOMProjectProductDetails> GetProductDetailsSubtableData(BOMProjectViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<BOMProjectProductDetails, BOMProjectViewID>(companyKey: companyKey, procedureName: "ProBOMProjectSelect", parameter: input);
        }
        public class DeleteBOMProject
        {
            public long FK_BOMProject { get; set; }
            public string TransMode { get; set; }

            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }
        public Output DeleteBOMProjectData(DeleteBOMProject input, string companyKey)
        {
            return Common.UpdateTableData<DeleteBOMProject>(parameter: input, companyKey: companyKey, procedureName: "ProBOMProjectDelete");
        }

    }
}