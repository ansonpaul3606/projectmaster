using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class MultipleImageModel
    {
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
        public class IdentityProof
        {
            public int ID_IdentityProof { get; set; }
            public string IdName { get; set; }

        }
        public class ImageMode
        {
            public Int32 Mode { get; set; }
        }
        public class CommonImageList
        {
            public List<CompanyImage> CompanyImage { get; set; }
            public List<EmployeeImage> EmployeeImage { get; set; }
            public List<ProductImage> ProductImage { get; set; }
            public List<WarrantyTypeImage> WarrantyTypeImage { get; set; }

        }
        public class CompanyImage
        {

            public Int64 ID_CompanyImage { get; set; }
            public long FK_Company { get; set; }
            public int ComImgMode { get; set; }
            public string ComImgValue { get; set; }
            public string ComImgName { get; set; }
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
        public class WarrantyTypeImage
        {
            public Int64 ID_WarrantyTypeImage { get; set; }
            public int warrantyImgmode { get; set; }
            public string WarrantyImgValue { get; set; }
            public string WarrantyImgName { get; set; }
        }
        public class ProductImage
        {

            public Int64 ID_ProductImage { get; set; }
            public long FK_Product { get; set; }
            public int ProdImgMode { get; set; }
            public string ProdImgValue { get; set; }
            public string ProdImgName { get; set; }
        }

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

            public string Name { get; set; }
            public string NameCriteria { get; set; }
        }
        public APIGetRecordsDynamic<ImgMode> GetImgModelist(ImageMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImgMode, ImageMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamicdn<dynamic> GetCommonlist(GetPopSearch input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetPopSearch>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);
        }
    }
}