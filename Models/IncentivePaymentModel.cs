using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class IncentivePaymentModel
    {
        public class IncentivePaymentViewlist
        {
            public List<Category> CategoryList { get; set; }
            public List<Branch> BranchList { get; set; }            
            public List<Department> DepartmentList { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }
        public class IncentivePaymentView
        {
            public long SlNo { get; set; }
            public long IPGroupID { get; set; }
            public DateTime IPDate { get; set; }
            public long FK_Branch { get; set; }
            public string Branch { get; set; }
            public long FK_Department { get; set; }
            public string Department { get; set; }
            public long FK_Employee { get; set; }
            public string Employee { get; set; }
            public string TransMode { get; set; }
            public string IPAmount { get; set; }
            public long TotalCount { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class Branch
        {
            public long BranchID { get; set; }
            public string BranchName { get; set; }
        }
        public class Department
        {
            public long ID_Department { get; set; }
            public string DeptName { get; set; }
        }
        public class IncentivePaymentview
        {
            public long FK_Employee { get; set; }
            public DateTime AsOnDate { get; set; }
        }
        public class GetIncentivePaymentview
        {
            public long FK_Employee { get; set; }
            public DateTime AsOnDate { get; set; }
            public long FK_Company { get; set; }

        }
        public class GetIncentivePaymentDetails
        {
            public long SlNo { get; set; }
            public string IncentiveType { get; set; }
            public long FK_IncentiveType { get; set; }
            public string LastProcessedDate { get; set; }
            public string BalancePayable { get; set; }
            public string PayableAmount { get; set; }
            public string CurrentBalance { get; set; }

        }
        public class IncentivePaymentviewDetails
        {
            public long ID_IncentivePayment { get; set; }
            public DateTime AsonDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public string TransMode { get; set; }             
            public decimal IPAmount { get; set; }            
            public List<IncentivePaymentDetails> IncentivePaymentDetails { get; set; }           
            public List<PaymentDetails> PaymentDetail { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class IncentivePaymentDetails
        {
            public long FK_IncentiveType { get; set; }
            public string LastProcessedDate { get; set; }
            public decimal BalancePayable { get; set; }
            public decimal PayableAmount { get; set; }
            public decimal CurrentBalance { get; set; }            
        }
        public class UpdateIncentivePayment
        {
            public long UserAction { get; set; }
            public long ID_IncentivePayment { get; set; }
            public DateTime AsonDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; } = 0;
            public long FK_Employee { get; set; }
            public string TransMode { get; set; }
            public decimal IPAmount { get; set; }
            public string IncentivePaymentDetails { get; set; }
            public string PaymentDetail { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }         
        }
        public class GetIncentivepayment
        {
            public Int64 IPGroupID { get; set; }            
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int Mode { get; set; }
        }
        public class IncentivePaymentViewDetails
        {
            public long SlNo { get; set; }
            public long ID_IncentivePayment { get; set; }
            public long IPGroupID { get; set; }
            public DateTime IPDate { get; set; }
            public long FK_Branch { get; set; }
            public string Department { get; set; }
            public long FK_Department { get; set; }
            public string Employee { get; set; }
            public long FK_Employee { get; set; }
            public string TransMode { get; set; }
            public string IPAmount { get; set; }
        }
        public class InputIncentivePaymentDetails
        {
            public Int64 IPGroupID { get; set; }
            public Int64 FK_IncentivePayment { get; set; }
            public string TransMode { get; set; }
            public int ReasonID { get; set; }
        }
        public class GetPaymentin
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64? FK_Master { get; set; }

        }
        public class IncentivePaymentDetaildata
        {
            public long SlNo { get; set; }
            public string IncentiveType { get; set; }
            public long FK_IncentiveType { get; set; }
            public string LastProcessedDate { get; set; }
            public decimal BalancePayable { get; set; }
            public decimal PayableAmount { get; set; }
            public decimal CurrentBalance { get; set; }
        }
        public class DeleteIncentivePayment
        {
            public string TransMode { get; set; }
            public long IPGroupID { get; set; }
            public string EntrBy { get; set; }
            public int FK_Reason { get; set; }
            public int FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }            
        }
        public APIGetRecordsDynamic<GetIncentivePaymentDetails> GetIncentivePaymentdata(GetIncentivePaymentview input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetIncentivePaymentDetails, GetIncentivePaymentview>(companyKey: companyKey, procedureName: "ProEmployeewiseIncentiveAmount", parameter: input);
        }
        public Output UpdateIncentivePaymentData(UpdateIncentivePayment input, string companyKey)
        {
            return Common.UpdateTableData<UpdateIncentivePayment>(parameter: input, companyKey: companyKey, procedureName: "ProIncentivePaymentUpdate");
        }
        public APIGetRecordsDynamic<IncentivePaymentView> GetIncentivePaymentData(GetIncentivepayment input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentivePaymentView, GetIncentivepayment>(companyKey: companyKey, procedureName: "ProIncentivePaymentSelect", parameter: input);
        }
        public APIGetRecordsDynamic<IncentivePaymentViewDetails> GetIncentivePaymentDataInfo(GetIncentivepayment input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentivePaymentViewDetails, GetIncentivepayment>(companyKey: companyKey, procedureName: "ProIncentivePaymentSelect", parameter: input);
        }
        public APIGetRecordsDynamic<IncentivePaymentDetaildata> GetPaymentDetailDataFill(GetIncentivepayment input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentivePaymentDetaildata, GetIncentivepayment>(companyKey: companyKey, procedureName: "ProIncentivePaymentSelect", parameter: input);
        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }

        public Output DeleteIncentivePaymentData(DeleteIncentivePayment input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIncentivePayment>(parameter: input, companyKey: companyKey, procedureName: "ProIncentivePaymentDelete");
        }
    }
}