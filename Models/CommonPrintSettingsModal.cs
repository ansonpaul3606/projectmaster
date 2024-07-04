using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Models
{
    public class CommonPrintSettingsModal
    {
        public class step1input
        {
            public  byte UserAction { get; set; }
            public byte Debug { get; set; }
            public long MasterID { get; set; }
            public string SettingsName { get; set; }

            public string EntrBy { get; set; }
 
            public bool Cancelled { get; set; }
           
            public long FK_Reason { get; set; }
            public long FK_Machine { get; set; }
            public string FrontSide { get; set; }
            public string FrontImgUrl { get; set; }
            public string TransMode { get; set; }
            public string ImagePath { get; set; }
        
            public string FrntImg { get; set; }
            public int CommonPrintSettingsMode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 ID_CommonPrintingSettings { get; set; }
            public Int64 FK_CommonPrintingSettings { get; set; }
            public string PageSize { get; set; }

        }
        public class GetTemplateIp
        {
            public string TransMode { get; set; }
            public string ReportType { get; set; }
            public Int64 FK_Master { get; set; }
        }
        public class GetElementsIp
        {
            public int TransType { get; set; }
        }

        public class CommonPrintSettingsView
        {
            public List<ActionStatus> Modules { get; set; }
           public List <PageOrientaion> PageOrientaions { get; set; }
        }
        public class CommonPrintSettingElemets
        {
            public string Html { get; set; }
            public string Id { get; set; }
            public string ElementName { get; set; }
         
        }
        public class CommonPrintSettingElemetsAll
        {
            public string Html { get; set; }
            public string Id { get; set; }
            public string ElementName { get; set; }
            public string TransMode { get; set; }
            public Int16 TransType { get; set; }

        }
        public class PageOrientaion
        {
            public string Page_Orientaion { get; set; }
            public string PValue { get; set; }

            public string width_in_px { get; set; }
            public string height_in_px { get; set; }

        }
        public class ActionStatus
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }
        public class GetCommonPrintElement
        {
            public Int64 FK_Master { get; set; }
            public int TableCount { get; set; }
            public string TransMode { get; set; }
            public Int16 FK_Branch { get; set; }
            public Int64 FK_Company { get; set; }
            public Int16 Mode { get; set; }
        }
        public class GetCommonPrintElementsIP
        {
            public Int64 FK_Company { get; set; }
            public int Module { get; set; }
            public string TransMode { get; set; }
            public string ImagePath { get; set; }
           
            public string FrntImg { get; set; }
            public int CommonPrintSettingsMode { get; set; }
            public Int64 ID_CommonPrintingSettings { get; set; }
            public Int64 FK_CommonPrintingSettings { get; set; }
            public string FrontSide { get; set; }
            public byte UserAction { get; set; }
            public byte Debug { get; set; }
            public string SettingsName { get; set; }

            public string EntrBy { get; set; }

            public bool Cancelled { get; set; }

            public long FK_Reason { get; set; }
            public string PageSize { get; set; }
        }
        public class DeleteCommonPrintingSettingsIP
        {
     
            public string EntrBy { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }

            public int FK_Reason { get; set; }
            public int Debug { get; set; }
            public Int64 FK_CommonPrintingSettings { get; set; }

        }
        public class SelectCommonPrintingSettingsIp
        {
            public Int64 PageIndex { get; set; }
            public Int64 PageSize { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public long FK_CommonPrintingSettings { get; set; }
            public string Name { get; set; }



        }
        public class UpdateCommonPrintingSettingsIp
        {
            public byte UserAction { get; set; }
            public string EntrBy { get; set; }
            public string SettingsName { get; set; }
            public Int64 ID_CommonPrintingSettings { get; set; }
            public Int64 FK_CommonPrintingSettings { get; set; }
            public Int64 FK_Company { get; set; }
            public long CommonPrintSettingsMode  { get; set; }

            public string ImagePath { get; set; }
            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string PageSize { get; set; }
          //  public byte[] FrntImg2 { get; set; }

        }
        public class GetCommonPrintElementsOP
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public Int64 SlNo { get; set; }
            public Int64 FK_CommonPrintingSettings { get; set; }
            public long TotalCount { get; set; }
            public string SettingsName { get; set; }
            public string CommonPrintSettingsMode { get; set; }
            public string CommonPrintSettingsModeName { get; set; }
            public DateTime EntrOn { get; set; }
            public string Module { get; set; }

        }
        public class SelectCommonPrintingSettingsOpt
        {
            public string SettingsName { get; set; }
            public Int64 ID_CommonPrintingSettings { get; set; }
            public long CommonPrintSettingsMode { get; set; }
            public string ImagePath { get; set; }
            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string StrFrntImg { get; set; }
            public string PageSize { get; set; }

        }
        public class CommonPrintSettings
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string PageSize { get; set; }
        }
        public class CommonPrintSettingsTemplateData
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string Logo_image { get; set; }
            public string Tittle { get; set; }
            public string Terms_and_conditions { get; set; }
            public string Subtotal { get; set; }
            public string Address { get; set; }
            public string PaymentInfo { get; set; }
            public string Invoice_No { get; set; }
            public string Table_data { get; set; }
            public string Table_column { get; set; }
            public string Box_data { get; set; }
        }
        public class CommonPrintTemplateData
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string Logo_image { get; set; }
            public string Tittle { get; set; }
            public string Terms_and_conditions { get; set; }
            public string Subtotal { get; set; }
            public string Address { get; set; }
            public string PaymentInfo { get; set; }
            public string Invoice_No { get; set; }
            public string Table_data { get; set; }

        }
        public class GetTemplateCommonPrintOp
        {
          
            public string FrontSideString { get; set; }
            public string FrntImg { get; set; }
            public string PageSize { get; set; }
        
        }

        public class GetIntvoiceDataOp
        {
            public Int64 ID_CommonPrintingSettings { get; set; }
            public string BillNo { get; set; }
            public string BillNoType { get; set; }
            public string CustomerName { get; set; }
            public string CustomerNo { get; set; }
            public string CustomerNoName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Mobile { get; set; }
            public string CustomerGSTIN { get; set; }
            public string BranchName { get; set; }
            public string TransportationType { get; set; }
            public string ShippingName { get; set; }
            public string ShippingAddress1 { get; set; }
            public string ShippingAddress2 { get; set; }
            public string ShippingAddress3 { get; set; }
            public string ShippingMobile { get; set; }
            public string VehicleNo { get; set; }
            public string DriverName { get; set; }
            public string DriverNo { get; set; }
            public string BillTotal { get; set; }
            public string OtherCharge { get; set; }
            public string Discount { get; set; }
            public string NetAmount { get; set; }
            public string BankName { get; set; }
            public string BankAccNo { get; set; }
            public string SalesMan { get; set; }
            public string BankIFSCCode { get; set; }
            public string EnteredBy { get; set; }
            public string EnteredCode { get; set; }

            public string EnteredOn { get; set; }
            public string EnteredTime { get; set; }
            public string EnteredDateTime { get; set; }



        }


        public class GetinvoiceDataIp
        {
            public Int64 FK_Master { get; set; }
            public string TransMode { get; set; }
        }
        /******edit hk**********/
        public class ProjectBillingInvoiceIP
        {
            public Int64 FK_Master { get; set; }
            public int TableCount { get; set; }
            public string TransMode { get; set; }
            public Int16 FK_Branch { get; set; }
            public Int64 FK_Company { get; set; }
        }
        public class GetInvoice_table2
        {           
            public string ProductName { get; set; }
            public string HSNCode { get; set; }
            public string Quantity { get; set; }
            public string FreeQuantity { get; set; }
            //public string Rate { get; set; }
            public string MRP { get; set; }
            public string DiscountAmount { get; set; }
            public string SalesPrice { get; set; }
            public string GST { get; set; }

            public string TotalAmount { get; set; }
            public string Total { get; set; }
            public string Category { get; set; }
            public string Finalamount { get; set; }
            public Int64 SlNo { get; set; }



        }
        public class GetInvoice_table1
        {
            public string InvoiceDate { get; set; }
            public string PBillNo { get; set; }
            public string PBillDate { get; set; }


            public string PCustomerName { get; set; }

            public string Place { get; set; }
            public string GSTINNo { get; set; }
            public string PinCode { get; set; }
            public string PShippingAddress1 { get; set; }
            public string PShippingAddress2 { get; set; }
            public string PShippingAddress3 { get; set; }
            public string InsAddress { get; set; }
            public string PAddress1 { get; set; }
            public string PAddress2 { get; set; }
            public string PAddress3 { get; set; }

            public string Branch { get; set; }
            public string PCustomerGSTIN { get; set; }
            public Int64 NetAmount { get; set; }
            public string ProjNumber { get; set; }
            public Int64 ID_Project { get; set; }
            public string RefNo { get; set; }
            public string PAmountinWords { get; set; }
            public string PTaxInwords { get; set; }

        }
        public class GetInvoice_table3
        {
            public string Product_HSN { get; set; }
            public string ProductCode { get; set; }
            public string Amount { get; set; }
            public string GSTType { get; set; }
            public string IGST2 { get; set; }
            public string CGST2 { get; set; }
            public string SGST2 { get; set; }
            public string TotalGST { get; set; }            
        }
        public class GetInvoice_table4
        {
            public string Product_HSN { get; set; }
            public string Amount { get; set; }
            public string GSTType { get; set; }
            public string IGST2 { get; set; }
            public string CGST2 { get; set; }
            public string SGST2 { get; set; }
            public string TotalGST { get; set; }
        }
        /*****EditEnd hk*****/
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public APIGetRecordsDynamic<ActionStatus> GetModules(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<ActionStatus, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
        public APIGetRecordsDynamic<PageOrientaion> GetPageOrientaion(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<PageOrientaion, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);
        }
       
        public Output UpdateCommonPrintingSettings(UpdateCommonPrintingSettingsIp input, string companyKey)
        {
            return Common.UpdateTableData<UpdateCommonPrintingSettingsIp>(parameter: input, companyKey: companyKey, procedureName: "ProCommonPrintingSettingsUpdate");
        }
        public APIGetRecordsDynamic<GetCommonPrintElementsOP> SelectCommonPrintingSettings(SelectCommonPrintingSettingsIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetCommonPrintElementsOP, SelectCommonPrintingSettingsIp>(companyKey: companyKey, procedureName: "ProCommonPrintingSettingsSelect", parameter: input);
        }
        public APIGetRecordsDynamic<SelectCommonPrintingSettingsOpt> SelectCommonPrintingSettingsbyId(SelectCommonPrintingSettingsIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<SelectCommonPrintingSettingsOpt, SelectCommonPrintingSettingsIp>(companyKey: companyKey, procedureName: "ProCommonPrintingSettingsSelect", parameter: input);
        }
       
        public Output DeleteCommonPrintingSettings(DeleteCommonPrintingSettingsIP input, string companyKey)
        {
            return Common.UpdateTableData<DeleteCommonPrintingSettingsIP>(parameter: input, companyKey: companyKey, procedureName: "ProCommonPrintingSettingsDelete");
        }
        public APIGetRecordsDynamic<GetIntvoiceDataOp> GetinvoiceData(GetinvoiceDataIp input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetIntvoiceDataOp, GetinvoiceDataIp>(companyKey: companyKey, procedureName: "ProGetIntvoiceData", parameter: input);
        }

        ////////edit hk///////////
        public APIGetRecordsDynamic<GetInvoice_table1> Pro_ProjectBillingInvoice_table1(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetInvoice_table1, ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);
        }
        public APIGetRecordsDynamicdn<DataTable> Pro_ProjectBillingInvoice_table(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedureDatatable<ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);

        }
        public APIGetRecordsDynamic<GetInvoice_table2> Pro_ProjectBillingInvoice_table2(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetInvoice_table2, ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);
        }
        public APIGetRecordsDynamic<GetInvoice_table3> Pro_ProjectBillingInvoice_table3(ProjectBillingInvoiceIP input, string companyKey)
        {
            return Common.GetDataViaProcedure<GetInvoice_table3, ProjectBillingInvoiceIP>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);
        }
        ////////edit end hk/////////
        public APIGetRecordsDynamic<CommonPrintSettingElemetsAll> GetCommonPrintElements(GetCommonPrintElement input, string companyKey)
        {
            return Common.GetDataViaProcedure<CommonPrintSettingElemetsAll, GetCommonPrintElement>(companyKey: companyKey, procedureName: "ProCmnPrintInvoice", parameter: input);
        }
    }
}