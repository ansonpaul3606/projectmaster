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
    public class BlProjectDetails : IProjectDetails
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
        private string _PageIndex { get; set; }
        private string _PageSize { get; set; }
        private string _Critrea { get; set; }
        public string _TransMode { get; set; }
        private string _Critrea1 { get; set; }
        private string _Critrea2 { get; set; }
        private string _Critrea3 { get; set; }
        private string _Critrea4 { get; set; }
        private string _ID { get; set; }
        private string _Critrea5 { get; set; }
        private string _Critrea6 { get; set; }
        private string _BankHeader { get; set; }
        private string _FK_Master { get; set; }
        private string _ID_CustomerWiseProductDetails { get; set; }
        private string _Name { get; set; }
        private string _Email { get; set; }
        private string _Address { get; set; }
        private string _FromDate { get; set; }
        private string _Todate { get; set; }
        private string _Pincode { get; set; }
        private string _CustomerNote { get; set; }
        private string _EmployeeNote { get; set; }
        private string _LocLatitude { get; set; }
        private string _LocLongitude { get; set; }
        private string _LeadNo { get; set; }
        private string _LocationName { get; set; }
        public string _EntrBy { get; set; }
        public string _FK_Project { get; set; }
        public string _UserAction { get; set; }
        public string _ID_ProjectMaterialUsage { get; set; }
        public string _ProMatUsageDate { get; set; }
        public string _FK_Stages { get; set; }
        public string _FK_Team { get; set; }
        public string _FK_Employee { get; set; }
        public string _FK_BranchCodeUser { get; set; }
        public string _FK_Machine { get; set; }
        public string ID_ProjectFollowUp { get; set; }
        public string _ProMatRequestDate { get; set; }
        public string _ID_ProjectMaterialRequest { get; set; }
        public byte[] _ProjImage { get; set; }
        public string _ProjImageDescription { get; set; }
        public string _ProjImageType { get; set; }
        public string _ProjImageName { get; set; }
        public string _FK_SiteVisit { get; set; }
        public string _FK_ProjectFollowUp { get; set; }
        public string _FK_Stage { get; set; }
        public string _EffectDate { get; set; }
        public string _CurrentStatus { get; set; }
        public string _StatusDate { get; set; }
        public string _Remarks { get; set; }
        public string _Reason { get; set; }
        public string _FK_LeadGeneration { get; set; }
        public string _SVInspectioncharge { get; set; }
        public string _SiteVisitDate { get; set; }
        public string _SitevisitTime { get; set; }
        public string _Note1 { get; set; }
        public string _Note2 { get; set; }
        public string _CustomerNotes { get; set; }
        public string _ExpenseAmount { get; set; }
        public string _FK_TaxGroup { get; set; }
        //string EmployeeDetails { get; set; }
        //string MeasurementDetails { get; set; }
        //string OtherChargeDetails { get; set; }
        //string CheckListSub { get; set; }
        //string OtherChgTaxDetails { get; set; }
        public string _Amount { get; set; }
        public string _IncludeTax { get; set; }
        public string _Date { get; set; }
        public string _OtherCharge { get; set; }
        public string _NetAmount { get; set; }
        public string _FK_SiteVisitAssignment;
        public string _AsOnDate { get; set; }
        public string _FK_PetyCashier { get; set; }
        public string _FK_TransactionType { get; set; }
        public string _RoundOff { get; set; }
       public string _FK_BillType { get; set; }
        public string _FK_PettyCashier { get; set; }
        public string _ID_User { get; set; }
        public string _ID_TokenUser { get; set; }
        #endregion


        #region Constructor
        public BlProjectDetails()
        {
            Initialize();
        }
        #endregion
        #region Initialize
        public void Initialize()
        {
            _Amount = string.Empty;
            _IncludeTax = string.Empty;
            _ReqMode = string.Empty;
            _FK_Company = string.Empty;
            _Token = string.Empty;
            _SubMode = string.Empty;
            _BankKey = string.Empty;
            _BankHeader = string.Empty;
            _OTP = string.Empty;
            _MPIN = string.Empty;
            _OldMPIN = string.Empty;
            _PageSize = string.Empty;
            _Critrea = string.Empty;
            _Critrea1 = string.Empty;
            _Critrea2 = string.Empty;
            _Critrea3 = string.Empty;
            _Critrea4 = string.Empty;
            _ID = string.Empty;
            _Critrea5 = string.Empty;
            _Critrea6 = string.Empty;
            _FK_Master = string.Empty;
            _Name = string.Empty;
            _Email = string.Empty;
            _Address = string.Empty;
            _ID_CustomerWiseProductDetails = string.Empty;
            _FromDate = string.Empty;
            _Todate = string.Empty;
            _Pincode = string.Empty;
            _CustomerNote = string.Empty;
            _EmployeeNote = string.Empty;
            _LocLatitude = string.Empty;
            _LocLongitude = string.Empty;
            _LeadNo = string.Empty;
            _LocationName = string.Empty;
            _EntrBy = string.Empty;
            _TransMode = string.Empty;
            _FK_Project = string.Empty;
            _UserAction = string.Empty;
            _ID_ProjectMaterialUsage = string.Empty;
            _ProMatUsageDate = string.Empty;
            _FK_Stages = string.Empty;
            _FK_Team = string.Empty;
            _FK_Employee = string.Empty;
            _FK_BranchCodeUser = string.Empty;
            _FK_Machine = string.Empty;
            _ProMatRequestDate = string.Empty;
            _ID_ProjectMaterialRequest = string.Empty;
            _ProjImage = null;
            _ProjImageDescription = string.Empty;
            _ProjImageType = string.Empty;
            _ProjImageName = string.Empty;
            _FK_SiteVisit = string.Empty;
            _FK_ProjectFollowUp = string.Empty;
            _FK_Stage = string.Empty;
            _EffectDate = string.Empty;
            _CurrentStatus = string.Empty;
            _StatusDate = string.Empty;
            _Remarks = string.Empty;
            _Reason = string.Empty;
            _FK_LeadGeneration = string.Empty;
            _SVInspectioncharge = string.Empty;
            _SiteVisitDate = string.Empty;
            _SitevisitTime = string.Empty;
            _Note1 = string.Empty;
            _Note2 = string.Empty;
            _CustomerNotes = string.Empty;
            _ExpenseAmount = string.Empty;
            EmployeeDetails = string.Empty;
            MeasurementDetails = string.Empty;
            OtherChargeDetails = string.Empty;
            CheckListSub = string.Empty;
            OtherChgTaxDetails = string.Empty;
            _FK_TaxGroup = string.Empty;
            _Date = string.Empty;
            _OtherCharge = string.Empty;
            _NetAmount = string.Empty;
            _FK_SiteVisitAssignment = string.Empty;
            _AsOnDate = string.Empty;
            _FK_PetyCashier = string.Empty;
            _FK_TransactionType = string.Empty;
            _RoundOff = string.Empty;
            _FK_BillType = string.Empty;
            _FK_PettyCashier = string.Empty;
            _ID_User = string.Empty;
            _ID_TokenUser = string.Empty;
        }
        #endregion
        #region Getters And Setters
        public string ID_TokenUser
        {
            get { return _ID_TokenUser; }
            set { _ID_TokenUser = value; }
        }
        public string ID_User
        {
            get { return _ID_User; }
            set { _ID_User = value; }
        }
        public string FK_PettyCashier
        {
            get { return _FK_PettyCashier; }
            set { _FK_PettyCashier = value; }
        }
        public string FK_BillType
        {
            get { return _FK_BillType; }
            set { _FK_BillType = value; }
        }
        public string RoundOff
        {
            get { return _RoundOff; }
            set { _RoundOff = value; }
        }
        public string FK_TransactionType
        {
            get { return _FK_TransactionType; }
            set { _FK_TransactionType = value; }
        }
        public string FK_PetyCashier
        {
            get { return _FK_PetyCashier; }
            set { _FK_PetyCashier = value; }
        }
        public string AsOnDate
        {
            get { return _AsOnDate; }
            set { _AsOnDate = value; }
        }
        public string FK_SiteVisitAssignment
        {
            get { return _FK_SiteVisitAssignment; }
            set { _FK_SiteVisitAssignment = value; }
        }
        public string NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        public string OtherCharge
        {
            get { return _OtherCharge; }
            set { _OtherCharge = value; }
        }
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public string IncludeTax
        {
            get { return _IncludeTax; }
            set { _IncludeTax = value; }
        }
        public string Amount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Amount); }
            set { _Amount = value; }
        }
        public string FK_TaxGroup
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_TaxGroup); }
            set { _FK_TaxGroup = value; }
        }
        public string FK_LeadGeneration
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_LeadGeneration); }
            set { _FK_LeadGeneration = value; }
        }
        public string SVInspectioncharge
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SVInspectioncharge); }
            set { _SVInspectioncharge = value; }
        }
        public string SiteVisitDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SiteVisitDate); }
            set { _SiteVisitDate = value; }
        }
        public string SitevisitTime
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_SitevisitTime); }
            set { _SitevisitTime = value; }
        }
        public string Note1
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Note1); }
            set { _Note1 = value; }
        }
        public string Note2
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Note2); }
            set { _Note2 = value; }
        }
        public string CustomerNotes
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CustomerNotes); }
            set { _CustomerNotes = value; }
        }
        public string ExpenseAmount
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ExpenseAmount); }
            set { _ExpenseAmount = value; }
        }
        public string EmployeeDetails { get; set; }
        public string MeasurementDetails { get; set; }
        public string OtherChargeDetails { get; set; }
        public string CheckListSub { get; set; }
        public string OtherChgTaxDetails {get;set;}
        public string PaymentDetail { get; set; }
        public string Reason
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Reason); }
            set { _Reason = value; }
        }
        public string Remarks
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Remarks); }
            set { _Remarks = value; }
        }
        public string StatusDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_StatusDate); }
            set { _StatusDate = value; }
        }
        public string CurrentStatus
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_CurrentStatus); }
            set { _CurrentStatus = value; }
        }
        public string EffectDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EffectDate); }
            set { _EffectDate = value; }
        }
        public string FK_Stage
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Stage); }
            set { _FK_Stage = value; }
        }
        public string FK_ProjectFollowUp
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_ProjectFollowUp); }
            set { _FK_ProjectFollowUp = value; }
        }
        public string ProjImageDescription
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProjImageDescription); }
            set { _ProjImageDescription = value; }
        }
        public string ProjImageType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProjImageType); }
            set { _ProjImageType = value; }
        }
        public string ProjImageName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProjImageName); }
            set { _ProjImageName = value; }
        }
        public string FK_SiteVisit
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_SiteVisit); }
            set { _FK_SiteVisit = value; }
        }
        public byte[] ProjImage
        {
            get { return _ProjImage; }
            set { _ProjImage = value; }
        }
        public string XMLdata { get; set; }
        public string Assignees { get; set; }
        public string ID_ProjectMaterialRequest
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ProjectMaterialRequest); }
            set { _ID_ProjectMaterialRequest = value; }
        }
       
        public string FK_Machine
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Machine); }
            set { _FK_Machine = value; }
        }
        public string FK_BranchCodeUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchCodeUser); }
            set { _FK_BranchCodeUser = value; }
        }
        public string ProMatRequestDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProMatRequestDate); }
            set { _ProMatRequestDate = value; }
        }
        public string FK_Team
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Team); }
            set { _FK_Team = value; }
        }
        public string FK_Employee
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Employee); }
            set { _FK_Employee = value; }
        }
        public string FK_Stages
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Stages); }
            set { _FK_Stages = value; }
        }
        
        public string ID_ProjectMaterialUsage
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_ProjectMaterialUsage); }
            set { _ID_ProjectMaterialUsage = value; }
        }
        public string ProMatUsageDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ProMatUsageDate); }
            set { _ProMatUsageDate = value; }
        }
        public string FK_Project
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Project); }
            set { _FK_Project = value; }
        }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
        }
        public string UserAction
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_UserAction); }
            set { _UserAction = value; }
        }
        public string EntrBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EntrBy); }
            set { _EntrBy = value; }
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
        public string BankHeader
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankHeader); }
            set { _BankHeader = value; }
        }
        public string Pincode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Pincode); }
            set { _Pincode = value; }
        }
        public string LeadNo
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LeadNo); }
            set { _LeadNo = value; }
        }
        public string FK_Master
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Master); }
            set { _FK_Master = value; }
        }
        public string LocationName
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_LocationName); }
            set { _LocationName = value; }
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

        public string BankKey
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankKey); }
            set { _BankKey = value; }
        }
        public string ID_CustomerWiseProductDetails
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_CustomerWiseProductDetails); }
            set { _ID_CustomerWiseProductDetails = value; }
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
        
        public string Critrea
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Critrea); }
            set { _Critrea = value; }
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
        public string TransMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransMode); }
            set { _TransMode = value; }
        }

        #endregion
        #region Data Access Function

        public LeadList BlLeadeList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProjectCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertLeadDetails(dt);
        }
        public ProjectList BlProjectList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalproAPIProjectSelect(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectDetails(dt);
        }
        public WorkTypeList BlWorkTypeList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertWorkTypeDetails(dt);
        }
        public MeasurementTypeList BlMeasurentTypeList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertMeasurementTypeDetails(dt);
        }
        public ProjectStagesList BlProjectStagesList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectStagesDetails(dt);
        }
        public ProjectTeamList BlProjectTeamList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectTeamDetails(dt);
        }
        public UnitList BlUnitList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertUnitDetails(dt);
        }
        public EmployeeListforProject BlEmployeeListforproject(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProjectCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertEmployeeListforProject(dt);
        }
        public ModeList BlModeList(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonPopupvalues(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertModeList(dt);
        }
        public UpdateMaterialUsage BlUpdateMaterialUsage(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalMaterialUsageUpdate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertMaterialUsage(dt);

        }
        public MatProductDetails BlMatProductDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();  
            DataTable dt = objDal.DalProjectCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertMatProductDetails(dt);
        }
        public MaterialRequestProductList BlMateriReqProductDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProjectCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertMaterialRequestProductDetails(dt);
        }
        public UpdateMaterialRequest BlUpdateMaterialRequest(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalMaterialRequestUpdate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertMaterialrequest(dt);

        }
        //public UpdateProjectFollowUp BlUpdateProjectFollowUp(BlProjectDetails objbl)
        //{

        //    DalProjectDetails objDal = new DalProjectDetails();
        //    DataTable dt = objDal.DalProjectFollowUpUpdate(objbl);
        //    objDal = null;
        //    return BlProjectDetailsFormat.ConvertProjectFollowUp(dt);

        //}
        //public MatProductDetails BlMatProductDetails(BlProjectDetails objbl)
        //{
        //    DalProjectDetails objDal = new DalProjectDetails();
        //    DataTable dt = objDal.DalProjectCommonValidate(objbl);
        //    objDal = null;
        //    return BlProjectDetailsFormat.ConvertMatProductDetails(dt);
        //} 
        public DownloadImage BlDownloadImage(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalDownloadImage(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertDownloadImage(dt);

        }
        public UpdateProjectFollowUp BlUpdateProjectFollowUp(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProjectFollowUpUpdate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertUpdateProjectFollowUp(dt);

        }
        public UpadateSiteVisit BlUpadateSiteVisit(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProjectSitevisit(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertUpadateSiteVisit(dt);

        }
        public ProjectStatus BlProjectStatus(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSearchCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectStatus(dt);

        }
        public ProjectOtherChargeDetails BlProjectOtherChargeDetails(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectOtherChargeDetails(dt);

        }
        public OtherChargeTaxCalculationDetails BlOtherChargeTaxCalculationDetails(BlProjectDetails objbl)
        {

            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertOtherChargeTaxCalculationDetails(dt);

        }
        public checkDetails BlcheckDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataSet dt = objDal.DalCommonValidates(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertcheckDetails(dt);
        }
        public UpdateProjectTransaction BlUpdateProjectTransaction(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable  dt = objDal.DalProjectTransactionUpdate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertUpdateProjectTransaction(dt);
        }
        public ProjectSiteVisitCount BlProjectSiteVisitCount(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSiteVisitAssignmentDetails(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectSiteVisitCount(dt);
        }
        public ProjectSiteVisitAssign BlProjectSiteVisitAssign(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalSiteVisitAssignmentDetails(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectSiteVisitAssign(dt);
        }
        public SiteVisitAssignViewDetails BlSiteVisitAssignViewDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataSet dt = objDal.DalCommonValidates(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertSiteVisitAssignViewDetails(dt);
        }
        public ProjectTransactionEmployeeDetails BlProjectTransactionEmployeeDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalEmployeeDetails(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertProjectTransactionEmployeeDetails(dt);
        }
        public TransactionTypeDetails BlTransactionTypeDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertTransactionTypeDetails(dt);
        }
        public BillTypeDetails BlBillTypeDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProGetBillType(objbl,0);
            objDal = null;
            return BlProjectDetailsFormat.ConvertBillTypeDetails(dt);
        }
        public PettyCashieDetails BlPettyCashieDetails(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalProGetBillType(objbl,1);
            objDal = null;
            return BlProjectDetailsFormat.ConvertPettyCashieDetails(dt);
        }
        public PaymentInformation BlPaymentInformation(BlProjectDetails objbl)
        {
            DalProjectDetails objDal = new DalProjectDetails();
            DataTable dt = objDal.DalCommonValidate(objbl);
            objDal = null;
            return BlProjectDetailsFormat.ConvertPaymentInformation(dt);
        }
        #endregion
    }
}
