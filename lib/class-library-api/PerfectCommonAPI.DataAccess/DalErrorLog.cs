using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalErrorLog
    {
        private Int16 _Action;
        private string _DbName;
        private string _Module;
        private string _ErrMessage;
        private string _ErrDetails;
        private Int32 _FK_Machine;
        private string _UserCode;
        private Int16 _BranchCodeUser;
       
        public DalErrorLog()
        {
            Initialize();
        }

        public void Initialize()
        {
            _Action = 0;
            _DbName = string.Empty;
            _Module = string.Empty;
            _ErrMessage = string.Empty;
            _FK_Machine = 0;
            _UserCode = string.Empty;
            _BranchCodeUser = 0;
            _ErrDetails = string.Empty;
        }
               
        public Int16 Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
       
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
        }
        public string ErrMessage
        {
            get { return _ErrMessage; }
            set { _ErrMessage = value; }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }
    
        public string ErrDetails
        {
            get { return _ErrDetails; }
            set { _ErrDetails = value; }
        }
       
        public Int32 MachineID
        {
            get { return _FK_Machine; }
            set { _FK_Machine = value; }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }
        public Int16 BranchCodeUser
        {
            get { return _BranchCodeUser; }
            set { _BranchCodeUser = value; }
        }

       
        public void UpdateErrorLog()
        {

            Database db = DatabaseFactory.CreateDatabase("Audit");
            string sqlCommand = "proErrorLogUpdate";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@FK_Machine", DbType.Int32, _FK_Machine);
            db.AddInParameter(dbCommand, "@UserAction", DbType.Int16, _Action);
            db.AddInParameter(dbCommand, "@Module", DbType.String, _Module);
            db.AddInParameter(dbCommand, "@ErrDbName", DbType.String, _DbName);
            db.AddInParameter(dbCommand, "@ErrDetails", DbType.String, _ErrDetails);
            db.AddInParameter(dbCommand, "@ErrMessage", DbType.String, _ErrMessage);
             db.AddInParameter(dbCommand, "@BranchCodeUser", DbType.Int16, _BranchCodeUser);
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, _UserCode);
    
            try
            {
                db.ExecuteScalar(dbCommand);
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}


