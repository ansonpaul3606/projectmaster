using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerfectWebERP.Business;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace PerfectWebERP.General
{
    public static class Common
    {
        //public static string Getdate()
        //{
        //    DateTime CurrentDate=DateTime.Now;
        //    CultureInfo culture = new CultureInfo("en-US");
        //    return CurrentDate.ToString("d", culture);
        //}
        //public struct DateTimeWithZone
        //{
        //    private readonly DateTime utcDateTime;
        //    private readonly TimeZoneInfo timeZone;

        //    public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        //    {
        //        var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
        //        utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone);
        //        this.timeZone = timeZone;
        //    }

        //    public DateTime UniversalTime { get { return utcDateTime; } }

        //    public TimeZoneInfo TimeZone { get { return timeZone; } }

        //    public DateTime LocalTime
        //    {
        //        get
        //        {
        //            return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
        //        }
        //    }
        //}

        //execute sql query
        public static APIGetRecordsDynamic<T> GetDataViaQuery<T>(APIParameters parameters, string companyKey)
        {
            //custom error set in General/ErrorMessages.Json File
            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }

            try
            {
                string procedureName = "proERPCmnGetRecords";
                BlServices blServices = new BlServices();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.ObjectName = procedureName;
                blServices.ObjectParameters = "@TableName|@ListFields|@SortField|@Criteria|@Groupby";
                blServices.ObjectArguments = $"{parameters.TableName}|{parameters.SelectFields}|{parameters.SortFields}|{parameters.Criteria}|{parameters.GroupByFileds}";
                blServices.ObjectDataTypes = "VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)";
                blServices.ObjectSplitChar = "|";
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Common/GetCommonProcedureOutputAsDataTable");


                APIOutputDynamic<T> root = new APIOutputDynamic<T>();
                //datresponse.IsSuccessStatusCode= true : Got a response from api controller
                if (datresponse.IsSuccessStatusCode)
                {
                 

                    root = JsonConvert.DeserializeObject<APIOutputDynamic<T>>(datresponse.Content.ReadAsStringAsync().Result);
                    if (root.StatusCode == 0)
                    {
                        //datatable probably has atleast one row
                        return new APIGetRecordsDynamic<T>
                        {
                            Process = new Output { IsProcess = true, Message = new List<string> { "Success" }, Status = "OK" },
                            Data = root.dtable
                        };
                    }
                    else if (root.StatusCode == 1)
                    {
                        //Status code !=0 means datatable row count ==0 
                        // need to call databse to get common error then call that code here with error code=1
                        return new APIGetRecordsDynamic<T>
                        {
                            Process = new Output
                            {
                                IsProcess = false,
                                Message = new List<string> { _errorMessages.Find(a=> a.ErrorCode==2).ErrorMessage },
                                Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType
                            }

                        };
                    }
                    else
                    {
                        // status code= -1
                        // probably need to call error from database
                        #region Error Message fetch section
                        //Error section------
                        BlServices blServicesError = new BlServices();
                        blServicesError.DbName = "PERFECTERP" + companyKey;
                        blServicesError.ObjectName = "proERPGetErrorMessage";
                        blServicesError.ObjectParameters = "@ErrorCode|@ErrorHeader";
                        blServicesError.ObjectArguments = $"{Math.Abs(root.StatusCode)}|";
                        blServicesError.ObjectDataTypes = "string|string";
                        blServicesError.ObjectSplitChar = "|";
                        HttpResponseMessage datresponseError = APIServices.CallAPiService(blServicesError, "api/Masters/GetMastersProcedureOutputAsScalarValue");//<-- send data to the api controller
                        rootProcedureOutputAsScalarValue rootError = new rootProcedureOutputAsScalarValue();
                        if (datresponseError.IsSuccessStatusCode)//api call from here to apiservice is successful
                        {
                            rootError = JsonConvert.DeserializeObject<rootProcedureOutputAsScalarValue>(datresponseError.Content.ReadAsStringAsync().Result);
                            if (rootError.StatusCode == 0)// api call from api service to db is successful
                            {

                                return new APIGetRecordsDynamic<T>
                                {
                                    Process = new Output
                                    {
                                        IsProcess = false,
                                        Message = new List<string> { rootError.ReturnValue },
                                        Status = "OK"
                                    }
                                };


                            }

                        }
                        //Error section------
                        #endregion Error Message fetch section


                    }
                }


                //this code works if datresponse.IsSuccessStatusCode is false ie, api controller didnt respond to request
                return new APIGetRecordsDynamic<T>
                {
                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType }
                };
            }
            catch (Exception ex)
            {
                //Exception
                return new APIGetRecordsDynamic<T>
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage },
                        Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType
                    }

                };

            }
        }

        //call a procedure but dont return any data(only return number of rows updated)
        public static Output UpdateTableData<T>(string companyKey, string procedureName, T parameter)
        {
            //custom error set in General/ErrorMessages.Json File
            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {
                string splitter = "|";


                //Method 1
                List<string> tableColumnsName = new List<string>();
                List<string> tableColumnsType = new List<string>();
                List<string> tableColumnsvalue = new List<string>();

                //Method 2
                //List<TableColumnBuilder> table = new List<TableColumnBuilder>(); 

                string UserAction = "";
                // loop to get the model property name , datatype and value
                foreach (var prop in parameter.GetType().GetProperties())
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    string type = propType.Name;

                    //Method 1
                    tableColumnsName.Add("@" + prop.Name);//<-- This will give model property name
                    tableColumnsType.Add(type);//<-- This will give model datatype
                    tableColumnsvalue.Add(prop.GetValue(parameter, null)?.ToString());//<-- This will give value in the model property
                    if (prop.Name == "UserAction")
                    {
                        UserAction = prop.GetValue(parameter, null)?.ToString();
                    }
                    //Method 2
                    //table.Add(new TableColumnBuilder { ColumnName = "@" + prop.Name, ColumnType = prop.PropertyType.Name, ColumnValue = prop.GetValue(parameter, null).ToString() });
                }

                //Method 1
                // Join the list of property name,datatype and value into a string
                string tableColumnName = string.Join(splitter, tableColumnsName);
                string tableColumnType = string.Join(splitter, tableColumnsType);
                string tableColumnvalue = string.Join(splitter, tableColumnsvalue);

                //Method 2
                //var tableColumn = string.Join(splitter, table.Select(t => t.ColumnName).ToList());
                //var tableColumnType = string.Join(splitter, table.Select(t => t.ColumnType).ToList());
                //var tableColumnvalue = string.Join(splitter, table.Select(t => t.ColumnValue).ToList());



                BlServices blServices = new BlServices();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.ObjectName = procedureName;
                blServices.ObjectParameters = tableColumnName;
                blServices.ObjectArguments = tableColumnvalue;
                blServices.ObjectDataTypes = tableColumnType;
                blServices.ObjectSplitChar = splitter;
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsScalarValue");//<-- send data to the api controller


                rootProcedureOutputAsScalarValue root = new rootProcedureOutputAsScalarValue();
                if (datresponse.IsSuccessStatusCode)//api call from here to apiservice is successful
                {
                    root = JsonConvert.DeserializeObject<rootProcedureOutputAsScalarValue>(datresponse.Content.ReadAsStringAsync().Result);

                    if (root.StatusCode == 0)// api call from api service to db is successful
                    {
                        int.TryParse(root.ReturnValue, out int roweffected);
                        if (roweffected > 0)
                        {
                            if (UserAction == "1")
                            {
                                root.EXMessage = "Saved  Successfully";
                            }
                            else if (UserAction == "2")
                            {
                                root.EXMessage = "Updated Successfully";
                            }
                         
                            return new Output
                            {
                                IsProcess = true,
                                Message = new List<string> { root.EXMessage },
                                Status = "OK",
                                code= roweffected
                            };
                        }
                        else if (root.EXMessage == "SUCCESS" && root.ReturnMessage == " ")
                        {
                            return new Output
                            {
                                IsProcess = true,
                                Message = new List<string> { root.ReturnValue },
                                Status = "OK",
                                code = roweffected
                            };
                        }
                        else
                        {
                            bool s = int.TryParse(root.ReturnValue, out int rowEffected);
                            return new Output
                            {
                                IsProcess = false,
                                Message = new List<string> { root.ReturnMessage },
                                Status = "OK"
                            };
                        }

                    }

                }

                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType
                };
            }
            catch(Exception ex)
            {
                //Exception
                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType
                };
            }
        }

        //call a procedure to get a list of data
        public static Output UpdateTableData1<T>(string companyKey, string procedureName, T parameter)
        {
            //custom error set in General/ErrorMessages.Json File
            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {
                string splitter = "|";


                //Method 1
                List<string> tableColumnsName = new List<string>();
                List<string> tableColumnsType = new List<string>();
                List<string> tableColumnsvalue = new List<string>();

                //Method 2
                //List<TableColumnBuilder> table = new List<TableColumnBuilder>(); 

                string UserAction = "";
                // loop to get the model property name , datatype and value
                foreach (var prop in parameter.GetType().GetProperties())
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    string type = propType.Name;

                    //Method 1
                    tableColumnsName.Add("@" + prop.Name);//<-- This will give model property name
                    tableColumnsType.Add(type);//<-- This will give model datatype
                    tableColumnsvalue.Add(prop.GetValue(parameter, null)?.ToString());//<-- This will give value in the model property
                    if (prop.Name == "UserAction")
                    {
                        UserAction = prop.GetValue(parameter, null)?.ToString();
                    }
                    //Method 2
                    //table.Add(new TableColumnBuilder { ColumnName = "@" + prop.Name, ColumnType = prop.PropertyType.Name, ColumnValue = prop.GetValue(parameter, null).ToString() });
                }

                //Method 1
                // Join the list of property name,datatype and value into a string
                string tableColumnName = string.Join(splitter, tableColumnsName);
                string tableColumnType = string.Join(splitter, tableColumnsType);
                string tableColumnvalue = string.Join(splitter, tableColumnsvalue);

                //Method 2
                //var tableColumn = string.Join(splitter, table.Select(t => t.ColumnName).ToList());
                //var tableColumnType = string.Join(splitter, table.Select(t => t.ColumnType).ToList());
                //var tableColumnvalue = string.Join(splitter, table.Select(t => t.ColumnValue).ToList());



                BlServices blServices = new BlServices();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.ObjectName = procedureName;
                blServices.ObjectParameters = tableColumnName;
                blServices.ObjectArguments = tableColumnvalue;
                blServices.ObjectDataTypes = tableColumnType;
                blServices.ObjectSplitChar = splitter;
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsScalarValue");//<-- send data to the api controller


                rootProcedureOutputAsScalarValue root = new rootProcedureOutputAsScalarValue();
                if (datresponse.IsSuccessStatusCode)//api call from here to apiservice is successful
                {
                    root = JsonConvert.DeserializeObject<rootProcedureOutputAsScalarValue>(datresponse.Content.ReadAsStringAsync().Result);

                    if (root.StatusCode == 0)// api call from api service to db is successful
                    {
                        int.TryParse(root.ReturnValue, out int roweffected);
                        if (roweffected > 0)
                        {
                            if (UserAction == "1")
                            {
                                root.EXMessage = "Saved  Successfully";
                            }
                            else if (UserAction == "2")
                            {
                                root.EXMessage = "Updated Successfully";
                            }

                            return new Output
                            {
                                IsProcess = true,
                                Message = new List<string> { root.EXMessage },
                                Status = "OK",
                                code = roweffected
                            };
                        }
                        else
                        {
                            bool s = int.TryParse(root.ReturnValue, out int rowEffected);
                            return new Output
                            {
                                IsProcess = false,
                                Message = new List<string> { root.ReturnValue },
                                Status = "OK"
                            };
                        }

                    }

                }

                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType
                };
            }
            catch (Exception ex)
            {
                //Exception
                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType
                };
            }
        }


        public static APIGetRecordsDynamic<T> GetDataViaProcedure<T, U>(string companyKey, string procedureName, U parameter)
        {
            //custom error set in General/ErrorMessages.Json File

            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {
                string splitter = "|";

                List<string> tableColumnsName = new List<string>();
                List<string> tableColumnsType = new List<string>();
                List<string> tableColumnsvalue = new List<string>();

                foreach (var prop in parameter.GetType().GetProperties())
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    string type = propType.Name;

                    tableColumnsName.Add("@" + prop.Name);
                    tableColumnsType.Add(type);
                    tableColumnsvalue.Add(prop.GetValue(parameter, null)?.ToString());
                }

                string tableColumnName = string.Join(splitter, tableColumnsName);
                string tableColumnType = string.Join(splitter, tableColumnsType);
                string tableColumnvalue = string.Join(splitter, tableColumnsvalue);

                BlServices blServices = new BlServices();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.ObjectName = procedureName;
                blServices.ObjectParameters = tableColumnName;
                blServices.ObjectArguments = tableColumnvalue;

                blServices.ObjectDataTypes = tableColumnType;
                blServices.ObjectSplitChar = splitter;
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsDataTable");

                APIOutputDynamic<T> root = new APIOutputDynamic<T>();
                if (datresponse.IsSuccessStatusCode)//api calll from here to apiservice is successful
                {
                    root = JsonConvert.DeserializeObject<APIOutputDynamic<T>>(datresponse.Content.ReadAsStringAsync().Result);
                    string Val = root.StatusCode.ToString();
                    Log log = new Log();
                    log.WriteLog("0API"+Val, false);
                    if (root.StatusCode == 0)
                    {
                        //datatable probably has atleast one row
                        return new APIGetRecordsDynamic<T>
                        {
                            Process = new Output { IsProcess=true, Message = new List<string> { "Success" }, Status ="OK" },
                            Data=root.dtable
                        };
                    }
                    else
                    {
                        //datatable dont have any row
                        return new APIGetRecordsDynamic<T>
                        {
                            Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 2).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType }
                        };
                    }
                }

                
                return new APIGetRecordsDynamic<T> {
                    Process= new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType }
                };
            }
            catch (Exception ex)
            {
                return new APIGetRecordsDynamic<T>
                {
                    Process= new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType }
                };
            }
        }
        public static string WriteToXml(ArrayList arlist)
        {
            StringBuilder strxml = new StringBuilder();
            try
            {
                strxml.Append("<root>");
                for (int i = 0; i < arlist.Count; i++)
                {
                    strxml.Append(ToXml(arlist[i]));
                    string a = arlist[i].ToString();
                }
                strxml.Append("</root>");
            }
            catch (Exception)
            {
                throw;
            }
            return strxml.ToString();

        }

        private static string ToXml(Object obj)
        {

            StringWriter Output = new StringWriter(new StringBuilder());
            string Ret = String.Empty;

            try
            {
                XmlSerializer s = new XmlSerializer(obj.GetType());
                s.Serialize(Output, obj);

                // To cut down on the size of the xml being sent to the database, we'll strip
                // out this extraneous xml.

                Ret = Output.ToString().Replace("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                Ret = Ret.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
                Ret = Ret.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "").Trim();
            }
            catch (Exception) { throw; }

            return Ret;
        }
        public class root<T>
        {
            public List<T> data { get; set; }
        }
        public static List<pssOtherCharge> GetOtherCharges(string TransMode)
        {
            List<pssOtherCharge> lstOtherCharges = null;
            string strSessionValue = "pssOtherCharge-" + TransMode;
            if (HttpContext.Current.Session[strSessionValue] != null)
            {
                lstOtherCharges = (List<pssOtherCharge>)HttpContext.Current.Session[strSessionValue];
            }
            return lstOtherCharges;
        }
        public static List<pssOtherChargeTax> GetOtherChargeTax(string TransMode)
        {
            List<pssOtherChargeTax> lstOtherCharges = null;
            string strSessionValue = "pssOtherChargeTax-" + TransMode;
            if (HttpContext.Current.Session[strSessionValue] != null)
            {
                lstOtherCharges = (List<pssOtherChargeTax>)HttpContext.Current.Session[strSessionValue];
            }
            return lstOtherCharges;
        }
        public static void ClearOtherCharges(string TransMode)
        {
            HttpContext.Current.Session.Remove("pssOtherCharge-" + TransMode);
            HttpContext.Current.Session.Remove("pssOtherChargeTax-" + TransMode);
        }
        public static void fillOtherCharges(string TransMode,long FK_Transaction)
        {
            OtherChargesModel Model = new OtherChargesModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)HttpContext.Current.Session["UserLoginInfo"];
            var OtherChargeList = Model.GetOtherChargeList(input: new OtherChargesModel.GetOtherCharge
            {
                TransMode = TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Transaction = FK_Transaction
            }, companyKey: _userLoginInfo.CompanyKey);
            var OtherChargeTaxList = Model.GetOtherChargeTaxDataList(input: new OtherChargesModel.GetOtherChargeTaxData
            {
                TransMode = TransMode,
                FK_Transaction = FK_Transaction
            }, companyKey: _userLoginInfo.CompanyKey);

            if (OtherChargeList.Data != null)
            {
                string strOtherCharge = "pssOtherCharge-" + TransMode;
                List<pssOtherCharge> lstOC = new List<pssOtherCharge>();
                foreach (var row in OtherChargeList.Data)
                {
                    pssOtherCharge lst = new pssOtherCharge();
                    lst.ID_OtherChargeType = row.ID_OtherChargeType;
                    lst.OctyTransType = row.OctyTransType;
                    lst.FK_TaxGroup = row.FK_TaxGroup;
                    lst.OctyAmount = row.OctyAmount;
                    lst.OctyTaxAmount = row.OctyTaxAmount;
                    lst.OctyIncludeTaxAmount = row.OctyIncludeTaxAmount;
                    lst.OctranRemarks = row.OctranRemarks;
                    lstOC.Add(lst);
                }
                HttpContext.Current.Session[strOtherCharge] = lstOC;
            }
            if (OtherChargeTaxList.Data != null)
            {
                string strOtherChargeTax = "pssOtherChargeTax-" + TransMode;
                List<pssOtherChargeTax> lstOCT = new List<pssOtherChargeTax>();
                foreach (var row in OtherChargeTaxList.Data)
                {
                    pssOtherChargeTax lst = new pssOtherChargeTax();
                    lst.ID_OtherChargeType = row.ID_OtherChargeType;
                    lst.ID_TaxSettings = row.ID_TaxSettings;
                    lst.Amount = row.TaxAmount;
                    lst.TaxPercentage = row.TaxPercentage;
                    lst.TaxGrpID = row.TaxGrpID;
                    lst.FK_TaxType = row.FK_TaxType;
                    lst.TaxTyName = row.TaxtyName;
                    lstOCT.Add(lst);
                }
                HttpContext.Current.Session[strOtherChargeTax] = lstOCT;
            }
        }
        public static void SetCorrectionData(string TransMode,string FK_Master)
        {
            string strSessionValue= "pssCorrectionData-" + TransMode;
            HttpContext.Current.Session[strSessionValue] = FK_Master;
        }
        public static string GetCorrectionData(string TransMode)
        {
            string strSessionValue = "pssCorrectionData-" + TransMode;
            string result = "";
            if (HttpContext.Current.Session[strSessionValue] != null)
            {
                result=HttpContext.Current.Session[strSessionValue].ToString();
            }
            return result;
        }
        public static void ClearCorrectionData(string TransMode)
        {
            HttpContext.Current.Session.Remove("pssCorrectionData-" + TransMode);           
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
        //------test

        //call a procedure to get a list of data
        public static APIGetRecordsDynamicdn<dynamic> GetDataViaProcedureDynamic<U>(string companyKey, string procedureName, U parameter)
        {
            //custom error set in General/ErrorMessages.Json File

            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {
                string splitter = "|";

                List<string> tableColumnsName = new List<string>();
                List<string> tableColumnsType = new List<string>();
                List<string> tableColumnsvalue = new List<string>();

                foreach (var prop in parameter.GetType().GetProperties())
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    string type = propType.Name;

                    tableColumnsName.Add("@" + prop.Name);
                    tableColumnsType.Add(type);
                    tableColumnsvalue.Add(prop.GetValue(parameter, null)?.ToString());
                }

                string tableColumnName = string.Join(splitter, tableColumnsName);
                string tableColumnType = string.Join(splitter, tableColumnsType);
                string tableColumnvalue = string.Join(splitter, tableColumnsvalue);

                BlServices blServices = new BlServices();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.ObjectName = procedureName;
                blServices.ObjectParameters = tableColumnName;
                blServices.ObjectArguments = tableColumnvalue;

                blServices.ObjectDataTypes = tableColumnType;
                blServices.ObjectSplitChar = splitter;
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsDataTable");
             
                //APIOutputDynamic<IEnumerable<JObject>> root = new APIOutputDynamic<IEnumerable<JObject>>();
                APIOutputDynamic<dynamic> root = new APIOutputDynamic<dynamic>();

               dynamic jsonObject = root;

                if (datresponse.IsSuccessStatusCode)//api calll from here to apiservice is successful
                {



                    jsonObject = datresponse.Content.ReadAsStringAsync().Result;
                    root = JsonConvert.DeserializeObject<APIOutputDynamic<dynamic>>(jsonObject);



                    if (root.StatusCode == 0)
                    {

                        return new APIGetRecordsDynamicdn<dynamic>
                        {
                            Process = new Output { IsProcess = true, Message = new List<string> { "Success" }, Status = "OK" },
                            Data = jsonObject
                        };
                    }
                    else
                    {
                        //datatable dont have any row
                        return new APIGetRecordsDynamicdn<dynamic>
                        {
                            Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 2).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType }
                        };
                    }
                }


                return new APIGetRecordsDynamicdn<dynamic>
                {
                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType }
                };
            }
            catch (Exception ex)
            {
                return new APIGetRecordsDynamicdn<dynamic>
                {
                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType }
                };
            }
        }

        public static APIGetRecordsDynamicdn<DataTable>  GetDataViaProcedureDatatable<U>(string companyKey, string procedureName, U parameter)
        {
            //custom error set in General/ErrorMessages.Json File

            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {
                string splitter = "|";

                List<string> tableColumnsName = new List<string>();
                List<string> tableColumnsType = new List<string>();
                List<string> tableColumnsvalue = new List<string>();

                foreach (var prop in parameter.GetType().GetProperties())
                {
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    string type = propType.Name;

                    tableColumnsName.Add("@" + prop.Name);
                    tableColumnsType.Add(type);
                    tableColumnsvalue.Add(prop.GetValue(parameter, null)?.ToString());
                }

                string tableColumnName = string.Join(splitter, tableColumnsName);
                string tableColumnType = string.Join(splitter, tableColumnsType);
                string tableColumnvalue = string.Join(splitter, tableColumnsvalue);

                BlServices blServices = new BlServices();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.ObjectName = procedureName;
                blServices.ObjectParameters = tableColumnName;
                blServices.ObjectArguments = tableColumnvalue;

                blServices.ObjectDataTypes = tableColumnType;
                blServices.ObjectSplitChar = splitter;
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsDataTable");


              //  APIOutputDynamic<DataTable> output = new APIOutputDynamic<DataTable>();
               




                if (datresponse.IsSuccessStatusCode)//api calll from here to apiservice is successful
                {
                    rootProcedureOutputAsDataTable root = new rootProcedureOutputAsDataTable();


                    root = datresponse.Content.ReadAsAsync<rootProcedureOutputAsDataTable>().Result;






                    if (root.StatusCode == 0)
                    {

                        return new APIGetRecordsDynamicdn<DataTable>
                        {
                            Process = new Output { IsProcess = true, Message = new List<string> { "Success" }, Status = "OK" },
                            Data = root.dtable
                        };
                    }
                    else if (root.dtable!=null)
                    {
                        root.dtable.Rows.RemoveAt(0);
                        return new APIGetRecordsDynamicdn<DataTable>
                        {
                            Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 2).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType },
                            Data = root.dtable
                        };
                    }
                    else
                    {
                        //datatable dont have any row
                        return new APIGetRecordsDynamicdn<DataTable>
                        {
                            Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 2).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType },

                        };
                    }
                }


                return new APIGetRecordsDynamicdn<DataTable>
                {
                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType }
                };
            }
            catch (Exception ex)
            {
                return new APIGetRecordsDynamicdn<DataTable>
                {
                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType }
                };
            }
        }
        //public static APIGetRecordsDynamicdn<DataSet> GetDataViaProcedureDataSet<U>(string companyKey, string procedureName, U parameter)
        //{
        //    //custom error set in General/ErrorMessages.Json File

        //    List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
        //    using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
        //    {
        //        _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
        //    }
        //    try
        //    {
        //        string splitter = "|";

        //        List<string> tableColumnsName = new List<string>();
        //        List<string> tableColumnsType = new List<string>();
        //        List<string> tableColumnsvalue = new List<string>();

        //        foreach (var prop in parameter.GetType().GetProperties())
        //        {
        //            var propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        //            string type = propType.Name;

        //            tableColumnsName.Add("@" + prop.Name);
        //            tableColumnsType.Add(type);
        //            tableColumnsvalue.Add(prop.GetValue(parameter, null)?.ToString());
        //        }

        //        string tableColumnName = string.Join(splitter, tableColumnsName);
        //        string tableColumnType = string.Join(splitter, tableColumnsType);
        //        string tableColumnvalue = string.Join(splitter, tableColumnsvalue);

        //        BlServices blServices = new BlServices();
        //        blServices.DbName = "PERFECTERP" + companyKey;
        //        blServices.ObjectName = procedureName;
        //        blServices.ObjectParameters = tableColumnName;
        //        blServices.ObjectArguments = tableColumnvalue;

        //        blServices.ObjectDataTypes = tableColumnType;
        //        blServices.ObjectSplitChar = splitter;
        //        HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsDataSet");


        //        //  APIOutputDynamic<DataTable> output = new APIOutputDynamic<DataTable>();





        //        if (datresponse.IsSuccessStatusCode)//api calll from here to apiservice is successful
        //        {
        //            rootProcedureOutputAsDataSet root = new rootProcedureOutputAsDataSet();


        //            root = datresponse.Content.ReadAsAsync<rootProcedureOutputAsDataSet>().Result;






        //            if (root.StatusCode == 0)
        //            {

        //                return new APIGetRecordsDynamicdn<DataSet>
        //                {
        //                    Process = new Output { IsProcess = true, Message = new List<string> { "Success" }, Status = "OK" },
        //                    Data = root.dataSet
        //                };
        //            }
        //            else if (root.dataSet != null)
        //            {
        //                root.dataSet.Tables[0].Rows.RemoveAt(0);
        //                root.dataSet.Tables[1].Rows.RemoveAt(0);
        //                return new APIGetRecordsDynamicdn<DataSet>
        //                {
        //                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 2).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType },
        //                    Data = root.dataSet
        //                };
        //            }
        //            else
        //            {
        //                //datatable dont have any row
        //                return new APIGetRecordsDynamicdn<DataSet>
        //                {
        //                    Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 2).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 2).ErrorType },

        //                };
        //            }
        //        }


        //        return new APIGetRecordsDynamicdn<DataSet>
        //        {
        //            Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType }
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new APIGetRecordsDynamicdn<DataSet>
        //        {
        //            Process = new Output { IsProcess = false, Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage }, Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType }
        //        };
        //    }
        //}



        //-----  Common function to send the Mobile notification

        #region Mobile notification to fire base
        public class SendMobileNotificationModel
        {
            public string BankKey { get; set; }
        }
    
        /* how to call --> Common.SendMobileNotification(companyKey: _companyKey);*/
        public static Output SendMobileNotification(string companyKey)
        {
            //custom error set in General/ErrorMessages.Json File
            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {

                SendMobileNotificationModel sendMobileNotification = new SendMobileNotificationModel();


                sendMobileNotification.BankKey = companyKey;
                HttpResponseMessage datresponse = APIServices.CallAPiService(sendMobileNotification, "api/MobileNotification/SendAllNotification");//<-- send data to the api controller


                PerfectWebERPAPI.Business.BlMobileNotificationModel.RootSendNotification root = new PerfectWebERPAPI.Business.BlMobileNotificationModel.RootSendNotification();
                if (datresponse.IsSuccessStatusCode)//api call from here to apiservice is successful
                {
                    root = JsonConvert.DeserializeObject<PerfectWebERPAPI.Business.BlMobileNotificationModel.RootSendNotification>(datresponse.Content.ReadAsStringAsync().Result);

                    if (root.StatusCode == 0)// api call from api service to db is successful
                    {


                    }

                }

                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType
                };
            }
            catch (Exception ex)
            {
                //Exception
                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType
                };
            }
        }
        #endregion

        //----- Common Function to Send email with Attachment
        #region [SendMailwithAttachment]

        // public static APIGetRecordsDynamicdn<DataTable> GetDataViaProcedureDatatable<U>(string companyKey, string procedureName, U parameter)
        public static Output SendMailwithAttachment(string companyKey)
        {
            List<CustomErrorMessage> _errorMessages = new List<CustomErrorMessage>();
            using (StreamReader sr = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/General/ErrorMessages.json")))
            {
                _errorMessages = JsonConvert.DeserializeObject<List<CustomErrorMessage>>(sr.ReadToEnd());
            }
            try
            {

                SendMobileNotificationModel sendMobileNotification = new SendMobileNotificationModel();


                //sendMobileNotification.BankKey = companyKey;

                EmialWithAttachmentAPIDTO blServices = new EmialWithAttachmentAPIDTO();
                blServices.DbName = "PERFECTERP" + companyKey;
                blServices.RecipientEmail = new string[] { "developer.gazeeb@gmail.com", "" };
                blServices.senderName = "";
                blServices.FilePath = "";

                //HttpResponseMessage datresponse = APIServices.CallAPiService(sendMobileNotification, "api/MobileNotification/SendAllNotification");//<-- send data to the api controller
                HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Email/EmailWithAttachment");

                PerfectWebERPAPI.Business.BlMobileNotificationModel.RootSendNotification root = new PerfectWebERPAPI.Business.BlMobileNotificationModel.RootSendNotification();
                if (datresponse.IsSuccessStatusCode)//api call from here to apiservice is successful
                {
                    root = JsonConvert.DeserializeObject<PerfectWebERPAPI.Business.BlMobileNotificationModel.RootSendNotification>(datresponse.Content.ReadAsStringAsync().Result);

                    if (root.StatusCode == 0)// api call from api service to db is successful
                    {


                    }

                }

                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 3).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 3).ErrorType
                };
            }
            catch (Exception ex)
            {
                //Exception
                return new Output
                {
                    IsProcess = false,
                    Message = new List<string> { _errorMessages.Find(a => a.ErrorCode == 1).ErrorMessage },
                    Status = _errorMessages.Find(a => a.ErrorCode == 1).ErrorType
                };
            }
            


               
        }


        #endregion

    }

   
}


