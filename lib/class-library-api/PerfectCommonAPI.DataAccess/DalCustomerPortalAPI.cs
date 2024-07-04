using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalCustomerPortalAPI
    {

        //Dal funciont to get the customer details based in the Mobile number
        public DataTable GetCustomerInfoByMobile(ICustomerPortalAPI dalInput)
        {


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalCheckUserAccess";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);



            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company)? 0 : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? 0 : Convert.ToInt64(dalInput.FK_Machine));



            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EnterBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@UserName", DbType.String, dalInput.Username);
            db.AddInParameter(dbCommand, "@Email", DbType.String, dalInput.Email);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, dalInput.Phone);
  

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

        // Manage the get,update OTP,UPdate MPIN, seset MPIN 
        public DataTable ManageCustomerPIN(ICustomerPortalAPI dalInput)
        {


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalManageOTP";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);



            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));

            Int64 customerID = (String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID));

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole)? default(byte): Convert.ToByte(dalInput.CustomerRole);
            Byte mode = String.IsNullOrWhiteSpace(dalInput.Mode) ? default(byte) : Convert.ToByte(dalInput.Mode);

            


            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EnterBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);
            db.AddInParameter(dbCommand, "@Mode", DbType.Byte, mode);
            db.AddInParameter(dbCommand, "@OTP", DbType.String, dalInput.OTP);
            db.AddInParameter(dbCommand, "@MPIN", DbType.String, dalInput.MPIN);
            db.AddInParameter(dbCommand, "@Phone", DbType.String, dalInput.Phone);


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

        public DataTable GetCompanyInfoByID(ICustomerPortalAPI dalInput)
        {


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetCompanyProfileInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));

            Int64 customerID = (String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID));
            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);


            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EnterBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);


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

        public DataTable GetCustomerInfoByID(ICustomerPortalAPI dalInput)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetUserInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);

            Int64 customerID = (String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID));



            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EnterBy);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if(ds.Tables.Count>0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);
                    
                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable GetTicketCountStatusByID(ICustomerPortalAPI dalInput)
        {


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetTicketCount";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company);
            Int64 fk_machine = String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine);

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);
            Int64 customerID = String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID);



            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable GetTicketList(ICustomerPortalAPI dalInput)
        {

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetTicketList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company);
            Int64 fk_machine = String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine);

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);
            Int64 customerID = String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID);
            byte ticketStatus = String.IsNullOrWhiteSpace(dalInput.TicketStatus) ? default(byte) : Convert.ToByte(dalInput.TicketStatus);



            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);

            db.AddInParameter(dbCommand, "@TicketStatus", DbType.Byte, ticketStatus);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable GetTicketInfoByID(ICustomerPortalAPI dalInput)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetTicketInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);

            Int64 customerID = (String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID));
            Int64 ticketID = (String.IsNullOrWhiteSpace(dalInput.TicketID) ? default(Int64) : Convert.ToInt64(dalInput.TicketID));


            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            //db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            //db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);
            db.AddInParameter(dbCommand, "@TicketID", DbType.Int64, ticketID);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable GetTrackInfo(ICustomerPortalAPI dalInput)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProTimeLineChartData";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 ticketID = (String.IsNullOrWhiteSpace(dalInput.TicketID) ? default(Int64) : Convert.ToInt64(dalInput.TicketID));


            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@ID_Master", DbType.Int64, ticketID);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable SaveSMSDraft(ICustomerPortalAPI dalInput)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "proCmnSmsDraftUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           
            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 MasterID = (String.IsNullOrWhiteSpace(dalInput.MasterID) ? default(Int64) : Convert.ToInt64(dalInput.MasterID));
            Decimal TransAmount = (String.IsNullOrWhiteSpace(dalInput.TransAmount) ? default(decimal) : Convert.ToDecimal(dalInput.TransAmount));
            Int64 TransID = (String.IsNullOrWhiteSpace(dalInput.TransID) ? default(Int64) : Convert.ToInt64(dalInput.TransID));
            Int32 Mode = (String.IsNullOrWhiteSpace(dalInput.Mode) ? default(Int32) : Convert.ToInt32(dalInput.Mode));
            Int32 Type = (String.IsNullOrWhiteSpace(dalInput.Type) ? default(Int32) : Convert.ToInt32(dalInput.Type));

            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, Mode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@JsonData", DbType.String, dalInput.JsonData);
            db.AddInParameter(dbCommand, "@Remarks", DbType.String, dalInput.Remarks);
            db.AddInParameter(dbCommand, "@MasterID", DbType.Int64, MasterID);
            db.AddInParameter(dbCommand, "@TransAmount", DbType.Decimal, TransAmount);
            db.AddInParameter(dbCommand, "@TransDate", DbType.String, dalInput.TransDate);
            db.AddInParameter(dbCommand, "@TransID", DbType.Int64, TransID);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, dalInput.TransType);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, Type);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        //test commit

        public DataTable GetProductEnqueryList(ICustomerPortalAPI dalInput)
        {

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalProductEnquiry";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            Int64 fk_Category = String.IsNullOrWhiteSpace(dalInput.FK_Category) ? default(Int64) : Convert.ToInt64(dalInput.FK_Category);
            Int64 fk_Company = String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company);
            Int64 fk_Branch = String.IsNullOrWhiteSpace(dalInput.FK_Branch) ? default(Int64) : Convert.ToInt64(dalInput.FK_Branch);
            Int64 fk_Product = String.IsNullOrWhiteSpace(dalInput.FK_Product) ? default(Int64) : Convert.ToInt64(dalInput.FK_Product);
            byte mode = String.IsNullOrWhiteSpace(dalInput.Mode) ? default(byte) : Convert.ToByte(dalInput.Mode);
            Int64 offer = String.IsNullOrWhiteSpace(dalInput.Offer) ? default(Int64) : Convert.ToInt64(dalInput.Offer);

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);
            Int64 customerID = String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID);
            Byte HasLeadOfferAmount = String.IsNullOrWhiteSpace(dalInput.SubMode) ? default(byte) : Convert.ToByte(dalInput.SubMode);


            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, fk_Category);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, fk_Branch);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, fk_Product);
            db.AddInParameter(dbCommand, "@Name", DbType.String, dalInput.Name);
            db.AddInParameter(dbCommand, "@Mode", DbType.Byte, mode);
            db.AddInParameter(dbCommand, "@Offer", DbType.Int64, offer);

            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);

            db.AddInParameter(dbCommand, "@Mobile", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@HasLeadOfferAmount", DbType.Byte, HasLeadOfferAmount);


            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable DalGetCustomerPortalSliderList(ICustomerPortalAPI dalInput)
        {

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetSliderList";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);



            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));
            byte mode = String.IsNullOrWhiteSpace(dalInput.Mode) ? default(byte) : Convert.ToByte(dalInput.Mode);
            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);
            Int64 customerID = String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID);


            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            db.AddInParameter(dbCommand, "@Mode", DbType.Byte, mode);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EnterBy);


            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);



            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        public DataTable DalGetCustomerWiseProductDetails(ICustomerPortalAPI dalInput)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetUserProductInfo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);

            Int64 customerID = (String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID));
            Int64 ticketID = (String.IsNullOrWhiteSpace(dalInput.TicketID) ? default(Int64) : Convert.ToInt64(dalInput.TicketID));


            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Int16, dalInput.CustomerRoleID);
            db.AddInParameter(dbCommand, "@CustomerID ", DbType.Int64, dalInput.CustomerID);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);

            //db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            //db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EntrBy);

            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalGetCompanyList(ICustomerPortalAPI dalInput)
        {
            
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "proAPICmnSearchPopup";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 FK_Category = (String.IsNullOrWhiteSpace(dalInput.FK_Category) ? default(Int64) : Convert.ToInt64(dalInput.FK_Category));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));
            Int32 Mode = (String.IsNullOrWhiteSpace(dalInput.Mode) ? default(Int32) : Convert.ToInt32(dalInput.Mode));

            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            //db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);
            db.AddInParameter(dbCommand, "@Criteria5", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, Mode);
            db.AddInParameter(dbCommand, "@Critrea3", DbType.String, dalInput.SubMode);
            //db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            //db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EntrBy);

            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public DataTable DalGetProductDetails(ICustomerPortalAPI dalInput)
        {
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + dalInput.BankKey);
            string sqlCommand = "ProPortalGetUserProductDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            Int64 fk_Company = (String.IsNullOrWhiteSpace(dalInput.FK_Company) ? default(Int64) : Convert.ToInt64(dalInput.FK_Company));
            Int64 fk_machine = (String.IsNullOrWhiteSpace(dalInput.FK_Machine) ? default(Int64) : Convert.ToInt64(dalInput.FK_Machine));

            Byte customerRole = String.IsNullOrWhiteSpace(dalInput.CustomerRole) ? default(byte) : Convert.ToByte(dalInput.CustomerRole);

            Int64 customerID = (String.IsNullOrWhiteSpace(dalInput.CustomerID) ? default(Int64) : Convert.ToInt64(dalInput.CustomerID));
            Int64 ticketID = (String.IsNullOrWhiteSpace(dalInput.TicketID) ? default(Int64) : Convert.ToInt64(dalInput.TicketID));
            Int64 ID_SalesProductDetails = (String.IsNullOrWhiteSpace(dalInput.ID_SalesProductDetails) ? default(Int64) : Convert.ToInt64(dalInput.ID_SalesProductDetails));


            db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Int64, dalInput.CustomerRoleID);
            db.AddInParameter(dbCommand, "@CustomerID ", DbType.Int64, dalInput.CustomerID);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, fk_Company);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, fk_machine);
            db.AddInParameter(dbCommand, "@ID_SalesProductDetails", DbType.Int64, dalInput.ID_SalesProductDetails);

            //db.AddInParameter(dbCommand, "@CustomerRoleID", DbType.Byte, customerRole);
            //db.AddInParameter(dbCommand, "@CustomerID", DbType.Int64, customerID);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, dalInput.TransMode);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, dalInput.EntrBy);

            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds.Tables.Count > 0)
                {
                    dtbl = ds.Tables[0];
                }
                else
                {
                    dtbl = default(DataTable);

                }

                return dtbl;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
