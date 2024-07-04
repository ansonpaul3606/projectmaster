using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class CustomerCategoryModel
    {
         
        public class CustomerCategoryViewModel
        {
            public long SlNo { get; set; }
          //  [Required(ErrorMessage = "Please Enter Customer Category Name")]

            public string CustomerCategoryName { get; set; }
             
          //  [Required(ErrorMessage = "Please Enter Short Name ")]
            public string CustomerCategoryShortName { get; set; }

            public Int16 CustomerCategoryID { get; set; }
            public int SortOrder { get; set; }
            public bool CustomerCategoryIndividual { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }

            //public Int64 FK_Company { get; set; }
            //public Int64 FK_BranchCodeUser { get; set; }
            //public string EntrBy { get; set; }
            //public DateTime? EntrOn { get; set; }
            //public Int64 FK_Machine { get; set; }

            //public Int64 BackupId { get; set; }
        }
        public class Sortorderlist
        {
            public Int32 SortOrder { get; set; }
        }

        public static string _updateProcedureName = "proCustomerCategoryUpdate";
        public class UpdateCustomerCategory
        {
            public long ID_CustomerCategory { get; set; }
            public long FK_CustomerCategory { get; set; }
            public string CuscattyName { get; set; }
            public string CuscattyShortName { get; set; }

            public Int32 SortOrder { get; set; }
            public bool CusCatIndividual { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
        }


        //public class CustomerCategoryInfoView
        //{
        //    [Required(ErrorMessage = "Please select a Customer")]
        //    [Range(1, long.MaxValue, ErrorMessage = "Please select a customer")]
        //    public Int64 CustomerCategoryID { get; set; }
        //    [Required(ErrorMessage = "Please select the reason")]
        //    public Int64 ReasonID { get; set; }

        //}

        public class CustomerCategoryInfoView
        {
            public long ID_CustomerCategory { get; set; }

            public long ReasonID { get; set; }
        }


        public static string _deleteProcedureName = "proCustomerCategoryDelete";
        public class DeleteCustomerCategory
        {
            public Int64 ID_CustomerCategory { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }

            public long FK_BranchCodeUser { get; set; }
        }

        public static string _selectProcedureName = "ProCustomerCategorySelect";
        public class GetCustomerCategory
        {
            public Int64 ID_CustomerCategory { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public string Name { get; set; }


        }


        public Output UpdateCustomerCategoryData(UpdateCustomerCategory input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerCategory>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeleteCustomerCategoryData(DeleteCustomerCategory input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustomerCategory>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<CustomerCategoryViewModel> GetCustomerCategoryData(GetCustomerCategory input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerCategoryViewModel, GetCustomerCategory>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}