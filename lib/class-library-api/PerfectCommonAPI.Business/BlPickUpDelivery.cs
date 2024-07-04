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
  public class BlPickUpDelivery:IPickUpDelivery
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
        private string _FK_Area { get; set; }
        private string _FK_Product { get; set; }
        private string _CusName { get; set; }
        private string _CusMobile { get; set; }
        private string _TicketNo { get; set; }
        private string _Status { get; set; }
        private string _ID_ProductDelivery { get; set; }
        private string _ID_ProductDeliveryFollowUp { get; set; }
        private string _FK_ProductDeliveryAssign { get; set; }
        private string _PdfDeliveryDate { get; set; }
        private string _PdfDeliveryTime { get; set; }
        private string _EmployeeNotes { get; set; }
        private string _CustomerNotes { get; set; }
        private string _DeliveryCharge { get; set; }
        private string _NetAmount { get; set; }
        private string _Action { get; set; }
        private string _FK_Reason { get; set; }
        private string _ID_NextAction { get; set; }
        private string _StandByAmount { get; set; }
        private string _FK_BillType { get; set; }
        private string _productReplace { get; set; }
        private string _PaymentDetail { get; set; }       
        private string _LocLatitude { get; set; }
        private string _LocLongitude { get; set; }
        private string _Address { get; set; }
        private string _DeliveryComplaints { get; set; }
        private string _FK_Category { get; set; }
        private string _TransMode { get; set; }
        private string _ID_User { get; set; }
        private string _ID_TokenUser { get; set; }
        #endregion
        #region Constructor
        public BlPickUpDelivery()
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
            _FK_Area = string.Empty;
            _FK_Product = string.Empty;
            _CusName = string.Empty;
            _CusMobile = string.Empty;
            _TicketNo = string.Empty;
            _Status = string.Empty;
            _ID_ProductDelivery = string.Empty;           
            _ID_ProductDeliveryFollowUp = string.Empty;
            _FK_ProductDeliveryAssign = string.Empty;
            _PdfDeliveryDate = string.Empty;
            _PdfDeliveryTime = string.Empty;
            _EmployeeNotes = string.Empty;
            _CustomerNotes = string.Empty;
            _DeliveryCharge = string.Empty;
            _NetAmount = string.Empty;
            _Action = string.Empty;
            _FK_Reason = string.Empty;
            _ID_NextAction = string.Empty;
            _StandByAmount = string.Empty;
            _FK_BillType = string.Empty;
            _productReplace = string.Empty;
            _PaymentDetail = string.Empty;      
            _LocLatitude = string.Empty;
            _LocLongitude = string.Empty;
            _Address = string.Empty;
            _DeliveryComplaints = string.Empty;
            _FK_Category = string.Empty;
            _TransMode = string.Empty;
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
        public string productReplace { get; set; }
        public string PaymentDetail { get; set; }
        public string DeliveryComplaints { get; set; }
        public string TransMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
            set { _TransMode = value; }
        }
        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
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
        public string FK_Area
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Area); }
            set { _FK_Area = value; }
        }
        public string FK_Product
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Product); }
            set { _FK_Product = value; }
        }
        public string CusName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusName); }
            set { _CusName = value; }
        }

        public string CusMobile
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusMobile); }
            set { _CusMobile = value; }
        }
        public string TicketNo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TicketNo); }
            set { _TicketNo = value; }
        }
        public string Status
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Status); }
            set { _Status = value; }
        }
        public string ID_ProductDelivery
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ProductDelivery); }
            set { _ID_ProductDelivery = value; }
        }

        public string ID_ProductDeliveryFollowUp
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ProductDeliveryFollowUp); }
            set { _ID_ProductDeliveryFollowUp = value; }
        }
        public string FK_ProductDeliveryAssign
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ProductDeliveryAssign); }
            set { _FK_ProductDeliveryAssign = value; }
        }
        public string PdfDeliveryDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PdfDeliveryDate); }
            set { _PdfDeliveryDate = value; }
        }
        public string EmployeeNotes
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EmployeeNotes); }
            set { _EmployeeNotes = value; }
        }
        public string PdfDeliveryTime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PdfDeliveryTime); }
            set { _PdfDeliveryTime = value; }
        }
        public string CustomerNotes
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerNotes); }
            set { _CustomerNotes = value; }
        }
        public string DeliveryCharge
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DeliveryCharge); }
            set { _DeliveryCharge = value; }
        }
        public string NetAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NetAmount); }
            set { _NetAmount = value; }
        }
        public string Action
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Action); }
            set { _Action = value; }
        }
        public string FK_Reason
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Reason); }
            set { _FK_Reason = value; }
        }
        public string ID_NextAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_NextAction); }
            set { _ID_NextAction = value; }
        }
        public string StandByAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_StandByAmount); }
            set { _StandByAmount = value; }
        }
        public string FK_BillType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BillType); }
            set { _FK_BillType = value; }
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
        public string FK_Category
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Category); }
            set { _FK_Category = value; }
        }
        #endregion
        #region Data Access Function
        public PickupandDeliveryCount BlPickupandDeliveryCount(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertPickupandDeliveryCount(dt);

        }
        public PickUpDeliveryDetails BlPickUpDeliveryDetails(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertPickUpDeliveryDetails(dt);

        }
        public UpdateDeliverStatusDetails BlUpdateDeliverStatusDetails(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertUpdateDeliverStatusDetails(dt);

        }
        public PickUPProductInformationDetails BlPickUPProductInformationDetails(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertPickUPProductInformationDetails(dt);

        }
        public ProductDetailsList BlProductDetailsList(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertProductDetails(dt);

        }
        public StandByProductDetails BlStandByProductDetails(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertStandByProductDetails(dt);

        }
        public UpdatePickUpAndDelivery BlUpdatePickUpAndDelivery(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalProductDeliveryFollowUpUpdate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertUpdatePickUpAndDelivery(dt);

        }
        public BillType BlBillType(BlPickUpDelivery objbl)
        {

            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertBillType(dt);
        }
        public DeliveryProductInformationDetails BlDeliveryProductInformationDetails(BlPickUpDelivery objbl)
        {
            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertDeliveryProductInformationDetails(dt);
        }
        public ProductComplaintList BlProductComplaintList(BlPickUpDelivery objbl)
        {
            DalPickUpDelivery objDal = new DalPickUpDelivery();
            DataTable dt = objDal.DalProductComplaintList(objbl);
            objDal = null;
            return BlPickUpDeliveryFormat.ConvertProductCompalintList(dt);
        }
        #endregion
    }
}
