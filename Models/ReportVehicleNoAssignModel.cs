using System;
using PerfectWebERP.General;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ReportVehicleNoAssignModel
    {

        public class VehicleNoassignreportview
        {
           // public long FK_Mode { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime FromDate { get; set; }
            public long FK_Vehicle { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long VehicleID { get; set; }
            public string VehicleName { get; set; }
            public string BranchID { get; set; }

        }

        public class VehiclelistNoassignReportInput
        {
            //public long FK_Mode { get; set; }
            public DateTime AsonDate { get; set; }
           
            public long FK_Vehicle { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }


        }
        public class VehiclelistNoassignOutputView
        {
            public long SLNo { get; set; }      
            public string Vehicle { get; set; }
            public string Number { get; set; }
            public string Category { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string FuelType { get; set; }

        }

        public APIGetRecordsDynamic<VehiclelistNoassignOutputView> GetVehiclelistNoassignReport(VehiclelistNoassignReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<VehiclelistNoassignOutputView, VehiclelistNoassignReportInput>(companyKey: companyKey, procedureName: "ProRptVehiclewithNoAssignmentListReport", parameter: input);

        }

    }
}