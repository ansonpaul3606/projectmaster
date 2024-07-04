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
   public  class DalStockTransfer
    {        
        public DataTable DalProERPCmnSearchPopup(IStockTransfer IStockTransfer)
        {
            string TransMode = "", Critrea3="", Critrea4="";
            Int64 PageIndex=0, PageSize=0,Pagemode = 0, FK_Company = 1, Critrea1=0, Critrea2=0, ID=0, Criteria5=0, Criteria6=0;

            
            if (IStockTransfer.TransMode != "")
                TransMode = IStockTransfer.TransMode;
            if (IStockTransfer.ReqMode != "")
                Pagemode = Convert.ToInt32(IStockTransfer.ReqMode);
            if (IStockTransfer.PageIndex != "")
                PageIndex = Convert.ToInt32(IStockTransfer.PageIndex);
            if (IStockTransfer.PageSize != "")
                PageSize = Convert.ToInt32(IStockTransfer.PageSize);
            if (IStockTransfer.FK_Company != "")
                FK_Company = Convert.ToInt32(IStockTransfer.FK_Company);
            if (IStockTransfer.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IStockTransfer.Critrea1);
            if (IStockTransfer.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IStockTransfer.Critrea2);
            if (IStockTransfer.Critrea3 != "")
                Critrea3 = IStockTransfer.Critrea3;
            if (IStockTransfer.Critrea4 != "")
                Critrea4 = IStockTransfer.Critrea4;
            if (IStockTransfer.ID != "")
                ID = Convert.ToInt32(IStockTransfer.ID);
            if (IStockTransfer.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IStockTransfer.Critrea5);
            if (IStockTransfer.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IStockTransfer.Critrea6);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IStockTransfer.BankKey);
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
            db.AddInParameter(dbCommand, "@Name", DbType.String, IStockTransfer.Name);
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
        public DataTable DalStockTransferUpdate(IStockTransfer IStockTransfer)
        {
            Int64 ID_StockTransfer = 0, FK_Company=1;

            if (IStockTransfer.ID_StockTransfer != "")
                ID_StockTransfer = Convert.ToInt64(IStockTransfer.ID_StockTransfer);
            if (IStockTransfer.FK_Company != "")
                FK_Company = Convert.ToInt64(IStockTransfer.FK_Company);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IStockTransfer.BankKey);
            string sqlCommand = "ProStockTransferUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int64, Convert.ToInt64(IStockTransfer.UserAction));
            db.AddInParameter(dbCommand, "@Debug", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IStockTransfer.TransMode);
            db.AddInParameter(dbCommand, "@ID_StockTransfer", DbType.Int64, ID_StockTransfer);
            db.AddInParameter(dbCommand, "@FK_BranchFrom", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_BranchFrom));
            db.AddInParameter(dbCommand, "@FK_DepartmentFrom", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_DepartmentFrom));
            db.AddInParameter(dbCommand, "@FK_EmployeeFrom", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_EmployeeFrom));
            db.AddInParameter(dbCommand, "@FK_BranchTo", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_BranchTo));
            db.AddInParameter(dbCommand, "@FK_DepartmentTo", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_DepartmentTo));
            db.AddInParameter(dbCommand, "@FK_EmployeeTo", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_EmployeeTo));
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_EmployeeTo));        
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IStockTransfer.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@EmployeeStockTransferDetails", DbType.Xml, IStockTransfer.EmployeeStockTransferDetails);
            db.AddInParameter(dbCommand, "@TransDate", DbType.DateTime, IStockTransfer.TransDate);
            db.AddInParameter(dbCommand, "@LastID", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@STRequest", DbType.Int64, Convert.ToInt64(IStockTransfer.STRequest));
            db.AddInParameter(dbCommand, "@FK_StockRequest", DbType.Int64, Convert.ToInt64(IStockTransfer.FK_StockRequest));
         
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
        public DataTable DalStockTransferSelect(IStockTransfer IStockTransfer)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser=0, FK_StockTransfer=0, Detailed=0;
            if (IStockTransfer.FK_Company != "")
                FK_Company = Convert.ToInt64(IStockTransfer.FK_Company);
            if (IStockTransfer.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IStockTransfer.FK_BranchCodeUser);
            if (IStockTransfer.FK_StockTransfer != "")
                FK_StockTransfer = Convert.ToInt64(IStockTransfer.FK_StockTransfer);
            if (IStockTransfer.Detailed != "")
                Detailed = Convert.ToInt64(IStockTransfer.Detailed);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IStockTransfer.BankKey);
            string sqlCommand = "ProStockTransferSelect";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_StockTransfer", DbType.Int64, FK_StockTransfer);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IStockTransfer.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IStockTransfer.TransMode);
            db.AddInParameter(dbCommand, "@PageIndex", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@PageSize", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@Name", DbType.String, IStockTransfer.Name);
            db.AddInParameter(dbCommand, "@Detailed", DbType.Int64, Detailed);          
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
        public DataTable DalStockRequestSelectInTransfer(IStockTransfer IStockTransfer)
        {
            Int64 FK_Company = 1, FK_BranchCodeUser=0, DepartmentID=0, DepartmentIDTo=0, BranchID=0, BranchIDTo=0, EmployeeID=0, EmployeeIDTo=0;
            if (IStockTransfer.FK_Company != "")
                FK_Company = Convert.ToInt64(IStockTransfer.FK_Company);
            if (IStockTransfer.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IStockTransfer.FK_BranchCodeUser);
            if (IStockTransfer.FK_DepartmentFrom != "")
                DepartmentID = Convert.ToInt64(IStockTransfer.FK_DepartmentFrom);
            if (IStockTransfer.FK_DepartmentTo != "")
                DepartmentIDTo = Convert.ToInt64(IStockTransfer.FK_DepartmentTo);
            if (IStockTransfer.FK_BranchFrom != "")
                BranchID = Convert.ToInt64(IStockTransfer.FK_BranchFrom);
            if (IStockTransfer.FK_BranchTo != "")
                BranchIDTo = Convert.ToInt64(IStockTransfer.FK_BranchTo);
            if (IStockTransfer.FK_EmployeeFrom != "")
                EmployeeID = Convert.ToInt64(IStockTransfer.FK_EmployeeFrom);
            if (IStockTransfer.FK_EmployeeTo != "")
                EmployeeIDTo = Convert.ToInt64(IStockTransfer.FK_EmployeeTo);



            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IStockTransfer.BankKey);
            string sqlCommand = "ProFindStockTransferRequest";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@DepartmentID", DbType.Int64, DepartmentID);
            db.AddInParameter(dbCommand, "@DepartmentIDTo", DbType.Int64, DepartmentIDTo);
            db.AddInParameter(dbCommand, "@BranchID", DbType.Int64, BranchID);
            db.AddInParameter(dbCommand, "@BranchIDTo", DbType.Int64, BranchIDTo);
            db.AddInParameter(dbCommand, "@EmployeeID", DbType.Int64, EmployeeID);
            db.AddInParameter(dbCommand, "@EmployeeIDTo", DbType.Int64, EmployeeIDTo);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IStockTransfer.TransMode);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
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
        public DataTable DalStockTransferDelete(IStockTransfer IStockTransfer)
        {
            Int64 FK_Company = 1, Reason = 0, FK_StockTransfer = 0, FK_BranchCodeUser=0;
            if (IStockTransfer.FK_Company != "")
                FK_Company = Convert.ToInt64(IStockTransfer.FK_Company);
            if (IStockTransfer.Reason != "")
                Reason = Convert.ToInt64(IStockTransfer.Reason);
            if (IStockTransfer.FK_StockTransfer != "")
                FK_StockTransfer = Convert.ToInt64(IStockTransfer.FK_StockTransfer);
            if (IStockTransfer.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(IStockTransfer.FK_BranchCodeUser);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IStockTransfer.BankKey);
            string sqlCommand = "ProStockTransferDelete";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@TransMode", DbType.String, IStockTransfer.TransMode);
            db.AddInParameter(dbCommand, "@FK_StockTransfer", DbType.Int64, FK_StockTransfer);
            db.AddInParameter(dbCommand, "@Debug", DbType.Int64, 0);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, IStockTransfer.EntrBy);
            db.AddInParameter(dbCommand, "@FK_Reason", DbType.Int64, Reason);
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
    }
}
