using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class IncentiveSettingsModal
    {
        public class IncentiveSettingsList
        {
           public List<DesignationList> DesignationList { get; set; }
            public List<Services> ServiceList { get; set; }
            public List<CategoryList> CategoryList { get; set; }
        }
        public class DesignationList
        {
            public long DesignationID { get; set; }
            public string Designation { get; set; }

        }
        public class CategoryList
        {
            public Int32 CategoryID { get; set; }
            public string Category { get; set; }

        }
        public class Services
        {
            public Int16 ServiceID { get; set; }
            public string ServiceList { get; set; }
        }
        public class Products
        {
            public int PrdID { get; set; }
            public string Product { get; set; }
            // public long FK_Employee{ get; set; }

        }
        public class Service
        {

            public long ID_Services { get; set; }
            public string ServicesName { get; set; }
        }

        public class inputdata {
            public Int64 Designation { get; set; }
            public Int64 Employee { get; set; }
           
            public string EntrBy { get; set; }
            public DateTime EffectDate { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 AccountHeadSub { get; set; }
            public Int64 AccountHead { get; set; }
            public bool IsActive { get; set; }


            public List<SubIncentive> SubIncentiveData {get; set;}
            public Int64 ID_ServiceIncentiveSettings { get; set; }
        }
        public class inputdatalist
        {
            public Int64  FK_Designation { get; set; }
            public Int64 FK_Employee { get; set; }

            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public string SubIncentiveData { get; set; }
            public DateTime EffectDate { get; set; }
            public int UserAction{get; set;}
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_ServiceIncentiveSettings { get; set; }
            public Int64 ID_ServiceIncentiveSettings { get; set; }
            public Int64 FK_AccountSubHead { get; set; }
            public Int64 FK_AccountHead { get; set; }
            public bool IsActive { get; set; }


        }
        public class SubIncentive
        {
            public long FK_Category { get; set; }
            public long Commitiontype { get; set; }
            public long FK_Service { get; set; }
            public string Amount { get; set; }
            public long FK_Product { get; set; }
        }
        public class outputDto
        {

        }
        public class IncentiveSettingsListDatainputModal {
     
            public string TransMode { get; set; }
            public Int64 FK_ServiceIncentiveSettings { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
           
            public bool Detailed { get; set; }
            public string Name { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
           
        }
        public class IncentiveSettingsDeleateinputModal
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 ID_IncentiveTransaction { get; set; }
        }
        public class GetDataOut
        {
            public int SlNo { get; set; }
            public Int64 ID_ServiceIncentiveSettings { get; set; }
            public DateTime EffectDate { get; set; }
            public Int64 FK_Designation { get; set; }
            public string Designation { get; set; }
            public Int64 FK_Employee { get; set; }
            public string Employee { get; set; }
            public Int64 TotalCount { get; set;}

            public Int64 FK_AccountSubHead { get; set; }
            public Int64 FK_AccountHead { get; set; }
            public bool IsActive { get; set; }
            public string AccountSubHead { get; set; }
            public string AccountHead { get; set; }

    }
        public class tableDataOutput
        {
            public string Product { get; set; }
            public string Services { get; set; }
            public string Category { get; set; }
            public float? Amount { get; set; }
            public float? Percentage { get; set; }
            public int Commitiontype { get; set; }
            public Int64 FK_Service { get; set; }
            public Int64 FK_Product { get; set; }
            public Int64 FK_Category { get; set; }
            public Int64 FK_ServiceIncentiveSettings { get; set; }
            public Int64 ID_ServiceIncentiveSettingsDetails { get; set; }
        }
        public Output Saveincetivesetting(inputdatalist input, string companyKey)
        {
            return Common.UpdateTableData< inputdatalist>(companyKey: companyKey, procedureName: "ProServiceIncentiveSettingsUpdate", parameter: input);
        }
        public APIGetRecordsDynamic<GetDataOut> IncentiveSettingsListData(IncentiveSettingsListDatainputModal input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetDataOut, IncentiveSettingsListDatainputModal>(companyKey: companyKey, procedureName: "ProServiceIncentiveSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<tableDataOutput> IncentiveSettingstableData(IncentiveSettingsListDatainputModal input, string companyKey)
        {
            return Common.GetDataViaProcedure<tableDataOutput, IncentiveSettingsListDatainputModal>(companyKey: companyKey, procedureName: "ProServiceIncentiveSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<outputDto> IncentiveSettingsDealte(IncentiveSettingsDeleateinputModal input, string companyKey)
        {
            return Common.GetDataViaProcedure<outputDto, IncentiveSettingsDeleateinputModal>(companyKey: companyKey, procedureName: "ProServiceIncentiveSettingsDelete", parameter: input);
        }
    }
}