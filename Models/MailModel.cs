using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MailModel
    {
        public class SendMail
        {           
            public string Email_ID { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }           
        }
        public class MailCredential
        {
            public string userName { get; set; }
            public string password { get; set; }
            public string host { get; set; }
            public bool enableSSL { get; set; }
            public string port { get; set; }          
        }
        public class GetMail
        {           
            public string TransMode { get; set; }

            public long MasterID { get; set; }
            public long TransID { get; set; } = 0;
            public string TransType { get; set; } = "";
            public decimal TransAmount { get; set; } = 0;
            public DateTime? TransDate { get; set; } 
            public string Remarks { get; set; }
            public string JsonData { get; set; } = "{}";
            public long FK_Company { get; set; }
            public int Mode { get; set; }
            public int Type { get; set; }            
        }

        public class UpdateMailData
        {
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public string EmailID { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Attachment { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }          
        }
        public class UpdateMailStringData
        {
            public string JsonInput { get; set; }
        }
        public class returnRootmodal
        {
            public bool Process { get; set; }
            public Int64? Count { get; set; }
            public ReturnCountObj CountObj { get; set; }
        }
        public class ReturnCountObj
        {
            public Int64? SMSCount { get; set; }
            public Int64 EmailCount { get; set; }
        }
        public APIGetRecordsDynamic<SendMail> GetMailData(GetMail input, string companyKey)
        {
            return Common.GetDataViaProcedure<SendMail, GetMail>(companyKey: companyKey, procedureName: "proCmnSmsDraftUpdate", parameter: input);
        }
        public Output UpdateMailDetails(UpdateMailData input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMailData>(parameter: input, companyKey: companyKey, procedureName: "ProMailDataUpdate");
        }
        public Output UpdateBulkMailDetails(UpdateMailStringData input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMailStringData>(parameter: input, companyKey: companyKey, procedureName: "ProBulkEmailUpdate");
        }
    }
}