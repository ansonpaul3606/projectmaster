/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 12/07/2022
Purpose		: SiteVisit
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class SiteVisitModel
    {
        //public class GetLeadGen
        //{
        //    public Int64 ID_LeadGenerate { get; set; }
        //    public string EntrBy { get; set; }
        //    public Int64 FK_Company { get; set; }
        //    public Int64 FK_Machine { get; set; }

        //    public string TransMode { get; set; }
        //    public Int32 PageIndex { get; set; }
        //    public Int32 PageSize { get; set; }

        //    public string Name { get; set; }
        //}

        public class SiteVisitView
        {
            public long SlNo { get; set; }
            public long SiteVisitID { get; set; }

            public long LeadGenerationID { get; set; }
            public string LeadNo { get; set; }

            public DateTime VisitDate { get; set; }
            public DateTime LeadDates { get; set; }
            public string VisitTime { get; set; }

            public string Note1 { get; set; }
            public string Note2 { get; set; }
            public string CusNote { get; set; }
            public string ExpenseAmount { get; set; }
            public string Remarks { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public Int64 OctyTransType { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransMode { get; set; }
            public long FK_Lead { get; set; }
            public string SitevisitassignTransmode { get; set; }
            public decimal ?SVExpenseAmount { get; set; }
            public long ?FK_SiteVisitAssignment { get; set; }
            public long ?LastID { get; set; }
            public int SVInspectionType { get; set; }
        }

        public class SiteVisitInputFromViewModel
        {

            [Required(ErrorMessage = "No SiteVisit Selected")]
            public long SiteVisitID { get; set; }

            public long LeadGenerationID { get; set; }

            public DateTime VisitDate { get; set; }

            public string VisitTime { get; set; }
            public string TransMode { get; set; }

            public string Note1 { get; set; }
            public string Note2 { get; set; }
            public string CusNote { get; set; }
            public string ExpenseAmount { get; set; }
            public string Remarks { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<EmployeeDetails> EmployeeDetails { get; set; }
            public List<MeasurementDetails> MeasurementDetails { get; set; }
            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }
            public List<CheckListSub> CheckListSub { get; set; } //nj
            public decimal ?SVExpenseAmount { get; set; }
            public long ?FK_SiteVisitAssignment { get; set; }
            public long? LastID { get; set; }
            public long InspectionType { get; set; }

        }
        public class CheckListSub //nj
        {
            public long FK_CheckList { get; set; }
            public long FK_CheckListType { get; set; }
        }
        public class SiteVisitInfoView
        {
            [Required(ErrorMessage = "No SiteVisit Selected")]
            public Int64 SiteVisitID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }
        public class EmployeeDetails
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
        public class OtherCharges
        {
            public long SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public Int64 OctyTransType { get; set; }
            public string OctyName { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransType { get; set; }
            public Int64 TransTypeID { get; set; }
        }

        public class SiteVisitImages
        {

            public Int64 ID_ProjectImage { get; set; }
            public int SVImgMode { get; set; }
            public string SVImgValue { get; set; }
            public string SVImgName { get; set; }
            public string SVModeName { get; set; }
        }
        public class ImageData
        {

            public Int64 ID_CompanyImage { get; set; }
            public long FK_Company { get; set; }
            public int ComImgMode { get; set; }

            public string ComImg { get; set; }
            public string ComImgName { get; set; }

            public byte[] ComImgValue { get; set; }
        }


        public static string _updateProcedureName = "proSiteVisitUpdate";
        public class UpdateSiteVisit
        {

            public long FK_SiteVisit { get; set; }
            public long ID_SiteVisit { get; set; }
            public long FK_LeadGeneration { get; set; }
            public DateTime SVSiteVisitDate { get; set; }
            public string SVSitevisitTime { get; set; }
            public string SVInspectionNote1 { get; set; }

            public string SVInspectionNote2 { get; set; }
            public string SVCustomerNotes { get; set; }
            public string SVExpenseAmount { get; set; }
            public string SVRemarks { get; set; }
            public string EmployeeDetails { get; set; }
            public string MeasurementDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public string ImageList { get; set; }
            public string CheckListSub { get; set; }
            public string OtherChgTaxDetails { get; set; }
            public decimal SVInspectioncharge { get; set; }
            public long ?FK_SiteVisitAssignment { get; set; }
            public long? LastID { get; set; }
            public long InspectionType { get; set; }
        }

        public class SelectSiteVisit
        {

            public long FK_SiteVisit { get; set; }
            public string PrStName { get; set; }
            public string PrStShortName { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }

            public Int64 FK_Machine { get; set; }
            public string UserCode { get; set; }
            public Int32 BranchCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 BackupId { get; set; }
            public bool Cancelled { get; set; }

        }
        public class Employee
        {
            public Int32 EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
        }
        public class EmployeeTypeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class MeasurementTypeList
        {
            public short MeasurementTypeID { get; set; }
            public string MeasurementType { get; set; }

        }
        public class MeasurementUnitList
        {
            public short MeasurementUnitID { get; set; }
            public string MeasurementUnit { get; set; }

        }
        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }

        public class CheckList//nj
        {

            public long FK_CheckListType { get; set; }
            public long ID_CheckList { get; set; }
            //public long ChkCount { get; set; }
            public string CkLstName { get; set; }


        }
        public class CheckListType//nj
        {
            public long ID_CheckListType { get; set; }

            public string CLTyName { get; set; }

        }

        public class SiteVisitListModel
        {
            public List<Employee> EmployeeList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }
            public List<MeasurementTypeList> MeasurementTypeList { get; set; }
            public List<MeasurementUnitList> MeasurementUnitList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public List<CheckList> CheckList { get; set; }//nj
            public List<CheckListType> CheckListType { get; set; }//nj
            public DateTime VisitDate { get; set; }

            public string VisitTime { get; set; }
            public List<InspectionList> InspectionListData { get; set; }
            public long ProjectID { get; set; }
        }

        public static string _deleteProcedureName = "proSiteVisitDelete";
        public class DeleteSiteVisit
        {
            public Int64 FK_SiteVisit { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputSiteVisitID
        {
            public Int64 FK_SiteVisit { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public Int64 CheckList { get; set; }
            public string Name { get; set; }

        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
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


        public Output UpdateSiteVisitData(UpdateSiteVisit input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSiteVisit>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSiteVisitData(DeleteSiteVisit input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSiteVisit>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proSiteVisitSelect";
        public APIGetRecordsDynamic<SiteVisitView> GetSiteVisitData(InputSiteVisitID input, string companyKey)
        {
            return Common.GetDataViaProcedure<SiteVisitView, InputSiteVisitID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<EmployeeDetails> GetSiteVisitEmployeeData(InputSiteVisitID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeDetails, InputSiteVisitID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MeasurementDetails> GetSiteVisitMeasurementData(InputSiteVisitID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MeasurementDetails, InputSiteVisitID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetSiteVisitOtherChargeData(InputSiteVisitID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, InputSiteVisitID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ImageListView> GetSiteVisitImageData(InputSiteVisitID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ImageListView, InputSiteVisitID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<EmployeeTypeList> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<SiteVisitView> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<SiteVisitView, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelect", parameter: input);

        }
        //public APIGetRecordsDynamic<LeadGenerateActionModel.ProdProjDTL> GetSubProduct(GetLeadGen input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<LeadGenerateActionModel.ProdProjDTL, GetLeadGen>(companyKey: companyKey, procedureName: "ProGetLeadDetailsForSiteVisit", parameter: input);

        //}
        //nj
        public class Inputprintchecklist
        {
            public long FK_SiteVisit { get; set; }

            public Int64 FK_Company { get; set; }
            public int Mode { get; set; }

        }
        public class Outputprintchecklist
        {
            public string CheckListType { get; set; }
            public string CheckList { get; set; }
            public Boolean Checked { get; set; }


        }
        public class OutCustomerdetails
        {
            public string Customer { get; set; }
            public string Location { get; set; }
            public string ProjectCode { get; set; }
            public string MobileNo { get; set; }
            public string ContactNo { get; set; }
            public string Measuredatetime { get; set; }
            public string Requirement { get; set; }
            public string Measuredby { get; set; }
            public string Payment { get; set; }
        }
        public class ModeValue
        {
            public Int32 Mode { get; set; }
        }
        public class InspectionList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public APIGetRecordsDynamic<Outputprintchecklist> GetChecklistPrint(Inputprintchecklist input, string companyKey)
        {
            return Common.GetDataViaProcedure<Outputprintchecklist, Inputprintchecklist>(companyKey: companyKey, procedureName: "ProRptSiteVisitCheckList", parameter: input);

        }
        public APIGetRecordsDynamic<OutCustomerdetails> GetChecklistcusPrint(Inputprintchecklist input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutCustomerdetails, Inputprintchecklist>(companyKey: companyKey, procedureName: "ProRptSiteVisitCheckList", parameter: input);

        }
        public APIGetRecordsDynamic<InspectionList> GetInspectionListData(ModeValue input, string companyKey)
        {
            return Common.GetDataViaProcedure<InspectionList, ModeValue>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
    }
}