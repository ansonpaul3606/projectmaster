using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SalesReportModel
    {
        public class salelist
        {


            public string Companyname { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Department> deprtmnt { get; set; }
            public List<Paymentmethods> PaymentmethodList { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<Categorydata> categorytyps { get; set; }
            public long FK_BranchMode { get; set; }
            
        }
        

     
     
        public class Categorydata
        {
            public Int32 ID_Category { get; set; }
            public string CatName { get; set; }
        }
       
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class Paymentmethods
        {
            public int PaymentmethodID { get; set; }
            public string Paymentmethod { get; set; }

        }
        public class Department
        {
            public int DepId { get; set; }
            public string Depname { get; set; }

        }

    }
}
   