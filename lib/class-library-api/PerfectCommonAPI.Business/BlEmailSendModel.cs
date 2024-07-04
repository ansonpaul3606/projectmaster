using PerfectWebERPAPI.DataAccess;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static PerfectWebERPAPI.DataAccess.DalEmailSend;

namespace PerfectWebERPAPI.Business
{
    public class BlEmailSendModel
    {
        private string _TableName;
        private string _DbName;
        private string _Module;
        private Int32 _FkCompany;
        private Int16 _BranchCodeUser;
        private string _UserCode;
        private string _ObjectName;
        private string _ObjectParameters;
        private string _ObjectArguments;
        private string _ObjectDataTypes;
        private string _QuerytoExecute;
        private string _ObjectSplitChar;
        private byte[] _ObjectPhoto;
        private byte[] _ObjectSign;
        private string _Body;
        private string _Subject;
        private string _FilePath;
        private string[] _RecipientEmail;
        private string _senderName;
        private string _DisplayName;

        public BlEmailSendModel()
        {
            Initialize();
        }
        public void Initialize()
        {
            _TableName = String.Empty;
            _DbName = String.Empty;
            _Module = String.Empty;
            _FkCompany = 0;
            _BranchCodeUser = 0;
            _UserCode = String.Empty;
            _ObjectName = String.Empty;
            _ObjectParameters = String.Empty;
            _ObjectArguments = String.Empty;
            _ObjectDataTypes = String.Empty;
            _QuerytoExecute = String.Empty;
            _Body = String.Empty;
            _FilePath = String.Empty;
            _Subject = String.Empty;
            _senderName = String.Empty;
            _ObjectSplitChar = ",";
            _ObjectPhoto = null;
            _ObjectSign = null;
            _RecipientEmail = null;
            _DisplayName= String.Empty;
        }


        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
            // set { _TableName = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
            // set { _DbName = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string Module
        {
            get { return _Module; }
            set { _Module = value; }
            //set { _Module = BlFormats.DecryptDataForAuthCode(value); }
        }
        public Int32 FK_Company
        {
            get { return _FkCompany; }
            set { _FkCompany = Convert.ToInt16(value.ToString()); }
            //set { _FkCompany = Convert.ToInt16(BlFormats.DecryptDataForAuthCode(value.ToString())); }
        }

        public Int16 BranchCodeUser
        {
            get { return _BranchCodeUser; }
            set { _BranchCodeUser = Convert.ToInt16((value.ToString())); }
            //set { _BranchCodeUser = Convert.ToInt16(BlFormats.DecryptDataForAuthCode(value.ToString())); }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
            // set { _UserCode = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string ObjectName
        {
            get { return _ObjectName; }
            set { _ObjectName = value; }
            // set { _ObjectName = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string ObjectParameters
        {
            get { return _ObjectParameters; }
            set { _ObjectParameters = (value == null ? "" : value); }
            // set { _ObjectParameters = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string ObjectArguments
        {
            get { return _ObjectArguments; }
            set { _ObjectArguments = (value == null ? "" : value); }
            //  set { _ObjectArguments = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string ObjectDataTypes
        {
            get { return _ObjectDataTypes; }
            set { _ObjectDataTypes = (value == null ? "" : value); }
            // set { _ObjectDataTypes = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string QuerytoExecute
        {
            get { return _QuerytoExecute; }
            set { _QuerytoExecute = (value == null ? "" : value); }
            //set { _QuerytoExecute = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string ObjectSplitChar
        {
            get { return _ObjectSplitChar; }
            set { _ObjectSplitChar = (value == null ? "," : value); }
            //set { _ObjectSplitChar = BlFormats.DecryptDataForAuthCode(value == null ? "," : value); }
        }
        public byte[] ObjectPhoto
        {
            get { return _ObjectPhoto; }
            set { _ObjectPhoto = value; }
        }
        public byte[] ObjectSign
        {
            get { return _ObjectSign; }
            set { _ObjectSign = value; }
        }
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = (value == null ? "" : value); }
            // set { _Subject = BlFormats.DecryptDataForAuthCode(value == null ? "," : value); }
        }
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = (value == null ? "" : value); }
            // set { _FilePath = BlFormats.DecryptDataForAuthCode(value == null ? "," : value); }
        }
        public string Body
        {
            get { return _Body; }
            set { _Body = (value == null ? "" : value); }
            //set { _Body = BlFormats.DecryptDataForAuthCode(value == null ? "," : value); }
        }
        public string[] RecipientEmail
        {

           get {return _RecipientEmail ;}
            set { _RecipientEmail = value; }
         }
        public string senderName
        {
            get { return _senderName; }
            set { _senderName = (value == null ? "" : value); }
            //set { _Body = BlFormats.DecryptDataForAuthCode(value == null ? "," : value); }
        }
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
            // set { _DisplayName = BlFormats.DecryptDataForAuthCode(value); }
        }


        public CommonAPIRes SendNewslettrs(BlEmailSendModel objBl)
        {

            try
            {
                string emailTemplate;
                if (System.IO.File.Exists(objBl.FilePath)){
                     emailTemplate = System.IO.File.ReadAllText(objBl.FilePath);
                    string cssPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailHTML", "stylesheets", "email.css");
                    emailTemplate = emailTemplate.Replace("%{#{CssPath}#}%", cssPath);
                }
                else
                {
                    throw new Exception("File does not exist");
                }
                   

                foreach (string item in objBl.RecipientEmail)
                {
                    //Console.WriteLine(item);

                    //try
                    //{

                    //    var data = SendEmail2(item, objBl.Subject, objBl.Body, objBl.FilePath, objBl.DisplayName);
                    //    if (!data)
                    //    {
                    //        throw new Exception("Failed");
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    // Handle the exception for the specific recipient, but continue to the next recipient
                    //    Console.WriteLine("Error sending email to " + item + ": " + ex.Message);
                    //}

                    
                   


                    var data = SendEmail2(item, objBl.Subject, emailTemplate, objBl.FilePath, objBl.DisplayName, objBl.DbName);
                    if (!data)
                    {
                        throw new Exception("Failed");
                    }
                }
                return new CommonAPIRes { ResponseCode = "0", ResponseMessage = "Success" }; 
            }
            catch (Exception ex)
            {
                return new CommonAPIRes { ResponseCode = "-1", ResponseMessage = "Failed" };
            }
        }
        public bool ModifyHtml (BlEmailSendModel objBl)
        {

            DalEmailSend objDal = new DalEmailSend();
     
            string recipientEmail = "example@gmail.com";
            string subject = "Test Email";
            string body = "This is a test email sent from the .NET controller.";
            string firstName = "John";
            string lastName = "Doe";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailAttachments", "invoiceredco1.html");

            //
            string emailTemplate = System.IO.File.ReadAllText(filePath);

            // Replace placeholders in the template with dynamic values
            emailTemplate = emailTemplate.Replace("%{#{OperatorName}#}%", firstName + " " + lastName);
            // emailTemplate = emailTemplate.Replace("%{#{Name}#}%", "gazeeb" + " " + "panali");

            // Define the path to the CSS file
           // string cssPath = Server.MapPath("~/EmailHTML/stylesheets/email.css");
            string cssPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EmailHTML", "stylesheets", "email.css");


            // Replace the placeholder for CSS path with the actual path
            emailTemplate = emailTemplate.Replace("%{#{CssPath}#}%", cssPath);




            ////
            SendEmail2(recipientEmail, subject, body, emailTemplate, objBl.senderName,DbName);

            // return BlUserValidationsFormats.ConvertResellerDetails(dt);
            return true;
        }

        public CommonAPIRes EmailWithAttachment(string[] recipientEmail, string subject, string body, string attachmentFilePath,string DisplayName,string DbName)
        {

            try
            {

                var EmailSettingsData = GetEmailSettings(DbName);
                DalEmailSend dalfunctions = new DalEmailSend();
 
               // SmtpClient smtpClient = new SmtpClient("smtp.zoho.in", 587);
                SmtpClient smtpClient = new SmtpClient(EmailSettingsData[0].SMTP, EmailSettingsData[0].SMTPPortNo);
                smtpClient.Credentials = new NetworkCredential(EmailSettingsData[0].FromAddress, EmailSettingsData[0].FromPassword);
               
 
                smtpClient.EnableSsl = true;
                MailMessage mailMessage;
                foreach (string data in recipientEmail)
                {

                    //try
                    //{
                             

                        mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(EmailSettingsData[0].FromAddress, DisplayName);
                        mailMessage.To.Add(new MailAddress(data));
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;


                        if (System.IO.File.Exists(attachmentFilePath))
                            {
                                Console.WriteLine("File exists");
                                Attachment attachment = new Attachment(attachmentFilePath);
                                mailMessage.Attachments.Add(attachment);
                        }
                        else
                        {
                            throw new Exception("File does not exist");
                        }

                        // Send the email
                        smtpClient.Send(mailMessage);
                    //}
                    //catch (Exception ex)
                    //{
                    //    // Handle the exception for the specific recipient, but continue to the next recipient
                    //    Console.WriteLine("Error sending email to " + data + ": " + ex.Message);
                    //}

                };
                
                return new CommonAPIRes
                {
                    ResponseCode = "0",
                    ResponseMessage="Success"
                };
            }
            catch (Exception ex)
            {
                return new CommonAPIRes
                {
                    ResponseCode = "-1",
                    ResponseMessage = ex.Message
                };

            }
          
        }
        bool SendEmail2(string EmailId, string Subject, string Body, string FilePath, string senderName,string DbName)
        {

            var data = GetEmailSettings(DbName);
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = Convert.ToBoolean(true);
            client.Host = data[0].SMTP;
            client.Port = data[0].SMTPPortNo;
            NetworkCredential credentials = new NetworkCredential();
            client.UseDefaultCredentials = false;
            credentials.UserName = data[0].FromAddress;
            credentials.Password = data[0].FromPassword;
            client.Credentials = credentials;
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(data[0].FromAddress,
            senderName);
          
            // mailMessage.CC.Add(new MailAddress("contactus@offsetnow.com"));

            mailMessage.To.Add(new MailAddress(EmailId));
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;

            

            string Body1 = Body;

            mailMessage.Body = Body1;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        

        public HtmlconvertionBlOutput Htmlconvertion(ConverthtmlintoPdfinput inputdata)
        {

           
            try
            {
                HtmlToPdf converter = new HtmlToPdf();

                //get html file path
                //var html = File.ReadAllText("D:\\NonBanking\\PerfectWebERP\\PerfectWebERPAPI\\EmailAttachments\\index.html");
                var html = File.ReadAllText(inputdata.source_path);

                // convert the url to pdf
                // PdfDocument doc = converter.ConvertHtmlString(html);

                // set converter options
                //converter.Options.PdfPageSize = pageSize;
                //converter.Options.PdfPageOrientation = pdfOrientation;
                //converter.Options.WebPageWidth = webPageWidth;
                //converter.Options.WebPageHeight = webPageHeight;




                PdfDocument doc = converter.ConvertHtmlString(html);
                string uniqueFileName;
                if (System.IO.Directory.Exists(inputdata.save_path))
                {
                    DateTime now = DateTime.Now;
                    string timestamp = now.ToString("yyyyMMddHHmmss");
                     uniqueFileName = $"{inputdata.save_path}" + "GeneratedPdf" + $"{ timestamp}" +".pdf";
                   
                }
                else
                {
                    Console.WriteLine("Directory does not exist");
                    throw new Exception("Directory does not exist");
                }
;
                doc.Save(uniqueFileName);
                doc.Close();
                HtmlconvertionBlOutput data = new HtmlconvertionBlOutput
                {
                    status = true,
                    save_path = uniqueFileName,
                    Message ="Success"
                };

                return data;


                //// return resulted pdf document
                //FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                //fileResult.FileDownloadName = "Document.pdf";
                //return fileResult;
            }
            catch (Exception ex)
            {
                HtmlconvertionBlOutput data = new HtmlconvertionBlOutput
                {
                    status = false,
                    save_path = null,
                    Message=ex.Message
                };
                return data;
            }
           


           
        }
        public List<EmailSettings> GetEmailSettings(string DbName)
        {
           
            DalEmailSend dalfunctions = new DalEmailSend();
            GetEmailSettings obj = new GetEmailSettings
            {
                DbName = DbName,
                QuerytoExecute = "select * from EmailSettings where IsActive=1"

            };
            DataTable data = dalfunctions.GetEmailSettings2(obj);
            var output = ConvertEmailSettings(data);
            return output;
        }
        
        public static List<EmailSettings> ConvertEmailSettings(DataTable dt)
        {
            List<EmailSettings> lst = new List<EmailSettings>();
            foreach (DataRow dr in dt.Rows)
            {
                EmailSettings obj = new EmailSettings();
                obj.IsActive = Convert.ToInt32(dr["IsActive"]);
                obj.SMTP = Convert.ToString(dr["SMTP"]);
                obj.SMTPPortNo = Convert.ToInt32(dr["SMTPPortNo"]);
                obj.SSL = Convert.ToBoolean(dr["SSL"]);
                obj.FromAddress = Convert.ToString(dr["FromAddress"]);
                obj.FromPassword = Convert.ToString(dr["FromPassword"]);
                lst.Add(obj);
            }
            return lst;
        }

    }

    public class EmailSettings
    {
        public Int64 IsActive { get; set; }
        public string FromAddress { get; set; }
        public string FromPassword { get; set; }
        public string SMTP { get; set; }
        public int SMTPPortNo { get; set; }
        public bool SSL { get; set; }
    }
    public class RootData : CommonAPIResponse
    {
        public RootDataDetails RootDataDetails { get; set; }
    }
    public class RootDataDetails : CommonAPIRes
    {
        public string Data { get; set; }
    }
    public class CommonAPIRes
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class ConverthtmlintoPdfinput
    {
        public string source_path { get; set; }
        public string save_path { get; set; }
    }
    public class HtmlconvertionBlOutput
    {
        public bool status { get; set; }
        public string save_path { get; set; }
        public string Message { get; set; }
    }

    public class EmailAttachmentinput
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string Procedure { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }



}
