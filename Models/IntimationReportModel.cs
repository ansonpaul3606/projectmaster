using System;
using PerfectWebERP.General;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class IntimationReportModel
    {
        public class IntimationReportModelview
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string CustomeName { get; set; }
            public long FK_Customer { get; set; }
            public long Channel { get; set; }
            public long Status { get; set; }
        }
        public class IntimationReportviewinput
        {

            public DateTime ToDate { get; set; }
            public DateTime FromDate { get; set; }
           
            public long Channel { get; set; }
            public long Status { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
          


        }
        public class IntimationReportviewoutput
        {
         
            public string Date { get; set; }
            public string Phone { get; set; }
            public string Channel { get; set; }
      
            public string Content { get; set; }

        }


        public APIGetRecordsDynamic<IntimationReportviewoutput> GetwalkingCustomerReport(IntimationReportviewinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<IntimationReportviewoutput, IntimationReportviewinput>(companyKey: companyKey, procedureName: "ProRptIntimationReport", parameter: input);

        }


    }
}