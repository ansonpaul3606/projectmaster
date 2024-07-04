//----------------------------------------------------------------------
//Created By  : Athul mathew
//Created On  : 25/01/2022
//Purpose		: Customerserviceregister
//-------------------------------------------------------------------------
//Modification
//On          By OMID/Remarks
//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class CustomerserviceregisterModel
    {
        public class CustomerserviceregisterView
        {
      
             public long SlNo { get; set; }
            public long ID_Customerserviceregister { get; set; }

            public string CustomerNumber { get; set; }
            public long ID_FIELD { get; set; }
            public long CusMode { get; set; }
            public DateTime? TicketDate { get; set; }
            public string TicketNumber { get; set; }
            public string TicketTime { get; set; }
            public string CustomeName { get; set; }
            public string CustomerMobile { get; set; }
            public string CustomerAddress { get; set; }
            public long Category { get; set; }
            public string PriorityName { get; set; }
            public long FK_Customer { get; set; }
            public long FK_CustomerOthers { get; set; }
            public long company { get; set; }
            public long FK_Product { get; set; }        
            public long ID_ComplaintList { get; set; }
            public long ID_ServiceList { get; set; }
            public long CSRChannelID { get; set; }
            public long CSRChannelSubID { get; set; }
            public string CSRChannelSub { get; set; }

           
            public string CSRDescription { get; set; }
            public string OtherMobile { get; set; }
            public string Landmark { get; set; }           
            public DateTime? Servicefromdate { get; set; }
         
            public DateTime? Servicetodate { get; set; }
           
            public string Servicefromtime { get; set; }
       
            public string Servicetotime { get; set; }

          

            public long Priority { get; set; }            
            public string Mode { get; set; }
            public string Product { get; set; }
            public int NextAction { get; set; }
            public int EmpId { get; set; }
            public string Emp { get; set; }
            public List<NextAction> NextActionMode { get; set; }
            public List<ComplaintListModel.ComplaintView> ComplaintList { get; set; }
            public List  <BrandList> BrandList { get; set; }
            public List<CheckList> CheckList { get; set; }
          
            public List<categorytyp> categorytyps { get; set; }
            public List<ChannelType> ChannelTypes { get; set; }
            public List<othercompany> othercompanies { get; set; }
        
            public List<Service> ServiceList { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64? LastID { get; set; }
            public string TransMode { get; set; }

            public List<CategoryList> CategoryList { get; set; }
            public Int32 CategoryID { get; set; }
            public string CategoryName { get; set; }
          
            public string Address2 { get; set; }
            
            public long CountryID { get; set; }
         

            public long StatesID { get; set; }
           
          
            public long DistrictID { get; set; }
            public long AreaID { get; set; }
            public long PostID { get; set; }
            public List<SubCategoryList> SubCategoryList { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Brand { get; set; }

        }
        public class SubCategoryList
        {
            public long CategoryID { get; set; }
            public long ID_SubCategory { get; set; }
            public string SubCatName { get; set; }
        }
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
         
        }
        public class Complaint
        {
            public long ID_ComplaintList { get; set; }
            public string ComplaintName { get; set; }
        }
        public class Service
        {

            public long ID_Services { get; set; }
            public string ServicesName { get; set; }
        }

        public class CheckList
        {
            public long CheckListDetails { get; set; }
            public string Remarks { get; set; }


        }
        public class CategoryList
        {
            public Int32? CategoryID { get; set; }
            public string CategoryName { get; set; }

        }
        public class BrandList
        {
            public Int64 BrandID { get; set; }
            public string BrandName { get; set; }

        }

        public class UpdateCustomerserviceregister
        {
            public byte UserAction { get; set; }
            public byte Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_CustomerServiceRegister { get; set; }
            public long FK_Customer { get; set; }
            public long FK_CustomerOthers { get; set; }
            public string CSRTickno { get; set; }
            public string CusName { get; set; }
            public string CusMobile { get; set; }
            public string CusAddress { get; set; }
            public string CSRContactNo { get; set; }
            public string CSRLandmark { get; set; }
            public DateTime? CSRServiceFromDate { get; set; }
            public DateTime? CSRServiceToDate { get; set; }
            public string CSRServicefromtime { get; set; }
            public string CSRServicetotime { get; set; }
            public DateTime? TicketDate { get; set; }
       
            public long CSRPriority { get; set; }
            public long CSRCurrentStatus { get; set; }
            public DateTime? CSRClosedDate { get; set; }       
            public long FK_BranchCodeUser { get; set; }                  
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Branch { get; set; }        
            public long FK_Customerserviceregister { get; set; }
            public long CSRPCategory { get; set; }
            public long FK_OtherCompany { get; set; }
            public long FK_Product { get; set; }
            public long FK_ComplaintList { get; set; }
            public long FK_ServiceList { get; set; }
            
            public string CSRODescription { get; set; }
            public string  CheckList { get; set; }
            public long CSRChannelID { get; set; }
            public long CSRChannelSubID { get; set; }

            public long Status { get; set; }
            public long AttendedBy { get; set; }
            public string TicketTime { get; set; }
            public long FK_Category { get; set; }
            public long FK_District { get; set; }
            public long FK_States { get; set; }
            public long FK_Country { get; set; }
            public long FK_Area { get; set; }
            public long FK_Post { get; set; }
            public string Address2 { get; set; }
            public Int64 LastID { get; set; }
            public long FK_SubCategory { get; set; }
            public long FK_Brand { get; set; }

        }

        //public class UpdateCustomerserviceregister
        //{
        //    public byte UserAction { get; set; }
        //    public byte Debug { get; set; }
        //    public string TransMode { get; set; }
        //    public long ID_Customerserviceregister { get; set; }
        //    public string CSRCustomerNo { get; set; }
        //    public string CSRCustomerName { get; set; }
        //    public string CSRCustomerMobile { get; set; }
        //    public string CSRCustomerAddress { get; set; }
        //    public string CSROtherMobile { get; set; }
        //    public string CSRLandmark { get; set; }
        //    public DateTime CSRServicefromdate { get; set; }
        //    public DateTime? CSRServicetodate { get; set; }
        //    public string CSRServicefromtime { get; set; }
        //    public string CSRServicetotime { get; set; }
        //    public long FK_Company { get; set; }
        //    public long FK_BranchCodeUser { get; set; }
        //    //public bool Passed { get; set; }
        //    //public bool Cancelled { get; set; }
        //    public string EntrBy { get; set; }
        //    public long FK_Machine { get; set; }
        //    public string CSRMode { get; set; }
        //    //public long BackupId { get; set; }
        //    public string ProductDetails { get; set; }
        //    public string OtherProductDetails { get; set; }
        //    public long FK_Customerserviceregister { get; set; }
        //    public long FK_Branch { get; set; }
        //    public string TicketNo { get; set; }

        //}

        public static string _deleteProcedureName = "proCustomerserviceregisterDelete";
        public static string _updateProcedureName = "proCustomerserviceregisterUpdate";
        public static string _selectProcedureName = "proCustomerserviceregisterSelect";
        public static string _SelectProductProcedureName = "Proselectproductforservice";
        public class DeleteCustomerserviceregister
        {
            public long FK_Customerserviceregister { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Reason { get; set; }
        }
        public class othercompany
        {       
            public int ID_OtherCompany { get; set; }
            public string OCName { get; set; }
        }
        public class DeleteReasonInput
        {
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
        }

        public class CustomerserviceregisterID
        {
            public Int64 ID_Customerserviceregister { get; set; }
            public Int64 ReasonID { get; set; }
        }

        public class InclusiveRegistration
        {
            public long ID_Customerserviceregister { get; set; }
            [Display(Name = "SI.NO")]
            public int SINO { get; set; }

            public string TicketNo { get; set; }

            public string CustomerName { get; set; }

            public string Mobile { get; set; }
          

        }
        public class IDCustomerRegister
        {
            public long FK_CustomerServiceRegister { get; set; }
       
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public int FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public int CheckList { get; set; }
           
    

        }
        public class ExistCustomer
        {
           
           public long FK_CustomerServiceRegister { get; set; }
            public long FK_Product { get; set; }
            public int Debug { get; set; }      
            public string Mobile { get; set; }
            public long FK_Company { get; set; }
            public int FK_Machine { get; set; }
            public int FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
           


        }

        public class ProductInput
        {
            public long FK_Customer { get; set; }            
            public long FK_Branch { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Byte Mode { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
           
        }

        public class ProductView
        {
            public long ID_Product { get; set; }
            public string ProductName { get; set; }
            public string ShortName { get; set; }
        }

        public static string _SelectTicketNumber = "ProTicketAutoGenerate";
        public class Ticket
        {
            public long FK_Branch { get; set; }
            public string TickNonew { get; set; }
        }


        public static string _SelectCheckList = "ProSelectproductchecklist";

        public class UpdateCustomerserviceregisterResult
        {
            public string Success { get; set; }
            public long FK_CustomerServiceRegister { get; set; }
          
        }
        public APIGetRecordsDynamic<UpdateCustomerserviceregisterResult> UpdateCustomerserviceregisterData(UpdateCustomerserviceregister input, string companyKey)
        {
            return Common.GetDataViaProcedure<UpdateCustomerserviceregisterResult, UpdateCustomerserviceregister>(companyKey: companyKey, procedureName: _updateProcedureName, parameter: input);

        }
        public Output UpdateCustomerserviceregisterDatas(UpdateCustomerserviceregister input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCustomerserviceregister>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        // 
        public Output DeleteCustomerserviceregisterData(DeleteCustomerserviceregister input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCustomerserviceregister>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<UpdateCustomerserviceregister> GeCustomerserviceregisterData(CustomerserviceregisterID input, string companyKey)
        {
            return Common.GetDataViaProcedure<UpdateCustomerserviceregister, CustomerserviceregisterID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<ProductView> GetProducts(ProductInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductView, ProductInput>(companyKey: companyKey, procedureName: _SelectProductProcedureName, parameter: input);

        }

        public APIGetRecordsDynamic<CheckList> GetCheckList(CheckList input, string companyKey)
        {
            return Common.GetDataViaProcedure<CheckList, CheckList>(companyKey: companyKey, procedureName: _SelectCheckList, parameter: input);

        }

        public APIGetRecordsDynamic<Ticket> GetTikcetNumber(Ticket input, string companyKey)
        {
            return Common.GetDataViaProcedure<Ticket, Ticket>(companyKey: companyKey, procedureName: _SelectTicketNumber, parameter: input);

        }

        public static string _selectReasonProcedureName = "proReasonSelect";
        public APIGetRecordsDynamic<ReasonModel.ReasonsView> GetReasonData(DeleteReasonInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ReasonModel.ReasonsView, DeleteReasonInput>(companyKey: companyKey, procedureName: _selectReasonProcedureName, parameter: input);

        }



       

     


        public static string _SelectCustomer = "ProCustomerSelects";

        public class CustomerSearchInput
        {
            public string TransMode { get; set; }
            public long FK_Customer { get; set; }
            public Int16 PageIndex { get; set; }
            public Int16 PageSize { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }

        public class CustomerSearchResult
        {
            public long CustomerID { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
        }
        public class categorytyp
        {
            public Int32? ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public class SettingsInput
        {
            public string PSModule { get; set; }
            public string PSPage { get; set; }
            public Int32 PSField { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 Mode { get; set; }

        }


        public class BillFld
        {                
            public Int32 FK_Product { get; set; }
            public Int32 FK_Customer { get; set; }
            public Int32 FK_Category { get; set; }
            public Int32 FK_CustomerOthers { get; set; }
            public Int32 FK_Branch { get; set; }
            public Int32 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int Mode { get; set; }
            public int MasterID { get; set; }

        }

        public class AccountbalFill
        {
          
            public Int32 FK_Customer { get; set; }
            public DateTime TransDate { get; set; }
          

        }
        public class ChannelType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class SettingData
        {
          
            public Boolean PSValue { get; set; }

            public string PSLabelName { get; set; }
            public Int32 PSField { get; set; }



        }

        public class GetChannelBy
        {
            public string TransMode { get; set; }
            public long ID_LeadFrom { get; set; }
            public string EntrBy { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
        }

        public APIGetRecordsDynamic<CustomerSearchResult> GetCustomer(CustomerSearchInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerSearchResult, CustomerSearchInput>(companyKey: companyKey, procedureName: _SelectCustomer, parameter: input);
        }
        public APIGetRecordsDynamic<CustomerserviceregisterView> GetservregData(IDCustomerRegister input, string companyKey)
        {
            return Common.GetDataViaProcedure<CustomerserviceregisterView, IDCustomerRegister>(companyKey: companyKey, procedureName: "ProCustomerServiceRegisterSelect", parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> GetservregcheckData(IDCustomerRegister input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<IDCustomerRegister>(companyKey: companyKey, procedureName: "ProCustomerServiceRegisterSelect", parameter: input);

        }
        public APIGetRecordsDynamicdn<dynamic> Getexistreg(ExistCustomer input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<ExistCustomer>(companyKey: companyKey, procedureName: "ProCustomerTicketHistoryChecking", parameter: input);
        }
        public APIGetRecordsDynamic<categorytyp> GetCategorylist(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<categorytyp, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
       
        public APIGetRecordsDynamicdn<dynamic> GetBillList(BillFld input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<BillFld>(companyKey: companyKey, procedureName: "ProProductWisePreviousComplaintHistory", parameter: input);

        }
        public APIGetRecordsDynamicdn<dynamic> GetCustomerAccountBalance(AccountbalFill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<AccountbalFill>(companyKey: companyKey, procedureName: "ProGetCustomerAccountBalance", parameter: input);

        }
        public APIGetRecordsDynamic<ChannelType> GetChannel(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ChannelType, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<SettingData> GetSettingsInfo(SettingsInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<SettingData, SettingsInput>(companyKey: companyKey, procedureName: "ProGetPageSettingsDetails", parameter: input);

        }
        public APIGetRecordsDynamicdn<dynamic> GetChannalTrough(GetChannelBy input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetChannelBy>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);



        }
    }
}
