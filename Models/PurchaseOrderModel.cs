/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 23/07/2022
Purpose		: PurchaseOrder
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
    public class PurchaseOrderModel
    {

        public class PurchaseOrderView
        {
            public long SlNo { get; set; }
            public long PurchaseOrderID { get; set; }
           
            public long PurordNo { get; set; }
            public long Number { get; set; }
            public string PurordQReferenceNo { get; set; }
            public long PurordQReferenceNoID { get; set; }

           public long? QuotationID { get; set; }
            public DateTime PurordDate { get; set; }
            public string TransMode { get; set; }


            public Decimal PurordNetAmount { get; set; }
          
            public Decimal PurordAdvanceAmount { get; set; }
           
            public DateTime PurordEstiDeliveryDate { get; set; }
          
            public byte PurordDeliveryType { get; set; }
          
            public byte ? PoPurchased { get; set; }



            public long ? PurordQuotation { get; set; }
          
            public long SupplierID { get; set; }
            public String Supplier { get; set; }
            public String Mobile { get; set; }
            public long BranchID { get; set; }
          public long StockID { get; set; }
            public long DepartmentID { get; set; }
            public string PurordRemark { get; set; }
            public long PurordUnitID { get; set; }
            public string UnitName { get; set; }
            public Decimal PurordPrice { get; set; }
            public Decimal PurordTotalAmount { get; set; }
            public string PurordQuantity { get; set; }
            public long ID_Mode { get; set; }
            public String ModeName { get; set; }
            public Int64 TotalCount { get; set; }
            public string Name { get; set; }
            public List<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
            public decimal UnitCount { get; set; }
            public Int64 ?LastID { get; set; }
        }

        public class PurchaseOrderDetails
        {
            public long ID_POProduct { get; set; }
            public long ProductID { get; set; }
            public String Product { get; set; }
            public Decimal PurordQuantity { get; set; }
            public Decimal PurordActQuantity { get; set; }
            public long PurordUnitID { get; set; }
            public String UnitName { get; set; }
            public Decimal PurordPrice { get;set; }
            //  public Decimal Total { get; set; }     
            public long StockID { get; set; }
        }
        public class PurchaseorderViewList
        {
            public long BranchID { get; set; }
            public long PurordNo { get; set; }
            public long Number { get; set; }
            public long DepartmentID { get; set; }
            public Int64? LastID { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<Unit> UnitList { get; set; }
            public List<DeliveryMode> DeliveryModeList { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }

        public class Unit
        {
            public long PurordUnitID { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }

        public class DeliveryMode
        {
            public long ID_Mode { get; set; }
            public string ModeName { get; set; }


        }
        public class ChangeModeInput
        {
            public int Mode { get; set; }

        }
        public class InputProductID
        {
            public long ProductID { get; set; }
          
        }
        public class UpdatePurchaseOrder
        {

            public long ID_PurchaseOrder { get; set; }
            public long FK_PurchaseOrder { get; set; }
            public long PoNo { get; set; }
            public string PoReferenceNo { get; set; }
            public DateTime PoDate { get; set; }
           // public Decimal PoTotalAmount { get; set; }
           public Decimal PoOthercharges { get; set; }
            public Decimal PoNetAmount { get; set; }
            public Decimal PoAdvanceAmount { get; set; }
            public DateTime PoEstiDeliveryDate { get; set; }
            public byte PoDeliveryType { get; set; }
            public String PoRemarks { get; set; }
            public byte PoPurchased { get; set; }
            public long ?FK_Quotation { get; set; }
            public long FK_Supplier { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }
            public long FK_Department { get; set; }
            public long FK_BranchCodeUser { get; set; }
           
            public String EntrBy { get; set; }
          
         
            public long FK_Machine { get; set; }
           
            public byte UserAction { get; set; }
            public int Debug { get; set; }
            public string TransMode { get; set; }
            public string PurchaseOrderDetails { get; set; }
            public Int64 ? LastID { get; set; }
        }
      

        public class DeletePurchaseOrder
        {
            public long FK_PurchaseOrder { get; set; }
            public string TransMode { get; set; }
           
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
        }

        public class PurchaseOrderInfoView
        {
            public long PurchaseOrderID { get; set; }
            public long ReasonID { get; set; }
        }
        public class PurchaseOrderID
        {
            public Int64 ID_PurchaseOrder { get; set; }
        }

        public class PurchaseorderbyQutotationView
        {
            public String Quotationno { get; set; }
            public long FK_Company { get; set; }
            public long Mode { get; set; }

        }
        public class PurchaseOrderViewID
        {
            public Int64 FK_PurchaseOrder{ get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public String TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
            public Byte Detailed { get; set; }
		
        }
        public class Purchasedatafill
        {

            public int Mode { get; set; }

            public long FK_Master { get; set; }

        }

        public class SmartFillInput
        {
            public long FK_Supplier { get; set; }
            public string TransMode { get; set; }
            public long FK_Branch { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
        }

        public static string _deleteProcedureName = "ProPurchaseOrderDelete";
        public static string _updateProcedureName = "ProPurchaseOrderUpdate";
        public static string _selectProcedureName = "ProPurchaseOrderSelect";
        public APIGetRecordsDynamic<PurchaseOrderView> GetPurchaseorderByQuotationid(PurchaseorderbyQutotationView input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseOrderView, PurchaseorderbyQutotationView>(companyKey: companyKey, procedureName: "ProPurchaseDataFill", parameter: input);
        }
        public Output UpdatePurchaseOrderData(UpdatePurchaseOrder input, string companyKey)
        {
            return Common.UpdateTableData<UpdatePurchaseOrder>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeletePurchaseOrderData(DeletePurchaseOrder input, string companyKey)
        {
            return Common.UpdateTableData<DeletePurchaseOrder>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<PurchaseOrderView> GetPurchaseOrderData(PurchaseOrderViewID input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseOrderView, PurchaseOrderViewID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public class PurchaseorderSubSelect
        {
            public Int64 FK_PurchaseOrder { get; set; }
            public Int64 FK_Company { get; set; }
            public String EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public String TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public String Name { get; set; }
            public Byte Detailed { get; set; }


        }
        public APIGetRecordsDynamic<PurchaseOrderDetails> GetSubTablePurchaseOrderData(PurchaseorderSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseOrderDetails, PurchaseorderSubSelect>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamicdn<dynamic> PurchaseOrderDataFill(Purchasedatafill input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<Purchasedatafill>(companyKey: companyKey, procedureName: "ProPurchaseDataFill", parameter: input);

        }

        public class inputGetPurOrdNo
        {
            public long FK_Company { get; set; }
        }
        public class outputPurOrdNo
        {
            public long PurordNo{ get; set; }
        }
        public APIGetRecordsDynamic<outputPurOrdNo> GetPurchaseOrderNo(inputGetPurOrdNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<outputPurOrdNo, inputGetPurOrdNo>(companyKey: companyKey, procedureName: "ProGetPurchaseorderNo", parameter: input);
        }

        public class LastIdIn
        {
          
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            
            public string TransMode { get; set; }
            public long FK_BranchCodeUser { get; set; }
          
          


        }
        public class LastIdOut
        {
            public Int64? LastID { get; set; }
        }
        
        //used for opening stock
        public APIGetRecordsDynamic<LastIdOut> GetPurchaseOrderlastID(LastIdIn input, string companyKey)
        {
            return Common.GetDataViaProcedure<LastIdOut, LastIdIn>(companyKey: companyKey, procedureName: "ProGetLastID", parameter: input);
        }

        public APIGetRecordsDynamic<PurchaseOrderDetails> GetPurchaseOrderCollection(SmartFillInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<PurchaseOrderDetails, SmartFillInput>(companyKey: companyKey, procedureName: "ProGetPurchaseCollection", parameter: input);

        }
        public class NumberGen
        {
            int ID_NumberGen { get; set; }
            string Submodule { get; set; }
        }
    }
}


