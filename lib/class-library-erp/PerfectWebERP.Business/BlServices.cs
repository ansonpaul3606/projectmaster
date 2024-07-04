using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.IO;
namespace PerfectWebERP.Business
{
        
    public class BlServices
    {
        private static byte[] bytes = ASCIIEncoding.ASCII.GetBytes("PssErp22");
        private string _TableName;
        private string _DbName;
        private string _ObjectName;
        private string _ObjectParameters;
        private string _ObjectArguments;
        private string _ObjectDataTypes;
        private string _QuerytoExecute;
        private string _ObjectSplitChar;

        public BlServices()
        {
            Initialize();
        }
        public void Initialize()
        {
            _TableName=string.Empty;
            _DbName=string.Empty;
            _ObjectName=string.Empty;
            _ObjectParameters=string.Empty;
            _ObjectArguments=string.Empty;
            _ObjectDataTypes=string.Empty;
            _QuerytoExecute=string.Empty;
            _ObjectSplitChar = ",";


        }
        public string TableName
        {
            get { return EncryptDataForAuthCode(_TableName); }
            set { _TableName = value; }
        }
        public string DbName
        {
            get { return EncryptDataForAuthCode(_DbName); }
            set { _DbName = value; }
        }
        public string ObjectName
        {
            get { return EncryptDataForAuthCode(_ObjectName); }
            set { _ObjectName = value; }
        }
        public string ObjectParameters
        {
            get { return EncryptDataForAuthCode(_ObjectParameters); }
            set { _ObjectParameters = value; }
        }
        public string ObjectArguments
        {
            get { return EncryptDataForAuthCode(_ObjectArguments); }
            set { _ObjectArguments = value; }
        }
        public string ObjectDataTypes
        {
            get { return EncryptDataForAuthCode(_ObjectDataTypes); }
            set { _ObjectDataTypes = value; }
        }
        public string QuerytoExecute
        {
            get { return EncryptDataForAuthCode(_QuerytoExecute); }
            set { _QuerytoExecute = value; }
        }
        public string ObjectSplitChar
        {
            get { return _ObjectSplitChar; }
            set { _ObjectSplitChar = EncryptDataForAuthCode(value == null ? "," : value); }
        }
        private static string EncryptDataForAuthCode(string originalString)
        {
            if (!String.IsNullOrEmpty(originalString))
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
                StreamWriter writer = new StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);

            }
            else
                return "";
        }
        private static string DecryptDataForAuthCode(string cryptedString)
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
                return data;
            }
            else
                return "";
        }
    }
    public class rootProcedureOutputAsDataTable : CmnAPIResponse
    {
        public DataTable dtable { get; set; }
    }
    public class rootProcedureOutputAsDataSet : CmnAPIResponse
    {
        public DataSet dataSet { get; set; }
    }
    public class rootProcedureOutputAsNonQuery : CmnAPIResponse
    {
        public bool NonQueryValue { get; set; }
    }
    public class rootQueryOutputAsDataTable : CmnAPIResponse
    {
        public DataTable dtable { get; set; }
    }
  
    public class rootQueryOutputAsDataSet : CmnAPIResponse
    {
        public DataSet dataSet { get; set; }
    }
    public class rootProcedureOutputAsScalarValue : CmnAPIResponse
    {
        public string ReturnValue { get; set; }
        public string ReturnMessage { get; set; }
    }
    public class CmnAPIResponse
    {
        public int StatusCode { get; set; }
        public string EXMessage { get; set; }
    }
}
