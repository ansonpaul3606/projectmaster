/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 15/09/2022
Purpose		: ExtraWork
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/
using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PerfectWebERP.Models
{
    public class ExtraWorkModel
    {

        public class ExtraWorkView
        {
            public long SlNo { get; set; }
            public long ProjectWorkDetailsID { get; set; }
            public DateTime Date { get; set; }
         
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
            public long FK_WorkType { get; set; }
            public string ExtraWork { get; set; }
            public string Project { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
            public string Details { get; set; }
            public string Remarks { get; set; }
            public decimal Amount { get; set; }
            public DateTime ProjectDate { get; set; }
            public Int64? FK_Supplier { get; set; }
            public string TransMode { get; set; }
        }
        public class ExtraWorkInputFromViewModel
        {

            [Required(ErrorMessage = "No ExtraWork Selected")]
            public long ProjectWorkDetailsID { get; set; }

            public DateTime Date { get; set; }
          
            public Int64 TotalCount { get; set; }
            public long FK_Project { get; set; }
            public long FK_Stage { get; set; }
            public long FK_WorkType { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string FromTime { get; set; }
            public string ToTime { get; set; }
            public string Details { get; set; }
            public string Remarks { get; set; }
            public decimal Amount { get; set; }
            public Int64 Subcontractor { get; set; }            
            public List<PaymentDetails> PaymentDetail { get; set; }
            public string TransMode { get; set; }
            public Int16 OverWrite { get; set; }
        }
        public class GetPaymentin
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Master { get; set; }

        }
        public class PaymentDetails
        {
            public long SlNo { get; set; }
            public Int32 PaymentMethod { get; set; }
            public string Refno { get; set; }
            public decimal PAmount { get; set; }

        }
        public class ExtraWorkInfoView
        {
            [Required(ErrorMessage = "No ExtraWork Selected")]
            public Int64 ProjectWorkDetailsID { get; set; }
            [Required(ErrorMessage = "Please select the reason")]
            public Int64 ReasonID { get; set; }

        }
        public class ModeList
        {
            public long ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }
  
  

        public class WorkTypeList
        {
            public short WorkTypeID { get; set; }
            public string WorkType { get; set; }

        }

        public static string _updateProcedureName = "proProjectExtraWorkUpdate";
        public class UpdateExtraWork
        {
            public long FK_ProjectWorkDetails { get; set; }
            public long ID_ProjectWorkDetails { get; set; }
            public string TransMode { get; set; }
            public DateTime PWDetTransDate { get; set; }
            public long FK_Master { get; set; }
            public long FK_Stage { get; set; }
            public long FK_WorkType { get; set; }
            public DateTime PWDetFromDate { get; set; }
            public DateTime PWDetToDate { get; set; }
            public string PWDetFromTime { get; set; }
            public string PWDetToTime { get; set; }
            public string PWDetDetails { get; set; }
            public string PWDetRemarks { get; set; }
            public decimal PMDetAmount { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 UserAction { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Supplier { get; set; }
            public string PaymentDetail { get; set; }
            public Int16 OverWrite { get; set; }
        }

        public class SelectExtraWork
        {

            public long FK_ProjectWorkDetails { get; set; }
            public string PrStName { get; set; }
            public string PrStShortName { get; set; }
            public long FK_Category { get; set; }
            public long FK_SubCategory { get; set; }
            public Int32 SortOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public byte UserAction { get; set; }

            public Int64 FK_Machine { get; set; }
            public string UserCode { get; set; }
            public Int32 BranchCode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string AuditData { get; set; }
            public string SqlUpdateQuery { get; set; }
            public Int64 FK_Reason { get; set; }
            public string EntrBy { get; set; }
            public Int64 BackupId { get; set; }
            public bool Cancelled { get; set; }

        }
        public class StageList
        {
            public string Mode { get; set; }
            public long ProjectStagesID { get; set; }
            public string StageName { get; set; }
        }
        public class EmployeeList
        {
            public long EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }

        public class TeamList
        {
            public long ProjectID { get; set; }
            public long ID_ProjectTeam { get; set; }
            public string TeamName { get; set; }
        }

        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

       
      
        public class ExtraWorkListModel
        {

           
            public List<StageList> StageList { get; set; }
            public List<EmployeeList> EmployeeList { get; set; }
            public List<TeamList> TeamList { get; set; }
            public List<WorkTypeList> WorkTypeList { get; set; }
            public DateTime VisitDate { get; set; }
            public List<ModeList> ModeList { get; set; }
            public long FK_Employee { get; set; }
            public List<Popupview> PopUpList { get; set; }
            public List<BillTypeModel.BillTypeView> BillTypeListView { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }

        public static string _deleteProcedureName = "proProjectExtraWorkDelete";
        public class DeleteExtraWork
        {
            public Int64 FK_ProjectWorkDetails { get; set; }
            public string TransMode { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }

        }



        public class InputProjectWorkDetailsID
        {
            public Int64 FK_ProjectWorkDetails { get; set; }
            public Int64 FK_Company { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int CheckList { get; set; }
            public string Name { get; set; }
        }
        public class Popupinput
        {
            public Int64 Pagemode { get; set; }
            public Int64 ID { get; set; }
            public Int64 Critrea1 { get; set; }
            public Int64 Critrea2 { get; set; }
            public string  Critrea3 { get; set; }
            public string Critrea4 { get; set; }
            public Int64 FK_Company { get; set; }

        }
        public class Popupview
        {
            public string SuppName { get; set; }
            public Int64 ID_Supplier { get; set; }
        }




        public Output UpdateExtraWorkData(UpdateExtraWork input, string companyKey)
        {
            return Common.UpdateTableData<UpdateExtraWork>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteExtraWorkData(DeleteExtraWork input, string companyKey)
        {
            return Common.UpdateTableData<DeleteExtraWork>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }


        public static string _selectProcedureName = "proProjectExtraWorkSelect";
        public APIGetRecordsDynamic<ExtraWorkView> GetExtraWorkData(InputProjectWorkDetailsID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ExtraWorkView, InputProjectWorkDetailsID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<Popupview> GetSubcontractorpopupValue(Popupinput input, string companyKey)
         {
            return Common.GetDataViaProcedure<Popupview, Popupinput>(companyKey: companyKey, procedureName: "proERPCmnSearchPopup", parameter: input);

        }

        public APIGetRecordsDynamic<PaymentDetails> GetPaymentselect(GetPaymentin input, string companyKey)
        {
            return Common.GetDataViaProcedure<PaymentDetails, GetPaymentin>(companyKey: companyKey, procedureName: "ProTransactionDetailsSelect", parameter: input);

        }






    }
}