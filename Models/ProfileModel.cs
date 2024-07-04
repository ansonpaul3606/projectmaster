using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProfileModel
    {
        public class ProfileInput
        {
            public long FK_employee { get; set; }
            public long FK_Company { get; set; }
            public List<ProfileDetails> ProfileList { get; set; }
        }


        public class ProfileDetails
        {

            public string DesName { get; set; }
            public string DeptName { get; set; }
            public string EmpCode { get; set; }
            public string EmpName { get; set; }
            public string EmpEmrgContactNum { get; set; }
            public string EmptyName { get; set; }
            public string Address { get; set; }
            public string LastonlineDate { get; set; }

           public string LastOnlineTime { get; set; }
            public string LEmployeeName { get; set; }
        }

        public class GetProfileData
        {
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Employee { get; set; }
            public long FK_User { get; set; }
            public string EntrBy { get; set; }
            
        }


        public APIGetRecordsDynamic<ProfileDetails> GetUserProfileData(GetProfileData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProfileDetails, GetProfileData>(companyKey: companyKey, procedureName: "ProUserProfilerChecking", parameter: input);

        }
    }
}