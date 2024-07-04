using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class GSTReportModel
    {

        public class GSTReportView
        {

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Companyname { get; set; }
            public List<Reports> ReportsList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<TaxType> TaxTypeList { get; set; }
            public List<Criteria> CriteriaList { get; set; }
            public long Reporttype { get; set; }
            public List<ModeList> ModeList { get; set; }

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
        public class TaxType
        {
            public Int32 TaxTypeID { get; set; }
            public string TaxTypeName { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }

        public class ModeList
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }


        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class Criteria
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class CriteriaViewList
        {
            public List<Criteria> CriteriaList { get; set; }

        }

        public APIGetRecordsDynamic<Criteria> GroupingCriteriaList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Criteria, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}