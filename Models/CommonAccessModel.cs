using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class CommonAccessModel
    {
        public class Moduleinput
        {
            public long ModeId { get; set; }
            public long customerId { get; set; }
            public string MobNo { get; set; }
            public long ID_Module { get; set; }
            public List<InputTable> Inputs { get; set; }
        }
        public class EncodeUrl
        {
            public long FK_Company { get; set; }
            public long ModeId { get; set; }
            public string CompanyCode { get; set; }
            public string Type { get; set; }
            public long ID_Module { get; set; }
        }
        public class updateInput
        {
            public long ID_Module { get; set; }
            public long customerId { get; set; }
            public string MobNo { get; set; }
            public string returnUrl { get; set; }
            public long ModeId { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public class RetreiveID
        {
            public string ModeId { get; set; }

        }
        public class QuestionList
        {
            public string OptionClass { get; set; }
            public string Options { get; set; }
            public int Mark { get; set; }
            public long FK_Feedback { get; set; }
        }
        public class InputTable
        {
            public long ModeId { get; set; }
            public long FK_Company { get; set; }
            public long ID_CommonBaseUrl { get; set; }
            public string Type { get; set; }
            public string BaseUrl { get; set; }
            public long IdType { get; set; }
        }
        public class OutPutTable
        {
            
            public long Module { get; set; }
            public string  Question{ get; set; }
            public long ID_Feedback { get; set; }
            public long FeedbackType { get; set; }
            public string QuestionList { get; set; } = "[]";
            public List<QuestionList> QuestionListDetails //ok
            {
                get
                {
                    if (this.QuestionList is null)
                    {
                        return default(List<QuestionList>);
                    }
                    else
                    {
                        return JsonSerializer.Deserialize<List<QuestionList>>(this.QuestionList);
                    }
                }
            }
            
        }



        public class OutputLoadCustomerFeedback
        {
            public List<OutPutTable> FeedbackDetails { get; set; }
        }

        public Output SendDataFeedback(updateInput input, string companyKey)
        {
            return Common.UpdateTableData<updateInput>(parameter: input, companyKey: companyKey, procedureName: "ProCommonFeedbackUpdate");
        }
        public APIGetRecordsDynamic<OutPutTable> GetFeedbackDetails(InputTable input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutPutTable, InputTable>(companyKey: companyKey, procedureName: "ProCommonFeedbackSelect", parameter: input);

        }

    }

}