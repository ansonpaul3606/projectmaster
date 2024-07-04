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
   public  class DalUserValidations
    {
        public DataSet DalSearchCommonValidates(IUserValidations IUserValidation)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IUserValidation.TransMode != "")
                TransMode = IUserValidation.TransMode;
            if (IUserValidation.ReqMode != "")
                Pagemode = Convert.ToInt32(IUserValidation.ReqMode);
            if (IUserValidation.PageIndex != "")
                PageIndex = Convert.ToInt32(IUserValidation.PageIndex);
            if (IUserValidation.PageSize != "")
                PageSize = Convert.ToInt32(IUserValidation.PageSize);
            if (IUserValidation.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidation.FK_Company);
            if (IUserValidation.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IUserValidation.Critrea1);
            if (IUserValidation.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IUserValidation.Critrea2);
            if (IUserValidation.Critrea3 != "")
                Critrea3 = IUserValidation.Critrea3;
            if (IUserValidation.Critrea4 != "")
                Critrea4 = IUserValidation.Critrea4;
            if (IUserValidation.ID != "")
                ID = Convert.ToInt32(IUserValidation.ID);
            if (IUserValidation.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IUserValidation.Critrea5);
            if (IUserValidation.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IUserValidation.Critrea6);

           // DataTable dtbl = new DataTable();
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidation.BankKey);
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
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidation.Name);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, ID);
            db.AddInParameter(dbCommand, "@Criteria5", DbType.Int64, Criteria5);
            db.AddInParameter(dbCommand, "@Criteria6", DbType.Int64, Criteria6);


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
        public DataTable DalSearchCommonValidate(IUserValidations IUserValidation)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0,  FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;
       

            if (IUserValidation.TransMode != "")
                TransMode = IUserValidation.TransMode;
            if (IUserValidation.ReqMode != "")
                Pagemode = Convert.ToInt32(IUserValidation.ReqMode);
            if (IUserValidation.PageIndex != "")
                PageIndex = Convert.ToInt32(IUserValidation.PageIndex);
            if (IUserValidation.PageSize != "")
                PageSize = Convert.ToInt32(IUserValidation.PageSize);
            if (IUserValidation.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidation.FK_Company);
            if (IUserValidation.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IUserValidation.Critrea1);
            if (IUserValidation.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IUserValidation.Critrea2);
            if (IUserValidation.Critrea3 != "")
                Critrea3 = IUserValidation.Critrea3;
            if (IUserValidation.Critrea4 != "")
                Critrea4 = IUserValidation.Critrea4;
            if (IUserValidation.ID != "")
                ID = Convert.ToInt32(IUserValidation.ID);
            if (IUserValidation.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IUserValidation.Critrea5);
            if (IUserValidation.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IUserValidation.Critrea6);
            if (Pagemode == 33)
            {
                Critrea1 = Convert.ToInt64(IUserValidation.ID_LeadFrom);
                //Critrea2 = Convert.ToInt64(IUserValidation.);
                Critrea4 = IUserValidation.EntrBy;
            }
            if (Pagemode == 36)
            {
                Critrea1 = Convert.ToInt64(IUserValidation.ID_Master);
                //Critrea2 = Convert.ToInt64(IUserValidation.);
                
            }

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidation.BankKey);
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
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidation.Name);
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
        public DataTable DalCommonValidate(IUserValidations IUserValidations)
        {

            Int32 ReqMode =0,SubMode=0, IsOnline=0;
            Int64 FK_Employee = 0, ID_LeadFrom=0, FK_Category=0, ID_Department=0, ID_BranchType=0, ID_ReportSettings=0, ID_LeadGenerateProduct=0,
                FK_Country=0, FK_District=0, FK_States=0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType=0, ID_Branch=0, ID_NotificationDetails=0, ID_LeadDocumentDetails=0, FK_MediaMaster = 0, FK_Company=0, FK_User=0,FK_BranchCodeUser=0, FK_Product=0,
                FK_Priority=0, FollowUpAction=0, FollowUpType=0, Criteria=0, Status=0, FK_CollectedBy=0, Category=0, BranchCode=0,FK_Customer=0, FK_CustomerOthers = 0, FK_Customerserviceregister=0
                ,ID_CustomerWiseProductDetails =0, FK_Designation=0;
            Int16 ID_TodoListLeadDetails = 0;


            Int16 ReportMode = 0, GroupId=0;
            bool PrductOnly = false;
            if (IUserValidations.ReqMode != "")
                ReqMode = Convert.ToInt32(IUserValidations.ReqMode);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt32(IUserValidations.SubMode);
            if (IUserValidations.ID_CustomerWiseProductDetails != "")
                ID_CustomerWiseProductDetails = Convert.ToInt64(IUserValidations.ID_CustomerWiseProductDetails);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(IUserValidations.ID_LeadFrom);
            if (IUserValidations.ID_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.ID_Category);
            if (IUserValidations.ID_Department != "")
                ID_Department = Convert.ToInt64(IUserValidations.ID_Department);
            if (IUserValidations.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(IUserValidations.ID_BranchType);
            if (IUserValidations.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(IUserValidations.ID_ReportSettings);
            if (IUserValidations.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IUserValidations.ID_LeadGenerateProduct);
            if (IUserValidations.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(IUserValidations.PrductOnly);
            if (IUserValidations.FK_Country != "")
                FK_Country = Convert.ToInt64(IUserValidations.FK_Country);
            if (IUserValidations.FK_District != "")
                FK_District = Convert.ToInt64(IUserValidations.FK_District);
            if (IUserValidations.FK_States != "")
                FK_States = Convert.ToInt64(IUserValidations.FK_States);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(IUserValidations.ID_ActionType);
            if (IUserValidations.IsOnline != "")
                IsOnline = Convert.ToInt32(IUserValidations.IsOnline);
            if (IUserValidations.ReportMode != "")
                ReportMode = Convert.ToInt16(IUserValidations.ReportMode);
            if (IUserValidations.ID_Branch != "")
                ID_Branch = Convert.ToInt64(IUserValidations.ID_Branch);
            if (IUserValidations.GroupId != "")
                GroupId = Convert.ToInt16(IUserValidations.GroupId);
            if (IUserValidations.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(IUserValidations.ID_NotificationDetails); 
            if (IUserValidations.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(IUserValidations.ID_LeadDocumentDetails);
            if(IUserValidations.FK_MediaMaster!="")
                FK_MediaMaster= Convert.ToInt32(IUserValidations.FK_MediaMaster);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt32(IUserValidations.FK_User);
            if(IUserValidations.FK_BranchCodeUser!="")
                FK_BranchCodeUser = Convert.ToInt32(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Product);
            if (IUserValidations.FK_Priority != "")
                FK_Priority = Convert.ToInt32(IUserValidations.FK_Priority);
            if (IUserValidations.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(IUserValidations.FollowUpAction);
            if (IUserValidations.FollowUpType != "")
                FollowUpType = Convert.ToInt32(IUserValidations.FollowUpType);
            if (IUserValidations.Criteria != "")
                Criteria = Convert.ToInt32(IUserValidations.Criteria);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt32(IUserValidations.Status);
            if (IUserValidations.FK_CollectedBy != "")
                FK_CollectedBy = Convert.ToInt32(IUserValidations.FK_CollectedBy);
            if (IUserValidations.Category != "")
                Category = Convert.ToInt32(IUserValidations.Category);
            if (IUserValidations.BranchCode != "")
                BranchCode = Convert.ToInt64(IUserValidations.BranchCode);
            if (IUserValidations.ID_TodoListLeadDetails != "")
                ID_TodoListLeadDetails = Convert.ToInt16(IUserValidations.ID_TodoListLeadDetails);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt32(IUserValidations.FK_Customer);
            if (IUserValidations.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt32(IUserValidations.FK_CustomerOthers);
            if (IUserValidations.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(IUserValidations.FK_Customerserviceregister);

            if (IUserValidations.FK_Designation != "")
                FK_Designation = Convert.ToInt32(IUserValidations.FK_Designation);
            if (ReqMode == 130)
            {
                Criteria = Convert.ToInt64(IUserValidations.FK_Master);
                //Critrea2 = Convert.ToInt64(IUserValidation.);

            }
            //if (IUserValidations.FromDate!=null&& IUserValidations.FromDate!="")
            //{
            //    IUserValidations.FromDate = Convert.ToDateTime(IUserValidations.FromDate).ToString("yyyy-MM-dd");

            //}
            //if (IUserValidations.ToDate != null && IUserValidations.ToDate != "")
            //{
            //    IUserValidations.ToDate = Convert.ToDateTime(IUserValidations.ToDate).ToString("yyyy-MM-dd");

            //}
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, IUserValidations.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, IUserValidations.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, IUserValidations.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, IUserValidations.OldMPIN);
            db.AddInParameter(dbCommand, "@Token", DbType.String, IUserValidations.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, IUserValidations.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, IUserValidations.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);           
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IUserValidations.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IUserValidations.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, IUserValidations.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);            
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, IUserValidations.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, IUserValidations.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, IUserValidations.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, IUserValidations.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, IUserValidations.LeadNo);
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
            db.AddInParameter(dbCommand, "@CompanyCode", DbType.String, IUserValidations.ConfigCode);

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
        public string GetSingleValue(string qry,IUserValidations user)
        {
            try
            {
                string str = "";
                Database db = DatabaseFactory.CreateDatabase("PerfectERP" + user.BankKey);
                DbCommand dbCommand = db.GetSqlStringCommand(qry);
                str = db.ExecuteScalar(dbCommand).ToString();
                return str;
            }
            catch (Exception e)
            {
                // clsFunctions.WriteToLog("Msg---" + e.Message + "---Stack Trace--" + e.StackTrace.ToString());
                throw;
            }
        }
        public DataTable DalLeadGenerateUpdate(IUserValidations IUserValidations)
        {
            try
            {

                Int64 FK_Company = 1, FK_LeadBy = 0, FK_MediaMaster = 0, LgCollectedBy = 0, FK_BranchCodeUser = 0, FK_Post = 0, FK_LeadFrom = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, ID_LeadGenerate = 0,  FK_Customer=0, FK_CustomerOthers=0, FK_SubMedia =0, FK_Area=0, LastID=0, FK_CustomerAssignment=0, FK_AuthorizationData=0;
            Int16 UserAction = 1;
            if (IUserValidations.UserAction != "")
                UserAction = Convert.ToInt16(IUserValidations.UserAction);
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt64(IUserValidations.FK_Customer);
            if (IUserValidations.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt64(IUserValidations.FK_CustomerOthers);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt64(IUserValidations.FK_Customer);
            if (IUserValidations.FK_Country != "")
                FK_Country = Convert.ToInt64(IUserValidations.FK_Country);
            if (IUserValidations.FK_States != "")
                FK_States = Convert.ToInt64(IUserValidations.FK_States);
            if (IUserValidations.FK_District != "")
                FK_District = Convert.ToInt64(IUserValidations.FK_District);
            if (IUserValidations.FK_Post != "")
                FK_Post = Convert.ToInt64(IUserValidations.FK_Post);
            if (IUserValidations.FK_LeadFrom != "")
                FK_LeadFrom = Convert.ToInt64(IUserValidations.FK_LeadFrom);
            if (IUserValidations.FK_LeadBy != "")
                FK_LeadBy = Convert.ToInt64(IUserValidations.FK_LeadBy);
            if (IUserValidations.FK_MediaMaster != "")
                FK_MediaMaster = Convert.ToInt64(IUserValidations.FK_MediaMaster);
            if (IUserValidations.LgCollectedBy != "")
                LgCollectedBy = Convert.ToInt64(IUserValidations.LgCollectedBy);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);             
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);

            if (IUserValidations.FK_SubMedia != "")
                FK_SubMedia = Convert.ToInt64(IUserValidations.FK_SubMedia);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.LastID != "")
                LastID = Convert.ToInt64(IUserValidations.LastID);
            if (IUserValidations.ID_CustomerAssignment != "")
                FK_CustomerAssignment = Convert.ToInt64(IUserValidations.ID_CustomerAssignment);
            if (IUserValidations.FK_AuthorizationData != "" && IUserValidations.FK_AuthorizationData!=null)
                FK_AuthorizationData = Convert.ToInt64(IUserValidations.FK_AuthorizationData);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProLeadGenerateUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@LgLeadDate", DbType.DateTime, IUserValidations.LgLeadDate);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_SubMedia", DbType.Int64, FK_SubMedia);
            db.AddInParameter(dbCommand, "@FK_CustomerOthers", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@LgCusNameTitle", DbType.String, IUserValidations.LgCusNameTitle);
            db.AddInParameter(dbCommand, "@LgCusName", DbType.String, IUserValidations.LgCusName);
            db.AddInParameter(dbCommand, "@LgCusAddress", DbType.String, IUserValidations.LgCusAddress);
            db.AddInParameter(dbCommand, "@LgCusAddress2", DbType.String, IUserValidations.LgCusAddress2);
            db.AddInParameter(dbCommand, "@LgCusMobile", DbType.String, IUserValidations.LgCusMobile);
            db.AddInParameter(dbCommand, "@LgCusEmail", DbType.String, IUserValidations.LgCusEmail);
            db.AddInParameter(dbCommand, "@CusCompany", DbType.String, IUserValidations.CusCompany);
            db.AddInParameter(dbCommand, "@CusPhone", DbType.String, IUserValidations.CusPhone);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_State", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Post", DbType.Int64, FK_Post);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@LastID", DbType.Int64, LastID);
            db.AddInParameter(dbCommand, "@FK_LeadFrom", DbType.Int64, FK_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_LeadBy", DbType.Int64, FK_LeadBy);
            db.AddInParameter(dbCommand, "@LeadByName", DbType.String, IUserValidations.FK_LeadByName);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@LgCollectedBy", DbType.Int64, LgCollectedBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            //db.AddInParameter(dbCommand, "@PreviousID", DbType.Int64, IUserValidations.PreviousID);
            db.AddInParameter(dbCommand, "@CusMobileAlternate", DbType.String, IUserValidations.CusMobileAlternate);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocationLandMark1", DbType.Binary, IUserValidations.LocationLandMark1);          
            db.AddInParameter(dbCommand, "@LocationLandMark2", DbType.Binary, IUserValidations.LocationLandMark2);
            db.AddInParameter(dbCommand, "@SubProductDetails", DbType.Xml, IUserValidations.SubProductDetails);           
            db.AddInParameter(dbCommand, "@LgLeadChannel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@FK_CustomerAssignment", DbType.Int64, FK_CustomerAssignment);
            db.AddInParameter(dbCommand, "@FK_AuthorizationData", DbType.Int64, FK_AuthorizationData);
            db.AddOutParameter(dbCommand, "@FK_LeadGenerate", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@CusMobile1", DbType.String, IUserValidations.LandNumber);

            dtbl = db.ExecuteDataSet(dbCommand).Tables[0];
            return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalDeleteLeadGenerate(IUserValidations IUserValidations)
        {

            Int64 FK_Company = 0,  FK_BranchCodeUser = 0,  ID_LeadGenerate = 0,FK_Reason=0;            
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Reason != "")
                FK_Reason = Convert.ToInt64(IUserValidations.FK_Reason);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProLeadGenerateDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);        
          
        
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, FK_Reason);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            
          
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
        public DataTable DalProLeadGenerateActionUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, ID_LeadGenerate = 0, LgActMode=0, FK_Action=0, FK_Departement=0,
                FK_ToEmployee=0, ID_LeadGenerateProduct = 0, ID_FollowUpBy = 0, FK_Priority=0, FK_ActionType=0, LgFollowUpStatus=0,ForAllProduct=0;
            Int16 ActStatus = 0;
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.LgActMode != "")
                LgActMode = Convert.ToInt64(IUserValidations.LgActMode);
            if (IUserValidations.ActStatus != "")
                ActStatus = Convert.ToInt16(IUserValidations.ActStatus);
            if (IUserValidations.FK_Action != "")
                FK_Action = Convert.ToInt64(IUserValidations.FK_Action);
            if (IUserValidations.FK_Departement != "")
                FK_Departement = Convert.ToInt64(IUserValidations.FK_Departement);
            if (IUserValidations.FK_ToEmployee != "")
                FK_ToEmployee = Convert.ToInt64(IUserValidations.FK_ToEmployee);
            if (IUserValidations.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IUserValidations.ID_LeadGenerateProduct);
            if (IUserValidations.ID_FollowUpBy != "")
                ID_FollowUpBy = Convert.ToInt64(IUserValidations.ID_FollowUpBy);
            if (IUserValidations.FK_Priority != "")
                FK_Priority = Convert.ToInt64(IUserValidations.FK_Priority);
            if (IUserValidations.FK_ActionType != "")
                FK_ActionType = Convert.ToInt64(IUserValidations.FK_ActionType);
            if (IUserValidations.LgFollowUpStatus != "")
                LgFollowUpStatus = Convert.ToInt32(IUserValidations.LgFollowUpStatus);
            if (IUserValidations.ForAllProduct != "")
                ForAllProduct = Convert.ToInt32(IUserValidations.ForAllProduct);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProLeadGenerateActionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, IUserValidations.UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);   
            db.AddInParameter(dbCommand, "@ID_LeadGenerateAction", DbType.Int64,0);
            db.AddInParameter(dbCommand, "@LgActMode", DbType.Int64, LgActMode);
            db.AddInParameter(dbCommand, "@LgActStatus", DbType.Int16, ActStatus);
            db.AddInParameter(dbCommand, "@LgActDate", DbType.String, IUserValidations.TrnsDate);
            db.AddInParameter(dbCommand, "@LgActCusComment", DbType.String, IUserValidations.CustomerNote);
            db.AddInParameter(dbCommand, "@LgActLeadComment", DbType.String, IUserValidations.EmployeeNote);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@LgActNextAction", DbType.Int64, FK_Action);
            db.AddInParameter(dbCommand, "@LgActNextDate", DbType.String, IUserValidations.NextActionDate);
            db.AddInParameter(dbCommand, "@checkval", DbType.Int64, ForAllProduct);
            db.AddInParameter(dbCommand, "@FK_Departement", DbType.Int64, FK_Departement);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_ToEmployee);
            db.AddInParameter(dbCommand, "@FK_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@FK_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_LeadGenerateAction", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@SubProductDetails", DbType.String, "");
            db.AddInParameter(dbCommand, "@FK_FollowUpBy", DbType.Int64, ID_FollowUpBy);
            db.AddInParameter(dbCommand, "@FK_ActionType", DbType.Int64, FK_ActionType);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@LgChannel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@LgFollowUpTime", DbType.String, IUserValidations.LgFollowUpTime);
            db.AddInParameter(dbCommand, "@LgFollowUpStatus ", DbType.Int32, LgFollowUpStatus);
            db.AddInParameter(dbCommand, "@LgFollowupDuration", DbType.String, IUserValidations.LgFollowupDuration);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLatitude ", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocationLandMark1", DbType.Binary, IUserValidations.LocationLandMark1);
            db.AddInParameter(dbCommand, "@LocationLandMark2", DbType.Binary, IUserValidations.LocationLandMark2);

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
        public DataTable DalLeadDocumentDetailsUpdate(IUserValidations IUserValidations)
        {


            Int64 ID_LeadGenerateProduct = 0, FK_Company=0;
            
            if (IUserValidations.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IUserValidations.ID_LeadGenerateProduct);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPILeadDocumentDetailsUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);           
            db.AddInParameter(dbCommand, "@DocumentImage", DbType.Binary, IUserValidations.DocumentImage);
            db.AddInParameter(dbCommand, "@DocSubject", DbType.String, IUserValidations.Doc_Subject);
            db.AddInParameter(dbCommand, "@DocDescription", DbType.String, IUserValidations.Doc_Description);
            db.AddInParameter(dbCommand, "@DocImageFormat", DbType.String, IUserValidations.DocImageFormat);
            db.AddInParameter(dbCommand, "@EnterBy", DbType.String, IUserValidations.EntrBy);

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
       
        public DataTable DalCustomerServiceRegisterUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 0, ID_CustomerServiceRegister=0, FK_Customer=0, FK_CustomerOthers=0, CSRChannelID=0, CSRPriority=0, CSRCurrentStatus=0, FK_BranchCodeUser=0, FK_Branch=0, FK_Customerserviceregister=0,
                  CSRPCategory=0, FK_OtherCompany=0, FK_ComplaintList=0, FK_ServiceList=0, CSRChannelSubID=0, Status=0, AttendedBy=0, FK_Category = 0, FK_District = 0, FK_States = 0,
                  FK_Country = 0, FK_Area = 0, FK_Post = 0, FK_SubCategory=0, FK_Brand=0;

            dynamic CSRServiceToDate = DBNull.Value, CSRServiceFromDate = DBNull.Value;

            if (IUserValidations.FK_SubCategory != "")
                FK_SubCategory = Convert.ToInt64(IUserValidations.FK_SubCategory);
            if (IUserValidations.FK_Brand != "")
                FK_Brand = Convert.ToInt64(IUserValidations.FK_Brand);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.ID_CustomerServiceRegister != "")
                ID_CustomerServiceRegister = Convert.ToInt64(IUserValidations.ID_CustomerServiceRegister);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt64(IUserValidations.FK_Customer);
            if (IUserValidations.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt64(IUserValidations.FK_CustomerOthers);
            if (IUserValidations.CSRChannelID != "")
                CSRChannelID = Convert.ToInt64(IUserValidations.CSRChannelID);
            if (IUserValidations.CSRPriority != "")
                CSRPriority = Convert.ToInt64(IUserValidations.CSRPriority);
            if (IUserValidations.CSRCurrentStatus != "")
                CSRCurrentStatus = Convert.ToInt64(IUserValidations.CSRCurrentStatus);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.CSRPCategory != "")
                CSRPCategory = Convert.ToInt64(IUserValidations.CSRPCategory);
            if (IUserValidations.FK_OtherCompany != "")
                FK_OtherCompany = Convert.ToInt64(IUserValidations.FK_OtherCompany);
            if (IUserValidations.FK_ComplaintList != "")
                FK_ComplaintList = Convert.ToInt64(IUserValidations.FK_ComplaintList);
            if (IUserValidations.FK_ServiceList != "")
                FK_ServiceList = Convert.ToInt64(IUserValidations.FK_ServiceList);
            if (IUserValidations.CSRChannelSubID != "")
                CSRChannelSubID = Convert.ToInt64(IUserValidations.CSRChannelSubID);
            if (IUserValidations.Status != "")
                CSRChannelSubID = Convert.ToInt64(IUserValidations.Status);
            if (IUserValidations.AttendedBy != "")
                AttendedBy = Convert.ToInt64(IUserValidations.AttendedBy);

            if (IUserValidations.CSRServiceFromDate != "")
                CSRServiceFromDate = Convert.ToString(IUserValidations.CSRServiceFromDate);
            if (IUserValidations.CSRServiceToDate != "")
                CSRServiceToDate = Convert.ToString(IUserValidations.CSRServiceToDate);            
            if (IUserValidations.FK_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.FK_Category);
            if (IUserValidations.FK_District != "")
                FK_District = Convert.ToInt64(IUserValidations.FK_District);
            if (IUserValidations.FK_States != "")
                FK_States = Convert.ToInt64(IUserValidations.FK_States);
            if (IUserValidations.FK_Country != "")
                FK_Country = Convert.ToInt64(IUserValidations.FK_Country);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.FK_Post != "")
                FK_Post = Convert.ToInt64(IUserValidations.FK_Post);
          


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "proCustomerserviceregisterUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);           
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, IUserValidations.UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@ID_CustomerServiceRegister", DbType.Int64, ID_CustomerServiceRegister);
            db.AddInParameter(dbCommand, "@CSRTickno", DbType.String, IUserValidations.CSRTickno);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_CustomerOthers", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@CusName", DbType.String, IUserValidations.CusName);
            db.AddInParameter(dbCommand, "@CusMobile", DbType.String, IUserValidations.CusMobile);
            db.AddInParameter(dbCommand, "@CusAddress", DbType.String, IUserValidations.CusAddress);
            db.AddInParameter(dbCommand, "@CSRContactNo", DbType.String, IUserValidations.CSRContactNo);
            db.AddInParameter(dbCommand, "@CSRLandmark", DbType.String, IUserValidations.CSRLandmark);
            db.AddInParameter(dbCommand, "@CSRServiceFromDate", DbType.String, CSRServiceFromDate);
            db.AddInParameter(dbCommand, "@CSRServiceToDate", DbType.String, CSRServiceToDate);
            db.AddInParameter(dbCommand, "@CSRServicefromtime", DbType.String, IUserValidations.CSRServicefromtime);
            db.AddInParameter(dbCommand, "@CSRServicetotime", DbType.String, IUserValidations.CSRServicetotime);
            db.AddInParameter(dbCommand, "@CSRChannelID ", DbType.Int64, CSRChannelID);
            db.AddInParameter(dbCommand, "@CSRPriority", DbType.Int64, CSRPriority);
            db.AddInParameter(dbCommand, "@CSRCurrentStatus", DbType.Int64, CSRCurrentStatus);
            db.AddInParameter(dbCommand, "@CSRClosedDate", DbType.String, DBNull.Value);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@CSRPCategory", DbType.Int64, CSRPCategory);
            db.AddInParameter(dbCommand, "@FK_OtherCompany", DbType.Int64, FK_OtherCompany);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.String, IUserValidations.FK_Product);
            db.AddInParameter(dbCommand, "@FK_ComplaintList ", DbType.Int64, FK_ComplaintList);
            db.AddInParameter(dbCommand, "@FK_ServiceList", DbType.String, FK_ServiceList);
            db.AddInParameter(dbCommand, "@CSRODescription", DbType.String, IUserValidations.CSRODescription);
            db.AddInParameter(dbCommand, "@CheckList", DbType.String, "");
            db.AddInParameter(dbCommand, "@CSRChannelSubID ", DbType.Int64, CSRChannelSubID);
            db.AddInParameter(dbCommand, "@Status ", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@AttendedBy ", DbType.Int64, AttendedBy);
            db.AddInParameter(dbCommand, "@TicketDate", DbType.String, IUserValidations.TicketDate);
            db.AddInParameter(dbCommand, "@CSRChannel", DbType.Int16, 1);
            db.AddInParameter(dbCommand, "@TicketTime", DbType.String, IUserValidations.TicketTime);
            db.AddInParameter(dbCommand, "@FK_Category ", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_Post", DbType.Int64, FK_Post);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
            db.AddInParameter(dbCommand, "@Address1", DbType.String, IUserValidations.Address1);
            db.AddInParameter(dbCommand, "@Address2", DbType.String, IUserValidations.Address2);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, IUserValidations.CusMobile);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocationLandMark1", DbType.Binary, IUserValidations.LocationLandMark1);
            db.AddInParameter(dbCommand, "@LocationLandMark2", DbType.Binary, IUserValidations.LocationLandMark2);
            db.AddInParameter(dbCommand, "@LocationAddress", DbType.String, IUserValidations.Address);  
            db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@LocationEnteredDate", DbType.String, IUserValidations.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@LocationEnteredTime", DbType.String, IUserValidations.LocationEnteredTime);
            db.AddInParameter(dbCommand, "@FK_Brand", DbType.Int64, FK_Brand);
            db.AddInParameter(dbCommand, "@FK_SubCategory", DbType.Int64, FK_SubCategory);
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
        public DataTable DalServiceAssignList(IUserValidations IUserValidations)
        {
            Int64 FK_Branch = 0, FK_Product = 0, FK_ComplaintType=0, Status=5, FK_Company=0, SortOrder=0, FK_Post=0, FK_Area=0, FK_Employee=0, DueDays=0, SubMode=0;            
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt64(IUserValidations.FK_Product);
            if (IUserValidations.FK_ComplaintType != "")
                FK_ComplaintType = Convert.ToInt64(IUserValidations.FK_ComplaintType);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt64(IUserValidations.Status);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.SortOrder != "")
                SortOrder = Convert.ToInt64(IUserValidations.SortOrder);
            if (IUserValidations.FK_Post != "")
                FK_Post = Convert.ToInt64(IUserValidations.FK_Post);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.DueDays != "")
                DueDays = Convert.ToInt64(IUserValidations.DueDays);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt64(IUserValidations.SubMode);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProSelectTickets";           
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_ComplaintType", DbType.Int64, FK_ComplaintType);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IUserValidations.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IUserValidations.Todate);
            db.AddInParameter(dbCommand, "@TicketNumber", DbType.String, IUserValidations.CSRTickno);
            db.AddInParameter(dbCommand, "@Customer", DbType.String, IUserValidations.CusName);
            db.AddInParameter(dbCommand, "@Mobile", DbType.String, IUserValidations.CusMobile);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64,1);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@SortOrder", DbType.Int64, SortOrder);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int64, SubMode);
            db.AddInParameter(dbCommand, "@FK_Post", DbType.Int64, FK_Post);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_Employee ", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@DueDays", DbType.Int64, DueDays);
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
        public DataTable DalCustomerBalanceList(IUserValidations IUserValidations)
        {
            Int64 FK_Branch = 0, FK_Product = 0, FK_ComplaintType = 0, FK_Customer=0, Status = 5, FK_Company = 0, SortOrder = 0, FK_Post = 0, FK_Area = 0, FK_Employee = 0, DueDays = 0, SubMode = 0, LastID= 0;
            string TransDate = "";
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt64(IUserValidations.FK_Product);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt64(IUserValidations.FK_Customer);
            if (IUserValidations.FK_ComplaintType != "")
                FK_ComplaintType = Convert.ToInt64(IUserValidations.FK_ComplaintType);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt64(IUserValidations.Status);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.SortOrder != "")
                SortOrder = Convert.ToInt64(IUserValidations.SortOrder);
            if (IUserValidations.TransDate != "")
                TransDate = Convert.ToString(IUserValidations.TransDate);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations);
            //if (IUserValidations.DueDays != "")
            //    DueDays = Convert.ToInt64(IUserValidations.DueDays);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt64(IUserValidations.SubMode);
            if (IUserValidations.LastID != "")
                LastID = Convert.ToInt64(IUserValidations.LastID);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProGetCustomerAccountBalance";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Customer", DbType.Int64, IUserValidations.FK_Customer);
            db.AddInParameter(dbCommand, "@TransDate", DbType.String, IUserValidations.TransDate);

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
        public DataSet DalCommonValidatewithDataset(IUserValidations IUserValidations)
        {

            Int32 ReqMode = 0, SubMode = 0, IsOnline = 0;
            Int64 FK_Employee = 0, ID_LeadFrom = 0, FK_Category = 0, ID_Department = 0, ID_BranchType = 0, ID_ReportSettings = 0, ID_LeadGenerateProduct = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType = 0, ID_Branch = 0, ID_NotificationDetails = 0, ID_LeadDocumentDetails = 0, FK_MediaMaster = 0, FK_Company = 0, FK_User = 0, FK_BranchCodeUser = 0, FK_Product = 0,
                FK_Priority = 0, FollowUpAction = 0, FollowUpType = 0, Criteria = 0, Status = 0, FK_CollectedBy = 0, Category = 0, BranchCode = 0, FK_Customer = 0, FK_CustomerOthers = 0, FK_Customerserviceregister = 0,
                ID_CustomerWiseProductDetails = 0;
            Int16 ID_TodoListLeadDetails = 0;


            Int16 ReportMode = 0, GroupId = 0;
            bool PrductOnly = false;
            if (IUserValidations.ReqMode != "")
                ReqMode = Convert.ToInt32(IUserValidations.ReqMode);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt32(IUserValidations.SubMode);
            if (IUserValidations.ID_CustomerWiseProductDetails != "")
                ID_CustomerWiseProductDetails = Convert.ToInt64(IUserValidations.ID_CustomerWiseProductDetails);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(IUserValidations.ID_LeadFrom);
            if (IUserValidations.ID_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.ID_Category);
            if (IUserValidations.ID_Department != "")
                ID_Department = Convert.ToInt64(IUserValidations.ID_Department);
            if (IUserValidations.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(IUserValidations.ID_BranchType);
            if (IUserValidations.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(IUserValidations.ID_ReportSettings);
            if (IUserValidations.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IUserValidations.ID_LeadGenerateProduct);
            if (IUserValidations.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(IUserValidations.PrductOnly);
            if (IUserValidations.FK_Country != "")
                FK_Country = Convert.ToInt64(IUserValidations.FK_Country);
            if (IUserValidations.FK_District != "")
                FK_District = Convert.ToInt64(IUserValidations.FK_District);
            if (IUserValidations.FK_States != "")
                FK_States = Convert.ToInt64(IUserValidations.FK_States);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(IUserValidations.ID_ActionType);
            if (IUserValidations.IsOnline != "")
                IsOnline = Convert.ToInt32(IUserValidations.IsOnline);
            if (IUserValidations.ReportMode != "")
                ReportMode = Convert.ToInt16(IUserValidations.ReportMode);
            if (IUserValidations.ID_Branch != "")
                ID_Branch = Convert.ToInt64(IUserValidations.ID_Branch);
            if (IUserValidations.GroupId != "")
                GroupId = Convert.ToInt16(IUserValidations.GroupId);
            if (IUserValidations.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(IUserValidations.ID_NotificationDetails);
            if (IUserValidations.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(IUserValidations.ID_LeadDocumentDetails);
            if (IUserValidations.FK_MediaMaster != "")
                FK_MediaMaster = Convert.ToInt32(IUserValidations.FK_MediaMaster);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt32(IUserValidations.FK_User);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Product);
            if (IUserValidations.FK_Priority != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Priority);
            if (IUserValidations.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(IUserValidations.FollowUpAction);
            if (IUserValidations.FollowUpType != "")
                FollowUpType = Convert.ToInt32(IUserValidations.FollowUpType);
            if (IUserValidations.Criteria != "")
                Criteria = Convert.ToInt32(IUserValidations.Criteria);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt32(IUserValidations.Status);
            if (IUserValidations.FK_CollectedBy != "")
                FK_CollectedBy = Convert.ToInt32(IUserValidations.FK_CollectedBy);
            if (IUserValidations.Category != "")
                Category = Convert.ToInt32(IUserValidations.Category);
            if (IUserValidations.BranchCode != "")
                BranchCode = Convert.ToInt64(IUserValidations.BranchCode);
            if (IUserValidations.ID_TodoListLeadDetails != "")
                ID_TodoListLeadDetails = Convert.ToInt16(IUserValidations.ID_TodoListLeadDetails);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt32(IUserValidations.FK_Customer);
            if (IUserValidations.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt32(IUserValidations.FK_CustomerOthers);
            if (IUserValidations.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(IUserValidations.FK_Customerserviceregister);

            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, IUserValidations.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, IUserValidations.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, IUserValidations.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, IUserValidations.OldMPIN);
            db.AddInParameter(dbCommand, "@Token", DbType.String, IUserValidations.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, IUserValidations.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, IUserValidations.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IUserValidations.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IUserValidations.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, IUserValidations.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, IUserValidations.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, IUserValidations.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, IUserValidations.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, IUserValidations.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, IUserValidations.LeadNo);
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
        public DataSet DalServiceAssignDetails(IUserValidations IUserValidations)
        {

            Int32 ReqMode = 0, SubMode = 0;
            Int64 FK_Company = 0,  FK_BranchCodeUser = 0,FK_Customerserviceregister = 0, FK_CustomerserviceregisterProductDetails = 0;
            if (IUserValidations.ReqMode != "")
                ReqMode = Convert.ToInt32(IUserValidations.ReqMode);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt32(IUserValidations.SubMode); 
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);           
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(IUserValidations.FK_BranchCodeUser);           
            if (IUserValidations.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(IUserValidations.FK_Customerserviceregister);
            if (IUserValidations.FK_CustomerserviceregisterProductDetails != "")
                FK_CustomerserviceregisterProductDetails = Convert.ToInt32(IUserValidations.FK_CustomerserviceregisterProductDetails);
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@Token", DbType.String, IUserValidations.Token);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);      
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@FK_CustomerserviceregisterProductDetails", DbType.Int64, FK_CustomerserviceregisterProductDetails);
          

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
        public DataTable DalCustomerserviceassignUpdate(IUserValidations IUserValidations,Int16 UserAction)
        {
            Int64 FK_Branch = 0,FK_CustomerserviceregisterProductDetails=0, FK_Customerserviceregister = 0, FK_BranchCodeUser = 0, FK_Company = 0,FK_Employee = 0, Status=0, FK_AttendedBy=0;
            Int32 FK_Priority = 0;
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt64(IUserValidations.FK_Customerserviceregister);
            if (IUserValidations.FK_Priority != "")
                FK_Priority = Convert.ToInt32(IUserValidations.FK_Priority);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);           
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt64(IUserValidations.Status);
            if (IUserValidations.FK_AttendedBy != "")
                FK_AttendedBy = Convert.ToInt64(IUserValidations.FK_AttendedBy);
            if (IUserValidations.FK_CustomerserviceregisterProductDetails != "")
                FK_CustomerserviceregisterProductDetails = Convert.ToInt64(IUserValidations.FK_CustomerserviceregisterProductDetails);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProCustomerserviceassignUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Int16, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, "APP");
            db.AddInParameter(dbCommand, "@ID_CustomerServiceAssign", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@CSAVisitdate", DbType.String, IUserValidations.Visitdate);
            db.AddInParameter(dbCommand, "@CSAVisittime", DbType.String, IUserValidations.Visittime);
            db.AddInParameter(dbCommand, "@CSAPriority", DbType.Int32, FK_Priority);
            db.AddInParameter(dbCommand, "@CSARemarks", DbType.String, IUserValidations.Remark);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_CustomerServiceAssign", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@Assignees", DbType.Xml, IUserValidations.Assignees);
            db.AddInParameter(dbCommand, "@ID_CustomerServiceRegisterProductDetails", DbType.Int64,FK_CustomerserviceregisterProductDetails);
            //db.AddInParameter(dbCommand, "@LastID", DbType.String, IUserValidations.Last);
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
        public DataTable DalWarrantyAMCDetailsList(IUserValidations IUserValidations)
        {
            Int64 FK_Branch = 0, FK_Product = 0, FK_ComplaintType = 0, FK_Customer = 0, Status = 5, FK_Company = 0, SortOrder = 0, FK_Post = 0, FK_Area = 0, FK_Employee = 0, DueDays = 0, SubMode = 0, Critrea1 = 0, Critrea3 = 0;
            string EntrBy = "", Critrea2;
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt64(IUserValidations.FK_Product);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt64(IUserValidations.FK_Customer);
            if (IUserValidations.FK_ComplaintType != "")
                FK_ComplaintType = Convert.ToInt64(IUserValidations.FK_ComplaintType);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt64(IUserValidations.Status);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.SortOrder != "")
                SortOrder = Convert.ToInt64(IUserValidations.SortOrder);
            if (IUserValidations.SortOrder != "")
                SortOrder = Convert.ToInt64(IUserValidations.SortOrder);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations);
            if (IUserValidations.EntrBy != "")
                EntrBy = Convert.ToString(IUserValidations.EntrBy);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt64(IUserValidations.SubMode);
            if (IUserValidations.Critrea1 != "")
                Critrea1 = Convert.ToInt64(IUserValidations.Critrea1);

            if (IUserValidations.Critrea3 != "")
                Critrea3 = Convert.ToInt64(IUserValidations.Critrea3);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProProductWisePreviousComplaintHistory";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Product ", DbType.Int64, IUserValidations.FK_Product);
            db.AddInParameter(dbCommand, "@FK_Customer ", DbType.Int64, IUserValidations.FK_Customer);
            db.AddInParameter(dbCommand, "@FK_Category ", DbType.Int64, IUserValidations.FK_Category);
            db.AddInParameter(dbCommand, "@FK_CustomerOthers ", DbType.Int64, IUserValidations.FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@FK_Company ", DbType.Int64, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Branch ", DbType.Int64, IUserValidations.FK_Branch);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, IUserValidations.Critrea3);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, IUserValidations.Critrea2);
            db.AddInParameter(dbCommand, "@MasterID", DbType.Int64, IUserValidations.Critrea1);

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
        public DataTable DalCommonAppChecking(IUserValidations iuser)
        {
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP");
            string sqlCommand = "proCommonAPPChecking";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Bankkey", DbType.String, iuser.BankKey);
           
            try
            {
                dtbl = db.ExecuteDataSet(dbCommand);
                return dtbl.Tables[0];
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public Boolean GetLoginCount(string BankKey,string UserCode,Int32 UserCount,Int32 CountType)
        {
            bool Exists=false;
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
            string sqlCommand = "ProCheckUserORBranchExistence";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, UserCode);
            db.AddInParameter(dbCommand, "@UserCount", DbType.Int32, UserCount);
            db.AddInParameter(dbCommand, "@CountType", DbType.Int32, CountType);
            db.AddInParameter(dbCommand, "@Exists", DbType.Boolean, Exists);
            try
            {
                Exists = Convert.ToBoolean(db.ExecuteScalar(dbCommand));
                return Exists;
                // mobile = dbCommand.Parameters["@Mobile"].Value.ToString();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public Int32 GetBranchCount(string BankKey)
        {
            try
            {
                Int32 str = 0;
                Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
                DbCommand dbCommand = db.GetSqlStringCommand("select count(1) from Branch  where Cancelled=0");
                str = Convert.ToInt32(db.ExecuteScalar(dbCommand).ToString());
                return str;
            }
            catch (Exception e)
            {
                // clsFunctions.WriteToLog("Msg---" + e.Message + "---Stack Trace--" + e.StackTrace.ToString());
                throw;
            }
        }
        public DataSet DalCommonValidateWithDataset(IUserValidations IUserValidations)
        {

            Int32 ReqMode = 0, SubMode = 0, IsOnline = 0;
            Int64 FK_Employee = 0, ID_LeadFrom = 0, FK_Category = 0, ID_Department = 0, ID_BranchType = 0, ID_ReportSettings = 0, ID_LeadGenerateProduct = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType = 0, ID_Branch = 0, ID_NotificationDetails = 0, ID_LeadDocumentDetails = 0, FK_MediaMaster = 0, FK_Company = 0, FK_User = 0, FK_BranchCodeUser = 0, FK_Product = 0,
                FK_Priority = 0, FollowUpAction = 0, FK_CustomerserviceregisterProductDetails=0, FollowUpType = 0, Criteria = 0, Status = 0, FK_CollectedBy = 0, Category = 0, BranchCode = 0, FK_Customer = 0, FK_CustomerOthers = 0, FK_Customerserviceregister = 0;
            Int16 ID_TodoListLeadDetails = 0;


            Int16 ReportMode = 0, GroupId = 0;
            bool PrductOnly = false;
            if (IUserValidations.ReqMode != "")
                ReqMode = Convert.ToInt32(IUserValidations.ReqMode);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt32(IUserValidations.SubMode);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(IUserValidations.ID_LeadFrom);
            if (IUserValidations.ID_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.ID_Category);
            if (IUserValidations.ID_Department != "")
                ID_Department = Convert.ToInt64(IUserValidations.ID_Department);
            if (IUserValidations.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(IUserValidations.ID_BranchType);
            if (IUserValidations.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(IUserValidations.ID_ReportSettings);
            if (IUserValidations.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IUserValidations.ID_LeadGenerateProduct);
            if (IUserValidations.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(IUserValidations.PrductOnly);
            if (IUserValidations.FK_Country != "")
                FK_Country = Convert.ToInt64(IUserValidations.FK_Country);
            if (IUserValidations.FK_District != "")
                FK_District = Convert.ToInt64(IUserValidations.FK_District);
            if (IUserValidations.FK_States != "")
                FK_States = Convert.ToInt64(IUserValidations.FK_States);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(IUserValidations.ID_ActionType);
            if (IUserValidations.IsOnline != "")
                IsOnline = Convert.ToInt32(IUserValidations.IsOnline);
            if (IUserValidations.ReportMode != "")
                ReportMode = Convert.ToInt16(IUserValidations.ReportMode);
            if (IUserValidations.ID_Branch != "")
                ID_Branch = Convert.ToInt64(IUserValidations.ID_Branch);
            if (IUserValidations.GroupId != "")
                GroupId = Convert.ToInt16(IUserValidations.GroupId);
            if (IUserValidations.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(IUserValidations.ID_NotificationDetails);
            if (IUserValidations.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(IUserValidations.ID_LeadDocumentDetails);
            if (IUserValidations.FK_MediaMaster != "")
                FK_MediaMaster = Convert.ToInt32(IUserValidations.FK_MediaMaster);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt32(IUserValidations.FK_User);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Product);
            if (IUserValidations.FK_Priority != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Priority);
            if (IUserValidations.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(IUserValidations.FollowUpAction);
            if (IUserValidations.FollowUpType != "")
                FollowUpType = Convert.ToInt32(IUserValidations.FollowUpType);
            if (IUserValidations.Criteria != "")
                Criteria = Convert.ToInt32(IUserValidations.Criteria);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt32(IUserValidations.Status);
            if (IUserValidations.FK_CollectedBy != "")
                FK_CollectedBy = Convert.ToInt32(IUserValidations.FK_CollectedBy);
            if (IUserValidations.Category != "")
                Category = Convert.ToInt32(IUserValidations.Category);
            if (IUserValidations.BranchCode != "")
                BranchCode = Convert.ToInt64(IUserValidations.BranchCode);
            if (IUserValidations.ID_TodoListLeadDetails != "")
                ID_TodoListLeadDetails = Convert.ToInt16(IUserValidations.ID_TodoListLeadDetails);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt32(IUserValidations.FK_Customer);
            if (IUserValidations.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt32(IUserValidations.FK_CustomerOthers);
            if (IUserValidations.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(IUserValidations.FK_Customerserviceregister);
            if (IUserValidations.FK_CustomerserviceregisterProductDetails != "")
                FK_CustomerserviceregisterProductDetails = Convert.ToInt32(IUserValidations.FK_CustomerserviceregisterProductDetails);
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, IUserValidations.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, IUserValidations.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, IUserValidations.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, IUserValidations.OldMPIN);
            db.AddInParameter(dbCommand, "@Token", DbType.String, IUserValidations.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, IUserValidations.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, IUserValidations.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IUserValidations.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IUserValidations.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, IUserValidations.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, IUserValidations.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, IUserValidations.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, IUserValidations.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, IUserValidations.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, IUserValidations.LeadNo);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int64, Criteria);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@FK_CollectedBy", DbType.Int64, FK_CollectedBy);
            db.AddInParameter(dbCommand, "@Category", DbType.Int64, Category);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, BranchCode);
            db.AddInParameter(dbCommand, "@ID_TodoListLeadDetails", DbType.Int16, ID_TodoListLeadDetails);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_CustomerOtherID", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@FK_CustomerserviceregisterProductDetails", DbType.Int64, FK_CustomerserviceregisterProductDetails);

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
        public DataSet DalAgendaCount(IUserValidations IUserValidations)
        {

            Int32 ReqMode = 0, SubMode = 0, IsOnline = 0;
            Int64 FK_Employee = 0, ID_LeadFrom = 0, FK_Category = 0, ID_Department = 0, ID_BranchType = 0, ID_ReportSettings = 0, ID_LeadGenerateProduct = 0,
                FK_Country = 0, FK_District = 0, FK_States = 0, FK_Area = 0, ID_LeadGenerate = 0, ID_ActionType = 0, ID_Branch = 0, ID_NotificationDetails = 0, ID_LeadDocumentDetails = 0, FK_MediaMaster = 0, FK_Company = 0, FK_User = 0, FK_BranchCodeUser = 0, FK_Product = 0,
                FK_Priority = 0, FollowUpAction = 0, FK_CustomerserviceregisterProductDetails = 0, FollowUpType = 0, Criteria = 0, Status = 0, FK_CollectedBy = 0, Category = 0, BranchCode = 0, FK_Customer = 0, FK_CustomerOthers = 0, FK_Customerserviceregister = 0;
            Int16 ID_TodoListLeadDetails = 0;


            Int16 ReportMode = 0, GroupId = 0;
            bool PrductOnly = false;
            if (IUserValidations.ReqMode != "")
                ReqMode = Convert.ToInt32(IUserValidations.ReqMode);
            if (IUserValidations.SubMode != "")
                SubMode = Convert.ToInt32(IUserValidations.SubMode);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.ID_LeadFrom != "")
                ID_LeadFrom = Convert.ToInt64(IUserValidations.ID_LeadFrom);
            if (IUserValidations.ID_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.ID_Category);
            if (IUserValidations.ID_Department != "")
                ID_Department = Convert.ToInt64(IUserValidations.ID_Department);
            if (IUserValidations.ID_BranchType != "")
                ID_BranchType = Convert.ToInt64(IUserValidations.ID_BranchType);
            if (IUserValidations.ID_ReportSettings != "")
                ID_ReportSettings = Convert.ToInt64(IUserValidations.ID_ReportSettings);
            if (IUserValidations.ID_LeadGenerateProduct != "")
                ID_LeadGenerateProduct = Convert.ToInt64(IUserValidations.ID_LeadGenerateProduct);
            if (IUserValidations.PrductOnly != "")
                PrductOnly = Convert.ToBoolean(IUserValidations.PrductOnly);
            if (IUserValidations.FK_Country != "")
                FK_Country = Convert.ToInt64(IUserValidations.FK_Country);
            if (IUserValidations.FK_District != "")
                FK_District = Convert.ToInt64(IUserValidations.FK_District);
            if (IUserValidations.FK_States != "")
                FK_States = Convert.ToInt64(IUserValidations.FK_States);
            if (IUserValidations.FK_Area != "")
                FK_Area = Convert.ToInt64(IUserValidations.FK_Area);
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(IUserValidations.ID_ActionType);
            if (IUserValidations.IsOnline != "")
                IsOnline = Convert.ToInt32(IUserValidations.IsOnline);
            if (IUserValidations.ReportMode != "")
                ReportMode = Convert.ToInt16(IUserValidations.ReportMode);
            if (IUserValidations.ID_Branch != "")
                ID_Branch = Convert.ToInt64(IUserValidations.ID_Branch);
            if (IUserValidations.GroupId != "")
                GroupId = Convert.ToInt16(IUserValidations.GroupId);
            if (IUserValidations.ID_NotificationDetails != "")
                ID_NotificationDetails = Convert.ToInt64(IUserValidations.ID_NotificationDetails);
            if (IUserValidations.ID_LeadDocumentDetails != "")
                ID_LeadDocumentDetails = Convert.ToInt64(IUserValidations.ID_LeadDocumentDetails);
            if (IUserValidations.FK_MediaMaster != "")
                FK_MediaMaster = Convert.ToInt32(IUserValidations.FK_MediaMaster);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt32(IUserValidations.FK_User);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Product);
            if (IUserValidations.FK_Priority != "")
                FK_Product = Convert.ToInt32(IUserValidations.FK_Priority);
            if (IUserValidations.FollowUpAction != "")
                FollowUpAction = Convert.ToInt32(IUserValidations.FollowUpAction);
            if (IUserValidations.FollowUpType != "")
                FollowUpType = Convert.ToInt32(IUserValidations.FollowUpType);
            if (IUserValidations.Criteria != "")
                Criteria = Convert.ToInt32(IUserValidations.Criteria);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt32(IUserValidations.Status);
            if (IUserValidations.FK_CollectedBy != "")
                FK_CollectedBy = Convert.ToInt32(IUserValidations.FK_CollectedBy);
            if (IUserValidations.Category != "")
                Category = Convert.ToInt32(IUserValidations.Category);
            if (IUserValidations.FK_Branch != "")
                BranchCode = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.ID_TodoListLeadDetails != "")
                ID_TodoListLeadDetails = Convert.ToInt16(IUserValidations.ID_TodoListLeadDetails);
            if (IUserValidations.FK_Customer != "")
                FK_Customer = Convert.ToInt32(IUserValidations.FK_Customer);
            if (IUserValidations.FK_CustomerOthers != "")
                FK_CustomerOthers = Convert.ToInt32(IUserValidations.FK_CustomerOthers);
            if (IUserValidations.FK_Customerserviceregister != "")
                FK_Customerserviceregister = Convert.ToInt32(IUserValidations.FK_Customerserviceregister);
            if (IUserValidations.FK_CustomerserviceregisterProductDetails != "")
                FK_CustomerserviceregisterProductDetails = Convert.ToInt32(IUserValidations.FK_CustomerserviceregisterProductDetails);
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, IUserValidations.MobileNumber);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, IUserValidations.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, IUserValidations.MPIN);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@OldMPIN", DbType.String, IUserValidations.OldMPIN);
            db.AddInParameter(dbCommand, "@Token", DbType.String, IUserValidations.Token);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
            db.AddInParameter(dbCommand, "@Email", DbType.String, IUserValidations.Email);
            db.AddInParameter(dbCommand, "@Address", DbType.String, IUserValidations.Address);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.String, ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_BranchType", DbType.Int64, ID_BranchType);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, ID_Department);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IUserValidations.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IUserValidations.Todate);
            db.AddInParameter(dbCommand, "@FK_ReportSetting", DbType.Int64, ID_ReportSettings);
            db.AddInParameter(dbCommand, "@ID_LeadGenerateProduct", DbType.Int64, ID_LeadGenerateProduct);
            db.AddInParameter(dbCommand, "@PrductOnly", DbType.Boolean, PrductOnly);
            db.AddInParameter(dbCommand, "@Pincode", DbType.String, IUserValidations.Pincode);
            db.AddInParameter(dbCommand, "@FK_Country", DbType.Int64, FK_Country);
            db.AddInParameter(dbCommand, "@FK_District", DbType.Int64, FK_District);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_States", DbType.Int64, FK_States);
            db.AddInParameter(dbCommand, "@CustomerNote", DbType.String, IUserValidations.CustomerNote);
            db.AddInParameter(dbCommand, "@EmployeeNote", DbType.String, IUserValidations.EmployeeNote);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@Remark", DbType.String, IUserValidations.Remark);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
            db.AddInParameter(dbCommand, "@IsOnline", DbType.Int32, IsOnline);
            db.AddInParameter(dbCommand, "@LocationName", DbType.String, IUserValidations.LocationName);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@ID_Branch", DbType.Int64, ID_Branch);
            db.AddInParameter(dbCommand, "@ReportMode", DbType.Int16, ReportMode);
            db.AddInParameter(dbCommand, "@GroupId", DbType.Int16, GroupId);
            db.AddInParameter(dbCommand, "@ID_NotificationDetails", DbType.Int64, ID_NotificationDetails);
            db.AddInParameter(dbCommand, "@ID_LeadDocumentDetails", DbType.Int64, ID_LeadDocumentDetails);
            db.AddInParameter(dbCommand, "@FK_MediaMaster", DbType.Int64, FK_MediaMaster);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, FK_Priority);
            db.AddInParameter(dbCommand, "@FollowUpAction", DbType.Int64, FollowUpAction);
            db.AddInParameter(dbCommand, "@FollowUpType", DbType.Int64, FollowUpType);
            db.AddInParameter(dbCommand, "@LeadNo", DbType.String, IUserValidations.LeadNo);
            db.AddInParameter(dbCommand, "@Criteria", DbType.Int64, Criteria);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            db.AddInParameter(dbCommand, "@FK_CollectedBy", DbType.Int64, FK_CollectedBy);
            db.AddInParameter(dbCommand, "@Category", DbType.Int64, Category);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, BranchCode);
            db.AddInParameter(dbCommand, "@ID_TodoListLeadDetails", DbType.Int16, ID_TodoListLeadDetails);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, FK_Customer);
            db.AddInParameter(dbCommand, "@FK_CustomerOtherID", DbType.Int64, FK_CustomerOthers);
            db.AddInParameter(dbCommand, "@FK_Customerserviceregister", DbType.Int64, FK_Customerserviceregister);
            db.AddInParameter(dbCommand, "@FK_CustomerserviceregisterProductDetails", DbType.Int64, FK_CustomerserviceregisterProductDetails);

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
        public DataTable DalProCustomerAssignmentUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0, FK_Employee = 0, ID_CustomerAssignment = 0, FK_Product = 0, FK_Category = 0;


            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.ID_CustomerAssignment != "")
                ID_CustomerAssignment = Convert.ToInt64(IUserValidations.ID_CustomerAssignment);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt64(IUserValidations.FK_Product);
            if (IUserValidations.FK_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.FK_Category);

            byte[] voiceData = Encoding.UTF8.GetBytes(IUserValidations.VoiceData);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProCustomerAssignmentUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, IUserValidations.UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@ID_CustomerAssignment", DbType.Int64, ID_CustomerAssignment);
            db.AddInParameter(dbCommand, "@CusName", DbType.String, IUserValidations.CusName);
            db.AddInParameter(dbCommand, "@CusMobile", DbType.String, IUserValidations.CusMobile);
            db.AddInParameter(dbCommand, "@CaAssignedDate", DbType.String, IUserValidations.CaAssignedDate);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.String, FK_Employee);
            db.AddInParameter(dbCommand, "@CaDescription", DbType.String, IUserValidations.CaDescription);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IUserValidations.FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@CaCusEmail", DbType.String, IUserValidations.Email);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@ProjectName", DbType.String, IUserValidations.@ProjectName);
            db.AddInParameter(dbCommand, "@leadByMobileNo", DbType.Xml, IUserValidations.SubProductDetails);
            db.AddInParameter(dbCommand, "@VoiceData", DbType.Binary, voiceData);
            db.AddInParameter(dbCommand, "@VoiceDataLabel", DbType.String, IUserValidations.VoiceDataName);

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

        public DataTable DalProLocationUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, ID_ImageLocation = 0, FK_Master=0;


            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
           
            if (IUserValidations.ID_ImageLocation != "")
                ID_ImageLocation = Convert.ToInt64(IUserValidations.ID_ImageLocation);
            if (IUserValidations.FK_Master != "")
                FK_Master = Convert.ToInt64(IUserValidations.FK_Master);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProImageLocationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int32, ID_ImageLocation==0?1:2);           
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@ID_ImageLocation", DbType.Int64, ID_ImageLocation);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, FK_Master);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocationAddress", DbType.String, IUserValidations.LocationAddress);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@LocationEnteredDate", DbType.String, IUserValidations.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@LocationEnteredTime", DbType.String, IUserValidations.LocationEnteredTime);
            db.AddInParameter(dbCommand, "@LocationLandMark1", DbType.Binary, IUserValidations.LocationLandMark1);
            db.AddInParameter(dbCommand, "@LocationLandMark2", DbType.Binary, IUserValidations.LocationLandMark2);
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
        public DataTable DalProFollowUpStatusUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, ID_ImageLocation = 0, FK_Master = 0,Status=0;


            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);

            if (IUserValidations.ID_ImageLocation != "")
                ID_ImageLocation = Convert.ToInt64(IUserValidations.ID_ImageLocation);
            if (IUserValidations.FK_Master != "")
                FK_Master = Convert.ToInt64(IUserValidations.FK_Master);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt64(IUserValidations.Status);

           

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProFollowUpStatusUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@ID_FollowUpStatus", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, FK_Master);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocationAddress", DbType.String, IUserValidations.LocationAddress);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@LocationEnteredDate", DbType.String, IUserValidations.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@LocationEnteredTime", DbType.String, IUserValidations.LocationEnteredTime);          
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Status", DbType.Int64, Status);

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

        public DataTable DalProAttanceMarkingUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, ID_EmployeeAttanceMarking = 0, FK_Employee = 0, Status=0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);

            if (IUserValidations.ID_EmployeeAttanceMarking != "")
                ID_EmployeeAttanceMarking = Convert.ToInt64(IUserValidations.ID_EmployeeAttanceMarking);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.Status != "")
                Status = Convert.ToInt64(IUserValidations.Status);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProEmployeeAttanceMarkingUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int32, ID_EmployeeAttanceMarking == 0 ? 1 : 2);          
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@ID_EmployeeAttanceMarking", DbType.Int64, ID_EmployeeAttanceMarking);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocAddress", DbType.String, IUserValidations.LocationAddress);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@AttanceEnteredDate", DbType.String, IUserValidations.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@AttanceEnteredTime", DbType.String, IUserValidations.LocationEnteredTime);           
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Status", DbType.Int64, Status);
            
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
        public DataTable DalProEmployeeLocationUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, ID_EmployeeLocation = 0, FK_Employee = 0, ChargePercentage = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.ID_EmployeeLocation != "")
                ID_EmployeeLocation = Convert.ToInt64(IUserValidations.ID_EmployeeLocation);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.ChargePercentage != "")
                ChargePercentage = Convert.ToInt64(IUserValidations.ChargePercentage);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProEmployeeLocationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@UserAction", DbType.Int32, ID_EmployeeLocation == 0 ? 1 : 2);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@ID_EmployeeLocation", DbType.Int64, ID_EmployeeLocation);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, IUserValidations.LocLongitude);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, IUserValidations.LocLatitude);
            db.AddInParameter(dbCommand, "@LocAddress", DbType.String, IUserValidations.LocationAddress);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@AttanceEnteredDate", DbType.String, IUserValidations.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@AttanceEnteredTime", DbType.String, IUserValidations.LocationEnteredTime);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@ChargePercentage", DbType.Int32, ChargePercentage);
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
        public DataTable DalProNotificationUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, Reciever = 0, ID_User = 0, FK_Master=0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.Reciever != "")
                Reciever = Convert.ToInt64(IUserValidations.Reciever);
            if (IUserValidations.ID_User != "")
                ID_User = Convert.ToInt64(IUserValidations.ID_User);
            if(IUserValidations.FK_Master!="")
                FK_Master = Convert.ToInt64(IUserValidations.FK_Master);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProNotificationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Reciever", DbType.Int64, Reciever);
            db.AddInParameter(dbCommand, "@Sender", DbType.Int64, ID_User);
            db.AddInParameter(dbCommand, "@Title", DbType.String, IUserValidations.Title);
            db.AddInParameter(dbCommand, "@Message", DbType.String, IUserValidations.Message);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, FK_Master);          
            db.AddInParameter(dbCommand, "@LgChannel", DbType.Int32, 1);

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
        public DataTable DalProEmployeeLocationList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_Departement = 0, FK_Employee = 0, FK_Designation = 0, FK_Branch=0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);           
            if (IUserValidations.FK_Departement != "")
                FK_Departement = Convert.ToInt64(IUserValidations.FK_Departement);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.FK_Designation != "")
                FK_Designation = Convert.ToInt64(IUserValidations.FK_Designation);
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProEmployeeLocationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Date", DbType.String, IUserValidations.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, FK_Departement);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Designation", DbType.Int64, FK_Designation);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
           
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
        public DataTable DalProEmployeeWiseLocationList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_Employee = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);           
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
          

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProEmployeeWiseLocationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Date", DbType.String, IUserValidations.LocationEnteredDate);            
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
        public DataTable DalGetMailSettings(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);           

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProGetGeneralSettings";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@GsModule", DbType.String, "MAIL");            

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
        public string SendMail(IUserValidations IUserValidations,DataTable dt)
        {
            Int64 FK_Company = 1, FK_Branch=0;

            string userName = "", password = "", host = "", port = "";
            bool enableSSL = false;


            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    switch(row["GsField"].ToString())
                    {
                        case "HOST":
                            host = row["GsData"].ToString();
                            break;
                        case "PORT":
                            port = row["GsData"].ToString();
                            break;
                        case "UNAME":
                            userName = row["GsData"].ToString();
                            break;
                        case "PWD":
                            password = row["GsData"].ToString();
                            break;
                        case "ENSSL":
                            enableSSL = Convert.ToBoolean(row["GsValue"]);
                            break;
                    }                   
                   
                }
                

                MailMessage message = new MailMessage(userName.ToString(), IUserValidations.ToEmail.ToString(), IUserValidations.Subject.ToString(), IUserValidations.Body.ToString());
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host.Trim();
                smtp.Port = Convert.ToInt32(port);
                smtp.Credentials = new NetworkCredential(userName.Trim(), password.Trim());
                smtp.EnableSsl = enableSSL;
                message.IsBodyHtml = true;
                smtp.Send(message);

                if (IUserValidations.FK_Company != "")
                    FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
                if (IUserValidations.FK_Branch != "")
                    FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);

                DataTable dtbl = new DataTable();
                Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
                string sqlCommand = "ProMailDataUpdate";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, 0);
                db.AddInParameter(dbCommand, "@TransMode", DbType.String, "APP");
                db.AddInParameter(dbCommand, "@EmailID", DbType.String, IUserValidations.ToEmail.ToString());
                db.AddInParameter(dbCommand, "@Subject", DbType.String, IUserValidations.Subject.ToString());
                db.AddInParameter(dbCommand, "@Body", DbType.String, IUserValidations.Body.ToString());                
                db.AddInParameter(dbCommand, "@Attachment", DbType.String, "");
                db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
                db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
                db.ExecuteNonQuery(dbCommand);
                return "Mail successfully sent";
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalProItemList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
           

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProItemSearchByBarcode";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);          
            db.AddInParameter(dbCommand, "@ReadCode", DbType.String, IUserValidations.Name);
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
        public DataTable DalProProductEnquiry(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_Category=0, FK_Branch=0, FK_Product=0, Offer=0;
            string Name = "";

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.FK_Category);
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt64(IUserValidations.FK_Product);
            if (IUserValidations.Offer != "")
                Offer = Convert.ToInt64(IUserValidations.Offer);
           
            
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProProductEnquiry";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
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

        public DataSet DalProProductEnquiryDetails(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_Category = 0, FK_Branch = 0, FK_Product = 0, Offer = 0;
           
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_Category != "")
                FK_Category = Convert.ToInt64(IUserValidations.FK_Category);
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.FK_Product != "")
                FK_Product = Convert.ToInt64(IUserValidations.FK_Product);
            if (IUserValidations.Offer != "")
                Offer = Convert.ToInt64(IUserValidations.Offer);


            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProProductEnquiryDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
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
        public DataTable DalProAuthorizationModuleList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_UserGroup = 0, FK_User = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);
            if (IUserValidations.FK_UserGroup != "")
                FK_UserGroup = Convert.ToInt64(IUserValidations.FK_UserGroup);
           


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationModuleList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
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
        public DataTable DalProAuthorizationList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_UserGroup = 0, FK_User = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);
            if (IUserValidations.FK_UserGroup != "")
                FK_UserGroup = Convert.ToInt64(IUserValidations.FK_UserGroup);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@Module", DbType.String, IUserValidations.Module);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
           
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
        public DataTable DalProAuthorizationAction(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_UserGroup = 0, FK_User = 0, AuthID=0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);
            if (IUserValidations.FK_UserGroup != "")
                FK_UserGroup = Convert.ToInt64(IUserValidations.FK_UserGroup);
            if(IUserValidations.AuthID!="")
                AuthID = Convert.ToInt64(IUserValidations.AuthID);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationAction";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@Module", DbType.String, IUserValidations.Module);
            db.AddInParameter(dbCommand, "@AuthID", DbType.Int64, AuthID);

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
        public DataTable DalAuthorizationListDetails(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_Master = 0, Mode = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);           
            if (IUserValidations.FK_Master != "")
                FK_Master = Convert.ToInt64(IUserValidations.FK_Master);
            if (IUserValidations.ReqMode != "")
                Mode = Convert.ToInt64(IUserValidations.ReqMode);

         
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationListDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, FK_Master);            
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int64, Mode);           
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
        public DataTable DalAuthorizationApproveUpdate(IUserValidations IUserValidations)
        {

          
            Int64 FK_Company = 0, FK_AuthorizationData = 0, SkipPrev=0;
          
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.AuthID != "")
                FK_AuthorizationData = Convert.ToInt32(IUserValidations.AuthID);
            if (IUserValidations.SkipPrev != "")
                SkipPrev = Convert.ToInt32(IUserValidations.SkipPrev);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationApproveUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_AuthorizationData", DbType.Int64, FK_AuthorizationData);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@SkipPrev", DbType.Int64, SkipPrev);
            db.AddInParameter(dbCommand, "@ID_AuthorizationLevel", DbType.Int64, 0);

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
        public DataTable DalAuthorizationRejectUpdate(IUserValidations IUserValidations)
        {


            Int64 FK_Company = 0, FK_AuthorizationData = 0, FK_Reason=0, SkipPrev = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.AuthID != "")
                FK_AuthorizationData = Convert.ToInt32(IUserValidations.AuthID);
            if (IUserValidations.FK_Reason != "")
                FK_Reason = Convert.ToInt32(IUserValidations.FK_Reason);
            if (IUserValidations.SkipPrev != "")
                SkipPrev = Convert.ToInt32(IUserValidations.SkipPrev);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationRejectUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_AuthorizationData", DbType.Int64, FK_AuthorizationData);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@SkipPrev", DbType.Int64, SkipPrev);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, IUserValidations.Reason);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, FK_Reason);
            db.AddInParameter(dbCommand, "@ID_AuthorizationLevel", DbType.Int64, 0);
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
        public DataTable DalAuthorizationCorrection(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 0, FK_AuthorizationData = 0, SkipPrev = 0, FK_Reason = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.AuthID != "")
                FK_AuthorizationData = Convert.ToInt32(IUserValidations.AuthID);
            if (IUserValidations.SkipPrev != "")
                SkipPrev = Convert.ToInt32(IUserValidations.SkipPrev);
            if (IUserValidations.FK_Reason != "")
                FK_Reason = Convert.ToInt32(IUserValidations.FK_Reason);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationCorrectionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_AuthorizationData", DbType.Int64, FK_AuthorizationData);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@SkipPrev", DbType.Int64, SkipPrev);
            db.AddInParameter(dbCommand, "@Reason", DbType.String, IUserValidations.Reason);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, FK_Reason);
            db.AddInParameter(dbCommand, "@ID_AuthorizationLevel", DbType.Int64, 0);

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
        public DataTable DalProUserTaskList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_UserGroup = 0, FK_User = 0, FK_Employee=0, FK_Branch=0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);
            if (IUserValidations.FK_UserGroup != "")
                FK_UserGroup = Convert.ToInt64(IUserValidations.FK_UserGroup);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProUserTaskList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            
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
        public DataTable DalProGetLeadByMobileNo(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);           


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProGetLeadByMobileNo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@MobileNo", DbType.String, IUserValidations.MobileNumber);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int64, 1);
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
        public DataSet DalProAuthorizationCorrectionModuleList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_User = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);
           


            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationCorrectionModuleList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
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
        public DataTable DalProAuthorizationCorrectionDetailsList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 0, FK_User = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAuthorizationCorrectionDetailsList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@Module", DbType.String, IUserValidations.Module);
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
        public DataSet DalProAuthorizationCorrectionLeadDetails(IUserValidations IUserValidations)
        {
            Int64 ID_LeadGenerate=0,FK_Company = 0, FK_BranchCodeUser=0, FK_AuthorizationData=0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.ID_LeadGenerate != "")
                ID_LeadGenerate = Convert.ToInt64(IUserValidations.ID_LeadGenerate);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
            if (IUserValidations.FK_AuthorizationData != "")
                FK_AuthorizationData = Convert.ToInt64(IUserValidations.FK_AuthorizationData);


            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProLeadGenerateCorrectionData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@ID_LeadGenerate", DbType.Int64, ID_LeadGenerate);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy);            
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@FK_AuthorizationData", DbType.Int64, FK_AuthorizationData);
            
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
        public DataTable DalProGetVoiceInfo(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 0, FK_CustomerAssignment = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.ID_CustomerAssignment != "")
                FK_CustomerAssignment = Convert.ToInt64(IUserValidations.ID_CustomerAssignment);
           

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProGetVoiceInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);          
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_CustomerAssignment", DbType.Int64, FK_CustomerAssignment);
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
        public DataSet DalAuthorizationDetails(IUserValidations IUserValidation)
        {
           
            Int64 FK_Employee = 0, FK_Company = 1;

            if (IUserValidation.FK_Employee != "")
                FK_Employee = Convert.ToInt32(IUserValidation.FK_Employee);
            if (IUserValidation.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidation.FK_Company);

            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidation.BankKey);
            string sqlCommand = "ProAPIAuthorizationDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidation.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);

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
        public DataTable DalProAPIUserTaskList(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_UserGroup = 0, FK_User = 0, FK_Employee = 0, FK_Branch = 0 , Critrea1 = 0, Pagemode = 0;

            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_User != "")
                FK_User = Convert.ToInt64(IUserValidations.FK_User);
            if (IUserValidations.FK_UserGroup != "")
                FK_UserGroup = Convert.ToInt64(IUserValidations.FK_UserGroup);
            if (IUserValidations.FK_Employee != "")
                FK_Employee = Convert.ToInt64(IUserValidations.FK_Employee);
            if (IUserValidations.FK_Branch != "")
                FK_Branch = Convert.ToInt64(IUserValidations.FK_Branch);
            if (IUserValidations.ReqMode != "")
                Pagemode = Convert.ToInt32(IUserValidations.ReqMode);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProAPIUserTaskList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_UserGroup", DbType.Int64, FK_UserGroup);
            db.AddInParameter(dbCommand, "@FK_User", DbType.Int64, FK_User);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            //db.AddInParameter(dbCommand, "@Mode", DbType.Int64, IUserValidations.ReqMode);

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
        public int VersionCheck(string versionNo, string BankKey, byte OsType)
        {
            int outvalue = 0;
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
            string sqlCommand = "proVersionCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@VersionNo", DbType.String, versionNo);
            db.AddInParameter(dbCommand, "@OsType", DbType.Byte, OsType);
            try
            {
                outvalue = Convert.ToInt16(db.ExecuteScalar(dbCommand));
                return outvalue;
                // mobile = dbCommand.Parameters["@Mobile"].Value.ToString();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalSendIntimationUpdate(IUserValidations IUserValidations)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser = 0;


            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt64(IUserValidations.FK_Company);
            if (IUserValidations.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IUserValidations.FK_BranchCodeUser);
           
           
            //if (IProjectDetails.FK_Product != "")
            //    FK_Product = Convert.ToInt64(IProjectDetails.FK_Product);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
            string sqlCommand = "ProcommonIntimationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, IUserValidations.UserAction == "" ? "0" : IUserValidations.UserAction);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IUserValidations.TransMode == "" ? "0" : IUserValidations.TransMode);
            db.AddInParameter(dbCommand, "@ID_CommonIntimation", DbType.Int64, IUserValidations.ID_CommonIntimation == "" ? "0" : IUserValidations.ID_CommonIntimation);
            db.AddInParameter(dbCommand, "@Module", DbType.Int64, IUserValidations.Module == "" ? "0" : IUserValidations.Module);
            db.AddInParameter(dbCommand, "@Branch", DbType.Int64, IUserValidations.Branch == "" ? "0" : IUserValidations.Branch);
            db.AddInParameter(dbCommand, "@Status ", DbType.Int16, IUserValidations.Status == "" ? "0" : IUserValidations.Status);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int16, IUserValidations.Channel == "" ? "0" : IUserValidations.Channel);
            db.AddInParameter(dbCommand, "@SheduledType", DbType.Int16, IUserValidations.SheduledType == "" ? "0" : IUserValidations.SheduledType);
            db.AddInParameter(dbCommand, "@DlId", DbType.String, IUserValidations.DlId == "" ? "0" : IUserValidations.DlId);
            db.AddInParameter(dbCommand, "@Subject", DbType.String, IUserValidations.Subject == "" ? "0" : IUserValidations.Subject);
            db.AddInParameter(dbCommand, "@Unicode", DbType.Byte,  IUserValidations.Unicode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, IUserValidations.FK_Company == "" ? "0" : IUserValidations.FK_Company);
            //db.AddInParameter(dbCommand, "@Attachment", DbType.String, IUserValidations.Attachment == "" ? "" : IUserValidations.Attachment);
            db.AddInParameter(dbCommand, "@Attachment", DbType.Binary, IUserValidations.Attachment);
            db.AddInParameter(dbCommand, "@Message", DbType.String, IUserValidations.Message == "" ? "" : IUserValidations.Message);
            db.AddInParameter(dbCommand, "@Date", DbType.DateTime, IUserValidations.Date == "" ? "" : IUserValidations.Date);
            db.AddInParameter(dbCommand, "@SheduledTime", DbType.String, IUserValidations.SheduledTime == "" ? "" : IUserValidations.SheduledTime);
            db.AddInParameter(dbCommand, "@SheduledDate", DbType.String, IUserValidations.SheduledDate == "" ? DBNull.Value.ToString() :Convert.ToDateTime( IUserValidations.SheduledDate).ToString("yyyy-MM-dd"));
            db.AddInParameter(dbCommand, "@FK_CommonIntimation", DbType.Int64, IUserValidations.FK_CommonIntimation == "" ? "0" : IUserValidations.Branch);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IUserValidations.EntrBy == "" ? "" : IUserValidations.EntrBy);
            db.AddInParameter(dbCommand, "@ID_LeadFrom", DbType.Int64, IUserValidations.ID_LeadFrom == "" ? "0" : IUserValidations.ID_LeadFrom);
            db.AddInParameter(dbCommand, "@FK_LeadThrough", DbType.Int64, IUserValidations.FK_LeadThrough == "" ? "0" : IUserValidations.FK_LeadThrough);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, IUserValidations.FromDate == "" ? "" : IUserValidations.FromDate);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, IUserValidations.ToDate == "" ? "" : IUserValidations.ToDate);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, IUserValidations.FK_Category == "" ? "0" : IUserValidations.FK_Category);
            db.AddInParameter(dbCommand, "@ProdType", DbType.Int64, IUserValidations.ProdType == "" ? "0" : IUserValidations.ProdType);
            db.AddInParameter(dbCommand, "@ID_Product", DbType.Int64, IUserValidations.ID_Product == "" ? "0" : IUserValidations.ID_Product);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, IUserValidations.FK_Employee == "" ? "0" : IUserValidations.FK_Employee);
            db.AddInParameter(dbCommand, "@Collectedby_ID", DbType.Int64, IUserValidations.Collectedby_ID == "" ? "0" : IUserValidations.Collectedby_ID);
            db.AddInParameter(dbCommand, "@Area_ID", DbType.Int64, IUserValidations.Area_ID == "" ? "0" : IUserValidations.Area_ID);
            db.AddInParameter(dbCommand, "@FK_NetAction", DbType.Int64, IUserValidations.FK_NetAction == "" ? "0" : IUserValidations.FK_NetAction);
            db.AddInParameter(dbCommand, "@FK_ActionType ", DbType.Int64, IUserValidations.FK_ActionType == "" ? "0" : IUserValidations.FK_ActionType);
            db.AddInParameter(dbCommand, "@FK_Priority", DbType.Int64, IUserValidations.FK_Priority == "" ? "0" : IUserValidations.FK_Priority);
            db.AddInParameter(dbCommand, "@SearchBy", DbType.Int64, IUserValidations.SearchBy == "" ? "0" : IUserValidations.SearchBy);
            db.AddInParameter(dbCommand, "@SearchBydetails", DbType.String, IUserValidations.SearchBydetails == "" ? "" : IUserValidations.SearchBydetails);
            //db.AddInParameter(dbCommand, "@LeadDetails", DbType.String, IUserValidations.LeadDetails == "" ? "0" : IUserValidations.LeadDetails);
            db.AddInParameter(dbCommand, "@GridData", DbType.Byte, IUserValidations.GridData == "" ? "0" : IUserValidations.GridData);
            db.AddInParameter(dbCommand, "@LeadDetails", DbType.Xml, IUserValidations.SubProductDetails);
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
        public DataTable DalAppConfigurationSettings(string BankKey, string ReqMode, string ConfigCode)
        {

                       

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);           
            db.AddInParameter(dbCommand, "@CompanyCode", DbType.String, ConfigCode);

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
        public DataTable DalPageSettingsDetails(string BankKey, string PSModule, string PSPage,Int64 FK_Company,Int16 Mode)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
            string sqlCommand = "ProGetPageSettingsDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@PSModule", DbType.String, PSModule);
            db.AddInParameter(dbCommand, "@PSPage", DbType.String, PSPage);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int16, Mode);
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
        public DataTable DalEmployeeLocationDistance(string BankKey, string FKCompany, string LocationEnteredDate, string FKEmployee)
        {
            Int64 FK_Company = 1, FK_Employee = 0;

            if (FKCompany != "")
                FK_Company = Convert.ToInt64(FKCompany);
            if (FKEmployee != "")
                FK_Employee = Convert.ToInt64(FKEmployee);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
            string sqlCommand = "ProAPIEmployeeWiseLocationSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@Date", DbType.String, LocationEnteredDate);
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
        public DataTable DalAttendanceDetails(string BankKey, string FK_Company, string EnteredDate, string EntrBy, Int16 Mode)
        {





            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
            string sqlCommand = "ProAPIEmployeeAttanceDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EnteredDate", DbType.String, EnteredDate);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, EntrBy);
            db.AddInParameter(dbCommand, "@Mode", DbType.String, Mode);

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
        public DataTable GetLicense(string BankKey)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet dsTrans = new DataSet();
                Database db = DatabaseFactory.CreateDatabase("PerfectERP" + BankKey);
                string sqlCommand = "proPssGetLicense";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                dsTrans = db.ExecuteDataSet(dbCommand);
                if (dsTrans.Tables.Count > 0)
                {
                    dt = dsTrans.Tables[0];
                }
                return dt;
            }
            catch (Exception ex)
            {
                return dt;
            }

        }
        public string GetBankNameAndTitle(string BankKey, Int64 FK_Company)
        {
            try
            {
                string str = "";
                Database db = DatabaseFactory.CreateDatabase("PerfectERP" +BankKey);
                //DbCommand dbCommand = db.GetSqlStringCommand("SELECT BraName + '!' + BraBankTitle FROM Branch WHERE BraHeadOffice = 1  ");
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT CompName + '!' + CompName  FROM Company WHERE ID_Company='"+FK_Company+"'");
                str = db.ExecuteScalar(dbCommand).ToString();
                return str;
            }
            catch (Exception e)
            {
                // clsFunctions.WriteToLog("Msg---" + e.Message + "---Stack Trace--" + e.StackTrace.ToString());
                throw;
            }
        }

        public DataTable DalTokenCheck(IUserValidations user)
        {
            Int64 ID_TokenUser = 0;
            if (user.ID_TokenUser != "")
                ID_TokenUser = Convert.ToInt64(user.ID_TokenUser);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + user.BankKey);
            string sqlCommand = "proTokenCheck";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);          
            db.AddInParameter(dbCommand, "@UserToken", DbType.String, user.Token);
            db.AddInParameter(dbCommand, "@ID_Users", DbType.Int64, ID_TokenUser);
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
        public DataTable DalMyActivities(IUserValidations user)
        {
            Int64 FK_Employee=0, ID_ActionType=0;
            Int32 ReqMode = 0, SubMode = 0;
            if (user.FK_Employee != "")
                FK_Employee = Convert.ToInt64(user.FK_Employee);
            if (user.ID_ActionType != "")
                ID_ActionType = Convert.ToInt64(user.ID_ActionType);
            if (user.ReqMode != "")
                ReqMode = Convert.ToInt32(user.ReqMode);
            if (user.SubMode != "")
                SubMode = Convert.ToInt32(user.SubMode);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + user.BankKey);
            string sqlCommand = "ProAPIMyActivities";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.String, FK_Employee);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int64, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.String, SubMode);
            db.AddInParameter(dbCommand, "@ID_ActionType", DbType.Int64, ID_ActionType);
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
