using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERPAPI.Interface;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using System.Data.Sql;

namespace PerfectWebERPAPI.Business
{
    public class BlPickUpDeliveryModel
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
                //BlCustomer objBl = (BlCustomer)obj;
                // objBl.RequestMessage = BlFormats.EncryptDataForAuthCode(ReqjsonParams);
            }
            else
            {
                string ResponseParams = "Response::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||\r\n";
                WriteToLog(ResponseParams, BankKey);
            }
        }
        public static string xmlTostring<T>(List<T> root)
        {
            //  root<T> root = new root<T>();
            // root.data = input;

            System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(root.GetType());
            System.IO.TextWriter writer2 = new System.IO.StringWriter();
            y.Serialize(writer2, root);

            return writer2.ToString();
        }

        #endregion
    }
    #region Api ReponseModel
    public class RootPickupandDeliveryCount : CommonAPIResponse
    {
        public PickupandDeliveryCount PickupandDeliveryCount { get; set; }
    }
    public class RootPickUpDeliveryDetails : CommonAPIResponse
    {
        public PickUpDeliveryDetails PickUpDeliveryDetails { get; set; }
    }
    public class RootUpdateDeliverStatusDetails : CommonAPIResponse
    {
        public UpdateDeliverStatusDetails UpdateDeliverStatusDetails { get; set; }
    }
    public class RootPickUPProductInformationDetails:CommonAPIResponse
    {
        public PickUPProductInformationDetails PickUPProductInformationDetails { get; set; }
    }
    public class RootStandByProductDetails : CommonAPIResponse
    {
        public StandByProductDetails StandByProductDetails { get; set; }
    }
    public class RootUpdatePickUpAndDelivery : CommonAPIResponse
    {
        public UpdatePickUpAndDelivery UpdatePickUpAndDelivery { get; set; }
    }
    public class RootBillType:CommonAPIResponse
    {
        public BillType BillType { get; set; }
    }
    public class RootDeliveryProductInformationDetails : CommonAPIResponse
    {
        public DeliveryProductInformationDetails DeliveryProductInformationDetails { get; set; }
    }
    public class RootProductComplaintList : CommonAPIResponse
    {
        public ProductComplaintList ProductComplaintsList { get; set; }
    }
    public class RootPickUpEMIRecoveryDetails:CommonAPIResponse
    {
        public PickUpEMIRecoveryDetails PickUpEMIRecoveryDetails { get; set; }
    }
    #endregion
}
