using System;
using System.Collections.Generic;
using System.Text;

namespace PerfectWebERP.Business
{
    public class BLCustomer
    {
        public class Customer
        {
            public int? CustomerID { get; set; }
            public string CustomerTitle { get; set; }
            public string CustomerName { get; set; }
            public DateTime CustomerDate { get; set; }
            public string Place { get; set; }
            public string District { get; set; }
            public string State { get; set; }
            public string Phone { get; set; }
            public string Mobile { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
        public class UpdateCustomer
        {
            public BLCustomer NewData { get; set; }
            public BLCustomer CurrentData { get; set; }
        }

    }

}
