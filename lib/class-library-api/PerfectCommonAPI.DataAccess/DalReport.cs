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
  public class DalReport
    {
        public DataTable DalSearchCommonValidate(IReport IReport)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IReport.TransMode != "")
                TransMode = IReport.TransMode;
            if (IReport.ReqMode != "")
                Pagemode = Convert.ToInt32(IReport.ReqMode);
            if (IReport.PageIndex != "")
                PageIndex = Convert.ToInt32(IReport.PageIndex);
            if (IReport.PageSize != "")
                PageSize = Convert.ToInt32(IReport.PageSize);
            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt32(IReport.FK_Company);
            if (IReport.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IReport.Critrea1);
            if (IReport.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IReport.Critrea2);
            if (IReport.Critrea3 != "")
                Critrea3 = IReport.Critrea3;
            if (IReport.Critrea4 != "")
                Critrea4 = IReport.Critrea4;
            if (IReport.ID != "")
                ID = Convert.ToInt32(IReport.ID);
            if (IReport.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IReport.Critrea5);
            if (IReport.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IReport.Critrea6);
            if (Pagemode == 35)
            {
                Critrea3 = "Cancelled=0 AND Passed=1 AND CatLeadGenerate = "+IReport.Criteria+" AND FK_Company = "+ IReport.FK_Company+" AND Project = "+IReport.Project;
                
            }

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
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
            //db.AddInParameter(dbCommand, "@Name", DbType.String, IReport.Name);
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
        public DataTable DalCommonValidate(IReport IReport)
        {

            Int32 ReqMode = 0, SubMode = 0, IsOnline = 0;
            Int64 FK_Employee = 0, ID_LeadFrom = 0, FK_Category = 0, ID_Department = 0, ID_BranchType = 0, ID_ReportSettings = 0, ID_LeadGenerateProduct = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType = 0, ID_Branch = 0, ID_NotificationDetails = 0, ID_LeadDocumentDetails = 0, FK_MediaMaster = 0, FK_Company = 0, FK_User = 0, FK_BranchCodeUser = 0, FK_Product = 0,
                FK_Priority = 0, FollowUpAction = 0, FollowUpType = 0, Criteria = 0, Status = 0, FK_CollectedBy = 0, Category = 0, BranchCode = 0, FK_Customer = 0, FK_CustomerOthers = 0, FK_Customerserviceregister = 0
                , ID_CustomerWiseProductDetails = 0, FK_Designation = 0;
            Int16 ID_TodoListLeadDetails = 0;


            Int16 ReportMode = 0, GroupId = 0;
            bool PrductOnly = false;
            if (IReport.ReqMode != "")
                ReqMode = Convert.ToInt32(IReport.ReqMode);
            if (IReport.SubMode != "")
                SubMode = Convert.ToInt32(IReport.SubMode);
            //if (IReport.ID_CustomerWiseProductDetails != "")
            //    ID_CustomerWiseProductDetails = Convert.ToInt64(IReport.ID_CustomerWiseProductDetails);
            //if (IReport.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IReport.FK_Employee==""?"0": IReport.FK_Employee);
            if (IReport.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(IReport.ID_LeadFrom);
            if (IReport.ID_Category != "")
                FK_Category = Convert.ToInt64(IReport.ID_Category);
            if (IReport.ID_Department != "")
                ID_Department = Convert.ToInt64(IReport.ID_Department);
            if (IReport.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(IReport.ID_BranchType);
            if (IReport.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(IReport.ID_ReportSettings);
            if (IReport.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IReport.ID_LeadGenerateProduct);
            if (IReport.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(IReport.PrductOnly);
            if (IReport.FK_Country != "")
                FK_Country = Convert.ToInt64(IReport.FK_Country);
            if (IReport.FK_District != "")
                FK_District = Convert.ToInt64(IReport.FK_District);
            if (IReport.FK_States != "")
                FK_States = Convert.ToInt64(IReport.FK_States);
            if (IReport.FK_Area != "")
                FK_Area = Convert.ToInt64(IReport.FK_Area);
            if (IReport.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IReport.ID_LeadGenerate);
            if (IReport.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(IReport.ID_ActionType);
            if (IReport.IsOnline != "")
                IsOnline = Convert.ToInt32(IReport.IsOnline);
            if (IReport.ReportMode != "")
                ReportMode = Convert.ToInt16(IReport.ReportMode);
            if (IReport.ID_Branch != "")
                ID_Branch = Convert.ToInt64(IReport.ID_Branch);
            if (IReport.GroupId != "")
                GroupId = Convert.ToInt16(IReport.GroupId);
            if (IReport.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(IReport.ID_NotificationDetails);
            if (IReport.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(IReport.ID_LeadDocumentDetails);
            //if (IReport.FK_MediaMaster != "")
            //    IReport = Convert.ToInt32(IReport.FK_MediaMaster);
            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt32(IReport.FK_Company);
            if (IReport.FK_User != "")
                FK_User = Convert.ToInt32(IReport.FK_User);
            if (IReport.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(IReport.FK_BranchCodeUser);
            if (IReport.FK_Product != "")
                FK_Product = Convert.ToInt32(IReport.FK_Product);
            if (IReport.FK_Priority != "")
                FK_Product = Convert.ToInt32(IReport.FK_Priority);
            if (IReport.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(IReport.FollowUpAction);
            if (IReport.FollowUpType != "")
                FollowUpType = Convert.ToInt32(IReport.FollowUpType);
            if (IReport.Criteria != "")
                Criteria = Convert.ToInt32(IReport.Criteria);
            if (IReport.Status != "")
                Status = Convert.ToInt32(IReport.Status);
            //if (IReport.FK_CollectedBy != "")
            //    FK_CollectedBy = Convert.ToInt32(IReport.FK_CollectedBy);
            //if (IReport.Category != "")
            //    Category = Convert.ToInt32(IReport.Category);
            //if (IReport.BranchCode != "")
            //    BranchCode = Convert.ToInt64(IReport.BranchCode);
            //if (IReport.ID_TodoListLeadDetails != "")
            //    ID_TodoListLeadDetails = Convert.ToInt16(IReport.ID_TodoListLeadDetails);
            //if (IReport.FK_Customer != "")
            //    FK_Customer = Convert.ToInt32(IReport.FK_Customer);
            //if (IReport.FK_CustomerOthers != "")
            //    FK_CustomerOthers = Convert.ToInt32(IReport.FK_CustomerOthers);
            //if (IReport.FK_Customerserviceregister != "")
            //    FK_Customerserviceregister = Convert.ToInt32(IReport.FK_Customerserviceregister);

            if (IReport.FK_Designation != "")
                FK_Designation = Convert.ToInt32(IReport.FK_Designation);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, IReport.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, IReport.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, IReport.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, IReport.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, IReport.OldMPIN);
            //db.AddInParameter(dbCommand, "@Token", DbType.String, IReport.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IReport.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, IReport.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, IReport.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IReport.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IReport.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, IReport.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, IReport.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, IReport.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, IReport.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, IReport.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IReport.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IReport.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IReport.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, IReport.LeadNo);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int64, Criteria);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@FK_CollectedBy", DbType.Int64, FK_CollectedBy);
            db.AddInParameter(dbCommand, "@Category", DbType.Int64, Category);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, BranchCode);
            db.AddInParameter(dbCommand, "@ID_TodoListLeadDetails", DbType.Int16, ID_TodoListLeadDetails);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_CustomerOtherID", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@MasterID", DbType.Int64, ID_CustomerWiseProductDetails);
            db.AddInParameter(dbCommand, "@FK_Designation", DbType.Int64, FK_Designation);


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
        public DataTable DalProProjectReport(IReport IReport)
        {
            Int64 FK_Company = 1, Status = 0;


            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt64(IReport.FK_Company);

          
            if (IReport.Status != "")
                Status = Convert.ToInt64(IReport.Status);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
            string sqlCommand = "ProRptProject";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int32, IReport.ReportMode == "" ? "0" : IReport.ReportMode);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IReport.FromDate==""?"0":IReport.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IReport.Todate==""?"0":IReport.Todate);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IReport.FK_Company==""?"0":IReport.FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, IReport.FK_BranchCodeUser==""?"0":IReport.FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IReport.EntrBy==""?"0":IReport.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.String, IReport.FK_Machine==""?"0":IReport.FK_Machine);
            db.AddInParameter(dbCommand, "@Debug", DbType.String, IReport.Debug==""?"0":IReport.Debug);
            db.AddInParameter(dbCommand, "@TableCount", DbType.Int32, IReport.TableCount==""?"0":IReport.TableCount);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, IReport.LeadNo==""?"0":IReport.LeadNo);
            db.AddInParameter(dbCommand, "@Criteria", DbType.String, IReport.Criteria==""?"0":IReport.Criteria);
            db.AddInParameter(dbCommand, "@Category", DbType.Int64, IReport.Category==""?"0":IReport.Category);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, IReport.FK_Area==""?"0":IReport.FK_Area);

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
        public DataTable DalProServiceReport(IReport IReport)
        {
            Int64 FK_Company = 1, Status = 0;


            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt64(IReport.FK_Company);


            if (IReport.Status != "")
                Status = Convert.ToInt64(IReport.Status);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
            string sqlCommand = "ProRptServiceManagement";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, IReport.ReportMode==""?"0":IReport.ReportMode);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, IReport.FromDate==""?"0":IReport.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, IReport.Todate==""?"0":IReport.Todate);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, IReport.FK_Product==""?"0": IReport.FK_Product);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, IReport.FK_Branch == "" ? "0" : IReport.FK_Branch);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, IReport.FK_Employee==""?"0":IReport.FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Priority ", DbType.Int64, IReport.FK_Priority==""?"0": IReport.FK_Priority);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, IReport.FK_BranchCodeUser==""?"0": IReport.FK_BranchCodeUser);

            db.AddInParameter(dbCommand, "@ComplaintType", DbType.Int16, IReport.ComplaintType == "" ? "0" : IReport.ComplaintType);
            db.AddInParameter(dbCommand, "@ComplaintService", DbType.Int16, IReport.ComplaintService==""?"0": IReport.ComplaintService);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IReport.EntrBy==""?"0":IReport.EntrBy);
            db.AddInParameter(dbCommand, "@TicketNo", DbType.String, IReport.TicketNo==""?"0":IReport.TicketNo);
            db.AddInParameter(dbCommand, "@Status", DbType.String, IReport.Status==""?"0":IReport.Status);
            db.AddInParameter(dbCommand, "@DueDaysFrom", DbType.Int32, IReport.DueDaysFrom==""?"0":IReport.DueDaysFrom);
            db.AddInParameter(dbCommand, "@DueDaysTo", DbType.Int32, IReport.DueDaysTo==""?"0":IReport.DueDaysTo);
            db.AddInParameter(dbCommand, "@DueCriteria", DbType.Int16, IReport.DueCriteria==""?"0":IReport.DueCriteria);
            db.AddInParameter(dbCommand, "@ReplacementType", DbType.Int16, IReport.ReplacementType==""?"0":IReport.ReplacementType);
            db.AddInParameter(dbCommand, "@FK_NextAction", DbType.Int64, IReport.FK_NextAction==""?"0": IReport.FK_NextAction);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IReport.FK_Company==""?"0":IReport.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, IReport.FK_Machine==""?"0":IReport.FK_Machine);
            db.AddInParameter(dbCommand, "@Debug", DbType.Int16, IReport.Debug==""?"0":IReport.Debug);
            db.AddInParameter(dbCommand, "@TableCount", DbType.Int16, IReport.TableCount==""?"0":IReport.TableCount);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int16,IReport.Criteria==""?"0":IReport.Criteria);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, IReport.FK_Category == "" ? "0" : IReport.FK_Category);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, IReport.FK_Area==""?"0":IReport.FK_Area);

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
        public DataTable DalProLeadReport(IReport IReport)
        {
            Int64 FK_Company = 1, Status = 0;


            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt64(IReport.FK_Company);


            if (IReport.Status != "")
                Status = Convert.ToInt64(IReport.Status);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
            string sqlCommand = "ProAPIRptLeadSummary";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, IReport.FromDate == "" ? "0" : IReport.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, IReport.Todate == "" ? "0" : IReport.Todate);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IReport.FK_Company == "" ? "0" : IReport.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, IReport.FK_Employee == "" ? "0" : IReport.FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, IReport.FK_Product == "" ? "0" : IReport.FK_Product);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, IReport.FK_Category == "" ? "0" : IReport.FK_Category);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int64, IReport.ReportMode == "" ? "0" : IReport.ReportMode);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, IReport.FK_Branch == "" ? "0" : IReport.FK_Branch);


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
        public DataTable DalProSummaryLeadReport(IReport IReport)
        {
            Int64 FK_Company = 1, Status = 0;


            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt64(IReport.FK_Company);


            if (IReport.Status != "")
                Status = Convert.ToInt64(IReport.Status);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
            string sqlCommand = "ProAPIRptLeadSummary";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, IReport.FromDate == "" ? "0" : IReport.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, IReport.Todate == "" ? "0" : IReport.Todate);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IReport.FK_Company == "" ? "0" : IReport.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, IReport.FK_Employee == "" ? "0" : IReport.FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, IReport.FK_Product == "" ? "0" : IReport.FK_Product);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, IReport.FK_Category == "" ? "0" : IReport.FK_Category);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int64, IReport.ReportMode == "" ? "0" : IReport.ReportMode);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, IReport.FK_Branch == "" ? "0" : IReport.FK_Branch);


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
        public DataTable DalLeadSummaryDetailsReport(IReport IReport)
        {
            Int64 FK_Company = 0, ID = 0;
            if (IReport.FK_Company != "")
                FK_Company = Convert.ToInt64(IReport.FK_Company);
            if (IReport.ID != "")
                ID = Convert.ToInt64(IReport.ID);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IReport.BankKey);
            string sqlCommand = "ProAPIRptLeadSummaryDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, IReport.FromDate == "" ? "0" : IReport.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.DateTime, IReport.Todate == "" ? "0" : IReport.Todate);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IReport.FK_Company == "" ? "0" : IReport.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, IReport.FK_Employee == "" ? "0" : IReport.FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, IReport.FK_Product == "" ? "0" : IReport.FK_Product);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, IReport.FK_Category == "" ? "0" : IReport.FK_Category);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int64, IReport.ReportMode == "" ? "0" : IReport.ReportMode);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, IReport.FK_Branch == "" ? "0" : IReport.FK_Branch);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int16, IReport.SubMode == "" ? "0" : IReport.SubMode);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, ID);

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
