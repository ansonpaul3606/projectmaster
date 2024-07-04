using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;
using Newtonsoft.Json;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalMobileNotification
    {

        
        public DataTable DalSendNotification(IMobileNotification IMobileNotification)
        {


            Int64 FK_Company = 0, CustomerID = 0, FK_Machine = 0, CustomerRoleID = 0;

            if (IMobileNotification.CustomerID != "")
                CustomerID = Convert.ToInt64(IMobileNotification.CustomerID);
            if (IMobileNotification.FK_Company != "")
                FK_Company = Convert.ToInt64(IMobileNotification.FK_Company);
            if (IMobileNotification.CustomerRoleID != "")
                CustomerRoleID = Convert.ToInt64(IMobileNotification.CustomerRoleID);
            if (IMobileNotification.FK_Machine != "")
                FK_Machine = Convert.ToInt64(IMobileNotification.FK_Machine);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IMobileNotification.BankKey);
            string sqlCommand = "ProPortalGetCompanyProfileInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Int64, CustomerRoleID);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, CustomerID);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IMobileNotification.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IMobileNotification.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, FK_Machine);



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
        
        /*
        public DataSet DalGetNotificationSettings(IMobileNotification IMobileNotification)
        {


            Int64 FK_Company = 0, ReceiverRole = 0, FK_Machine = 0, ReceiverID = 0;

            string receiverList = "[]";

            if (IMobileNotification.ReceiverRole != "")
                ReceiverRole = Convert.ToInt64(IMobileNotification.ReceiverRole);

            if (IMobileNotification.FK_Company != "")
                FK_Company = Convert.ToInt64(IMobileNotification.FK_Company);

            if (IMobileNotification.ReceiverID != "")
                ReceiverID = Convert.ToInt64(IMobileNotification.ReceiverID);

            if (IMobileNotification.FK_Machine != "")
                FK_Machine = Convert.ToInt64(IMobileNotification.FK_Machine);


            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IMobileNotification.BankKey);
            string sqlCommand = "ProGetMobileNotiificationSettings";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            db.AddInParameter(dbCommand, "@ReceiverList", DbType.String, IMobileNotification.ReceiverListJSON);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IMobileNotification.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IMobileNotification.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, FK_Machine);



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
        */

        public DataTable DalSaveCustomerFirebaseInfo(IMobileNotification IMobileNotification)
        {


            Int64 ID_User = 0, fk_Company = 0;
            Byte User_Type = 0;
            string Device_ID = "0";

            if (IMobileNotification.ID_User != "")
                ID_User = Convert.ToInt64(IMobileNotification.ID_User);

            if (IMobileNotification.User_Type != "")
                User_Type = Convert.ToByte(IMobileNotification.User_Type);
            if (IMobileNotification.Device_ID != "")
                Device_ID = IMobileNotification.Device_ID;
            if (IMobileNotification.FK_Company != "")
                fk_Company = Convert.ToInt64(IMobileNotification.FK_Company);




            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IMobileNotification.BankKey);
            string sqlCommand = "ProPushNotificationUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            db.AddInParameter(dbCommand, "@PnMode", DbType.Byte, User_Type);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, ID_User);
            db.AddInParameter(dbCommand, "@PnDeviceId", DbType.String, Device_ID);
            db.AddInParameter(dbCommand, "@PnUserToken", DbType.String, IMobileNotification.User_Token);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);




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
        public DataSet DalGetFirebaseNoitification(IMobileNotification IMobileNotification)
        {


            Int64 FK_Company = 0, FK_Machine = 0;

            if (IMobileNotification.FK_Company != "")
                FK_Company = Convert.ToInt64(IMobileNotification.FK_Company);

            if (IMobileNotification.FK_Machine != "")
                FK_Machine = Convert.ToInt64(IMobileNotification.FK_Machine);


            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IMobileNotification.BankKey);
            string sqlCommand = "ProGetMobileNotiificationSettings";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IMobileNotification.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IMobileNotification.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, FK_Machine);


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
        public DataSet DalUpdateNotificationStatus(IMobileNotification IMobileNotification)
        {

            string notification_Status_Json = "[]";
            Int64 fk_Company = 0, fk_Machine = 0;
            Byte action = 0;

            if (IMobileNotification.FK_Company != "")
                fk_Company = Convert.ToInt64(IMobileNotification.FK_Company);

            if (IMobileNotification.FK_Machine != "")
                fk_Machine = Convert.ToInt64(IMobileNotification.FK_Machine);
            if (IMobileNotification.Action != "")
                action = Convert.ToByte(IMobileNotification.Action);

            if (String.IsNullOrWhiteSpace(IMobileNotification.Notification_Status_Json))
            {
                notification_Status_Json = "[]";
            }
            else
            {
                notification_Status_Json = IMobileNotification.Notification_Status_Json;
            }



            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IMobileNotification.BankKey);
            string sqlCommand = "ProGetMobileNotiificationSettings";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "@Action", DbType.Byte, action);
            db.AddInParameter(dbCommand, "@NotificationStatusJson", DbType.String, notification_Status_Json);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IMobileNotification.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IMobileNotification.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_Machine);


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
    }
}
