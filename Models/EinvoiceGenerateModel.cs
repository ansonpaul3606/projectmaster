using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class EinvoiceGenerateModel
    {
        public class GenerateEinvoiceData
        {



            public string Version { get; set; }
            //public VersionInfo Version { get; set; }
            public TranDetails TranDtls { get; set; }
            public DocDetails DocDtls { get; set; }
            public SellerDetails SellerDtls { get; set; }
            public BuyerDetails BuyerDtls { get; set; }
            //public DispDetails DispDtls { get; set; }
            //public ShipDetails ShipDtls { get; set; }
            public List<ItemList> ItemList { get; set; }
            public ValDetails ValDtls { get; set; }
            public PayDetails PayDtls { get; set; }
            public RefDetails RefDtls { get; set; }
            //public List<AddlDocDetails> AddlDocDtls { get; set; }
            //public ExpDetails ExpDtls { get; set; }
            //public EwbDetails EwbDtls { get; set; }
            // Add other necessary properties as needed
        }

        public class TranDetails
        {
            public string TaxSch { get; set; }
            public string SupTyp { get; set; }
            public string RegRev { get; set; }
            public string EcmGstin { get; set; }
            public string IgstOnIntra { get; set; }
            // Add other properties as needed
        }

        public class DocDetails
        {
            public string Typ { get; set; }
            public string No { get; set; }
            public string Dt { get; set; }
            // Add other properties as needed
        }

        public class SellerDetails
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            //public string TrdNm { get; set; }
            public string Addr1 { get; set; }
            //public string Addr2 { get; set; }
            public string Loc { get; set; }
            public Int32 Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            //public string Em { get; set; }
            // Add other properties as needed
        }

        public class BuyerDetails
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            //public string TrdNm { get; set; }
            public string Pos { get; set; }
            public string Addr1 { get; set; }
            //public string Addr2 { get; set; }
            public string Loc { get; set; }
            public Int32 Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            //public string Em { get; set; }
            // Add other properties as needed
        }

        //public class DispDetails
        //{
        //    public string Nm { get; set; }
        //    //public string Addr1 { get; set; }
        //    ////public string Addr2 { get; set; }
        //    //public string Loc { get; set; }
        //    //public Int32 Pin { get; set; }
        //    public string Stcd { get; set; }
        //    // Add other properties as needed
        //}

        //public class ShipDetails
        //{
            
        //    public string Gstin { get; set; }
        //    public string LglNm { get; set; }
        //    //public string TrdNm { get { return this._TrdNm; } set { this._TrdNm = String.IsNullOrWhiteSpace(value) ? default(string) : value; } }
        //    //public string TrdNm { get; set; }
        //    public string Addr1 { get; set; }
        //    //public string Addr2 { get; set; }
        //    public string Loc { get; set; }
        //    public Int32 Pin { get; set; }
        //    public string Stcd { get; set; }
        //    // Add other properties as needed
        //}

        public class ItemList
        {
            public string SlNo { get; set; }
            public string PrdDesc { get; set; }
            public string IsServc { get; set; }
            public string HsnCd { get; set; }
            //public string Barcde { get; set; }
            public double Qty { get; set; }
            public double FreeQty { get; set; }
            public string Unit { get; set; }
            public double UnitPrice { get; set; }
            public double TotAmt { get; set; }
            public double Discount { get; set; }
            public double PreTaxVal { get; set; }
            public double AssAmt { get; set; }
            public double GstRt { get; set; }
            public double IgstAmt { get; set; }
            public double CgstAmt { get; set; }
            public double SgstAmt { get; set; }
            public double CesRt { get; set; }
            public double CesAmt { get; set; }
            public double CesNonAdvlAmt { get; set; }
            public double StateCesRt { get; set; }
            public double StateCesAmt { get; set; }
            public double StateCesNonAdvlAmt { get; set; }
            public double OthChrg { get; set; }
            public double TotItemVal { get; set; }
            public string OrdLineRef { get; set; }
            //public string OrgCntry { get; set; }
            public string PrdSlNo { get; set; }
            //public BchDetails BchDtls { get; set; }
            //public List<AttribDetails> AttribDtls { get; set; }
            //// Add other properties as needed
        }

        //public class BchDetails
        //{
        //    private string _Nm;
        //    private string _ExpDt;
        //    private string _WrDt;
        //    public string Nm { get {return this._Nm; } set {this._Nm= String.IsNullOrWhiteSpace(value)?default(string):value; } }
        //    public string ExpDt { get { return this._ExpDt; } set{this._ExpDt=String.IsNullOrWhiteSpace(value)?default(string):value; } }
        //    public string WrDt { get { return this._WrDt; } set { this._WrDt = String.IsNullOrWhiteSpace(value) ? default(string) : value; } }
        //    // Add other properties as needed
        //}

        //public class AttribDetails
        //{
        //    public string Nm { get; set; }
        //    public string Val { get; set; }
        //    // Add other properties as needed
        //}

        public class ValDetails
        {
            public double AssVal { get; set; }
            public double CgstVal { get; set; }
            public double SgstVal { get; set; }
            public double IgstVal { get; set; }
            public double CesVal { get; set; }
            public double StCesVal { get; set; }
            public double Discount { get; set; }
            public double OthChrg { get; set; }
            public double RndOffAmt { get; set; }
            public double TotInvVal { get; set; }
            public double TotInvValFc { get; set; }
            // Add other properties as needed
        }

        public class PayDetails
        {
            public string Nm { get; set; }
            public string AccDet { get; set; }
            public string Mode { get; set; }
            public string FinInsBr { get; set; }
            public string PayTerm { get; set; }
            public string PayInstr { get; set; }
            public string CrTrn { get; set; }
            public string DirDr { get; set; }
            public int CrDay { get; set; }
            public int PaidAmt { get; set; }
            public int PaymtDue { get; set; }
            // Add other properties as needed
        }

        public class RefDetails
        {
            public string InvRm { get; set; }
            public DocPeriodDetails DocPerdDtls { get; set; }
            public List<PrecDocDetails> PrecDocDtls { get; set; }
            public List<ContrDetails> ContrDtls { get; set; }
            // Add other properties as needed
        }

        public class DocPeriodDetails
        {
            public string InvStDt { get; set; }
            public string InvEndDt { get; set; }
            // Add other properties as needed
        }

        public class PrecDocDetails
        {
            public string InvNo { get; set; }
            public string InvDt { get; set; }
            public string OthRefNo { get; set; }
            // Add other properties as needed
        }

        public class ContrDetails
        {
            public string RecAdvRefr { get; set; }
            public string RecAdvDt { get; set; }
            public string TendRefr { get; set; }
            public string ContrRefr { get; set; }
            public string ExtRefr { get; set; }
            public string ProjRefr { get; set; }
            public string PORefr { get; set; }
            public string PORefDt { get; set; }
            // Add other properties as needed
        }

        //public class AddlDocDetails
        //{
        //    public string Url { get; set; }
        //    public string Docs { get; set; }
        //    public string Info { get; set; }
        //    // Add other properties as needed
        //}

        //public class ExpDetails
        //{
        //    public string ShipBNo { get; set; }
        //    public string ShipBDt { get; set; }
        //    public string Port { get; set; }
        //    public string RefClm { get; set; }
        //    public string ForCur { get; set; }
        //    public string CntCode { get; set; }
        //    public string ExpDuty { get; set; }
        //    // Add other properties as needed
        //}

        //public class EwbDetails
        //{
        //    //public string TransId { get; set; }
        //    public string TransName { get; set; }
        //    //public int  Distance { get; set; } 
        //    public string TransDocNo { get; set; }
        //    public string TransDocDt { get; set; }
        //    public string VehNo { get; set; }
        //    public string VehType { get; set; }
        //    public string TransMode { get; set; }
        //    // Add other properties as needed
        //}
    }
}



