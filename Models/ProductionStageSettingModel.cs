/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 25/11/2022
Purpose		: ProductionStage
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
    public class ProductionStageSettingModel
    {

        public class ProductionStageView
        {
            public long ID_ProductionStage { get; set; }
            public long FK_ProductionStage { get; set; }
            public DateTime PSDate { get; set; }          
            public long FK_Product { get; set; }         
            public long FK_Stage { get; set; }          
            public long FK_Team { get; set; }        
            public long PSDurationType { get; set; }         
            public Int32 PSDurationPrd { get; set; }          
            public Int32 PSPrllCount { get; set; }        
            public bool PSEmpWise { get; set; }          
            public decimal PSWorkPer { get; set; }          
            public string TransMode { get; set; }          
            public string PSRemarks { get; set; }
            public Int32 SortOrder { get; set; }
            public List<DurationType> DurationType { get; set; }
            public List<InputMaterialDetails> InputMaterialDetails { get; set; }
            public List<OutputProductsDetails> OutputProductsDetails { get; set; }
            public List<ResourceDetails> ResourceDetails { get; set; }
            public List<CommonSearchPopupModel.ImageListView> ImageList { get; set; }
            public long? LastID { get; set; }
          
        }

        public class ProductionStageViewData
        {
            public Int16 SlNo { get; set; }
            public long ID_ProductionStage { get; set; }
            public long FK_ProductionStage { get; set; }
            public DateTime PSDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_ProjectResources { get; set; }
            public decimal Amount { get; set; }
            public string ResourceType { get; set; }

            public long FK_Stage { get; set; }
            public long FK_Team { get; set; }
            public string Product { get; set; }
            public string Team { get; set; }
            public string Stage { get; set; }            
            public Int32 DurationPrd { get; set; }
            public Int32 PrllCount { get; set; }
            public long PSDurationType { get; set; }
            public bool EmpWise { get; set; }
            public decimal WorkPer { get; set; }
            public string Remarks { get; set; }
            public Int64 TotalCount { get; set; }
            public string DurationType { get; set; }
            public decimal Quantity { get; set; }
            public bool PSMStockUpdate { get; set; }
            public string Product_Name { get; set; }
            public long ?LastID { get; set; }
        }

            public class InputMaterialDetails
        {
            public long FK_ProductionStage { get; set; }
            public string PSMMode { get; set; }
            public long FK_Product { get; set; }
            public decimal Quantity { get; set; }
            public bool PSMStockUpdate { get; set; }

        }
        public class OutputProductsDetails
        {
            public long FK_ProductionStage { get; set; }
            public string PSMMode { get; set; }
            public long FK_Product { get; set; }
            public decimal Quantity { get; set; }
            public bool PSMStockUpdate { get; set; }


        }
        public class ResourceDetails
        {
            public long FK_ProductionStage { get; set; }
            public long FK_ProjectResources { get; set; }
            public string Quantity { get; set; }
            public decimal Amount { get; set; }
        }

        public class UpdateProductionStage
        {
            public long ID_ProductionStage { get; set; }
            public DateTime PSDate { get; set; }
            public long FK_Product { get; set; }
            public long FK_Stage { get; set; }
            public long FK_Team { get; set; }
            public long PSDurationType { get; set; }
            public Int32 PSDurationPrd { get; set; }
            public Int32 PSPrllCount { get; set; }
            public bool PSEmpWise { get; set; }
            public decimal PSWorkPer { get; set; }
            public string TransMode { get; set; }
            public string PSRemarks { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int16 UserAction { get; set; }
            public string InputMaterialDetails { get; set; }
            public string OutputProductsDetails { get; set; }

            public string ResourceDetails { get; set; }
            public string ImageList { get; set; }
            public int Debug { get; set; }
            public long FK_ProductionStage { get; set; }
            public long? LastID { get; set; }
        }
        public static string _deleteProcedureName = "ProProductionStageDelete";
        public static string _updateProcedureName = "ProProductionStageUpdate";
        public static string _selectProcedureName = "ProProductionStageSelect";

        public class DeleteProductionStage
        {
            public string TransMode { get; set; }
            public long FK_ProductionStage { get; set; }
            public Int64 Debug { get; set; }         
            public string EntrBy { get; set; }
            public long FK_Reason { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
        }
        public class ProductionStageInfoView
        {
            public Int64 FK_ProductionStage { get; set; }
            public Int64 ReasonID { get; set; }
            public string TransMode { get; set; }

        }

        public class ProductionStageID
        {
            public string TransMode { get; set; }
            public Int64 FK_ProductionStage { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }          
            public string EntrBy { get; set; }
            public long FK_Company { get; set; }
            public long FK_Machine { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Int16 CheckList { get; set; }
        }
        public class Duration
        {
            public Int32 Mode { get; set; }
        }


        public class DurationType
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }
        }

       

        public Output UpdateProductionStageData(UpdateProductionStage input, string companyKey)
        {
            return Common.UpdateTableData<UpdateProductionStage>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteProductionStageData(DeleteProductionStage input, string companyKey)
        {
            return Common.UpdateTableData<DeleteProductionStage>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<ProductionStageViewData> GetProductionStageData(ProductionStageID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStageViewData, ProductionStageID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }

        public APIGetRecordsDynamic<ProductionStageViewData> GetProductionStageInputDetailData(ProductionStageID input, string companyKey)
        {
            return Common.GetDataViaProcedure<ProductionStageViewData, ProductionStageID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);        }

      
        public APIGetRecordsDynamic<DurationType> GetDurationList(Duration input, string companyKey)
        {
            return Common.GetDataViaProcedure<DurationType, Duration>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }

       
    }
}


