/*----------------------------------------------------------------------
Created By	: Aiswarya
Created On	: 21/02/2022
Purpose		: SubscriptionModel
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class SubsciptionModel
    {

        public class SubscriptionView
        {

            public long SlNo { get; set; }

            //[Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            //public long? EmployeeID { get; set; }
           
            [Required(ErrorMessage = "Please Enter FirstName")]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "Please Enter Last Name")]
            public string lastName { get; set; }
            [Required(ErrorMessage = "Please Enter Email")]
            public string email { get; set; }
            [Required(ErrorMessage = "Please Enter phoneNumber")]
            public string phoneNumber { get; set; }
            public string companyname { get; set; }
            [Required(ErrorMessage = "Please Enter UserName")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Please Enter Password")]
            public string enterPW { get; set; }
            [Required(ErrorMessage = "Please Enter Confirmation Password")]
           // [Compare("enterPW", ErrorMessage = "The  password and confirmation password do not match.")]
            public string ReenterPW { get; set; }
            public string Language { get; set; }
            public string Currency { get; set; }
            [Required(ErrorMessage = "Please Accept Terms & Condition")]
            public Boolean TermsCondition { get; set; }
            public Int64 Mode { get; set; }
            public Int64 FK_Users { get; set; }
            public Int64 FK_Company { get; set; }
            public string Subject { get; set; }
            public string TockenID { get; set; }
            public string ErrMsg { get; set; } = "";
            public string ErrCode { get; set; } = "";

        }

        public class UpdateSubscriptionData
        {
            public string FirstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string MobileNo { get; set; }
            public string CompanyName { get; set; }
            public string UserCode { get; set; }
            public string PassWord { get; set; }
            // public string  ReenterPW { get; set; }
            //  public string Language { get; set; }
            //  public string Currency { get; set; }
            //  public Boolean TermsCondition { get; set; }
           public string TockenID { get; set; }
            public Int64 Mode { get; set; }
            public string FK_User { get; set; }
            public string FK_Company { get; set; } 
            //  public string SessionID { get; set; }
            // public string ipAddress { get; set; }
        }

        public class UpdateSubscriptionResult
        {
            public string ErrCode { get; set; }
            public long FK_Users { get; set; }
            public string TockenID { get; set; }

        }
        public class Outputclass
        {
            public bool IsProcess { get; set; }
            public List<string> Message { get; set; }
            public string Status { get; set; }
            public int? code { get; set; }
        }
       

        
        public Output CheckExistance(UpdateSubscriptionData input, string companyKey)
        {
            return Common.UpdateTableData<UpdateSubscriptionData>(parameter: input, companyKey: companyKey, procedureName: "proSignUpandSubscriptionUpdate");
        }
        
        public APIGetRecordsDynamic<SubscriptionView> UpdateSubData(UpdateSubscriptionData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubscriptionView, UpdateSubscriptionData>(companyKey: companyKey, procedureName: "proSignUpandSubscriptionUpdate", parameter: input);
        }
        public APIGetRecordsDynamic<UpdateSubscriptionResult> UpdateEmailSubData(UpdateSubscriptionData input, string companyKey)
        {
            return Common.GetDataViaProcedure<UpdateSubscriptionResult, UpdateSubscriptionData>(companyKey: companyKey, procedureName: "proSignUpandSubscriptionUpdate", parameter: input);

        }


    }
}


