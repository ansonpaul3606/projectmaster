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
  public  class DalEMICollection
    {
        public DataTable DalCommonValidate(IEMICollection eMICollection)
        {

            Int32 ReqMode = 0, SubMode = 0, Demand=0;          
            Int64 FK_Company = 0, FK_BranchCodeUser = 0, FK_Employee = 0, FK_FinancePlanType=0, FK_Product=0, FK_Branch=0, fk_Customer=0, FK_Area=0, FK_Category=0;
            if (eMICollection.ReqMode != "")
                ReqMode = Convert.ToInt32(eMICollection.ReqMode);
            if (eMICollection.SubMode != "")
                SubMode = Convert.ToInt32(eMICollection.SubMode);
            if (eMICollection.FK_Company != "")
                FK_Company = Convert.ToInt32(eMICollection.FK_Company);
            if (eMICollection.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(eMICollection.FK_BranchCodeUser);
            if (eMICollection.FK_Employee != "")
                FK_Employee = Convert.ToInt64(eMICollection.FK_Employee);
            if (eMICollection.FK_FinancePlanType != "")
                FK_FinancePlanType = Convert.ToInt64(eMICollection.FK_FinancePlanType);
            if (eMICollection.FK_Product != "")
                FK_Product = Convert.ToInt64(eMICollection.FK_Product);
            if (eMICollection.FK_Branch != "")
                FK_Branch = Convert.ToInt64(eMICollection.FK_Branch);
            if (eMICollection.FK_Customer != "")
                fk_Customer = Convert.ToInt64(eMICollection.FK_Customer);
            if (eMICollection.FK_Area != "")
                FK_Area = Convert.ToInt64(eMICollection.FK_Area);
            if (eMICollection.FK_Category != "")
                FK_Category = Convert.ToInt64(eMICollection.FK_Category);
            if (eMICollection.Demand != "")
                Demand = Convert.ToInt32(eMICollection.Demand);
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + eMICollection.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, eMICollection.FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, eMICollection.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, eMICollection.ToDate);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, eMICollection.FromDate);
            db.AddInParameter(dbCommand, "@FK_FinancePlanType", DbType.Int64, FK_FinancePlanType);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, fk_Customer);
            db.AddInParameter(dbCommand, "@CedEMINo", DbType.String, eMICollection.CedEMINo);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@Demand", DbType.Int32, Demand);
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
        public DataSet DalCommonValidatewithdataset(IEMICollection eMICollection)
        {

            Int32 ReqMode = 0, SubMode = 0, Demand = 0;
            Int64 FK_Company = 0, FK_BranchCodeUser = 0, FK_Employee = 0, FK_FinancePlanType = 0, FK_Product = 0, FK_Branch = 0, fk_Customer = 0, FK_Area = 0, FK_Category = 0, ID_CustomerWiseEMI=0;
            Byte AccountMode = 0;
            if (eMICollection.ReqMode != "")
                ReqMode = Convert.ToInt32(eMICollection.ReqMode);
            if (eMICollection.SubMode != "")
                SubMode = Convert.ToInt32(eMICollection.SubMode);
            if (eMICollection.FK_Company != "")
                FK_Company = Convert.ToInt32(eMICollection.FK_Company);
            if (eMICollection.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt32(eMICollection.FK_BranchCodeUser);
            if (eMICollection.FK_Employee != "")
                FK_Employee = Convert.ToInt64(eMICollection.FK_Employee);
            if (eMICollection.FK_FinancePlanType != "")
                FK_FinancePlanType = Convert.ToInt64(eMICollection.FK_FinancePlanType);
            if (eMICollection.FK_Product != "")
                FK_Product = Convert.ToInt64(eMICollection.FK_Product);
            if (eMICollection.FK_Branch != "")
                FK_Branch = Convert.ToInt64(eMICollection.FK_Branch);
            if (eMICollection.FK_Customer != "")
                fk_Customer = Convert.ToInt64(eMICollection.FK_Customer);
            if (eMICollection.FK_Area != "")
                FK_Area = Convert.ToInt64(eMICollection.FK_Area);
            if (eMICollection.FK_Category != "")
                FK_Category = Convert.ToInt64(eMICollection.FK_Category);
            if (eMICollection.Demand != "")
                Demand = Convert.ToInt32(eMICollection.Demand);
            if (eMICollection.ID_CustomerWiseEMI != "")
                ID_CustomerWiseEMI = Convert.ToInt64(eMICollection.ID_CustomerWiseEMI);
            if (eMICollection.AccountMode != "")
                AccountMode = Convert.ToByte(eMICollection.AccountMode);
            DataSet dtbl = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + eMICollection.BankKey);
            string sqlCommand = "ProAPIProdSuitValidate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@Mode", DbType.Int32, ReqMode);
            db.AddInParameter(dbCommand, "@SubMode", DbType.Int32, SubMode);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.String, eMICollection.FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, eMICollection.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@ToDate", DbType.String, eMICollection.ToDate);
            db.AddInParameter(dbCommand, "@FromDate", DbType.String, eMICollection.FromDate);
            db.AddInParameter(dbCommand, "@FK_FinancePlanType", DbType.Int64, FK_FinancePlanType);
            db.AddInParameter(dbCommand, "@FK_Product", DbType.Int64, FK_Product);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_CustomerID", DbType.Int64, fk_Customer);
            db.AddInParameter(dbCommand, "@CedEMINo", DbType.String, eMICollection.CedEMINo);
            db.AddInParameter(dbCommand, "@FK_Area", DbType.Int64, FK_Area);
            db.AddInParameter(dbCommand, "@FK_Category", DbType.Int64, FK_Category);
            db.AddInParameter(dbCommand, "@Demand", DbType.Int32, Demand);
            db.AddInParameter(dbCommand, "@TransDate", DbType.String, eMICollection.TrnsDate);
            db.AddInParameter(dbCommand, "@ID_CustomerWiseEMI", DbType.Int64, ID_CustomerWiseEMI);
            db.AddInParameter(dbCommand, "@AccountMode", DbType.Byte, AccountMode);
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
        public DataTable DalCustomerTransactionUpdate(IEMICollection eMICollection)
        {


            Byte AccountMode = 0;
            Int64 FK_Company = 0, FK_BranchCodeUser = 0, FK_Employee = 0, FK_Master = 0;
            if (eMICollection.AccountMode != "")
                AccountMode = Convert.ToByte(eMICollection.AccountMode);
            if (eMICollection.ID_CustomerWiseEMI != "")
                FK_Master = Convert.ToInt64(eMICollection.ID_CustomerWiseEMI);         
            if (eMICollection.FK_Company != "")
                FK_Company = Convert.ToInt32(eMICollection.FK_Company);
            if (eMICollection.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(eMICollection.FK_BranchCodeUser);
            if (eMICollection.FK_Employee != "")
                FK_Employee = Convert.ToInt64(eMICollection.FK_Employee);            
            
            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + eMICollection.BankKey);
            string sqlCommand = "ProCustomerTransactionUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, 1);
            db.AddInParameter(dbCommand, "@Debug", DbType.Byte, 0);
            db.AddInParameter(dbCommand, "@ID_EMICollection", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@AccountMode", DbType.Byte, AccountMode);
            db.AddInParameter(dbCommand, "@FK_Master", DbType.Int64, FK_Master);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, "ACCT");
            db.AddInParameter(dbCommand, "@TransDate", DbType.String, eMICollection.TrnsDate);
            db.AddInParameter(dbCommand, "@CollectDate", DbType.String, eMICollection.CollectDate);
            db.AddInParameter(dbCommand, "@CusTrAmount", DbType.String, eMICollection.TotalAmount);
            db.AddInParameter(dbCommand, "@CusTrFineAmount", DbType.String, eMICollection.FineAmount);
            db.AddInParameter(dbCommand, "@TransType", DbType.String, "0");
            db.AddInParameter(dbCommand, "@CusTrFineWaiveAmount", DbType.String, "0");
            db.AddInParameter(dbCommand, "@NetAmount", DbType.String, eMICollection.NetAmount);
            db.AddInParameter(dbCommand, "@CusTrCollectedBy", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_PaymentMethod", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@EMIDetails", DbType.Xml, eMICollection.EMIDetails);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, eMICollection.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "@LastID", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PaymentDetail", DbType.Xml, eMICollection.PaymentDetail);
            db.AddInParameter(dbCommand, "@Channel", DbType.Int32, 1);
            db.AddInParameter(dbCommand, "@LocLatitude", DbType.String, eMICollection.LocLatitude);
            db.AddInParameter(dbCommand, "@LocLongitude", DbType.String, eMICollection.LocLongitude);
            db.AddInParameter(dbCommand, "@LocationAddress", DbType.String, eMICollection.Address);
            db.AddInParameter(dbCommand, "@LocationEnteredDate", DbType.String, eMICollection.LocationEnteredDate);
            db.AddInParameter(dbCommand, "@LocationEnteredTime", DbType.String, eMICollection.LocationEnteredTime);
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
