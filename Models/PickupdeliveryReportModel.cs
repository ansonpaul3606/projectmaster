
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using PerfectWebERP.General;

namespace PerfectWebERP.Models
{
    public class PickupdeliveryReportModel
    {

        public class PickupdeliveryReportView
        {

            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public Int32 EmployeeID { get; set; }
            public string Employee { get; set; }
            public List<Reports> ReportsList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public string Companyname { get; set; }
            public List<Department> DepartmentList { get; set; }
            public Int32 ID_Report { get; set; }
            public long Reporttype { get; set; }
            public Int32 BranchID { get; set; }
            public Int32 DepartmentID { get; set; }
            public Int32 ?AreaID { get; set; }
            public string Area { get; set; }
            public List<DeliveryList> DeliveryList { get; set; }
            public long? FK_Deliverytype { get; set; }
        }
        public class DeliveryList
        {
            public Int32 FK_Deliverytype { get; set; }
            public string Deliverytype { get; set; }
        }
        public class Reports
        {
            public Int32 ID_Report { get; set; }
            public string ReportsName { get; set; }
        }
        public class Branchs
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }
        }


        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class Area
        {
            public Int32 AreaID { get; set; }
            public string AreaName { get; set; }

        }
        public class Pickupdeliveryreportinput
        {
            public int ReportMode { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Employee { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Deliverytype { get; set; }
            public long AreaID { get; set; }


        }
        public class PickupdeliveryReportGridview
        {
            public string Date { get; set; }
            public string Branch { get; set; }
            public string Department { get; set; }
            public string EmployeeCode { get; set; }
            public string Employee { get; set; }
            public string Designation { get; set; }
            public string Area { get; set; }
           
            public string Status { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
          
            public string DeliveryType { get; set; }
          


        }

        public APIGetRecordsDynamic<PickupdeliveryReportGridview> GetPayRollAttendanceDetailsData(Pickupdeliveryreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PickupdeliveryReportGridview, Pickupdeliveryreportinput>(companyKey: companyKey, procedureName: "ProPickupdeliveryGridView", parameter: input);

        }

    }
}