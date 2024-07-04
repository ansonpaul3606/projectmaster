using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;
using PerfectWebERPAPI.Business;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace PerfectWebERP.Controllers
{
    
    public class SubscriptionController : Controller
    {
        // GET: Subscription
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Verify()
        {
            return View();
        }
        public ActionResult ErorMessage()
        {
            return View();
        }
        public ActionResult Timer()
        {
            return View();
        }
        public ActionResult EmailError()
        {
            return View();
        }
        public ActionResult UserError()
        {
            return View();
        }
        public ActionResult PhoneError()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ValidateUserName(SubsciptionModel.SubscriptionView input)


        {
            try
            {
                SubsciptionModel ScrModel = new SubsciptionModel();
                BlFunctions blfunlog = new BlFunctions();
                var datresponse = ScrModel.CheckExistance(input: new SubsciptionModel.UpdateSubscriptionData
                {

                    Mode = input.Mode,
                    FirstName = input.FirstName,
                    lastName = input.lastName,
                    email = input.email,
                    CompanyName = input.companyname,
                    UserCode = input.UserName,
                    MobileNo = input.phoneNumber,
                    PassWord = input.ReenterPW,

                },

            companyKey: "");
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                return Json(new { Process = new SubsciptionModel.Outputclass { IsProcess = false, Message = new List<string> { "Something went wrong" }, Status = "exception" } }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult SaveSubscriptionDetails(SubsciptionModel.SubscriptionView input)
        {
            try
            {
                SubsciptionModel ScrModel = new SubsciptionModel();
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        Process = new SubsciptionModel.Outputclass
                        {
                            IsProcess = false,
                            Message = ModelState.Values.SelectMany(m => m.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList(),
                            Status = "Validation failed",
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                BlFunctions blfunlog = new BlFunctions();
                string Encpasswordlog = blfunlog.Encrypt(Convert.ToString(input.UserName[0].ToString() + input.enterPW));
                var Token = GenerateToken();
                string EncpToken = blfunlog.Encrypt(Token);
                string sessionID = Session.SessionID;
                string ipAddress = Request.UserHostAddress;
                var proOutput = ScrModel.UpdateSubData(input: new SubsciptionModel.UpdateSubscriptionData()
                {
                    Mode = input.Mode,
                    FirstName = input.FirstName,
                    lastName = input.lastName,
                    email = input.email,
                    CompanyName = input.companyname,
                    UserCode = input.UserName,
                    MobileNo = input.phoneNumber,
                    PassWord = Encpasswordlog,
                    TockenID = EncpToken,
                }, companyKey: "");
                string errmsg = "";
                string errcode = "";
                if (proOutput.Process.IsProcess)
                {

                    errcode = proOutput.Data[0].ErrCode;
                    if (errcode == "-2")
                    {
                        errmsg = proOutput.Data[0].ErrMsg;

                    }
                    else if (errcode == "-7")
                    {
                        errmsg = proOutput.Data[0].ErrMsg;

                    }
                    else if (errcode == "-8")
                    {
                        errmsg = proOutput.Data[0].ErrMsg;

                    }
                    else
                    {
                        sendMail objMail = new sendMail();
                        objMail.sendSubscriptionMailData(proOutput.Data[0].FK_Users, proOutput.Data[0].FK_Company, "", proOutput.Data[0].email, proOutput.Data[0].FirstName, "Subscription", proOutput.Data[0].Subject, EncpToken, sessionID, ipAddress);

                    }

                }
                return Json(new { Process = proOutput }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { Process = new SubsciptionModel.Outputclass { IsProcess = false, Message = new List<string> { "Something went wrong" }, Status = "exception" } }, JsonRequestBehavior.AllowGet);
            }
        }

        public string DecryptToken(string encryptedText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            // Restore the padding characters
            encryptedText = encryptedText.Replace('-', '+').Replace('_', '/') + "==";
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encryptedText = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return encryptedText;
        }
        public ActionResult VerifyEmail(string userId, string token,string FK_Company)
        {
            SubsciptionModel ScrModel = new SubsciptionModel();
            BlFunctions blfunlog = new BlFunctions();
            var FK_User = DecryptString(userId);
            var TokenDecrpted = DecryptToken(token);
            var proOutput = ScrModel.UpdateEmailSubData(input: new SubsciptionModel.UpdateSubscriptionData()
            {
                Mode = 4,
                TockenID = TokenDecrpted,
                FK_User = DecryptString(userId),
                FK_Company = DecryptString(FK_Company),
            },
              companyKey: "");

            SubsciptionModel.UpdateSubscriptionResult obData = new SubsciptionModel.UpdateSubscriptionResult();
            if (proOutput.Data != null)
            {
                obData = proOutput.Data[0];
                List<string> objMsg = new List<string>();
                objMsg.Add(obData.ErrCode);
                if (proOutput.Data[0].FK_Users > 0 && obData.ErrCode == "0")

                {
                    var TokenID = Decryptvalue(proOutput.Data[0].TockenID);
                    if (Convert.ToString(proOutput.Data[0].FK_Users) == FK_User && proOutput.Data[0].TockenID == TokenDecrpted)
                    {
                        return RedirectToAction("Timer", "Subscription");
                    }

                }
                else if (obData.ErrCode == "-4")
                {
                    return RedirectToAction("Verify", "Subscription");
                }
                else if (obData.ErrCode == "-5")
                {
                    return RedirectToAction("ErorMessage", "Subscription");
                }
                else if (obData.ErrCode == "-3")
                {
                    return RedirectToAction("Error", "Subscription");
                }
                else if (obData.ErrCode == "-2")
                {
                    return RedirectToAction("UserError", "Subscription");
                }
                else if (obData.ErrCode == "-7")
                {
                    return RedirectToAction("EmailError", "Subscription");
                }
                else if (obData.ErrCode == "-8")
                {
                    return RedirectToAction("PhoneError", "Subscription");
                }
            }
            return Json(new { Process = proOutput.Process }, JsonRequestBehavior.AllowGet);

           
        }
        public static string GenerateToken(int length = 32)
        {

            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[length];
            byte[] randomBytes = new byte[length];
            string uniqueString = Guid.NewGuid().ToString(); 

           
            byte[] combinedBytes = Encoding.UTF8.GetBytes(uniqueString);

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[(randomBytes[i] ^ combinedBytes[i]) % allowedChars.Length];
            }

            return new string(chars);
        }
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
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
            return clearText;
        }
        public string Decryptvalue(string encryptedText)
{
    string EncryptionKey = "MAKV2SPBNI99212";
    byte[] cipherBytes = Convert.FromBase64String(encryptedText);
    using (Aes encryptor = Aes.Create())
    {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }
            encryptedText = Encoding.Unicode.GetString(ms.ToArray());
        }
    }
    return encryptedText;
}

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public string EncryptString(string encryptString)
        {

            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string DecryptString(string cipherText)
        {

            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}