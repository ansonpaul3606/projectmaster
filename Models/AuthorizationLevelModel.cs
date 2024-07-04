
using PerfectWebERP.General;
using System;
using System.Collections.Generic;

namespace PerfectWebERP.Models
{
    public class AuthorizationLevelModel
    {
        public static string _updateProcedureName = "ProAuthorizationLevelUpdate";
        public static string _selectProcedureName = "ProAuthorizationLevelSelect";
        public static string _deleteProcedureName = "ProAuthorizationLevelDelete";        
       
        public class AuthorizationlevelInit
        {
            public IEnumerable<MenuGroupModel.MenuGroupView> ModuleList { get; set; }
            public IEnumerable<Branchs> BranchList { get; set; }
            public IEnumerable<BranchTypes> BranchTypelists { get; set; }
            public IEnumerable<Userrole> UserRolelists { get; set; }
            public IEnumerable<ActionStatus> AmountCriteria { get; set; }
        }
        public class BranchTypes
        {
            public int BranchTypeID { get; set; }
            public string BranchType { get; set; }
            public long BranchModeID { get; set; }
            public long FK_BranchMode { get; set; }
            public long FK_BranchType { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
            public long FK_Branch { get; set; }
        }
        public class Userrole
        {
            public int UserRoleID { get; set; }
            public string UserRole { get; set; }
            public long FK_UserRole { get; set; }
        }
        public class MenuGroups
        {
            public int MenuGroupID { get; set; }
            public string MenuGroup { get; set; }
        }
        public class MenuLists
        {
            public int MenuListID { get; set; }
            public string MenuList { get; set; }
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }       
        public class AuthorizationLevelNew
        {
            public long ID_AuthorizationLevel { get; set; }           
            public DateTime AuthEffectDate { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_MenuList { get; set; }       
            public bool SkipPreviousLevel { get; set; }
            public bool ActiveCorrectionOption { get; set; }          
            public List<SubAuthorizationLevel> SubAuthorizationLevel { get; set; }
        }

        public class SubAuthorizationLevel
        {
            public long Priority { get; set; }
            public long ID_AuthorizationLevelDetails { get; set; }
            public long FK_AmountCriteria { get; set; }
            public decimal AldAmountFrom { get; set; }
            public decimal AldAmountTo { get; set; }       
            public long FK_AuthorizationLevel { get; set; }          
            public long FK_UserGroup { get; set; }
            public long FK_BranchType { get; set; }
            public long FK_User { get; set; }
            public string UserName { get; set; }           
        }
        public class AuthorizationLevelID
        {
            public Int64 FK_AuthorizationLevel { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
        }
        public class AuthorizationLevelView
        {
            public long SlNo { get; set; }
            public long AuthorizationLevelID { get; set; }
            public DateTime EffectDate { get; set; }
            public long MenuGroupID { get; set; }
            public string MenuGroup { get; set; }
            public long MenuListID { get; set; }
            public string MenuList { get; set; }           
            public Int64 TotalCount { get; set; }
            public bool SkipPreviousLevel { get; set; }
            public bool ActiveCorrectionOption { get; set; }           
        }
        public class SubAuthorizationLevelDetailsView
        {
            public long ID_Authorizationleveldetails { get; set; }
            public long AmountFrom { get; set; }
            public long AmountTo { get; set; }
            public bool Adds { get; set; }
            public bool Edits { get; set; }
            public bool Deletes { get; set; }
            public long FK_AuthorizationLevel { get; set; }
            public long UserRoleID { get; set; }
            public string UserRole { get; set; }
            public long UsersID { get; set; }
            public string UserName { get; set; }
            public Int16 Priority { get; set; }
            public long FK_AmountCriteria { get; set; }
        }
        public class AuthorizationLevelInfoView
        {
            public long ID_AuthorizationLevel { get; set; }
            public long ReasonID { get; set; }
        }
        public class Users
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
        }
        public class Usersrelated
        {
            public long FK_BranchMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_UserRole { get; set; }
        }
        public class AuthorizationLevelSubSelect
        {
            public Int64 FK_AuthorizationLevel { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class AuthorizationListSelect
        {
            public long FK_Company { get; set; }
            public long FK_UserGroup { get; set; }
            public long FK_User { get; set; }
            public string Module { get; set; }
            public string Name { get; set; }
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }           
        }
        public class AuthorizationListDetails
        {
            public long FK_Company { get; set; }
            public long FK_Master { get; set; }          
            public string TransMode { get; set; }
            public long Mode { get; set; }
        }
        public class AuthorizationList
        {
            public long ID_AuthorizationData { get; set; }
            public Int64 SlNo { get; set; }
            public string TransTitle { get; set; }
            public string TransNo { get; set; }
            public DateTime TransDate { get; set; }
            public decimal TransAmount { get; set; }
            public long TotalCount { get; set; }           
        }
        public class UpdateAuthorizationLevel
        {
            public long ID_AuthorizationLevel { get; set; }
            public DateTime AuthEffectDate { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_MenuList { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long UserAction { get; set; }
            public long FK_AuthorizationLevel { get; set; }
            public bool SkipPreviousLevel { get; set; }
            public bool ActiveCorrectionOption { get; set; }
            public string SubAuthorizationLevelDetails { get; set; }
        }
        public class UpdateAuthorizationApprove
        {
            public long FK_AuthorizationData { get; set; }          
            public long FK_Company { get; set; }         
            public string EntrBy { get; set; }
            public long SkipPrev { get; set; }
            public long ID_AuthorizationLevel { get; set; }

        }
        public class UpdateAuthorizationCorrection
        {
            public long FK_AuthorizationData { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long SkipPrev { get; set; }
            public string Reason { get; set; }
            public long FK_Reason { get; set; }
            public long ID_AuthorizationLevel { get; set; }
        }
        public class UpdateAuthorizationReject
        {
            public long FK_AuthorizationData { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public string Reason { get; set; }
            public long FK_Reason { get; set; }
            public long SkipPrev { get; set; }
            public long ID_AuthorizationLevel { get; set; }
           
        }
        public class DeleteAuthorizationLevel
        {
            public long FK_AuthorizationLevel { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }
        public class AuthorizationModuleSelect
        {
            public long FK_Company { get; set; }
            public long FK_UserGroup { get; set; }
            public long FK_User { get; set; }
        }
        public class AuthorizationModule
        {
            public string Module { get; set; }
            public string Module_Name { get; set; }
            public int NoofRecords { get; set; }
            public string Color { get; set; }
        }
        public class AuthorizationActionSelect
        {
            public long FK_Company { get; set; }
            public long FK_UserGroup { get; set; }
            public long FK_User { get; set; }
            public long AuthID { get; set; }
            public string Module { get; set; }
        }
        public class AuthorizationAction
        {
            public string TransactionDetails { get; set; }
            public string PartyDetails { get; set; }
            public string SubTitle { get; set; }
            public bool SubDetails { get; set; }
            public string FooterLeft { get; set; }
            public string FooterRight { get; set; }
            public long FK_Master { get; set; }
            public string TransMode { get; set; }
            public long Mode { get; set; }
            public bool ActiveCorrectionOption { get; set; }
        }
        public class AuthorizationAwaitHistorySelect
        {
            public long FK_Company { get; set; }
            public long FK_TransMaster { get; set; }
            public string TransMode { get; set; }
        }
        public class AuthorizationAwaitHistory
        {
            public string URName { get; set; }
            public string Approved { get; set; }
            public string ApprovedOn { get; set; }
        }
        public class AuthorizationActionVM
        {
            public IEnumerable<ReasonModel.ReasonsView> Reason { get; set; }
        }
        public class AuthorizationModuleVM
        {
            public IEnumerable<AuthorizationModule> AMData { get; set; }
        }
        public APIGetRecordsDynamic<ActionStatus> GetModeList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public Output UpdateAuthorizationLevelData(UpdateAuthorizationLevel input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAuthorizationLevel>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<AuthorizationLevelView> GetAuthorizationLevelData(AuthorizationLevelID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AuthorizationLevelView, AuthorizationLevelID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<SubAuthorizationLevel> GetSubAuthorizationLevelData(AuthorizationLevelSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubAuthorizationLevel, AuthorizationLevelSubSelect>(companyKey: companyKey, procedureName: "ProAuthorizationlevelsubtabledataSelect", parameter: input);
        }
        public APIGetRecordsDynamic<Users> GetAuthorizationlevelUsersnamelist(Usersrelated input, string companyKey)
        {
            return Common.GetDataViaProcedure<Users, Usersrelated>(companyKey: companyKey, procedureName: "ProAuthorizationlevelUsersnamelist", parameter: input);
        }
      
        public APIGetRecordsDynamicdn<dynamic> GetAuthorizationListData(AuthorizationListSelect input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<AuthorizationListSelect>(companyKey: companyKey, procedureName: "ProAuthorizationList", parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> GetAuthorizationListDetails(AuthorizationListDetails input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<AuthorizationListDetails>(companyKey: companyKey, procedureName: "ProAuthorizationListDetails", parameter: input);
        }
        public Output UpdateAuthorizationApproveData(UpdateAuthorizationApprove input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAuthorizationApprove>(parameter: input, companyKey: companyKey, procedureName: "ProAuthorizationApproveUpdate");
        }
        public Output UpdateAuthorizationRejectData(UpdateAuthorizationReject input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAuthorizationReject>(parameter: input, companyKey: companyKey, procedureName: "ProAuthorizationRejectUpdate");
        }
        public Output DeleteAuthorizationLevelData(DeleteAuthorizationLevel input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAuthorizationLevel>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AuthorizationModule> GetAuthorizationModuleData(AuthorizationModuleSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<AuthorizationModule, AuthorizationModuleSelect>(companyKey: companyKey, procedureName: "ProAuthorizationModuleList", parameter: input);
        }
        public APIGetRecordsDynamic<AuthorizationAction> GetAuthorizationActionData(AuthorizationActionSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<AuthorizationAction, AuthorizationActionSelect>(companyKey: companyKey, procedureName: "ProAuthorizationAction", parameter: input);
        }
        public APIGetRecordsDynamic<AuthorizationAwaitHistory> GetAuthorizationAwaitHistoryData(AuthorizationAwaitHistorySelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<AuthorizationAwaitHistory, AuthorizationAwaitHistorySelect>(companyKey: companyKey, procedureName: "ProAuthorizationAwaitHistory", parameter: input);
        }
        public Output UpdateAuthorizationCorrectionData(UpdateAuthorizationCorrection input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAuthorizationCorrection>(parameter: input, companyKey: companyKey, procedureName: "ProAuthorizationCorrectionUpdate");
        }
    }
}


