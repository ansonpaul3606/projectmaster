/*----------------------------------------------------------------------  Created By : NIJI TJ  Created On : 10/05/2023  Purpose  : Feedback  -------------------------------------------------------------------------  Modification  On   By     OMID/Remarks  -------------------------------------------------------------------------  -------------------------------------------------------------------------*/  using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class FeedbackModel
    {
        public class FeedbackView
        {
            [Required(ErrorMessage = "Please Enter Effect From")]
            public DateTime EffectFrom { get; set; }
            [Required(ErrorMessage = "Please Enter Effect To")]
            public DateTime EffectTo { get; set; }
            [Required(ErrorMessage = "Please Enter Mode")]
            public byte Mode { get; set; }
            [Required(ErrorMessage = "Please Enter Feedback Type")]
            public byte FeedbackType { get; set; }
            [Required(ErrorMessage = "Please Enter Question")]
            public string Question { get; set; }
        }
        public class UpdateFeedback
        {
            public long ID_Feedback { get; set; }
            public DateTime EffectFrom { get; set; }
            public DateTime EffectTo { get; set; }
            public byte Mode { get; set; }
            public byte FeedbackType { get; set; }
            public string Question { get; set; }
        }
        public static string _deleteProcedureName = "ProFeedbackDelete";
        public static string _updateProcedureName = "ProFeedbackUpdate";
        public static string _selectProcedureName = "ProFeedbackSelect";
        public class DeleteFeedback
        {
            public long ID_Feedback { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class FeedbackID
        {
            public Int64 ID_Feedback { get; set; }
        }
        public Output UpdateFeedbackData(UpdateFeedback input, string companyKey)
        {
            return Common.UpdateTableData<UpdateFeedback>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteFeedbackData(DeleteFeedback input, string companyKey) { return Common.UpdateTableData<DeleteFeedback>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName); }
        public APIGetRecordsDynamic<FeedbackView> GetFeedbackData(FeedbackID input, string companyKey) { return Common.GetDataViaProcedure<FeedbackView, FeedbackID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input); }
    }
}