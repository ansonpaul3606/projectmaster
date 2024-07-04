/*----------------------------------------------------------------------
Created By	: NIJI TJ
Created On	: 28/02/2023
Purpose		: StockConversion
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
    public class StockConversionModel
    {
        public class StockConversion
        {           
            public long ID_StockConversion { get; set; }
            public DateTime TransDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }          
            public int LastID { get; set; }
            public string TransMode { get; set; }
            public List<StockConversionDetail> StockConversionDetails { get; set; }           
        }
        public class StockConversionDetail
        {            
            public Int16 StockMode { get; set; }
            public long ID_Product { get; set; }
            public string Product { get; set; }
            public long ID_Stock { get; set; }
            public decimal FromQuantity { get; set; }
            public decimal ToQuantity { get; set; }
            public long SubProductID { get; set; }
            public string Component { get; set; }
            public decimal Amount { get; set; }           
        }
        public class StockConversionUpdate
        {
            public int UserAction { get; set; }
            public string TransMode { get; set; }
            public long ID_StockConversion { get; set; }
            public DateTime TransDate { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public int LastID { get; set; }
            public string StockConversionDetails { get; set; }
            public long FK_StockConversion { get; set; }
        }
        public class StockConversionView
        {
            public long SlNo { get; set; }
            public long ID_StockConversion { get; set; }
            public DateTime TransDate { get; set; }
            public string TransMode { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long SortOrder { get; set; }
            public Int64 TotalCount { get; set; }
            public Int64? LastID { get; set; }
            public long ReasonID { get; set; }
            public string BranchName { get; set; }
            public string Employee { get; set; }
            public string DepartmentName { get; set; }
            public List<Department> DepartmentList { get; set; }
            public List<Branch> BranchList { get; set; }
            public List<StockMode> StockModeList { get; set; }
            public List<StockConversionDetailsView> StockConversionDetailsList { get; set; }
        }
       
        public class StockConversionDetailsView
        {
            public long StockConversion { get; set; }
            public Int16 StockMode { get; set; }
            public long ID_Product { get; set; }
            public long ID_Stock { get; set; }
            public long subProductID { get; set; }
            public long subProductStockID { get; set; }
            public decimal FromQuantity { get; set; }

            public decimal ToQuantity { get; set; }
            public decimal Amount { get; set; }

        }

        public class StockConversionDetailsSelect
        {
            public Int64 FK_StockConversion { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
        }
        public class UpdateStockConversion
        {
            public long ID_StockConversion { get; set; }
            public DateTime TransDate { get; set; }
            public string TransMode { get; set; }
            public long FK_Branch { get; set; }
            public long FK_Department { get; set; }
            public long FK_Employee { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public Int64? LastID { get; set; }
            public byte UserAction { get; set; }
            public string StockConversionDetails { get; set; }

        }
        public class ModeLead
        {
            public Int32 Mode { get; set; }
        }
        public static string _deleteProcedureName = "ProStockConversionDelete";
        public static string _updateProcedureName = "ProStockConversionUpdate";
        public static string _selectProcedureName = "ProStockConversionSelect";

        public class DeleteStockConversion
        {
            public long FK_StockConversion { get; set; }
            public string TransMode { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }
            public long FK_Company { get; set; }
            public long FK_Reason { get; set; }
            public long FK_BranchCodeUser { get; set; }

        }
         

        public class Department
        {
            public Int32 DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }

        public class Branch
        {
            public Int32 BranchID { get; set; }
            public string BranchName { get; set; }

        }
        public class StockMode
        {
            public Int32 ID_Mode { get; set; }
            public string ModeName { get; set; }

        }
        public class StockConversionID
        {
            public Int64 FK_StockConversion { get; set; }
            public Int64 FK_BranchCodeUser { get; set; }
            public Int64 FK_Company { get; set; }
            public string EntrBy { get; set; }
            public Int64 FK_Machine { get; set; }
            public string TransMode { get; set; }
            public Int32 PageIndex { get; set; }
            public Int32 PageSize { get; set; }
            public string Name { get; set; }
            public Int32 Detailed { get; set; }
        }
        public Output UpdateStockConversionData(StockConversionUpdate input, string companyKey)
        {
            return Common.UpdateTableData<StockConversionUpdate>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }
        public Output DeleteStockConversionData(DeleteStockConversion input, string companyKey)
        {
            return Common.UpdateTableData<DeleteStockConversion>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        }
        public APIGetRecordsDynamic<StockConversionView> GetStockConversionData(StockConversionID input, string companyKey)
        {
            return Common.GetDataViaProcedure<StockConversionView, StockConversionID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<StockConversionDetail> GetStockConversionDetailsData(StockConversionID input, string companyKey)
        {
            return Common.GetDataViaProcedure<StockConversionDetail, StockConversionID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        }
        public APIGetRecordsDynamic<StockMode> GeLeadStatusList(ModeLead input, string companyKey)
        {
            return Common.GetDataViaProcedure<StockMode, ModeLead>(companyKey: companyKey, procedureName: "ProCommonPopupValues", parameter: input);

        }
    }
}
