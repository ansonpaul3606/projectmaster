using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class CustomerTicketsModel
    {
        public class customerview
        {
            public string FK_Employee { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Rptype { get; set; }
            public string ReportField { get; set; }
            public int Status { get; set; }

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
        public class StatusViewList
        {
            public List<Status> StatusList { get; set; }
            
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
        public class Customerlist
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
            public List<EmployeeInfo> EmployeeInfoList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<GetProduct> ProductList { get; set; }
            public List<CustomerTicketsModel.EmployeeLeadInfo> EmployeeLeadInfoList { get; set; }
             public List<NextAction> NextActionList { get; set; }
            public List<ActionTypes> Actntyplists { get; set; }
            public List<Status> StatusList { get; set; }
           public List<ReportFields>RpFields { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<SummaryList> summaryLists { get; set; }
            public List<TargetCriteria> TargetCriteriaList { get; set; }
            public List<Department> deprtmnt { get; set; }
            public List<GroupData> GroupList { get; set; }
            public long FK_BranchMode { get; set; }
            public List<SummaryList> NewListSummary { get; set; }
        }
        public class Department
        {
            public int DepId { get; set; }
            public string Depname { get; set; }

        }
        public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }


        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class EmployeeLeadInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public string EmpLName { get; set; }
            public long ID_Branch { get; set; }
            public long ID_BranchMode { get; set; }
            public long ID_BranchType { get; set; }
            public long FK_Department { get; set; }
            public string BTName { get; set; }
            public string BrName { get; set; }
            public string DeptName { get; set; }

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
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
        }
        public class ActionTypes
        {
            public int ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }
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
        public class Reportdto
        {
            //public Int64 EmployeeID { get; set; }
            //public int Mode { get; set; }
            //public Int64 BranchID { get; set; }
            //public DateTime? FromDate { get; set; }
            //public DateTime? ToDate { get; set; }
            //public Int64 FK_company { get; set; }
            //public string TransMode { get; set; }
            //public string Companyname { get; set; }
            //public Int32 PageIndex { get; set; }
            //public Int32 PageSize { get; set; }
            //public long ID_Report { get; set; }
            //public long ID_Product { get; set; }
            //public long FK_Priority { get; set; }
            //public long FK_NetAction { get; set; }
            //public long Collectedby_ID { get; set; }
            //public long FK_Category { get; set; }
            //public long LeadNo { get; set; }
            //public long Criteria { get; set; }
            //public long Status { get; set; }
            //public long Area_ID { get; set; }
            //public DateTime? ASonDate { get; set; }

            public long ReportMode { get; set; }
        public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Priority { get; set; }
            public long FollowUpAction { get; set; }
            public long FollowUpType { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long LeadNo { get; set; }
            public long TableCount { get; set; }
            public long Status { get; set; }
            public long Criteria { get; set; }
            public long FK_CollectedBy { get; set; }
            public long Category { get; set; }
            public long FK_Area { get; set; }
            public Int32 ProductType { get; set; }
            public string Destination { get; set; }
            public int TargetCriteria { get; set; }
            public long DepartmentID { get; set; }
            public long FK_District { get; set; }
            public long FK_AssignTo { get; set; }
            public long FK_AssignBy { get; set; }
            public long FK_CollectBy { get; set; }
            
        }
        
         public class GetLedgerList2ReportDataOut
        {
            public Int64 SlNo { get; set; }
            public string Product { get; set; }
            public string Action { get; set; }
            public string CommittedDate { get; set; }
            public string FollowUpThrough { get; set; }
            public string Status { get; set; }
            public string CompletedDate { get; set; }
            public string AssignedTo { get; set; }
            public long DueDays { get; set; }
            public string Remarks { get; set; }

            public string ProductID { get; set; }
            public string Mode2 { get; set; }
            public string Category { get; set; }
            public string ComCategory { get; set; }
           
        }
         public class GetLedgerList1ReportDataOut
        {

          
            public string CusNumber { get; set; }
            public string CusName { get; set; }
            public string CusAddress { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string LeadFrom { get; set; }
            public string Mode1 { get; set; }
            public string CurrentStatus { get; set; }
            public string LgCollectedBy { get; set; }
            

        }
        public class GetNewListReportDataOut
        {

            public Int64 SlNo { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string CollectedBy { get; set; }
            public long PriorityID { get; set; }
            public string Priority { get; set; }

            public string CurrentStatus { get; set; }
            public string Criteria { get; set; }
            public string Branch { get; set; }
            public string Remarks { get; set; }
            public string LgCollectedBy { get; set; }

            public string Area { get; set; }
            public string LeadFrom { get; set; }
            public string LeadByName { get; set; }
            public string AssignedTo { get; set; }
            public string Mobile { get; set; }
            public long ComCategory { get; set; }


        }

        public class GetFollowUpListReportDataOut
        {

            public Int64 SlNo { get; set; }
            public Int64 ID_LeadGenerateProduct { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string NextAction { get; set; }
            public string NextActionDate { get; set; }
            public string FollowUpMethod { get; set; }
            public string AssignedTo { get; set; }
            public string CompletedDate { get; set; }
            public long DueDays { get; set; }
            public string Status { get; set; }
            public string Branch { get; set; }
            public long PriorityID { get; set; }
            public string Priority { get; set; }
            public string criteria { get; set; }
            public string Remarks { get; set; }
            public string LgCollectedBy { get; set; }
            public string Category { get; set; }
            public string Area { get; set; }
            public string Mobile { get; set; }
            public long ComCategory { get; set; }
        }
        public class GetStatusListReportDataOut
        {

            public Int64 SlNo { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string NextAction { get; set; }
            public string NextActionDate { get; set; }
            public string FollowUpMethod { get; set; }
            public string AssignedTo { get; set; }
            public long DueDays { get; set; }
            public string CompletedDate { get; set; }
            public string Status { get; set; }
            public string Branch { get; set; }
            public long PriorityID { get; set; }
            public string Priority { get; set; }
            public string criteria { get; set; }
            public string Remarks { get; set; }
            public string LgCollectedBy { get; set; }
            public string Area { get; set; }
            public string Mobile { get; set; }
            public long ComCategory { get; set; }


        }
        public class GetActionListReportDataOut
        {
            //    //public decimal IncNetAmount { get; set; }
            //    //public decimal SUMVALUE { get; set; }
            //    //public decimal Balance { get; set; }
            //    public Int64 SlNo { get; set; }
            //    //public string EmpFName { get; set; }
            //    //public string Opening { get; set; }
            //    //public string Incentive { get; set; }
            //    //public string paid { get; set; }
            //    //public DateTime? PaidOn { get; set; }
            //    //public DateTime? PaidUpTo { get; set; }
            //    //public string Amount { get; set; }
            //    //public string Balance { get; set; }
            //    //public string Recipt { get; set; }
            //    //public DateTime? LastPaidDate { get; set; }
            //    //public DateTime? LastIncentiveDate { get; set; }
            //    public Int64 FK_Employee { get; set; }
            //    public Int64 FK_Branch { get; set; }
            //    public Int64 TotalCount { get; set; }
            //    public Int32 pageSize { get; set; }
            //    public Int32 pageIndex { get; set; }

            public Int64 SlNo { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string NextAction { get; set; }
            public string NextActionDate { get; set; }
            public string FollowUpMethod { get; set; }
            public string AssignedTo { get; set; }
            public long DueDays { get; set; }
            public string criteria { get; set; }
            public string Branch { get; set; }
            public long FK_Priority { get; set; }
            public string Status { get; set; }
            public long PriorityID { get; set; }
            public string Priority { get; set; }
            public string Remarks { get; set; }
            public string LgCollectedBy { get; set; }
            public string Category { get; set; }
            public string Area { get; set; }
            public string Mobile { get; set; }
            public long ComCategory { get; set; }


        }
        public class ReportLeadSummaryInput
        {
            public long ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Company { get; set; }
            //public long Criteria { get; set; }
            public long FK_Category { get; set; }

        }
        public class GetProductWiseListReportDataOut
        {

            public Int64 SlNo { get; set; }
            public string Category { get; set; }
            public string Product { get; set; }
            public string Count { get; set; }
            public string Criteria { get; set; }
            public long ComCategory { get; set; }
            //    //public string Incentive { get; set; 
        }

        public class SummaryList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class GetLeadSummaryData
        {
            public Int64 ID_Table { get; set; }
            public string Employee { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string Category { get; set; }
            public string Product { get; set; }
            public decimal Quantity { get; set; }
            public string Nextaction { get; set; }
            public string ActionType { get; set; }
            public string AssignedTo { get; set; }
            public string Customer { get; set; }
            public string LeadStatusOn { get; set; }
            public string LeadStatus { get; set; }
            public long Opening { get; set; }
            public long New { get; set; }
            public long Closed { get; set; }
            public long Lost { get; set; }
            public long Balance { get; set; }
                   

            
        }

        public class TargetCriteria
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GroupModeinput
        {
            public Int32 Mode { get; set; }
            public long Group { get; set; }
        }
        public class GroupData
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public APIGetRecordsDynamicdn<DataTable> GetCustomerReport(customerid input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<customerid>(companyKey: companyKey, procedureName: "ProRptTicketList", parameter: input);

        }
        public APIGetRecordsDynamic<Status> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Status, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<GetActionListReportDataOut> GetActionListReportdata(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetActionListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
       
        public APIGetRecordsDynamicdn<DataTable> GetActionListReportdataTab(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetStatusListReportDataTab(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);

        }
        public APIGetRecordsDynamic<GetStatusListReportDataOut> GetStatusListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetStatusListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetStatusListReportdataTab(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetFollowUpListReportDataTab(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamic<GetFollowUpListReportDataOut> GetFollowUpListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetFollowUpListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetNewListReportDataTab(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamic<GetNewListReportDataOut> GetNewListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetNewListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamic<GetProductWiseListReportDataOut> GetProductwiseLeadListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetProductWiseListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
      
        public APIGetRecordsDynamic<GetLedgerList1ReportDataOut> GetLedgerListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetLedgerList1ReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamic<GetLedgerList2ReportDataOut> GetLedgerListDetailsReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetLedgerList2ReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }

        public APIGetRecordsDynamic<GetLeadSummaryData> GetLeadSummaryGrid(ReportLeadSummaryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetLeadSummaryData, ReportLeadSummaryInput>(companyKey: companyKey, procedureName: "ProRptLeadSummary", parameter: input);
        }

        //public APIGetRecordsDynamicdn<DataTable> GetLeadSummary(ReportLeadSummaryInput input, string companyKey)
        //{
        //    return Common.GetDataViaProcedureDatatable<ReportLeadSummaryInput>(companyKey: companyKey, procedureName: "ProRptLeadSummary", parameter: input);
        //}


        #region for Tabulator
        public APIGetRecordsDynamicdn<DataTable> GetProductwiseLeadListRptData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetLeadSummary(ReportLeadSummaryInput input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<ReportLeadSummaryInput>(companyKey: companyKey, procedureName: "ProRptLeadSummary", parameter: input);
        }
        public APIGetRecordsDynamic<SummaryList> GeSummaryList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<SummaryList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<TargetCriteria> GetTargetCriteria(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<TargetCriteria, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetEmployeeWiseTargetListRptData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamic<GroupData> GetGroupList(GroupModeinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GroupData, GroupModeinput>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetLeadAssignListRptData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        #endregion
    }
}