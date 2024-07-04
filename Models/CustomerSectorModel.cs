using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class CustomerSectorModel
    {
        public class CustomerSectorView
        {
            public long SlNo { get; set; }
            public long CustomerSectorID { get; set; }
            public string Name { get; set; }

            public string ShortName { get; set; }

            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }

        }


        public class CustomerSectorInputView
        {

           // [Required(ErrorMessage = "No Customer Sector Selected")]
            public long CustomerSectorID { get; set; }

            [Required(ErrorMessage = "Please Enter CustomerSector Name")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name  ")]
            public string ShortName { get; set; }
            public string TransMode { get; set; }
            //[Required(ErrorMessage = "Please Enter SortOrder")]
            public Int32 SortOrder { get; set; }
            [Required(ErrorMessage = "Please Select Reason")]
            [Range(1, long.MaxValue, ErrorMessage = "Select Reason")]
            public long ReasonID { get; set; }
          //  public long TotalCount {get; set; }


        }

        public static string _updateProcedureName = "proCustomersectorUpdate";
        // Db values set
        public class UpdateCustomerSector
        {
            public long ID_CustomerSector { get; set; }

            public long FK_CustomerSector { get; set; }
            public string CussectyName { get; set; }
            public string CussectyShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }


        public static string _deleteProcedureName = "proCustomerSectorDelete";
        public class DeleteCustomerSector
        {
            public Int64 ID_CustomerSector { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }


        public class InputCustomerSectorID
        {
            public Int64 ID_CustomerSector { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }


        }



        public class Sortorderlist
        {
          public Int32 SortOrder { get; set; }
        }
        public Output UpdateCustomerSectorData(UpdateCustomerSector input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return Common.UpdateTableData<UpdateCustomerSector>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }


        public Output DeleteCustomerSectorData(DeleteCustomerSector input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustomerSector>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public static string _selectProcedureName = "proCustomerSectorSelect";
        public APIGetRecordsDynamic<CustomerSectorView> GetCustomerSectorData(InputCustomerSectorID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerSectorView, InputCustomerSectorID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }


    }
}