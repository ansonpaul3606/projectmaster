/*----------------------------------------------------------------------
Created By	: Haseena k
Created On	: 07/09/2023
Purpose		: CheckList
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PerfectWebERP.Models
{
    public class CheckListModel
    {
        public static string _deleteProcedureName = "ProCheckListDelete";
        public static string _updateProcedureName = "ProCheckListUpdate";
        public static string _selectProcedureName = "ProCheckListSelect";

        public class CheckListNew
        {
            public int Sort { get; set; }
            //public IEnumerable<Module> ModuleList { get; set; }
        }
        public class CheckListView
        {
            public int SlNo { get; set; }
            public long ID_CheckList { get; set; }
          //  [Required(ErrorMessage = "Please Enter Checklist Name")]
            public string CkLstName { get; set; }
           // [Required(ErrorMessage = "Please Enter Short Name")]
            public string CkLstShortName { get; set; }
           // [Required(ErrorMessage = "Please Enter Sort Order")]
            public Int32 SortOrder { get; set; }
          //  [Required(ErrorMessage = "Please Enter Trans Mode")]
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
          // public string[] ChecklistArray { get; set; }
            public List<string> ChecklistArray { get; set; }
            public string FK_CheckListType_JSON { get; set; }
            public long CkLstInspectionType { get; set; }
            public string InspectionType { get; set; }
            

            public List<string> FK_CheckListType//ok
            {
                get
                {
                    //PortalCommonMethod portalCommonMethod = new PortalCommonMethod();
                    //return portalCommonMethod.Encrypt(strClearText: this.TicketID.ToString()); //some iisue with '/' while routing to view
                    if (this.FK_CheckListType_JSON is null)
                    {
                        return default(List<string>);
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<List<string>>(this.FK_CheckListType_JSON);
                    }
                }
            }
            public List<ChecklistCategory> ChecklistCategorylist { get; set; }
            public List<InspectionList> InspectionListData { get; set; }
        }
        public class ChecklistCategory
        {
            public Int64 ID_CheckListType { get; set; }
            public string CLTyName { get; set; }
        }
        public class CheckListType
        {
            public string FK_CheckListType { get; set; }
        }

        public class UpdateCheckList
        {
            public int UserAction { get; set; }
            public short Debug { get; set; }
            public long ID_CheckList { get; set; }
            public string CkLstName { get; set; }
            public string CkLstShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
           // public bool Passed { get; set; }
           // public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
           // public DateTime EntrOn { get; set; }
           // public string CancelBy { get; set; }
           // public DateTime CancelOn { get; set; }
           // public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
           // public long BackupId { get; set; }
            public string TransMode { get; set; }
            public long FK_CheckList { get; set; }
            public string checklistcategory { get; set; }
            public long InspectionType { get; set; }
        }
        
        public class DeleteCheckList
        {
            public long FK_CheckList { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            
            public long FK_Reason { get; set; }           
            public long FK_Company { get; set; }           
            public long FK_BranchCodeUser { get; set; }
        }


        public class CheckListID
        {
            public Int64 ID_CheckList { get; set; }
            public string TransMode { get; set; }
        }
        
        public class GetCheckList
        {
            public long FK_CheckList { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
        }
        public class GetChecklistCategory
        {
            public long FK_Company { get; set; }
            public Int64 Pagemode { get; set; }
            public Int64 Critrea1 { get; set; }
            public Int64 Critrea2 { get; set; }
            public string Critrea3 { get; set; }
            public string Critrea4 { get; set; }
            public Int64 ID { get; set; }
            public string TransMode { get; set; }

        }
        public class ModeValue
        {
            public Int32 Mode { get; set; }
        }
        public class InspectionList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public Output UpdateCheckListData(UpdateCheckList input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCheckList>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteCheckListData(DeleteCheckList input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCheckList>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<CheckListView> GetCheckListData(GetCheckList input, string companyKey)
        {
            return Common.GetDataViaProcedure<CheckListView, GetCheckList>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ChecklistCategory> GetChecklistCategorylist(GetChecklistCategory input, string companyKey)
        {
            return Common.GetDataViaProcedure<ChecklistCategory, GetChecklistCategory>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);
        }
        public APIGetRecordsDynamic<InspectionList> GetInspectionListData(ModeValue input, string companyKey)
        {
            return Common.GetDataViaProcedure<InspectionList, ModeValue>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
    }
}
