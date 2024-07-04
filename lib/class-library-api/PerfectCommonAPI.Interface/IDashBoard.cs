using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IDashBoard
    {
        string ReqMode { get; set; }
        string BankKey { get; set; }
        string FK_Employee { get; set; }
        string TransDate { get; set; }
        string FK_Department { get; set; }
        string FK_Branch { get; set; }
        string FK_BranchCodeUser { get; set; }
        string EntrBy { get; set; }
        string FK_Company { get; set; }
        string DashMode { get; set; }
        string DashType { get; set; }
        string FK_Module { get; set; }
        string ID_User { get; set; }
        string Token { get; set; }
        string ID_TokenUser { get; set; }
    }
    #region Response Interface Model Objects
    public class MainDashBoardDetails : CommonAPIR
    {
        public string DashBoardName { get; set; }
        public string DashBoardData { get; set; }
        public string Reamrk { get; set; }


    }//LeadTileDashBoardDetails
    public class TileLeadDashBoardDetails : CommonAPIR
    {
        public string ChartName { get; set; }
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<LeadTileData> LeadTileData { get; set; }
    }
    public class LeadTileData
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class EmployeeWiseTaegetInPercentage : CommonAPIR
    {
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public string Reamrk { get; set; }
        public List<EmployeeWiseTaegetDetails> EmployeeWiseTaegetDetails { get; set; }
    }
    public class EmployeeWiseTaegetDetails
    {
        public string EmpFName { get; set; }
        public string ActualPercentage { get; set; }
        public string ActualAmount { get; set; }
        public string TargetAmount { get; set; }
    }
    public class LeadActivites : CommonAPIR
    {
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public string Reamrk { get; set; }
        public List<LeadActivitesList> LeadActivitesList { get; set; }
    }
    public class LeadActivitesList
    {
        public string ActionName { get; set; }
        public string Closed { get; set; }
        public string Total { get; set; }

    }
    public class Leadstagewiseforcast : CommonAPIR
    {
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public string Reamrk { get; set; }
        public List<LeadstagewiseforcastList> LeadstagewiseforcastList { get; set; }
    }
    public class LeadstagewiseforcastList
    {
        public string StatusName { get; set; }
        public string Amount { get; set; }
        public string Percentage { get; set; }

    }
    public class CRMTileDashBoardDetails : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string ChartName { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMTileDashBoardDetailsList> CRMTileDashBoardDetailsList { get; set; }
    }
    public class CRMTileDashBoardDetailsList
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class CRMStagewiseDetails : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMStagewiseDetailsList> CRMStagewiseDetailsList { get; set; }
    }
    public class CRMStagewiseDetailsList
    {
        public string StatusName { get; set; }
        public string StatusCount { get; set; }
    }
    public class CRMcomplaintwise : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMcomplaintwiseList> CRMcomplaintwiseList { get; set; }
    }
    public class CRMcomplaintwiseList
    {
        public string ComplaintName { get; set; }
        public string ComplaintCount { get; set; }
    }
    public class CRMservicewise : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMservicewiseList> CRMservicewiseList { get; set; }
    }
    public class CRMservicewiseList
    {
        public string ServiceName { get; set; }
        public string ServiceCount { get; set; }
       
    }
    public class ProjectTileDashBoardDetails : CommonAPIR
    {
        public string ChartName { get; set; }
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProjectTileDashBoardDetailsList> ProjectTileDashBoardDetailsList { get; set; }
    }
    public class ProjectTileDashBoardDetailsList
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class InventoryMonthlySaleGraph : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<InventoryMonthlySaleGraphList> InventoryMonthlySaleGraphList { get; set; }
    }
    public class InventoryMonthlySaleGraphList
    {
        public string Month { get; set; }
        public string Amount { get; set; }
    }
    public class InventorySupplierWisePurchase : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<InventorySupplierWisePurchaseList> InventorySupplierWisePurchaseList { get; set; }
    }
    public class InventorySupplierWisePurchaseList
    {
        public string SuppName { get; set; }
        public string Amount { get; set; }
    }
    public class InventoryProductReorderLevel : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<InventoryProductReorderLevelList> InventoryProductReorderLevelList { get; set; }
    }
    public class InventoryProductReorderLevelList
    {
        public string ProdName { get; set; }
        public string ReorderLevel { get; set; }
        public string CurrentQuantity { get; set; }
    }
    public class InventoryTopSupplierList : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<InventoryTopSupplierListDetails> InventoryTopSupplierListDetails { get; set; }
    }
    public class InventoryTopSupplierListDetails
    {
        public string SuppName { get; set; }
        public string Amount { get; set; }

    }
    public class InventoryTopSellingItem : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<InventoryTopSellingItemList> InventoryTopSellingItemList { get; set; }
    }
    public class InventoryTopSellingItemList
    {
        public string Product { get; set; }
        public string Count { get; set; }

    }
    public class InventoryStockListCategory : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<InventoryStockListCategoryDetails> InventoryStockListCategoryDetails { get; set; }
    }
    public class InventoryStockListCategoryDetails
    {
        public string CatName { get; set; }
        public string Count { get; set; }

    }
    public class Top10ProductsinLead : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<Top10ProductsinLeadlist> Top10ProductsinLeadlist { get; set; }

    }
    public class Top10ProductsinLeadlist
    {
        public string Productname { get; set; }
        public string TotalCount { get; set; }
    }
    public class LeadSource : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<LeadSourceList> LeadSourceList { get; set; }

    }
    public class LeadSourceList
    {
        public string LeadFrom { get; set; }
        public string TotalCount { get; set; }
        public string TotalPercentage { get; set; }

    }
    public class ExpenseVSGain : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ExpenseVSGainList> ExpenseVSGainList { get; set; }
    }
    public class ExpenseVSGainList
    {
        public string MediaName { get; set; }
        public string MediaAmount { get; set; }
        public string LeadAmount { get; set; }
    }
    public class EmployeeWiseConversionTime : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<EmployeeWiseConversionTimeList> EmployeeWiseConversionTimeList { get; set; }
    }
    public class EmployeeWiseConversionTimeList
    {
        public string EmployeeName { get; set; }
        public string Conversion { get; set; }
    }
    public class SalesComparison : CommonAPIR
    {

        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<SalesComparisonData> SalesComparisonData { get; set; }
    }
    public class SalesComparisonData
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class AccountTileData : CommonAPIR
    {
        public string ChartName { get; set; }
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<AccountTileDataList> AccountTileDataList { get; set; }
    }
    public class AccountTileDataList
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class CashBalance : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CashBalanceList> CashBalanceList { get; set; }
    }
    public class CashBalanceList
    {
        public string BranchName { get; set; }
        public string CashBalance { get; set; }
    }
    public class BankBalance : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<BankBalanceList> BankBalanceList { get; set; }
    }
    public class BankBalanceList
    {
        public string BankAccount { get; set; }
        public string Balance { get; set; }
    }
    public class ExpenseChart : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ExpenseChartList> ExpenseChartList { get; set; }
    }
    public class ExpenseChartList
    {
        public string Branch { get; set; }
        public string ExpenseAmount { get; set; }
    }
    public class SupplierOutstanding : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<SupplierOutstandingList> SupplierOutstandingList { get; set; }
    }
    public class SupplierOutstandingList
    {
        public string Supplier { get; set; }
        public string Amount { get; set; }
    }
    public class ProjectDelayedStatus : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProjectDelayedStatusList> ProjectDelayedStatusList { get; set; }
    }
    public class ProjectDelayedStatusList
    {
        public string Project { get; set; }
        public string ActualPeriod { get; set; }
        public string CurrentPeriod { get; set; }
    }
    public class StockValueData : CommonAPIR 
    {
        public string Reamrk { get; set; }
        public string ChartName { get; set; }
        public string StockValue { get; set; }
    }
    public class Leadstagecountwisefrorecast : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<LeadstagecountwisefrorecastData> LeadstagecountwisefrorecastData { get; set; }
    }
    public class LeadstagecountwisefrorecastData
    {
        public string StageName { get; set; }
        public string TotalCount { get; set; }

    }
    public class DashBoardNameDetails : CommonAPIR
    {
        public List<DashBoardNameDetailsList> DashBoardNameDetailsList { get; set; }
    }
    public class DashBoardNameDetailsList
    {
        public string ModuleId { get; set; }
        public string DashBoardName { get; set; }
        public string DashMode { get; set; }
        public string DashType { get; set; }
    }
    public class ProjectExpenseAnalysis : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProjectExpenseAnalysisList> ProjectExpenseAnalysisList { get; set; }
    }
    public class ProjectExpenseAnalysisList
    {
        public string Project { get; set; }
        public string ProjectAmount { get; set; }
        public string Expense { get; set; }

    }
    public class CostofMaterialUsageAllocatedandUsed : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<PCostofMaterialUsageAllocatedandUsedList> PCostofMaterialUsageAllocatedandUsedList { get; set; }
    }
    public class PCostofMaterialUsageAllocatedandUsedList
    {
        public string Project { get; set; }
        public string Allocated { get; set; }
        public string Usage { get; set; }

    }
    public class UpcomingStageDueDates : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<PUpcomingStageDueDatesList> PUpcomingStageDueDatesList { get; set; }
    }
    public class PUpcomingStageDueDatesList
    {
        public string Project { get; set; }
        public string Stages { get; set; }
        public string DueDate { get; set; }

    }
    public class TotalStagewiseDue : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<TotalStagewiseDueList> TotalStagewiseDueList { get; set; }
    }
    public class TotalStagewiseDueList
    {
        public string Stages { get; set; }
        public string TotalCount { get; set; }
        public string TotalPercentage { get; set; }

    }
    public class Top10Projects : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<Top10ProjectsList> Top10ProjectsList { get; set; }
    }
    public class Top10ProjectsList
    {
        public string Project { get; set; }
        public string Amount { get; set; }

    }
    public class CRMCountofWarrantyPaidandAMC : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMCountofWarrantyPaidandAMCList> CRMCountofWarrantyPaidandAMCList { get; set; }
    }
    public class CRMCountofWarrantyPaidandAMCList
    {
        public string ItemName { get; set; }
        public string TotalCount { get; set; }
    }
    public class CRMTop10Products : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMTop10ProductsList> CRMTop10ProductsList { get; set; }
    }
    public class CRMTop10ProductsList
    {
        public string ProductName { get; set; }
        public string Count { get; set; }
    }
    public class CRMChannelWise : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMChannelWiseList> CRMChannelWiseList { get; set; }
    }
    public class CRMChannelWiseList
    {
        public string ChannelName { get; set; }
        public string ChannelCount { get; set; }
    }
    public class CRMSLAViolationStatus:CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<CRMSLAViolationStatusList> CRMSLAViolationStatusList { get; set; }
    }
    public class CRMSLAViolationStatusList
    {
        public string StatusName { get; set; }
        public string Count { get; set; }
    }
    public class ProductionTileDashBoard:CommonAPIR
    {
        public string ChartName { get; set; }
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProductionTileDashBoardList> ProductionTileDashBoardList { get; set; }
    }
    public class ProductionTileDashBoardList
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
    public class ProductionUpcomingStock:CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProductionUpcomingStockList> ProductionUpcomingStockList { get; set; }

    }
    public class ProductionUpcomingStockList
    {
        public string Product { get; set; }
        public string Quantity { get; set; }
    }
    public class ProductionMaterialShortage : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProductionMaterialShortageList> ProductionMaterialShortageList { get; set; }

    }
    public class ProductionMaterialShortageList
    {
        public string Product { get; set; }
        public string ActualQuantity { get; set; }
        public string ShortageQuantity { get; set; }
    }
    public class ProductionCompletedProducts : CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<ProductionCompletedProductsList> ProductionCompletedProductsList { get; set; }

    }
    public class ProductionCompletedProductsList
    {
        public string Product { get; set; }
        public string Quantity { get; set; }
    }
    public class VehicleDetails: CommonAPIR
    {
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<VehicleDetailsList> VehicleDetailsList { get; set; }

    }
    public class VehicleDetailsList
    {
        public string Vehicle { get; set; }
        public string Pickup{ get; set; }
        public string Delivery{ get; set; }
    }
    public class OrderTrackingTileDashBoard : CommonAPIR
    {
        public string ChartName { get; set; }
        public string Reamrk { get; set; }
        public string XAxis { get; set; }
        public string YAxis { get; set; }
        public List<OrderTrackingTileDashBoardList> OrderTrackingTileDashBoardList { get; set; }
        public List<PickUpTileData> PickUpTileData { get; set; }


    }
    public class PickUpTileData
    {
        public string Label { get; set; }
        public string Value { get; set; }

    }
    public class OrderTrackingTileDashBoardList
    {
        public string Pending { get; set; }
        public string Processing { get; set; }
        public string Completed { get; set; }
      
    }
    #endregion
}
