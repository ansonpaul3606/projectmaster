//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace PerfectWebERP.Models
//{
//    public class EmployeeLevelModel
//    {
//    }
//}



using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class EmployeeLevelModel
    {

        //select procedure output
        public class EmployeeLevelView
        {
            public long SlNo { get; set; }
            public long EmployeeLevelID { get; set; }

            [Required(ErrorMessage = "Please Enter Employee Level Name")]
            public string EmployeeLevelName { get; set; }
            [Required(ErrorMessage = "Please Enter Employee Level Short Name")]
            public string EmployeeLevelShortName { get; set; }
            //[Required(ErrorMessage = "Please Enter Employee Level Priority")]
            public Int32 EmployeeLevelPriority { get; set; }
            //[Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }

            [Required(ErrorMessage = "Please Select Reason")]
            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }


      

        


        public static string _deleteProcedureName = "ProEmployeeLevelDelete";
        public static string _updateProcedureName = "ProEmployeeLevelUpdate";
        public static string _selectProcedureName = "ProEmployeeLevelSelect";

        //procedure input for update /add
        public class UpdateEmployeeLevel
        {
            public long ID_EmployeeLevel { get; set; }
            public string TransMode { get; set; }
            public long FK_EmployeeLevel { get; set; }
            public string EmpLvlName { get; set; }
            public string EmpLvlShortName { get; set; }
            public Int32 EmpLvlPriority { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            //public bool Passed { get; set; }
            //public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
            //public DateTime EntrOn { get; set; }
            //public string CancelBy { get; set; }
            //public DateTime CancelOn { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public long FK_Branch { get; set; }
            public byte UserAction { get; set; }


        }

        //procedure input for delete
        public class DeleteEmployeeLevel
        {
            public long FK_EmployeeLevel { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_Reason { get; set; }
            //public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
             
        }



        //procedure input for select
        public class EmployeeLevelID
        {
            public Int64 FK_EmployeeLevel { get; set; }

            public string TransMode { get; set; }

            public Int32 PageIndex { get; set; }


            public Int32 PageSize { get; set; }

            public string EntrBy { get; set; }

            public long FK_Company { get; set; }

            public string Name { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }

        //update procedure function
        public Output UpdateEmployeeLevelData(UpdateEmployeeLevel input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeLevel>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        //delete procedure function
        public Output DeleteEmployeeLevelData(DeleteEmployeeLevel input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEmployeeLevel>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        //select prrocedure function  eg:     selectOutputMode  selectfunction (selectInputModel)
        public APIGetRecordsDynamic<EmployeeLevelView> GetEmployeeLevelData(EmployeeLevelID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeLevelView, EmployeeLevelID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
