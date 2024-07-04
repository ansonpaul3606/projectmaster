using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class WalkingCustomerModel
    {
        public class WalkingCustomerView
        {
            public long SlNo { get; set; }
            public long ID_WalkingCustomer { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public long FK_AssignedTo { get; set; }
            public string AssignedTo { get; set; }
            public string Description { get; set; }
            public DateTime AssignedDate { get; set; }
            public Int32 TotalCount { get; set; }

            public long ID_CustomerAssignment { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public long FK_Employee { get; set; }
            public DateTime CaAssignedDate { get; set; }
            public List<leadByMobileNo> leadByMobileNoItems { get; set; }
            public long ?LastID { get; set; }
        }
        public class WalkingCustomerPartialModel
        {
            public long ID_Users { get; set; }
            public long? LastID { get; set; }
        }

        public class leadByMobileNo
        {
            public DateTime FollowUpDate { get; set; }
            //public long SlNo { get; set; }
            public long FK_Employee { get; set; }
            public long ID_LeadGenerateProduct { get; set; }

            public string Customer { get; set; }
            public string Mobile { get; set; }
            public string Product { get; set; }
            public string Action { get; set; }
            public string AssignedTo { get; set; }
            public long? LastID { get; set; }
        }
        public class WalkingCustomerViewList
        {
            public long SlNo { get; set; }           
            public long ID_CustomerAssignment { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public long FK_Employee { get; set; }
            public string Employee { get; set; }
            public string CaDescription { get; set; }
            public DateTime CaAssignedDate { get; set; }
            public Int32 TotalCount { get; set; }
            public long? LastID { get; set; }
        }
        public class UpdateWalkingCustomer
        {
            public byte UserAction { get; set; }
            public byte Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_CustomerAssignment { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public DateTime CaAssignedDate { get; set; }
            public long FK_Employee { get; set; }
            public string CaDescription { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string leadByMobileNo { get; set; }
            public long? LastID { get; set; }
        }
        public class GetWalkingCustomer
        {
            public Int64 FK_CustomerAssignment { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }
        public class GetWalkingCustomerById
        {
            public Int64 FK_CustomerAssignment { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }            
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class DeleteWalkingCustomer
        {
            public string TransMode { get; set; }
            public Int64 FK_CustomerAssignment { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }
        public class MobileInput
        {
            public long FK_Company { get; set; }
            public string MobileNo { get; set; }
        }
        public class MobileOutput
        {
            public string Customer { get; set; }
            public string Mobile { get; set; }
            public string Product { get; set; }
            public string Action { get; set; }
            public string AssignedTo { get; set; }
            public long ID_LeadGenerateProduct { get; set; }
            public DateTime FollowUpDate { get; set; }
            public long FK_Employee { get; set; }
            public long ID_Users { get; set; }
            public string LeadNo { get; set; }
        }
        public class DeleteView
        {
            public long ID_WalkingCustomer { get; set; }
            public Int64 ReasonID { get; set; }
        }
        public Output UpdateWalkingCustomerData(UpdateWalkingCustomer input, string companyKey)
        {
            return Common.UpdateTableData<UpdateWalkingCustomer>(parameter: input, companyKey: companyKey, procedureName: "ProCustomerAssignmentUpdate");
        }
        public APIGetRecordsDynamic<WalkingCustomerViewList> GetWalkingCustomerData(GetWalkingCustomer input, string companyKey)
        {
            return Common.GetDataViaProcedure<WalkingCustomerViewList, GetWalkingCustomer>(companyKey: companyKey, procedureName: "ProCustomerAssignmentSelect", parameter: input);
        }
        public APIGetRecordsDynamic<WalkingCustomerViewList> GetWalkingCustomerDataByID(GetWalkingCustomerById input, string companyKey)
        {
            return Common.GetDataViaProcedure<WalkingCustomerViewList, GetWalkingCustomerById>(companyKey: companyKey, procedureName: "ProCustomerAssignmentSelect", parameter: input);
        }
        public Output DeleteWalkingCustomerData(DeleteWalkingCustomer input, string companyKey)
        {
            return Common.UpdateTableData<DeleteWalkingCustomer>(parameter: input, companyKey: companyKey, procedureName: "ProCustomerAssignmentDelete");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="companyKey"></param>
        /// <returns></returns>
   
        public APIGetRecordsDynamic<MobileOutput> GetmobileData(MobileInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<MobileOutput, MobileInput>(companyKey: companyKey, procedureName: "ProGetLeadByMobileNo", parameter: input);
        }
    }
}