using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class CommonModel
    {
        public class SideMenuList
        {
            public IEnumerable<MenuGroup> SideMenu { get; set; }
        }
        public class MainMenuList
        {
            public IEnumerable<MenuList> MainMenu { get; set; }
            public string HeaderName { get; set; }
        }
        public class MenuList
        {
            public long MenuListID { get; set; }
            public string MenuListName { get; set; }
            public string MenuListLink { get; set; }
            public string TransMode { get; set; }
            public long MenuGroupID { get; set; }
            public bool MenuListVisible { get; set; }
            public string MenuGroupName { get; set; }
            public string MenuGroupSubName { get; set; }
            public long SortOrder { get; set; }
            //----
            public string MenuIcon { get; set; }
            public string MenuIconImage { get; set; }
            public long SubList { get; set; }
            public string MenuListPara { get; set; }
            public string ControllerName { get; set; }
            public string ActionData { get; set; }
            public string TransKey { get; set; }

        }
        public class SearchMenuList
        {
            public long SlNo { get; set; }
            public long MenuListID { get; set; }
            public string MenuListName { get; set; }           
            public string TransMode { get; set; }           
            public string MenuGroupName { get; set; }          
            public string MenuIconImage { get; set; }         
            public string ControllerName { get; set; }
            public string ActionData { get; set; }
            public string TransKey { get; set; }
            public string MenuListLink { get; set; }
            public long FK_MenuGroup { get; set; }
        }
        public class MenuGroup
        {
            public long MenuGroupID { get; set; }
            public string MenuGroupName { get; set; }
            public List<MenuGroupSub> SubModule { get; set; }          
            public bool MenuGroupVisible { get; set; }
            public long SortOrder { get; set; }
            public long ParentGroupID { get; set; }
            public string MenuIconImage { get; set; }
        }
        public class GetMenuGroup
        {
            public long FK_UserGroup { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class GetMainMenu
        {
            public long FK_UserGroup { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_MenuGroup { get; set; }
            public long FK_SubMenu { get; set; }

        }
        public class MenuGroupSub
        {
            public long MenuGroupSubID { get; set; }
            public string MenuGroupSubName { get; set; }
            // public List<MenuGroupSub> SubModule { get; set; }
            public string SubModule { get; set; }
            public bool MenuGroupSubVisible { get; set; }
            public long SortOrder { get; set; }
            public long ParentGroupID { get; set; }
            public string MenuIconImage { get; set; }
        }

        
        public class DashBoardList
        {
           
            public string UserCode { get; set; }
            public long BranchCode { get; set; }
            public long BranchType { get; set; }


        }
        public class DashBoardListOut
        {

            public decimal Service { get; set; }
            public decimal Production { get; set; }
            public decimal Lead { get; set; }
            public decimal Customer { get; set; }
        }

        public class DashBoardGraphList
        {

            public long FK_Employee { get; set; }
            public long FK_Departmnet { get; set; }

            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Mode { get; set; }


        }
        public class DashBoardGraphOut
        {
            public string Field { get; set; }
            public decimal Value { get; set; }           
        }

        public class MainPrjectDash
        {
            public string ProjectName { get; set; }
            public string ShortName { get; set; }
            public decimal Count { get; set; }
            public List<ProjectDashBoardGraphOut> StageDetails { get; set; }
        }

        public class ProjectDashBoardGraphOut
        {

            //public long Leads { get; set; }
            //public long Products { get; set; }
            public string Project { get; set; }
            public string ShortName { get; set; }
            public string Stage { get; set; }
            public string CurrentStatus { get; set; }
            
            public decimal Percentage { get; set; }
            public string Field { get; set; }
            public decimal Value { get; set; }
           
        }
        public class DashserviceBoardGraphOut
        {

            //public long Leads { get; set; }
            //public long Products { get; set; }
            public string Field { get; set; }
            public decimal Value { get; set; }
            //public long Cool { get; set; }
            //public long TodaysPending  { get; set; }
            //public long Overdue { get; set; }
            //public long UpComingWorks { get; set; }
        }
        public class DashBoardServiceGraphList
        {

            public long FK_Employee { get; set; }
            public long FK_Departmnet { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Mode { get; set; }


        }
        public class DashBoardInventoryGraphList
        { 
            public long FK_Department { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public string UserCode { get; set; }
          //  public long FK_Machine { get; set; }
            public long Month { get; set; }
            public long Year { get; set; }

        }
        public class DashInventoryBoardGraphOut
        {
            public string Field { get; set; }
            public decimal Value { get; set; }
            public int DashBoard { get; set; }
            //public List<Months> MonthList { get; set; }
            //public List<Years>  YearList { get; set; }
            //public List<Types> TypeList { get; set; }
            //public List<GraphModels> GraphModelList { get; set; }
        }
        public class Months
        {
            public long MonthId { get; set; }
            public string Month { get; set; }
        }
        public class Years
        {
            public long YearId { get; set; }
            public long Year { get; set; }
        }
        public class Types
        {
            public long TypeId { get; set; }
            public string Type { get; set; }
        }
        public class GraphModels
        {
            public long GraphModelId { get; set; }
            public string GraphModel { get; set; }
        }

        public class GetGeneralSettings
        {          
            public long FK_Company { get; set; }
            public string GsModule { get; set; }
        }
        public class GeneralSettingsData
        {
            public string GsField { get; set; }
            public bool GsValue { get; set; }
            public string GsData { get; set; }
        }
        public class GetFromNotficationMenu
        {
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
        }
        public class FromNotficationMenu
        {
            public string MnuLstName { get; set; }
            public string ControllerName { get; set; }
            public string Url { get; set; }
          
        }
        public APIGetRecordsDynamic<GeneralSettingsData> GetGeneralSettingsData(GetGeneralSettings input, string companyKey)
        {
            return Common.GetDataViaProcedure<GeneralSettingsData, GetGeneralSettings>(companyKey: companyKey, procedureName: "ProGetGeneralSettings", parameter: input);
        }
        public APIGetRecordsDynamic<DashBoardListOut> GetCountData(DashBoardList input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashBoardListOut, DashBoardList>(companyKey: companyKey, procedureName: "ProAdminDashBoardCount", parameter: input);

        }


        public APIGetRecordsDynamic<DashBoardGraphOut> GetGraphData(DashBoardGraphList input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashBoardGraphOut, DashBoardGraphList>(companyKey: companyKey, procedureName: "ProLeadGenerateDashBoard", parameter: input);

        }

        public APIGetRecordsDynamic<DashserviceBoardGraphOut> GetServiceGraphData(DashBoardGraphList input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashserviceBoardGraphOut, DashBoardGraphList>(companyKey: companyKey, procedureName: "ProCustomerServiceDashBoard", parameter: input);

        }

        public APIGetRecordsDynamic<MenuGroup> GetMenuGroupData(GetMenuGroup input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuGroup, GetMenuGroup>(companyKey: companyKey, procedureName: "ProMenuGroupForMenuSelect", parameter: input);
        }
        public APIGetRecordsDynamic<MenuList> GetMainMenuData(GetMainMenu input, string companyKey)
        {
            return Common.GetDataViaProcedure<MenuList, GetMainMenu>(companyKey: companyKey, procedureName: "ProMenuListForMenuSelect", parameter: input);
        }
     
        public APIGetRecordsDynamic<ProjectDashBoardGraphOut> GetProjectGraphData(DashBoardGraphList input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProjectDashBoardGraphOut, DashBoardGraphList>(companyKey: companyKey, procedureName: "ProProjectDashBoard", parameter: input);

        }
       
        public APIGetRecordsDynamic<DashInventoryBoardGraphOut> GetInventoryGraphData(DashBoardInventoryGraphList input, string companyKey)
        {
            return Common.GetDataViaProcedure<DashInventoryBoardGraphOut, DashBoardInventoryGraphList>(companyKey: companyKey, procedureName: "ProInventoryDashBoard", parameter: input);
        }
        public APIGetRecordsDynamic<FromNotficationMenu> GetMenuFromNotification(GetFromNotficationMenu input, string companyKey)
        {
            return Common.GetDataViaProcedure<FromNotficationMenu, GetFromNotficationMenu>(companyKey: companyKey, procedureName: "ProMenuDataSelectNotification", parameter: input);
        }

        public class PageInput
        {
            public string MenuName { get; set; }

            public string TransMode { get; set; }

            public long FK_Company { get; set; }
            public long FK_Machine {get; set; }
            public long FK_BranchCodeUser { get; set; }

            public string EntrBy { get; set; }

            public List<PageDetails> PageDetails { get; set; }
            public long FK_UserGroup { get; set; }
            public long ID_MenuList { get; set; }
        }
        public class PageDetails
        {
            public string MnuLstName { get; set; }
            public string MnuLstLinkName { get; set; }
            public string ControllerName { get; set; }
            public string Url { get; set; }
        }

        public APIGetRecordsDynamic<SearchMenuList> GetPages(PageInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<SearchMenuList, PageInput>(companyKey: companyKey, procedureName: "ProGetPages", parameter: input);

        }
    }
    public class Output
    {
        public bool IsProcess { get; set; }
        public List<string> Message { get; set; }
        public string Status { get; set; }
        public int? code { get; set; }

    }
    // this Model is used to receive API response
    public class APIGetRecordsDynamic<T>
    {
        public Output Process { get; set; }
        public List<T> Data { get; set; }
    }
    public class APIGetRecordsDynamicMulti<T>
    {
        public Output Process { get; set; }
        public DataSet Data { get; set; }
    }
    public class APIGetRecordsDynamicdn<T>
    {
        public Output Process { get; set; }
        public dynamic Data { get; set; }
    }
    public class APIParameters
    {
        public string TableName { get; set; }
        public string SelectFields { get; set; }
        public string SortFields { get; set; }
        public string Criteria { get; set; }
        public string GroupByFileds { get; set; }
    }

    public class APIOutputDynamic<T>
    {
        public List<T> dtable { get; set; }
       
        public int StatusCode { get; set; }
        public string EXMessage { get; set; }
    }
    public class APIOutputDynamicMulti<T>
    {
        public DataSet dtable { get; set; }
       
        public int StatusCode { get; set; }
        public string EXMessage { get; set; }
    }
    public class CustomErrorMessage
    {
        public int ErrorCode { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class NextSortOrder
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public byte Debug { get; set; }
        public long FK_Company { get; set; }
    }
    public class NextSortOrderOutput
    {
        public int NextNo { get; set; }
    }

    public class PageAccessRights
    {
        public string MasterName { get; set; }
        public bool HasDeleteRight { get; set; }
        public bool HasViewRight { get; set; }
        public bool HasUpdateRight { get; set; }
        public bool HasAddRight { get; set; }
    }

    public class ReturnSendmailwithAttachment
    {
        public string Data { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    
    public class EmialWithAttachmentAPIDTO
    {

        public string DbName { get; set; }
        public string TableName { get; set; }

        public string Module { get; set; }
        public Int32 FkCompany{ get; set; }
        public Int16 BranchCodeUser{get; set;}
        public string UserCode{get; set;}
        public string ObjectName{get; set;}
        public string ObjectParameters{get; set;}
        public string ObjectArguments{get; set;}
        public string ObjectDataTypes{get; set;}
        public string QuerytoExecute{get; set;}
        public string ObjectSplitChar{get; set;}
        public byte[] ObjectPhoto{get; set;}
        public byte[] ObjectSign{get; set;}
        public string Body{get; set;}
        public string Subject{get; set;}
        public string FilePath{get; set;}
        public string[] RecipientEmail { get; set; }
        public string senderName { get; set; }
        public string DisplayName { get; set; }
    }


}