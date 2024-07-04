using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class UserPolicySettingsModel
    {
        public class UserPolicySettingsView
        {
            //public long PasswordcombinationId { get; set; }
            //public long Passwordcombination { get; set; }
            //public long Passwordminimumlength { get; set; }
            //public long Passwordmaximumlength { get; set; }
            //public long Passwordchangeperiod { get; set; }
            //public long Passwordloginattempt { get; set; }
            //public long Passwordhistorycheck { get; set; }
            //public long Unuseduserbanperiod { get; set; }
            //public string[] UplSecMods { get; set; }
            //public string[] UplSecVals { get; set; }
            //public List<UplSecModes> UplSecMods { get; set; }
            //public List<UplSecValues> UplSecVals { get; set; }
            public List<UplSecdata> UplSecdata { get; set; }

        }
        public class UplSecdata
        {
            public long UplSecMode { get; set; }
            public string UplSecValue { get; set; }
        }
            public class UplSecModes
        {
            //public long Passwordcombinationid { get; set; }
            //public long Passwordminimumlengthid { get; set; }
            //public long Passwordmaximumlengthid { get; set; }
            //public long Passwordchangeperiodid { get; set; }
            //public long Passwordloginattemptid { get; set; }
            //public long Passwordhistorycheckid { get; set; }
            //public long Unuseduserbanperiodid { get; set; }
            public long UplSecMode { get; set; }
            public string UplSecValue { get; set; }

        }
        public class UplSecValues
        {
            //public long Passwordcombination { get; set; }
            //public long Passwordminimumlength { get; set; }
            //public long Passwordmaximumlength { get; set; }
            //public long Passwordchangeperiod { get; set; }
            //public long Passwordloginattempt { get; set; }
            //public long Passwordhistorycheck { get; set; }
            //public long Unuseduserbanperiod { get; set; }
            public string UplSecValue { get; set; }
        }
        public class UplSecListValues
        {
            public long Passcombination { get; set; }
            public long Passminimumlength { get; set; }
            public long Passmaximumlength { get; set; }
            public long Passchangeperiod { get; set; }
            public long Passloginattempt { get; set; }
            public long Passhistorycheck { get; set; }
            public long Unuseduserbanperiod { get; set; }

        }
        public class UserPolicyView
        {
            public long FK_UserPolicySettings { get; set; }
            public List<UplValues> UplSecdata { get; set; }

        }
        public class UplValues
        {
            public long SlNo { get; set; }
            public long ID_UserPolicySettings { get; set; }
            public string UplSecMode { get; set; }
            public string UplSecValue { get; set; }
           

        }
        public class GetUserpolicy
        {
            public Int64 FK_UserPolicySettings { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            
        }

            public class UpdateUserPolicySettings
        {
            public long FK_UserPolicySettings { get; set; }
            public long ID_UserPolicySettings { get; set; }
            public long UplSecMode { get; set; }
            public string UplSecValue { get; set; }
            public string UplSecdata { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public bool Debug { get; set; }

        }
        public static string _updateProcedureName = "ProUserPolicySettingsUpdate";
        public static string _selectProcedureName = "ProUserPolicySettingsSelect";
        public Output UpdateuserPolicySettings(UpdateUserPolicySettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateUserPolicySettings>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<UplValues> GetuserpolicyData(GetUserpolicy input, string companyKey)
        {
            return Common.GetDataViaProcedure<UplValues, GetUserpolicy>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
    
}