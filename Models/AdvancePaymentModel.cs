using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class AdvancePaymentModel
    {

        public class AdvancePaymentView
        {
            public long SlNo { get; set; }

            public long ID_SalaryAdvancePayment { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_AllowanceType { get; set; }
            public string EmployeeName { get; set; }
            public long FK_EMployee { get; set; }
            public long FK_SalaryAdvancePayment { get; set; }
            public decimal SalAdvAmount { get; set; }
            public decimal SalMonthlyRecAmount { get; set; }

            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public List<BranchTo> BranchListTo { get; set; }
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public List<AllowanceTypeList> AllowanceTypeList { get; set; }


            public long ReasonID { get; set; }

            public Int64 TotalCount { get; set; }

        }

        public class BranchTo
        {
            public Int32 FK_Branch { get; set; }
            public string BranchNameTo { get; set; }

        }
        public class DepartmentTo
        {
            public Int32 FK_Department { get; set; }
            public string DepartmentNameTo { get; set; }

        }

        public class EmployeeTo
        {
            public Int32 FK_EMployee { get; set; }
            public string EmployeeName { get; set; }
        }

        public class AllowanceTypeList
        {
            public Int32 FK_AllowanceType { get; set; }
            public string Allowancetype { get; set; }
        }
        public class AdvancePaymentUpdate
        {
            public long ID_SalaryAdvancePayment { get; set; }
            public long FK_EMployee { get; set; }
            public DateTime TransDate { get; set; }
            public DateTime EffectDate { get; set; }
            public long Debug { get; set; }
            public string TransMode { get; set; }
            public long FK_Machine { get; set; }
            public long FK_SalaryAdvancePayment { get; set; }
            public decimal SalAdvAmount { get; set; }
            public decimal SalMonthlyRecAmount { get; set; }
          
            public long FK_Company { get; set; }

            public long FK_AllowanceType { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public byte UserAction { get; internal set; }
        }

        public class AdvancePayment
        {
            public long FK_SalaryAdvancePayment { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }


        }
        public class DeleteAdvancePayment
        {
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_SalaryAdvancePayment { get; set; }

            public long FK_BranchCodeUser { get; set; }

        }
        public Output UpdateAdvancePaymentData(AdvancePaymentUpdate input, string companyKey)
        {
            return Common.UpdateTableData<AdvancePaymentUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        public Output DeleteAdvancePaymentData(DeleteAdvancePayment input, string companyKey)
        {
            return Common.UpdateTableData<DeleteAdvancePayment>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }

        public APIGetRecordsDynamic<AdvancePaymentView> GetAdvancePaymentData(AdvancePayment input, string companyKey)
        {
            return Common.GetDataViaProcedure<AdvancePaymentView, AdvancePayment>(companyKey: companyKey, procedureName: "ProSalaryAdvancePaymentSelect", parameter: input);

        }

        public static string _updateProcedureName = "ProSalaryAdvancePaymentUpdate";
        public static string _deleteProcedureName = "ProSalaryAdvancePaymentDelete";
        public static string _selectProcedureName = "ProSalaryAdvancePaymentSelect";

    }
}