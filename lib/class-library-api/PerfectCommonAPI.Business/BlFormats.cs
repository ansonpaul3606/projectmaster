

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Timers;
using PerfectWebERPAPI.Interface;



namespace PerfectWebERPAPI.Business
{
    public class BlFormats
    {
        static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("PssErp22");
        public static string DecryptDataForAuthCode(string cryptedString)
        {
            try
            {

                if (!String.IsNullOrEmpty(cryptedString))
                {
                    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                    MemoryStream memoryStream = new MemoryStream
                            (Convert.FromBase64String(cryptedString));
                    CryptoStream cryptoStream = new CryptoStream(memoryStream,
                        cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
                    StreamReader reader = new StreamReader(cryptoStream);
                    memoryStream = null;
                    string data = reader.ReadToEnd();
                    reader = null;
                   // string data = cryptedString;
                    return data;
                }
                else
                    return "";
              //  return cryptedString;
            }
            catch (Exception)
            {
                return "";
            }
        }
        
    }
}
