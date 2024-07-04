/*----------------------------------------------------------------------  Created By : NIJI TJ  Created On : 10/05/2023  Purpose  : CustFeedback  -------------------------------------------------------------------------  Modification  On   By     OMID/Remarks  -------------------------------------------------------------------------  -------------------------------------------------------------------------*/  using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class CustFeedbackModel
    {
        public class CustFeedbackReportView
        {
            public string TicketNo { get; set; }
            public string Question { get; set; }
            public string Options { get; set; }
            public Int64 Mark { get; set; }
            public List<ChangeMode> StatusModeList { get; set; }
            public List<Branch> BranchList { get; set; }

            public List<Complaint> ComplaintList { get; set; }


        }
        public class TicketInputRpt
        {
            public long FK_Branch { get; set; }
            public string Product { get; set; }
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            // public long FK_ComplaintType { get; set; }
            public Int16 Status { get; set; }
            public string CurrentStatus { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string TicketNumber { get; set; }
            public string Customer { get; set; }
            // public string Mobile { get; set; }
            public string Content { get; set; }
            public long FK_Company { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Machine { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }

        public class ChangeMode
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class ChangeModeInput
        {
            public int Mode { get; set; }

        }
        public class Branch
        {
            public long ID_Branch { get; set; }

            public string BranchName { get; set; }
        }

        public class Complaint
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set; }
        }
        public class CustFeedbackView
        {
            public long ID_CustFeedback { get; set; }
            public long FK_Feedback { get; set; }
            public long FK_FeedbackDetails { get; set; }
            public long FK_CustomerServiceRegister { get; set; }
            public long ID_CustomerServiceRegisterProductDetails { get; set; }

            public List<CustFeedbackDetails> CustFeedbackDetails { get; set; }
            public Int32 AnswerType { get; set; }
            public string Remarks { get; set; }
            public Int64 FeedbackMark { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public class FeedbackView
        {
            public long FK_Feedback { get; set; }
            public Int32 Mode { get; set; }//cust portal,web,mobile
            public Int32 FeedbackType { get; set; }//emoji-1, star rating-2,choice-3,answer-4 types
            public string Question { get; set; }
           
            public long FK_FeedbackDetails { get; set; }
            public string Options { get; set; }
            public Int64 Mark { get; set; }

        }
        public class FeedbackDetails
        {
            public long ID_FeedbackDetails { get; set; }
            public string Options { get; set; }
            public Int64 Mark { get; set; }

        }
        public class UpdateCustFeedback
        {

            public Int32 UserAction { get; set; }
            public long ID_CustFeedback { get; set; }
            public long FK_CustFeedback { get; set; }
            public long FK_CustomerServiceRegister { get; set; }
            public long ID_CustomerServiceRegisterProductDetails { get; set; }
            public string Remarks { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string CustFeedbackDetails { get; set; }
        }
        public class GetCustFeedback
        {
            public Int64 ID_CustFeedback { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }
        public class GetFeedback
        {
            public Int64 FK_Feedback { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int32 Mode { get; set; }
            public DateTime FromDate { get; set; }
        }
        public static string _deleteProcedureName = "ProCustFeedbackDelete";
        public static string _updateProcedureName = "ProCustFeedbackUpdate";
        public static string _selectProcedureName = "ProFeedbackSelect";
        public static string _selectCustFeedbackProcedureName = "ProCustFeedbackSelect";
        public class DeleteCustFeedbackView
        {
            public long ID_CustFeedback { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public class CustFeedbackDetails
        {
            public long FK_Feedback { get; set; }
            public long ID_FeedbackDetails { get; set; }
            public bool SkipFeedback { get; set; }

        }

        public class DeleteCustFeedback
        {
            public string TransMode { get; set; }
            public Int64 ID_CustFeedback { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }

        public Output UpdateCustFeedbackData(UpdateCustFeedback input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustFeedback>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCustFeedbackData(DeleteCustFeedback input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustFeedback>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<FeedbackView> GetFeedbackData(GetFeedback input, string companyKey)
        {
            return Common.GetDataViaProcedure<FeedbackView, GetFeedback>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<CustFeedbackView> GetCustFeedbackData(GetCustFeedback input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustFeedbackView, GetCustFeedback>(companyKey: companyKey, procedureName: _selectCustFeedbackProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ServiceListModel.ServiceListView> GetServicefollowupData(ServiceListModel.ServicefollowupselectID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceListModel.ServiceListView, ServiceListModel.ServicefollowupselectID>(companyKey: companyKey, procedureName: "ProCustFeedbackServiceFollowUpSelect", parameter: input);
        }
        public static string _SelectTickets = "ProCustFeedbackEmployeeWiseTicketSelect";
        public APIGetRecordsDynamic<ServiceListModel.TicketView> GetTicketDetails(ServiceListModel.TicketInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceListModel.TicketView, ServiceListModel.TicketInput>(companyKey: companyKey, procedureName: _SelectTickets, parameter: input);
        }
        public APIGetRecordsDynamic<FeedbackView> GetTicketFeedbackDetailsRpt(ServiceListModel.TicketInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<FeedbackView, ServiceListModel.TicketInput>(companyKey: companyKey, procedureName: "ProRptCustFeedback", parameter: input);
        }
    }
}