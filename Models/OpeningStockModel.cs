/*----------------------------------------------------------------------
Created By	: Jisi Rajan
Created On	: 12/02/2022
Purpose		: OpeningStock
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
    public class OpeningStockModel
    {

        public class OpeningStockView
        {
            public long SlNo { get; set; }
            public long OpeningStockID { get; set; }

            public decimal MRP { get; set; }

            public decimal SalPrice { get; set; }


            public decimal ProductionCost { get; set; }

            public decimal OpeningQuantity { get; set; }

            public decimal OpeningStbyQuantity { get; set; }

            public string Product { get; set; }
            public string ProdHSNCode { get; set; }

            public long ProductID { get; set; }
            public long BranchID { get; set; }
            public string BranchName { get; set; }
            public long DepartmentID { get; set; }
            public string DepartmentName { get; set; }

            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public string QRCode { get; set; }

            public string BarCode { get; set; }
            public string SerialNumber { get; set; }
            public string BatchNo { get; set; }
            public DateTime ExpiryDate { get; set; }
            public decimal PurRate { get; set; }
            public List<OpeningStockListxml> OpeningStockListxmls { get; set; }
            public Int64? LastID { get; set; }
            public string TransMode { get; set; }
        }

        public class OpeningStockListxml
        {
            public long OpeningStockID { get; set; }
            public decimal MRP { get; set; }
            public decimal SalPrice { get; set; }
            public decimal ProductionCost { get; set; }
            public decimal OpeningQuantity { get; set; }
            public decimal OpeningStbyQuantity { get; set; }
            public string Product { get; set; }
            public string ProdHSNCode { get; set; }
            public long ProductID { get; set; }
            public long BranchID { get; set; }
            public string BranchName { get; set; }
            public long DepartmentID { get; set; }
            public string DepartmentName { get; set; }

            public short ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
            public string QRCode { get; set; }

            public string BarCode { get; set; }
            public string SerialNumber { get; set; }
            public string BatchNo { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public decimal PurRate { get; set; }
           
        }

        public class OpeningStockIDView
        {
            public long OpeningStockID { get; set; }
            public long ReasonID { get; set; }
        }
        public class UpdateOpeningStock
        {
            public long ID_OpeningStock { get; set; }
            public Int16 UserAction { get; set; }
            public string OpeningStockXmlDetails { get; set; }
            public long FK_Company { get; set; }
            public long FK_BranchCodeUser { get; set; }
            public string EntrBy { get; set; }
            public long FK_Machine { get; set; }

            public Int64? LastID { get; set; }
            public string TransMode { get; set; }
        }

        public static string _updateProcedureName = "ProOpeningStockUpdate";

        //public class DeleteOpeningStock
        //{
        //    public long FK_OpeningStock { get; set; }
        //    public string TransMode { get; set; }
        //    public long FK_Reason { get; set; }
        //    public long FK_Company { get; set; }
        //    public long FK_Machine { get; set; }
        //    public long FK_BranchCodeUser { get; set; }
        //    public string EntrBy { get; set; }
        //}

        public class ModeList
        {
            public long ModeID { get; set; }
            public string ModeName { get; set; }
            public string Mode { get; set; }
        }
        public class OpeningStockListModel
        {
            public long FK_Employee { get; set; }
            public List<ModeList> ModeList { get; set; }
          public  List<BranchList>BranchList { get; set; }
            public List<DepartmentList> DepartmentList { get; set; }
            public string SerialNumber { get; set; }
            public long StockID { get; set; }
            public Int64? LastID { get; set; }

        }
        public class DepartmentList
        {
            public long DepartmentID { get; set; }
            public string DepartmentName { get; set; }

        }
        public class BranchList
        {
            public long BranchID { get; set; }
            public string BranchName { get; set; }

        }
        public class ProductListView
        {

            public long ProductID { get; set; }

            public string Product { get; set; }
            public string ProdHSNCode { get; set; }


        }


        //public class OpeningStockID
        //{
         
        //    public string TransMode { get; set; }
        //    public long FK_OpeningStock { get; set; }
        //    public Int32 PageIndex { get; set; }
        //    public Int32 PageSize { get; set; }
        //    public long FK_Company { get; set; }
        //    public long FK_Machine { get; set; }
        //    public long FK_BranchCodeUser { get; set; }
        //    public string EntrBy { get; set; }

        //}
        public Output UpdateOpeningStockData(UpdateOpeningStock input, string companyKey)
        {
            return Common.UpdateTableData<UpdateOpeningStock>(parameter: input, companyKey: companyKey, procedureName: _updateProcedureName);
        }

        //public Output DeleteOpeningStockData(DeleteOpeningStock input, string companyKey)
        //{
        //    return Common.UpdateTableData<DeleteOpeningStock>(parameter: input, companyKey: companyKey, procedureName: _deleteProcedureName);
        //}
        //public APIGetRecordsDynamic<OpeningStockView> GetOpeningStockData(OpeningStockID input, string companyKey)
        //{
        //    return Common.GetDataViaProcedure<OpeningStockView, OpeningStockID>(companyKey: companyKey, procedureName: _selectProcedureName, parameter: input);
        //}

        public class inputGetSerialNo
        {
            public long FK_Company { get; set; }
        }
        public class outputGetSerialNo
        {
            public string SerialNumber { get; set; }
        }
        public APIGetRecordsDynamic<outputGetSerialNo> GetCustomerNo(inputGetSerialNo input, string companyKey)
        {
            return Common.GetDataViaProcedure<outputGetSerialNo, inputGetSerialNo>(companyKey: companyKey, procedureName: "ProGetSerialNo", parameter: input);
        }

    }
}

