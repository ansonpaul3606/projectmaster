using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class FuleTypeModel
    {
        public class FuelTypeView
        {
            public int ID_Brand { get; set; }
            public string BrName { get; set; }
            public string BrShortName { get; set; }
            public Int32 SortOrder { get; set; }        
            
        }

        
        public class FuelList
        {
           
            public int SortOrder { get; set; }
        }

       
    }
}

    
