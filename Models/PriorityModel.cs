using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class PriorityModel
    {
        public  class PriorityViewModel
        {
            public long SlNo { get; set; }
            public long PriorityID { get; set; }
            public string Name { get; set; }
            public string ShortName { get; set; }
            public string ColorCode { get; set; }
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
            public long SortOrder { get; set; }
            public Int64 TotalCount { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
        }

        //public class SortOrderData
        //{

        //    public int SortOrder { get; set; }
        //}
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Prioritylist
        {
            public List<ActionStatus> Modulelist { get; set; }
            public int SortOrder { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class UpdatePriority
        {
            public long ID_Priority { get; set; }
            public string PrtyName { get; set; }
            public string PrtySName { get; set; }
            public string ColorCode { get; set; }
            public long ID_Mode { get; set; }
            public long SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
          
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
        }
        public class InputPriorityID
        {
            public Int64 FK_PriorityMaster { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }

        }
        public class DeletePriority
        {
            public long FK_PriorityMaster { get; set; }

            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public static string _updateProcedureName = "ProPriorityMasterUpdate";
        public static string _deleteProcedureName = "ProPriorityMasterDelete";
        public static string _selectProcedureName = "ProPriorityMasterSelect";
        public APIGetRecordsDynamic<ActionStatus> GetModulesList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdatePriorityData(UpdatePriority input, string companyKey)
        {
           
            return Common.UpdateTableData<UpdatePriority>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public APIGetRecordsDynamic<PriorityViewModel> GetPriorityData(InputPriorityID input, string companyKey)
        {
            return Common.GetDataViaProcedure<PriorityViewModel, InputPriorityID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}