using Newtonsoft.Json;
using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Business
{
    public class BlCustomerPortalAPIModel
    {
        private static object locker = new Object();

        public static void WriteToLog(String data, string BankKey)
        {
            //string path = @"C:\MscoreLog";
            string path = @"C:\ProdSuitAPIPILog\" + BankKey;
            string FilePath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
            lock (locker)
            {
                using (StreamWriter file = new StreamWriter(FilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString() + "::" + data);
                    file.Flush();
                    file.Close();
                }
            }
        }
        public static void CommonLog(string TransName, object obj, bool isrequest, string BankKey)
        {
            if (isrequest)
            {
                string ReqjsonParams = "Reqjsonparams::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||";
                WriteToLog(ReqjsonParams, BankKey);
                //BlCustomer objBl = (BlCustomer)obj;
                // objBl.RequestMessage = BlFormats.EncryptDataForAuthCode(ReqjsonParams);
            }
            else
            {
                string ResponseParams = "Response::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||\r\n";
                WriteToLog(ResponseParams, BankKey);
            }
        }
        public static string xmlTostring<T>(List<T> root)
        {
            //  root<T> root = new root<T>();
            // root.data = input;

            System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(root.GetType());
            System.IO.TextWriter writer2 = new System.IO.StringWriter();
            y.Serialize(writer2, root);

            return writer2.ToString();
        }

        public class CommonAPIResponse
        {
            public int StatusCode { get; set; }
            public string EXMessage { get; set; }
        }

        public class CommonAPIInputs
        {
            public string BankKey { get; set; }
        }

        public class CustomerLoginOutput : CommonAPIResponse
        {
            public CustomerLoginModel Data { get; set; }
        }
        public class CustomerLoginModel
        {
            public long CustomerID { get; set; }
            public short CustomerRoleID { get; set; }
            public string Role { get; set; }
            public long BranchID { get; set; }
            public long BranchUserCode { get; set; }
        }
        public class CustomerInfoMobileModel : CommonAPIResponse
        {
            public long CustomerID { get; set; }
            public short CustomerRoleID { get; set; }
            public string Role { get; set; }
            public long BranchID { get; set; }
            public long BranchUserCode { get; set; }
            public string MPIN { get; set; }
        }
        public class CustomerManagePIN : CommonAPIResponse
        {
            public bool HasMPIN { get; set; }
            public string MPIN { get; set; }
            public string OTP { get; set; }
            public bool Process { get; set; }
            public long ID_User { get; set; }
        }
        public class CustomerOTPOutput : CommonAPIResponse
        {
            public string Data { get; set; }
        }
        public class CustomerMPINOutput : CommonAPIResponse
        {
            public string Data { get; set; }
        }
        public class CompanyInfoOutput : CommonAPIResponse
        {
            public CompanyInfoModel Data { get; set; }
        }
        public class CompanyInfoModel 
        {
            public long ID_Company { get; set; }
            public string CompName { get; set; }
            public string CompContactPerson { get; set; }
            public string CompContactPMobile { get; set; }
            public string CompContactPEmail { get; set; }
            public string CompAddress1 { get; set; }
            public string CompAddress2 { get; set; }
            public string CompAddress3 { get; set; }
            public string CompEmail { get; set; }
            public string CompMobile { get; set; }
            public string CompPhone { get; set; }
            public string CompFax { get; set; }
            public string CompWebSite { get; set; }
            public string CompSocialMediaWebsite1 { get; set; }
            public string CompSocialMediaWebsite2 { get; set; }
            public string CompLogo { get; set; }
        }
        public class CustomerInfoOutput : CommonAPIResponse
        {

            public CustomerInfoModel Data { get; set; }
        }
        public class CustomerInfoModel
        {

            public long CustomerID { get; set; }
            //private string _customerNmae;
            //public string CustomerName {
            //    get { return _customerNmae; }
            //    set { _customerNmae = value == null ? "" : value; }
            //}
            private string _customerName;

            public string CustomerName
            {
                get { return _customerName ?? ""; }
                set { _customerName = value; }
            }
            public string CustomerNumber { get; set; }
            public string CustomerAddress1 { get; set; }
            public string CustomerMobile { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerEmail { get; set; }
            public byte CustomerRoleID { get; set; }
            public string CustomerRole { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Machine { get; set; }
        }
        public class TicketListOutput : CommonAPIResponse
        {
            public List<TicketInfoModel> Data { get; set; }
        }

        public class TicketInfoModel {
            public long TicketID { get; set; } //ok
            public string TicketNumber { get; set; }//ok
            public long CustomerID { get; set; }
            public string CustomerName { get; set; }//ok
            public string RegistrationOn { get; set; }//ok
            public byte Status { get; set; }//ok
            public string StatusText { get; set; }//ok
            public byte Priority { get; set; }//ok
            public string PriorityText { get; set; }//ok
            public string ClosedDate { get; set; }//ok
            public string TicketDescription { get; set; } //ok
            public byte RequestType { get; set; }//ok
            public long ProductID { get; set; }
            public string ProductName { get; set; } //OK

            public long ServiceID { get; set; }
            public string ServiceName { get; set; } //OK

            public long ComplaintID { get; set; }
            public string ComplaintName { get; set; } //OK
            public string ComplaintPriority { get; set; } //OK

            public long CategoryID { get; set; }
            public string CategoryName { get; set; } //OK
            public string VisitDate { get; set; }//OK 
            public string VisitTime { get; set; } //OK
            public string VisitPriority { get; set; }//OK 
            public string VisitRemark { get; set; } //OK
        }
        public class TicketStatusModel
        {
            public long StatusCount { get; set; }
            public string StatusText { get; set; }
            public long StatusID { get; set; }
        }
        public class TicketStatusCountOutput : CommonAPIResponse
        {
            public List<TicketStatusModel> Data { get; set; }
        }

        public class TicketDetailedInfoModel
        {
           
            public long TicketID { get; set; } //ok
            public string TicketNumber { get; set; }//ok
            public long CustomerID { get; set; }
            public short CustomerRoleID { get; set; }
            public string CustomerName { get; set; }
            public string RegistrationOn { get; set; }//ok
            public string CustomerContactNumber { get; set; }
            public string CustomerLandmark { get; set; }
            public byte Status { get; set; }//ok
            public string StatusText { get; set; }//ok
            public byte Priority { get; set; }//ok
            public string PriorityText { get; set; }//ok
            public string ClosedDate { get; set; }//ok
            public string TicketDescription { get; set; } //ok
            public long ProductID { get; set; } //ok
            public string ProductName { get; set; } //OK
            //ShowFeedbackForm OpenFeedbackForm
            public long ServiceID { get; set; } //ok
            public string ServiceName { get; set; } //OK
            public long ComplaintID { get; set; } //ok
            public string ComplaintName { get; set; } //OK
            public string ComplaintPriority { get; set; } //OK
            public long CategoryID { get; set; } //ok
            public string CategoryName { get; set; } //OK
            public string VisitDate { get; set; }//OK 
            public string VisitTime { get; set; } //OK
            public string VisitRemark { get; set; } //OK
            public string VisitPriority { get; set; }//OK 
            public long ID_Customerserviceassign { get; set; } //ok
            public byte ComplaintCategory { get; set; }//ok
            public string OtherCompanyName { get; set; }
            public string RequestType { get; set; }
            public string ProductType { get; set; }
            public List<PortalTicketEmployee> AssignList { get; set; }//ok
            public List<PortalFeedbackSubmitDetails> FeedbackSubmit { get; set; } //ok//ok
            public List<TimeLineChartList> TicketActivity { get; set; }
            public bool ShowFeedbackForm { get; set; } //ok
            public bool OpenFeedbackForm { get; set; } //ok
           
        }
        public class PortalTicketEmployee
        {
            public long EmployeeID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ContactNumber { get; set; }
        }
        public class PortalFeedbackSubmitDetails
        {
            public long FK_FeedbackDetails { get; set; }
            public long FK_Feedback { get; set; }
            public long ID_CustFeedback { get; set; }
            public string Remarks { get; set; }
        }
        public class TimeLineChartList
        {
            public long SLNo { get; set; }
            public string Title1 { get; set; }
            public string Title2 { get; set; }
            public string Description { get; set; }
            public string EntrOn { get; set; }
            public string EntrBy { get; set; }
            public string MoreData { get; set; }

        }
        public class TicketInfoOutput : CommonAPIResponse
        {
            public TicketDetailedInfoModel Data { get; set; }
        }
        public class SMSDraftOutput : CommonAPIResponse
        {
            public string Data { get; set; }
        }

        public class PortalSMSOTP
        {
            public string Mobile { get; set; }
            public string OTP { get; set; }
            public string Name { get; set; }

        }

        //test commit
        public class CustomerMPINInfoModel
        {
            public bool HasMPIN { get; set; }
          //  public string MPIN { get; set; }
           // public string OTP { get; set; }
            public bool Process { get; set; }
        }
        public class CustomerMPINInfoOutput : CommonAPIResponse
        {
            public CustomerMPINInfoModel Data { get; set; }
        }

        public class CustomerMPINUpdateOutput : CommonAPIResponse
        {
            public string Data { get; set; }
        }

        public class ProductEnqueryInfo
        {
            public long ID_Category { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal MRP { get; set; }
            public decimal SalPrice { get; set; }
            public long ID_Product { get; set; }
            public long CurrentQuantity { get; set; }
            public bool OFFER { get; set; }
            public string OfrName { get; set; }
            public string OfrDescription { get; set; }
            public string OfrExpireDate { get; set; }
            public bool HasLeadOfferAmount { get; set; }
            public decimal? LgpMRP { get; set; }
            public decimal? LgpSalesPrice { get; set; }
        }
        public class ProductEnqueryListOutput : CommonAPIResponse
        {
            public List<ProductEnqueryInfo> Data { get; set; }
        }


        // Model for the customer portal slider
        public class CustomerPortalSlider
        {
            public string SilderImage { get; set; }
            public string SilderImageRedirect { get; set; }
            public string SliderTitle { get; set; }
            public string SliderSubTitle { get; set; }
        }
        public class CustomerPortalSliderOutput : CommonAPIResponse
        {
            public List<CustomerPortalSlider> Data { get; set; }
        }
       
        public class CustomerProductInfoOutput : CommonAPIResponse
        {
            public List <CustomerProductInfoModel> Data { get; set; }
        }
        public class CustomerProductInfoModel
        {

            public long ProductID { get; set; }
            public string ProductName { get; set; }
            public long CategoryID { get; set; }
            public string Category { get; set; }
            public string SalBillNo { get; set; }
            public string SalBillDate { get; set; }
            public long ID_SalesProductDetails { get; set; }

        }
        public class CompanyListOutput : CommonAPIResponse
        {
            public List<CompanyListModel> Data { get; set; }
        }
        public class CompanyListModel
        {

            public long CompanyID { get; set; }
            public string CompanyName { get; set; }
            

        }
        public class CategoryListOutput : CommonAPIResponse
        {
            public List<CategoryListModel> Data { get; set; }
        }
        public class CategoryListModel
        {

            public long CategoryID { get; set; }
            public string CategoryName { get; set; }


        }
        public class ProductListOutput : CommonAPIResponse
        {
            public List<ProductListModel> Data { get; set; }
        }
        public class ProductListModel
        {

            public long ProductID { get; set; }
            public string ProductName { get; set; }


        }
        public class GetRequestTypeOutput : CommonAPIResponse
        {
            public List<RequestTypeModel> Data { get; set; }
        }
        public class RequestTypeModel
        {

            public long RequestTypeID { get; set; }
            public string RequestTypeName { get; set; }


        }
        public class GetServiceListOutput : CommonAPIResponse
        {
            public List<ServiceListModel> Data { get; set; }
        }
        public class ServiceListModel
        {

            public long ServiceListID { get; set; }
            public string ServiceListName { get; set; }


        }
        public class GetComplaintListOutput : CommonAPIResponse
        {
            public List<ComplaintListModel> Data { get; set; }
        }
       
        public class ComplaintListModel
        {

            public long ComplaintListID { get; set; }
            public string ComplaintListName { get; set; }


        }
        public class GetProductDetailsOutput : CommonAPIResponse
        {
            public List<ProductDetailsModel> Data { get; set; }
           
        }
        public class ProductDetailsModel
        {

            public long ProductID { get; set; }
            public string ProdctName { get; set; }
            public long CategoryID { get; set; }
            //public string Category { get; set; }
            private string _CatName;

            public string Category
            {
                get { return _CatName ?? ""; }
                set { _CatName = value; }
            }
            public string AMC { get; set; }
            public string Address { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string GSTIN { get; set; }

            public string Description { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceDate { get; set; }
            public string Quantity { get; set; }
            public long InvoiceAmount { get; set; }
            public string ReplacementWarrantyExpireDate { get; set; }
            public string ServiceWarrantyExpireDate { get; set; }
       
            public string ManufName { get; set; }
            public string ManufPContact { get; set; }
            public string ManufMobile { get; set; }
            public string ManufPhone { get; set; }
            public string ManufAddress { get; set; }
            public string ManufEmail { get; set; }
            public string ManufGSTIN { get; set; }
            public string ManufDescription { get; set; }
        }


        public class GetFeedBackUrlOutput : CommonAPIResponse
        {
            public FeedBackUrl Data { get; set; }
        }
        public class FeedBackUrl
        {

            public long TicketID { get; set; }
            public string TicketCode { get; set; }
            public string FeedbackURL { get; set; }
        }
    }
}
