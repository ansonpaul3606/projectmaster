/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 05/01/2023
Purpose		: Job Card Assign
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class ProductionStatusModel
    {
         
       
        public class ProductionStatusView
        {
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_ProjectStages { get; set; }
            public long FK_Team { get; set; }
            public long FK_Product { get; set; }
            public long FK_Project { get; set; }
            public long FilterId { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime? TargetFromDate { get; set; }
            public DateTime? TargetToDate { get; set; }
            public List<Stage> StageList { get; set; }
            public List<Team> TeamList { get; set; }
            public List<ProductionStatusDetailsView> JobCardDetails { get; set; }
            public long NewQty { get; set; }
            public long FK_Employee { get; set; }
            public string Name { get; set; }
            public long FK_ProductionStatus { get; set; }
            public string UserCode { get; set; }
            public List<LeadGenerateModel.EmployeeInfo> EmployeeInfoList { get; set; }
        }
        public class UpdateProductionStatus
        {
            public int UserAction { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_JobCardFollowUp { get; set; }

            public long FK_JobCardAssign { get; set; }

            public long FK_Employee { get; set; }

            public long FK_ProjectStages { get; set; }

            public decimal JcfCompletedQty { get; set; }
            public decimal JcfDamagedQty { get; set; }
            public decimal JcfReturnedQty { get; set; }
            public long FK_ReturnProjectStage { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public short FK_Machine { get; set; }
            public string JobCardFollowUpProductDetailList { get; set; }
        }
        public class JobCardFollowUpProductDetails
        {
            public long FK_Product { get; set; }
            public long FK_Stock{ get; set; }
        public decimal JcfdQuantity { get; set; }
    }
        public class DeleteProductionStatus
        {
            public long ID_JobCardFollowUp { get; set; }

            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
        }
        public class EmployeeAssign
        {
            //public long SlNo { get; set; }
            public long FK_Employee { get; set; }
            public string EmployeeCode { get; set; }
            public string Employee { get; set; }
            public long JcadPendingJobs { get; set; }
            public long JcadProduceQuantity { get; set; }
        }

        public class ProductionStatusDetailsView
        {
            public long SlNo { get; set; }
            public long ID_JobCardAssignDetails { get; set; }
            public long FK_JobCardAssign { get; set; }
            public long ID_JobCardFollowUp { get; set; }
            public long FK_ProjectStages { get; set; }
            public long FK_ReturnProjectStage { get; set; }
            public string ReturnProjectStage { get; set; }
            public string JobCardNo { get; set; }
            public string JobCardDetails { get; set; }
            public string Stage { get; set; }
            public string Employee { get; set; }
            public string ProductName { get; set; }
            public string JcaAssignDate { get; set; }
            public string JcaTargetDate { get; set; }

            public string JcaStartDate { get; set; }
            public string JcaStartTime { get; set; }
            public decimal Quantity { get; set; }
            public decimal JcfCompletedQty { get; set; }
            public decimal JcfDamagedQty { get; set; }
            public decimal JcfReturnedQty { get; set; }
            public Int64 TotalCount { get; set; }
            public string Field { get; set; }
            public long Value { get; set; }
            public long MasterId { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long ErrCode { get; set; }
            public string ErrMsg { get; set; }
            public string TransMode { get; set; }
            public bool ReturnStage { get; set; }
            public List<FollowUpDetailsList> FollowUpDetailsList { get; set; }
            public List<JobCardFollowUpProductDetails> JobCardFollowUpProductDetailList { get; set; }
        }
        public class FollowUpDetailsList
        {
            public long ID_Product { get; set; }
            public string Product { get; set; }
            public string BatchNo { get; set; }
            public long ID_Stock { get; set; }
            public decimal MRP { get; set; }
            public decimal SalPrice { get; set; }
            public decimal PurRate { get; set; }
            public decimal CurrentQuantity { get; set; }
            public decimal Quantity { get; set; }
            public decimal ReqQuantity { get; set; }
            public string ExpiryDate { get; set; }
            
        }
        public class GetProductionStatus
        {
            public long FK_JobCardFollowUp { get; set; }
            //public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            //public Int16 Detailed { get; set; }
        }
        public class GetJobCards
        {
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_JobCardDetails { get; set; }
            //public long fk_team { get; set; }
            public long Filterid { get; set; }
            public long FK_Product { get; set; }
            public long FK_Employee { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime? TargetFrom { get; set; }
            public DateTime? TargetTo { get; set; }
        }
        public class GetJobCardAssignEmployees
        {
            public long ID_JobCardFollowUp { get; set; }
            public long FK_JobCardAssign { get; set; }

            public long FK_Employee { get; set; }
            public long FK_ProjectStages { get; set; }
            public long FK_Product { get; set; }
            public int Mode { get; set; }

            public long FK_Stock { get; set; }
            public long View { get; set; }




        }

        public class Stage
        {
            public long ID_ProjectStages { get; set; }

            public string PrStName { get; set; }
        }

        public static string _selectProcedureName = "ProJobCardStatusMarkingSearchSelect";
        public static string _selectStatusProcedureName = "ProGetStatusMarkingDetails";
        public static string _updateProcedureName = "ProJobCardFollowUpUpdate";
        public static string _selectListProcedureName = "ProJobCardFollowUpSelect";
        public static string _deleteProcedureName = "ProJobCardFollowUpDelete";
        public class Team
        {
            public long ID_ProjectTeam { get; set; }

            public string ProjTeamName { get; set; }
        }
        public APIGetRecordsDynamic<ProductionStatusDetailsView> GetJobCardData(GetJobCards input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStatusDetailsView, GetJobCards>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<ProductionStatusDetailsView> GetJobCardStatusMarkingData(GetJobCardAssignEmployees input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStatusDetailsView, GetJobCardAssignEmployees>(companyKey: companyKey, procedureName: _selectStatusProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<FollowUpDetailsList> GetJobCardStatusMarkingDetailsData(GetJobCardAssignEmployees input, string companyKey)
        {
            return Common.GetDataViaProcedure<FollowUpDetailsList, GetJobCardAssignEmployees>(companyKey: companyKey, procedureName: _selectStatusProcedureName, parameter: input);
        }
        //public APIGetRecordsDynamic<EmployeeAssign> GetJobCardEmpDtlsData(GetJobCardAssignEmployees input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<EmployeeAssign, GetJobCardAssignEmployees>(companyKey: companyKey, procedureName: _selectStatusProcedureName, parameter: input);
        //}
        public Output UpdateJobCardData(UpdateProductionStatus input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductionStatus>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<ProductionStatusDetailsView> GetProductionStatusData(GetProductionStatus input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStatusDetailsView, GetProductionStatus>(companyKey: companyKey, procedureName: _selectListProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EmployeeAssign> GetProductionStatusEmpData(GetProductionStatus input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeAssign, GetProductionStatus>(companyKey: companyKey, procedureName: _selectListProcedureName, parameter: input);
        }
        public Output DeleteJobCardData(DeleteProductionStatus input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductionStatus>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}