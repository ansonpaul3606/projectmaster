using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PerfectWebERP.Models
{
    public class DesignationModel
    {

        public class DesignationView
        {
            public long SlNo { get; set; }
            public Int64 DesignationID { get; set; }
            public string  Designation{ get; set; }
            public string ShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public Int16 EmployeeLevelID { get; set; }
            public string EmployeeLevel { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public class DesignationFormData
        {

            public List<EmployeeLevel> EmployeeLevelList { get; set; }
            public int SortOrder { get; set; }
        }

        public class EmployeeLevel
        {
            public int EmployeeLevelID { get; set; }
            public string EmployeeLevelName { get; set; }

        }

        public class DesignationInfoView
        {
            [Required(ErrorMessage = "Please select a Designation")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Designation")]
            public Int64 ID_Designation { get; set; }

        }



        public static string _selectProcedureName = "ProDesignationSelect";
        public class GetDesignation
        {
            public Int64 ID_Designation { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }

      


        public static string _updateProcedureName = "proDesignationUpdate";
        public class UpdateDesignation
        {
            public Int64 ID_Designation { get; set; }
            public Int64 FK_Designation { get; set; }
            public int UserAction { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 BackupID { get; set; }
            public string DesShortName { get; set; }
            public string DesName { get; set; }
            public string TransMode { get; set; }
            public Int16 FK_EmployeeLevel { get; set; }

        }

        public static string _deleteProcedureName = "proDesignationDelete";
        public class DeleteDesignation
        {
            //public Int64 ID_Designation { get; set; }
            public Int64 FK_Designation { get; set; }
            //public string CancelBy { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }


        public Output UpdateDesignationData(UpdateDesignation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateDesignation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteDesignationData(DeleteDesignation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDesignation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<DesignationView> GetDesignationtData(GetDesignation input, string companyKey)
        {
            return Common.GetDataViaProcedure<DesignationView, GetDesignation>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}