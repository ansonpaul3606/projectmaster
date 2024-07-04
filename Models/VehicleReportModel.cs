using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class VehicleReportModel
    {

        public class VehicleReportView
        {
            public List<CategoryList> CategoryList { get; set; }
        }
        public class CategoryList
        {
            public long FK_Category { get; set; }
            public string CategoryName { get; set; }

        }
        public class VehicleReportInput
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public int FK_Mode { get; set; }
            public long FK_Category { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }

        }

        public class VehicleReportOutputView
        {
            public long SLNo { get; set; }
            public string VehicleNo { get; set; }
            public string PurchaseDate { get; set; }
            public string ChasisNo { get; set; }
            public string RegDate { get; set; }
            public string Manufacturer { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Supplier { get; set; }
            public decimal Amount { get; set; }
        }

        public APIGetRecordsDynamic<VehicleReportOutputView> GetVehicleGeneralReport(VehicleReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<VehicleReportOutputView, VehicleReportInput>(companyKey: companyKey, procedureName: "ProRptVehicleReport", parameter: input);

        }
    }
}