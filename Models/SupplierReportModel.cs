using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class SupplierReportModel
    {
        public class Supplierreportview
        {
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public DateTime AsonDate { get; set; }
            public List<Branchs> BranchList { get; set; }
            public List<SupplierTypeNameList> SupplierType { get; set; }
            public bool IncludeAdvance { get; set; }
            public long SuppID { get; set; }
            public long BranchID { get; set; }
        }
        public class Branchs
        {
            public long BranchID { get; set; }
            public string Branch { get; set; }

        }

        public class SupplierType
        {
            public long SupplierTypeID { get; set; }
            public string SupplierTypeName { get; set; }

        }
        public class SupplierTypeNameList
        {
            public Int64 ID_SupplierType { get; set; }
            public string STName { get; set; }
        }

        public class Suppliergridview
        {

            public  long SlNo { get; set; }
            public string SupplierName { get; set; }
            public string Opening { get; set; }
            public string Credit { get; set; }
            public string Paid { get; set; }
            public string Balance { get; set; }
            public string SupplierType { get; set; }
            public string SubTotal { get; set; }
            public string GrandTotal { get; set; }
        }

        public class Suppliergridinput
        {
            public DateTime ?FromDate { get; set; }
            public DateTime ?ToDate { get; set; }
            public DateTime ?AsonDate { get; set; }
            public long BranchID { get; set; }
            public long SupplierTypeID { get; set; }
            public bool IncludeAdvance { get; set; }
            public long FK_Mode { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }

            public long FK_Company { get; set; }
            public string TransMode { get; set; }
           
        }

        public class SupplierLedgerinput
        {
            public long ?FK_Branch { get; set; }
            public long ?FK_Supplier { get; set; }
            public long TableClount { get; set; }
            //public string EntrBy { get; set; }
            //public long FK_Machine { get; set; }
            //public long FK_BranchCodeUser { get; set; }

            public Int64 FK_Company { get; set; }
        }
        

        public class Supplierledgerouttopview
        {
            public string Supplier { get; set; }
            public string Address { get; set; } = "";
            public string GSTNo { get; set; } = "";
            public string Opening { get; set; } = "";
            public string OpeningDate { get; set; } = "";
            public string TransType { get; set; }

        }

        public class Supplierledgerview
        {
            public string SlNo { get; set; }
            public string Date { get; set; }
            public string TransactionType { get; set; }
            public string TransactionNo { get; set; }
            public string Remark { get; set; } = "";
            public string PaymentMode { get; set; }
            public string Credit { get; set; } = "";
            public string Paid { get; set; } = "";
            public string Balance { get; set; } = "";
            public string TransType { get; set; }
            public string Total2 { get; set; }
            public string Total3 { get; set; }
        }

        public APIGetRecordsDynamic<Suppliergridview> GetSupplierOutStandingReportView(Suppliergridinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Suppliergridview, Suppliergridinput>(companyKey: companyKey, procedureName: "proSupplieroutstandingReportView", parameter: input);
        }


        public APIGetRecordsDynamic<Supplierledgerouttopview> GetSupplierledgertopDetailsData(SupplierLedgerinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Supplierledgerouttopview, SupplierLedgerinput>(companyKey: companyKey, procedureName: "proRptSupplierLedger", parameter: input);

        }

        public APIGetRecordsDynamic<Supplierledgerview> GetSupplierledgerDetailsData(SupplierLedgerinput input, string companyKey)
        {
            return Common.GetDataViaProcedure<Supplierledgerview, SupplierLedgerinput>(companyKey: companyKey, procedureName: "proRptSupplierLedger", parameter: input);

        }
    }
}