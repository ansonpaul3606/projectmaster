using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SearchFilter
    {
        public string FilterNameKey { get; set; }
        public string FilterDataURL { get; set; }
        public string FilterType { get; set; }
        public dynamic FilterData { get; set; }

    }
    public class FilterData_input //When FilterType = input
    {
        /// <summary>
        /// labe given in front end
        /// </summary>
        public string FilterLabel { get; set; }
        /// <summary>
        /// variable name when you send back the data 
        /// </summary>
        public string FilterName { get; set; }
        /// <summary>
        /// if it has  a predefined value assigend | This can be null
        /// </summary>
        public string FilterValue { get; set; }
        /// <summary>
        /// in the case of inout field set this as place holder
        /// </summary>
        public string Placeholder { get; set; }
    }
    public class FilterData_dropdown //When Filtertype = dropdown
    {
        public string FilterLabel { get; set; }// labe given in front end
        public string FilterName { get; set; } // variable name when you send back the data 
        public string FilterValue { get; set; } // if it has  a predefined value assigend | This can be null
        public bool MultipleSelect { get; set; } // if dropdown need multiple select
        public List<FilterKeyValuePair> SelectOptions { get; set; }
    }
    public class FilterData_radio //When Filtertype = radio
    {
        public string FilterLabel { get; set; }// labe given in front end
        public string FilterName { get; set; } // variable name when you send back the data 
        public string FilterValue { get; set; } // if it has  a predefined value assigend | This can be null
        public List<FilterKeyValuePair> SelectOptions { get; set; }
    }
    public class FilterData_checkbox //When Filtertype = checkbox
    {
        public string FilterLabel { get; set; }// labe given in front end
        public string FilterName { get; set; } // variable name when you send back the data 
        public string FilterValue { get; set; } // if it has  a predefined value assigend | This can be null
        public List<FilterKeyValuePair> SelectOptions { get; set; }
    }
    public class FilterData_datepicker //When Filtertype = datepicker
    {
        public string FilterLabel { get; set; }// labe given in front end
        public string FilterName { get; set; } // variable name when you send back the data 
        public string FilterValue { get; set; } // if it has  a predefined value assigend | This can be null
        public bool DateRangePicker { get; set; }
    }
    public class FilterData_check //When Filtertype = checkbox
    {
        public string FilterLabel { get; set; }// labe given in front end
        public string FilterName { get; set; } // variable name when you send back the data 
        public string FilterValue { get; set; } // if it has  a predefined value assigend | This can be null
        public FilterKeyValuePair SelectOptions { get; set; }
    }
    public class FilterKeyValuePair
    {
        public string FilterJSONKey { get; set; }
        public string FilterJSONvalue { get; set; }
        //  public List<SearchFilter> FilterSelectAction { get; set; }
    }
}