using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PerfectWebERP.Models
{
    public class ProcessLevelModel
    {

        public class ProcessLevelView
        {
            public long SlNo { get; set; }
            public Int64 ProcessLevelID { get; set; }
            public Int64 FK_ProcessLevel { get; set; }
            public string ProcessLevel { get; set; }
            public string ShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public long ReasonID { get; set; }
            //public string ProLevelName { get; set; }

            public string TransMode { get; set; }


        }
        public class ProcessLevelFormData
        {


            public int SortOrder { get; set; }
        }
        public class ProcessLevelInfoView
        {
            [Required(ErrorMessage = "Please select a Process Level")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Process Level")]
            public Int64 ID_ProcessLevel { get; set; }

        }

        public static string _selectProcedureName = "ProProcessLevelSelect";
        public class GetProcessLevel
        {
            public Int64 FK_ProcessLevel { get; set; }
            //public Int64 ID_ProcessLevel { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }

        public static string _updateProcedureName = "proProcessLevelUpdate";
        public class UpdateProcessLevel
        {
            public Int64 ID_ProcessLevel { get; set; }
            public int UserAction { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 BackupID { get; set; }
            public Int64 Fk_ProcessLevel { get; set; }
            public string ShortName { get; set; }
            public string ProLevelName { get; set; }
            public string TransMode { get; set; }



        }

        public static string _deleteProcedureName = "proProcessLevelDelete";
        public class DeleteProcessLevel
        {
            //public Int64 ID_ProcessLevel { get; set; }
            //public string CancelBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_ProcessLevel { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }

        public Output UpdateProcessLevelData(UpdateProcessLevel input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProcessLevel>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeleteProcessLevelData(DeleteProcessLevel input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProcessLevel>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<ProcessLevelView> GetProcessLeveltData(GetProcessLevel input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProcessLevelView, GetProcessLevel>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }



    }
}