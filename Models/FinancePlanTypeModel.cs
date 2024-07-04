using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class FinancePlanTypeModel
    {
        public class FinancePlanList
        {

            public long SlNo { get; set; }
            public Int16 FinancePlanID { get; set; }
            public string FinancePlanName { get; set; }
            public string FinancePlanShortName { get; set; }
            public int ProductID { get; set; }
            public int FinanceInstallmentPeriod { get; set; }
            public int FinanceDuration { get; set; }
            public string FinancePeriodType { get; set; }
            public string FinancePeriod { get; set; }
            //public int FinanceAddCostMethod { get; set; }
            //public int FinanceDownPayMethod { get; set; }
            //public int FinanceFineMethod { get; set; }
            //public int FinanceFineCalcMethod { get; set; }
            public List<Services> ServiceList { get; set; }
            public List<PeriodType> PerdList { get; set; }
            public Int64 TotalCount { get; set; }
            public String ProductName { get; set; }
            public string TransMode { get; set; }
        }

        public class PeriodType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Services
        {
            public Int16 ServiceID { get; set; }
            public string ServiceList { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
       
        public class FinancePlanUpdate
        {
            public Int16 ID_FinancePlanType { get; set; }
            public string FpName { get; set; }
            public string FpShortName { get; set; }
            //public int FK_Product { get; set; }
            public string FpPeriodType { get; set; }
            public int FpPeriod { get; set; }
            public int FpDuration { get; set; }
            //public int FpAddlCostMethod { get; set; }
            //public int FpDownPayMethod { get; set; }
            //public int FpFineMethod { get; set; }
            //public int FpFineCalcMethod { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public Int16 FK_FinancePlanType { get; set; }

        }
        public class ID
        {

            public Int16 FK_FinancePlanType { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public class FinancePlanDelete
        {
            public string TransMode { get; set; }
            public Int16 FK_FinancePlanType { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }

        public class FinanceModelView
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
        }

        public APIGetRecordsDynamic<PeriodType> GetPerdTypList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodType, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public static string _updateProcedureName = "ProFinancePlanTypeUpdate";
        public Output UpdateFinancePlanData(FinancePlanUpdate input, string companyKey)
        {
            return Common.UpdateTableData<FinancePlanUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public APIGetRecordsDynamic<FinancePlanList> GetFinancePlanData(ID input, string companyKey)
        {
            return Common.GetDataViaProcedure<FinancePlanList, ID>(companyKey: companyKey, procedureName: "ProFinancePlanTypeSelect", parameter: input);

        }

    }
}