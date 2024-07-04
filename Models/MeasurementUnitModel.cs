using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class MeasurementUnitModel
    {

        public class MeasurementUnitsView
        {
            public long SlNo { get; set; }
            public long MeasurementUnitID { get; set; }

            public string MeasurementUnit { get; set; }

            public string ShortName { get; set; }

            public string Description { get; set; }


            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }
        }

        public class MeasurementUnitsInputFromViewModel
        {

            [Required(ErrorMessage = "No MeasurementUnit Selected")]
            public long MeasurementUnitID { get; set; }

            [Required(ErrorMessage = "Please Enter MeasurementUnit Name")]
            public string MeasurementUnit { get; set; }
            //[Required(ErrorMessage = "Please Enter Short Name ")]
            public string ShortName { get; set; }
            public string Description { get; set; }
            public Int32 SortOrder { get; set; }

            public string TransMode { get; set; }
        }

        public class MeasurementUnitInfoView
        {
            [Required(ErrorMessage = "No MeasurementUnit Selected")]
            public Int64 MeasurementUnitID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }


        public static string _updateProcedureName = "proMeasurementUnitUpdate";
        public class UpdateMeasurementUnit
        {
            public long FK_MeasurementUnit { get; set; }
            public long ID_MeasurementUnit { get; set; }
            public string MUName { get; set; }
            public string MUShortName { get; set; }
            public string MUDescription { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }



        }

        public class SelectMeasurementUnit
        {

            public long FK_MeasurementUnit { get; set; }
            public string MUName { get; set; }
            public string MUShortName { get; set; }
            public string MUDescription { get; set; }
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

        public static string _deleteProcedureName = "proMeasurementUnitDelete";
        public class DeleteMeasurementUnit
        {
            public Int64 FK_MeasurementUnit { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }


        public class MeasurementUnitListModel
        {
            public int SortOrder { get; set; }

        }
        public class InputMeasurementUnitID
        {
            public Int64 FK_MeasurementUnit { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }


        public Output UpdateMeasurementUnitData(UpdateMeasurementUnit input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMeasurementUnit>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMeasurementUnitData(DeleteMeasurementUnit input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMeasurementUnit>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proMeasurementUnitSelect";
        public APIGetRecordsDynamic<MeasurementUnitsView> GetMeasurementUnitData(InputMeasurementUnitID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MeasurementUnitsView, InputMeasurementUnitID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}