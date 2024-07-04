/*----------------------------------------------------------------------
Created By	: Riyas
Created On	: 03/08/2022
Purpose		: Quotation
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
    public class QuotationModel
    {
        public static string _deleteProcedureName = "ProQuotationDelete";
        public static string _updateProcedureName = "ProQuotationUpdate";
        public static string _selectProcedureName = "ProQuotationSelect";

        public class QuotationViewInit
        {
            public string QuoMode { get; set; }
            public List<Category> CategoryList { get; set; }
            public List<WarrantyTypeModel.WarrantyTypeView> Warrantytype { get; set; }
            public List<PaymentMethodModel.PaymentMethodView> PaymentView { get; set; }
        }
        public class Category
        {
            public long ID_Catg { get; set; }
            public string CatgName { get; set; }
            public bool Project { get; set; }
            public string Mode { get; set; }
        }
        public class GetProduct
        {
            public long ID_Product { get; set; }
            public string ProdName { get; set; }
            public string ProdShortName { get; set; }
            public decimal MRP { get; set; }
            public decimal Rate { get; set; }
            public long FK_Category { get; set; }
        }
        public class QuotationViewList
        {
            public int SlNo { get; set; }
            public long ID_Quotation { get; set; }
            public string QuoNO { get; set; }
            public string QuoGenNO { get; set; }
            public DateTime QuoDate { get; set; }
           
            public string Contact_Name { get; set; }
            public string Contact_Mob { get; set; }
            public string QuoNetAmount { get; set; }
            public Int64 TotalCount { get; set; }
            public long QuoMode { get; set; }
            public Int64? LastID { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
       public class QuotationViewUpdate
        {           
            public long ID_Quotation { get; set; }
            public long QuoMode { get; set; }
            public int QuoFrom { get; set; }
            public int FK_Master { get; set; }
            public string QuoNO { get; set; }
            public DateTime QuoDate { get; set; }
            public DateTime? QuoExpireDate { get; set; }
            public decimal QuoBillTotal { get; set; }
            public decimal QuoDiscount { get; set; }
            public decimal QuoOthercharges { get; set; }
            public decimal QuoRoundoff { get; set; }
            public decimal QuoNetAmount { get; set; }
            public string ContactName { get; set; }
            public string ContactMobile { get; set; }
            public string ContactAddress { get; set; }
            public int FK_QuotationGen { get; set; }
            public string QuotationGenNo { get; set; }
            public string QuoRemarks { get; set; }
            public string QuoReferenceNo { get; set; }
            public DateTime QuoEntrDate { get; set; }
        }
        public class QuotationView
        {
            [Required(ErrorMessage = "Please Enter Quo Mode")]
            public string QuoMode { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Master")]
            public long Master { get; set; }
            [Required(ErrorMessage = "Please Enter Quo N O")]
            public string QuoNO { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Date")]
            public DateTime QuoDate { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Expire Date")]
            public DateTime QuoExpireDate { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Bill Total")]
            public decimal QuoBillTotal { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Discount")]
            public decimal QuoDiscount { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Othercharges")]
            public decimal QuoOthercharges { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Roundoff")]
            public decimal QuoRoundoff { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Net Amount")]
            public decimal QuoNetAmount { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Actual Quantity")]
            public decimal QuoActualQuantity { get; set; }
            [Required(ErrorMessage = "Please Enter Quo Remarks")]
            public string QuoRemarks { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Unit")]
            public long Unit { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Branch")]
            public long Branch { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Department")]
            public long Department { get; set; }
            public Int64 TotalCount { get; set; }
            public int SortOrder { get; set; }
            public List<ModuleType> ModuleTypeList { get; set; }
        }
        public class QuotationNew
        {
            public long ID_Quotation { get; set; }
            public string TransMode { get; set; }
            public int QuoMode { get; set; }
            public int QuoFrom { get; set; }
            public long FK_Master { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string QuoNO { get; set; }
            public long FK_QuotationGen { get; set; }
            public DateTime QuoDate { get; set; }
           
            public DateTime? QuoExpireDate { get; set; }
           
            public decimal QuoBillTotal { get; set; }
         
            public decimal QuoDiscount { get; set; }
       
            public decimal QuoOthercharges { get; set; }
           
            public decimal QuoRoundoff { get; set; }
           
            public decimal QuoNetAmount { get; set; }
            public List<QuotationProductDetailsView> QuotationDetail { get; set; }
            public List<TaxTypeDet> TaxDetails { get; set; }
            public List<OtherCharges> OtherChgDetails { get; set; }
            public List<WarrantyDetails> WarrantyDetails { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerAddress { get; set; }
            public string QuoRemarks { get; set; }
            public string QuoReferenceNo { get; set; }
            public Int64? LastID { get; set; }
            public DateTime? QuoEntrDate { get; set; }
            public List<QuotationCriteriaDetails> QuotationCriteriaDetails { get; set; }
        }
        public class WarrantyDetails
        {
            public long SlNo { get; set; }
            public Int32 prodtid { get; set; }
            public Int32 subProductID { get; set; }
            public long WarrantyType { get; set; }
            public DateTime? Replcwardt { get; set; } 
            public DateTime? Serwardt { get; set; } 
            public Int32 WDDurationType { get; set; } = 0;
            public Int32 WDDuration { get; set; } = 0;
        }
      
        public class WarrantyListCriteria
        {
            public string TransMode { get; set; }
            public long FK_Master { get; set; }
            public long FK_Product { get; set; }
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
        }
        public class WarrantyDetailsList
        {
            public long SlNo { get; set; }
            public long Warrantyindex { get; set; }
            public string prodtid { get; set; }
            public string subProName { get; set; }
            public string  subProductID { get; set; }
            public string WarrantyType { get; set; }
            public string WarrantyType_d { get; set; }
            public DateTime? Replcwardt { get; set; }
            public DateTime? Serwardt { get; set; }
            public Int32 WDDurationType { get; set; }
            public Int32 WDDuration { get; set; }

        }
        public class QuotationProductDetailsView
        {
            public long ID_QuotationProductDetails { get; set; }
            public long SlNo { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public string QpdQuantity { get; set; }
            
              
            public string QpdMRP { get; set; }
            public string QpdRate { get; set; }
            public string Sprice { get; set; }
            public string QpdDiscount { get; set; }
            public string QpdDiscountPercent { get; set; }
            public string ProdName { get; set; }
            public string CategoryName { get; set; }
            public string QpdTotalAmount { get; set; }
            
            public string QpdRWRemarks { get; set; }
            public string QpdSize { get; set; }
         



        }
        public class QuotationProductDetailsgridView
        {
            public long ID_QuotationProductDetails { get; set; }
            public long SlNo { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_Category { get; set; }
            public long FK_Product { get; set; }
            public string QpdQuantity { get; set; }
            public string QpdMRP { get; set; }
            public string QpdRate { get; set; }
            public string Sprice { get; set; }
            public string QpdDiscount { get; set; }
            public string QpdDiscountPercent { get; set; }
            public string ProdName { get; set; }
            public string CategoryName { get; set; }
            public string QpdTotalAmount { get; set; }

            public string QpdRWRemarks { get; set; }
            public string QpdSize { get; set; }
        }
        public class UpdateQuotation
        {         
            public int UserAction { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Quotation { get; set; }            
            public int QuoMode { get; set; }
            public int QuoFrom { get; set; }
            public long FK_Master { get; set; }
            public string QuoNO { get; set; }
            public DateTime QuoDate { get; set; }
            public DateTime? QuoExpireDate { get; set; }
            public decimal QuoBillTotal { get; set; }
            public decimal QuoDiscount { get; set; }
            public decimal QuoOthercharges { get; set; }
            public decimal QuoRoundoff { get; set; }
            public decimal QuoNetAmount { get; set; }
            public string QuoRemarks { get; set; }           
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }          
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string QuotationDetails { get; set; }
            public string TaxDetails { get; set; }
            public string OtherChargeDetails { get; set; }
            public string WarrantyDetails { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerAddress { get; set; }
            public long FK_Quotation { get; set; }
            public long FK_QuotationGen { get; set; }
            public string QuoReferenceNo { get; set; }
            public Int64? LastID { get; set; } = 0;
            public DateTime? QuoEntrDate { get; set; }
            public string QuotationCriteriaDetails { get; set; }
        }
       
        public class GetQuotation
        {
            public Int64 ID_Quotation { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public long QuoMode { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }
        public class DeleteQuotation
        {
            public long ID_Quotation { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int FK_Company { get; set; }
            public int FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public int QuoMode { get; set; }
        }
        public class ModuleType
        {
            public long ID_ModuleType { get; set; }
            public string ModuleName { get; set; }
            public string Mode { get; set; }
        }
        public class QuotationGetInfo
        {
            public Int64 ID_Quotation { get; set; }
            public long QuoMode { get; set; }
            public string TransMode { get; set; }
        }
        public class QuotationID
        {
            public Int64 ID_Quotation { get; set; }
        }
        public class QuotationSelectDetails
        {
            public Int64 ID_Quotation { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public long QuoMode { get; set; }
        }
        public class QuotationItemDetails
        {
            public Int64 ID_Quotation { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long QuoMode { get; set; }

        }
        public class OutputGetQuotationNo
        {
            public string QuotationNumber { get; set; }
        }
        public class InputGetQuotationNo
        {
            public long FK_Company { get; set; }
        }
        public class TaxTypeDet
        {
            public long SlNo { get; set; }
            public long FK_TaxType { get; set; }

            public string TaxtyName { get; set; }

            public decimal TaxPercentage { get; set; }
            public decimal TaxAmount { get; set; }
            public long ProductID { get; set; }
        }
        public class OtherCharges
        {
            public long SlNo { get; set; }
            public long ID_OtherChargeType { get; set; }
            public Int64 OctyTransType { get; set; }
            public string OctyName { get; set; }
            public decimal OctyAmount { get; set; }
            public string TransType { get; set; }
            public Int64 TransTypeID { get; set; }
        }
        public class BindOtherCharge
        {
            public Int32 Mode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class GetOtherCharge
        {
            public long Mode { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Transaction { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class GetQuotationPopup {
            public Int64 ID_Quotation { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 QuoMode { get; set; }
        }
        public class SetQuotationPopup
        {
            public Int64 ID_Quotation { get; set; }
            public Int64 SlNo { get; set; }
            public string QuotationNo { get; set; }
            public string Date { get; set; }           
            public string ContactName { get; set; }
            public string MobileNo { get; set; }
            public decimal NetAmount { get; set; }
            public Int64 TotalCount { get; set; }
        }
        public class QuotationGenView
        {
            public int SlNo { get; set; }
            public long ID_QuotationGen { get; set; }
            public string QuoNO { get; set; }           
            public string QuoDate { get; set; }
            public string QuoExpireDate { get; set; }
            public string QuoTerms { get; set; }
        }
        public class GetQuotationGen
        {
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_Branch { get; set; }
            public string TransMode { get; set; }

        }
        public class GetQuotationComparison
        {
            public Int64 FK_QuotationGen { get; set; }          
            public Int64 FK_Company { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public bool IsNetAmount { get; set; }
            public bool IsBillAmount { get; set; }
            public bool IsOtherAmount { get; set; }
            public bool IsDiscountAmount { get; set; }
        }
        public class QuotationComparisonList
        {
            public long ID_Quotation { get; set; }
            public string QuoNO { get; set; }          
            public string QuoDate { get; set; }
            public string QuoExpireDate { get; set; }
            public decimal QuoBillTotal { get; set; }
            public decimal QuoDiscount { get; set; }
            public decimal QuoOthercharges { get; set; }
            public decimal QuoNetAmount { get; set; }
            public string QuoRemarks { get; set; }
            public string QuoReferenceNo { get; set; }
            public string Contact_Name { get; set; }
            public string Contact_Mob { get; set; }
            public bool Accepted { get; set; }
            public long FK_QuotationGen { get; set; }
            public int RankData { get; set; }
        }
        public class QuotationComparisonData
        {
            public long ID_Quotation { get; set; }
            public bool Approved { get; set; }
            public string TransMode { get; set; }
        }
        public class UpdateQuotationComparison
        {
            public int UserAction { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }
            public long ID_Quotation { get; set; }
            public bool Accepted { get; set; }
            public int QuoMode { get; set; }          
            public long FK_Company { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }           
            public long FK_Quotation { get; set; }         
        }
        public class Leadfill
        {
            public int IsLead { get; set; }
            public long FK_Master { get; set; }
        }
        public class QuotationCriteriaDetails
        {
            public long SlNo { get; set; }
            public long FK_Header { get; set; }
            public string Header { get; set; }
            public string QuoCriteria { get; set; }
        }
        public APIGetRecordsDynamic<SetQuotationPopup> GetQuotationPopupData(GetQuotationPopup input, string companyKey)
        {
            return Common.GetDataViaProcedure<SetQuotationPopup, GetQuotationPopup>(companyKey: companyKey, procedureName: "ProQuotationList", parameter: input);
        }
        public Output UpdateQuotationData(UpdateQuotation input, string companyKey)
        {
            return Common.UpdateTableData<UpdateQuotation>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteQuotationData(DeleteQuotation input, string companyKey)
        {
            return Common.UpdateTableData<DeleteQuotation>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<QuotationViewList> GetQuotationData(GetQuotation input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationViewList, GetQuotation>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<SampleT.OutputData> GetQuotationDataSample(SampleT.InputData input, string companyKey)
        {
            return Common.GetDataViaProcedure<SampleT.OutputData, SampleT.InputData> (companyKey: companyKey, procedureName: "SelectTProductList", parameter: input);
        }
        public APIGetRecordsDynamic<QuotationViewUpdate> GetQuotationSelectDetails(QuotationSelectDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationViewUpdate, QuotationSelectDetails>(companyKey: companyKey, procedureName: "ProQuotationSelectDetails", parameter: input);
        }
        public APIGetRecordsDynamic<QuotationProductDetailsgridView> GetQuotationItemDetailsSelect(QuotationItemDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationProductDetailsgridView, QuotationItemDetails>(companyKey: companyKey, procedureName: "ProQuotationItemDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<WarrantyDetailsList> GetQuotationWarrantySelect(WarrantyListCriteria input, string companyKey)
        {
            return Common.GetDataViaProcedure<WarrantyDetailsList, WarrantyListCriteria>(companyKey: companyKey, procedureName: "ProQuotationWarrantySelect", parameter: input);
        }
        public APIGetRecordsDynamic<OutputGetQuotationNo> GetQuotationNo(InputGetQuotationNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<OutputGetQuotationNo, InputGetQuotationNo>(companyKey: companyKey, procedureName: "ProGetQuotationNo", parameter: input);
        }
        public APIGetRecordsDynamic<OtherCharges> FillOtherCharges(BindOtherCharge input, string companyKey)
        {
            return Common.GetDataViaProcedure<OtherCharges, BindOtherCharge>(companyKey: companyKey, procedureName: "ProOtherChargeSelectForQuotation", parameter: input);

        }
        public APIGetRecordsDynamic<OtherCharges> GetOthrChargeDetails(GetOtherCharge input, string companyKey)
        {
            //return Common.GetDataViaProcedure<OtherCharges, GetOtherCharge>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelect", parameter: input);
            return Common.GetDataViaProcedure<OtherCharges, GetOtherCharge>(companyKey: companyKey, procedureName: "ProOthChgTransactionDetailsSelectForQuoation", parameter: input);
            
        }
        public APIGetRecordsDynamic<QuotationProductDetailsView> GetQuotationGenInwardItemDetailsSelect(QuotationItemDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationProductDetailsView, QuotationItemDetails>(companyKey: companyKey, procedureName: "ProQuotationGenInItemDetailsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<QuotationGenView> GetQuotationGenSelectForInwardQuotation(GetQuotationGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationGenView, GetQuotationGen>(companyKey: companyKey, procedureName: "ProQuotationGenSelectForInwardQuotation", parameter: input);
        }
        public APIGetRecordsDynamic<QuotationGenView> GetQuotationGenSelectForComparison(GetQuotationGen input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationGenView, GetQuotationGen>(companyKey: companyKey, procedureName: "ProQuotationGenSelectForComparison", parameter: input);
        }

        public APIGetRecordsDynamic<QuotationComparisonList> GetQuotationSelectByQuotationGen(GetQuotationComparison input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationComparisonList, GetQuotationComparison>(companyKey: companyKey, procedureName: "ProQuotationSelectByQuotationGen", parameter: input);
        }
        public Output UpdateQuotationComparisonData(UpdateQuotationComparison input, string companyKey)
        {
            return Common.UpdateTableData<UpdateQuotationComparison>(parameter: input, companyKey: companyKey, procedureName: "ProQuotationApprovedUpdate");
        }

        public APIGetRecordsDynamic<SalesOrderModel.SalesOrderProductDetails> GetSalesOrderItemDetailsSelectForSalesOrderImport(QuotationItemDetails input, string companyKey)
        {
            return Common.GetDataViaProcedure<SalesOrderModel.SalesOrderProductDetails, QuotationItemDetails>(companyKey: companyKey, procedureName: "ProQuotationItemDetailsSelectForSalesOrderImport", parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> Fillead(Leadfill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<Leadfill>(companyKey: companyKey, procedureName: "ProQuotationDataFill", parameter: input);

        }

        public class Menulist
        {
            public string Transmode { get; set; }
        }

        public class LeadCustomerData
        {
            public long CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string Mobile { get; set; }
        }
        public class QuotationCriteriaView
        {
            public long SlNo { get; set; }
            public long FK_Header { get; set; }
            public string Header { get; set; }
            public string Criteria { get; set; }
        }
        public class QuotationCriteriaByID
        {
            public long FK_Master { get; set; }
            public long Mode { get; set; }

        }
        public class QuotationCriteriaSelectView
        {
            public long SlNo { get; set; }
            public long FK_Header { get; set; }
            public string Header { get; set; }
            public string Criteria { get; set; }
        }
        public class GetQuotationCriteriaByID
        {
            public long ID_Quotation { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public string EntrBy { get; set; }
        }
        public class GetTemplateIp
        {
            public string TransMode { get; set; }
            public string ReportType { get; set; }
            public Int64 FK_Master { get; set; }
        }
        public class GetTemplateCommonPrintOp
        {

            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string PageSize { get; set; }

        }
        public APIGetRecordsDynamic<QuotationCriteriaView> GetQuotationCriteriaDetails(QuotationCriteriaByID input, string companyKey)
        {
            return Common.GetDataViaProcedure<QuotationCriteriaView, QuotationCriteriaByID>(companyKey: companyKey, procedureName: "ProQuoCriteriaTabSelect", parameter: input);
        }

        public APIGetRecordsDynamic<QuotationCriteriaSelectView> GetQuotationCriteriaDetailsSelect(GetQuotationCriteriaByID input, string companyKey)
        {            
            return Common.GetDataViaProcedure<QuotationCriteriaSelectView, GetQuotationCriteriaByID>(companyKey: companyKey, procedureName: "ProQuotationCriteriaDetailsSelect", parameter: input);
        }

        public APIGetRecordsDynamic<Getpritreportoutput1> Getpritreportdata1(Getpritreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Getpritreportoutput1, Getpritreportinput>(companyKey: companyKey, procedureName: "ProPrintRepotdata", parameter: input);

        }
        public APIGetRecordsDynamic<Getpritreportoutput2> Getpritreportdata2(Getpritreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Getpritreportoutput2, Getpritreportinput>(companyKey: companyKey, procedureName: "ProPrintRepotdata", parameter: input);

        }
        public APIGetRecordsDynamic<Getpritreportoutput3> Getpritreportdata3(Getpritreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Getpritreportoutput3, Getpritreportinput>(companyKey: companyKey, procedureName: "ProPrintRepotdata", parameter: input);

        }

        public APIGetRecordsDynamic<GetCompritreportoutput1> GetCompritreportdata1(GetCommPritreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetCompritreportoutput1, GetCommPritreportinput>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);

        }
        public APIGetRecordsDynamic<GetCompritreportoutput2> GetCompritreportdata2(GetCommPritreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetCompritreportoutput2, GetCommPritreportinput>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);

        }
        public APIGetRecordsDynamic<GetCompritreportoutput3> GetCompritreportdata3(GetCommPritreportinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetCompritreportoutput3, GetCommPritreportinput>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);

        }


        public class Getpritreportinput
        {
            public int Mode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Quotation { get; set; }
        }

        public class GetCommPritreportinput
        {
            public Int64 FK_Master { get; set; }
            public int TableCount { get; set; }
            public string TransMode { get; set; }
            //public int Mode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Branch { get; set; }
        }
        public class QuatationNumber
        {
            public Int64 QuatationNum { get; set; }
            public string TransMode { get; set; }

        }
        public class Getpritreportoutput1
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public string email { get; set; }
            public string Quotation_refrence { get; set; }
            public DateTime Date { get; set; }
            public string Mobile { get; set; }
        }
        public class Getpritreportoutput2
        {
            public string FINAL_AMOUNT_AFTER_DISCOUNT { get; set; }
            public string TOTAL_AMOUNT { get; set; }

        }
        public class Getpritreportoutput3
        {
            public string Product { get; set; }
            public string Description1 { get; set; }
            public string Size { get; set; }
            public float Price { get; set; }
            public float Qty { get; set; }
            public string Criteria { get; set; }
            public string Heading { get; set; }
        }

        public class GetCompritreportoutput1
        {//PCustomerName	Date	PAddress1	PMobile	PEMail	InvoiceNo
            public string PCustomerName { get; set; }
            public string PAddress1 { get; set; }
            public string PEMail { get; set; }
            public string InvoiceNo { get; set; }
            public string Date { get; set; }
            public string PMobile { get; set; }
            public string logoImage { get; set; }

        }
        
        public class GetCompritreportoutput2
        {
            public string SlNo { get; set; }
            public string Product { get; set; }
            public string ProductName { get; set; }

            public string Description1 { get; set; }
            public string Size { get; set; }
            public string SalesPrice { get; set; }
            public string Quantity { get; set; }
            public string Criteria { get; set; }
            public string Heading { get; set; }
        }
        public class GetCompritreportoutput3
        {
            public string TotalAmount { get; set; }
            public string Finalamount { get; set; }


        }
        public class BindOtherCharges
        {
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }

        }
    }
}