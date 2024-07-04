using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Business
{
    public class BlServiceSection
    {
       public string BankKey { get; set; }
       public string Token { get; set; }
       public int FK_Branch { get; set; }
       public int FK_BranchCodeUser { get; set; }
       public int FK_Company { get; set; }
       public string EntrBy { get; set; }
       public int FK_Machine { get; set; }
       public int FK_Customer { get; set; }
       public int FK_CustomerOthers { get; set; }
       public string CSRTickno { get; set; }
       public string CusName { get; set; }
       public string CusMobile { get; set; }
       public DateTime TicketDate { get; set; }
       public string CusAddress { get; set; }
       public string CSRContactNo { get; set; }
       public string CSRLandmark { get; set; }
       public string CSRServiceFromDate { get; set; }
       public string CSRServiceToDate { get; set; }
       public string CSRServicefromtime { get; set; }
       public string CSRServicetotime { get; set; }
       public string CSRChannelID { get; set; }
       public string CSRChannelSubID { get; set; }
       public string CSRPriority { get; set; }
       public string CSRCurrentStatus { get; set; }
       public string CSRPCategory { get; set; }
       public string FK_OtherCompany { get; set; }
       public string FK_Product { get; set; }
       public int FK_ComplaintList { get; set; }
       public string CSRODescription { get; set; }
       public int FK_ServiceList { get; set; }
       public int Status { get; set; }
       public string AttendedBy { get; set; }
       public string CheckList { get; set; }
    }
}
