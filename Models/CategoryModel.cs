/*----------------------------------------------------------------------
Created By	: Diljith
Created On	: 30/01/2022
Purpose		: Category
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
    public class CategoryModel
    {

        public class CategoryView
        {
            public long SlNo { get; set; }
            public long CategoryID { get; set; }
            public string Category { get; set; }
            public string ShortName { get; set; }
            public string Mode { get; set; }
            public string ModeName { get; set; }
            public long ?AccountHeadID { get; set; }
            public string AccountHeadName { get; set; }
            public long? AccountHeadSubID { get; set; }
            public string AccountHeadSubName { get; set; }
            public bool Project { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }



            public bool ProdSales { get; set; }

            public bool ProdSalesReturn { get; set; }

            public bool ProdPurchase { get; set; }

            public bool ProdPurchaseReturn { get; set; }

            public bool ProdStockTransfer { get; set; }

            public bool ProdProductionIn { get; set; }

            public bool ProdProductionOut { get; set; }
            public bool ProdLead { get; set; }
            public bool ProdProject { get; set; }
        }

        public class AccountHeadView
        {

            public long AccountHeadID { get; set; }

            public string AHeadName { get; set; }

        }

        public class AccountSubHeadView
        {
            public long AccountHeadSubID { get; set; }
            public string ASHeadName { get; set; }

        }

        public class CategoryInputFromViewModel
        {
            ////[Required(ErrorMessage = "No Category Selected")]
            public long CategoryID { get; set; }

            ////[Required(ErrorMessage = "Please Enter Category")]
            public string Category { get; set; }

            ////[Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }

            //[Range(1, long.MaxValue, ErrorMessage = "Select Category Mode")]
            public string Mode { get; set; }

            public Int32 SortOrder { get; set; }

            //[Required(ErrorMessage = "Please Enter Account Head")]
            public long? AccountHeadID { get; set; }

            //[Required(ErrorMessage = "Please Enter Account Head Sub")]
            public long? AccountHeadSubID { get; set; }
            public bool Project { get; set; }
            public string TransMode { get; set; }


            public bool ProdSales { get; set; }

            public bool ProdSalesReturn { get; set; }

            public bool ProdPurchase { get; set; }

            public bool ProdPurchaseReturn { get; set; }

            public bool ProdStockTransfer { get; set; }

            public bool ProdProductionIn { get; set; }

            public bool ProdProductionOut { get; set; }
            public bool ProdLead { get; set; }
            public bool ProdProject { get; set; }

        }


        public class CategoryInfoView
        {
            [Required(ErrorMessage = "No Category Selected")]
            public Int64 CategoryID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }


        public static string _updateProcedureName = "procategoryUpdate";
        public class UpdateCategory
        {




            public byte UserAction { get; set; }
            public long FK_AccountHeadSub { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Category { get; set; }
            public bool Project { get; set; }

            public long ID_Category { get; set; }
            public string Mode { get; set; }
            public string CatName { get; set; }
            public string CatShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public bool Passed { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long BackupId { get; set; }


            public bool ProdSales { get; set; }
            public bool ProdSalesReturn { get; set; }
            public bool ProdPurchase { get; set; }
            public bool ProdPurchaseReturn { get; set; }
            public bool ProdStockTransfer { get; set; }
            public bool ProdProductionIn { get; set; }
            public bool ProdProductionOut { get; set; }
            public bool ProdLead { get; set; }
            public bool ProdProject { get; set; }
        }

        public static string _selectProcedureName = "proCategorySelect";
        public class GetCategory
        {
            public Int64 FK_Category { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

            public string Name { get; set; }
        }



        public static string _deleteProcedureName = "proCategoryDelete";
        public class DeleteCategory
        {
            public long FK_Category { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }


        public class ModeList
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }

        public class CategoryListModel
        {

            public List<ModeList> ModeList { get; set; }

            public int SortOrder { get; set; }

        }



      
        public class OutputTest
        {
            public long Column1 { get; set; }
            public int ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
       
        public APIGetRecordsDynamic<OutputTest> UpdateCategoryData(UpdateCategory input, string companyKey)
        
        {
            
            return Common.GetDataViaProcedure<OutputTest, UpdateCategory>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }


        public Output DeleteCategoryData(DeleteCategory input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCategory>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CategoryView> GetCategoryData(GetCategory input, string companyKey)
        {
            return Common.GetDataViaProcedure<CategoryView, GetCategory>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}

