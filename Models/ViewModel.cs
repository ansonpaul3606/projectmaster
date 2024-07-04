using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ViewModel
    {
        public class SideMenu
        {
            public string Name { get; set; }
            public bool HasURL { get; set; }//HasURL :true ,Fill page url info || HasUR : false, Fill submenu option 
            public string PageURL { get; set; }
            public List<SideMenu> SubMenu { get; set; }
            public string Icon { get; set; }
            public long MenuID { get; set; }

        }



        public class LocationLists
        {
            public List<Place> PlaceList { get; set; }
            public List<District> DistrictList { get; set; }
            public List<State> StateList { get; set; }
            public List<CustomerTitle> CustomerTitlesList { get; set; }
            public Customer customerInfo { get; set; }
        }

        public class SampleCustomerView
        {
            public List<Place> PlaceList { get; set; }
            public List<CustomerTitle> CustomerTitlesList { get; set; }
            public List<State> StateList { get; set; }
        }


        // Model for favorate bar
        //same data for both favo and the tile like page selection

      





    }
}