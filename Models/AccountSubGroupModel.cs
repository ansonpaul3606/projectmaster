/*----------------------------------------------------------------------
Created By	: Kavya
Created On	: 20/10/2022
Purpose		: Account Sub Group
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
    public class AccountSubGroupModel
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
            public List<AccountGroupType> AccountGroupType { get;set;}
        }

        public class AccountGroupType
        {
            public Int32 ID_AccountGroup { get; set; }
            public string AGName { get; set; }

        }
        public class AccountSubGroupViewData
        {

            public long SlNo { get; set; }
            public long ID_AccountSubGroup { get; set; }
            public string ASGName { get; set; }
            public string ASGShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public byte FK_AccountGroup { get; set; }
            public long ReasonID { get; set; }
            public Int64 TotalCount { get; set; }
            public string TransMode { get; set; }

        }

        public class UpdateAccountSubGroup
        {
            public long ID_AccountSubGroup { get; set; }
            public long FK_AccountSubGroup { get; set; }
            public string ASGName { get; set; }
            public string ASGShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public byte FK_AccountGroup { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
        }

        public class AccountSubGroupInfoView
        {
            public long ID_AccountSubGroup { get; set; }
            public long FK_AccountSubGroup { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
        }

        public class DeleteAccountSubGroup
        {
            public long FK_AccountSubGroup { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }

        public class AccountSubGroupData
        {
            public long SlNo { get; set; }
            public long ID_AccountSubGroup { get; set; }
            public string ASGName { get; set; }
            public string ASGShortName { get; set; }
            public Int32 SortOrder { get; set; }
            public byte FK_AccountGroup { get; set; }
            public Int64 TotalCount { get; set; }
        }

        public class GetAccountSubGroup
        {
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long FK_AccountSubGroup { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

       

        public static string _deleteProcedureName = "ProAccountSubGroupDelete";
        public static string _updateProcedureName = "ProAccountSubGroupUpdate";
        public static string _selectProcedureName = "ProAccountSubGroupSelect";

        public Output UpdateAccountSubGroupData(UpdateAccountSubGroup input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAccountSubGroup>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }       

        public APIGetRecordsDynamic<AccountSubGroupData> GetAccountSubGroupData(GetAccountSubGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountSubGroupData, GetAccountSubGroup>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public Output DeleteAccountSubGroupData(DeleteAccountSubGroup input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAccountSubGroup>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
    }
}