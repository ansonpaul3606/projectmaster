using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IService
    {
        //string BankKey
        //int FK_Branch { get; set; }
        //int FK_BranchCodeUser { get; set; }
        //int FK_Company { get; set; }
        //string EntrBy { get; set; }
        //int FK_Machine { get; set; }
        //int FK_Customer { get; set; }
        //int FK_CustomerOthers { get; set; }
        //string CSRTickno { get; set; }
        //string CusName { get; set; }
        //string CusMobile { get; set; }
        //DateTime TicketDate { get; set; }
        //string CusAddress { get; set; }
        //string CSRContactNo { get; set; }
        //string CSRLandmark { get; set; }
        //string CSRServiceFromDate { get; set; }
        //string CSRServiceToDate { get; set; }
        //string CSRServicefromtime { get; set; }
        //string CSRServicetotime { get; set; }
        //string CSRChannelID { get; set; }
        //string CSRChannelSubID { get; set; }
        //string CSRPriority { get; set; }
        //string CSRCurrentStatus { get; set; }
        //string CSRPCategory { get; set; }
        //string FK_OtherCompany { get; set; }
        //string FK_Product { get; set; }
        //int FK_ComplaintList { get; set; }
        //string CSRODescription { get; set; }
        //int FK_ServiceList { get; set; }
        //int Status { get; set; }
        //string AttendedBy { get; set; }
        //string CheckList { get; set; }
    }
    #region Response Interface Model Objects
   
    public class CustomerDueDetils:CommonAPIR
    {
         public List<CustomerDueDetilsList> CustomerDueDetilsList { get; set; }
    }
    public class CustomerDueDetilsList
    {
        public string  AccountDetails { get; set; }
        public string  Balance { get; set; }
        public decimal  Due { get; set; }

    }
    #endregion

}
