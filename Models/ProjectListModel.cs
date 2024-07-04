using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProjectListModel
    {
      
        public class ProjectListData
        {
           
            public long SlNo { get; set; }
       
            public long Branch { get; set; }
            public long FK_Customer { get; set; }
           
            public long FK_Employee { get; set; }
            public List<Branch> BranchList { get; set; }
            public string Product { get; set; }
            
            public long ID_Product { get; set; }
            public long FK_Product { get; set; }
            public DateTime StartingDate { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int64 TotalCount { get; set; }
            public DateTime? TicketDate { get; set; }

            public List<SiteVisitTransModes> SiteVisitTransMode { get; set; }
            public List<ProjectFlwUpTransModes> ProjectFlwUpTransMode { get; set; }

            public List<ProjectTransactionTransModes> ProjectTransactionTransMode { get; set; }
            public List<MaterialAllocationTransModes> MaterialAllocationTransMode { get; set; }
            public List<MaterialUsageTransModes> MaterialUsageTransModes { get; set; }
        }
        public class SiteVisitTransModes
        {
            public string TransMode { get; set; }
            public string MenulistName { get; set; }
        }
        public class ProjectFlwUpTransModes
        {
            public string TransMode { get; set; }
            public string MenulistName { get; set; }
        }
        public class ProjectTransactionTransModes
        {
            public string TransMode { get; set; }
            public string MenulistName { get; set; }
        }
        public class MaterialAllocationTransModes
        {
            public string TransMode { get; set; }
            public string MenulistName { get; set; }
        }
        public class MaterialUsageTransModes
        {
            public string TransMode { get; set; }
            public string MenulistName { get; set; }
        }
        public class Branch
        {
            public long ID_Branch { get; set; }

            public string BranchName { get; set; }
        }


        public class InputData
        {
            public string TicketNo { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long ID_Customerserviceregister { get; set; }
            public long ID_CustomerserviceregisterProductDetails { get; set; }
        }

        public class ProjectListInput
        {
            public long FK_Branch { get; set; }

            public long FK_Employee { get; set; }
           // public string EntrBy { get; set; }
           // public string FromDate { get; set; }
          //  public string ToDate { get; set; }
            public long FK_Company { get; set; }
          //  public string Content { get; set; }
           // public long FK_Category { get; set; }

          //  public long FK_AssignTo { get; set; }
            public long FK_Machine { get; set; }
            public Int16 Mode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public long FK_Project { get; set; }
            public Boolean Detailed { get; set; }

            
        }
        public class ProjectListView
        {
            public long SlNo { get; set; }
           
            public string ProjNumber { get; set; }

            public string ProjectNumber { get; set; }

            public string ProjectName { get; set; }
            public string ProjName { get; set; }

            public string Customer { get; set; }
            public string CustomerName { get; set; }

            public string ContactNumber{ get; set; }


            public string Address { get; set; }
            public string StartDate { get; set; }

            public string CurrentStage { get; set; }
            public string Duration { get; set; }
            public string Stage { get; set; }
            public string LeadNo { get; set; }

            public string Category { get; set; }

            public string ShortName { get; set; }

            public string CurrentStatus { get; set; }

            public long Value { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
      
            public string TransMode { get; set; }
            public long FK_Project { get; set; }
            public string Channel { get; set; }
            public string ProjectImage { get; set; }
            public Int64 TotalCount { get; set; }
            public string Label_Name { get; set; }

            public long Count { get; set; }
            public string TotalExpense { get; set; }
            public string TotalIncome { get; set; }
            public string LossProfit { get; set; }
            public long Mode { get; set; }
            public string ProjectView { get; set; }

        }

       
        public APIGetRecordsDynamic<ProjectListView> GetProjectList(ProjectListInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectListView, ProjectListInput>(companyKey: companyKey, procedureName: "ProProjectListview", parameter: input);

        }
       
        
   
    }

}