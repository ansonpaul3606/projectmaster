using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PerfectWebERPAPI.Business
{
   public class BlServiceFollowUpModel
    {
        public class RootServiceFollowUpdetails : CommonAPIResponse
        {
            public ServiceFollowUpdetails ServiceFollowUpdetails { get; set; }
        }
        public class RootComplaintListDetails : CommonAPIResponse
        {
            public ComplaintListDetails ComplaintListDetails { get; set; }
        }

        public class RootWarrantyAMCDetails : CommonAPIResponse
        {
            public WarrantyAMCDetails WarrantyAMCDetails { get; set; }
        }
        public class RootEmployeeWiseTicketSelect: CommonAPIResponse
        {
            public EmployeeWiseTicketSelect EmployeeWiseTicketSelect { get; set; }
        }
        public class RootAttendancedetails:CommonAPIResponse
        {
            public Attendancedetails Attendancedetails { get; set; }
        }
        public class RootServiceHistoryDetails:CommonAPIResponse
        {
            public ServiceHistoryDetails ServiceHistoryDetails { get; set; }
        }
        public class RootServiceAttendedDetails : CommonAPIResponse
        {
            public ServiceAttendedDetails ServiceAttendedDetails { get; set; }
        }
        public class RootServiceType : CommonAPIResponse
        {
            public ServiceType ServiceType { get; set; }
        }
        public class RootAddedService : CommonAPIResponse
        {
            public AddedService AddedService { get; set; }
        }
        public class RootChangemodeDetails : CommonAPIResponse
        {
            public ChangemodeDetails ChangemodeDetails { get; set; }
        }
        public class RootReplaceProductdetails : CommonAPIResponse
        {
            public ReplaceProductdetails ReplaceProductdetails { get; set; }
        }
        public class RooPopUpProductdetails : CommonAPIResponse
        {
            public PopUpProductdetails PopUpProductdetails { get; set; }
        }
        public class RootPopUpSerchCritrea : CommonAPIResponse
        {
            public PopUpSerchCritrea PopUpSerchCritrea { get; set; }
        }
        public class RootActionDetails : CommonAPIResponse
        {
            public FollowUpActionDetails FollowUpActionDetails { get; set; }
        }
        public class RootFollowUpPaymentMethod : CommonAPIResponse
        {
            public FollowUpPaymentMethod FollowUpPaymentMethod { get; set; }
        }
        public class RootGetSubProductDetails : CommonAPIResponse
        {
            public SubproductDetails SubproductDetails { get; set; }
        }
        
        public class RootUpdateServiceFollowUp : CommonAPIResponse
        {
            public UpdateServiceFollowUp UpdateServiceFollowUp { get; set; }
        }
        public class RootMoreComponentDetails:CommonAPIResponse
        {
            public MoreComponentDetails MoreComponentDetails { get; set; }
        }
        //public class RootServiceDetails : CommonAPIResponse
        //{
        //    public ServiceList ServiceList { get; set; }
        //}
        public class RootServiceDetails : CommonAPIResponse
        {
            public SerDetails ServiceDetails { get; set; }
        }
        public class RootproductInfo : CommonAPIResponse
        {
            public ProductInfo ProductInfo { get; set; }
        }
        public class RootServiceInvoiceDetails : CommonAPIResponse
        {
            public ServiceInvoice ServiceInvoice { get; set; }
        }
        public class RootOtherChargeDetails : CommonAPIResponse
        {
            public OtherChargeDetails OtherChargeDetails { get; set; }
        }
        public class RootGetMainProductDetails : CommonAPIResponse
        {
            public MainProductDetails MainProductDetails { get; set; }

        }
        public class RootSearchList : CommonAPIResponse
        {
            public ServiceSerach ServiceSerach { get; set; }
        }
        public class RootProductList : CommonAPIResponse
        {
            public ProductListDetails ProductListDetails { get; set; }
        }
        
    }
}
