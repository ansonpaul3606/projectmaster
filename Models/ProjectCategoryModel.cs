using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class ProjectCategoryModel
    {
        public class ProjectCategorysView
        {

            public long SlNo { get; set; }
            public long ProjectCategoryID { get; set; }


            public string ProjectCategory { get; set; }

            public string ShortName { get; set; }

            public Int32 SortOrder { get; set; }

        


        }

        public class ProjectCategorysInputFromViewModel
        {

            [Required(ErrorMessage = "No ProjectCategory Selected")]
            public long ProjectCategoryID { get; set; }

            [Required(ErrorMessage = "Please Enter ProjectCategory Name")]
            public string ProjectCategory { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string ShortName { get; set; }

            public Int32 SortOrder { get; set; }


        }

        public class ProjectCategoryInfoView
        {
            [Required(ErrorMessage = "No ProjectCategory Selected")]
            public Int64 ProjectCategoryID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }


        public static string _updateProcedureName = "proProjectCategoryUpdate";
        public class UpdateProjectCategory
        {

            public long ID_ProjectCategory { get; set; }
            public long FK_ProjectCategory { get; set; }


            public string ProjShortName { get; set; }
            public string ProjName { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }



        }

        public class SelectProjectCategory
        {

            public long ID_ProjectCategory { get; set; }


            public string ProjShortName { get; set; }
            public string ProjName { get; set; }
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

        public static string _deleteProcedureName = "proProjectCategoryDelete";
        public class DeleteProjectCategory
        {
            public Int64 FK_ProjectCategory { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }


        public class ProjectCategoryListModel
        {
            public int SortOrder { get; set; }

        }
        public class InputProjectCategoryID
        {
            public Int64 FK_ProjectCategory { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }


        public Output UpdateProjectCategoryData(UpdateProjectCategory input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProjectCategory>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProjectCategoryData(DeleteProjectCategory input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProjectCategory>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectCategorySelect";
        public APIGetRecordsDynamic<ProjectCategorysView> GetProjectCategoryData(InputProjectCategoryID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectCategorysView, InputProjectCategoryID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}