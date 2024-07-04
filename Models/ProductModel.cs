//---------------------------------------------------------------------
//Created By  : Aiswarya
//Created On	: 15/01/2022
//Purpose		: Product
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductModel
    {
        public class ProductView
        {
            public long SlNo { get; set; }
            public string Mode { get; set; }
            public string TransMode { get; set; }
           
            public string ProdName { get; set; }
          
            public string ProdShortName { get; set; }

            public string ProdHSNCode { get; set; }

            public decimal ProdMinLevel { get; set; }
            public decimal ProdMaxLevel { get; set; }

            public decimal ProdReorderLevel { get; set; }

            public decimal ProdReorderQuantity { get; set; }

            public string ProdMaterialDetails { get; set; }

            public bool ProdSales { get; set; }

            public bool ProdSalesReturn { get; set; }

            public bool ProdPurchase { get; set; }

            public bool ProdPurchaseReturn { get; set; }

            public bool ProdStockTransfer { get; set; }

            public bool ProdProductionIn { get; set; }

            public bool ProdProductionOut { get; set; }

            public string ProdQRCode { get; set; }
            public string ProdCode { get; set; }

            public string ProdBarcode { get; set; }
          
            public long CategoryID { get; set; }
            public bool ProdNonStockItem { get; set; }

            public long SubCategotyID { get; set; }
            
            public long ManufacturerID { get; set; }
         
            public long UnitID { get; set; }
            public long TaxGroupID { get; set; }
          
            public List<TaxType> TaxTypeDetails  { get; set; }
            public List<InclusiveProduct> SubProductDetails { get; set; }
            public List<ProductWiseWarrantyDetails> WarrantyDetails { get; set; }
            public Int64 ProductID { get; set; }
            public Int64 ReasonID { get; set; }
            public decimal? NoOfUnits { get; set; }
            public Int64 TotalCount { get; set; }
            public bool ProdLead { get; set; }
            public bool ProdProject { get; set; }

            public decimal ?MinAmount { get; set; }
            public decimal ?MaxAmount { get; set; }
            //public List<OpeningStockDetails> OpeningStockDetails { get; set; }

            public decimal MRP { get; set; }
            public decimal SalPrice { get; set; }
            public decimal ProductionCost { get; set; }
            public decimal OpeningQuantity { get; set; }
            public decimal OpeningStbyQuantity { get; set; }
            public long BranchID { get; set; }
            public long DepartmentID { get; set; }
            //public string QRCode { get; set; }
            //public string BarCode { get; set; }
            public string BatchNo { get; set; }
            public string ExpiryDate { get; set; }
            public decimal PurRate { get; set; }
            public List<string> MultipleUnits { get; set; }
            public List<MultiUnit> MultiUnitList { get; set; }

        }
        public class WarrantyList
        {
            public int ID_WarrantyTypeSetting { get; set; }
            public string WarrantyTypeSetting { get; set; }
        }
        public class TaxType
        {
            public long ID_TaxType { get; set; }

            public string TaxtyName { get; set; }

            public decimal TaxPercentage { get; set; }
            public decimal AmountWise { get; set; }
            public Boolean Percentage { get; set; }
            public Boolean TaxOnNetamount { get; set; }
        }
        public class InclusiveProduct
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            public decimal Quantity { get; set; }
            public Boolean Fixed { get; set; }
            public Boolean SprodSubProduct { get; set; }
            public Boolean PrintView { get; set; }
            
        }
        public class ProductWiseWarrantyDetails
        {
            public long FK_WarrantyType { get; set; }
            public int ProdWtyRepDurType { get; set; }
            public int ProdWtyRepDurPrdU { get; set; }
            public int ProdWtySerDurType { get; set; }
            public int ProdWtySerDurPrd { get; set; }
        }
        public class ProductWiseWarrantyView
        {
            public long FK_WarrantyType { get; set; }
            public int ProdWtyRepDurType { get; set; }
            public int ProdWtyRepDurPrdU { get; set; }
            public int ProdWtySerDurType { get; set; }
            public int ProdWtySerDurPrd { get; set; }
            
        }
        public class UpdateProduct
        {
            public byte UserAction { get; set; }
            public long ID_Product { get; set; }
            public string Mode { get; set; }
            public string ProdName { get; set; }
            public string ProdShortName { get; set; }
            public string ProdHSNCode { get; set; }
            public decimal ProdMinLevel { get; set; }
            public decimal ProdMaxLevel { get; set; }
           public decimal ProdReorderLevel { get; set; }
            public decimal ProdReorderQuantity { get; set; }
            public string ProdMaterialDetails { get; set; }
            public bool ProdSales { get; set; }
            public bool ProdSalesReturn { get; set; }
            public bool ProdPurchase { get; set; }
            public bool ProdPurchaseReturn { get; set; }
            public bool ProdStockTransfer { get; set; }
            public bool ProdProductionIn { get; set; }
            public bool ProdProductionOut { get; set; }
            public string ProdQRCode { get; set; }
            public string ProdCode { get; set; }
            public string ProdBarcode { get; set; }

            public bool ProdNonStockItem { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategoty { get; set; }
            public long FK_Manufacturer { get; set; }
            public long FK_Unit { get; set; }
            public long TaxGroupID { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
          
            public string EntrBy { get; set; }
           
            public long FK_Machine { get; set; }
          
            public string SubProductDetails { get; set; }
            public string WarrantyDetails { get; set; }
            public long FK_Product { get; set; }
            public bool Debug { get; set; }
           public string TransMode { get; set; }

            public bool ProdLead { get; set; }
            public bool ProdProject { get; set; }
            public decimal MinRate { get; set; }
            public decimal MaxRate { get; set; }
            //public string OpeningStockDetails { get; set; }

            public decimal MRP { get; set; }
            public decimal SalPrice { get; set; }
            public decimal ProductionCost { get; set; }
            public decimal OpeningQuantity { get; set; }
            public decimal OpeningStbyQuantity { get; set; }
            public long BranchID { get; set; }
            public long DepartmentID { get; set; }
            //public string QRCode { get; set; }
            //public string BarCode { get; set; }
            public string BatchNo { get; set; }
            public string ExpiryDate { get; set; }
            public decimal PurRate { get; set; }
            public string MultipleUnits { get; set; }
        }
        public static string _deleteProcedureName = "proProductDelete";
        public static string _updateProcedureName = "proProductUpdate";
        public static string _selectProcedureName = "proProductSelect";

        public class DeleteProduct
        {
            public long ID_Product { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }

        }
        public class Category
        {
            public string  Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public class Subcategory
        {
            public long CategoryID { get; set; }
            public long ID_SubCategory { get; set; }
            public string SubCatName { get; set; }
        }
        public class Manufacture
        {
            public long ID_Manufacturer { get; set; }
            public string ManufName { get; set; }
        }
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
        }
        public class MultiUnit
        {
            public long FK_Unit { get; set; }
            public string UnitName { get; set; }
        }
        public class MultipleUnits
        {
            public string FK_Unit { get; set; }
        }
        public class TaxGroup
        {
            public long TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class ModuleType
        {
            public long ID_ModuleType { get; set; }
            public string ModuleName { get; set; }

            public string Mode { get; set; }
        }
        public class DropDownListModel
        {
         
            public List<Category> CategoryList { get; set; }
            public List<Subcategory> SubCategoryList { get; set; }
            public List<Manufacture> ManufactureList   { get; set; }
            public List<Unit> UnitList { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public List<StateModel.StatesView> Statelist { get; set; }
            public List<ModuleType> ModuleTypeList { get; set; }
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }
            public List<WarrantyList> warrlist { get; set; }
            public List<BranchTo> BranchListTo { get; set; }
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public List<MultiUnit> MultipleUnitList { get; set; }
            public List<MultipleUnitSettings> MultiUnitSettingList { get; set; }
        }

        public class MultipleUnitSettings
        {
            public bool GsValue { get; set; }
            public string GsField { get; set; }

        }
        public class ProductID
        {
            public Int64 ID_Product { get; set; }
        }
        public class GetProduct
        {
            public Int64 ID_Product { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Manufacturer { get; set; }
            public Int64 FK_Supplier { get; set; }
            public string Name  { get; set; }
            public string ShortName { get; set; }
            public string TransMode { get; set; }

        }

        public class GetProductID
        {
            public Int64 ID_Product { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Manufacturer { get; set; }
            public Int64 FK_Supplier { get; set; }
            public string Name { get; set; }
         
          
           
          //  public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string ShortName { get; set; }
            public string Search { get; set; }    
        }
        public class ProductEnquiry
        {           
            public List<CategoryList> CategoryList { get; set; }           
        }

        public class CategoryList
        {
            public Int32 FK_Category { get; set; }
            public string Category { get; set; }

        }
        public class GetProductEnquiry
        {
            public long FK_Category { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Product { get; set; }
            public string Name { get; set; }
            public long Offer { get; set; }
        }
        public class ProductEnquiryData
        {
            public long ID_Product { get; set; }
            public long ID_Category { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal MRP { get; set; }
            public decimal SalPrice { get; set; }
            public decimal CurrentQuantity { get; set; }
            public int OFFER { get; set; }
            public string OfrName { get; set; }
            public string OfrDescription { get; set; }
            public string OfrExpireDate { get; set; }
           
        }
        public class GetProductOffer
        {
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
        }
        public class ProductOfferData
        {
            public long ID_Offers { get; set; }
            public string OfrName { get; set; }
            public string OfrDescription { get; set; }
            public string ExpireDate { get; set; }
        }
        public class ProductImages
        {
            public List<CategoryList> CategoryList { get; set; }
        }
        public class ProductImageDetails
        {
            public string ImageData { get; set; }
            public string FileName { get; set; }
            public int DefaultImage { get; set; }
        }
        public class SaveImages
        {           
            public long FK_Product { get; set; }          
            public List<ProductImageDetails> ImageList { get; set; }
        }
        public class UpdateProductImages
        {
            public long FK_Company { get; set; }         
            public long FK_Product { get; set; }
            public string ImageList { get; set; }
            public string EntrBy { get; set; }
        }
        public class GetProductInfoImages
        {
            public long FK_Product { get; set; }
            public long FK_Company { get; set; }
        }
        public class ProductInfoImages
        {
            public string ImageData { get; set; }
            public string FileName { get; set; }
            public bool DefaultImage { get; set; } 
           
        }

        public class BranchTo
        {
            public Int32 BranchIDTo { get; set; }
            public string BranchNameTo { get; set; }

        }
        public class DepartmentTo
        {
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }

        }

        public class OpeningStockDetails
        {
            public decimal OpeningQuantity { get; set; }
            public decimal OpeningStbyQuantity { get; set; }
            public long BranchID { get; set; }
            public long DepartmentID { get; set; }
        }
        public class OutputTest{
            public long Column1 { get; set; }
            public int ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
        //  Change UpdateProductData to GetDataViaProcedure for show the Error message in Direct procedure
        public APIGetRecordsDynamic<OutputTest> UpdateProductData(UpdateProduct input, string companyKey)
        //public Output UpdateProductData(UpdateProduct input, string companyKey)
        {
           // return Common.UpdateTableData<UpdateProduct>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
            return Common.GetDataViaProcedure<OutputTest, UpdateProduct>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProductData(DeleteProduct input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProduct>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProductView> GetProductData(GetProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductView, GetProduct>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ProductView> GetProductDataSearch(GetProductID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductView, GetProductID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        // public static string _selectProcedureName = "proProductSelect";
        public APIGetRecordsDynamic<TaxType> GettaxData(GetProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<TaxType, GetProduct>(companyKey: companyKey, procedureName: "proProductTaxSelect", parameter: input);

        }
        public APIGetRecordsDynamic<InclusiveProduct> GetSubProduct(GetProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<InclusiveProduct, GetProduct>(companyKey: companyKey, procedureName: "ProSubProductSelect", parameter: input);

        }

        public APIGetRecordsDynamic<ProductWiseWarrantyView> GetProductWarranty(GetProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductWiseWarrantyView, GetProduct>(companyKey: companyKey, procedureName: "ProProductWiseWarrantySelect", parameter: input);
        }
        public APIGetRecordsDynamic<ProductEnquiryData> GetProductEnquiryData(GetProductEnquiry input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductEnquiryData, GetProductEnquiry>(companyKey: companyKey, procedureName: "ProProductEnquiry", parameter: input);
        }
        public APIGetRecordsDynamic<ProductOfferData> GetProductOfferData(GetProductOffer input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductOfferData, GetProductOffer>(companyKey: companyKey, procedureName: "ProGetProductOffers", parameter: input);
        }
        public Output UpdateProductImagesData(UpdateProductImages input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductImages>(parameter: input, companyKey: companyKey, procedureName: "ProProductImagesUpdate");
        }
        public APIGetRecordsDynamic<ProductInfoImages> GetProductDataWithImage(GetProductInfoImages input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductInfoImages, GetProductInfoImages>(companyKey: companyKey, procedureName: "ProGetProductInfoWithImage", parameter: input);
        }
        public APIGetRecordsDynamic<MultiUnit> GetMultipleunits(GetProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<MultiUnit, GetProduct>(companyKey: companyKey, procedureName: "ProMultiUnitProductSelect", parameter: input);

        }
    }
}


