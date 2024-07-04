using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class ProductionWorkFlowRptModel
    {
        public class ProductionWorkFlowRptlist
        {
            public string ProductName { get; set; }
            public string StageName { get; set; }
            public long FK_Product { get; set; }
            public long FK_Stage { get; set; }
            public int BranchID { get; set; }
            public long FK_Company { get; set; }

        }
        public class ProductionWorkFlowRptInput
        {
            public int Mode { get; set; }
            public long FK_Product { get; set; }
            public long FK_Stage { get; set; }            
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

        }
        public class MaterialDetails
        {
            public long SlNo { get; set; }
            public string Stage { get; set; }
            public string Material { get; set; }
            public string Quantity { get; set; }
            public string Amount { get; set; }
            public string TotalAmount { get; set; }
            public string EffectStock { get; set; }

        }
        public class ProductDetails
        {
            public long SlNo { get; set; }
            public string Stage { get; set; }
            public string Product { get; set; }
            public string Quantity { get; set; }
            public string AddtoStock { get; set; }           

        }
        public class ResourceDetails
        {
            public long SlNo { get; set; }
            public string Stage { get; set; }
            public string Resource { get; set; }
            public string Cost { get; set; }

        }
        public class StageDetails
        {
            public long SlNo { get; set; }
            public string Stage { get; set; }
            public string Duration { get; set; }
            public string TotalCost { get; set; }
            
        }
        public class CostDetails
        {            
            public string ProductCost { get; set; }
            public string SalesPrice { get; set; }

        }
        public static string _selectProcedureName = "ProRptProductionWorkFlow";/*"ProRptProductionWorkFlow"*/
        public APIGetRecordsDynamic<ProductionWorkFlowRptlist> GetProductionWorkFlowRptDetailsData(ProductionWorkFlowRptInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionWorkFlowRptlist, ProductionWorkFlowRptInput>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<MaterialDetails> GetMaterialsDetailsData(ProductionWorkFlowRptInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<MaterialDetails, ProductionWorkFlowRptInput>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ProductDetails> GetProductDetailsData(ProductionWorkFlowRptInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductDetails, ProductionWorkFlowRptInput>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<ResourceDetails> GetResourceDetailsData(ProductionWorkFlowRptInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<ResourceDetails, ProductionWorkFlowRptInput>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<StageDetails> GetStageDetailsData(ProductionWorkFlowRptInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<StageDetails, ProductionWorkFlowRptInput>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
        public APIGetRecordsDynamic<CostDetails> GetCostDetailsData(ProductionWorkFlowRptInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<CostDetails, ProductionWorkFlowRptInput>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }
    }
}