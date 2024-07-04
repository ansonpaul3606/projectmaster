/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 14/11/2022
Purpose		: AccountHead
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
    public class AccountHeadModel
    {
        public class AccountHeadViewList
        {
            public int SortOrder { get; set; }
            public List<AccountGroup> AccountGroup { get; set; }
            public List<AccountSubGroup> AccountSubGroup { get; set; }
            public List<AccountType> AccountType { get; set; }
            public List<AccountVoucherGrp> AccountVoucher { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            public List<BranchHead> branchHeads { get; set; }
        }
        public class AccountHeadView
        {
            public long SlNo { get; set; }
            public long ID_AccountHead { get; set; }
            public string AHName { get; set; }
            public string AHShortName { get; set; }
            public string AHVoucherGrouping { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountGroup { get; set; }
            public long FK_AccountSubGroup { get; set; }
            public Int64 AHAccountMode { get; set; }
            public long ReasonID { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public long FK_AccountHead { get; set; }
            public long TaxGroupID { get; set; }
            public long FK_Branch { get; set; }
        }

       
        public class AccountGroupViewData
        {          
            public byte AGFinalAccType { get; set; }
            public byte AGFinalAccGroupType { get; set; }        

        }

        public class AccountGroup
        {
            public Int32 ID_AccountGroup { get; set; }
            public string AGName { get; set; }
        }

        public class AccountSubGroup
        {
            public Int32 ID_AccountSubGroup { get; set; }
            public string ASGName { get; set; }

        }
        public class GrpListNew
        {
            public int Sort { get; set; }
            public IEnumerable<GrpList> GrpData { get; set; }
        }
        public class GrpList
        {
            public string Name { get; set; }
        }
      
        public class UpdateAccountHead
        {
            public long ID_AccountHead { get; set; }
            public long FK_AccountHead { get; set; }
            public string AHName { get; set; }
            public string AHShortName { get; set; }
            public string AHVoucherGrouping { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountGroup { get; set; }
            public long FK_AccountSubGroup { get; set; }
            public long AHAccountMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long UserAction { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long TaxGroupID { get; set; }
            public long FK_Branch { get; set; }
            
        }

        public class GetAccountHead
        {
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_AccountHead { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

        public class AccountHeadViewData
        {
            public long FK_AccountGroup { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public long FK_AccountSubGroup { get; set; }
        }

        public class AccountSubGroupView
        {
            public int SlNo { get; set; }
            public long ID_AccountSubGroup { get; set; }
            public long FK_AccountGroup { get; set; }
            public string ASGName { get; set; }
            public string TransMode { get; set; }
            public int TotalCount { get; set; }
        }
        public class DeleteAccount
        {
            public long ID_AccountHead { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
            public long Debug { get; set; }
            public long ReasonID { get; set; }
        }
        public class DeleteAccountHead
        {
            public long ID_AccountHead { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
            public long Debug { get; set; }
        }

        public class AccountHeadID
        {
            public long ID_AccountHead { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class AccountTypeMode
        {
            public Int32 Mode { get; set; }
        }


        public class AccountType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

        public class AccountVoucherGrpMode
        {
            public Int32 Mode { get; set; }
        }


        public class AccountVoucherGrp
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class TaxGroup
        {
            public long TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class BranchHead
        {
            public long FK_Branch { get; set; }
            public string BranchName { get; set; }
        }
        public class InputBranch
        {
            public long FK_Company { get; set; }
        }
        public static string _deleteProcedureName = "ProAccountHeadDelete";
        public static string _updateProcedureName = "ProAccountHeadUpdate";
        public static string _selectProcedureName = "ProAccountHeadSelect";

      
        public Output UpdateAccountHeadData(UpdateAccountHead input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAccountHead>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteAccountHeadData(DeleteAccountHead input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAccountHead>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AccountHeadView> GetAccountHeadData(AccountHeadID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountHeadView, AccountHeadID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<AccountType> GetAccountTypeList(AccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountType, AccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<AccountVoucherGrp> GetAccountVoucherGrpngList(AccountVoucherGrpMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountVoucherGrp, AccountVoucherGrpMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<AccountSubGroupView> GetSubGroupList(AccountHeadViewData input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountSubGroupView, AccountHeadViewData>(companyKey: companyKey, procedureName: "ProAccountSubGroupSelectByGroup", parameter: input);
        }
        public APIGetRecordsDynamic<BranchHead> GetBranchHead(InputBranch input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchHead, InputBranch>(companyKey: companyKey, procedureName: "ProSelectAccountHeadBranch", parameter: input);
        }

    }
}


