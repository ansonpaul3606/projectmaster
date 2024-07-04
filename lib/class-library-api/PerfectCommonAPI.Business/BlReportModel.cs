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
    public class BlReportModel
    {
        #region CommonFunctions
        private static object locker = new Object();
        // public static void CreateIamge(List<> data)
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
    //public class CommonAPIResponse
    //{
    //    public int StatusCode { get; set; }
    //    public string EXMessage { get; set; }
    //}
    public class RootProjectReportNameDetails : CommonAPIResponse
    {
        public ProjectReportNameDetails ProjectReportNameDetails { get; set; }
    }
    public class RootProjectReport : CommonAPIResponse
    {
        public ProjectReport ProjectReport { get; set; }
    }
    public class RootProjectReportDetail : CommonAPIResponse
    {
        public ProjectReportDetail ProjectReportDetail { get; set; }
    }
    public class RootServiceNewList : CommonAPIResponse
    {
        public ServiceNewList ServiceNewList { get; set; }
    }
    public class RootCategoryNameDetails : CommonAPIResponse
    {
        public CategoryNameDetails CategoryNameDetails { get; set; }
    }
    public class RootProjectListDetail : CommonAPIResponse
    {
        public ProjectListDetail ProjectListDetail { get; set; }
    }
    public class RootOutstanding : CommonAPIResponse
    {
        public OutStanding Outstanding { get; set; }
    }
    public class RootLeadDetailedReport : CommonAPIResponse
    {
        public LeadDetailedReport LeadDetailedReport { get; set; }
    }
    public class RootEmployeeWiseReport : CommonAPIResponse
    {
        public EmployeeWiseReport EmployeeWiseReport { get; set; }
    }
    public class RootAssignedWiseReport : CommonAPIResponse
    {
        public AssignedWiseReport AssignedWiseReport { get; set; }
    }
    public class RootCategoryWiseReport : CommonAPIResponse
    {
        public CategoryWiseReport CategoryWiseReport { get; set; }
    }
    public class RootProductWiseReport : CommonAPIResponse
    {
        public ProductWiseReport ProductWiseReport { get; set; }
    }
    public class RootGrouping : CommonAPIResponse
    {
        public Grouping Grouping { get; set; }
    }
    public class RootSummaryWiseReport : CommonAPIResponse
    {
        public SummaryWiseLeadReport SummaryWiseReport { get; set; }
    }
    public class RootComplaintService : CommonAPIResponse
    {
        public ComplaintService ComplaintService { get; set; }

    }
    public class RootTicketNo : CommonAPIResponse
    {
        public TicketNo TicketNo { get; set; }

    }
    public class RootServiceProfile : CommonAPIResponse
    {
        public ServiceProfile ServiceProfile { get; set; }

    }
    public class RootIntimationCategory : CommonAPIResponse
    {
        public IntimationCategory IntimationCategory { get; set; }

    }
    public class RootLeadSummaryDetailsReport:CommonAPIResponse
    {
        public LeadSummaryDetailsReport LeadSummaryDetailsReport { get; set; }
    }
    
}
#endregion