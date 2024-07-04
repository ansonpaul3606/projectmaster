using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class IncentiveModel
    {

        public class IncentiveInitView
        {
            public long IncModule { get; set; }
            public long ReferenceNo { get; set; }
            public decimal IDtPercentage { get; set; }
            public decimal PrAmount { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }

        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class IncentiveView
        {
            //public Int32 IModule { get; set; }
            public long FK_Master { get; set; }
            public string Module { get; set; }
            public string Party { get; set; }
            public string Description { get; set; }
            public decimal IncTrDividentPercent { get; set; }
            public decimal Percentage { get; set; }
            public decimal IncTrTotalAmount { get; set; }
            public decimal Amount { get; set; }
            public string TransMode { get; set; }
            public long IncModule { get; set; }
            public long ReferenceNo { get; set; }
            public decimal IncTrProfitAmount { get; set; }
            public int IncTrType { get; set; }
            public int MasterType { get; set; }
            public long FK_MasterID { get; set; }
            public decimal IncNetAmount { get; set; }
            public long ID_IncentiveTransaction { get; set; }

            public DateTime TransDate { get; set; }

            public List<IncentiveSubViewOutput> Modoutputs { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }
        }
        public class IncentiveSubViewOutput
        {
            public string Module { get; set; }
            public string Party { get; set; }
            public string IncTrDetRemarks { get; set; }
            public string IncTrDetDescription { get; set; }
            public decimal IncTrDetIncPercentage { get; set; }
            public decimal IncTrDetIncAmount { get; set; }
            public int IncTrDetMasterType { get; set; }
            public int IncTrDetPayWithSalalry { get; set; }
            public int IncTrDetPayAsCash { get; set; }
            public long FK_MasterID { get; set; }
        }
        public class Modedata
        {
            public long Mode { get; set; }
            public long FK_Master { get; set; }
        }
        public class IncentiveViewTable
        {
            public long IncModule { get; set; }
            public string Module { get; set; }
            public List<IncentiveSubViewOutputTable> Modoutputs { get; set; }
        }

        public class IncentiveSubViewOutputTable
        {
            public string IncTrDetDescription { get; set; }
            public long IModule { get; set; }

            public string Party { get; set; }
            public string IncTrDetRemarks { get; set; }
            public decimal IncTrDetIncPercentage { get; set; }
            public decimal IncTrDetIncAmount { get; set; }
            public  int MasterType { get; set; }
            public  long FK_MasterID { get; set; }
            public bool IncTrDetPayWithSalalry { get; set; }
            public bool IncTrDetPayAsCash { get; set; }

        }

        public class UpdateIncentive {
            public long ID_IncentiveTransaction { get; set; }
            public DateTime TransDate { get; set; }
            public int UserAction { get; set; }
            public string  TransMode { get; set; }
            public string IncentiveTransactionDetails { get; set; }
            public string PaymentDetail { get; set; }
            public int Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            //public long IncModule { get; set; }
           // public long ReferenceNo { get; set; }
            public decimal IncTrTotalAmount { get; set; }
            public decimal IncTrDividentPercent { get; set; }
            public int IncTrType { get; set; }
            public long FK_Master { get; set; }
            public long FK_IncentiveTransaction { get; set; }
            public decimal IncTrProfitAmount { get; set; }
            public decimal IncNetAmount { get; set; }


        }

        public class GetIncentivedetails
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_IncentiveTransaction { get; set; }
            public byte Detailed { get; set; }
        }

        public class IncentiveViewOut
        {
            public long SlNo { get; set; }
            public long ID_IncentiveTransaction { get; set; }
            public DateTime TransDate { get; set; }
            public int IncTrType { get; set; }
            public string Module { get; set; }
            public long FK_Master { get; set; }
            public decimal IncTrProfitAmount { get; set; }
            public decimal IncTrDividentPercent { get; set; }
            public decimal IncTrTotalAmount { get; set; }
            public long TotalCount { get; set; }
            public string MasterName { get; set; }
            public decimal IncNetAmount { get; set; }


        }
        public class IncentiveInputDel
        {
            public Int64 ID_IncentiveTransaction { get; set; }
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }
        }
        public class DeleteIncentiveData
        {
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_IncentiveTransaction { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class IncentiveGridOut
        {
            public long FK_IncentiveTransaction { get; set; }
            public string IncTrDetDescription { get; set; }
            public int IncTrDetMasterType { get; set; }
            public long FK_MasterID { get; set; }
            public long FK_Master { get; set; }
            public string IncTrDetRemarks { get; set; }
            public decimal IncTrDetIncPercentage { get; set; }
            public decimal IncTrDetIncAmount { get; set; }
            public bool IncTrDetPayWithSalalry { get; set; }
            public bool IncTrDetPayAsCash { get; set; }
            public string Party { get; set; }

        }
        public class GetPaymentin
        {

            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }


        }

        public APIGetRecordsDynamic<IncentiveView> GetfillData(Modedata input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentiveView, Modedata>(companyKey: companyKey, procedureName: "ProIncetiveDataFill", parameter: input);
        }

        public Output UpdateIncentivedetails(UpdateIncentive input, string companyKey)
        {
            return Common.UpdateTableData<UpdateIncentive>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveTransactionUpdate");
        }

        public APIGetRecordsDynamic<IncentiveViewOut>GetIncentiveData(GetIncentivedetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentiveViewOut, GetIncentivedetails>(companyKey: companyKey, procedureName: "ProIncentiveTransactionSelect", parameter: input);
        }

        public Output DelIncentiveData(DeleteIncentiveData input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIncentiveData>(parameter: input, companyKey: companyKey, procedureName: "ProIncentiveTransactionDelete");
        }
        public APIGetRecordsDynamic<IncentiveGridOut>GetIncentiveDataGrid(GetIncentivedetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<IncentiveGridOut, GetIncentivedetails>(companyKey: companyKey, procedureName: "ProIncentiveTransactionSelect", parameter: input);
        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }
    }
}