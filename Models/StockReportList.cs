using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PerfectWebERP.Models
{
    public class StockReportList
    {        
            public class StockReportListView
            {
                public string FromDate { get; set; }
                public string ToDate { get; set; }
                public Int32 EmployeeID { get; set; }
                public string Employee { get; set; }
                public List<Reports> ReportsList { get; set; }
                public List<Branchs> BranchList { get; set; }
                public List<BranchTo> BranchListTo { get; set; }
            public int ReportMode { get; set; }
         
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Department { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Category { get; set; }
            public int Criteria { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public string ActionStatus { get; set; }
            public string EntrBy { get; set; }
            public long FK_BillType { get; set; }
            public int TableCount { get; set; }
            public long FK_BranchTo { get; set; }
            public long FK_Employee { get; set; }
            public long FK_BranchFrom { get; set; }
            public long ID_Master { get; set; }
            public string BillNo { get; set; }
            public string TranSMode { get; set; }
            public long FK_DepartmentFrom { get; set; }
            public long FK_DepartmentTo { get; set; }
            public long FK_EmployeeFrom { get; set; }
            public long FK_EmployeeTo { get; set; }
            public long FK_PaymentMethod { get; set; }
            public long FK_ProductNew { get; set; }
            public string HeaderColor { get; set; }
            public long FK_ProductOld { get; set; }
            public string Companyname { get; set; }
                public List<Department> DepartmentList { get; set; }
                public List<DepartmentTo> DepartmentListTo { get; set; }
                public List<ModeList> ModeList { get; set; }
                public List<Category> CategoryList { get; set; }
                public List<Products> ProductList { get; set; }
                public List<Criteria> CriteriaList { get; set; }
          

            public List<ActionStatus> ActionStatusList { get; set; }
            public string FK_Mode_JSON { get; set; }


            public List<string> FK_Mode//ok
            {
                get
                {

                    if (this.FK_Mode_JSON is null)
                    {
                        return default(List<string>);
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<string>>(this.FK_Mode_JSON);
                    }
                }
            }
            public long Reporttype { get; set; }
            public List<string> ActionlistArray { get; set; }
            public long FK_BranchMode { get; set; }
        }
            
        public class ModeList
            {
                public long ModeID { get; set; }
                public string ModeNames { get; set; }
                public string Modes { get; set; }
            }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Actionlist
        {
            public string FK_Mode { get; set; }
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
        public class Productlistviewinput
        {
            public long ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }   
            
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Department { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Category { get; set; }
            public long Criteria { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }         
            public string ActionStatus { get; set; }            
            public string EntrBy { get; set; }
            public long FK_BillType { get; set; }
            public long TableCount { get; set; }
            public long FK_BranchTo { get; set; }
            public long FK_Employee { get; set; }
            public long FK_BranchFrom { get; set; }
            public long ID_Master { get; set; }
            public string BillNo { get; set; }
            public string TranSMode { get; set; }
            public long FK_DepartmentFrom { get; set; }
            public long FK_DepartmentTo { get; set; }
            public long FK_EmployeeFrom { get; set; }
            public long FK_EmployeeTo { get; set; }
            public long FK_PaymentMethod { get; set; }
            public long FK_ProductNew { get; set; }
            public string HeaderColor { get; set; }
            public long FK_ProductOld { get; set; }

     }
        public class ProductlistlistOutputView
        {
           
            public string LeadNo { get; set; }
            public string Customer { get; set; }
            public string Mobile { get; set; }

            public string AssignedTo { get; set; }
            public string AssignedDate { get; set; }

        }

        public APIGetRecordsDynamic<ProductlistlistOutputView> GetProductlistlList(Productlistviewinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductlistlistOutputView, Productlistviewinput>(companyKey: companyKey, procedureName: "ProRptInventory", parameter: input);

        }


        public APIGetRecordsDynamic<Criteria> GroupingCriteriaList(ModeLead input, string companyKey)
            {
                return Common.GetDataViaProcedure<Criteria, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

            }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }


    }
}
