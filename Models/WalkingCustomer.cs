using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class WalkingCustomer
    {
        public long CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }    
        
        public long CategoryId { get; set; }
        public long ProductId { get; set; }
        public string ProdName { get; set; }
        public string CusEmail { get; set; }
    }
}