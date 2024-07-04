using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SalesReturnGSTReportModel
    {
        public class SalesReturnGSTReportListModel
        {           
            public List<Department> DepartmentList { get; set; }          
            public List<Branchs> BranchList { get; set; }          

        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class SalesReturnGSTReportView
        {
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }

        }
    }
}