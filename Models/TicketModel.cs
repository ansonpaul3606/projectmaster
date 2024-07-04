using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class TicketModel
    {
        public class Ticketview
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string   TicketNumber { get; set; }

        }
    }
}