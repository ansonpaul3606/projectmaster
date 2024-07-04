using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class NextActionModel
    {

        public class NextActionView
        {
            public long SlNo { get; set; }
            public long NextActionID { get; set; }
            public string NextAction { get; set; }
            public string ShortName { get; set; }
            public short ActionModuleID { get; set; }
            public short ActionStatusID { get; set; }
            public string ActionModuleName { get; set; }
            public string ActionStatusName { get; set; }
            public Int32 SortOrder { get; set; }
            // public short FK_ActionModule { get; set; }
            public short NxtActnStage { get; set; }
        }


        public class NextActionInputFromViewModel
        {

            [Required(ErrorMessage = "No NextAction Selected")]
            public long NextActionID { get; set; }

            [Required(ErrorMessage = "Please Enter NextAction Name")]
            public string NextAction { get; set; }
            [Required(ErrorMessage = "Please Enter Short Name For NextAction ")]
            public string ShortName { get; set; }



            [Range(1, long.MaxValue, ErrorMessage = "Select Action Module ")]

            public short ActionModuleID { get; set; }

            [Range(1, long.MaxValue, ErrorMessage = "Select Action Status ")]

            public short ActionStatusID { get; set; }
            public short ActionStageID { get; set; }

            public Int32 SortOrder { get; set; }

        }


        public class NextActionInfoView
        {
            [Required(ErrorMessage = "No NextAction Selected")]
            public Int64 NextActionID { get; set; }
            [Required(ErrorMessage = "Please Select The Reason")]
            public Int64 ReasonID { get; set; }
        }


        public static string _updateProcedureName = "proNextActionUpdate";
        public class UpdateNextAction
        {
            public long FK_NextAction { get; set; }
            public long ID_NextAction { get; set; }
            public short FK_ActionModule { get; set; }
            public short FK_ActionStatus { get; set; }
            public string NxtActnName { get; set; }
            public string NxtActnShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public string TransMode { get; set; }

            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public short NxtActnStage { get; set; }


        }

        public static string _selectProcedureName = "proNextActionSelect";
        public class GetNextAction
        {
            public Int64 FK_NextAction { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }


        public static string _deleteProcedureName = "proNextActionDelete";
        public class DeleteNextAction
        {
            public long FK_NextAction { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }

        public class ActionStatusModel
        {
            public short ActionStatusID { get; set; }
            public string ActionStatusName { get; set; }
            public short FK_ActionModule { get; set; }


        }
        public class ActionModuleModel
        {
            public short ActionModuleID { get; set; }
            public string ActionModuleName { get; set; }


        }
        public class NextActionListModel
        {
            public List<ActionModuleModel> ActionModuleList { get; set; }

            public List<ActionStatusModel> ActionStatusList { get; set; }
            public List<ActionStatus> ActionStatusNewList { get; set; }
            public int SortOrder { get; set; }
            public short ActionModuleID { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class NextActionViewList
        {
            public long SlNo { get; set; }
            public long NextActionID { get; set; }
            public string NextAction { get; set; }
            public string ShortName { get; set; }
            public short ActionModuleID { get; set; }
            public short ActionStatusID { get; set; }
            public string ActionModuleName { get; set; }
            public string ActionStatusName { get; set; }
            public Int32 SortOrder { get; set; }
            public short NxtActnStage { get; set; }
            public Int64 TotalCount { get; set; }
        }
       
        public class OutputTest
        {
            public long Column1 { get; set; }
            public int ErrCode { get; set; }
            public string ErrMsg { get; set; }
        }
        public APIGetRecordsDynamic<OutputTest> UpdateNextActionData(UpdateNextAction input, string companyKey)
        
        {
           
            return Common.GetDataViaProcedure<OutputTest, UpdateNextAction>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteNextActionData(DeleteNextAction input, string companyKey)
        {
            return Common.UpdateTableData<DeleteNextAction>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public APIGetRecordsDynamic<NextActionView> GetNextActionData(GetNextAction input, string companyKey)
        {
            return Common.GetDataViaProcedure<NextActionView, GetNextAction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<NextActionViewList> GetNextActionDataList(GetNextAction input, string companyKey)
        {
            return Common.GetDataViaProcedure<NextActionViewList, GetNextAction>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

    }

}