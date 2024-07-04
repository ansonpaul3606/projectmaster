using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PerfectWebERPAPI.Interface;
using System.Data.Common;
using System.Data;
using System.Collections;
using System.IO;

namespace PerfectWebERPAPI.DataAccess
{
    public class DalFunctions
    {


        public Boolean WriteLog(string strData, bool blnErrorOrService)
        {

            try
            {
                bool EnableLog = System.Configuration.ConfigurationManager.AppSettings["EnableLog"] == null ? true : Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableLog"].ToString());
                string strFolder = System.Configuration.ConfigurationManager.AppSettings["LogPath"] == null ? @"C:\PerfectERPAPILog" : System.Configuration.ConfigurationManager.AppSettings["LogPath"].ToString();
                if (EnableLog)
                {
                    string FileName;
                    string datestamp = DateTime.Now.ToString("yyMMdd");
                    // replace / with // for escape character handling
                    strFolder = strFolder.Replace("\\", "\\\\");
                    FileName = strFolder + "\\" + (blnErrorOrService == true ? "ScoreAgentError" + datestamp + ".Log" : "ScoreAgentService" + datestamp + ".Log");

                    // if file not exists create file
                    if (!Directory.Exists(strFolder))
                        Directory.CreateDirectory(strFolder);


                    // if file not exists create file
                    if (!File.Exists(FileName))
                    {
                        FileStream fstream = File.Create(FileName);
                        fstream.Close();
                    }

                    StreamWriter sWriter = new StreamWriter(FileName, true);
                    sWriter.WriteLine(DateTime.Now.ToString() + " - " + strData);
                    sWriter.Close();
                }
                return false;
            }
            catch (Exception)
            {
                UpdateErrorLog("Log Writing Failed", true);
                return false;
            }
        }
        private bool UpdateErrorLog(string strData, bool Error)
        {
            WriteLog(strData, Error);
            return true;
        }

        public DataSet GetProcedureOutputAsDataSet(IFunctions ifunctions)
        {
            DataSet ds = new DataSet("dsTable");
            Database db = DatabaseFactory.CreateDatabase(ifunctions.DbName);
            string sqlCommand = ifunctions.ObjectName;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            if (ifunctions.ObjectParameters != "" && ifunctions.ObjectArguments != "" && ifunctions.ObjectDataTypes != "")
            {
                string[] ObjectParametersArray = ifunctions.ObjectParameters.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectArgumentsArray = ifunctions.ObjectArguments.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectDataTypesArray = ifunctions.ObjectDataTypes.Split(ifunctions.ObjectSplitChar[0]);
                for (int i = 0; i < ObjectParametersArray.Length; i++)
                {
                    switch (ObjectDataTypesArray[i].ToLower())
                    {

                        case "boolean":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Boolean, Convert.ToBoolean(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "byte":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Byte, Convert.ToByte(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int16":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int16, Convert.ToInt16(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int32":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int32, Convert.ToInt32(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int64":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int64, Convert.ToInt64(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "decimal":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Decimal, Convert.ToDecimal(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "datetime":
                            //--edited by sonin to remove the null exception in date field  | conversion fail when "" is passed for convertion from string to datetime

                            DateTime? dateString = null;

                            if (ObjectArgumentsArray[i].ToString() != "")
                            {
                                dateString = Convert.ToDateTime(ObjectArgumentsArray[i].ToString());
                            }
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, dateString);
                            //db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, Convert.ToDateTime(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "xml":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Xml, ObjectArgumentsArray[i].ToString());
                            break;
                        case "double":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Double, Convert.ToDouble(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "single":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Single, Convert.ToSingle(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "binary":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, Convert.FromBase64String(ObjectArgumentsArray[i]));
                            break;
                        case "object":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Object, ObjectArgumentsArray[i]);
                            break;
                        default:
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.String, ObjectArgumentsArray[i].ToString());
                            break;

                    }
                }
            }
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                UpdateErrorLog("GetProcedureOutputAsDataTable-Success:DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, false);
                return ds;
            }
            catch (Exception e)
            {
                UpdateErrorLog("GetProcedureOutputAsDataTable-Error:" + e.Message + ",DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, true);
                return ds;
            }

        }

        public DataTable GetProcedureOutputAsDataTable(IFunctions ifunctions)
        {
            DataSet ds = new DataSet("dsTable");
            Database db = DatabaseFactory.CreateDatabase(ifunctions.DbName);
            string sqlCommand = ifunctions.ObjectName;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            if (ifunctions.ObjectParameters != "" && ifunctions.ObjectArguments != "" && ifunctions.ObjectDataTypes != "")
            {
                string[] ObjectParametersArray = ifunctions.ObjectParameters.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectArgumentsArray = ifunctions.ObjectArguments.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectDataTypesArray = ifunctions.ObjectDataTypes.Split(ifunctions.ObjectSplitChar[0]);
                for (int i = 0; i < ObjectParametersArray.Length; i++)
                {
                    switch (ObjectDataTypesArray[i].ToLower())
                    {

                        case "boolean":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Boolean, Convert.ToBoolean(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "byte":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Byte, Convert.ToByte(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int16":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int16, Convert.ToInt16(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int32":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int32, Convert.ToInt32(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int64":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int64, Convert.ToInt64(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "decimal":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Decimal, Convert.ToDecimal(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "datetime":
                            //--edited by sonin to remove the null exception in date field  | conversion fail when "" is passed for convertion from string to datetime

                            DateTime? dateString = null;

                            if (ObjectArgumentsArray[i].ToString() != "")
                            {
                                dateString = Convert.ToDateTime(ObjectArgumentsArray[i].ToString());
                            }
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, dateString);
                            //db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, Convert.ToDateTime(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "xml":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Xml, ObjectArgumentsArray[i].ToString());
                            break;
                        case "double":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Double, Convert.ToDouble(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "single":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Single, Convert.ToSingle(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "binary":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, Convert.FromBase64String(ObjectArgumentsArray[i]));
                            break;
                        case "object":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Object, ObjectArgumentsArray[i]);
                            break;
                        default:
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.String, ObjectArgumentsArray[i].ToString());
                            break;

                    }
                }
            }
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                UpdateErrorLog("GetProcedureOutputAsDataTable-Success:DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, false);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                UpdateErrorLog("GetProcedureOutputAsDataTable-Error:" + e.Message + ",DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, true);
                return ds.Tables[0];
            }

        }
        public string GetProcedureOutputAsScalarValue(IFunctions ifunctions)
        {
            Database db = DatabaseFactory.CreateDatabase(ifunctions.DbName);
            string sqlCommand = ifunctions.ObjectName;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            if (ifunctions.ObjectParameters != "" && ifunctions.ObjectArguments != "" && ifunctions.ObjectDataTypes != "")
            {
                string[] ObjectParametersArray = ifunctions.ObjectParameters.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectArgumentsArray = ifunctions.ObjectArguments.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectDataTypesArray = ifunctions.ObjectDataTypes.Split(ifunctions.ObjectSplitChar[0]);

                for (int i = 0; i < ObjectParametersArray.Length; i++)
                {
                    switch (ObjectDataTypesArray[i].ToLower())
                    {

                        case "boolean":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Boolean, Convert.ToBoolean(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "byte":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Byte, Convert.ToByte(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int16":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int16, Convert.ToInt16(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int32":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int32, Convert.ToInt32(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int64":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int64, Convert.ToInt64(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "decimal":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Decimal, Convert.ToDecimal(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "datetime":
                            //--edited by sonin to remove the null exception in date field  | conversion fail when "" is passed for convertion from string to datetime
                            //string test = (ObjectArgumentsArray[i].ToString() == "") ? null: ObjectArgumentsArray[i].ToString();

                            DateTime? dateString = null;

                            if (ObjectArgumentsArray[i].ToString() != "")
                            {
                                dateString = Convert.ToDateTime(ObjectArgumentsArray[i].ToString());
                            }
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, dateString);
                            //db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, Convert.ToDateTime(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "xml":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Xml, ObjectArgumentsArray[i].ToString());
                            break;
                        case "double":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Double, Convert.ToDouble(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "single":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Single, Convert.ToSingle(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "binary":
                            if (ifunctions.ObjectName == "ProCmnUpdateImages")
                            {
                                if (ObjectParametersArray[i].ToString() == "@dataPhoto")
                                    db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, ifunctions.ObjectPhoto);
                                else
                                    if (ObjectParametersArray[i].ToString() == "@dataSign")
                                    db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, ifunctions.ObjectSign);
                            }
                            else
                                db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, Convert.FromBase64String(ObjectArgumentsArray[i]));

                            break;
                        case "object":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Object, ObjectArgumentsArray[i]);
                            break;
                        default:
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.String, ObjectArgumentsArray[i].ToString());
                            break;

                    }
                }
            }
            try
            {

                string retunVal = db.ExecuteScalar(dbCommand).ToString();
                UpdateErrorLog("GetProcedureOutputAsScalarValue-Success:DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, false);
                return retunVal;
            }
            catch (Exception e)
            {
                UpdateErrorLog("GetProcedureOutputAsScalarValue-:" + e.Message + ",DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, true);
                return "-1";
            }
        }
        public bool GetProcedureOutputAsNonQuery(IFunctions ifunctions)
        {
            Database db = DatabaseFactory.CreateDatabase(ifunctions.DbName);
            string sqlCommand = ifunctions.ObjectName;
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            if (ifunctions.ObjectParameters != "" && ifunctions.ObjectArguments != "" && ifunctions.ObjectDataTypes != "")
            {
                string[] ObjectParametersArray = ifunctions.ObjectParameters.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectArgumentsArray = ifunctions.ObjectArguments.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectDataTypesArray = ifunctions.ObjectDataTypes.Split(ifunctions.ObjectSplitChar[0]);

                for (int i = 0; i < ObjectParametersArray.Length; i++)
                {
                    switch (ObjectDataTypesArray[i].ToLower())
                    {

                        case "boolean":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Boolean, Convert.ToBoolean(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "byte":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Byte, Convert.ToByte(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int16":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int16, Convert.ToInt16(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int32":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int32, Convert.ToInt32(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int64":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int64, Convert.ToInt64(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "decimal":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Decimal, Convert.ToDecimal(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "datetime":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, Convert.ToDateTime(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "xml":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Xml, ObjectArgumentsArray[i].ToString());
                            break;
                        case "double":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Double, Convert.ToDouble(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "single":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Single, Convert.ToSingle(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "binary":
                            if (ifunctions.ObjectName == "ProCmnUpdateImages")
                            {
                                if (ObjectParametersArray[i].ToString() == "@dataPhoto")
                                    db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, ifunctions.ObjectPhoto);
                                else
                                    if (ObjectParametersArray[i].ToString() == "@dataSign")
                                    db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, ifunctions.ObjectSign);
                            }
                            else
                                db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, Convert.FromBase64String(ObjectArgumentsArray[i]));

                            break;
                        case "object":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Object, ObjectArgumentsArray[i]);
                            break;
                        default:
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.String, ObjectArgumentsArray[i].ToString());
                            break;

                    }
                }
            }
            try
            {

                int retunVal = db.ExecuteNonQuery(dbCommand);
                UpdateErrorLog("GetProcedureOutputAsNonQuery-Success:DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, false);
                return Convert.ToInt32(retunVal) > 0 ? true : false;
            }
            catch (Exception e)
            {
                UpdateErrorLog("GetProcedureOutputAsNonQuery-:" + e.Message + ",DbName:=" + ifunctions.TableName + ":ObjectName=" + ifunctions.ObjectName + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, true);
                return false;
            }
        }
        public DataTable GetQueryOutputAsDataTable(IFunctions ifunctions)
        {
            DataSet ds = new DataSet("dsTable");
            Database db = DatabaseFactory.CreateDatabase(ifunctions.DbName);
            string sqlCommand = ifunctions.QuerytoExecute;
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
            if (ifunctions.ObjectParameters != "" && ifunctions.ObjectArguments != "" && ifunctions.ObjectDataTypes != "")
            {
                string[] ObjectParametersArray = ifunctions.ObjectParameters.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectArgumentsArray = ifunctions.ObjectArguments.Split(ifunctions.ObjectSplitChar[0]);
                string[] ObjectDataTypesArray = ifunctions.ObjectDataTypes.Split(ifunctions.ObjectSplitChar[0]);

                for (int i = 0; i < ObjectParametersArray.Length; i++)
                {
                    switch (ObjectDataTypesArray[i].ToLower())
                    {

                        case "boolean":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Boolean, Convert.ToBoolean(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "byte":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Byte, Convert.ToByte(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int16":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int16, Convert.ToInt16(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int32":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int32, Convert.ToInt32(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "int64":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Int64, Convert.ToInt64(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "decimal":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Decimal, Convert.ToDecimal(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "datetime":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.DateTime, Convert.ToDateTime(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "xml":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Xml, ObjectArgumentsArray[i].ToString());
                            break;
                        case "double":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Double, Convert.ToDouble(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "single":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Single, Convert.ToSingle(ObjectArgumentsArray[i].ToString()));
                            break;
                        case "binary":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Binary, Convert.FromBase64String(ObjectArgumentsArray[i]));
                            break;
                        case "object":
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.Object, ObjectArgumentsArray[i]);
                            break;
                        default:
                            db.AddInParameter(dbCommand, ObjectParametersArray[i].ToString(), DbType.String, ObjectArgumentsArray[i].ToString());
                            break;

                    }
                }
            }
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                UpdateErrorLog("GetQueryOutputAsDataTable-Success:DbName:=" + ifunctions.TableName + ":QuerytoExecute=" + ifunctions.QuerytoExecute + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, false);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                UpdateErrorLog("GetQueryOutputAsDataTable-Error:" + e.Message + ",DbName:=" + ifunctions.TableName + ":QuerytoExecute=" + ifunctions.QuerytoExecute + ",ObjectParameters:" + ifunctions.ObjectParameters + ",ObjectArguments:" + ifunctions.ObjectArguments + ",ObjectDataTypes:" + ifunctions.ObjectDataTypes, true);
                return ds.Tables[0];
            }
        }
        public string GetReturnMessage(string Returnvalue, string DbName)
        {
            Database db = DatabaseFactory.CreateDatabase(DbName);

            string sqlCommand = "proERPGetErrorMessage";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "@ErrorCode", DbType.String, Returnvalue);
            string ReturnMessage = db.ExecuteScalar(dbCommand).ToString();
            return ReturnMessage;
        }





    }//Class End Here
}//Name Space End Here
