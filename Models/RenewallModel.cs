using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class RenewallModel
    {
        public string NAME { get; set; }      
        public string ID_Papper { get; set; }
        public string PapperNo { get; set; }
        public string ID_Provider { get; set; }

        public string IssueDate { get; set; }
        public string ExpireDATE { get; set; }
        public string FK_Master { get; set; }



    }
}