/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 12/09/2022
Purpose		: MaterialUsage
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
    public class MaterialUsageModel
    {

        public class MaterialUsageView
        {
            public long SlNo { get; set; }
            public long ProjectMaterialUsageID { get; set; }
            public DateTime Date { get; set; }
         
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Department { get; set; }
            public string Project { get; set; }
            public string Stage { get; set; }
            public string Team { get; set; }
            public string Employee { get; set; }
            public DateTime ProjectDate { get; set; }
            public string TransMode { get; set; }
            public long ?LastID { get; set; }
        }
        public class MaterialUsageInputFromViewModel
        {

            [Required(ErrorMessage = "No MaterialUsage Selected")]
            public long ProjectMaterialUsageID { get; set; }

            public DateTime Date { get; set; }
          
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Department { get; set; }
           

            public List<MaterialDetails> MaterialDetails { get; set; }
            public List<ProductSerialNumbers> ProductSerialNumbers { get; set; }
            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public long? LastID { get; set; }
            public string TransMode { get; set; }

        }

        public class MaterialUsageInfoView
        {
            [Required(ErrorMessage = "No MaterialUsage Selected")]
            public Int64 ProjectMaterialUsageID { get; set; }
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
        public class ModeList
        {
            public long ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }
        public class MaterialDetails
        {
            public long ProductID { get; set; }
            public long StockId { get; set; }
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public string Mode { get; set; }
            public string ProductName { get; set; }
            public long Stock { get; set; }
            public long FK_EmployeeStock { get; set; }

            public long AMCFK_Master { get; set; }
            public long AMCMType { get; set; }
            public long AMCNoOfServices { get; set; }
            public DateTime? AMCMRenewduedate { get; set; }
            public DateTime? AMCMDuedate { get; set; }
            public decimal AmcTotalAmount { get; set; }
            public decimal AMCTaxTotalAmt { get; set; }
            public decimal AMCNetTotalAmt { get; set; }
            public string AMCRemarks { get; set; }
            public Boolean MapCustomer { get; set; }

        }
        public class WorkDetails
        {
            public long WorkType { get; set; }
            public string WorkTypeName { get; set; }
            public long DurationType { get; set; }
            public string DurationTypeName { get; set; }

            public long Duration { get; set; }
            public decimal WorkAmount { get; set; }


        }



        public static string _updateProcedureName = "proProjectMaterialUsageUpdate";
        public class UpdateMaterialUsage
        {
            public long FK_ProjectMaterialUsage { get; set; }
            public long ID_ProjectMaterialUsage { get; set; }
            public DateTime ProMatUsageDate { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stages { get; set; }
            public long FK_Team { get; set; }
            public long FK_Employee { get; set; }
            public string MaterialDetails { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long? LastID { get; set; }
            public string ProductSerialNumbers { get; set; }
            public string WarrantyDetails { get; set; }
        }

        public class SelectMaterialUsage
        {

            public long FK_ProjectMaterialUsage { get; set; }
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
        public class StageList
        {
            public string Mode { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }
        public class EmployeeList
        {
            public long EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }

        public class TeamList
        {
            public long ProjectID { get; set; }
            public long ID_ProjectTeam { get; set; }
            public string TeamName { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ModeTypeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }

        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }
      
        public class MaterialUsageListModel
        {

           
            public List<StageList> StageList { get; set; }
            public List<EmployeeList> EmployeeList { get; set; }
            public List<TeamList> TeamList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public DateTime VisitDate { get; set; }
            public List<ModeList> ModeList { get; set; }
            public long FK_Employee { get; set; }
            public List<ModeTypeList> ModeTypeList { get; set; }
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }
            public List<AMCTypeModel.AMCTypeView> AMCtype { get; set; }

        }

        public static string _deleteProcedureName = "proProjectMaterialUsageDelete";
        public class DeleteMaterialUsage
        {
            public Int64 FK_ProjectMaterialUsage { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputProjectMaterialUsageID
        {
            public Int64 FK_ProjectMaterialUsage { get; set; }
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

        public class GetSubProduct
        {
            public Int64 ID_Sales { get; set; } = 0;
            public string TransMode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }
            public Int64 StockID { get; set; }
            public int Mode { get; set; }
            public decimal Quantity { get; set; }
            public Int64 FK_Project { get; set; }
            public Int64 FK_Stage { get; set; }
            public Int64 FK_Team { get; set; }
            public Int64 FK_Employee { get; set; }
        }
        public class InclusiveProduct
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            public decimal SprodQuanity { get; set; }
            public decimal CurrentStock { get; set; }
            public Boolean SprodQtyFixed { get; set; }
            public long Number { get; set; }

            public long ID_ProductNumberingDetails { get; set; }
            public string ProdSerielNo { get; set; }
            public long FK_Stock { get; set; }

        }
        public class InputProduct
        {
            public Int64 ID_Sales { get; set; }
            public Int64 ID_ProjectMaterialUsage { get; set; }
            public Int64 ID_ProjectMaterialDetails { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }
            public Int64 StockID { get; set; }
            public int Mode { get; set; }
            public decimal Quantity { get; set; }
            public Int64 FK_Project { get; set; }
            public Int64 FK_Stage { get; set; }
            public Int64 FK_Team { get; set; }
            public Int64 FK_Employee { get; set; } = 0;
        }
        public class ProductSerialNumbers
        {
            public long FK_Stock { get; set; }
            public long FK_MasterID { get; set; }
            public long ID_ProductNumberingDetails { get; set; }
            public string UID { get; set; }
        }
        public class WarrantyDetails
        {
            public long SlNo { get; set; }
            public Int32 prodtid { get; set; }
            public Int32 stkid { get; set; }
            public Int32 subProductID { get; set; }
            public string subProName { get; set; }
            public long WarrantyType { get; set; }
            public string Replcwardt { get; set; }
            public string Serwardt { get; set; }
            public decimal WarrantyTaxAmount { get; set; }
            public decimal WarrantyAmount { get; set; }
            public decimal WarrantyNetAmount { get; set; }
        }
        public class GetSerialNumbers
        {
            public long FK_Stock { get; set; }
            public long FK_MasterID { get; set; }
            public long ID_ProductNumberingDetails { get; set; }
            public string ProdSerielNo { get; set; }
            public string UID { get; set; }
            public long ProductId { get; set; }
        }
        public class SelectSerialNo
        {
            public Int64 ID_Sales { get; set; } = 0;
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public int Mode { get; set; }
            public Int64 ID_ProjectMaterialDetails { get; set; }
        }
        public class GetWarranty
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 StockId { get; set; }
            public Int64 FK_Master { get; set; }


        }
        public class WarrantyDetailsReturn
        {
            public long SlNo { get; set; }
            public Int32 prodtid { get; set; }
            public Int32 stkid { get; set; }
            public Int32 subProductID { get; set; }
            public string subProName { get; set; }
            public long WarrantyType { get; set; }
            public string Replcwardt { get; set; }
            public string Serwardt { get; set; }
            public string WarrantyTaxAmount { get; set; } = "0";
            public string WarrantyAmount { get; set; } = "0";
            public string WarrantyNetAmount { get; set; } = "0";
        }
        public Output UpdateMaterialUsageData(UpdateMaterialUsage input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMaterialUsage>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMaterialUsageData(DeleteMaterialUsage input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMaterialUsage>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectMaterialUsageSelect";
        public APIGetRecordsDynamic<MaterialUsageView> GetMaterialUsageData(InputProjectMaterialUsageID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialUsageView, InputProjectMaterialUsageID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MaterialDetails> GetMaterialUsageMaterialData(InputProjectMaterialUsageID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialDetails, InputProjectMaterialUsageID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<InclusiveProduct> GetSubProducts(GetSubProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<InclusiveProduct, GetSubProduct>(companyKey: companyKey, procedureName: "ProGetSubProductInfo", parameter: input);

        }

        public APIGetRecordsDynamic<GetSerialNumbers> GetSerialNumber(SelectSerialNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetSerialNumbers, SelectSerialNo>(companyKey: companyKey, procedureName: "ProSalesSubProductSelect", parameter: input);

        }
        public APIGetRecordsDynamic<WarrantyDetailsReturn> GetWarrantySelect(GetWarranty input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetailsReturn, GetWarranty>(companyKey: companyKey, procedureName: "ProWarrantyDetailSelect", parameter: input);

        }



    }
}