/*----------------------------------------------------------------------
Created By  : Haseena K
Created On  : 25/01/2022
Purpose		: LeadGenerate
-------------------------------------------------------------------------
Modification
On          By OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class LeadGenerateModel
    {
        public class LeadGenerateView
        {
            public long SlNo { get; set; }
            public string LeadNo { get; set; }
            [Required(ErrorMessage = "Please Enter Enquiry Date")]
            public DateTime LeadDate { get; set; }
            [Required(ErrorMessage = "Please Enter Contact Name")] 
            public string CusName { get; set; }
            public string CusNameTitle { get; set; }
            public long ID_Customer { get; set; }
            public long FK_CustomerOthers { get; set; }
            [Required(ErrorMessage = "Please Enter Contact Address")]
            public string CusAddress { get; set; }
            public string CusAddress2 { get; set; }
            public string CusMobile { get; set; }

            public string Area { get; set; }//Address3
            public long AreaID { get; set; }//Address3_ID

            public string CusEmail { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead From")]
            public long LeadFrom { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead By")]
            public long LeadBy { get; set; }
                 public string LeadByName { get; set; }
            
            [Required(ErrorMessage = "Please Enter Collected By")]
            public long CollectedBy { get; set; }
            public string CollectedByName { get; set; }
            public string Company { get; set; }
            public string TransMode { get; set; }
            public string CusPhnNo { get; set; }
            public List<ProdProjDTL> ProductDetails { get; set; }
            public Int64 LeadGenerateID { get; set; }
            public long SubMedia { get; set; }
            public Int64 ReasonID { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public Int64? LastID { get; set; }
            public long DistrictID { get; set; }
            public long PostID { get; set; }
            public long ID_MediaMaster { get; set; }
            
            public string CntryName { get; set; }
            public string StName { get; set; }
            public string DtName { get; set; }
            public string PostName { get; set; }
           
            public Int64 TotalCount { get; set; }
            public string PinCode { get; set; }
            public string CusMobileAlternate { get; set; }
            public long FK_CustomerAssignment { get; set; }
            public long? FK_Quotation { get; set; } = 0;
            public long FK_Departements { get; set; }
            public string CusMobile1 { get; set; }
            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }

        }
        public class LeadFromViewPop
        {
            public Int32 ID_FIELD { get; set; }
            public string Name { get; set; }
            public string EmpCode { get; set; }
            public string MobNo { get; set; }
            public string Address { get; set; }
        }
        public class LeadFrom
        {
            public Int32 ID_LeadFrom { get; set; }
            public string LeadFromName { get; set; }
        }
        public class Departement
        {
            public long ID_Department { get; set; }
            public string DeptName { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
            public long NxtActnStage { get; set; }
            public long FK_ActionStatus { get; set; }
        }
        public class DuplicateCheck
        {
            public long PreviousID { get; set; }
            public string EntrBy { get; set; }
        }
        public class LeadGenerateViewList
        { 
            public List<LeadFrom> LeadFromList { get; set; }
            public List<Departement> DepartementList { get; set; }
            public List<NextAction> NextActionList { get; set; }
            public long FK_Employee{ get; set; }
            public List<EmployeeInfo> EmployeeInfoList { get; set; }
            public List<LeadGenerateModel.EmployeeLeadInfo> EmployeeLeadInfoList { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<BranchTypes> BranchTypelists { get; set; }
            public List<ActionTypes> Actntyplists { get; set; }
            public List<MediaTypes> mediatyplists { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<SubMedia> SubMedias { get; set; }
            public List<ProductLocationList> ProductLocationList { get; set; }

            public IEnumerable<ListData> Title { get; set; }
            public long BranchIDs { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
        }
        public class BranchTypes
        {
            public int BranchTypeID { get; set; }
            public string BranchType { get; set; }

            public long BranchModeID { get; set; }
            public long FK_BranchMode { get; set; }
            public long FK_BranchType { get; set; }

        }
        public class ActionTypes
        {
            public int ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }       
        }

        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GetModeData
        {
            public Int32 Mode { get; set; }
        }
        public class ListData
        {
            public string ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class MediaTypes
        {
            public int ID_MediaMaster { get; set; }
            public string MdaName { get; set; }


        }
        public class ProductLocationList
        {
            public long FK_ProductLocation { get; set; }
            public string ProductLocation { get; set; }

        }
        public class PostList
        {
            public int PostID { get; set; }
            public string Post { get; set; }

            public string PinCode { get; set; }
            //public long FK_Post { get; set; }

        }

        public class Postlistrelated
        {
            public int DistrictID { get; set; }
            
        }

        public class Branchs
        {
            public int BranchID { get; set; }
            public string Branch { get; set; }
            public long FK_Branch { get; set; }


        }
        public class SubMedia
        {
            public int SubmediaID { get; set; }
            public string Submedia { get; set; }
          
        }
        public class GetLeadBy
        {
            public string TransMode { get; set; }
            public long ID_LeadFrom { get; set; }
            public string EntrBy { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
        }

        public class GetLeadThrough
        {

          public long ID_FIELD { get; set; }
            public string CODE { get; set; }
            public string NAME { get; set; }
            public string ShortName { get; set; }
            public string Address1 { get; set; }
           
        }
        
        public class CustInfo
        {
            public long ID_Customer { get; set; }
          
            public string CusName { get; set; }
          
            public string CusAddress1 { get; set; }
            public string CusAddress2 { get; set; }
            public string CusEmail { get; set; }
               public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long PostID { get; set; }
            public long AreaID { get; set; }
            public string Company { get; set; }

            public string CntryName { get; set; }
            public string StName { get; set; }
            public string DtName { get; set; }
            public string PostName { get; set; }
            public string Area { get; set; }
            public string PinCode { get; set; }
            public string CusMobileAlternate { get; set; }
          

        }
        public class CustInfoOther
        {
            public long ID_CustomerOthers { get; set; }

            public string CusName { get; set; }
       
            public string CusAddress1 { get; set; }
            public string CusAddress2 { get; set; }
            public string CusEmail { get; set; }
            public long CountryID { get; set; }
            public long StatesID { get; set; }
            public long DistrictID { get; set; }
            public long PostID { get; set; }
            public long AreaID { get; set; }
            public string Company { get; set; }
            public string CntryName { get; set; }
            public string StName { get; set; }
            public string DtName { get; set; }
            public string PostName { get; set; }
            public string Area { get; set; }
            public string PinCode { get; set; }
            public string CusMobileAlternate { get; set; }
        }
        public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public long FK_Branch { get; set; }

            public long FK_Department { get; set; }
            // public string UserCode { get; set; }
        }
        public class EmployeeLeadInfo
        {
            public long ID_Employee { get; set; }
            public string EmpCode { get; set; }
            public string EmpFName { get; set; }
            public string EmpLName { get; set; }
            public long ID_Branch { get; set; }
            public long ID_BranchMode { get; set; }
            public long ID_BranchType { get; set; }
            public long FK_Department { get; set; }
            public string BTName { get; set; }
            public string BrName { get; set; }
            public string DeptName { get; set; }
            public long FK_BranchMode { get; set; }
            //public long FK_Priority { get; set; }
            //public string PriorityName { get; set; }




        }
        public class EmployeeName
        {          
            public string EmpFName { get; set; }

        }
        public class GetProduct
        {
            public long ID_Product { get; set; }

            public string ProdName { get; set; } 
            public string ProdShortName { get; set; }
           
            public string ProdHSNCode { get; set; }
            public decimal SodMRP { get; set; }
            public decimal SodSalPrice { get; set; }
        }
        public class ProdProjDTL
        {
            public long ID_Product { get; set; }
            
            public long FK_Category { get; set; }
            
            public string ProdName { get; set; }
        //   public long ProjectName { get; set; }
            public string ProjectName { get; set; }
            public string AssignEmp { get; set; }
            public decimal LgpPQuantity { get; set; }

            public string LgpDescription { get; set; }
            public int ActStatus { get; set; }
            public string Status { get; set; } 
            public long FK_NetAction { get; set; }

            public long BranchID { get; set; }
            public long BranchTypeID { get; set; }
            public long FK_ActionType { get; set; }
            public DateTime? NextActionDate { get; set; }
            public long FK_Departement { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Priority { get; set; }
          
           
            public string EntrBy { get; set; }

            public string ActnTypeName { get; set; }
            public string CatName{ get; set; }
            public string NxtActnName { get; set; }

            public string PriorityName { get; set; }
            public DateTime? LgpExpectDate { get; set; }

            public decimal LgpMRP { get; set; }
            public decimal LgpSalesPrice { get; set; }
            public long FK_ProductLocation { get; set; } = 0;

        }
        public class UpdateLeadGenerate
        {
            public byte UserAction { get; set; }
            public Int16 Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_LeadGenerate { get; set; }
            //public string LgLeadNo { get; set; }
            public DateTime LgLeadDate { get; set; }
            public long FK_Customer { get; set; }
            public long FK_SubMedia { get; set; }
            public long FK_CustomerOthers { get; set; }
            public string LgCusNameTitle { get; set; }
            public string LgCusName { get; set; }
            public string LgCusAddress { get; set; }
            public string LgCusAddress2 { get; set; }
            public string LgCusMobile { get; set; }
            public string LgCusEmail { get; set; }
            public string CusCompany { get; set; }
            public string CusPhone { get; set; }
            public long FK_Country { get; set; }
            public long FK_State { get; set; }
            public long FK_District { get; set; }
            public long FK_Post { get; set; }
            public long FK_Area { get; set; }
            public Int64? LastID { get; set; }

            public long FK_LeadFrom { get; set; }
            
            public long FK_LeadBy { get; set; }
            public string LeadByName { get; set; }
            public long FK_MediaMaster { get; set; }
          
            public long LgCollectedBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
          
            
            public string EntrBy { get; set; }
            
            
            
            
            public long FK_Machine { get; set; }
         //   public long PreviousID { get; set; }
            public string SubProductDetails { get; set; }
            public long FK_LeadGenerate { get; set; }
            public string CusMobileAlternate { get; set; }
            public long FK_CustomerAssignment { get; set; }
            public long? FK_Quotation { get; set; } = 0;
            public string CusMobile1 { get; set; }
            public string ImageList { get; set; }
        }
        public static string _deleteProcedureName = "proLeadGenerateDelete";
        public static string _updateProcedureName = "ProLeadGenerateUpdate";
        public static string _selectProcedureName = "proLeadGenerateSelect";

        public class DeleteLeadGenerate
        {
            public long ID_LeadGenerate { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int64 FK_Reason { get; set; }
            public Int64 FK_Company { get; set; }
        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class LeadGenerateID
        {
            public Int64 ID_LeadGenerate { get; set; }
        }
        public class GetLeadGen
        {
            public Int64 ID_LeadGenerate { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }

            public string Name { get; set; }
        }
       
        public Output UpdateLeadGenerateData(UpdateLeadGenerate input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLeadGenerate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteLeadGenerateData(DeleteLeadGenerate input, string companyKey)
        {
            return Common.UpdateTableData<DeleteLeadGenerate>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<LeadGenerateView> GeLeadGenerateData(GetLeadGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadGenerateView, GetLeadGen>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ProdProjDTL> GetSubProduct(GetLeadGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProdProjDTL, GetLeadGen>(companyKey: companyKey, procedureName: "ProLeadGenerateProductFill", parameter: input);

        }
        public APIGetRecordsDynamic<ProdProjDTL> GetSubProductSite(GetLeadGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProdProjDTL, GetLeadGen>(companyKey: companyKey, procedureName: "ProGetLeadDetailsForSiteVisit", parameter: input);

        }

        public APIGetRecordsDynamicdn<dynamic> GetLeadTrough(GetLeadBy input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<GetLeadBy>(companyKey: companyKey, procedureName: "proERPCmnSelect", parameter: input);

        }

        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public APIGetRecordsDynamic<ListData> GetModeList(GetModeData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ListData, GetModeData>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public class Menulist
        {
            public string Transmode { get; set; }
        }
        public class MRPEdit
        {
            public bool GsValue { get; set; }
            public string GsField { get; set; }
            
        }
       
    }
}