using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerfectWebERPAPI.Interface;
using PerfectWebERPAPI.DataAccess;
using System.Security.Cryptography;

using System.Collections;
using System.Xml;
using System.Globalization;
using System.Data;
using System.Configuration;
//using System.Web.Security;
using System.IO;

namespace PerfectWebERPAPI.Business
{
    public class BlFunctions : IFunctions
    {
        private string _TableName;
        private string _DbName;
        private string _Module;
        private Int32 _FkMachine;
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


        public BlFunctions()
        {
            Initialize();
        }
        public void Initialize()
        {
            _TableName = String.Empty;
            _DbName = String.Empty;
            _Module = String.Empty;
            _FkMachine = 0;
            _BranchCodeUser = 0;
            _UserCode = String.Empty;
            _ObjectName = String.Empty;
            _ObjectParameters = String.Empty;
            _ObjectArguments = String.Empty;
            _ObjectDataTypes = String.Empty;
            _QuerytoExecute = String.Empty;
            _ObjectSplitChar = ",";
            _ObjectPhoto = null;
            _ObjectSign = null;


        }
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string Module
        {
            get { return _Module; }
            set { _Module = BlFormats.DecryptDataForAuthCode(value); }
        }
        public Int32 MachineID
        {
            get { return _FkMachine; }
            set { _FkMachine = Convert.ToInt16(BlFormats.DecryptDataForAuthCode(value.ToString())); }
        }

        public Int16 BranchCodeUser
        {
            get { return _BranchCodeUser; }
            set { _BranchCodeUser =Convert.ToInt16(BlFormats.DecryptDataForAuthCode(value.ToString())); }
        }
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string ObjectName
        {
            get { return _ObjectName; }
            set { _ObjectName = BlFormats.DecryptDataForAuthCode(value); }
        }
        public string ObjectParameters
        {
            get { return _ObjectParameters; }
            set { _ObjectParameters = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string ObjectArguments
        {
            get { return _ObjectArguments; }
            set { _ObjectArguments = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string ObjectDataTypes
        {
            get { return _ObjectDataTypes; }
            set { _ObjectDataTypes = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string QuerytoExecute
        {
            get { return _QuerytoExecute; }
            set { _QuerytoExecute = BlFormats.DecryptDataForAuthCode(value == null ? "" : value); }
        }
        public string ObjectSplitChar
        {
            get { return _ObjectSplitChar; }
            set { _ObjectSplitChar = BlFormats.DecryptDataForAuthCode(value == null ? "," : value); }
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
        public DataTable GetProcedureOutputAsDataTable()
        {
            DalFunctions dalfunctions = new DalFunctions();
            try
            {
                return dalfunctions.GetProcedureOutputAsDataTable(this);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public DataSet GetProcedureOutputAsDataSet()
        {
            DalFunctions dalfunctions = new DalFunctions();
            try
            {
                return dalfunctions.GetProcedureOutputAsDataSet(this);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
        public string GetProcedureOutputAsScalarValue()
        {

            DalFunctions dalfunctions = new DalFunctions();
            try
            {
                return dalfunctions.GetProcedureOutputAsScalarValue(this);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool GetProcedureOutputAsNonQuery()
        {
            
            DalFunctions dalfunctions = new DalFunctions();
            try
            {
                return dalfunctions.GetProcedureOutputAsNonQuery(this);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public DataTable GetQueryOutputAsDataTable()
        {
            DalFunctions dalfunctions = new DalFunctions();
            try
            {
                return dalfunctions.GetQueryOutputAsDataTable(this);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public string GetReturnMessage(string Returnvalue, string DbName)
        {
            DalFunctions dalFunctions = new DalFunctions();
            return dalFunctions.GetReturnMessage(Returnvalue, DbName);

        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
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
        public string EncryptText(string input)
        {
            string password = "MAKV2SPBNI99212";
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public string DecryptText(string input)
        {
            string password = "MAKV2SPBNI99212";
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
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
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
    public class rootProcedureOutputAsDataTable : rootCmnAPIResponse
    {
        public DataTable dtable { get; set; }
    }
    public class rootProcedureOutputAsDataSet : rootCmnAPIResponse
    {
        public DataSet dataSet { get; set; }
    }
    
    public class rootProcedureOutputAsScalarValue : rootCmnAPIResponse
    {
        public string ReturnValue { get; set; }
        public string ReturnMessage { get; set; }
    }
    public class rootProcedureOutputAsNonQuery : rootCmnAPIResponse
    {
        public bool NonQueryValue { get; set; }
    }
    public class rootQueryOutputAsDataTable : rootCmnAPIResponse
    {
        public DataTable dtable { get; set; }
    }
    public class rootCmnAPIResponse
    {
        public int StatusCode { get; set; }
        public string EXMessage { get; set; }

    }
    public class RootERPCmnSearchPopup : CommonAPIResponse
    {
        public DynamicData ERPCmnSearchPopup { get; set; }
    }
    public class RootERPConverthtml : CommonAPIResponse
    {
        public string path { get; set; }
    }

}
