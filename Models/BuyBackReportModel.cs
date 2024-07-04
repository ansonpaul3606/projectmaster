using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class BuyBackReportModel
    {
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class ModeList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class BuyBacklist
        {
            public string CompName { get; set; }
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public long FK_Employee { get; set; }            
            public List<Category> CategoryList { get; set; }
            public List<Branchs> BranchList { get; set; }            
            public long FK_BranchMode { get; set; }
            public List<ModeList> ModeList { get; set; }
        }
        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }

        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class InputBuybackReport
        {
            public Int32 ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public int Mode { get; set; }
        }
        public class GetBuybackReport
        {
            public Int32 ReportMode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public int Mode { get; set; }
            public string EntrBy { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int FK_Machine { get; set; }
            public int FK_Company { get; set; }
        }
        public APIGetRecordsDynamic<ModeList> GetBuyBackModeList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ModeList, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> GetBuybackReportdata(GetBuybackReport input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<GetBuybackReport>(companyKey: companyKey, procedureName: "ProRptBuyback", parameter: input);
        }
    }
}