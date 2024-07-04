using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class GraphSettingsModel
    {
        public class customerview
        {
            public string FK_Employee { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Rptype { get; set; }
            public string ReportField { get; set; }
            public int Status { get; set; }

            public Int64 ID_MenuGroup { get; set; }
            public Int64 ID_Mode { get; set; }
            public Int64 ID_SubModule { get; set; }
            //public Int16 SLNO { get; set; }
            //public string CustomerName { get; set; }
            //public string CustomerAddress { get; set; }
            //public string TicketNo { get; set; }
            //public string AssignedEmployee { get; set; }
            //public string Status { get; set; }
            //public string VisitDate { get; set; }  
        }
        public class customerid
        {
            public string FK_Employee { get; set; }
            //public string Rptype { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 Status { get; set; }
            public string cusName { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class Status
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Module
        {
            public Int64 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class SubModule
        {
            public Int64 Mode { get; set; }
            public Int64 Group { get; set; }
        }
        public class SubModuleType
        {

            public Int64 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GraphType
        {

            public Int64 FK_GraphType { get; set; }
            public string GraphName { get; set; }
        }
        public class StatusViewList
        {
            public List<Status> StatusList { get; set; }
            
        }
        public class SubModuleList
        {
            public List<SubModuleType> SubModuleData { get; set; }

        }
        public class ReportFields
        {
            public Int32 Report_Id { get; set; }
            public string ReportField { get; set; }
            public ReportFields()
            {
                Report_Id = 1;
                ReportField = "test1";
                Report_Id = 2;
                ReportField = "test2";
            }

        }
        public class Graphlist
        {
            public string CompName { get; set; }
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            //public int EmployeeID { get; set; }
            //public string Employeename { get; set; }
            //public string DeptName { get; set; }
            //public string Designation { get; set; }
            public long FK_Employee { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Module> ModuleList { get; set; }
        }
        public class DashboardDetails
        {
            public List<ChartListData> ChartData { get; set; }
        }
        public class ChartListData
        {
            public long ChartID { get; set; }
            public string ChartName { get; set; }
            public string xValues { get; set; }
            public string yValues { get; set; }
            public string yColor { get; set; }
            public string ySecondValues { get; set; }
            public string ySecondColor { get; set; }
            public string yThirdValues { get; set; }
            public string yThirdColor { get; set; }
            public string XAxis { get; set; }
            public string YAxis { get; set; }
            public string ChartModalName { get; set; }
            public bool ShowYInhndreds { get; set; }
            public bool ShowY2Inhndreds { get; set; }
            public bool ShowY3Inhndreds { get; set; }
            public bool ShowXInhndreds { get; set; }
            public long DevideXAmnt { get; set; }
            public long DevideYAmnt { get; set; }
            public long DevideY2Amnt { get; set; }
            public long DevideY3Amnt { get; set; }

        }
        public class DashboardData
        {
            public int Leads { get; set; }
            public int Complaints { get; set; }
            public int Services { get; set; }
            public int Projects { get; set; }
            public int Products { get; set; }
            public decimal LeadHot { get; set; }
            public decimal LeadWarm { get; set; }
            public decimal LeadCold { get; set; }
            public int LeadColdCount { get; set; }
            public int LeadHotCount { get; set; }
            public int LeadWarmCount { get; set; }
            public decimal LeadFollowupPendingDue { get; set; }
            public decimal LeadFollowupPendingTodays { get; set; }
            public decimal LeadFollowupPendingUpComing { get; set; }

            public int LeadFollowupPendingDueCount { get; set; }
            public int LeadFollowupPendingTodaysCount { get; set; }
            public int LeadFollowupPendingUpComingCount { get; set; }
            public decimal ProjectStart { get; set; }
            public decimal ProjectPause { get; set; }
            public decimal ProjectCompleted { get; set; }
            public decimal ProjectStartCount { get; set; }
            public decimal ProjectPauseCount { get; set; }
            public decimal ProjectCompletedCount { get; set; }
            public decimal ServiceTodaysPending { get; set; }
            public decimal ServiceOverDue { get; set; }
            public decimal ServiceUpcoming { get; set; }
            public int ServiceTodaysPendingCount { get; set; }
            public int ServiceOverDueCount { get; set; }
            public int ServiceUpcomingCount { get; set; }
            public IEnumerable<ListData> MonthList { get; set; }
            public IEnumerable<ListData> YearList { get; set; }
           
        }
        public enum CustomColorClass
        {
            bg_primary,
            bg_secondary,
            bg_success,
            bg_info,
            bg_warning,
            bg_danger

        }
        public class ListData
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
           
        }
        public class GetProduct
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            //public string ProdShortName { get; set; }
            //public string ProdHSNCode { get; set; }
        }
       
        
        public class cusview
        {
            public Int16 SLNO { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public string TicketNo { get; set; }
            public string AssignedEmployee { get; set; }
            public string Status { get; set; }
            public string VisitDate { get; set; }
            public string CompanyName { get; set; }
            public string BranchName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public byte[] img { get; set; }
            public byte[] footer { get; set; }
            public byte[] wmark { get; set; }
        }
        public class DashboardOutData
        {
            public long ID_DashBoardDetails { get; set; }
            public string ChartName { get; set; }
            public dynamic Datafile { get; set; }
            public long ChartList { get; set; }
            public long ChartType { get; set; }
            public string XAxis { get; set; }
            public string YAxis { get; set; }
            public string Remarks { get; set; }
            public long GraphType { get; set; }
        }
        public class GetDashboardInData
        {
            public long FK_Employee { get; set; }
            public long FK_Department { get; set; }
            public long FK_Branch { get; set; }
            public int FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public DateTime AsOnDate { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Module  { get; set; }
            public long  FK_SubModule { get; set; }

            public Int16 FK_GraphType { get; set; }
            public Int32 DashMode  { get; set; }
            public Int16 ChartType   { get; set; }
    }
        public class Reportdto
        {
            

            public long ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Module { get; set; }
            public long FK_SubModule { get; set; }
            public long Debug { get; set; }
          
            public long TableCount { get; set; }
            public long Status { get; set; }
            public long Criteria { get; set; }
           

          
        }
        
       
     
        public APIGetRecordsDynamicdn<DataTable> GetCustomerReport(customerid input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<customerid>(companyKey: companyKey, procedureName: "ProRptTicketList", parameter: input);

        }
        public APIGetRecordsDynamic<Module> GetModuleList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Module, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<SubModuleType> GetSubModule(SubModule input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubModuleType, SubModule>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<GraphType> GetGraphType(SubModule input, string companyKey)
        {
            return Common.GetDataViaProcedure<GraphType, SubModule>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<DashboardOutData> GetDashboard(GetDashboardInData input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashboardOutData, GetDashboardInData>(companyKey: companyKey, procedureName: "ProERPDashBoardDetails", parameter: input);
        }


        #region for Tabulator
        public APIGetRecordsDynamicdn<DataTable> GetProductwiseLeadListRptData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        #endregion
    }
}