using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ExportImportModel
    {
        public class FormateView
        {
            public long SlNo { get; set; }
            public string SalBillDate { get; set; }
            public string SalBillNo { get; set; }
            public string CustomerName { get; set; }
            public string MobileNo { get; set; }

            private string delAddress1; // Private field to store the value

            public string DelAddress1
            {
                get { return delAddress1; }
                set { delAddress1 = value == null ? "" : value; } // Set to "" if the value is null
            }

            private string delAddress2;

            public string DelAddress2
            {
                get { return delAddress2; }
                set { delAddress2 = value == null ? "" : value; } // Set to "" if the value is null
            }



            private string area;
            public string Area
            {
                get { return area; }
                set { area = value == null ? "" : value; }
            }

            private string post;
            public string Post
            {
                get { return post; }
                set { post = value == null ? "" : value; }
            }

            private string district;
            public string District
            {
                get { return district; }
                set { district = value == null ? "" : value; }
            }

            private string state;
            public string State
            {
                get { return state; }
                set { state = value == null ? "" : value; }
            }

            private string country;
            public string Country
            {
                get { return country; }
                set { country = value == null ? "" : value; }
            }


            private string salDiscount;
            public string SalDiscount
            {
                get { return salDiscount; }
                set { salDiscount = value == null ? "" : value; }
            }

            private string salRoundoff;
            public string SalRoundoff
            {
                get { return salRoundoff; }
                set { salRoundoff = value == null ? "" : value; }
            }


            public string Product { get; set; }
            public string SpdSalQuantity { get; set; }
            public string MRPs { get; set; }
            public string SalePrice { get; set; }

            // public List<ProductDetailsView> Products { get; set; }

            public string SalBillNo_Product { get; set; }
        }
        public class ProductDetailsView
        {
            public string Product { get; set; }
            public string SpdSalQuantity { get; set; }
            public string MRPs { get; set; }
            public string SalePrice { get; set; }

        }

        public class ExportUpdate
        {
            public byte UserAction { get; set; }

            public string TransMode { get; set; }
            //public DateTime? SalEnterDate { get; set; }

            public long FK_Company { get; set; }
            public long FK_BrachCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            //List<FormateView> formates { get; set; }

            public string SalesDataDetails { get; set; }
            public long ImportFrom { get; set; }

        }


        public class ProductDetails
        {
            public long SlNo { get; set; }
            public Int32 ProductID { get; set; }
            public decimal SpdSalQuantity { get; set; }
            public decimal SalePrice { get; set; }
            public decimal MRPs { get; set; }
            public decimal Discp { get; set; }
            public decimal Discamt { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal NetAmt { get; set; }


        }

        public class SaveExceldata
        {
            public List<FormateView> dataitem { get; set; }
            public List<LeadDataView> leadDatas { get; set; }
            public List<ServiceDataView> serviceData { get; set; }
            public List<ProductDataView> productData { get; set; }
            public int TypeForm { get; set; }

        }

        public class GetSalesOutput
        {
            public string ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
        public class LeadDataView
        {
            public long SlNo { get; set; }
            public string EnquiryDate { get; set; }
            public string CollectedBy { get; set; }
            public string CustomerName { get; set; }
            public string ContactNo { get; set; }
            public string Category { get; set; }

            private string productOrProject; // Private field to store the value

            public string ProductOrProject
            {
                get { return productOrProject; }
                set { productOrProject = value == null ? "" : value; } // Set to "" if the value is null
            }


            public string Priority { get; set; }
            public string EnquiryNote { get; set; }
            public string Action { get; set; }
            public string FollowUpThrough { get; set; }
            public string FollowUpDate { get; set; }
            public string AssignedTo { get; set; }

            private string _LeadSource;
            public string LeadSource
            {
                get { return _LeadSource; }
                set { _LeadSource = value == null ? "" : value; }
            }

            private string _LeadFrom;
            public string LeadFrom
            {

                get { return _LeadFrom; }
                set { _LeadFrom = value == null ? "" : value; }
            }

            private string _whatsAppNo;
            public string WhatsAppNo
            {

                get { return _whatsAppNo; }
                set { _whatsAppNo = value == null ? "" : value; }
            }



            private string companyOrContactPerson;
            public string CompanyOrContactPerson
            {
                get { return companyOrContactPerson; }
                set { companyOrContactPerson = value == null ? "" : value; }
            }

            private string contactEmail;
            public string ContactEmail
            {
                get { return contactEmail; }
                set { contactEmail = value == null ? "" : value; }
            }

            private string houseName;
            public string HouseName
            {
                get { return houseName; }
                set { houseName = value == null ? "" : value; }
            }

            private string place;
            public string Place
            {
                get { return place; }
                set { place = value == null ? "" : value; }
            }

            private string country;
            public string Country
            {
                get { return country; }
                set { country = value == null ? "" : value; }
            }

            private string state;
            public string State
            {
                get { return state; }
                set { state = value == null ? "" : value; }
            }

            private string district;
            public string District
            {
                get { return district; }
                set { district = value == null ? "" : value; }
            }

            private string area;
            public string Area
            {
                get { return area; }
                set { area = value == null ? "" : value; }
            }

            private string post;
            public string Post
            {
                get { return post; }
                set { post = value == null ? "" : value; }
            }

            private string productQuantity;
            public string ProductQuantity
            {
                get { return productQuantity; }
                set { productQuantity = value == null ? "" : value; }
            }

            private string mrp;
            public string MRP
            {
                get { return mrp; }
                set { mrp = value == null ? "" : value; }
            }

            private string offerPrice;
            public string OfferPrice
            {
                get { return offerPrice; }
                set { offerPrice = value == null ? "" : value; }
            }

            private string floor;
            public string Floor
            {
                get { return floor; }
                set { floor = value == null ? "" : value; }
            }


        }

        public class ServiceDataView
        {
            public long SlNo { get; set; }
            public string RegisterDate { get; set; }
            public string RegisterTime { get; set; }
            public string Customer { get; set; }
            public string MobileNo { get; set; }
            public string Category { get; set; }

            private string product;
            public string Product {

                get { return product; }
                set { product = value == null ? "" : value; }
            }


            public string Type { get; set; }
            public string ComplaintOrService { get; set; }
            public string Priority { get; set; }
            public string Description { get; set; }

            private string contactNo;
            public string ContactNo
            {
                get { return contactNo; }
                set { contactNo = value == null ? "" : value; }
            }

            private string houseName;
            public string HouseName
            {
                get { return houseName; }
                set { houseName = value == null ? "" : value; }
            }

            private string place;
            public string Place
            {
                get { return place; }
                set { place = value == null ? "" : value; }
            }

            private string post;
            public string Post
            {
                get { return post; }
                set { post = value == null ? "" : value; }
            }

            private string area;
            public string Area
            {
                get { return area; }
                set { area = value == null ? "" : value; }
            }

            private string district;
            public string District
            {
                get { return district; }
                set { district = value == null ? "" : value; }
            }

            private string landmark;
            public string Landmark
            {
                get { return landmark; }
                set { landmark = value == null ? "" : value; }
            }

            private string requestedDateFrom;
            public string RequestedDateFrom
            {
                get { return requestedDateFrom; }
                set { requestedDateFrom = value == null ? "" : value; }
            }

            private string requestedDateTo;
            public string RequestedDateTo
            {
                get { return requestedDateTo; }
                set { requestedDateTo = value == null ? "" : value; }
            }

            private string requestedTimeFrom;
            public string RequestedTimeFrom
            {
                get { return requestedTimeFrom; }
                set { requestedTimeFrom = value == null ? "" : value; }
            }

            private string requestedTimeTo;
            public string RequestedTimeTo
            {
                get { return requestedTimeTo; }
                set { requestedTimeTo = value == null ? "" : value; }
            }

            private string complaintMedia;
            public string ComplaintMedia
            {
                get { return complaintMedia; }
                set { complaintMedia = value == null ? "" : value; }
            }


        }

        public class ProductDataView
        {
            public long SlNo { get; set; }
            public string Department { get; set; }
            public string Category { get; set; }
            private string _subCategory;
            public string SubCategory {
                get { return _subCategory; }
                set { _subCategory = value == null ? "" : value; }
            }
            public string Product { get; set; }
            private string _productDes;
            public string ProductDescription
            {
                get { return _productDes; }
                set { _productDes = value == null ? "" : value; }
            }
            private string _manufacture;
            public string Manufacture {
                get { return _manufacture; }
                set { _manufacture = value == null ? "" : value; }
            }
            private string _hsncode;
            public string HSNCode {
                get { return _hsncode; }
                set { _hsncode = value == null ? "" : value; }
            }
            public string Unit { get; set; }
            public string AltUnit { get; set; }
            private string _qrcode;
            public string QRCode {
                get { return _qrcode; }
                set { _qrcode = value == null ? "" : value; }
            }
            private string _barcode;
            public string BarCode {
                get { return _barcode; }
                set { _barcode = value == null ? "" : value; }
            }
            public string TaxPercentage { get; set; }

            private string _mrp;
            public string MRP {
                get { return _mrp; }
                set { _mrp = value == null ? "0" : value; }
            }
            private string _salePrice;
            public string SalePrice {
                get { return _salePrice; }
                set { _salePrice = value == null ? "0" : value; }
            }
            private string _purRate;
            public string PurRate {
                get { return _purRate; }
                set { _purRate = value == null ? "0" : value; }
            }
            private string _openingQty;
            public string OpeningQty {
                get { return _openingQty; }
                set { _openingQty = value == null ? "0" : value; }
            }




        }
        public APIGetRecordsDynamic<GetSalesOutput> UpdateSalesData(ExportUpdate input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetSalesOutput, ExportUpdate>(companyKey: companyKey, procedureName: "ProBulkSalesUpdate", parameter: input);
        }

    }
}