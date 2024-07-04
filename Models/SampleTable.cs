using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SampleTable
    {
        public int id { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        
        

    }

    public class sampleinput {
        public SampleTable newValue { get; set; }
        public SampleTable oldValue { get; set; }
    }

    //public class CustomerDetails
    //{
    //    public string CustomerTitle { get; set; }
    //    public string CustomerName { get; set; }
    //    public DateTime CustomerDate { get; set; }
    //    public string Place { get; set; }
    //    public string District { get; set; }
    //    public string State { get; set; }
    //    public string Phone { get; set; }
    //    public string Mobile { get; set; }
    //    public DateTime DateOfBirth { get; set; }
    //} 


    //public class CustomerInputData
    //{
    //    public CustomerDetails NewCustomerData { get; set; }
    //    public CustomerDetails OldCustomerData { get; set; }
    //}

    public class SampleOutput
    {
        public bool IsProcess { get; set; }
        public List<string> Message { get; set; }
        public string Status { get; set; }
        public dynamic Data { get; set; }
    }

    public class Customer
    {
        [Required( ErrorMessage ="No customer selected")]
        public int? CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Title")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please Select Customer Title")]
        public string CustomerTitle { get; set; }
        public int CustomerTitleID { get; set; }
        [Required(ErrorMessage = "Please Select Customer date")]
        public DateTime CustomerDate { get; set; }
        [Required(ErrorMessage = "Please Select a place")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Please Select a place")]
        public int PlaceID { get; set; }
        [Required(ErrorMessage = "Please Select a district")]
        public string District { get; set; }
        public int DistrictID { get; set; }
        [Required(ErrorMessage = "Please Select state")]
        public string State { get; set; }
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please enter phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please Select Date of birth")]
        public DateTime DateOfBirth { get; set; }
       
    }
    public class UpdateCustomer
    {
        [Required(ErrorMessage = "Need old")]
        public Customer NewData { get; set; }
        [Required(ErrorMessage = "Need current")]
        public Customer CurrentData { get; set; }
    }

    public class District
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
    }
    public class State
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }

    public class Manufacturer
    {
        public long FK_Manufacturer { get; set; }
        public string ManufacturerName { get; set; }

    }
    public class Place
    {
        public int PlaceID { get; set; }
        public string PlaceName { get; set; }
    }
    public class CustomerTitle
    {
        public int CustomerTitleID { get; set; }
        public string CustomerTitleName { get; set; }
    }
    public class AccountName
    {
        //public int AccountHeadID { get; set; }
        public string AccoundHead { get; set; }
    }
    public class APIOutputAccountHeadList
    {
        public List<AccountName> dtable { get; set; }

    }

    public class _Mode
    {
        public int ModeID { get; set; }
        public string ModeName { get; set; }
        public string Mode { get; set; }
        public int FK_ModuleType { get; set; }
    }

    public class APIOutputManufacturerlList
    {
        public List<Manufacturer> dtable { get; set; }

    }


    public class APIOutputModelList
    {
        public List<_Mode> dtable { get; set; }

    }
    public class APIOutputDistrictList
    {
        public List<District> dtable { get; set; }
       
    }



    public class APIOutputStateList
    {
        public List<State> dtable { get; set; }
    }
    public class APIOutputCustomerType
    {
        public List<CustomerTitle> dtable { get; set; }
    }
    public class APIOutputCustomerList
    {
        public List<Customer> dtable { get; set; }
    }

    public class InputCustomer
    {
        public Int16 ID_Customer { get; set; }
    }

    public class sclarerror
    {
        public string ReturnValue { get; set; }
    }


    public class TableColumnBuilder
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public string ColumnValue { get; set; }
    }


    //login info


    //this is a temporary thing  it spefift class corresponding to database table
    public class Tabless
    {
        public class CustomerSample
        {
            public Int64 FK_Company { get; set; }
            public Int64 BranchCode { get; set; }
            public byte UserAction { get; set; }
            public Int16 FK_Machine { get; set; }
            public Int16 ID_Customer { get; set; }
            public string CusTitle { get; set; }
            public string CusName { get; set; }
            public Int64 FK_Place { get; set; }
            public Int64 FK_District { get; set; }
            public Int64 FK_State { get; set; }
            public string CusPhone { get; set; }
            public string cusMobile { get; set; }
            public DateTime CusDOB { get; set; }
            public string UserCode { get; set; }
            public DateTime CusDate { get; set; }
            public Int64 BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
        }


    }
}

