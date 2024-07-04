using System;
using PerfectWebERP.General;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class WalkingCustomerReportModel
    {

        public class Walkingcustomerview
        {
            public long FK_Report { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime FromDate { get; set; }
            public long EmployeeID { get; set; }
            public string Employee { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Employee { get; set; }


        }

        public class Walkingcustomerviewinput
        {
            public long FK_Report { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime FromDate { get; set; }
           
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Employee { get; set; }


        }
        public class WalkingcustomerlistOutputView
        {
            //public long SLNo { get; set; }  

            public string LeadNo { get; set; }
            public string Customer { get; set; }
            public string Mobile { get; set; }
          
            public string AssignedTo { get; set; }
            public string AssignedDate { get; set; }

        }

        public APIGetRecordsDynamic<WalkingcustomerlistOutputView> GetwalkingCustomerReport(Walkingcustomerviewinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<WalkingcustomerlistOutputView, Walkingcustomerviewinput>(companyKey: companyKey, procedureName: "ProRptWalkingCustomerListReport", parameter: input);

        }


    }
}