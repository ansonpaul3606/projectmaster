using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CustomerProductMappingModel
    {
        public class CustInfo
        {

            public long ID_Customer { get; set; }

            public string CustomerName { get; set; }
            public string CusReferenceNo { get; set; }
            
            public string CusAddress1 { get; set; }
            public long CusPhnNo { get; set; }
            public string CusEmail { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long PostID { get; set; }
            public long AreaID { get; set; }
            public string Company { get; set; }

            public string CntryName { get; set; }
            public string StName { get; set; }
            public string DtName { get; set; }
            public string PostName { get; set; }
            public string AreaName { get; set; }
            public string PinCode { get; set; }
            public string Landmark { get; set; }

            //public List<Customer> CustomerDetails { get; set; }


        }
        //public class Customer
        //{
        //    public string ID_customer { get; set; }
        //    public string Name { get; set; }
        //}

       
            public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public string EmpLName { get; set; }
            public long ID_Branch { get; set; }
            public long ID_BranchMode { get; set; }
            public long ID_BranchType { get; set; }
            public long FK_Department { get; set; }
            public string BTName { get; set; }
            public string BrName { get; set; }
            public string DeptName { get; set; }


        }

        public class CustomerProductMappingViewList
        {
            public long FK_Employee { get; set; }
            public List<EmployeeInfo> EmployeeInfoList { get; set; }
            public List<LeadFrom> BranchNameList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }
            public List<AMCTypeModel.AMCTypeView> AMCtype { get; set; }
            public string PinCode { get; set; }
            public long ?LastID { get; set; }
        }

        public class LeadFrom
        {
            public Int32 ID_LeadFrom { get; set; }
            public string LeadFromName { get; set; }
        }



        public class BranchTypes
        {
            public long BranchTypeID { get; set; }
            public string BranchType { get; set; }

            public long BranchModeID { get; set; }
            public long FK_BranchMode { get; set; }
            public long FK_BranchType { get; set; }

        }
        public class PostList
        {
            public int PostID { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }

        }
        public class GetProduct
        {
            public long ID_Product { get; set; }

            public string ProductName { get; set; }
            public string ProdShortName { get; set; }

            public string ProdHSNCode { get; set; }
            public Int64 Quantity { get; set; }
        }

       
        public class CustomerProductMappingDetailsView
        {
            public long ID_ServicemapProduct { get; set; }
            public Int32 Quantity { get; set; }
            public long ProductID { get; set; }
            public string ProductName { get; set; }

        }
        public class searchPinModel
        {
            public long CountryID { get; set; }    
            public string Country { get; set; }
            public long StatesID { get; set; }
            public string States { get; set; }
            public long DistrictID { get; set; }
            public string District { get; set; }
            public long PostID { get; set; }
            public string Post { get; set; }
            //public string CustomerName { get; set; }
            //public long CusPhnNo { get; set; }
            //public string CusAddress1 { get; set; }
            //public string Company { get; set; }
            public long PinCode { get; set; }
            public long AreaID { get; set; }
            public string AreaName { get; set; }
        }

       
        public class GetBranchName
        {
            public string TransMode { get; set; }
            public long ID_LeadFrom { get; set; }
            public string EntrBy { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
        }

        public class Branchs
        {
            public long BranchID { get; set; }
            public string Branch { get; set; }
            //public long FK_Branch { get; set; }
        }
       
        public class CustomerWiseProductList
        {
            public long SlNo { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            public long FK_CustomerWiseProductDetails { get; set; }    
            public long FK_Customer { get; set; }
            public long FK_Sales { get; set; }
            public long FK_Product { get; set; }
            public long FK_EMIPlan { get; set; }
            public DateTime? CWPDSalesDate { get; set; }
            public Int64 CWPDTotalAmount { get; set; }
            public Int64 CWPDInstalmentAmount { get; set; }
            public Int64 CWPDSalFreeQuantity { get; set; }
            public Int64 CWPDSalActualQuantity { get; set; }
            public byte CWPDProductStatus { get; set; }
            public string CWPDModelNo { get; set; }
            public string BillNo { get; set; }
            public string Remarks { get; set; }
            public Int64 TotalCount { get; set; }
            public string ProductName { get; set; }
            
            public string CustomerName { get; set; }
            public string CusAddress1 { get; set; }
            public string CusPhnNo { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long PostID { get; set; }
            public string Company { get; set; }
            public string Country { get; set; }
            public string States { get; set; }
            public string District { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }
            public Int64 CWPDSalQuantity { get; set; }
            public string Name { get; set; }
            public long BranchID { get; set; }
            public string Branch { get; set; }
            public long BranchTypeID { get; set; }
          
            public List<ProductDetailsView> CustomerWiseProductDetails { get; set; }
            //public List<ProductImage> ProductImageDetail { get; set; }
            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public string TransMode { get; set; }
            public string ID_LeadFrom { get; set; }
            public long ProductID { get; set; }
            public Int64 GroupID { get; set; }
            public long FK_SubProduct { get; set; }

            public long AreaID { get; set; }
            public string AreaName { get; set; }
            public string Landmark { get; set; }
            public string CusReferenceNo { get; set; }
            public long ?LastID { get; set; }
        }

        public class CustomerWiseProductID
        {

            //public long ID_CustomerWiseProductDetails { get; set; }
            public Int64 GroupID { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int64 Detailed { get; set; }
        }

        public class CustomerWiseProductDetailsView
        {
            public long SlNo { get; set; }
            public Int64 TotalCount { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Sales { get; set; }
            public long ProductID { get; set; }
            public long FK_EMIPlan { get; set; }
            public DateTime? CWPDSalesDate { get; set; }
            public Int64 CWPDTotalAmount { get; set; }
            public Int64 CWPDInstalmentAmount { get; set; }
            public Int64 CWPDSalQuantity { get; set; }
            public Int64 CWPDSalFreeQuantity { get; set; }
            public Int64 CWPDSalActualQuantity { get; set; }
            public byte CWPDProductStatus { get; set; }
            public string CWPDModelNo { get; set; }
            public string BillNo { get; set; }
            public string Remarks { get; set; }        
            public List<ProductDetailsView> CustomerWiseProductDetails { get; set; }
            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public string TransMode { get; set; }
            public long? PostID { get; set; }
            public long? AreaID { get; set; }
            public string AreaName { get; set; }
            public string Post { get; set; }
            public string PinCode { get; set; }
            public long DistrictID { get; set; }
            public string District { get; set; }
            public long StatesID { get; set; }
            public string States { get; set; }
            public long CountryID { get; set; }
            public string Country { get; set; }
            public string CusPhnNo { get; set; }
            public string Address1 { get; set; }
            public string CustomerName { get; set; }
            public long BranchID { get; set; }
            public string Branch { get; set; }
            public long BranchTypeID { get; set; }
            public string Company { get; set; }
            public string Landmark { get; set; }
            public string CusReferenceNo { get; set; }
            public long ?LastID { get; set; }
           
        }

        public class UpdateCustomerWiseProductDetails
        {
            public byte UserAction { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_CustomerWiseProductDetails { get; set; }
            public long FK_CustomerWiseProductDetails { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Sales { get; set; }
            public long FK_Product { get; set; }
            public long FK_EMIPlan { get; set; }
            public DateTime? CWPDSalesDate { get; set; }
            public Int64 CWPDTotalAmount { get; set; }
            public Int64 CWPDInstalmentAmount { get; set; }
            public Int64 CWPDSalQuantity { get; set; }
            public Int64 CWPDSalFreeQuantity { get; set; }
            public Int64 CWPDSalActualQuantity { get; set; }
            public byte CWPDProductStatus { get; set; }
            public string CWPDModelNo { get; set; }
            public string BillNo { get; set; }
            public string Remarks { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string CustomerWiseProductDetails { get; set; }
            public string WarrantyDetails { get; set; }
            public string WarrantyImgDetails { get; set; }
            public string CustomerName { get; set; }
            public string Address1 { get; set; }
            public string CusPhnNo { get; set; }
            public long FK_Country { get; set; }
            public long FK_State { get; set; }
            public long FK_Post { get; set; }
            public long FK_Area { get; set; }
            public long FK_District { get; set; }
            public long FK_Branch { get; set; }
            public string Company { get; set; }
            public string PinCode { get; set; }
            public string Landmark { get; set; }
            public string CusReferenceNo { get; set; }
            public long? LastID { get; set; }

        }
        public class ProductDetailsView
        {
            public long ID_CustomerWiseProductDetails { get; set; }
            public long GroupID { get; set; }
            public long ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal CWPDSalQuantity { get; set; }
            public String CWPDModelNo { get; set; }
            
            public long AMCFK_Master { get; set; }
            public long AMCMType { get; set; }
            public long AMCNoOfServices { get; set; }
            public DateTime? AMCMRenewduedate { get; set; }
            public DateTime? AMCMDuedate { get; set; }
            public string AMCRemarks { get; set; }
            public bool CWPDStandby { get; set; }

        }
        public class DeleteCustomerProduct
        {
           // public long FK_CustomerWiseProductDetails { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 GroupID { get; set; }
        }
        
        public class CustomerWiseProductViewData
        {
            public long FK_CustomerWiseProductDetails { get; set; }
            public Int64 GroupID { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 Detailed { get; set; }
        }

        public class CustomerWiseProductDataView
        {
            public Int64 GroupID { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int64 Detailed { get; set; }
        }

        public class WarProductImage
        {

            public Int64 ID_ProductImage { get; set; }
            public string TransMode { get; set; }
            public int ProdImageMode { get; set; }
            public long StockId { get; set; }
            public long ProductID { get; set; }
            public string ProdImageName { get; set; }
            public string ProdImage { get; set; }
            public long WarrantyType { get; set; }
            public string WarTypName { get; set; }


        }

        public class GetWarrantyImagein
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Master { get; set; }
        }

        public class GetProductImagein
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Master { get; set; }
        }
        public class WarrantyDetails
        {
            public long SlNo { get; set; }
            public string prodtid { get; set; }
            public Int32 stkid { get; set; }
            public Int32 subProductID { get; set; }
            public string subProName { get; set; }
            public long WarrantyType { get; set; }
            public string WarrantyType_d { get; set; } = "";
            public string Replcwardt { get; set; }
            public string Serwardt { get; set; }
           
        }
        public class AmcWarrantyDetailsInput
        {
            public byte Mode { get; set; }
            public long FK_Type { get; set; }
            public DateTime Date { get; set; }
            public decimal Quantity { get; set; }
        }
        public class GetAmcWarrantyDetails
        {
            public byte Mode { get; set; }
            public long FK_Type { get; set; }
            public DateTime Date { get; set; }
            public decimal Quantity { get; set; }
        }
        public class AmcWarrantyDetails
        {
            public long FK_WarrantyType { get; set; }
            public DateTime ReplaceWarrantyDate { get; set; }
            public DateTime ServiceWarrantyDate { get; set; }
            public string Amount { get; set; }
            public string TaxAmount { get; set; }
            public string NetAmount { get; set; }

            public long FK_AMCType { get; set; }
            public Int64 NoOfServices { get; set; }
            public DateTime AMCDuedate { get; set; }
            public DateTime AMCRenewduedate { get; set; }
            public string AmcAmount { get; set; }
            public string AMCTaxAmount { get; set; }
            public string AMCNetAmount { get; set; }
            public string ErrMsg { get; set; }
            public long ErrCode { get; set; }
        }
        public class GetWarrantyDtls
        {
            public long FK_Product { get; set; }
            public DateTime TransDate { get; set; }
            public long stockId { get; set; } = 0;
        }
        public static string _updateProcedureName = "ProCustomerWiseProductDetailsUpdate";

        public Output UpdateCustomerProductMappingData(UpdateCustomerWiseProductDetails input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerWiseProductDetails>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamicdn<dynamic> GetBranchNames(GetBranchName input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetBranchName>(companyKey: companyKey, procedureName: "proERPCmnSelect", parameter: input);
        }
        public APIGetRecordsDynamic<CustomerWiseProductList> GetCustomerWiseProductData(CustomerWiseProductID input, string companyKey)
        {
            // return Common.GetDataViaProcedure<CustomerWiseProductList, CustomerWiseProductID>(companyKey: companyKey, procedureName: "ProCustomerWiseProductDetailsSelect", parameter: input);
            return Common.GetDataViaProcedure<CustomerWiseProductList, CustomerWiseProductID>(companyKey: companyKey, procedureName: "ProCustomerWiseProductDetailsSelect", parameter: input);
            


        }
        public APIGetRecordsDynamic<ProductDetailsView> GetCustomerWiseProductDetails(CustomerWiseProductDataView input, string companyKey)
        {
            //return Common.GetDataViaProcedure<ProductDetailsView, CustomerWiseProductViewData>(companyKey: companyKey, procedureName: "ProCustomerWiseProductDataSelect", parameter: input);
            return Common.GetDataViaProcedure<ProductDetailsView, CustomerWiseProductDataView>(companyKey: companyKey, procedureName: "ProCustomerWiseProductDetailsSelect", parameter: input);
        }

        public APIGetRecordsDynamic<WarrantyDetails> GetWarrantySelect(GetWarrantyImagein input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetails, GetWarrantyImagein>(companyKey: companyKey, procedureName: "ProCustomerWiseProductDetailsWarrantyDetail", parameter: input);

        }
        public APIGetRecordsDynamic<WarProductImage> GetImageSelect(GetProductImagein input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarProductImage, GetProductImagein>(companyKey: companyKey, procedureName: "ProCustomerWiseProductDetailsProductImageSelect", parameter: input);

        }
        public APIGetRecordsDynamic<AmcWarrantyDetails> GetAmcWarrantyfill(GetAmcWarrantyDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<AmcWarrantyDetails, GetAmcWarrantyDetails>(companyKey: companyKey, procedureName: "ProGetAMCandWarrantyDetails", parameter: input);

        }
        public APIGetRecordsDynamic<WarrantyDetails> GetWarrantyDtlsData(GetWarrantyDtls input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetails, GetWarrantyDtls>(companyKey: companyKey, procedureName: "ProGetProductwarrantyDetails", parameter: input);

        }
    }
}