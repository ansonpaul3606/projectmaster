using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERP.General;
using System.Data;
using Newtonsoft.Json;


namespace PerfectWebERP.Models
{
    public class AuditTrialReportModel
    {
        public class AuditReportParams
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_MenuList { get; set; }
            public long FK_UserRole { get; set; }
            public string ReferenceNo { get; set; }
            public long FK_Company { get; set; }
            public long FK_Users { get; set; }
            public Int32 Action { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }

            //public List<ModuleList> ModuleList { get; set; }

        }
        public class AuditReportOutputView
        {
            public long SLNo { get; set; }
            public string Module { get; set; }
            public string SubModule { get; set; }
            public string AccountNo { get; set; }
            public string EnteredUser { get; set; }
            public DateTime CreatedTime { get; set; }
            public string TransactionDetails { get; set; }
            public string Voucher { get; set; }
            public Decimal Amount { get; set; }
            public string ModifiedUser { get; set; }
            public string OldValue { get; set; }
            public string ChangedValue { get; set; }
            public DateTime ModiFiedTime { get; set; }
            public string Action { get; set; }
            public long TransactionID { get; set; }
            public string Reason { get; set; }
            public string ApprovalStatus { get; set; }
            public string Comments { get; set; }
            public string ReferenceID { get; set; }
            public string ApprovalBy { get; set; }
        }

        public class auditlist
        {


            public string Companyname { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<Department> deprtmnt { get; set; }
            public List<MenuGroup> MenuGroup { get; set; }
            public List<MenuList> MenuList { get; set; }
            public List<UserRole> UserRole { get; set; }
            public List<Users> Users { get; set; }
            public List<ActionType> ActionType { get; set; }
            public List<ActiotypeList> ActiotypeList { get; set; }

        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class Department
        {
            public int DepId { get; set; }
            public string Depname { get; set; }

        }
        public class MenuGroup
        {
            public int ID_MenuGroup { get; set; }
            public string MnuGrpName { get; set; }

        }
        public class MenuList
        {
            public int ID_MenuList { get; set; }
            public string MnuLstName { get; set; }

        }
        public class UserRole
        {
            public int ID_UserRole { get; set; }
            public string UsrrlName { get; set; }

        }
        public class Users
        {
            public int ID_Users { get; set; }
            public string EmpFName { get; set; }

        }
        public class ActionType
        {
            public int ID_Action { get; set; }
            public string ActionName { get; set; }

        }
        public class ModeValue
        {
            public Int32 Mode { get; set; }
        }
        public class ActiotypeList
        {
            public Int32 ID_Action { get; set; }
            public string ActionName { get; set; }
        }
        public class UserList
        {
            public long ID_Users { get; set; }
            public string EmpFName { get; set; }
        }
        public class SubModule
        {
            public long ID_MenuList { get; set; }
            public string MnuLstName { get; set; }
        }
       
        public APIGetRecordsDynamic<ActiotypeList> GetActionListData(ModeValue input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActiotypeList, ModeValue>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<AuditReportOutputView> GetAuditTrailReport(AuditReportParams input, string companyKey)
        {
            return Common.GetDataViaProcedure<AuditReportOutputView, AuditReportParams>(companyKey: companyKey, procedureName: "ProRptAuditTrailReport", parameter: input);

        }
        
    }
}