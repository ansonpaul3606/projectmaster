using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PerfectWebERP.Models
{
    public class AttendanceMarkingModel
    {
        public static string _updateProcedureName = "ProAttendanceMarkingUpdate";
        public static string _deleteProcedureName = "ProAttendanceMarkingDelete";
        public static string _selectProcedureName = "ProAttendanceMarkingSelect";
        public class AttendanceMarkingModelView
        {

            public long SlNo { get; set; }
            public long ?ID_EmployeeAttendanceDetails { get; set; }
            public DateTime AttDate { get; set; }
            public DateTime AMKDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }

            public string DepartmentName { get; set; }
            public string BranchName { get; set; }

            public List<AttendanceMarkingDetailsView> AttendanceMarkingDetailsView { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public List<BranchTo> BranchListTo { get; set; }
         
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public Int64? LastID { get; set; }
           
            public int ID { get; set; }
        }
        public class AttendanceMarkingModelViewlist
        {

         
            public DateTime AttDate { get; set; }
            public DateTime AMKDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public int ID { get; set; }
        }
        public class AttendanceMarkingDetailsView
        {
            public long ?ID_EmployeeAttendanceDetails { get; set; }
           
            public long EmployeeID { get; set; }
            public string Employee { get; set; }
            public string EmployeeCode { get; set; }
            public string Designation { get; set; }
            public bool ?Present { get; set; }
            public bool ?Partial { get; set; }
            public bool ?Site { get; set; }
            public long ?PartialStatus { get; set; }
            public DateTime AttDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }

//            ID_EmployeeAttendanceDetails AttDate EmployeeID EmployeeCode    Employee Designation Present Site    Partial PartialStatus   FK_Department FK_Branch
//214	2023-04-24 00:00:00.000	7	Amritha AmrithaAK   Service Engineer	1	1	0		2	2

        }
        public class BranchTo
        {
            public Int32 FK_Branch { get; set; }
            public string BranchNameTo { get; set; }


        }
        public class DepartmentTo
        {
            public Int32 FK_Department { get; set; }
            public string DepartmentNameTo { get; set; }

        }
        public class AttendanceMarkingUpdate
        {
            public long ?ID_EmployeeAttendanceDetails { get; set; }

            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public DateTime AMKDate { get; set; }


            public long Debug { get; set; }
            public string TransMode { get; set; }
            public string AttendanceMarkingDetailsView { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public byte UserAction { get; internal set; }
            public long FK_Machine { get; set; }

        }
        public class AttendanceMarkingID
        {
           
            public DateTime? AMKDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public int Details { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

            
        }

        public class GetEmployeedetails
        {
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public DateTime ProcessDate { get; set; }
           public long  FK_Company { get; set; }
        }
        public class AttendnaceMarkingDelete
        {
            //public long FK_EmployeeAttendanceDetails { get; set; }
            public string TransMode { get; set; }
            public DateTime AMKDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
         


        }
        public class AttendanceMarkingRsnView
        {
            public long ID_EmployeeAttendanceDetails { get; set; }

            public long ReasonID { get; set; }
            public DateTime AMKDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
        }
        public APIGetRecordsDynamic<AttendanceMarkingDetailsView> GetEmployeeattendanceDetails(GetEmployeedetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<AttendanceMarkingDetailsView, GetEmployeedetails>(companyKey: companyKey, procedureName: "ProGetEmployeeDetails", parameter: input);

        }
        public APIGetRecordsDynamic<AttendanceMarkingModelView> GetAttendanceMarkingData(AttendanceMarkingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AttendanceMarkingModelView, AttendanceMarkingID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public Output UpdateAttendanceMarkingData(AttendanceMarkingUpdate input, string companyKey)
        {
            return Common.UpdateTableData<AttendanceMarkingUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteAttendanceMarking(AttendnaceMarkingDelete input, string companyKey)
        {
            return Common.UpdateTableData<AttendnaceMarkingDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AttendanceMarkingModelViewlist> GetAttendanceMarkingDatainfoid(AttendanceMarkingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AttendanceMarkingModelViewlist, AttendanceMarkingID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<AttendanceMarkingDetailsView> GetAttendanceMarkingDatainfodetails(AttendanceMarkingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AttendanceMarkingDetailsView, AttendanceMarkingID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
       

    }
}