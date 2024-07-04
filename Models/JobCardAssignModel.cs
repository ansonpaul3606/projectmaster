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
    public class JobCardAssignModel
    {
        public class JobCardAssignView
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
            public List<JobCardAssignDetailsView> JobCardDetails { get; set; }
            public long NewQty { get; set; }
            public long FK_Employee { get; set; }
            public string Name { get; set; }
            public long FK_JobCardAssign { get; set; }

        }
        public class UpdateJobCardAssign
        {
            public long ID_JobCardAssign { get; set; }
            public long FK_JobCard { get; set; }
            public long FK_Product { get; set; }
            public long FK_ProjectStages { get; set; }
            public DateTime? JcaStartDate { get; set; }
            public string JcaStartTime { get; set; }

            public string JobCardAssignDetails { get; set; }
            public short FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public long Debug { get; set; }
            public int UserAction { get; set; }
            public string TransMode { get; set; }
            public bool ReturnJob { get; set; }
            public DateTime? JCStartDate { get; set; }
            public long? LastID { get; set; }
        }
        public class DeleteJobCardAssign
        {
            public long FK_JobCardAssign { get; set; }

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
            
        public class JobCardAssignDetailsView
        {
            public long SlNo { get; set; }
            public long ID_JobCardDetails { get; set; }
            public long ID_JobCard { get; set; }
            public long ID_JobCardAssign { get; set; }
            public long FK_ProjectStages { get; set; }
            public string JobCardNo { get; set; }
            public string JobCardDetails { get; set; }
            public string Stage { get; set; }
            public string ProductName { get; set; }
            public DateTime JcaAssignDate { get; set; }
            public DateTime JcaTargetDate { get; set; }
            public string StartDate { get; set; }
            public DateTime? JcaStartDate { get; set; }
            public string JcaStartTime { get; set; }
            public string TargetDate { get; set; }
            public string DueDays { get; set; }
            public decimal Quantity { get; set; }
            public Int64 TotalCount { get; set; }
            public string Field { get; set; }
            public long Value { get; set; }
            public long MasterId { get; set; }
            public long FK_Product { get; set; }
            public List<EmployeeAssign> EmployeeAssign { get; set; }
            public long ErrCode { get; set; }
            public string ErrMsg { get; set; }
            public string TransMode { get; set; }
            public bool ReturnJob { get; set; } 
            public DateTime JCStartDate { get; set; }
            public string mdlStartDate { get; set; }

            public long? LastID { get; set; }
        }
        public class GetJobCardAssign
        {
            public long FK_JobCardAssign { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Int16 Detailed { get; set; }
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
            //public long FK_Project { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime? TargetFrom { get; set; }
            public DateTime? TargetTo { get; set; }
        }
        public class GetJobCardEmployees
        {
            public long FK_Product { get; set; }
            public long FK_JobCard { get; set; }
            public long FK_ProjectStages { get; set; }
            public long Filterid { get; set; }
            public long FK_Company { get; set; }


            public string EntrBy { get; set; }

            public short FK_Machine { get; set; } 
            public Int16 FK_BranchCodeUser { get; set; }

            public bool ReturnJob { get; set; }

        }

            public class Stage
        {
            public long ID_ProjectStages { get; set; }

            public string PrStName { get; set; }
        }

        public static string _selectProcedureName = "ProJobCardSearchSelect";
        public static string _selectEmpProcedureName = "ProGetEmployeeForProductionAssignment";
        public static string _updateProcedureName = "ProJobCardAssignUpdate";
        public static string _selectListProcedureName = "ProJobCardAssignSelect";
        public static string _deleteProcedureName = "ProJobCardAssignDelete";
        public class Team
        {
            public long ID_ProjectTeam { get; set; }

            public string ProjTeamName { get; set; }
        }
        public APIGetRecordsDynamic<JobCardAssignDetailsView> GetJobCardData(GetJobCards input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobCardAssignDetailsView, GetJobCards>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<JobCardAssignDetailsView> GetJobCardEmpData(GetJobCardEmployees input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobCardAssignDetailsView, GetJobCardEmployees>(companyKey: companyKey, procedureName: _selectEmpProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EmployeeAssign> GetJobCardEmpDtlsData(GetJobCardEmployees input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeAssign, GetJobCardEmployees>(companyKey: companyKey, procedureName: _selectEmpProcedureName, parameter: input);
        }
        public Output UpdateJobCardData(UpdateJobCardAssign input, string companyKey)
        {
            return Common.UpdateTableData<UpdateJobCardAssign>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<JobCardAssignDetailsView> GetJobCardAssignData(GetJobCardAssign input, string companyKey)
        {
            return Common.GetDataViaProcedure<JobCardAssignDetailsView, GetJobCardAssign>(companyKey: companyKey, procedureName: _selectListProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EmployeeAssign> GetJobCardAssignEmpData(GetJobCardAssign input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeAssign, GetJobCardAssign>(companyKey: companyKey, procedureName: _selectListProcedureName, parameter: input);
        }
        public Output DeleteJobCardData(DeleteJobCardAssign input, string companyKey)
        {
            return Common.UpdateTableData<DeleteJobCardAssign>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}