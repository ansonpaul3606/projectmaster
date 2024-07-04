/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 19/07/2022
Purpose		: CostPreparation
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
    public class CostPreparationModel
    {
        
        public class CostPreparationView
        {
            public long SlNo { get; set; }
            public long CostPreparationID { get; set; }

            public long LeadGenerationID { get; set; }

            public DateTime VisitDate { get; set; }
            
            public decimal MaterialCost { get; set; }
            public decimal WorkCost { get; set; }
            public decimal OtherCharges { get; set; }
            public decimal AdditionalCost { get; set; }
            public decimal TotalAmount { get; set; }
            
            public string Remarks { get; set; }
            public Int64 TotalCount { get; set; }
            public string LeadNo { get; set; }
            public string TransMode { get; set; }
            public DateTime LeadDates { get; set; }
            public long? LastID { get; set; }
            public long FK_Category { get; set; }
            public long FK_BOMProject { get; set; }
            public long FK_Quotation { get; set; }
            public long? ImportFrom { get; set; }
            public long FK_Branch { get; set; }
            public Int64 ID_OtherChargeType { get; set; }
            public string OctyName { get; set; }
            public Int64 OctyTransType { get; set; }
            public decimal OctyAmount { get; set; }
        }

        public class CostPreparationInputFromViewModel
        {

            [Required(ErrorMessage = "No CostPreparation Selected")]
            public long CostPreparationID { get; set; }

            public long LeadGenerationID { get; set; }

            public DateTime VisitDate { get; set; }



            public decimal MaterialCost { get; set; }
            public decimal WorkCost { get; set; }
            public decimal OtherCharges { get; set; }
            public decimal AdditionalCost { get; set; }
            public decimal TotalAmount { get; set; }
            
            public string Remarks { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<EmployeeDetails> EmployeeDetails { get; set; }
            public List<MaterialDetails> MaterialDetails { get; set; }
            public List<WorkDetails> WorkDetails { get; set; }
            public List<ImageData> ImageList { get; set; }
            public string TransMode { get; set; }
            public long? LastID { get; set; }
             
            public long FK_Category { get; set; }
            public long FK_BOMProject { get; set; }
            public long FK_Quotation { get; set; }
            public long? ImportFrom { get; set; }
            public long FK_Branch { get; set; }

        }

        public class CostPreparationInfoView
        {
            [Required(ErrorMessage = "No CostPreparation Selected")]
            public Int64 CostPreparationID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }
        public class EmployeeDetails
        {
            public long EmployeeID { get; set; }
            public string EmployeeTypeName { get; set; }
            public string DepartmentName { get; set; }
            public long Department { get; set; }
            public string Employee { get; set; }
            public long EmployeeType { get; set; }


        }
        public class MaterialDetails
        {
            public long ProductID { get; set; }
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }

            public string ProductName { get; set; }
            public long StockId { get; set; }
            public decimal SalePrice { get; set; }

        }
        public class WorkDetails
        {
            public long WorkType { get; set; }
            public string WorkTypeName { get; set; }
            public long DurationType { get; set; }
            public string DurationTypeName { get; set; }

            public decimal Duration { get; set; }
            public decimal WorkAmount { get; set; }
            
            
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

        public class CostPreparationImages
        {

            public Int64 ID_ProjectImage { get; set; }
            public int ImgMode { get; set; }
            public string ImgValue { get; set; }
            public string ImgName { get; set; }
            public string ModeName { get; set; }
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


        public static string _updateProcedureName = "proProjectCostPreparationUpdate";
        public class UpdateCostPreparation
        {

            public long FK_ProjectCostPreparation { get; set; }
            public long ID_ProjectCostPreparation { get; set; }
            public long FK_LeadGeneration { get; set; }
            public DateTime PCPVisitDate { get; set; }
            public decimal PCPMaterialCost { get; set; }
            public decimal PCPWorkCost { get; set; }
            public decimal PCPOtherCharges { get; set; }
            public decimal PCPAdditionalCost { get; set; }
            public decimal PCPTotalAmount { get; set; }
            
            public string PCPRemarks { get; set; }
            public string EmployeeDetails { get; set; }
            public string MaterialDetails { get; set; }
            public string WorkDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string ImageList { get; set; }
            public long? LastID { get; set; }

            public Int64 FK_Category { get; set; }
            public Int64 FK_BOMProject { get; set; }
            public long? ImportFrom { get; set; }

        }

        public class SelectCostPreparation
        {

            public long FK_CostPreparation { get; set; }
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
        
        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }
        public class DurationTypeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class CostPreparationListModel
        {
            public List<Employee> EmployeeList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }
            public List<DurationTypeList> DurationTypeList { get; set; }
           
            public List<WorkTypeList> WorkTypeList { get; set; }
            public DateTime VisitDate { get; set; }
            public List<Category> CategoryList { get; set; }
            public long FK_Quotation { get; set; }
            public long ImportID { get; set; }
            public long FK_Branch { get; set; }

        }
        public class Category
        {
            public string Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }
        public static string _deleteProcedureName = "proProjectCostPreparationDelete";
        public class DeleteCostPreparation
        {
            public Int64 FK_ProjectCostPreparation { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputCostPreparationID
        {
            public Int64 FK_ProjectCostPreparation { get; set; }
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

        public class BOMProjectFill
        {

            public int Mode { get; set; }

            public long FK_Master { get; set; }
            public long FK_Category { get; set; }
            public long ID_BOMProject { get; set; }
            public long FK_Company { get; set; }

        }
        public class BOMList
        {
            public long ID_BOMProject { get; set; }
            public string BOMName { get; set; }
        }
        public Output UpdateCostPreparationData(UpdateCostPreparation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCostPreparation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCostPreparationData(DeleteCostPreparation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCostPreparation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectCostPreparationSelect";
        public APIGetRecordsDynamic<CostPreparationView> GetCostPreparationData(InputCostPreparationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostPreparationView, InputCostPreparationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<EmployeeDetails> GetCostPreparationEmployeeData(InputCostPreparationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeDetails, InputCostPreparationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MaterialDetails> GetCostPreparationMaterialData(InputCostPreparationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialDetails, InputCostPreparationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<WorkDetails> GetCostPreparationWorkData(InputCostPreparationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<WorkDetails, InputCostPreparationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetCostPreparationOtherChargeData(InputCostPreparationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, InputCostPreparationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<CostPreparationImages> GetCostPreparationImageData(InputCostPreparationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostPreparationImages, InputCostPreparationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<EmployeeTypeList> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<CostPreparationView> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostPreparationView, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelect", parameter: input);

        }
        public APIGetRecordsDynamicdn<dynamic> FillBOMProject(BOMProjectFill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<BOMProjectFill>(companyKey: companyKey, procedureName: "ProBOMProjectDataFill", parameter: input);

        }
        public class ProjectfillQuoation
        {



            public long FK_Master { get; set; }

        }
        public APIGetRecordsDynamicdn<dynamic> FilleadQuotation(ProjectfillQuoation input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<ProjectfillQuoation>(companyKey: companyKey, procedureName: "ProProjectQuotationDataFill", parameter: input);

        }
    }
}