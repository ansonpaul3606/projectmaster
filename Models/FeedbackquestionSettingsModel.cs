using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class FeedbackquestionSettingsModel
    {
        public class FeedbackquestionSettingsModelview
        {
            public long SlNo { get; set; }
            public List<ModuleLists> ModuleList { get; set; }
            public List<ModuleLists> PageList { get; set; }
            public List<Questiontablepopup> QuestiontablepopupList { get; set; }
            public int Page { get; set; }
            public int Module { get; set; }
            public long FK_FeedbackQtSettings { get; set; }
            public List<QuestionDetails> QuestionDetails { get; set; }
            public DateTime EffectFrom { get; set; }
            public DateTime EffectTo { get; set; }
            public string TransMode { get; set; }
            public Int64? LastID { get; set; } = 0;
            public long ID_Feedback { get; set; }
            public int Active { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_FeedBack { get; set; }
            public long ReasonID { get; set; }
            public long ID_FeedbackQuestionSettings { get; set; }
            public long GroupID { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Actives { get; set; }
        }
        public class QuestionDetails
        {

            public long FK_FeedBackQuestion { get; set; }

        }
        public class ModuleLists
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
            public Int32 Group { get; set; }
        }

        public class Questiontablepopup
        {
            public Int32 ID_Question { get; set; }
            public string Question { get; set; }
            public string QuestionMode { get; set; }
            public string ShowIn { get; set; }
            public string FeedBackType { get; set; }
        }
        public class UpdateFeedbackQuestion
        {
           
            public long ID_FeedbackQuestionSettings { get; set; }
            public long FK_FeedbackQuestionSettings { get; set; }
            public DateTime EffectFrom { get; set; }
            public DateTime EffectTo { get; set; }
            public long Module { get; set; }
            public long Page { get; set; }
            public int Active { get; set; }
          
            public string QuestionDetails { get; set; }
            public string TransMode { get; set; }
            public long FK_FeedBack { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64? LastID { get; set; } = 0;
          
        }
        public APIGetRecordsDynamic<ModuleLists> GetPagemodeList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModuleLists, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public static string _updateProcedureName = "ProFeedbackQuestionSettingsUpdate";
        public Output UpdatefeedbackQuestionsettingsData(UpdateFeedbackQuestion input, string companyKey)
        {
            return Common.UpdateTableData<UpdateFeedbackQuestion>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public class FeedBackupQuestionSettingViewID
        {
            public long FK_FeedbackQuestionSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }


        }

        public class FeedbackquestionSettingsModelviewbyID
        {
            public long FK_FeedBack { get; set; }
            public  long GroupID { get; set; }
            public string Question { get; set; }
            public string Mode { get; set; }
            public string QuestionMode { get; set; }
            public string FeedbackType { get; set; }
        }
        public APIGetRecordsDynamic<FeedbackquestionSettingsModelview> GetFeedbackquestionSettingsListviewData(FeedBackupQuestionSettingViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<FeedbackquestionSettingsModelview, FeedBackupQuestionSettingViewID>(companyKey: companyKey, procedureName: "ProFeedbackQuestionSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<FeedbackquestionSettingsModelviewbyID> GetFeedbackquestionSettingsListviewDatabyID(FeedBackupQuestionSettingViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<FeedbackquestionSettingsModelviewbyID, FeedBackupQuestionSettingViewID>(companyKey: companyKey, procedureName: "ProFeedbackQuestionSettingsSelect", parameter: input);
        }
        public class DeleteFeedBackQuestionSetting
        {
            public long FK_FeedbackQuestionSettings { get; set; }
            public string TransMode { get; set; }

            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }
        public Output DeleteFeedBackQuestionSettingData(DeleteFeedBackQuestionSetting input, string companyKey)
        {
            return Common.UpdateTableData<DeleteFeedBackQuestionSetting>(parameter: input, companyKey: companyKey, procedureName: "ProFeedbackQuestionSettingsDelete");
        }
    }
}