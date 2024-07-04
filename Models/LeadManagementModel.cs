/*----------------------------------------------------------------------
Created By	: Haseena K
Created On	: 03/02/2022
Purpose		: LeadManagement
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
    public class LeadManagementModel
    {
        public class LeadManagementView
        {
            [Required(ErrorMessage = "Please Enter Lg Lead No")]
            public string LgLeadNo { get; set; }
            [Required(ErrorMessage = "Please Enter Lg Lead Date")]
            public DateTime LgLeadDate { get; set; }
            [Required(ErrorMessage = "Please Enter Lg Cus Name")]
            public string LgCusName { get; set; }
            [Required(ErrorMessage = "Please Enter Lg Cus Address")]
            public string LgCusAddress { get; set; }
            [Required(ErrorMessage = "Please Enter Lg Cus Mobile")]
            public string LgCusMobile { get; set; }
            [Required(ErrorMessage = "Please Enter Lg Cus Email")]
            public string LgCusEmail { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead From")]
            public long LeadFrom { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Lead By")]
            public long LeadBy { get; set; }
            [Required(ErrorMessage = "Please Enter Lead By Name")]
            public string LeadByName { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long Branch { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Dealer")]
            public long Dealer { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Franchise")]
            public long Franchise { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Extension Counter")]
            public long ExtensionCounter { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Employee")]
            public long Employee { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Customer")]
            public long Customer { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Third Party")]
            public long ThirdParty { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Freelancer")]
            public long Freelancer { get; set; }
            [Required(ErrorMessage = "Please Enter Lg Collected By")]
            public long LgCollectedBy { get; set; }
            public long LeadManagementID { get; set; }
           public long ReasonID { get; set; }
           public long FK_ActionStatus { get; set; }

        }
        public class LeadFrom
        {
            public Int32 ID_LeadFrom { get; set; }
            public string LeadFromName { get; set; }
        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class LeadManagementViewList
        {
            public List<LeadGenerateModel.LeadFrom> LeadFromList { get; set; }
            public List<LeadGenerateModel.NextAction> NextActionList { get; set; }
            public List<ActionStatus> ActionStatusList { get; set; }
            public List<LeadGenerateModel.EmployeeInfo> EmployeeInfoList { get; set; }
            public long FK_Employee { get; set; }
            public string UserCode { get; set; }
            public long Branch { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<ActionTypes> Actntyplists { get; set; }
            public List<NextAction> NxtActionList { get; set; }
            public string BrName { get; set; }
            public string CompName { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
        }
        public class ActionTypes
        {
            public int ID_ActionType { get; set; }
            public string ActnTypeName { get; set; }
        }
        public class Branchdetails
        {
            public int ID_Branch { get; set; }
            public string BrName { get; set; }
        }
        public class Companydetails
        {
            public int FK_Company { get; set; }
            public string CompName { get; set; }
        }
        public class NextAction
        {
            public long ID_NextAction { get; set; }
            public string NxtActnName { get; set; }
        }
        public class UpdateLeadManagement
        {
            public long ID_LeadManagement { get; set; }
            public string LgLeadNo { get; set; }
            public DateTime LgLeadDate { get; set; }
            public string LgCusName { get; set; }
            public string LgCusAddress { get; set; }
            public string LgCusMobile { get; set; }
            public string LgCusEmail { get; set; }
            public long FK_LeadFrom { get; set; }
            public long FK_LeadBy { get; set; }
            public string LeadByName { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Dealer { get; set; }
            public long FK_Franchise { get; set; }
            public long FK_ExtensionCounter { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Customer { get; set; }
            public long FK_ThirdParty { get; set; }
            public long FK_Freelancer { get; set; }
            public long LgCollectedBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public bool Passed { get; set; }
            public bool Cancelled { get; set; }
            public string EntrBy { get; set; }
            public DateTime EntrOn { get; set; }
            public string CancelBy { get; set; }
            public DateTime CancelOn { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public long BackupId { get; set; }
        }
        public class GetLeadGen
        {
            public string TransMode { get; set; }
            public long FK_LeadFrom { get; set; }
            
            public long FK_LeadBy { get; set; }
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public long FK_Product { get; set; }
            public string ProjectName { get; set; }           
            public long Status { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Category { get; set; }
            public long Collectedby_ID { get; set; }
            public long Area_ID { get; set; }
            public long FK_NetAction { get; set; }
            public long FK_ActionType { get; set; }
            public long FK_Priority { get; set; }
            public long ProductType { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 PageIndex  { get; set; }
        public Int32 PageSize   { get; set; }
            public byte Filterid { get; set; }
            public Int64 SearchBy { get; set; }
            public string SearchBydetails { get; set; }
            public DateTime? ExpectedFrom { get; set; } = null;
            public DateTime? ExpectedTo { get; set; } = null;

        }
        public class LeadGenerateDetails
        {
            public long SlNo { get; set; }
            public long ID_LeadGenerateProduct { get; set; }
            public string Preference { get; set; }
            public string LeadNo { get; set; }
            public DateTime LgLeadDate { get; set; }
            public string LgCusName { get; set; }
            public string Area { get; set; }
            public string LgCusMobile { get; set; }
            public string FK_Product { get; set; }
            public string ProdName { get; set; }         
            public long ProductType { get; set; }
            public DateTime NextActionDate { get; set; }
            public string DueDays { get; set; }
            public string LgpDescription { get; set; }        
            public string PLgpMRP { get; set; }
            public string PLgpOfferPrice { get; set; }
            // public string FK_Employee { get; set; }
            public string LgCollectedBy { get; set; }
            public DateTime ?LgpExpectDate { get; set; }
            public string Field { get; set; }
            public int Value { get; set; }
            public int Masterid { get; set; }                   
           
            public string AssignedTo { get; set; }
         
            public Int64 TotalCount { get; set; }
            public string Customer { get; set; }
            public string Mobile { get; set; }
            public string Employee { get; set; }
            public string DESCRIPTION{ get; set; }
            public DateTime ?AssignedDate { get; set; }
            public long ID_LeadGenerate { get; set; }
            public long FK_Employee { get; set; }
            public long ID_CustomerAssignment { get; set; }
            public long? ID_Product { get; set; } = 0;
            public long? FK_Category { get; set; } = 0;
            public string CusEmail { get; set; }
            public long ?LastID { get; set; }
            public string Remarks { get; set; }
            public string VoiceName { get; set; } = "";
            public long ID_VoiceInfo { get; set; } 

        }


        public class LeadGenerateRequestDetails
        {
            public long SlNo { get; set; }
           
            public string LgCusName { get; set; }
            public string LgCusMobile { get; set; }
            public Int64 TotalCount { get; set; }

           
        }
        public class GetLeadRequestinput
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
           
            public Int64 FK_Employee { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
           

        }
        public static string _deleteProcedureName = "ProLeadManagementDelete";
        public static string _updateProcedureName = "ProLeadManagementUpdate";
        public static string _selectProcedureName = "ProLeadManagementSelect";
        public static string _SearchProcedureName = "ProLeadGenerateSearchSelect";

        public class DeleteLeadManagement
        {
            public long ID_LeadManagement { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_Reason { get; set; }
            public DateTime CancelOn { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public class EmployeeInfo
        {
            public long ID_Employee { get; set; }
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
          
            public long FK_Branch { get; set; }
            public long FK_BranchType { get; set; }
        }

        public class GetProduct
        {
            public long ID_Product { get; set; }

            public string ProductName { get; set; }
            public string ShortName { get; set; }

            public string ProductHSNCode { get; set; }
        }

        public class LeadManagementID
        {
            public Int64 ID_LeadManagement { get; set; }
        }
        public class ViewLeadmanagementAssigne
        {
            public long LeadNo { get; set; }
            public long ID_LeadGenerateProduct { get; set; }
            public DateTime NextActionDate { get; set; }
            public long FK_AssignedTo { get; set; }
            public long FK_Product { get; set; }
          
            public string PLgpOfferPrice { get; set; }

        }
        public class InputLeadmanagementAssigne
        {
            public long ID_LeadGenerateProduct { get; set; }
            public DateTime NextActionDate { get; set; }
            public long FK_AssignedTo { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
          
            public string PLgpOfferPrice { get; set; }
        }
        public class GetVoiceDataDetails
        {
            public long FK_Company { get; set; }
            public long FK_CustomerAssignment { get; set; }
        }
        public class VoiceDataDetails
        {
            public string VoiceName { get; set; }
            public byte[] VoiceData { get; set; }
        }

        public APIGetRecordsDynamic<LeadGenerateDetails> GeLeadGenerateData(GetLeadGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadGenerateDetails, GetLeadGen>(companyKey: companyKey, procedureName: _SearchProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ActionStatus> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
        public Output UpdateLeadManagementData(UpdateLeadManagement input, string companyKey)
        {
            return Common.UpdateTableData<UpdateLeadManagement>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteLeadManagementData(DeleteLeadManagement input, string companyKey)
        {
            return Common.UpdateTableData<DeleteLeadManagement>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<LeadManagementView> GetLeadManagementData(LeadManagementID input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadManagementView, LeadManagementID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<LeadGenerateRequestDetails> GetLeadGenerateRequestData(GetLeadRequestinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<LeadGenerateRequestDetails, GetLeadRequestinput>(companyKey: companyKey, procedureName: "", parameter: input);

        }
        public Output UpdateLeadManagementAssigne(InputLeadmanagementAssigne input, string companyKey)
        {
            return Common.UpdateTableData<InputLeadmanagementAssigne>(parameter: input, companyKey: companyKey, procedureName: "ProLeadManagementAssigneUpdate");
        }
        public APIGetRecordsDynamic<VoiceDataDetails> GetVoiceData(GetVoiceDataDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<VoiceDataDetails, GetVoiceDataDetails>(companyKey: companyKey, procedureName: "ProGetVoiceInfo", parameter: input);
        }

    }
}