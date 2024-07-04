/*----------------------------------------------------------------------
Created By	: NIJI TJ
Created On	: 23/11/2023
Purpose		: EMIPlan
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
    public class EMIPlanModel
    {

        public class EMIPlanView
        {
           public Int64 SlNo { get; set; }
            public string CedEMINo { get; set; }
            public string TransMode { get; set; }
           public string Project { get; set; }
            public string CusNumber { get; set; }
            public string Customer { get; set; }
            public DateTime CedFirstInstDate { get; set; }
            public long FK_Category { get; set; }
            public long ID_CustomerWiseEMI { get; set; }
            public long FK_Master { get; set; }
            public long FK_FinancePlanType { get; set; }
            public decimal CedTotalAmount { get; set; }
            public decimal CedDownPayment { get; set; }
            public decimal CedAddAmount { get; set; }
            public string CedInstAmount { get; set; }
            public long FK_AssignedTo { get; set; }
            public string EMIDetails { get; set; }
            public List<EMIPlan> EMIPlanList { get; set; }
            public List<EMIPlanDetails> EMIPlanDetailsList { get; set; }

            public Int64 TotalCount { get; set; }
           
        }
        public class EMIPlan
        {
            public long ID_FinancePlanType { get; set; }
            public string FpName { get; set; }
        }
        public class EMIPlanDetails
        {
            public long SLNo { get; set; }

            public DateTime EMIDate { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }
        }
        public class UpdateEMIPlan
        {
            public long UserAction { get; set; }
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public long FK_FinancePlanType { get; set; }
            public string CedEMINo { get; set; }
            public decimal CedTotalAmount { get; set; }
            public decimal CedDownPayment { get; set; }
            public decimal CedAddAmount { get; set; }
            public decimal CedInstAmount { get; set; }
            public DateTime CedFirstInstDate { get; set; }
            public decimal FK_AssignedTo { get; set; }
            public string EMIDetails { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Machine { get; set; }
        }
        public static string _deleteProcedureName = "ProProjectEMIDelete";
        public static string _updateProcedureName = "ProProjectEMIUpdate";
        public static string _selectProcedureName = "ProProjectEMISelect";

        public class DeleteEMIPlan
        {
            public string TransMode { get; set; }
            public long FK_CustomerWiseEMI { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class ProjectEMI
        {
            public decimal DownPayment { get; set; }
            public decimal InstalmentAmt { get; set; }
            public decimal AddnAmount { get; set; }

        }
        public class ProjectEMISlab
        {
            public long SLNo { get; set; }
            public string EMIDate { get; set; }
            public decimal Amount { get; set; }
            public string Remarks { get; set; }

        }
        
        public class ProjectEMIInput
        {
            public long FK_Category { get; set; }
            public decimal Amount { get; set; }
            public long FK_FInancePlan { get; set; }

        }
        public class ProjectEMISlabInput
        {
            public long FK_FInancePlan { get; set; }

            public DateTime Date { get; set; }
            public decimal Downpayment { get; set; }
            public decimal AdditionalAmount { get; set; }
            public decimal Installment { get; set; }
            public decimal BillAmount { get; set; }

        }
        
        public class ProjectEMIPlanID
        {
            public string TransMode { get; set; }
            public Int64 FK_CustomerWiseEMI { get; set; }
            public int PageIndex { get; set; } 
            public Int64 FK_Company { get; set; } 
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; } 
            public int PageSize { get; set; }
            public int Detailed { get; set; }
            public string Name { get; set; } 
        }
        public Output UpdateEMIPlanData(UpdateEMIPlan input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEMIPlan>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteEMIPlanData(DeleteEMIPlan input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEMIPlan>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<EMIPlanView> GetEMIPlanData(ProjectEMIPlanID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EMIPlanView, ProjectEMIPlanID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ProjectEMISlab> GetEMIPlanSlabDetails(ProjectEMIPlanID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectEMISlab, ProjectEMIPlanID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ProjectEMI> FillProjectEMI(ProjectEMIInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectEMI, ProjectEMIInput>(companyKey: companyKey, procedureName: "ProFindProjectEMI", parameter: input);
        }
        public APIGetRecordsDynamic<ProjectEMISlab> GetEMISlabDetails(ProjectEMISlabInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectEMISlab, ProjectEMISlabInput>(companyKey: companyKey, procedureName: "ProEMICalculate", parameter: input);
        }

    }
}

 
