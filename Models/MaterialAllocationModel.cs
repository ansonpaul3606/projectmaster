/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 22/08/2022
Purpose		: MaterialAllocation
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
    public class MaterialAllocationModel
    {

        public class MaterialAllocationView
        {
            public long SlNo { get; set; }
            public long ProjectMaterialAllocationID { get; set; }
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
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public long RequestMode { get; set; }
            public DateTime ProjectDate { get; set; }

            public long?FK_ProjectMaterialRequest { get; set; }
            public long ID_FIELD { get; set; }
            public string RequestNo { get; set; }
            public long FK_Branch { get; set; }

            public Int64? LastID { get; set; }
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public long FK_Category { get; set; }
            public long FK_BOMProject { get; set; }
        }
        public class MaterialAllocationInputFromViewModel
        {

            [Required(ErrorMessage = "No MaterialAllocation Selected")]
            public long ProjectMaterialAllocationID { get; set; }

            public DateTime Date { get; set; }

            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Department { get; set; }

            public long RequestMode { get; set; }
            public List<MaterialDetails> MaterialDetails { get; set; }
            public long ?FK_ProjectMaterialRequest { get; set; }
            public Int64? LastID { get; set; }
            public string TransMode { get; set; }
            public List<ProductSerialNumbers> ProductSerialNumbers { get; set; }
            public long FK_Category { get; set; } = 0;
            public long FK_BOMProject { get; set; } = 0;
            public long FK_Branch { get; set; } = 0;

        }
        public class ProductSerialNumbers
        {
            public long FK_Stock { get; set; }
            public long FK_MasterID { get; set; }
            public long ID_ProductNumberingDetails { get; set; }
            public string UID { get; set; }
        }
        public class MaterialAllocationInfoView
        {
            [Required(ErrorMessage = "No MaterialAllocation Selected")]
            public Int64 ProjectMaterialAllocationID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class EmployeeDetails
        {
            public long FK_Employee { get; set; }
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
            public decimal SalePrice { get; set; }
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            public string Mode { get; set; }
            // public string ProductName { get; set; }
            public long StandByProduct { get; set; }
            public decimal StandByQuantity { get; set; }
            public long StandByStockId { get; set; }
            public string StandByProdName { get; set; }
            public decimal CrntQnty { get; set; }
            public decimal CrntQntys { get; set; }
            public string ProdNumbering { get; set; }


        }
        public class Productview {

            public long SLNo { get; set; }
            public string ProductID { get; set; }
            public string Product { get; set; }
            //public decimal RequestedQty { get; set; }
            public decimal Quantity { get; set; }
            public decimal Amount { get; set; }
            //public long StockId{ get; set; }
            public string Mode { get; set; }
            public string WarnMsg { get; set; }
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



        public static string _updateProcedureName = "proProjectMaterialAllocationUpdate";
        public class UpdateMaterialAllocation
        {
            public long FK_ProjectMaterialAllocation { get; set; }
            public long ID_ProjectMaterialAllocation { get; set; }
            public DateTime ProMatAllocationDate { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stages { get; set; }
            public long FK_Team { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Department { get; set; }
            public string MaterialDetails { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long? FK_ProjectMaterialRequest { get; set; }

            public long RequestMode { get; set; }
            public Int64? LastID { get; set; } = 0;
            public string ProductSerialNumbers { get; set; }
            public long FK_Category { get; set; } 
            public long FK_BOMProject { get; set; }
            public long FK_Branch { get; set; }
        }

        public class SelectMaterialAllocation
        {

            public long FK_ProjectMaterialAllocation { get; set; }
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
            public long StageID { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }
        public class EmployeeList
        {
            public long FK_Employee { get; set; }
            public string EmployeeName { get; set; }
        }

        public class TeamList
        {
            public long FK_Project { get; set; }
            //public long StageID { get; set; }
            public long TeamID { get; set; }
            public string TeamName { get; set; }
        }

     

        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }
      
        public class MaterialAllocationListModel
        {

           
            public List<StageList> StageList { get; set; }
            public List<EmployeeList> EmployeeList { get; set; }
            public List<TeamList> TeamList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public DateTime VisitDate { get; set; }
            public List<ModeList> ModeList { get; set; }
            public long FK_Employee { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<Branch> BranchList { get; set; }

        }

        public static string _deleteProcedureName = "proProjectMaterialAllocationDelete";
        public class DeleteMaterialAllocation
        {
            public Int64 FK_ProjectMaterialAllocation { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }
        public class Branch
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }

        }
        public class Category
        {
            public long ID_Project { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

        public class InputProjectMaterialAllocationID
        {
            public Int64 FK_ProjectMaterialAllocation { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int CheckList { get; set; }
            public string Name { get; set; }
            public long FK_Project { get; set; }
        }
        public class Datamaterialfill
        {
            public long FK_ProjectMaterialRequest { get; set; }
            public long  FK_Branch { get; set; }
            public long FK_Department { get; set; }






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
        public class GetSerialNumbers
        {
            public long FK_Stock { get; set; }
            public long FK_MasterID { get; set; }
            public long ID_ProductNumberingDetails { get; set; }
            public string ProdSerielNo { get; set; }
            public string UID { get; set; }
            public long ProductId { get; set; }
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
            public Int64 ID_ProjectMaterialDetails { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_Department { get; set; }
            public Int64 StockID { get; set; }
            public int Mode { get; set; }
            public decimal Quantity { get; set; }
        }
        public class GetDeliverySlipDataProIp
        {
            public int Mode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 Fk_MaterilaAllocation { get; set; }
          
        }
        public class GetDeliverySlipTableDataOut
        {
            public string Product { get; set; }
            public string HSNCode { get; set; }
            public string Quantity { get; set; }
            public string Service { get; set; }
            public Int64 ID_ProjectMaterialAllocation { get; set; }
            public Int64 FK_product { get; set; }
        }
        public class GetDeliverySlipDataIp
        {
            public Int64 Fk_MaterilaAllocation { get; set; }
        }
        public class GetDeliverySlipDataOut
        {
            public  Int64 deliveryNoteId { get; set; }
            public  Int64 FK_Customer { get; set; }
            public string BillCusname { get; set; }
            public string BillCusAddress { get; set; }
            public string BillCusEmail { get; set; }
            public string BillCusMobile { get; set; }
            public string shipCusname { get; set; }
            public string shipCusMobile { get; set; }
            public string shipCusAddress { get; set; }
            public string shipCusEmail { get; set; }
            public string ComName { get; set; }
            public string ComAddress { get; set; }
            public string ComPhone { get; set; }
            public string ComEmail { get; set; }
            public string Comwebsite { get; set; }
            public string Comlogo { get; set; }
            public string Extension { get; set; }
            public string ComlogoName { get; set; }
            public DateTime date { get; set; }
            public DateTime shipping_date { get; set; }


        }
        public class BOMProjectFill
        {

            public int Mode { get; set; }
            //public long FK_Master { get; set; }
            public long FK_Category { get; set; }
            public long ID_BOMProject { get; set; }
            public long FK_Company { get; set; }

        }
        public class FillDataBom
        {
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public long StockId { get; set; }
            public decimal CrntQnty { get; set; }
            public decimal BMPPQuantity { get; set; }
            public long ProductID { get; set; }
            public decimal Amount { get; set; } = 0;
                  
        }
        public Output UpdateMaterialAllocationData(UpdateMaterialAllocation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMaterialAllocation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMaterialAllocationData(DeleteMaterialAllocation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMaterialAllocation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectMaterialAllocationSelect";
        public APIGetRecordsDynamic<MaterialAllocationView> GetMaterialAllocationData(InputProjectMaterialAllocationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialAllocationView, InputProjectMaterialAllocationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MaterialDetails>GetMaterialAllocationMaterialData(InputProjectMaterialAllocationID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialDetails, InputProjectMaterialAllocationID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }


        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }


        public APIGetRecordsDynamic<Productview> GetMaterialfill(Datamaterialfill input, string companyKey)
        {
            return Common.GetDataViaProcedure<Productview, Datamaterialfill>(companyKey: companyKey, procedureName: "ProProjectMaterialAllocationDatafill", parameter: input);

        }

        public APIGetRecordsDynamic<GetSerialNumbers> GetSerialNumber(SelectSerialNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetSerialNumbers, SelectSerialNo>(companyKey: companyKey, procedureName: "ProSalesSubProductSelect", parameter: input);

        }
        public APIGetRecordsDynamic<InclusiveProduct> GetSubProducts(GetSubProduct input, string companyKey)
        {
            return Common.GetDataViaProcedure<InclusiveProduct, GetSubProduct>(companyKey: companyKey, procedureName: "ProGetSubProductInfo", parameter: input);

        }
        public APIGetRecordsDynamic<GetDeliverySlipTableDataOut> GetDeliverySlipTableData(GetDeliverySlipDataProIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetDeliverySlipTableDataOut, GetDeliverySlipDataProIp>(companyKey: companyKey, procedureName: "ProMaterialallocationDeliveryNoteSelect", parameter: input);

        }
        public APIGetRecordsDynamic<GetDeliverySlipDataOut> GetDeliverySlipData(GetDeliverySlipDataProIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetDeliverySlipDataOut, GetDeliverySlipDataProIp>(companyKey: companyKey, procedureName: "ProMaterialallocationDeliveryNoteSelect", parameter: input);

        }

        public APIGetRecordsDynamic<FillDataBom> FillBOMProject(BOMProjectFill input, string companyKey)
        {
            return Common.GetDataViaProcedure<FillDataBom, BOMProjectFill>(companyKey: companyKey, procedureName: "ProBOMProjectDataFill", parameter: input);
        }
    }
}