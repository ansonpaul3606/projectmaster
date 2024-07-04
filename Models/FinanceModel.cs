using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class FinanceModel
    {
        public class FinancePlanList
        {
            public long SlNo { get; set; }
            public Int16 FinancePlanID { get; set; }
            public string FinancePlanName { get; set; }
            public int FinanceInstallmentPeriod { get; set; }
            public int FinanceDuration { get; set; }
            public int FinancePeriodType { get; set; }
            public int FinanceAddCostMethod { get; set; }
            public int FinanceDownPayMethod { get; set; }
            public int FinanceFineMethod { get; set; }
            public int FinanceFineCalcMethod { get; set; }
            public List<FinancePlanType> FinanceNameList { get; set; }
            public DateTime EffectDate { get; set; }
            public Int64 FinanceAddCostValue { get; set; }
            public Int64 FinanceDownPayValue { get; set; }
            public Int64 FinanceFineValue { get; set; }
            public Int64 FinanceFineCalcValue { get; set; }
            public Int64 FinanceFineGracePeriod{ get; set; }
            public Int64 FinanceFineGracePeriodValue { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadSub { get; set; }
            public long? FK_Product { get; set; }
            public Int64 TotalCount { get; set; }
            public List<Category> CategoryList { get; set; }
            public long Mode { get; set; }
        }


        public class FinancePlanType
        {
            public Int32 FinancePlanTypeID { get; set; }
            public string FinanceName { get; set; }

        }

        public class FinancePlanDetails
        {
            public Int32 FinancePlanTypeID { get; set; }
            public string FinancePlanName { get; set; }
            public int FinanceInstallmentPeriod { get; set; }
            public int FinanceDuration { get; set; }
            public string FinancePeriodType { get; set; }
            public DateTime EffectDate { get; set; }

        }
       

        public class FinanceViewList
        {
            public Int16 FK_FinancePlanTypeSettings { get; set; }
            public long SlNo { get; set; }
            public Int16 FinancePlanType { get; set; }
            public string FinancePlanName { get; set; }
            public int FinanceInstallmentPeriod { get; set; }
            public int FinanceDuration { get; set; }
            public string FinancePeriodType { get; set; }
            public int FinanceAddCostMethod { get; set; }
            public int FinanceDownPayMethod { get; set; }
            public int FinanceFineMethod { get; set; }
            public int FinanceFineCalcMethod { get; set; }
            public List<FinancePlanType> FinanceNameList { get; set; }
            public DateTime EffectDate { get; set; }
            public decimal FinanceAddCostValue { get; set; }
            public decimal FinanceDownPayValue { get; set; }
            public decimal FinanceFineValue { get; set; }
            public decimal FinanceFineCalcValue { get; set; }
            public Int64 FinanceFineGracePeriod { get; set; }
            public decimal FinanceFineGracePeriodValue { get; set; }
            public string ProductName { get; set; }
            public string AccountHeadName { get; set; }
            public long FK_AccountHeadAddAmount { get; set; }
            public string AccountHeadAddAmountName { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public long FK_AccountHeadFine { get; set; }
            public string FineAccountHeadName { get; set; }
            public long? FK_Category { get; set; }
            public long? FK_Product { get; set; }
            public long ProductID { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
            public long Mode { get; set; }
        }
        public class FinancePlanView
        {           
          
            public Int16 FK_FinancePlanTypeSettings { get; set; }         
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
         
        }
        public class UpdateFinancePlanTypeSettings
        {
            public long ID_FinancePlanTypeSettings { get; set; }
            public long FK_FinancePlanType { get; set; }
            public Int16 FK_FinancePlanTypeSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public Int64 FpstAddlCostMethod { get; set; }
            public decimal FpstAddlCostValue { get; set; }
            public Int64 FpstDownPayMethod { get; set; }
            public decimal FpstDownPayValue { get; set; }
            public Int64 FpstFineMethod { get; set; }
            public decimal FpstFinevalue { get; set; }
            public Int64 FpstFineCalcMethod { get; set; }
            public decimal FpstFineCalcvalue { get; set; }
            public Int64 FpstFineGracePrd { get; set; }
            public decimal FpstFineGracePrdvalue { get; set; }
            public long? FK_AccountHead { get; set; }
            public long? FK_AccountHeadAddAmount { get; set; }
            public long? FK_Product { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }          
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public long? FK_AccountHeadFine { get; set; }
            public long? FK_Category { get; set; }
           

        }

        public static string _updateProcedureName = "ProFinancePlanTypeSettingsUpdate";

        public class DeleteFinancePlanTypeSettings
        {
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_FinancePlanTypeSettings { get; set; }
        }


        public class FinancePlanTypeSettingsID
        {
            public Int64 ID_FinancePlanTypeSettings { get; set; }
        }
        public Output UpdateFinancePlanTypeSettingsData(UpdateFinancePlanTypeSettings input, string companyKey)
        {
            return Common.UpdateTableData<UpdateFinancePlanTypeSettings>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
       
        public APIGetRecordsDynamic<FinanceViewList> GetFinancePlanData(FinancePlanView input, string companyKey)
        {
            return Common.GetDataViaProcedure<FinanceViewList, FinancePlanView>(companyKey: companyKey, procedureName: "ProFinancePlanTypeSettingsSelect", parameter: input);

        }
          public class Category
        {
            public string Mode { get; set; }
            public long CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

    }

}

