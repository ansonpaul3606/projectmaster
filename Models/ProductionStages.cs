using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductionStages
    {
        public class ProductionStagesView
        {
            public long SlNo { get; set; }
            public long ProjectStagesID { get; set; }

            public string Name { get; set; }

            public string ShortName { get; set; }

            public int Category { get; set; }

            public int SubCategory { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }

        }

        public class ProductionStagesInputFromViewModel
        {

            [Required(ErrorMessage = "No ProjectStages Selected")]
            public long ProjectStagesID { get; set; }

            [Required(ErrorMessage = "Please Enter ProjectStages Name")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string ShortName { get; set; }
            //public int Category { get; set; }
            //public int SubCategory { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

        }

        public class ProductionStagesInfoView
        {
            [Required(ErrorMessage = "No ProjectStages Selected")]
            public Int64 ProjectStagesID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }
        }


        public static string _updateProcedureName = "proProjectStagesUpdate";
        public class UpdateProductionStages
        {
            public long FK_ProjectStages { get; set; }
            public long ID_ProjectStages { get; set; }
            public string PrStName { get; set; }
            public string PrStShortName { get; set; }

            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }



        }

        public class SelectProductionStages
        {

            public long FK_ProjectStages { get; set; }
            public string PrStName { get; set; }
            public string PrStShortName { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }

            public Int64 FK_Machine { get; set; }
            public string UserCode { get; set; }
            public Int32 BranchCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 BackupId { get; set; }
            public bool Cancelled { get; set; }

        }

        public static string _deleteProcedureName = "proProjectStagesDelete";
        public class DeleteProductionStages
        {
            public Int64 FK_ProjectStages { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }

        public class Category
        {
            public string Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class Subcategory
        {
            public long CategoryID { get; set; }
            public long ID_SubCategory { get; set; }
            public string SubCatName { get; set; }
        }
        public class ProductionStagesListModel
        {
            public int SortOrder { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<Subcategory> SubCategoryList { get; set; }
        }
        public class InputProductionStagesID
        {
            public Int64 FK_ProjectStages { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }




        public Output UpdateProductionStagesData(UpdateProductionStages input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductionStages>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProductionStagesData(DeleteProductionStages input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductionStages>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectStagesSelect";
        public APIGetRecordsDynamic<ProductionStagesView> GetProductionStagesData(InputProductionStagesID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStagesView, InputProductionStagesID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}
