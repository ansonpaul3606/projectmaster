using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace PerfectWebERP.Models
{
    public class EmployeeTypeModel
    {

        public class EmployeeTypeView
        {
            public long SlNo { get; set; }
            public Int64 EmployeeTypeID { get; set; }
            public string EmployeeType { get; set; }
            public string ShortName { get; set; }
            public Int16 SortOrder { get; set; }
            public Int16 EmployeeCategoryID { get; set; }
            //public Int16 EmptyCategory { get; set; }
            public string EmployeeCategory { get; set; }
           // public string CategoryName { get; set; }
            public long ReasonID { get; set; }

            public Int32 TotalCount { get; set; }
            public string TransMode { get; set; }


        }

        public class EmployeeTypeFormData 
        {

            public List<EmployeeCategory> EmployeeCategoryList { get; set; }
            public int SortOrder { get; set; }
        }

        public class EmployeeCategory
        {
            public int EmployeeCategoryID { get; set; }
            public string EmployeeCategoryName { get; set; }

        }

        public class EmployeeInfoView
        {
            [Required(ErrorMessage = "Please select a Employee Type")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a Employee Type")]
            public Int64 IDEmployeeType { get; set; }

        }



        public static string _selectProcedureName = "ProEmployeeTypeSelect";
        public class GetEmployeeType
        {
            public Int64 FK_EmployeeType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

            public string TransMode { get; set; }
            public string Name { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }

        public static string _updateProcedureName = "proEmployeeTypeUpdate";
        public class UpdateEmployeeType
        {
            public Int64 ID_EmployeeType { get; set; }
            public int UserAction { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 SortOrder { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string EmptyShortName { get; set; }
            public string EmptyName { get; set; }
            public Int16 EmptyCategory { get; set; }
            public Int64 BackupID { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_EmployeeType { get; set; }
            public string TransMode { get; set; }


        }

        public static string _deleteProcedureName = "proEmployeeTypeDelete";
        public class DeleteEmployeeType
        {
            public Int64 FK_EmployeeType { get; set; }
            //public string CancelBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }


        public Output UpdateEmployeeTypeData(UpdateEmployeeType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteEmployeeTypeData(DeleteEmployeeType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEmployeeType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<EmployeeTypeView> GetEmployeeTypetData(GetEmployeeType input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeTypeView, GetEmployeeType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}