using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using PerfectWebERPAPI.Interface;

namespace PerfectWebERPAPI.Business
{
    public class BlStockTransferModel
    {
        #region CommonFunctions
        private static object locker = new Object();        
        public static void WriteToLog(String data, string BankKey)
        {
            //string path = @"C:\MscoreLog";
            string path = @"C:\ProdSuitAPIPILog\" + BankKey;
            string FilePath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
            lock (locker)
            {
                using (StreamWriter file = new StreamWriter(FilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString() + "::" + data);
                    file.Flush();
                    file.Close();
                }
            }
        }
        public static void CommonLog(string TransName, object obj, bool isrequest, string BankKey)
        {
            if (isrequest)
            {
                string ReqjsonParams = "Reqjsonparams::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||";
                WriteToLog(ReqjsonParams, BankKey);               
            }
            else
            {
                string ResponseParams = "Response::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||\r\n";
                WriteToLog(ResponseParams, BankKey);
            }
        }
        public static string xmlTostring<T>(List<T> root)
        {
           
            System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(root.GetType());
            System.IO.TextWriter writer2 = new System.IO.StringWriter();
            y.Serialize(writer2, root);

            return writer2.ToString();
        }
        #endregion
    }
    public class RootStockRTEmployeeDetails : CommonAPIResponse
    {
        public StockRTEmployeeDetails StockRTEmployeeDetails { get; set; }
    }
    public class RootStockRTProductDetails : CommonAPIResponse
    {
        public StockRTProductDetails StockRTProductDetails { get; set; }
    }
    public class RootUpdateStockTransfer : CommonAPIResponse
    {
        public UpdateStockTransfer UpdateStockTransfer { get; set; }
    }
    public class RootStockRequestTransfer : CommonAPIResponse
    {
        public StockRequest StockRequestList { get; set; }      
    }
    public class RootStockRequestProduct : CommonAPIResponse
    {
        public StockRequestProduct StockRequestProductList { get; set; }
    }
    public class RootStockRequestInTransfer : CommonAPIResponse
    {
        public StockRequestInTransfer StockRequestInTransfer { get; set; }
    }
    public class RootStockSTProductDetails : CommonAPIResponse
    {
        public StockSTProductDetails StockSTProductDetails { get; set; }
    }
    public class RootStockSTDeleteDetails : CommonAPIResponse
    {
        public StockRTDeleteDetails StockRTDeleteDetails { get; set; }
    }
}
