using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;


namespace PerfectWebERP.Models
{
 public class DistrictModel
 {

        //view models
        public class DistrictView
        {
            public long SlNo { get; set; }
            public long ID { get; set; }       
            public string DistrictName { get; set; }
          
            public string DistrictShortName { get; set; }
      
            public Int32 SortOrder { get; set; } 
       
            public long StatesID { get; set; }

            public string States { get; set; }

            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }

        public class DistrictInputView
        {
            public long SlNo { get; set; }
            // [Required(ErrorMessage = "No District Selected")]
            public long ID { get; set; }

            [Required(ErrorMessage = "Please Enter District Name")]
            public string DistrictName { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name  ")]
            public string DistrictShortName { get; set; }

            //[Required(ErrorMessage = "Please Enter SortOrder")]
            public Int32 SortOrder { get; set; }

            //[Required(ErrorMessage = "Please Select From Country")]

            [Range(1, long.MaxValue, ErrorMessage = "Select State")]
            //public long FK_States { get; set; }
            public long StatesID { get; set; }
            public string States { get; set; }

            [Required(ErrorMessage = "Please Select Reason")]
            [Range(1, long.MaxValue, ErrorMessage = "Select Reason")]
            public long ReasonID { get; set; }

            public string TransMode { get; set; }

        }

        public static string _updateProcedureName = "proDistrictUpdate";
        // Db values set
        public class UpdateDistrict
        {
          public long ID_District { get; set; }
            public string DtName { get; set; }
            public string DtShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_States { get; set; }

            public long FK_District { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }

        

        public static string _deleteProcedureName = "proDistrictDelete";
        public class DeleteDistrict
        {
            public Int64 ID_District { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class States
    {
        public int StatesID { get; set; }
        public string StatesName { get; set; }
        public long FK_States { get; set; }
      
    }


    public class InputDistrictID
    {
           public Int64 ID_District { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public string Name { get; set; }

        }

        public class APIOutputStatesList
    {
        public List<States> dtable { get; set; }
    }

        public class DistrictViewList
    {
        public List<States> StatesList { get; set; }
            public long StatesID { get; set; }

            public Int32 SortOrder { get; set; }
        }
        public class Drictict_ID
        {
            public int ID { get; set; }
            public int StatesID { get; set; }
        }
        public Output UpdateDData(UpdateDistrict input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return Common.UpdateTableData<UpdateDistrict>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        
        public Output DeleteDistrictData(DeleteDistrict input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDistrict>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public static string _selectProcedureName = "proDistrictSelect";
        public APIGetRecordsDynamic<DistrictView> GetDistrictData(InputDistrictID input, string companyKey)
        {
            return Common.GetDataViaProcedure<DistrictView, InputDistrictID>( companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

       
}
}