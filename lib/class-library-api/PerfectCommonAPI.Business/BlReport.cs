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
    public class BlReport : IReport
    {
        #region Variable
        public string _FK_Branch { get; set; }
        private string _Token { get; set; }
        private string _ComplaintService { get; set; }
        private string _BankKey { get; set; }
        private string _ReqMode { get; set; }
        private string _Debug { get; set; }
        public string _TransMode { get; set; }
        private string _PageIndex { get; set; }
        private string _PageSize { get; set; }
        private string _FK_Company { get; set; }
        private string _FK_Machine { get; set; }
        private string _Critrea1 { get; set; }
    
        private string _Critrea2 { get; set; }
        private string _Critrea3 { get; set; }
        private string _Critrea4 { get; set; }
        private string _ID { get; set; }
        private string _TableCount{ get; set; }
        private string _Critrea5 { get; set; }
        private string _Critrea6 { get; set; }
        private string _Project { get; set; }
        private string _ComplaintType { get; set; }
        private string _ComplaintSevice { get; set; }
        private string _ReplacementType { get; set; }     
        private string _SubMode { get; set; }
   
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
        public string _DueDaysFrom { get; set; }
        public string _DueDaysTo { get; set; }
        public string _DueCriteria { get; set; }
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

        public string _TicketNo { get; set; }
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
        public string _FK_NextAction { get; set; }
        public string _ID_CustomerAssignment { get; set; }
        //public string _ID_CustomerWiseProductDetails { get; set; }
        public string _CustomerNotes { get; set; }
       

        public string _FK_Master { get; set; }
       
        public string _ID_User { get; set; }
        public string _FK_Designation { get; set; }
        public string _ToEmail { get; set; }
        public string _Subject { get; set; }    
       
        public string _FK_UserGroup { get; set; }
        public string _ID_TokenUser { get; set; }


        #endregion


        #region Constructor
        public BlReport()
        {
            Initialize();
        }
        #endregion
        #region Initialize
        public void Initialize()
        {
            _ID_TokenUser = string.Empty;
            _FK_Branch = string.Empty;
            _BankKey = string.Empty;
           _TableCount = string.Empty;
            _ReqMode = string.Empty;
            _Token = string.Empty;
            _PageIndex = string.Empty;
          _PageSize = string.Empty;
          _FK_Company = string.Empty;
           _Critrea1 = string.Empty;
            _ReplacementType= string.Empty;
            _Critrea2 = string.Empty;
           _Critrea3 = string.Empty;
           _Critrea4 = string.Empty;
           _ID = string.Empty;
           _Critrea5 = string.Empty;
           _Critrea6 = string.Empty;
            _MobileNumber = string.Empty;
            _Project = string.Empty;
            _OTP = string.Empty;
            _MPIN = string.Empty;
            _OldMPIN = string.Empty;
            //_BankHeader = string.Empty;
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
            _DueCriteria= string.Empty;
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

            _FK_Machine = string.Empty;
            _LocationName = string.Empty;
            _Debug = string.Empty;
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
            //SubProductDetails = string.Empty;
            _PreviousID = 0;
            _FK_BranchCodeUser = string.Empty;
          
            _ID_Branch = string.Empty;
            _GroupId = string.Empty;
            _ReportMode = string.Empty;
            //XMLdata = string.Empty;
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
            _DueDaysFrom = string.Empty;
            _FK_NextAction = string.Empty;
            _DueDaysTo = string.Empty;
            _TicketNo = string.Empty;
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
            _ComplaintType = string.Empty;
            _FK_ComplaintType = string.Empty;
            _SortOrder = string.Empty;
            _DueDays = string.Empty;
            _LgpExpectDate = string.Empty;
            _FK_Customerserviceregister = string.Empty;
           _Visitdate = string.Empty;
            _Visittime = string.Empty;
            _EmployeeType = string.Empty;
            _FK_AttendedBy = string.Empty;
            _TicketTime = string.Empty;
            _Address1 = string.Empty;
            _Address2 = string.Empty;

            _CaAssignedDate = string.Empty;
            _CaDescription = string.Empty;
            _ID_CustomerAssignment = string.Empty;
            //_ID_CustomerWiseProductDetails = string.Empty;
            _CustomerNotes = string.Empty;
         
            _FK_Master = string.Empty;
           
            _FK_Designation = string.Empty;
            _ToEmail = string.Empty;
            _Subject = string.Empty;
      
            _PageIndex = string.Empty;
            _PageSize = string.Empty;
            _Critrea1 = string.Empty;
            _Critrea2 = string.Empty;
            _Critrea3 = string.Empty;
            _Critrea4 = string.Empty;
            _ID = string.Empty;
            _Critrea5 = string.Empty;
            _Critrea6 = string.Empty;
            _ID_User = string.Empty;
               




        }
        #endregion
        #region Getters And Setters
        
        public string ID_TokenUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_TokenUser); }
            set { _ID_TokenUser = value; }
        }
        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string BankKey
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankKey); }
            set { _BankKey = value; }
        }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
        }
        public string TransMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
            set { _TransMode = value; }
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
        public string Debug
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Debug); }
            set { _Debug = value; }
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
        public string TicketNo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TicketNo); }
            set { _TicketNo = value; }
        }
        public string PrductOnly
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_PrductOnly); }
            set { _PrductOnly = value; }
        }
        public string DueDaysFrom
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DueDaysFrom); }
            set { _DueDaysFrom = value; }
        }
        public string DueDaysTo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DueDaysTo); }
            set { _DueDaysTo = value; }
        }
        public string FK_NextAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_NextAction); }
            set { _FK_NextAction= value; }
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
        public string FK_Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Branch); }
            set { _FK_Branch = value; }
        }

        public string Token
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Token); }
            set { _Token = value; }
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
        public string TableCount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TableCount); }
            set { _TableCount = value; }
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
        public string FK_Machine
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Machine); }
            set { _FK_Machine = value; }
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
        public string ComplaintService
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ComplaintService ); }
            set { _ComplaintService = value; }
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
        public string DueCriteria
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DueCriteria); }
            set { _DueCriteria = value; }
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
        //public string TransMode
        //{
        //    get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
        //    set { _TransMode = value; }
        //}
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
        public string ReplacementType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReplacementType); }
            set { _ReplacementType = value; }
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
        public string ComplaintType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ComplaintType); }
            set { _ComplaintType = value; }
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
        public string SubMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SubMode); }
            set { _SubMode = value; }
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
       
        public string FK_Master
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Master); }
            set { _FK_Master = value; }
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
        public string FK_UserGroup
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_UserGroup); }
            set { _FK_UserGroup = value; }
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
 
       
        public string FK_Company
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Company); }
            set { _FK_Company = value; }
        }

        public string Project
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Project); }
            set { _Project = value; }
        }




        #endregion

        #region Data Access Function

        public ProjectReportNameDetails BlReportNameDetails(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlReportFormat.ConvertProjectReportNameDetails(dt);
        }
        public ProjectReport BlProjectReportDetails(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalProProjectReport(objbl);
            objDal = null;
            return BlReportFormat.ConvertProjectReport(dt);
        }
        public ProjectReportDetail BlProjectReportsDetails(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalProProjectReport(objbl);
            objDal = null;
            return BlReportFormat.ConvertProjectReportDetail(dt);
        }
        public ServiceNewList BlServiceNewList(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalProServiceReport(objbl);
            objDal = null;
            return BlReportFormat.ConvertServiceNewList(dt);
        }
        public CategoryNameDetails BlCategoryNameDetails(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlReportFormat.ConvertCategoryNameDetails(dt);
        }
        public ProjectListDetail BlProjectListDetail(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalProProjectReport(objbl);
            objDal = null;
            return BlReportFormat.ConvertProjectListDetail(dt);
        }
        public OutStanding BlOutStanding(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalProServiceReport(objbl);
            objDal = null;
            return BlReportFormat.ConvertOutStanding(dt);
        }
       
        
        public Grouping BlGrouping(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlReportFormat.ConvertGrouping(dt);
        }

        public SummaryWiseLeadReport BlSummaryWiseLeadReport(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            //outpu from db
            DataTable dt = objDal.DalProSummaryLeadReport(objbl);
            objDal = null;
            //mode -> parameter
            byte mode= Convert.ToByte(objbl.ReportMode);

            //convert
            return BlReportFormat.ConvertSummaryWiseLeadReport(dt, mode);
        }
        public ComplaintService BlComplaintService(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            //Int64 SubMode = Convert.ToInt64(objbl.SubMode);
            return BlReportFormat.ConvertComplaintService(dt);
        }
        public TicketNo BlTicketNo(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            //Int64 SubMode = Convert.ToInt64(objbl.SubMode);
            return BlReportFormat.ConvertTicketNo(dt);
        }
        public ServiceProfile BlServiceProfile(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalProServiceReport(objbl);
            objDal = null;
            //Int64 SubMode = Convert.ToInt64(objbl.SubMode);
            return BlReportFormat.ConvertServiceProfile(dt);
        }
        public IntimationCategory BlIntimationCategory(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            //Int64 SubMode = Convert.ToInt64(objbl.SubMode);
            return BlReportFormat.ConvertIntimationCategory(dt);
        }
        public LeadSummaryDetailsReport BlLeadSummaryDetailsReport(BlReport objbl)
        {
            DalReport objDal = new DalReport();
            DataTable dt = objDal.DalLeadSummaryDetailsReport(objbl);
            objDal = null;          
            return BlReportFormat.ConvertLeadSummaryDetailsReport(dt);
        }
      
        #endregion
    }
}
