/*----------------------------------------------------------------------
Created By	: Amritha AK
Created On	: 09/02/2022
Purpose		: ServiceMapping
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

namespace PerfectWebERP.Models
{
    public class ServiceMappingModel
    {

        public class ServiceMappingView
        {
            public long SlNo { get; set; }
            public long ServiceMappingID { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            public long EmployeeID { get; set; }
            public string Employee { get; set; }
            public bool SMActive { get; set; }
           // [Required(ErrorMessage = "Please select Product and Priority")]
            public List<SubServiceMappingDetailsView>Subservicemapping { get; set; }
            public Int64 TotalCount { get; set; }
        }
        //sub table
        public class SubServiceMappingDetailsView
        {
            public long ID_ServicemapProduct { get; set; }
           // [Required(ErrorMessage = "Please Enter Priority")]
            public Int32 Priority { get; set; }
           // [Range(1, long.MaxValue, ErrorMessage = "Select Product")]
            public long ProductID { get; set; }
            public string ProductName { get; set; }
            //[Range(1, long.MaxValue, ErrorMessage = "Select Service Mapping")]
            //public long ServiceMapping { get; set; }

        }
        public class Product
        {
            public Int32 ProductID { get; set; }
            public string ProductName { get; set; }
        }
        public class Employee
        {
            public Int32 EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }
        public class EmployeeBaseInfo
        {
            public Int32 EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public string Number { get; set; }
            public string EmployeeDesignation { get; set; }
            public string EmployeeDepartment { get; set; }
        }
        public class ServiceMappingViewList
        {
            public List<Product> ProductList { get; set; }
            public List<Employee> EmployeeList { get; set; }
           
        }



        public class UpdateServiceMapping
        {
            public long ID_ServiceMapping { get; set; }
            public long FK_Employee { get; set; }
            public bool Active { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_ServiceMapping { get; set; }

            public string SubServiceMappingDetails { get; set; }


        }
        public static string _deleteProcedureName = "ProservicemappingDelete";
        public static string _updateProcedureName = "ProServiceMappingUpdate";
        public static string _selectProcedureName = "ProservicemappingSelect";
        public static string _selectSubtableServicemapping = "ProServicemappingsubtabledataSelect";

        public class DeleteServiceMapping
        {
            public long ID_ServiceMapping { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }

        public class ServiceMappingInfoView
        {
            public long ID_ServiceMapping { get; set; }
        //    public long ID_ServiceMappingDetails { get; set; }

            public long ReasonID { get; set; }
        }

        public class ServiceMappingID
        {
            public Int64 ID_ServiceMapping { get; set; }
           public Int64  FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }

        public Output UpdateServiceMappingData(UpdateServiceMapping input, string companyKey)
        {
            return Common.UpdateTableData<UpdateServiceMapping>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteServiceMappingData(DeleteServiceMapping input, string companyKey)
        {
            return Common.UpdateTableData<DeleteServiceMapping>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ServiceMappingView> GetServiceMappingData(ServiceMappingID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ServiceMappingView, ServiceMappingID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public class ServiceMappigSubSelect
        {
            public Int64 ID_ServiceMapping { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }


        }
        public APIGetRecordsDynamic<SubServiceMappingDetailsView> GetSubServiceMappingData(ServiceMappigSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubServiceMappingDetailsView, ServiceMappigSubSelect>(companyKey: companyKey, procedureName: _selectSubtableServicemapping, parameter: input);

        }

    }

}

