/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 29/11/2022
Purpose		: ProductionWorkFlow
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
17/12/2022     Kavya K         Save , Update ,Select for View
-------------------------------------------------------------------------
19/12/2022     Kavya K         Delete
 --------------------------------------------------------------------*/

using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Models
{
    public class ProductionWorkFlowModel
    {

        public class ProductionWorkFlowView
        {
            public Int64 SlNo { get; set; }
            public DateTime PWDate { get; set; }
            public long FK_Product { get; set; }
            public string TransMode { get; set; }
            public Int32 SortOrder { get; set; }
            public long ID_ProductionWorkFlow { get; set; }
            public List<ProductionworkflowDetailsView> ProductionworkflowDetails { get; set; }
            public Int64 TotalCount { get; set; }
            public string ProductName { get; set; }
            public long FK_ProductionWorkFlow { get; set; }
            public long? LastID { get; set; }
        }
        public class ProductionworkflowDetailsView
        {
            public long ID_ProductionWorkFlowDetails { get; set; }
            public long FK_ProductionWorkFlow { get; set; }
            public Int32 FK_ProductionStage { get; set; }
            public Int64 Priority { get; set; }
            public bool Returnstage { get; set; }
            public bool AcceptJobs {get;set;}
            public string ProductName { get; set; }
            public DateTime PWDate { get; set; }
            public long FK_Product { get; set; }
            public string StageName { get; set; }
            
        }
        public class UpdateProductionWorkFlow
        {
            public long ID_ProductionWorkFlow { get; set; }
            public DateTime PWDate { get; set; }
            public long FK_Product { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }            
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int16 UserAction { get; set; }
            public string ProductionworkflowDetails { get; set; }
            public long FK_ProductionWorkFlow { get; set; }
            public Int32 Debug { get; set; }
            public long? LastID { get; set; }
        }
        public static string _deleteProcedureName = "ProProductionWorkFlowDelete";
        public static string _updateProcedureName = "ProProductionWorkFlowUpdate";
        public static string _selectProcedureName = "ProProductionWorkFlowSelect";

        public class DeleteProductionWorkFlow
        {
            public string TransMode { get; set; }
            public long FK_ProductionWorkFlow { get; set; }
            public long Debug { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
        }


        public class ProductionWorkFlowID
        {
            public long FK_ProductionWorkFlow { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string Name { get; set; }
            public string TransMode { get; set; }
            public Int16 Detailed { get; set; }
        }
        public class ProductionWorkFlowData
        {
            public long FK_ProductionWorkFlow { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int64 Detailed { get; set; }
        }
        public Output UpdateProductionWorkFlowData(UpdateProductionWorkFlow input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductionWorkFlow>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProductionWorkFlowData(DeleteProductionWorkFlow input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductionWorkFlow>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProductionWorkFlowView> GetProductionWorkFlowData(ProductionWorkFlowID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionWorkFlowView, ProductionWorkFlowID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<ProductionworkflowDetailsView> GetProductionWorkFlowDataDetailsSelect(ProductionWorkFlowData input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionworkflowDetailsView, ProductionWorkFlowData>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
    }
}
