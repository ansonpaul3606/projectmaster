using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PerfectWebERP.Models
{
    public class UserchangePasswordModel
    {
        public class UserChangePassword
        {
            public string UserCode { get; set; }
            [Required(ErrorMessage = "Please Enter Password")]
            public string PresentPassword { get; set; }
            [Required(ErrorMessage = "Please Enter New Password")]
            public string NewPassword { get; set; }
            [Required(ErrorMessage = "Please Enter Confirm Password")]
            public string ConfirmPassword { get; set; }
        }
        public class Userchangepwdlist
        {
          public string UserCode { get; set; }
        }
        public class UserpswdcheckView
        {
            public string UserCode { get; set; }
            public long FK_Company { get; set; }

        }
        public class UpdateUserChangePassword
        {

             public long ID_Users { get; set; }
            public string UserCode { get; set; }
            public string UserPassword { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            //public byte UserAction { get; set; }
            //public long FK_Users { get; set; }


        }

        public class passwordcheck
        {
            public string UserCode { get; set; }
            public string UserPassword { get; set; }
        }
        public class Userpolicysettinglistcheck
        {
            public long UsersettingsMode { get; set; }
            public long FK_Company { get; set; }

        }
        public class Userpasswordcombinationchecklist
        {
            public long Passcombination { get; set; }
            public long Passminimumlength { get; set; }
            public long Passmaximumlength { get; set; }
            public long Passchangeperiod { get; set; }
            public long Passloginattempt { get; set; }
            public long Passhistorycheck { get; set; }
            public long Unuseduserbanperiod { get; set; }

        }

        public static string _updateProcedureName = "proChangePassword";
        public Output UpdateuserchangepasswordData(UpdateUserChangePassword input, string companyKey)
        {
            return Common.UpdateTableData<UpdateUserChangePassword>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<UserChangePassword>GetUserpasswordcheck(UserpswdcheckView input, string companyKey)
        {
            return Common.GetDataViaProcedure<UserChangePassword, UserpswdcheckView>(companyKey: companyKey, procedureName: "ProUsersCodeList", parameter: input);
        }
        public APIGetRecordsDynamic<Userpasswordcombinationchecklist>GetUserpolicysettingchecklist( Userpolicysettinglistcheck input, string companyKey)
        {
            return Common.GetDataViaProcedure<Userpasswordcombinationchecklist, Userpolicysettinglistcheck>(companyKey: companyKey, procedureName: "ProUserpolicysettingsList", parameter: input);
        }
    }
}