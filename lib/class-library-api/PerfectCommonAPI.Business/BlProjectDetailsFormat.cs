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
   public class BlProjectDetailsFormat
    {
        
        public static LeadList ConvertLeadDetails(DataTable dt)
        {
            LeadList log = new LeadList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadListDetails = ConvertLeadListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<LeadListDetails> ConvertLeadListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadListDetails()
                    {

                        ID_FIELD = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString()),
                        MobileNo = Convert.ToString(dr["Mobile"].ToString())

                    }).ToList();

        }
        public static ProjectList ConvertProjectDetails(DataTable dt)
        {
            ProjectList log = new ProjectList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectListDetails = ConvertProjectListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectListDetails> ConvertProjectListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectListDetails()
                    {

                        ID_Project = Convert.ToInt64(dr["ID_Project"].ToString()),
                        ProjName = Convert.ToString(dr["Name"].ToString()),
                        ShortName = Convert.ToString(dr["ShortName"].ToString()),
                        CreateDate = Convert.ToString(dr["CreateDate"].ToString()),
                        StartDate = Convert.ToString(dr["StartDate"].ToString()),
                        FinishDate = Convert.ToString(dr["FinishDate"].ToString()),
                        ProjectDate = Convert.ToString(dr["ProjectDate"].ToString()),
                        FinalAmount = Convert.ToDecimal(dr["FinalAmount"].ToString()),
                        DueDate = Convert.ToString(dr["FinishDate"].ToString()),
                        CustomerName = Convert.ToString(dr["CusName"].ToString()),
                        Address = Convert.ToString(dr["CusAddress1"].ToString()),
                        MobileNumber = Convert.ToString(dr["CusMobile"].ToString()),
                    }).ToList();

        }
        public static WorkTypeList ConvertWorkTypeDetails(DataTable dt)
        {
            WorkTypeList log = new WorkTypeList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.WorkTypeListDetails = ConvertWorkTypeListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<WorkTypeListDetails> ConvertWorkTypeListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new WorkTypeListDetails()
                    {

                        WorkTypeID = Convert.ToInt64(dr["WorkTypeID"].ToString()),
                        WorkType = Convert.ToString(dr["WorkType"].ToString()),
                        

                    }).ToList();

        }
        public static MeasurementTypeList ConvertMeasurementTypeDetails(DataTable dt)
        {
            MeasurementTypeList log = new MeasurementTypeList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MeasurementTypeListDetails= ConvertMeasurementTypeListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<MeasurementTypeListDetails> ConvertMeasurementTypeListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MeasurementTypeListDetails()
                    {

                        MeasurementTypeID = Convert.ToInt64(dr["MeasurementTypeID"].ToString()),
                        MeasurementType = Convert.ToString(dr["MeasurementType"].ToString()),


                    }).ToList();

        }
        public static ProjectStagesList ConvertProjectStagesDetails(DataTable dt)
        {
            ProjectStagesList log = new ProjectStagesList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectStagesListDetails = ConvertProjectStagesListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectStagesListDetails> ConvertProjectStagesListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectStagesListDetails()
                    {

                       ProjectStagesID = Convert.ToInt64(dr["ProjectStagesID"].ToString()),
                        StageName = Convert.ToString(dr["StageName"].ToString()),
                        DueDate = Convert.ToString(dr["DueDate"].ToString())
                    }).ToList();

        }
        public static ProjectTeamList ConvertProjectTeamDetails(DataTable dt)
        {
            ProjectTeamList log = new ProjectTeamList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectTeamListDetails = ConvertProjectTeamListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectTeamListDetails> ConvertProjectTeamListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectTeamListDetails()
                    {

                        ID_ProjectTeam = Convert.ToInt64(dr["ID_ProjectTeam"].ToString()),
                        TeamName = Convert.ToString(dr["TeamName"].ToString()),


                    }).ToList();

        }

        public static UnitList ConvertUnitDetails(DataTable dt)
        {
            UnitList log = new UnitList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.UnitListDetails = ConvertUnitListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<UnitListDetails> ConvertUnitListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new UnitListDetails()
                    {

                        MeasurementUnitID = Convert.ToInt64(dr["MeasurementUnitID"].ToString()),
                        MeasurementUnit = Convert.ToString(dr["MeasurementUnit"].ToString()),


                    }).ToList();

        }
        public static EmployeeListforProject ConvertEmployeeListforProject(DataTable dt)
        {
            EmployeeListforProject log = new EmployeeListforProject();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EmployeeListforProjectDetails = ConvertEmployeeListforProjectDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<EmployeeListforProjectDetails> ConvertEmployeeListforProjectDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new EmployeeListforProjectDetails()
                    {

                        ID_FIELD = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        Code = Convert.ToString(dr["Code"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString()),
                        Department = Convert.ToString(dr["Department"].ToString()),
                        Designation = Convert.ToString(dr["Designation"].ToString()),


                    }).ToList();

        }
        public static ModeList ConvertModeList(DataTable dt)
        {
            ModeList log = new ModeList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ModeListDetails = ConvertModeListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ModeListDetails> ConvertModeListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ModeListDetails()
                    {

                        ID_Mode = Convert.ToInt64(dr["ID_Mode"].ToString()),
                        ModeName = Convert.ToString(dr["ModeName"].ToString())

                    }).ToList();

        }
        public static MatProductDetails ConvertMatProductDetails(DataTable dt)
        {
            MatProductDetails log = new MatProductDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MatProductListDetails = ConvertMatProductListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<MatProductListDetails> ConvertMatProductListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MatProductListDetails()
                    {
                        ProductID = Convert.ToString(dr["ID_FIELD"].ToString()),
                        Code = Convert.ToString(dr["Code"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString()),
                        SalesPrice= Convert.ToString(dr["SalePrice"].ToString()),
                        AssignedStock = Convert.ToString(dr["AssignedStock1R"].ToString()),
                        AvailableStock = Convert.ToString(dr["AvailableStock"].ToString()),
                        StockId = Convert.ToString(dr["Stockid"].ToString()),
                        //  AvailableStock = Convert.ToString(dr["AvailableStock"].ToString()),//

                    }).ToList();

        }
        public static MaterialRequestProductList ConvertMaterialRequestProductDetails(DataTable dt)
        {
            MaterialRequestProductList log = new MaterialRequestProductList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MaterialRequestProductListDetails = ConvertMaterialRequestProductListDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<MaterialRequestProductListDetails> ConvertMaterialRequestProductListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MaterialRequestProductListDetails()
                    {

                        ProductID = Convert.ToString(dr["ID_FIELD"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString()),
                        Code = Convert.ToString(dr["Code"].ToString()),
                        SalesPrice = Convert.ToString(dr["SalePrice"].ToString()),
                        CurrentStock = Convert.ToString(dr["CurrentStock1R"].ToString()),
                        Department = Convert.ToString(dr["Department"].ToString()),
                        StockId = Convert.ToString(dr["Stockid"].ToString()),
                        //  AvailableStock = Convert.ToString(dr["AvailableStock"].ToString()),//

                    }).ToList();

        }


        public static UpdateMaterialUsage ConvertMaterialUsage(DataTable dt)
        {
            UpdateMaterialUsage log = new UpdateMaterialUsage();
            if (dt.Columns.Contains("ErrCode"))
            {
                log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                 //   log.FK_CustomerAssignment = Convert.ToString(dt.Rows[0]["FK_CustomerAssignment"]);

                }
                else
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";
                }
            }

            return log;
        }

        public static UpdateMaterialRequest ConvertMaterialrequest(DataTable dt)
        {
            UpdateMaterialRequest log = new UpdateMaterialRequest();
            if (dt.Columns.Contains("ErrCode"))
            {
                log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    //   log.FK_CustomerAssignment = Convert.ToString(dt.Rows[0]["FK_CustomerAssignment"]);

                }
                else
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Found";
                }
            }

            return log;
        }
        //public static UpdateProjectFollowUp ConvertProjectFollowUp(DataTable dt)
        //{
        //    UpdateProjectFollowUp log = new UpdateProjectFollowUp();
        //    if (dt.Columns.Contains("ErrCode"))
        //    {
        //        log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
        //        log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
        //    }
        //    else
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            log.ResponseCode = "0";
        //            log.ResponseMessage = "Transaction Verified";
        //            //   log.FK_CustomerAssignment = Convert.ToString(dt.Rows[0]["FK_CustomerAssignment"]);

        //        }
        //        else
        //        {
        //            log.ResponseCode = "-2";
        //            log.ResponseMessage = "No Data Found";
        //        }
        //    }

        //    return log;
        //}
        public static DownloadImage ConvertDownloadImage(DataTable dt)
        {
            DownloadImage log = new DownloadImage();            
            if (dt.Rows.Count > 0)
            {
              
                if(Convert.ToInt64(dt.Rows[0]["FK_ProjectImage"])>0)
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ResponseCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]);
                    log.FK_ProjectImage = Convert.ToInt64(dt.Rows[0]["FK_ProjectImage"]);
                }
                else
                {
                    log.ResponseCode = "-1";
                    log.ResponseMessage = "Download image failed";
                }
                
            }
            else
            {
                log.ResponseCode = "-1";
                log.ResponseMessage = "Download image failed";
            }           

            return log;
        }
        public static UpdateProjectFollowUp ConvertUpdateProjectFollowUp(DataTable dt)
        {
            UpdateProjectFollowUp log = new UpdateProjectFollowUp();
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt64(dt.Rows[0][0].ToString()) > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Successfully Updated";
                    log.FK_ProjectFollowUp = Convert.ToInt64(dt.Rows[0][0].ToString());
                }                              
            }
            else
            {
                log.ResponseCode = "-1";
                log.ResponseMessage = "Updation failed";
            }
            return log;
        }
        public static UpadateSiteVisit ConvertUpadateSiteVisit(DataTable dt)
        {
            UpadateSiteVisit log = new UpadateSiteVisit();
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt64(dt.Rows[0][0].ToString()) > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Successfully Saved";
                    log.FK_SiteVisit = Convert.ToInt64(dt.Rows[0][0].ToString());
                }
            }
            else
            {
                log.ResponseCode = "-1";
                log.ResponseMessage = "Updation failed";
            }
            return log;
        }
        public static ProjectStatus ConvertProjectStatus(DataTable dt)
        {
            ProjectStatus log = new ProjectStatus();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectStatusList = ConvertProjectStatusList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectStatusList> ConvertProjectStatusList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectStatusList()
                    {
                        FK_Status = Convert.ToInt32(dr["FK_Status"].ToString()),
                        StatusName = Convert.ToString(dr["StatusName"].ToString())
                    }).ToList();

        }
        public static ProjectOtherChargeDetails ConvertProjectOtherChargeDetails(DataTable dt)
        {
            ProjectOtherChargeDetails log = new ProjectOtherChargeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectOtherChargeDetailsList = ConvertProjectOtherChargeDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectOtherChargeDetailsList> ConvertProjectOtherChargeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectOtherChargeDetailsList()
                    {
                        SlNo = Convert.ToInt64(dr["SlNo"].ToString()),
                        ID_OtherChargeType = Convert.ToInt64(dr["ID_OtherChargeType"].ToString()),
                        OctyName = Convert.ToString(dr["OctyName"].ToString()),
                        OctyTransTypeActive = Convert.ToInt16(dr["OctyTransTypeActive"].ToString()),
                        OctyTransType = Convert.ToInt16(dr["OctyTransType"].ToString()),
                        FK_TaxGroup = Convert.ToInt64(dr["FK_TaxGroup"].ToString()),
                        OctyAmount = Convert.ToDecimal(dr["OctyAmount"].ToString()),
                        OctyTaxAmount = Convert.ToDecimal(dr["OctyTaxAmount"].ToString()),
                        OctyIncludeTaxAmount = Convert.ToString(dr["OctyIncludeTaxAmount"].ToString())

                    }).ToList();

        }
        public static OtherChargeTaxCalculationDetails ConvertOtherChargeTaxCalculationDetails(DataTable dt)
        {
            OtherChargeTaxCalculationDetails log = new OtherChargeTaxCalculationDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.OtherChargeTaxCalculationDetailsList = ConvertOtherChargeTaxCalculationDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<OtherChargeTaxCalculationDetailsList> ConvertOtherChargeTaxCalculationDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new OtherChargeTaxCalculationDetailsList()
                    {
                        SlNo = Convert.ToInt32(dr["SlNo"].ToString()),
                        ID_TaxSettings = Convert.ToInt64(dr["ID_TaxSettings"].ToString()),
                        FK_TaxType = Convert.ToString(dr["FK_TaxType"].ToString()),
                        TaxTyName = Convert.ToString(dr["TaxTyName"].ToString()),
                        TaxPercentage = Convert.ToString(dr["TaxPercentage"].ToString()),
                        TaxtyInterstate = Convert.ToBoolean(dr["TaxtyInterstate"].ToString()),
                        TaxUpto = Convert.ToDecimal(dr["TaxUpto"].ToString()),
                        TaxUptoPercentage = Convert.ToBoolean(dr["TaxUptoPercentage"].ToString()),
                        Amount = Convert.ToDecimal(dr["Amount"].ToString())

                    }).ToList();

        }
        public static checkDetails ConvertcheckDetails(DataSet dts)
        {
            checkDetails log = new checkDetails();
            
            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");              
                log.checkDetailsList = ConvertcheckDetailsList(dts);
            }
            return log;
        }
        public static List<checkDetailsList> ConvertcheckDetailsList(DataSet dts)
        {
            List<checkDetailsList> lst = new List<checkDetailsList>();
            DataTable dt = dts.Tables[0];
            DataTable dt1 = dts.Tables[1];
            Int64 ID_CheckListType = 0;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                        checkDetailsList obj = new checkDetailsList();
                        obj.ID_CheckListType = Convert.ToInt64(dr["ID_CheckListType"].ToString());
                        obj.CLTyName = Convert.ToString(dr["CLTyName"].ToString());
                        ID_CheckListType = Convert.ToInt64(dr["ID_CheckListType"].ToString());
                         obj.SubArrary = ConvertSubArrary(dt1, ID_CheckListType);
                        lst.Add(obj);
                }
              
            }
            return lst;
        }
        public static List<SubArrary> ConvertSubArrary(DataTable dt,Int64 ID_CheckListType)
        {
            List<SubArrary> lst = new List<SubArrary>();
            foreach (DataRow dr in dt.Rows)
            {
               
                if (ID_CheckListType == Convert.ToInt64(dr["FK_CheckListType"].ToString()))
                {
                    SubArrary obj = new SubArrary();
                    obj.ID_CheckListType = Convert.ToInt64(dr["FK_CheckListType"].ToString());
                    obj.ID_CheckList = Convert.ToInt64(dr["ID_CheckList"].ToString());
                    obj.CkLstName = Convert.ToString(dr["CkLstName"].ToString());
                    lst.Add(obj);
                }
               
            }
            return lst;

        }
        public static UpdateProjectTransaction ConvertUpdateProjectTransaction(DataTable dt)
        {
            UpdateProjectTransaction log = new UpdateProjectTransaction();
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt64(dt.Rows[0][0].ToString()) > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Successfully Saved";
                    log.FK_ProjectTransaction = Convert.ToInt64(dt.Rows[0][0].ToString());
                }
            }
            else
            {
                log.ResponseCode = "-1";
                log.ResponseMessage = "Updation failed";
            }
            return log;
        }
        public static ProjectSiteVisitCount ConvertProjectSiteVisitCount(DataTable dt)
        {
            ProjectSiteVisitCount log = new ProjectSiteVisitCount();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";              
                log.ProjectSiteVisitCountDetail = ConvertProjectSiteVisitCountDetail(dt);

            }
            else
            {
                log.ResponseCode = "-1";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectSiteVisitCountDetail> ConvertProjectSiteVisitCountDetail(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectSiteVisitCountDetail()
                    {
                        Mode = Convert.ToInt32(dr["Mode"].ToString()),
                        Label_Name = Convert.ToString(dr["Label_Name"].ToString()),
                        Count = Convert.ToInt32(dr["Count"].ToString())
                       

                    }).ToList();

        }
        public static ProjectSiteVisitAssign ConvertProjectSiteVisitAssign(DataTable dt)
        {
            ProjectSiteVisitAssign log = new ProjectSiteVisitAssign();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectSiteVisitAssignList = ConvertProjectSiteVisitAssignList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectSiteVisitAssignList> ConvertProjectSiteVisitAssignList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectSiteVisitAssignList()
                    {
                        ID_LeadGenerate = Convert.ToInt64(dr["ID_LeadGenerate"].ToString()),
                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
                        CustomeName = Convert.ToString(dr["CustomeName"].ToString()),
                        MobileNo = Convert.ToString(dr["MobileNo"].ToString()),
                        CusAddress = Convert.ToString(dr["CusAddress"].ToString()),
                        FK_Customer = Convert.ToInt64(dr["FK_Customer"].ToString()),
                        FK_CustomerOthers = Convert.ToInt64(dr["FK_CustomerOthers"].ToString()),
                        ID_SiteVisitAssignment = Convert.ToInt64(dr["ID_SiteVisitAssignment"].ToString()),
                        SiteVisitDate = Convert.ToString(dr["SiteVisitDate"].ToString()),
                        ExpenseAmount = Convert.ToString(dr["ExpenseAmount"].ToString()),
                        IsSiteVisit = Convert.ToString(dr["IsSiteVisit"].ToString())

                    }).ToList();

        }
        public static SiteVisitAssignViewDetails ConvertSiteVisitAssignViewDetails(DataSet dts)
        {
            SiteVisitAssignViewDetails log = new SiteVisitAssignViewDetails();

            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {

                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.AssignEmployeeDetails = ConvertAssignEmployeeDetails(dts.Tables[1]);
                    log.AssignDetails = ConvertAssignDetails(dts.Tables[0]);
                
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static AssignDetails ConvertAssignDetails(DataTable dt)
        {
            AssignDetails log = new AssignDetails();


            if (dt.Rows.Count > 0)
            {
               
                    
                    log.ID_SiteVisitAssignment = dt.Rows[0].Field<Int64>("ID_SiteVisitAssignment");
                    log.LeadGenerationID = dt.Rows[0].Field<Int64>("LeadGenerationID");
                    log.SiteVisitDate =Convert.ToString(dt.Rows[0].Field<DateTime>("SVSiteVisitDate"));
                    log.VisitTime = Convert.ToString(dt.Rows[0].Field<TimeSpan>("VisitTime"));
                    log.Note1 = dt.Rows[0].Field<string>("Note1");
                    log.Note2 = dt.Rows[0].Field<string>("Note2");
                    log.ExpenseAmount = Convert.ToString(dt.Rows[0].Field<decimal>("SVExpenseAmount"));
                    log.LeadNo = dt.Rows[0].Field<string>("LeadNo");
                  
               
            }
            
            return log;
        }
        public static List<AssignEmployeeDetails> ConvertAssignEmployeeDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AssignEmployeeDetails()
                    {
                        EmployeeID = Convert.ToString(dr["EmployeeID"].ToString()),
                        EmployeeName = Convert.ToString(dr["EmployeeType"].ToString()),
                        EmployeeType = Convert.ToString(dr["DepartmentName"].ToString()),
                        EmployeeTypeName = Convert.ToString(dr["EmployeeTypeName"].ToString()),
                        DepartmentID = Convert.ToString(dr["Department"].ToString()),
                        DepartmentName = Convert.ToString(dr["DepartmentName"].ToString())

                    }).ToList();

        }
        public static ProjectTransactionEmployeeDetails ConvertProjectTransactionEmployeeDetails(DataTable dt)
        {
            ProjectTransactionEmployeeDetails log = new ProjectTransactionEmployeeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProjectTransactionEmployeeDetailsList = ConvertProjectTransactionEmployeeDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProjectTransactionEmployeeDetailsList> ConvertProjectTransactionEmployeeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProjectTransactionEmployeeDetailsList()
                    {
                        FK_Employee = Convert.ToString(dr["ID_FIELD"].ToString()),
                        EmployeeName = Convert.ToString(dr["Name"].ToString()),
                        Department = Convert.ToString(dr["Department"].ToString()),
                        Designation = Convert.ToString(dr["Designation"].ToString())
                       

                    }).ToList();

        }
        public static TransactionTypeDetails ConvertTransactionTypeDetails(DataTable dt)
        {
            TransactionTypeDetails log = new TransactionTypeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.TransactionTypeList = ConvertTransactionTypeList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<TransactionTypeList> ConvertTransactionTypeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new TransactionTypeList()
                    {
                        FK_TransactionType = Convert.ToString(dr["FK_TransactionType"].ToString()),
                        TransactionTypename = Convert.ToString(dr["TransactionTypename"].ToString())


                    }).ToList();

        }
        public static PettyCashieDetails ConvertPettyCashieDetails(DataTable dt)
        {
            PettyCashieDetails log = new PettyCashieDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PettyCashieList = ConvertPettyCashieList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PettyCashieList> ConvertPettyCashieList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PettyCashieList()
                    {
                        ID_PettyCashier = Convert.ToString(dr["ID_PettyCashier"].ToString()),
                        PtyCshrName = Convert.ToString(dr["PtyCshrName"].ToString())


                    }).ToList();

        }
        public static BillTypeDetails ConvertBillTypeDetails(DataTable dt)
        {
            BillTypeDetails log = new BillTypeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.BillTypeDetailsList = ConvertBillTypeDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<BillTypeDetailsList> ConvertBillTypeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new BillTypeDetailsList()
                    {
                        ID_BillType = Convert.ToString(dr["ID_BillType"].ToString()),
                        BTName = Convert.ToString(dr["BTName"].ToString())


                    }).ToList();

        }
        public static PaymentInformation ConvertPaymentInformation(DataTable dt)
        {
            PaymentInformation log = new PaymentInformation();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PaymentInformationList = ConvertPaymentInformationList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PaymentInformationList> ConvertPaymentInformationList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PaymentInformationList()
                    {
                        FK_PaymentInformation = Convert.ToString(dr["FK_PaymentInformation"].ToString()),
                        TypeName = Convert.ToString(dr["TypeName"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())

                    }).ToList();

        }
    }

}

