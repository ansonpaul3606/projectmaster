using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Interface;
using System.Data;
using PerfectWebERPAPI.DataAccess;

namespace PerfectWebERPAPI.Business
{
    public class  BlDashBoard:IDashBoard
    {
        #region Variable
        private string _ReqMode { get; set; }
        private string _TransDate { get; set; }
        private string _FK_Company { get; set; }
        private string _BankKey { get; set; }
        private string _FK_Employee { get; set; }
        private string _FK_Departement { get; set; }
        private string _FK_Branch { get; set; }
        private string _FK_BranchCodeUser { get; set; }
        private string _EntrBy { get; set; }
        private string _DashMode { get; set; }
        private string _DashType { get; set; }
        private string _FK_Module { get; set; }
        private string _ID_User { get; set; }
        private string _Token { get; set; }
        private string _ID_TokenUser { get; set; }
        #endregion
        #region Constructor
        public BlDashBoard()
        {
            Initialize();
        }
        #endregion
        #region Initialize
        public void Initialize()
        {
            _TransDate = string.Empty;
            _ReqMode = string.Empty;
            _FK_Company = string.Empty;          
            _BankKey = string.Empty;
            _FK_Employee = string.Empty;
            _FK_Departement = string.Empty;
            _FK_Branch = string.Empty;
            _FK_BranchCodeUser = string.Empty;
            _EntrBy = string.Empty;
            _DashMode = string.Empty;
            _DashType = string.Empty;
            _FK_Module = string.Empty;
            _ID_User = string.Empty;
            _Token = string.Empty;
            _ID_TokenUser = string.Empty;

        }
        #endregion
        #region Getters And Setters
        public string ID_TokenUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_TokenUser); }
            set { _ID_TokenUser = value; }
        }
        public string Token
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_Token); }
            set { _Token = value; }
        }
        public string ID_User
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ID_User); }
            set { _ID_User = value; }
        }
        public string FK_Module
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Module); }
            set { _FK_Module = value; }
        }
        public string TransDate
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_TransDate); }
            set { _TransDate = value; }
        }
        public string ReqMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_ReqMode); }
            set { _ReqMode = value; }
        }
        public string FK_Company
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Company); }
            set { _FK_Company = value; }
        }
        public string BankKey
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_BankKey); }
            set { _BankKey = value; }
        }
        public string FK_Employee
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Employee); }
            set { _FK_Employee = value; }
        }
        public string FK_Department
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Departement); }
            set { _FK_Departement = value; }
        }
        public string FK_Branch
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_Branch); }
            set { _FK_Branch = value; }
        }
        public string FK_BranchCodeUser
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_FK_BranchCodeUser); }
            set { _FK_BranchCodeUser = value; }
        }
        public string DashMode
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DashMode); }
            set { _DashMode = value; }
        }
        public string DashType
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_DashType); }
            set { _DashType = value; }
        }
        public string EntrBy
        {
            get { return BlDecryptFormat.DecryptDataForAuthCode(_EntrBy); }
            set { _EntrBy = value; }
        }
        #endregion
        #region Data Access Function
        public TileLeadDashBoardDetails BlTileLeadDashBoardDetails(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertTileLeadDashBoardDetails(dt, objbl.DashMode, objbl.DashType);

        }
        public EmployeeWiseTaegetInPercentage BlEmployeeWiseTaegetInPercentage(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertEmployeeWiseTaegetInPercentage(dt, objbl.DashMode, objbl.DashType);

        }
        public LeadActivites BlLeadActivites(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertLeadActivites(dt, objbl.DashMode, objbl.DashType);

        }
        public Leadstagewiseforcast BlLeadstagewiseforcast(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertLeadstagewiseforcast(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMTileDashBoardDetails BlCRMTileDashBoardDetails(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMTileDashBoardDetails(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMStagewiseDetails BlCRMStagewiseDetails(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMStagewiseDetails(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMcomplaintwise BlCRMcomplaintwise(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMcomplaintwise(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMservicewise BlCRMservicewise(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMservicewise(dt, objbl.DashMode, objbl.DashType);

        }
        public ProjectTileDashBoardDetails BlProjectTileDashBoardDetails(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProjectTileDashBoardDetails(dt, objbl.DashMode, objbl.DashType);

        }
        public InventoryMonthlySaleGraph BlInventoryMonthlySaleGraph(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertInventoryMonthlySaleGraph(dt, objbl.DashMode, objbl.DashType);

        }
        public InventorySupplierWisePurchase BlInventorySupplierWisePurchase(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertInventorySupplierWisePurchase(dt, objbl.DashMode, objbl.DashType);

        }
        public InventoryProductReorderLevel BlInventoryProductReorderLevel(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertInventoryProductReorderLevel(dt, objbl.DashMode, objbl.DashType);

        }
        public InventoryTopSupplierList BlInventoryTopSupplierList(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertInventoryTopSupplierList(dt, objbl.DashMode, objbl.DashType);

        }
        public InventoryTopSellingItem BlInventoryTopSellingItem(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertInventoryTopSellingItem(dt, objbl.DashMode, objbl.DashType);

        }
        public InventoryStockListCategory BlInventoryStockListCategory(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertInventoryStockListCategory(dt, objbl.DashMode, objbl.DashType);

        }
        public Top10ProductsinLead BlTop10ProductsinLead(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertTop10ProductsinLead(dt, objbl.DashMode, objbl.DashType);

        }
        public LeadSource BlLeadSource(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertLeadSource(dt, objbl.DashMode, objbl.DashType);

        }
        public ExpenseVSGain BlExpenseVSGain(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertExpenseVSGain(dt, objbl.DashMode, objbl.DashType);

        }
        public EmployeeWiseConversionTime BlEmployeeWiseConversionTime(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertEmployeeWiseConversionTime(dt, objbl.DashMode, objbl.DashType);

        }
        public SalesComparison BlSalesComparison(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertSalesComparison(dt, objbl.DashMode, objbl.DashType);

        }
        public AccountTileData BlAccountTileData(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertAccountTileData(dt, objbl.DashMode, objbl.DashType);

        }
        public CashBalance BlCashBalance(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCashBalance(dt, objbl.DashMode, objbl.DashType);

        }
        public BankBalance BlBankBalance(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertBankBalance(dt, objbl.DashMode, objbl.DashType);

        }
        public ExpenseChart BlExpenseChart(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertExpenseChart(dt, objbl.DashMode, objbl.DashType);

        }
        public SupplierOutstanding BlSupplierOutstanding(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertSupplierOutstanding(dt, objbl.DashMode, objbl.DashType);

        }
        public ProjectDelayedStatus BlProjectDelayedStatus(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProjectDelayedStatus(dt, objbl.DashMode, objbl.DashType);

        }
        public StockValueData BlStockValueData(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertStockValueData(dt, objbl.DashMode, objbl.DashType);

        }
        public Leadstagecountwisefrorecast BlLeadstagecountwisefrorecast(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertLeadstagecountwisefrorecast(dt, objbl.DashMode, objbl.DashType);

        }
        public DashBoardNameDetails BlDashBoardNameDetails(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertDashBoardNameDetails(dt, objbl.FK_Module);

        }
        public ProjectExpenseAnalysis BlProjectExpenseAnalysis(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProjectExpenseAnalysis(dt, objbl.DashMode, objbl.DashType);

        }
        public CostofMaterialUsageAllocatedandUsed BlCostofMaterialUsageAllocatedandUsed(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCostofMaterialUsageAllocatedandUsed(dt, objbl.DashMode, objbl.DashType);

        }
        public UpcomingStageDueDates BlUpcomingStageDueDates(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertUpcomingStageDueDates(dt, objbl.DashMode, objbl.DashType);

        }
        public TotalStagewiseDue BlTotalStagewiseDue(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertTotalStagewiseDue(dt, objbl.DashMode, objbl.DashType);

        }
        public Top10Projects BlTop10Projects(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertTop10Projects(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMCountofWarrantyPaidandAMC BlCRMCountofWarrantyPaidandAMC(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMCountofWarrantyPaidandAMC(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMTop10Products BlCRMTop10Products(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMTop10Products(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMChannelWise BlCRMChannelWise(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMChannelWise(dt, objbl.DashMode, objbl.DashType);

        }
        public CRMSLAViolationStatus BlCRMSLAViolationStatus(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertCRMSLAViolationStatus(dt, objbl.DashMode, objbl.DashType);

        }
        public ProductionTileDashBoard BlProductionTileDashBoard(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProductionTileDashBoard(dt, objbl.DashMode, objbl.DashType);

        }
        public ProductionMaterialShortage BlProductionMaterialShortage(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProductionMaterialShortage(dt, objbl.DashMode, objbl.DashType);

        }
        public ProductionUpcomingStock BlProductionUpcomingStock(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProductionUpcomingStock(dt, objbl.DashMode, objbl.DashType);

        }
        public ProductionCompletedProducts BlProductionCompletedProducts(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertProductionCompletedProducts(dt, objbl.DashMode, objbl.DashType);

        }
        public VehicleDetails BlVehicleDetails(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertVehicleDetails(dt, objbl.DashMode, objbl.DashType);

        }
        public OrderTrackingTileDashBoard BlOrderTrackingTileDashBoard(BlDashBoard objbl)
        {

            DalDashBoard objDal = new DalDashBoard();
            DataTable dt = objDal.DalDashBoardDetails(objbl);
            objDal = null;
            return BlDashBoardFormat.ConvertOrderTrackingTileDashBoard(dt, objbl.DashMode, objbl.DashType);

        }
        #endregion
    }
}
