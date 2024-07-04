using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class OtherChargeReportModel
    {
        public class OtherChargeReportView
        {
            //public List<CategoryList> CategoryList { get; set; }
        }
        public class OtherChargeReportInput
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Branch { get; set; }
            public long OtherChargeType { get; set; }
            public long Module { get; set; }
            public int ImportID { get; set; }
            public long ID_Customer { get; set; }
            public long SupplierID { get; set; }
            public int ProdRptCriteria { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
          
            //public List<ModuleList> ModuleList { get; set; }

        }
        public class OtherChargeReportOutputView
        {
            public long SLNo { get; set; }
            public string TransDate { get; set; }
            public string Branch { get; set; }
            public string Module { get; set; }
            public string Debit { get; set; }
            public string Credit { get; set; }
            //public string Category { get; set; }
            //public string Brand { get; set; }
            //public string Model { get; set; }
            //public string Supplier { get; set; }
            //public decimal Amount { get; set; }
        }
        public class OtherChargeListModel
        {
            public List<ModuleList> ModuleList { get; set; }
            public List<OtherChargeType> OtherChargeTypeList { get; set; }
            public List<BranchList> BranchList { get; set; }
        }
        public class BranchList
        {
            public long BranchID { get; set; }
            public string Branch { get; set; }
        }
        public class OtherChargeType
        {
            public long OtherChargeTypeID { get; set; }
            public string OtherChargeTypeName { get; set; }
        }
        public class ModuleList
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public APIGetRecordsDynamic<OtherChargeReportOutputView> GetOtherChargeGeneralReport(OtherChargeReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherChargeReportOutputView, OtherChargeReportInput>(companyKey: companyKey, procedureName: "ProRptOtherChargeType", parameter: input);

        }
    }
}