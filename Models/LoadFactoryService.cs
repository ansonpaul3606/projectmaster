using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class LoadFactoryService
    {
        public class CustomerserviceassignView
        {
            [Range(1, long.MaxValue, ErrorMessage = "Select Customerserviceregister")]
            public long Customerserviceregister { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            public string Employee { get; set; }
            [Required(ErrorMessage = "Please Enter Visitdate")]
            public DateTime Visitdate { get; set; }
            [Required(ErrorMessage = "Please Enter Visittime")]
            public string Visittime { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long Branch { get; set; }
            public int Priority { get; set; }
            public string Remarks { get; set; }

            public long FK_Employee { get; set; }


            public List<Products> Product { get; set; }

            public List<OtherProducts> OtherProducts { get; set; }

            public List<CustomerDetails> customerDetails { get; set; }
            public List<Employeedetails> employeedetails { get; set; }

            public List<Branch> BranchList { get; set; }

            public List<Complaint> ComplaintList { get; set; }

            public List<Department> DepartmentList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<Status> StatusList { get; set; }
            public long ID_DefaultDept { get; set; }

        }
        public class othercompany
        {
            public int ID_OtherCompany { get; set; }
            public string OCName { get; set; }
        }
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }

        }
        public class ChannelType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class categorytyp
        {
            public Int32? ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class Complaint
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set; }
        }
        public class Service
        {

            public long ID_Services { get; set; }
            public string ServicesName { get; set; }
        }

        public class CheckList
        {
            public long CheckListDetails { get; set; }
            public string Remarks { get; set; }


        }
        public class CategoryList
        {
            public Int32? CategoryID { get; set; }
            public string CategoryName { get; set; }

        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
        }
        public class Branch
        {
            public long ID_Branch { get; set; }

            public string BranchName { get; set; }
        }
        public class Status
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
            public long FK_ActionStatus { get; set; }
        }

       
        public class Employeedetails
        {
            public Int32 ID_Employee { get; set; }
            public string Employee { get; set; }
            public Int32 ID_CSAEmployeeType { get; set; }
            public string EmployeeType { get; set; }
            public Int32 SLNO { get; set; }
            public Int32 DepartmentID { get; set; }

        }

        public class CustomerDetails
        {
            public long FK_Customerserviceregister { get; set; }

            public string FromDate { get; set; }

            public string ToDate { get; set; }

            public string FromTime { get; set; }

            public string ToTime { get; set; }

            public string Customer { get; set; }

            public string Address { get; set; }

            public string Mobile { get; set; }

            public string OtherMobile { get; set; }

            public string Landmark { get; set; }

            public string Ticket { get; set; }
            public string ID_Customer { get; set; }
            public string PickUpRemarks { get; set; }
            public string PickUpBy { get; set; }
            public string PickUpOn { get; set; }
            public string Productname { get; set; }
            public string ProductComplaint { get; set; }
            public string ProductDescription { get; set; }

            public string FollowUpDate { get; set; }
            public string FollowUpOn { get; set; }
            public string Remarks { get; set; }
            public long ProvideStandBy { get; set; } = 0;
        }

        public class Products
        {
            public long FK_Customerserviceregisterproductdetails { get; set; }
            public string Productname { get; set; }
            public string ProductComplaint { get; set; }
            public string ProductDescription { get; set; }

        }
        public class OtherProducts
        {
            public long FK_Customerserviceregisterothproductdetails { get; set; }
            public string Company { get; set; }
            public string Product { get; set; }
            public string Complaint { get; set; }
            public string Description { get; set; }

        }
        public class TicketInputNew
        {
            public long FK_Branch { get; set; }
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            public long FK_ComplaintType { get; set; }
            public Int16 Status { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string TicketNumber { get; set; }
            public string Customer { get; set; }
            public string Mobile { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long SortOrder { get; set; }
            public long Mode { get; set; }
            public long FK_Post { get; set; }
            public long FK_Area { get; set; }
            public long FK_Employee { get; set; }
            public long DueDays { get; set; }



            public class TicketInput
            {
                public long FK_Branch { get; set; }
                public string Product { get; set; }
                public string EntrBy { get; set; }
                public long FK_ComplaintType { get; set; }
                public Int16 Status { get; set; }
                public DateTime? FromDate { get; set; }
                public DateTime? ToDate { get; set; }
                public string TicketNumber { get; set; }
                public string Customer { get; set; }
                public string Mobile { get; set; }
                public string Content { get; set; }
                public long FK_Company { get; set; }


            }


        }
        public class ServiceAssignDataCount
        {
            public int New { get; set; } = 0;
            public int Completed { get; set; } = 0;
            public int Pending { get; set; } = 0;
            public int PickupRequest { get; set; } = 0;
            public int ReplacementRequest { get; set; } = 0;
            public int DeliveryRequest { get; set; } = 0;
            public int FactoryService { get; set; } = 0;
            public string NewStatus { get; set; } = "display:none";
            public string CompletedStatus { get; set; } = "display:none";
            public string PendingStatus { get; set; } = "display:none";
            public string PickupRequestStatus { get; set; } = "display:none";
            public string ReplacementRequestStatus { get; set; } = "display:none";
            public string DeliveryRequestStatus { get; set; } = "display:none";
            public string FactoryServiceStatus { get; set; } = "display:none";
        }
        public class TicketViewNew
        {
            public long Slno { get; set; }
            public long ID_CustomerServiceRegister { get; set; }
            public string TicketNo { get; set; }
            public string TicketDate { get; set; }
            public string Branch { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string Mobile { get; set; }
            public string Area { get; set; }
            //public string Product { get; set; }
            //public string Complaint { get; set; }
            public Int32 PriorityCode { get; set; }
            public string Employee { get; set; }
            public string Channel { get; set; }
            public string Priority { get; set; }
            public Int32 StatusCode { get; set; }
            public string Status { get; set; }
            public string Due { get; set; }
            public Int32 Assign { get; set; }
            public Int64 TotalCount { get; set; }
            public Int32 pageSize { get; set; }
            public Int32 pageIndex { get; set; }
            public string Actions { get; set; }
            public int MasterID { get; set; }
            public int Value { get; set; }
            public string Field { get; set; }
            public long ID_Master { get; set; }
            public string TransMode { get; set; }

            public long ID_CustomerServiceRegisterProductDetails { get; set; }
        }


        public APIGetRecordsDynamic<TicketViewNew> GetTicketsnew(TicketInputNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<TicketViewNew, TicketInputNew>(companyKey: companyKey, procedureName: "ProSelectTickets", parameter: input);
        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}