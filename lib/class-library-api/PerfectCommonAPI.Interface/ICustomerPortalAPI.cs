using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface ICustomerPortalAPI
    {
        string BankKey { get; set; }
        string CustomerID { get; set; }
        string ID_SalesProductDetails { get; set; }
        
        string CustomerRole { get; set; }
        string TransMode { get; set; }
        string EnterBy { get; set; }
        string FK_Company { get; set; }
        string FK_Machine { get; set; }

        string Username { get; set; }
        string Phone { get; set; }
        string Email { get; set; }

        string Mode { get; set; }
        string SubMode { get; set; }
        string OTP { get; set; }
        string MPIN { get; set; }

        string TicketStatus { get; set; }
        string TicketID { get; set; }


        string JsonData { get; set; }
        string Remarks { get; set; }
        string MasterID { get; set; }
        string TransAmount { get; set; }
        string TransDate { get; set; }
        string TransID { get; set; }
        string TransType { get; set; }
        string Type { get; set; }

        string FK_Category { get; set; }
        string FK_Branch { get; set; }
        string FK_Product { get; set; }
        string Name { get; set; }
        string Offer { get; set; }
        string Mobile { get; set; }
        string EntrBy { get; set; }
       
        string CustomerRoleID { get; set; }
    }
    #region Response Interface Model Objects

    //public class CustomerWiseProductDetails : CommonAPIR
    //{
    //    public List<CustomerWiseProductList> CustomerWiseProductList { get; set; }
    //}
    //public class CustomerWiseProductList
    //{
    //    public int ProductID { get; set; }
    //    public string ProductName { get; set; }
    //    public int CategoryID { get; set; }
    //    public string Category { get; set; }
    //    public string SalBillNo { get; set; }
    //    public string SalBillDate { get; set; }
    //    public int ID_SalesProductDetails { get; set; }
      
    //}
    #endregion

}