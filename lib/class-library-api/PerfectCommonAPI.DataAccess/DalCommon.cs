using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalCommon
    {
        public DataTable DalProERPCmnSearchPopup(IUserValidations IUserValidations)
        {
            string TransMode = "", Critrea3 = "", Critrea4 = "";
            Int64 PageIndex = 0, PageSize = 0, Pagemode = 0, FK_Company = 1, Critrea1 = 0, Critrea2 = 0, ID = 0, Criteria5 = 0, Criteria6 = 0;


            if (IUserValidations.TransMode != "")
                TransMode = IUserValidations.TransMode;
            if (IUserValidations.ReqMode != "")
                Pagemode = Convert.ToInt32(IUserValidations.ReqMode);
            if (IUserValidations.PageIndex != "")
                PageIndex = Convert.ToInt32(IUserValidations.PageIndex);
            if (IUserValidations.PageSize != "")
                PageSize = Convert.ToInt32(IUserValidations.PageSize);
            if (IUserValidations.FK_Company != "")
                FK_Company = Convert.ToInt32(IUserValidations.FK_Company);
            if (IUserValidations.Critrea1 != "")
                Critrea1 = Convert.ToInt32(IUserValidations.Critrea1);
            if (IUserValidations.Critrea2 != "")
                Critrea2 = Convert.ToInt32(IUserValidations.Critrea2);
            if (IUserValidations.Critrea3 != "")
                Critrea3 = IUserValidations.Critrea3;
            if (IUserValidations.Critrea4 != "")
                Critrea4 = IUserValidations.Critrea4;
            if (IUserValidations.ID != "")
                ID = Convert.ToInt32(IUserValidations.ID);
            if (IUserValidations.Critrea5 != "")
                Criteria5 = Convert.ToInt32(IUserValidations.Critrea5);
            if (IUserValidations.Critrea6 != "")
                Criteria6 = Convert.ToInt32(IUserValidations.Critrea6);

            DataTable dtbl = new DataTable();
            Database db = DatabaseFactory.CreateDatabase("PerfectERP" + IUserValidations.BankKey);
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
            db.AddInParameter(dbCommand, "@Name", DbType.String, IUserValidations.Name);
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
    }
}
