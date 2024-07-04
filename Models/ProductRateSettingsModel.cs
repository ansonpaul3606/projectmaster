using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductRateSettingsModel
    {

        public class Productratesettingsview
        {
            public long SlNo { get; set; }
            public long ID_ProductRateSetting { get; set; }
            public long FK_PriceFixingType { get; set; }
            public string ID_Name { get; set; }
            public string PRSName { get; set; }

            public DateTime EffectDate { get; set; }
            public DateTime ExpiryDate { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public List<ProductRateSettingDetails> ProductRateSettingDetails { get; set; }
            public Int64 TotalCount { get; set; }

            public string TransMode { get; set; }
            public string Product { get; set; }
            public Int16? FK_Product { get; set; }

            public bool Active { get; set; }
           
                
        }

        public class CategoryList
        {
            public Int32 FK_Category { get; set; }
            public string Category { get; set; }

        }

        public class ProductRateSettingDetails
        {
            public Int16 ID_ProductRateSettingDetails { get; set; }

            public Int16 FK_Category { get; set; }

            public Int16? FK_Product { get; set; }
            public Int16 DiscountInType { get; set; }
            public decimal DiscountInValue { get; set; }
            public string FK_ProductRateSetting { get; set; }
            public string CatName { get; set; }
            public string Product { get; set; }






        }
        public class ProductratesettingsUpdate
        {
            public long ID_ProductRateSetting { get; set; }
            public long FK_PriceFixingType { get; set; }           

            public DateTime EffectDate { get; set; }
            public DateTime ExpiryDate { get; set; }
            public long FK_Company { get; set; }
          //  public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public long FK_ProductRateSetting { get; set; }

            public string ProductRateSettingDetails { get; set; }

            public bool Active { get; set; }  





        }



        public class ProductratesettingsID
        {
            public long FK_Productratesettings { get; set; }



            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Byte Details { get; set; }
        }

        public class PrdratesettingsRsnView
        {
            public long ID_Productratesettings { get; set; }

            public long ReasonID { get; set; }
        }
        public class ProductratesettingsDelete
        {
            public long FK_Productratesettings { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }
        public static string _updateProcedureName = "ProProductratesettingsUpdate";
        public static string _deleteProcedureName = "ProProductratesettingsDelete";
        public static string _selectProcedureName = "ProProductratesettingsSelect";
        public Output UpdateProductratesettingsData(ProductratesettingsUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ProductratesettingsUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<Productratesettingsview> GetProductratesettingsData(ProductratesettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<Productratesettingsview, ProductratesettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<Productratesettingsview> GetProductratesettingsDatainfoid(ProductratesettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<Productratesettingsview, ProductratesettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public class ProductRateSettingDetailsSubSelect
        {
            public Int64 FK_Productratesettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Byte Details { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public APIGetRecordsDynamic<ProductRateSettingDetails> GetProductratesettingsSubtabledata(ProductRateSettingDetailsSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductRateSettingDetails, ProductRateSettingDetailsSubSelect>(companyKey: companyKey, procedureName: "ProProductrateSettingDetailsSelect", parameter: input);

        }
    }
}