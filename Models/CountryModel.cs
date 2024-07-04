using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class CountryModel
    {

        //show data in datatable grid/or click view button to get country data(with id)
        public class CountryView
        {
 
            public long SlNo { get; set; }

            [Required(ErrorMessage = "Please Enter Country Name")]
            
            public string CountryName { get; set; }

            [Required(ErrorMessage = "Please Enter Country Code")]
            public string CountryCode { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string CountryShortName { get; set; }

            public Int16 CountryID { get; set; }
            public int Sort { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public DateTime? EntrOn { get; set; }
            public Int64 TotalCount { get; set; }

            public string TransMode { get; set; }

        }

        //this model is used to insert data to Controller   add/updateajax--> add/update controller action
        public class CountryInputView
        {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Country Name")]

            public string CountryName { get; set; }

            [Required(ErrorMessage = "Please Enter Country Code")]
            public string CountryCode { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string CountryShortName { get; set; }

            public Int16 CountryID { get; set; }
            public int Sort { get; set; }
            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }

            public string TransMode { get; set; }

        }


        public static string _updateProcedureName = "proCountryUpdate";

        //this model is used to insert data to database(from common method-> stored procedure,so variable name should be same as stored procedure parameter name)
        public class CountryUpdate
        {
            public Int16 ID_Country { get; set; }
            public string CntryName { get; set; }
            public string CntryCode { get; set; }
            public string CntryShortName { get; set; }
            public int SortOrder { get; set; }

            public Int16 FK_Country { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }

            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }

        public class Locationlist
        {
               public int Sort { get; set; }
        }


        public static string _deleteProcedureName = "proCountryDelete";
        public class DeleteCountry
        {
            public Int64 FK_Country { get; set; }
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
            public Int64 FK_Country { get; set; }
            //public Int64 ID_Country { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser {     get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public string Name { get; set; }


        }

        public Output UpdateCountryData(CountryUpdate input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return   Common.UpdateTableData<CountryUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCountryData(DeleteCountry input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCountry>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CountryView>GetCountryData(ID input, string companyKey)
        {
            return Common.GetDataViaProcedure<CountryView, ID>(companyKey: companyKey, procedureName: "proCountrySelect", parameter: input);

        }

    }
}