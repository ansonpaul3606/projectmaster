using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class UserPasswordresetModel
    {

        public class UserpasswordResetView
        {

           public long UserID { get; set; }
            public String UserCode { get; set; }
            public string UserPassword { get; set; }


        }

        public class UserCodeList
        {
            public int UserID { get; set; }
            public string UserName { get; set; }

            public string UserCode { get; set; }
            public string Branch { get; set; }
           

        }
        public class Updateuserpaswordreset
        {
          
            public long ID_Users { get; set; }
            public string UserCode { get; set; }     
            public string UserPassword { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
           // public byte UserAction { get; set; }


            //public long FK_Users { get; set; }
            // public DateTime UserExpDate { get; set; }




        }

        public static string _updateProcedureName = "ProUserPasswordresetUpdate";
        public Output UpdateUserpasswordresetData(Updateuserpaswordreset input, string companyKey)
        {
            return Common.UpdateTableData<Updateuserpaswordreset>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

    }
}