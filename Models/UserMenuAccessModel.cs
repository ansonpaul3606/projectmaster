
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;

namespace PerfectWebERP.Models
{
    public class UserMenuAccessModel
    {       
        public static string _deleteProcedureName = "ProUserGroupMenuAccessDelete";
        public static string _updateProcedureName = "ProUserGroupMenuAccessUpdate";
        public static string _selectProcedureName = "ProUserGroupMenuAccessSelect";
        public class UserMenuAccessNew
        {
            public IEnumerable<UserRoleModel.UserRoleView> Role { get; set; }           
        }
        public class UserGroupMenuAccessID
        {
            public long ID_UserGroupMenuAccess { get; set; }
        }
        public class UserMenuAccessUpdate
        {
            public long ID_UserGroupMenuAccess { get; set; }
            public long FK_UserGroup { get; set; }
            public List<SelectedNode> SelectedNodes { get; set; }
        }
       public class SelectedNode
        {
            public string ID { get; set; }
        }
        public class TreeViewMenu
        {
            public long ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
            public long FK_SubMenu { get; set; }
            public List<TreeViewMenu> SubList { get; set; }
        }


        public class TreeViewNodeState
        {
            public bool selected { get; set; }
            public bool opened { get; set; }
            public bool checkbox_disabled { get; set; }
        }
        public class TreeViewNode
        {
            public string id { get; set; }
            public string parent { get; set; }
            public string text { get; set; }
            public TreeViewNodeState state { get; set; }
        }
        public class UserGroupMenuAccessView
        {
            public int SlNo { get; set; }
            public long ID_UserGroupMenuAccess { get; set; }
            public string UsrGrpMnuLst { get; set; }
            public long FK_UserGroup { get; set; }
            public string UsrrlName { get; set; }
            public int TotalCount { get; set; }
        }
        public class UpdateUserGroupMenuAccess
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public long ID_UserGroupMenuAccess { get; set; }
            public string UsrGrpMnuLst { get; set; }
            public long FK_UserGroup { get; set; }
            public long FK_Company { get; set; }

            public long FK_BranchCodeUser { get; set; }
            
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_UserGroupMenuAccess { get; set; }
           
        }
        public class GetUserGroupMenuAccess
        {
            public long FK_UserGroupMenuAccess { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }
        public class DeleteUserGroupMenuAccess
        {
            public long FK_UserGroupMenuAccess { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
        public Output UpdateUserGroupMenuAccessData(UpdateUserGroupMenuAccess input, string companyKey)
        {
            return Common.UpdateTableData<UpdateUserGroupMenuAccess>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteUserGroupMenuAccessData(DeleteUserGroupMenuAccess input, string companyKey)
        {
            return Common.UpdateTableData<DeleteUserGroupMenuAccess>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<UserGroupMenuAccessView> GetUserGroupMenuAccessData(GetUserGroupMenuAccess input, string companyKey)
        {
            return Common.GetDataViaProcedure<UserGroupMenuAccessView, GetUserGroupMenuAccess>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}