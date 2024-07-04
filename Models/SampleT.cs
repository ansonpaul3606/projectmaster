using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SampleT
    {
        public class InputData
        {
            public int ProdID { get; set; }
        }
        public class OutputData
        {
            public List<ProductData> ProductDatas { get; set; }
            public List<SubProductData> SubProductDatas { get; set; }
        }

        public class ProductData
        {
            public int ID_Product { get; set; }
            public int ProdName { get; set; }
        }
        public class SubProductData
        {
            public int ID_SubProduct { get; set; }
        }
    }
}