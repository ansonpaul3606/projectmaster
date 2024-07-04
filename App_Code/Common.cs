using PerfectWebERP.Business;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PerfectWebERP.General
{
    public class Common1
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




        //public  T GettableData<T>(APIParameters parameters)
        //{
        //    string Bankkey = "";
        //    BlServices blServices = new BlServices();
        //    blServices.DbName = "PERFECTERP" + Bankkey;
        //    DataTable dt = new DataTable();


        //    blServices.ObjectName = parameters.ProcedureName;
        //    blServices.ObjectParameters = "@TableName|@ListFields|@SortField|@Criteria|@Groupby";
        //    blServices.ObjectArguments = $"{parameters.TableName}|{parameters.SelectFields}|{parameters.SortFields}|{parameters.Criteria}|{parameters.GroupByFileds}";
        //    blServices.ObjectDataTypes = "VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)|VARCHAR(MAX)";
        //    blServices.ObjectSplitChar = "|";
        //    HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Common/GetCommonProcedureOutputAsDataTable");


        //    return datresponse.Content.ReadAsAsync<T>().Result;
        //}

        public string Test()
        {return ""; }

         

        



     }
        
}