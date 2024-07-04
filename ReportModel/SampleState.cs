using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.ReportModel
{
    public class SampleState
    {
        public long StateID { get; set; }


        public string State { get; set; }

        public string ShortName { get; set; }


        public int GSTINCode { get; set; }



        public long CountryID { get; set; }

        public string Country { get; set; }
        public Int32 SortOrder { get; set; }
    }
}