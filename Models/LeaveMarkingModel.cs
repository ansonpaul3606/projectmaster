using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class LeaveMarkingModel
    {
        public class LeaveMarkingView
        {
            public long SlNo { get; set; }

            public long ID_LeaveMarking { get; set; }

            public long FK_Employee { get; set; }
            public long? FK_Branch { get; set; }
            public long? FK_Department { get; set; }
            public long FK_LeaveType { get; set; }
            public string Leavetype { get; set; }
            public DateTime LMFromDate { get; set; }
            public DateTime LMToDate { get; set; }
            public string EmployeeName { get; set; }
            public string Branch { get; set; }

            public List<BranchTo> BranchListTo { get; set; }
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public List<LeaveTypeList> LeaveTypeList { get; set; }
            public long FK_LeaveMarking { get; set; }

            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }
        }

        public class BranchTo
        {
            public Int32 FK_Branch { get; set; }
            public string BranchNameTo { get; set; }
            public string Branch { get; set; }


        }
        public class DepartmentTo
        {
            public Int32 FK_Department { get; set; }
            public string DepartmentNameTo { get; set; }

        }

        public class LeaveTypeList
        {
            public Int32 FK_LeaveType { get; set; }
            public string Leavetype { get; set; }
        }
        public class EmployeeTo
        {
            public Int32 FK_Employee { get; set; }
            public string EmployeeName { get; set; }
        }

        public class LeaveMarkingUpdate
        {
            public long ID_LeaveMarking { get; set; }
            public long FK_Employee { get; set; }
            public long FK_LeaveType { get; set; }
            public DateTime LMFromDate { get; set; }
            public DateTime LMToDate { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long FK_Machine { get; set; }
            public long FK_LeaveMarking { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public byte UserAction { get; internal set; }
        }

        public class LeaveMarking
        {
            public long FK_LeaveMarking { get; set; }
         
          
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }






        }
        public class DeleteLeaveMarking
        {
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_LeaveMarking { get; set; }

            public long FK_BranchCodeUser { get; set; }

        }


        public Output UpdateLeaveMarkingData(LeaveMarkingUpdate input, string companyKey)
        {
            return Common.UpdateTableData<LeaveMarkingUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProLeaveMarkingUpdate");
        }

        public Output DeleteLeaveMarkingData(DeleteLeaveMarking input, string companyKey)
        {
            return Common.UpdateTableData<DeleteLeaveMarking>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<LeaveMarkingView> GetLeaveMarkingData(LeaveMarking input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeaveMarkingView, LeaveMarking>(companyKey: companyKey, procedureName: "ProLeaveMarkingSelect", parameter: input);

        }

        public static string _updateProcedureName = "ProLeaveMarkingUpdate";
        public static string _deleteProcedureName = "ProLeaveMarkingDelete";
        public static string _selectProcedureName = "ProLeaveMarkingSelect";





    }
}