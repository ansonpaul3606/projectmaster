/*----------------------------------------------------------------------
Created By	: Athul mathew
Created On	: 07/02/2022
Purpose		: Customerserviceassign
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PerfectWebERP.Models
{
    public class CustomerserviceassignModel
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
        public class Employeedetails
        {
            public Int32 ID_Employee { get; set; }
            public string Employee { get; set; }
            public Int32 ID_CSAEmployeeType { get; set; }
            public string EmployeeType { get; set; }
            public Int32 SLNO { get; set; }
            public Int32 DepartmentID { get; set; }

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

            public string  BranchName { get; set; }
        }
        public class Status
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
            public long FK_ActionStatus { get; set; }
        }

        public class Complaint
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set;} 
        }
        public class UpdateCustomerserviceassign
        {
            public byte Debug { get; set; }
            public int UserAction { get; set; }
            public long ID_CustomerServiceAssign { get; set; }
            public long FK_Customerserviceregister { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            public long FK_Employee { get; set; }
            [Required(ErrorMessage = "Please Enter Date")]
            public DateTime CSAVisitdate { get; set; }
            [Required(ErrorMessage = "Please Enter Time")]
            public string CSAVisittime { get; set; }
            public int CSAPriority { get; set; }
            public string CSARemarks { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            //public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
            //public DateTime EntrOn { get; set; }
            //public string CancelBy { get; set; }
            //public DateTime CancelOn { get; set; }
           // public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_Branch { get; set; }
            public long FK_CustomerServiceAssign { get; set; }
            public string Assignees { get; set; }
            public long Status { get; set; }
            public long FK_Attendedby { get; set; }
            public long ID_CustomerServiceRegisterProductDetails { get; set; }
            public long ?LastID { get; set; }
            public long FK_Vehicle { get; set; }
        }
        public class Customerserviceassignviewnew
        {
            public byte Debug { get; set; }
            public int UserAction { get; set; }
            public long ID_Customerserviceassign { get; set; }
            public long FK_Customerserviceregister { get; set; }
            public long FK_Employee { get; set; }
            public DateTime CSAVisitdate { get; set; }
            public string CSAVisittime { get; set; }
            public int CSAPriority { get; set; }
            public string CSARemarks { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Customerserviceassign { get; set; }
            public string EmployeeDetails { get; set; }
            public List<Assignees> Assignees { get; set; }
            public long Status { get; set; }
            public long FK_Attendedby { get; set; }
            public long ID_CustomerServiceRegisterProductDetails { get; set; }
            public long ?LastID { get; set; }
            public long FK_Vehicle { get; set; }
        }
            public class UpdateCustomerserviceassignnew
        {
            public byte Debug { get; set; }
            public int UserAction { get; set; }
            public long ID_CustomerServiceAssign { get; set; }
            public long FK_Customerserviceregister { get; set; }
            public long FK_Employee { get; set; }
            public DateTime CSAVisitdate { get; set; }
            public string CSAVisittime { get; set; }
            public int CSAPriority { get; set; }
            public string CSARemarks { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            //public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
            //public DateTime EntrOn { get; set; }
            //public string CancelBy { get; set; }
            //public DateTime CancelOn { get; set; }
            // public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Customerserviceassign { get; set; }
            public string Assignees { get; set; }
            public long ID_CustomerServiceRegisterProductDetails { get; set; }
            public long ?LastID { get; set; }
           
        }
        public class Assignees
        {
           
            public long FK_Employee { get; set; }
            public Int32 EmployeeType { get; set; }
           
           

        }

        public static string _deleteProcedureName = "ProCustomerserviceassignDelete";
        public static string _updateProcedureName = "ProCustomerserviceassignUpdate";
        public static string _selectProcedureName = "ProCustomerserviceassignSelect";

        public class DeleteCustomerserviceassign
        {
            public long ID_Customerserviceassign { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }

        public static string _SelectTickets = "ProSelectTickets";
        public class TicketView
        {
            public long FK_Customerserviceregister { get; set; }
            public string Branch { get; set; }
            public string Customer { get; set; }
            public string Ticket { get; set; }
            public string TicketDate { get; set; }
            public string Product { get; set; }
            public string Complaint { get; set; }
            public string Priority { get; set; }
            public bool Assigned { get; set; }
            public string Mobile { get; set; }

            public Int32 TicketStatus { get; set; }
        }
        public class TicketViewNew
        {
            public long ?Slno { get; set; }
            public long ?ID_CustomerServiceRegister { get; set; }
            public string TicketNo { get; set; }
            public string TicketDate { get; set; }
            public string Branch { get; set; }
            public string Customer { get; set; }
            public string Product { get; set; }
            public string Mobile { get; set; }
            public string Area { get; set; }
            //public string Product { get; set; }
            //public string Complaint { get; set; }
            public Int32 ?PriorityCode { get; set; }
            public string Employee { get; set; }
            public string Channel { get; set; }
            public string Priority { get; set; }
            public Int32 ?StatusCode { get; set; }
            public string Status { get; set; }
            public string Due { get; set; }
            public Int32 ?Assign { get; set; }
            public Int64 ?TotalCount { get; set; }
            public Int32 ?pageSize { get; set; }
            public Int32 ?pageIndex { get; set; }
            public string Actions { get; set; }
            public int ?MasterID { get; set; }
            public int ?Value { get; set; }
            public string Field { get; set; }
            public long ?ID_Master { get; set; }
            public string TransMode { get; set; }

            public long ?ID_CustomerServiceRegisterProductDetails { get; set; }    
            public long ?LastID { get; set; }
        }


        
        public class DeliveryInput
        {

            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Employee { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Area { get; set; }
            public string FK_Category { get; set; }

            public long FK_Product { get; set; }
            public string Priority { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public string TicketNo { get; set; }
            public long Mode { get; set; }
            public string Module { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public Int16 Status { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }


        }

        public class DeliveryTicket
        {
            public long Slno { get; set; }
            public long ID_ProductDeliveryAssign { get; set; }
            public string Module { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Master { get; set; }
            public long FK_Employee { get; set; }
            public string ReferenceNo { get; set; }
            public string PickUpTime { get; set; }
            public string AssignedOn { get; set; }
            public string CustomerName { get; set; }
            public string Mobile { get; set; }
            public string Area { get; set; }
            public string EMPName { get; set; }
            public string Priority { get; set; }
            public Int32 PriorityCode { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64 Count { get; set; }
            public Int32 pageSize { get; set; }
            public Int32 pageIndex { get; set; }
            public string Channel { get; set; }
            public long Mode { get; set; }
            public string TicketNo { get; set; }


        }


        

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

        public static string _SelectEmployee = "ProEmployeeServiceAssign";
        public class EmployeeInput
        {
            public string TransMode { get; set; }
            public long FK_Employee { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class Employee
        {
            public long ID_Employee { get; set; }

            public long EmpID { get; set; }

            [DisplayName("Employee Code")]
            public string EmployeeCode { get; set; }
            [DisplayName("Employee Name")]
            public string EmployeeName { get; set; }
            public string EmpStatus { get; set; }


        }

        public class CustomerserviceregisterInput
        {
            public string TransMode { get; set; }
            public long FK_Customerserviceregister { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public static string _SelectServiceRegister = "";

        public class CustomerserviceregisterView
        {
            public long FK_Customerserviceregister { get; set; }
            public long FK_CustomerserviceregisterProductDetails { get; set; }
            public List<Products> Product { get; set; }

            public List<OtherProducts> OtherProducts { get; set; }

            public DateTime FromDate { get; set; }

            public DateTime ToDate { get; set; }

            public string FromTime { get; set; }

            public string ToTime { get; set; }

            public string Customer { get; set; }

            public string Address { get; set;}

            public string Mobile { get; set; }

            public string OtherMobile { get; set; }

            public string Landmark { get; set;}

            public string Ticket { get; set; }
            public string TransMode { get; set; }

        }

        public class Products
        {
            public long FK_Customerserviceregisterproductdetails { get; set; }
            public string Productname { get; set; }
            public string ProductComplaint { get; set; }
            public string  ProductDescription { get; set; }

        }

        public class OtherProducts
        {
            public long FK_Customerserviceregisterothproductdetails { get; set; }
            public string Company { get; set; }
            public string Product { get; set; }
            public string Complaint { get; set; }
            public string Description { get; set; }

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
            public long ProvideStandBy { get; set; }=0;
        }

        public class ServiceAssignCustomerInfo
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
            public string Priority { get; set; }
            public long ID_Customerserviceassign { get; set; }
            public long ID_Customer { get; set; }

            public string FollowUpDate { get; set; }
            public string FollowUpOn { get; set; }
        }

        public class CustomerserviceregisterViewNew
        {
            public Int64 ID_Customerserviceassign { get; set; }
            public long FK_Customerserviceregister { get; set; }
            //public List<Products> Product { get; set; }
            public long FK_Customerserviceregisterproductdetails { get; set; }
            public string Productname { get; set; }
            public string ProductComplaint { get; set; }
            public string  ProductDescription { get; set; }
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
            public string Priority { get; set; }
            public long ID_Customer { get; set; }
            public List<Status> StatusList { get; set; }
            public string FollowUpDate { get; set; }
            public string FollowUpOn { get; set; }
            public string Remarks { get; set; }
            public long ProvideStandBy { get; set; } = 0;
        }
        public class CustomerserviceassignViewNew
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

            public List<ServiceAssignCustomerInfo> Assign { get; set; }
            
            public List<Products> Product { get; set; }
            public long FK_Customerserviceregisterproductdetails { get; set; }
            public string Productname { get; set; }
            public string ProductComplaint { get; set; }
            public string ProductDescription { get; set; }

            public List<OtherProducts> OtherProducts { get; set; }

            public List<CustomerDetails> customerDetails { get; set; }

            public List<Branch> BranchList { get; set; }

            public List<Complaint> ComplaintList { get; set; }

            public List<Department> DepartmentList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public Int64 ID_Customerserviceassign { get; set; }
            public List<Status> StatusList { get; set; }

        }
        public class CustomerserviceassignID
        {
            public Int64 ID_Customerserviceassign { get; set; }
        }

        public class CustomerserDataInputs
        {
            public string TransMode { get; set; }
            public long FK_Customerserviceregister { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class EmployeeGetInput
        {
            public long FK_Customerserviceregister { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long PageMode { get; set; }
        }
        public class EmployeeHistoryInput
        {
            public long FK_Employee { get; set; }
            public string VisitDate { get; set; }
            public string Visittime { get; set; }
            public long Mode { get; set; }
            
        }
        public class EmployeeHistoryview
        {
            public string TicketNo { get; set; }

            public string VisitDate { get; set; }

            public string VisitDime { get; set; }

            public string Message { get; set; }

           
        }

        public class EmployeeDataInputs
        {
            public string TransMode { get; set; }
            public long FK_CustomerServiceAssign { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
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

        public Output UpdateCustomerserviceassignData(UpdateCustomerserviceassign input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerserviceassign>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCustomerserviceassignData(DeleteCustomerserviceassign input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustomerserviceassign>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CustomerserviceassignView> GetCustomerserviceassignData(CustomerserviceassignID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerserviceassignView, CustomerserviceassignID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<TicketView> GetTickets(TicketInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<TicketView, TicketInput>(companyKey: companyKey, procedureName: _SelectTickets, parameter: input);
        }
        public APIGetRecordsDynamic<TicketViewNew> GetTicketsnew(TicketInputNew input, string companyKey)
        {
            return Common.GetDataViaProcedure<TicketViewNew, TicketInputNew>(companyKey: companyKey, procedureName:"ProSelectTickets", parameter: input);
        }

        public APIGetRecordsDynamic<CustomerserviceregisterView> GetServiceRegisterDetails(CustomerserviceregisterInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerserviceregisterView, CustomerserviceregisterInput>(companyKey: companyKey, procedureName: _SelectTickets, parameter: input);
        }

        public APIGetRecordsDynamic<Employee> GetEmployee(EmployeeInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Employee, EmployeeInput>(companyKey: companyKey, procedureName: _SelectEmployee, parameter: input);
        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdateCustomerserviceassignDatanew(UpdateCustomerserviceassignnew input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerserviceassignnew>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<CustomerDetails> GetCustomerserviceData(CustomerserDataInputs input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerDetails, CustomerserDataInputs>(companyKey: companyKey, procedureName: "ProCustomerserviceregisterSelectCustomer", parameter: input);
        }
        public APIGetRecordsDynamic<CustomerserviceregisterViewNew> GetCustomerserviceData1(CustomerserDataInputs input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerserviceregisterViewNew, CustomerserDataInputs>(companyKey: companyKey, procedureName: "ProCustomerserviceregisterSelectCustomer", parameter: input);
        }
        public APIGetRecordsDynamic<ServiceAssignCustomerInfo> GetCustomerserviceAssignee(EmployeeDataInputs input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceAssignCustomerInfo, EmployeeDataInputs>(companyKey: companyKey, procedureName: "ProCustomerServiceAssigneeSelect", parameter: input);
        }
        public APIGetRecordsDynamic<Employeedetails> GetEmployeeData(EmployeeGetInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Employeedetails, EmployeeGetInput>(companyKey: companyKey, procedureName: "ProServiceFollowUpDetailSelect", parameter: input);
        }
        public APIGetRecordsDynamic<EmployeeHistoryview> GetEmployeeHistory(EmployeeHistoryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeHistoryview, EmployeeHistoryInput>(companyKey: companyKey, procedureName: "ProCheckEmployeeAvailability", parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> GetEmployeeTicketHistory(EmployeeHistoryInput input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<EmployeeHistoryInput>(companyKey: companyKey, procedureName: "ProCheckEmployeeAvailability", parameter: input);
        }

        public Output UpdateCustomerserviceDeliveryassignData(UpdateCustomerserviceassign input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerserviceassign>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<DeliveryTicket> GetDeliveryresult(DeliveryInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliveryTicket, DeliveryInput>(companyKey: companyKey, procedureName: "ProProductDeliveryList", parameter: input);
            
        }

        //service assign new changes

        public class CustomerserDataSelectInputs
        {
            public string TransMode { get; set; }
            public long FK_CustomerserviceregisterProductDetails { get; set; }            
            public long FK_Customerserviceregister { get; set; }            
        }
        public APIGetRecordsDynamic<CustomerDetails> GetCustomerserviceNewDataSelect(CustomerserDataSelectInputs input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerDetails, CustomerserDataSelectInputs>(companyKey: companyKey, procedureName: "ProCustomerserviceregisterSelectCustomer", parameter: input);
        }
        public APIGetRecordsDynamic<CustomerserviceregisterViewNew> GetCustomerserviceDataSelect(CustomerserDataSelectInputs input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerserviceregisterViewNew, CustomerserDataSelectInputs>(companyKey: companyKey, procedureName: "ProCustomerserviceregisterSelectCustomer", parameter: input);
        }
    }
}