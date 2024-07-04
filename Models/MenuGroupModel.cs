using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MenuGroupModel
    {
        public static string _deleteProcedureName = "ProMenuGroupDelete";
        public static string _updateProcedureName = "ProMenuGroupUpdate";
        public static string _selectProcedureName = "ProMenuGroupSelect";
        public class MenuGroupNew
        {
            public int Sort { get; set; }
            public IEnumerable<Module> ModuleList { get; set; }
        }
        public class ModuleCriteria
        {          
            public Int64 Mode { get; set; }
        }
        public class Module
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class MenuGroupView
        {
            public int SlNo { get; set; }
            public long ID_MenuGroup { get; set; }
            public string MnuGrpName { get; set; }
            public string SubModule { get; set; }
            public bool MnuGrpVisible { get; set; }
            public Int64 SortOrder { get; set; }
            public string MnuGrpIcon { get; set; }
            public int TotalCount { get; set; }
        }

        public class UpdateMenuGroup
        {
            public int UserAction { get; set; }
            public int Debug { get; set; }
            public long ID_MenuGroup { get; set; }
            public string MnuGrpName { get; set; }
            public string SubModule { get; set; }
            public bool MnuGrpVisible { get; set; }
            public Int64 SortOrder { get; set; }
            public string MnuGrpIcon { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_MenuGroup { get; set; }

        }
        public class DeleteMenuGroup
        {
            public long FK_MenuGroup { get; set; }
            public long FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }



        }
        public class MenuGroupID
        {
            public Int64 ID_MenuGroup { get; set; }
        }
        public class GetMenuGroup
        {
            public long FK_MenuGroup { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
        }
        public APIGetRecordsDynamic<Module> GeMenuGroupModuleData(ModuleCriteria input, string companyKey)
        {
            return Common.GetDataViaProcedure<Module, ModuleCriteria>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateMenuGroupData(UpdateMenuGroup input, string companyKey)
        {
            return Common.UpdateTableData<UpdateMenuGroup>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteMenuGroupData(DeleteMenuGroup input, string companyKey)
        {
            return Common.UpdateTableData<DeleteMenuGroup>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<MenuGroupView> GetMenuGroupData(GetMenuGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuGroupView, GetMenuGroup>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}