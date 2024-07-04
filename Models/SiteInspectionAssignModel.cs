using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace PerfectWebERP.Models
{
    public class SiteInspectionAssignModel
    {
      

        public class SiteInspectionView
        {
            public long SlNo { get; set; }
            public long SiteInspectionAssignID { get; set; }
            public long ID_SiteVisitAssignment { get; set; }
            public long LeadGenerationID { get; set; }
            public string LeadNo { get; set; }
            public string Customer { get; set; }
            public string MobileNo { get; set; }
            public DateTime SVSiteVisitDate { get; set; }
            public DateTime VisitDate { get; set; }
            public DateTime LeadDates { get; set; }
            public string VisitTime { get; set; }

            public string Note1 { get; set; }
            public string Note2 { get; set; }
            public decimal ?SVExpenseAmount { get; set; }
          
          
            public Int64 TotalCount { get; set; }
         
            public string TransMode { get; set; }
            public long FK_Lead { get; set; }
            public long ?LastID { get; set; }
            public int SVAInspectionType { get; set; }
        }

        public class SiteInspectionInputFromViewModel
        {

            [Required(ErrorMessage = "No SiteVisit Selected")]
            public long SiteInspectionAssignID { get; set; }

            public long LeadGenerationID { get; set; }

            public DateTime VisitDate { get; set; }

            public string VisitTime { get; set; }
            public string TransMode { get; set; }

            public string Note1 { get; set; }
            public string Note2 { get; set; }
            public decimal ?SVExpenseAmount { get; set; }
            public long? LastID { get; set; }

            public List<EmployeeDetails> EmployeeDetails { get; set; }
           
          
            public List<CheckListSub> CheckListSub { get; set; } //nj
            public long InspectionType { get; set; }
            public Int16 Clear { get; set; }
        }
        public class CheckListSub //nj
        {
            public long FK_CheckList { get; set; }
            public long FK_CheckListType { get; set; }
        }
        public class SiteInspectionInfoView
        {
            [Required(ErrorMessage = "No SiteVisit Selected")]
            public Int64 SiteInspectionAssignID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }

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
     
      
   

        public static string _updateProcedureName = "ProSiteVisitAssignmentUpdate";
        public class UpdateSiteInspection
        {
           
            public long FK_SiteVisitAssignment { get; set; }
            public long ID_SiteVisitAssignment { get; set; }
            public long FK_LeadGeneration { get; set; }
            public DateTime SVSiteVisitDate { get; set; }
            public string SVSitevisitTime { get; set; }
            public string SVInspectionNote1 { get; set; }

            public string SVInspectionNote2 { get; set; }
            public Int16 Clear { get; set; }

            public decimal ?SVExpenseAmount { get; set; }
         
            public string EmployeeDetails { get; set; }
           
           
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

           
            public string CheckListSub { get; set; }
            public long? LastID { get; set; }
            public long InspectionType { get; set; }
            
        }

        public class SelectSiteInspection
        {

            public long FK_SiteInspectionAssign { get; set; }
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
     
      
        public class CheckList//nj
        {

            public long FK_CheckListType { get; set; }
            public long ID_CheckList { get; set; }
            //public long ChkCount { get; set; }
            public string CkLstName { get; set; }


        }
        public class CheckListType//nj
        {
            public long ID_CheckListType { get; set; }

            public string CLTyName { get; set; }

        }

        public class SiteInspectionModel
        {
            public List<Employee> EmployeeList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<EmployeeTypeList> EmployeeTypeList { get; set; }
          
            public List<CheckList> CheckList { get; set; }//nj
            public List<CheckListType> CheckListType { get; set; }//nj
            public List<InspectionList> InspectionListData { get; set; }
            public DateTime VisitDate { get; set; }

            public string VisitTime { get; set; }
        }

        public static string _deleteProcedureName = "ProSiteVisitAssignmentDelete";
        public class DeleteSiteInspection
        {
            public Int64 FK_SiteVisitAssignment { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputSiteInspectionID
        {
            public Int64 FK_SiteVisitAssignment { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public Int64 Detailed { get; set; }
            public string Name { get; set; }

        }
     


        public Output UpdateSiteInspectionAssignData(UpdateSiteInspection input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSiteInspection>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteSiteInspectionAssignData(DeleteSiteInspection input, string companyKey)
        {
            return Common.UpdateTableData<DeleteSiteInspection>(parameter: input, companyKey: companyKey, procedureName: "ProSiteVisitAssignmentDelete");
        }


        public static string _selectProcedureName = "ProSiteVisitAssignmentSelect";
        public APIGetRecordsDynamic<SiteInspectionView> GetSiteInspectionAssignData(InputSiteInspectionID input, string companyKey)
        {
            return Common.GetDataViaProcedure<SiteInspectionView, InputSiteInspectionID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<EmployeeDetails> GetSiteInspectionAssignEmployeeData(InputSiteInspectionID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeDetails, InputSiteInspectionID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<SiteVisitModel.CheckList> GetSiteInspectionAssignChecklistData(InputSiteInspectionID input, string companyKey)
        {
            return Common.GetDataViaProcedure <SiteVisitModel.CheckList, InputSiteInspectionID >(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }


        public APIGetRecordsDynamic<EmployeeTypeList> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
      
      
        public class Inputprintchecklist
        {
            public long FK_SiteVisitAssignment { get; set; }

            public Int64 FK_Company { get; set; }
            public int Mode { get; set; }

        }
        public class Outputprintchecklist
        {
            public string CheckListType { get; set; }
            public string CheckList { get; set; }
            public Boolean Checked { get; set; }


        }
        public class OutCustomerdetails
        {
            public string Customer { get; set; }
            public string Location { get; set; }
            public string ProjectCode { get; set; }
            public string MobileNo { get; set; }
            public string ContactNo { get; set; }
            public string Measuredatetime { get; set; }
            public string Requirement { get; set; }
            public string Measuredby { get; set; }
            public string Payment { get; set; }
        }
        public class ModeValue
        {
            public Int32 Mode { get; set; }
        }
        public class InspectionList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public APIGetRecordsDynamic<Outputprintchecklist> GetChecklistPrint(Inputprintchecklist input, string companyKey)
        {
            return Common.GetDataViaProcedure<Outputprintchecklist, Inputprintchecklist>(companyKey: companyKey, procedureName: "ProRptSiteVisitInpectionAssignCheckList", parameter: input);

        }
        public APIGetRecordsDynamic<OutCustomerdetails> GetChecklistcusPrint(Inputprintchecklist input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutCustomerdetails, Inputprintchecklist>(companyKey: companyKey, procedureName: "ProRptSiteVisitInpectionAssignCheckList", parameter: input);

        }
        public APIGetRecordsDynamic<InspectionList> GetInspectionListData(ModeValue input, string companyKey)
        {
            return Common.GetDataViaProcedure<InspectionList, ModeValue>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
  
        //public APIGetRecordsDynamic<Outputprintchecklist> GetChecklistPrint(Inputprintchecklist input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<Outputprintchecklist, Inputprintchecklist>(companyKey: companyKey, procedureName: "ProRptSiteVisitInpectionAssignCheckList", parameter: input);

        //}
    }
}