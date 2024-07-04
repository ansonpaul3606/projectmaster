using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CommonSearchPopupModel
    {
        public class GetPopSearch
        {
            public string TransMode { get; set; }
            public long Pagemode { get; set; }          
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }         
            public int Critrea1 { get; set; }
            public int Critrea2 { get; set; }
            public string Critrea3 { get; set; }
            public string Critrea4 { get; set; }
        
            public Int64 TotalCount { get; set; }
            public Int64 ID { get; set; }
            public int Criteria5 { get; set; }
            public string Name { get; set; }
            public string NameCriteria { get; set; }
        }
        public class CommonImageList
        {
            public List<CompanyImage> CompanyImage { get; set; }
            public List<EmployeeImage> EmployeeImage { get; set; }
            public List<WarProductImage> WarProductImage { get; set; }
            public List<WarrantyProductImage> WarrantyProductImage { get; set; }
            public List<ProductionImage> ProductionImage { get; set; }
        }
        public class Imagelist
        {
            public List<ImgMode> ImgModeList { get; set; }
          
            public List<IdentityProof> IdentityProofs { get; set; }
        }
        public class ImgMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; } 
            public Int32 ModeVal { get; set; }
        }
        public class ProductImage
        {
            public Int32 FK_Product { get; set; }
            public string ModeName { get; set; }
            public Int32 ModeVal { get; set; }
        }

        
        public class IdentityProof
        {
            public int ID_IdentityProof { get; set; }
            public string IdName { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class CompanyImage
        {

            public Int64 ID_CompanyImage { get; set; }
            public long FK_Company { get; set; }
            public int ComImgMode { get; set; }
            public string ComImgValue { get; set; }
            public string ComImgName { get; set; }
        }
        //ID_ProductImage TransMode   FK_Master FK_Customer FK_Product FK_SubProduct   ProdImageName ProdImage   Cancelled CancelledBy CancelledOn

        public class WarProductImage
        {
                     
            public Int64 ID_ProductImage { get; set; }
            public string TransMode { get; set; }
            public int ProdImageMode { get; set; }
            public long StockId { get; set; }
            public long FK_Product { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public long WarrantyType { get; set; }
           public string WarTypName { get; set; }
    }
        public class WarrantyProductImage
        {

            public Int64 ID_ProductImage { get; set; }
            public string TransMode { get; set; }
            public int ProdImageMode { get; set; }
            public long StockId { get; set; }
            public long FK_Product { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public long WarrantyType { get; set; }
            public string WarTypName { get; set; }
        }

        public class ProductionImage
        {

            public Int64 ID_ProductionStageImages { get; set; }
            public string TransMode { get; set; }
            public int ProdImageMode { get; set; }
            public string PSImageName { get; set; }
            public string PSImage { get; set; }
            public string PSDescription { get; set; }
        }
        public class ImageListView
        {
            public long SLNo { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Product { get; set; }
            public long FK_SubProduct { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public string ProdImageMode { get; set; }
            public string ProdImageDescription { get; set; }
            public long ID_ProductImage { get; set; }
            public long WarrantyType { get; set; }
            public string WarTypName { get; set; }
            public string ProdImageType { get; set; }

        }
        public class EmployeeImage
        {
            public Int64 ID_EmployeeImage { get; set; }
            public long FK_Employee { get; set; }
            public int FK_AccountCode { get; set; }
            public int EmpImgmode { get; set; }
            public string EmpImgValue { get; set; }
            public string EmpImgName { get; set; }
        }
        public class GetImagein
        {

            public string TransMode { get; set; }
            public Int64 FK_Master { get; set; }
            public Int64 FK_Product { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 StockId { get; set; }

        }

        public APIGetRecordsDynamicdn<dynamic> GetCommonlist(GetPopSearch input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetPopSearch>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);
        }
        public APIGetRecordsDynamic<ImgMode> GetImgModelist(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImgMode, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<ImageListView> GetImageSelect(GetImagein input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageListView, GetImagein>(companyKey: companyKey, procedureName: "ProProductImageSelect", parameter: input);

        }

    }
}