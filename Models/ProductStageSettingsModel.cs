/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 07/11/2022
Purpose		: ProductStageSettings
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
    public class ProductStageSettingsModel
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

        public class ProductStageSettingsView
        {
            public long SlNo { get; set; }
            public long ProductStageSettingsID { get; set; }

            public long LeadGenerationID { get; set; }
            public long LeadNo { get; set; }

            public DateTime VisitDate { get; set; }

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

        }

        public class ProductStageSettingsInputFromViewModel
        {

            [Required(ErrorMessage = "No ProductStageSettings Selected")]
            public long ProductStageSettingsID { get; set; }

            public long LeadGenerationID { get; set; }

            public DateTime VisitDate { get; set; }

            public string VisitTime { get; set; }

            public string Note1 { get; set; }
            public string Note2 { get; set; }
            public string CusNote { get; set; }
            public string ExpenseAmount { get; set; }
            public string Remarks { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<EmployeeDetails> EmployeeDetails { get; set; }
            public List<MeasurementDetails> MeasurementDetails { get; set; }
            public List<ImageData> ImageList { get; set; }


        }

        public class ProductStageSettingsInfoView
        {
            [Required(ErrorMessage = "No ProductStageSettings Selected")]
            public Int64 ProductStageSettingsID { get; set; }
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

        public class ProductStageSettingsImages
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


        public static string _updateProcedureName = "proProductStageSettingsUpdate";
        public class UpdateProductStageSettings
        {

            public long FK_ProductStageSettings { get; set; }
            public long ID_ProductStageSettings { get; set; }
            public long FK_LeadGeneration { get; set; }
            public DateTime SVProductStageSettingsDate { get; set; }
            public string SVProductStageSettingsTime { get; set; }
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

        }

        public class SelectProductStageSettings
        {

            public long FK_ProductStageSettings { get; set; }
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
        public class ProductStageSettingsListModel
        {
            public List<Employee> EmployeeList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }
            public List<MeasurementTypeList> MeasurementTypeList { get; set; }
            public List<MeasurementUnitList> MeasurementUnitList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public DateTime VisitDate { get; set; }

            public string VisitTime { get; set; }
        }

        public static string _deleteProcedureName = "proProductStageSettingsDelete";
        public class DeleteProductStageSettings
        {
            public Int64 FK_ProductStageSettings { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputProductStageSettingsID
        {
            public Int64 FK_ProductStageSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int CheckList { get; set; }
            public string Name { get; set; }

        }
        public class BindOtherCharge
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }




        public Output UpdateProductStageSettingsData(UpdateProductStageSettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductStageSettings>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProductStageSettingsData(DeleteProductStageSettings input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductStageSettings>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProductStageSettingsSelect";
        public APIGetRecordsDynamic<ProductStageSettingsView> GetProductStageSettingsData(InputProductStageSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductStageSettingsView, InputProductStageSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<EmployeeDetails> GetProductStageSettingsEmployeeData(InputProductStageSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeDetails, InputProductStageSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MeasurementDetails> GetProductStageSettingsMeasurementData(InputProductStageSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MeasurementDetails, InputProductStageSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetProductStageSettingsOtherChargeData(InputProductStageSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, InputProductStageSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ProductStageSettingsImages> GetProductStageSettingsImageData(InputProductStageSettingsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductStageSettingsImages, InputProductStageSettingsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<EmployeeTypeList> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<ProductStageSettingsView> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductStageSettingsView, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelect", parameter: input);

        }
        //public APIGetRecordsDynamic<LeadGenerateActionModel.ProdProjDTL> GetSubProduct(GetLeadGen input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<LeadGenerateActionModel.ProdProjDTL, GetLeadGen>(companyKey: companyKey, procedureName: "ProGetLeadDetailsForProductStageSettings", parameter: input);

        //}

    }
}