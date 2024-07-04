using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectWebERPAPI.Interface
{
    public interface IStockTransfer
    {
        string ReqMode { get; set; }
        string Token { get; set; }
        string BankKey { get; set; }
        string SubMode { get; set; }
        string FK_Company { get; set; }
        string FK_BranchCodeUser { get; set; }
        string EntrBy { get; set; }
        string TransMode { get; set; }
        string Name { get; set; }
        string PageIndex { get; set; }
        string PageSize { get; set; }
        string Critrea1 { get; set; }
        string Critrea2 { get; set; }
        string Critrea3 { get; set; }
        string Critrea4 { get; set; }
        string ID { get; set; }
        string Critrea5 { get; set; }
        string Critrea6 { get; set; }
        string ID_StockTransfer { get; set; }
        string TransDate { get; set; }
        string FK_EmployeeFrom { get; set; }
        string FK_EmployeeTo { get; set; }
        string FK_BranchFrom { get; set; }
        string FK_BranchTo { get; set; }
        string FK_DepartmentFrom { get; set; }
        string FK_DepartmentTo { get; set; }
        string STRequest { get; set; }
        string FK_StockRequest { get; set; }
        string EmployeeStockTransferDetails { get; set; }
        string UserAction { get; set; }
        string FK_StockTransfer { get; set; }
        string Detailed { get; set; }
        string Reason { get; set; }
         string ID_User { get; set; }
    }
    public class StockRTEmployeeDetails : CommonAPIR
    {
        public List<StockRTEmployee> StockRTEmployeeList { get; set; }
    }
    public class StockRTEmployee
    {
        public long FK_Employee { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }
    public class StockRTProductDetails : CommonAPIR
    {
        public List<StockRTProduct> StockRTProductList { get; set; }
    }
    public class StockRTProduct
    {
        public long FK_Product { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class StockTransferUpdate
    {
        public string BankKey { get; set; }
        public string Token { get; set; }
        public string UserAction { get; set; }
        public string TransMode { get; set; }
        public string ID_StockTransfer { get; set; }
        public string TransDate { get; set; }
        public string FK_EmployeeFrom { get; set; }
        public string FK_EmployeeTo { get; set; }
        public string FK_BranchFrom { get; set; }
        public string FK_BranchTo { get; set; }
        public string FK_DepartmentFrom { get; set; }
        public string FK_DepartmentTo { get; set; }
        public string STRequest { get; set; }
        public string FK_Company { get; set; }
        public string FK_BranchCodeUser { get; set; }
        public string EntrBy { get; set; }
        public string FK_Machine { get; set; }
        public string LastID { get; set; }
        public string FK_StockRequest { get; set; }
        public List<SubEmployeeStockTransferDetailsView> EmployeeStockTransferDetails { get; set; }
        public string EmployeesStockTransferDetails { get; set; }
    }
    public class SubEmployeeStockTransferDetailsView
    {
        public long ID_Product { get; set; }
        public string Quantity { get; set; }
        public long ID_Stock { get; set; }
        public string QuantityStandBy { get; set; }
        public long? StockMode { get; set; }
    }
    public class UpdateStockTransfer : CommonAPIR
    {
        public long FK_StockTransfer { get; set; }
    }
   public class StockRequest:CommonAPIR
    {
        public List<StockRequestList> StockRequestListData { get; set; }
    }
   public class StockRequestList
    {
        public long StockTransferID { get; set; }
        public long BranchID { get; set; }
        public string BranchName { get; set; }
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public long BranchIDTo { get; set; }
        public string BranchNameTo { get; set; }
        public long DepartmentIDTo { get; set; }
        public string DepartmentNameTo { get; set; }
        public long EmployeeIDTo { get; set; }
        public string EmployeeNameTo { get; set; }
        public string TransDate { get; set; }    
        public long Transfered { get; set; }
        public long Approved { get; set; }
        public long FK_StockRequest { get; set; }
    }
    public class StockRequestProduct : CommonAPIR
    {
        public List<StockRequestProductList> StockRequestProductListData { get; set; }
    }
    public class StockRequestProductList
    {
        public long ID_StockTransferDetails { get; set; }
        public long FK_StockTransfer { get; set; }
        public long ID_Product { get; set; }
        public string Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityStandBy { get; set; }
        public long ID_Stock { get; set; }
        public long StockMode { get; set; }       
    }
    public class StockRequestInTransfer : CommonAPIR
    {
        public List<StockRequestListInTransfer> StockRequestList { get; set; }
    }
    public class StockRequestListInTransfer
    {
        public string TransDate { get; set; }
        public long FK_StockTransfer { get; set; }
        public long BranchID { get; set; }
        public string BranchName { get; set; }
        public long DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public long EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public long BranchIDTo { get; set; }
        public string BranchNameTo { get; set; }
        public long DepartmentIDTo { get; set; }
        public string DepartmentNameTo { get; set; }
        public long EmployeeIDTo { get; set; }
        public string EmployeeNameTo { get; set; }  
       
    }
    public class StockSTProductDetails : CommonAPIR
    {
        public List<StockSTProduct> StockSTProductList { get; set; }
    }
    public class StockSTProduct
    {
        public long FK_Product { get; set; }       
        public string Name { get; set; }
        public decimal PurRate { get; set; }
        public decimal ProductAmount { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal Stock { get; set; }
        public decimal StandbyStock { get; set; }
        public decimal StandbyQuantity { get; set; }
        public long ID_Stock { get; set; }        
    }
    public class StockRTDeleteDetails : CommonAPIR
    {
        public long FK_StockTransfer { get; set; }
    }
}
