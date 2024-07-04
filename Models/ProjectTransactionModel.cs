using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProjectTransactionModel
    {
        public class ProjectTransactionView
        {
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
            public List<DropdownList> BillTypeListView { get; set; }
            public List<DropdownList> PettyCashierView { get; set; }
            public List<StageList> StageList { get; set; }
            public List<TransactionTypeList> TransactionTypeList { get; set; }
            public long? LastID { get; set; }
        }
        public class StageList
        {
            public string Mode { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }
        public class CommonPrintSettingsView
        {
            public List<ActionStatus> Modules { get; set; }
            public List<PageOrientaion> PageOrientaions { get; set; }
        }
        public class PageOrientaion
        {
            public string Page_Orientaion { get; set; }
            public string PValue { get; set; }

            public string width_in_px { get; set; }
            public string height_in_px { get; set; }

        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class TransactionTypeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class DropdownList
        {
            public Int64 ID_BillType { get; set; }
            public string BTName { get; set; }
            public Int64 ID_PettyCashier { get; set; }
            public string PtyCshrName { get; set; }

        }
        public class GetCommonPrintElementsIP
        {
            public Int64 FK_Company { get; set; }
            public int Module { get; set; }
            public string TransMode { get; set; }
            public string ImagePath { get; set; }

            public string FrntImg { get; set; }
            public int CommonPrintSettingsMode { get; set; }
            public Int64 ID_CommonPrintingSettings { get; set; }
            public Int64 FK_CommonPrintingSettings { get; set; }
            public string FrontSide { get; set; }
            public byte UserAction { get; set; }
            public byte Debug { get; set; }
            public string SettingsName { get; set; }

            public string EntrBy { get; set; }

            public bool Cancelled { get; set; }

            public long FK_Reason { get; set; }
            public string PageSize { get; set; }
        }
        public class DeleteCommonPrintingSettingsIP
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Branch { get; set; }
            public int FK_Reason { get; set; }
            public Int64 FK_ProjectTransaction { get; set; }

        }
        public class SelectProjectTransactionIp
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_ProjectTransaction { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }
            
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
        public class UpdateProjectTransactionIp
        {
            public byte UserAction { get; set; }
            public string EntrBy { get; set; }

            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public DateTime Date { get; set; }
            public Int64 FK_Project { get; set; }
            public Int64 PTPrjTransType { get; set; }
            public Int64 FK_PettyCashier { get; set; }
            public Int64 FK_BillType { get; set; }
            public Int64 FK_Employee { get; set; }
            public Int64 FK_Stage { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public decimal NetAmount { get; set; }
            public decimal  PTRoundOff { get; set; }
            public decimal PTAmount { get; set; }
            public decimal OtherCharge { get; set; }
            public string Remark { get; set; }
            public List<PaymentDetails> PaymentDetail { get; set; }

            public Int64 FK_ProjectTransaction { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set;}
            public int PaymentType { get; set; }
            public long? LastID { get; set; }
        }
        public class UpdateProjectTransactionDTO
        {

         

            public string OtherChgTaxDetails { get; set; }
            public short UserAction { get; set; }  // SMALLINT in SQL corresponds to short in C#
            public byte Debug { get; set; }        // TINYINT in SQL corresponds to byte in C#
            public string TransMode { get; set; }  // VARCHAR(6) in SQL corresponds to string in C#
            public long ID_ProjectTransaction { get; set; }  // BIGINT in SQL corresponds to long in C#
            public DateTime Date { get; set; }
            public long FK_Project { get; set; }
            public long PTPrjTransType { get; set; }
            public long FK_PettyCashier  { get; set; }
        public long FK_BillType { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string Remark { get; set; }
           // public decimal OtherCharge { get; set; }
            public string PaymentDetail { get; set; }    
            public decimal NetAmount { get; set; }
            public decimal  PTRoundOff { get; set; }
            public decimal PTAmount { get; set; }
            public long FK_ProjectTransaction { get; set; }


            public long? LastID { get; set; }

            //public string EntrBy { get; set; }

            //public Int64 FK_Company { get; set; }
            //public string TransMode { get; set; }
            //public DateTime Date { get; set; }
            //public Int64 FK_Project { get; set; }
            //public Int64 FK_Stage { get; set; }
            //public Int64 FK_BranchCodeUser { get; set; }
            //public Int64 FK_Machine { get; set; }
            //public decimal NetAmount { get; set; }
            //public decimal OtherCharge { get; set; }
            //public string Remark { get; set; }
            //public string PaymentDetail { get; set; }

            // public Int64 FK_ProjectTransaction { get; set; }
            public string OtherChgDetails { get; set; }
           // public Int64 ID_ProjectTransaction { get; set; }
        }

        public class OtherCharges
        {
            public long SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public Int64 OctyTransType { get; set; }
            public string OctyName { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransType { get; set; }
            public Int64 TransTypeID { get; set; }
            public long OctyTransTypeActive { get; set; }
        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class GetProjectTransactionList
        {
            public Int64 FK_ProjectTransaction { get; set; }
            public Int64 SlNo { get; set; }
            public Int64 ID_ProjectTransaction { get; set; }
            public long TotalCount { get; set; }
           

            public string TransactionType { get; set; }
            public DateTime Date { get; set; }
            public decimal NetAmount { get; set; }
            public string CusNumber { get; set; }

            public decimal OtherCharge { get; set; }
           
            public Int64 FK_Project { get; set; }
            public Int64 FK_Employee { get; set; }

            public string Employee { get; set; }
            
            public Int64 FK_Stage { get; set; }

            public Int64 PTPrjTransType { get; set; }

            public Int64 FK_BillType { get; set; }

            public Int64 FK_PettyCashier { get; set; }
            public decimal PTAmount { get; set; }
            public decimal PTRoundOff { get; set; }
            public string Remark { get; set; }
            public long? LastID { get; set; }
            public string Project { get; set; }
            public string Stage { get; set; }

            //  public int PaymentType { get; set; }

        }
        public class PaymentInfo
        {
            public decimal Allocated { get; set; }
            public decimal Returned { get; set; }
            public decimal Used { get; set; }
            public decimal Balance { get; set; }
            public decimal PettyCashier { get; set; }
        }
        public class InputPaymentinfo
        {
            public long FK_Project { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Stage { get; set; }
            public long FK_PetyCashier { get; set; }
            public DateTime AsOnDate { get; set; }
            

            public long FK_Company { get; set; }
        }
        public class SelectCommonPrintingSettingsOpt
        {
            public string SettingsName { get; set; }
            public Int64 ID_CommonPrintingSettings { get; set; }
            public long CommonPrintSettingsMode { get; set; }
            public string ImagePath { get; set; }
            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string StrFrntImg { get; set; }
            public string PageSize { get; set; }

        }
        public class CommonPrintSettings
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string PageSize { get; set; }
        }
        public class CommonPrintSettingsTemplateData
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string Logo_image { get; set; }
            public string Tittle { get; set; }
            public string Terms_and_conditions { get; set; }
            public string Subtotal { get; set; }
            public string Address { get; set; }
            public string PaymentInfo { get; set; }
            public string Invoice_No { get; set; }
            public string Table_data { get; set; }
            public string Table_column { get; set; }
            public string Box_data { get; set; }
        }
        public class CommonPrintTemplateData
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string Logo_image { get; set; }
            public string Tittle { get; set; }
            public string Terms_and_conditions { get; set; }
            public string Subtotal { get; set; }
            public string Address { get; set; }
            public string PaymentInfo { get; set; }
            public string Invoice_No { get; set; }
            public string Table_data { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class GetBillType
        {
            public Int64 FK_Project { get; set; }
            public Int32 Mode { get; set; }
            public long FK_Company { get; set; }
            public long  FK_Employee { get; set; }
        }

        public class getStagesIP
        {
            public Int64 FK_Project { get; set; }
        }
        public APIGetRecordsDynamic<TransactionTypeList> GetTransactionType(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<TransactionTypeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<GetProjectTransactionList> LoadProjectTransactionList(SelectProjectTransactionIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetProjectTransactionList, SelectProjectTransactionIp>(companyKey: companyKey, procedureName: "ProProjectTransactionSelect", parameter: input);
        }
       
        public Output DeleteProjectTransaction(DeleteCommonPrintingSettingsIP input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCommonPrintingSettingsIP>(parameter: input, companyKey: companyKey, procedureName: "ProProjectTransactionDelete");
        }
        public Output SaveProjectTransaction(UpdateProjectTransactionDTO input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProjectTransactionDTO>(parameter: input, companyKey: companyKey, procedureName: "ProProjectTransactionUpdate");
        }
        public APIGetRecordsDynamic<PaymentInfo> GetPaymentInformation(InputPaymentinfo input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentInfo, InputPaymentinfo>(companyKey: companyKey, procedureName: "ProProjectTransactionBalance", parameter: input);
           // return Common.GetDataViaProcedure<PaymentInfo, InputPaymentinfo>(parameter: input,companyKey: companyKey, procedureName: "ProProjectTransactionBalance");

        }
        public APIGetRecordsDynamic<DropdownList> GetBillTypeInfo(GetBillType input, string companyKey)
        {
            return Common.GetDataViaProcedure<DropdownList, GetBillType>(companyKey: companyKey, procedureName: "ProGetBillType", parameter: input);

        }
        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);
        }

    }
}