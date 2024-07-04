using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Business
{
    public class BlServiceAssignModel
    {
        public class RootServiceCountDetails : CommonAPIResponse
        {
            public ServiceCountDetails ServiceCountDetails { get; set; }
        }
        public class RootServiceAssignNewDetails : CommonAPIResponse
        {
            public ServiceAssignNewDetails ServiceAssignNewDetails { get; set; }
        }
        public class RootCustomerBalanceDetails : CommonAPIResponse
        {
            public CustomerBalanceDetails CustomerBalanceDetails { get; set; }
        }
        public class RootServiceAssignOnGoingDetails : CommonAPIResponse
        {
            public ServiceAssignOnGoingDetails ServiceAssignOnGoingDetails { get; set; }
        }        
        public class RootTicketDetails : CommonAPIResponse
        {
            public ServiceAssignOnGoingDetails ServiceAssignOnGoingDetails { get; set; }
        }
        public class RootRoleDetails : CommonAPIResponse
        {
            public RoleDetails RoleDetails { get; set; }
        }
        public class RootServiceAssignDetails : CommonAPIResponse
        {
            public ServiceAssignDetails ServiceAssignDetails { get; set; }
        }
        public class RootServiceAssignedWork : CommonAPIResponse
        {
            public ServiceAssignedWork ServiceAssignedWork { get; set; }
        }
        
        public class RootCustomerserviceassignUpdate : CommonAPIResponse
        {
            public CustomerserviceassignUpdate CustomerserviceassignUpdate { get; set; }
        }
        public class RootCustomerserviceassignEdit : CommonAPIResponse
        {
            public CustomerserviceassignEdit CustomerserviceassignEdit { get; set; }
        }
      
       
    }
}
