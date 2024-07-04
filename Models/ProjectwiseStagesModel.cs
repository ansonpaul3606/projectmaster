/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 02/08/2022
Purpose		: ProjectwiseStages
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
    public class ProjectwiseStagesModel
    {

        public class ProjectwiseStagesView
        {
            public long SlNo { get; set; }
            public long ProjectwiseStagesID { get; set; }
            public DateTime Date { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stage { get; set; }
            public decimal Duration { get; set; }
            public int DurationType { get; set; }
            public decimal CollectionAmount { get; set; }

            public string Project { get; set; }
            public string Team { get; set; }
            public string Stage { get; set; }
            public string CusNumber { get; set; }
            public decimal Percentage { get; set; }
            public string TransMode { get; set; }
            public DateTime ProjectDate { get; set; }
            public decimal FinalAmount { get; set; }
            public long ?LastID { get; set; }
        }
        public class ProjectwiseStagesInputFromViewModel
        {

            [Required(ErrorMessage = "No ProjectwiseStages Selected")]
            public long ProjectwiseStagesID { get; set; }

            public DateTime Date { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stage { get; set; }
            public Decimal Duration { get; set; }
            public int DurationType { get; set; }
            public decimal Percentage { get; set; }
            public decimal CollectionAmount { get; set; }

            public List<MaterialDetails> MaterialDetails { get; set; }
            public List<WorkDetails> WorkDetails { get; set; }
            public string TransMode { get; set; }
            public long? LastID { get; set; }


        }

        public class ProjectwiseStagesInfoView
        {
            [Required(ErrorMessage = "No ProjectwiseStages Selected")]
            public Int64 ProjectwiseStagesID { get; set; }
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

            public Decimal Duration { get; set; }
            public decimal WorkAmount { get; set; }


        }

        public class EnableEdit
        {
            public long FK_Project { get; set; }
            public int PrFuCurrentStatus { get; set; }

        }

        public static string _updateProcedureName = "proProjectwiseStagesUpdate";
        public class UpdateProjectwiseStages
        {

            public long FK_ProjectwiseStages { get; set; }
            public long ID_ProjectWiseStages { get; set; }
            public long FK_Project { get; set; }
            public DateTime PWSDate { get; set; }
            public long FK_ProjectStages { get; set; }
            public long FK_ProjectTeam { get; set; }
            public DateTime PWSStartDate { get; set; }
            public decimal PWSDuration { get; set; }
            public decimal CollectionAmount { get; set; }

            public int PWSDurationType { get; set; }
            public DateTime PWSFinishDate { get; set; }
            public decimal PWSProjPercent { get; set; }
            public string MaterialDetails { get; set; }
            public string WorkDetails { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

            public long? LastID { get; set; }

        }

        public class SelectProjectwiseStages
        {

            public long FK_ProjectwiseStages { get; set; }
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
        public class DurationTypeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ProjectwiseStagesListModel
        {

            public List<DurationTypeList> DurationTypeList { get; set; }
            public List<StageList> StageList { get; set; }
            public List<TeamList> TeamList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public DateTime VisitDate { get; set; }


        }

        public static string _deleteProcedureName = "proProjectwiseStagesDelete";
        public class DeleteProjectwiseStages
        {
            public Int64 FK_ProjectwiseStages { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputProjectwiseStagesID
        {
            public Int64 FK_ProjectwiseStages { get; set; }
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




        public Output UpdateProjectwiseStagesData(UpdateProjectwiseStages input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProjectwiseStages>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProjectwiseStagesData(DeleteProjectwiseStages input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProjectwiseStages>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectwiseStagesSelect";
        public APIGetRecordsDynamic<ProjectwiseStagesView> GetProjectwiseStagesData(InputProjectwiseStagesID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectwiseStagesView, InputProjectwiseStagesID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<EmployeeDetails> GetProjectwiseStagesEmployeeData(InputProjectwiseStagesID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeDetails, InputProjectwiseStagesID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MaterialDetails> GetProjectwiseStagesMaterialData(InputProjectwiseStagesID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialDetails, InputProjectwiseStagesID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<WorkDetails> GetProjectwiseStagesWorkData(InputProjectwiseStagesID input, string companyKey)
        {
            return Common.GetDataViaProcedure<WorkDetails, InputProjectwiseStagesID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }







    }
}