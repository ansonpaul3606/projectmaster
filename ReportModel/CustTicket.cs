using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.ReportModel
{
    public class CustTicket
    {
        public Int16 SLNO { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string TicketNo { get; set; }
        public string AssignedEmployee { get; set; }
        public string Status { get; set; }
        public string VisitDate { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public byte[] img { get; set; }
        public byte[] footer { get; set; }
        public byte[] wmark { get; set; }



    }
}