using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Business;
using PerfectWebERPAPI.Interface;
using System.Data;
using System.IO;
using System.Web.Hosting;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using PerfectWebERPAPI.DataAccess;


namespace PerfectWebERPAPI.Business
{
   public class BlUserValidationsFormats:BlUserValidations
    {
        
        public static ResellerDetails ConvertResellerDetails(DataTable dt)
        {
            ResellerDetails log = new ResellerDetails();
           
        
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                    log.ResellerName = dt.Rows[0].Field<string>("ResellerName");
                    log.AppIconImageCode = dt.Rows[0].Field<string>("AppIconImageCode");
                    log.TechnologyPartnerImage = dt.Rows[0].Field<string>("TechnologyPartnerImage");
                    log.ProductName = dt.Rows[0].Field<string>("ProductName");
                    log.PlayStoreLink = dt.Rows[0].Field<string>("PlayStoreLink");
                    log.AppStoreLink = dt.Rows[0].Field<string>("AppStoreLink");
                    log.ContactAddress = dt.Rows[0].Field<string>("ContactAddress");
                    log.ContactEmail = dt.Rows[0].Field<string>("ContactEmail");
                    log.ContactNumber = dt.Rows[0].Field<string>("ContactNumber");
                  
                    log.CertificateName = dt.Rows[0].Field<string>("CertificateStatus");
                    log.TestingURL = dt.Rows[0].Field<string>("TestingURL");
                    log.TestingMachineId = dt.Rows[0].Field<string>("TestingMachineId");
                    log.TestingImageURL = dt.Rows[0].Field<string>("TestingImageURL");
                    log.TestingMobileNo = dt.Rows[0].Field<string>("TestingMobileNo");
                    log.TestingBankKey = dt.Rows[0].Field<string>("BankKey");
                    log.TestingBankHeader = dt.Rows[0].Field<string>("BankHeader");
                    log.AboutUs= dt.Rows[0].Field<string>("AboutUs");
                    log.AudioClipEnabled = dt.Rows[0].Field<Boolean>("AudioClipEnabled");
                    log.IsLocationDistanceShowing = dt.Rows[0].Field<Boolean>("IsLocationDistanceShowing");
                    log.EditMRPLead = dt.Rows[0].Field<Boolean>("IsMRP");
                   

                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Reseller Details";
            }
            return log;
        }
        public static List<PageDetails> ConvertDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PageDetails()
                    {
                        PSValue = Convert.ToByte(dr["PSValue"]),
                        PSLabelName = Convert.ToString(dr["PSLabelName"].ToString()),
                        PSField = Convert.ToInt32(dr["PSField"].ToString())                        
                    }).ToList();
        }
        public static UserLoginDetails ConvertUserLoginDetails(string ResponseCode, string ResponseMessage)
        {
            UserLoginDetails log = new UserLoginDetails();
            log.ResponseCode = ResponseCode;
            log.ResponseMessage = ResponseMessage;
            return log;
        }

        public static UserLoginDetails ConvertUserLoginDetails(DataTable dt, DataTable dtCRM, DataTable dtFUP,string ModulesAndFeatures)
        {
            UserLoginDetails log = new UserLoginDetails();


            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ResponseCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]);
                    log.UserName = Convert.ToString(dt.Rows[0]["CusName"]);
                    log.Address = Convert.ToString(dt.Rows[0]["CusAddress1"]);
                    log.Email = Convert.ToString(dt.Rows[0]["CusEmail"]);
                    log.FK_Employee = Convert.ToInt64(dt.Rows[0]["FK_Employee"]);
                    log.MobileNumber = Convert.ToString(dt.Rows[0]["CusMobile"]);
                    log.Token = Convert.ToString(dt.Rows[0]["Token"]);
                    log.UserCode = Convert.ToString(dt.Rows[0]["UserCode"]);
                    log.FK_Branch = Convert.ToInt64(dt.Rows[0]["FK_Branch"]);
                    log.BranchName = Convert.ToString(dt.Rows[0]["BranchName"]);
                    log.FK_BranchType = Convert.ToInt64(dt.Rows[0]["FK_BranchType"]);
                    log.FK_Company = Convert.ToInt64(dt.Rows[0]["FK_Company"]);
                    log.FK_BranchCodeUser = Convert.ToInt64(dt.Rows[0]["FK_BranchCodeUser"]);
                    log.FK_UserRole = Convert.ToInt64(dt.Rows[0]["FK_UserRole"]);
                    log.UserRole = Convert.ToString(dt.Rows[0]["UserRole"]);
                    log.IsAdmin = Convert.ToInt32(dt.Rows[0]["IsAdmin"]);
                    log.IsManager = Convert.ToInt32(dt.Rows[0]["IsManager"]);
                    log.ID_User= Convert.ToInt64(dt.Rows[0]["ID_User"]);
                    log.ID_TokenUser = Convert.ToInt64(dt.Rows[0]["ID_User"]);
                    log.CompanyCategory = Convert.ToInt64(dt.Rows[0]["CompanyCategory"]);
                    log.CRMDetails = ConvertDetails(dtCRM);
                    log.FollowUpDetails = ConvertDetails(dtFUP);
                   // log.ModuleList = ConvertMenuList(ModulesAndFeatures);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Login Details";
            }
            return log;
        }
        public static ModuleList ConvertMenuList(string ModulesAndFeatures)
        {
            ModuleList log = new ModuleList();
            //string ExpDate = "";
            //DateTime date, TodayDate;
            //int Length = ModulesAndFeatures.Length;
            //TodayDate = DateTime.Now;
            //if (Length > 0)
            //{
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(29, 1))))
            //    {


            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(378, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.WATSAPP = true;
            //        }
            //        else
            //            log.WATSAPP = false;
            //        ExpDate = "";
            //    }
            //    else
            //        log.WATSAPP = false;
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(30, 1))))
            //    {
            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(386, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.EMAIL = true;
            //        }
            //        else
            //            log.EMAIL = true;
            //        ExpDate = "";
            //    }

            //    else
            //        log.EMAIL = false;
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(31, 1))))
            //    {
            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(394, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.LEAD = true;
            //        }
            //        else
            //            log.LEAD = true;
            //        ExpDate = "";
            //    }
            //    else
            //        log.LEAD = false;
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(32, 1))))
            //    {
            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(402, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.SERVICE = true;
            //        }
            //        else
            //            log.SERVICE = true;
            //        ExpDate = "";
            //    }
            //    else
            //        log.SERVICE = false;
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(33, 1))))
            //    {
            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(410, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.COLLECTION = true;
            //        }
            //        else
            //            log.COLLECTION = true;
            //        ExpDate = "";
            //    }
            //    else
            //        log.COLLECTION = false;
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(34, 1))))
            //    {
            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(418, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.PROJECT = true;
            //        }
            //        else
            //            log.PROJECT = true;
            //        ExpDate = "";
            //    }
            //    else
            //        log.PROJECT = false;
            //    if (Convert.ToBoolean(Convert.ToInt32(ModulesAndFeatures.Substring(35, 1))))
            //    {
            //        ExpDate = Convert.ToString(ModulesAndFeatures.Substring(426, 8));
            //        date = DateTime.ParseExact(ExpDate, "ddMMyyyy", null);
            //        if (TodayDate <= date)
            //        {
            //            log.DELIVERY = true;
            //        }
            //        else
            //            log.DELIVERY = true;
            //        ExpDate = "";
            //    }
            //    else
            //        log.DELIVERY = false;
            //}
            //else
            //{
            //log.WATSAPP = false;
            //log.EMAIL = false;
            //log.LEAD = false;
            //log.SERVICE = false;
            //log.COLLECTION = false;
            //log.PROJECT = false;
            //log.DELIVERY = false;
            //}
            log.WATSAPP = true;
            log.EMAIL = true;
            log.LEAD = true;
            log.SERVICE = true;
            log.COLLECTION = true;
            log.PROJECT = true;
            log.DELIVERY = true;
            return log;
        }
        public string GetCompanyName(string BankKey, Int64 FK_Company)
        {
            DalUserValidations dalUserValidations = new DalUserValidations();
            string BankTitle = "";
            BankTitle = dalUserValidations.GetBankNameAndTitle(BankKey, FK_Company);
            //BankTitle = "Head Office Chalappuram!Head Office Chalappuram";
            return BankTitle;
        }
        public static UserLoginDetails ConvertUserLoginDetailsDS(DataSet dts ,DataTable dtCRM, DataTable dtFUP,string ModulesAndFeatures)
        {
            UserLoginDetails log = new UserLoginDetails();
            
            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ResponseCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]);
                    log.UserName = Convert.ToString(dt.Rows[0]["CusName"]);
                    log.Address = Convert.ToString(dt.Rows[0]["CusAddress1"]);
                    log.Email = Convert.ToString(dt.Rows[0]["CusEmail"]);
                    log.FK_Employee = Convert.ToInt64(dt.Rows[0]["FK_Employee"]);
                   // log.LoggedFK_Employee = Convert.ToInt64(dt.Rows[0]["FK_Employee"]);
                    log.MobileNumber = Convert.ToString(dt.Rows[0]["CusMobile"]);
                    log.Token = Convert.ToString(dt.Rows[0]["Token"]);
                    log.UserCode = Convert.ToString(dt.Rows[0]["UserCode"]);
                    log.FK_Branch = Convert.ToInt64(dt.Rows[0]["FK_Branch"]);
                    log.BranchName = Convert.ToString(dt.Rows[0]["BranchName"]);
                    log.FK_BranchType = Convert.ToInt64(dt.Rows[0]["FK_BranchType"]);
                    log.FK_Company = Convert.ToInt64(dt.Rows[0]["FK_Company"]);
                    log.FK_BranchCodeUser = Convert.ToInt64(dt.Rows[0]["FK_BranchCodeUser"]);
                    log.FK_UserRole = Convert.ToInt64(dt.Rows[0]["FK_UserRole"]);
                    log.UserRole = Convert.ToString(dt.Rows[0]["UserRole"]);
                    log.IsAdmin = Convert.ToInt32(dt.Rows[0]["IsAdmin"]);
                    log.IsManager = Convert.ToInt32(dt.Rows[0]["IsManager"]);
                    log.ID_User = Convert.ToInt64(dt.Rows[0]["ID_User"]);
                    log.ID_TokenUser = Convert.ToInt64(dt.Rows[0]["ID_User"]);
                    log.CompanyCategory = Convert.ToInt64(dt.Rows[0]["CompanyCategory"]);
                    log.FK_Department = Convert.ToInt64(dt.Rows[0]["FK_Department"]);
                    log.Department = Convert.ToString(dt.Rows[0]["Department"]);
                    log.LocLongitude = Convert.ToString(dt.Rows[0]["LocLongitude"]);
                    log.LocLattitude = Convert.ToString(dt.Rows[0]["LocLattitude"]);
                    log.LocLocationName = Convert.ToString(dt.Rows[0]["LocLocationName"]);
                    log.EnteredDate = Convert.ToString(dt.Rows[0]["EnteredDate"]);
                    log.EnteredTime = Convert.ToString(dt.Rows[0]["EnteredTime"]);
                    log.Status = (dt.Rows[0]["Status"]!=DBNull.Value)?Convert.ToInt32(dt.Rows[0]["Status"]):0;
                    log.PSValue = Convert.ToInt16(dt.Rows[0]["PSValue"]);
                    log.ModuleList = ConvertMenuList(ModulesAndFeatures);
                    //log.ModuleList = ConvertMenuList(dts.Tables[1]);
                    log.UtilityList = ConvertUtilityList(dts.Tables[2]);
                    log.CRMDetails = ConvertDetails(dtCRM);
                    log.FollowUpDetails = ConvertDetails(dtFUP);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Login Details";
            }
            return log;
        }
        //public static ModuleList ConvertMenuList(DataTable dt)
        //{
        //    ModuleList log = new ModuleList();
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "MASTER" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.MASTER = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "SERVICE" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.SERVICE = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "LEAD" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.LEAD = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "INVENTORY" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.INVENTORY = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "SETTINGS" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.SETTINGS = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "SECURITY" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.SECURITY = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "REPORT" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.REPORT = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "PROJECT" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.PROJECT = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "OTHER" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.OTHER = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "PRODUCTION" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.PRODUCTION = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "ACCOUNTS" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.ACCOUNTS = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "ASSET" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.ASSET = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "TOOL" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.TOOL = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "VEHICLE" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.VEHICLE = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "DELIVERY" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.DELIVERY = true;
        //            }
        //            if (Convert.ToString(dt.Rows[i]["Module"]) == "HR" && Convert.ToBoolean(dt.Rows[i]["MenuGroupVisible"]))
        //            {
        //                log.HR = true;
        //            }

        //        }
        //    }
        //    return log;
        //}
        public static UtilityList ConvertUtilityList(DataTable dt)
        {
            UtilityList log = new UtilityList();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Rows[i]["GsModule"]) == "APPATT")
                        {
                            log.ATTANCE_MARKING = Convert.ToBoolean(dt.Rows[i]["GsValue"]);
                        }
                        if (Convert.ToString(dt.Rows[i]["GsModule"]) == "APPLOC")
                        {
                            log.LOCATION_TRACKING = Convert.ToBoolean(dt.Rows[i]["GsValue"]);
                        }
                        if (Convert.ToString(dt.Rows[i]["GsModule"]) == "LOCINT")
                        {
                            log.LOCATION_INTERVAL = Convert.ToInt32(dt.Rows[i]["GsData"]);
                        }
                        if (Convert.ToString(dt.Rows[i]["GsModule"]) == "LOCLIC")
                        {
                            log.LOCATION_ROOTE = Convert.ToBoolean(dt.Rows[i]["GsData"]);
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return log;
        }
        public static MPINDetails ConvertToMPINDetails(DataTable dt)
        {
            MPINDetails log = new MPINDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                if (dt.Rows[0].Field<string>("ResponseCode") == "0")
                {
                    log.MPIN = dt.Rows[0].Field<string>("MPIN");
                }
            }
            return log;
        }
        public static CustomerDetailsList ConvertCustomerDetailsList(DataTable dt)
        {
            CustomerDetailsList log = new CustomerDetailsList();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.CustomerDetails = ConvertCustomerDetails(dt);

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "Invalid Customer Name";
            }
            return log;
        }
        public static List<CustomerDetails> ConvertCustomerDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CustomerDetails()
                    {
                        ID_Customer = Convert.ToInt64(dr["ID_Customer"]),
                        CusNameTitle = Convert.ToString(dr["CusNameTitle"].ToString()),
                        CusName = Convert.ToString(dr["CusName"].ToString()),
                        CusAddress1 = Convert.ToString(dr["CusAddress1"].ToString()),
                        CusAddress2 = Convert.ToString(dr["CusAddress2"].ToString()),
                        CusEmail = Convert.ToString(dr["CusEmail"].ToString()),
                        CusPhnNo = Convert.ToString(dr["CusPhnNo"].ToString()),
                        Company = Convert.ToString(dr["Company"].ToString()),
                        CountryID = Convert.ToInt64(dr["CountryID"].ToString()),
                        CntryName = Convert.ToString(dr["CntryName"].ToString()),
                        StatesID = Convert.ToInt64(dr["StatesID"]),
                        StName = Convert.ToString(dr["StName"].ToString()),
                        DistrictID = Convert.ToInt64(dr["DistrictID"]),
                        DtName = Convert.ToString(dr["DtName"].ToString()),
                        PostID = Convert.ToInt64(dr["PostID"]),
                        PostName = Convert.ToString(dr["PostName"].ToString()),
                        FK_Area = Convert.ToInt64(dr["FK_Area"]),
                        Area = Convert.ToString(dr["Area"].ToString()),
                        CusMobileAlternate = Convert.ToString(dr["CusMobileAlternate"].ToString()),
                        Pincode = Convert.ToString(dr["Pincode"].ToString()),
                        Customer_Type= Convert.ToInt64(dr["Customer_Type"]),
                        LandNumber = Convert.ToString(dr["LandNumber"]),


                    }).ToList();
        }
        public static LeadFromDetailsList ConvertLeadFromDetailsList(DataTable dt)
        {
            LeadFromDetailsList log = new LeadFromDetailsList();
            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                //{
                //    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                //    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                //}
                //else
                //{
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.LeadFromDetails = ConvertLeadFromDetails(dt);

                //}

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead From Details";
            }
            return log;
        }
        public static List<LeadFromDetails> ConvertLeadFromDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadFromDetails()
                    {
                        ID_LeadFrom = Convert.ToInt32(dr["ID_LeadFrom"].ToString()),
                        LeadFromName = Convert.ToString(dr["LeadFromName"].ToString()),
                        LeadFromType = Convert.ToInt32(dr["LeadFromType"].ToString())
                    }).ToList();
        }
        public static LeadThroughDetailsList ConvertLeadThroughDetailsList(DataTable dt)
        {
            LeadThroughDetailsList log = new LeadThroughDetailsList();
            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                //{
                //    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                //    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                //}
                //else
                //{
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.LeadThroughDetails = ConvertLeadThroughDetails(dt);

                //}

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Details";
            }
            return log;
        }
        public static List<LeadThroughDetails> ConvertLeadThroughDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadThroughDetails()
                    {
                        ID_LeadThrough = Convert.ToInt32(dr["ID_FIELD"].ToString()),
                        LeadThroughName = Convert.ToString(dr["Name"].ToString()),
                        HasSub= Convert.ToInt32(dr["HasSub"].ToString())

                    }).ToList();
        }
        public static AddNewCustomer ConvertAddNewCustomer(DataTable dt)
        {
            AddNewCustomer log = new AddNewCustomer();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.ID_Customer = dt.Rows[0].Field<Int64>("ID_Customer");

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "Customer Not Added";
            }
            return log;
        }
        public static MaintenanceMessage ConvertMaintenanceMessage(DataTable dt)
        {
            MaintenanceMessage log = new MaintenanceMessage();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.Description = dt.Rows[0].Field<string>("UtlDescription");
                    log.Type = dt.Rows[0].Field<byte>("UtlType");//0-No Maintance,1-Warning,2-Maintance
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Maintenance Details";
            }
            return log;
        }
        public static BannerDetails ConvertBannerDetails(DataTable dt)
        {
            BannerDetails log = new BannerDetails();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.BannerDetailsList = ConvertBannerDetailsList(dt);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Banner Details";
            }
            return log;
        }
        public static List<BannerDetailsList> ConvertBannerDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new BannerDetailsList()
                    {

                        ID_Banner = Convert.ToInt64(dr["ID_Banner"].ToString()),
                        ImagePath = Convert.ToString(dr["ImagePath"].ToString())


                    }).ToList();

        }
        public static CollectedByUsersList ConvertCollectedByUsersList(DataTable dt)
        {
            CollectedByUsersList log = new CollectedByUsersList();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.CollectedByUsers = ConvertCollectedByUsers(dt);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Collected By Users Deetails";
            }
            return log;
        }
        public static List<CollectedByUsers> ConvertCollectedByUsers(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CollectedByUsers()
                    {

                        ID_CollectedBy = Convert.ToInt64(dr["ID_Employee"].ToString()),
                        Name = Convert.ToString(dr["EmpFName"].ToString()),
                        DepartmentName = Convert.ToString(dr["DepartmentName"].ToString()),
                        DesignationName = Convert.ToString(dr["DesignationName"].ToString())

                    }).ToList();

        }

        public static CategoryDetailsList ConvertCategoryDetailsList(DataTable dt)
        {
            CategoryDetailsList log = new CategoryDetailsList();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.CategoryList = ConvertCategoryList(dt);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Category Details";
            }
            return log;
        }
        public static List<CategoryList> ConvertCategoryList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CategoryList()
                    {

                        ID_Category = Convert.ToInt64(dr["ID_Category"].ToString()),
                        CategoryName = Convert.ToString(dr["CatName"].ToString()),
                        Project = Convert.ToInt32(dr["Project"])

                    }).ToList();

        }
        public static ProductDetailsList ConvertProductDetailsList(DataTable dt)
        {
            ProductDetailsList log = new ProductDetailsList();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.ProductList = ConvertProductList(dt);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Details";
            }
            return log;
        }
        public static List<ProductList> ConvertProductList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProductList()
                    {

                        ID_Product = Convert.ToInt64(dr["ID_Product"].ToString()),
                        ProductName = Convert.ToString(dr["ProductName"].ToString()),
                        MRP = Convert.ToString(dr["MRP"].ToString()),
                        SalPrice = Convert.ToString(dr["SalPrice"].ToString()),
                        FK_Category=Convert.ToString(dr["FK_Category"].ToString()),
                        ProdBarcode=Convert.ToString(dr["ProdBarcode"].ToString())

                    }).ToList();

        }
        public static StatusDetailsList ConvertStatusDetailsList(DataTable dt)
        {
            StatusDetailsList log = new StatusDetailsList();
            if (dt.Rows.Count > 0)
            {
               
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.StatusList = ConvertStatusList(dt);
               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Status Details";
            }
            return log;
        }
        public static List<StatusList> ConvertStatusList(DataTable dt)
        {
            List<StatusList> objList = new List<StatusList>();
            foreach(DataRow dr in dt.Rows)
            {
                StatusList obj = new StatusList();
                obj.ID_Status = Convert.ToInt64(dr["ID_Mode"].ToString());
                obj.StatusName = Convert.ToString(dr["ModeName"].ToString());
                if(dr.Table.Columns.Contains("IsEnable"))
                {
                    obj.IsEnable = Convert.ToInt32(dr["IsEnable"].ToString());
                }
                objList.Add(obj);
            }
            return objList;           
        }
        public static PriorityDetailsList ConvertPriorityDetailsList(DataTable dt)
        {
            PriorityDetailsList log = new PriorityDetailsList();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.PriorityList = ConvertPriorityList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Priority Details";
            }
            return log;
        }
        public static List<PriorityList> ConvertPriorityList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PriorityList()
                    {

                        ID_Priority = Convert.ToInt64(dr["ID_Priority"].ToString()),
                        PriorityName = Convert.ToString(dr["PriorityName"].ToString())


                    }).ToList();

        }

        public static FollowUpActionDetails ConvertFollowUpActionDetails(DataTable dt)
        {
            FollowUpActionDetails log = new FollowUpActionDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.FollowUpActionDetailsList = ConvertFollowUpActionDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the FollowUpAction Details";
            }
            return log;
        }
        public static List<FollowUpActionDetailsList> ConvertFollowUpActionDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new FollowUpActionDetailsList()
                    {

                        ID_NextAction = Convert.ToInt64(dr["ID_NextAction"].ToString()),
                        NxtActnName = Convert.ToString(dr["NxtActnName"].ToString()),
                        Status = Convert.ToInt32(dr["NxtActnStage"])

                    }).ToList();

        }
        public static FollowUpTypeDetails ConvertFollowUpTypeDetails(DataTable dt)
        {
            FollowUpTypeDetails log = new FollowUpTypeDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.FollowUpTypeDetailsList = ConvertFollowUpTypeDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the FollowUp Type Details";
            }
            return log;
        }
        public static List<FollowUpTypeDetailsList> ConvertFollowUpTypeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new FollowUpTypeDetailsList()
                    {

                        ID_ActionType = Convert.ToInt64(dr["ID_ActionType"].ToString()),
                        ActnTypeName = Convert.ToString(dr["ActnTypeName"].ToString()),
                         ActionMode = Convert.ToInt64(dr["ActionMode"].ToString())

                    }).ToList();

        }
        public static MediaTypeDetails ConvertMediaTypeDetails(DataTable dt)
        {
            MediaTypeDetails log = new MediaTypeDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.MediaTypeDetailsList = ConvertMediaTypeDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Media Type Details";
            }
            return log;
        }
        public static List<MediaTypeDetailsList> ConvertMediaTypeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MediaTypeDetailsList()
                    {

                        ID_MediaMaster = Convert.ToInt64(dr["ID_MediaMaster"].ToString()),
                        MdaName = Convert.ToString(dr["MdaName"].ToString()),
                        HasSubMedia = Convert.ToInt64(dr["HasSubMedia"].ToString())
                    }).ToList();

        }
        public static SubMediaTypeDetails ConvertSubMediaTypeDetails(DataTable dt)
        {
            SubMediaTypeDetails log = new SubMediaTypeDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.SubMediaTypeDetailsList = ConvertSubMediaTypeDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Sub Media Type Details";
            }
            return log;
        }       
        public static List<SubMediaTypeDetailsList> ConvertSubMediaTypeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new SubMediaTypeDetailsList()
                    {

                        ID_MediaSubMaster = Convert.ToInt64(dr["ID_MediaSubMaster"].ToString()),
                        SubMdaName = Convert.ToString(dr["SubMdaName"].ToString())
                    }).ToList();

        }
        public static ReasonDetails ConvertReasonDetails(DataTable dt)
        {
            ReasonDetails log = new ReasonDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.ReasonDetailsList = ConvertReasonDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Reason Details";
            }
            return log;
        }
        public static List<ReasonDetailsList> ConvertReasonDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ReasonDetailsList()
                    {

                        ID_Reason = Convert.ToInt64(dr["ID_Reason"].ToString()),
                        ResnName = Convert.ToString(dr["ResnName"].ToString())
                    }).ToList();

        }
        public static BranchTypeDetails ConvertBranchTypeDetails(DataTable dt)
        {
            BranchTypeDetails log = new BranchTypeDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.BranchTypeDetailsList = ConvertBranchTypeDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Branch Type Details";
            }
            return log;
        }
        public static List<BranchTypeDetailsList> ConvertBranchTypeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new BranchTypeDetailsList()
                    {

                        ID_BranchType = Convert.ToInt64(dr["ID_BranchType"].ToString()),
                        BranchTypeName = Convert.ToString(dr["BTName"].ToString())


                    }).ToList();

        }
        public static BranchDetails ConvertBranchDetails(DataTable dt)
        {
            BranchDetails log = new BranchDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.BranchDetailsList = ConvertBranchDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Branch Details";
            }
            return log;
        }
        public static List<BranchDetailsList> ConvertBranchDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new BranchDetailsList()
                    {

                        ID_Branch = Convert.ToInt64(dr["ID_Branch"].ToString()),
                        BranchName = Convert.ToString(dr["BranchName"].ToString())


                    }).ToList();

        }
        public static DepartmentDetails ConvertDepartmentDetails(DataTable dt)
        {
            DepartmentDetails log = new DepartmentDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.DepartmentDetailsList = ConvertDepartmentDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Department Details";
            }
            return log;
        }
        public static List<DepartmentDetailsList> ConvertDepartmentDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new DepartmentDetailsList()
                    {

                        ID_Department = Convert.ToInt64(dr["ID_Department"].ToString()),
                        DeptName = Convert.ToString(dr["DeptName"].ToString())


                    }).ToList();

        }
        public static EmployeeDetails ConvertEmployeeDetails(DataTable dt)
        {
            EmployeeDetails log = new EmployeeDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.EmployeeDetailsList = ConvertEmployeeDetailsList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "Employee details not found in this branch.";
            }
            return log;
        }
        public static List<EmployeeDetailsList> ConvertEmployeeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new EmployeeDetailsList()
                    {

                        ID_Employee = Convert.ToInt64(dr["ID_Employee"].ToString()),
                        EmpName = Convert.ToString(dr["EmpName"].ToString()),
                        DepartmentName = Convert.ToString(dr["DepartmentName"].ToString()),
                        DesignationName = Convert.ToString(dr["DesignationName"].ToString())

                    }).ToList();

        }
        public static EmployeeAllDetails ConvertEmployeeAllDetails(DataTable dt)
        {
            EmployeeAllDetails log = new EmployeeAllDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EmployeeAllDetailsList = ConvertEmployeeAllDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Details";
            }
            return log;
        }
        public static List<EmployeeAllDetailsList> ConvertEmployeeAllDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new EmployeeAllDetailsList()
                    {

                        ID_Employee = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        EmpName = Convert.ToString(dr["Name"].ToString()),
                        DepartmentName = Convert.ToString(dr["DeptName"].ToString()),
                        DesignationName = Convert.ToString(dr["Designation"].ToString()),
                        Branch = Convert.ToString(dr["BrName"].ToString())                    

                    }).ToList();

        }
        public static LeadManagementDetailsList ConvertLeadManagementDetailsList(DataTable dt)
        {
            LeadManagementDetailsList log = new LeadManagementDetailsList();
            if (dt.Rows.Count > 0)
            {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.LeadManagementDetails = ConvertLeadManagementDetails(dt);               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Details";
            }
            return log;
        }
        public static List<LeadManagementDetails> ConvertLeadManagementDetails(DataTable dt)
        {
            List<LeadManagementDetails> lst = new List<LeadManagementDetails>();
            foreach(DataRow dr in dt.Rows)
            {
                LeadManagementDetails obj = new LeadManagementDetails();
                obj.SlNo = Convert.ToInt32(dr["SlNo"].ToString());
                obj.ID_LeadGenerate = Convert.ToInt64(dr["ID_LeadGenerate"].ToString());
                obj.ID_LeadGenerateProduct = Convert.ToInt64(dr["ID_LeadGenerateProduct"].ToString());
                var tempLeadDate = Convert.ToDateTime(dr["LgLeadDate"]);
                obj.LgLeadDate = tempLeadDate.ToString("dd/MM/yyyy");
                obj.LgCusName = Convert.ToString(dr["LgCusName"].ToString());
                obj.LgCusMobile = Convert.ToString(dr["LgCusMobile"].ToString());
                obj.LgCollectedBy = Convert.ToString(dr["LgCollectedBy"].ToString());
                var tempNextActionDate = Convert.ToDateTime(dr["NextActionDate"]);
                obj.NextActionDate = tempNextActionDate.ToString("dd/MM/yyyy");
                obj.DueDays = Convert.ToInt32(dr["DueDays"].ToString());
                obj.ProdName = Convert.ToString(dr["ProdName"].ToString());
                obj.ProjectName = Convert.ToString(dr["ProjectName"].ToString());
                obj.LgpDescription = Convert.ToString(dr["LgpDescription"].ToString());
                obj.FK_Employee = Convert.ToString(dr["FK_Employee"].ToString());
                obj.LeadNo = Convert.ToString(dr["LeadNo"].ToString());
                obj.Preference = Convert.ToString(dr["Preference"].ToString());
                obj.AssignedTo = Convert.ToString(dr["AssignedTo"].ToString());
                obj.CusAddress = Convert.ToString(dr["Address"].ToString());
                obj.ID_NextAction = Convert.ToInt64(dr["ID_NextAction"].ToString());
                obj.Action = Convert.ToString(dr["Action"].ToString());
                obj.ID_Users = Convert.ToString(dr["ID_Users"].ToString());
                obj.Email = Convert.ToString(dr["CusEmail"].ToString());
                lst.Add(obj);
            }
            return lst;           

        }
        public static RoportSettingsList ConvertRoportSettingsList(DataTable dt)
        {
            RoportSettingsList log = new RoportSettingsList();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.RoportSettings = ConvertRoportSettings(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Roport Settings";
            }
            return log;
        }
      
        public static List<RoportSettings> ConvertRoportSettings(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new RoportSettings()
                    {

                        ID_ReportSettings = Convert.ToInt64(dr["ID_ReportSettings"].ToString()),
                        SettingsName = Convert.ToString(dr["SettingsName"].ToString())


                    }).ToList();

        }
        public static GeneralReport ConvertGeneralReport(DataTable dt)
        {
            GeneralReport log = new GeneralReport();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.Data = DataTableToJSONWithJSONNet(dt);
                // log.Data = DataConvert(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the General Report";
            }
            return log;
        }
        public static List<Dictionary<string, object>> DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            List<Dictionary<string, object>> main = new List<Dictionary<string, object>>();
            List<DataDetails> sr = new List<DataDetails>();
            //Dictionary<string, object> innermain = new Dictionary<string, object>();
            List<dynamic> childRow;
            int i = 1;
            foreach (DataRow row in table.Rows)
            {
                DataDetails obsr = new DataDetails();
                childRow = new List<dynamic>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(new KeyValuePair<string, string>(col.ColumnName, row[col].ToString()));
                }
                obsr.datadet = new Dictionary<string, object>();
                obsr.datadet.Add("Details", childRow);
                main.Add(obsr.datadet);
                i = i + 1;

            }
            // main.Add(obsr);

            return main;

        }
        public static LeadHistoryDetails ConvertLeadHistoryDetails(DataTable dt)
        {
            LeadHistoryDetails log = new LeadHistoryDetails();
            if (dt.Rows.Count > 0)
            {

                //if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                //{
                //    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                //    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                //}
                //else
                //{
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadHistoryDetailsList = ConvertLeadHistoryDetailsList(dt);
                //}

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead History Details";
            }
            return log;
        }
        public static List<LeadHistoryDetailsList> ConvertLeadHistoryDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadHistoryDetailsList()
                    {

                        Product = Convert.ToString(dr["Product"].ToString()),
                        EnquiryAbout = Convert.ToString(dr["EnquiryAbout"].ToString()),
                        LgpDescription = Convert.ToString(dr["LgpDescription"].ToString()),
                        Action = Convert.ToString(dr["Action"].ToString()),
                        ActionDate = Convert.ToString(dr["ActionDate"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        CustomerReamrks = Convert.ToString(dr["CustomerReamrks"].ToString()),
                        Agentremarks = Convert.ToString(dr["Agentremarks"].ToString()),
                        FollowedBy = Convert.ToString(dr["FollowedBy"].ToString())

                    }).ToList();

        }
        public static LeadInfoDetails ConvertLeadInfoDetails(DataTable dt)
        {
            LeadInfoDetails log = new LeadInfoDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadInfoDetailsList = ConvertLeadInfoDetailsList(dt);
               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Info Details";
            }
            return log;
        }
        public static List<LeadInfoDetailsList> ConvertLeadInfoDetailsList(DataTable dt)
        {
            List<LeadInfoDetailsList> lst = new List<LeadInfoDetailsList>();
            foreach(DataRow dr in dt.Rows)
            {
                LeadInfoDetailsList obj = new LeadInfoDetailsList();
                obj.LeadNo = Convert.ToString(dr["LgLeadNo"]);
                var tempLeadDate = Convert.ToDateTime(dr["LeadDate"]);
                obj.LeadDate = tempLeadDate.ToString("dd/MM/yyyy"); 
                obj.LeadSource = Convert.ToString(dr["LeadSourceName"]);
                obj.LeadFrom = Convert.ToString(dr["LeadFromName"]);
                obj.Category = Convert.ToString(dr["CatName"]);
                obj.Product = Convert.ToString(dr["ProdName"]);
                obj.Action = Convert.ToString(dr["NxtActnName"]);
                obj.Customer = Convert.ToString(dr["LgCusName"]);
                obj.Address = Convert.ToString(dr["LgCusAddress"]);
                obj.MobileNumber = Convert.ToString(dr["LgCusMobile"]);
                obj.Email = Convert.ToString(dr["LgCusEmail"]);
                obj.CollectedBy = Convert.ToString(dr["LgCollectedBy"]);
                obj.AssignedTo = Convert.ToString(dr["AssignedTo"]);              
                var TempTargetDate = Convert.ToDateTime(dr["NextActionDate"]);
                obj.TargetDate = TempTargetDate.ToString("dd/MM/yyyy");
                obj.ExpectDate = "";
                if (dr["ExpectDate"].ToString()!="")
                {
                    var TempExpectDate = Convert.ToDateTime(dr["ExpectDate"]);
                    obj.ExpectDate = TempExpectDate.ToString("dd/MM/yyyy");
                }
                obj.ID_Users = Convert.ToString(dr["ID_Users"]);
                obj.LgpMRP = Convert.ToString(dr["LgpMRP"]);
                obj.LgpSalesPrice = Convert.ToString(dr["LgpSalesPrice"]);                  
                lst.Add(obj);
            }

            return lst;
    }
        public static LeadImageDetails ConvertLeadImageDetails(DataTable dt)
        {
            LeadImageDetails log = new LeadImageDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                    log.LocationLandMark1 = dt.Rows[0].Field<byte[]>("LocLandMark1");
                    log.LocationLandMark2 = dt.Rows[0].Field<byte[]>("LocLandMark2");
                    log.LocationLatitude = dt.Rows[0].Field<string>("LocLattitude");
                    log.LocationLongitude = dt.Rows[0].Field<string>("LocLongitude");
                    log.LocationName = dt.Rows[0].Field<string>("LocLocationName");
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found Please Provide a location";
            }
            return log;
        }
        public static NotificationDetails ConvertNotificationDetails(DataTable dt)
        {
            NotificationDetails log = new NotificationDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.count = dt.Rows.Count;
                log.NotificationInfo = ConvertNotificationInfo(dt);
               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Notification Details";
            }
            return log;
        }
        public static List<NotificationInfo> ConvertNotificationInfo(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new NotificationInfo()
                    {

                        ID_Notification = Convert.ToInt64(dr["ID_Notification"]),
                        SendOn = Convert.ToString(dr["SendOn"]),
                        Title = Convert.ToString(dr["Title"]),
                        Message = Convert.ToString(dr["Message"]),
                        //EmpImgValue = Convert.ToString(dr["EmpImgValue"]),
                        IsRead = Convert.ToInt32(dr["IsRead"]),
                        EmpFName = Convert.ToString(dr["EmpFName"])                  

                    }).ToList();
        }
        public static UpdateLeadGenerateAction ConvertUpdateLeadGenerateAction(DataTable dt)
        {
            UpdateLeadGenerateAction log = new UpdateLeadGenerateAction();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }

                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Lead Generation Updated successfully";
                    log.FK_LeadGenerateAction = dt.Rows[0].Field<Int64>("FK_LeadGenerateAction");
                   
                }
               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records Updated";
            }
            return log;
        }
       
        public static PincodeDetails ConvertPincodeDetails(DataTable dt)
        {
            PincodeDetails log = new PincodeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PincodeDetailsList = ConvertPincodeDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Pincode Details";
            }
            return log;
        }
        public static List<PincodeDetailsList> ConvertPincodeDetailsList(DataTable dt)
        {
            List<PincodeDetailsList> lst = new List<PincodeDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                PincodeDetailsList obj = new PincodeDetailsList();
               
                obj.Post = Convert.ToString(dr["Post"]);
                obj.FK_Post = Convert.ToInt64(dr["FK_Post"]);
                obj.Place = Convert.ToString(dr["Place"]);
                obj.FK_Place = Convert.ToInt64(dr["FK_Place"]);
                obj.Area = Convert.ToString(dr["Area"]);
                obj.FK_Area = Convert.ToInt64(dr["FK_Area"]);
                obj.District = Convert.ToString(dr["District"]);
                obj.FK_District = Convert.ToInt64(dr["FK_District"]);
                obj.States = Convert.ToString(dr["States"]);
                obj.FK_States = Convert.ToInt64(dr["FK_States"]);
                obj.Country = Convert.ToString(dr["Country"]);
                obj.FK_Country = Convert.ToInt64(dr["FK_Country"]);
                lst.Add(obj);
            }
            return lst;

        }
        public static CountryDetails ConvertCountryDetails(DataTable dt)
        {
            CountryDetails log = new CountryDetails();
            if (dt.Rows.Count > 0)
            {
               
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";                    
                    log.CountryDetailsList = ConvertCountryDetailsList(dt);
               

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Country Details";
            }
            return log;
        }
        public static List<CountryDetailsList> ConvertCountryDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CountryDetailsList()
                    {

                        FK_Country = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        Country = Convert.ToString(dr["Name"].ToString())
                       

                    }).ToList();
        }
        public static StatesDetails ConvertStatesDetails(DataTable dt)
        {
            StatesDetails log = new StatesDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StatesDetailsList = ConvertStatesDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the State Details";
            }
            return log;
        }
        public static List<StatesDetailsList> ConvertStatesDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new StatesDetailsList()
                    {

                        FK_States = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        States = Convert.ToString(dr["Name"].ToString())


                    }).ToList();
        }
       
        public static DistrictDetails ConvertDistrictDetails(DataTable dt)
        {
            DistrictDetails log = new DistrictDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.DistrictDetailsList = ConvertDistrictDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the District Details";
            }
            return log;
        }
        public static List<DistrictDetailsList> ConvertDistrictDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new DistrictDetailsList()
                    {

                        FK_District = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        District = Convert.ToString(dr["Name"].ToString())


                    }).ToList();
        }
        public static AreaDetails ConvertAreaDetails(DataTable dt)
        {
            AreaDetails log = new AreaDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AreaDetailsList = ConvertAreaDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Area Details";
            }
            return log;
        }
        public static List<AreaDetailsList> ConvertAreaDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AreaDetailsList()
                    {
                        FK_Area = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        Area = Convert.ToString(dr["Name"].ToString())
                    }).ToList();
        }
        public static PostDetails ConvertPostDetails(DataTable dt)
        {
            PostDetails log = new PostDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PostDetailsList = ConvertPostDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Post Details";
            }
            return log;
        }
        public static List<PostDetailsList> ConvertPostDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PostDetailsList()
                    {

                        FK_Post = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        PostName = Convert.ToString(dr["Name"].ToString()),
                        PinCode = Convert.ToString(dr["Pincode"].ToString())


                    }).ToList();
        }
        public static UpdateLeadGeneration ConvertUpdateLeadGeneration(DataTable dt)
        {
            UpdateLeadGeneration log = new UpdateLeadGeneration();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                if(dt.Rows[0].Field<string>("ResponseCode")=="0")
                {
                    log.FK_LeadGenerate = dt.Rows[0].Field<Int64>("FK_LeadGenerate");
                    log.LeadNumber = dt.Rows[0].Field<string>("LeadNumber");
                }
                
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records Updated";
            }
            return log;
        }
        public static AddAgentCustomerRemarks ConvertAddAgentCustomerRemarks(DataTable dt)
        {
            AddAgentCustomerRemarks log = new AddAgentCustomerRemarks();
            if (dt.Rows.Count > 0)
            {



                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.CustomerNote = dt.Rows[0].Field<string>("CustomerNote");

                log.AgentNote = dt.Rows[0].Field<string>("EmployeeNote");

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Customer Remarks";
            }
            return log;
        }
        public static LeadGenerationDetails ConvertLeadGenerationDetails(DataTable dt)
        {
            LeadGenerationDetails log = new LeadGenerationDetails();
            if (dt.Rows.Count > 0)
            {               
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadGenerationDetailsList = ConvertLeadGenerationDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Generation Details";
            }
            return log;
        }
        public static List<LeadGenerationDetailsList> ConvertLeadGenerationDetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new LeadGenerationDetailsList()
                    {

                        LeadGenerateID = Convert.ToInt64(dr["LeadGenerateID"].ToString()),
                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
                        CusNameTitle = Convert.ToString(dr["CusNameTitle"].ToString()),
                        CusName = Convert.ToString(dr["CusName"].ToString()),
                        Area = Convert.ToString(dr["Area"].ToString()),
                        CusMobile = Convert.ToString(dr["CusMobile"].ToString()),
                        CusEmail = Convert.ToString(dr["CusEmail"].ToString()),
                        CollectedByName = Convert.ToString(dr["CollectedByName"].ToString())
                     

                    }).ToList();
        }
        public static LeadGenerationListDetails ConvertLeadGenerationListDetails(DataTable dt)
        {
            LeadGenerationListDetails log = new LeadGenerationListDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode ="0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadGenerateID = Convert.ToInt64(dt.Rows[0]["LeadGenerateID"]);
                log.LeadNo = Convert.ToString(dt.Rows[0]["LeadNo"]);
                log.LeadID = Convert.ToString(dt.Rows[0]["LeadID"]);
                log.LeadDate = Convert.ToString(dt.Rows[0]["LeadDate"]);
                log.LeadFrom = Convert.ToString(dt.Rows[0]["LeadFrom"]);
                log.LeadBy = Convert.ToString(dt.Rows[0]["LeadBy"]);
                log.ID_MediaMaster = Convert.ToInt64(dt.Rows[0]["ID_MediaMaster"]);
                log.SubMedia = Convert.ToInt64(dt.Rows[0]["SubMedia"]);
                log.PinCode = Convert.ToString(dt.Rows[0]["PinCode"]);
                log.CountryID = Convert.ToInt64(dt.Rows[0]["CountryID"]);
                log.StatesID = Convert.ToInt64(dt.Rows[0]["StatesID"]);
                log.DistrictID = Convert.ToInt64(dt.Rows[0]["DistrictID"]);
                log.PostID = Convert.ToInt64(dt.Rows[0]["PostID"]);
                log.Company = Convert.ToString(dt.Rows[0]["Company"]);
                log.CntryName = Convert.ToString(dt.Rows[0]["CntryName"]);
                log.StName = Convert.ToString(dt.Rows[0]["StName"]);
                log.DtName = Convert.ToString(dt.Rows[0]["DtName"]);
                log.PostName = Convert.ToString(dt.Rows[0]["PostName"]);
                log.CusPhnNo = Convert.ToString(dt.Rows[0]["CusPhnNo"]);
                log.LeadByName = Convert.ToString(dt.Rows[0]["LeadByName"]);
                log.ID_Customer = Convert.ToInt64(dt.Rows[0]["ID_Customer"]);
                log.FK_CustomerOthers = Convert.ToInt64(dt.Rows[0]["FK_CustomerOthers"]);
                log.CollectedBy = Convert.ToInt64(dt.Rows[0]["CollectedBy"]);
                log.CollectedByName = Convert.ToString(dt.Rows[0]["CollectedByName"]);
                log.CusName = Convert.ToString(dt.Rows[0]["CusName"]);
                log.CusMobile = Convert.ToString(dt.Rows[0]["CusMobile"]);
                log.CusEmail = Convert.ToString(dt.Rows[0]["CusEmail"]);
                log.CusAddress = Convert.ToString(dt.Rows[0]["CusAddress"]);
                log.CusAddress2 = Convert.ToString(dt.Rows[0]["CusAddress2"]);
                log.CusNameTitle = Convert.ToString(dt.Rows[0]["CusNameTitle"]);
                log.CusMobileAlternate = Convert.ToString(dt.Rows[0]["CusMobileAlternate"]);
                log.Area = Convert.ToString(dt.Rows[0]["Area"]);
                log.AreaID = Convert.ToInt64(dt.Rows[0]["AreaID"]);
              

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Generation Details";
            }
            return log;
        }
        public static LeadsDashBoardDetails ConvertLeadsDashBoardDetails(DataTable dt)
        {
            LeadsDashBoardDetails log = new LeadsDashBoardDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = Convert.ToString(dt.Rows[0]["ResponseCode"]); //dt.Rows[0].Field<string>("ResponseCode");
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]);// dt.Rows[0].Field<string>("ResponseMessage");
                log.TotalCount = Convert.ToDecimal(dt.Rows[0]["Total"]); //dt.Rows[0].Field<Int32>("Total");
                //log.TotalVAL = Convert.ToDecimal(dt.Rows[0]["Total"]); //dt.Rows[0].Field<Int32>("Total");
                log.LeadsDashBoardDetailsList = ConvertLeadsDashBoardDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Leads DashBoard Details";
            }
            return log;
        }
        public static List<LeadsDashBoardDetailsList> ConvertLeadsDashBoardDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadsDashBoardDetailsList()
                    {
                        Fileds = Convert.ToString(dr["Fileds"].ToString()),
                        Count = Convert.ToString(dr["Count"].ToString()),
                        Value = Convert.ToString(dr["Value"].ToString())


                    }).ToList();
        }
        public static AddQuatation ConvertAddQuatation(DataTable dt)
        {
            AddQuatation log = new AddQuatation();

            log.ResponseCode = "0";
            log.ResponseMessage = "Transaction Verified";
            log.ID_LeadGenerateProduct = 1;            



                
            return log;
        }
        public static AddDocument ConvertAddDocument(DataTable dt)
        {
            AddDocument log = new AddDocument();

            log.ResponseCode = "0";
            log.ResponseMessage = "Transaction Verified";
            if (dt.Rows[0].Field<string>("ResponseCode") == "0")
            {
                
                log.ID_LeadDocumentDetails = dt.Rows[0].Field<Int64>("ID_LeadDocumentDetails");
               
            }
            log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
            log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");





            return log;
        }
        public static DateWiseExpenseDetails ConvertDateWiseExpenseDetails(DataTable dt)
        {
            DateWiseExpenseDetails log = new DateWiseExpenseDetails();
            if (dt.Rows.Count > 0)
            {
                log.Total = dt.Rows[0].Field<string>("Total");
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.DateWiseExpenseDetailsList = ConvertDateWiseExpenseDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Date Wise Expense Details";
            }
            return log;
        }
        public static List<DateWiseExpenseDetailsList> ConvertDateWiseExpenseDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new DateWiseExpenseDetailsList()
                    {
                        TrnsDate = Convert.ToString(dr["TrnsDate"].ToString()),
                        ID_Expense = Convert.ToInt64(dr["ID_Expense"].ToString()),
                        ExpenseName = Convert.ToString(dr["ExpenseName"].ToString()),
                        Amount = Convert.ToString(dr["Amount"].ToString())


                    }).ToList();
        }
        public static ExpenseType ConvertExpenseType(DataTable dt)
        {
            ExpenseType log = new ExpenseType();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                  
                    log.ExpenseTypeList = ConvertExpenseTypeList(dt);
                  

                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Expense Type Details";
            }
            return log;
        }
        public static List<ExpenseTypeList> ConvertExpenseTypeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ExpenseTypeList()
                    {
                        ID_Expense = Convert.ToInt64(dr["ID_Expense"].ToString()),
                        ExpenseName = Convert.ToString(dr["ExpenseName"].ToString())


                    }).ToList();
        }
        public static UpdateExpenseDetails ConvertUpdateExpenseDetails(DataTable dt)
        {
            UpdateExpenseDetails log = new UpdateExpenseDetails();
            //if (dt.Rows.Count > 0)
            //{

            //    if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
            //    {
            //        log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
            //        log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
            //    }
            //    else
            //    {
            //        log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
            //        log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");

                    log.ID_Expense = 25;
            log.ResponseCode = "0";
            log.ResponseMessage = "Transaction Verified";

            //    }

            //}
            //else
            //{
            //    log.ResponseCode = "-2";
            //    log.ResponseMessage = "No Data Found";
            //}
            return log;
        }
        public static ActionType ConvertActionType(DataTable dt)
        {
            ActionType log = new ActionType();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");

                    log.ActionTypeList = ConvertActionTypeList(dt);


                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Action Type";
            }
            return log;
        }
        public static List<ActionTypeList> ConvertActionTypeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ActionTypeList()
                    {
                        ID_ActionType = Convert.ToInt64(dr["ID_ActionType"].ToString()),
                        ActionTypeName = Convert.ToString(dr["ActionTypeName"].ToString())


                    }).ToList();
        }
        public static PendingCountDetails ConvertPendingCountDetails(DataTable dt)
        {
            PendingCountDetails log = new PendingCountDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.Todolist = dt.Rows[0].Field<Int32>("Value");              
                log.OverDue = dt.Rows[1].Field<Int32>("Value");
                log.UpComingWorks = dt.Rows[2].Field<Int32>("Value");
                log.MyLeads = dt.Rows[3].Field<Int32>("Value");
                log.LeadRequest = 0;
                if (dt.Rows.Count > 4)
                {
                    log.LeadRequest = dt.Rows[4].Field<Int32>("Value");
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Pending count Details";
            }
            return log;
        }
        public static AgendaDetails ConvertAgendaDetails(DataTable dt)
        {
            AgendaDetails log = new AgendaDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");

                    log.AgendaDetailsList = ConvertAgendaDetailsList(dt);


                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Agenda  Details";
            }
            return log;
        }
        public static List<AgendaDetailsList> ConvertAgendaDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AgendaDetailsList()
                    {
                        ID_LeadGenerate= Convert.ToInt64(dr["ID_LeadGenerate"].ToString()),
                        ID_LeadGenerateProduct = Convert.ToInt64(dr["ID_LeadGenerateProduct"].ToString()),
                        ID_ActionType = Convert.ToInt64(dr["ID_ActionType"].ToString()),
                        ActionTypeName = Convert.ToString(dr["ActionTypeName"].ToString()),
                        TrnsDate = Convert.ToString(dr["TrnsDate"].ToString()),
                        EnquiryAbout = Convert.ToString(dr["EnquiryAbout"].ToString()),
                        Action = Convert.ToString(dr["Action"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        FollowedBy = Convert.ToString(dr["FollowedBy"].ToString()),
                        CustomerRemark = Convert.ToString(dr["CustomerRemark"].ToString()),
                        EmployeeRemark = Convert.ToString(dr["EmployeeRemark"].ToString()),
                        CustomerName = Convert.ToString(dr["CusName"].ToString()),
                        CustomerMobile = Convert.ToString(dr["CusMobile"].ToString()),
                        CustomerAddress = Convert.ToString(dr["CusAddress1"].ToString()),
                        SubMode = Convert.ToInt32(dr["SubMode"].ToString()),
                        Latitude = Convert.ToString(dr["Latitude"].ToString()),
                        Longitude = Convert.ToString(dr["Longitude"].ToString()),
                        LocationName = Convert.ToString(dr["LocationName"].ToString()),
                        FK_Priority = Convert.ToInt64(dr["FK_Priority"].ToString()),
                        PriorityName = Convert.ToString(dr["PriorityName"].ToString())
                      
                    }).ToList();
        }
        public static EmployeeProfileDetails ConvertEmployeeProfileDetails(DataTable dt)
        {
            EmployeeProfileDetails log = new EmployeeProfileDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                    log.ID_Employee = dt.Rows[0].Field<Int64>("ID_Employee");
                    log.Name = dt.Rows[0].Field<string>("CusName");
                    log.Address = dt.Rows[0].Field<string>("CusAddress1");
                    log.MobileNumber = dt.Rows[0].Field<string>("CusMobile");
                    log.Email = dt.Rows[0].Field<string>("CusEmail");
                    log.EmpDOB = dt.Rows[0].Field<string>("EmpDOB");
                    log.LoginDate = dt.Rows[0].Field<string>("LoginDate");
                    log.LoginTime = dt.Rows[0].Field<string>("LoginTime");
                    log.LoginStatus = dt.Rows[0].Field<string>("LoginStatus");
                    log.LoginMode = dt.Rows[0].Field<Int32>("LoginMode");
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Profile Details";
            }
            return log;
        }
        public static UpdateUserLoginStatus ConvertUpdateUserLoginStatus(DataTable dt)
        {
            UpdateUserLoginStatus log = new UpdateUserLoginStatus();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                    log.FK_Employee = dt.Rows[0].Field<Int64>("FK_Employee");
                    log.Name = dt.Rows[0].Field<string>("CusName");
                    log.Address = dt.Rows[0].Field<string>("CusAddress1");
                    log.LocLatitude = dt.Rows[0].Field<string>("LocLatitude");
                    log.LocLongitude = dt.Rows[0].Field<string>("LocLongitude");
                    log.LocationName = dt.Rows[0].Field<string>("LocationName");
                    log.LoginDate = dt.Rows[0].Field<string>("LoginDate");
                    log.LoginTime = dt.Rows[0].Field<string>("LoginTime");
                    log.LoginStatus = dt.Rows[0].Field<string>("LoginStatus");
                    log.LoginMode = dt.Rows[0].Field<Int32>("LoginMode");
                    log.DutyStatus= dt.Rows[0].Field<string>("DutyStatus");
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the User Login";
            }
            return log;
        }
        public static ActivitiesDetails ConvertActivitiesDetails(DataTable dt)
        {
            ActivitiesDetails log = new ActivitiesDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ActivitiesDetailsList = ConvertActivitiesDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Activities Details";
            }
            return log;
        }
        public static List<ActivitiesDetailsList> ConvertActivitiesDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ActivitiesDetailsList()
                    {
                        ID_LeadGenerateProduct = Convert.ToString(dr["ID_LeadGenerateProduct"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        EnquiryAbout = Convert.ToString(dr["EnquiryAbout"].ToString()),
                        LgpDescription = Convert.ToString(dr["LgpDescription"].ToString()),
                        Action = Convert.ToString(dr["Action"].ToString()),
                        ActionDate = Convert.ToString(dr["ActionDate"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        Agentremarks = Convert.ToString(dr["Agentremarks"].ToString()),
                        FollowedBy = Convert.ToString(dr["FollowedBy"].ToString()),
                        ActionType = Convert.ToString(dr["ActionType"].ToString())
                    }).ToList();
        }
        public static LeadGenerateReport ConvertLeadGenerateReport(DataTable dt)
        {
            LeadGenerateReport log = new LeadGenerateReport();
            if (dt.Rows.Count > 0)
            {

                
                    log.ResponseCode ="0";
                    log.ResponseMessage = "Transaction Verified";
                    log.LeadGenerateReportList = ConvertLeadGenerateReportList(dt);                

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Generate Report";
            }
            return log;
        }
        public static List<LeadGenerateReportList> ConvertLeadGenerateReportList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadGenerateReportList()
                    {
                        LeadNo = Convert.ToString(dr["Lead No"].ToString()),
                        ProjectName = Convert.ToString(dr["Project Name"].ToString()),
                        NextActionDate = Convert.ToString(dr["NextActionDate"].ToString()),
                        CustomerName = Convert.ToString(dr["Customer Name"].ToString()),
                        ContactNo = Convert.ToString(dr["Contact No."].ToString()),
                        ProductName = Convert.ToString(dr["Product Name"].ToString()),
                        Department = Convert.ToString(dr["Department"].ToString()),
                        LeadBY = Convert.ToString(dr["Lead BY"].ToString()),
                        ActionModuleName = Convert.ToString(dr["Action Module Name"].ToString()),
                        ActionStatusname = Convert.ToString(dr["Action Status name"].ToString()),
                        PriorityType = Convert.ToString(dr["Priority Type"].ToString()),
                        Branch = Convert.ToString(dr["Branch"].ToString()),
                        Company = Convert.ToString(dr["Company"].ToString())
                    }).ToList();
        }
        public static ProductWiseLeadReport ConvertProductWiseLeadReport(DataTable dt)
        {
            ProductWiseLeadReport log = new ProductWiseLeadReport();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductWiseLeadReportList = ConvertProductWiseLeadReportList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Wise Lead Report";
            }
            return log;
        }
        public static List<ProductWiseLeadReportList> ConvertProductWiseLeadReportList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProductWiseLeadReportList()
                    {
                        ProductName = Convert.ToString(dr["Product Name"].ToString()),
                        NextActionDate = Convert.ToString(dr["NextActionDate"].ToString()),
                        LeadBy = Convert.ToString(dr["Lead By"].ToString()),
                        LeadNo = Convert.ToString(dr["Lead No"].ToString())
                        
                    }).ToList();
        }
        public static PriorityWiseLeadReport ConvertPriorityWiseLeadReport(DataTable dt)
        {
            PriorityWiseLeadReport log = new PriorityWiseLeadReport();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PriorityWiseLeadReportList = ConvertPriorityWiseLeadReportList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Priority Wise Lead Report";
            }
            return log;
        }
        public static List<PriorityWiseLeadReportList> ConvertPriorityWiseLeadReportList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new PriorityWiseLeadReportList()
                    {
                        PriorityType = Convert.ToString(dr["Priority Type"].ToString()),
                        LeadNo = Convert.ToString(dr["Lead No"].ToString()),
                        CustomerName = Convert.ToString(dr["Customer Name"].ToString()),
                        ContactNo = Convert.ToString(dr["Contact No."].ToString()),
                        ActionStatusName = Convert.ToString(dr["Action Status Name"].ToString())

                    }).ToList();
        }
        public static ReportNameDetails ConvertReportNameDetails(DataTable dt)
        {
            ReportNameDetails log = new ReportNameDetails();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ReportNameDetailsList = ConvertReportNameDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Report Name  Details";
            }
            return log;
        }
        public static List<ReportNameDetailsList> ConvertReportNameDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ReportNameDetailsList()
                    {
                        ReportMode = Convert.ToInt64(dr["ReportMode"].ToString()),
                        ReportName = Convert.ToString(dr["ReportName"].ToString())

                    }).ToList();
        }
        public static GroupingDetails ConvertGroupingDetails(DataTable dt)
        {
            GroupingDetails log = new GroupingDetails();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.GroupingDetailsList = ConvertGroupingDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Grouping  Details";
            }
            return log;
        }
        public static List<GroupingDetailsList> ConvertGroupingDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new GroupingDetailsList()
                    {
                        GroupId = Convert.ToInt64(dr["GroupId"].ToString()),
                        GroupName = Convert.ToString(dr["GroupName"].ToString())

                    }).ToList();
        }
        public static ActionListDetailsReport ConvertActionListDetailsReport(DataTable dt)
        {
            ActionListDetailsReport log = new ActionListDetailsReport();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.ActionList = ConvertActionList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Action List Details Report";
            }
            return log;
        }
        public static List<ActionList> ConvertActionList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ActionList()
                    {

                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        ProductORProject = Convert.ToString(dr["Prouct"].ToString()),
                        Action = Convert.ToString(dr["NextAction"].ToString()),
                        ActionType = Convert.ToString(dr["FollowUpMethod"].ToString()),
                        FollowupDt = Convert.ToString(dr["NextActionDate"].ToString()),
                        Assignee = Convert.ToString(dr["AssignedTo"].ToString()),
                        DueDays = Convert.ToInt32(dr["DueDays"].ToString()),
                        Remarks = Convert.ToString(dr["Remarks"].ToString())
                    }).ToList();

        }
        public static StatusListDetailsReport ConvertStatusListDetailsReport(DataTable dt)
        {
            StatusListDetailsReport log = new StatusListDetailsReport();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.StatusListDetails = ConvertStatusListDetails(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the  Status List Details Report";
            }
            return log;
        }
        public static List<StatusListDetails> ConvertStatusListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new StatusListDetails()
                    {

                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        ProductORProject = Convert.ToString(dr["Prouct"].ToString()),
                        Assignee = Convert.ToString(dr["AssignedTo"].ToString()),
                        CompletedDate = Convert.ToString(dr["CompletedDate"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),                    
                        Remarks = Convert.ToString(dr["Remarks"].ToString())
                    }).ToList();

        }
        public static NewListDetailsReport ConvertNewListDetailsReport(DataTable dt)
        {
            NewListDetailsReport log = new NewListDetailsReport();
            if (dt.Rows.Count > 0)
            {

                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.NewListDetails = ConvertNewListDetails(dt);
                //}

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the New List Details Report";
            }
            return log;
        }
        public static List<NewListDetails> ConvertNewListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new NewListDetails()
                    {

                        LeadNo = Convert.ToString(dr["Lead No"].ToString()),
                        LeadDate = Convert.ToString(dr["Lead Date"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        PhnNo = Convert.ToString(dr["Mobile"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        CollectedBy = Convert.ToString(dr["Collected By"].ToString()),
                        Priority = Convert.ToString(dr["Priority"].ToString()),
                        CurrentStatus = Convert.ToString(dr["Current Status"].ToString()),
                        Branch = Convert.ToString(dr["Branch"].ToString()),
                        Remarks = Convert.ToString(dr["Remarks"].ToString()),
                        LgCollectedBy = Convert.ToString(dr["Lead Collected By"].ToString()),
                        Area = Convert.ToString(dr["Area"].ToString()),
                        LeadFrom = Convert.ToString(dr["Lead From"].ToString()),
                        LeadByName = Convert.ToString(dr["Lead By Name"].ToString()),
                        AssignedTo = Convert.ToString(dr["Assigned To"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        MRP = Convert.ToString(dr["MRP"].ToString()),
                        Address = Convert.ToString(dr["Address"].ToString())

                    }).ToList();

        }
        public static FollowUpListDetailsReport ConvertFollowUpListDetailsReport(DataTable dt)
        {
            FollowUpListDetailsReport log = new FollowUpListDetailsReport();
            if (dt.Rows.Count > 0)
            {
              
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FollowUpListDetails = ConvertFollowUpListDetails(dt);
              
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the FollowUp List Details Report";
            }
            return log;
        }
        public static List<FollowUpListDetails> ConvertFollowUpListDetails(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new FollowUpListDetails()
                    {                       
                        ID_LeadGenerateProduct = Convert.ToString(dr["ID_LeadGenerateProduct"].ToString()),
                        LeadNo = Convert.ToString(dr["Lead No"].ToString()),
                        LeadDate = Convert.ToString(dr["Lead Date"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        PhnNo = Convert.ToString(dr["Mobile"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        NextAction = Convert.ToString(dr["Next Action"].ToString()),
                        NextActionDate = Convert.ToString(dr["Next Action Date"].ToString()),
                        FollowUpMethod = Convert.ToString(dr["Follow Up Method"].ToString()),
                        AssignedTo = Convert.ToString(dr["Assigned To"].ToString()),
                        CompletedDate = Convert.ToString(dr["Completed Date"].ToString()),
                        DueDays = Convert.ToString(dr["Due Days"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        BRANCH = Convert.ToString(dr["Branch"].ToString()),
                        PriorityID = Convert.ToString(dr["hdn_PriorityID"].ToString()),
                        Priority = Convert.ToString(dr["Priority"].ToString()),
                        criteria = Convert.ToString(dr["hdn_criteria"].ToString()),
                        Remarks = Convert.ToString(dr["Remarks"].ToString()),
                        LgCollectedBy = Convert.ToString(dr["Lead Collected By"].ToString()),
                        Category = Convert.ToString(dr["Category"].ToString()),
                        Area = Convert.ToString(dr["Area"].ToString())

                    }).ToList();

        }
        public static LeadGenerationDefaultvalueSettings ConvertLeadGenerationDefaultvalueSettings(DataTable dt)
        {
            LeadGenerationDefaultvalueSettings log = new LeadGenerationDefaultvalueSettings();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }

                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";

                    log.ID_Employee = Convert.ToString(dt.Rows[0]["ID_Employee"]);
                    log.EmpFName = Convert.ToString(dt.Rows[0]["EmpFName"]);
                    log.ID_BranchType = Convert.ToString(dt.Rows[0]["ID_BranchType"]);
                    log.BranchType = Convert.ToString(dt.Rows[0]["BranchType"]);
                    log.ID_Branch = Convert.ToString(dt.Rows[0]["ID_Branch"]);
                    log.Branch = Convert.ToString(dt.Rows[0]["Branch"]);                   
                    log.FK_Department = Convert.ToString(dt.Rows[0]["FK_Department"]);
                    log.Department = Convert.ToString(dt.Rows[0]["Department"]);                  
                    log.FK_Country = Convert.ToString(dt.Rows[0]["FK_Country"]);
                    log.Country = Convert.ToString(dt.Rows[0]["Country"]);
                    log.FK_States = Convert.ToString(dt.Rows[0]["FK_States"]);
                    log.StateName = Convert.ToString(dt.Rows[0]["StateName"]);
                    log.FK_District = Convert.ToString(dt.Rows[0]["FK_District"]);
                    log.DistrictName = Convert.ToString(dt.Rows[0]["DistrictName"]);

                   
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Generation Details";
            }
            return log;
        }
        public static DeleteLeadGenerate ConvertDeleteLeadGenerate(DataTable dt)
        {
            DeleteLeadGenerate log = new DeleteLeadGenerate();
            if (dt.Rows.Count > 0)
            {
                if(dt.Columns.Contains("ErrCode"))
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
                    log.Message = Convert.ToString(dt.Rows[0]["ErrMsg"]);
                }
                else
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Delete Successfully";
                    log.Message = "Delete Successfully";
                }                   

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Deleted";
            }
            return log;
        }
        public static AgendaType ConvertAgendaType(DataTable dt)
        {
            AgendaType log = new AgendaType();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                    log.Count = 2;
                log.AgendaTypeList = ConvertAgendaTypeList(dt);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Agenda Type Details";
            }
            return log;
        }
        public static List<AgendaTypeList> ConvertAgendaTypeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AgendaTypeList()
                    {

                        Id_Agenda = Convert.ToInt32(dr["Id_Agenda"].ToString()),
                        AgendaName = Convert.ToString(dr["AgendaName"].ToString()),
                        ImageCode= Convert.ToString(dr["ImageCode"].ToString()),
                        Count=0


                    }).ToList();

        }
        public static UpdateLeadManagement ConvertUpdateLeadManagement(DataTable dt)
        {
            UpdateLeadManagement log = new UpdateLeadManagement();
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("ErrCode"))
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
                    log.LeadNo = "";
                }
                else if(dt.Columns.Contains("LeadNo"))
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]);
                    log.LeadNo = Convert.ToString(dt.Rows[0]["LeadNo"]);
                }                
               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static DocumentDetails ConvertDocumentDetails(DataTable dt)
        {
            DocumentDetails log = new DocumentDetails();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                    log.DocumentDetailsList = ConvertDocumentDetailsList(dt);
                }
                
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Document Details";
            }
            return log;
        }
        public static List<DocumentDetailsList> ConvertDocumentDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new DocumentDetailsList()
                    {
                       
                        ID_LeadDocumentDetails = Convert.ToInt64(dr["ID_LeadDocumentDetails"].ToString()),
                        DocumentDate = Convert.ToString(dr["DocumentDate"].ToString()),
                        DocumentSubject = Convert.ToString(dr["DocumentSubject"].ToString()),
                        DocumentDescription = Convert.ToString(dr["DocumentDescription"].ToString()),
                        DocumentImageFormat = Convert.ToString(dr["DocImageFormat"].ToString())


                    }).ToList();
        }
        public static DocumentImageDetails ConvertDocumentImageDetails(DataTable dt)
        {
            DocumentImageDetails log = new DocumentImageDetails();
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                }
                else
                {
                    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                    log.DocumentImage = dt.Rows[0].Field<byte[]>("DocumentImage");
                   
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Document Image Details";
            }
            return log;
        }
        public static AddRemark ConvertAddRemark(DataTable dt)
        {
            AddRemark log = new AddRemark();
            if (dt.Rows.Count > 0)
            {

                //if (dt.Rows[0].Field<string>("ResponseCode") == "-1")
                //{
                //    log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                //    log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                //}
                //else
                //{
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ID_LeadGenerateProduct = 64;

                //}

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Remark Details";
            }
            return log;
        }
        public static TodoListLeadDetails ConvertTodoListLeadDetails(DataTable dt)
        {
            TodoListLeadDetails log = new TodoListLeadDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.TodoListLeadDetailsList = ConvertTodoListLeadDetailsList(dt);
               

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead ToDo List Details";
            }
            return log;
        }
        public static List<TodoListLeadDetailsList> ConvertTodoListLeadDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new TodoListLeadDetailsList()
                    {

                       ID_TodoListLeadDetails = Convert.ToInt32(dr["ID_Mode"].ToString()),
                        TodoListLeadDetailsName = Convert.ToString(dr["ModeName"].ToString())
                        


                    }).ToList();
        }
        public static ServiceCustomerDetails ConvertServiceCustomerDetails(DataTable dt)
        {
            ServiceCustomerDetails log = new ServiceCustomerDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceCustomerList = ConvertServiceCustomerDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Customer Details";
            }
            return log;
        }
        public static List<ServiceCustomerList> ConvertServiceCustomerDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ServiceCustomerList()
                    {
                        Customer_ID = Convert.ToString(dr["ID_FIELD"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        Address = Convert.ToString(dr["Address"].ToString()),
                        CusMode = Convert.ToString(dr["CusMode"].ToString()),
                        OtherMobile = Convert.ToString(dr["OtherMobile"].ToString())
                    }).ToList();          
        }
        public static CommonPopupDetails ConvertCommonPopupDetails(DataTable dt)
        {
            CommonPopupDetails log = new CommonPopupDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.CommonPopupList = ConvertCommonPopupDetailsList(dt);


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records...";
            }
            return log;
        }
        public static List<CommonPopupList> ConvertCommonPopupDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new CommonPopupList()
                    {
                        Code = Convert.ToString(dr["ID_Mode"].ToString()),
                        Description = Convert.ToString(dr["ModeName"].ToString())
                    }).ToList();
        }
        public static ServiceProductDetails ConvertServiceProductDetails(DataTable dt)
        {
            ServiceProductDetails log = new ServiceProductDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceProductDetailsList = ConvertServiceProductDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Product Details";
            }
            return log;
        }
        public static List<ServiceProductDetailsList> ConvertServiceProductDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ServiceProductDetailsList()
                    {
                     
                        ID_Product = Convert.ToString(dr["ID_FIELD"].ToString()),
                        ProductName = Convert.ToString(dr["Name"].ToString())

                    }).ToList();

        }
        public static ServiceDet ConvertServiceDetails(DataTable dt)
        {
            ServiceDet log = new ServiceDet();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceDetailsList = ConvertServiceDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Details";
            }
            return log;
        }
        public static List<ServiceDetailsList> ConvertServiceDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ServiceDetailsList()
                    {

                        ID_Services = Convert.ToString(dr["ID_Services"].ToString()),
                        ServicesName = Convert.ToString(dr["ServicesName"].ToString())

                    }).ToList();

        }
        public static ComplaintDetails ConvertComplaintDetails(DataTable dt)
        {
            ComplaintDetails log = new ComplaintDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ComplaintDetailsList = ConvertComplaintDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Complaint Details";
            }
            return log;
        }
        public static List<ComplaintDetailsList> ConvertComplaintDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ComplaintDetailsList()
                    {
                        ID_ComplaintList = Convert.ToString(dr["ID_ComplaintList"].ToString()),
                        ComplaintName = Convert.ToString(dr["ComplaintName"].ToString())
                    }).ToList();

        }
        public static WarrantyDetails ConvertWarrantyDetails(DataTable dt)
        {
            WarrantyDetails log = new WarrantyDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.WarrantyDetailsList = ConvertWarrantyDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Warranty Details";
            }
            return log;
        }
        public static List<WarrantyDetailsList> ConvertWarrantyDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new WarrantyDetailsList()
                    {
                        ID_CustomerWiseProductDetails = Convert.ToString(dr["ID_CustomerWiseProductDetails"].ToString()),
                        ID_WarrantyDetails = Convert.ToString(dr["ID_WarrantyDetails"].ToString()),
                        WarrantyType = Convert.ToString(dr["WarrantyType"].ToString()),
                        ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"].ToString()),
                        ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"].ToString()),
                        ReplacementWarrantyStatus = Convert.ToString(dr["ReplacementWarrantyStatus"].ToString()),
                        ServiceWarrantyStatus = Convert.ToString(dr["ServiceWarrantyStatus"].ToString())
                    }).ToList();

        }
        public static ProductHistoy ConvertProductHistory(DataTable dt)
        {
            ProductHistoy log = new ProductHistoy();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductHistoyList = ConvertProductHistoryList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product History Details";
            }
            return log;
        }
        public static List<ProductHistoyList> ConvertProductHistoryList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProductHistoyList()
                    {
                        TicketNo = Convert.ToString(dr["TicketNo"].ToString()),
                        RegOn = Convert.ToString(dr["RegOn"].ToString()),
                        Complaint = Convert.ToString(dr["Complaint"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        AttendedBy = Convert.ToString(dr["AttendedBy"].ToString()),
                        EmployeeRemarks = Convert.ToString(dr["EmployeeRemarks"].ToString())
                    }).ToList();

        }
        public static SalesHistory ConvertSalesHistory(DataTable dt)
        {
            SalesHistory log = new SalesHistory();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.SalesHistoryList = ConvertSalesHistoryList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Sales History Details";
            }
            return log;
        }
        public static List<SalesHistoryList> ConvertSalesHistoryList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new SalesHistoryList()
                    {
                        ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"].ToString()),
                        InvoiceNo = Convert.ToString(dr["InvoiceNo"].ToString()),
                        InvoiceDate = Convert.ToString(dr["InvoiceDate"].ToString()),
                        Dealer = Convert.ToString(dr["Dealer"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        Quantity = Convert.ToString(dr["Quantity"].ToString())
                     
                    }).ToList();

        }
        public static UpdateCustomerServiceRegister ConvertUpdateCustomerServiceRegister(DataTable dt)
        {
            UpdateCustomerServiceRegister log = new UpdateCustomerServiceRegister();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]); 
                log.CSRTickno = Convert.ToString(dt.Rows[0]["CSRTickno"]);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static CompanyLogDetails ConvertCompanyLogDetails(DataTable dt)
        {
            CompanyLogDetails log = new CompanyLogDetails();
            if (dt.Rows.Count > 0)
            {

                //log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                //log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                //log.CompanyLog = dt.Rows[0].Field<byte[]>("CompanyLog");
                //log.CompanyName = dt.Rows[0].Field<string>("CompanyName");
                //log.DisplayType = dt.Rows[0].Field<string>("DisplayType");
                //log.BranchName = dt.Rows[0].Field<string>("BranchName");
                log.ResponseCode = Convert.ToString(dt.Rows[0]["ResponseCode"]);
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ResponseMessage"]);
                log.CompanyLogo = (byte[])dt.Rows[0]["CompanyLogo"];
                log.CompanyName = Convert.ToString(dt.Rows[0]["CompanyName"]);
                log.DisplayType = Convert.ToString(dt.Rows[0]["DisplayType"]);
                log.BranchName = Convert.ToString(dt.Rows[0]["BranchName"]);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Company Log Details";
            }
            return log;
        }

        public static MediaDetails ConvertMediaDetails(DataTable dt)
        {
            MediaDetails log = new MediaDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MediaList = ConvertMediaList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Media Details";
            }
            return log;
        }
        public static List<MediaDetailsList> ConvertMediaList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MediaDetailsList()
                    {
                        ID_FIELD = Convert.ToString(dr["ID_FIELD"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString())

                    }).ToList();

        }
        public static ServiceCountDetails ConvertServiceCountDetails(DataTable dt)
        {
            ServiceCountDetails log = new ServiceCountDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceCountList = ConvertServiceCountDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No data found for the selected Branch And Employee";
            }
            return log;
        }
        public static List<ServiceCountListDetails> ConvertServiceCountDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ServiceCountListDetails()
                    {
                        MasterID = Convert.ToString(dr["MasterID"].ToString()),
                        Field = Convert.ToString(dr["Field"].ToString()),
                        value = Convert.ToString(dr["Value"].ToString())

                    }).ToList();

        }
        public static ServiceAssignNewDetails ConvertServiceAssignNewDetails(DataTable dt)
        {
            ServiceAssignNewDetails log = new ServiceAssignNewDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceAssignNewList = ConvertServiceAssignNewList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Assign Details";
            }
            return log;
        }
        public static List<ServiceAssignNewList> ConvertServiceAssignNewList(DataTable dt)
        {
            List<ServiceAssignNewList> lst = new List<ServiceAssignNewList>();
            foreach (DataRow dr in dt.Rows)
            {
                ServiceAssignNewList obj = new ServiceAssignNewList();
                obj.ID_CustomerServiceRegister = Convert.ToString(dr["ID_CustomerServiceRegister"]);
                obj.ID_CustomerServiceRegisterProductDetails = Convert.ToString(dr["ID_CustomerServiceRegisterProductDetails"]);
                obj.TicketNo = Convert.ToString(dr["TicketNo"]);
                obj.TicketDate = Convert.ToString(dr["TicketDate"]);
                obj.Branch = Convert.ToString(dr["Branch"]);
                obj.Customer = Convert.ToString(dr["Customer"]);
                obj.Mobile = Convert.ToString(dr["Mobile"]);
                obj.PriorityCode = Convert.ToString(dr["PriorityCode"]);
                obj.Priority = Convert.ToString(dr["Priority"]);
                obj.StatusCode = Convert.ToString(dr["StatusCode"]);
                obj.Status = Convert.ToString(dr["Status"]);
               // obj.Assign = Convert.ToString(dr["Assign"]);
               // obj.ProductStatus = Convert.ToString(dr["ProductStatus"]);
                obj.Area = Convert.ToString(dr["Area"]);
                obj.Due = Convert.ToString(dr["Due"]);
                obj.Employee = Convert.ToString(dr["Employee"]);
                obj.Channel = Convert.ToString(dr["Channel"]);
                obj.ID_Master = Convert.ToString(dr["ID_Master"]);
                obj.TransMode = Convert.ToString(dr["TransMode"]);
                obj.Product = Convert.ToString(dr["Product"]);
                obj.TicketStatus = Convert.ToInt16(dr["TicketStatus"]);
                lst.Add(obj);
            }
            return lst;

        }
        public static CustomerBalanceDetails ConvertCustomerBalanceDetails(DataTable dt)
        {
            CustomerBalanceDetails log = new CustomerBalanceDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.CustomerBalanceList = ConvertCustomerBalanceList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "Customer has no balance";
            }
            return log;
        }
        public static List<CustomerBalanceList> ConvertCustomerBalanceList(DataTable dt)
        {
            List<CustomerBalanceList> lst = new List<CustomerBalanceList>();
            foreach (DataRow dr in dt.Rows)
            {
                CustomerBalanceList obj = new CustomerBalanceList();
               // obj.ID_CustomerServiceRegister = Convert.ToString(dr["ID_CustomerServiceRegister"]);
              
                //obj.SlNo = Convert.ToString(dr["SlNo"]);
                obj.AccountDetails = Convert.ToString(dr["AccountDetails"]);
                obj.Balance = Convert.ToString(dr["Balance"]);
                obj.Due = Convert.ToString(dr["Due"]);
                lst.Add(obj);
            }
            return lst;

        }
        public static WarrantyAMCDetails ConvertWarrantyAMCDetails(DataTable dt)
        {
            WarrantyAMCDetails log = new WarrantyAMCDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.WarrantyAMCDetailsList = ConvertWarrantyAMCDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Warranty AMC Details";
            }
            return log;
        }
        public static List<WarrantyAMCDetailsList> ConvertWarrantyAMCDetailsList(DataTable dt)
        {
            List<WarrantyAMCDetailsList> lst = new List<WarrantyAMCDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                WarrantyAMCDetailsList obj = new WarrantyAMCDetailsList();
                // obj.ID_CustomerServiceRegister = Convert.ToString(dr["ID_CustomerServiceRegister"]);

                //obj.SlNo = Convert.ToString(dr["SlNo"]);
                obj.InvoiceNo = Convert.ToString(dr["InvoiceNo"]);
                obj.InvoiceDate = Convert.ToString(dr["InvoiceDate"]);
                obj.Dealer = Convert.ToString(dr["Dealer"]);
                lst.Add(obj);
            }
            return lst;

        }
        public static ServiceAssignOnGoingDetails ConvertServiceAssignOnGoingDetails(DataTable dt)
        {
            ServiceAssignOnGoingDetails log = new ServiceAssignOnGoingDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceAssignOnGoingList = ConvertServiceAssignOnGoingList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Assign OnGoing List";
            }
            return log;
        }
        public static List<ServiceAssignOnGoingList> ConvertServiceAssignOnGoingList(DataTable dt)
        {
            List<ServiceAssignOnGoingList> lst = new List<ServiceAssignOnGoingList>();
            foreach (DataRow dr in dt.Rows)
            {
                ServiceAssignOnGoingList obj = new ServiceAssignOnGoingList();
                obj.ID_CustomerServiceRegister = Convert.ToString(dr["ID_CustomerServiceRegister"]);
                obj.TicketNo = Convert.ToString(dr["TicketNo"]);
                obj.TicketDate = Convert.ToString(dr["TicketDate"]);
                obj.Branch = Convert.ToString(dr["Branch"]);
                obj.Customer = Convert.ToString(dr["Customer"]);
                obj.Mobile = Convert.ToString(dr["Mobile"]);
                obj.PriorityCode = Convert.ToString(dr["PriorityCode"]);
                obj.Priority = Convert.ToString(dr["Priority"]);
                obj.StatusCode = Convert.ToString(dr["StatusCode"]);
                obj.Status = Convert.ToString(dr["Status"]);
                obj.Assign = Convert.ToString(dr["Assign"]);
                obj.ProductStatus = Convert.ToString(dr["ProductStatus"]);
                obj.Area = Convert.ToString(dr["Area"]);
                obj.Due = Convert.ToString(dr["Due"]);
                obj.Employee = Convert.ToString(dr["Employee"]);
                obj.Channel = Convert.ToString(dr["Channel"]);
                lst.Add(obj);
            }
            return lst;

        }
        public static RoleDetails ConvertRoleDetails(DataTable dt)
        {
            RoleDetails log = new RoleDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.RoleDetailsList = ConvertRoleDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Role Details";
            }
            return log;
        }
        public static List<RoleDetailsList> ConvertRoleDetailsList(DataTable dt)
        {
            List<RoleDetailsList> lst = new List<RoleDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                RoleDetailsList obj = new RoleDetailsList();
                obj.ID_Role = Convert.ToInt32(dr["ID_Mode"].ToString());
                obj.RoleName = Convert.ToString(dr["ModeName"].ToString());
                lst.Add(obj);
            }
            return lst;

        }
        public static ServiceAssignedWork ConvertServiceAssignedWorkDetails(DataTable dt)
        {
            ServiceAssignedWork log = new ServiceAssignedWork();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceAssignedWorkList = ConvertServiceAssignedWorkDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Assigned Work Details List";
            }
            return log;
        }
        public static List<ServiceAssignedWorkList> ConvertServiceAssignedWorkDetailsList(DataTable dt)
        {
            List<ServiceAssignedWorkList> lst = new List<ServiceAssignedWorkList>();
            foreach (DataRow dr in dt.Rows)
            {
                ServiceAssignedWorkList obj = new ServiceAssignedWorkList();
                obj.TicketNo = Convert.ToString(dr["TicketNo"].ToString());
                obj.VisitDate = Convert.ToString(dr["VisitDate"].ToString());
                obj.VisitTime = Convert.ToString(dr["VisitTime"].ToString());
                lst.Add(obj);
            }
            return lst;

        }
        public static ServiceAssignDetails ConvertServiceAssignDetails(DataSet ds)
        {
            ServiceAssignDetails log = new ServiceAssignDetails();
            if (ds.Tables.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    log.ID_Customerserviceregister = ds.Tables[0].Rows[0].Field<Int64>("ID_Customerserviceregister");
                    log.FromDate = ds.Tables[0].Rows[0].Field<string>("FromDate");
                    log.ToDate = ds.Tables[0].Rows[0].Field<string>("ToDate");
                    log.FromTime = ds.Tables[0].Rows[0].Field<string>("FromTime");
                    log.ToTime = ds.Tables[0].Rows[0].Field<string>("ToTime");
                    log.Customer = ds.Tables[0].Rows[0].Field<string>("Customer");
                    log.Address = ds.Tables[0].Rows[0].Field<string>("Address");
                    log.Remarks = ds.Tables[0].Rows[0].Field<string>("Remarks");
                    log.Mobile = ds.Tables[0].Rows[0].Field<string>("Mobile");
                    log.OtherMobile = ds.Tables[0].Rows[0].Field<string>("OtherMobile");
                    log.Landmark = ds.Tables[0].Rows[0].Field<string>("Landmark");
                    log.Ticket = ds.Tables[0].Rows[0].Field<string>("Ticket");
                    log.Priority = ds.Tables[0].Rows[0].Field<byte>("Priority");
                    log.PriorityName = ds.Tables[0].Rows[0].Field<string>("PriorityName");
                    log.CurrentStatus = ds.Tables[0].Rows[0].Field<byte>("CurrentStatus");
                    log.Productname = ds.Tables[0].Rows[0].Field<string>("Productname");
                    log.ProductComplaint = ds.Tables[0].Rows[0].Field<string>("ProductComplaint");
                    log.ProductDescription = ds.Tables[0].Rows[0].Field<string>("ProductDescription");
                    log.ID_Customer = ds.Tables[0].Rows[0].Field<Int64>("ID_Customer");
                }
                if(ds.Tables[1].Rows.Count>0)
                {
                    log.EmployeeRoleDetailsList = ConvertEmployeeRoleDetailsList(ds.Tables[1]);
                }
              

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service Assign Details";
            }
            return log;
        }
        public static List<EmployeeRoleDetailsList> ConvertEmployeeRoleDetailsList(DataTable dt)
        {

            List<EmployeeRoleDetailsList> lst = new List<EmployeeRoleDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeRoleDetailsList obj = new EmployeeRoleDetailsList();
                obj.ID_Employee = Convert.ToString(dr["ID_Employee"].ToString());
                obj.EmpCode = Convert.ToString(dr["EmpCode"].ToString());
                obj.Employee = Convert.ToString(dr["Employee"].ToString());
                obj.ID_CSAEmployeeType = Convert.ToString(dr["ID_CSAEmployeeType"].ToString());
                obj.EmployeeType = Convert.ToString(dr["EmployeeType"].ToString());
                obj.Attend = Convert.ToString(dr["Attend"].ToString());
                obj.DepartmentID = Convert.ToString(dr["DepartmentID"].ToString());
                obj.Designation = Convert.ToString(dr["Designation"].ToString());
                obj.Department = Convert.ToString(dr["Department"].ToString());           
                lst.Add(obj);
            }
            return lst;

        }
        public static CustomerserviceassignUpdate ConvertCustomerserviceassignUpdate(DataTable dt)
        {
            CustomerserviceassignUpdate log = new CustomerserviceassignUpdate();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Successfully Updated";
                log.Message = "Successfully Updated";

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static CustomerserviceassignEdit ConvertCustomerserviceassignEdit(DataTable dt)
        {
            CustomerserviceassignEdit log = new CustomerserviceassignEdit();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Successfully Updated";
                log.Message = "Successfully Updated";

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static CustomerServiceRegisterCount ConvertCustomerServiceRegisterCount(DataTable dt)
        {
            CustomerServiceRegisterCount log = new CustomerServiceRegisterCount();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.WarrantyCount = Convert.ToInt32(dt.Rows[0]["WarrantyCount"]);
                log.ServiceHistoryCount = Convert.ToInt32(dt.Rows[0]["ServiceHistoryCount"]);
                log.SalesCount = Convert.ToInt32(dt.Rows[0]["SalesCount"]);
                log.CustomerDueCount = Convert.ToInt32(dt.Rows[0]["CustomerDueCount"]);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Customer Service Register Details";
            }
            return log;
        }
        public static ServiceFollowUpdetails ConvertServiceFollowUpdetails(DataTable dt)
        {
            ServiceFollowUpdetails log = new ServiceFollowUpdetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";               
                log.ServiceFollowUpdetailsList = ConvertServiceFollowUpdetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service FollowUp Details";
            }
            return log;
        }
        public static List<ServiceFollowUpdetailsList> ConvertServiceFollowUpdetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ServiceFollowUpdetailsList()
                    {
                        ID_Customerserviceregister = Convert.ToInt64(dr["ID_Customerserviceregister"]),
                        ID_ServiceFollowup = Convert.ToInt64(dr["ID_ServiceFollowup"].ToString()),
                        Ticket = Convert.ToString(dr["Ticket"].ToString()),
                        TicketDate = Convert.ToString(dr["TicketDate"].ToString()),
                        AssignedDate = Convert.ToString(dr["AssignedDate"].ToString()),
                        DueDays = Convert.ToInt32(dr["DueDays"].ToString()),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        CurrentStatus = Convert.ToString(dr["CurrentStatus"].ToString()),
                        PriorityCode = Convert.ToInt32(dr["PriorityCode"].ToString()),
                        Priority = Convert.ToString(dr["Priority"]),
                        Area=Convert.ToString(dr["AreaName"]),
                        FK_ImageLocation =(dr["FK_ImageLocation"]==DBNull.Value)?"0": Convert.ToString(dr["FK_ImageLocation"]),
                        LocLongitude = Convert.ToString(dr["LocLongitude"]),
                        LocLattitude = Convert.ToString(dr["LocLattitude"]),
                        LocLocationName = Convert.ToString(dr["LocLocationName"]),
                        FK_Status = Convert.ToString(dr["FK_Status"]),
                        Status_Longitude = Convert.ToString(dr["FS_LocLongitude"]),
                        Status_Lattitude = Convert.ToString(dr["FS_LocLattitude"]),
                        Status_LocationName = Convert.ToString(dr["FS_LocLocationName"]),
                        Status_Date = Convert.ToString(dr["FS_Date"]),
                        Status_Time = Convert.ToString(dr["FS_Time"]),
                        ID_CustomerserviceregisterProductDetails = Convert.ToInt64(dr["ID_CustomerserviceregisterProductDetails"]),
                    }).ToList();

        }
        public static ComplaintListDetails ConvertComplaintListDetails(DataTable dt)
        {
            ComplaintListDetails log = new ComplaintListDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ComplaintListDetailsList = ConvertComplaintListDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Complaint List Details";
            }
            return log;
        }
        public static List<ComplaintListDetailsList> ConvertComplaintListDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ComplaintListDetailsList()
                    {

                        ID_ComplaintList = Convert.ToInt64(dr["ID_ComplaintList"].ToString()),
                        ComplaintName = Convert.ToString(dr["ComplaintName"].ToString()),


                    }).ToList();

        }
       

        public static CustomerDueDetils ConvertCustomerDueDetils(DataTable dt)
        {
            CustomerDueDetils log = new CustomerDueDetils();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.CustomerDueDetilsList = ConvertCustomerDueDetilsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Customer Due Details";
            }
            return log;
        }
        public static List<CustomerDueDetilsList> ConvertCustomerDueDetilsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new CustomerDueDetilsList()
                    {
                        AccountDetails = Convert.ToString(dr["AccountDetails"]),
                        Balance = Convert.ToString(dr["Balance"].ToString()),
                        Due = Convert.ToDecimal(dr["Due"].ToString())
                    }).ToList();

        }
        public static EmployeeWiseTicketSelect ConvertEmployeeWiseTicketSelect(DataSet ds)
        {
            EmployeeWiseTicketSelect log = new EmployeeWiseTicketSelect();
            if (ds.Tables.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    log.Ticket = ds.Tables[0].Rows[0].Field<string>("TicketNo");
                    log.RegisterdOn = ds.Tables[0].Rows[0].Field<string>("RegisteredOn");
                    log.VisitOn = ds.Tables[0].Rows[0].Field<string>("Visiteddate");
                    log.CustomerName = ds.Tables[0].Rows[0].Field<string>("CusName");
                    log.Mobile = ds.Tables[0].Rows[0].Field<string>("Mobile");
                    log.Address1 = ds.Tables[0].Rows[0].Field<string>("Address1");
                    log.Address2 = ds.Tables[0].Rows[0].Field<string>("Address2");
                    log.Address3 = ds.Tables[0].Rows[0].Field<string>("Address3");
                    log.FK_Customer = ds.Tables[0].Rows[0].Field<Int64>("FK_Customer");
                    log.FK_CustomerOthers = ds.Tables[0].Rows[0].Field<Int64>("FK_CustomerOthers");
                    log.CusNumber = ds.Tables[0].Rows[0].Field<string>("CusNumber");
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    log.FK_Product = ds.Tables[1].Rows[0].Field<Int64>("ID_Product");
                    log.ProductName = ds.Tables[1].Rows[0].Field<string>("Product");
                    log.Category = ds.Tables[1].Rows[0].Field<string>("Category");
                    log.Service = ds.Tables[1].Rows[0].Field<string>("ServiceName");
                    log.Description = ds.Tables[1].Rows[0].Field<string>("ProductDescription");
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Wise Ticket Details";
            }
            return log;
        }

        public static MenuGroupDetails ConvertMenuGroupDetails(DataTable dt)
        {
            MenuGroupDetails log = new MenuGroupDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MenuGroupList = ConvertMenuGroupList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Menu Group Details";
            }
            return log;
        }
        public static List<MenuGroupDetailsList> ConvertMenuGroupList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MenuGroupDetailsList()
                    {
                        MenuGroupID = Convert.ToInt32(dr["MenuGroupID"]),
                        MenuGroupName = Convert.ToString(dr["MenuGroupName"]),
                        MenuGroupVisible = Convert.ToBoolean(dr["MenuGroupVisible "]),                       
                        SortOrder = Convert.ToInt32(dr["SortOrder"])
                    }).ToList();

        }
        public static GenralSettingsDetails ConvertGenralSettingsDetails(DataTable dt)
        {
            GenralSettingsDetails log = new GenralSettingsDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.GsValue = Convert.ToBoolean(dt.Rows[0]["GsValue"]);
                

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Genral Settings Details";
            }
            return log;
        }
        public static UpdateWalkingCustomer ConvertUpdateWalkingCustomer(DataTable dt)
        {
            UpdateWalkingCustomer log = new UpdateWalkingCustomer();
            if (dt.Columns.Contains("ErrCode"))
            {
                log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);               
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.FK_CustomerAssignment = Convert.ToString(dt.Rows[0]["FK_CustomerAssignment"]);

                }
                else
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Updated";
                }
            }
           
            return log;
        }
        public static WalkingCustomerDetailsList ConvertWalkingCustomerDetailsList(DataTable dt)
        {
            WalkingCustomerDetailsList log = new WalkingCustomerDetailsList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.WalkingCustomerDetails = ConvertWalkingCustomerDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Walking Customer Details";
            }
            return log;
        }
        public static List<WalkingCustomerDetails> ConvertWalkingCustomerDetails(DataTable dt)
        {
            List<WalkingCustomerDetails> lst = new List<WalkingCustomerDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                WalkingCustomerDetails obj = new WalkingCustomerDetails();
                obj.SlNo = Convert.ToInt32(dr["SlNo"].ToString());
                obj.ID_CustomerAssignment = Convert.ToInt64(dr["ID_CustomerAssignment"].ToString());
                obj.Customer = Convert.ToString(dr["Customer"]);
                obj.Mobile = Convert.ToString(dr["Mobile"]);
                var tempAssignedDate = Convert.ToDateTime(dr["AssignedDate"]);
                obj.AssignedDate = tempAssignedDate.ToString("dd/MM/yyyy");
                obj.Employee = Convert.ToString(dr["Employee"]);
                obj.FK_Employee = Convert.ToInt64(dr["FK_Employee"]);
                obj.DESCRIPTION = Convert.ToString(dr["DESCRIPTION"]);
                if(dr["VoiceData"]!=DBNull.Value)
                {
                    //byte[] byteData = (byte[])dr["VoiceData"];
                    //string strData = Encoding.UTF8.GetString(byteData);
                    //obj.VoiceData = strData;
                    obj.VoiceData = "";
                    obj.blnVoiceData = 1;
                }
                else
                {
                    obj.VoiceData = "";
                    obj.blnVoiceData = 0;
                }
                obj.VoiceName = Convert.ToString(dr["VoiceName"]);              
                lst.Add(obj);
            }
            return lst;

        }
        public static ServiceDashBoardDetails ConvertServiceDashBoardDetails(DataSet dts)
        {
            ServiceDashBoardDetails log = new ServiceDashBoardDetails();
            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["Field"]) == "Services")
                        log.TotalCount = Convert.ToString(dr["Value"]); 
                }
                log.ServiceStages = ConvertServiceStages(dts.Tables[0]);
                log.Services = ConvertServices(dts.Tables[1]);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Service DashBoard Details";
            }
            return log;
        }
        public static List<ServiceStages> ConvertServiceStages(DataTable dt)
        {
          
            List<ServiceStages> lst = new List<ServiceStages>();
            foreach (DataRow dr in dt.Rows)
            {
                ServiceStages obj = new ServiceStages();
                if (Convert.ToString(dr["Module"]) == "Service")
                {
                    obj.Fileds = Convert.ToString(dr["Field"].ToString());
                    obj.Value = Convert.ToString(dr["Value"].ToString());
                    obj.Count = Convert.ToString(dr["Count"].ToString());
                    lst.Add(obj);
                }
                                   
               
            }
            return lst;
        }
        public static List<Services> ConvertServices(DataTable dt)
        {
            List<Services> lst = new List<Services>();
            foreach (DataRow dr in dt.Rows)
            {
                Services obj = new Services();
                obj.Fileds = Convert.ToString(dr["Field"].ToString());
                obj.Count = Convert.ToString(dr["DashBoard"].ToString());
                obj.Value = Convert.ToString(dr["Value"].ToString());
               
                lst.Add(obj);
            }
            return lst;
        }

        public static AgendaCount ConvertAgendaCountDetails(DataSet dts)
        {
            AgendaCount log = new AgendaCount();
            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LEAD = dt.Rows[0]["LEAD"].ToString();
                log.SERVICE = dt.Rows[0]["SERVICE"].ToString();
                log.EMI = dt.Rows[0]["EMI"].ToString();
                log.LeadData = ConvertLeadManagementDetails(dts.Tables[1]);
                log.ServiceData = ConvertServiceFollowUpdetailsList(dts.Tables[2]);
                log.EMIData = ConvertEMICollectionReportList(dts.Tables[3]);
              
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Agenda Count Details";
            }
            return log;
        }

        public static List<EMICollectionReportList> ConvertEMICollectionReportList(DataTable dt)
        {
            List<EMICollectionReportList> lst = new List<EMICollectionReportList>();
            foreach (DataRow dr in dt.Rows)
            {
                EMICollectionReportList obj = new EMICollectionReportList();

                obj.ID_CustomerWiseEMI = Convert.ToString(dr["ID_CustomerWiseEMI"].ToString());
                obj.EMINo = Convert.ToString(dr["EMINo"].ToString());
                obj.Customer = Convert.ToString(dr["Customer"].ToString());
                obj.Mobile = Convert.ToString(dr["Mobile"].ToString());
                obj.Product = Convert.ToString(dr["Product"].ToString());
                obj.FinancePlan = Convert.ToString(dr["FinancePlan"].ToString());
                obj.LastTransaction = Convert.ToString(dr["LastTransaction"].ToString());
                obj.DueAmount = Convert.ToString(dr["DueAmount"].ToString());
                obj.FineAmount = Convert.ToString(dr["FineAmount"].ToString());
                obj.Balance = Convert.ToString(dr["Balance"].ToString());
                obj.DueDate = Convert.ToString(dr["DueDate"].ToString());
                obj.NextEMIDate = Convert.ToString(dr["NextEMIDate"].ToString());
                obj.InstNo = Convert.ToString(dr["InstNo"].ToString());
                obj.FK_Area = Convert.ToString(dr["FK_Area"].ToString());
                obj.Area = Convert.ToString(dr["Area"].ToString());
                lst.Add(obj);
            }
            return lst;
        }
        public static UpdateLocation ConvertUpdateImageLocation(DataTable dt)
        {
            UpdateLocation log = new UpdateLocation();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FK_Location = Convert.ToString(dt.Rows[0]["FK_ImageLocation"]);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static UpdateFollowupStatus ConvertUpdateFollowupSatus(DataTable dt)
        {
            UpdateFollowupStatus log = new UpdateFollowupStatus();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FK_FollowUpStatus = Convert.ToString(dt.Rows[0]["FK_FollowUpStatus"]);
                log.FK_Status = Convert.ToString(dt.Rows[0]["FK_Status"]);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }

        public static EmployeeDetails ConvertWalkingEmployeeDetails(DataTable dt)
        {
            EmployeeDetails log = new EmployeeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EmployeeDetailsList = ConvertWalkingEmployeeDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Walking Employee Details";
            }
            return log;
        }
        public static List<EmployeeDetailsList> ConvertWalkingEmployeeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new EmployeeDetailsList()
                    {

                        ID_Employee = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        EmpName = Convert.ToString(dr["Employee"].ToString()),
                        DepartmentName = Convert.ToString(dr["Department"].ToString()),
                        DesignationName = Convert.ToString(dr["Designation"].ToString()),
                        FK_Branch = Convert.ToInt64(dr["Branchs"].ToString()),
                        Branch = Convert.ToString(dr["Branch"].ToString())

                    }).ToList();


           

        }
        public static UpdateAttanceMarking ConvertUpdateAttanceMarking(DataTable dt)
        {
            UpdateAttanceMarking log = new UpdateAttanceMarking();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FK_EmployeeAttanceMarking = Convert.ToString(dt.Rows[0]["FK_EmployeeAttanceMarking"]);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static UpdateEmployeeLocation ConvertUpdateEmployeeLocation(DataTable dt)
        {
            UpdateEmployeeLocation log = new UpdateEmployeeLocation();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FK_EmployeeLocation = Convert.ToString(dt.Rows[0]["FK_EmployeeLocation"]);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static UpdateNotification ConvertUpdateNotification(DataTable dt)
        {
            UpdateNotification log = new UpdateNotification();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ID_Notification = Convert.ToString(dt.Rows[0]["ID_Notification"]);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Updated";
            }
            return log;
        }
        public static EmployeeLocationList ConvertEmployeeLocationList(DataTable dt)
        {

            EmployeeLocationList log = new EmployeeLocationList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EmployeeLocationListData = ConvertEmployeeLocationData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Location Details";
            }
            return log;
        }
        public static List<EmployeeLocation> ConvertEmployeeLocationData(DataTable dt)
        {
            List<EmployeeLocation> lst = new List<EmployeeLocation>();
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeLocation obj = new EmployeeLocation();
                obj.FK_Employee = Convert.ToString(dr["FK_Employee"]);
                obj.EmployeeName = Convert.ToString(dr["EmpFName"]);
                obj.LocLongitude = Convert.ToString(dr["LocLongitude"]);
                obj.LocLattitude = Convert.ToString(dr["LocLattitude"]);
                obj.LocLocationName = Convert.ToString(dr["LocLocationName"]);
                obj.EnteredDate = Convert.ToString(dr["EnteredDate"]);
                obj.EnteredTime = Convert.ToString(dr["EnteredTime"]);
                obj.ChargePercentage = Convert.ToString(dr["ChargePercentage"]);
                obj.FK_Department = Convert.ToString(dr["FK_Department"]);
                obj.DeptName = Convert.ToString(dr["DeptName"]);
                obj.FK_Designation = Convert.ToString(dr["FK_Designation"]);
                obj.DesName = Convert.ToString(dr["DesName"]);
               obj.Attance = Convert.ToInt32(dr["Attance"]);
                lst.Add(obj);
            }
            return lst;

        }

        public static EmployeeWiseLocationList ConvertEmployeeWiseLocationList(DataTable dt)
        {

            EmployeeWiseLocationList log = new EmployeeWiseLocationList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EmployeeWiseLocationListData = ConvertEmployeeWiseLocationData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the EmployeeLocation Details ";
            }
            return log;
        }
        public static List<EmployeeWiseLocation> ConvertEmployeeWiseLocationData(DataTable dt)
        {
            List<EmployeeWiseLocation> lst = new List<EmployeeWiseLocation>();
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeWiseLocation obj = new EmployeeWiseLocation();
                obj.FK_Employee = Convert.ToString(dr["FK_Employee"]);
                obj.EmployeeName = Convert.ToString(dr["EmpFName"]);
                obj.LocLongitude = Convert.ToString(dr["LocLongitude"]);
                obj.LocLattitude = Convert.ToString(dr["LocLattitude"]);
                obj.LocLocationName = Convert.ToString(dr["LocLocationName"]);
                obj.EnteredDate = Convert.ToString(dr["EnteredDate"]);
                obj.EnteredTime = Convert.ToString(dr["EnteredTime"]);
                obj.ChargePercentage = Convert.ToString(dr["ChargePercentage"]);
              

                lst.Add(obj);
            }
            return lst;

        }

        public static DesignationList ConvertDesignationList(DataTable dt)
        {

            DesignationList log = new DesignationList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.DesignationDetails = ConvertDesignationDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Designation Details";
            }
            return log;
        }
        public static List<DesignationDetails> ConvertDesignationDetails(DataTable dt)
        {
            List<DesignationDetails> lst = new List<DesignationDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                DesignationDetails obj = new DesignationDetails();
                obj.ID_Designation = Convert.ToString(dr["ID_Designation"]);
                obj.DesignationName = Convert.ToString(dr["DesName"]);
               
                lst.Add(obj);
            }
            return lst;

        }
        public static MailResult SendMail(string result)
        {
            MailResult log = new MailResult();
            log.ResponseCode = "0";
            log.ResponseMessage = "Transaction Verified";
            log.Result = result;
            return log;
        }
        public static ItemSearchList ConvertItemList(DataTable dt)
        {
            ItemSearchList log = new ItemSearchList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ItemSearchListData = ConvertItemListData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "Invalid QRCode";
            }
            return log;
        }
        public static List<ProductListData> ConvertItemListData(DataTable dt)
        {
            List<ProductListData> lst = new List<ProductListData>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductListData obj = new ProductListData();
                obj.ID_Product = Convert.ToString(dr["ID_Product"]);
                obj.ProductName = Convert.ToString(dr["ProductName"]);
                obj.ProdShortName = Convert.ToString(dr["ProdShortName"]);
                obj.MRP = Convert.ToString(dr["MRP"]);
                obj.Price = Convert.ToString(dr["Price"]);
                obj.FK_Category = Convert.ToString(dr["FK_Category"]);
                obj.CategoryName = Convert.ToString(dr["CategoryName"]);
                obj.Project = (Convert.ToBoolean(dr["Project"])?"1":"0");

                lst.Add(obj);
            }
            return lst;

        }

        public static ProductEnquiryList ConvertProductEnquiryList(DataTable dt)
        {

            ProductEnquiryList log = new ProductEnquiryList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductEnquiryListData = ConvertProductEnquiryData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Enquiry Details";
            }
            return log;
        }
        public static List<ProductEnquiry> ConvertProductEnquiryData(DataTable dt)
        {
            List<ProductEnquiry> lst = new List<ProductEnquiry>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProductEnquiry obj = new ProductEnquiry();
                    obj.ID_Product = Convert.ToString(dr["ID_Product"]);
                    obj.ID_Category = Convert.ToString(dr["ID_Category"]);
                    obj.Code = Convert.ToString(dr["Code"]);
                    obj.Name = Convert.ToString(dr["Name"]);
                    obj.MRP = Convert.ToString(dr["MRP"]);
                    obj.SalPrice = Convert.ToString(dr["SalPrice"]);
                    obj.CurrentQuantity = Convert.ToString(dr["CurrentQuantity"]);
                    obj.ProductDescription = Convert.ToString(dr["ProductDescription"]);
                    obj.Offer = Convert.ToString(dr["OFFER"]);
                    obj.OfrName = Convert.ToString(dr["OfrName"]);
                    obj.OfrDescription = Convert.ToString(dr["OfrDescription"]);
                    obj.OfrExpireDate = Convert.ToString(dr["OfrExpireDate"]);                  
                    string fileName = Convert.ToString(dr["FileName"]);
                    string filePath = "~/ProductDetails/" + fileName;
                    if(dr["ImageData"]!=DBNull.Value)
                    {
                        byte[] bytes = (byte[])dr["ImageData"];
                        if (bytes.Length > 0)
                        {
                            using (var stream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                                stream.Flush();
                                obj.Image = "/ProductDetails/" + fileName;
                            }
                        }
                    }
                    else
                    {
                        obj.Image = "";
                    }

                    lst.Add(obj);
                }
            }
            catch(Exception ex)
            {

            }
           
            return lst;

        }
        public static ProductEnquiryDetails ConvertProductEnquiryDetailList(DataSet ds)
        {

            ProductEnquiryDetails log = new ProductEnquiryDetails();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ID_Category = dt.Rows[0]["FK_Category"].ToString();
                log.ID_Product = dt.Rows[0]["ID_Product"].ToString();
                log.Code = dt.Rows[0]["ProdCode"].ToString();
                log.Name = dt.Rows[0]["ProdName"].ToString();
                log.ProductDescription = dt.Rows[0]["ProdMaterialDetails"].ToString(); 
                log.ImageList = ConvertProductImageData(ds.Tables[1]);
                log.RelatedItem = ConvertProductEnquiryData(ds.Tables[2]);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Enquiry Details";
            }
            return log;
        }
        public static List<ProductEnquiryImageData> ConvertProductImageData(DataTable dt)
        {
            List<ProductEnquiryImageData> lst = new List<ProductEnquiryImageData>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProductEnquiryImageData obj = new ProductEnquiryImageData();                   
                    string fileName = Convert.ToString(dr["FileName"]);
                    string filePath = "~/ProductDetails/" + fileName;
                    if (dr["ImageData"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])dr["ImageData"];
                        if (bytes.Length > 0)
                        {
                            using (var stream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                                stream.Flush();
                                obj.Image = "/ProductDetails/" + fileName;
                            }
                        }
                    }
                    else
                    {
                        obj.Image = "";
                    }
                    lst.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }

            return lst;

        }
        public static StockRTEmployeeDetails ConvertStockRTEmployee(DataTable dt)
        {
            StockRTEmployeeDetails log = new StockRTEmployeeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRTEmployeeList = ConvertStockRTEmployeeList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Details";
            }
            return log;
        }
        public static List<StockRTEmployee> ConvertStockRTEmployeeList(DataTable dt)
        {
            List<StockRTEmployee> lst = new List<StockRTEmployee>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRTEmployee obj = new StockRTEmployee();
                obj.FK_Employee = Convert.ToInt32(dr["ID_FIELD"]);
                obj.Code = Convert.ToString(dr["Code"]);
                obj.Name = Convert.ToString(dr["Name"]);
                obj.Designation = Convert.ToString(dr["Designation"]);               
                lst.Add(obj);
            }
            return lst;
        }
        public static StockRTProductDetails ConvertStockRTProduct(DataTable dt)
        {
            StockRTProductDetails log = new StockRTProductDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRTProductList = ConvertStockRTProductList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Details";
            }
            return log;
        }
        public static List<StockRTProduct> ConvertStockRTProductList(DataTable dt)
        {
            List<StockRTProduct> lst = new List<StockRTProduct>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRTProduct obj = new StockRTProduct();
                obj.FK_Product = Convert.ToInt32(dr["ID_FIELD"]);
                obj.Code = Convert.ToString(dr["Code"]);
                obj.Name = Convert.ToString(dr["Name"]);              
                lst.Add(obj);
            }
            return lst;

        }
        public static AuthorizationModuleData ConvertAuthorizationModuleList(DataTable dt)
        {

            AuthorizationModuleData log = new AuthorizationModuleData();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AuthorizationModuleDetails = ConvertAuthorizationModuleData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Module List";
            }
            return log;
        }
        public static List<AuthorizationModuleDetails> ConvertAuthorizationModuleData(DataTable dt)
        {
            List<AuthorizationModuleDetails> lst = new List<AuthorizationModuleDetails>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if(Convert.ToInt32(dr["NoofRecords"])>0)
                    {
                        AuthorizationModuleDetails obj = new AuthorizationModuleDetails();
                        obj.Module = Convert.ToString(dr["Module"]);
                        obj.Module_Name = Convert.ToString(dr["Module_Name"]);
                        obj.NoofRecords = Convert.ToString(dr["NoofRecords"]);
                        obj.MobColor = Convert.ToString(dr["MobColor"]);
                        obj.MobImage = Convert.ToString(dr["MobImage"]);
                        lst.Add(obj);
                    }                    
                }
            }
            catch (Exception ex)
            {

            }

            return lst;
        }
        
        public static DynamicData ConvertAuthorizationList(DataTable dt)
        {
            DynamicData log = new DynamicData();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ListData = (IEnumerable<dynamic>)DataTableToList<dynamic>(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Details";
            }
            return log;
        }
     

        public static IEnumerable<dynamic> DataTableToList<T>(DataTable table) where T : class, new()
        {
            try
            {   
                var lst = table.AsEnumerable()
                      .Select(r => r.Table.Columns.Cast<DataColumn>()
                      .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
                return lst;
            }
            catch
            {
                return Enumerable.Empty<T>();
            }
        }
        public static AuthorizationActionDetails ConvertAuthorizationAction(DataTable dt,DataTable dts,string AuthID)
        {
            AuthorizationActionDetails log = new AuthorizationActionDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.TransactionDetails = Convert.ToString(dt.Rows[0]["TransactionDetails"]);
                log.PartyDetails = Convert.ToString(dt.Rows[0]["PartyDetails"]);
                log.SubTitle = Convert.ToString(dt.Rows[0]["SubTitle"]);
                log.SubDetails = Convert.ToInt32(dt.Rows[0]["SubDetails"]);
                log.FooterLeft = Convert.ToString(dt.Rows[0]["FooterLeft"]);
                log.FooterRight = Convert.ToString(dt.Rows[0]["FooterRight"]);
                log.FK_Master = Convert.ToString(dt.Rows[0]["FK_Master"]);
                log.TransMode = Convert.ToString(dt.Rows[0]["TransMode"]);
                log.Mode = Convert.ToString(dt.Rows[0]["Mode"]);
                log.SubTitleHTML = Convert.ToString("<strong>" + dt.Rows[0]["SubTitle"].ToString() + "</strong>");
               if(log.SubDetails==1 && dts.Rows.Count>0)
                {
                 
                    log.SubDetailsData = (IEnumerable<dynamic>)DataTableToList<dynamic>(dts);
                }
                log.AuthID = AuthID;
                log.ActiveCorrectionOption= Convert.ToString(dt.Rows[0]["ActiveCorrectionOption"]);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Action Details";
            }
            return log;
        }
        public static UpdateAuthorization ConvertUpdateAuthorization(DataTable dt)
        {
            UpdateAuthorization log = new UpdateAuthorization();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Your action is successfully approved";
                log.ID_AuthorizationLevel = dt.Rows[0][0].ToString();

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Approved";
            }
            return log;
        }
        public static UpdateAuthorization ConvertRejectAuthorization(DataTable dt)
        {
            UpdateAuthorization log = new UpdateAuthorization();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Your action has been rejected.";
                log.ID_AuthorizationLevel = dt.Rows[0][0].ToString();

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Rejected";
            }
            return log;
        }
        public static DynamicData ConvertERPCmnSearchPopup(DataTable dt)
        {
            DynamicData log = new DynamicData();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ListData = (IEnumerable<dynamic>)DataTableToList<dynamic>(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records...";
            }
            return log;
        }
        public static CorrectionAuthorization ConvertCorrectionAuthorization(DataTable dt)
        {
            CorrectionAuthorization log = new CorrectionAuthorization();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Your action has been passed to correction.";
                log.ID_AuthorizationLevel = dt.Rows[0][0].ToString();

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data passed to correction";
            }
            return log;
        }
        public static ProductLocationList ConvertProductLocationList(DataTable dt)
        {

            ProductLocationList log = new ProductLocationList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductLocationListData = ConvertProductLocationData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Location Details";
            }
            return log;
        }
        public static List<ProductLocation> ConvertProductLocationData(DataTable dt)
        {
            List<ProductLocation> lst = new List<ProductLocation>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProductLocation obj = new ProductLocation();
                    obj.ID_ProductLocation = Convert.ToString(dr["ID_ProductLocation"]);
                    obj.LocationName = Convert.ToString(dr["LocationName"]);
                   
                    lst.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }

            return lst;

        }

        public static UserTaskList ConvertUserTaskList(DataTable dt)
        {

            UserTaskList log = new UserTaskList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.UserTaskListData = ConvertUserTaskListData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the User Task Details";
            }
            return log;
        }
        public static List<TaskList> ConvertUserTaskListData(DataTable dt)
        {
            List<TaskList> lst = new List<TaskList>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TaskList obj = new TaskList();
                    obj.Value = Convert.ToInt32(dr["Value"]);
                    obj.Task = Convert.ToString(dr["Task"]);
                    obj.ModuleCount = Convert.ToInt32(dr["ModuleCount"]);
                    obj.ID_AuthorizationData = Convert.ToString(dr["ID_AuthorizationData"]);
                    obj.TransMode = Convert.ToString(dr["TransMode"]);
                    obj.Mode = Convert.ToString(dr["Mode"]);
                    obj.FK_TransMaster = Convert.ToString(dr["FK_TransMaster"]);
                    obj.MobImage = Convert.ToString(dr["MobImage"]);
                    obj.MobColor = Convert.ToString(dr["MobColor"]);
                    obj.Criteria = Convert.ToString(dr["Criteria"]);
                    lst.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }

            return lst;

        }
        public static WalkingCustomerList ConvertWalkingCustomerList(DataTable dt)
        {

            WalkingCustomerList log = new WalkingCustomerList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.WalkingCustomerDetails = ConvertWalkingCustomerData(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "Not registered with this number";
            }
            return log;
        }
        public static List<WalkingCustomerListDetails> ConvertWalkingCustomerData(DataTable dt)
        {
            List<WalkingCustomerListDetails> lst = new List<WalkingCustomerListDetails>();
            try
            {
               if(dt.Rows.Count>0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        WalkingCustomerListDetails obj = new WalkingCustomerListDetails();
                        obj.Customer = Convert.ToString(dr["Customer"]);
                        obj.Mobile = Convert.ToString(dr["Mobile"]);
                        obj.Product = Convert.ToString(dr["Product"]);
                        obj.Action = Convert.ToString(dr["Action"]);
                        obj.AssignedTo = Convert.ToString(dr["AssignedTo"]);
                        obj.ID_LeadGenerateProduct = Convert.ToString(dr["ID_LeadGenerateProduct"]);                      
                        var tempFollowUpDate = Convert.ToDateTime(dr["FollowUpDate"]);
                        obj.FollowUpDate = tempFollowUpDate.ToString("dd-MM-yyyy");                       
                        obj.FK_Employee = Convert.ToString(dr["FK_Employee"]);
                        obj.ID_Users = Convert.ToString(dr["ID_Users"]);
                        obj.LeadNo = Convert.ToString(dr["LeadNo"]);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return lst;
        }

        public static AuthorizationCorrectionModuleData ConvertAuthorizationCorrectionModuleList(DataSet ds)
        {

            AuthorizationCorrectionModuleData log = new AuthorizationCorrectionModuleData();           
            if (ds.Tables[0].Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
               log.AuthorizationModuleCorrectionDetails = ConvertAuthorizationCorrectionModuleData(ds);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Correction Module Details";
            }
            return log;
        }
        public static List<AuthorizationCorrectionModuleDetails> ConvertAuthorizationCorrectionModuleData(DataSet ds)
        {
            List<AuthorizationCorrectionModuleDetails> lst = new List<AuthorizationCorrectionModuleDetails>();
            try
            {
                DataTable dt = ds.Tables[0],dtOther= ds.Tables[1];

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(dr["NoofRecords"]) > 0)
                    {
                        AuthorizationCorrectionModuleDetails obj = new AuthorizationCorrectionModuleDetails();
                        obj.Module = Convert.ToString(dr["Module"]);
                        obj.Module_Name = Convert.ToString(dr["Module_Name"]);
                        obj.NoofRecords = Convert.ToString(dr["NoofRecords"]);
                        obj.MobColor = Convert.ToString(dr["MobColor"]);
                        obj.MobImage = Convert.ToString(dr["MobImage"]);
                        if(Convert.ToInt32(dr["NoofRecords"])==1)
                        {
                            DataRow drOther = dtOther.Select("Module='" + obj.Module+"'").FirstOrDefault();
                            obj.ID_AuthorizationData = Convert.ToString(drOther["ID_AuthorizationData"]);
                            obj.TransMode = Convert.ToString(drOther["TransMode"]);
                            obj.Mode = Convert.ToString(drOther["Mode"]);
                            obj.FK_TransMaster = Convert.ToString(drOther["FK_TransMaster"]);
                        }
                        else
                        {
                            obj.ID_AuthorizationData ="";
                            obj.TransMode ="";
                            obj.Mode = "";
                            obj.FK_TransMaster = "";
                        }


                        lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return lst;
        }

        public static AuthorizationCorrectionDetailsData ConvertAuthorizationCorrectionDetailsData(DataTable dt)
        {

            AuthorizationCorrectionDetailsData log = new AuthorizationCorrectionDetailsData();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AuthorizationCorrectionDetails = ConvertAuthorizationCorrectionDetails(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Correction Details";
            }
            return log;
        }
        public static List<AuthorizationCorrectionDetails> ConvertAuthorizationCorrectionDetails(DataTable dt)
        {
            List<AuthorizationCorrectionDetails> lst = new List<AuthorizationCorrectionDetails>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        AuthorizationCorrectionDetails obj = new AuthorizationCorrectionDetails();
                        obj.ID_AuthorizationData = Convert.ToString(dr["ID_AuthorizationData"]);
                        obj.TransMode = Convert.ToString(dr["TransMode"]);
                        obj.Mode = Convert.ToString(dr["Mode"]);
                        obj.TransTitle = Convert.ToString(dr["TransTitle"]);
                        obj.TransNo = Convert.ToString(dr["TransNo"]);
                        obj.FK_TransMaster = Convert.ToString(dr["FK_TransMaster"]);
                        obj.CorrectionPassBy = Convert.ToString(dr["CorrectionPassBy"]);
                        obj.CorrectionPassOn = Convert.ToString(dr["CorrectionPassOn"]);
                        obj.ContactNum = Convert.ToString(dr["ContactNum"]);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return lst;
        }
        public static CorrectionLeadGenerate ConvertAuthorizationCorrectionLeadData(DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            CorrectionLeadGenerate log = new CorrectionLeadGenerate();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";                          
                log.LeadGenerateID = dt.Rows[0]["LeadGenerateID"].ToString();
                log.LeadNo = dt.Rows[0]["LeadNo"].ToString();
                log.LeadID = dt.Rows[0]["LeadID"].ToString();
                if(dt.Rows[0]["LeadDate"]!=DBNull.Value)
                {
                    var tempLeadDate = Convert.ToDateTime(dt.Rows[0]["LeadDate"]);
                    log.LeadDate = tempLeadDate.ToString("dd/MM/yyyy");
                }                           
                log.LeadFrom = dt.Rows[0]["LeadFrom"].ToString();
                log.LeadBy = dt.Rows[0]["LeadBy"].ToString();
                log.ID_MediaMaster = dt.Rows[0]["ID_MediaMaster"].ToString();
                log.SubMedia = dt.Rows[0]["SubMedia"].ToString();
                log.PinCode = dt.Rows[0]["PinCode"].ToString();
                log.CountryID = dt.Rows[0]["CountryID"].ToString();
                log.StatesID = dt.Rows[0]["StatesID"].ToString();
                log.DistrictID = dt.Rows[0]["DistrictID"].ToString();
                log.PostID = dt.Rows[0]["PostID"].ToString();
                log.Company = dt.Rows[0]["Company"].ToString();
                log.CntryName = dt.Rows[0]["CntryName"].ToString();
                log.StName = dt.Rows[0]["StName"].ToString();
                log.DtName = dt.Rows[0]["DtName"].ToString();
                log.PostName = dt.Rows[0]["PostName"].ToString();
                log.CusPhnNo = dt.Rows[0]["CusPhnNo"].ToString();
                log.LeadByName = dt.Rows[0]["LeadByName"].ToString();
                log.ID_Customer = dt.Rows[0]["ID_Customer"].ToString();
                log.FK_CustomerOthers = dt.Rows[0]["FK_CustomerOthers"].ToString();
                log.CollectedBy = dt.Rows[0]["CollectedBy"].ToString();
                log.CollectedByName = dt.Rows[0]["CollectedByName"].ToString();
                log.FK_Company = dt.Rows[0]["FK_Company"].ToString();
                log.FK_BranchCodeUser = dt.Rows[0]["FK_BranchCodeUser"].ToString();
                log.CusName = dt.Rows[0]["CusName"].ToString();
                log.CusMobile = dt.Rows[0]["CusMobile"].ToString();
                log.CusEmail = dt.Rows[0]["CusEmail"].ToString();
                log.CusAddress = dt.Rows[0]["CusAddress"].ToString();
                log.CusAddress2 = dt.Rows[0]["CusAddress2"].ToString();
                log.CusNameTitle = dt.Rows[0]["CusNameTitle"].ToString();
                log.CusMobileAlternate = dt.Rows[0]["CusMobileAlternate"].ToString();
                log.Area = dt.Rows[0]["Area"].ToString();
                log.AreaID = dt.Rows[0]["AreaID"].ToString();
                log.FK_Quotation = dt.Rows[0]["FK_Quotation"].ToString();
                if(ds.Tables[1].Rows.Count>0)
                {
                    log.ProductDetails = ConvertAuthorizationCorrectionLeadProductData(ds.Tables[1]);
                }
                if(ds.Tables[2].Rows.Count>0)
                {
                    log.SenderDetails = ConvertCorrectionSenderDtls(ds.Tables[2]);
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Correction Lead Details";
            }
            return log;
        }
        public static List<CorrectionLeadGenerateProduct> ConvertAuthorizationCorrectionLeadProductData(DataTable dt)
        {
            List<CorrectionLeadGenerateProduct> lst = new List<CorrectionLeadGenerateProduct>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CorrectionLeadGenerateProduct obj = new CorrectionLeadGenerateProduct();

                        obj.ID_LeadGenerateProduct = Convert.ToString(dr["ID_LeadGenerateProduct"]);
                        obj.FK_LeadGenerate = Convert.ToString(dr["FK_LeadGenerate"]);
                        obj.ID_Product = Convert.ToString(dr["ID_Product"]);
                        obj.ProdName = Convert.ToString(dr["ProdName"]);
                        obj.ProjectName = Convert.ToString(dr["ProjectName"]);
                        obj.LgpPQuantity = Convert.ToString(dr["LgpPQuantity"]);
                        obj.LgpDescription = Convert.ToString(dr["LgpDescription"]);
                        obj.FK_NetAction = Convert.ToString(dr["FK_NetAction"]);                      
                        if (dr["NextActionDate"] != DBNull.Value)
                        {
                            var tempNextActionDate = Convert.ToDateTime(dr["NextActionDate"]);
                            obj.NextActionDate = tempNextActionDate.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            obj.NextActionDate = "";
                        }
                        obj.FK_Departement = Convert.ToString(dr["FK_Departement"]);
                        obj.FK_Employee = Convert.ToString(dr["FK_Employee"]);
                        obj.LgActStatus = Convert.ToString(dr["LgActStatus"]);
                        obj.LgActDate = Convert.ToString(dr["LgActDate"]);
                        obj.LgActCusComment = Convert.ToString(dr["LgActCusComment"]);
                        obj.LgActLeadComment = Convert.ToString(dr["LgActLeadComment"]);
                        obj.FK_Priority = Convert.ToString(dr["FK_Priority"]);
                        obj.BranchID = Convert.ToString(dr["BranchID"]);
                        obj.AssignEmp = Convert.ToString(dr["AssignEmp"]);
                        obj.BranchTypeID = Convert.ToString(dr["BranchTypeID"]);
                        obj.FK_ActionType = Convert.ToString(dr["FK_ActionType"]);
                        obj.ActStatus = Convert.ToString(dr["ActStatus"]);
                        obj.FK_Category = Convert.ToString(dr["FK_Category"]);
                       
                        if (dr["LgpExpectDate"] != DBNull.Value)
                        {
                            var tempLgpExpectDate = Convert.ToDateTime(dr["LgpExpectDate"]);
                            obj.LgpExpectDate = tempLgpExpectDate.ToString("dd/MM/yyyy");
                        }
                        obj.LgpMRP = Convert.ToString(dr["LgpMRP"]);
                        obj.LgpSalesPrice = Convert.ToString(dr["LgpSalesPrice"]);
                        obj.FK_ProductLocation = Convert.ToString(dr["FK_ProductLocation"]);
                        obj.CategoryName = Convert.ToString(dr["CatName"]);
                        obj.Project = Convert.ToString(dr["Project"]);
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return lst;
        }
        public static List<CorrectionSenderDtls> ConvertCorrectionSenderDtls(DataTable dt)
        {
            List<CorrectionSenderDtls> lst = new List<CorrectionSenderDtls>();
            try
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CorrectionSenderDtls obj = new CorrectionSenderDtls();

                        obj.Sender = Convert.ToString(dr["Sender"]);
                        obj.Reason = Convert.ToString(dr["Reason"]);
                        obj.Remark = Convert.ToString(dr["Remark"]);
                       
                        lst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return lst;
        }
        public static WalkingCustomerVoiceDetails ConvertWalkingCustomerVoiceDetails(DataTable dt)
        {
            WalkingCustomerVoiceDetails log = new WalkingCustomerVoiceDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";                
                if (dt.Rows[0]["VoiceData"] != DBNull.Value)
                {
                    byte[] byteData = (byte[])dt.Rows[0]["VoiceData"];
                    string strData = Encoding.UTF8.GetString(byteData);
                    log.VoiceData = strData;
                    log.VoiceName = Convert.ToString(dt.Rows[0]["VoiceName"]);
                }
                else
                {
                    log.VoiceData = "";
                    log.VoiceName = "";
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Walking Customer Voice Details";
            }
            return log;
        }
        public static AuthorizationDetails ConvertAuthorizationDetails(DataTable dt)
        {
            AuthorizationDetails log = new AuthorizationDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AuthorizationDetailsList = ConvertAuthorizationDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Details";
            }
            return log;
        }
        public static List<AuthorizationDetailsList> ConvertAuthorizationDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AuthorizationDetailsList()
                    {
                        label = Convert.ToString(dr["label"].ToString()),
                        color = Convert.ToString(dr["color"].ToString()),
                        count = Convert.ToString(dr["count"].ToString()),
                        icon = Convert.ToString(dr["icon"].ToString()),
                        mode = Convert.ToString(dr["mode"].ToString())
                    }).ToList();
        }
        public static ModuleDetails ConvertModuleDetails(DataTable dt)
        {
            ModuleDetails log = new ModuleDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ModuleDetailsList = ConvertModuleDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Module Details";
            }
            return log;
        }
        public static List<ModuleDetailsList> ConvertModuleDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ModuleDetailsList()
                    {
                        ID_MenuGroup = Convert.ToInt64(dr["ID_MenuGroup"].ToString()),
                        MnuGrpName = Convert.ToString(dr["MnuGrpName"].ToString()),
                       
                    }).ToList();
        }
        public static ModuleWiseSearchDetails ConvertModuleWiseSearchDetails(DataTable dt)
        {
            ModuleWiseSearchDetails log = new ModuleWiseSearchDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ModuleWiseSearchDetailsList = ConvertModuleWiseSearchDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Module Details";
            }
            return log;
        }
        public static List<ModuleWiseSearchDetailsList> ConvertModuleWiseSearchDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ModuleWiseSearchDetailsList()
                    {
                        Mode = Convert.ToInt64(dr["Mode"].ToString()),
                        ModeName = Convert.ToString(dr["ModeName"].ToString()),
                        SubMode = Convert.ToInt64(dr["ModeName"].ToString()),
                        SubModeName = Convert.ToString(dr["SubModeName"].ToString()),


                    }).ToList();
        }

        public static AuthorizationModuleCountDetails ConvertAuthorizationModuleCountDetails(DataTable dt)
        {
            AuthorizationModuleCountDetails log = new AuthorizationModuleCountDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AuthorizationModuleCountDetailsList = ConvertAuthorizationModuleCountDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Module Details";
            }
            return log;
        }
        public static List<AuthorizationModuleCountDetailsList> ConvertAuthorizationModuleCountDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AuthorizationModuleCountDetailsList()
                    {
                        label = Convert.ToString(dr["label"].ToString()),
                        color = Convert.ToString(dr["color"].ToString()),
                        count = Convert.ToString(dr["count"].ToString()),
                        icon = Convert.ToString(dr["icon"].ToString()),
                        mode = Convert.ToString(dr["mode"].ToString())
                    }).ToList();
        }
        public static AuthorizationPendingDetails ConvertAuthorizationPendingDetails(DataTable dt)
        {
            AuthorizationPendingDetails log = new AuthorizationPendingDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AuthorizationPendingDetailsList = ConvertAuthorizationPendingDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Authorization Pending Details";
            }
            return log;
        }
        public static List<AuthorizationPendingDetailsList> ConvertAuthorizationPendingDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AuthorizationPendingDetailsList()
                    {
                        label = Convert.ToString(dr["label"].ToString()),
                        color = Convert.ToString(dr["color"].ToString()),
                        count = Convert.ToString(dr["count"].ToString()),
                        icon = Convert.ToString(dr["icon"].ToString()),
                        mode = Convert.ToString(dr["mode"].ToString()),
                        SlNo= Convert.ToString(dr["SlNo"].ToString()),
                        ID_FIELD= Convert.ToString(dr["ID_FIELD"].ToString()),
                        Action = Convert.ToString(dr["Action"].ToString()),
                        TicketNo = Convert.ToString(dr["ID_FIELD"].ToString()),
                        TicketDate= Convert.ToString(dr["TicketDate"].ToString()),
                        drank = Convert.ToString(dr["drank"].ToString()),
                        EnteredBy = Convert.ToString(dr["EnteredBy"].ToString()),
                        EnteredOn= Convert.ToString(dr["EnteredOn"].ToString()),
                        TotalCount = Convert.ToString(dr["TotalCount"].ToString()),
                    }).ToList();
        }
        public static OtherChargeDetails ConvertOtherChargeDetails(DataTable dt)
        {
            OtherChargeDetails log = new OtherChargeDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.OtherChargeDetailsList = ConvertOtherChargeDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Other Charge Details";
            }
            return log;
        }
        public static List<OtherChargeDetailsList> ConvertOtherChargeDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new OtherChargeDetailsList()
                    {
                        ID_OtherChargeType = Convert.ToString(dr["ID_OtherChargeType"].ToString()),
                        OctyName = Convert.ToString(dr["OctyName"].ToString()),
                        OctyTransType = Convert.ToString(dr["OctyTransType"].ToString()),
                       
                    }).ToList();
        }
        public static DashBoardModule ConvertDashBoardModule(DataTable dt)
        {
            DashBoardModule log = new DashBoardModule();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.DashBoardModuleList = ConvertDashBoardModuleList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the DashBoard Module Details";
            }
            return log;
        }
        public static List<DashBoardModuleList> ConvertDashBoardModuleList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new DashBoardModuleList()
                    {
                        ModuleMode = Convert.ToInt32(dr["ModuleMode"].ToString()),
                        ModuleName = Convert.ToString(dr["ModuleName"].ToString())
                      

                    }).ToList();
        }


        public static AuthorizationDataList ConvertAuthorizationDataList(DataSet dts)
        {
            AuthorizationDataList log = new AuthorizationDataList();

            DataTable dt = dts.Tables[0];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                log.DataItems = ConvertDataItems(dts);
            }
            return log;
        }
        public static List<DataItems> ConvertDataItems(DataSet dts)
        {
            List<DataItems> lst = new List<DataItems>();
            DataTable dt = dts.Tables[0];
            DataTable dt1 = dts.Tables[1];
            Int32 ID_Module = 0;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataItems obj = new DataItems();
                    obj.Module = Convert.ToString(dr["Module"].ToString());
                    ID_Module = Convert.ToInt32(dr["ID_Module"].ToString());
                    obj.Details = ConvertDetails(dt1, ID_Module);
                    lst.Add(obj);
                }

            }
            return lst;
        }
        public static List<Details> ConvertDetails(DataTable dt, Int32 Mode)
        {
            List<Details> lst = new List<Details>();
            foreach (DataRow dr in dt.Rows)
            {
                Details obj = new Details();
                if (Convert.ToInt32(dr["Mode"]) == Mode)
                {
                    obj.FK_Transaction = Convert.ToInt64(dr["FK_Transaction"]);
                    obj.TransNumber = Convert.ToString(dr["TransNumber"]);
                    obj.TransDate = Convert.ToString(dr["TransDate"]);
                    obj.CusName = Convert.ToString(dr["CusName"]);
                    obj.CusMobile = Convert.ToString(dr["CusMobile"]);
                    obj.CusAddress = Convert.ToString(dr["CusAddress"]);
                    obj.Priority = Convert.ToString(dr["Priority"]);
                    obj.EnteredBy = Convert.ToString(dr["EnteredBy"]);
                    obj.Reason = Convert.ToString(dr["Reason"]);
                    obj.Amount = Convert.ToString(dr["Amount"]);
                    lst.Add(obj);
                }
            }
            return lst;

        }
        public static UserTaskListDetails ConvertUserTaskListDetails(DataTable  dt)
        {
            UserTaskListDetails log = new UserTaskListDetails();           
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
                log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
                log.DataItemsList = ConvertDataItemsList(dt);
            }
            return log;
        }
        public static List<DataItemsList> ConvertDataItemsList(DataTable  dt)
        {
            Int32 Submode =0;
            List<DataItemsList> lst = new List<DataItemsList>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataItemsList obj = new DataItemsList();
                    obj.ModuleName = Convert.ToString(dr["ModuleName"].ToString());
                    //Submode=
                    obj.ListData= ConvertListData(dt);
                    lst.Add(obj);
                }

            }
            return lst;
        }
        public static List<ListData> ConvertListData(DataTable dt)
        {
            List<ListData> lst = new List<ListData>();
            foreach (DataRow dr in dt.Rows)
            {
               ListData obj = new ListData();              
                obj.SlNo = Convert.ToInt32(dr["SlNo"]);
                obj.LeadNo = Convert.ToString(dr["LeadNo"]);
                obj.CustomerName = Convert.ToString(dr["CustomerName"]);
                obj.Address = Convert.ToString(dr["Address"]);
                obj.Product = Convert.ToString(dr["Product"]);
                obj.AuthorizedBy = Convert.ToString(dr["AuthorizedBy"]);
                obj.Date = Convert.ToString(dr["Date"]);
                obj.Time = Convert.ToString(dr["Time"]);
                obj.Rejection = Convert.ToString(dr["Rejection"]);                    
                lst.Add(obj);
                
            }
            return lst;

        }

        public static SendIntimationModule ConvertSendIntimationModule(DataTable dt)
        {
            SendIntimationModule log = new SendIntimationModule();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.IntimationModuleList = ConvertIntimationModuleList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Intimation Module Details";
            }
            return log;
        }

        public static List<IntimationModuleList> ConvertIntimationModuleList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new IntimationModuleList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                        ID_Mode = Convert.ToString(dr["ID_Mode"].ToString()),
                        ModeName = Convert.ToString(dr["ModeName"].ToString()),

                    }).ToList();
        }
        public static ScheduleType ConvertScheduleType(DataTable dt)
        {
            ScheduleType log = new ScheduleType();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ScheduleTypeList = ConvertScheduleTypeList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Schedule Type Details";
            }
            return log;
        }

        public static List<ScheduleTypeList> ConvertScheduleTypeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ScheduleTypeList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                        ID_SheduledType = Convert.ToInt32(dr["ID_SheduledType"].ToString()),
                        SheduledTypeName = Convert.ToString(dr["SheduledTypeName"].ToString()),

                    }).ToList();
        }
        public static Channel ConvertChannel(DataTable dt)
        {
            Channel log = new Channel();
            if (dt.Rows.Count > 0)
            {


                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ChannelList = ConvertChannelList(dt);
                //log.ProjectStatusList = ConvertProjectStatusList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Channel Details";
            }
            return log;
        }

        public static List<ChannelList> ConvertChannelList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ChannelList()
                    {
                        //ID= Convert.ToInt32(dr["ID_Table"].ToString()),
                        ID_Channel = Convert.ToInt32(dr["ID_Channel"].ToString()),
                       ChannelName = Convert.ToString(dr["ChannelName"].ToString()),

                    }).ToList();
        }
        public static UpdateSendIntimation ConvertUpdateSendIntimation(DataTable dt)
        {
            UpdateSendIntimation log = new UpdateSendIntimation();
            if (dt.Columns.Contains("ErrCode"))
            {
                log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    //   log.FK_CustomerAssignment = Convert.ToString(dt.Rows[0]["FK_CustomerAssignment"]);
                  
                }
                else
                {
                    log.ResponseCode = "-2";
                    log.ResponseMessage = "No Data Updated";
                    
                }
            }

            return log;
        }
     
        public static AppConfigurationSettings ConvertAppConfigurationSettings(DataTable dt)
        {
            AppConfigurationSettings log = new AppConfigurationSettings();


            if (dt.Rows.Count > 0)
            {

                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.CERT_NAME = dt.Rows[0].Field<string>("ConfigBankName");
                    log.BASE_URL = dt.Rows[0].Field<string>("ConfigCommonAPIURL");
                    log.IMAGE_URL = dt.Rows[0].Field<string>("ConfigCommonImageURL");
                    log.BANK_KEY = dt.Rows[0].Field<string>("ConfigBankKey");               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Configuration Settings";
            }
            return log;
        }
        public static EmployeeLocationDistance ConvertEmployeeLocationDistance(DataTable  dt, string BankKey, string FKCompany, string LocationEnteredDate, string URLKey)
        {
            EmployeeLocationDistance log = new EmployeeLocationDistance();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.EmployeeLocationDistanceList = ConvertEmployeeLocationDistanceList( dt,  BankKey,  FKCompany,  LocationEnteredDate,  URLKey);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Employee Location Details";
            }
            return log;
        }
        public static List<EmployeeLocationDistanceList> ConvertEmployeeLocationDistanceList(DataTable dt, string BankKey, string FKCompany, string LocationEnteredDate, string URLKey)
        {
            List<EmployeeLocationDistanceList> lst = new List<EmployeeLocationDistanceList>();
            BlUserValidations objbl = new BlUserValidations();
            DalUserValidations objDal = new DalUserValidations();
            string Origin = "", Destination = "", LocationDets = "", FKEmployee = "";
            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeLocationDistanceList obj = new EmployeeLocationDistanceList();
                objbl.FK_Employee = Convert.ToString(dt.Rows[i]["FK_Employee"]);
                FKEmployee = Convert.ToString(dt.Rows[i]["FK_Employee"]);
                // objbl.FK_Company = FK_Company;
                DataTable dtloc = objDal.DalEmployeeLocationDistance(BankKey, FKCompany, LocationEnteredDate, FKEmployee);
                objbl.Todate = LocationEnteredDate;
                objbl.BankKey = BankKey;
                objbl.ReqMode = "124";
                DataTable dt1 = objDal.DalCommonValidate(objbl);
                Origin = dt1.Rows[0].Field<string>("Origin");
                Destination = dt1.Rows[0].Field<string>("Destination");
                count = dtloc.Rows.Count - 1;

                if (dtloc.Rows.Count > 0)
                {
                    for (int j = 0; j < dtloc.Rows.Count; j++)
                    {
                        if (Convert.ToString(dtloc.Rows[j]["FK_Employee"]) == Convert.ToString(dt.Rows[i]["FK_Employee"]))
                        {
                            if (j == count)
                                LocationDets = LocationDets + Convert.ToString(dtloc.Rows[j]["LocLattitude"]) + "," + Convert.ToString(dtloc.Rows[j]["LocLongitude"]);
                            else
                                LocationDets = LocationDets + Convert.ToString(dtloc.Rows[j]["LocLattitude"]) + "," + Convert.ToString(dtloc.Rows[j]["LocLongitude"]) + "|";
                        }


                    }
                }

                string url = "https://maps.googleapis.com/maps/api/directions/json?";
                url = url + "origin=" + Origin + "&destination=" + Destination + "&waypoints=" + LocationDets + "&key=" + URLKey;
                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results1 = sr.ReadToEnd();
                dynamic dynJson = JsonConvert.DeserializeObject(results1);
                //obj.urldata = url;
                obj.FK_Employee = Convert.ToInt64(dt.Rows[i]["FK_Employee"]);
                obj.EmployeeName = Convert.ToString(dt.Rows[i]["EmpFName"]);
                obj.StartingPoint = Origin;
                obj.EndingPoint = Destination;
                obj.JSonData = results1;
                Origin = "";
                url = "";
                Destination = "";
                LocationDets = "";
                lst.Add(obj);
            }

            return lst;

        }
        public static SubCategoryDetailsList ConvertSubCategoryDetailsList(DataTable dt)
        {
            SubCategoryDetailsList log = new SubCategoryDetailsList();
            if (dt.Rows.Count > 0)
            {
                
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.SubCategoryList = ConvertSubCategoryList(dt);
                
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Sub Category Details";
            }
            return log;
        }
        public static List<SubCategoryList> ConvertSubCategoryList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new SubCategoryList()
                    {

                        ID_SubCategory = Convert.ToInt64(dr["ID_SubCategory"].ToString()),
                        SubCatName = Convert.ToString(dr["SubCatName"].ToString())
                     

                    }).ToList();

        }
        public static BrandDetails ConvertBrandDetails(DataTable dt)
        {
            BrandDetails log = new BrandDetails();
            if (dt.Rows.Count > 0)
            {
              
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.BrandDetailsList = ConvertSubBrandDetailsList(dt);
               
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Brand Details";
            }
            return log;
        }
        public static List<BrandDetailsList> ConvertSubBrandDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new BrandDetailsList()
                    {

                        ID_Brand = Convert.ToInt64(dr["ID_Brand"].ToString()),
                        BrandName = Convert.ToString(dr["BrName"].ToString())
                    }).ToList();

        }

        public static LeadSourceDetails ConvertLeadSourceDetails(DataTable dt)
        {
            LeadSourceDetails log = new LeadSourceDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadSourceDetailsList = ConvertLeadSourceDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead Source Details";
            }
            return log;
        }
        public static List<LeadSourceDetailsList> ConvertLeadSourceDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadSourceDetailsList()
                    {

                        ID_LeadFrom = Convert.ToInt64(dr["ID_LeadFrom"].ToString()),
                        LeadFromName = Convert.ToString(dr["LeadFromName"].ToString())
                    }).ToList();

        }
        public static LeadFromInfo ConvertLeadFromInfo(DataTable dt)
        {
            LeadFromInfo log = new LeadFromInfo();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                // log.LeadFromList= ConvertLeadFromList(dt);
                log.LeadFromList = dt;

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead From Details";
            }
            return log;
        }
        public static List<LeadFromList> ConvertLeadFromList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadFromList()
                    {

                        ID_FIELD = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        Name = Convert.ToString(dr["Name"].ToString()),
                        Code = Convert.ToInt64(dr["Code"].ToString()),
                        HasSub = Convert.ToString(dr["HasSub"].ToString()),
                        ShortName = Convert.ToString(dr["Short Name"].ToString()),
                        Address = Convert.ToString(dr["Address"].ToString())
                    }).ToList();

        }
        public static LeadHistory ConvertLeadHistory(DataTable dt)
        {
            LeadHistory log = new LeadHistory();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.LeadHistoryList = ConvertLeadHistoryList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Lead History";
            }
            return log;
        }
        public static List<LeadHistoryList> ConvertLeadHistoryList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new LeadHistoryList()
                    {

                        SlNo = Convert.ToString(dr["SlNo"].ToString()),
                        LeadNo = Convert.ToString(dr["LeadNo"].ToString()),
                        LeadDate = Convert.ToString(dr["LeadDate"].ToString()),
                        CustomerName = Convert.ToString(dr["CustomerName"].ToString()),
                        Mobile = Convert.ToString(dr["Mobile"].ToString()),
                        Email = Convert.ToString(dr["Email"].ToString())
                    }).ToList();

        }
        public static ProductType ConvertProductType(DataTable dt)
        {
            ProductType log = new ProductType();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductTypeList = ConvertProductTypeList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Product Type Details";
            }
            return log;
        }
        public static List<ProductTypeList> ConvertProductTypeList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProductTypeList()
                    {

                        ID_ProductType = Convert.ToInt64(dr["ID_ProductType"].ToString()),
                        ProductTypeName = Convert.ToString(dr["ProductTypeName"].ToString())
                    }).ToList();

        }
        public static TimeLineDetails ConvertTimeLineDetails(DataTable dt)
        {
            TimeLineDetails log = new TimeLineDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.TimeLineList = ConvertTimeLineList(dt);
            }
            return log;
        }
        public static List<TimeLineList> ConvertTimeLineList(DataTable dt)
        {
            Int32 Submode = 0;
            List<TimeLineList> lst = new List<TimeLineList>();
            if (dt.Rows.Count > 0)
            {
                //foreach (DataRow dr in dt.Rows)
                //{
                    TimeLineList obj = new TimeLineList();
                //obj.Title1 = Convert.ToString(dr["Title1"].ToString());
                //Submode=
                obj.DataList = ConvertDataList(dt);
                    lst.Add(obj);
                //}

            }
            return lst;
        }
        public static List<DataList> ConvertDataList(DataTable dt)
        {
            List<DataList> lst = new List<DataList>();

            //  string delimiter = "</br>";
            /*
             * <B>Employees List</B> 
             * <B>Follow-up Details</B>
             * <B>Replaced Componants</B>
             * <B>Provided Services</B>
             * <B>Attended by</B>
             * <B>Picked-Up Products</B>
             * <B>Delivered Components</B>
             * 
             */
            //List<string> list=new List<string>();
            // string[] rowval  ;
            // string sval = "";
            // foreach (DataRow dr in dt.Rows)
            // {
            //     list = new List<string>();
            //     sval = "";
            //     rowval = dr["Description"].ToString().Replace("</br>", ",").Replace("<B>", "").Replace("</B>", "").Split(',');
            //     foreach (var s in rowval)
            //     {
            //         if (s.Trim() != "")
            //         {
            //             var nn = s.Trim().Split(':');
            //             var qte = "'";
            //             if (nn.Length > 1)
            //             {
            //                 sval = sval+ qte + nn[0] + qte + ":" + qte + nn[1] + qte + ",";

            //             }
            //             else
            //             {
            //                 sval = sval+ qte + nn[0] + qte + ":" + "''" + ",";
            //             }
            //             //sval = sval + rowval[i];
            //         }

            //     }

            //     if (sval != "")
            //     {

            //         sval = "{"+sval.Trim(',')+"}";
            //         //string nnm = ;

            //         list.Add(sval);
            //         sval="";
            //     }


            //     DataList obj = new DataList();
            //     obj.SLNo = Convert.ToInt32(dr["SLNo"]);
            //     obj.Head = Convert.ToString(dr["Head"]);
            //     obj.Title1 = Convert.ToString(dr["Title1"]);
            //     obj.Title2 = Convert.ToString(dr["Title2"]);
            //     obj.Description = list;

            //     // obj.Description =  Convert.ToString(dr["Description"]).Split(new string[] { delimiter }, StringSplitOptions.None).ToList();
            //     //obj.Description = "{ "+Convert.ToString(dr["Description"]).Replace(delimiter," , ")+" }";
            //     obj.ID_Transaction = Convert.ToString(dr["ID_Transaction"]);
            //     obj.MoreData = Convert.ToString(dr["MoreData"]);
            //     obj.EntrOn = Convert.ToString(dr["EntrOn"]);
            //     obj.EntrBy = Convert.ToString(dr["EntrBy"]);
            //     lst.Add(obj);


            // }


            foreach (DataRow dr in dt.Rows)
            {
                string delimiter = "</Removesection/>";
                string delimiter_br = "</br>";
                string description = Convert.ToString(dr["Description"])
                    .Replace("</br><B>Employees List</B>", delimiter + " Employees List")
                    .Replace("</br><B>Follow-up Details</B>", delimiter + " Follow-up Details")
                    .Replace("</br><B>Replaced Componants</B>", delimiter + " Replaced Componants")
                    .Replace("</br><B>Provided Services</B>", delimiter + "Provided Services")
                    .Replace("</br><B>Attended by</B>", delimiter + " Attended by")
                    .Replace("</br><B>Picked-Up Products</B>", delimiter + " Picked-Up Products")
                    .Replace("</br><B>Delivered Components</B>", delimiter + " Delivered Components")
                .Replace("</br>Remarks ", delimiter + "Remarks");
                List<string> descriptionList = description.Split(new string[] { delimiter }, StringSplitOptions.None).ToList();
                List<string> descriptionOutput = new List<string>();

                foreach (var item in descriptionList)
                {

                    List<string> descriptionItem = item.Split(new string[] { delimiter_br }, StringSplitOptions.None).ToList();

                    if (descriptionItem[0].Trim() == "Employees List")
                    {
                        int indexToRemove = 0; // Index of the item to remove
                        if (indexToRemove >= 0 && indexToRemove < descriptionItem.Count)
                        {
                            descriptionItem.RemoveAt(indexToRemove);
                            descriptionOutput.Add("\"Employees List\":[" + String.Join(",", descriptionItem) + "]");
                        }
                    }
                    else if (descriptionItem[0].Trim() == "Follow-up Details")
                    {
                        var htmlText = descriptionItem[2].Trim()
                            .Replace("<div class='tblreponsive'>", "")
                            .Replace("</div>", "")
                            .Replace("<table class='table table-striped'>", "")
                            .Replace("</table>", "")
                            .Replace("<thead  class='thead-primary'>", "")
                            .Split(new string[] { "</thead>" }, StringSplitOptions.None).ToList();

                        var headingListString = htmlText[0];
                        var dataListString= htmlText[1];
                        //
                        var HeadingList= headingListString.Replace("<th>", "").Split(new string[] { "</th>" }, StringSplitOptions.None).ToList();
                        var dataList= dataListString.Replace("<tr>", "").Split(new string[] { "</tr>" }, StringSplitOptions.None).ToList();

                        List<string> jsonDataAll = new List<string>();
                       
                        foreach (var oneRow in dataList)
                        {
                           var oneRowArray= oneRow.Replace("<td>","").Split(new string[] { "</td>" }, StringSplitOptions.None).ToList();

                           
                            int ind = 0;
                            //foreach (var oneRowData in oneRowArray)
                            //{
                            //    sb.AppendFormat("'{0}':'{1}',", HeadingList[ind], oneRowData);
                            //    ind++;
                            //}

                            List<string> jsondata = new List<string>();
                            foreach (var heading in HeadingList)
                            {
                                string inputData = "";
                                if (ind >= 0 && ind < oneRowArray.Count)
                                {
                                    inputData = oneRowArray[ind];
                                }
                                else {
                                    inputData = "";
                                }

                                jsondata.Add($"'{heading}':'{inputData}'" );
                                ind++;
                            }

                            jsonDataAll.Add("{"+String.Join(",", jsondata)+"}");


                        }
                       

                        descriptionOutput.Add($"'{descriptionItem[0].Trim()}':["+ String.Join(",", jsonDataAll) + "]");
                    }
                    else if (descriptionItem[0].Trim() == "Replaced Componants")
                    {

                    }
                    else if (descriptionItem[0].Trim() == "Provided Services")
                    {

                    }
                    else if (descriptionItem[0].Trim() == "Attended by")
                    {
                        int indexToRemove = 0; // Index of the item to remove
                        if (indexToRemove >= 0 && indexToRemove < descriptionItem.Count)
                        {
                            descriptionItem.RemoveAt(indexToRemove);
                            descriptionOutput.Add("\"Attended by\":[" + String.Join(",", descriptionItem) + "]");
                        }
                    }
                    else if (descriptionItem[0].Trim() == "Picked-Up Products")
                    {

                    }
                    else if (descriptionItem[0].Trim() == "Delivered Components")
                    {

                    }
                    else if (descriptionItem[0].Trim() == "Remarks")
                    {
                        descriptionOutput.Add("\"Remarks\":" + (String.IsNullOrWhiteSpace(descriptionItem[1].Trim()) ? "\"\"" : descriptionItem[1].Trim()));
                    }
                    else
                    {
                        
                        List<string> jsondata = new List<string>();
                        foreach (var data in descriptionItem)
                        {
                            var dataList = data.Split(new string[] { ":" }, StringSplitOptions.None).ToList();
                            int ind = 1;
                            string inputData = "";
                            if (ind >= 0 && ind < dataList.Count)
                            {
                                inputData = dataList[ind].Trim();
                            }
                            else
                            {
                                inputData = "";
                            }

                            jsondata.Add($"'{dataList[0].Trim()}':'{inputData}'");
                        }
                        descriptionOutput.Add("{" + String.Join(",", jsondata) + "}");
                    }

                }



                DataList obj = new DataList();
                obj.SLNo = Convert.ToInt32(dr["SLNo"]);
                obj.Head = Convert.ToString(dr["Head"]);
                obj.Title1 = Convert.ToString(dr["Title1"]);
                obj.Title2 = Convert.ToString(dr["Title2"]);
                obj.Description = descriptionOutput;
                // obj.Description =  Convert.ToString(dr["Description"]).Split(new string[] { delimiter }, StringSplitOptions.None).ToList();
                //obj.Description = "{ "+Convert.ToString(dr["Description"]).Replace(delimiter," , ")+" }";
                obj.ID_Transaction = Convert.ToString(dr["ID_Transaction"]);
                obj.MoreData = Convert.ToString(dr["MoreData"]);
                obj.EntrOn = Convert.ToString(dr["EntrOn"]);
                obj.EntrBy = Convert.ToString(dr["EntrBy"]);
                lst.Add(obj);
            }
            return lst;

        }
        public static AttendanceDetails ConvertAttendanceDetails(DataTable dt)
        {
            AttendanceDetails log = new AttendanceDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AttendanceDetailsList = ConvertAttendanceDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Attendance Punch Details";
            }
            return log;
        }
        public static List<AttendanceDetailsList> ConvertAttendanceDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new AttendanceDetailsList()
                    {

                        ID_EmployeeAttanceMarking = Convert.ToInt64(dr["ID_EmployeeAttanceMarking"].ToString()),
                        FK_Employee = Convert.ToInt64(dr["FK_Employee"].ToString()),
                        EmployeeName = Convert.ToString(dr["EmployeeName"].ToString()),
                        Longitude = Convert.ToString(dr["LocLongitude"].ToString()),
                        Lattitude = Convert.ToString(dr["LocLattitude"].ToString()),
                        LocationName = Convert.ToString(dr["LocLocationName"].ToString()),
                        EnteredDate = Convert.ToString(dr["EnteredDate"].ToString()),
                        EnteredTime = Convert.ToString(dr["EnteredTime"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        TimeDuration = Convert.ToString(dr["EnteredTime"].ToString())
                    }).ToList();

        }
        public static AttendancePunchDetails ConvertAttendancePunchDetails(DataTable dt)
        {
            AttendancePunchDetails log = new AttendancePunchDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                //if (Convert.ToInt64(dt.Rows[0]["PunchMode"]) == 1)
                 log.AttendanceFirstPunchdetails = ConvertAttendanceFirstPunchdetails(dt);
                 log.AttendanceLastPunchdetails = ConvertAttendanceLastPunchdetails(dt);
               

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Attendance Punch Details";
            }
            return log;
        }

        public static List<AttendanceFirstPunchdetails> ConvertAttendanceFirstPunchdetails(DataTable dt)
        {
            List<AttendanceFirstPunchdetails> lst = new List<AttendanceFirstPunchdetails>();
            int count =0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    AttendanceFirstPunchdetails obj = new AttendanceFirstPunchdetails();//PunchMode
            //    if (dr["PunchMode"] == "1")
            //    {
            //        obj.ID_EmployeeAttanceMarking = Convert.ToInt64(dr["ID_EmployeeAttanceMarking"]);
            //        obj.FK_Employee = Convert.ToInt64(dr["FK_Employee"]);
            //        obj.EmployeeName = Convert.ToString(dr["EmployeeName"]);
            //        obj.Longitude = Convert.ToString(dr["LocLongitude"]);
            //        obj.Lattitude = Convert.ToString(dr["LocLattitude"]);
            //        obj.LocationName = Convert.ToString(dr["LocLocationName"]);
            //        obj.EnteredDate = Convert.ToString(dr["EnteredDate"]);
            //        obj.EnteredTime = Convert.ToString(dr["EnteredTime"]);
            //        obj.Status = Convert.ToString(dr["Status"]);
            //    }                   
            //    lst.Add(obj);
            //}
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0].Field<Int32>("PunchMode")) == "1")
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AttendanceFirstPunchdetails obj = new AttendanceFirstPunchdetails();
                        if (Convert.ToString(dt.Rows[i]["PunchMode"]) == "1")
                        {
                            count = 1;
                            obj.ID_EmployeeAttanceMarking = Convert.ToString(dt.Rows[i]["ID_EmployeeAttanceMarking"]);
                            obj.FK_Employee = Convert.ToString(dt.Rows[i]["FK_Employee"]);
                            obj.EmployeeName = Convert.ToString(dt.Rows[i]["EmployeeName"]);
                            obj.Designation = Convert.ToString(dt.Rows[i]["DesName"]);
                            obj.Longitude = Convert.ToString(dt.Rows[i]["LocLongitude"]);
                            obj.Lattitude = Convert.ToString(dt.Rows[i]["LocLattitude"]);
                            obj.LocationName = Convert.ToString(dt.Rows[i]["LocLocationName"]);
                            obj.EnteredDate = Convert.ToString(dt.Rows[i]["EnteredDate"]);
                            obj.EnteredTime = Convert.ToString(dt.Rows[i]["EnteredTime"]);
                            obj.PunchStatus = "PunchIn";
                            lst.Add(obj);
                        }
                    }
                }
                else
                {
                    if (count == 0)
                    {
                        AttendanceFirstPunchdetails obj = new AttendanceFirstPunchdetails();
                        obj.ID_EmployeeAttanceMarking = "";
                        obj.FK_Employee = "";
                        obj.EmployeeName = "";
                        obj.Designation = "";
                        obj.Longitude = "";
                        obj.Lattitude = "";
                        obj.LocationName = "";
                        obj.EnteredDate = "";
                        obj.EnteredTime = "";
                        obj.PunchStatus = "";
                        lst.Add(obj);
                    }
                }

            }
            
            return lst;
           

        }
        public static List<AttendanceLastPunchdetails> ConvertAttendanceLastPunchdetails(DataTable dt)
        {
            int count = 0;

            List<AttendanceLastPunchdetails> lst = new List<AttendanceLastPunchdetails>();
            if (dt.Rows.Count > 0)
            {
                //if (Convert.ToString(dt.Rows[0].Field<Int32>("PunchMode")) == "2")
                //{
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AttendanceLastPunchdetails obj = new AttendanceLastPunchdetails();
                        if (Convert.ToString(dt.Rows[i]["PunchMode"]) == "2")
                        {
                            count = 1;
                            obj.ID_EmployeeAttanceMarking = Convert.ToString(dt.Rows[i]["ID_EmployeeAttanceMarking"]);
                            obj.FK_Employee = Convert.ToString(dt.Rows[i]["FK_Employee"]);
                            obj.EmployeeName = Convert.ToString(dt.Rows[i]["EmployeeName"]);
                            obj.Designation = Convert.ToString(dt.Rows[i]["DesName"]);
                            obj.Longitude = Convert.ToString(dt.Rows[i]["LocLongitude"]);
                            obj.Lattitude = Convert.ToString(dt.Rows[i]["LocLattitude"]);
                            obj.LocationName = Convert.ToString(dt.Rows[i]["LocLocationName"]);
                            obj.EnteredDate = Convert.ToString(dt.Rows[i]["EnteredDate"]);
                            obj.EnteredTime = Convert.ToString(dt.Rows[i]["EnteredTime"]);
                            obj.PunchStatus = "PunchOut";
                            lst.Add(obj);
                        }
                    }
                //}
              
                    if (count == 0)
                    {
                        AttendanceLastPunchdetails obj = new AttendanceLastPunchdetails();
                        obj.ID_EmployeeAttanceMarking = "";
                        obj.FK_Employee = "";
                        obj.EmployeeName = "";
                        obj.Designation = "";
                        obj.Longitude = "";
                        obj.Lattitude = "";
                        obj.LocationName = "";
                        obj.EnteredDate = "";
                        obj.EnteredTime = "";
                        obj.PunchStatus = "";
                        lst.Add(obj);
                    }
               

            }
           
            return lst;

        }
        public static UserDetails ConvertUserDetails(DataTable dt)
        {
            UserDetails log = new UserDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.UserList = ConvertUserList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the User Details";
            }
            return log;
        }
        public static List<UserList> ConvertUserList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new UserList()
                    {

                        ID_Employee = Convert.ToInt64(dr["ID_Employee"].ToString()),
                        EmployeeName = Convert.ToString(dr["EmployeeName"].ToString()),
                        EmpCode = Convert.ToString(dr["EmpCode"].ToString()),
                        PhoneNo = Convert.ToString(dr["EmpEmrgContactNum"].ToString()),
                        PnUserToken = Convert.ToString(dr["PnUserToken"].ToString()),
                        FK_Branch= Convert.ToString(dr["FK_Branch"].ToString()),
                        BranchName= Convert.ToString(dr["BrName"].ToString()),
                        EntrBy = Convert.ToString(dr["EntrBy"].ToString()),
                    }).ToList();

        }
        public static CommonAPIR ConvertTokencheck(DataTable dt)
        {
            CommonAPIR log = new CommonAPIR();
            log.ResponseCode = dt.Rows[0].Field<string>("ResponseCode");
            log.ResponseMessage = dt.Rows[0].Field<string>("ResponseMessage");
            return log;
        }
        //public static MyActivityCount ConvertMyActivityCount(DataTable dt)
        //{
        //    MyActivityCount log = new MyActivityCount();
        //    log.ResponseCode = "0";
        //    log.ResponseMessage = "Transaction Verified";
        //    log.Completed = 2;
        //    log.Pending = 5;
        //    log.Upcoming = 3;
        //    log.Todolist = 2;
        //    return log;
        //}
        public static MyActivityCount ConvertMyActivityCount(DataTable dt)
        {
            MyActivityCount log = new MyActivityCount();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MyActivityCountList = ConvertMyActivityCountList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the count Details";
            }
            return log;
        }
        public static List<MyActivityCountList> ConvertMyActivityCountList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MyActivityCountList()
                    {
                        SubMode = Convert.ToInt32(dr["SubMode"].ToString()),                      
                        Name = Convert.ToString(dr["Name"].ToString()),
                        Count = Convert.ToInt32(dr["Count"].ToString())
                    }).ToList();

        }
        public static MyActivitysActionCountDetails ConvertMyActivitysActionCountDetails(DataTable dt)
        {
            MyActivitysActionCountDetails log = new MyActivitysActionCountDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MyActivitysActionCountList = ConvertMyActivitysActionCountList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the count Details";
            }
            return log;
        }
        public static List<MyActivitysActionCountList> ConvertMyActivitysActionCountList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MyActivitysActionCountList()
                    {

                        ID_ActionType = Convert.ToInt64(dr["ID_ActionType"].ToString()),
                        ActionName = Convert.ToString(dr["ActnTypeName"].ToString()),
                        count = Convert.ToInt32(dr["Count"].ToString()),
                     
                    }).ToList();

        }
        public static MyActivitysActionDetails ConvertMyActivitysActionDetails(DataTable dt)
        {
            MyActivitysActionDetails log = new MyActivitysActionDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MyActivitysActionList = ConvertMyActivitysActionList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the count Details";
            }
            return log;
        }
        public static List<MyActivitysActionList> ConvertMyActivitysActionList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MyActivitysActionList()
                    {

                        ID_LeadGenerateProduct = Convert.ToString(dr["ID_LeadGenerateProduct"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        EnquiryAbout = Convert.ToString(dr["EnquiryAbout"].ToString()),
                        LgpDescription = Convert.ToString(dr["LgpDescription"].ToString()),
                        Action = Convert.ToString(dr["Action"].ToString()),
                        ActionDate = Convert.ToString(dr["ActionDate"].ToString()),
                        Status = Convert.ToString(dr["Status"].ToString()),
                        Agentremarks = Convert.ToString(dr["Agentremarks"].ToString()),
                        CustomerRemarks = Convert.ToString(dr["Agentremarks"].ToString()),
                        CustomerName = Convert.ToString(dr["Customer"].ToString()),
                        FollowedBy = Convert.ToString(dr["FollowedBy"].ToString()),
                        ActionType = Convert.ToString(dr["ActionType"].ToString())
                    }).ToList();

        }
        public static MyActivitysFliters ConvertMyActivitysFliters(DataTable dt)
        {
            MyActivitysFliters log = new MyActivitysFliters();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MyActivitysFlitersList = ConvertMyActivitysFlitersList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the count Details";
            }
            return log;
        }
        public static List<MyActivitysFlitersList> ConvertMyActivitysFlitersList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MyActivitysFlitersList()
                    {

                        IdFliter = Convert.ToInt32(dr["IdFliter"].ToString()),
                        FliterType = Convert.ToString(dr["FliterType"].ToString()),
                       
                    }).ToList();

        }
        public static ClosedLeadReport ConvertClosedLeadReport(DataTable dt)
        {
            ClosedLeadReport log = new ClosedLeadReport();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ClosedLeadReportList = ConvertClosedLeadReportList(dt);
                //}

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Records in the Closed Report";
            }
            return log;
        }
        public static List<ClosedLeadReportList> ConvertClosedLeadReportList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ClosedLeadReportList()
                    {

                        Category = Convert.ToString(dr["Category"].ToString()),
                        Employee = Convert.ToString(dr["Employee"].ToString()),
                        Product = Convert.ToString(dr["Product"].ToString()),
                        Branch = Convert.ToString(dr["Branch"].ToString()),
                        Count = Convert.ToString(dr["Count"].ToString()),
                        Criteria = Convert.ToString(dr["Criteria"].ToString()),
                        FK_ComCategory = Convert.ToString(dr["FK_ComCategory"].ToString())

                    }).ToList();

        }
    }

}

