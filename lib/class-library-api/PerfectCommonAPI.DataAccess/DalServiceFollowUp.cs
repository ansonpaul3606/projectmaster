using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalServiceFollowUp
    {
        public DataSet DalCommonValidatewithDataset(IServiceFollowUp iServiceFollowUp)
        {


            Int32 ReqMode = 0, SubMode = 0, IsOnline = 0;
            Int64 FK_Employee = 0, ID_LeadFrom = 0, FK_Category = 0, ID_Department = 0, ID_BranchType = 0, ID_ReportSettings = 0, ID_LeadGenerateProduct = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType = 0, ID_Branch = 0, ID_NotificationDetails = 0, ID_LeadDocumentDetails = 0, FK_MediaMaster = 0, FK_Company = 0, FK_User = 0, FK_BranchCodeUser = 0, FK_Product = 0,
                FK_Priority = 0, FollowUpAction = 0, FollowUpType = 0, Criteria = 0, Status = 0, FK_CollectedBy = 0, Category = 0, BranchCode = 0, FK_Customer = 0, FK_CustomerOthers = 0, FK_Customerserviceregister = 0;
            Int16 ID_TodoListLeadDetails = 0;

            string NameCriteria = "";
            Int16 ReportMode = 0, GroupId = 0;
            bool PrductOnly = false;
            if (iServiceFollowUp.ReqMode != "")
                ReqMode = Convert.ToInt32(iServiceFollowUp.ReqMode);
            if (iServiceFollowUp.SubMode != "")
                SubMode = Convert.ToInt32(iServiceFollowUp.SubMode);
            if (iServiceFollowUp.FK_Employee != "")
                FK_Employee = Convert.ToInt64(iServiceFollowUp.FK_Employee);
            if (iServiceFollowUp.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(iServiceFollowUp.ID_LeadFrom);
            if (iServiceFollowUp.ID_Category != "")
                FK_Category = Convert.ToInt64(iServiceFollowUp.ID_Category);
            if (iServiceFollowUp.ID_Department != "")
                ID_Department = Convert.ToInt64(iServiceFollowUp.ID_Department);
            if (iServiceFollowUp.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(iServiceFollowUp.ID_BranchType);
            if (iServiceFollowUp.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(iServiceFollowUp.ID_ReportSettings);
            if (iServiceFollowUp.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(iServiceFollowUp.ID_LeadGenerateProduct);
            if (iServiceFollowUp.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(iServiceFollowUp.PrductOnly);
            if (iServiceFollowUp.FK_Country != "")
                FK_Country = Convert.ToInt64(iServiceFollowUp.FK_Country);
            if (iServiceFollowUp.FK_District != "")
                FK_District = Convert.ToInt64(iServiceFollowUp.FK_District);
            if (iServiceFollowUp.FK_States != "")
                FK_States = Convert.ToInt64(iServiceFollowUp.FK_States);
            if (iServiceFollowUp.FK_Area != "")
                FK_Area = Convert.ToInt64(iServiceFollowUp.FK_Area);
            if (iServiceFollowUp.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(iServiceFollowUp.ID_LeadGenerate);
            if (iServiceFollowUp.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(iServiceFollowUp.ID_ActionType);
            if (iServiceFollowUp.IsOnline != "")
                IsOnline = Convert.ToInt32(iServiceFollowUp.IsOnline);
            if (iServiceFollowUp.ReportMode != "")
                ReportMode = Convert.ToInt16(iServiceFollowUp.ReportMode);
            if (iServiceFollowUp.ID_Branch != "")
                ID_Branch = Convert.ToInt64(iServiceFollowUp.ID_Branch);
            if (iServiceFollowUp.GroupId != "")
                GroupId = Convert.ToInt16(iServiceFollowUp.GroupId);
            if (iServiceFollowUp.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(iServiceFollowUp.ID_NotificationDetails);
            if (iServiceFollowUp.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(iServiceFollowUp.ID_LeadDocumentDetails);
            if (iServiceFollowUp.FK_MediaMaster != "")
                FK_MediaMaster = Convert.ToInt32(iServiceFollowUp.FK_MediaMaster);
            if (iServiceFollowUp.FK_Company != "")
                FK_Company = Convert.ToInt32(iServiceFollowUp.FK_Company);
            if (iServiceFollowUp.FK_User != "")
                FK_User = Convert.ToInt32(iServiceFollowUp.FK_User);
            if (iServiceFollowUp.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(iServiceFollowUp.FK_BranchCodeUser);
            if (iServiceFollowUp.FK_Product != "")
                FK_Product = Convert.ToInt32(iServiceFollowUp.FK_Product);
            if (iServiceFollowUp.FK_Priority != "")
                FK_Priority = Convert.ToInt32(iServiceFollowUp.FK_Priority);
            if (iServiceFollowUp.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(iServiceFollowUp.FollowUpAction);
            if (iServiceFollowUp.FollowUpType != "")
                FollowUpType = Convert.ToInt32(iServiceFollowUp.FollowUpType);
            if (iServiceFollowUp.Criteria != "")
                Criteria = Convert.ToInt32(iServiceFollowUp.Criteria);
            if (iServiceFollowUp.Status != "")
                Status = Convert.ToInt32(iServiceFollowUp.Status);
            if (iServiceFollowUp.FK_CollectedBy != "")
                FK_CollectedBy = Convert.ToInt32(iServiceFollowUp.FK_CollectedBy);
            if (iServiceFollowUp.Category != "")
                Category = Convert.ToInt32(iServiceFollowUp.Category);
            if (iServiceFollowUp.BranchCode != "")
                BranchCode = Convert.ToInt64(iServiceFollowUp.BranchCode);
            if (iServiceFollowUp.ID_TodoListLeadDetails != "")
                ID_TodoListLeadDetails = Convert.ToInt16(iServiceFollowUp.ID_TodoListLeadDetails);
            if (iServiceFollowUp.FK_Customer != "")
                FK_Customer = Convert.ToInt32(iServiceFollowUp.FK_Customer);
            if (iServiceFollowUp.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt32(iServiceFollowUp.FK_CustomerOthers);
            if (iServiceFollowUp.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(iServiceFollowUp.FK_Customerserviceregister);

            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + iServiceFollowUp.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, iServiceFollowUp.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, iServiceFollowUp.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, iServiceFollowUp.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, iServiceFollowUp.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, iServiceFollowUp.OldMPIN);
            db.AddInParameter(dbCommand, "@Token", DbType.String, iServiceFollowUp.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iServiceFollowUp.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, iServiceFollowUp.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, iServiceFollowUp.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, iServiceFollowUp.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, iServiceFollowUp.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, iServiceFollowUp.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, iServiceFollowUp.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, iServiceFollowUp.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, iServiceFollowUp.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, iServiceFollowUp.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, iServiceFollowUp.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, iServiceFollowUp.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, iServiceFollowUp.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, iServiceFollowUp.LeadNo);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int64, Criteria);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@FK_CollectedBy", DbType.Int64, FK_CollectedBy);
            db.AddInParameter(dbCommand, "@Category", DbType.Int64, Category);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, BranchCode);
            db.AddInParameter(dbCommand, "@ID_TodoListLeadDetails", DbType.Int16, ID_TodoListLeadDetails);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_CustomerOtherID", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@NameCriteria", DbType.String, iServiceFollowUp.NameCriteria);
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand);
                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalSearchCommonValidate(IServiceFollowUp IServiceFollup)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0,Criteria3 = 0;


            if (IServiceFollup.TransMode != "")
                TransMode = IServiceFollup.TransMode;
            if (IServiceFollup.ReqMode != "")
                Pagemode = Convert.ToInt32(IServiceFollup.ReqMode);
           
            if (IServiceFollup.FK_Company != "")
                FK_Company = Convert.ToInt32(IServiceFollup.FK_Company);

            if (IServiceFollup.Criteria != "")
                Critrea1 = Convert.ToInt32(IServiceFollup.Criteria);
            if (IServiceFollup.Criteria4 != "")
                Critrea2 = Convert.ToInt32(IServiceFollup.Criteria4);
            if (IServiceFollup.Criteria3 != "")
                Criteria3 = Convert.ToInt32(IServiceFollup.Criteria3);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IServiceFollup.BankKey);
            string sqlCommand = "proAPICmnSearchPopup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, TransMode);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int64, Pagemode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, FK_Company);
            db.AddInParameter(dbCommand, "@Critrea1", DbType.Int64, Critrea1);
            db.AddInParameter(dbCommand, "@Critrea2", DbType.Int64, Critrea2);
            db.AddInParameter(dbCommand, "@Critrea3", DbType.String, IServiceFollup.Criteria3);
            db.AddInParameter(dbCommand, "@Critrea4", DbType.String, Critrea4);
            db.AddInParameter(dbCommand, "@TotalCount", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IServiceFollup.Name);
            db.AddInParameter(dbCommand, "@NameCriteria", DbType.String, IServiceFollup.NameCriteria);
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
        public DataTable DalCommonValidate(IServiceFollowUp iServiceFollowUp)
        {

            Int32 ReqMode = 0, SubMode = 0, IsOnline = 0;
            Int64 FK_Employee = 0, ID_LeadFrom = 0, FK_Category = 0, ID_Department = 0, ID_BranchType = 0, ID_ReportSettings = 0, ID_LeadGenerateProduct = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType = 0, ID_Branch = 0, ID_NotificationDetails = 0, ID_LeadDocumentDetails = 0, FK_MediaMaster = 0, FK_Company = 0, FK_User = 0, FK_BranchCodeUser = 0, FK_Product = 0,
                FK_Priority = 0, FollowUpAction = 0, FollowUpType = 0, Criteria = 0, Status = 0, FK_CollectedBy = 0, Category = 0, BranchCode = 0, FK_Customer = 0, FK_CustomerOthers = 0, FK_Customerserviceregister = 0;
            Int16 ID_TodoListLeadDetails = 0;


            Int16 ReportMode = 0, GroupId = 0;
            bool PrductOnly = false;
            if (iServiceFollowUp.ReqMode != "")
                ReqMode = Convert.ToInt32(iServiceFollowUp.ReqMode);
            if (iServiceFollowUp.SubMode != "")
                SubMode = Convert.ToInt32(iServiceFollowUp.SubMode);
            if (iServiceFollowUp.FK_Employee != "")
                FK_Employee = Convert.ToInt64(iServiceFollowUp.FK_Employee);
            if (iServiceFollowUp.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(iServiceFollowUp.ID_LeadFrom);
            if (iServiceFollowUp.ID_Category != "")
                FK_Category = Convert.ToInt64(iServiceFollowUp.ID_Category);
            if (iServiceFollowUp.ID_Department != "")
                ID_Department = Convert.ToInt64(iServiceFollowUp.ID_Department);
            if (iServiceFollowUp.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(iServiceFollowUp.ID_BranchType);
            if (iServiceFollowUp.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(iServiceFollowUp.ID_ReportSettings);
            if (iServiceFollowUp.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(iServiceFollowUp.ID_LeadGenerateProduct);
            if (iServiceFollowUp.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(iServiceFollowUp.PrductOnly);
            if (iServiceFollowUp.FK_Country != "")
                FK_Country = Convert.ToInt64(iServiceFollowUp.FK_Country);
            if (iServiceFollowUp.FK_District != "")
                FK_District = Convert.ToInt64(iServiceFollowUp.FK_District);
            if (iServiceFollowUp.FK_States != "")
                FK_States = Convert.ToInt64(iServiceFollowUp.FK_States);
            if (iServiceFollowUp.FK_Area != "")
                FK_Area = Convert.ToInt64(iServiceFollowUp.FK_Area);
            if (iServiceFollowUp.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(iServiceFollowUp.ID_LeadGenerate);
            if (iServiceFollowUp.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(iServiceFollowUp.ID_ActionType);
            if (iServiceFollowUp.IsOnline != "")
                IsOnline = Convert.ToInt32(iServiceFollowUp.IsOnline);
            if (iServiceFollowUp.ReportMode != "")
                ReportMode = Convert.ToInt16(iServiceFollowUp.ReportMode);
            if (iServiceFollowUp.ID_Branch != "")
                ID_Branch = Convert.ToInt64(iServiceFollowUp.ID_Branch);
            if (iServiceFollowUp.GroupId != "")
                GroupId = Convert.ToInt16(iServiceFollowUp.GroupId);
            if (iServiceFollowUp.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(iServiceFollowUp.ID_NotificationDetails);
            if (iServiceFollowUp.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(iServiceFollowUp.ID_LeadDocumentDetails);
            if (iServiceFollowUp.FK_MediaMaster != "")
                FK_MediaMaster = Convert.ToInt32(iServiceFollowUp.FK_MediaMaster);
            if (iServiceFollowUp.FK_Company != "")
                FK_Company = Convert.ToInt32(iServiceFollowUp.FK_Company);
            if (iServiceFollowUp.FK_User != "")
                FK_User = Convert.ToInt32(iServiceFollowUp.FK_User);
            if (iServiceFollowUp.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(iServiceFollowUp.FK_BranchCodeUser);
            if (iServiceFollowUp.FK_Product != "")
                FK_Product = Convert.ToInt32(iServiceFollowUp.FK_Product);
            if (iServiceFollowUp.FK_Priority != "")
                FK_Priority = Convert.ToInt32(iServiceFollowUp.FK_Priority);
            if (iServiceFollowUp.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(iServiceFollowUp.FollowUpAction);
            if (iServiceFollowUp.FollowUpType != "")
                FollowUpType = Convert.ToInt32(iServiceFollowUp.FollowUpType);
            if (iServiceFollowUp.Criteria != "")
                Criteria = Convert.ToInt32(iServiceFollowUp.Criteria);
            if (iServiceFollowUp.Status != "")
                Status = Convert.ToInt32(iServiceFollowUp.Status);
            if (iServiceFollowUp.FK_CollectedBy != "")
                FK_CollectedBy = Convert.ToInt32(iServiceFollowUp.FK_CollectedBy);
            if (iServiceFollowUp.Category != "")
                Category = Convert.ToInt32(iServiceFollowUp.Category);
            if (iServiceFollowUp.BranchCode != "")
                BranchCode = Convert.ToInt64(iServiceFollowUp.BranchCode);
            if (iServiceFollowUp.ID_TodoListLeadDetails != "")
                ID_TodoListLeadDetails = Convert.ToInt16(iServiceFollowUp.ID_TodoListLeadDetails);
            if (iServiceFollowUp.FK_Customer != "")
                FK_Customer = Convert.ToInt32(iServiceFollowUp.FK_Customer);
            if (iServiceFollowUp.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt32(iServiceFollowUp.FK_CustomerOthers);
            if (iServiceFollowUp.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(iServiceFollowUp.FK_Customerserviceregister);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + iServiceFollowUp.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, iServiceFollowUp.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, iServiceFollowUp.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, iServiceFollowUp.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, iServiceFollowUp.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, iServiceFollowUp.OldMPIN);
            db.AddInParameter(dbCommand, "@Token", DbType.String, iServiceFollowUp.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, iServiceFollowUp.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, iServiceFollowUp.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, iServiceFollowUp.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, iServiceFollowUp.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, iServiceFollowUp.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, iServiceFollowUp.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, iServiceFollowUp.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, iServiceFollowUp.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, iServiceFollowUp.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, iServiceFollowUp.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, iServiceFollowUp.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, iServiceFollowUp.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, iServiceFollowUp.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, iServiceFollowUp.LeadNo);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int64, Criteria);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@FK_CollectedBy", DbType.Int64, FK_CollectedBy);
            db.AddInParameter(dbCommand, "@Category", DbType.Int64, Category);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, BranchCode);
            db.AddInParameter(dbCommand, "@ID_TodoListLeadDetails", DbType.Int16, ID_TodoListLeadDetails);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_CustomerOtherID", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
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
        public DataTable DalServiceFollowUpUpdate(IServiceFollowUp iServiceFollowUp)
        {

          
            Int64 FK_Employee = 0, FK_Company = 0,  FK_BranchCodeUser = 0, FK_Customer = 0, ID_CustomerServiceRegisterProductDetails=0,  FK_Customerserviceregister = 0, FK_BillType=0, FK_NextActionLead = 0, FK_ActionTypeLead=0,FK_EmployeeLead=0;
            Int32 FK_ActionType = 0, FK_NextAction=0;
            decimal ComponentCharge = 0, OtherCharge=0, TotalAmount=0, ServiceCharge=0, DiscountAmount =0, TotalSecurityAmount=0;
            if (iServiceFollowUp.FK_Company != "")
                FK_Company = Convert.ToInt32(iServiceFollowUp.FK_Company);            
            if (iServiceFollowUp.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(iServiceFollowUp.FK_BranchCodeUser);         
            if (iServiceFollowUp.FK_Customer != "")
                FK_Customer = Convert.ToInt32(iServiceFollowUp.FK_Customer);            
            if (iServiceFollowUp.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(iServiceFollowUp.FK_Customerserviceregister);
            if (iServiceFollowUp.FK_Employee != "")
                FK_Employee = Convert.ToInt64(iServiceFollowUp.FK_Employee);
            if (iServiceFollowUp.FK_ActionType != "")
                FK_ActionType = Convert.ToInt32(iServiceFollowUp.FK_ActionType);
            if (iServiceFollowUp.FK_NextAction != "")
                FK_NextAction = Convert.ToInt32(iServiceFollowUp.FK_NextAction);
            if (iServiceFollowUp.FK_BillType != "")
                FK_BillType = Convert.ToInt64(iServiceFollowUp.FK_BillType);
            if (iServiceFollowUp.FK_NextActionLead != "")
                FK_NextActionLead = Convert.ToInt64(iServiceFollowUp.FK_NextActionLead);
            if (iServiceFollowUp.FK_ActionTypeLead != "")
                FK_ActionTypeLead = Convert.ToInt64(iServiceFollowUp.FK_ActionTypeLead);
            if (iServiceFollowUp.FK_EmployeeLead != "")
                FK_EmployeeLead = Convert.ToInt64(iServiceFollowUp.FK_EmployeeLead);
            if (iServiceFollowUp.ComponentCharge != "")
                ComponentCharge = Convert.ToDecimal(iServiceFollowUp.ComponentCharge);
            if (iServiceFollowUp.OtherCharge != "")
                OtherCharge = Convert.ToDecimal(iServiceFollowUp.OtherCharge);
            if (iServiceFollowUp.TotalAmount != "")
                TotalAmount = Convert.ToDecimal(iServiceFollowUp.TotalAmount);
            if (iServiceFollowUp.DiscountAmount != "")
                DiscountAmount = Convert.ToDecimal(iServiceFollowUp.DiscountAmount);
            if (iServiceFollowUp.TotalSecurityAmount != "")
                TotalSecurityAmount = Convert.ToDecimal(iServiceFollowUp.TotalSecurityAmount);
            if (iServiceFollowUp.ServiceCharge != "")
                ServiceCharge = Convert.ToDecimal(iServiceFollowUp.ServiceCharge);
            if (iServiceFollowUp.ID_CustomerServiceRegisterProductDetails != "")
                ID_CustomerServiceRegisterProductDetails = Convert.ToInt32(iServiceFollowUp.ID_CustomerServiceRegisterProductDetails);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + iServiceFollowUp.BankKey);
            string sqlCommand = "ProServiceFollowUpUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, 1);
            db.AddInParameter(dbCommand, "@ID_ServiceFollowUp", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@ID_CustomerServiceRegisterProductDetails", DbType.Int64, ID_CustomerServiceRegisterProductDetails);
            //   db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            //  db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, FK_Customer);
            //  db.AddInParameter(dbCommand, "@CustomerNotes", DbType.String, iServiceFollowUp.CustomerNotes);
            //  db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, iServiceFollowUp.EmployeeNote);
            db.AddInParameter(dbCommand, "@StartingDate", DbType.String, iServiceFollowUp.StartingDate);
            db.AddInParameter(dbCommand, "@ComponentCharge", DbType.String, ComponentCharge);
            db.AddInParameter(dbCommand, "@ServiceCharge", DbType.Decimal, ServiceCharge);
            db.AddInParameter(dbCommand, "@OtherCharge", DbType.Decimal, OtherCharge);
            db.AddInParameter(dbCommand, "@TotalAmount", DbType.Decimal, TotalAmount);
         //   db.AddInParameter(dbCommand, "@ReplaceAmount", DbType.Decimal, iServiceFollowUp.ReplaceAmount);
            db.AddInParameter(dbCommand, "@DiscountAmount", DbType.Decimal, DiscountAmount);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, iServiceFollowUp.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, iServiceFollowUp.TransMode);
          //  db.AddInParameter(dbCommand, "@FK_ActionType", DbType.Int32, FK_ActionType);
         //   db.AddInParameter(dbCommand, "@FK_NextAction", DbType.Int32, FK_NextAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "@ServiceIncentiveDetails", DbType.Xml, iServiceFollowUp.ServiceIncentive);
            db.AddInParameter(dbCommand, "@CustomerProductdetails", DbType.Xml, iServiceFollowUp.ServiceDetails);
            db.AddInParameter(dbCommand, "@Subproductreplacedetails", DbType.Xml, iServiceFollowUp.ProductDetails);
            db.AddInParameter(dbCommand, "@AttendedEmployeeDetails", DbType.Xml, iServiceFollowUp.AttendedEmployeeDetails);
            db.AddInParameter(dbCommand, "@Actionproductdetails", DbType.Xml, iServiceFollowUp.Actionproductdetails);
            db.AddInParameter(dbCommand, "@OtherChgDetails", DbType.Xml, iServiceFollowUp.OtherCharges);
            db.AddInParameter(dbCommand, "@PaymentDetail", DbType.Xml, iServiceFollowUp.PaymentDetail);
            db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, FK_BillType);
            db.AddInParameter(dbCommand, "@TotalSecurityAmount", DbType.Decimal, TotalSecurityAmount);
            //db.AddInParameter(dbCommand, "@FK_NextActionLead", DbType.Int64, FK_NextActionLead);
            //db.AddInParameter(dbCommand, "@FK_ActionTypeLead", DbType.Int64, FK_ActionTypeLead);
            //db.AddInParameter(dbCommand, "@FK_EmployeeLead", DbType.Int64, FK_EmployeeLead);
            //db.AddInParameter(dbCommand, "@NextActionDateLead", DbType.String, iServiceFollowUp.NextActionDateLead);

            //db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            //db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, iServiceFollowUp.LocLatitude);
            //db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, iServiceFollowUp.LocLongitude);
            //db.AddInParameter(dbCommand, "@LocationAddress", DbType.String, iServiceFollowUp.Address);
            //db.AddInParameter(dbCommand, "@LocationEnteredDate", DbType.String, iServiceFollowUp.LocationEnteredDate);
            //db.AddInParameter(dbCommand, "@LocationEnteredTime", DbType.String, iServiceFollowUp.LocationEnteredTime);
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
        public DataTable DalGetSubproductDetails(IServiceFollowUp iServiceFollowUp)
        {


            Int64 FK_Employee = 0, FK_Company = 0, FK_BranchCodeUser = 0, FK_Customer = 0, FK_Customerserviceregister = 0, FK_BillType = 0, FK_NextActionLead = 0, FK_ActionTypeLead = 0, FK_EmployeeLead = 0;
            Int32 FK_ActionType = 0, FK_NextAction = 0;
            if (iServiceFollowUp.FK_Company != "")
                FK_Company = Convert.ToInt32(iServiceFollowUp.FK_Company);
            if (iServiceFollowUp.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(iServiceFollowUp.FK_BranchCodeUser);
            if (iServiceFollowUp.FK_Customer != "")
                FK_Customer = Convert.ToInt32(iServiceFollowUp.FK_Customer);
            if (iServiceFollowUp.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(iServiceFollowUp.FK_Customerserviceregister);
            if (iServiceFollowUp.FK_Employee != "")
                FK_Employee = Convert.ToInt64(iServiceFollowUp.FK_Employee);
            if (iServiceFollowUp.FK_ActionType != "")
                FK_ActionType = Convert.ToInt32(iServiceFollowUp.FK_ActionType);
            if (iServiceFollowUp.FK_NextAction != "")
                FK_NextAction = Convert.ToInt32(iServiceFollowUp.FK_NextAction);
            if (iServiceFollowUp.FK_BillType != "")
                FK_BillType = Convert.ToInt64(iServiceFollowUp.FK_BillType);
            if (iServiceFollowUp.FK_NextActionLead != "")
                FK_NextActionLead = Convert.ToInt64(iServiceFollowUp.FK_NextActionLead);
            if (iServiceFollowUp.FK_ActionTypeLead != "")
                FK_ActionTypeLead = Convert.ToInt64(iServiceFollowUp.FK_ActionTypeLead);
            if (iServiceFollowUp.FK_EmployeeLead != "")
                FK_EmployeeLead = Convert.ToInt64(iServiceFollowUp.FK_EmployeeLead);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + iServiceFollowUp.BankKey);
            string sqlCommand = "ProProductSubDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, iServiceFollowUp.FK_Employee);
            db.AddInParameter(dbCommand, "@ProductSubDetails", DbType.Xml, iServiceFollowUp.SubProductDetails);
            //db.AddInParameter(dbCommand, "@Subproductreplacedetails", DbType.Xml, iServiceFollowUp.ProductDetails);
    
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
        //public DataTable DalProServiceFollowUpUpdate(IServiceFollowUp iServiceFollowUp)
        //{


        //    Int64 FK_Company = 1, FK_Customerserviceregister = 0, FK_Employee = 0, FK_BranchCodeUser = 0,
        //        FK_Customer = 0, FK_BillType = 0, FK_NextActionLead = 0, FK_ActionTypeLead = 0, FK_EmployeeLead = 0;
        //    Int32 FK_ActionType = 0, FK_NextAction = 0;
        //    decimal ServiceAmount = 0, ProductAmount = 0, NetAmount = 0, TotalAmount = 0, ReplaceAmount = 0, DiscountAmount = 0;
        //    if (iServiceFollowUp.FK_Customerserviceregister != "")
        //        FK_Customerserviceregister = Convert.ToInt16(iServiceFollowUp.FK_Customerserviceregister);
        //    if (iServiceFollowUp.FK_Employee != "")
        //        FK_Employee = Convert.ToInt64(iServiceFollowUp.FK_Employee);
        //    if (iServiceFollowUp.FK_Customer != "")
        //        FK_Customer = Convert.ToInt64(iServiceFollowUp.FK_Customer);
        //    if (iServiceFollowUp.FK_BranchCodeUser != "")
        //        FK_BranchCodeUser = Convert.ToInt64(iServiceFollowUp.FK_BranchCodeUser);
        //    if (iServiceFollowUp.FK_BillType != "")
        //        FK_BillType = Convert.ToInt64(iServiceFollowUp.FK_BillType);
        //    if (iServiceFollowUp.FK_NextActionLead != "")
        //        FK_NextActionLead = Convert.ToInt64(iServiceFollowUp.FK_NextActionLead);
        //    if (iServiceFollowUp.FK_ActionTypeLead != "")
        //        FK_ActionTypeLead = Convert.ToInt64(iServiceFollowUp.FK_ActionTypeLead);
        //    if (iServiceFollowUp.FK_EmployeeLead != "")
        //        FK_EmployeeLead = Convert.ToInt64(iServiceFollowUp.FK_EmployeeLead);
        //    if (iServiceFollowUp.FK_ActionType != "")
        //        FK_ActionType = Convert.ToInt32(iServiceFollowUp.FK_ActionType);
        //    if (iServiceFollowUp.FK_NextAction != "")
        //        FK_NextAction = Convert.ToInt32(iServiceFollowUp.FK_NextAction);
        //    if (iServiceFollowUp.ServiceAmount != "")
        //        ServiceAmount = Convert.ToDecimal(iServiceFollowUp.ServiceAmount);
        //    if (iServiceFollowUp.ProductAmount != "")
        //        ProductAmount = Convert.ToDecimal(iServiceFollowUp.ProductAmount);
        //    if (iServiceFollowUp.NetAmount != "")
        //        NetAmount = Convert.ToDecimal(iServiceFollowUp.NetAmount);
        //    if (iServiceFollowUp.TotalAmount != "")
        //        TotalAmount = Convert.ToDecimal(iServiceFollowUp.TotalAmount);
        //    if (iServiceFollowUp.ReplaceAmount != "")
        //        ReplaceAmount = Convert.ToDecimal(iServiceFollowUp.ReplaceAmount);
        //    if (iServiceFollowUp.DiscountAmount != "")
        //        DiscountAmount = Convert.ToDecimal(iServiceFollowUp.DiscountAmount);



        //    DataTable dtbl = new DataTable();
        //    Database db = DatabaseFactory.CreateDatabase("PerfectERP" + iServiceFollowUp.BankKey);
        //    string sqlCommand = "ProLeadGenerateUpdate";
        //    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //    db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, 1);
        //    db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
        //    db.AddInParameter(dbCommand, "@ID_ServiceFollowUp", DbType.Int64, 0);
        //    db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
        //    db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
        //    db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, FK_Customer);
        //    db.AddInParameter(dbCommand, "@CustomerNotes", DbType.String, iServiceFollowUp.CustomerNotes);
        //    db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, iServiceFollowUp.EmployeeNote);
        //    db.AddInParameter(dbCommand, "@StartingDate", DbType.String, iServiceFollowUp.StartingDate);
        //    db.AddInParameter(dbCommand, "@ServiceAmount", DbType.Decimal, ServiceAmount);
        //    db.AddInParameter(dbCommand, "@ProductAmount", DbType.Decimal, ProductAmount);
        //    db.AddInParameter(dbCommand, "@NetAmount", DbType.Decimal, NetAmount);
        //    db.AddInParameter(dbCommand, "@TotalAmount", DbType.Decimal, TotalAmount);
        //    db.AddInParameter(dbCommand, "@ReplaceAmoun", DbType.Decimal, ReplaceAmount);
        //    db.AddInParameter(dbCommand, "@DiscountAmount", DbType.Decimal, DiscountAmount);
        //    db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
        //    db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
        //    db.AddInParameter(dbCommand, "@EntrBy", DbType.String, iServiceFollowUp.EntrBy);
        //    db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
        //    db.AddInParameter(dbCommand, "@TransMode", DbType.String, "CUSF");
        //    db.AddInParameter(dbCommand, "@FK_ActionType", DbType.Int32, FK_ActionType);
        //    db.AddInParameter(dbCommand, "@FK_NextAction", DbType.Int32, FK_NextAction);
        //    db.AddInParameter(dbCommand, "@Debug", DbType.Int32, 0);
        //    db.AddInParameter(dbCommand, "@ServiceDetails", DbType.Xml, iServiceFollowUp.ServiceDetails);
        //    db.AddInParameter(dbCommand, "@ProductDetails", DbType.Xml, iServiceFollowUp.PrdctDetails);
        //    db.AddInParameter(dbCommand, "@AttendedEmployeeDetails", DbType.Xml, iServiceFollowUp.AttendedEmployeeDetails);
        //    db.AddInParameter(dbCommand, "@PaymentDetail", DbType.Xml, iServiceFollowUp.PaymentDetail);
        //    db.AddInParameter(dbCommand, "@FK_BillType", DbType.Int64, FK_BillType);
        //    db.AddInParameter(dbCommand, "@FK_NextActionLead", DbType.Int64, FK_NextActionLead);
        //    db.AddInParameter(dbCommand, "@FK_ActionTypeLead", DbType.Int64, FK_ActionTypeLead);
        //    db.AddInParameter(dbCommand, "@FK_EmployeeLead", DbType.Int64, FK_EmployeeLead);
        //    db.AddInParameter(dbCommand, "@NextActionDateLead", DbType.String, iServiceFollowUp.NextActionDateLead);

        //    try
        //    {
        //        dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
        //        return dtbl;
        //    }
        //    catch (SqlException e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
