using PerfectWebERP.Filters;
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Models
{

    public class CommonIntimationSendModels
    {
        public class IntimationSearchList
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Item { get; set; }
            public string Area { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public long TotalCount { get; set; }

        }
        public class IntimationSearchSelect
        {
            public string TransMode { get; set; }
            public long FK_LeadFrom { get; set; }
            public long FK_LeadBy { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Product { get; set; }
            public string ProjectName { get; set; }
            public long Status { get; set; }
            public long FK_Employee { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Category { get; set; }
            public long Collectedby_ID { get; set; }
            public long Area_ID { get; set; }
            public long FK_NetAction { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
            public string CusName { get; set; }
            public Int16 SearchBy { get; set; }
            public string SearchBydetails { get; set; }
            public long FK_Branch { get; set; }
            public DateTime? ExpectedFrom { get; set; }
            public DateTime? ExpectedTo { get; set; }
            public int ActionType { get; set; }
        }


        public class CommonIntimationSendModelsView
        {
            public List<Module> ModulesList { get; set; }
            public List<Branch> BranchList { get; set; }
            //
            public List<LeadGenerateModel.LeadFrom> LeadFromList { get; set; }
            public List<LeadGenerateModel.NextAction> NextActionList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<LeadGenerateModel.EmployeeInfo> EmployeeInfoList { get; set; }
            public long FK_Employee { get; set; }
            public string UserCode { get; set; }
            public long Branch { get; set; }
            public List<LeadManagementModel.Category> CategoryList { get; set; }
            public List<LeadManagementModel.ActionTypes> Actntyplists { get; set; }
            public List<LeadManagementModel.NextAction> NxtActionList { get; set; }
            public string BrName { get; set; }
            public string CompName { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class ActionTypes
        {
            public int ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }
        }

        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
        }
        public class Branch
        {
            public string Name { get; set; }
            public Int64 ID_Branch { get; set; }
        }
        public class StageList
        {
            public string Mode { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }


        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Module
        {
            public String ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class SaveIntimationIP
        {
            public Int64 FK_Company { get; set; }

            public int SheduledType { get; set; }
            public string Message { get; set; }
            public bool UniqueCode { get; set; }

            public string Subject { get; set; }

            public int Channel { get; set; }
            public string DLId { get; set; }

            public Int64 Branch { get; set; }
            public FileUrlObj FileUrlObj { get; set; }
            public int Module { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan SheduledTime { get; set; }
            public DateTime SheduledDate { get; set; }


            public long ID_LeadFrom { get; set; }
            public long FK_LeadThrough { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long ProdType { get; set; }
            public long ID_Product { get; set; }
            public long FK_Employee { get; set; }
            public long Collectedby_ID { get; set; }
            public long Area_ID { get; set; }
            public long FK_NetAction { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
            public int SearchBy { get; set; }
            public string SearchBydetails { get; set; }

            public List<LeadCusDetails> LeadDetails { get; set; }
            public int GridData { get; set; }

        }
        public class SaveIntimationIP2
        {
            public int UserAction { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 ID_CommonIntimation { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int SheduledType { get; set; }
            public string Message { get; set; }
            public bool Unicode { get; set; }

            public string Subject { get; set; }
            public int Status { get; set; }

            public int Channel { get; set; }
            public string DLId { get; set; }
            public string Attachment { get; set; }
            public Int64 Branch { get; set; }
            public Int64 FK_CommonIntimation { get; set; }
            public string EntrBy { get; set; }
            public int Module { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan SheduledTime { get; set; }
            public DateTime SheduledDate { get; set; }


            public long ID_LeadFrom { get; set; }
            public long FK_LeadThrough { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long ProdType { get; set; }
            public long ID_Product { get; set; }
            public long FK_Employee { get; set; }
            public long Collectedby_ID { get; set; }
            public long Area_ID { get; set; }
            public long FK_NetAction { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
            public int SearchBy { get; set; }
            public string SearchBydetails { get; set; }
            public string LeadDetails { get; set; }
            public int GridData { get; set; }
        }

        public class SaveIntimationLead2
        {
            public string[] checklist { get; set; }
            public int UserAction { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 ID_CommonIntimation { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int SheduledType { get; set; }
            public string Message { get; set; }
            public bool Unicode { get; set; }

            public string Subject { get; set; }
            public int Status { get; set; }

            public int Channel { get; set; }
            public string DLId { get; set; }
            public string Attachment { get; set; }
            public Int64 Branch { get; set; }
            public Int64 FK_CommonIntimation { get; set; }
            public string EntrBy { get; set; }
            public int Module { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan SheduledTime { get; set; }
            public DateTime SheduledDate { get; set; }
        }
        public class SaveIntimationLead
        {
            public Int64 FK_Company { get; set; }
            public string[] Checklist { get; set; }

            public int SheduledType { get; set; }
            public string Message { get; set; }
            public bool UniqueCode { get; set; }

            public string Subject { get; set; }

            public int Channel { get; set; }
            public string DLId { get; set; }

            public Int64 Branch { get; set; }
            public FileUrlObj FileUrlObj { get; set; }
            public int Module { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan SheduledTime { get; set; }
            public DateTime SheduledDate { get; set; }
        }


        public class FileUrlObj
        {
            public string extension { get; set; }
            public string FileUrl { get; set; }
        }
        public class DeleteIntimationIP
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Branch { get; set; }
            public int FK_Reason { get; set; }
            public Int64 FK_commonIntimation { get; set; }

        }
        public class LoadCommonIntimationListIp
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_commonIntimation { get; set; }
            public string TransMode { get; set; }
            public string Name { get; set; }

        }
        public class UpdateProjectTransactionIp
        {
            public byte UserAction { get; set; }
            public string EntrBy { get; set; }

            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public DateTime Date { get; set; }
            public Int64 FK_Project { get; set; }
            public Int64 FK_Stage { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public decimal NetAmount { get; set; }
            public decimal OtherCharge { get; set; }
            public string Remark { get; set; }
            // public List<PaymentDetails> PaymentDetail { get; set; }

            public Int64 FK_ProjectTransaction { get; set; }
            // public List<OtherCharges> OtherChgDetails { get; set; }
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
            public long FK_Stage { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string Remark { get; set; }
            public decimal OtherCharge { get; set; }
            public string PaymentDetail { get; set; }
            public decimal NetAmount { get; set; }
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




        public class SelectCommonIntimationOpt
        {


            public Int64 FK_Company { get; set; }
            public int SheduledType { get; set; }
            public string Message { get; set; }
            public bool UniqueCode { get; set; }

            public string Subject { get; set; }

            public int Channel { get; set; }
            public string DLId { get; set; }

            public Int64 Branch { get; set; }
            public FileUrlObj FileUrlObj { get; set; }
            public int Module { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Time { get; set; }
            public string PageSize { get; set; }
            public long TotalCount { get; set; }

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

        public class getStagesIP
        {
            public Int64 FK_Project { get; set; }
        }

        public class LeadCusDetails
        {
            public long FK_Lead { get; set; }
            public string LeadNo { get; set; }
            public long FK_Customer { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public DateTime LeadDate { get; set; }
        }

        public class SendLeadRequestInput
        {
            public long ID_LeadFrom { get; set; }
            public long FK_LeadThrough { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long ProdType { get; set; }
            public long ID_Product { get; set; }
            public long FK_Employee { get; set; }
            public long Collectedby_ID { get; set; }
            public long Area_ID { get; set; }
            public long FK_NetAction { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
            public int SearchBy { get; set; }
            public string SearchBydetails { get; set; }
            public string TransMode { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
            public string ProdName { get; set; }

        }
        public  class LeadCustomerView
        {

            public long SlNo { get; set; }
            public long ID_FIELD { get; set; }
            public string ID_Name { get; set; }
            public string LeadNo { get; set; }
            public string LeadDate { get; set; }
            public string CustomerName { get; set; }
            public long FK_Customer { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string MobileNo { get; set; }
            public string Customer { get; set; }
            public long TotalCount { get; set; }

                               
        }

        public APIGetRecordsDynamic<IntimationSearchList> LoadCommonIntimationSearchList(IntimationSearchSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<IntimationSearchList, IntimationSearchSelect>(companyKey: companyKey, procedureName: "ProCommonIntimationSearchSelect", parameter: input);
        }

        public APIGetRecordsDynamic<SelectCommonIntimationOpt> LoadCommonIntimationList(LoadCommonIntimationListIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<SelectCommonIntimationOpt, LoadCommonIntimationListIp>(companyKey: companyKey, procedureName: "ProcommonIntimationSelect", parameter: input);
        }

        public Output DeleteIntimation(DeleteIntimationIP input, string companyKey)
        {
            return Common.UpdateTableData<DeleteIntimationIP>(parameter: input, companyKey: companyKey, procedureName: "ProcommonIntimationDelete");
        }
        public Output SaveIntimation(SaveIntimationIP2 input, string companyKey)
        {
            return Common.UpdateTableData<SaveIntimationIP2>(parameter: input, companyKey: companyKey, procedureName: "ProCommonIntimationUpdate");
        }
        public APIGetRecordsDynamic<Module> SaveIntimation_Lead(SaveIntimationLead2 input, string companyKey)
        {
            return Common.GetDataViaProcedure<Module, SaveIntimationLead2>(parameter: input, companyKey: companyKey, procedureName: "ProCommonIntimationUpdate");
        }
        public APIGetRecordsDynamic<Module> GetModules(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Module, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<Module> GetWhatsAppDraftData(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Module, ModeLead>(companyKey: companyKey, procedureName: "ProGetWhatsAppDraftData", parameter: input);
        }
        public APIGetRecordsDynamic<LeadCustomerView> GetSearchRequestdata(SendLeadRequestInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadCustomerView, SendLeadRequestInput>(companyKey: companyKey, procedureName: "ProLeadCustomerFilter", parameter: input);
        }

    }
}