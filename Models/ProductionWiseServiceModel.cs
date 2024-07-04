using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductionWiseServiceModel
    {
        public class ProductionwiseView
        {
            public long SlNo { get; set; }
            public long ID_ProductWiseServices { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public Int32 CategoryID { get; set; }
            public string Category { get; set; }
            public Int16 PrdID { get; set; }       
            public string Product { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
            public DateTime? EffectDate { get; set; }
            public List<SubProductwiseserviceDetailsView> SubproductServiceDetails { get; set; }
           
            public Int64 TotalCount { get; set; }

            public string TransMode { get; set; }
            public bool IncludeTax { get; set; }
            public long? TaxGroupID { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
            // public string TaxGroupName { get; set; }
        }
        public class SubProductwiseserviceDetailsView
        {
            public Int16? SubProductID { get; set; }
            public string SubProduct { get; set; }
            public Int16? ID_Mode { get; set; }
            public string ModeName { get; set; }
            public Int16 ServiceID { get; set; }
            public string ServiceList { get; set; }
            public Int16? PeriodFrom { get; set; }
            public Int16? PeriodTo { get; set; }
            public decimal ? ServiceCost { get; set; }

          
         
            //public Int64 FK_Company { get; set; }
            //public Int64 FK_BranchCodeUser { get; set; }
            //public string EntrBy { get; set; }
            //public DateTime? EntrOn { get; set; }

        }

        public class CategoryList
        {
            public Int32 CategoryID { get; set; }
            public string Category { get; set; }

        }
        public class Services
        {
            public Int16 ServiceID { get; set; }
            public string ServiceList { get; set; }
        }
        public class Products
        {
            public int PrdID { get; set; }
            public string Product { get; set; }
            // public long FK_Employee{ get; set; }

        }
        public class SubProducts
        {
            public int SubProductID { get; set; }
            public string SubProduct { get; set; }
            // public long FK_Employee{ get; set; }


        }

        public class PeriodType
        {
            public Int32 ?ID_Mode { get; set; }
            public string ModeName { get; set; }


        }

        public class PrdwiseViewList
        {
            public List<Services> ServiceList { get; set; }
            public List<PeriodType> PerdList { get; set; }
            public List<CategoryList> CategoryList { get; set; }
            public List<TaxGroup> TaxgroupList { get; set; }
        }
        public class TaxGroup
        {
            public long? TaxGroupID { get; set; }
            public string TaxGroupName { get; set; }
        }
        public class ProductionwiseUpdate
        {
            public long ID_ProductWiseServices { get; set; }
            public long FK_Product { get; set; }
            public long FK_Category { get; set; }
            public DateTime? EffectDate { get; set; }

            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public long Debug { get; set; }
            public long FK_ProductWiseServices { get; set; }

            public string SubproductServiceDetails { get; set; }
            public long? FK_TaxGroup { get; set; }
            public bool IncludeTaxOnServiceCharge { get; set; }


        }



        public class ProductionwiseDelete
        {
            public long FK_ProductWiseServices { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }
      


        }

        public class ProductwiseID
        {
            public long FK_ProductWiseServices { get; set; }

        

            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
           public string Name { get; set; }
           public Byte Details { get; set; }
        }
        public class PrdWiseRsnView
        {
            public long ID_ProductWiseServices { get; set; }

            public long ReasonID { get; set; }
        }


        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }

        public static string _deleteProcedureName = "ProProductWiseServicesDelete";
        public static string _updateProcedureName = "ProProductWiseServicesUpdate";
        public static string _selectProcedureName = "ProProductWiseServicesSelect";

        public static string _selectSubtableSubproductwiseservice = "ProSubProductWiseSerSelect";
        public Output UpdatePrdwiseData(ProductionwiseUpdate input, string companyKey)
        {
            return Common.UpdateTableData<ProductionwiseUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeletePrdwiseData(ProductionwiseDelete input, string companyKey)
        {
            return Common.UpdateTableData<ProductionwiseDelete>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProductionwiseView> GetPrdwiseData(ProductwiseID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionwiseView, ProductwiseID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<PeriodType> GetPerdTypList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<PeriodType, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

        public class SubproductwiseSubSelect
        {
            public Int64 FK_ProductWiseServices { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Byte Details { get; set; }
   
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }

        }
        public APIGetRecordsDynamic<SubProductwiseserviceDetailsView> GetSubProductwiseServiceData(SubproductwiseSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubProductwiseserviceDetailsView, SubproductwiseSubSelect>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

    }
}
