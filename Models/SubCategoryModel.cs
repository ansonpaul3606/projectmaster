
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SubCategoryModel
    {
        public class SubCategoryView
        {

            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Mode")]
            public string Mode { get; set; }
            [Required(ErrorMessage = "Please Enter Sub Cat Name")]
            public string SubCatName { get; set; }
            [Required(ErrorMessage = "Please Enter Sub Cat Short Name")]
            public string SubCatShortName { get; set; }
            [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Category")]
            public long Category { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Backup Id")]
            public long SubCategoryID { get; set; }

            public Int64 TotalCount { get; set; }
        }

        public class SubCategoryInputView
        {

            public long SlNo { get; set; }
            //[Required(ErrorMessage = "Please select a Post")]
            [Required(ErrorMessage = "Please Select Category")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Category")]
            public int CategoryID { get; set; }
            [Required(ErrorMessage = "Please Enter Sub Category Name")]
            [StringLength(150, ErrorMessage = "Sub Category Name Should be less than 150 characters")]
            public string SubCatName { get; set; }
            [Required(ErrorMessage = "Please Enter Sub Category Short Name")]
            [StringLength(10, ErrorMessage = "Sub Category Short Name Should be less than 10 charactures")]
            public string SubCatShortName { get; set; }
            
            [Required(ErrorMessage = "Please Select Sub Category")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Sub Category")]
            public Int64 SubCategoryID { get; set; }
            [Required(ErrorMessage = "Please Select Mode")]
            
            public string Mode { get; set; }
            // [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int16 SortOrder { get; set; }

            public Int64 Category { get; set; }
        }




        public class UpdateSubCategory
        {
            public long ID_SubCategory { get; set; }
            public string TransMode { get; set; }
            public string Mode { get; set; }
            public long FK_SubCategory { get; set; }
            public string SubCatName { get; set; }
            public string SubCatShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Category { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
           
            public string EntrBy { get; set; }
              
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_Branch { get; set; }
            public byte UserAction { get; set; }

        }
        public static string _deleteProcedureName = "proSubCategoryDelete";
        public static string _updateProcedureName = "proSubCategoryUpdate";
        public static string _selectProcedureName = "proSubCategorySelect";


       

        public class DeleteSubCategory
        {
            public long FK_SubCategory { get; set; }
            
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
           
        }


        public class SubCategoryInfoView
        {
            [Required(ErrorMessage = "Please select a Warranty Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Warranty Type")]
            public long SubCategoryID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }


        }


        public class Category_ID
        {
            public Int64 FK_Category { get; set; }
        }

        public class ModuleType
        {
            public string Mode { get; set; }
            public string ModuleName { get; set; }

        }

        public class Category
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

        public class Module
        {
            public string Mode { get; set; }
            public Int64 FK_Company { get; set; }
        }

        public class CategoryTypeList
        {
            public List<ModuleType> Modulelist { get; set; }
            public List<Category> CategoryList { get; set; }
            public int SortOrder { get; set; }

        }

        public class SubCategoryID
        {
            public Int64 FK_SubCategory { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }

            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser{ get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }


        public Output UpdateSubCategoryData(UpdateSubCategory input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSubCategory>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSubCategoryData(DeleteSubCategory input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSubCategory>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<SubCategoryView> GetSubCategoryData(SubCategoryID input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubCategoryView, SubCategoryID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}
