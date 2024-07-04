using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ReasonModel
    {
        public class ReasonsView
        {


            public long ReasonID { get; set; }

            public long SlNo { get; set; }
            public string Reason { get; set; }

            public string ShortName { get; set; }



            public short ModeID { get; set; }

            public Int32 SortOrder { get; set; }

            public string ModeName { get; set; }
            public long BranchCodeUserID { get; set; }
            public Int64 TotalCount { get; set; }
           

        }
        
        public class ReasonsInputFromViewModel
        {

            [Required(ErrorMessage = "No Reason Selected")]
            public long ReasonID { get; set; }

            [Required(ErrorMessage = "Please Enter Reason Name")]
            public string Reason { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name ")]
            public string ShortName { get; set; }


            [Range(1, long.MaxValue, ErrorMessage = "Select Mode")]
            public short ModeID { get; set; }

            public Int32 SortOrder { get; set; }


        }

        public class ReasonInfoView
        {
            [Required(ErrorMessage = "No Reason Selected")]
            public Int64 ReasonID { get; set; }
            //[Required(ErrorMessage = "Please select the reason")]
            //public Int64 ReasonID { get; set; }

        }


        public static string _updateProcedureName = "proReasonUpdate";
        public class UpdateReason
        {
            public long FK_Reason { get; set; }
            public long ID_Reason { get; set; }
            public short FK_ReasonMode { get; set; }

            public string ResnShortName { get; set; }
            public string ResnName { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

           
        }

        public class SelectReason
        {

            public long ID_Reason { get; set; }
            public short FK_ReasonMode { get; set; }

            public string ResnShortName { get; set; }
            public string ResnName { get; set; }
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

        public static string _deleteProcedureName = "proReasonDelete";
        public class DeleteReason
        {
            public Int64 ID_Reason{ get; set; }
            public string TransMode { get; set; }
          
            //public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
    
        }

        public class ReasonMode
        {
            public short ModeID { get; set; }
            public string ModeName { get; set; }
        }
        public class ReasonListModel
        {
            public List<ReasonMode> ReasonModeList { get; set; }
            public int SortOrder { get; set; }

        }
        public class InputReasonID
        {
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }

        }


        public Output UpdateReasonData(UpdateReason input, string companyKey)
        {
            return Common.UpdateTableData<UpdateReason>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteReasonData(DeleteReason input, string companyKey)
        {
            return Common.UpdateTableData<DeleteReason>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proReasonSelect";
        public APIGetRecordsDynamic<ReasonsView> GetReasonData(InputReasonID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReasonsView, InputReasonID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}