using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PerfectWebERP.Models
{
    public class AccountSubHeadModel
    {
        public class AccountSubGroupView
        {
            public long ID_AccountSubGroup { get; set; }
            public string ASGName { get; set; }
            public string ASGShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public byte AccType { get; set; }
            public byte AcGroupType { get; set; }
            public Int64 TotalCount { get; set; }
            public List<AccountGroup> AccountGroup { get; set; }
            public List<AccountSubGroup> AccountSubGroup { get; set; }
            public List<AccountHead> AccountHead { get; set; }
            public List<BranchHead> branchHeads { get; set; }
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
        public class AccountHead
        {
            public Int32 ID_AccountHead { get; set; }
            public string AHName { get; set; }
        }
        public class AccountSubHeadViewList
        {
            public long SlNo { get; set; }
            public long ID_AccountSubHead { get; set; }
            public string ASHName { get; set; }
            public string ASHShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountHead { get; set; }
            public long ASHFrom { get; set; }
            public long ReasonID { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }
            public long FK_AccountSubHead { get; set; }
            public long FK_Master { get; set; }
            public long FK_Branch { get; set; }
        }
        public class UpdateAccountSubHead
        {
            public long ID_AccountSubHead { get; set; }
            public long FK_AccountSubHead { get; set; }
            public string ASHName { get; set; }
            public string ASHShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountHead{ get; set; }
            public long ASHFrom { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long UserAction { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public long FK_Branch { get; set; }
        }

        public class DeleteAccountSubHead
        {
            public long ID_AccountSubHead { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
            public long Debug { get; set; }
        }

        public class AccountSubHeadView
        {
            public Int16 SlNo { get; set; }
            public long ID_AccountSubHead { get; set; }
            public Int32 ASHCode { get; set; }
            public string ASHName { get; set; }
            public string ASHShortName { get; set; }
            public byte ASHFrom { get; set; }
            public byte Master { get; set; }
            public Int32 SortOrder { get; set; }
            public long FK_AccountHead { get; set; }
            public Int64 TotalCount { get; set; }
            public string AccountHeadName { get; set; }
            public long FK_Master { get; set; }
            public long FK_Branch { get; set; }
        }
        public class DeleteAccountSub
        {
            public long ID_AccountSubHead { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
            public long Debug { get; set; }
            public long ReasonID { get; set; }
        }
    

        public static string _deleteProcedureName = "ProAccountSubHeadDelete";
        public static string _updateProcedureName = "ProAccountSubHeadUpdate";
        public static string _selectProcedureName = "ProAccountSubHeadSelect";

        public class AccountSubHeadID
        {
            public Int64 ID_AccountSubHead { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
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
        public Output UpdateAccountSubHeadData(UpdateAccountSubHead input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAccountSubHead>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteAccountSubHeadData(DeleteAccountSubHead input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAccountSubHead>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<AccountSubHeadView> GetAccountSubHeadData(AccountSubHeadID input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountSubHeadView, AccountSubHeadID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<BranchHead> GetBranchHead(InputBranch input, string companyKey)
        {
            return Common.GetDataViaProcedure<BranchHead, InputBranch>(companyKey: companyKey, procedureName: "ProSelectAccountHeadBranch", parameter: input);
        }
    }
}