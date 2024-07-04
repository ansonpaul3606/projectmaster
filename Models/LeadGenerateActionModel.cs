/*----------------------------------------------------------------------
Created By	: Haseena K
Created On	: 07/02/2022
Purpose		: LeadGenerateAction(Followup)
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class LeadGenerateActionModel
    {

        
        public class LeadGenerateActionView
        {
            public short LgActMode { get; set; }
            public short LgActStatus { get; set; }

            [Required(ErrorMessage = "Please Enter Act Date")]
            public DateTime? ActDate { get; set; }
           
            [Required(ErrorMessage = "Please Enter Act Cus Comment")]
            public string ActCusComment { get; set; }
            [Required(ErrorMessage = "Please Enter Act Lead Comment")]
            public string ActLeadComment { get; set; }
            public long FK_Product { get; set; }
            
            //public string LgActProjectName { get; set; }
            public short LgActNextAction { get; set; }
            [Required(ErrorMessage = "Please Enter Act Next Date")]
            public DateTime? ActNextDate { get; set; }
            [Required(ErrorMessage = "Please Enter Act Mode")]

            public long FK_Departements { get; set; }
            public long FK_Departement { get; set; }
            public long FK_Employee { get; set; }
            public byte ActMode { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead Generate")]
            public long LeadGenerate { get; set; }
             public List<ProdProjDTL> ProductDetails { get; set; }
            public long FK_FollowUpBy { get; set; }
            public List<NextActionDetails> NextActionDetailsList { get; set; }
            public long FK_LeadGenerateProduct { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
            public bool checkval { get; set; }
            public decimal MRP { get; set; }
            public decimal OfferPrice { get; set; }
            public long FK_ActionStatus { get; set; }
            public bool IsQuotation { get; set; }
            public DateTime? QuotationDate { get; set; }
            public long? LastID { get; set; }
        }


    
        public class NextActionDetails
        {
            public short FK_NetAction { get; set; }
            public DateTime? NextActionDate { get; set; }
            public long FK_Departement { get; set; }
            public long FK_Employee { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
        }


        public class LeadGenerateActionViewList
        {
            public List<LeadGenerateModel.NextAction> NextActionList { get; set; }
            public List<LeadManagementModel.ActionStatus> ActionStatusList { get; set; }
            public List<LeadGenerateModel.Departement> DepartementList { get; set; }
            public long FK_Employee { get; set; }
            public List<LeadGenerateModel.EmployeeInfo> EmployeeInfoList { get; set; }

            public List<LeadGenerateModel.LeadFrom> LeadFromList { get; set; }

            public List<ActioMode> ActioModeList { get; set; }
            public List<ActionType> ActioTypeList { get; set; }
            public string UserCode { get; set; }
            public List<Category> CategoryList { get; set; }
            public string MapKey { get; set; }
            public long? LastID { get; set; }
            public long FK_BranchType { get; set; }
            public long FK_Branch { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class ActioMode
        {
            public long ID_ActionMode { get; set; }
            public string ActionModeName { get; set; }
        }
        public class ActionType
        {
            public long ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }
        }
        public class LeadInfo
        {
            public long ID_LeadGenerateProduct { get; set; }
            public long FK_LeadGenerate { get; set; }
            public long FK_Product { get; set; }
            public string ProjectName { get; set; }
            public string ProdName { get; set; }
            public decimal LgpPQuantity { get; set; }
            public string LgpDescription { get; set; }
            public long? FK_NetAction { get; set; }
            public string NxtActnName { get; set; }
           
            public DateTime? NextActionDate { get; set; }
            public long FK_Departement { get; set; }
            public long FK_Employee { get; set; }
            public string AssignedTo { get; set; }
            public string LgCusName { get; set; }
            public string LgCusAddress { get; set; }
            public string LgCusMobile { get; set; }
            public string LgCusEmail { get; set; }
            public long FK_Priority { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public string CatName { get; set; }
            public string LgLeadNo { get; set; }
            public string LeadDate { get; set; }
            public string ActnTypeName { get; set; }
            public long FK_Netaction { get; set; }

            public long ?FK_Actiontype { get; set; }
            public string LgCollectedBy { get; set; }

           public string LeadFromName { get; set; }
            public string LeadSourceName { get; set; }
            public long ID_Users { get; set; }
            public long ID_LeadGenerate { get; set; }
            public long LgpMRP { get; set; }
            public long LgpSalesPrice { get; set; }
            public long FK_ActionStatus { get; set; }
            public long ?LastID { get; set; }
            public string  CusCompany { get; set; }
            public string LgCusContmobile { get; set; }


        }
        public class ProdProjDTL
        {
            public long ID_Product { get; set; }
            public long FK_Category { get; set; }
            public string ProdName { get; set; }
            public long ProjectName { get; set; }
            public decimal LgpPQuantity { get; set; }
            public string LgpDescription { get; set; }
            public long FK_NetAction { get; set; }
            public DateTime NextActionDate { get; set; }
            public long FK_Departement { get; set; }
            public long FK_EmployeeNew { get; set; }
            public long FK_Priority { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string EntrBy { get; set; }
            public long FK_BranchType { get; set; }
            public long FK_Branch { get; set; }
        }
        public class UpdateLeadGenerateAction
        {
            public byte UserAction { get; set; }
            public Int16 Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_LeadGenerateAction { get; set; }
            public short LgActMode { get; set; }
            public short LgActStatus { get; set; }
            public DateTime? LgActDate { get; set; }           
            public string LgActCusComment { get; set; }
            public string LgActLeadComment { get; set; }
            public long FK_Product { get; set; }
            //public string LgActProjectName { get; set; }
            public short LgActNextAction { get; set; }
            public DateTime? LgActNextDate { get; set; }
            public bool checkval { get; set; }

            public long FK_Departement { get; set; }
            
            public long FK_Employee { get; set; }
            //public byte LgActMode { get; set; }
            public long FK_LeadGenerate { get; set; }
            public long FK_LeadGenerateProduct { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }          
            public long FK_Machine { get; set; }
           public long FK_LeadGenerateAction { get; set; }
            public string SubProductDetails { get; set; }
            public long FK_FollowUpBy { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }

            //public decimal LgpMRP { get; set; }
            public decimal LgpSalesPrice { get; set; }

            public bool LgGenerateQuot { get; set; }        
            public DateTime? LgQuotExpiryDate { get; set; }
            public long? LastID { get; set; }

        }
        public static string _deleteProcedureName = "ProLeadGenerateActionDelete";
        public static string _updateProcedureName = "ProLeadGenerateActionUpdate";
        public static string _selectProcedureName = "ProLeadGenerateActionSelect";

        public class DeleteLeadGenerateAction
        {
            public long ID_LeadGenerateAction { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
        }
        public class LeadFollowup
        {
            public Int64 ID_LeadGenerateAction { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class LeadGenerateActionID
        {
            public Int64 ID_LeadGenerateAction { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
        }
        public class LeadGenerateProduct
        {
            public Int64 ID_LeadGenerateAction { get; set; }
            public Int64 FK_LeadGenerateProduct { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
        }
        public class LeadGenerateProductHistory
        {
            public string Product { get; set; }
            public string EnquiryAbout { get; set; }
            public string LgpDescription { get; set; }
            public string Action { get; set; }
            public string ActionType { get; set; }
            public string ActionDate { get; set; }
            public string Status { get; set; }
            public string CustomerReamrks { get; set; }
            public string Agentremarks { get; set; }
            public string FollowedBy { get; set; }

        }
        public class LeadGenerateHistory
        {
            public Int64 LeadGenerateProduct { get; set; }
            public bool PrductOnly { get; set; }
          
        }
        public class GetLeadGenerateLocation
        {
            public long FK_LeadGenerateProduct { get; set; }
        }
        public class LeadGenerateLocationList
        {
            public string LocLongitude { get; set; }
            public string LocLattitude { get; set; }
            public string LocLocationName { get; set; }
            public string LocLandMark1 { get; set; }
            public string LocLandMark2 { get; set; }

        }

      
        public Output UpdateLeadGenerateActionData(UpdateLeadGenerateAction input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLeadGenerateAction>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteLeadGenerateActionData(DeleteLeadGenerateAction input, string companyKey)
        {
            return Common.UpdateTableData<DeleteLeadGenerateAction>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<LeadInfo> GetLeadGenerateActionData(LeadFollowup input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadInfo, LeadFollowup>(companyKey: companyKey, procedureName: "ProLeadGenerateInfoSelect", parameter: input);
        }
        public APIGetRecordsDynamic<LeadInfo> GetLeadGenerateActionDetails(LeadGenerateActionID input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadInfo, LeadGenerateActionID>(companyKey: companyKey, procedureName: "ProLeadGenerateDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<LeadGenerateProductHistory> GetLeadGenerateActionHistory(LeadGenerateHistory input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadGenerateProductHistory, LeadGenerateHistory>(companyKey: companyKey, procedureName: "ProLeadHistorySelect", parameter: input);
        }
        public APIGetRecordsDynamic<LeadGenerateLocationList> GetLeadGenerateLocationData(GetLeadGenerateLocation input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadGenerateLocationList, GetLeadGenerateLocation>(companyKey: companyKey, procedureName: "ProLeadLocationandImagesSelect", parameter: input);
        }
    }
}

