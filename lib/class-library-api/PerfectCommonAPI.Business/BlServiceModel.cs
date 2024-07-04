using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Business
{
    public class BlServiceModel
    {
        public class RootServiceCustomer : CommonAPIResponse
        {
            public ServiceCustomerDetails ServiceCustomerDetails { get; set; }
        }
        public class RootCommonPopup : CommonAPIResponse
        {
            public CommonPopupDetails CommonPopupDetails { get; set; }
        }
        public class RootServiceProductDetails : CommonAPIResponse
        {
            public ServiceProductDetails ServiceProduct { get; set; }
        }
        public class RootServiceDet : CommonAPIResponse
        {
            public ServiceDet ServiceDetails { get; set; }
        }
        public class RootComplaintsDetails : CommonAPIResponse
        {
            public ComplaintDetails ComplaintsDetails { get; set; }
        }
        public class RootWarrantyDetails : CommonAPIResponse
        {
            public WarrantyDetails WarrantyDetails { get; set; }
        }
        public class RootProductHistory : CommonAPIResponse
        {
            public ProductHistoy ProductHistory { get; set; }
        }
        public class RootSalesHistory : CommonAPIResponse
        {
            public SalesHistory SalesHistory { get; set; }
        }
        public class RootUpdateCustomerServiceRegister : CommonAPIResponse
        {
            public UpdateCustomerServiceRegister UpdateCustomerServiceRegister { get; set; }
        }
        public class RootMediaDetails : CommonAPIResponse
        {
            public MediaDetails MediaDetails { get; set; }
        }
        public class RootCustomerServiceRegisterCount : CommonAPIResponse
        {
            public CustomerServiceRegisterCount CustomerServiceRegisterCount { get; set; }
        }
        public class RootCustomerDueDetils : CommonAPIResponse
        {
            public CustomerDueDetils CustomerDueDetils { get; set; }
        }

    }
}
