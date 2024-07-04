using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;
namespace PerfectWebERPAPI.DataAccess
{
   public  class DalProjectDetails
    {

   
        public DataTable DalProjectCommonValidate(IProjectDetails IProject)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IProject.TransMode != "")
                TransMode = IProject.TransMode;
            if (IProject.ReqMode != "")
                Pagemode = Convert.ToInt32(IProject.ReqMode);
            if (IProject.PageIndex != "")
                PageIndex = Convert.ToInt32(IProject.PageIndex);
            if (IProject.PageSize != "")
                PageSize = Convert.ToInt32(IProject.PageSize);
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);
            if (IProject.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IProject.Critrea1);
            if (IProject.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IProject.Critrea2);
            if (IProject.Critrea3 != "")
                Critrea3 = IProject.Critrea3;
            if (IProject.Critrea4 != "")
                Critrea4 = IProject.Critrea4;
            if (IProject.ID != "")
                ID = Convert.ToInt32(IProject.ID);
            if (IProject.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IProject.Critrea5);
            if (IProject.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IProject.Critrea6);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "proAPICmnSearchValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, TransMode);
            db.AddInParameter(dbCommand, "@Pagemode", DbType.Int64, Pagemode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, FK_Company);
            db.AddInParameter(dbCommand, "@Critrea1", DbType.Int64, Critrea1);
            db.AddInParameter(dbCommand, "@Critrea2", DbType.Int64, Critrea2);
            db.AddInParameter(dbCommand, "@Critrea3", DbType.String, Critrea3);
            db.AddInParameter(dbCommand, "@Critrea4", DbType.String, Critrea4);
            db.AddInParameter(dbCommand, "@TotalCount", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IProject.Name);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, ID);
            db.AddInParameter(dbCommand, "@Criteria5", DbType.Int64, Criteria5);
            db.AddInParameter(dbCommand, "@Criteria6", DbType.Int64, Criteria6);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalSearchCommonValidate(IProjectDetails IProject)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0, FK_Project=0, FK_Stage=0;
            Int32 SubMode =0;

            if (IProject.TransMode != "")
                TransMode = IProject.TransMode;
            if (IProject.ReqMode != "")
                Pagemode = Convert.ToInt32(IProject.ReqMode);
            if (IProject.PageIndex != "")
                PageIndex = Convert.ToInt32(IProject.PageIndex);
            if (IProject.PageSize != "")
                PageSize = Convert.ToInt32(IProject.PageSize);
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);
            if (IProject.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IProject.Critrea1);
            if (IProject.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IProject.Critrea2);
            if (IProject.Critrea3 != "")
                Critrea3 = IProject.Critrea3;
            if (IProject.Critrea4 != "")
                Critrea4 = IProject.Critrea4;
            if (IProject.ID != "")
                ID = Convert.ToInt32(IProject.ID);
            if (IProject.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IProject.Critrea5);
            if (IProject.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IProject.Critrea6);
            if (IProject.SubMode != "")
                SubMode = Convert.ToInt32(IProject.SubMode);
            if (IProject.FK_Project != "")
                FK_Project = Convert.ToInt64(IProject.FK_Project);
            if (IProject.FK_Stage != "")
                FK_Stage = Convert.ToInt64(IProject.FK_Stage);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "proAPICmnSearchPopup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, TransMode);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int64, Pagemode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, FK_Company);
            db.AddInParameter(dbCommand, "@Critrea1", DbType.Int64, Critrea1);
            db.AddInParameter(dbCommand, "@Critrea2", DbType.Int64, Critrea2);
            db.AddInParameter(dbCommand, "@Critrea3", DbType.String, Critrea3);
            db.AddInParameter(dbCommand, "@Critrea4", DbType.String, Critrea4);
            db.AddInParameter(dbCommand, "@TotalCount", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IProject.Name);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, ID);
            db.AddInParameter(dbCommand, "@Criteria5", DbType.Int64, Criteria5);
            db.AddInParameter(dbCommand, "@Criteria6", DbType.Int64, Criteria6);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, FK_Project);
            db.AddInParameter(dbCommand, "@FK_Stage", DbType.Int64, FK_Stage);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalSearchCommonPopupvalues(IProjectDetails IProject)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IProject.TransMode != "")
                TransMode = IProject.TransMode;
            if (IProject.ReqMode != "")
                Pagemode = Convert.ToInt32(IProject.ReqMode);
            if (IProject.PageIndex != "")
                PageIndex = Convert.ToInt32(IProject.PageIndex);
            if (IProject.PageSize != "")
                PageSize = Convert.ToInt32(IProject.PageSize);
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);
            if (IProject.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IProject.Critrea1);
            if (IProject.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IProject.Critrea2);
            if (IProject.Critrea3 != "")
                Critrea3 = IProject.Critrea3;
            if (IProject.Critrea4 != "")
                Critrea4 = IProject.Critrea4;
            if (IProject.ID != "")
                ID = Convert.ToInt32(IProject.ID);
            if (IProject.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IProject.Critrea5);
            if (IProject.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IProject.Critrea6);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "ProAPICommonPopupValues";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, Pagemode);
           

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalMaterialUsageUpdate(IProjectDetails IProjectDetails)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, FK_Employee = 0, ID_ProjectMaterialUsage = 0, FK_Product = 0;


            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IProjectDetails.FK_BranchCodeUser);
            if (IProjectDetails.ID_ProjectMaterialUsage != "")
                ID_ProjectMaterialUsage = Convert.ToInt64(IProjectDetails.ID_ProjectMaterialUsage);
            if (IProjectDetails.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IProjectDetails.FK_Employee);
            //if (IProjectDetails.FK_Product != "")
            //    FK_Product = Convert.ToInt64(IProjectDetails.FK_Product);
            
                        DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "ProProjectMaterialUsageUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, IProjectDetails.UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IProjectDetails.TransMode);
            db.AddInParameter(dbCommand, "@ID_ProjectMaterialUsage", DbType.Int64, ID_ProjectMaterialUsage);
            db.AddInParameter(dbCommand, "@ProMatUsageDate", DbType.String, IProjectDetails.ProMatUsageDate);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.String, IProjectDetails.FK_Project);
            db.AddInParameter(dbCommand, "@FK_Stages", DbType.String, IProjectDetails.FK_Stages);
            db.AddInParameter(dbCommand, "@FK_Team", DbType.String, IProjectDetails.FK_Team);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.String, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IProjectDetails.FK_Company);
            db.AddInParameter(dbCommand, "@MaterialDetails", DbType.Xml, IProjectDetails.SubProductDetails);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int64, 1);
            db.AddInParameter(dbCommand, "@FK_ProjectMaterialUsage", DbType.Int64, ID_ProjectMaterialUsage);



            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalMaterialRequestUpdate(IProjectDetails IProjectDetails)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, FK_Employee = 0, ID_ProjectMaterialRequest = 0, FK_Product = 0;


            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IProjectDetails.FK_BranchCodeUser);
            if (IProjectDetails.ID_ProjectMaterialUsage != "")
                ID_ProjectMaterialRequest = Convert.ToInt64(IProjectDetails.ID_ProjectMaterialRequest);
            if (IProjectDetails.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IProjectDetails.FK_Employee);
            //if (IProjectDetails.FK_Product != "")
            //    FK_Product = Convert.ToInt64(IProjectDetails.FK_Product);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "ProProjectMaterialRequestUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, IProjectDetails.UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IProjectDetails.TransMode);
            db.AddInParameter(dbCommand, "@ID_ProjectMaterialRequest", DbType.Int64, ID_ProjectMaterialRequest);
            db.AddInParameter(dbCommand, "@ProMatRequestDate", DbType.String, IProjectDetails.ProMatRequestDate);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.String, IProjectDetails.FK_Project);
            db.AddInParameter(dbCommand, "@FK_Stages", DbType.String, IProjectDetails.FK_Stages);
            db.AddInParameter(dbCommand, "@FK_Team", DbType.String, IProjectDetails.FK_Team);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.String, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IProjectDetails.FK_Company);
            db.AddInParameter(dbCommand, "@MaterialRequestDetailsView", DbType.Xml, IProjectDetails.SubProductDetails);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int64,1);
            db.AddInParameter(dbCommand, "@FK_ProjectMaterialRequest", DbType.Int64, ID_ProjectMaterialRequest);



            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalDownloadImage(IProjectDetails IProjectDetails)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0,FK_SiteVisit = 0;


            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IProjectDetails.FK_BranchCodeUser);         
            if (IProjectDetails.FK_SiteVisit != "")
                FK_SiteVisit = Convert.ToInt64(IProjectDetails.FK_SiteVisit);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "ProAPIDownloadImage";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, 1);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IProjectDetails.TransMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@FK_SiteVisit", DbType.Int64, FK_SiteVisit);
            db.AddInParameter(dbCommand, "@ProdImageName", DbType.String, IProjectDetails.ProjImageName);
            db.AddInParameter(dbCommand, "@ProjImageType", DbType.String, IProjectDetails.ProjImageType);
            db.AddInParameter(dbCommand, "@ProdImageDescription", DbType.String, IProjectDetails.ProjImageDescription);
            db.AddInParameter(dbCommand, "@ProdImage", DbType.Binary, IProjectDetails.ProjImage);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProjectFollowUpUpdate(IProjectDetails IProjectDetails)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, FK_Project = 0, FK_Stage=0, UserAction=0;
            Int16 CurrentStatus = 0;

            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IProjectDetails.FK_BranchCodeUser);
            if (IProjectDetails.FK_Project != "")
                FK_Project = Convert.ToInt64(IProjectDetails.FK_Project);
            if (IProjectDetails.FK_Stage != "")
                FK_Stage = Convert.ToInt64(IProjectDetails.FK_Stage);
            if (IProjectDetails.CurrentStatus != "")
                CurrentStatus = Convert.ToInt16(IProjectDetails.CurrentStatus);
            if (IProjectDetails.UserAction != "")
                UserAction = Convert.ToInt64(IProjectDetails.UserAction);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "proProjectFollowUpUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_ProjectFollowUp", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@ID_ProjectFollowUp", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, FK_Project);
            db.AddInParameter(dbCommand, "@FK_Stage", DbType.Int64, FK_Stage);
            db.AddInParameter(dbCommand, "@EffectDate", DbType.String, IProjectDetails.EffectDate);
            db.AddInParameter(dbCommand, "@PrFuCurrentStatus", DbType.Int16, CurrentStatus);
            db.AddInParameter(dbCommand, "@PrFuStatusDate", DbType.String, IProjectDetails.StatusDate);
            db.AddInParameter(dbCommand, "@PrPuRemarks", DbType.String, IProjectDetails.Remarks);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, UserAction);           
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IProjectDetails.TransMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, IProjectDetails.Reason);                  
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProjectSitevisit(IProjectDetails IProjectDetails)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, FK_LeadGeneration = 0, UserAction = 0, FK_SiteVisitAssignment=0;
            decimal SVInspectioncharge = 0;

            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IProjectDetails.FK_BranchCodeUser);            
            if (IProjectDetails.FK_LeadGeneration != "")
                FK_LeadGeneration = Convert.ToInt64(IProjectDetails.FK_LeadGeneration);           
            if (IProjectDetails.UserAction != "")
                UserAction = Convert.ToInt64(IProjectDetails.UserAction);
            if (IProjectDetails.SVInspectioncharge != "")
                SVInspectioncharge = Convert.ToDecimal(IProjectDetails.SVInspectioncharge);
            if (IProjectDetails.FK_SiteVisitAssignment != "")
                FK_SiteVisitAssignment = Convert.ToInt64(IProjectDetails.FK_SiteVisitAssignment);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "proSiteVisitUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_SiteVisit", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@ID_SiteVisit", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_LeadGeneration", DbType.Int64, FK_LeadGeneration);
            db.AddInParameter(dbCommand, "@SVSiteVisitDate", DbType.String, IProjectDetails.SiteVisitDate);
            db.AddInParameter(dbCommand, "@SVSitevisitTime", DbType.String, IProjectDetails.SitevisitTime);
            db.AddInParameter(dbCommand, "@SVInspectionNote1", DbType.String, IProjectDetails.Note1);
            db.AddInParameter(dbCommand, "@SVInspectionNote2", DbType.String, IProjectDetails.Note2);
            db.AddInParameter(dbCommand, "@SVCustomerNotes", DbType.String, IProjectDetails.CustomerNotes);
            db.AddInParameter(dbCommand, "@SVExpenseAmount", DbType.String, IProjectDetails.ExpenseAmount);
            db.AddInParameter(dbCommand, "@SVRemarks", DbType.String, IProjectDetails.Remarks);
            db.AddInParameter(dbCommand, "@EmployeeDetails", DbType.Xml, IProjectDetails.EmployeeDetails);
            db.AddInParameter(dbCommand, "@MeasurementDetails", DbType.Xml, IProjectDetails.MeasurementDetails);
            db.AddInParameter(dbCommand, "@OtherChargeDetails", DbType.Xml, IProjectDetails.OtherChargeDetails);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IProjectDetails.TransMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, UserAction);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@CheckListSub", DbType.Xml, IProjectDetails.CheckListSub);
            db.AddInParameter(dbCommand, "@OtherChgTaxDetails", DbType.Xml, IProjectDetails.OtherChgTaxDetails);
            db.AddInParameter(dbCommand, "@SVInspectioncharge", DbType.Decimal, SVInspectioncharge);
            db.AddInParameter(dbCommand, "@FK_SiteVisitAssignment", DbType.Int64, FK_SiteVisitAssignment);
            db.AddInParameter(dbCommand, "@LastID", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int16, 1);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalCommonValidate(IProjectDetails IProjectDetails)
        {

            Int32 ReqMode = 0, SubMode = 0;
            Int64 FK_Company = 0, FK_TaxGroup=0, FK_Project=0, FK_Stage=0, FK_Employee=0, FK_PetyCashier=0, FK_TransactionType=0;
            decimal Amount = 0;
            bool IncludeTax = false;
            if (IProjectDetails.ReqMode != "")
                ReqMode = Convert.ToInt32(IProjectDetails.ReqMode);
            if (IProjectDetails.SubMode != "")
                SubMode = Convert.ToInt32(IProjectDetails.SubMode);
            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt32(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_TaxGroup != "")
                FK_TaxGroup = Convert.ToInt32(IProjectDetails.FK_TaxGroup);
            if (IProjectDetails.Amount != "")
                Amount = Convert.ToDecimal(IProjectDetails.Amount);
            if (IProjectDetails.IncludeTax == "1")
                IncludeTax = true;
            else
                IncludeTax = false;
            if (IProjectDetails.FK_Project != "")
                FK_Project = Convert.ToInt64(IProjectDetails.FK_Project);
            if (IProjectDetails.FK_Stages != "")
                FK_Stage = Convert.ToInt64(IProjectDetails.FK_Stages);
            if (IProjectDetails.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IProjectDetails.FK_Employee);
            if (IProjectDetails.FK_PetyCashier != "")
                FK_PetyCashier = Convert.ToInt64(IProjectDetails.FK_PetyCashier);
            if (IProjectDetails.FK_TransactionType != "")
                FK_TransactionType = Convert.ToInt64(IProjectDetails.FK_TransactionType);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@FK_TaxGroup", DbType.Int64, FK_TaxGroup);
            db.AddInParameter(dbCommand, "@Amount", DbType.Decimal, Amount);
            db.AddInParameter(dbCommand, "@IncludeTax", DbType.Boolean,IncludeTax);
            db.AddInParameter(dbCommand, "@AsOnDate", DbType.String, IProjectDetails.AsOnDate);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, FK_Project);
            db.AddInParameter(dbCommand, "@FK_Stage", DbType.Int64, FK_Stage); 
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee); 
            db.AddInParameter(dbCommand, "@FK_PetyCashier", DbType.Int64, FK_PetyCashier);
            db.AddInParameter(dbCommand, "@FK_TransactionType", DbType.Int64, FK_TransactionType);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataSet DalCommonValidates(IProjectDetails IProjectDetails)
        {
            Int32 ReqMode = 0, SubMode = 0, FK_SiteVisitAssignment=0, FK_BranchCodeUser = 0;
            Int64 FK_Company = 0;
      
            if (IProjectDetails.ReqMode != "")
                ReqMode = Convert.ToInt32(IProjectDetails.ReqMode);
            if (IProjectDetails.SubMode != "")
                SubMode = Convert.ToInt32(IProjectDetails.SubMode);
            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_SiteVisitAssignment != "")
                FK_SiteVisitAssignment = Convert.ToInt32(IProjectDetails.FK_SiteVisitAssignment);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(IProjectDetails.FK_BranchCodeUser);
            // DataTable dtbl = new DataTable();
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@FK_SiteVisitAssignment", DbType.Int64, FK_SiteVisitAssignment);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);


            try
            {
                //dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                dtbl = db.ExecuteDataSet(dbCommand);
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProjectTransactionUpdate(IProjectDetails IProjectDetails)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, FK_Project = 0, FK_Stage=0, FK_Employee= 0 ,FK_BillType = 0, FK_PettyCashier=0;
            Int16 UserAction = 0, FK_TransactionType=0;
            decimal OtherCharge = 0, NetAmount=0, RoundOff=0;

            if (IProjectDetails.FK_Company != "")
                FK_Company = Convert.ToInt64(IProjectDetails.FK_Company);
            if (IProjectDetails.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IProjectDetails.FK_BranchCodeUser);
            if (IProjectDetails.FK_Project != "")
                FK_Project = Convert.ToInt64(IProjectDetails.FK_Project);
            if (IProjectDetails.UserAction != "")
                UserAction = Convert.ToInt16(IProjectDetails.UserAction);
            if (IProjectDetails.OtherCharge != "")
                OtherCharge = Convert.ToDecimal(IProjectDetails.OtherCharge);
            if (IProjectDetails.NetAmount != "")
                NetAmount = Convert.ToDecimal(IProjectDetails.NetAmount);
            if (IProjectDetails.FK_Stage != "")
                FK_Stage = Convert.ToInt64(IProjectDetails.FK_Stage);
            if (IProjectDetails.FK_TransactionType != "")
                FK_TransactionType = Convert.ToInt16(IProjectDetails.FK_TransactionType);
            if (IProjectDetails.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IProjectDetails.FK_Employee);
            if (IProjectDetails.RoundOff != "")
                RoundOff = Convert.ToDecimal(IProjectDetails.RoundOff);
            if (IProjectDetails.FK_BillType != "")
                FK_BillType = Convert.ToInt64(IProjectDetails.FK_BillType);
            if (IProjectDetails.FK_PettyCashier != "")
                FK_PettyCashier = Convert.ToInt64(IProjectDetails.FK_PettyCashier);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProjectDetails.BankKey);
            string sqlCommand = "ProProjectTransactionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, UserAction);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, "PRTP");
            db.AddInParameter(dbCommand, "@ID_ProjectTransaction", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Date", DbType.String, IProjectDetails.Date);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, FK_Project);
            db.AddInParameter(dbCommand, "@FK_Stage", DbType.Int64, FK_Stage);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IProjectDetails.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, IProjectDetails.Remarks);
            db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, NetAmount);
            db.AddInParameter(dbCommand, "@PTPrjTransType", DbType.Int16, FK_TransactionType);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@PTAmount", DbType.Decimal, OtherCharge);
            db.AddInParameter(dbCommand, "@PTRoundOff", DbType.Decimal,RoundOff);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, FK_BillType);
            db.AddInParameter(dbCommand, "@OtherChgDetails", DbType.Xml, IProjectDetails.OtherChargeDetails);
            db.AddInParameter(dbCommand, "@PaymentDetail", DbType.Xml, IProjectDetails.PaymentDetail);         
            db.AddInParameter(dbCommand, "@OtherChgTaxDetails", DbType.Xml, IProjectDetails.OtherChgTaxDetails);
            db.AddInParameter(dbCommand, "@LastID", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_ProjectTransaction", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_PettyCashier", DbType.Int64, FK_PettyCashier);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalproAPIProjectSelect(IProjectDetails IProject)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IProject.TransMode != "")
                TransMode = IProject.TransMode;
            if (IProject.ReqMode != "")
                Pagemode = Convert.ToInt32(IProject.ReqMode);
            if (IProject.PageIndex != "")
                PageIndex = Convert.ToInt32(IProject.PageIndex);
            if (IProject.PageSize != "")
                PageSize = Convert.ToInt32(IProject.PageSize);
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);
            if (IProject.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IProject.Critrea1);
            if (IProject.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IProject.Critrea2);
            if (IProject.Critrea3 != "")
                Critrea3 = IProject.Critrea3;
            if (IProject.Critrea4 != "")
                Critrea4 = IProject.Critrea4;
            if (IProject.ID != "")
                ID = Convert.ToInt32(IProject.ID);
            if (IProject.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IProject.Critrea5);
            if (IProject.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IProject.Critrea6);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "proAPIProjectSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, TransMode);
            db.AddInParameter(dbCommand, "@Pagemode", DbType.Int64, Pagemode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, FK_Company);
            db.AddInParameter(dbCommand, "@Critrea1", DbType.Int64, Critrea1);
            db.AddInParameter(dbCommand, "@Critrea2", DbType.Int64, Critrea2);
            db.AddInParameter(dbCommand, "@Critrea3", DbType.String, Critrea3);
            db.AddInParameter(dbCommand, "@Critrea4", DbType.String, Critrea4);
            db.AddInParameter(dbCommand, "@TotalCount", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IProject.Name);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, ID);
            db.AddInParameter(dbCommand, "@Criteria5", DbType.Int64, Criteria5);
            db.AddInParameter(dbCommand, "@Criteria6", DbType.Int64, Criteria6);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalSiteVisitAssignmentDetails(IProjectDetails IProject)
        {
           
            Int64 FK_Company = 1;
            Int32 ReqMode = 0, SubMode = 0;

            if (IProject.ReqMode != "")
                ReqMode = Convert.ToInt32(IProject.ReqMode);
            if (IProject.SubMode != "")
                SubMode = Convert.ToInt32(IProject.SubMode);           
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);
       

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "ProAPISiteVisitAssignmentDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalEmployeeDetails(IProjectDetails IProject)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, FK_Project = 0, FK_Stages = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IProject.TransMode != "")
                TransMode = IProject.TransMode;
            if (IProject.ReqMode != "")
                Pagemode = Convert.ToInt32(IProject.ReqMode);
            if (IProject.PageIndex != "")
                PageIndex = Convert.ToInt32(IProject.PageIndex);
            if (IProject.PageSize != "")
                PageSize = Convert.ToInt32(IProject.PageSize);
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);
            if (IProject.FK_Project != "")
                FK_Project = Convert.ToInt32(IProject.FK_Project);
            if (IProject.FK_Stages != "")
                FK_Stages = Convert.ToInt32(IProject.FK_Stages);
            if (IProject.Critrea3 != "")
                Critrea3 = IProject.Critrea3;
            if (IProject.Critrea4 != "")
                Critrea4 = IProject.Critrea4;
            if (IProject.ID != "")
                ID = Convert.ToInt32(IProject.ID);
            if (IProject.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IProject.Critrea5);
            if (IProject.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IProject.Critrea6);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "proAPICmnSearchValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, TransMode);
            db.AddInParameter(dbCommand, "@Pagemode", DbType.Int64, Pagemode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, FK_Company);
            db.AddInParameter(dbCommand, "@Critrea1", DbType.Int64, FK_Project);
            db.AddInParameter(dbCommand, "@Critrea2", DbType.Int64, FK_Stages);
            db.AddInParameter(dbCommand, "@Critrea3", DbType.String, Critrea3);
            db.AddInParameter(dbCommand, "@Critrea4", DbType.String, Critrea4);
            db.AddInParameter(dbCommand, "@TotalCount", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IProject.Name);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, ID);
            db.AddInParameter(dbCommand, "@Criteria5", DbType.Int64, Criteria5);
            db.AddInParameter(dbCommand, "@Criteria6", DbType.Int64, Criteria6);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProGetBillType(IProjectDetails IProject,Int16 Mode)
        {

            Int64 FK_Company = 1, FK_Project=0, FK_Employee=0;
            

            if (IProject.FK_Project != "")
                FK_Project = Convert.ToInt64(IProject.FK_Project);
            if (IProject.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IProject.FK_Employee);
            if (IProject.FK_Company != "")
                FK_Company = Convert.ToInt32(IProject.FK_Company);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IProject.BankKey);
            string sqlCommand = "ProGetBillType";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Project", DbType.Int64, FK_Project);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, Mode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);

            try
            {
                dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

    }
}