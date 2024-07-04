using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class MaterialModel
    {
        public class MaterialRequestView
        {
            public long ID_ProjectMaterialRequest { get; set; }
            public DateTime ProMatRequestDate { get; set; }
            public List<StageList> StageList { get; set; }
            public List<EmployeeList> EmployeeList { get; set; }
            public List<TeamList> TeamList { get; set; }
            public List<MaterialRequestDetailsView> MaterialRequestDetailsView { get; set; }
            public List<ModeList> ModeList { get; set; }
            public long FK_Project { get; set; }
            public string Project { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stages { get; set; }
            public string TeamName { get; set; }
            public string Stage { get; set; }
            public string Employee { get; set; }
            public long FK_Employee { get; set; }
            public long FK_ProjectMaterialRequest { get; set; }
            public string TransMode { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }
            public long? LastID { get; set; }


        }

        public class StageList
        {
            public string Mode { get; set; }
            public long StageID { get; set; }
            public string StageName { get; set; }
        }
        public class QuantityCheckResult
        {
            public string Warnmsg { get; set; }
        }
            public class QuantityCheck
        {
            public long FK_ProjectMaterialAllocation { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Team { get; set; }
            public long FK_Product { get; set; }
            public decimal Quantity { get; set; }
        }
        public class TeamList
        {
            public long ProjectID { get; set; }
            public long TeamID { get; set; }
            public string TeamName { get; set; }
        }
        public class EmployeeList
        {
            public long EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }
        public class ModeList
        {
            public long ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }


        public class StockTransferID
        {
            public Int64 FK_Department { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64? FK_Employee { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
        }

        public class UpdateMaterialRequest
        {
            public long ID_ProjectMaterialRequest { get; set; }      

            public string MaterialRequestDetailsView { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long FK_Project { get; set; }
            public DateTime ProMatRequestDate { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stages { get; set; }
            public long FK_Employee { get; set; }
            public long FK_ProjectMaterialRequest { get; set; }
            public long? LastID { get; set; }

        }
      
 
        public class MaterialRequestDetailsView
        {
            public long ID_ProjectMaterial { get; set; }
            [Required(ErrorMessage = "Please Enter Quantity")]
            public string Quantity { get; set; }
           
            [Range(1, long.MaxValue, ErrorMessage = "Select Product")]
            public long ProductID { get; set; }
            public string Product { get; set; }
            public string Mode { get; set; }





        }
        public class MaterialRequestID
        {
            public Int64 FK_ProjectMaterialRequest { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }
        }
        public class MaterialSelectDetails
        {
            public long SlNo { get; set; }
            public long FK_ProjectMaterialRequest { get; set; }
            public long FK_Project { get; set; }
            public string Project { get; set; }
            public DateTime ProMatRequestDate { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stages { get; set; }
            public string TeamName { get; set; }
            public string Stage { get; set; }          
            public Int64 TotalCount { get; set; }
            public long ID_ProjectMaterialRequest { get; set; }
            public long? LastID { get; set; }
        }
        public class DeleteMaterialRequest
        {
            public long FK_ProjectMaterialRequest { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }



        public Output UpdateMaterialData(UpdateMaterialRequest input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMaterialRequest>(parameter: input, companyKey: companyKey, procedureName: "ProProjectMaterialRequestUpdate");
        }

        public APIGetRecordsDynamic<MaterialSelectDetails> GetMaterialRequestData(MaterialRequestID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialSelectDetails, MaterialRequestID>(companyKey: companyKey, procedureName: "ProProjectMaterialRequestSelect", parameter: input);
        }


        public APIGetRecordsDynamic<MaterialRequestView> GetMaterialData(MaterialRequestID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialRequestView, MaterialRequestID>(companyKey: companyKey, procedureName: "ProProjectMaterialRequestSelect", parameter: input);
        }

        public Output DeleteMaterialRequestData(DeleteMaterialRequest input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMaterialRequest>(parameter: input, companyKey: companyKey, procedureName: "ProProjectMaterialRequestDelete");
        }

        public APIGetRecordsDynamic<MaterialRequestDetailsView> GetRenewalSelectDatanew(MaterialRequestID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialRequestDetailsView, MaterialRequestID>(companyKey: companyKey, procedureName: "ProProjectMaterialRequestSelect", parameter: input);

        }
       
        public APIGetRecordsDynamic<MaterialRequestView> GetStockTransferData(StockTransferID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialRequestView, StockTransferID>(companyKey: companyKey, procedureName:"ProStockProductSelect", parameter: input);
        }
        public APIGetRecordsDynamic<QuantityCheckResult> CheckQuantity(QuantityCheck input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuantityCheckResult, QuantityCheck>(companyKey: companyKey, procedureName: "ProProjectQtyLimitCheck", parameter: input);
        }
    }
}