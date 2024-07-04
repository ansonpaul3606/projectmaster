using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class AuthorizationReportModel
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
           
            public Int64 ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
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
            public List<Module> ModuleList { get; set; }
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
        public APIGetRecordsDynamic<GetActionListReportDataOut> GetAuthorizationReportdata(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetActionListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptAuthorizationList", parameter: input);
        }

        public APIGetRecordsDynamic<GetStatusListReportDataOut> GetStatusListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetStatusListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        public APIGetRecordsDynamic<GetFollowUpListReportDataOut> GetFollowUpListReportData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetFollowUpListReportDataOut, Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
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
        #region for Tabulator
        public APIGetRecordsDynamicdn<DataTable> GetProductwiseLeadListRptData(Reportdto input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportdto>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);
        }
        #endregion
    }
}