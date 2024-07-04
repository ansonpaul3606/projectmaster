//@*----------------------------------------------------------------------
//Created By  : Athul M
//Created On  : 23/01/2022
//Purpose		: ExtraWorkType
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
    public class ExtraWorkTypeModel
    {
        public class ExtraWorkTypeView
        {
            // [Required(ErrorMessage = "Please select Extra Work Type")]
            //[Range(1, long.MaxValue, ErrorMessage = "Please select Extra Work Type")]
            public long SlNo { get; set; }
            public int ExtraWorkTypeID { get; set; }
           // [Required(ErrorMessage = "Please Enter Extra Work Name")]
           // [StringLength(150, ErrorMessage = "Maximum 150 characture")]
            public string WorkName { get; set; }
           // [Required(ErrorMessage = "Please Enter Extra Work Short Name")]
           // [StringLength(10, ErrorMessage = "Maximum 10 characture")]
            public string ShortName { get; set; }

            public Int32 SortOrder { get; set; }
//[Required(ErrorMessage = "Please Enter Account Head")]

            public Int32 AccountHeadID { get; set; }

            public string AccountHead { get; set; }
            public bool ExtraWork { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
        }
        public class UpdateExtraWorkType
        {
            public byte UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; } /// <summary>
            /// ////////////////////
            /// </summary>
            public long ExtraWorkTypeID { get; set; }
            public string ExtwrktyName { get; set; }
            public string ExtwrktyShortName { get; set; }
            public bool EWTExtraWorkType { get; set; }
            
            public Int32 SortOrder { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
            public Int64 FK_ExtwrkType { get; set; }
        }
        public class AccountName
        {
             public int AccountHeadID { get; set; }
            public string AccoundHead { get; set; }
        }

        public static string _deleteProcedureName = "proExtraWorkTypeDelete";
        public static string _updateProcedureName = "proExtraWorkTypeUpdate";
        public static string _selectProcedureName = "proExtraWorkTypeSelect";

        public class DeleteView
        {

            public long ExtraWorkTypeID { get; set; }
            [Required(ErrorMessage = "Please Select a Reason")]
            public Int64 ReasonID { get; set; }
        }


        public class DeleteExtraWorkType
        {
            //public long ID_ExtraWorkType { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_ExtraWorkType { get; set; }
            public Int64 FK_Reason { get; set; }
            public int Debug { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }

        public class ExtWkList
        {
        
            public int SortOrder { get; set; }
        }
        public class ExtraWorkTypeID
        {
            public Int64 FK_ExtraWorkType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }


        }
        public Output UpdateExtraWorkTypeData(UpdateExtraWorkType input, string companyKey)
        {
            return Common.UpdateTableData<UpdateExtraWorkType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteExtraWorkTypeData(DeleteExtraWorkType input, string companyKey)
        {
            return Common.UpdateTableData<DeleteExtraWorkType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ExtraWorkTypeView> GeExtraWorkTypeData(ExtraWorkTypeID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ExtraWorkTypeView, ExtraWorkTypeID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}


