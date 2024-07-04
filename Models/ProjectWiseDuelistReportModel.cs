using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class ProjectWiseDuelistReportModel
    {
        public class Projectwiseduelistview
        {
            public string AsonDate { get; set; }
            public long ProjectID { get; set; }
            public string Project { get; set; }

        }

        public class ProjectwiseduelistGridview
        {
            public string Date { get; set; }
            public string Project { get; set; }
            public string Team { get; set; }
            public string Stage{ get; set; }
            public string DueDays { get; set; }
            public string Customer { get; set; }
           
           


        }
        public class Projectwiseduelistreportinput
        {
           
            public long FK_Project { get; set; }
            public string AsonDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
           
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
          


        }
        public APIGetRecordsDynamic<ProjectwiseduelistGridview> GetProjectWiseDuelistDetailsData(Projectwiseduelistreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectwiseduelistGridview, Projectwiseduelistreportinput>(companyKey: companyKey, procedureName: "ProProjectWiseDuelistReportGridView", parameter: input);

        }
    }
}