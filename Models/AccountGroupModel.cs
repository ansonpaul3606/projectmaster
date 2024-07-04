/*----------------------------------------------------------------------
Created By	: Kavya
Created On	: 17/10/2022
Purpose		: AccountGroup
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

namespace PerfectWebERP.Models
{
    public class AccountGroupModel
    {
        public class AccountGroupList
        {
            public int SortOrder { get; set; }
        }

        public class GetAccountGroup
        {
            public long ID_AccountGroup { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_AccountGroup { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class AccountGroupView
        {
            public long SlNo { get; set; }
            public long ID_AccountGroup { get; set; }
            public string AGName { get; set; }
            public string AGShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public byte AccType { get; set; }
            public byte AccGroupType { get; set; }
            public List<FinalAccountType> FinalAccountType { get; set; }
            public List<FinalAccountGroup> FinalAccountGroup { get; set; }
            public Int64 TotalCount { get; set; }
            public bool Trading { get; set; }
            public bool ProfitLoss { get; set; }
            public bool BalanceSheet { get; set; }
            public List<SubGroupType> SubGroupType { get; set; }
            public long AGFinalAccGroupSubType { get; set; }
        }

        public class AccountGroupViewData
        {
         
            public long SlNo { get; set; }
            public long ID_AccountGroup { get; set; }
            public string AGName { get; set; }          
            public string AGShortName { get; set; }
            public Int32 SortOrder { get; set; }          
            public byte AGFinalAccType { get; set; }          
            public byte  AGFinalAccGroupType { get; set; } 
            public long ReasonID { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public bool Trading { get; set; }
            public bool ProfitLoss { get; set; }
            public bool BalanceSheet { get; set; }
            public long ?AGFinalAccGroupSubType { get; set; }

        }
        
        public class AccountGroupTypeView
        {
        public Int32 FK_AccType { get; set; }
            public List<AccountGroupType> AccountGroupType { get; set; }
            public List<AccountGroupType> result { get; set; }
        }
        public class UpdateAccountGroup
        {
            public long ID_AccountGroup { get; set; }
            public long FK_AccountGroup { get; set; }
            public string AGName { get; set; }
            public string AGShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public byte AGFinalAccType { get; set; }
            public byte AGFinalAccGroupType { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public bool Trading { get; set; }
            public bool ProfitLoss { get; set; }
            public bool BalanceSheet { get; set; }
            public long AGFinalAccGroupSubType { get; set; }

        }
        public static string _deleteProcedureName = "ProAccountGroupDelete";
        public static string _updateProcedureName = "ProAccountGroupUpdate";
        public static string _selectProcedureName = "ProAccountGroupSelect";


        public class AccountGroupInfoView
        {
            public long ID_AccountGroup { get; set; }
            public long FK_AccountGroup { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long ReasonID { get; set; }
        }

        public class DeleteAccountGroup
        {
            public long FK_AccountGroup { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }

        public class FinalAccountTypeMode
        {
            public Int32 Mode { get; set; }
        }
      

        public class FinalAccountType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class FinalAccountGroup
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class AccountGroupType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
            public Int32 ModeParent { get; set; } = 0;
        }
        public class SubGroupType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
            public Int32 ModeParent { get; set; }
        }


        public Output UpdateAccountGroupData(UpdateAccountGroup input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAccountGroup>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteAccountGroupData(DeleteAccountGroup input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAccountGroup>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AccountGroupView> GetAccountGroupData(GetAccountGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountGroupView, GetAccountGroup>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<FinalAccountType> GetAccountTypeList(FinalAccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<FinalAccountType, FinalAccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<FinalAccountGroup> GetAccountGroupList(FinalAccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<FinalAccountGroup, FinalAccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<AccountGroupType> FillAccountGroup(FinalAccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountGroupType, FinalAccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<SubGroupType> GetAccountSubGroupList(FinalAccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubGroupType, FinalAccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

    }
}
