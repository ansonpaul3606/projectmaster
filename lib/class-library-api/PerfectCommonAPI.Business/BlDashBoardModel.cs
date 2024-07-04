using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PerfectWebERPAPI.Interface;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using System.Data.Sql;

namespace PerfectWebERPAPI.Business
{
    public class BlDashBoardModel
    {
        
    }
    public class RootTileLeadDashBoardDetails : CommonAPIResponse
    {
        public TileLeadDashBoardDetails TileLeadDashBoardDetails { get; set; }

    }
    public class RootEmployeeWiseTaegetInPercentage:CommonAPIResponse
    {
        public EmployeeWiseTaegetInPercentage EmployeeWiseTaegetInPercentage { get; set; }
    }
    public class RootLeadActivites : CommonAPIResponse
    {
        public LeadActivites LeadActivites { get; set; }
    }
    public class RootLeadstagewiseforcast : CommonAPIResponse
    {
        public Leadstagewiseforcast Leadstagewiseforcast { get; set; }
    }
    public class RootCRMTileDashBoardDetails : CommonAPIResponse
    {
        public CRMTileDashBoardDetails CRMTileDashBoardDetails { get; set; }
    }
    public class RootCRMStagewiseDetails : CommonAPIResponse
    {
        public CRMStagewiseDetails CRMStagewiseDetails { get; set; }
    }
    public class RootCRMcomplaintwises : CommonAPIResponse
    {
        public CRMcomplaintwise CRMcomplaintwise { get; set; }
    }
    public class RootCRMservicewise : CommonAPIResponse
    {
        public CRMservicewise CRMservicewise { get; set; }
    }
    public class RootProjectTileDashBoardDetails : CommonAPIResponse
    {
        public ProjectTileDashBoardDetails ProjectTileDashBoardDetails { get; set; }
    }
    public class RootInventoryMonthlySaleGraph: CommonAPIResponse
    {
        public InventoryMonthlySaleGraph InventoryMonthlySaleGraph { get; set; }
    }
    public class RootInventorySupplierWisePurchase:CommonAPIResponse
    {
        public InventorySupplierWisePurchase InventorySupplierWisePurchase { get; set; }
    }
    public class RootInventoryProductReorderLevel : CommonAPIResponse
    {
        public InventoryProductReorderLevel InventoryProductReorderLevel { get; set; }
    }
    public class RootInventoryTopSupplierList : CommonAPIResponse
    {
        public InventoryTopSupplierList InventoryTopSupplierList { get; set; }
    }
    public class RootInventoryTopSellingItem : CommonAPIResponse
    {
        public InventoryTopSellingItem InventoryTopSellingItem { get; set; }
    }
    public class RootInventoryStockListCategory : CommonAPIResponse
    {
        public InventoryStockListCategory InventoryStockListCategory { get; set; }
    }
    public class RootTop10ProductsinLead:CommonAPIResponse
    {
        public Top10ProductsinLead Top10ProductsinLead { get; set; }
    }
    public class RootLeadSource : CommonAPIResponse
    {
        public LeadSource LeadSource { get; set; }
    }
    public class RootExpenseVSGain : CommonAPIResponse
    {
        public ExpenseVSGain ExpenseVSGain { get; set; }
    }
    public class RootEmployeeWiseConversionTime : CommonAPIResponse
    {
        public EmployeeWiseConversionTime EmployeeWiseConversionTime { get; set; }
    }
    public class RootSalesComparison : CommonAPIResponse
    {
        public SalesComparison SalesComparison { get; set; }
    }
    public class RootAccountTileData : CommonAPIResponse
    {
        public AccountTileData AccountTileData { get; set; }
    }
    public class RootCashBalance : CommonAPIResponse
    {
        public CashBalance CashBalance { get; set; }
    }
    public class RootBankBalance : CommonAPIResponse
    {
        public BankBalance BankBalance { get; set; }
    }
    public class RootExpenseChart : CommonAPIResponse
    {
        public ExpenseChart ExpenseChart { get; set; }
    }
    public class RootSupplierOutstanding : CommonAPIResponse
    {
        public SupplierOutstanding SupplierOutstanding { get; set; }
    }
    public class RootProjectDelayedStatus:CommonAPIResponse
    {
        public ProjectDelayedStatus ProjectDelayedStatus { get; set; }
    }
    public class RootStockValueData:CommonAPIResponse
    {
        public StockValueData StockValueData { get; set; }
    }
    public class RootLeadstagecountwisefrorecast : CommonAPIResponse
    {
        public Leadstagecountwisefrorecast Leadstagecountwisefrorecast { get; set; }
    }
    public class RootDashBoardNameDetails : CommonAPIResponse
    {
        public DashBoardNameDetails DashBoardNameDetails { get; set; }
    }

    public class RootProjectExpenseAnalysis : CommonAPIResponse
    {
        public ProjectExpenseAnalysis ProjectExpenseAnalysis { get; set; }
    }
   
    public class RootCostofMaterialUsageAllocatedandUsed : CommonAPIResponse
    {
        public CostofMaterialUsageAllocatedandUsed CostofMaterialUsageAllocatedandUsed { get; set; }
    }
    public class RootUpcomingStageDueDates : CommonAPIResponse
    {
        public UpcomingStageDueDates UpcomingStageDueDates { get; set; }
    }
    public class RootTotalStagewiseDue : CommonAPIResponse
    {
        public TotalStagewiseDue TotalStagewiseDue { get; set; }
    }
    public class RootTop10Projects : CommonAPIResponse
    {
        public Top10Projects Top10Projects { get; set; }
    }
    public class RootCRMCountofWarrantyPaidandAMC : CommonAPIResponse
    {
        public CRMCountofWarrantyPaidandAMC CRMCountofWarrantyPaidandAMC { get; set; }
    }
    public class RootCRMTop10Products : CommonAPIResponse
    {
        public CRMTop10Products CRMTop10Products { get; set; }
    }
    public class RootCRMChannelWise : CommonAPIResponse
    {
        public CRMChannelWise CRMChannelWise { get; set; }
    }
    public class RootCRMSLAViolationStatus :CommonAPIResponse
    {
        public CRMSLAViolationStatus CRMSLAViolationStatus { get; set; }
    }
    public class RootProductionTileDashBoard : CommonAPIResponse
    {
        public ProductionTileDashBoard ProductionTileDashBoard { get; set; }
    }
    public class RootProductionMaterialShortage : CommonAPIResponse
    {
        public ProductionMaterialShortage ProductionMaterialShortage { get; set; }
    }
    public class RootProductionUpcomingStock : CommonAPIResponse
    {
        public ProductionUpcomingStock ProductionUpcomingStock { get; set; }
    }
    public class RootProductionCompletedProducts : CommonAPIResponse
    {
        public ProductionCompletedProducts ProductionCompletedProducts { get; set; }
    }
    public class RootVehicleDetails : CommonAPIResponse
    {
        public VehicleDetails VehicleDetails { get; set; }
    }
    public class RootOrderTrackingTileDashBoard : CommonAPIResponse
    {
        public OrderTrackingTileDashBoard OrderTrackingTileDashBoard { get; set; }
    }

}
