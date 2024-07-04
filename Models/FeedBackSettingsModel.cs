using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class FeedBackSettingsModel
    {
        public class FeedBackSettingsView
        {
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<ActionStatus> FeedStatus { get; set; }
            public List<ActionStatus> ReviewStatus { get; set; }
            public List<ActionStatus> quesMode { get; set; }
            public List<EmoList> Emojilist { get; set; }
            public string Question { get; set; }
            public int Mode { get; set; }
            public int FeedbackType { get; set; }
            public long ID_Feedback { get; set; }
            public int QueMode { get; set; }

            public List<FeedbackDetails> FeedbackSettingsdetails { get; set; }
        }
        public class FeedbackDetails
        {
            private string _options;
            public string Options {
                get { return _options; } set { _options = value == null ? "" : value; }
            }
            public int Mark { get; set; }
            private string _optionclass;
            public string OptionClass
            {
                get { return _optionclass; }
                set { _optionclass = value == null ? "" : value; }
            }
            public long StarRating { get; set; }

        }


        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class EmoList
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class FeedbackSettingsUpdate
        {
            public long FK_Company { get; set; }
            //public long FK_Branch { get; set; }
            public Int16 UserAction { get; set; }
            public Int16 Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string Question { get; set; }
            public int Mode { get; set; }
            public int FeedbackType { get; set; }
            public long ID_Feedback { get; set; }
            public long FK_Feedback { get; set; }
            public string FeedbackSettingsdetails { get; set; }
            public int QueMode { get; set; }
        }

        public class FeedbackSettingsInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Feedback { get; set; }
            public string TransMode { get; set; }
            public int Detailed { get; set; }

        }
        public class FeedbackSettingsOutput
        {
            public long SlNo { get; set; }
            public string Question { get; set; }
            public string Mode { get; set; }
            public int ModeId { get; set; }
            public int FeedbackType { get; set; }
            public long ID_Feedback { get; set; }
            public long TotalCount { get; set; }
            public int QueMode { get; set; }
            public string QueType { get; set; }

        }
        public class FeedbackSettingsSubOutput
        {
            public long ID_FeedbackDetails { get; set; }
            public long FK_Feedback { get; set; }
            public string Options { get; set; }
            public int Mark { get; set; }
            private string _optionClass;
            // Property with logic in the setter
            public string OptionClass
            {
                get { return _optionClass; }
                set { _optionClass = string.IsNullOrWhiteSpace(value) ? "0" : value; }
            }
            public Int32 Mark1 { get; set; }
            public string StarRating { get; set; }
        }

        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public long FK_Feedback { get; set; }
        }
        public class DeleteFeedbk
        {

            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Feedback { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }


        }

        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<EmoList> GetemojisList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmoList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output Updatefeedbacksettingsdetails(FeedbackSettingsUpdate input, string companyKey)
        {
            return Common.UpdateTableData<FeedbackSettingsUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProFeedbackSettingsUpdate");
        }
        public APIGetRecordsDynamic<FeedbackSettingsOutput> GetFeedbackSettingsdetails(FeedbackSettingsInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<FeedbackSettingsOutput, FeedbackSettingsInput>(companyKey: companyKey, procedureName: "ProFeedbackSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<FeedbackSettingsSubOutput> GetFeedbackSettingsSublist(FeedbackSettingsInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<FeedbackSettingsSubOutput, FeedbackSettingsInput>(companyKey: companyKey, procedureName: "ProFeedbackSettingsSelect", parameter: input);
        }

        public Output DeleteItemCsData(DeleteFeedbk input, string companyKey)
        {
            return Common.UpdateTableData<DeleteFeedbk>(parameter: input, companyKey: companyKey, procedureName: "ProFeedbackSettingsDelete");
        }

    }
}