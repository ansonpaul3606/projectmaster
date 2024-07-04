using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class PayRollReportModel
    {

        public class PayRollReportView
        {

            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public Int32 EmployeeID { get; set; }
            public string Employee { get; set; }
            public List<Reports> ReportsList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public string Companyname { get; set; }
            public List<Department> DepartmentList { get; set; }
            public Int32 ID_Report { get; set; }
            public long Reporttype { get; set; }
            public Int32 BranchID { get; set; }
            public Int32 DepartmentID { get; set; }
            public List<AllowanceTypeList> AllowanceTypeList { get; set; }
            public long ?FK_AllowanceType { get; set; }
        }
        public class AllowanceTypeList
        {
            public Int32 FK_AllowanceType { get; set; }
            public string Allowancetype { get; set; }
        }
        public class Reports
        {
            public Int32 ID_Report { get; set; }
            public string ReportsName { get; set; }
        }
        public class Branchs
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }
        }
       
      
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
    public class Payrollattendancereportinput
        {
            public int ReportMode { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
          public long FK_Employee { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_AllowanceType { get; set; }
            

        }
        public class PayRollReportGridview
        {
            public string Date { get; set; }
            public string Branch { get; set; }
            public string Department { get; set; }
            public string EmployeeCode { get; set; }
            public string Employee { get; set; }
            public string Designation { get; set; }
            public string Present { get; set; }
            public string Site { get; set; }
            public string Status { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string LeaveType { get; set; }
            public string RecoveryAmount { get; set; }
            public string AdvanceAmount { get; set; }
            public string AllowanceType { get; set; }
            public string Amount { get; set; }
            public string TypeName { get; set; }
            public string ProcessDate { get; set; }


        }

        public APIGetRecordsDynamic<PayRollReportGridview> GetPayRollAttendanceDetailsData(Payrollattendancereportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PayRollReportGridview, Payrollattendancereportinput>(companyKey: companyKey, procedureName: "ProPayRollReportGridView", parameter: input);

        }




    }

}