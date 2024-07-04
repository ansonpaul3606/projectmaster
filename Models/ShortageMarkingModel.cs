using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class ShortageMarkingModel
    {
        public class ShortageMarkingView
        {
            public long PurchaseID { get; set; }
            public string InvoiceNo { get; set; }
            public string SuppName { get; set; }
            public long SuppID { get; set; }
            public List<Unit> UnitList { get; set; }
            public List<Categorydata> CategoryList { get; set; }
        }
        public class ShortageView
        {
            public long ShortageID { get; set; }
            public string TransMode { get; set; }
            public long FK_Supplier{ get; set; }
            public string SuppName { get; set; }
            public long FK_Purchase{ get; set; }
            public DateTime ShTransDate { get; set; }
            //public string ShortageProductDetails { get; set; }
            public List<ShortageProductDetails> ShortageProductDetails { get; set; }
            public decimal ShNetAmount { get; set; }
            public long? LastID { get; set; }
        }
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class Categorydata
        {
            public Int32 ID_Category { get; set; }
            public string CategoryName { get; set; }
        }
        public class ShortageProductDetails
        {
            public Int64 ShPdProductID { get; set; }
            public Int64 ShPdUnit { get; set; }
            public Int64 FK_Stock { get; set; }
            public decimal ShPdQty { get; set; }
            public decimal ShPdShortQty { get; set; }
            public decimal ShPdActQty { get; set; }
            public decimal ShPdRate { get; set; }
            public decimal ShPdTax { get; set; }
            public decimal ShPdNet { get; set; } 

        }
        public class UpdateShortage
        {
            public byte UserAction { get; set; }
            public bool Debug { get; set; }
            public string TransMode { get; set; }            
            public long ID_Shortage { get; set; }
            public DateTime ShTransDate { get; set; }
            public long FK_Purchase { get; set; }           
            public long FK_Supplier { get; set; }
            public string ShortageProductDetails { get; set; }
            public decimal ShNetAmount { get; set; }          
            public long FK_Branch { get; set; }
            public long FK_Company { get; set; }            
            public long FK_BranchCodeUser { get; set; } 
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; } 
            public long FK_Shortage { get; set; }  
            public long? LastID { get; set; }
        }
        public class GetShortage
        {           
            public string TransMode { get; set; }
            public long FK_Shortage { get; set; } 
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public int Mode { get; set; }
        }
        public class ShortageViewDetails
        {
            public Int16 SlNo { get; set; }
            public long ID_Shortage { get; set; }
            public string TransDate { get; set; }
            public DateTime ShTransDate { get; set; }
            public string Supplier { get; set; }
            public string Purchase { get; set; }
            public string ShNetAmount { get; set; }
            public long FK_Supplier { get; set; }
            
            public long FK_Purchase { get; set; }
            
            public Int64? TotalCount { get; set; }
            public long? LastID { get; set; }
        }
        public class ShortageProductDetailsView
        {
            public Int64 ShPdProductID { get; set; }
            public string ShPdProduct { get; set; }
            public Int64 ShPdUnit { get; set; }
            public Int64 FK_Stock { get; set; }
            public string ShPdQty { get; set; }
            public string ShPdShortQty { get; set; }
            public string ShPdActQty { get; set; }
            public string ShPdRate { get; set; }
            public string ShPdTax { get; set; }
            public string ShPdNet { get; set; }

        }
        public Output UpdateShortageDetails(UpdateShortage input, string companyKey)
        {
            return Common.UpdateTableData<UpdateShortage>(parameter: input, companyKey: companyKey, procedureName: "ProShortageUpdate");
        }
        public APIGetRecordsDynamic<ShortageViewDetails> GetShortageData(GetShortage input, string companyKey)
        {
            return Common.GetDataViaProcedure<ShortageViewDetails, GetShortage>(companyKey: companyKey, procedureName: "ProShortageSelect", parameter: input);
        }
        public APIGetRecordsDynamic<ShortageProductDetailsView> GetShortageProductDetails(GetShortage input, string companyKey)
        {
            return Common.GetDataViaProcedure<ShortageProductDetailsView, GetShortage>(companyKey: companyKey, procedureName: "ProShortageSelect", parameter: input);
        }
    }
}