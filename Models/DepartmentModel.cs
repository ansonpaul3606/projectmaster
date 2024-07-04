using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class DepartmentModel
    {
        public class DepartmentVM
        {
            public int SortOrder { get; set; }
            public IEnumerable<DepartmentMode> Mode { get; set; }
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class DepartmentMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class DepartmentViewModel
        {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Department Name")]

            public string DepartmentName { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string DepartmentShortName { get; set; }

            public Int16 DepartmentID { get; set; }
            public int SortOrder { get; set; }

            public Int64 ReasonID { get; set; }

            public string TransMode { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public Int64 TotalCount { get; set; }
            public long FK_DeptMode { get; set; }
            public string ModeName { get; set; }
        }

        public static string _updateProcedureName = "proDepartmentUpdate";
        public class UpdateDepartment
        {
            public long ID_Department { get; set; }
            public string DeptName { get; set; }
            public string DeptShortName { get; set; }

            public Int32 SortOrder { get; set; }
            public long FK_Department { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public long FK_DeptMode { get; set; }

        }


        public class DepartmentInfoView
        {
            [Required(ErrorMessage = "Please select a Department")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a department")]
            public Int64 DepartmentID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }

        public static string _deleteProcedureName = "proDepartmentDelete";
        public class DeleteDepartment
        {
            public Int64 FK_Department { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public static string _selectProcedureName = "proDepartmentSelect";
        public class GetDepartment
        {
            public Int64 FK_Department { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public class GetDefaultDepartment
        {
            public long Mode { get; set; }
            public long FK_Company { get; set; }
        }
        public class SetDefaultDepartment
        {
            public long ID_Department { get; set; }
        }
        public Output UpdateDepartmentData(UpdateDepartment input, string companyKey)
        {
            return Common.UpdateTableData<UpdateDepartment>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeleteDepartmentData(DeleteDepartment input, string companyKey)
        {
            return Common.UpdateTableData<DeleteDepartment>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<DepartmentViewModel> GetDepartmentData(GetDepartment input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepartmentViewModel, GetDepartment>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<DepartmentMode> GetModeList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<DepartmentMode, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<SetDefaultDepartment> GetDefault(GetDefaultDepartment input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetDefaultDepartment, GetDefaultDepartment>(companyKey: companyKey, procedureName: "ProGetDefaultDepartmentValues", parameter: input);
        }
    }
}











