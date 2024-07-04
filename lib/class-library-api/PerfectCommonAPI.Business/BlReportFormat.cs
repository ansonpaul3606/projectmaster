using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Business;
using PerfectWebERPAPI.Interface;
using System.Data;
using System.IO;
using System.Web.Hosting;
using System.Reflection;
namespace PerfectWebERPAPI.Business
{
   public class BlReportFormat
    {
        public static ProjectReportNameDetails ConvertProjectReportNameDetails(DataTable dt)
        {
            ProjectReportNameDetails log = new ProjectReportNameDetails();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectReportNameDetailsList = ConvertProjectReportNameDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectReportNameDetailsList> ConvertProjectReportNameDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectReportNameDetailsList()
                    {
                        ReportName = Convert.ToString(dr["ReportName"].ToString()),
                       ReportMode= Convert.ToString(dr["ReportMode"].ToString()),
                       

                    }).ToList();
        }
        public static ProjectReport ConvertProjectReport(DataTable dt)
        {
            ProjectReport log = new ProjectReport();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.SiteVisitList= ConvertSiteVisitList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<SiteVisitList> ConvertSiteVisitList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new SiteVisitList()
                    {
                        SlNo = Convert.ToString(dr["SlNo"].ToString()),
                        SiteVisitID = Convert.ToString(dr["SiteVisitID"].ToString()),
                        LeadGenerationID = Convert.ToString(dr["LeadGenerationID"].ToString()),
                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        VisitDate = Convert.ToString(dr["VisitDate"].ToString()),
                        VisitTime = Convert.ToString(dr["VisitTime"].ToString()),
                        Note1 = Convert.ToString(dr["Note1"].ToString()),
                        Note2 = Convert.ToString(dr["Note2"].ToString()),
                        CusNote = Convert.ToString(dr["CusNote"].ToString()),
                        ExpenseAmount = Convert.ToString(dr["ExpenseAmount"].ToString()),
                        Remarks = Convert.ToString(dr["Remarks"].ToString()),
                       
                    }).ToList();
        }
        public static ProjectReportDetail ConvertProjectReportDetail(DataTable dt)
        {
            ProjectReportDetail log = new ProjectReportDetail();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectStatusList = ConvertProjectStatusLists(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<ProjectStatusLists> ConvertProjectStatusLists(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectStatusLists()
                    {
                        SlNo = Convert.ToString(dr["SlNo"].ToString()),
                        ID_ProjectFollowUp = Convert.ToString(dr["ID_ProjectFollowUp"].ToString()),
                        FollowupDate = Convert.ToString(dr["Followup Date"].ToString()),
                        StatusDate = Convert.ToString(dr["Status Date"].ToString()),
                        FK_Project = Convert.ToString(dr["FK_Project"].ToString()),
                        Project = Convert.ToString(dr["Project"].ToString()),
                        FK_Stage = Convert.ToString(dr["FK_Stage"].ToString()),
                        Stage = Convert.ToString(dr["Stage"].ToString()),
                        Remarks = Convert.ToString(dr["Remarks"].ToString()),
                        ID_CurrentStatus = Convert.ToString(dr["ID_CurrentStatus"].ToString()),
                        CurrentStatus = Convert.ToString(dr["Current Status"].ToString()),
                        hdn_EntrOn = Convert.ToString(dr["hdn_EntrOn"].ToString()),
                        hdn_Groupby = Convert.ToString(dr["hdn_Groupby"].ToString()),
                    }).ToList();
        }
        public static ServiceNewList ConvertServiceNewList(DataTable dt)
        {
           ServiceNewList log = new ServiceNewList();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.NewList = ConvertNewList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<NewList> ConvertNewList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new NewList()
                    {
                        SlNo = Convert.ToString(dr["SlNo"].ToString()),
                        TicketNo = Convert.ToString(dr["Ticket No"].ToString()),
                        TicketDate = Convert.ToString(dr["Ticket Date"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        Complaint = Convert.ToString(dr["Complaint"].ToString()),
                        CurrentStatus = Convert.ToString(dr["Current Status"].ToString()),
                        Description = Convert.ToString(dr["Description"].ToString()),
                        Criteria = Convert.ToString(dr["hdn_Criteria"].ToString()),
                        Mobile = Convert.ToString(dr["hdn_Mobile"].ToString()),
                        Address = Convert.ToString(dr["hdn_Address"].ToString()),
                        Place = Convert.ToString(dr["hdn_Place"].ToString()),
                        Post = Convert.ToString(dr["hdn_Post"].ToString()),
                        Area = Convert.ToString(dr["hdn_Area"].ToString()),
                        District = Convert.ToString(dr["hdn_District"].ToString()),
                        Pincode = Convert.ToString(dr["hdn_Pincode"].ToString()),
                        Category = Convert.ToString(dr["hdn_Category"].ToString()),
                        Priority = Convert.ToString(dr["hdn_Priority"].ToString()),
                    }).ToList();
        }
        public static CategoryNameDetails ConvertCategoryNameDetails(DataTable dt)
        {
            CategoryNameDetails log = new CategoryNameDetails();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.CategoryNameList = ConvertCategoryNameList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<CategoryNameList> ConvertCategoryNameList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CategoryNameList()
                    {
                        CategoryMode = Convert.ToString(dr["ID_Category"].ToString()),
                        CategoryName = Convert.ToString(dr["CatName"].ToString()),
                       


                    }).ToList();
        }
        public static ProjectListDetail ConvertProjectListDetail(DataTable dt)
        {
            ProjectListDetail log = new ProjectListDetail();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectLists = ConvertProjectLists(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<ProjectLists> ConvertProjectLists(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectLists()
                    {
                        SlNo = Convert.ToString(dr["SlNo"].ToString()),
                        ProjectName = Convert.ToString(dr["Project Name"].ToString()),
                        Category = Convert.ToString(dr["Category"].ToString()),
                        LeadNumber = Convert.ToString(dr["Lead Number"].ToString()),
                        FinalAmount_R = Convert.ToString(dr["Final Amount_R"].ToString()),
                        StartDate = Convert.ToString(dr["Start Date"].ToString()),
                        DueDate = Convert.ToString(dr["Due Date"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        Duration = Convert.ToString(dr["Duration"].ToString()),
                        SubCategory = Convert.ToString(dr["hdn_SubCategory"].ToString()),
                        ShortName = Convert.ToString(dr["hdn_ShortName"].ToString()),
                        DurationType = Convert.ToString(dr["hdn_DurationType"].ToString()),
                        CreateDate  = Convert.ToString(dr["hdn_CreateDate"].ToString()),

                    }).ToList();
        }
        public static OutStanding ConvertOutStanding(DataTable dt)
        {
            OutStanding log = new OutStanding();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.OutStandingList = ConvertOutStandingList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<OutStandingList> ConvertOutStandingList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new OutStandingList()
                    {
                        SLNo = Convert.ToString(dr["SLNo"].ToString()),
                        TicketNo= Convert.ToString(dr["Ticket No"].ToString()),
                        TicketDate = Convert.ToString(dr["Ticket Date"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        ComplaintorService = Convert.ToString(dr["Complaint/Service"].ToString()),
                        Description = Convert.ToString(dr["Description"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        Area= Convert.ToString(dr["Area"].ToString()),
                        CurrentStatus = Convert.ToString(dr["CurrentStatus"].ToString()),
                        Criteria = Convert.ToString(dr["hdn_Criteria"].ToString()),
                       
                        Address = Convert.ToString(dr["hdn_Address"].ToString()),
                        Place = Convert.ToString(dr["hdn_Place"].ToString()),
                        Post = Convert.ToString(dr["hdn_Post"].ToString()),
                      
                        District = Convert.ToString(dr["hdn_District"].ToString()),
                        Pincode = Convert.ToString(dr["hdn_Pincode"].ToString()),
                        Category = Convert.ToString(dr["hdn_Category"].ToString()),
                        Priority = Convert.ToString(dr["hdn_Priority"].ToString()),
                        Due = Convert.ToString(dr["hdn_Due"].ToString()),

                    }).ToList();
        }
        //public static LeadDetailedReport ConvertLeadDetailedReport(DataTable dt)
        //{
        //    LeadDetailedReport log = new LeadDetailedReport();
        //    if (dt.Rows.Count > 0)
        //    {


        //        log.ResponseCode = "0";
        //        log.ResponseMessage = "Transaction Verified";
        //        log.DetailedList = ConvertDetailedList(dt);
        //        //log.ProjectStatusList = ConvertProjectStatusList(dt);

        //    }
        //    else
        //    {
        //        log.ResponseCode = "-2";
        //        log.ResponseMessage = "No Data Found";
        //    }
        //    return log;
        //}

        //public static List<DetailedList> ConvertDetailedList(DataTable dt)
        //{
        //    return (from DataRow dr in dt.Rows
        //            select new DetailedList()
        //            {
        //                //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
        //                EmployeeName = Convert.ToString(dr["EmpName"].ToString()),
        //                LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
        //                LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
        //                CategoryName = Convert.ToString(dr["CatName"].ToString()),
        //                ProductName = Convert.ToString(dr["ProdName"].ToString()),
        //               Quantity = Convert.ToString(dr["Quantity"].ToString()),
        //               NextAction= Convert.ToString(dr["NextAction"].ToString()),
        //                ActionType = Convert.ToString(dr["ActionType"].ToString()),
        //                AssignedTo = Convert.ToString(dr["AssignedTo"].ToString()),
        //                CustomerName= Convert.ToString(dr["AssignedTo"].ToString()),
        //                LeadStatus= Convert.ToString(dr["LeadStatus"].ToString()),
        //                LeadStatusOn= Convert.ToString(dr["LeadStatusOn"].ToString()),
        //                LeadStatusName = Convert.ToString(dr["LeadStatusName"].ToString()),
        //            }).ToList();
        //}
        //public static EmployeeWiseReport ConvertEmployeeWiseReport(DataTable dt)
        //{
        //    EmployeeWiseReport log = new EmployeeWiseReport();
        //    if (dt.Rows.Count > 0)
        //    {


        //        log.ResponseCode = "0";
        //        log.ResponseMessage = "Transaction Verified";
        //        log.EmployeeWiseList = ConvertEmployeeWiseList(dt);
        //        //log.ProjectStatusList = ConvertProjectStatusList(dt);

        //    }
        //    else
        //    {
        //        log.ResponseCode = "-2";
        //        log.ResponseMessage = "No Data Found";
        //    }
        //    return log;
        //}

        //public static List<EmployeeWiseList> ConvertEmployeeWiseList(DataTable dt)
        //{
        //    return (from DataRow dr in dt.Rows
        //            select new EmployeeWiseList()
        //            {
        //                //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
        //                EmployeeName = Convert.ToString(dr["EmpName"].ToString()),
        //               Opening = Convert.ToString(dr["Opening"].ToString()),
        //               New = Convert.ToString(dr["New"].ToString()),
        //               Closed = Convert.ToString(dr["Closed"].ToString()),
        //                Losed = Convert.ToString(dr["Losed"].ToString()),
        //                Balance = Convert.ToString(dr["Balance"].ToString()),
        //            }).ToList();
        //}
        //public static AssignedWiseReport ConvertAssignedWiseReport(DataTable dt)
        //{
        //    AssignedWiseReport log = new AssignedWiseReport();
        //    if (dt.Rows.Count > 0)
        //    {


        //        log.ResponseCode = "0";
        //        log.ResponseMessage = "Transaction Verified";
        //        log.AssignedList = ConvertAssignedListList(dt);
        //        //log.ProjectStatusList = ConvertProjectStatusList(dt);

        //    }
        //    else
        //    {
        //        log.ResponseCode = "-2";
        //        log.ResponseMessage = "No Data Found";
        //    }
        //    return log;
        //}

        //public static List<AssignedList> ConvertAssignedListList(DataTable dt)
        //{
        //    return (from DataRow dr in dt.Rows
        //            select new AssignedList()
        //            {
        //                //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
        //                AssignedTo= Convert.ToString(dr["AssignedTo"].ToString()),
        //                Opening = Convert.ToString(dr["Opening"].ToString()),
        //                New = Convert.ToString(dr["New"].ToString()),
        //                Closed = Convert.ToString(dr["Closed"].ToString()),
        //               Losed = Convert.ToString(dr["Losed"].ToString()),
        //                Balance = Convert.ToString(dr["Balance"].ToString()),
        //            }).ToList();
        //}
        //public static CategoryWiseReport ConvertCategoryWiseReport(DataTable dt)
        //{
        //    CategoryWiseReport log = new CategoryWiseReport();
        //    if (dt.Rows.Count > 0)
        //    {


        //        log.ResponseCode = "0";
        //        log.ResponseMessage = "Transaction Verified";
        //        log.CategoryWiseList = ConvertCategoryWiseList(dt);
        //        //log.ProjectStatusList = ConvertProjectStatusList(dt);

        //    }
        //    else
        //    {
        //        log.ResponseCode = "-2";
        //        log.ResponseMessage = "No Data Found";
        //    }
        //    return log;
        //}

        //public static List<CategoryWiseList> ConvertCategoryWiseList(DataTable dt)
        //{
        //    return (from DataRow dr in dt.Rows
        //            select new CategoryWiseList()
        //            {
        //                //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
        //                CategoryName = Convert.ToString(dr["CatName"].ToString()),
        //                Opening = Convert.ToString(dr["Opening"].ToString()),
        //                New = Convert.ToString(dr["New"].ToString()),
        //                Closed = Convert.ToString(dr["Closed"].ToString()),
        //                Losed = Convert.ToString(dr["Losed"].ToString()),
        //                Balance = Convert.ToString(dr["Balance"].ToString()),
        //            }).ToList();
        //}
        //public static ProductWiseReport ConvertProductWiseReport(DataTable dt)
        //{
        //    ProductWiseReport log = new ProductWiseReport();
        //    if (dt.Rows.Count > 0)
        //    {


        //        log.ResponseCode = "0";
        //        log.ResponseMessage = "Transaction Verified";
        //        log.ProductsList = ConvertProductsList(dt);
        //        //log.ProjectStatusList = ConvertProjectStatusList(dt);

        //    }
        //    else
        //    {
        //        log.ResponseCode = "-2";
        //        log.ResponseMessage = "No Data Found";
        //    }
        //    return log;
        //}

        //public static List<ProductsList> ConvertProductsList(DataTable dt)
        //{
        //    return (from DataRow dr in dt.Rows
        //            select new ProductsList()
        //            {
        //                //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
        //                ProductName = Convert.ToString(dr["ProdName"].ToString()),
        //                Opening = Convert.ToString(dr["Opening"].ToString()),
        //                New = Convert.ToString(dr["New"].ToString()),
        //                Closed = Convert.ToString(dr["Closed"].ToString()),
        //                Lost = Convert.ToString(dr["Lost"].ToString()),
        //                Balance = Convert.ToString(dr["Balance"].ToString()),
        //            }).ToList();
        //}
        public static Grouping ConvertGrouping(DataTable dt)
        {
            Grouping log = new Grouping();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.GroupList = ConvertGroupList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<GroupList> ConvertGroupList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new GroupList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                        GroupMode = Convert.ToInt32(dr["GroupMode"].ToString()),
                        GroupName = Convert.ToString(dr["GroupName"].ToString()),
                     
                    }).ToList();
        }

        ///---------------



        public static SummaryWiseLeadReport ConvertSummaryWiseLeadReport(DataTable dt,Byte Mode)
        {
            SummaryWiseLeadReport log = new SummaryWiseLeadReport();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.SummaryLeadList = ConvertSummaryLeadList(dt,Mode);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<SummaryLead> ConvertSummaryLeadList(DataTable dt, Byte mode)
        {

            /*
              5 - Employee wise Summry, 2 - Assigned wise Summry
	7 - Category Wise, 4 - Product Wise 0- Detailed
             */
            string type = "";
            string ID_Table = "";
            if (mode==5)
            {
                type = "Employee";
                ID_Table = "FK_Employee";
            }
           
            else if(mode==2)
            {
                type = "AssignedTo";
                ID_Table = "FK_AssignedTo";

            }
            else if (mode == 7)
            {
                type = "Category";
                ID_Table = "FK_Category";
            }
            else if (mode == 4)
            {
                type = "Product";
                ID_Table = "FK_Product";
            }
            else
            {
                type = "Employee";
                ID_Table = "FK_Employee";
            }

            return (from DataRow dr in dt.Rows
                    select new SummaryLead()
                    {
                        ID= Convert.ToInt64(dr[ID_Table].ToString()),
                        GroupName = Convert.ToString(dr[type].ToString()),
                        Opening = Convert.ToString(dr["Opening"].ToString()),
                        New = Convert.ToString(dr["New"].ToString()),
                        Closed = Convert.ToString(dr["Closed"].ToString()),
                        Lost = Convert.ToString(dr["Lost"].ToString()),
                        Balance = Convert.ToString(dr["Balance"].ToString()),
                    }).ToList();
        }

        public static ComplaintService ConvertComplaintService(DataTable dt)
        {
            ComplaintService log = new ComplaintService();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                if (Convert.ToInt64(dt.Rows[0]["SubMode"]) == 1)
                    log.ComplaintList = ConvertComplaintList(dt);
                else
                {
                    log.ServiceList = ConvertServiceList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<ComplaintList> ConvertComplaintList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ComplaintList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                        ID_ComplaintList = Convert.ToString(dr["ID_ComplaintList"].ToString()),
                        CompntName = Convert.ToString(dr["CompntName"].ToString()),
                       
                    }).ToList();
        }
        public static List<ServiceList> ConvertServiceList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ServiceList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                       
                        ID_Service = Convert.ToString(dr["ID_Service"].ToString()),
                        ServiceName = Convert.ToString(dr["ServiceName"].ToString()),
                    }).ToList();
        }

        public static TicketNo ConvertTicketNo(DataTable dt)
        {
            TicketNo log = new TicketNo();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.TicketList = ConvertTicketList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<TicketList> ConvertTicketList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new TicketList()
                    {
                        TicketNo = Convert.ToString(dr["TicketNo"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        Priority = Convert.ToString(dr["Priority"].ToString()),

                    }).ToList();
        }



        ///
        public static ServiceProfile ConvertServiceProfile(DataTable dt)
        {
            ServiceProfile log = new ServiceProfile();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.TicketLedgerList = ConvertTicketLedgerList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<TicketLedgerList> ConvertTicketLedgerList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new TicketLedgerList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                        TicketNo = Convert.ToString(dr["TicketNo"].ToString()),
                        TicketDate = Convert.ToString(dr["TicketDate"].ToString()),
                        Branch = Convert.ToString(dr["Branch"].ToString()),
                        CustomerNo = Convert.ToString(dr["CustomerNo"].ToString()),
                        CustomerName = Convert.ToString(dr["CustomerName"].ToString()),
                        CustomerAddress = Convert.ToString(dr["CustomerAddress"].ToString()),
                        Place = Convert.ToString(dr["Place"].ToString()),
                        District = Convert.ToString(dr["District"].ToString()),
                        Area = Convert.ToString(dr["Area"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        Landmark = Convert.ToString(dr["Landmark"].ToString()),
                        OtherMobile = Convert.ToString(dr["OtherMobile"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        Complaint = Convert.ToString(dr["Complaint"].ToString()),
                        CurrentStatus = Convert.ToString(dr["CurrentStatus"].ToString()),
                        Post = Convert.ToString(dr["Post"].ToString()),

                    }).ToList();
        }


        public static IntimationCategory ConvertIntimationCategory(DataTable dt)
        {
            IntimationCategory log = new IntimationCategory();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.IntimationCategoryList = ConvertIntimationCategoryList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<IntimationCategoryList> ConvertIntimationCategoryList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new IntimationCategoryList()
                    {
                        ID_Catg = Convert.ToString(dr["ID_Catg"].ToString()),
                        CatgName = Convert.ToString(dr["CatgName"].ToString()),
                        Project = Convert.ToString(dr["Project"].ToString()),
                     
                    }).ToList();
        }
        public static LeadSummaryDetailsReport ConvertLeadSummaryDetailsReport(DataTable dt)
        {
            LeadSummaryDetailsReport log = new LeadSummaryDetailsReport();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadSummaryDetailsList = ConvertLeadSummaryDetailsList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<LeadSummaryDetailsList> ConvertLeadSummaryDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadSummaryDetailsList()
                    {
                        
                        FK_Employee = Convert.ToInt64(dr["FK_Employee"].ToString()),
                        Employee = Convert.ToString(dr["Employee"].ToString()),
                        FK_Lead = Convert.ToInt64(dr["FK_Lead"].ToString()),
                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
                        Category = Convert.ToString(dr["Category"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        Quantity = Convert.ToString(dr["Quantity"].ToString()),
                        Nextaction = Convert.ToString(dr["Nextaction"].ToString()),
                        ActionType = Convert.ToString(dr["ActionType"].ToString()),
                        AssignedTo = Convert.ToString(dr["AssignedTo"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        LeadStatusOn = Convert.ToString(dr["Product"].ToString()),
                        LeadStatusName = Convert.ToString(dr["LeadStatusName"].ToString())

                    }).ToList();
        }



    }
}
