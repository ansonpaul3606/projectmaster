using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Business;
using PerfectWebERPAPI.Interface;
using System.Data;
using System.IO;
using System.Web.Hosting;

namespace PerfectWebERPAPI.Business
{
   public class BlStockTransferFormats
    {        
        public static StockRTEmployeeDetails ConvertStockRTEmployee(DataTable dt)
        {
            StockRTEmployeeDetails log = new StockRTEmployeeDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRTEmployeeList = ConvertStockRTEmployeeList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StockRTEmployee> ConvertStockRTEmployeeList(DataTable dt)
        {
            List<StockRTEmployee> lst = new List<StockRTEmployee>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRTEmployee obj = new StockRTEmployee();
                obj.FK_Employee = Convert.ToInt32(dr["ID_FIELD"]);
                obj.Code = Convert.ToString(dr["Code"]);
                obj.Name = Convert.ToString(dr["Name"]);
                obj.Designation = Convert.ToString(dr["Designation"]);               
                lst.Add(obj);
            }
            return lst;
        }
        public static StockRTProductDetails ConvertStockRTProduct(DataTable dt)
        {
            StockRTProductDetails log = new StockRTProductDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRTProductList = ConvertStockRTProductList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StockRTProduct> ConvertStockRTProductList(DataTable dt)
        {
            List<StockRTProduct> lst = new List<StockRTProduct>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRTProduct obj = new StockRTProduct();
                obj.FK_Product = Convert.ToInt32(dr["ID_FIELD"]);
                obj.Code = Convert.ToString(dr["Code"]);
                obj.Name = Convert.ToString(dr["Name"]);              
                lst.Add(obj);
            }
            return lst;

        }

        public static UpdateStockTransfer ConvertUpdateStockTransfer(DataTable dt)
        {
            UpdateStockTransfer log = new UpdateStockTransfer();
            if (dt.Rows.Count > 0)
            {
                if(dt.Columns.Contains("FK_StockTransfer"))
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.FK_StockTransfer = Convert.ToInt64(dt.Rows[0]["FK_StockTransfer"]);
                }   
                if(dt.Columns.Contains("ErrCode"))
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
                    log.FK_StockTransfer = 0;
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static StockRequest ConvertStockRequestList(DataTable dt)
        {
            StockRequest log = new StockRequest();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRequestListData = ConvertStockRequest(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StockRequestList> ConvertStockRequest(DataTable dt)
        {
            List<StockRequestList> lst = new List<StockRequestList>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRequestList obj = new StockRequestList();
                obj.StockTransferID = Convert.ToInt32(dr["StockTransferID"]);
                obj.BranchID = Convert.ToInt32(dr["BranchID"]);
                obj.BranchName = Convert.ToString(dr["BranchName"]);              
                obj.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                obj.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                obj.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                obj.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                obj.BranchIDTo = Convert.ToInt32(dr["BranchIDTo"]);
                obj.BranchNameTo = Convert.ToString(dr["BranchNameTo"]);
                obj.DepartmentIDTo = Convert.ToInt32(dr["DepartmentIDTo"]);
                obj.DepartmentNameTo = Convert.ToString(dr["DepartmentNameTo"]);
                obj.EmployeeIDTo = Convert.ToInt32(dr["EmployeeIDTo"]);
                obj.EmployeeNameTo = Convert.ToString(dr["EmployeeNameTo"]);              
                var TempTransDate = Convert.ToDateTime(dr["TransDate"]);
                obj.TransDate = TempTransDate.ToString("dd/MM/yyyy");
                obj.Transfered = 0;
                if(Convert.ToInt32(dr["FK_StockRequest"])>0)
                {
                    obj.Transfered = 1;
                }
                obj.FK_StockRequest = Convert.ToInt32(dr["FK_StockRequest"]);
                lst.Add(obj);
            }
            return lst;
        }

        public static StockRequestProduct ConvertStockRequestProductList(DataTable dt)
        {
            StockRequestProduct log = new StockRequestProduct();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRequestProductListData = ConvertStockRequestProduct(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StockRequestProductList> ConvertStockRequestProduct(DataTable dt)
        {
            List<StockRequestProductList> lst = new List<StockRequestProductList>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRequestProductList obj = new StockRequestProductList();
                obj.ID_StockTransferDetails = Convert.ToInt32(dr["ID_StockTransferDetails"]);
                obj.FK_StockTransfer = Convert.ToInt32(dr["FK_StockTransfer"]);
                obj.ID_Product = Convert.ToInt32(dr["ID_Product"]);
                obj.Product = Convert.ToString(dr["Product"]);
                obj.Quantity = Convert.ToDecimal(dr["Quantity"]);
                obj.QuantityStandBy = Convert.ToInt32(dr["QuantityStandBy"]);
                obj.ID_Stock = Convert.ToInt32(dr["ID_Stock"]);
                obj.StockMode = Convert.ToInt32(dr["StockMode"]);

                lst.Add(obj);
            }
            return lst;
        }
        public static StockRequestInTransfer ConvertStockRequestInTransfer(DataTable dt)
        {
            StockRequestInTransfer log = new StockRequestInTransfer();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockRequestList = ConvertStockRequestInTransferList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StockRequestListInTransfer> ConvertStockRequestInTransferList(DataTable dt)
        {
            List<StockRequestListInTransfer> lst = new List<StockRequestListInTransfer>();
            foreach (DataRow dr in dt.Rows)
            {
                StockRequestListInTransfer obj = new StockRequestListInTransfer();
                var TempTransDate = Convert.ToDateTime(dr["TransDate"]);
                obj.TransDate = TempTransDate.ToString("dd/MM/yyyy");               
                obj.FK_StockTransfer = Convert.ToInt32(dr["FK_StockTransfer"]);

                obj.BranchID= Convert.ToInt32(dr["BranchID"]);
                obj.BranchName = Convert.ToString(dr["BranchName"]);
                obj.DepartmentID = Convert.ToInt32(dr["DepartmentID"]);
                obj.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                obj.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                obj.EmployeeName = Convert.ToString(dr["EmployeeName"]);

                obj.BranchIDTo = Convert.ToInt32(dr["BranchIDTo"]);
                obj.BranchNameTo = Convert.ToString(dr["BranchFromName"]);
                obj.DepartmentIDTo = Convert.ToInt32(dr["DepartmentIDTo"]);
                obj.DepartmentNameTo = Convert.ToString(dr["DepartmentFromName"]);
                obj.EmployeeIDTo = Convert.ToInt32(dr["EmployeeIDTo"]);
                obj.EmployeeNameTo = Convert.ToString(dr["EmployeeNameTo"]);

               


                lst.Add(obj);
            }
            return lst;
        }
        public static StockSTProductDetails ConvertStockSTProduct(DataTable dt)
        {
            StockSTProductDetails log = new StockSTProductDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.StockSTProductList = ConvertStockSTProductList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StockSTProduct> ConvertStockSTProductList(DataTable dt)
        {
            List<StockSTProduct> lst = new List<StockSTProduct>();
            foreach (DataRow dr in dt.Rows)
            {
                StockSTProduct obj = new StockSTProduct();
                obj.FK_Product = Convert.ToInt32(dr["ID_FIELD"]);           
                obj.Name = Convert.ToString(dr["Name"]);
                if (dt.Columns.Contains("PurRate"))
                {
                    obj.PurRate = Convert.ToDecimal(dr["PurRate"]);
                }
                if (dt.Columns.Contains("ProductAmount"))
                {
                    obj.ProductAmount = Convert.ToDecimal(dr["ProductAmount"]);
                }
                else if(dt.Columns.Contains("Amount"))
                {
                    obj.ProductAmount = Convert.ToDecimal(dr["Amount"]);
                }
                if (dt.Columns.Contains("CurrentStock"))
                {
                    obj.CurrentStock = Convert.ToDecimal(dr["CurrentStock"]);
                }
                else if (dt.Columns.Contains("Quantity"))
                {
                    obj.CurrentStock = Convert.ToDecimal(dr["Quantity"]);
                }
                if (dt.Columns.Contains("Stock"))
                {
                    obj.Stock = Convert.ToDecimal(dr["Stock"]);
                }
                if (dt.Columns.Contains("StandbyStock"))
                {
                    obj.StandbyStock = Convert.ToDecimal(dr["StandbyStock"]);
                }
                if (dt.Columns.Contains("StandbyQuantity"))
                {
                    obj.StandbyQuantity = Convert.ToDecimal(dr["StandbyQuantity"]);
                }
                
                obj.ID_Stock = Convert.ToInt32(dr["ID_Stock"]);

                lst.Add(obj);
            }
            return lst;
        }
        public static StockRTDeleteDetails ConvertDeleteStockTransfer(DataTable dt)
        {
            StockRTDeleteDetails log = new StockRTDeleteDetails();
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("FK_StockTransfer"))
                {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Deleted Successfully";
                    log.FK_StockTransfer = Convert.ToInt64(dt.Rows[0][0]);
                }
                if (dt.Columns.Contains("ErrCode"))
                {
                    log.ResponseCode = Convert.ToString(dt.Rows[0]["ErrCode"]);
                    log.ResponseMessage = Convert.ToString(dt.Rows[0]["ErrMsg"]);
                    log.FK_StockTransfer = 0;
                }
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
    }
}
