using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;
namespace PerfectWebERP.Models
{
    public class QualificationModel
    {

        public class QualificationViewModel
        {
            public long SlNo { get; set; }
            [Required(ErrorMessage = "Please Enter Qualification Name")]

            public string QualificationName { get; set; }

            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string QualificationShortName { get; set; }

            public Int16 QualificationID { get; set; }
            public int SortOrder { get; set; }

            public long ReasonID { get; set; }
            public string TransMode { get; set; }
        }


        public static string _updateProcedureName = "proQualificationUpdate";
        public class UpdateQualification
        {
            public long ID_Qualification { get; set; }
            public string QuaName { get; set; }
            public string QuaShortName { get; set; }

            public Int32 SortOrder { get; set; }
            public long FK_Qualification { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            //public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }

        }

        public class QualificationInfoView
        {
            [Required(ErrorMessage = "Please select a Department")]
            [Range(1, long.MaxValue, ErrorMessage = "Please select a department")]
            public Int64 QualificationID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }


        public static string _deleteProcedureName = "proQualificationDelete";
        public class DeleteQualification
        {
            public Int64 FK_Qualification { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        


        public static string _selectProcedureName = "proQualificationSelect";
        public class GetQualification
        {
            public Int64 FK_Qualification { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }


        public Output UpdateQualificationData(UpdateQualification input, string companyKey)
        {
            return Common.UpdateTableData<UpdateQualification>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeleteQualificationData(DeleteQualification input, string companyKey)
        {
            return Common.UpdateTableData<DeleteQualification>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<QualificationViewModel> GetQualificationData(GetQualification input, string companyKey)
        {
            return Common.GetDataViaProcedure<QualificationViewModel, GetQualification>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }





    }
}