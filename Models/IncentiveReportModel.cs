using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class IncentiveReportModel
    {
        public class IncentiveReportView
        {
            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<DesignationList> DesignationList { get; set; }

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
        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }

        }
        public class IncentiveReportdto
        {
            public Int64 EmployeeID { get; set; }
            public int Mode { get; set; }
            public Int64 BranchID { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public Int64 DesignationID { get; set; }
            public Int64 FK_company { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public DateTime? ASonDate { get; set; }



        }
        public class GetReportDataOut
        {
           
            //public decimal IncNetAmount { get; set; }
            //public decimal SUMVALUE { get; set; }
            //public decimal Balance { get; set; }
            public Int64 SlNo { get; set; }
            public string EmpFName { get; set; }
            public string Opening { get; set; }
            public string Incentive { get; set; }
            public string paid { get; set; }

            public DateTime? PaidOn { get; set; }
            public DateTime? PaidUpTo { get; set; }
            public string Amount { get; set; }
            public string Balance { get; set; }
            
            public string Recipt { get; set; }
            public DateTime? LastPaidDate { get; set; }
            public DateTime? LastIncentiveDate { get; set; }
           
           
            public Int64 FK_Employee { get; set; }
            public Int64 FK_Designation { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 TotalCount { get; set; }
            public Int32 pageSize { get; set; }
            public Int32 pageIndex { get; set; }

        }
       

        public APIGetRecordsDynamic<GetReportDataOut> GetReportdata(IncentiveReportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetReportDataOut, IncentiveReportdto>(companyKey: companyKey, procedureName: "ProIncentiveReportdata", parameter: input);
        }
    }
}