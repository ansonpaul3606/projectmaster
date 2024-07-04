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
    public class BlServiceFollowUp: IServiceFollowUp
    {
        #region Variable
        private string _ReqMode { get; set; }
        private string _FK_Company { get; set; }
        private string _Token { get; set; }
        private string _SubMode { get; set; }
        private string _BankKey { get; set; }
        private string _MobileNumber { get; set; }
        private string _OTP { get; set; }
        private string _MPIN { get; set; }
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
        public string _ID_FollowUpBy { get; set; }
        public byte[] _DocumentImage { get; set; }
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
        public string _NameCriteria { get; set; }
        public string _CustomerNotes { get; set; }
        public string _StartingDate { get; set; }
        public string _ServiceAmount { get; set; }
        public string _ProductAmount { get; set; }
        public string _NetAmount { get; set; }
        public string _TotalAmount { get; set; }
        public string _ReplaceAmount { get; set; }
        public string _DiscountAmount { get; set; }
        public string _ServiceDetails { get; set; }
        public string _ProductDetails { get; set; }
        public string _AttendedEmployeeDetails { get; set; }
        public string _PaymentDetail { get; set; }
        public string _NextActionDateLead { get; set; }
        public string _FK_BillType { get; set; }
        public string _FK_NextActionLead { get; set; }
        public string _FK_ActionTypeLead { get; set; }
        public string _ID_CustomerWiseProductDetails { get; set; }
        public string _FK_EmployeeLead { get; set; }
        public string _FK_NextAction { get; set; }
        public string _LocationEnteredDate { get; set; }
        public string _LocationEnteredTime { get; set; }
        public string _ComponentCharge { get; set; }
        public string _ServiceCharge { get; set; }
        public string _OtherCharge { get; set; }
        public string _TotalSecurityAmount { get; set; }
        public string _ServiceIncentive { get; set; }
        public string _Criteria4 { get; set; }
        public string _Criteria3 { get; set; }
        public string _ID_CustomerServiceRegisterProductDetails { get; set; }
        #endregion


        #region Constructor
        public BlServiceFollowUp()
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
            _FromDate = string.Empty;
            _Todate = string.Empty;
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
            _ID_CustomerWiseProductDetails= string.Empty;
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
            _FK_NetAction = string.Empty;
            _BranchID = string.Empty;
            _BranchTypeID = string.Empty;
            _AssignEmp = string.Empty;
            _LocationAddress = string.Empty;
            _selectProcedureName = string.Empty;
            SubProductDetails = string.Empty;
            _PreviousID = 0;
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
            _Visitdate = string.Empty;
            _Visittime = string.Empty;
            _EmployeeType = string.Empty;
            _FK_AttendedBy = string.Empty;
            _TicketTime = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;
            _NameCriteria = string.Empty;
               _CustomerNotes = string.Empty;
            _StartingDate = string.Empty;
            _ServiceAmount = string.Empty;
            _ProductAmount = string.Empty;
            _NetAmount = string.Empty;
            _TotalAmount = string.Empty;
            _ReplaceAmount = string.Empty;
            _DiscountAmount = string.Empty;
            _ServiceDetails = string.Empty;
            _ProductDetails = string.Empty;
            _AttendedEmployeeDetails = string.Empty;
            _PaymentDetail = string.Empty;
            _NextActionDateLead = string.Empty;
            _FK_BillType = string.Empty;
            _FK_NextActionLead = string.Empty;
            _FK_ActionTypeLead = string.Empty;
            _FK_EmployeeLead = string.Empty;
            _FK_NextAction = string.Empty;
            _LocationEnteredDate = string.Empty;
            _LocationEnteredTime = string.Empty;
            _ComponentCharge = string.Empty;
            _ServiceCharge = string.Empty;
            _OtherCharge = string.Empty;
            _TotalSecurityAmount = string.Empty;
            _Criteria3 = string.Empty;
           _Criteria4 = string.Empty;
            _ID_CustomerServiceRegisterProductDetails = string.Empty;
            _ServiceIncentive = String.Empty;
        }
        #endregion
        #region Getters And Setters
        public string ServiceIncentiveDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ServiceIncentive); }
            set { _ServiceIncentive = value; }
        }
        public string Criteria3
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Criteria3); }
            set { _Criteria3 = value; }
        }
        public string ID_CustomerServiceRegisterProductDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerServiceRegisterProductDetails); }
            set { _ID_CustomerServiceRegisterProductDetails = value; }
        }
       
        public string Criteria4
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Criteria4); }
            set { _Criteria4 = value; }
        }
        public string ComponentCharge
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ComponentCharge); }
            set { _Criteria4 = value; }
        }
        public string ServiceCharge
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ServiceCharge); }
            set { _ServiceCharge = value; }
        }
        public string OtherCharge
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_OtherCharge); }
            set { _OtherCharge = value; }
        }
        public string TotalSecurityAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TotalSecurityAmount); }
            set { _TotalSecurityAmount = value; }
        }
        public string FK_BillType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BillType); }
            set { _FK_BillType = value; }
        }
        public string FK_NextActionLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NextActionLead); }
            set { _FK_NextActionLead = value; }
        }
        public string FK_ActionTypeLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ActionTypeLead); }
            set { _FK_ActionTypeLead = value; }
        }
        public string FK_EmployeeLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_EmployeeLead); }
            set { _FK_EmployeeLead = value; }
        }
        public string FK_NextAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NextAction); }
            set { _FK_NextAction = value; }
        }
        public string CustomerNotes
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerNotes); }
            set { _CustomerNotes = value; }
        }
        public string StartingDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_StartingDate); }
            set { _StartingDate = value; }
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
        public string ReplaceAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReplaceAmount); }
            set { _ReplaceAmount = value; }
        }
        public string DiscountAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DiscountAmount); }
            set { _DiscountAmount = value; }
        }
        public string ServiceDetails { get; set; }

        public string ProductDetails { get; set; }
       
        public string AttendedEmployeeDetails { get; set; }

        public string PaymentDetail { get; set; }

        public string Actionproductdetails { get; set; }
        public string ServiceIncentive { get; set; }

        public string OtherCharges { get; set; }
        public string ProductSubDetails { get; set; }
        public string NextActionDateLead
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NextActionDateLead); }
            set { _NextActionDateLead = value; }
        }
        public string NameCriteria
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_NameCriteria); }
            set { _NameCriteria = value; }
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
        public string ID_CustomerWiseProductDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerWiseProductDetails); }
            set { _ID_CustomerWiseProductDetails = value; }
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
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Area); }
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


        public Attendancedetails BlAttendancedetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertAttendancedetails(dt);
        }
        public ServiceAttendedDetails BlServiceAttendedDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertServiceAttendedDetails(dt);
        }
        public ServiceHistoryDetails BlServiceHistoryDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertServiceHistoryDetails(dt);
        }
       
        public ServiceType BlServiceType(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertServiceType(dt);
        }
        public AddedService BlAddedService(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertAddedService(dt);
        }
        public SerDetails BlServiceDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataSet dt = objDal.DalCommonValidatewithDataset(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertServiceList(dt);
        }
        public ProductInfo BlProductInfo(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataSet dt = objDal.DalCommonValidatewithDataset(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertProductInfoList(dt);
        }
        public ServiceSerach BlSearchList(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertSearchList(dt);
        }
        public ProductListDetails BlProductList(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertProductList(dt);
        }
        public ServiceInvoice BlServiceInvoiceDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataSet dt = objDal.DalCommonValidatewithDataset(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertServiceInvoiceList(dt);
        }
        public ChangemodeDetails BlChangemodeDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertChangemodeDetails(dt);
        }
        public ReplaceProductdetails BlReplaceProductdetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertReplaceProductdetails(dt);
        }
        public PopUpProductdetails BlPopUpProductdetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConverPopUpProductdetails(dt);
        }
        public PopUpSerchCritrea BlPopUpSerchCritrea(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertPopUpSerchCritrea(dt);
        }
        public FollowUpPaymentMethod BlFollowUpPaymentMethod(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertFollowUpPaymentMethod(dt);
        }
        public FollowUpActionDetails BlFollowUpActionDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertFollowUpActionDetails(dt);
        }
        public UpdateServiceFollowUp BlUpdateServiceFollowUp(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalServiceFollowUpUpdate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertUpdateServiceFollowUp(dt);
        }
        public SubproductDetails GetSubProductDetails(BlServiceFollowUp objbl, ServiceFollowUpList data)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalGetSubproductDetails(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertGetSubProduct(dt);
        }
        public MoreComponentDetails BlMoreComponentDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertMoreComponentDetails(dt);
        }
       
        public MainProductDetails BlMainProductDetails(BlServiceFollowUp objbl)
        {
            DalServiceFollowUp objDal = new DalServiceFollowUp();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlServiceFollowUpFormat.ConvertMainProductDetails(dt);
        }
     
        #endregion
    }
}
