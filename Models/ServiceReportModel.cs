using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class ServiceReportModel
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
            public List<CustomerTicketsModel.EmployeeLeadInfo> EmployeeLeadInfoList { get; set; }
            public List<ComplaintType> ComplaintTypeList { get; set; }
            public List<ActionTypes> Actntyplists { get; set; }
            public List<Status> StatusList { get; set; }
            public List<Categorydata> categorytyps { get; set; }
            public List<ReportFields> RpFields { get; set; }
            public List<NextAction> NextActionList { get; set; }
            public List<ServiceType> ServiceTypeList { get; set; }
            public List<ReplacementType> ReplacementList { get; set; }
            public List<BillTypeType> BillModeList { get; set; }
            public List<CriteriaList> CriteriaList { get; set; }
            public long FK_BranchMode { get; set; }
        }
        public class ServiceType
        {
            public long ID_Service { get; set; }
            public string SeviceName { get; set; }
           
        }
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
            public long FK_ActionStatus { get; set; }
        }

        public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }


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
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class Status
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class ModeLead2
        {
            public Int32 Mode { get; set; }
        }
        public class Categorydata
        {
            public Int32 ID_Category { get; set; }
            public string CatName { get; set; }
        }
        public class StatusViewList
        {
            public List<Status> StatusList { get; set; }

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
        public class ComplaintType
        {
            public long ID_Complaintlist { get; set; }
            public string CompntName { get; set; }
        }
        public class ActionTypes
        {
            public int ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }
        }
        public class ReplacementType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class RplcmntMode
        {
            public Int32 Mode { get; set; }
        }
        public class BillTypeType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class RBillTypeMode
        {
            public Int32 Mode { get; set; }
        }
        public class CriteriaMode
        {
            public Int32 Mode { get; set; }
            public long Group { get; set; }
        }
        public class CriteriaList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public APIGetRecordsDynamic<Status> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<Status, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<Categorydata> GetCategorylist(ModeLead2 input, string companyKey)
        {
            return Common.GetDataViaProcedure<Categorydata, ModeLead2>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<ReplacementType> GetReplacemntList(RplcmntMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReplacementType, RplcmntMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<BillTypeType> GetBillTypeList(RBillTypeMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<BillTypeType, RBillTypeMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamic<CriteriaList> GetGroupCriteriaList(CriteriaMode input, string companyKey)
        {
            return Common.GetDataViaProcedure<CriteriaList, CriteriaMode>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}