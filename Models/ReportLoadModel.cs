using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERP.General;
using System.Data;
using Newtonsoft.Json;
using static PerfectWebERP.Models.SupplierReportModel;

namespace PerfectWebERP.Models
{
    public class ReportLoadModel
    {
        public class Reportproparam
        {
            public string FK_Employee { get; set; }
            //public string Rptype { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 Status { get; set; }
            public string cusName { get; set; }
            public byte TableCount { get; set; }
        }
        public class Reportprocedureparams
        {
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Priority { get; set; }
            public long FollowUpAction { get; set; }
            public long FollowUpType { get; set; }
            public long FK_Company { get; set; }
            //public Int64 Status { get; set; }
            //public string cusName { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public string LeadNo { get; set; }
          
            public int TableCount { get; set; }
            //public long Debug { get; set; }
            public int Criteria { get; set; }
            public int Status { get; set; }

            public long FK_CollectedBy { get; set; }
            public long Category { get; set; }
            public long FK_Area { get; set; }


        }
        public class Reportparameters
        {
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Project { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Priority { get; set; }
            public long FollowUpAction { get; set; }
            public long FollowUType { get; set; }
            public long FK_Company { get; set; }
            public int Rptype { get; set; }
            public string Reportname { get; set; }
            public string ReportSchema { get; set; }
            public byte TableCount { get; set; }
            public string filename { get; set; }
            public string CompName { get; set; }
            public string Branchname { get; set; }
            public string LeadNo { get; set; }
            public int Status { get; set; }

            public int Criteria { get; set; }
            public string Employee { get; set; }
            public string Prodname { get; set; }
            public string Actioname { get; set; }
            public string Actiontypname { get; set; }
            public string Statusname { get; set; }
            public string Critername { get; set; }
            public string Leadname { get; set; }
            public string Priority { get; set; }
            public string complaintservicename { get; set; }
            public long ComplaintService { get; set; }
            public string areas { get; set; }
            public string categorys { get; set; }
            public string nextaction { get; set; }



            public long ComplaintType { get; set; }
            public long ComplaintProductType { get; set; }
            public string TicketNo { get; set; }

            public long FK_CollectedBy { get; set; }
            public long Category { get; set; }
            public long FK_Area { get; set; }
            public long FK_Category { get; set; }
            public long FK_NextAction { get; set; }
            public long DueDaysFrom { get; set; }
            public long DueDaysTo { get; set; }
            public long DueCriteria { get; set; }
            public long FK_Servicetype { get; set; }

            public string ReportFrom { get; set; }

            public long ReplacementType { get; set; }
            public string ReplacementTypeName { get; set; }
            public bool Detailed { get; set; }
            public long FK_Expense { get; set; }
            public long FK_Stage { get; set; }
            public long FK_PettyCashier { get; set; }
            public int ReportSubMode { get; set; }
            public long BillingType { get; set; }
        }
        public class AuditReports
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_MenuList { get; set; }
            public long FK_UserRole { get; set; }
            public string ReferenceNo { get; set; }
            public long FK_Users { get; set; }
            public long Action { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public string TransMode { get; set; }


        }
        //public class Reportdata
        //{
        //    public Int16 SLNO { get; set; }
        //    public string CustomerName { get; set; }
        //    public string CustomerAddress { get; set; }
        //    public string TicketNo { get; set; }
        //    public string AssignedEmployee { get; set; }
        //    public string Status { get; set; }
        //    public string VisitDate { get; set; }
        //    public string CompanyName { get; set; }
        //    public string BranchName { get; set; }
        //    public string FromDate { get; set; }
        //    public string ToDate { get; set; }
        //    public byte[] img { get; set; }
        //    public byte[] footer { get; set; }
        //    public byte[] wmark { get; set; }
        //}
        public class usdata
        {
            public string FK_Employee { get; set; }
            public int Rptype { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int Status { get; set; }
            public string RpFieldptype { get; set; }
            public int Report_Id { get; set; }
        }
        public class Rppsarams
        {

            public string FK_Employee { get; set; }
            public int Rptype { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int Status { get; set; }
            public string RpFieldptype { get; set; }
            public int ReportMode { get; set; }
        }
        public class ServiceReportprocedureparams
        {
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Priority { get; set; }
            public long FK_Company { get; set; }
            //public Int64 Status { get; set; }
            //public string cusName { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }
            //public long Debug { get; set; }
            public int Criteria { get; set; }
            public int Status { get; set; }

            public long ComplaintType { get; set; }
            //public long ComplaintProductType { get; set; }
            public string TicketNo { get; set; }
            public long FK_Area { get; set; }
            public long FK_Category { get; set; }
            public long FK_NextAction { get; set; }
            public long DueDaysFrom { get; set; }
            public long DueDaysTo { get; set; }
            public long DueCriteria { get; set; }
            //public long FK_Servicetype { get; set; }
            public long ComplaintService { get; set; }
            public long ReplacementType { get; set; }
            public long BillingType { get; set; }
        }
        public class ProjectReportprocedureparams
        {
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public long FK_Company { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Project { get; set; }
            //public Int64 Status { get; set; }
            //public string cusName { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }
            //public long Debug { get; set; }
            public int Criteria { get; set; }
            public bool Detailed { get; set; }
            public string LeadNo { get; set; }
            public long FK_Area { get; set; }
            public long Category { get; set; }
            public long FK_Branch { get; set; }
            public long FK_PettyCashier { get; set; }
            public int ReportSubMode { get; set; }
            //public long FK_Employee { get; set; }


        }
        public class SalesReports
        {

          
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public long FK_Company { get; set; }
            public long FK_BillType { get; set; }
            public long FK_PaymentMethod { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Category { get; set; }
            public long FK_Department { get; set; }
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            public string TranSMode { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }
            //public long Debug { get; set; }
            public int Criteria { get; set; }
            
            public string BillNo { get; set; }

            public int ID_Master { get; set; }

        }
        public class invReports
        {


            public int FK_SALES { get; set; }
          
            public int TableCount { get; set; }
        
        }
        public class StockReports
        {


            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public long FK_Company { get; set; }
        

            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_BranchTo { get; set; }
            public long FK_Category { get; set; }
            public long FK_Department { get; set; }
            public long FK_DepartmentTo { get; set; }
            public long FK_Product { get; set; }
            public long FK_Employee { get; set; }
            public long FK_EmployeeTo { get; set; }

            public long FK_ProductOld { get; set; }
            public long FK_ProductNew { get; set; }

            public string EntrBy { get; set; }
            public string TranSMode { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }

            public int Criteria { get; set; }

            public string ActionStatus { get; set; }

        }
        public class GSTReports
        {


            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public long FK_Company { get; set; }

            public long FK_TaxType { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Customer { get; set; }
            public string Mode { get; set; }

            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
           
            public long FK_Department { get; set; }
           
            public string EntrBy { get; set; }
            public string TranSMode { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }

           // public int Criteria { get; set; }



        }
        public class PurchaseReportProcedureparms
        {


            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public long FK_Company { get; set; }
            public long FK_BillType { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Category { get; set; }
            public long FK_Department { get; set; }
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            public string TranSMode { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }
            //public long Debug { get; set; }
            public int Criteria { get; set; }

            public string BillNo { get; set; }



        }
        public class AccountsReportProcedure
        {
            public int ReportMode { get; set; }
            public long FK_Branch { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int TableCount { get; set; }
        }

        public class AMCWarrantyReports
        {


            public int FK_Master { get; set; }
            public int Modes { get; set; }
            public int TableCount { get; set; }

        }
        public class serviceReports
        {


            public long FK_ServiceBill { get; set; }

            public int TableCount { get; set; }

        }
        public class PayRollReports
        {


            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }

            public long FK_Company { get; set; }


            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
          
            public long FK_Department { get; set; }
           
            public long FK_Employee { get; set; }
           

            public string EntrBy { get; set; }
            public string TranSMode { get; set; }
            public long FK_Machine { get; set; }

            public int TableCount { get; set; }

            public int Criteria { get; set; }



        }
        public class CustomerReports
        {


            public int ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_CustomerSector { get; set; }
            public long FK_CustomerType { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public long FK_State { get; set; }
            public long FK_District { get; set; }
            public long FK_Area { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public string TransMode { get; set; }
            public int Criteria { get; set; }
            //public DateTime AsonDate { get; set; }
            public long TableCount { get; set; }


        }
        public class ShortageReports
        {
            public int ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Product { get; set; }
            public long FK_Purchase { get; set; }            

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public string TransMode { get; set; }
            public int Criteria { get; set; }

        }
        public class MediaPromoReports
        {
            public int ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Media { get; set; }
            public long FK_SubMedia { get; set; }


            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public string TransMode { get; set; }
            public int Criteria { get; set; }

        }
        public class SalesReturnGSTReports
        {
            public int ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public int Criteria { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Department { get; set; }
            public string TranSMode { get; set; }
            
        }
    
        public APIGetRecordsDynamicdn<DataTable> GetReport(Reportproparam input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportproparam>(companyKey: companyKey, procedureName: "ProRptTicketList", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> ReportGet(Reportprocedureparams input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Reportprocedureparams>(companyKey: companyKey, procedureName: "ProRptLeadManagement", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> ServiceReportGet(ServiceReportprocedureparams input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<ServiceReportprocedureparams>(companyKey: companyKey, procedureName: "ProRptServiceManagement", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> ProjectReportGet(ProjectReportprocedureparams input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<ProjectReportprocedureparams>(companyKey: companyKey, procedureName: "ProRptProject", parameter: input);

        }
        //public APIGetRecordsDynamicdn<DataSet> GetProjectReportSummary(ProjectReportprocedureparams input, string companyKey)
        //{
        //    return Common.GetDataViaProcedureDataSet<ProjectReportprocedureparams>(companyKey: companyKey, procedureName: "ProRptProject", parameter: input);

        //}
        public APIGetRecordsDynamicdn<DataTable> GetSalesReport(SalesReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<SalesReports>(companyKey: companyKey, procedureName: "ProRptInventory", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetInvoice(invReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<invReports>(companyKey: companyKey, procedureName: "ProInvoiceBill", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetStockReport(StockReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<StockReports>(companyKey: companyKey, procedureName: "ProRptInventory", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetGSTReport(GSTReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<GSTReports>(companyKey: companyKey, procedureName: "ProRptTax", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetPurchaseReport(PurchaseReportProcedureparms input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<PurchaseReportProcedureparms>(companyKey: companyKey, procedureName: "ProRptInventory", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetAccountsReport(AccountsReportProcedure input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<AccountsReportProcedure>(companyKey: companyKey, procedureName: "ProRptAccounts", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetAMCWarrantyInvoice(AMCWarrantyReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<AMCWarrantyReports>(companyKey: companyKey, procedureName: "ProAMCWarrantyInvoice", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetServiceInvoice(serviceReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<serviceReports>(companyKey: companyKey, procedureName: "ProServiceInvoice", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetPayRollReport(PayRollReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<PayRollReports>(companyKey: companyKey, procedureName: "ProPayRollReport", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetCustomerReport(CustomerReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<CustomerReports>(companyKey: companyKey, procedureName: "ProRptCustomer", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetShortageReport(ShortageReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<ShortageReports>(companyKey: companyKey, procedureName: "ProRptShortageMarking", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetSlaReport(SlaReportsParams input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<SlaReportsParams>(companyKey: companyKey, procedureName: "ProRptSlaReport", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetMediaPromotionReport(MediaPromoReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<MediaPromoReports>(companyKey: companyKey, procedureName: "ProRptMediaPromotion", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetSalesReturnGSTReport(SalesReturnGSTReports input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<SalesReturnGSTReports>(companyKey: companyKey, procedureName: "ProRptInventory", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetAuditTrailReport(AuditReportParams input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<AuditReportParams>(companyKey: companyKey, procedureName: "ProRptAuditTrailReport", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> GetOtherChargeGeneralReport(OtherChargeReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<OtherChargeReportInput>(companyKey: companyKey, procedureName: "ProRptOtherChargeType", parameter: input);

        }

        public APIGetRecordsDynamicdn<DataTable> GetSupplierOutStandingReportView(Suppliergridinput input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<Suppliergridinput>(companyKey: companyKey, procedureName: "proSupplieroutstandingReportView", parameter: input);
        }

        public APIGetRecordsDynamicdn<DataTable> PurchaseReportTablr(PurchaseReportProcedureparms input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<PurchaseReportProcedureparms>(companyKey: companyKey, procedureName: "ProRptInventory", parameter: input);

        }

    }
    public class Rpparams
    {
        public string FK_Employee { get; set; }
        public int Rptype { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Status { get; set; }
        public string RpFieldptype { get; set; }
        public string Reportname { get; set; }
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
    }
    public class Reportpassparams
    {
        public int ID_Master { get; set; }
        public int FK_SALES { get; set; }
        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BillNo { get; set; }
      
        public string categorys { get; set; }
        public long FK_Product { get; set; }
        public long FK_Branch { get; set; }
        public long FK_BranchTo { get; set; }
        public long FK_Employee { get; set; }
        public long FK_EmployeeTo { get; set; }
        public long FK_Priority { get; set; }
        public long FollowUpAction { get; set; }
        public long FollowUType { get; set; }
        public long FK_Company { get; set; }
        public int Rptype { get; set; }
        public string Reportname { get; set; }
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
        public string Filter { get; set; }

        public string LeadNo { get; set; }

        public long FK_Supplier { get; set; }
        public string SuppName { get; set; }


        public int Criteria { get; set; }
        public int Status { get; set; }

        public long ComplaintType { get; set; }
        public long ComplaintProductType { get; set; }
        public string Reportsection { get; set; }
        public string TicketNo { get; set; }

        public long FK_CollectedBy { get; set; }
        public long Category { get; set; }
        public long FK_Area { get; set; }
        public long FK_Category { get; set; }
        public long FK_NextAction { get; set; }

        public long DueDaysFrom { get; set; }
        public long DueDaysTo { get; set; }
        public long DueCriteria { get; set; }
        public long FK_Servicetype { get; set; }
        public long ComplaintService { get; set; }
        public long FK_Department { get; set; }
    
        public long FK_DepartmentTo { get; set; }
        public long billtype { get; set; }
        public string depname { get; set; }
        public string TranSMode { get; set; }
        public string Critername { get; set; }
        public string Prodname { get; set; }
        public long taxtypeId { get; set; }
        public long pmtype { get; set; }
        public long FK_Customer { get; set; }
        public string Mode { get; set; }
        public long FK_TaxType { get; set; }
        public string ReportFrom { get; set; }

        public int FK_AccountHead { get; set; }
        public string AHeadName { get; set; }
        public int FK_AccountHeadSub { get; set; }
        public string ASHeadName { get; set; }
        public int Modes { get; set; }
        public int FK_Master { get; set; }
        public long ReplacementType { get; set; }
        public long FK_ServiceBill { get; set; }

        public long FK_ProductNew { get; set; }
        public long FK_ProductOld { get; set; }

    }
    public class saleReportpassparams
    {
      
        public int ID_Master { get; set; }
        public string BillNo { get; set; }
        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long FK_Product { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Department { get; set; }
        public long FK_Company { get; set; }
        public long FK_Category { get; set; }
        public int Rptype { get; set; }
        public string Reportname { get; set; }
        public string categorys { get; set; }
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
        public string Filter { get; set; }

        
        public long FK_Deparment { get; set; }
        public long billtype { get; set; }
        public string depname { get; set; }
        public string billtypename { get; set; }
        public string Critername { get; set; }
        public string Prodname { get; set; }
        public int Criteria { get; set; }
        public string Reportsection { get; set; }
        public long pmtype { get; set; }
        public string pmtypename { get; set; }

    }
    public class Invoiceparams
    {

        public int FK_SALES { get; set; }
            
        public int Rptype { get; set; }
        public string Reportname { get; set; }
     
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
        
        public string Reportsection { get; set; }

    }

    public class AMCWarrantyparams
    {

        public int FK_Master { get; set; }
        public int Modes { get; set; }

        public int Rptype { get; set; }
        public string Reportname { get; set; }

        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }

        public string Reportsection { get; set; }

    }
    public class stockReportpassparams
    {
       
        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long FK_Product { get; set; }
        public long FK_Employee { get; set; }
        public long FK_EmployeeTo { get; set; }
        public long FK_Branch { get; set; }
        public long FK_BranchTo { get; set; }
        public long FK_Department { get; set; }
        public long FK_DepartmentTo { get; set; }
        public long FK_Company { get; set; }
        public long FK_Category { get; set; }
        public int Rptype { get; set; }
        public string Reportname { get; set; }
        public string Categoryname { get; set; }
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
        public string BranchnameTo { get; set; }
        public string Employeename { get; set; }
        public string EmployeenameTo { get; set; }
        public string Filter { get; set; }
        public string Modes { get; set; }


       
        public long billtype { get; set; }
        public string depname { get; set; }
        public string Departmentname { get; set; }
        public string DepartmentnameTo { get; set; }
        public string billtypename { get; set; }
        public string Critername { get; set; }
        public string Prodname { get; set; }
        public int Criteria { get; set; }
        public string Reportsection { get; set; }

        public long FK_ProductNew { get; set; }
        public long FK_ProductOld { get; set; }

        public string ProductOld { get; set; }
        public string ProductNew { get; set; }
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
       
        public List<string> ActionlistArray { get; set; }
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

    public class PurchaseReportpassparams
    {

        public string BillNo { get; set; }
        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long FK_Product { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Department { get; set; }
        public long FK_Company { get; set; }
        public long FK_Category { get; set; }
        public long FK_Supplier { get; set; }
        public int Rptype { get; set; }
        public string Reportname { get; set; }
        public string categorys { get; set; }
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
        public string Filter { get; set; }
        public string SuppName { get; set; }

        public long FK_Deparment { get; set; }
        public long billtype { get; set; }
        public string depname { get; set; }
        public string billtypename { get; set; }
        public string Critername { get; set; }
        public string Prodname { get; set; }
        public int Criteria { get; set; }
        public string Reportsection { get; set; }
        public string TransMode { get; set; }

    }

  
    public class PurchaseReturnReportpassparams
    {

        public string BillNo { get; set; }
        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long FK_Product { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Department { get; set; }
        public long FK_Company { get; set; }
        public long FK_Category { get; set; }
        public long FK_Supplier { get; set; }
        public int Rptype { get; set; }
        public string Reportname { get; set; }
        public string categorys { get; set; }
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
        public string Filter { get; set; }
        public string SuppName { get; set; }

        public long FK_Deparment { get; set; }
        public long billtype { get; set; }
        public string depname { get; set; }
        public string billtypename { get; set; }
        public string Critername { get; set; }
        public string Prodname { get; set; }
        public int Criteria { get; set; }
        public string Reportsection { get; set; }
        public string TransMode { get; set; }
    }

    public class GSTReportpassparams
    {

        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
       
       
        public long FK_Branch { get; set; }
        public long FK_Department { get; set; }
        public long FK_Company { get; set; }
        public long TaxTypeID { get; set; }
        public string TaxType { get; set; }
        public long SupplierID { get; set; }
        public string SuppName { get; set; }
        public long CustomerID { get; set; }
        public string CusName { get; set; }
        public string ModeID { get; set; }
        public string ModeName { get; set; }
        public int Rptype { get; set; }
        public string Reportname { get; set; }
      
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
     
        public string Filter { get; set; }
       
        public string TaxTypename { get; set; }
        public string Departmentname { get; set; }
      
        public string Reportsection { get; set; }

    }
    public class AccountsReportpassparams
    {        
        public int ReportMode { get; set; }
        public long FK_Branch { get; set; }
        public int AccountHead { get; set; }
        public string AHeadName { get; set; }
        public int AccountHeadSub { get; set; }
        public string ASHeadName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        
        public int Rptype { get; set; }
        public string Reportname { get; set; }       
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string Branchname { get; set; }
        public string Filter { get; set; }


       
        public string depname { get; set; }
        public string Critername { get; set; }
        public int Criteria { get; set; }
        public string Reportsection { get; set; }      

    }


    public class ServiceInvoice
    {

        public long FK_ServiceBill { get; set; }

        public int Rptype { get; set; }
        public string Reportname { get; set; }

        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }

        public string Reportsection { get; set; }

    }

    public class PayRollReportpassparams
    {

        public int ReportMode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
       
        public long FK_Employee { get; set; }
       
        public long FK_Branch { get; set; }
      
        public long FK_Department { get; set; }
       
        public long FK_Company { get; set; }
     
        public int Rptype { get; set; }
        public string Reportname { get; set; }
    
        public string ReportSchema { get; set; }
        public byte TableCount { get; set; }
        public string filename { get; set; }
        public string CompName { get; set; }
        public string Branchname { get; set; }
      
        public string Employeename { get; set; }
      
        public string Filter { get; set; }
      



     
        public string depname { get; set; }
        public string Departmentname { get; set; }
       
        public int Criteria { get; set; }
        public string Reportsection { get; set; }

      
    }

    public class CustomerReportsParams
    {

        public int ReportMode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FK_Branch { get; set; }
        public long FK_CustomerSector { get; set; }
        public long FK_CustomerType { get; set; }
        public long FK_Customer { get; set; }
        public long FK_Category { get; set; }
        public long FK_Product { get; set; }
        public long FK_State { get; set; }
        public long FK_District { get; set; }
        public long FK_Area { get; set; }

        public long FK_Company { get; set; }
        public long FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public long FK_Machine { get; set; }

        public string TransMode { get; set; }
        public int Criteria { get; set; }
        public DateTime AsonDate { get; set; }


    }

    public class ShortageReportsParams
    {

        public int ReportMode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Supplier { get; set; }
        public long FK_Purchase { get; set; }
        public long FK_Product { get; set; }        

        public long FK_Company { get; set; }
        public long FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public long FK_Machine { get; set; }

        public string TransMode { get; set; }
        public int Criteria { get; set; }



    }


    public class SlaReportsParams
    {
        public DateTime AsOnDate { get; set; }
        public long FK_CustomerType { get; set; }
        public long FK_ComplaintList { get; set; }
        public long FK_Category { get; set; }
        public long FK_Product { get; set; }
        public long FK_Company { get; set; }
        public long FK_Branch { get; set; }
        public int FK_Criteria { get; set; }
    }

    public class GetSlaReports
    {
        public int ReportMode { get; set; }
        public DateTime AsOnDate { get; set; }
        public long FK_CustomerType { get; set; }
        public long FK_ComplaintList { get; set; }
        public long FK_Category { get; set; }
        public long FK_Product { get; set; }
        public long FK_Company { get; set; }
        public long FK_Branch { get; set; }
        public string TransMode { get; set; }
        public int FK_Criteria { get; set; }
    }




    public class MediaPromotionReportsParams
    {

        public int ReportMode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Media { get; set; }
        public long FK_SubMedia { get; set; }
        

        public long FK_Company { get; set; }
        public long FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public long FK_Machine { get; set; }

        public string TransMode { get; set; }
        public int Criteria { get; set; }



    }
    public class SalesReturnGstReportpassparams
    {
        public int ReportMode { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Department { get; set; }       
        public int Criteria { get; set; }
        public string TranSMode { get; set; }      

    }
    public class AuditReportParams
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FK_Branch { get; set; }
        public long FK_Department { get; set; }
        public long FK_MenuGroup { get; set; }
        public long FK_MenuList { get; set; }
        public long FK_UserRole { get; set; }
        public string ReferenceNo { get; set; }
        public long FK_Users { get; set; }
        public long Action { get; set; }
        public long FK_Company { get; set; }
        public long FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public long FK_Machine { get; set; }

        public string TransMode { get; set; }
        public int ReportMode { get; set; } = 1;
    }
    
    public class OtherChargeReportInput
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public long FK_Branch { get; set; }
        public long OtherChargeType { get; set; }
        public long Module { get; set; }
        public int ImportID { get; set; }
        public long ID_Customer { get; set; }
        public long SupplierID { get; set; }
        public int ProdRptCriteria { get; set; }
        public long FK_Company { get; set; }
        public long FK_Machine { get; set; }
        public string TransMode { get; set; }
        //public List<ModuleList> ModuleList { get; set; }
        public int ReportMode { get; set; } = 1;
    }


}