using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using static PerfectWebERP.Models.AMCMonitoringModel;
using System.Text;
using Newtonsoft.Json;
using System.Security.Cryptography;
using PerfectWebERPAPI.Business;
using System.IO;

namespace PerfectWebERP.Controllers
{
    public class sendMail
    {
        public void sendMailData(long FK_Master, string TransMode, long FK_Company, int Mode, int Type, string companyKey, string remark)
        {
            try
            {
                CommonModel objComm = new CommonModel();
                MailModel objMail = new MailModel();
                var mailSettings = objComm.GetGeneralSettingsData(companyKey: companyKey, input: new CommonModel.GetGeneralSettings
                {
                    FK_Company = FK_Company,
                    GsModule = "MAIL"
                });
                MailModel.MailCredential objMCr = new MailModel.MailCredential();
                foreach (var row in mailSettings.Data)
                {
                    if (row.GsField == "HOST")
                        objMCr.host = row.GsData;
                    if (row.GsField == "PORT")
                        objMCr.port = row.GsData;
                    if (row.GsField == "UNAME")
                        objMCr.userName = row.GsData;
                    if (row.GsField == "PWD")
                        objMCr.password = row.GsData;
                    if (row.GsField == "ENSSL")
                        objMCr.enableSSL = Convert.ToBoolean(row.GsValue);
                }

                //pushing data into SmsDraft table 
                var mailData = objMail.GetMailData(companyKey: companyKey, input: new MailModel.GetMail
                {
                    FK_Company = FK_Company,
                    TransMode = TransMode,
                    MasterID = FK_Master,
                    Mode = Mode,
                    Type = Type,
                    Remarks = remark
                });

                foreach (var mailRow in mailData.Data)
                {
                    MailMessage message = new MailMessage(objMCr.userName.ToString(), mailRow.Email_ID.ToString(), mailRow.Subject.ToString(), mailRow.Content.ToString());
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = objMCr.host.Trim();
                    smtp.Port = Convert.ToInt32(objMCr.port);
                    smtp.Credentials = new NetworkCredential(objMCr.userName.Trim(), objMCr.password.Trim());
                    smtp.EnableSsl = objMCr.enableSSL;
                    message.IsBodyHtml = true;
                    smtp.Send(message);

                    //insert data into emial daraft table 
                    objMail.UpdateMailDetails(companyKey: companyKey, input: new MailModel.UpdateMailData
                    {
                        FK_Master = FK_Master,
                        TransMode = TransMode,
                        EmailID = mailRow.Email_ID.ToString(),
                        Subject = mailRow.Subject.ToString(),
                        Body = mailRow.Content.ToString(),
                        Attachment = "",
                        FK_Company = FK_Company,
                        FK_Branch = 0
                    });
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }

        }

      
        public bool SendMailDatabulk(List<SendIntimationTable> inputData, long FK_Company, string TransMode, string companyKey, string remark)
        {
            try
            {
                CommonModel objComm = new CommonModel();
                MailModel objMail = new MailModel();
                //var mailSettings = objComm.GetGeneralSettingsData(companyKey: companyKey, input: new CommonModel.GetGeneralSettings
                //{
                //    FK_Company = FK_Company,
                //    GsModule = "MAIL"
                //});
                //MailModel.MailCredential objMCr = new MailModel.MailCredential();
                //foreach (var row in mailSettings.Data)
                //{
                //    if (row.GsField == "HOST")
                //        objMCr.host = row.GsData;
                //    if (row.GsField == "PORT")
                //        objMCr.port = row.GsData;
                //    if (row.GsField == "UNAME")
                //        objMCr.userName = row.GsData;
                //    if (row.GsField == "PWD")
                //        objMCr.password = row.GsData;
                //    if (row.GsField == "ENSSL")
                //        objMCr.enableSSL = Convert.ToBoolean(row.GsValue);
                //}
                List<MailModel.UpdateMailData> updateMailDatas = new List<MailModel.UpdateMailData>();

                foreach (var item in inputData)
                {
                    if (item.Mode == 2)
                    {
                        //MailMessage message = new MailMessage(objMCr.userName, item.Email_ID, item.Subject, item.Content);
                        //SmtpClient smtp = new SmtpClient();
                        //smtp.Host = objMCr.host.Trim();
                        //smtp.Port = Convert.ToInt32(objMCr.port);
                        //smtp.Credentials = new NetworkCredential(objMCr.userName.Trim(), objMCr.password.Trim());
                        //smtp.EnableSsl = objMCr.enableSSL;
                        //message.IsBodyHtml = true;
                        //smtp.Send(message);


                        updateMailDatas.Add(new MailModel.UpdateMailData
                        {
                            FK_Master = item.FK_Master,
                            TransMode = TransMode,
                            EmailID = item.Email_ID,
                            Subject = item.Subject,
                            Body = item.Content,
                            Attachment = "",
                            FK_Company = FK_Company,
                            FK_Branch = 0
                        });

                    }

                }


                string Jsoninput = JsonConvert.SerializeObject(updateMailDatas);



                var data = objMail.UpdateBulkMailDetails(companyKey: companyKey, input: new MailModel.UpdateMailStringData
                {
                    JsonInput = Jsoninput
                });
                return true;
            }
            catch
            {
                return false;
            }



        }
        public string EncryptToken(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            // Replace unsafe URL characters with safe ones
            return clearText.Replace('+', '-').Replace('/', '_').Replace("=", "");
        }
        public Output sendSubscriptionMailData(long FK_Master, long FK_Company, string companyKey, string email, string FirstName, string TransMode, string Subject, string EncpToken, string sessionID, string ipAddress)
        {
            try
            {

                CommonModel objComm = new CommonModel();
                MailModel objMail = new MailModel();
                var mailSettings = objComm.GetGeneralSettingsData(companyKey: companyKey, input: new CommonModel.GetGeneralSettings
                {
                    FK_Company = 0,
                    GsModule = "SUBS"
                });
                MailModel.MailCredential objMCr = new MailModel.MailCredential();
                foreach (var row in mailSettings.Data)
                {
                    if (row.GsField == "HOST")
                        objMCr.host = row.GsData;
                    if (row.GsField == "PORT")
                        objMCr.port = row.GsData;
                    if (row.GsField == "UNAME")
                        objMCr.userName = row.GsData;
                    if (row.GsField == "PWD")
                        objMCr.password = row.GsData;
                    if (row.GsField == "ENSSL")
                        objMCr.enableSSL = Convert.ToBoolean(row.GsValue);
                }

                BlFunctions blfunlog = new BlFunctions();
                objMCr.userName.ToString();
                var UserName = objMCr.userName.ToString();
                string SubscriptionKey = System.Web.Configuration.WebConfigurationManager.AppSettings["SubscriptionKey"]?.ToString();
                // var EncpToken  = GenerateToken();
                //string Token = blfunlog.Encrypt(EncpToken);
                var messagecontent = @"
<html lang ='en'>
 <head>
     <meta charset ='UTF-8'>
       <meta name='viewport' content='width=device-width, initial-scale=1.0'>
     
             <title>Email Template </title>
       
               <style>
                 html{ margin: 0px; padding: 0px; box - sizing:border - box;
                }
    </style >
</head >
<body style = 'font -family:'Poppins',Arial,sans-serif;margin:0px; padding:0px;' >
   
        <table style='margin:0px;padding:0px;'width='100%' cellpadding='0' cellspacing='0' border='0' >
            
                              <tr>
                                  <td align='center' style='padding:0px;' >
               
                                  <table class='content' width='600' border='0' cellspacing='0' cellpadding='0'
                    style='border-collapse: collapse; border:0;'>
                    <!-- Header -->
                    <tr>
                        <td class='header'
                            style='background-color:#3B8183;padding:15px;text-align:center;color:white;font-size: 24px;'>
                            <p style='font-weight:400;color:#ffffff;font-size:16px;letter-spacing:0;line-height:16px;white-space:nowrap;'>
                                Thanks for signing up</p>
                            <h1 style='font-weight:400;color:#ffffff;font-size:24px;letter-spacing:-0.48px;line-height:normal;margin:10px 0;'>
                                Verify Your Email Address</h1>
                        </td>
                    </tr>

                    <!-- Body -->
                    <tr>
                             <td class='body style='padding:40px;text-align:center;font-size:16px; line-height:1.6;'>
                              
                            <br><br>
                            <p
                                style='font-weight:400;color:#000000;font-size:13px;text-align:center;letter-spacing:0;line-height: normal; margin:0;'>
                                Hi " + FirstName + @",</p>
                            <p
                                style='font-weight:400;color:#000000;font-size:13px;text-align:center;letter-spacing:0;line-height:normal;margin:5px;'>
                                You’re almost ready to get started.<br> Please click on the button below to<br> verify your
                                email address.</p>
                        </td>
                    </tr>

                    <!-- Call to action Button -->
                    <tr>
                        <td style='padding:0px40px0px40px;text-align:center;' >
                            <table cellspacing= '0' cellpadding= '0' style= 'margin: auto;' >
                                <tr>

                                    <td align='center'
                                        style='background-color:#3B8183;padding:8px 80px;border-radius:5px;'>
<a href='" + SubscriptionKey + "/Subscription/VerifyEmail?userId=" + blfunlog.EncryptString(FK_Master.ToString()) + "&token=" + EncryptToken(EncpToken.ToString()) + "&FK_Company=" + blfunlog.EncryptString(FK_Company.ToString()) + @"'style='color:#ffffff;text-decoration:none; font-weight: 300; font-size:13px;'>Verify Email</a>





                                    </td>

    
                                    </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class='body' style='padding:10px 0;text-align:center;font-size:16px;line-height:1.6;'>
                          <p style='font-weight:400;color:transparent;font-size:13px;text-align:center;letter-spacing:0;line-height:normal;' >
                                <a style='color:#000; text-decoration:none;'>Need help? Ask at</a>&nbsp;
                                <a style='color:#3B8183; text-decoration: none;'>Contact@Perfectlimited.Com</a> &nbsp;
                                <a style='color:#000; text-decoration:none;' > or visit our</a>  &nbsp;
                                <a style='color:#3B8183; text-decoration: underline;'href ='https://perfectlimited.com/'>https://perfectlimited.com/</a>
                            </p>

                        </td>
                    </tr>

                    <tr>
                        <td class='body' style='padding: 10px; text-align:center;font-size: 16px;line-height: 1.6; display:flex;justify-content:center; align-items:center;'>

                            <p style='margin:0auto;font-weight:300;color:#636363;font-size:11px;text-align:center;letter-spacing: 0;line-height: 16.5px;display:flex;justify-content: center; align-items: center;' > 1201 B , 2nd Floor, Hilite Business Park,<br>Thondayad By-Pass, Calicut, 673014</p>

                        </td>
                    </tr>
                    <!-- Footer -->
                    <tr>
                        <td class='footer'
                            style='background-color:#8CB4B5;padding: 10px;text-align:center;color:#000000; font-size: 13px;'>
                            Copyright &copy; 2024 <b> Persuit ERP Software.</b> All rights reserved
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>

</html>";
                //if (!mailData.Process.IsProcess)
                // {
                //    return new Output { IsProcess = false, Message = mailData.Process.Message };
                // }

                // foreach (var mailRow in mailData.Data)
                // {
                //  string updatedContent = messagecontent.Replace("{##{Body}##}", mailRow.Content.ToString());
                // MailMessage message = new MailMessage(FirstName.ToString(), mailRow.Email_ID.ToString(), Subject.ToString(),"");
                MailMessage message = new MailMessage(objMCr.userName.ToString(), email.ToString(),
                    Subject.ToString(), messagecontent.ToString());
                SmtpClient smtp = new SmtpClient();
                smtp.Host = objMCr.host.Trim();
                smtp.Port = Convert.ToInt32(objMCr.port);
                smtp.Credentials = new NetworkCredential(objMCr.userName.Trim(), objMCr.password.Trim());
                smtp.EnableSsl = objMCr.enableSSL;
                message.IsBodyHtml = true;
                try
                {
                    smtp.Send(message);
                }

                catch (Exception ex)
                {

                }
                //insert data into emial daraft table 
                objMail.UpdateMailDetails(companyKey: companyKey, input: new MailModel.UpdateMailData
                {
                    FK_Master = FK_Master,
                    TransMode = TransMode,
                    EmailID = email.ToString(),
                    Subject = Subject.ToString(),
                    Body = messagecontent.ToString(),
                    Attachment = "",
                    FK_Company = FK_Company,
                    FK_Branch = 0
                });
                //  }


                var outptt = new Output { IsProcess = true, Message = new List<string> { "Success" } };


                return outptt;



            }
            catch (Exception ex)
            {
                var outptt = new Output { IsProcess = false, Message = new List<string> { "Something went wrong" }, Status = ex.Message.ToString() };
                return outptt;
            }


        }

        public static string GenerateToken(int length = 32)
        {

            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[length];
            byte[] randomBytes = new byte[length];

            // Use a unique identifier (such as a GUID) or a timestamp to ensure uniqueness
            string uniqueString = Guid.NewGuid().ToString(); // or DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

            // Combine randomBytes with the uniqueString
            byte[] combinedBytes = Encoding.UTF8.GetBytes(uniqueString);

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // XOR operation between randomBytes and combinedBytes
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[(randomBytes[i] ^ combinedBytes[i]) % allowedChars.Length];
            }

            return new string(chars);
        }

        public Output sendMailDataforgotpassword(long FK_Master, string TransMode, long FK_Company, int Mode, int Type, string companyKey, string remark, string UserCode, string useremail,string EmployeeName)
        {
            try
            {
                CommonModel objComm = new CommonModel();
                MailModel objMail = new MailModel();
                var mailSettings = objComm.GetGeneralSettingsData(companyKey: companyKey, input: new CommonModel.GetGeneralSettings
                {
                    FK_Company = 0,
                    GsModule = "SUBS"
                });
                MailModel.MailCredential objMCr = new MailModel.MailCredential();
                foreach (var row in mailSettings.Data)
                {
                    if (row.GsField == "HOST")
                        objMCr.host = row.GsData;
                    if (row.GsField == "PORT")
                        objMCr.port = row.GsData;
                    if (row.GsField == "UNAME")
                        objMCr.userName = row.GsData;
                    if (row.GsField == "PWD")
                        objMCr.password = row.GsData;
                    if (row.GsField == "ENSSL")
                        objMCr.enableSSL = Convert.ToBoolean(row.GsValue);
                }


                BlFunctions blfunlog = new BlFunctions();
                objMCr.userName.ToString();
                var UserName = objMCr.userName.ToString();
                string  ForgotPasswordKey = System.Web.Configuration.WebConfigurationManager.AppSettings["ForgotpasswordKey"]?.ToString();            
                var contentval= @"
                                <html lang ='en'>
                                 <head>
                                 <meta charset ='UTF-8'>
                                 <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                 <title>ForgotPassword Template </title>
                                 <style>
                                 html{ margin: 0px; padding: 0px; box - sizing:border - box;
                                 }
                                 </style >
                                </head >
                                <body style = 'font -family:'Poppins',Arial,sans-serif;margin:0px; padding:0px;' >
                                <table style='margin:0px;padding:0px;'width='100%' cellpadding='0' cellspacing='0' border='0' >
                                 <tr>
                                  <td align='center' style='padding:0px;' >
                                 <table class='content' width='600' border='0' cellspacing='0' cellpadding='0'
                                  style='border-collapse: collapse; border:0;'>
                               <!-- Header -->
                               <tr>
                              <td class='header'
                              style='background-color:#3B8183;padding:15px;text-align:center;color:white;font-size: 24px;'>
                            <p style='font-weight:400;color:#ffffff;font-size:16px;letter-spacing:0;line-height:16px;white-space:nowrap;'>
                               </p>
                            <h1 style='font-weight:400;color:#ffffff;font-size:24px;letter-spacing:-0.48px;line-height:normal;margin:10px 0;'>
                                Password Reset Request</h1>
                            </td>
                          </tr>
                    <!-- Body -->
                         <tr>
                          <td class='body style='padding:40px;text-align:center;font-size:16px; line-height:1.6;'>
                          <img src='~/Assets/images/Persuite-logo.svg' alt='' style='width:120px;'>
                            <br><br>
                            <p
                                style='font-weight:400;color:#000000;font-size:13px;text-align:center;letter-spacing:0;line-height: normal; margin:0;'>
                              Hello " + EmployeeName + @",</p>
                          
                 <p 
                       style='font-weight:400;color:#000000;font-size:13px;text-align:center;letter-spacing:0;line-height: normal; margin:0;'> <strong>" + remark + @"</strong>

                                                     is your temporary password. Please reset your password during this login attempt.</p>                  
                       </td>
                    </tr>
                    <!-- Call to action Button -->
                       <tr>
                        <td style='padding:0px40px0px40px;text-align:center;' >
                            <table cellspacing= '0' cellpadding= '0' style= 'margin: auto;' >
                                <tr>
                            <a href='" + ForgotPasswordKey + @"/Security/Login' style='display:inline-block; margin-top:20px; padding:5px 10px;background-color:#205456;color:#ffffff; text-decoration: none; font-size: 16px; font-weight: bold; border-radius: 5px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); transition: background-color 0.3s ease, box-shadow 0.3s ease;'> Sign In </ a >
                                    </tr>
                     </table>
                        </td>
                    </tr>
                    <tr>
                        <td class='body' style='padding:10px 0;text-align:center;font-size:16px;line-height:1.6;'>
                          <p style='font-weight:400;color:transparent;font-size:13px;text-align:center;letter-spacing:0;line-height:normal;'>    
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class='body' style='padding: 10px; text-align:center;font-size: 16px;line-height: 1.6; display:flex;justify-content:center; align-items:center;'>
                            <p style='margin:0auto;font-weight:300;color:#636363;font-size:11px;text-align:center;letter-spacing: 0;line-height: 16.5px;display:flex;justify-content: center; align-items: center;' ></p>
                        </td>
                    </tr>
                    <!-- Footer -->
                    <tr>
                        <td class='footer'
                            style='background-color:#8CB4B5;padding: 10px;text-align:center;color:#000000; font-size: 13px;'>
                            Copyright &copy; 2024 <b> Persuit ERP Software.</b> All rights reserved
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
                MailMessage message = new MailMessage(objMCr.userName.ToString(), useremail.ToString(),
                  "Forgot Password", contentval.ToString());
                SmtpClient smtp = new SmtpClient();
                smtp.Host = objMCr.host.Trim();
                smtp.Port = Convert.ToInt32(objMCr.port);
                smtp.Credentials = new NetworkCredential(objMCr.userName.Trim(), objMCr.password.Trim());
                smtp.EnableSsl = objMCr.enableSSL;
                message.IsBodyHtml = true;
                try
                {
                    smtp.Send(message);
                }

                catch (Exception ex)
                {

                }
                //insert data into emial daraft table 
                objMail.UpdateMailDetails(companyKey: companyKey, input: new MailModel.UpdateMailData
                {
                    FK_Master = FK_Master,
                    TransMode = TransMode,
                    EmailID = useremail.ToString(),
                    Subject = "",
                    Body = contentval.ToString(),
                    Attachment = "",
                    FK_Company = FK_Company,
                    FK_Branch = 0
                });
                //  }


                var outptt = new Output { IsProcess = true, Message = new List<string> { "Success" } };


                return outptt;



            }
            catch (Exception ex)
            {
                var outptt = new Output { IsProcess = false, Message = new List<string> { "Something went wrong" }, Status = ex.Message.ToString() };
                return outptt;
            }
        }



       

    }
}