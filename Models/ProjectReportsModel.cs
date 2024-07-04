using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class ProjectReportsModel
    {
        public class customerview
        {
            public string FK_Employee { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Rptype { get; set; }
            public string ReportField { get; set; }
            public int Status { get; set; }

            //public Int16 SLNO { get; set; }
            //public string CustomerName { get; set; }
            //public string CustomerAddress { get; set; }
            //public string TicketNo { get; set; }
            //public string AssignedEmployee { get; set; }
            //public string Status { get; set; }
            //public string VisitDate { get; set; }  
        }
        public class customerid
        {
            public string FK_Employee { get; set; }
            //public string Rptype { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64 Status { get; set; }
            public string cusName { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class Status
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class StatusViewList
        {
            public List<Status> StatusList { get; set; }
            
        }
        public class ReportFields
        {
            public Int32 Report_Id { get; set; }
            public string ReportField { get; set; }
            public ReportFields()
            {
                Report_Id = 1;
                ReportField = "test1";
                Report_Id = 2;
                ReportField = "test2";
            }

        }
        public class Customerlist
        {
            public string CompName { get; set; }
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            //public int EmployeeID { get; set; }
            //public string Employeename { get; set; }
            //public string DeptName { get; set; }
            //public string Designation { get; set; }
            public long FK_Employee { get; set; }
            public List<EmployeeInfo> EmployeeInfoList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<GetProduct> ProductList { get; set; }
            public List<ProjectReportsModel.EmployeeLeadInfo> EmployeeLeadInfoList { get; set; }
             public List<NextAction> NextActionList { get; set; }
            public List<ActionTypes> Actntyplists { get; set; }
            public List<Status> StatusList { get; set; }
           public List<ReportFields>RpFields { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<StageList> StageList { get; set; }
            public List<Othercharge> OtherchargeList { get; set; }
            public List<PettyCashier> PettyCashierList { get; set; }
        }
        public class PettyCashierInput
        {
            public long FK_Project { get; set; }
            public Int32 Mode { get; set; }
            public long FK_Company { get; set; }

        }
        public class PettyCashier
        {
            public long ID_PettyCashier { get; set; }
            public string PtyCshrName { get; set; }

        }  public class Othercharge
        {
            public int OtherchargeID { get; set; }
            public string OtherchargeName { get; set; }

        }
        public class StageList
        {
            public string Mode { get; set; }
            public long StageID { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }
        public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }


        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class EmployeeLeadInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public string EmpLName { get; set; }
            public long ID_Branch { get; set; }
            public long ID_BranchMode { get; set; }
            public long ID_BranchType { get; set; }
            public long FK_Department { get; set; }
            public string BTName { get; set; }
            public string BrName { get; set; }
            public string DeptName { get; set; }

        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
           
        }
        public class GetProduct
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            //public string ProdShortName { get; set; }
            //public string ProdHSNCode { get; set; }
        }
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
        }
        public class ActionTypes
        {
            public int ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }
        }
        public class cusview
        {
            public Int16 SLNO { get; set; }
            public string CustomerName { get; set; }
            public string CustomerAddress { get; set; }
            public string TicketNo { get; set; }
            public string AssignedEmployee { get; set; }
            public string Status { get; set; }
            public string VisitDate { get; set; }
            public string CompanyName { get; set; }
            public string BranchName { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public byte[] img { get; set; }
            public byte[] footer { get; set; }
            public byte[] wmark { get; set; }
        }
       
        public APIGetRecordsDynamicdn<DataTable> GetCustomerReport(customerid input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<customerid>(companyKey: companyKey, procedureName: "ProRptTicketList", parameter: input);

        }
        
        public APIGetRecordsDynamic<PettyCashier> GePettyCashierList(PettyCashierInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PettyCashier, PettyCashierInput>(companyKey: companyKey, procedureName: "ProGetBillType", parameter: input);

        }
        public APIGetRecordsDynamic<Status> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Status, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

    }
}