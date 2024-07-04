using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class MaterialRequestReportsModel
    {
        public class MaterialRequestReportView
        {
            public List<StageList> StageList { get; set; }
            public List<EmployeeList> EmployeeList { get; set; }
            public List<TeamList> TeamList { get; set; }
            public long FK_Team { get; set; }
            public long FK_Stages { get; set; }
            public string TeamName { get; set; }
            public string Stage { get; set; }
            public string Employee { get; set; }
            public long FK_Employee { get; set; }
            public long ProductID { get; set; }
            public string Product { get; set; }
            public long FK_Project { get; set; }
            public string Project { get; set; }
            public int FK_Mode { get; set; }

        }
        public class StageList
        {
            public string Mode { get; set; }
            public long StageID { get; set; }
            public string StageName { get; set; }
        }

        public class TeamList
        {
            public long ProjectID { get; set; }
            public long TeamID { get; set; }
            public string TeamName { get; set; }
        }
        public class EmployeeList
        {
            public long EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }
        public class MaterialRequestReportInput
        {
            public int FK_Mode { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stages { get; set; }
            public long FK_Team { get; set; }
            public long FK_Employee { get; set; }
            public long ProductID { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }






        }

        public class ProjectWiseMaterialDetailsOutputView
        {
            public long SLNo { get; set; }
            public string Project { get; set; }
            public string Stage { get; set; }
            public string Product { get; set; }
            public decimal Assigned { get; set; }
            public decimal Requested { get; set; }
            public decimal Allotted { get; set; }
            public decimal Used { get; set; }
            public decimal Damage { get; set; }
            public decimal Wastage { get; set; }
            public decimal Returned { get; set; }
            public decimal Excess { get; set; }
            public decimal Surplus { get; set; }




        }
        public class ProjectWiseMaterialDetailsInput
        {
         
            public DateTime AsOnDate { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
      
            public long FK_Company { get; set; }
           






        }
        public class MaterialRequestOutputView
        {
            public long SLNo { get; set; }
            public string RequestNo { get; set; }
            public string Project { get; set; }
            public string RequestDate { get; set; }
            public string AllocatedDate { get; set; }
            public string Stage { get; set; }
            public string Employee { get; set; }
            public string Team { get; set; }
            public string Product { get; set; }
            public string Quantity { get; set; }
            public string RequestQty { get; set; }
            public string AllocatedQty { get; set; }
            public string RequestedQty { get; set; }
            public string BalanceQty { get; set; }




        }






        public APIGetRecordsDynamic<MaterialRequestOutputView> GetMaterialGeneralReport(MaterialRequestReportInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialRequestOutputView, MaterialRequestReportInput>(companyKey: companyKey, procedureName: "ProRptProjectMaterialRequest", parameter: input);

        }
        public APIGetRecordsDynamic<ProjectWiseMaterialDetailsOutputView> GetProjectWiseMaterialDetailsReport(ProjectWiseMaterialDetailsInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectWiseMaterialDetailsOutputView, ProjectWiseMaterialDetailsInput>(companyKey: companyKey, procedureName: "ProRptProjectWiseMaterialDetails", parameter: input);

        }
    }
}







