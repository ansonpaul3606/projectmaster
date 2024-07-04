/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 17/09/2022
Purpose		: ProjectFollowUp
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
    public class ProjectFollowUpModel
    {

        public class ProjectFollowUpView
        {
            public long SlNo { get; set; }
            public long ProjectFollowUpID { get; set; }
            public DateTime Date { get; set; }         
            public Int64 TotalCount { get; set; }        
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
            public byte CurrentStatusID { get; set; }        
            public string Remarks { get; set; }         
            public long? PrevStatusID { get; set; }
            public string Project { get; set; }
            public string Stage { get; set; }
            public string CurrentStatus { get; set; }
            public DateTime StatusDate { get; set; }
            public string TransMode { get; set; }
            public DateTime ProjectDate { get; set; }
            public DateTime DueDate { get; set; }

            public string Reason { get; set; }
            public long? LastID { get; set; }

        }
        public class ProjectFollowUpInputFromViewModel
        {

            [Required(ErrorMessage = "No ProjectFollowUp Selected")]
            public long ProjectFollowUpID { get; set; }
            public long? LastID { get; set; }
            public DateTime Date { get; set; }
          
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
            public byte CurrentStatusID { get; set; }
            public string CurrentStatus { get; set; }
            public DateTime StatusDate { get; set; }
            public string Remarks { get; set; }
            public string Project { get; set; }
            public long PrevStatusID { get; set; }
            public string Stage { get; set; }

            public string TransMode { get; set; }
            public List<MaterialDetails> MaterialDetails { get; set; }

            public string Reason { get; set; }

            


        }

        public class ProjectFollowUpInfoView
        {
            [Required(ErrorMessage = "No ProjectFollowUp Selected")]
            public Int64 ProjectFollowUpID { get; set; }
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



        public static string _updateProcedureName = "proProjectFollowUpUpdate";
        public class UpdateProjectFollowUp
        {
            public long FK_ProjectFollowUp { get; set; }
            public long ID_ProjectFollowUp { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
            public DateTime EffectDate { get; set; }
            public byte PrFuCurrentStatus { get; set; }
            public DateTime PrFuStatusDate { get; set; }
            public string PrPuRemarks { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public string Reason { get; set; }
            public long? LastID { get; set; }


        }

        public class SelectProjectFollowUp
        {

            public long FK_ProjectFollowUp { get; set; }
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

        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }
        public class StatusList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class StageDueDate
        {
            public string DueDate { get; set; }
        }
        public class ProjectFollowUpListModel
        {

           
            public List<StageList> StageList { get; set; }
            public List<EmployeeList> EmployeeList { get; set; }
            public List<StatusList> StatusList { get; set; }
            public DateTime VisitDate { get; set; }
            public List<ModeList> ModeList { get; set; }
            public long FK_Employee { get; set; }

        }

        public static string _deleteProcedureName = "proProjectFollowUpDelete";
        public class DeleteProjectFollowUp
        {
            public Int64 FK_ProjectFollowUp { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputProjectFollowUpID
        {
            public Int64 FK_ProjectFollowUp { get; set; }
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




        public Output UpdateProjectFollowUpData(UpdateProjectFollowUp input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProjectFollowUp>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProjectFollowUpData(DeleteProjectFollowUp input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProjectFollowUp>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectFollowUpSelect";
        public APIGetRecordsDynamic<ProjectFollowUpView> GetProjectFollowUpData(InputProjectFollowUpID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectFollowUpView, InputProjectFollowUpID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MaterialDetails> GetProjectFollowUpMaterialData(InputProjectFollowUpID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialDetails, InputProjectFollowUpID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
      







    }
}