
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Net;
using System.Net.Security;
using PerfectWebERP.Business;
using System.Configuration;
using PerfectWebERP.General;

namespace PerfectWebERP.Services
{
    public static class APIServices
    {
        public static bool DisableSSL= ConfigurationManager.AppSettings["DisableSSL"] != null ? Convert.ToBoolean(ConfigurationManager.AppSettings["DisableSSL"].ToString()) : false;
        public static string APIURLPath;
        public static HttpResponseMessage CallAPiService(object obj, String routingURL)
        {
            //HttpClient client = new HttpClient();
            //string baseurl = "";
            //baseurl = WebConfigurationManager.AppSettings["api-url"].ToString();
            HttpResponseMessage datresponse;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpClient client = new HttpClient();
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidationCallBack);
                string APIURLPath = ConfigurationManager.AppSettings["APIURL"] != null ? ConfigurationManager.AppSettings["APIURL"].ToString() : "";
                client.BaseAddress = new Uri(APIURLPath);//"http://localhost:49708/"https://202.21.32.35:14001/TouchNBuyAPI//http://202.21.32.35:22001/
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = new TimeSpan(0, 10, 0);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                datresponse = client.PostAsJsonAsync(routingURL, obj).Result;

            }
            catch (Exception HttpEx)
            {
                UpdateErrorLog("Error CallAPiService:"+HttpEx.Message);
                datresponse = null;
            }
            return datresponse;
        }
        private static bool UpdateErrorLog(string strData)
        {
            Log dalfun = new Log();

            dalfun.WriteLog(strData,false);
            return true;
        }
        private static bool CertificateValidationCallBack(
     object sender,
     System.Security.Cryptography.X509Certificates.X509Certificate certificate,
     System.Security.Cryptography.X509Certificates.X509Chain chain,
     System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {

            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                //  clsWriteLog.WriteLog("System.Net.Security.SslPolicyErrors.None:true", @"C:\SCORESCANLog");
                return true;
            }
            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid.
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                if (!DisableSSL)
                                {
                                    UpdateErrorLog("CallAPiService:FAILED CONDITION 1:certificate.Subject:" + certificate.Subject + ",certificate.Issuer:" + certificate.Issuer + ",status.Status" + status.Status.ToString());
                                    return false;
                                }
                                else
                                    return true;
                            }
                        }
                    }
                }
                // When processing reaches this line, the only errors in the certificate chain are
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange Server installations, so return true.
                //clsWriteLog.WriteLog(sslPolicyErrors.ToString() + ":true :1", @"C:\SCORESCANLog");
                return true;
            }
            else
            {
                // In all other cases, return false.

                if (!DisableSSL)
                {
                    string SerailsslPolicyErrors = "";
                    foreach (string name in sslPolicyErrors.ToString().Split('|'))
                    {
                        SerailsslPolicyErrors = SerailsslPolicyErrors + name;
                    }
                    UpdateErrorLog("CallAPiService:FAILED CONDITION 2:SslPolicyErrors-" + SerailsslPolicyErrors);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }

}