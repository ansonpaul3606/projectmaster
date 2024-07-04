using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AccountOpeningModel
    {
        public class AccountOPeningView
        {
            public List<FinalAccountGroup> FinalAccountGroup { get; set; }
            public List<BranchHead> branchHeads { get; set; }
            public List<AccountGroup> accountGroup { get; set; }
        }

        #region[InAndOut]

        public class inputAccGrp
        {
            public long AccGroupType { get; set; }
            public long FK_Company { get; set; }
        }
        public class AccountOpeningInput
        {
            public long AccGroupType { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_AccGroupType { get; set; }
            public long FinYear { get; set; }
        }

        public class UpdateDtainput
        {
            public long AccGroupType { get; set; }
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_AccGroupType { get; set; }
            public long FinYear { get; set; }
            public List<AccOpeningDetails> AccountOpeningDetails { get; set; }
            public string TransMode { get; set; }
            public long? LastID { get; set; }
            public decimal DebitTotal { get; set; }
            public decimal CreditTotal { get; set; }
        }

        public class AccOpeningDetails
        {
            public long ID_AccountHeadBalance { get; set; }
            public long FK_Branch { get; set; }
            public long BranchCode { get; set; }
            public string Branch { get; set; }
            public string Name { get; set; }
            public long FK_AccountHead { get; set; }
            public decimal CreditBalance { get; set; }
            public decimal DebitBalance { get; set; }
        }
        public class AccountOpeningOutPut
        {
          
            public long ID_AccountHeadBalance { get; set; }
            public long FK_Branch { get; set; }
            public long BranchCode { get; set; }
            public string Branch { get; set; }
            public string Name { get; set; }
            public long FK_AccountHead { get; set; }
            public decimal CreditBalance { get; set; }
            public decimal DebitBalance { get; set; }
        }

        public class UpdateAccountOpening {

            public long ID_AccountHeadBalance { get; set; }
            public int UserAction { get; set; }
            public string TransMode { get; set; }
            public int Debug { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string AccountOpeningDetails { get; set; }
            public long? LastID { get; set; }
            public long FinYear { get; set; }
        }
        #endregion

        #region[DropDown]
        public class FinalAccountGroup
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class FinalAccountTypeMode
        {
            public Int32 Mode { get; set; }
        }
        public class BranchHead
        {
            public long FK_Branch { get; set; }
            public string BranchName { get; set; }
        }
        public class AccountGroup
        {
            public long FK_AccGroup { get; set; }
            public string AccGroupName { get; set; }
        }
        public class InputBranch
        {
            public long FK_Company { get; set; }
        }

        #endregion

        #region[API]
        public APIGetRecordsDynamic<FinalAccountGroup> GetAccountGroupList(FinalAccountTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<FinalAccountGroup, FinalAccountTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<AccountOpeningOutPut> GetOpeningAccountDetails(AccountOpeningInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountOpeningOutPut, AccountOpeningInput>(companyKey: companyKey, procedureName: "ProGetOpeningAccountData", parameter: input);
        }

        public APIGetRecordsDynamic<AccountGroup> GetAccountGroupDetails(inputAccGrp input, string companyKey)
        {
            return Common.GetDataViaProcedure<AccountGroup, inputAccGrp>(companyKey: companyKey, procedureName: "ProSelectAccountGroupData", parameter: input);
        }

        public Output UpdateAccountOpeningdetails(UpdateAccountOpening input, string companyKey)
        {
            return Common.UpdateTableData<UpdateAccountOpening>(parameter: input, companyKey: companyKey, procedureName: "ProAccountOpeningnUpdate");
        }
        #endregion


    }

}