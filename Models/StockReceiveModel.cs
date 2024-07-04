using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectWebERP.Models
{
    public class StockReceiveModel
    {
        public class StockReceiveView
        {
            public List<Branch> BranchList { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<BranchTo> BranchListTo { get; set; }
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public List<Unit> UnitList { get; set; }
            public long? FK_StockTransfer { get; set; }
            public string TransMode { get; set; }
            public DateTime TransDate { get; set; }
            public string TranDate { get; set; }
            public long BranchID { get; set; }
            public long DepartmentID { get; set; }
            public long BranchIDTo { get; set; }
            public long DepartmentIDTo { get; set; }
            public long EmployeeID { get; set; }
            public long EmployeeIDTo { get; set; }
            public List<SubEmployeeStockTransferDetailsTable> SubEmployeeStockTransfers { get; set; }
            public long? LastID { get; set; }
            
        }
        #region[Dropdown]
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class Branch
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }

        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class BranchTo
        {
            public Int32 BranchIDTo { get; set; }
            public string BranchNameTo { get; set; }

        }
        public class DepartmentTo
        {
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }

        }
        #endregion
        public class SearchInput
        {
            public long DepartmentIDTo { get; set; }
            public long BranchIDTo { get; set; }
            public long? EmployeeIDTo { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Company { get; set; }
        }

        public class StockTransferDataView
        {

            public long SlNo { get; set; }
            public long StockTransferID { get; set; }
            public long? FK_StockTransfer { get; set; }
            public DateTime TransDate { get; set; }
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public Int32 BranchIDTo { get; set; }
            public string BranchNameTo { get; set; }
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }
            public Int32? EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public Int32? EmployeeIDTo { get; set; }
            public string EmployeeNameTo { get; set; }
            public List<SubEmployeeStockTransferDetailsTable> SubEmployeeStockTransfers { get; set; }
            public long TotalCount { get; set; }
        }

        public class SubEmployeeStockTransferDetailsTable
        {
            public long ID_SubEmployeeStockTransfer { get; set; }
            [Required(ErrorMessage = "Please Enter Quantity")]
            public string Quantity { get; set; }
            [Required(ErrorMessage = "Please Enter Stand By Quantity")]
            public string QuantityStandBy { get; set; }
            [Range(1, long.MaxValue, ErrorMessage = "Select Product")]
            public long ID_Product { get; set; }
            public string Product { get; set; }

            public long ID_Stock { get; set; }
            public long? StockMode { get; set; }
            public decimal Stock { get; set; }
            public string StandbyQuantity { get; set; }
            public long ID_Unit { get; set; }
        }

        public class EmployeeStockTransferSubView
        {
            public long FK_StockTransfer { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }
        }

        public class UpdateStockReceive
        {
            public long? ID_StockTransfer { get; set; }
            public DateTime TransDate { get; set; }
            public long FK_EmployeeFrom { get; set; }
            public long FK_EmployeeTo { get; set; }
            public long FK_BranchFrom { get; set; }
            public long FK_BranchTo { get; set; }
            public long FK_DepartmentFrom { get; set; }
            public long FK_DepartmentTo { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public Int64? LastID { get; set; } = 0;
            public string EmployeeStockReceiveDetails { get; set; }
            public long Debug { get; set; }
        }
        public APIGetRecordsDynamic<StockTransferDataView> GetSearchTransferdata(SearchInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<StockTransferDataView, SearchInput>(companyKey: companyKey, procedureName: "ProSelectStockTransferReqData", parameter: input);
        }
        public APIGetRecordsDynamicdn<dynamic> Fillproduct(EmployeeStockTransferSubView input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<EmployeeStockTransferSubView>(companyKey: companyKey, procedureName: "ProStockTransferSelect", parameter: input);
        }
        public Output UpdateEmployeeStockReceiveData(UpdateStockReceive input, string companyKey)
        {
            return Common.UpdateTableData<UpdateStockReceive>(parameter: input, companyKey: companyKey, procedureName: "ProStockReceiveUpdate");
        }
    }
}