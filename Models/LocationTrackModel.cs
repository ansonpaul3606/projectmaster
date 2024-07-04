using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERP.General;


namespace PerfectWebERP.Models
{
    public class LocationTrackModel
    {
        public class LocationTrackerView
        {
            public List<Branchs> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<DesignationList> DesignationList { get; set; }
            public string MapKey { get; set; }
            //public Int64 FK_Branch { get; set; }
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
        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }

        }
        public class InputDto
        {
            public Int64 FK_Company { get; set; }
            public Int64 FK_Designation { get; set; }
            public Int64 FK_Employee { get; set; }
            public Int64 FK_Department { get; set; }
            public Int64 FK_Branch { get; set; }
            public DateTime Date { get; set; }
            public string EntrBy { get; set; }
        }
        public class OutputDto
        {
            public string EmpFName { get; set; }
            public string LocLocationName { get; set; }
            public string EnteredDate { get; set; }
            public string EnteredTime { get; set; }
            public string ChargePercentage { get; set; }
            public string DeptName { get; set; }
            public string DesName { get; set; }
            public string BranchName { get; set; }

            public float LocLongitude { get; set; }
            public float LocLattitude { get; set; }
            //public string LocLongitude { get; set; }
            //public string LocLattitude { get; set; }
            public Int64 FK_Employee { get; set; }

        }
        public class inputRoute
        {
            public Int64 FK_Company { get; set; }
            public Int64 FK_Employee { get; set; }
            public DateTime Date { get; set; }
        }
        public class RouteOutputDto{

           


            public string  EmpFName { get; set; }
            public float LocLongitude { get; set; }
            public float LocLattitude { get; set; }
            public string LocLocationName { get; set; }
            public string EnteredDate { get; set; }
            public string EnteredTime { get; set; }
            public string ChargePercentage { get; set; }
            public Int64 FK_Employee { get; set; }



          
            //public string DeptName { get; set; }
            //public string DesName { get; set; }
            //public string BranchName { get; set; } }

        }
        public APIGetRecordsDynamic<OutputDto> GetLocationTrackerDetailsData(InputDto input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutputDto, InputDto>(companyKey: companyKey, procedureName: "ProEmployeeLocationSelect", parameter: input);

        }
        public APIGetRecordsDynamic<RouteOutputDto> GetLocationTrackerRouteDetails(inputRoute input, string companyKey)
        {
            return Common.GetDataViaProcedure<RouteOutputDto, inputRoute>(companyKey: companyKey, procedureName: "ProEmployeeWiseLocationSelect", parameter: input);

        }
    }
}