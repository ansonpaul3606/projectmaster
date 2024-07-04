using PerfectWebERPAPI.DataAccess;
using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PerfectWebERPAPI.Business.BlCustomerPortalAPIModel;

namespace PerfectWebERPAPI.Business
{
    public class BlCustomerPortalAPI : ICustomerPortalAPI
    {

        public BlCustomerPortalAPI()
        {
            Initialize();
        }


        private string _EntrBy { get; set; }
        private string _BankKey { get; set; }
        private string _TransMode { get; set; }
        private string _EnterBy { get; set; }
        private string _FK_Company { get; set; }
        private string _FK_Machine { get; set; }
        private string _CustomerRoleID { get; set; }
        private string _CustomerID { get; set; }
        private string _CustomerRole { get; set; }

        private string _Username { get; set; }
        private string _Phone { get; set; }
        private string _Email { get; set; }
        private string _RequestMode { get; set; }
        private string _Mode { get; set; }
        private string _SubMode { get; set; }
        private string _MPIN { get; set; }
        private string _OldMPIN { get; set; }
        private string _OTP { get; set; }
        private string _TicketStatus { get; set; }
        private string _TicketID { get; set; }


        string _JsonData { get; set; }
        string _Remarks { get; set; }
        string _MasterID { get; set; }
        string _TransAmount { get; set; }
        string _TransDate { get; set; }
        string _TransID { get; set; }
        string _TransType { get; set; }
        string _Type { get; set; }


        string _FK_Category { get; set; }
        string _FK_Branch { get; set; }
        string _FK_Product { get; set; }
        string _Name { get; set; }
        string _Offer { get; set; }
        string _Mobile { get; set; }
        string _ID_SalesProductDetails { get; set; }
        




        public void Initialize()
        {
            _EntrBy = string.Empty;
            _BankKey = string.Empty;
            _TransMode = string.Empty;
            _EnterBy = string.Empty;
            _FK_Company = string.Empty;
            _FK_Machine = string.Empty;
            ID_SalesProductDetails= string.Empty;
            _CustomerID = string.Empty;
            _CustomerRole = string.Empty;

            _Username = string.Empty;
            _Phone = string.Empty;
            _Email = string.Empty;
            _RequestMode = string.Empty;
            _Mode = string.Empty;
            _SubMode = string.Empty;
            _MPIN = string.Empty;
            _OTP = string.Empty;
            _TicketStatus = string.Empty;
            _TicketID = string.Empty;



            _JsonData = string.Empty;
            _Remarks = string.Empty;
            _MasterID = string.Empty;
            _TransAmount = string.Empty;
            _TransDate = string.Empty;
            _TransID = string.Empty;
            _TransType = string.Empty;
            _Type = string.Empty;

            _OldMPIN = string.Empty;

            _FK_Category = string.Empty;
            _FK_Branch = string.Empty;
            _FK_Product = string.Empty;
            _Name = string.Empty;
            _Offer = string.Empty;
            _Mobile = string.Empty;
            _CustomerRoleID = string.Empty;

        }
        public string ID_SalesProductDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_SalesProductDetails); }
            set { _ID_SalesProductDetails = value; }
        }
        public string CustomerRoleID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerRoleID); }
            set { _CustomerRoleID = value; }
        }
        public string EntrBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EntrBy); }
            set { _EntrBy = value; }
        }
        public string BankKey
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankKey); }
            set { _BankKey = value; }
        }
        public string TransMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
            set { _TransMode = value; }
        }
        public string EnterBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EnterBy); }
            set { _EnterBy = value; }
        }
        public string FK_Company
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Company); }
            set { _FK_Company = value; }
        }
        public string FK_Machine
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Machine); }
            set { _FK_Machine = value; }
        }


        public string CustomerID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerID); }
            set { _CustomerID = value; }
        }
        public string CustomerRole
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerRole); }
            set { _CustomerRole = value; }
        }


        public string Username
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Username); }
            set { _Username = value; }
        }
        public string Phone
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Phone); }
            set { _Phone = value; }
        }
        public string Email
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Email); }
            set { _Email = value; }
        }
        public string RequestMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_RequestMode); }
            set { _RequestMode = value; }
        }

        public string SubMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SubMode); }
            set { _SubMode = value; }
        }
        public string Mode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Mode); }
            set { _Mode = value; }
        }
        public string MPIN
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_MPIN); }
            set { _MPIN = value; }
        }

        public string OTP
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_OTP); }
            set { _OTP = value; }
        }
        public string TicketStatus
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TicketStatus); }
            set { _TicketStatus = value; }
        }

        public string TicketID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TicketID); }
            set { _TicketID = value; }
        }





        public string JsonData
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_JsonData); }
            set { _JsonData = value; }
        }
        public string Remarks
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Remarks); }
            set { _Remarks = value; }
        }
        public string MasterID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_MasterID); }
            set { _MasterID = value; }
        }
        public string TransAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransAmount); }
            set { _TransAmount = value; }
        }
        public string TransDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransDate); }
            set { _TransDate = value; }
        }

        public string TransID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransID); }
            set { _TransID = value; }
        }
        public string TransType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransType); }
            set { _TransType = value; }
        }
        public string Type
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Type); }
            set { _Type = value; }
        }

        public string OldMPIN
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_OldMPIN); }
            set { _OldMPIN = value; }
        }



        public string FK_Category
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Category); }
            set { _FK_Category = value; }
        }
        public string FK_Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Branch); }
            set { _FK_Branch = value; }
        }
        public string FK_Product
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Product); }
            set { _FK_Product = value; }
        }
        public string Name
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Name); }
            set { _Name = value; }
        }
        public string Offer
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Offer); }
            set { _Offer = value; }
        }
        public string Mobile
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Mobile); }
            set { _Mobile = value; }
        }


        //--- funcitons

        public CustomerInfoMobileModel BlGetCustomerInfoByMobile(BlCustomerPortalAPI objbl)
        {
            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetCustomerInfoByMobile(objbl);
            objDal = null;

            CustomerInfoMobileModel output = new CustomerInfoMobileModel();


            output=BlCustomerPortalAPIFormat.ConvertToCustomerLoginOutput(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }

        public CustomerManagePIN BlGetCustomerPinInfo(BlCustomerPortalAPI objbl)
        {
            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            objbl.Mode = "0";
            DataTable dt = objDal.ManageCustomerPIN(objbl);
            objDal = null;
            
            CustomerManagePIN output = new CustomerManagePIN();


            output = BlCustomerPortalAPIFormat.ConvertToCustomerManagePIN(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }

        public static string generate_Digits(int length)
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }

        public CustomerManagePIN BlUpdateCustomerOTP(BlCustomerPortalAPI objbl)
        {
            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();

            objbl.Mode = "1";  // this mode is to update the otp for the user in the table

            DataTable dt = objDal.ManageCustomerPIN(objbl);
            objDal = null;

            CustomerManagePIN output = new CustomerManagePIN();


            output = BlCustomerPortalAPIFormat.ConvertToCustomerManagePIN(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }

        public CompanyInfoOutput BlGetCompanyInfo(BlCustomerPortalAPI objbl)
        {
            CompanyInfoOutput output = new CompanyInfoOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetCompanyInfoByID(objbl);
            objDal = null;



            output = BlCustomerPortalAPIFormat.ConvertToCompanyInfoOutput(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }
        public CustomerInfoOutput BlGetCustomerInfo(BlCustomerPortalAPI objbl)
        {
            CustomerInfoOutput output = new CustomerInfoOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetCustomerInfoByID(objbl);
            objDal = null;


            output = BlCustomerPortalAPIFormat.ConvertToCustomerInfoOutput(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }

        public TicketStatusCountOutput BlGetTicketCountStatus(BlCustomerPortalAPI objbl)
        {
            TicketStatusCountOutput output = new TicketStatusCountOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetTicketCountStatusByID(objbl);
            objDal = null;


            output = BlCustomerPortalAPIFormat.ConvertToTicketStatusCountOutput(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }

        public TicketListOutput BlGetTicketList(BlCustomerPortalAPI objbl)
        {
            TicketListOutput output = new TicketListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetTicketList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToTicketListOutput(dt);
            return output;
        }
        public TicketInfoOutput BlGetTicketInfo(BlCustomerPortalAPI objbl)
        {
            TicketInfoOutput output = new TicketInfoOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetTicketInfoByID(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToTicketInfoOutput(dt);
            return output;
        }
        public List<TimeLineChartList> BlGetTrackInfo(BlCustomerPortalAPI objbl)
        {
            List<TimeLineChartList> output = new List<TimeLineChartList>();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetTrackInfo(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToTimeLineChartList(dt);
            return output;
        }

        public SMSDraftOutput BlSaveSMSDraft(BlCustomerPortalAPI objbl)
        {
            SMSDraftOutput output = new SMSDraftOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.SaveSMSDraft(objbl);
            objDal = null;

       
            return output;
        }

        public CustomerManagePIN BlResetCustomerMPIN(BlCustomerPortalAPI objbl)
        {
            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();

            objbl.Mode = "3";  // this mode is to update the otp for the user in the table

            DataTable dt = objDal.ManageCustomerPIN(objbl);
            objDal = null;

            CustomerManagePIN output = new CustomerManagePIN();


            output = BlCustomerPortalAPIFormat.ConvertToCustomerManagePIN(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }
        public CustomerManagePIN BlUpdateCustomerMPIN(BlCustomerPortalAPI objbl)
        {
            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();

            objbl.Mode = "2";  // this mode is to update the otp for the user in the table

            DataTable dt = objDal.ManageCustomerPIN(objbl);
            objDal = null;

            CustomerManagePIN output = new CustomerManagePIN();


            output = BlCustomerPortalAPIFormat.ConvertToCustomerManagePIN(dt);
            //return BlMobileNotificationFormat.ConvertToSendNotification(dt);
            return output;
        }
        //test commit
        //test commit

        public ProductEnqueryListOutput BlGetProductEnqueryList(BlCustomerPortalAPI objbl)
        {
            ProductEnqueryListOutput output = new ProductEnqueryListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.GetProductEnqueryList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToProductEnqueryListOutput(dt);
            return output;
        }

        // method to get the customer portal slider info
        public CustomerPortalSliderOutput BlGetCustomerPortalSliderList(BlCustomerPortalAPI objbl)
        {
            CustomerPortalSliderOutput output = new CustomerPortalSliderOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCustomerPortalSliderList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToCustomerPortalSliderOutput(dt);
            return output;
        }
        
        public CustomerProductInfoOutput BlGetCustomerProductInfo(BlCustomerPortalAPI objbl)
        {
            CustomerProductInfoOutput output = new CustomerProductInfoOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCustomerWiseProductDetails(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToCustomerProductInfoOutput(dt);
            return output;
        }
        public CompanyListOutput BlGetCompanyList(BlCustomerPortalAPI objbl)
        {
            CompanyListOutput output = new CompanyListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCompanyList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToCompanyListOutput(dt);
            return output;
        }
        public CategoryListOutput BlGetCategoryList(BlCustomerPortalAPI objbl)
        {
            CategoryListOutput output = new CategoryListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCompanyList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToCategoryListOutput(dt);
            return output;
        }
        public ProductListOutput BlGetProductList(BlCustomerPortalAPI objbl)
        {
            ProductListOutput output = new ProductListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCompanyList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToProductListOutput(dt);
            return output;
        }
        public GetRequestTypeOutput BlGetRequestType(BlCustomerPortalAPI objbl)
        {
            GetRequestTypeOutput output = new GetRequestTypeOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCompanyList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToGetRequestTypeOutput(dt);
            return output;
        }
        public GetServiceListOutput BlGetServiceListOutput(BlCustomerPortalAPI objbl)
        {
            GetServiceListOutput output = new GetServiceListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCompanyList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToGetServiceListOutput(dt);
            return output;
        }
        public GetComplaintListOutput BlGetComplaintListOutput(BlCustomerPortalAPI objbl)
        {
            GetComplaintListOutput output = new GetComplaintListOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetCompanyList(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToGetComplaintListOutput(dt);
            return output;
        }
        public GetProductDetailsOutput BlGetProductDetailsOutput(BlCustomerPortalAPI objbl)
        {
            GetProductDetailsOutput output = new GetProductDetailsOutput();

            DalCustomerPortalAPI objDal = new DalCustomerPortalAPI();
            DataTable dt = objDal.DalGetProductDetails(objbl);
            objDal = null;

            output = BlCustomerPortalAPIFormat.ConvertToGetProductDetailsOutput(dt);
            return output;
        }
    }
}
