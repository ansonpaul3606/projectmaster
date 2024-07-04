using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class StockReportModel
    {

        public  class StockReportView
        {

            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int32 EmployeeID { get; set; }
            public string Employee { get; set; }
            public List<Reports> ReportsList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<BranchTo> BranchListTo { get; set; }

            public string Companyname { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public List<ModeList> ModeList { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<Products> ProductList { get; set; }
            public List<Criteria> CriteriaList { get; set; }
            public long Reporttype { get; set; }
            public long FK_BranchMode { get; set; }
        }
        public class ModeList
        {
            public long ModeID { get; set; }
            public string ModeNames { get; set; }
            public string Modes { get; set; }
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
        public class BranchTo
        {
            public Int32 BranchIDTo { get; set; }
            public string BranchNameTo { get; set; }
        }
        public class DepartmentTo
        {
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }

        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class Category
        {
            public Int32 CategoryID { get; set; }
            public string CategoryName { get; set; }

        }
        public class Products
        {
            public Int32 ID_Product { get; set; }
            public string Product { get; set; }
           public string ProductCode { get; set; }
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