using System;
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class DeliverylistreportModel
    {
        public class Deliveryview
        {
            public long FK_Mode { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime FromDate { get; set; }
            public long FK_Area { get; set; }
            public long FK_State { get; set; }
            public long FK_District { get; set; }
            public long ID_Product { get; set; }
            public long FK_Status { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public string ReportNames { get; set; }

        }

        public class DeliverylistReportInput
        {
            public long FK_Mode { get; set; }
            public DateTime ToDate { get; set; }       
            public DateTime FromDate { get; set; }       
            public long FK_Area { get; set; }
            public long FK_State { get; set; }
            public long FK_District { get; set; }
            public long FK_Product { get; set; }
            public long FK_Status { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }


        }
        public class DeliverylistOutputView
        {
            public long SLNo { get; set; }      
            public string Customer { get; set; }          
            public string Mobile { get; set; }
            public string State { get; set; }
            public string District { get; set; }
            public string Area { get; set; }
            public string AssignedDate { get; set; }
            public string AssignedTime { get; set; }
            public string Product { get; set; }
            public long Quantity { get; set; }
            public string Employee { get; set; }
            public string Vehicle { get; set; }
            public long DueDays { get; set; }
            public string TransDate { get; set; }
            public string ExpectedDeliveryDate { get; set; }
            public long ActualQuantity { get; set; }
            public long AssignedQuantity { get; set; }
            public long Pending { get; set; }
            public string OrderNumber { get; set; }
            public string OrderDate { get; set; }
            public string Location { get; set; }
            public long ID { get; set; }
            public string ExceptedDate { get; set; }
        }
        
        public APIGetRecordsDynamic<DeliverylistOutputView> GetDeliveryGeneralReport(DeliverylistReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<DeliverylistOutputView, DeliverylistReportInput>(companyKey: companyKey, procedureName: "ProRptDeliveryListReport", parameter: input);

        }

    }
}


   









