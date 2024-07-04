using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class StateModel
    {

        public class StatesView
        {

            public long SlNo { get; set; }
            public long StateID { get; set; }

            
            public string State { get; set; }
            
            public string ShortName { get; set; }

            
            public int GSTINCode  { get; set; }

          
           
            public long CountryID  { get; set; }

            public string  Country { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }

        }


        public class StatesInputFromViewModel
        {

            //[Required(ErrorMessage = "No State Selected")]
            public long StateID { get; set; }

           // [Required(ErrorMessage = "Please Enter State Name")]
            public string State { get; set; }
            //[Required(ErrorMessage = "Please Enter Short Name For State ")]
            public string ShortName { get; set; }

           // [Required(ErrorMessage = "Please Enter GSTIN Code")]
            public int GSTINCode { get; set; }


          //  [Range(1, long.MaxValue, ErrorMessage = "Select Country")]
            public long CountryID { get; set; }

         
            public Int32 SortOrder { get; set; }
            public Int64 TotalCount { get; set; }

        }


        public class StateInfoView
        {
            [Required(ErrorMessage = "No State Selected")]
            public Int64 StateID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }


        public static string _updateProcedureName = "proStateUpdate";
        public class UpdateState
        {
     

                public long ID_States { get; set; }
                public long FK_Country { get; set; }
                //public long CountryID { get; set; }
                public string StName { get; set; }
                public string StShortName { get; set; }               
                public int StGSTINCode { get; set; }
                public string TransMode { get; set; }
                public Int32 SortOrder { get; set; } 
                public Int64 FK_Company { get; set; }
                public Int16 UserAction { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int64 FK_BranchCodeUser { get; set; }
             
              
                public string EntrBy { get; set; }
                public Int64 FK_States { get; set; }
               
                

        }

        public static string _selectProcedureName = "proStateSelect";
        public class GetState
        {
            public Int64 FK_States { get; set; }
            public string TransMode { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Country { get; set; }
            public string Name { get; set; }
        }
        public class SelectState
        {

            public long ID_States { get; set; }
            public long FK_Country { get; set; }
            //public long CountryID { get; set; }
            public string StName { get; set; }
            public string StShortName { get; set; }
            public int StGSTINCode { get; set; }

            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public string UserCode { get; set; }
            public Int32 BranchCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 BackupId { get; set; }
            public bool Cancelled { get; set; }


        }


        public static string _deleteProcedureName = "proStateDelete";
        public class DeleteState
        {
            public long FK_States { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }

            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Reason { get; set; }

        }

        public class Country
        {
            public int CountryID { get; set; }
            public string CountryName { get; set; }
            public long FK_Country { get; set; }

        }
        public class MenuDeatils
        {
            public string MnuLstName { get; set; }
        }
       

        public class StateListModel
        {
            public List<Country> CountryList { get; set; }
            public long CountryID { get; set; }
            public int SortOrder { get; set; } 

        }
        public class InputStateID
        {
            public Int64 ID_States { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }
        }


        public Output UpdateStateData(UpdateState input, string companyKey)
        {
            return Common.UpdateTableData<UpdateState>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteStateData(DeleteState input, string companyKey)
        {
            return Common.UpdateTableData<DeleteState>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public APIGetRecordsDynamic<StatesView> GetStateData(GetState input, string companyKey)
        {
            return Common.GetDataViaProcedure<StatesView, GetState>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}