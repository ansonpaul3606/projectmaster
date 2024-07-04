using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Interface;
using System.Data;
using PerfectWebERPAPI.DataAccess;

namespace PerfectWebERPAPI.Business
{
   public class BlEMICollection:IEMICollection
    {
        #region Variable
        private string _ReqMode { get; set; }
        private string _FK_Company { get; set; }
        private string _Token { get; set; }
        private string _SubMode { get; set; }
        private string _BankKey { get; set; }
        private string _EntrBy { get; set; }
        private string _FK_BranchCodeUser { get; set; }
        private string _FK_Employee { get; set; }
        private string _FromDate { get; set; }
        private string _ToDate { get; set; }
        private string _FK_FinancePlanType { get; set; }
        private string _FK_Product { get; set; }
        private string _FK_Branch { get; set; }
        private string _FK_Customer { get; set; }
        private string _CedEMINo { get; set; }
        private string _FK_Area { get; set; }
        private string _FK_Category { get; set; }
        private string _Demand { get; set; }
        private string _TrnsDate { get; set; }
        private string _ID_CustomerWiseEMI { get; set; }
        private string _AccountMode { get; set; }
        private string _CollectDate { get; set; }
        private string _TotalAmount { get; set; }
        private string _FineAmount { get; set; }
        private string _NetAmount { get; set; }
        private string _LocLatitude { get; set; }
        private string _LocLongitude { get; set; }
        private string _Address { get; set; }
        private string _LocationEnteredDate { get; set; }
        private string _LocationEnteredTime { get; set; }
        private string _ID_User { get; set; }
        private string _ID_TokenUser { get; set; }
        #endregion
        #region Constructor
        public BlEMICollection()
        {
            Initialize();
        }
        #endregion
        #region Initialize
        public void Initialize()
        {
            _ReqMode = string.Empty;
            _FK_Company = string.Empty;
            _Token = string.Empty;
            _SubMode = string.Empty;
            _BankKey = string.Empty;
            _EntrBy = string.Empty;
            _FK_BranchCodeUser = string.Empty;
            _FK_Employee = string.Empty;
            _FromDate = string.Empty;
            _ToDate = string.Empty;
            _FK_FinancePlanType = string.Empty;
            _FK_Product = string.Empty;
            _FK_Branch = string.Empty;
            _FK_Customer = string.Empty;
            _CedEMINo = string.Empty;
            _FK_Area = string.Empty;
            _FK_Category = string.Empty;
            _Demand = string.Empty;
            _TrnsDate = string.Empty;
            _ID_CustomerWiseEMI = string.Empty;
             _AccountMode = string.Empty;
            _CollectDate = string.Empty;
            _TotalAmount = string.Empty;
             _FineAmount = string.Empty;
            _NetAmount = string.Empty;
            _LocLatitude = string.Empty;
            _LocLongitude = string.Empty;
            _Address = string.Empty;
            _LocationEnteredDate = string.Empty;
            _LocationEnteredTime = string.Empty;
            _ID_User = string.Empty;
            _ID_TokenUser = string.Empty;
        }
        #endregion
        #region Getters And Setters
        public string ID_TokenUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_TokenUser); }
            set { _ID_TokenUser = value; }
        }
        public string PaymentDetail { get; set; }
        public string EMIDetails { get; set; }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
        }
        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string Token
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Token); }
            set { _Token = value; }
        }

        public string FK_Company
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Company); }
            set { _FK_Company = value; }
        }
        public string SubMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SubMode); }
            set { _SubMode = value; }
        }
        public string BankKey
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankKey); }
            set { _BankKey = value; }
        }
        public string EntrBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EntrBy); }
            set { _EntrBy = value; }
        }
        public string FK_BranchCodeUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchCodeUser); }
            set { _FK_BranchCodeUser = value; }
        }
        public string FK_Employee
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Employee); }
            set { _FK_Employee = value; }
        }
        public string FromDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FromDate); }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ToDate); }
            set { _ToDate = value; }
        }
        public string FK_FinancePlanType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_FinancePlanType); }
            set { _FK_FinancePlanType = value; }
        }
        public string FK_Product
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Product); }
            set { _FK_Product = value; }
        }
        public string FK_Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Branch); }
            set { _FK_Branch = value; }
        }
        public string FK_Customer
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Customer); }
            set { _FK_Customer = value; }
        }
        public string CedEMINo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CedEMINo); }
            set { _CedEMINo = value; }
        }
        public string FK_Area
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Area); }
            set { _FK_Area = value; }
        }
        public string FK_Category
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Category); }
            set { _FK_Category = value; }
        }
        public string Demand
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Demand); }
            set { _Demand = value; }
        }
      
        public string TrnsDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TrnsDate); }
            set { _TrnsDate = value; }
        }
        public string ID_CustomerWiseEMI
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerWiseEMI); }
            set { _ID_CustomerWiseEMI = value; }
        }     
        public string AccountMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_AccountMode); }
            set { _AccountMode = value; }
        }
        public string CollectDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CollectDate); }
            set { _CollectDate = value; }
        }
        public string TotalAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TotalAmount); }
            set { _TotalAmount = value; }
        }
        public string FineAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FineAmount); }
            set { _FineAmount = value; }
        }
        public string NetAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NetAmount); }
            set { _NetAmount = value; }
        }
        public string LocLatitude
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocLatitude); }
            set { _LocLatitude = value; }
        }
        public string LocLongitude
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocLongitude); }
            set { _LocLongitude = value; }
        }
        public string Address
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Address); }
            set { _Address = value; }
        }
        public string LocationEnteredDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocationEnteredDate); }
            set { _LocationEnteredDate = value; }
        }
        public string LocationEnteredTime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocationEnteredTime); }
            set { _LocationEnteredTime = value; }
        }
       
        #endregion
        #region Data Access Function
        public EMICollectionReportCount BlEMICollectionReportCount(BlEMICollection objbl)
        {

            DalEMICollection objDal = new DalEMICollection();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlEMICollectionFormats.ConvertEMICollectionReportCount(dt);

        }
        public EMICollectionReport BlEMICollectionReport(BlEMICollection objbl)
        {

            DalEMICollection objDal = new DalEMICollection();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlEMICollectionFormats.ConvertEMICollectionReport(dt);

        }
        public EMIAccountDetails BlEMIAccountDetails(BlEMICollection objbl)
        {

            DalEMICollection objDal = new DalEMICollection();
            DataSet dt = objDal.DalCommonValidatewithdataset(objbl);
            objDal = null;
            return BlEMICollectionFormats.ConvertEMIAccountDetails(dt);

        }
        public UpdateEMICollection BlUpdateEMICollection(BlEMICollection objbl)
        {

            DalEMICollection objDal = new DalEMICollection();
            DataTable dt = objDal.DalCustomerTransactionUpdate(objbl);
            objDal = null;
            return BlEMICollectionFormats.ConvertUpdateEMICollection(dt);

        }
        public FinancePlanTypeDetails BlFinancePlanTypeDetails(BlEMICollection objbl)
        {

            DalEMICollection objDal = new DalEMICollection();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlEMICollectionFormats.ConvertFinancePlanTypeDetails(dt);

        }
        #endregion

    }
}
