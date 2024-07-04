/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 01/08/2022
Purpose		: TeamCreation
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
    public class TeamCreationModel
    {
        
        public class TeamCreationView
        {
            public long SlNo { get; set; }
            public long ProjectTeamID { get; set; }

            public DateTime CreateDate { get; set; }

            public DateTime ProjectDate { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string CusNote { get; set; }
            public string ExpenseAmount { get; set; }
            public string Remarks { get; set; }
            public Int64 TotalCount { get; set; }
            public long ProjectID { get; set; }
            public string Project { get; set; }
            public string TransMode { get; set; }
            public long? LastID { get; set; }
        }

        public class TeamCreationInputFromViewModel
        {

            [Required(ErrorMessage = "No TeamCreation Selected")]
            public long ProjectTeamID { get; set; }

            public DateTime CreateDate { get; set; }


            public string Name { get; set; }
            public string ShortName { get; set; }
         
            public Int64 TotalCount { get; set; }
            public long Project { get; set; }
            public List<EmployeeDetails> EmployeeDetails { get; set; }

            public string TransMode { get; set; }
            public long? LastID { get; set; }

        }

        public class TeamCreationInfoView
        {
            [Required(ErrorMessage = "No TeamCreation Selected")]
            public Int64 ProjectTeamID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }
        public class EmployeeDetails
        {
            public long EmployeeID { get; set; }
            public long EmployeeType { get; set; }
            public string EmployeeTypeName { get; set; }
            public long Department { get; set; }
            public string DepartmentName { get; set; }
            public string Employee { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public DateTime CreateDate { get; set; }
            public long ProjectID { get; set; }
            public long TeamID { get; set; }


        }
       


        public static string _updateProcedureName = "proProjectTeamUpdate";
        public class UpdateTeamCreation
        {

            public long FK_ProjectTeam { get; set; }
            public long ID_ProjectTeam { get; set; }
            public string ProjTeamName { get; set; }
            public DateTime ProjTeamCreateDate { get; set; }
            public string ProjTeamShortName { get; set; }
            public long FK_Project { get; set; }
            public string EmployeeDetails { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long? LastID { get; set; }



        }

        public class SelectTeamCreation
        {

            public long FK_TeamCreation { get; set; }
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
        
      
        public class TeamCreationListModel
        {
            public List<Employee> EmployeeList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }
            
            public DateTime CreateDate { get; set; }

   
        }

        public static string _deleteProcedureName = "proProjectTeamDelete";
        public class DeleteTeamCreation
        {
            public Int64 FK_ProjectTeam { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputProjectTeamID
        {
            public Int64 FK_ProjectTeam { get; set; }
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




        public Output UpdateTeamCreationData(UpdateTeamCreation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateTeamCreation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteTeamCreationData(DeleteTeamCreation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteTeamCreation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectTeamSelect";
        public APIGetRecordsDynamic<TeamCreationView> GetTeamCreationData(InputProjectTeamID input, string companyKey)
        {
            return Common.GetDataViaProcedure<TeamCreationView, InputProjectTeamID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<EmployeeDetails> GetTeamCreationEmployeeData(InputProjectTeamID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeDetails, InputProjectTeamID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<EmployeeTypeList> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }


    }
}