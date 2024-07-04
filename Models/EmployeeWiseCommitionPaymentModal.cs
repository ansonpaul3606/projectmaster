using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EmployeeWiseCommitionPaymentModal
    {
        public class EmployeeLoadwiseLoad
        {
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }

        }
        public class employeeinput
        {
            public Int64 FK_Employee { get; set; }
            public DateTime UptoDate { get; set; }

           // public DateTime TransDate { get; set; }
        }
        public class SaveEmployeeCommisiondataIP
        {

            public List<PaymentDetails> paymentdetails { get; set; }
            public DateTime UptoDate { get; set; }
            public string EntrBy { get; set; }

            public DateTime TransDate { get; set; }
            public int Mode { get; set; }



            public Int64 FK_Employee { get; set; }

            public Int64 ID_IncentiveTransaction { get; set; }
            public string Transmode { get; set; }
            public int incTrType { get; set; }
            public Int64 FK_Master { get; set; }
            //       public DateTime EntrOn { get; set; }
            public decimal NetAmount { get; set; }
            public string TransType { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class GetEmployeeWiseDataOP
        {
            public decimal TotalPaid { get; set; }
            public decimal Payable { get; set; }
        }
        public class GetPreviousPaymentDetailsOP
        {
            public Int64 Amount { get; set; }
            public DateTime date { get; set; }
            public int SlNo { get; set; }
        }
        public class SaveEmployeeCommisiondataIP2
        {
            public Int64 FK_Employee { get; set; }
            public string paymentdetails { get; set; }
            public DateTime UptoDate { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }

            public DateTime TransDate { get; set; }
            public int Mode { get; set; }
            public Int64 ID_IncentiveTransaction { get; set; }
            public string Transmode { get; set; }
            public int incTrType { get; set; }
            public Int64 FK_Master { get; set; }
          //  public DateTime EntrOn { get; set; }
            public decimal NetAmount { get; set; }
            // public string TransType { get; set; }
            public Int64 FK_ServiceIncentiveSettings { get; set; }




        }
        public class CommisionwiseListDatainput{

            public string TransMode { get; set; }
            public Int64 ID_IncentiveTransaction { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }

            public bool Detailed { get; set; }
            public string Name { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }

        }
        public class GetDataOut
        {

            public int SlNo { get; set; }
            public Int64 ID_IncentiveTransaction { get; set; }
            public DateTime TransDate { get; set; }
            public string  EmpFname { get; set; }
            public DateTime? UptoDate { get; set; }
            //public string Designation { get; set; }
            public Int64 FK_Employee { get; set; }
            public string Employee { get; set; }
            public Int64 TotalCount { get; set; }

            public decimal NetAmount { get; set; }
            //public Int64 FK_AccountHead { get; set; }
            //public bool IsActive { get; set; }
            //public string AccountSubHead { get; set; }
            //public string AccountHead { get; set; }
        }

        public class EmployeeWisesDeleateinputModal
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 ID_IncentiveTransaction { get; set; }
        }
         public class DeleteoutputDto
        {

        }


        public APIGetRecordsDynamic<GetEmployeeWiseDataOP> GetEmployeeWiseData(employeeinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetEmployeeWiseDataOP, employeeinput>(companyKey: companyKey, procedureName: "proIncentiveWiseCollection", parameter: input);

        }
        public APIGetRecordsDynamic<GetDataOut>CommissionListData(CommisionwiseListDatainput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetDataOut, CommisionwiseListDatainput>(companyKey: companyKey, procedureName: "proIncentiveWiseEmployeeCommissionSelect", parameter: input);
        }
       
        public Output InsertData(SaveEmployeeCommisiondataIP2 input, string companyKey)
        {
            return Common.UpdateTableData<SaveEmployeeCommisiondataIP2>(parameter: input, companyKey: companyKey, procedureName: "proIncentiveWiseCollectionUpdate");
        }
      
        public APIGetRecordsDynamic<DeleteoutputDto> CommissionwisetDatadelete(EmployeeWisesDeleateinputModal input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeleteoutputDto, EmployeeWisesDeleateinputModal>(companyKey: companyKey, procedureName: "proIncentiveWiseCollectionDelete", parameter: input);
        }
    }
}