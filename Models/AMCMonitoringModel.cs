using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PerfectWebERP.Models
{
    public class AMCMonitoringModel
    {
        public class AMCMonitoringViewList
        {
            public long FK_Employee { get; set; }
            public long Branch { get; set; }
            public string BrName { get; set; }
            public string CompName { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<Branch> BranchList { get; set; }
            public List<AMCTypeModel.AMCTypeView> AMCtype { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class Branch
        {
            public long ID_Branch { get; set; }
            public string BranchName { get; set; }
        }
        public class AmcDetailsdata
        {
            public string AMCNo { get; set; }
            public long FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long FK_Area { get; set; }
            public long fk_Customer { get; set; }
            public long Demand { get; set; }
            public long FK_AMCType { get; set; }
            public long Status { get; set; }
            public Int64 Mode { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
           
        }



        public class sendItimationDetailsdata
        {
            public string AMCNo { get; set; }
            public long FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long FK_Area { get; set; }
            public long fk_Customer { get; set; }
            public long Demand { get; set; }
            public long FK_AMCType { get; set; }
            public long Status { get; set; }
            public Int64 Mode { get; set; }

            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int64 Intimation_Type { get; set; }
            public Int64 SendTo { get; set; }
        }



        public class GetAMCDetails
        {
            public string AMCNo { get; set; }
            public long FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long FK_Area { get; set; }
            public long fk_Customer { get; set; }
            public long Demand { get; set; }
            public long FK_AMCType { get; set; }
            public long Status { get; set; }
            public Int64 Mode { get; set; }

            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public Int32 SMSMode { get; set; }

        }


        public class SendIntimate
        {
            public string AMCNo { get; set; }
            public long FK_Product { get; set; }
            public Int64 FK_Branch { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Category { get; set; }
            public long FK_Area { get; set; }
            public long fk_Customer { get; set; }
            public long Demand { get; set; }
            public long FK_AMCType { get; set; }
            public long Status { get; set; }
            public Int64 Mode { get; set; }

            public Int32 SMSMode{ get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

        }
        public class AmcDetails
        {
            public long SLNo { get; set; }
            public long ID_AMCDetails { get; set; }
            public long FK_Master { get; set; }
            public long FK_Customer { get; set; }
            public string AMCNo { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public string AMCType { get; set; }
            public string Product { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime RenewDate { get; set; }

            public int Masterid { get; set; }
            public string Field { get; set; }
            public int Value { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class SendIntimationTable
        {
            public string Content { get; set; }
            public string Email_ID { get; set; }
            public string Subject { get; set; }
            public Int32 Mode { get; set; }
            public Int64? Count { get; set; }
            public Int64 FK_Master { get; set; }
        }
        public static string _SearchProcedureName = "ProAmcMonitoringList";
        public APIGetRecordsDynamic<AmcDetails> GetAmcData(GetAMCDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<AmcDetails, GetAMCDetails>(companyKey: companyKey, procedureName: _SearchProcedureName, parameter: input);
        }
      
        public APIGetRecordsDynamicdn<DataTable> SendIndimation(SendIntimate input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<SendIntimate>(companyKey: companyKey, procedureName: _SearchProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}