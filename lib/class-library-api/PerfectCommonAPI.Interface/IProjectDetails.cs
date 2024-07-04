using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    
    public interface IProjectDetails
    {
        string ReqMode { get; set; }
        string XMLdata { get; set; }
        string OTP { get; set; }
        string Token { get; set; }
        string MPIN { get; set; }
        string OldMPIN { get; set; }
        string BankHeader { get; set; }
        string BankKey { get; set; }
        string FK_Master { get; set; }
        string SubMode { get; set; }
        string ID_CustomerWiseProductDetails { get; set; }
        string PageIndex { get; set; }
        string PageSize { get; set; }
        string Critrea1 { get; set; }
        string Critrea2 { get; set; }
        string Critrea3 { get; set; }
        string Critrea4 { get; set; }
        string ID { get; set; }
        string Critrea5 { get; set; }
        string Critrea6 { get; set; }
        string  MobileNumber { get; set; }
        string FK_Company { get; set; }
        string FK_Project { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        string FromDate { get; set; }
        string Todate { get; set; }
        string Pincode { get; set; }
        string CustomerNote { get; set; }
        string EmployeeNote { get; set; }
        string LocationName { get; set; }
        string LocLatitude { get; set; }
        string LocLongitude { get; set; }
        string EntrBy { get; set; }
        string LeadNo { get; set; }
        string TransMode { get; set; }
        string UserAction { get; set; }
        string ID_ProjectMaterialUsage { get; set; }
        string ProMatUsageDate { get; set; }
        string FK_Stages { get; set; }
        string FK_Team { get; set; }
        string FK_Employee { get; set; }
        string FK_BranchCodeUser { get; set; }
        string FK_Machine { get; set; }
        string SubProductDetails { get; set; }
        string ProMatRequestDate { get; set; }
        string ID_ProjectMaterialRequest { get; set; }
        byte[] ProjImage { get; set; }
        string ProjImageDescription { get; set; }
        string ProjImageType { get; set; }
        string ProjImageName { get; set; }
        string FK_SiteVisit { get; set; }
        string FK_ProjectFollowUp { get; set; }
        string FK_Stage { get; set; }
        string EffectDate { get; set; }
        string CurrentStatus { get; set; }
        string StatusDate { get; set; }
        string Remarks { get; set; }
        string Reason { get; set; }
        string FK_LeadGeneration { get; set; }
        string SVInspectioncharge { get; set; }
        string SiteVisitDate { get; set; }
        string SitevisitTime { get; set; }
        string Note1 { get; set; }
        string Note2 { get; set; }
        string CustomerNotes { get; set; }
        string ExpenseAmount { get; set; }
        string EmployeeDetails { get; set; }
        string MeasurementDetails { get; set; }
        string OtherChargeDetails { get; set; }
        string PaymentDetail { get; set; }
        string CheckListSub { get; set; }
        string OtherChgTaxDetails { get; set; }
        string FK_TaxGroup { get; set; }
        string Amount { get; set; }
        string IncludeTax { get; set; }
        string Date { get; set; }
        string OtherCharge { get; set; }
        string NetAmount { get; set; }
        string FK_SiteVisitAssignment { get; set; }
        string AsOnDate { get; set; }
        string FK_PetyCashier { get; set; }
        string FK_TransactionType { get; set; }
        string RoundOff { get; set; }
        string FK_BillType { get; set; }
        string FK_PettyCashier { get; set; }
        string ID_User { get; set; }
        string ID_TokenUser { get; set; }
    }
    #region Response Interface Model Objects  
   
    public class LeadList : CommonAPIR
    {
        public List<LeadListDetails> LeadListDetails { get; set; }
    }
    public class LeadListDetails
    {
        public Int64 ID_FIELD { get; set; }
        public string LeadNo { get; set; }
        public DateTime LeadDate { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }

    }
    public class ProjectList : CommonAPIR
    {
        public List<ProjectListDetails> ProjectListDetails { get; set; }
    }
    public class ProjectListDetails
    {
        public Int64 ID_Project { get; set; }
        public string ProjName { get; set; }
        public string  ShortName { get; set; }
        public string CreateDate { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string ProjectDate { get; set; }
        public decimal FinalAmount { get; set; }
        public string DueDate { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
    }
    public class WorkTypeList : CommonAPIR
    {
        public List<WorkTypeListDetails> WorkTypeListDetails { get; set; }
    }
    public class WorkTypeListDetails
    {
        public Int64 WorkTypeID { get; set; }
        public string WorkType { get; set; }
    }
    public class MeasurementTypeList : CommonAPIR
    {
        public List<MeasurementTypeListDetails> MeasurementTypeListDetails { get; set; }

    }
    public class MeasurementTypeListDetails
    {
        public Int64 MeasurementTypeID { get; set; }
        public string MeasurementType { get; set; }
    }
    public class ProjectStagesList : CommonAPIR
    {
        public List<ProjectStagesListDetails> ProjectStagesListDetails { get; set; }

    }
    public class ProjectStagesListDetails
    {
        public Int64 ProjectStagesID { get; set; }
        public string StageName { get; set; }
        public string DueDate { get; set; }
    }
    public class ProjectTeamList : CommonAPIR
    {
        public List<ProjectTeamListDetails> ProjectTeamListDetails { get; set; }

    }
    public class ProjectTeamListDetails
    {
        public Int64 ID_ProjectTeam { get; set; }
        public string TeamName { get; set; }
    }
    public class UnitList : CommonAPIR
    {
        public List<UnitListDetails> UnitListDetails { get; set; }

    }
    public class UnitListDetails
    {
        public Int64 MeasurementUnitID { get; set; }
        public string MeasurementUnit { get; set; }
    }
    public class EmployeeListforProject : CommonAPIR
    {
        public List<EmployeeListforProjectDetails> EmployeeListforProjectDetails { get; set; }

    }
    public class EmployeeListforProjectDetails
    {
        public Int64 ID_FIELD { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
    }
    public class ModeList : CommonAPIR
    {
        public List<ModeListDetails> ModeListDetails { get; set; }

    }
    public class ModeListDetails
    {
        public Int64 ID_Mode { get; set; }
        public string ModeName { get; set; }
        
    }
    public class UpdateMaterialUsage : CommonAPIR
    {
        public string Message { get; set; }
    }
    
    public class MaterialUsageData
    {
        public string BankKey { get; set; }
        public string UserAction { get; set; }
        public string TransMode { get; set; }
        public string ID_ProjectMaterialUsage { get; set; }
        public string ProMatUsageDate { get; set; }
        public string FK_Project { get; set; }
        public string FK_Stages { get; set; }
        public string FK_Team { get; set; }
        public string FK_Employee { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string FK_Machine { get; set; }
        public string ID_TokenUser { get; set; }
        public string Token { get; set; }
        public List<MatUsageProductDetails> MatUsageProductDetails { get; set; }
        public string MatUsageProductsDetails { get; set; }
    }
    public class UpdateMaterialRequest : CommonAPIR
    {
        public string Message { get; set; }
    }

    public class MaterialRequestData
    {
        public string BankKey { get; set; }
        public string UserAction { get; set; }
        public string TransMode { get; set; }
        public string ID_ProjectMaterialUsage { get; set; }
        public string ProMatUsageDate { get; set; }
        public string FK_Project { get; set; }
        public string FK_Stages { get; set; }
        public string FK_Team { get; set; }
        public string FK_Employee { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string FK_Machine { get; set; }
        public string ProMatRequestDate { get; set; }
        public string ID_TokenUser { get; set; }
        public string Token { get; set; }
        public List<MaRequestProductDetails> MaRequestProductDetails  { get; set; }
        
        public string MatRequestProductsDet { get; set; }
    }
    
    public class MatUsageProductDetails
    {
        public string ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string StockId { get; set; }
        public string Mode { get; set; }

    }
    

    public class MaRequestProductDetails
    {
        public string ProductID { get; set; }
        public decimal Quantity { get; set; }
        public string StockId { get; set; }
        public string Mode { get; set; }

    }
    public class MatProductDetails : CommonAPIR
    {
        public List<MatProductListDetails> MatProductListDetails { get; set; }

    }
    public class MatProductListDetails
    {
        public string ProductID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MRP { get; set; }
        public string SalesPrice { get; set; }
        public string AssignedStock { get; set; }
        public string AvailableStock { get; set; }
        public string StockId { get; set; }
        public string Department { get; set; }
    }
    public class MaterialRequestProductList : CommonAPIR
    {
        public List<MaterialRequestProductListDetails> MaterialRequestProductListDetails { get; set; }

    }
    public class MaterialRequestProductListDetails
    {
        public string ProductID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string MRP { get; set; }
        public string SalesPrice { get; set; }
        public string AssignedStock { get; set; }
        public string CurrentStock { get; set; }
        public string StockId { get; set; }
        public string Department { get; set; }
    }
    public class UpdateProjectFollowUp : CommonAPIR
    {
        public Int64 FK_ProjectFollowUp { get; set; }
    }
    public class ProjectFollowUpList
    {
        public string BankKey { get; set; }
        public string FK_Project { get; set; }
        public string FK_Stages { get; set; }
        public string FK_Machine { get; set; }
        public string UserAction { get; set; }
        public string ID_ProjectFollowUp { get; set; }
        public string FollowUpDate { get; set; }
        public string CurrentStatus { get; set; }
        public string DueDate { get; set; }
        public string StatusDate { get; set; }
        public string Reason { get; set; }
        public string Remarks { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
     
    }
    public class DownloadImage:CommonAPIR
    {
        public Int64 FK_ProjectImage { get; set; }

    }
    public class UpadateSiteVisit : CommonAPIR
    {
        public Int64 FK_SiteVisit { get; set; }
    }
    public class SiteVisitInput
    {
        public string EntrBy { get; set; }
        public string BankKey { get; set; }
         public string UserAction { get; set; }
        public string FK_LeadGeneration { get; set; }

        public string VisitDate { get; set; }

        public string VisitTime { get; set; }
        public string TransMode { get; set; }

        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string CusNote { get; set; }
        public string ExpenseAmount { get; set; }
        public string Remarks { get; set; }
        public List<pssOtherCharge> pssOtherCharge { get; set; }
        public List<pssOtherChargeTax> pssOtherChargeTax { get; set; }
        public List<EmpDetails> EmployeeDetails { get; set; }
        public List<MeasurementDetails> MeasurementDetails { get; set; }
        public List<ImageListView> ImageList { get; set; }
        public List<CheckListSub> CheckListSub { get; set; } 
        public string SVExpenseAmount { get; set; }
        public string FK_SiteVisitAssignment { get; set; }
        public string LastID { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string Inspectioncharge { get; set; }
        public string ID_TokenUser { get; set; }
        public string Token { get; set; }

    }
    public class pssOtherCharge
    {
        public long ID_OtherChargeType { get; set; }
        public long OctyTransType { get; set; }
        public long FK_TaxGroup { get; set; }
        public decimal OctyAmount { get; set; }
        public decimal OctyTaxAmount { get; set; }
        public bool OctyIncludeTaxAmount { get; set; }
    }
    public class pssOtherChargeTax
    {
        public long ID_OtherChargeType { get; set; }
        public long ID_TaxSettings { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxPercentage { get; set; }
        public long TaxGrpID { get; set; }
        public long FK_TaxType { get; set; }
        public string TaxTyName { get; set; }
    }

    public class EmpDetails
    {
        public long EmployeeID { get; set; }
        public long EmployeeType { get; set; }
        public long Department { get; set; }
        public string DepartmentName { get; set; }
        public string Employee { get; set; }
        public string EmployeeTypeName { get; set; }

    }
    public class MeasurementDetails
    {
        public long WorkType { get; set; }
        public string WorkTypeName { get; set; }
        public long MeasurementType { get; set; }
        public string MeasurementTypeName { get; set; }
        public decimal MeasurementValue { get; set; }
        public long MeasurementUnit { get; set; }
        public string MeasurementUnitName { get; set; }
        public string MDRemarks { get; set; }

    }
    public class ImageListView
    {
        public long SLNo { get; set; }
        public string TransMode { get; set; }
        public long FK_Master { get; set; }
        public long FK_Customer { get; set; }
        public long FK_Product { get; set; }
        public long FK_SubProduct { get; set; }
        public string ProdImageName { get; set; }
        public string ProdImage { get; set; }
        public string ProdImageMode { get; set; }
        public string ProdImageDescription { get; set; }
        public long ID_ProductImage { get; set; }
        public long WarrantyType { get; set; }
        public string WarTypName { get; set; }
        public string ProdImageType { get; set; }

    }
    public class CheckListSub //nj
    {
        public long FK_CheckList { get; set; }
        public long FK_CheckListType { get; set; }
    }
    public class ProjectStatus : CommonAPIR
    {
        public List<ProjectStatusList> ProjectStatusList { get; set; }
    }
    public class ProjectStatusList
    {
        public Int32 FK_Status { get; set; }
        public string StatusName { get; set; }
    }
    public class ProjectOtherChargeDetails : CommonAPIR
    {
        public List<ProjectOtherChargeDetailsList> ProjectOtherChargeDetailsList { get; set; }
    }
    public class ProjectOtherChargeDetailsList
    {
        public Int64 SlNo { get; set; }
        public Int64 ID_OtherChargeType { get; set; }
        public string OctyName { get; set; }
        public Int16 OctyTransTypeActive { get; set; }
        public Int16 OctyTransType { get; set; }
        public Int64 FK_TaxGroup { get; set; }
        public decimal OctyAmount { get; set; }
        public decimal OctyTaxAmount { get; set; }
        public string OctyIncludeTaxAmount { get; set; }
    }
    public class OtherChargeTaxCalculationDetails : CommonAPIR
    {
        public List<OtherChargeTaxCalculationDetailsList> OtherChargeTaxCalculationDetailsList { get; set; }
    }
    public class OtherChargeTaxCalculationDetailsList
    {
        public Int32 SlNo { get; set; }
        public Int64 ID_TaxSettings { get; set; }
        public string FK_TaxType { get; set; }
        public string TaxTyName { get; set; }
        public string TaxPercentage { get; set; }
        public bool TaxtyInterstate { get; set; }
        public decimal TaxUpto { get; set; }
        public bool TaxUptoPercentage { get; set; }
        public decimal Amount { get; set; }
    }
    public class checkDetails:CommonAPIR
    {
        public List<checkDetailsList> checkDetailsList { get; set; }
    }
    public class checkDetailsList
    {
        public Int64 ID_CheckListType { get; set; }
        public string CLTyName { get; set; }
        public List<SubArrary> SubArrary { get; set; }
    }
    public class SubArrary
    {
        public Int64 ID_CheckListType { get; set; }
        public Int64 ID_CheckList { get; set; }      
        public string CkLstName { get; set; }
    }
    public class ProjectTransaction
    {
        public List<pssOtherChargeTax> pssOtherChargeTax { get; set; }
        public string UserAction { get; set; }
        public string Date { get; set; }
        public string FK_Project { get; set; }
        public string FK_Stage { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string BankKey { get; set; }
        public string Remark { get; set; }
        public string OtherCharge { get; set; }
        public List<PaymentDetails> PaymentDetail { get; set; }
        public string NetAmount { get; set; }
        public List<pssOtherCharge> pssOtherCharge { get; set; }
        public string FK_TransactionType { get; set; }
        public string FK_Employee { get; set; }
        public string RoundOff { get; set; }
        public string FK_BillType { get; set; }
        public string FK_PettyCashier { get; set; }
        public  string ID_TokenUser { get; set; }
        public string Token { get; set; }
    }
    public class UpdateProjectTransaction:CommonAPIR
    {
        public Int64 FK_ProjectTransaction { get; set; }
    }
    public class ProjectSiteVisitCount:CommonAPIR
    {
       public List<ProjectSiteVisitCountDetail> ProjectSiteVisitCountDetail { get; set; }
    }
    public class ProjectSiteVisitCountDetail
    {
        public Int32 Mode { get; set; }
        public string Label_Name { get; set; }
        public Int32 Count { get; set; }
    }
    public class ProjectSiteVisitAssign:CommonAPIR
    {
        public List<ProjectSiteVisitAssignList> ProjectSiteVisitAssignList { get; set; }
    }
    public class ProjectSiteVisitAssignList
    {
        public Int64 ID_LeadGenerate { get; set; }
        public string LeadNo { get; set; }
        public string LeadDate { get; set; }
        public string CustomeName { get; set; }
        public string MobileNo { get; set; }
        public string CusAddress { get; set; }
        public Int64 FK_Customer { get; set; }
        public Int64 FK_CustomerOthers { get; set; }
        public Int64 ID_SiteVisitAssignment { get; set; }
        public string SiteVisitDate { get; set; }
        public string ExpenseAmount { get; set; }
        public string IsSiteVisit { get; set; }
    }
    public class SiteVisitAssignViewDetails:CommonAPIR
    {
        public AssignDetails AssignDetails { get; set; }
        public List<AssignEmployeeDetails> AssignEmployeeDetails { get; set; }
    }
    public class AssignDetails
    {
        public Int64 ID_SiteVisitAssignment { get; set; }
        public Int64 LeadGenerationID { get; set; }
        public string SiteVisitDate { get; set; }
        public string VisitTime { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string ExpenseAmount { get; set; }
        public string LeadNo { get; set; }
       // public string InspectionType { get; set; }
    }
    public class AssignEmployeeDetails
    {
        public string EmployeeID { get;set;}
        public string EmployeeName { get; set; }
        public string EmployeeType { get; set; }
        public string EmployeeTypeName { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
    public class ProjectTransactionEmployeeDetails:CommonAPIR

    {
        public List<ProjectTransactionEmployeeDetailsList> ProjectTransactionEmployeeDetailsList { get; set; }
    }

    public class ProjectTransactionEmployeeDetailsList
    {
        public string FK_Employee { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
    }
    public class TransactionTypeDetails:CommonAPIR
    {
        public List<TransactionTypeList> TransactionTypeList { get; set; }
    }
  public class TransactionTypeList
    {
        public string FK_TransactionType { get; set; }
        public string TransactionTypename { get; set; }
    }
    public class BillTypeDetails:CommonAPIR
    {
        public List<BillTypeDetailsList> BillTypeDetailsList { get; set; }
    }
    public class BillTypeDetailsList
    {
        public string ID_BillType { get; set; }
        public string BTName { get; set; }
    }
    public class PettyCashieDetails : CommonAPIR
    {
        public List<PettyCashieList> PettyCashieList { get; set; }
    }
    public class PettyCashieList
    {
        public string ID_PettyCashier { get; set; }
        public string PtyCshrName { get; set; }
    }
    public class PaymentInformation : CommonAPIR
    {
        public List<PaymentInformationList> PaymentInformationList { get; set; }
    }
    public class PaymentInformationList
    {
        public string FK_PaymentInformation { get; set; }
        public string TypeName { get; set; }
        public string Amount { get; set; }
    }
    #endregion

}


           