using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductionStockListModel
    {

        public class ProductionStockListModelView
        {
            public long FK_Product { get; set; }
            public long Quantity { get; set; }
            public DateTime DeliveryDate { get; set; }
            public int Mode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
           
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

        }
        public class ProductionAnalysisGridview
        {
            public long SlNo { get; set; }
            public string Stage { get; set; }
            public string Material { get; set; }
            public long RequiredQuantity { get; set; }
            public long Amount { get; set; }
            public long CurrentStock { get; set; }
            public long AvaliableStock { get; set; }
        }


        public class ProductionAnalysisSubGridview
        {
            public string TotalMaterialCost { get; set; }
            public string ExpectedDeliveryDate { get; set; }
            public string TotalResourceCost { get; set; }

            public string TotalDuration { get; set; }
            public string TotalProductionCost { get; set; }
            public string SalesPrice { get; set; }


        }
        public class ProductionStatusGridview
        {
            public long SlNo { get; set; }
            public string JobCardNo { get; set; }
            public string Remark { get; set; }
            public long Quantity { get; set; }
            public string Stage { get; set; }
            public string RemainingPeriod { get; set; }
            public string CompletionDate { get; set; }
        }
        public class ProductionStatussubGridview
        {
            public long Availablestock { get; set; }
        }
        public APIGetRecordsDynamic<ProductionAnalysisGridview> GetProductionStockListData(ProductionStockListModelView input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionAnalysisGridview, ProductionStockListModelView>(companyKey: companyKey, procedureName: "ProProductionStockList", parameter: input);

        }
        public APIGetRecordsDynamic<ProductionAnalysisSubGridview> GetProductionStockSubListData(ProductionStockListModelView input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionAnalysisSubGridview, ProductionStockListModelView>(companyKey: companyKey, procedureName: "ProProductionStockList", parameter: input);

        }
        public APIGetRecordsDynamic<ProductionStatusGridview> GetProductionStatusdetial(ProductionStockListModelView input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStatusGridview, ProductionStockListModelView>(companyKey: companyKey, procedureName: "ProProductionStockList", parameter: input);

        }
        public APIGetRecordsDynamic<ProductionStatussubGridview> GetStatusreportsubsectiondetail(ProductionStockListModelView input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStatussubGridview, ProductionStockListModelView>(companyKey: companyKey, procedureName: "ProProductionStockList", parameter: input);

        }
    }
}