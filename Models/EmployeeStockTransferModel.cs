/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 19/03/2022
Purpose		: EmployeeStockTransfer
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
    public class EmployeeStockTransferModel
    {

        public class EmployeeStockTransferView
        {
            public long SlNo { get; set; }
            public long StockTransferID { get; set; }
            public long? FK_StockTransfer { get; set; }
            public DateTime TransDate { get; set; }
            public string TranDate { get; set; }
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
            public Int32 BranchIDTo { get; set; }
            public string BranchNameTo { get; set; }
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }
          
            public byte ModeTR { get; set; }
           
            public Int32 ?EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public Int32?EmployeeIDTo { get; set; }
            public string EmployeeNameTo { get; set; }
            public List<SubEmployeeStockTransferDetailsView> SubEmployeeStockTransfers { get; set; }
            public long TotalCount { get; set; }
            public long ReasonID { get; set; }
            public string TransMode { get; set; }
           
            public Int64? LastID { get; set; }
            public string EnteredBy { get; set; }
        }

        public class SubEmployeeStockTransferDetailsView
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
            public long ?StockMode { get; set; }

            public decimal Stock { get; set; }

            public string StandbyQuantity { get; set; }
            //public string ReqQuantity { get; set; }
            //public string ReqQtyStandBy { get; set; }
            public string StandbyStock { get; set; }
            public long ID_Unit { get; set; }
        }
        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }
           
        }

        public  class DepartmentTo
        {
            public Int32 DepartmentIDTo { get; set; }
            public string DepartmentNameTo { get; set; }

        }
        public class BranchTo
        {
            public Int32 BranchIDTo { get; set; }
            public string BranchNameTo { get; set; }

        }
        
        public class Branch
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }
          
        }
        public class Products
        {
            public Int32 ID_Product { get; set; }
            public string Product { get; set; }
            public string Mode { get; set; }
            public decimal Stock { get; set; }
            public long ID_Stock { get; set; }
            public decimal StandbyQuantity { get; set; }
        }
        public class Employee
        {
            public Int32 EmployeeID { get; set; }
            public string EmployeeName { get; set; }
        }
        public class EmployeeTo
        {
            public Int32 EmployeeIDTo { get; set; }
            public string EmployeeNameTo { get; set; }
        }
        public class EmployeeBaseInfo
        {
            public Int32 EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeDesignation { get; set; }
            public string EmployeeDepartment { get; set; }
        }
        public class ProductBaseInfo
        {
            public Int32 ID_Product { get; set; }
            public string Product { get; set; }
            public string  Mode { get; set; }
            public decimal Stock { get; set; }
            public long StockID { get; set; }
            public decimal StandbyQuantity{ get; set; }



        }
      
        public class EmployeeStockTransferViewList
        {
            public List<Products> ProductList { get; set; }
            public List<Employee> EmployeeList { get; set; }
            public List<EmployeeTo> EmployeeListTo { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<DepartmentTo> DepartmentListTo { get; set; }
            public List<Branch> BranchList { get; set; }
            public List<BranchTo> BranchListTo { get; set; }
            public Int64? LastID { get; set; }
            public List<Unit> UnitList { get; set; }
        }
        public class Unit
        {
            public long ID_Unit { get; set; }
            public string UnitName { get; set; }
            public decimal UnitCount { get; set; }
        }
        public class ProductWiseUnit
        {
            public string Mode { get; set; }
            public string TransMode { get; set; }
            public long ProductID { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Company { get; set; }
            public Int64 FK_Machine { get; set; }
            public long FK_Transaction { get; set; }
            public int SalesReturnMode { get; set; }
        }
        public class ProductWiseUnitVue
        {
            public string TransMode { get; set; }
            public long ProductID { get; set; }
        }


        public class UpdateEmployeeStockTransfer
        {
            public long ID_StockTransfer { get; set; }
            public DateTime TransDate { get; set; }
            public long FK_EmployeeFrom { get; set; }
            public long FK_EmployeeTo { get; set; }
            public long FK_BranchFrom { get; set; }
            public long FK_BranchTo { get; set; }
            public long FK_DepartmentFrom { get; set; }
            public long FK_DepartmentTo { get; set; }
            public byte STRequest { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public byte UserAction { get; set; }
            public string TransMode { get; set; }
            public Int64? LastID { get; set; } = 0;
            public long? FK_StockRequest { get; set; }
            public string EmployeeStockTransferDetails { get; set; }
        }
        public static string _deleteProcedureName = "ProStockTransferDelete";
        public static string _updateProcedureName = "ProStockTransferUpdate";
        public static string _selectProcedureName = "ProStockTransferSelect";
        public static string _selectSubtableSubEmployeeStockTransfer = "ProEmployeeStockTransferDetailsSelect";

        public class DeleteEmployeeStockTransfer
        {
            public long FK_StockTransfer { get; set; }
            public string TransMode { get; set; }
            public long FK_Company { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long Debug { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public long FK_Reason { get; set; }
        }

        public class EmployeeStockTransferInfoView
        {
            public long StockTransferID { get; set; }
            //    public long ID_ServiceMappingDetails { get; set; }
            public string TransMode { get; set; }
            public long ReasonID { get; set; }
        }
        public class EmployeeStockTransferID
        {
            public Int64 FK_StockTransfer { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }
        }

        public class StockTransferID
        {
            public Int64 FK_Department { get; set; }
            public Int64 FK_Branch { get; set; }
            public Int64? FK_Employee { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
        }
        public class SearchInput
        {
            public Int32 DepartmentID { get; set; }
            public Int32 ?DepartmentIDTo { get; set; }
            public Int32 ?BranchID { get; set; }
            public Int32 ?BranchIDTo { get; set; }
            public Int32? EmployeeID { get; set; }
            public Int32? EmployeeIDTo { get; set; }
            public string TransMode { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
        }
        public Output UpdateEmployeeStockTransferData(UpdateEmployeeStockTransfer input, string companyKey)
        {
            return Common.UpdateTableData<UpdateEmployeeStockTransfer>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteEmployeeStockTransferData(DeleteEmployeeStockTransfer input, string companyKey)
        {
            return Common.UpdateTableData<DeleteEmployeeStockTransfer>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<EmployeeStockTransferView> GetEmployeeStockTransferData(EmployeeStockTransferID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeStockTransferView, EmployeeStockTransferID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EmployeeStockTransferView> GetStockTransferproductData(EmployeeStockTransferID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeStockTransferView, EmployeeStockTransferID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<EmployeeStockTransferView> GetSearchRequestdata(SearchInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeStockTransferView, SearchInput>(companyKey: companyKey, procedureName: "ProFindStockTransferRequest", parameter: input);
        }
        public APIGetRecordsDynamic<SubEmployeeStockTransferDetailsView> GetSearchRequestDetailsdata(SearchInput input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubEmployeeStockTransferDetailsView, SearchInput>(companyKey: companyKey, procedureName: "ProSearchdata", parameter: input);
        }
        public static string _selectProcedureNameStockprd = "ProStockProductSelect";
        public APIGetRecordsDynamic<EmployeeStockTransferView> GetStockTransferData(StockTransferID input, string companyKey)
        {
            return Common.GetDataViaProcedure<EmployeeStockTransferView, StockTransferID>(companyKey: companyKey, procedureName: _selectProcedureNameStockprd, parameter: input);
        }
        public class EmployeeStockTransferSubSelect
        {
            public Int64 FK_StockTransfer { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string Name { get; set; }
            public Byte Detailed { get; set; }


        }
        public APIGetRecordsDynamic<SubEmployeeStockTransferDetailsView> GetSubTableEmployeeStockTransferData(EmployeeStockTransferSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedure<SubEmployeeStockTransferDetailsView, EmployeeStockTransferSubSelect>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);

        }

        public APIGetRecordsDynamicdn<dynamic> Fillproduct(EmployeeStockTransferSubSelect input, string companyKey)
        {
            return Common.GetDataViaProcedureDynamic<EmployeeStockTransferSubSelect>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);


        }
        public APIGetRecordsDynamic<Unit> GetMultiunit(ProductWiseUnit input, string companyKey)
        {
            return Common.GetDataViaProcedure<Unit, ProductWiseUnit>(companyKey: companyKey, procedureName: "ProProductWiseMultiUnitSelect", parameter: input);
        }
    }
}


