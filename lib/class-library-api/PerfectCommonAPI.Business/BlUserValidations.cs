using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Interface;
using System.Data;
using PerfectWebERPAPI.DataAccess;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;

namespace PerfectWebERPAPI.Business
{
    public  class BlUserValidations:IUserValidations
    {
        public static string ModulesAndFeatures="";
        #region Variable
        private string _ReqMode { get; set; }
        private string _TransDate { get; set; }
        private string _FK_Company { get; set; }
        private string _Token { get; set; }
        private string _SubMode { get; set; }
        private string _BankKey { get; set; }
        private string _MobileNumber { get; set; }  
        private string _OTP { get; set; }
        private string _MPIN { get; set; }
        private string _ID_Master { get; set; }
        private string _OldMPIN { get; set; }
        private string _BankHeader { get; set; }
        private string _FK_Employee { get; set; }
        private string _Name { get; set; }
        private string _Email { get; set; }
        private string _Address { get; set; }
        private string _ID_LeadFrom { get; set; }
        private string _ID_Category { get; set; }
        private string _ID_BranchType { get; set; }
        private string _ID_Department { get; set; }
        private string _ID_ReportSettings { get; set; }
        private string _FromDate { get; set; }
        private string _Todate { get; set; }
        private string _ID_LeadGenerateProduct { get; set; }
        private string _PrductOnly { get; set; }
        private string _TrnsDate { get; set; }
        private string _ID_RiskType { get; set; }
        private string _CustomerNote { get; set; }
        private string _EmployeeNote { get; set; }
        private string _Id_Status { get; set; }
        private string _CusMensDate { get; set; }
        private string _LocLatitude { get; set; }
        private string _LocLongitude { get; set; }
        private byte[] _LocationLandMark1 { get; set; }
        private byte[] _LocationLandMark2 { get; set; }
        private string _ID_FollowUpType { get; set; }
        private string _Pincode { get; set; }
        private string _FK_Country { get; set; }
        private string _FK_States { get; set; }
        private string _FK_District { get; set; }
        private string _FK_ToEmployee { get; set; }
        private string _FK_Departement { get; set; }
        private string _FK_Priority { get; set; }
        private string _FollowupDate { get; set; }
        private string _FK_ActionType { get; set; }
        private string _FK_Action { get; set; }
        private string _FK_Product { get; set; }
        private string _FK_Category { get; set; }
        private string _NextActionDate { get; set; }
        private string _ID_LeadGenerate { get; set; }
        public string _Remark { get; set; }
        public string _ID_ActionType { get; set; }
        public string _IsOnline { get; set; }      
     
        public string _LocationName { get; set; }

        public string _UserAction { get; set; }
        public string _LgLeadDate { get; set; }
        public string _LgCollectedBy { get; set; }
        public string _FK_LeadFrom { get; set; }
        public string _FK_LeadBy { get; set; }
        public string _FK_Customer { get; set; }
        public string _FK_CustomerOthers { get; set; }        
        public string _LgCusName { get; set; }
        public string _LgCusAddress { get; set; }
        public string _LgCusAddress2 { get; set; }
        public string _LgCusMobile { get; set; }
        public string _LgCusEmail { get; set; }
        public string _CusCompany { get; set; }
        public string _CusPhone { get; set; }
        public string _FK_MediaMaster { get; set; }
        public string _FK_State { get; set; }
        public string _FK_Post { get; set; }
        public string _FK_Area { get; set; }
        public string _ID_Product { get; set; }
        public string _ProdName { get; set; }
        public string _ProjectName { get; set; }
        public string _LgpPQuantity { get; set; }
        public string _LgpDescription { get; set; }
        public string _ActStatus { get; set; }
        public string _FK_NetAction { get; set; }
        public string _BranchID { get; set; }
        public string _BranchTypeID { get; set; }
        public string _AssignEmp { get; set; }
        public string _LocationAddress { get; set; }
        public string _selectProcedureName { get; set; }
        //public List<ProdProjDTL> ProductDetails { get; set; }
       
        public Int64 _PreviousID { get; set; }
        public string _FK_BranchCodeUser { get; set; }
        public string _ID_Branch { get; set; }
        public string _GroupId { get; set; }
        public string _ReportMode { get; set; }
        public string _ID_NotificationDetails { get; set; }
        public string _EntrBy { get; set; }
        public string _CusPerson { get; set; }
        public string _LgActMode { get; set; }
        public  string _ID_FollowUpBy { get; set; }
        public byte[] _DocumentImage { get; set; }
        public byte[] _Attachment{ get; set; }
        public string _Doc_Subject { get; set; }
        public string _Doc_Description { get; set; }
        public string _ID_LeadDocumentDetails { get; set; }
        public string _DocImageFormat { get; set; }

        public string _TransMode { get; set; }
        public string _LgCusNameTitle { get; set; }
        public string _CusMobileAlternate { get; set; }
        public string _FK_LeadByName { get; set; }
        public string _FK_SubMedia { get; set; }
        public string _LastID { get; set; }
        public string _FK_Reason { get; set; }
        public string _FK_User { get; set; }
        public string _LgFollowUpTime { get; set; }
        public string _LgFollowUpStatus { get; set; }
        public string _LgFollowupDuration { get; set; }
        public string _FollowUpAction { get; set; }
        string _FollowUpType { get; set; }
        string _LeadNo { get; set; }
        string _Criteria { get; set; }
        string _Status { get; set; }
        string _FK_CollectedBy { get; set; }
        string _Category { get; set; }
        public string _BranchCode { get; set; }
        public string _ID_TodoListLeadDetails { get; set; }
        public string _CompanyCode { get; set; }
        public string _ID_CustomerServiceRegister { get; set; }
        public string _CSRChannelID { get; set; }
        public string _CSRPriority { get; set; }
        public string _CSRCurrentStatus { get; set; }
        public string _FK_Branch { get; set; }
        public string _CSRPCategory { get; set; }
        public string _FK_OtherCompany { get; set; }
        public string _FK_ComplaintList { get; set; }
        public string _FK_ServiceList { get; set; }
        public string _CSRChannelSubID { get; set; }
        public string _AttendedBy { get; set; }
        public string _CSRTickno { get; set; }
        public string _CusName { get; set; }
        public string _CusMobile { get; set; }
        public string _CusAddress { get; set; }
        public string _CSRContactNo { get; set; }
        public string _CSRLandmark { get; set; }
        public string _CSRServiceFromDate { get; set; }
        public string _CSRServiceToDate { get; set; }
        public string _CSRServicefromtime { get; set; }
        public string _CSRServicetotime { get; set; }
        public string _CSRODescription { get; set; }
        public string _TicketDate { get; set; }
        public string _FK_ComplaintType { get; set; }
        public string _SortOrder { get; set; }
        public string _DueDays { get; set; }

        public string _LgpExpectDate { get; set; }
        public string _FK_Customerserviceregister { get; set; }
        public string _Visitdate { get; set; }
        public string _Visittime { get; set; }
        public string _EmployeeType { get; set; }
        public string _FK_AttendedBy { get; set; }
        public string _TicketTime { get; set; }
        public string _Address1 { get; set; }
        public string _Address2 { get; set; }
        public string _CaAssignedDate { get; set; }
        public string _CaDescription { get; set; }
        public string _ID_CustomerAssignment { get; set; }
        public string _ID_CustomerWiseProductDetails { get; set; }
        public string _CustomerNotes { get; set; }
        public string _StartingDat { get; set; }
        public string _ServiceAmount { get; set; }
        public string _ProductAmount { get; set; }
        public string _NetAmount { get; set; }
        public string _TotalAmount { get; set; }
        public string _ReplaceAmount { get; set; }
        public string _DiscountAmount { get; set; }
        public string _FK_NextAction { get; set; }        
        public string _FK_BillType { get; set; }
        public string _FK_NextActionLead { get; set; }
        public string _FK_ActionTypeLead { get; set; }
        public string _FK_EmployeeLead { get; set; }
        public string _NextActionDateLead { get; set; }
        public string _StartingDate { get; set; }
        public string _LocationEnteredDate { get; set; }
        public string _LocationEnteredTime { get; set; }

        public string _ID_ImageLocation { get; set; }
        public string _FK_Master { get; set; }
        public string _ID_EmployeeAttanceMarking { get; set; }
        public string _ID_EmployeeLocation { get; set; }
        public string _ChargePercentage { get; set; }
        public string _Reciever { get; set; }
        public string _Title { get; set; }
        public string _Message { get; set; }
        public string _ID_User { get; set; }
        public string _FK_Designation { get; set; }
        public string _ToEmail { get; set; }
        public string _Subject { get; set; }
        public string _Body { get; set; }
        public string _LgpMRP { get; set; }
        public string _LgpSalesPrice { get; set; }
       public string _Offer { get; set; }
      public string _ForAllProduct { get; set; }
        public string _FK_UserGroup { get; set; }
        public string _Module { get; set; }
        public string _AuthID { get; set; }
        public string _Reason { get; set; }      
        private string _PageIndex { get; set; }
        private string _PageSize { get; set; }
        private string _Critrea1 { get; set; }
        private string _Critrea2 { get; set; }
        private string _Critrea3 { get; set; }
        private string _Critrea4 { get; set; }
        private string _ID { get; set; }
        private string _Critrea5 { get; set; }
        private string _Critrea6 { get; set; }
        private string _SkipPrev { get; set; }
        private string _FK_AuthorizationData { get; set; }
        private string _VoiceData { get; set; }
        private string _VoiceDataName { get; set; }
        private string _FK_CustomerserviceregisterProductDetails { get; set; }
        private string _OsType { get; set; }
        private string _versionNo { get; set; }
        private string _ID_CommonIntimation { get; set; }
        private string _Branch { get; set; }
        private string _Channel { get; set; }
        private string _SheduledType { get; set; }
        private string _DlId { get; set; }
        private string _Unicode { get; set; }
        //private string _Attachment { get; set; }
        private string _Date { get; set; }
        private string _SheduledTime { get; set; }
        private string _SheduledDate { get; set; }
        private string _FK_CommonIntimation { get; set; }
        private string _FK_Machine { get; set; }
        private string _ConfigCode { get; set; }
        private string _URLKey { get; set; }
        private string _FK_SubCategory { get; set; }
        private string _FK_Brand { get; set; }
        private string _FK_LeadThrough { get; set; }
        private string _ToDate { get; set; }
        private string _ProdType { get; set; }
        private string _Collectedby_ID { get; set; }
        private string _Area_ID { get; set; }
        private string _SearchBy { get; set; }
        private string _SearchBydetails { get; set; }
        private string _LeadDetails { get; set; }
        private string _GridData { get; set; }
        private string _EnteredDate { get; set; }
        private string _LandNumber { get; set; }
        private string _IdFliter { get; set; }
        private string  _ID_TokenUser { get; set; }

        #endregion


        #region Constructor
        public BlUserValidations()
        {
            Initialize();
        }
        #endregion
        #region Initialize
        public void Initialize()
        {
            _OsType = string.Empty;
            _IdFliter = string.Empty;
            _TransDate = string.Empty;
            _ReqMode = string.Empty;
            _FK_Company = string.Empty;
            _Token = string.Empty;
            _SubMode = string.Empty;
            _BankKey = string.Empty;          
            _MobileNumber = string.Empty;
            _OTP = string.Empty;
            _MPIN = string.Empty;
            _OldMPIN = string.Empty;
            _BankHeader = string.Empty;
            _FK_Employee = string.Empty;
            _Name = string.Empty;
            _Email = string.Empty;
            _Address = string.Empty;
            _ID_LeadFrom = string.Empty;
            _ID_Category = string.Empty;
            _ID_BranchType = string.Empty;
            _ID_Department = string.Empty;
            _ID_ReportSettings = string.Empty;
            _FromDate =  string.Empty;
            _Todate =   string.Empty;
            _ID_LeadGenerateProduct = string.Empty;
            _PrductOnly = string.Empty;
            _TrnsDate = string.Empty;
            _ID_RiskType = string.Empty;
            _CustomerNote = string.Empty;
            _EmployeeNote = string.Empty;
            _Id_Status = string.Empty;
            _CusMensDate = string.Empty;
            _LocLatitude = string.Empty;
            _LocLongitude = string.Empty;
            _LocationLandMark1 = null;
            _LocationLandMark2 = null;
            _ID_FollowUpType = string.Empty;
            _Pincode = string.Empty;
            _FK_Country = string.Empty;
            _FK_District = string.Empty;
            _FK_States = string.Empty;
            _FK_ToEmployee = string.Empty;
            _FK_Departement = string.Empty;
            _FK_Priority = string.Empty;
            _FollowupDate = string.Empty;
            _FK_ActionType = string.Empty;
            _FK_Action = string.Empty;
            _FK_Product = string.Empty;
            _FK_Category = string.Empty;
            _NextActionDate = string.Empty;
            _ID_LeadGenerate = string.Empty;
            _Remark = string.Empty;
            _ID_ActionType = string.Empty;
            _IsOnline = string.Empty;          
            
            _LocationName = string.Empty;

            _UserAction = string.Empty;
            _LgLeadDate = string.Empty;
            _LgCollectedBy = string.Empty;
            _FK_LeadFrom = string.Empty;
            _FK_LeadBy = string.Empty;
            _FK_Customer = string.Empty;
            _FK_CustomerOthers = string.Empty;
            _LgCusName = string.Empty;
            _LgCusAddress = string.Empty;
            _LgCusAddress2 = string.Empty;
            _LgCusMobile = string.Empty;
            _LgCusEmail = string.Empty;
            _CusCompany = string.Empty;
            _CusPhone = string.Empty;
            _FK_MediaMaster = string.Empty;
            _FK_State = string.Empty;
            _FK_Area = string.Empty;
            _FK_Post = string.Empty;
            _ID_Product = string.Empty;
            _ProdName = string.Empty;
            _ProjectName = string.Empty;
            _LgpPQuantity = string.Empty;
            _LgpDescription = string.Empty;
            _ActStatus = string.Empty;
            
            _BranchID = string.Empty;
            _BranchTypeID = string.Empty;
            _AssignEmp = string.Empty;
            _LocationAddress = string.Empty;
            _selectProcedureName = string.Empty;
             SubProductDetails = string.Empty;
            _PreviousID =0;
            _FK_BranchCodeUser = string.Empty;
             _ID_Branch = string.Empty;
            _GroupId = string.Empty;
            _ReportMode = string.Empty;
            XMLdata = string.Empty;
            _ID_NotificationDetails = string.Empty;
            _EntrBy = string.Empty;
            _CusPerson = string.Empty;
            _LgActMode = string.Empty;
            _ID_FollowUpBy = string.Empty;
            _DocumentImage = null;
            _Attachment = null;
            _Doc_Description = string.Empty;
            _Doc_Subject = string.Empty;
            _ID_LeadDocumentDetails = string.Empty;
            _DocImageFormat = string.Empty;
            _TransMode = string.Empty;
            _LgCusNameTitle = string.Empty;
            _CusMobileAlternate = string.Empty;
            _FK_LeadByName = string.Empty;
            _FK_SubMedia = string.Empty;
            _LastID = string.Empty;
            _LgFollowUpTime = string.Empty;
            _LgFollowUpStatus = string.Empty;
            _LgFollowupDuration = string.Empty;
            _BranchCode = string.Empty;
            _ID_TodoListLeadDetails = string.Empty;
            _CompanyCode = string.Empty;
            _ID_CustomerServiceRegister = string.Empty;
            _CSRChannelID = string.Empty;
            _CSRPriority = string.Empty;
            _CSRCurrentStatus = string.Empty;
            _FK_Branch = string.Empty;
            _CSRPCategory = string.Empty;
            _FK_OtherCompany = string.Empty;
            _FK_ComplaintList = string.Empty;
            _FK_ServiceList = string.Empty;
            _CSRChannelSubID = string.Empty;
            _AttendedBy = string.Empty;
            _CSRTickno = string.Empty;
            _CusName = string.Empty;
            _CusMobile = string.Empty;
            _CusAddress = string.Empty;
            _CSRContactNo = string.Empty;
            _CSRLandmark = string.Empty;
            _CSRServiceFromDate = string.Empty;
            _CSRServiceToDate = string.Empty;
            _CSRServicefromtime = string.Empty;
            _CSRServicetotime = string.Empty;
            _CSRODescription = string.Empty;
            _TicketDate = string.Empty;
            _FK_ComplaintType = string.Empty;
            _SortOrder = string.Empty;
            _DueDays = string.Empty;
            _LgpExpectDate = string.Empty;
            _FK_Customerserviceregister = string.Empty;
            Assignees = string.Empty;
            _Visitdate  = string.Empty;
           _Visittime = string.Empty;
            _EmployeeType = string.Empty;
            _FK_AttendedBy = string.Empty;
             _TicketTime = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;

            _CaAssignedDate = string.Empty;
            _CaDescription = string.Empty;
            _ID_CustomerAssignment = string.Empty;
            _ID_CustomerWiseProductDetails = string.Empty;
            _CustomerNotes = string.Empty;
            _StartingDat = string.Empty;
            _ServiceAmount = string.Empty;
            _ProductAmount = string.Empty;
            _NetAmount = string.Empty;
            _TotalAmount = string.Empty;
            _ReplaceAmount = string.Empty;
            _DiscountAmount = string.Empty;
            _FK_NextAction = string.Empty;
            
            _FK_BillType = string.Empty;
            _FK_NextActionLead = string.Empty;
            _FK_ActionTypeLead = string.Empty;
            _FK_EmployeeLead = string.Empty;
            _NextActionDateLead = string.Empty;
            _StartingDate = string.Empty;
            _LocationEnteredDate = string.Empty;
            _LocationEnteredTime = string.Empty;
            _ID_ImageLocation = string.Empty;
            _FK_Master = string.Empty;
            _ID_EmployeeAttanceMarking = string.Empty;
            _ID_EmployeeLocation = string.Empty;
            _ChargePercentage = string.Empty;
            _Reciever = string.Empty;
            _Title = string.Empty;
            _Message = string.Empty;
            _ID_User = string.Empty;
            _FK_Designation = string.Empty;
            _ToEmail = string.Empty;
            _Subject = string.Empty;
            _Body = string.Empty;
            _LgpMRP = string.Empty;
            _Offer = string.Empty;
            _ForAllProduct = string.Empty;
            _FK_UserGroup = string.Empty;
            _Module = string.Empty;
            _AuthID = string.Empty;
            _Reason = string.Empty;
            _PageIndex = string.Empty;
            _PageSize = string.Empty;
            _Critrea1 = string.Empty;
            _Critrea2 = string.Empty;
            _Critrea3 = string.Empty;
            _Critrea4 = string.Empty;
            _ID = string.Empty;
            _Critrea5 = string.Empty;
            _Critrea6 = string.Empty;
            _SkipPrev = string.Empty;
            _FK_AuthorizationData = string.Empty;
            _VoiceData = string.Empty;
            _VoiceDataName = string.Empty;
            _FK_CustomerserviceregisterProductDetails = string.Empty;
            _VoiceDataName = string.Empty;
            _versionNo = string.Empty;
            _ID_CommonIntimation= string.Empty;
             _Branch = string.Empty;
            _Channel = string.Empty;
           _SheduledType= string.Empty;
           _DlId = string.Empty;
           _Unicode = string.Empty;
            //_Attachment = string.Empty;
            _Date = string.Empty;
            _SheduledTime = string.Empty;
            _SheduledDate = string.Empty;
            _FK_CommonIntimation = string.Empty;
            _FK_Machine = string.Empty;
            _ConfigCode = string.Empty;
            _URLKey = string.Empty;
            _FK_Brand = string.Empty;
            _FK_SubCategory = string.Empty;
            _FK_LeadThrough= string.Empty;
            _ToDate= string.Empty;
            _Collectedby_ID = string.Empty;
            _FK_NetAction = string.Empty;
            _SearchBy = string.Empty;
            _GridData = string.Empty;
            _ID_Master = string.Empty;
            _EnteredDate= string.Empty;
            _ID_TokenUser = string.Empty;
        }
        #endregion

        #region Getters And Setters
        public string ID_TokenUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_TokenUser); }
            set { _ID_TokenUser = value; }
        }
        public string IdFliter
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_IdFliter); }
            set { _IdFliter = value; }
        }
        public string LandNumber
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LandNumber); }
            set { _LandNumber = value; }
        }
        public string FK_Brand
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Brand); }
            set { _FK_Brand = value; }
        }
        public string FK_SubCategory
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_SubCategory); }
            set { _FK_SubCategory = value; }
        }
        public string URLKey
        {
            get { return _URLKey; }
            set { _URLKey = value; }
        }
        public string ConfigCode
        {
            get { return _ConfigCode; }
            set { _ConfigCode = value; }
        }
        public string ID_CommonIntimation
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CommonIntimation); }
            set { _ID_CommonIntimation = value; }
        }
        public string FK_CommonIntimation
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_CommonIntimation); }
            set { _FK_CommonIntimation = value; }
        }

        public string Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Branch); }
            set { _Branch = value; }
        }
        public string Channel
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Channel); }
            set { _Channel = value; }
        }

        public string SheduledType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SheduledType); }
            set { _SheduledType = value; }
        }

        public string DlId
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DlId); }
            set { _DlId = value; }
        }
        //public string Attachment
        //{
        //    get { return BlDecryptFormat.DecryptDataForAuthCode(_Attachment); }
        //    set { _Attachment = value; }
        //}
        public string Unicode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Unicode); }
            set { _Unicode = value; }
        }
        public string Date
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Date); }
            set { _Date = value; }
        }
        public string SheduledTime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SheduledTime); }
            set { _SheduledTime = value; }
        }
        public string SheduledDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SheduledDate); }
            set { _SheduledDate = value; }
        }

        public string versionNo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_versionNo); }
            set { _versionNo = value; }
        }
        public string OsType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_OsType); }
            set { _OsType = value; }
        }
        public string FK_CustomerserviceregisterProductDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_CustomerserviceregisterProductDetails); }
            set { _FK_CustomerserviceregisterProductDetails = value; }
        }
        public string StartingDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_StartingDate); }
            set { _StartingDate = value; }
        }
        public string CustomerNotes
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerNotes); }
            set { _CustomerNotes = value; }
        }
        public string StartingDat
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_StartingDat); }
            set { _StartingDat = value; }
        }
        public string ServiceAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ServiceAmount); }
            set { _ServiceAmount = value; }
        }
        public string ProductAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProductAmount); }
            set { _ProductAmount = value; }
        }
        public string NetAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NetAmount); }
            set { _NetAmount = value; }
        }
        public string TotalAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TotalAmount); }
            set { _TotalAmount = value; }
        }
   
    
        public string FK_BillType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BillType); }
            set { _FK_BillType = value; }
        }
        public string FK_NextAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NextAction); }
            set { _FK_NextAction = value; }
        }
        public string DiscountAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DiscountAmount); }
            set { _DiscountAmount = value; }
        }
        public string FK_NextActionLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NextActionLead); }
            set { _FK_NextActionLead = value; }
        }
        public string NextActionDateLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NextActionDateLead); }
            set { _NextActionDateLead = value; }
        }
        public string FK_EmployeeLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_EmployeeLead); }
            set { _FK_EmployeeLead = value; }
        }
        public string FK_ActionTypeLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ActionTypeLead); }
            set { _FK_ActionTypeLead = value; }
        }
        public string ReplaceAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NextActionLead); }
            set { _FK_NextActionLead = value; }
        }
        public string ID_Master
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_Master); }
            set { _ID_Master = value; }
        }
        public string ID_CustomerWiseProductDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerWiseProductDetails); }
            set { _ID_CustomerWiseProductDetails = value; }
        }
        public string Address1
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Address1); }
            set { _Address1 = value; }
        }
        public string Address2
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Address2); }
            set { _Address2 = value; }
        }
        public string TicketTime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TicketTime); }
            set { _TicketTime = value; }
        }
        public string XMLdata { get; set; }
        public string Assignees { get; set; }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
        }
        public string TransDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransDate); }
            set { _TransDate = value; }
        }
        public string FK_AttendedBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_AttendedBy); }
            set { _FK_AttendedBy = value; }
        }

        public string Token
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Token); }
            set { _Token = value; }
        }
        public string SubProductDetails { get; set; }
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
        public string BankHeader
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankHeader); }
            set { _BankHeader = value; }
        }
        public string MobileNumber
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_MobileNumber); }
            // get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }
       
        public string OTP
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_OTP); }
            set { _OTP = value; }
        }
        public string MPIN
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_MPIN); }
            set { _MPIN = value; }
        }
        public string OldMPIN
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_OldMPIN); }
            set { _OldMPIN = value; }
        }
        public string FK_Employee
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Employee); }
            set { _FK_Employee = value; }
        }
        public string Name
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Name); }
            set { _Name = value; }
        }
        public string Email
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Email); }
            set { _Email = value; }
        }
        public string Address
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Address); }
            set { _Address = value; }
        }
        public string ID_LeadFrom
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_LeadFrom); }
            set { _ID_LeadFrom = value; }
        }
        public string ID_Category
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_Category); }
            set { _ID_Category = value; }
        }
        public string ID_Department
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_Department); }
            set { _ID_Department = value; }
        }
        public string ID_BranchType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_BranchType); }
            set { _ID_BranchType = value; }
        }
        public string ID_ReportSettings
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ReportSettings); }
            set { _ID_ReportSettings = value; }
        }
        public string FromDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FromDate); }
            set { _FromDate = value; }
        }
        public string Todate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Todate); }
            set { _Todate = value; }
        }
        public string ID_LeadGenerateProduct
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_LeadGenerateProduct); }
            set { _ID_LeadGenerateProduct = value; }
        }
        public string PrductOnly
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PrductOnly); }
            set { _PrductOnly = value; }
        }
        public string TrnsDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TrnsDate); }
            set { _TrnsDate = value; }
        }
        public string CustomerNote
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerNote); }
            set { _CustomerNote = value; }
        }
        public string EmployeeNote
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EmployeeNote); }
            set { _EmployeeNote = value; }
        }
        public string Id_Status
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Id_Status); }
            set { _Id_Status = value; }
        }
        public string CusMensDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusMensDate); }
            set { _CustomerNote = value; }
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
        public byte[] LocationLandMark1
        {
            get { return _LocationLandMark1; }
            set { _LocationLandMark1 = value; }
        }
        public byte[] LocationLandMark2
        {
            get { return _LocationLandMark2; }
            set { _LocationLandMark2 = value; }
        }
        public string ID_FollowUpType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_FollowUpType); }
            set { _ID_FollowUpType = value; }
        }
        public string ID_RiskType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_RiskType); }
            set { _ID_RiskType = value; }
        }
        public string Pincode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Pincode); }
            set { _Pincode = value; }
        }
        public string FK_Country
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Country); }
            set { _FK_Country = value; }
        }
        public string FK_District
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_District); }
            set { _FK_District = value; }
        }
        public string FK_States
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_States); }
            set { _FK_States = value; }
        }
        public string FK_ToEmployee
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ToEmployee); }
            set { _FK_ToEmployee = value; }
        }
        public string FK_Departement
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Departement); }
            set { _FK_Departement = value; }
        }
        public string FK_Priority
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Priority); }
            set { _FK_Priority = value; }
        }
        public string FollowupDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FollowupDate); }
            set { _FollowupDate = value; }
        }
        public string FK_ActionType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ActionType); }
            set { _FK_ActionType = value; }
        }
        public string FK_Action
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Action); }
            set { _FK_Action = value; }
        }
        public string FK_Product
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Product); }
            set { _FK_Product = value; }
        }
        public string FK_Category
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Category); }
            set { _FK_Category = value; }
        }
        public string NextActionDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NextActionDate); }
            set { _NextActionDate = value; }
        }
        public string ID_LeadGenerate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_LeadGenerate); }
            set { _ID_LeadGenerate = value; }
        }
        public string Remark
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Remark); }
            set { _Remark = value; }
        }
        public string ID_ActionType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ActionType); }
            set { _ID_ActionType = value; }
        }
        public string IsOnline
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_IsOnline); }
            set { _IsOnline = value; }
        }
        public string LocationName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocationName); }
            set { _LocationName = value; }
        }
        public string UserAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_UserAction); }
            set { _UserAction = value; }
        }
        public string LgLeadDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgLeadDate); }
            set { _LgLeadDate = value; }
        }
        public string LgCollectedBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCollectedBy); }
            set { _LgCollectedBy = value; }
        }
        public string FK_LeadFrom
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_LeadFrom); }
            set { _FK_LeadFrom = value; }
        }
        public string FK_LeadBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_LeadBy); }
            set { _FK_LeadBy = value; }
        }
        public string FK_Customer
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Customer); }
            set { _FK_Customer = value; }
        }
        public string FK_CustomerOthers
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_CustomerOthers); }
            set { _FK_CustomerOthers = value; }
        }
        public string LgCusName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCusName); }
            set { _LgCusName = value; }
        }

        public string LgCusAddress
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCusAddress); }
            set { _LgCusAddress = value; }
        }
        public string LgCusAddress2
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCusAddress2); }
            set { _LgCusAddress2 = value; }
        }
        public string LgCusMobile
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCusMobile); }
            set { _LgCusMobile = value; }
        }
        public string LgCusEmail
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCusEmail); }
            set { _LgCusEmail = value; }
        }
        public string CusCompany
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusCompany); }
            set { _CusCompany = value; }
        }
        public string CusPhone
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusPhone); }
            set { _CusPhone = value; }
        }
        public string FK_MediaMaster
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_MediaMaster); }
            set { _FK_MediaMaster = value; }
        }

        public string FK_State
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_State); }
            set { _FK_State = value; }
        }
        public string FK_Area
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Area);  }
            set { _FK_Area = value; }
        }
        public string FK_Post
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Post); }
            set { _FK_Post = value; }
        }
        public string ID_Product
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_Product); }
            set { _ID_Product = value; }
        }
        public string LgpDescription
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgpDescription); }
            set { _LgpDescription = value; }
        }
        public string ProjectName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProjectName); }
            set { _ProjectName = value; }
        }
        public string LgpPQuantity
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgpPQuantity); }
            set { _LgpPQuantity = value; }
        }

        public string ActStatus
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ActStatus); }
            set { _ActStatus = value; }
        }
        public string BranchID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BranchID); }
            set { _BranchID = value; }
        }
        public string BranchTypeID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BranchTypeID); }
            set { _BranchTypeID = value; }
        }
        public string AssignEmp
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_AssignEmp); }
            set { _AssignEmp = value; }
        }
        public string LocationAddress
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocationAddress); }
            set { _LocationAddress = value; }
        }
        public string selectProcedureName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_selectProcedureName); }
            set { _selectProcedureName = value; }
        }

        public string ProdName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProdName); }
            set { _ProdName = value; }
        }

        public string FK_NetAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NetAction); }
            set { _FK_NetAction = value; }
        }

        public Int64 PreviousID

        {
            get { return _PreviousID; }
            set { _PreviousID = value; }
        }
        public string FK_BranchCodeUser

        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchCodeUser); }
            set { _FK_BranchCodeUser = value; }
        }
        public string ID_Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_Branch); }
            set { _ID_Branch = value; }
        }

        public string ReportMode

        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReportMode); }
            set { _ReportMode = value; }
        }
        public string GroupId

        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_GroupId); }
            set { _GroupId = value; }
        }
        public string ID_NotificationDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_NotificationDetails); }
            set { _ID_NotificationDetails = value; }
        }
        public string EntrBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EntrBy); }
            set { _EntrBy = value; }
        }
        public string CusPerson
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusPerson); }
            set { _CusPerson = value; }
        }
        public string LgActMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgActMode); }
            set { _LgActMode = value; }
        }
        public string ID_FollowUpBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_FollowUpBy); }
            set { _ID_FollowUpBy = value; }
        }
        public byte[] DocumentImage
        {
            get { return _DocumentImage; }
            set { _DocumentImage = value; }
        }
        public byte[] Attachment
        {
            get { return _Attachment; }
            set { _Attachment = value; }
        }
        public string Doc_Subject
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Doc_Subject); }
            set { _Doc_Subject = value; }
        }
        public string Doc_Description
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Doc_Description); }
            set { _Doc_Description = value; }
        }
      public string ID_LeadDocumentDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_LeadDocumentDetails); }
            set { _ID_LeadDocumentDetails = value; }
        }
        public string DocImageFormat
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DocImageFormat); }
            set { _DocImageFormat = value; }
        }
        public string TransMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
            set { _TransMode = value; }
        }
        public string LgCusNameTitle
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgCusNameTitle); }
            set { _LgCusNameTitle = value; }
        }
        public string CusMobileAlternate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusMobileAlternate); }
            set { _CusMobileAlternate = value; }
        }
        public string FK_LeadByName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_LeadByName); }
            set { _FK_LeadByName = value; }
        }
        public string FK_SubMedia
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_SubMedia); }
            set { _FK_SubMedia = value; }
        }
        public string LastID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LastID); }
            set { _LastID = value; }
        }
      
        public string FK_Reason
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Reason); }
            set { _FK_Reason = value; }
        }
        public string FK_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_User); }
            set { _FK_User = value; }
        }
        public string LgFollowUpTime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgFollowUpTime); }
            set { _LgFollowUpTime = value; }
        }
        public string LgFollowUpStatus
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgFollowUpStatus); }
            set { _LgFollowUpStatus = value; }
        }
        public string LgFollowupDuration
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgFollowupDuration); }
            set { _LgFollowupDuration = value; }
        }
        public string FollowUpAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FollowUpAction); }
            set { _FollowUpAction = value; }
        }
        public string FollowUpType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FollowUpType); }
            set { _FollowUpType = value; }
        }
        public string LeadNo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LeadNo); }
            set { _LeadNo = value; }
        }
        public string Criteria
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Criteria); }
            set { _Criteria = value; }
        }
        public string Status
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Status); }
            set { _Status = value; }
        }
        public string FK_CollectedBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_CollectedBy); }
            set { _FK_CollectedBy = value; }
        }
        public string Category
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Category); }
            set { _Category = value; }
        }
        public string BranchCode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BranchCode); }
            set { _BranchCode = value; }
        }
        public string ID_TodoListLeadDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_TodoListLeadDetails); }
            set { _ID_TodoListLeadDetails = value; }
        }
        public string CompanyCode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CompanyCode); }
            set { _CompanyCode = value; }
        }

        public string ID_CustomerServiceRegister
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerServiceRegister); }
            set { _ID_CustomerServiceRegister = value; }
        }

        public string CSRChannelID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRChannelID); }
            set { _CSRChannelID = value; }
        }
        public string CSRPriority
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRPriority); }
            set { _CSRPriority = value; }
        }

        public string CSRCurrentStatus
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRCurrentStatus); }
            set { _CSRCurrentStatus = value; }
        }

        public string FK_Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Branch); }
            set { _FK_Branch = value; }
        }

        public string CSRPCategory
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRPCategory); }
            set { _CSRPCategory = value; }
        }

        public string FK_OtherCompany
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_OtherCompany); }
            set { _FK_OtherCompany = value; }
        }

        public string FK_ComplaintList
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ComplaintList); }
            set { _FK_ComplaintList = value; }
        }

        public string FK_ServiceList
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ServiceList); }
            set { _FK_ServiceList = value; }
        }

        public string CSRChannelSubID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRChannelSubID); }
            set { _CSRChannelSubID = value; }
        }

        public string AttendedBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_AttendedBy); }
            set { _AttendedBy = value; }
        }

        public string CSRTickno
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRTickno); }
            set { _CSRTickno = value; }
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

        public string CusAddress
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CusAddress); }
            set { _CusAddress = value; }
        }

        public string CSRContactNo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRContactNo); }
            set { _CSRContactNo = value; }
        }

        public string CSRLandmark
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRLandmark); }
            set { _CSRLandmark = value; }
        }

        public string CSRServiceFromDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRServiceFromDate); }
            set { _CSRServiceFromDate = value; }
        }

        public string CSRServiceToDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRServiceToDate); }
            set { _CSRServiceToDate = value; }
        }
        public string CSRServicefromtime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRServicefromtime); }
            set { _CSRServicefromtime = value; }
        }
        public string CSRServicetotime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRServicetotime); }
            set { _CSRServicetotime = value; }
        }

        public string CSRODescription
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CSRODescription); }
            set { _CSRODescription = value; }
        }
        public string TicketDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TicketDate); }
            set { _TicketDate = value; }
        }
        public string FK_ComplaintType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ComplaintType); }
            set { _FK_ComplaintType = value; }
        }

        public string SortOrder
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SortOrder); }
            set { _SortOrder = value; }
        }
        public string DueDays
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DueDays); }
            set { _DueDays = value; }
        }
        public string LgpExpectDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgpExpectDate); }
            set { _LgpExpectDate = value; }
        }        
        public string FK_Customerserviceregister
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Customerserviceregister); }
            set { _FK_Customerserviceregister = value; }
        }
        public string Visitdate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Visitdate); }
            set { _Visitdate = value; }
        }
        public string Visittime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Visittime); }
            set { _Visittime = value; }
        }
        public string EmployeeType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EmployeeType); }
            set { _EmployeeType = value; }
        }
        public string CaAssignedDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CaAssignedDate); }
            set { _CaAssignedDate = value; }
        }
        public string CaDescription
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CaDescription); }
            set { _CaDescription = value; }
        }
        public string ID_CustomerAssignment
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerAssignment); }
            set { _ID_CustomerAssignment = value; }
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

        public string ID_ImageLocation
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ImageLocation); }
            set { _ID_ImageLocation = value; }
        }
        public string FK_Master
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Master); }
            set { _FK_Master = value; }
        }
        public string FK_Machine
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Machine); }
            set { _FK_Machine = value; }
        }
        public string ID_EmployeeAttanceMarking
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_EmployeeAttanceMarking); }
            set { _ID_EmployeeAttanceMarking = value; }
        }
        public string ID_EmployeeLocation
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_EmployeeLocation); }
            set { _ID_EmployeeLocation = value; }
        }
        public string ChargePercentage
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ChargePercentage); }
            set { _ChargePercentage = value; }
        }

        public string Reciever
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Reciever); }
            set { _Reciever = value; }
        }
        public string Title
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Title); }
            set { _Title = value; }
        }
        public string Message
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Message); }
            set { _Message = value; }
        }
        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string FK_Designation
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Designation); }
            set { _FK_Designation = value; }
        }

        public string ToEmail
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ToEmail); }
            set { _ToEmail = value; }
        }
        public string Subject
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Subject); }
            set { _Subject = value; }
        }
        public string Body
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Body); }
            set { _Body = value; }
        }
        public string LgpMRP
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgpMRP); }
            set { _LgpMRP = value; }
        }
        public string LgpSalesPrice
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LgpSalesPrice); }
            set { _LgpSalesPrice = value; }
        }
        public string Offer
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Offer); }
            set { _Offer = value; }
        }
      public string ForAllProduct
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ForAllProduct); }
            set { _ForAllProduct = value; }
        }
        public string FK_UserGroup
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_UserGroup); }
            set { _FK_UserGroup = value; }
        }
        public string Module
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Module); }
            set { _Module = value; }
        }
        public string AuthID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_AuthID); }
            set { _AuthID = value; }
        }
        public string Reason
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Reason); }
            set { _Reason = value; }
        }
        public string PageIndex
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PageIndex); }
            set { _PageIndex = value; }
        }
        public string PageSize
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PageSize); }
            set { _PageSize = value; }
        }
        public string Critrea1
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea1); }
            set { _Critrea1 = value; }
        }
        public string Critrea2
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea2); }
            set { _Critrea2 = value; }
        }
        public string Critrea3
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea3); }
            set { _Critrea3 = value; }
        }
        public string Critrea4
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea4); }
            set { _Critrea4 = value; }
        }
        public string ID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID); }
            set { _ID = value; }
        }
        public string Critrea5
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea5); }
            set { _Critrea5 = value; }
        }
        public string Critrea6
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea6); }
            set { _Critrea6 = value; }
        }
        public string SkipPrev
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SkipPrev); }
            set { _SkipPrev = value; }
        }
        public string FK_AuthorizationData
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_AuthorizationData); }
            set { _FK_AuthorizationData = value; }
        }
        public string VoiceData
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_VoiceData); }
            set { _VoiceData = value; }
        }
        public string VoiceDataName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_VoiceDataName); }
            set { _VoiceDataName = value; }
        }
        public string FK_LeadThrough
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_LeadThrough); }
            set { _FK_LeadThrough = value; }
        }
        public string ToDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ToDate); }
            set { _ToDate = value; }
        }
        public string ProdType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProdType); }
            set { _ProdType = value; }
        }
        public string GridData
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_GridData); }
            set { _GridData = value; }
        }
        public string Collectedby_ID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Collectedby_ID); }
            set { _Collectedby_ID = value; }
        }
        public string Area_ID
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Area_ID); }
            set { _Area_ID = value; }
        }
        public string SearchBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SearchBy); }
            set { _SearchBy = value; }
        }
        public string SearchBydetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SearchBydetails); }
            set { _SearchBydetails = value; }
        }
        public string LeadDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LeadDetails); }
            set { _LeadDetails = value; }
        }
        public string EnteredDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EnteredDate); }
            set { _EnteredDate = value; }
        }
        #endregion
        #region Data Access Function
        public bool LicenseChecking(string BankKey, Int64 FK_Company)
        {
            bool status = false;
            try
            {
                //DataTable dt = new DataTable();

                //List<LicenceList> LstAcinfo = GetLicense(BankKey);
                //string val = "";
                //val = GetBankNameAndTitle(BankKey, FK_Company);
                //var firstEven = LstAcinfo.FirstOrDefault(n => n.Field == val);
                //if (firstEven != null)
                //{
                //    val = firstEven.value.ToString();
                //    ModulesAndFeatures = val;
                //    int Length = val.Length;
                //    if (Length < 851)
                //    {
                //        status = false;
                //    }
                //    else
                //    {
                //        if (Convert.ToBoolean(Convert.ToInt32(val.Substring(476, 1))))
                //        {
                            status = true;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                WriteToLog("Exception||LicenseChecking::License Verification Failed||\r\n",BankKey);
                //return new RootUserLogin() { EXMessage = "License Error", StatusCode = -8 };
                status = false;
            }
            return status;
        }
        public bool LicenseUserCountChecking(string BankKey, Int64 FK_Company,string UserCode)
        {
            bool status = false;
            try
            {
                Int32 UserCount = 0, CountType = 0;
                DataTable dt = new DataTable();
                List<LicenceList> LstAcinfo = GetLicense(BankKey);
                string val = "", val2 = "" ;
                val = "LicenseCount";
                val2 = "LicenseMethod";
                var firstEven = LstAcinfo.FirstOrDefault(n => n.Field == val);
                var firstEven1 = LstAcinfo.FirstOrDefault(n => n.Field == val2);
                if (firstEven != null)
                {
                    val = firstEven.value.ToString();
                    UserCount=Convert.ToInt32(val);
                    val2 = firstEven1.value.ToString();
                    CountType = Convert.ToInt32(val2);
                    //status = GetUserCount(BankKey, UserCode, UserCount, CountType);
                    status = true;
                        


                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        public static List<LicenceList> GetLicense(string BankKey)
        {
            List<LicenceList> LicList = new List<LicenceList>();

            try
            {

                DataTable dt = new DataTable();
                DalUserValidations dalUserValidations = new DalUserValidations();
                dt = dalUserValidations.GetLicense(BankKey);
                LicList = (from DataRow dr in dt.Rows
                           select new LicenceList()
                           {
                               Field = (DecryptLicense(dr["Field"].ToString())),
                               value = (DecryptLicense(dr["value"].ToString())),

                           }).ToList();
                dalUserValidations = null;
                return LicList;
            }
            catch (Exception ex)
            {
                WriteToLog("Exception||GetLicense::License Verification Failed||\r\n",BankKey);
                return LicList;
            }


        }
        private static string DecryptLicense(string inputText)
        {
            const string _KEY = "oi0e06108823e25";//No need to change : this key is used in Licensing Appliction also
            byte[] _SALT = Encoding.ASCII.GetBytes(_KEY.Length.ToString());
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(inputText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(_KEY, _SALT);
            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        public string GetBankNameAndTitle(string BankKey, Int64 FK_Company)
        {
            DalUserValidations dalUserValidations = new DalUserValidations();
            string BankTitle = "";
            BankTitle = dalUserValidations.GetBankNameAndTitle(BankKey, FK_Company);
            //BankTitle = "Head Office Chalappuram!Head Office Chalappuram";
            return BankTitle;
        }
        private static object locker = new Object();
        public static void WriteToLog(String data,string BankKey)
        {
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
        

        public ResellerDetails BlResellerDetails(BlUserValidations objbl)
        {
         
            DalUserValidations objDal = new DalUserValidations();         
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertResellerDetails(dt);

        }

        public UserLoginDetails BlUserLoginDetails(BlUserValidations objbl)
        {

            Int64 FK_Company = 0;
            string BankKey = "";
            DalUserValidations objDal = new DalUserValidations();
            BankKey = objbl.BankKey;
            DataTable dt = objDal.DalCommonValidate(objbl);
            DataTable dtCRM = new DataTable();
            DataTable dtFUP = new DataTable();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") != "-1")
                {
                    FK_Company = Convert.ToInt64(dt.Rows[0]["FK_Company"]);
                    dtCRM = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSV", Convert.ToInt64(FK_Company), 3);
                    dtFUP = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSF", Convert.ToInt64(FK_Company), 3);
                }
            }
            objDal = null;
            return BlUserValidationsFormats.ConvertUserLoginDetails(dt, dtCRM, dtFUP,"");

        }
        //public UserLoginDetails BlUserLoginDetails(BlUserValidations objbl)
        //{
            //Int64 FK_Company = 0;
            //string BankKey = "", UserCode="";
            //string ResponseCode = "", ResponseMessage = "";
            //DalUserValidations objDal = new DalUserValidations();
            //BankKey = objbl.BankKey;
            //DataTable dt = objDal.DalCommonValidate(objbl);
            //DataTable dtCRM = new DataTable();
            //DataTable dtFUP = new DataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0].Field<string>("ResponseCode") != "-1")
            //    {
            //         FK_Company = Convert.ToInt64(dt.Rows[0]["FK_Company"]);
            //         UserCode = Convert.ToString(dt.Rows[0]["UserCode"]);
            //         dtCRM = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSV", Convert.ToInt64(FK_Company), 3);
            //         dtFUP = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSF", Convert.ToInt64(FK_Company), 3);
            //    }
            //    else
            //    {
            //        ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
            //        ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
            //    }
            //}
            //objDal = null;
            //if (ResponseCode == "-1")
            //{
                //return BlUserValidationsFormats.ConvertUserLoginDetails(dt, dtCRM, dtFUP, ModulesAndFeatures);
            //}
            //else if (objbl.LicenseChecking(BankKey, FK_Company))
            //{
            //    if (objbl.LicenseUserCountChecking(BankKey, FK_Company, UserCode))
            //    {
            //        return BlUserValidationsFormats.ConvertUserLoginDetails(dt, dtCRM, dtFUP, ModulesAndFeatures);
            //    }
            //    else
            //    {

            //        ResponseMessage = "License Verification Failed.Please Update License";
            //        ResponseCode = "-8";
            //        BlUserValidationsModel.WriteToLog("Exception||UserLogin::License Verification Failed.Please Update License ||\r\n", BankKey);
            //        return BlUserValidationsFormats.ConvertUserLoginDetails(ResponseCode, ResponseMessage);
            //    }
            //}
            //else
            //{

            //    ResponseMessage = "License Verification Failed.Please Update License";
            //    ResponseCode = "-8";
            //    BlUserValidationsModel.WriteToLog("Exception||UserLogin::License Verification Failed.Please Update License ||\r\n", BankKey);
            //    return BlUserValidationsFormats.ConvertUserLoginDetails(ResponseCode, ResponseMessage);
            //}


        //}
      
        private Boolean GetUserCount(string BankKey, string UserCode, Int32 UserCount, Int32 CountType)
        {
            DalUserValidations dalUser = new DalUserValidations();
            bool count = dalUser.GetLoginCount(BankKey, UserCode, UserCount, CountType);
            return count;
        }
        public DataTable blCommonAppChecking(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonAppChecking(objbl);
            objDal = null;
            return dt;
        }
        public CommonAPIR BlTokenheck(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalTokenCheck(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertTokencheck(dt);
        }
        public UserLoginDetails BlVerification(BlUserValidations objbl)
        {
            Int64 FK_Company = 0;
            string BankKey = "", UserCode = "";
            string ResponseCode = "", ResponseMessage = "";
            DalUserValidations objDal = new DalUserValidations();
            BankKey = objbl.BankKey;
            DataSet dt = objDal.DalCommonValidateWithDataset(objbl);
            DataTable dt1 = dt.Tables[0];
            DataTable dtCRM =new DataTable();
            DataTable dtFUP = new DataTable();
            if (dt1.Rows.Count>0)
            {
                if (dt1.Rows[0].Field<string>("ResponseCode") != "-1")
                {
                     FK_Company = Convert.ToInt64(dt1.Rows[0]["FK_Company"]);
                    UserCode = Convert.ToString(dt1.Rows[0]["UserCode"]);
                    dtCRM = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSV", Convert.ToInt64(FK_Company), 3);
                     dtFUP = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSF", Convert.ToInt64(FK_Company), 3);
                }
            }
            
            objDal = null;
            //if (ResponseCode == "-1")
            //{
            //    return BlUserValidationsFormats.ConvertUserLoginDetailsDS(dt, dtCRM, dtFUP, ModulesAndFeatures);
            //}
            //else if (objbl.LicenseChecking(BankKey, FK_Company))
            //{
            //    if (objbl.LicenseUserCountChecking(BankKey, FK_Company, UserCode))
            //    {
            //        return BlUserValidationsFormats.ConvertUserLoginDetailsDS(dt, dtCRM, dtFUP, ModulesAndFeatures);
            //    }
            //    else
            //    {

            //        ResponseMessage = "License Verification Failed.Please Update License";
            //        ResponseCode = "-8";
            //        BlUserValidationsModel.WriteToLog("Exception||UserLogin::License Verification Failed.Please Update License ||\r\n", BankKey);
            //        return BlUserValidationsFormats.ConvertUserLoginDetails(ResponseCode, ResponseMessage);
            //    }
            //}
            //else
            //{

            //    ResponseMessage = "License Verification Failed.Please Update License";
            //    ResponseCode = "-8";
            //    BlUserValidationsModel.WriteToLog("Exception||UserLogin::License Verification Failed.Please Update License ||\r\n", BankKey);
            //    return BlUserValidationsFormats.ConvertUserLoginDetails(ResponseCode, ResponseMessage);
            //}
            return BlUserValidationsFormats.ConvertUserLoginDetailsDS(dt, dtCRM, dtFUP, ModulesAndFeatures);

        }
        public MPINDetails BlMPINDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
           DataTable dt = objDal.DalCommonValidate(objbl);
            
            objDal = null;
            return BlUserValidationsFormats.ConvertToMPINDetails(dt);

        }
        public CustomerDetailsList BlCustomerDetailsList(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCustomerDetailsList(dt);

        }
        public LeadFromDetailsList BlLeadFromDetailsList(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadFromDetailsList(dt);

        }
        public LeadThroughDetailsList BlLeadThroughDetailsList(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadThroughDetailsList(dt);

        }
        public AddNewCustomer BlAddNewCustomer(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAddNewCustomer(dt);

        }
        public MaintenanceMessage BlMaintenanceMessage(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertMaintenanceMessage(dt);
           

        }
        public BannerDetails BlBannerDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertBannerDetails(dt);


        }
        public CollectedByUsersList BlCollectedByUsersList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCollectedByUsersList(dt);


        }
        public CategoryDetailsList BlCategoryDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCategoryDetailsList(dt);


        }
        public ProductDetailsList BlProductDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductDetailsList(dt);


        }
        public StatusDetailsList BlStatusDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertStatusDetailsList(dt);


        }
        public PriorityDetailsList BlPriorityDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertPriorityDetailsList(dt);


        }
        public FollowUpActionDetails BlFollowUpActionDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertFollowUpActionDetails(dt);


        }
        public FollowUpTypeDetails BlFollowUpTypeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertFollowUpTypeDetails(dt);


        }
        public MediaTypeDetails BlMediaTypeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertMediaTypeDetails(dt);
        }
        public SubMediaTypeDetails BlSubMediaTypeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertSubMediaTypeDetails(dt);
        }
        public ReasonDetails BlReasonDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertReasonDetails(dt);
        }
        public BranchTypeDetails BlBranchTypeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertBranchTypeDetails(dt);
        }
        public BranchDetails BlBranchDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertBranchDetails(dt);
        }
        public DepartmentDetails BlDepartmentDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDepartmentDetails(dt);
        }
        public EmployeeDetails BlEmployeeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeDetails(dt);
        }
        public EmployeeAllDetails BlEmployeeAllDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeAllDetails(dt);
        }
        public LeadManagementDetailsList BlLeadManagementDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadManagementDetailsList(dt);
        }
        public RoportSettingsList BlRoportSettingsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertRoportSettingsList(dt);
        }
        public GeneralReport BlGeneralReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertGeneralReport(dt);
        }
        public LeadHistoryDetails BlLeadHistoryDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadHistoryDetails(dt);
        }
        public LeadInfoDetails BlLeadInfoDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadInfoDetails(dt);
        }
        public LeadImageDetails BlLeadImageDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadImageDetails(dt);
        }
        public NotificationDetails BlNotificationDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertNotificationDetails(dt);
        }
        public UpdateLeadGenerateAction BlUpdateLeadGenerateAction(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateLeadGenerateAction(dt);
        }
        public PincodeDetails BlPincodeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertPincodeDetails(dt);
        }
        public CountryDetails BlCountryDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCountryDetails(dt);
        }
        public StatesDetails BlStatesDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertStatesDetails(dt);
        }
        public DistrictDetails BlDistrictDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDistrictDetails(dt);
        }
        public AreaDetails BlAreaDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAreaDetails(dt);
        }
        public PostDetails BlPostDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertPostDetails(dt);
        }

        public UpdateLeadGeneration BlUpdateLeadGeneration(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalLeadGenerateUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateLeadGeneration(dt);

        }
        public UpdateLeadGeneration BlUpdateLeadGenerationOld(BlUserValidations objBl)
        {
            DalUserValidations objDal = new DalUserValidations();
            string prvid = "",usercode="";
            usercode = "(SELECT TOP 1 UserCode FROM Users WHERE FK_Employee='" + objBl.FK_Employee + "'  AND Cancelled=0 AND Passed=1)";
            prvid = "SELECT ISNULL((SELECT TOP 1 isnull( ID_LeadGenerate,0) LastID FROM LeadGenerate WHERE EntrBy=(SELECT TOP 1 UserCode FROM Users WHERE FK_Employee='" + objBl.FK_Employee + "'  AND Cancelled=0 AND Passed=1) ORDER BY ID_LeadGenerate DESC),0)  ";
            objBl.LastID = Convert.ToString(objDal.GetSingleValue(prvid,objBl));
            objBl.EntrBy = objDal.GetSingleValue(usercode, objBl);
            dynamic ExpectDate = DBNull.Value;
            if (objBl.LgpExpectDate != "")
                ExpectDate = objBl.LgpExpectDate;
            DataTable dt = new DataTable();
            dt = getdatatablestruct();
            DataRow nr = dt.NewRow();
            nr["ID_Product"] = objBl.ID_Product;
            nr["FK_Category"] = objBl.FK_Category;
            nr["ProdName"] = objBl.ProdName;
            nr["ProjectName"] = objBl.ProjectName;
            nr["AssignEmp"] = objBl.AssignEmp;
            nr["LgpPQuantity"] = objBl.LgpPQuantity;
            nr["LgpDescription"] = objBl.LgpDescription;
            nr["ActStatus"] = objBl.ActStatus;
            nr["FK_NetAction"] = objBl.FK_NetAction;
            nr["BranchID"] = objBl.BranchID;
            nr["BranchTypeID"] = objBl.BranchTypeID;
            nr["FK_ActionType"] = objBl.FK_ActionType;
            nr["NextActionDate"] = objBl.NextActionDate;
            nr["FK_Departement"] = objBl.FK_Departement;
            nr["FK_Employee"] = objBl.FK_Employee;
            nr["FK_Priority"] = objBl.FK_Priority;
            nr["LgpExpectDate"] = ExpectDate;
            nr["LgpMRP"] = objBl.LgpMRP;
            nr["LgpSalesPrice"] = objBl.LgpSalesPrice;
            //nr["EntrBy"] = objBl.EntrBy;
            dt.Rows.Add(nr);
            objBl.SubProductDetails = BlUserValidationsModel.xmlTostring(ProductDetails(dt));
            DataTable dt1 = objDal.DalLeadGenerateUpdate(objBl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateLeadGeneration(dt1);
        }
        public static List<ProdProjDTL> ProductDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProdProjDTL()
                    {
                        ID_Product = Convert.ToString(dr["ID_Product"].ToString()),
                        FK_Category = Convert.ToString(dr["FK_Category"].ToString()),
                        ProdName = Convert.ToString(dr["ProdName"].ToString()),
                        ProjectName = Convert.ToString(dr["ProjectName"].ToString()),
                        AssignEmp = Convert.ToString(dr["AssignEmp"].ToString()),
                        LgpPQuantity = Convert.ToString(dr["LgpPQuantity"].ToString()),
                        LgpDescription = Convert.ToString(dr["LgpDescription"].ToString()),
                        ActStatus = Convert.ToString(dr["ActStatus"].ToString()),
                        FK_NetAction = Convert.ToString(dr["FK_NetAction"].ToString()),
                        BranchID = Convert.ToString(dr["BranchID"].ToString()),
                        BranchTypeID = Convert.ToString(dr["BranchTypeID"].ToString()),
                        FK_ActionType = Convert.ToString(dr["FK_ActionType"].ToString()),
                        NextActionDate = Convert.ToString(dr["NextActionDate"].ToString()),
                        FK_Departement = Convert.ToString(dr["FK_Departement"].ToString()),
                        FK_Employee = Convert.ToString(dr["FK_Employee"].ToString()),
                        FK_Priority = Convert.ToString(dr["FK_Priority"].ToString()),
                        EntrBy = Convert.ToString(dr["EntrBy"].ToString()),
                        LgpExpectDate= Convert.ToString(dr["LgpExpectDate"].ToString()),
                        LgpMRP = Convert.ToString(dr["LgpMRP"].ToString()),
                        LgpSalesPrice = Convert.ToString(dr["LgpSalesPrice"].ToString()),
                        FK_ProductLocation=Convert.ToString(dr["FK_ProductLocation"].ToString())
                    }).ToList();

        }
        public DataTable getdatatablestruct()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID_Product");
            dt.Columns.Add("FK_Category");
            dt.Columns.Add("ProdName");
            dt.Columns.Add("ProjectName");
            dt.Columns.Add("AssignEmp");
            dt.Columns.Add("LgpPQuantity");
            dt.Columns.Add("LgpDescription");
            dt.Columns.Add("ActStatus");
            dt.Columns.Add("FK_NetAction");
            dt.Columns.Add("BranchID");
            dt.Columns.Add("BranchTypeID");
            dt.Columns.Add("FK_ActionType");
            dt.Columns.Add("NextActionDate");
            dt.Columns.Add("FK_Departement");
            dt.Columns.Add("FK_Employee");
            dt.Columns.Add("FK_Priority");
            dt.Columns.Add("EntrBy");
            dt.Columns.Add("LgpExpectDate");

            return dt;
        }
        //public ProductWiseComplaintList BlProductWiseComplaintList(BlUserValidations objbl)
        //{
        //    DalUserValidations objDal = new DalUserValidations();
        //    DataTable dt = objDal.DalCommonValidate(objbl);
        //    objDal = null;
        //    return BlUserValidationsFormats.ConvertProductWiseComplaintListDetails(dt);
        //}
        public AddAgentCustomerRemarks BlAddAgentCustomerRemarks(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAddAgentCustomerRemarks(dt);
        }

        public LeadGenerationDetails BlLeadGenerationDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadGenerationDetails(dt);
        }
        public LeadGenerationListDetails BlLeadGenerationListDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);                    
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadGenerationListDetails(dt);
        }
        public LeadsDashBoardDetails BlLeadsDashBoardDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadsDashBoardDetails(dt);
        }
        public AddQuatation BlAddQuatation(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();         
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAddQuatation(dt);
        }
        public DateWiseExpenseDetails BlDateWiseExpenseDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDateWiseExpenseDetails(dt);
        }
        public AddDocument BlAddDocument(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalLeadDocumentDetailsUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAddDocument(dt);
        }
        public UpdateExpenseDetails BlUpdateExpenseDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateExpenseDetails(dt);
        }
        public ExpenseType BlExpenseType(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertExpenseType(dt);
        }
        public ActionType BlActionType(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertActionType(dt);
        }
        public PendingCountDetails BlPendingCountDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertPendingCountDetails(dt);
        }
        public AgendaDetails BlAgendaDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAgendaDetails(dt);
        }
        public EmployeeProfileDetails BlEmployeeProfileDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeProfileDetails(dt);
        }
        public UpdateUserLoginStatus BlUpdateUserLoginStatus(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateUserLoginStatus(dt);
        }
        public ActivitiesDetails BlActivitiesDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertActivitiesDetails(dt);
        }
        public LeadGenerateReport BlLeadGenerateReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadGenerateReport(dt);
        }
        public ProductWiseLeadReport BlProductWiseLeadReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductWiseLeadReport(dt);
        }
        public PriorityWiseLeadReport BlPriorityWiseLeadReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertPriorityWiseLeadReport(dt);
        }
        public ReportNameDetails BlReportNameDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertReportNameDetails(dt);
        }
        public GroupingDetails BlGroupingDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertGroupingDetails(dt);
        }
        public ActionListDetailsReport BlActionListDetailsReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertActionListDetailsReport(dt);
        }
        public StatusListDetailsReport BlStatusListDetailsReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertStatusListDetailsReport(dt);
        }
        public NewListDetailsReport BlNewListDetailsReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertNewListDetailsReport(dt);
        }
        public FollowUpListDetailsReport BlFollowUpListDetailsReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertFollowUpListDetailsReport(dt);
        }
        public LeadGenerationDefaultvalueSettings BlLeadGenerationDefaultvalueSettings(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadGenerationDefaultvalueSettings(dt);
        }
        public DeleteLeadGenerate BlDeleteLeadGenerate(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalDeleteLeadGenerate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDeleteLeadGenerate(dt);
        }
        public AgendaType BlAgendaType(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAgendaType(dt);
        }
        public UpdateLeadManagement BlUpdateLeadManagement(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
           
            DataTable dt = objDal.DalProLeadGenerateActionUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateLeadManagement(dt);
        }
        public DocumentDetails BlDocumentDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDocumentDetails(dt);
        }
        public DocumentImageDetails BlDocumentImageDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDocumentImageDetails(dt);
        }
        public AddRemark BlAddRemark(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAddRemark(dt);
        }
        public TodoListLeadDetails BlTodoListLeadDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertTodoListLeadDetails(dt);
        }
        public ServiceCustomerDetails BlServiceCustomerDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceCustomerDetails(dt);
        }
        public CommonPopupDetails BlCommonPopupDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCommonPopupDetails(dt);
        }
        public ServiceProductDetails BlServiceProductDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceProductDetails(dt);
        }
        public ServiceDet BlServiceDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceDetails(dt);
        }
        public ComplaintDetails BlComplaintDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertComplaintDetails(dt);
        }
        public WarrantyDetails BlWarrantyDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertWarrantyDetails(dt);
        }
        public ProductHistoy BlProductHistory(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductHistory(dt);
        }
        public SalesHistory BlSalesHistory(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertSalesHistory(dt);
        }
        public UpdateCustomerServiceRegister BlUpdateCustomerServiceRegister(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();           
            DataTable dt = objDal.DalCustomerServiceRegisterUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateCustomerServiceRegister(dt);
        }
        public CompanyLogDetails BlCompanyLogDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCompanyLogDetails(dt);
        }
        public MediaDetails BlMediaDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertMediaDetails(dt);
        }
        public ServiceCountDetails BlServiceCountDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalServiceAssignList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceCountDetails(dt);
        }
        public ServiceAssignNewDetails BlServiceAssignNewDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalServiceAssignList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceAssignNewDetails(dt);
        }
        public CustomerBalanceDetails BlCustomerBalanceDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCustomerBalanceList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCustomerBalanceDetails(dt);
        }
        public ServiceAssignOnGoingDetails BlServiceAssignOnGoingDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalServiceAssignList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceAssignOnGoingDetails(dt);
        }
        public RoleDetails BlRoleDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertRoleDetails(dt);
        }
        public ServiceAssignDetails BlServiceAssignDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataSet ds = objDal.DalServiceAssignDetails(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceAssignDetails(ds);
        }
        public ServiceAssignedWork BlServiceAssignedWork(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceAssignedWorkDetails(dt);
        }
        public CustomerserviceassignUpdate BlCustomerserviceassignUpdate(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCustomerserviceassignUpdate(objbl,1);
            objDal = null;
            return BlUserValidationsFormats.ConvertCustomerserviceassignUpdate(dt);
        }
        public CustomerserviceassignEdit BlCustomerserviceassignEdit(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCustomerserviceassignUpdate(objbl, 2);
            objDal = null;
            return BlUserValidationsFormats.ConvertCustomerserviceassignEdit(dt);
        }
        public CustomerServiceRegisterCount BlCustomerServiceRegisterCount(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCustomerServiceRegisterCount(dt);
        }
        public ServiceFollowUpdetails BlServiceFollowUpdetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceFollowUpdetails(dt);
        }
        public ComplaintListDetails BlComplaintListDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertComplaintListDetails(dt);
        }
        public CustomerDueDetils BlCustomerDueDetils(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCustomerDueDetils(dt);
        }
        public EmployeeWiseTicketSelect BlEmployeeWiseTicketSelect(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataSet dt = objDal.DalCommonValidateWithDataset(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeWiseTicketSelect(dt);
        }
        public UserLoginDetails BlVerificationDataSet(BlUserValidations objbl)
        {
            Int64 FK_Company = 0;
            string BankKey = "";
            DalUserValidations objDal = new DalUserValidations();
            BankKey = objbl.BankKey;
            DataSet dt = objDal.DalCommonValidateWithDataset(objbl);
            DataTable dt1 = dt.Tables[0];
            FK_Company = Convert.ToInt64(dt1.Rows[0]["FK_Company"]);
            DataTable dtCRM = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSV", Convert.ToInt64(FK_Company), 3);
            DataTable dtFUP = objDal.DalPageSettingsDetails(BankKey, "SERVICE", "CUSF", Convert.ToInt64(FK_Company), 3);
            objDal = null;
            return BlUserValidationsFormats.ConvertUserLoginDetailsDS(dt, dtCRM, dtFUP,"");
           

        }
        public GenralSettingsDetails BlGenralSettings(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertGenralSettingsDetails(dt);
        }
        public UpdateWalkingCustomer BlUpdateWalkingCustomer(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();           
            DataTable dt = objDal.DalProCustomerAssignmentUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateWalkingCustomer(dt);
        }
        public WalkingCustomerDetailsList BlWalkingCustomerDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertWalkingCustomerDetailsList(dt);
        }
        public ServiceDashBoardDetails BlServiceDashBoardDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataSet dt = objDal.DalCommonValidatewithDataset(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertServiceDashBoardDetails(dt);
        }
        public AgendaCount BlAgendaCountDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataSet dt = objDal.DalAgendaCount(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAgendaCountDetails(dt);
        }
        public UpdateLocation BlUpdateLocation(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProLocationUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateImageLocation(dt);
        }
        public UpdateFollowupStatus BlUpdateFollowUpSatatus(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProFollowUpStatusUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateFollowupSatus(dt);
        }
        public EmployeeDetails BlWalkingCustomerEmployeeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertWalkingEmployeeDetails(dt);
        }
        public UpdateAttanceMarking BlUpdateAttanceMarking(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProAttanceMarkingUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateAttanceMarking(dt);
        }
        public UpdateEmployeeLocation BlUpdateEmployeeLocation(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProEmployeeLocationUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateEmployeeLocation(dt);
        }
        public UpdateNotification BlUpdateNotification(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProNotificationUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateNotification(dt);
        }
        public EmployeeLocationList BlEmployeeLocationList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProEmployeeLocationList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeLocationList(dt);
        }
        public EmployeeWiseLocationList BlEmployeeWiseLocationList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProEmployeeWiseLocationList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeWiseLocationList(dt);
        }
        public EmployeeLocationDistance BlEmployeeLocationDistance(BlUserValidations objbl)
        {
            string ToDate = "", BankKey = "", FKCompany = "", URLKey = "";
            ToDate = objbl.LocationEnteredDate;
            BankKey = objbl.BankKey;
            FKCompany = objbl.FK_Company;
            URLKey = objbl.URLKey;
            DalUserValidations objDal = new DalUserValidations();
            DataTable dtEmp = objDal.DalProEmployeeLocationList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertEmployeeLocationDistance(dtEmp, BankKey, FKCompany, ToDate, URLKey);
        }
        public DesignationList BlDesignationList(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDesignationList(dt);

        }
        public MailResult BlMail(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalGetMailSettings(objbl);
            string result=objDal.SendMail(objbl, dt);
            objDal = null;
            return BlUserValidationsFormats.SendMail(result);
        }
        public ItemSearchList BlItemSearchList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProItemList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertItemList(dt);
        }
        public ProductEnquiryList BlProductEnquiryList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProProductEnquiry(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductEnquiryList(dt);
        }
        public ProductEnquiryDetails BlProductEnquiryDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataSet ds = objDal.DalProProductEnquiryDetails(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductEnquiryDetailList(ds);
        }
        public AuthorizationModuleData BlAuthorizationModuleDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProAuthorizationModuleList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationModuleList(dt);
        }
        public DynamicData BlAuthorizationList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProAuthorizationList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationList(dt);
        }
        public AuthorizationActionDetails BlAuthorizationAction(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dtls = null;
            DataTable dt = objDal.DalProAuthorizationAction(objbl);
            string AuthID = objbl.AuthID;
            if(dt.Rows.Count>0)
            {
                objbl.FK_Master=Convert.ToString(dt.Rows[0]["FK_Master"]);
                objbl.ReqMode= Convert.ToString(dt.Rows[0]["Mode"]);
                objbl.TransMode = Convert.ToString(dt.Rows[0]["TransMode"]);
                dtls = objDal.DalAuthorizationListDetails(objbl);          
            }
           
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationAction(dt, dtls, AuthID);
        }
        public DynamicData BlAuthorizationListDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalAuthorizationListDetails(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationList(dt);
        }
        public UpdateAuthorization BlAuthorizationApproveUpdate(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalAuthorizationApproveUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateAuthorization(dt);
        }
        public UpdateAuthorization BlAuthorizationRejectUpdate(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalAuthorizationRejectUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertRejectAuthorization(dt);
        }
        public DynamicData BlCmnSearchPopup(BlUserValidations objbl)
        {
            DalCommon objDal = new DalCommon();
            DataTable dt = objDal.DalProERPCmnSearchPopup(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertERPCmnSearchPopup(dt);
        }
        public CorrectionAuthorization BlAuthorizationCorrection(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalAuthorizationCorrection(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertCorrectionAuthorization(dt);
        }
        public ProductLocationList BlProductLocationList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductLocationList(dt);
        }
        public WarrantyAMCDetails BlWarrantyAMCDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalWarrantyAMCDetailsList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertWarrantyAMCDetails(dt);
        }
        public UserTaskList BlUserTaskList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProUserTaskList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUserTaskList(dt);
        }
        public WalkingCustomerList BlWalkingCustomerList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProGetLeadByMobileNo(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertWalkingCustomerList(dt);
        }
       
        public AuthorizationCorrectionModuleData BlAuthorizationCorrectionModuleDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();          
            DataSet ds = objDal.DalProAuthorizationCorrectionModuleList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationCorrectionModuleList(ds);
        }
        public AuthorizationCorrectionDetailsData BlAuthorizationCorrectionDetailsList(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProAuthorizationCorrectionDetailsList(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationCorrectionDetailsData(dt);
        }
        public CorrectionLeadGenerate BlAuthorizationCorrectionDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataSet ds = objDal.DalProAuthorizationCorrectionLeadDetails(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationCorrectionLeadData(ds);
        }
        public WalkingCustomerVoiceDetails BlWalkingCustomerVoiceDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalProGetVoiceInfo(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertWalkingCustomerVoiceDetails(dt);
        }
        public AuthorizationDetails BlAuthorizationDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationDetails(dt);
        }
        public ModuleDetails BlModuleDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertModuleDetails(dt);
        }
        public ModuleWiseSearchDetails BlModuleWiseSearchDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertModuleWiseSearchDetails(dt);
        }
        public AuthorizationModuleCountDetails BlAuthorizationModuleCountDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationModuleCountDetails(dt);
        }
        public AuthorizationPendingDetails BlAuthorizationPendingDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationPendingDetails(dt);
        }
        public OtherChargeDetails BlOtherChargeDetails(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertOtherChargeDetails(dt);
        }
        public DashBoardModule BlDashBoardModule(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertDashBoardModule(dt);
        }
        public AuthorizationDataList BlAuthorizationDataList(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataSet ds = objDal.DalAuthorizationDetails(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAuthorizationDataList(ds);

        }
        public UserTaskListDetails BlUserTaskListDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUserTaskListDetails(dt);

        }
        public SendIntimationModule BlSendIntimationModule(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertSendIntimationModule(dt);

        }
        public int VersionCheck(string VersionNo, string BankKey, byte OsType)
        {
            DalUserValidations objDal = new DalUserValidations();
            int outvalue = objDal.VersionCheck(VersionNo, BankKey, OsType);
            objDal = null;
            return outvalue;

        }
        public ScheduleType BlScheduleType(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertScheduleType(dt);

        }
        public Channel BlChannel(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertChannel(dt);

        }
        public UpdateSendIntimation BlUpdateSendIntimation(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSendIntimationUpdate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUpdateSendIntimation(dt);

        }
        public AppConfigurationSettings BlAppConfigurationSettings(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertAppConfigurationSettings(dt);

        }
        public DataTable AppConfigurationSettings(string BankKey, string ReqMode, string ConfigCode)
        {
            DalUserValidations objDal = new DalUserValidations();
            DateTime dtRecievedTime = DateTime.Now;
            DataTable dt = objDal.DalAppConfigurationSettings( BankKey,  ReqMode,  ConfigCode);
            objDal = null;
            return dt;
        }
        public SubCategoryDetailsList BlSubCategoryDetailsList(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertSubCategoryDetailsList(dt);

        }
        public BrandDetails BlBrandDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertBrandDetails(dt);

        }
        public LeadSourceDetails BlLeadSourceDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadSourceDetails(dt);

        }
        public LeadFromInfo BlLeadFromInfo(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadFromInfo(dt);

        }
        public LeadHistory BlLeadHistory(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertLeadHistory(dt);

        }
        public ProductType BlProductType(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertProductType(dt);

        }
        public TimeLineDetails BlTimeLineDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertTimeLineDetails(dt);

        }
        public AttendanceDetails BlAttendanceDetails(BlUserValidations objbl)
        {
         
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalAttendanceDetails(objbl.BankKey, objbl.FK_Company, objbl.EnteredDate, objbl.EntrBy,1);
            objDal = null;
            return BlUserValidationsFormats.ConvertAttendanceDetails(dt);

        }
       
        public AttendancePunchDetails BlAttendancePunchDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalAttendanceDetails(objbl.BankKey, objbl.FK_Company, objbl.EnteredDate, objbl.EntrBy,2);
            objDal = null;
            return BlUserValidationsFormats.ConvertAttendancePunchDetails(dt);

        }
        public UserDetails BlUserDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertUserDetails(dt);

        }
        public MyActivityCount BlMyActivityCount(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalMyActivities(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertMyActivityCount(dt);

        }
        public MyActivitysActionCountDetails BlMyActivitysActionCountDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalMyActivities(objbl);
           
            objDal = null;
            return BlUserValidationsFormats.ConvertMyActivitysActionCountDetails(dt);

        }
        public MyActivitysActionDetails BlMyActivitysActionDetails(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalMyActivities(objbl);           
            objDal = null;
            return BlUserValidationsFormats.ConvertMyActivitysActionDetails(dt);

        }
        public MyActivitysFliters BlMyActivitysFliters(BlUserValidations objbl)
        {

            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalMyActivities(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertMyActivitysFliters(dt);

        }
        public ClosedLeadReport BlClosedLeadReport(BlUserValidations objbl)
        {
            DalUserValidations objDal = new DalUserValidations();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlUserValidationsFormats.ConvertClosedLeadReport(dt);
        }
        #endregion
    }
}
