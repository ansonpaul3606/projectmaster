using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PerfectWebERP.Models
{
    public class EMIDetailsGridModel
    {
        public class EmiDetails
        {
            public long FK_FinancePlanType { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public string EMINo { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Customer { get; set; }
            public long FK_Area { get; set; }
            public long FK_Category { get; set; }
            public int Demand { get; set; }
            public int Mode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int32 SMSMode { get; set; }
            public long FK_District { get; set; }
        }
        public class GetEmiDetails
        {
            public int Mode { get; set; }
            public long FK_FinancePlanType { get; set; }
            public long FK_Product { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long fk_Customer { get; set; }
            public string CedEMINo { get; set; }
            public long Status { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int32 Pageindex { get; set; }
            public Int32 PageSize { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Area { get; set; }
            public long FK_District { get; set; }
            public long FK_Category { get; set; }
            public int Demand { get; set; }
            public Int32 SMSMode { get; set; }
        }
        public class EmiViewList
        {
            public long FK_Employee { get; set; }
            public string UserCode { get; set; }
            public long Branch { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<FinancePlanType> FinancePlanlists { get; set; }
            public List<Branch> BranchList { get; set; }
            public string BrName { get; set; }
            public string CompName { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
        }


        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class Branch
        {
            public long ID_Branch { get; set; }
            public string BranchName { get; set; }
        }
        public class FinancePlanType
        {
            public Int32 FinancePlanTypeID { get; set; }
            public string FinanceName { get; set; }

        }
        public class EMIDetails
        {
            public long SLNo { get; set; }
            public long ID_CustomerWiseEMI { get; set; }
            public string EMINo { get; set; }
            public string CusNumber { get; set; }
            public string Customer { get; set; }
            public string CustomerAddress { get; set; }
            public string Mobile { get; set; }
            public string Product { get; set; }
            public DateTime StartDate { get; set; }
            public string FinancePlan { get; set; }
            public DateTime? LastTransaction { get; set; }
            public decimal NetAmount { get; set; }
            public decimal TotalPaid { get; set; }
            public decimal Balance { get; set; }
            public decimal DueAmount { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime NextEMIDate { get; set; }
            public string Remark { get; set; }
            public decimal FineAmount { get; set; }
            public int InstNo { get; set; }

            public int Masterid { get; set; }

            public string Field { get; set; }
            public int Value { get; set; }
            public Int64 TotalCount { get; set; }
           
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public static string _SearchProcedureName = "ProCustomerWiseEMIMonitoring";
        public APIGetRecordsDynamic<EMIDetails> GeEMIGridTodaysData(GetEmiDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIDetails, GetEmiDetails>(companyKey: companyKey, procedureName: _SearchProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public APIGetRecordsDynamicdn<DataTable> SendIndimation(GetEmiDetails input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<GetEmiDetails>(companyKey: companyKey, procedureName: _SearchProcedureName, parameter: input);
        }

    }
}