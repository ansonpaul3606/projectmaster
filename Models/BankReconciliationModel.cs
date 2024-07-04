using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class BankReconciliationModel
    {
        public static string _deleteProcedureName = "";
        public static string _updateProcedureName = "ProAccountReconcilDataUpdate";
        public static string _selectProcedureName = "ProAccountReconcilDataFill";
        public class BankReconciliationNew
        {             
            public List<Branchs> Branch { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_Branch { get; set; }
            public int Mode { get; set; }
            public long FK_Company { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
        }        
        public class GetAccountReconcilData
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_AccountHead { get; set; }
            public long FK_Branch { get; set; }
            public int Mode { get; set; }            
            public long FK_Company { get; set; }
        }
        public class SetAccountReconcilData
        {
            public int SLNo { get; set; }
            public long ID_AccountHeadSubTransaction { get; set; }
            public string TransDate { get; set; }
            public string AhstParty { get; set; }
            public string TransType { get; set; }
            public string Remarks { get; set; }
            public decimal Amount { get; set; }
            public bool AhstReconciled { get; set; }
            public string ReconcilRemarks { get; set; }        

        }
        public class UpdateBankReconciliation
        {
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public string ReconciliationDtls { get; set; }
        }
        public class UpdateBankReconciliationData
        {
            public long FK_Company { get; set; }
            public List<ReconciliationDtls> ReconciliationDtls { get; set; }
        }
        public class ReconciliationDtls
        {
            public long ID_AccountHeadSubTransaction { get; set; }
            public bool AhstReconciled { get; set; }
            public string ReconcilRemarks { get; set; }
        }
        public APIGetRecordsDynamic<SetAccountReconcilData> GetAccountReconcilDataFill(GetAccountReconcilData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetAccountReconcilData, GetAccountReconcilData>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public Output UpdateAccountReconcil(UpdateBankReconciliation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateBankReconciliation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
    }
}