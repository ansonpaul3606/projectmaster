using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CostCenterModel
    {
        public class CostCenterView
        {
            public long FK_AssignedTo { get; set; }
            public long FK_CostCenter { get; set; }

            public List<CostcenterDetails> costDetails { get; set; }
        }
        public class CostcenterDetails
        {
            public string CCName { get; set; }
            public string CCShortName { get; set; }
            public int ReferanceExist { get; set; }
            public long ID_CostCenterDetails { get; set; }
        }
        public class CostcenterUpdate
        {

            public Int32 UserAction { get; set; }
            public Int32 Debug { get; set; }
            public string costDetails { get; set; }
            public long FK_AssignedTo { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_CostCenter { get; set; }
        }

        public class CostcenterInput
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public string Name { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_CostCenter { get; set; }
           
        }
        public class DeleteCostcenter
        {
            public long FK_CostCenter { get; set; }
            public Int32 Debug { get; set; }
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
        }
        public class CostCenterOutput
        {
            public long Slno { get; set;}
            public long TotalCount { get; set; }
            public string EmpName { get; set; }
            public long FK_Employee { get; set; }
            public long ID_CostCenter { get; set; }
        }
        public class DeleteInput
        {
            public long ReasonID { get; set; }
            public long FK_CostCenter { get; set; }
        }
        public class CostCenterSubOutput
        {
            public long FK_CostCenter { get; set; }
            public string CCName { get; set; }
            public string CCShortName { get; set; }
            public int ReferanceExist { get; set; }
            public long ID_CostCenterDetails { get; set; }
        }
        public Output Updatecostcenterdetails(CostcenterUpdate input, string companyKey)
        {
            return Common.UpdateTableData<CostcenterUpdate>(parameter: input, companyKey: companyKey, procedureName: "ProCostCenterUpdate");
        }
        public APIGetRecordsDynamic<CostCenterOutput> Getcostcenterdetails(CostcenterInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostCenterOutput, CostcenterInput>(companyKey: companyKey, procedureName: "ProCostCenterSelect", parameter: input);
        }
        public Output DeleteCostcenterdata(DeleteCostcenter input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCostcenter>(parameter: input, companyKey: companyKey, procedureName: "ProCostCenterDelete");
        }
        public APIGetRecordsDynamic<CostCenterSubOutput> Getcostcentersubdetails(CostcenterInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostCenterSubOutput, CostcenterInput>(companyKey: companyKey, procedureName: "ProCostCenterSelect", parameter: input);
        }

    }
}