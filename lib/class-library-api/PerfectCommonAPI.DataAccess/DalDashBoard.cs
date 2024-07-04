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
  public  class DalDashBoard
    {
        public DataTable DalDashBoardDetails(IDashBoard idashBoard)
        {

            Int32 DashType = 0, DashMode = 0;
            Int64 FK_Employee = 0, FK_Department = 0, FK_Branch = 0, FK_Company = 0, FK_BranchCodeUser = 0;
            if (idashBoard.DashMode != "")
                DashMode = Convert.ToInt32(idashBoard.DashMode);
            if (idashBoard.DashType != "")
                DashType = Convert.ToInt32(idashBoard.DashType);

            if (idashBoard.FK_Employee != "")
                FK_Employee = Convert.ToInt64(idashBoard.FK_Employee);
            if (idashBoard.FK_Department != "")
                FK_Department = Convert.ToInt64(idashBoard.FK_Department);
            if (idashBoard.FK_Branch != "")
                FK_Branch = Convert.ToInt64(idashBoard.FK_Branch);
            if (idashBoard.FK_Company != "")
                FK_Company = Convert.ToInt64(idashBoard.FK_Company);
            if (idashBoard.FK_BranchCodeUser != "")
                FK_BranchCodeUser = Convert.ToInt64(idashBoard.FK_BranchCodeUser);


            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + idashBoard.BankKey);
            string sqlCommand = "ProAPIERPDashBoardDetails";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Employee", DbType.Int64, FK_Employee);
            db.AddInParameter(dbCommand, "@FK_Department", DbType.Int64, FK_Department);
            db.AddInParameter(dbCommand, "@FK_Branch", DbType.Int64, FK_Branch);
            db.AddInParameter(dbCommand, "@FK_Company", DbType.Int64, FK_Company);
            db.AddInParameter(dbCommand, "@FK_BranchCodeUser", DbType.Int64, FK_BranchCodeUser);
            db.AddInParameter(dbCommand, "@EntrBy", DbType.String, idashBoard.EntrBy);
            db.AddInParameter(dbCommand, "@AsOnDate", DbType.String, idashBoard.TransDate);
            db.AddInParameter(dbCommand, "@DashMode", DbType.Int32, DashMode);
            db.AddInParameter(dbCommand, "@DashType", DbType.Int32, DashType);
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
