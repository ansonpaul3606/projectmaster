using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ActionTypeModel
    {
    
            public class ActionTypeView
            {

                public long SlNo { get; set; }
                public long ActionTypeID { get; set; }


                public string ActionType { get; set; }

                public string ShortName { get; set; }

                public short ActionModuleID { get; set; }
                public short ActionModeID { get; set; }

                public string ActionModuleName { get; set; }
                public string ActionModeName { get; set; }
                public Int32 SortOrder { get; set; }
               public Int64 TotalCount { get; set; }

        }


            public class ActionTypeInputFromViewModel
            {

                [Required(ErrorMessage = "No ActionType Selected")]
                public long ActionTypeID { get; set; }

                [Required(ErrorMessage = "Please Enter ActionType Name")]
                public string ActionType { get; set; }
                [Required(ErrorMessage = "Please Enter Short Name For ActionType ")]
                public string ShortName { get; set; }



                [Range(1, long.MaxValue, ErrorMessage = "Select Action Module ")]

                public short ActionModuleID { get; set; }

                [Range(1, long.MaxValue, ErrorMessage = "Select Action Mode ")]

                public short ActionModeID { get; set; }
            public string TransMode { get; set; }

            public Int32 SortOrder { get; set; }


            }


            public class ActionTypeInfoView
            {
                [Required(ErrorMessage = "No ActionType Selected")]
                public Int64 ActionTypeID { get; set; }
                [Required(ErrorMessage = "Please Select The Reason")]
                public Int64 ReasonID { get; set; }
            }


            public static string _updateProcedureName = "proActionTypeUpdate";
            public class UpdateActionType
            {
                public long FK_ActionType { get; set; }
                public long ID_ActionType { get; set; }
                public short FK_ActionModule { get; set; }
                public short FK_ActionMode { get; set; }
                public string ActnTypeName { get; set; }
                public string ActnTypeShortName { get; set; }
                public Int32 SortOrder { get; set; }
                public string TransMode { get; set; }

                public Int64 FK_Company { get; set; }
                public Int16 UserAction { get; set; }
                public Int64 FK_Machine { get; set; }
                public Int64 FK_BranchCodeUser { get; set; }
                public string EntrBy { get; set; }



        }

        public static string _selectProcedureName = "proActionTypeSelect";
            public class GetActionType
            {
            public Int64 FK_ActionType { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
        }


            public static string _deleteProcedureName = "proActionTypeDelete";
        public class DeleteActionType
        {
            public long FK_ActionType { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
          

        }

        public class ActionModeModel
            {
                public short ActionModeID { get; set; }
                public string ActionModeName { get; set; }


            }
            public class ActionModuleModel
            {
                public short ActionModuleID { get; set; }
                public string ActionModuleName { get; set; }


            }
            public class ActionTypeListModel
            {
                public List<ActionModuleModel> ActionModuleList { get; set; }
                public List<ActionModeModel> ActionModeList { get; set; }

                public int SortOrder { get; set; }

            }

            public Output UpdateActionTypeData(UpdateActionType input, string companyKey)
            {
                return Common.UpdateTableData<UpdateActionType>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
            }
            public Output DeleteActionTypeData(DeleteActionType input, string companyKey)
            {
                return Common.UpdateTableData<DeleteActionType>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
            }


            public APIGetRecordsDynamic<ActionTypeView> GetActionTypeData(GetActionType input, string companyKey)
            {
                return Common.GetDataViaProcedure<ActionTypeView, GetActionType>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

            }
        }
    }