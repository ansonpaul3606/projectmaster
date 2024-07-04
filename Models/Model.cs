//@*----------------------------------------------------------------------
//Created By  : AthulM
//Created On	: 20/01/2022
//Purpose		: Model
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------*@
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class Model
    {
        public class ModelView
        {
           // [Required(ErrorMessage = "Please select Extra Work Type")]
            //[Range(1, long.MaxValue, ErrorMessage = "Please select Extra Work Type")]
            public int ID_Model { get; set; }


          //  [Required(ErrorMessage = "Please enter Model Name")]
          //  [StringLength(150, ErrorMessage = "Maximum 150 characture")]
            public string ModelName { get; set; }
           // [StringLength(10, ErrorMessage = "Maximum 10 characture")]
          //  [Required(ErrorMessage = "Please Enter Model Short Name")]
            public string ShortName { get; set; }
           
            public Int32 SortOrder { get; set; }
            //[Required(ErrorMessage = "Please Select a Manufacturer")]
            //[Range(1, long.MaxValue, ErrorMessage = "Please select a Manufacturer")]
            public string Manufacturer { get; set; }
            public long FK_Manufacturer { get; set; }

           // [Required(ErrorMessage = "Please Select a Mode")]
           // [Range(1, long.MaxValue, ErrorMessage = "Please select a Mode")]
            public string Mode { get; set; }
            public string ModuleName { get; set; }

        }
        public class UpdateModel
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Model { get; set; }
            public string ModelName { get; set; }
            public string ModelShortName { get; set; }
            public string Mode { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Manufacturer { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public Int64 FK_Model { get; set; }

        }
        public static string _deleteProcedureName = "proModelDelete";
        public static string _updateProcedureName = "ProModelUpdate";
        public static string _selectProcedureName = "proModelSelect";


        public class DeleteView
        {
            public long ID_Model { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }



        public class DeleteModel
        {
            public string TransMode { get; set; }
            public Int64 FK_Model { get; set; }
            public int Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
        }

        public class ModeList
        {
            public int ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public int FK_ModuleType { get; set; }
        }
        public class ModelList
        {
            public List<Manufacturer> ManufacturerList { get; set; }
            public List<ModeList> modeList { get; set; }
            public int SortOrder { get; set; }
        }

        public class GetModelList
        {
            public Int64 ID_Model { get; set; }
            public string UserCode { get; set; }
            public Int64 FK_Company { get; set; }

        }
        public class ModelID
        {
            public Int64 FK_Model{ get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
        }
        public Output UpdateModelData(UpdateModel input, string companyKey)
        {
            return Common.UpdateTableData<UpdateModel>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteModelData(DeleteModel input, string companyKey)
        {
            return Common.UpdateTableData<DeleteModel>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ModelView> GeModelData(ModelID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModelView, ModelID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}