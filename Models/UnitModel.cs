

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class UnitModel
    {
        public class UnitView
        {
            public long SlNo { get; set; }
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public int UnitID { get; set; }
            public string UnitName { get; set; }
            public string Name { get; set; }
            public string UnitShortName { get; set; }
            public string ShortName { get; set; }
            //public decimal NoOfUnits { get; set; }
           
            public string Mode { get; set; }
            public string ModuleName { get; set; }
            public decimal NoOfUnit { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }

            public DateTime EntrOn { get; set; }

            public string UserCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 TotalCount { get; set; }

            

        }

        public class UnitInputView
            {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please select a Unit")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Unit")]
            public int UnitID { get; set; }
            [Required(ErrorMessage = "Please Enter Unit Name")]
            [StringLength(150, ErrorMessage = "Maximum 150 character")]
            public string UnitName { get; set; }
            [Required(ErrorMessage = "Please Enter Unit Short Name")]
            [StringLength(10, ErrorMessage = "Maximum 10 character")]
            public string UnitShortName { get; set; }
           
           
            public Int16 SortOrder { get; set; }
            [Required(ErrorMessage = "Please Select a Mode")]
            //[Range(1, long.MaxValue, ErrorMessage = "Please select a Mode")]
            public string Mode { get; set; }

            [Required(ErrorMessage = "Please Enter Number of Unit")]
            public decimal NoOfUnit { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }

            public Int64 TotalCount { get; set; }
        }

        public static string _updateProcedureName = "proUnitUpdate";

        public class UpdateUnit
        {
            public byte UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public int FK_Unit { get; set; }
            public long ID_Unit { get; set; }
            public string Mode { get; set; }
            public string UnitName { get; set; }
            public string UnitShortName { get; set; }
            public decimal NoOfUnits { get; set; }
           
            public Int16 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int16 FK_Machine { get; set; }
            public Int64 BackupId { get; set; }
         


        }
        public class ModeList
        {
            public List<_Mode> modeList { get; set; }
            public int SortOrder { get; set; }
        }

        public class Unit_ID
        {
            public Int16 UnitID { get; set; }

        }

        public class DeleteView
        {

            public long UnitID { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }




        public class DeleteUnit //commonprocedure
        {
            public string TransMode { get; set; }
            public Int64 FK_Unit { get; set; }
            public Int64 FK_Reason { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }


        public static string _deleteProcedureName = "proUnitDelete";

        public class GetUnitList
        {
            public Int64 FK_Unit { get; set; }
          
          
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public string Name { get; set; }

        }
        public static string _selectProcedureName = "proUnitSelect";

        public Output UpdateUnitData(UpdateUnit input, string companyKey)
        {
            //// UpdateCustomer table = this.ConvertToUpdateCustomer(input);
            return Common.UpdateTableData<UpdateUnit>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }


        public Output DeleteUnitData(DeleteUnit input, string companyKey)
        {
            return Common.UpdateTableData<DeleteUnit>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<UnitView> GetUnitData(GetUnitList input, string companyKey)
        {
            return Common.GetDataViaProcedure<UnitView, GetUnitList>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);


        }

    }
}