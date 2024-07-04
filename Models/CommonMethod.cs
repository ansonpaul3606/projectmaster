using PerfectWebERPAPI.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CommonMethod
    {
        BlFunctions blfunlog = new BlFunctions();
        public string Encrypt(string strClearText)
        {
            return blfunlog.Encrypt(strClearText);
        }
        public string Decrypt(string strcipherText)
        {
            return blfunlog.Decrypt(strcipherText);
        }
        public string EncryptString(string strClearText)
        {
            return blfunlog.EncryptString(strClearText);
        }
        public string DecryptString(string strcipherText)
        {
            return blfunlog.DecryptString(strcipherText);
        }
    }
}