using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MenuListModel
    {
        public static string _deleteProcedureName = "ProMenuDelete";
        public static string _updateProcedureName = "ProMenuListUpdate";
        public static string _selectProcedureName = "ProMenuListSelect";
        public class MenuListNew
        {
            public int Sort { get; set; }
            public IEnumerable<MenuGroupModel.MenuGroupView> MenuGroups { get; set; }
            public IEnumerable<ActionStatus> TransMode { get; set; }
            public IEnumerable<ControllerList> ControllerListData { get; set; }
        }

        public class ControllerData
        {
            public string ControllerName { get; set; }
        }

        public class TransModeCriteria
        {
            public string ID_TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class TransModeData
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class ControllerList
        {
            public string Name { get; set; }
        }
        public class ActionList
        {
            public string Name { get; set; }
        }
        public class MenuListShortView
        {
            public int SlNo { get; set; }
            public long ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
        }
        public class MenuListData
        {
            public int SlNo { get; set; }
            public long ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
            public string MnuGrpName { get; set; }
            public string MnuLstNameSub { get; set; }
            public int TotalCount { get; set; }
        }
        public class MenuListView
        {
            public int SlNo { get; set; }
            public long ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
            public long FK_MenuGroup { get; set; }
            public string ControllerName { get; set; }
            public string Url { get; set; }
            public string MnuParameter { get; set; }
            public string TransMode { get; set; }
            public string MnuIcon { get; set; }
            public string MnuImage { get; set; }
            public long FK_SubMenu { get; set; }
            public bool MnuLstVisible { get; set; }
            public Int64 SortOrder { get; set; }
            public int TotalCount { get; set; }
        }

        public class UpdateMenuList
        {

            public int UserAction { get; set; }
            public int Debug { get; set; }
            public long ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
            public long FK_MenuGroup { get; set; }
            public string ControllerName { get; set; }
            public string Url { get; set; }
            public string MnuParameter { get; set; }
            public string TransMode { get; set; }
            public string MnuIcon { get; set; }
            public string MnuImage { get; set; }
            public long FK_SubMenu { get; set; }
            public bool MnuLstVisible { get; set; }
            public Int64 SortOrder { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_MenuList { get; set; }

        }
        public class DeleteMenu
        {
            public long FK_MenuList { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }
        public class MenuListID
        {
            public long ID_MenuList { get; set; }
        }
        public class GetMenuDetails
        {
            public long FK_MenuList { get; set; }
        }
        public class GetMenuList
        {
            public long FK_MenuList { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }
        public class GetMenuListByMenuGroup
        {
            public long FK_MenuList { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public long FK_MenuGroup { get; set; }
        }
        public class GetMenuListForAccess
        {
            public long FK_MenuList { get; set; }          
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }           
        }
        public class ActionStatus
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GenTransMode
        {
            public Int32 Mode { get; set; }
        }
        
        public APIGetRecordsDynamic<ActionStatus> GeMenuListTransModeData(GenTransMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, GenTransMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdateMenuListData(UpdateMenuList input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMenuList>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMenuData(DeleteMenu input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMenu>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<MenuListShortView> GetMenuListData(GetMenuList input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuListShortView, GetMenuList>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<MenuListData> GetMenuData(GetMenuList input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuListData, GetMenuList>(companyKey: companyKey, procedureName: "ProMenuDataSelect", parameter: input);
        }
        public APIGetRecordsDynamic<MenuListView> GetMenuDetailsData(GetMenuDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuListView, GetMenuDetails>(companyKey: companyKey, procedureName: "ProMenuDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<MenuListView> GetMenuDataForMenuAccess(GetMenuListForAccess input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuListView, GetMenuListForAccess>(companyKey: companyKey, procedureName: "ProMenuListforAccess", parameter: input);
        }
        public APIGetRecordsDynamic<MenuListShortView> GetMenuListDataByMenuGroup(GetMenuListByMenuGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuListShortView, GetMenuListByMenuGroup>(companyKey: companyKey, procedureName: "ProMenuListSelectByGroup", parameter: input);
        }
        public APIGetRecordsDynamic<MenuListShortView> GetMenuListDataWithoutParentByMenuGroup(GetMenuListByMenuGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuListShortView, GetMenuListByMenuGroup>(companyKey: companyKey, procedureName: "ProMenuListSelectWithoutParentByGroup", parameter: input);
        }
    }
}
