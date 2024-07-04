using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class EmployeeTransferModel
    {
        public class EmployeeTransferView
        {

            public long SlNo { get; set; }

            public long ID_EmployeeTransfer { get; set; }            
            public string EmployeeName { get; set; }     
            public long BranchID { get; set; }
            public string Branch { get; set; }
            public string BranchNew { get; set; }
            public string NewBrachType { get; set; }
            public string NewDesignation { get; set; }
            public string DepartmentNew { get; set; }

            public long BranchTypeID { get; set; }
            public string BranchType { get; set; }
            public long DepartmentID { get; set; }

            public string Department { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Designation")]
            public long DesignationID { get; set; }
            public string Designation { get; set; }           
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_EmployeeTransfer { get; set; }           
            public string EmployeeID { get; set; }
            public long NewBranchtypeID { get; set; }
            public long NewBranchID { get; set; }
            public long NewDesignationID { get; set; }
            public long NewDepartmentID { get; set; }
            public long ReasonID { get; set; }
            public string Employeeno{ get; set; }
          

        }
      
        public class EmployeeListModel
        {
            public List<BranchTypelist> BranchTypeList { get; set; }
            public List<Branchlist> BranchList { get; set; }
            public List<DesignationList> DesignationList { get; set; }
            public List<DepartmentList> DepartmentList { get; set; }

        }

        public class BranchTypelist
        {
            public long BranchTypeID { get; set; }
            public string BranchType { get; set; }
        }
        public class Branchlist
        {
            public long BranchID { get; set; }
            public string Branch { get; set; }
        }
        public class NewBranchlist
        {
            public long NewBranchID { get; set; }
            public string NewBranch { get; set; }
        }
        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }

        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string Department { get; set; }

        }
        public class EmployeeTransferID
        {
            public string TransMode { get; set; }
            public long FK_EmployeeTransfer { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }

          		
        }
        public class EmployeeIDView
        {
            public long FK_EmployeeTransfer { get; set; }
            public long ReasonID { get; set; }
        }
        public class UpdateEmployeeTransfer
        {
            public long FK_EmployeeTransfer { get; set; }
            public long ID_EmployeeTransfer { get; set; }
            public string EmployeeID { get; set; }         
            public long NewBranchtypeID { get; set; }
            public long NewBranchID { get; set; }         
            public long NewDesignationID { get; set; }
            public long NewDepartmentID { get; set; }
            public long DesignationID { get; set; }
            public long DepartmentID { get; set; }
            public long BranchTypeID { get; set; }
            public long BranchID { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
          
        }

        public static string _deleteProcedureName = "ProEmployeeTransferDelete";
        public static string _updateProcedureName = "ProEmployeeTransferUpdate";
        public static string _selectProcedureName = "ProEmployeeTransferSelect";

        public class DeleteEmployeeTransfer
        {
            public string TransMode { get; set; }
            public long FK_EmployeeTransfer { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }


        public Output UpdateEmployeeTransferData(UpdateEmployeeTransfer input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeTransfer>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteEmployeeTransferData(DeleteEmployeeTransfer input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEmployeeTransfer>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public APIGetRecordsDynamic<EmployeeTransferView> GetEmployeeTransferData(EmployeeTransferID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTransferView, EmployeeTransferID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

      
    }
    
}