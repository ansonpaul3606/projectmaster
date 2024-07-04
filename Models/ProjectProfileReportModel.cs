using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProjectProfileReportModel
    {
        public class ProfileList
        {
            public string Companyname { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Reports> ReportsList { get; set; }
            public int ReportMode { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
              
           public long FK_Project { get; set; }
            
            public int BranchID { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int TableCount { get; set; }
            public string Branch { get; set; }
          
            public List<AccountGroup> AccountGroup { get; set; }
            public List<AccountSubGroup> AccountSubGroup { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class Reports
        {
            public Int32 ID_Report { get; set; }
            public string ReportsName { get; set; }
        }

        public class ProjectDetailsView
        {
            public string SlNo { get; set; }
            public string ProjectName { get; set; }

            public string CreatedDate { get; set; }
            public string ProjectNo { get; set; }
            public string ShortName { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string ProjectAmount { get; set; }
            public string FinalAmount { get; set; }
            public string StartDate { get; set; }
            public string Duration { get; set; }
            public string DueDate { get; set; }
            public string Branch { get; set; }
            public string LeadNo { get; set; }

            public string CustomerName { get; set; }
            public string ContactNo { get; set; }
            public string Address { get; set; }
            public String Department { get; set; }

            public String Employee { get; set; }
            public String TeamName { get; set; }

            public String EmployeeRole { get; set; }

            public String EndDate { get; set; }
            public String WorkPercentage { get; set; }

            public String CurrentStatus { get; set; }
            public String ItemName { get; set; }
            public String Quantity { get; set; }
            public String Unit { get; set; }
            public String Rate { get; set; }
            public String Amount { get; set; }
            public String WorkType { get; set; }
            public String Stage { get; set; }
            public String Subcontractor { get; set; }

            public String Remarks { get; set; }
            public String Date { get; set; }
            public String Expense { get; set; }
            public String Debit { get; set; }
            public String Credit { get; set; }
            public String ResourceType { get; set; }
            public String CompletedPercentage { get; set; }
            public String AmountDue { get; set; }
            public String TotalIncome { get; set; }
            public String TotalExpense { get; set; }
            public String SubContractor { get; set; }
            public String ProfitLossLabel1 { get; set; }
            public String ProfitLossLabel2 { get; set; }
            public String  ProfitLossAPer { get; set; }

            public String ProfitLossAmount { get; set; }

            public String StageName { get; set; }
            public String Tax { get; set; }
            public String BillMode { get; set; }
            public String NetAmount { get; set; }
            public String OtherCharges { get; set; }
        }
        public class ProjectDetailsinput {
          //  public int ReportMode { get; set; }
           // public long FK_Branch { get; set; }
          //  public string FromDate { get; set; }
           // public string ToDate { get; set; }
          //  public long FK_Company { get; set; }
          //  public long FK_BranchCodeUser { get; set; }
           
            public long FK_Project { get; set; }
            public int TableCount { get; set; }
        

        }

        public class Transctiondetailsview
        {
            public string SlNo { get; set; }
            public string Voucher{ get; set; }
           
            public string TransDate { get; set; }
            public string EffectDate { get; set; }
            public string Party{ get; set; }
            public string Remarks { get; set; }
            public string Payment { get; set; }
            public string Receipt { get; set; }
            public string Balance { get; set; }
        }
        public class AccountGroup
        {
            public Int32 ID_AccountGroup { get; set; }
            public string AGName { get; set; }
        }

        public class AccountSubGroup
        {
            public Int32 ID_AccountSubGroup { get; set; }
            public string ASGName { get; set; }

        }
        public class AccountHeadViewData
        {
            public long FK_AccountGroup { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public long FK_AccountSubGroup { get; set; }
        }
        public class AccountSubGroupView
        {
            public int SlNo { get; set; }
            public long ID_AccountSubGroup { get; set; }
            public long FK_AccountGroup { get; set; }
            public string ASGName { get; set; }
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
        }
        public class RAndDReoportInput
        {
            public long FK_AccountHead { get; set; }
            public long FK_AccountSubHead { get; set; }
           // public int BranchID { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long AccountHead { get; set; }
            public long AccountSubHead { get; set; }
            public int ReportMode { get; set; }
            public int AccountGroup { get; set; }
            public int AccountSubGroup { get; set; }


            //
         
            public long FK_Branch { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
   
            //public long FK_BranchCodeUser { get; set; }
      
            //public long FK_Machine { get; set; }
            public int TableCount { get; set; }
        }


        public APIGetRecordsDynamic<ProjectDetailsView> GetProjectDetailsData(ProjectDetailsinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectDetailsView, ProjectDetailsinput>(companyKey: companyKey, procedureName: "ProProjectProfile", parameter: input);

        }
        public APIGetRecordsDynamic<Transctiondetailsview> GetTransactionDetailsData(ProjectDetailsinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Transctiondetailsview, ProjectDetailsinput>(companyKey: companyKey, procedureName: "ProRptAccounts", parameter: input);

        }
        public APIGetRecordsDynamic<AccountSubGroupView> GetSubGroupList(AccountHeadViewData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountSubGroupView, AccountHeadViewData>(companyKey: companyKey, procedureName: "ProAccountSubGroupSelectByGroup", parameter: input);
        }

    }
}