using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERPAPI.Interface;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using System.Data.Sql;

namespace PerfectWebERPAPI.Business
{
  public  class BlUserValidationsModel
    {
        #region CommonFunctions
        private static object locker = new Object();
       // public static void CreateIamge(List<> data)
        public static void WriteToLog(String data, string BankKey)
        {
            //string path = @"C:\MscoreLog";
            string path = @"C:\ProdSuitAPIPILog\" + BankKey;
            string FilePath = path + "\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
            lock (locker)
            {
                using (StreamWriter file = new StreamWriter(FilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString() + "::" + data);
                    file.Flush();
                    file.Close();
                }
            }
        }
        public static void CommonLog(string TransName, object obj, bool isrequest, string BankKey)
        {
            if (isrequest)
            {
                string ReqjsonParams = "Reqjsonparams::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||";
                WriteToLog(ReqjsonParams, BankKey);
                //BlCustomer objBl = (BlCustomer)obj;
                // objBl.RequestMessage = BlFormats.EncryptDataForAuthCode(ReqjsonParams);
            }
            else
            {
                string ResponseParams = "Response::" + TransName + "::" + JsonConvert.SerializeObject(obj) + "||\r\n";
                WriteToLog(ResponseParams, BankKey);
            }
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

        #endregion
    }
    #region Api ReponseModel
    public class CommonAPIResponse
    {
        public int StatusCode { get; set; }
        public string EXMessage { get; set; }
    }
    //public class RootComplaintListDetails : CommonAPIResponse
    //{
    //    public ComplaintListDetails ComplaintListDetails { get; set; }
    //}
    public class RootResellerDetails : CommonAPIResponse
    {
        public ResellerDetails ResellerDetails { get; set; }
    }
    public class RootTokencheck : CommonAPIResponse
    {
        public CommonAPIR CommonAPIR { get; set; }
    }
    public class RootUserLoginDetails : CommonAPIResponse
    {
        public UserLoginDetails UserLoginDetails { get; set; }
    }
    public class RootCommonAppChecking : CommonAPIResponse
    {
        public bool CommonApp { get; set; }
    }
    public class RootCommoncodeChecking : CommonAPIResponse
    {
        public string CommonCode { get; set; }
        public string CommonAPIURL { get; set; }
        public bool CommonAPI { get; set; }
    }
    public class RootMPIN : CommonAPIResponse
    {
        public MPINDetails MPINDetails { get; set; }
    }
    public class RootCustomerDetailsList : CommonAPIResponse
    {
        public CustomerDetailsList CustomerDetailsList { get; set; }
    }
    public class RootLeadFromDetailsList : CommonAPIResponse
    {
        public LeadFromDetailsList LeadFromDetailsList { get; set; }
    }
    public class RootLeadThroughDetailsList : CommonAPIResponse
    {
        public LeadThroughDetailsList LeadThroughDetailsList { get; set; }
    }
    public class RootAddNewCustomer:CommonAPIResponse
    {
        public AddNewCustomer AddNewCustomer { get; set; }
    }
    public class RootMaintenanceMessage : CommonAPIResponse
    {
        public MaintenanceMessage MaintenanceMessage { get; set; }
    }
    public class RootBannerDetails : CommonAPIResponse
    {
        public BannerDetails BannerDetails { get; set; }
    }
    public class RootCollectedByUsersList : CommonAPIResponse
    {
        public CollectedByUsersList CollectedByUsersList { get; set; }
    }
    public class RootCategoryDetailsList : CommonAPIResponse
    {
        public CategoryDetailsList CategoryDetailsList { get; set; }
    }
    public class RootProductDetailsList : CommonAPIResponse
    {
        public ProductDetailsList ProductDetailsList { get; set; }
    }
    public class RootStatusDetailsList : CommonAPIResponse
    {
        public StatusDetailsList StatusDetailsList { get; set; }
    }
    public class RootPriorityDetailsList : CommonAPIResponse
    {
        public PriorityDetailsList PriorityDetailsList { get; set; }
    }
    public class RootFollowUpActionDetails : CommonAPIResponse
    {
        public FollowUpActionDetails FollowUpActionDetails { get; set; }
    }
    public class RootFollowUpTypeDetails : CommonAPIResponse
    {
        public FollowUpTypeDetails FollowUpTypeDetails { get; set; }
    }
    public class RootMediaTypeDetails : CommonAPIResponse
    {
        public MediaTypeDetails MediaTypeDetails { get; set; }
    }
    public class RootSubMediaTypeDetails : CommonAPIResponse
    {
        public SubMediaTypeDetails SubMediaTypeDetails { get; set; }
    }
    public class RootReasonDetails : CommonAPIResponse
    {
        public ReasonDetails ReasonDetails { get; set; }
    }
    public class RootDepartmentDetails : CommonAPIResponse
    {
        public DepartmentDetails DepartmentDetails { get; set; }
    }
    public class RootBranchTypeDetails : CommonAPIResponse
    {
        public BranchTypeDetails BranchTypeDetails { get; set; }
    }
    public class RootBranchDetails : CommonAPIResponse
    {
        public BranchDetails BranchDetails { get; set; }
    }
    public class RootEmployeeDetails : CommonAPIResponse
    {
        public EmployeeDetails EmployeeDetails { get; set; }
    }
    public class RootEmployeeAllDetails : CommonAPIResponse
    {
        public EmployeeAllDetails EmployeeAllDetails { get; set; }
    }
    public class RootLeadManagementDetailsList:CommonAPIResponse
    {
        public LeadManagementDetailsList LeadManagementDetailsList { get; set; }
    }
    public class RootRoportSettingsList : CommonAPIResponse
    {
        public RoportSettingsList RoportSettingsList { get; set; }
    }
    public class RootGeneralReport : CommonAPIResponse
    {
        public GeneralReport GeneralReport { get; set; }
    }
    public class RootLeadHistoryDetails : CommonAPIResponse
    {
        public LeadHistoryDetails LeadHistoryDetails { get; set; }
    }
    public class RootLeadInfoetails : CommonAPIResponse
    {
        public LeadInfoDetails LeadInfoDetails { get; set; }
    }
    public class RootLeadImageDetails : CommonAPIResponse
    {
        public LeadImageDetails LeadImageDetails { get; set; }
    }
    public class RootNotificationDetails : CommonAPIResponse
    {
        public NotificationDetails NotificationDetails { get; set; }
    }
    public class RootUpdateLeadGenerateAction : CommonAPIResponse
    {
        public UpdateLeadGenerateAction UpdateLeadGenerateAction { get; set; }
    }
    public class RootPincodeDetails:CommonAPIResponse
    {
        public PincodeDetails PincodeDetails { get; set; }
    }
    public class RootCountryDetails:CommonAPIResponse
    {
        public CountryDetails CountryDetails { get; set; }
    }
    public class RootStatesDetails : CommonAPIResponse
    {
        public StatesDetails StatesDetails { get; set; }
    }
    public class RootDistrictDetails : CommonAPIResponse
    {
        public DistrictDetails DistrictDetails { get; set; }
    }
    public class RootAreaDetails : CommonAPIResponse
    {
        public AreaDetails AreaDetails { get; set; }
    }
    public class RootPostDetails : CommonAPIResponse
    {
        public PostDetails PostDetails { get; set; }
    }
    public class RootAddAgentCustomerRemarks:CommonAPIResponse
    {
        public AddAgentCustomerRemarks AddAgentCustomerRemarks { get; set; }
    }
    public class RootUpdateLeadGeneration:CommonAPIResponse
    {
        public UpdateLeadGeneration UpdateLeadGeneration { get; set; }
    }
    public class RootLeadGenerationDetails:CommonAPIResponse
    {
        public LeadGenerationDetails LeadGenerationDetails { get; set; }
    }
    public class RootLeadGenerationListDetails : CommonAPIResponse
    {
        public LeadGenerationListDetails LeadGenerationListDetails { get; set; }
    }
    public class RootLeadsDashBoardDetails : CommonAPIResponse
    {
        public LeadsDashBoardDetails LeadsDashBoardDetails { get; set; }
    }
    public class RootAddQuatation:CommonAPIResponse
    {
        public AddQuatation AddQuatation { get; set; }
    }
    public class RootAddDocument : CommonAPIResponse
    {
        public AddDocument AddDocument { get; set; }
    }
    public class RootDateWiseExpenseDetails : CommonAPIResponse
    {
        public DateWiseExpenseDetails DateWiseExpenseDetails { get; set; }
    }
    public class RootExpenseType : CommonAPIResponse
    {
        public ExpenseType ExpenseType { get; set; }
    }
    public class RootUpdateExpenseDetails : CommonAPIResponse
    {
        public UpdateExpenseDetails UpdateExpenseDetails { get; set; }
    }
    public class RootPendingCountDetails:CommonAPIResponse
    {
        public PendingCountDetails PendingCountDetails { get; set; }
    }
    public class RootActionType:CommonAPIResponse
    {
        public ActionType ActionType { get; set; }
    }
    public class RootAgendaDetails:CommonAPIResponse
    {
        public AgendaDetails AgendaDetails { get; set; }
    }
    public class RootEmployeeProfileDetails:CommonAPIResponse
    {
        public EmployeeProfileDetails EmployeeProfileDetails { get; set; }
    }
    public class RootUpdateUserLoginStatus:CommonAPIResponse
    {
        public UpdateUserLoginStatus UpdateUserLoginStatus { get; set; }
    }
    public class RootActivitiesDetails:CommonAPIResponse
    {
        public ActivitiesDetails ActivitiesDetails { get; set; }
    }
    public class RootLeadGenerateReport:CommonAPIResponse
    {
        public LeadGenerateReport LeadGenerateReport { get; set; }
    }
    public class RootProductWiseLeadReport : CommonAPIResponse
    {
        public ProductWiseLeadReport ProductWiseLeadReport { get; set; }
    }
    public class RootPriorityWiseLeadReport:CommonAPIResponse
    {
        public PriorityWiseLeadReport PriorityWiseLeadReport { get; set; }
    }
    public class RootReportNameDetails : CommonAPIResponse
    {
        public ReportNameDetails ReportNameDetails { get; set; }
    }
    public class RootGroupingDetails:CommonAPIResponse
    {
        public GroupingDetails GroupingDetails { get; set; }
    }
    public class RootActionListDetailsReport : CommonAPIResponse
    {
        public ActionListDetailsReport ActionListDetailsReport { get; set; }
    }
    public class RootStatusListDetailsReport:CommonAPIResponse
    {
        public StatusListDetailsReport StatusListDetailsReport { get; set; }
    }
    public class RootNewListDetailsReport:CommonAPIResponse
    {
        public NewListDetailsReport NewListDetailsReport { get; set; }
    }
    public class RootFollowUpListDetailsReport:CommonAPIResponse
    {
        public FollowUpListDetailsReport FollowUpListDetailsReport { get; set; }
    }
    public class RootLeadGenerationDefaultvalueSettings:CommonAPIResponse
    {
        public LeadGenerationDefaultvalueSettings LeadGenerationDefaultvalueSettings { get; set; }
    }
    public class RootDeleteLeadGenerate:CommonAPIResponse
    {
        public DeleteLeadGenerate DeleteLeadGenerate { get; set; }
    }
    public class RootAgendaType:CommonAPIResponse
    {
        public AgendaType AgendaType { get; set; }
    }
    public class RootUpdateLeadManagement:CommonAPIResponse
    {
        public UpdateLeadManagement UpdateLeadManagement { get; set; }
    }
    public class RootDocumentDetails:CommonAPIResponse
    {
        public DocumentDetails DocumentDetails { get; set; }
    }
    public class RootDocumentImageDetails : CommonAPIResponse
    {
        public DocumentImageDetails DocumentImageDetails { get; set; }
    }
    public class RootAddRemark:CommonAPIResponse
    {
        public AddRemark AddRemark { get; set; }
    }
    public class RootTodoListLeadDetails:CommonAPIResponse
    {
        public TodoListLeadDetails TodoListLeadDetails { get; set; }
    }
    public class RootAppDetails : CommonAPIResponse
    {
        public string Mode { get; set; }
    }
    public class RootCompanyDetails : CommonAPIResponse
    {
        public string BankKey { get; set; }      
        public string BASE_URL { get; set; }
        public string IMAGE_URL { get; set; }
        public string FeedBackURL { get; set; }
        public string ClientName { get; set; }
    }
    public class RootCompanyLogDetails : CommonAPIResponse
    {
        public CompanyLogDetails CompanyLogDetails { get; set; }
    }
    public class RootMenuGroupDetails : CommonAPIResponse
    {
        public MenuGroupDetails MenuGroupDetails { get; set; }
    }
    public class RootGenralSettingsDetails : CommonAPIResponse
    {
        public GenralSettingsDetails GenralSettingsDetails { get; set; }
    }
    public class RootUpdateWalkingCustomer : CommonAPIResponse
    {
        public UpdateWalkingCustomer UpdateWalkingCustomer { get; set; }
    }
    public class RootWalkingCustomerDetailsList : CommonAPIResponse
    {
        public WalkingCustomerDetailsList WalkingCustomerDetailsList { get; set; }
    }
    public class RootServiceDashBoardDetails: CommonAPIResponse
    {
        public ServiceDashBoardDetails ServiceDashBoardDetails { get; set; }
    }
    public class RootEMICollectionReportCount : CommonAPIResponse
    {
        public EMICollectionReportCount EMICollectionReportCount { get; set; }
    }
    public class RootEMICollectionReport : CommonAPIResponse
    {
        public EMICollectionReport EMICollectionReport { get; set; }
    }  
    public class RootEMIAccountDetails : CommonAPIResponse
    {
        public EMIAccountDetails EMIAccountDetails { get; set; }
    }
    public class RootUpdateEMICollection: CommonAPIResponse
    {
        public UpdateEMICollection UpdateEMICollection { get; set; }
    }
    public class RootFinancePlanTypeDetails : CommonAPIResponse
    {
        public FinancePlanTypeDetails FinancePlanTypeDetails { get; set; }
    }
    public class RootAgendaCount : CommonAPIResponse
    {
        public AgendaCount AgendaCountDtls { get; set; }
    }
  
    public class RootLocationUpdate : CommonAPIResponse
    {
        public UpdateLocation UpdateLocationDetails { get; set; }
    }

    public class RootFollowupStatusUpdate : CommonAPIResponse
    {
        public UpdateFollowupStatus UpdateFollowupStatusDetails { get; set; }
    }
    public class RootWalkingCustomerEmployeeDetails : CommonAPIResponse
    {
        public EmployeeDetails EmployeeDetails { get; set; }
    }
    
    public class RootAttanceMarkingUpdate : CommonAPIResponse
    {
        public UpdateAttanceMarking UpdateAttanceMarkingDetails { get; set; }
    }
    public class RootEmployeeLocationUpdate : CommonAPIResponse
    {
        public UpdateEmployeeLocation UpdateEmployeeLocationDetails { get; set; }
    }
    public class RootNotificationUpdate : CommonAPIResponse
    {
        public UpdateNotification UpdateNotificationDetails { get; set; }
    }
    public class RootEmployeeLocationList : CommonAPIResponse
    {
        public EmployeeLocationList EmployeeLocationList { get; set; }
    }
    public class RootEmployeeWiseLocationList : CommonAPIResponse
    {
        public EmployeeWiseLocationList EmployeeWiseLocationList { get; set; }
    }
    public class RootDesignationList : CommonAPIResponse
    {
        public DesignationList DesignationList { get; set; }
    }
    public class RootMail : CommonAPIResponse
    {
        public MailResult MailResult { get; set; }
    }
    public class RootItemSearchList : CommonAPIResponse
    {
        public ItemSearchList ItemList { get; set; }
    }
    public class RootProductEnquiryList : CommonAPIResponse
    {
        public ProductEnquiryList ProductEnquiryList { get; set; }
    }
    public class RootProductEnquiryDetails : CommonAPIResponse
    {
        public ProductEnquiryDetails ProductEnquiryDetails { get; set; }
    }
    public class RootAuthorizationModuleList : CommonAPIResponse
    {
        public AuthorizationModuleData AuthorizationModuleList { get; set; }
    }
    public class RootAuthorizationList : CommonAPIResponse
    {
        public DynamicData AuthorizationList { get; set; }
    }
    public class RootAuthorizationAction:CommonAPIResponse
    {
        public AuthorizationActionDetails AuthorizationActionDetails { get; set; }
    }
    public class RootAuthorizationListDetails : CommonAPIResponse
    {
        public DynamicData AuthorizationListDetailsList { get; set; }
    }
    public class RootAuthorizationApproveUpdate : CommonAPIResponse
    {
        public UpdateAuthorization UpdateAuthorization { get; set; }
    }
    public class RootAuthorizationRejectUpdate : CommonAPIResponse
    {
        public UpdateAuthorization RejectAuthorization { get; set; }
    }
    public class RootAuthorizationCorrection : CommonAPIResponse
    {
        public CorrectionAuthorization CorrectionAuthorization { get; set; }
    }
    public class RootProductLocationList : CommonAPIResponse
    {
        public ProductLocationList ProductLocationList { get; set; }
    }
  
    public class RootUserTaskList : CommonAPIResponse
    {
        public UserTaskList UserTaskList { get; set; }
    }
    public class RootWalkingCustomerList : CommonAPIResponse
    {
        public WalkingCustomerList WalkingCustomerList { get; set; }
    }
    public class RootAuthorizationCorrectionModuleList : CommonAPIResponse
    {
        public AuthorizationCorrectionModuleData AuthorizationCorrectionModuleList { get; set; }
    }
    public class RootAuthorizationCorrectionDetailsList : CommonAPIResponse
    {
        public AuthorizationCorrectionDetailsData AuthorizationCorrectionDetailsData { get; set; }
    }
    public class RootAuthorizationCorrectionLeadDetails : CommonAPIResponse
    {
        public CorrectionLeadGenerate AuthorizationCorrectionLeadDetails { get; set; }
    }
    public class RootWalkingCustomerVoiceDetails : CommonAPIResponse
    {
        public WalkingCustomerVoiceDetails WalkingCustomerVoiceDetails { get; set; }
    }
    public class RootAuthorizationDetails : CommonAPIResponse
    {
        public AuthorizationDetails AuthorizationDetails { get; set; }
    }
    public class RootModuleDetails : CommonAPIResponse
    {
        public ModuleDetails ModuleDetails { get; set; }
    }
    public class RootModuleWiseSearchDetails : CommonAPIResponse
    {
        public ModuleDetails ModuleWiseSearchDetails { get; set; }
    }
    public class RootAuthorizationModuleCountDetails : CommonAPIResponse
    {
        public AuthorizationModuleCountDetails AuthorizationModuleCountDetails { get; set; }
    }
    public class RootAuthorizationPendingDetails : CommonAPIResponse
    {
        public AuthorizationPendingDetails AuthorizationPendingDetails { get; set; }
    }
    public class RootDashBoardModule:CommonAPIResponse
    {
        public DashBoardModule DashBoardModule { get; set; }
    }
    public class RootAuthorizationDataList : CommonAPIResponse
    {
        public AuthorizationDataList AuthorizationDataList { get; set; }
    }
    public class RootAttendancePunchDetails : CommonAPIResponse
    {
        public AttendancePunchDetails AttendancePunchDetails { get; set; }
    }

    public class RootUserTaskListDetails : CommonAPIResponse
    {
        public UserTaskListDetails UserTaskListDetails { get; set; }
    }
    public class RootSendIntimationModule : CommonAPIResponse
    {
        public SendIntimationModule SendIntimationModule { get; set; }
    }
    public class RootCheckVersionCode : CommonAPIResponse
    {
        public CheckVersionCode CheckVersionCode { get; set; }
    }
    public class RootScheduleType : CommonAPIResponse
    {
        public ScheduleType ScheduleType { get; set; }
    }
    public class RootChannel : CommonAPIResponse
    {
        public Channel Channel { get; set; }
    }
    public class RootUpdateSendIntimation : CommonAPIResponse
    {
        public UpdateSendIntimation UpdateSendIntimation { get; set; }
    }
    public class RootAppConfigurationSettings:CommonAPIResponse
    {
        public AppConfigurationSettings AppConfigurationSettings { get; set; }
    }
    public class RootEmployeeLocationDistance:CommonAPIResponse
    {
        public EmployeeLocationDistance EmployeeLocationDistance { get; set; }
    }
    public class RootSubCategoryDetailsList : CommonAPIResponse
    {
        public SubCategoryDetailsList SubCategoryDetailsList { get; set; }
    }
    public class RootBrandDetails : CommonAPIResponse
    {
        public BrandDetails BrandDetails { get; set; }
    }
    public class RootLeadSourceInfo : CommonAPIResponse
    {
        public LeadSourceDetails LeadSourceDetails { get; set; }
    }
    public class RootLeadFromInfo : CommonAPIResponse
    {
        public LeadFromInfo LeadFromInfo { get; set; }
    }
    public class RootLeadHistory : CommonAPIResponse
    {
        public LeadHistory LeadHistory { get; set; }
    }
    public class RootProductType : CommonAPIResponse
    {
        public ProductType ProductType { get; set; }
    }
    public class RootTimeLineDetails : CommonAPIResponse
    {
        public TimeLineDetails TimeLineDetails { get; set; }
    }
    public class RootAttendanceDetails : CommonAPIResponse
    {
        public  AttendanceDetails AttendanceDetails { get; set; }
    }
    public class RootUserDetails : CommonAPIResponse
    {
        public UserDetails UserDetails { get; set; }
    }
    public class RootMyActivityCount:CommonAPIResponse
    {
        public MyActivityCount MyActivityCount { get; set; }
    }
    public class RootMyActivitysActionCountDetails : CommonAPIResponse
    {
        public MyActivitysActionCountDetails MyActivitysActionCountDetails { get; set; }
    }
    public class RootMyActivitysActionDetails : CommonAPIResponse
    {
        public MyActivitysActionDetails MyActivitysActionDetails { get; set; }
    }
    public class RootMyActivitysFliters:CommonAPIResponse
    {
        public MyActivitysFliters MyActivitysFliters { get; set; }
    }
    public class RootClosedLeadReport : CommonAPIResponse
    {
        public ClosedLeadReport ClosedLeadReport { get; set; }
    }
}
    #endregion
