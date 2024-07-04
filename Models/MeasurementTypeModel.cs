using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MeasurementTypeModel
    {
        public class MeasurementTypesView
        {
            public long SlNo { get; set; }
            public long MeasurementTypeID { get; set; }

            public string MeasurementType { get; set; }

            public string ShortName { get; set; }

            public string Description { get; set; }


            public Int32 SortOrder { get; set; }

        }

        public class MeasurementTypesInputFromViewModel
        {

            [Required(ErrorMessage = "No MeasurementType Selected")]
            public long MeasurementTypeID { get; set; }

            [Required(ErrorMessage = "Please Enter MeasurementType Name")]
            public string MeasurementType { get; set; }
         
            public string ShortName { get; set; }
            public string Description { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

        }

        public class MeasurementTypeInfoView
        {
            [Required(ErrorMessage = "No MeasurementType Selected")]
            public Int64 MeasurementTypeID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }


        public static string _updateProcedureName = "proMeasurementTypeUpdate";
        public class UpdateMeasurementType
        {
            public long FK_MeasurementType { get; set; }
            public long ID_MeasurementType { get; set; }
            public string MTName { get; set; }
            public string MTShortName { get; set; }
            public string MTDescription { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }


        }


        public static string _deleteProcedureName = "proMeasurementTypeDelete";
        public class DeleteMeasurementType
        {
            public Int64 FK_MeasurementType { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }


        public class MeasurementTypeListModel
        {
            public int SortOrder { get; set; }

        }
        public class InputMeasurementTypeID
        {
            public Int64 FK_MeasurementType { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }


        public Output UpdateMeasurementTypeData(UpdateMeasurementType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMeasurementType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMeasurementTypeData(DeleteMeasurementType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMeasurementType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proMeasurementTypeSelect";
        public APIGetRecordsDynamic<MeasurementTypesView> GetMeasurementTypeData(InputMeasurementTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<MeasurementTypesView, InputMeasurementTypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}