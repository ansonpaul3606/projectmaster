using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IEMICollection
    {
        string ReqMode { get; set; }
        string Token { get; set; }
        string BankKey { get; set; }
        string SubMode { get; set; }
        string FK_Company { get; set; }
        string FK_BranchCodeUser { get; set; }
        string EntrBy { get; set; }
        string FK_Employee { get; set; }
        string FromDate { get; set; }
        string ToDate { get; set; }
        string FK_FinancePlanType { get; set; }
        string FK_Product { get; set; }
        string FK_Branch { get; set; }
        string FK_Customer { get; set; }
        string CedEMINo { get; set; }
        string FK_Area { get; set; }
        string FK_Category { get; set; }
        string Demand { get; set; }
        string TrnsDate { get; set; }
        string ID_CustomerWiseEMI { get; set; }
        string AccountMode { get; set; }
        string CollectDate { get; set; }
        string TotalAmount { get; set; }
        string FineAmount { get; set; }
        string NetAmount { get; set; }
        string PaymentDetail { get; set; }
        string EMIDetails { get; set; }
        string LocLatitude { get; set; }
        string LocLongitude { get; set; }
        string Address { get; set; }
        string LocationEnteredDate { get; set; }
        string LocationEnteredTime { get; set; }
        string ID_User { get; set; }
        string ID_TokenUser { get; set; }


    }
    #region Response Interface Model Objects
    public class EMICollectionReportCount : CommonAPIR
    {
        public Int32 ToDoList { get; set; }
        public Int32 OverDue { get; set; }
        public Int32 Upcoming { get; set; }
    }
   
    public class EMICollectionReport : CommonAPIR
    {
        public List<EMICollectionReportList> EMICollectionReportList { get; set; }
    }
    public class EMICollectionReportList
    {
        public string ID_CustomerWiseEMI { get; set; }
        public string EMINo { get; set; }
        public string Customer { get; set; }
        public string Mobile { get; set; }
        public string Product { get; set; }
        public string FinancePlan { get; set; }
        public string LastTransaction { get; set; }
        public string DueAmount { get; set; }
        public string FineAmount { get; set; }
        public string Balance { get; set; }
        public string DueDate { get; set; }
        public string NextEMIDate { get; set; }
        public string InstNo { get; set; }
        public string Area { get; set; }
        public string FK_Area { get; set; }
      
    }
    public class EMIAccountDetails : CommonAPIR
    {
        public List<CustomerInformationList> CustomerInformationList { get; set; }
        public List<EMIAccountDetailsList> EMIAccountDetailsList { get; set; }


    }

    public class CustomerInformationList
    {
        public string ID_Customer { get; set; }
        public string ID_CustomerWiseEMI { get; set; }
        public string CusName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }


    public class EMIAccountDetailsList
    {
        public string FK_CustomerWiseEMI { get; set; }
        public string BillDate { get; set; }
        public string EMINo { get; set; }
        public string Product { get; set; }
        public string Amount { get; set; }
        public string Fine { get; set; }
        public string Balance { get; set; }
        public string FK_FinancePlanType { get; set; }
    }
    public class EMICollectionDetails
    {
        public string BankKey { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string AccountMode { get; set; }
        public string ID_CustomerWiseEMI { get; set; }
        public string TrnsDate { get; set; }

        public string CollectDate { get; set; }

        public string TotalAmount { get; set; }

        public string FineAmount { get; set; }

        public string NetAmount { get; set; }
        public string FK_Employee { get; set; }

        public string LocLatitude { get; set; }
        public string LocLongitude { get; set; }
        public string Address { get; set; }
        public string LocationEnteredDate { get; set; }
        public string LocationEnteredTime { get; set; }
       
        public List<EMIDetails> EMIDetails { get; set; }
        public List<PaymentDetails> PaymentDetail { get; set; }
      
    }
    public class EMIDetails
    {
        public Int64 FK_CustomerWiseEMI { get; set; }
        public decimal CusTrDetPayAmount { get; set; }
        public decimal CusTrDetFineAmount { get; set; }
        public decimal Total { get; set; }
        public decimal Balance { get; set; }
        public bool FK_Closed { get; set; }
    
    }
    public class UpdateEMICollection : CommonAPIR
    {
        public string Message { get; set; }
    }
    public class FinancePlanTypeDetails:CommonAPIR
    {
      public List<FinancePlanTypeDetailsList> FinancePlanTypeDetailsList { get; set; }
    }
    public class FinancePlanTypeDetailsList
    {
        public string ID_FinancePlanType { get; set; }
        public string FinanceName { get; set; }
    }
    #endregion

}
