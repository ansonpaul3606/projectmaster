using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class EinvoiceModel
    {
        public class Einvoice_Schema
        {
            public string Version { get; set; }
            public TranDtls TranDtls { get; set; }
            public DocDtls DocDtls { get; set; }
            public SellerDtls SellerDtls { get; set; }
            public BuyerDtls BuyerDtls { get; set; }
            public ValDtls ValDtls { get; set; }
            public string EwbDtls { get; set; }
            public RefDtls RefDtls { get; set; }
            public List<ItemList> ItemList { get; set; }
        }
        public class TranDtls
        {
            public string TaxSch { get; set; }
            public string SupTyp { get; set; }
            public string IgstOnIntra { get; set; }
            public string RegRev { get; set; }
            public string EcmGstin { get; set; }
          
        }
        public class DocDtls
        {
            public string Typ { get; set; }
            public string No { get; set; }
            public string Dt { get; set; }
        }
      
        public class SellerDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public long Pin { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            public string Em { get; set; }
        }
        public class BuyerDtls
        {
            public string Gstin { get; set; }
            public string LglNm { get; set; }
            public string Addr1 { get; set; }
            public string Addr2 { get; set; }
            public string Loc { get; set; }
            public long Pin { get; set; }
            public string Pos { get; set; }
            public string Stcd { get; set; }
            public string Ph { get; set; }
            public string Em { get; set; }
        }
        public class ValDtls
        {
            public decimal AssVal { get; set; }
            public decimal IgstVal { get; set; }
            public decimal CgstVal { get; set; }
            public decimal SgstVal { get; set; }
            public decimal CesVal { get; set; }
            public decimal StCesVal { get; set; }
            public decimal Discount { get; set; }
            public decimal OthChrg { get; set; }
            public decimal RndOffAmt { get; set; }
            public decimal TotInvVal { get; set; }
        }
        public class RefDtls
        {
            public string InvRm { get; set; }
        }
        public class ItemList

        {
            public string SlNo { get; set; }
            public string PrdDesc { get; set; }
            public string IsServc { get; set; }
            public string HsnCd { get; set; }
            public decimal Qty { get; set; }
            public decimal FreeQty { get; set; }
            public string Unit { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotAmt { get; set; }
            public decimal Discount { get; set; }
            public decimal PreTaxVal { get; set; }
            public decimal AssAmt { get; set; }
            public decimal GstRt { get; set; }
            public decimal IgstAmt { get; set; }
            public decimal CgstAmt { get; set; }
            public decimal SgstAmt { get; set; }
            public decimal CesRt { get; set; }
            public decimal CesAmt { get; set; }
            public decimal CesNonAdvlAmt { get; set; }
            public decimal StateCesRt { get; set; }
            public decimal StateCesAmt { get; set; }
            public decimal StateCesNonAdvlAmt { get; set; }
            public decimal OthChrg { get; set; }
            public decimal TotItemVal { get; set; }

        }
    }
}