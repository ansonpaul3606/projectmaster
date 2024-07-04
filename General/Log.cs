using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PerfectWebERP.General
{
    public class Log
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
                    FileName = strFolder + "\\" + (blnErrorOrService == true ? "ERP" + datestamp + ".Log" : "ERP" + datestamp + ".Log");

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
    }
}