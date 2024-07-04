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
    public class DalEmailSend
    {

        public DataTable GetEmailSettings2(GetEmailSettings ifunctions)
        {
            DataSet ds = new DataSet("dsTable");
            Database db = DatabaseFactory.CreateDatabase(ifunctions.DbName);
           
            string sqlCommand = ifunctions.QuerytoExecute;
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
               // UpdateErrorLog("GetQueryOutputAsDataTable-Success:DbName:=" + ifunctions.TableName + ":QuerytoExecute=" + ifunctions.QuerytoExecute + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, false);
              
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                //UpdateErrorLog("GetQueryOutputAsDataTable-Error:" + e.Message + ",DbName:=" + ifunctions.TableName + ":QuerytoExecute=" + ifunctions.QuerytoExecute + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, true);
                return ds.Tables[0];
              
            }
        }

        public class GetEmailSettings
        {
            public string DbName { get; set; }
            public string QuerytoExecute { get; set; }

        }

    }
}
