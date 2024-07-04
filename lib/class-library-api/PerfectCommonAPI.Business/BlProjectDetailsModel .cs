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
  public  class BlProjectDetailsModel
    {
        #region CommonFunctions
        // private static object locker = new Object();
        //// public static void CreateIamge(List<> data)
        // public static void WriteToLog(String data, string BankKey)
        // {
        //     //string path = @"C:\MscoreLog";
        //     string path = @"C:\ProdSuitAPIPILog\" + BankKey;
        //     string FilePath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        //     if (!Directory.Exists(path))
        //     {
        //         Directory.CreateDirectory(path);
        //     }
        //     if (!File.Exists(FilePath))
        //     {
        //         FileStream fs = File.Create(FilePath);
        //         fs.Close();
        //     }
        //     lock (locker)
        //     {
        //         using (StreamWriter file = new StreamWriter(FilePath, true))
        //         {
        //             file.WriteLine(DateTime.Now.ToString() + "::" + data);
        //             file.Flush();
        //             file.Close();
        //         }
        //     }
        // }
        // public static void CommonLog(string TransName, object obj, bool isrequest, string BankKey)
        // {
        //     if (isrequest)
        //     {
        //         string ReqjsonParams = "Reqjsonparams::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||";
        //         WriteToLog(ReqjsonParams, BankKey);
        //         //BlCustomer objBl = (BlCustomer)obj;
        //         // objBl.RequestMessage = BlFormats.EncryptDataForAuthCode(ReqjsonParams);
        //     }
        //     else
        //     {
        //         string ResponseParams = "Response::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||\r\n";
        //         WriteToLog(ResponseParams, BankKey);
        //     }
        // }
        // public static string xmlTostring<T>(List<T> root)
        // {
        //     //  root<T> root = new root<T>();
        //     // root.data = input;

        //     System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(root.GetType());
        //     System.IO.TextWriter writer2 = new System.IO.StringWriter();
        //     y.Serialize(writer2, root);

        //     return writer2.ToString();
        // }
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
   
    public class RootLeadList : CommonAPIResponse
    {
        public LeadList LeadList { get; set; }
    }
    public class RootProjectList : CommonAPIResponse
    {
        public ProjectList ProjectList { get; set; }
    }
    public class RootWorkTypeDetailsList : CommonAPIResponse
    {
        public WorkTypeList WorkTypeList { get; set; }
    }
    public class RootMeasurementTypeDetailsList : CommonAPIResponse
    {
        public MeasurementTypeList MeasurementTypeList { get; set; }
    }
    public class RootProjectStagesDetailsList : CommonAPIResponse
    {
        public ProjectStagesList ProjectStagesList { get; set; }
    }
    public class RootProjectTeamList : CommonAPIResponse
    {
        public ProjectTeamList ProjectTeamList { get; set; }
    }
    public class RootUnitList : CommonAPIResponse
    {
        public UnitList UnitList { get; set; }
    }
    public class RootEmployeeListforProject : CommonAPIResponse
    {
        public EmployeeListforProject EmployeeListforProject { get; set; }
    }
    public class RootModeList : CommonAPIResponse
    {
        public ModeList ModeList { get; set; }
    }


    public class RootUpdateMaterialUsage : CommonAPIResponse
    {
        public UpdateMaterialUsage UpdateMaterialUsage { get; set; }
    }
    public class RootUpdateProjectFollowUp : CommonAPIResponse
    {
        public UpdateProjectFollowUp UpdateProjectFollowUp { get; set; }
    }
    public class RootMatProductDetails: CommonAPIResponse
    {
        public MatProductDetails MatProductDetails { get; set; }
    }
    public class RootMaterialRequestProductList : CommonAPIResponse
    {
        public MaterialRequestProductList MaterialRequestProductList { get; set; }
    }
    
    public class RootUpdateMaterialRequest : CommonAPIResponse
    {
        public UpdateMaterialRequest UpdateMaterialRequest { get; set; }
    }
    public class RootDownloadImage : CommonAPIResponse
    {
        public DownloadImage DownloadImage { get; set; }
    }
    //public class RootUpdateProjectFollowUp : CommonAPIResponse
    //{
    //    public UpdateProjectFollowUp UpdateProjectFollowUp { get; set; }
    //}
    public class RootUpadateSiteVisit:CommonAPIResponse
    {
        public UpadateSiteVisit UpadateSiteVisit { get; set; }
    }
    public class RootProjectStatus : CommonAPIResponse
    {
        public ProjectStatus ProjectStatus { get; set; }
    }
    public class RootProjectOtherChargeDetails:CommonAPIResponse
    {
        public ProjectOtherChargeDetails ProjectOtherChargeDetails { get; set; }
    }
    public class RootOtherChargeTaxCalculationDetails : CommonAPIResponse
    {
        public OtherChargeTaxCalculationDetails OtherChargeTaxCalculationDetails { get; set; }
    }
    public class RootcheckDetails: CommonAPIResponse
    {
        public checkDetails checkDetails { get; set; }
    }
    public class RootUpdateProjectTransaction:CommonAPIResponse
    {
        public UpdateProjectTransaction UpdateProjectTransaction { get; set; }
    }
    public class RootProjectSiteVisitCount:CommonAPIResponse
    {
        public ProjectSiteVisitCount ProjectSiteVisitCount { get; set; }
    }
    public class RootProjectSiteVisitAssign : CommonAPIResponse
    {
        public ProjectSiteVisitAssign ProjectSiteVisitAssign { get; set; }
    }
    public class RootSiteVisitAssignViewDetails:CommonAPIResponse
    {
        public SiteVisitAssignViewDetails SiteVisitAssignViewDetails { get; set; }
    }
    public class RootProjectTransactionEmployeeDetails : CommonAPIResponse
    {
        public ProjectTransactionEmployeeDetails ProjectTransactionEmployeeDetails { get; set; }
    }
    public class RootTransactionTypeDetails : CommonAPIResponse
    {
        public TransactionTypeDetails TransactionTypeDetails { get; set; }
    }
    public class RootBillTypeDetails : CommonAPIResponse
    {
        public BillTypeDetails BillTypeDetails { get; set; }
    }
    public class RootPettyCashieDetails : CommonAPIResponse
    {
        public PettyCashieDetails PettyCashieDetails { get; set; }
    }
    public class RootPaymentInformation: CommonAPIResponse
    {
       public PaymentInformation PaymentInformation { get; set; }


    }
}
#endregion
        