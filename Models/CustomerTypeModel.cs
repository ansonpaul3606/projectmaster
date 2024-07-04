using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CustomerTypeModel
    {
        

        public class CustomerTypeView
        {
            public long SlNo { get; set; }
            public long CustomerTypeID { get; set; }
            [Required(ErrorMessage = "Please Enter  Name")]
            public string CustomerTypeName { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name")]
            public string ShortName { get; set; }
           // [Required(ErrorMessage = "Please Enter  Priority")]
            public Int32 Priority { get; set; }
           // [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Customer Category")]
            public long CategoryID { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Customer Sector")]
            public long SectorID { get; set; }

            [Required(ErrorMessage = "Please Select Reason")]
            [Range(1, long.MaxValue, ErrorMessage = "Select Reason")]
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
            public Boolean CustyDefault { get; set; }

    }

        public class UpdateCustomerType
        {
            public long ID_CustomerType { get; set; }
            public string CustyName { get; set; }
            public string CustyShortName { get; set; }
            public Int32 CusPriority { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_CustomerCategory { get; set; }
            public long FK_CustomerSector { get; set; }
            public long FK_CustomerType { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public Boolean CustyDefault { get; set; }

        }

        public class CustomerTypeFormData
        {

            public List<Sector> SectorList { get; set; }
            public List<Categorys> CategoryList { get; set; }
            public int SortOrder { get; set; }
        }
        public class Sector
        {
            public int SectorID { get; set; }
            public string SectorName { get; set; }

        }
        public class Categorys
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }

        }
        //public class CustomerTypeFormViewModel
        //{

        //    //[Required(ErrorMessage = "Please enter Customer Short Name")]
        //    //[StringLength(10, ErrorMessage = "Maximum 10 characture")]
        //    public string ShortName { get; set; }
        //    public string CustomerTypeName { get; set; }
        //    public Int16 Priority { get; set; }
        //    public Int16 SortOrder { get; set; }
        //    public Int16 SectorID { get; set; }
        //    public Int16 CategoryID { get; set; }
        //    public Int64 CustomerTypeID { get; set; }
        //    public long ReasonID { get; set; }

        //}

        public class CustomerTypeInfoView
        {
            [Required(ErrorMessage = "Please select a Customer Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a customer Type")]
            public Int64 CustomerTypeID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }

        public static string _selectProcedureName = "ProCustomerTypeSelect";
        public class GetCustomerType
        {
            public Int64 ID_CustomerType { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }

        public static string _updateProcedureName = "proCustomerTypeUpdate";
        //public class UpdateCustomerType
        //{
        //    //public Int64 ID_CustomerType { get; set; }
        //    public Int64 ID_CustomerType { get; set; }
        //    public int UserAction { get; set; }
        //    public Int64 FK_BranchCodeUser { get; set; }
        //    public Int64 FK_Company { get; set; }
        //    public Int16 SortOrder { get; set; }
        //    public string EntrBy { get; set; }
        //    public Int64 FK_Machine { get; set; }
        //    public string ShortName { get; set; }
        //    public string CustomerType { get; set; }
        //    public Int16 CusPriority { get; set; }
        //    public Int16 FK_CustomerSector { get; set; }
        //    public Int16 FK_CustomerCategory { get; set; }


        //}

        public static string _deleteProcedureName = "proCustomerTypeDelete";
        public class DeleteCustomerType
        {
            public Int64 ID_CustomerType { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class ID
        {
            public Int64 ID_CustomerType { get; set; }
            public long ReasonID { get; set; }
        }

        public Output UpdateCustomerTypeData(UpdateCustomerType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCustomerTypeData(DeleteCustomerType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustomerType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CustomerTypeView> GetCustomerTypetData(GetCustomerType input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerTypeView, GetCustomerType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

    }
}