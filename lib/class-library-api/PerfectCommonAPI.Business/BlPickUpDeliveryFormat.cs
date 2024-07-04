using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectWebERPAPI.Business;
using PerfectWebERPAPI.Interface;
using System.Data;

namespace PerfectWebERPAPI.Business
{
    public class BlPickUpDeliveryFormat
    {
        public static PickupandDeliveryCount ConvertPickupandDeliveryCount(DataTable dt)
        {
            PickupandDeliveryCount log = new PickupandDeliveryCount();


            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["Field"]) == "Pick-up")
                        log.PickUp = Convert.ToInt32(dt.Rows[i]["Count"]);
                    if (Convert.ToString(dt.Rows[i]["Field"]) == "Delivery")
                        log.Delivery = Convert.ToInt32(dt.Rows[i]["Count"]);
                }

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static PickUpDeliveryDetails ConvertPickUpDeliveryDetails(DataTable dt)
        {
            PickUpDeliveryDetails log = new PickUpDeliveryDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PickUpDeliveryDetailsList = ConvertPickUpDeliveryDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PickUpDeliveryDetailsList> ConvertPickUpDeliveryDetailsList(DataTable dt)
        {
            List<PickUpDeliveryDetailsList> lst = new List<PickUpDeliveryDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                PickUpDeliveryDetailsList obj = new PickUpDeliveryDetailsList();
                obj.ID_ProductDelivery = Convert.ToString(dr["ID_ProductDeliveryAssign"].ToString());
                obj.Module = Convert.ToString(dr["Module"].ToString());
                obj.TransMode = Convert.ToString(dr["TransMode"]);
                obj.FK_Master = Convert.ToString(dr["FK_Master"]);               
                obj.FK_Employee = Convert.ToString(dr["FK_Employee"]);
                obj.ReferenceNo = Convert.ToString(dr["ReferenceNo"]);
                obj.CustomerName = Convert.ToString(dr["CustomerName"]);
                obj.Mobile = Convert.ToString(dr["Mobile"]);
                obj.Area = Convert.ToString(dr["Area"]);
                obj.EMPName = Convert.ToString(dr["EMPName"]);
                obj.PriorityCode = Convert.ToString(dr["PriorityCode"]);
                obj.Priority = Convert.ToString(dr["Priority"]);
                obj.PickUpTime = Convert.ToString(dr["PickUpTime"]);
                obj.AssignedOn = Convert.ToString(dr["AssignedOnText"]);
                
                //obj.TotalCount = Convert.ToString(dr["TotalCount"]);
                obj.Mode = Convert.ToString(dr["Mode"]);
                obj.AssignedDate = Convert.ToString(dr["AssignedDate"]);
                obj.ProductName = (DBNull.Value == dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);
                obj.FK_ImageLocation = (DBNull.Value == dr["FK_ImageLocation"]) ? "0" : Convert.ToString(dr["FK_ImageLocation"]);
                obj.LocLongitude = (DBNull.Value == dr["LocLongitude"]) ? "" : Convert.ToString(dr["LocLongitude"]);
                obj.LocLattitude = (DBNull.Value == dr["LocLattitude"]) ? "" : Convert.ToString(dr["LocLattitude"]);
                lst.Add(obj);
            }
            return lst;

        }
       
        public static UpdateDeliverStatusDetails ConvertUpdateDeliverStatusDetails(DataTable dt)
        {
            UpdateDeliverStatusDetails log = new UpdateDeliverStatusDetails(); 
            if (dt.Rows.Count > 0)
            {
                
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                if(dt.Rows[0]["TransMode"].ToString()== "CUSA")
                {
                    log.FK_Master = Convert.ToString(dt.Rows[0].Field<Int64>("FK_Master"));
                    log.ID_ProductDelivery = Convert.ToString(dt.Rows[0].Field<Int64>("ID_ProductDelivery"));
                    log.FK_Product = Convert.ToString(dt.Rows[0].Field<Int64>("FK_Product"));
                    log.CSRTickno = Convert.ToString(dt.Rows[0].Field<string>("CSRTickno"));
                    log.CSRRegDate = Convert.ToString(dt.Rows[0].Field<string>("CSRRegDate"));
                    log.Customer = Convert.ToString(dt.Rows[0].Field<string>("Customer"));
                    log.Address1 = Convert.ToString(dt.Rows[0].Field<string>("Address1"));
                    log.Address2 = Convert.ToString(dt.Rows[0].Field<string>("Address2"));
                    log.Area = Convert.ToString(dt.Rows[0].Field<string>("Area"));
                    log.Post = Convert.ToString(dt.Rows[0].Field<string>("Post"));
                    log.Area = Convert.ToString(dt.Rows[0].Field<string>("Area"));
                    log.District = Convert.ToString(dt.Rows[0].Field<string>("District"));
                    log.State = Convert.ToString(dt.Rows[0].Field<string>("State"));
                    log.Category = Convert.ToString(dt.Rows[0].Field<string>("Category"));
                    log.Product = Convert.ToString(dt.Rows[0].Field<string>("Product"));
                }
                if (dt.Rows[0]["TransMode"].ToString() == "INDA")
                {
                  
                    log.ID_ProductDelivery = Convert.ToString(dt.Rows[0]["ID_ProductDelivery"]);
                    log.ReferenceNo = Convert.ToString(dt.Rows[0]["ReferenceNo"]);                   
                    var TempCSRRegDate = Convert.ToDateTime(dt.Rows[0]["BillDate"]);
                    log.CSRRegDate = TempCSRRegDate.ToString("dd/MM/yyyy");                   
                    log.Customer = Convert.ToString(dt.Rows[0]["Customer"]);
                    log.MobileNo = Convert.ToString(dt.Rows[0]["MobileNo"]);
                    log.DelName = Convert.ToString(dt.Rows[0]["DelName"]);
                    log.Address1 = Convert.ToString(dt.Rows[0]["DelAddress1"]);
                    log.Address2 = Convert.ToString(dt.Rows[0]["DelAddress2"]);                    
                    log.Post = Convert.ToString(dt.Rows[0]["Post"]);
                    log.Area = Convert.ToString(dt.Rows[0]["Area"]);
                    log.District = Convert.ToString(dt.Rows[0]["District"]);
                    log.State = Convert.ToString(dt.Rows[0]["State"]);
                    log.PinCode = Convert.ToString(dt.Rows[0]["PinCode"]);
                    log.Country = Convert.ToString(dt.Rows[0]["Country"]);                 
                    log.DelMobileNo = Convert.ToString(dt.Rows[0]["DelMobileNo"]);
                    log.DelVehicleNo = Convert.ToString(dt.Rows[0]["DelVehicleNo"]);
                    log.DelDriverName = Convert.ToString(dt.Rows[0]["DelDriverName"]);
                    log.DelDriverMobileNo = Convert.ToString(dt.Rows[0]["DelDriverMobileNo"]);
                    log.PdEWayBillNo = Convert.ToString(dt.Rows[0]["PdEWayBillNo"]);
                    
                }
                if (dt.Rows[0]["TransMode"].ToString() == "INPDR")
                {
                    log.FK_Master = Convert.ToString(dt.Rows[0].Field<Int64>("FK_Master"));
                    log.ID_ProductDelivery = Convert.ToString(dt.Rows[0].Field<Int64>("ID_ProductDelivery"));
                    var AssignedOn = Convert.ToDateTime(dt.Rows[0]["AssignedOn"]);
                    log.AssignedOn = AssignedOn.ToString("dd/MM/yyyy");               
                    log.Customer = Convert.ToString(dt.Rows[0].Field<string>("Customer"));
                    log.Address1 = Convert.ToString(dt.Rows[0].Field<string>("Address1"));
                    log.Address2 = Convert.ToString(dt.Rows[0].Field<string>("Address2"));
                    log.Area = Convert.ToString(dt.Rows[0].Field<string>("Area"));
                    log.Post = Convert.ToString(dt.Rows[0].Field<string>("Post"));
                    log.Area = Convert.ToString(dt.Rows[0].Field<string>("Area"));
                    log.District = Convert.ToString(dt.Rows[0].Field<string>("District"));
                    log.State = Convert.ToString(dt.Rows[0].Field<string>("State"));
                    log.TransMode = Convert.ToString(dt.Rows[0].Field<string>("TransMode"));
                    
                }
                if (dt.Rows[0]["TransMode"].ToString() == "INSBR")
                {
                    log.FK_Master = Convert.ToString(dt.Rows[0].Field<Int64>("FK_Master"));
                    log.ID_ProductDelivery = Convert.ToString(dt.Rows[0].Field<Int64>("ID_ProductDelivery"));
                    var AssignedOn = Convert.ToDateTime(dt.Rows[0]["AssignedOn"]);
                    log.AssignedOn = AssignedOn.ToString("dd/MM/yyyy");
                    log.Customer = Convert.ToString(dt.Rows[0].Field<string>("Customer"));
                    log.Address1 = Convert.ToString(dt.Rows[0].Field<string>("Address1"));
                    log.Address2 = Convert.ToString(dt.Rows[0].Field<string>("Address2"));
                    log.Area = Convert.ToString(dt.Rows[0].Field<string>("Area"));
                    log.Post = Convert.ToString(dt.Rows[0].Field<string>("Post"));
                    log.Area = Convert.ToString(dt.Rows[0].Field<string>("Area"));
                    log.District = Convert.ToString(dt.Rows[0].Field<string>("District"));
                    log.State = Convert.ToString(dt.Rows[0].Field<string>("State"));
                    log.TransMode = Convert.ToString(dt.Rows[0].Field<string>("TransMode"));
                    log.SalBillNo = Convert.ToString(dt.Rows[0].Field<string>("SalBillNo"));
                    log.SalBillDate = Convert.ToString(dt.Rows[0].Field<string>("SalBillDate"));

                }
            }
            else
            {
                log.ResponseCode = "-2"; 
                log.ResponseMessage = "No Data Found"; 
            }
                return log; 

        }
        public static PickUPProductInformationDetails ConvertPickUPProductInformationDetails(DataTable dt)
        {
            PickUPProductInformationDetails log = new PickUPProductInformationDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PickUPProductInformationDetailsList = ConvertPickUPProductInformationDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PickUPProductInformationDetailsList> ConvertPickUPProductInformationDetailsList(DataTable dt)
        {
            List<PickUPProductInformationDetailsList> lst = new List<PickUPProductInformationDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                PickUPProductInformationDetailsList obj = new PickUPProductInformationDetailsList();
                if (dt.Rows[0]["TransMode"].ToString() == "CUSA")
                {
                    obj.ID_Product = Convert.ToString(dr["ID_Product"].ToString());
                    obj.ProdName = Convert.ToString(dr["ProdName"].ToString());
                    obj.ProvideStandBy = Convert.ToString(dr["ProvideStandBy"]);
                    obj.Quantity = Convert.ToString(dr["Quantity"]);
                    obj.Product = Convert.ToString(dr["Product"]);
                    obj.SPQuantity = Convert.ToString(dr["SPQuantity"]);
                    obj.SPAmount = Convert.ToString(dr["SPAmount"]);
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                }
                if (dt.Rows[0]["TransMode"].ToString() == "INPDR")
                {
                    obj.ID_Product = Convert.ToString(dr["ID_Product"].ToString());
                    obj.ProdName = Convert.ToString(dr["ProdName"].ToString());                  
                    obj.Quantity = Convert.ToString(dr["Quantity"]);               
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                }
                if (dt.Rows[0]["TransMode"].ToString() == "INDA")
                {
                    obj.ID_Product = Convert.ToString(dr["FK_Product"].ToString());
                    obj.ProdName = Convert.ToString(dr["ProdName"].ToString());
                    obj.Quantity = Convert.ToString(dr["Quantity"]);
                    obj.FK_Category = Convert.ToString(dr["FK_Category"]);
                    obj.ID_ProductDelivery = Convert.ToString(dr["ID_ProductDelivery"]);
                }
                if (dt.Rows[0]["TransMode"].ToString() == "INSBR")
                {
                    obj.ID_Product = Convert.ToString(dr["ID_Product"].ToString());
                    obj.ProdName = Convert.ToString(dr["ProdName"].ToString());
                    obj.ProvideStandBy = Convert.ToString(dr["ProvideStandBy"]);
                    obj.Quantity = Convert.ToString(dr["Quantity"]);
                    obj.Product = Convert.ToString(dr["Product"]);
                    obj.FK_StandByProduct = Convert.ToString(dr["FK_StandByProduct"]);
                    obj.SPQuantity = Convert.ToString(dr["SPQuantity"]);
                    obj.SPAmount = Convert.ToString(dr["SPAmount"]);
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                    obj.FK_StandByStock = Convert.ToString(dr["FK_StandByStock"]);
                }
                lst.Add(obj);
            }
            return lst;

        }
        public static ProductDetailsList ConvertProductDetails(DataTable dt)
        {
            ProductDetailsList log = new ProductDetailsList();
            if (dt.Rows.Count > 0)
            {
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.ProductList = ConvertProductList(dt);
             
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductList> ConvertProductList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new ProductList()
                    {

                        ID_Product = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        ProductName = Convert.ToString(dr["ID_Name"].ToString()),
                        ProductCode = Convert.ToString(dr["Code"].ToString())


                    }).ToList();

        }
        public static StandByProductDetails ConvertStandByProductDetails(DataTable dt)
        {
            StandByProductDetails log = new StandByProductDetails();
            if (dt.Rows.Count > 0)
            {
               
                    log.ResponseCode = "0";
                    log.ResponseMessage = "Transaction Verified";
                    log.StandByProductDetailsList = ConvertStandByProductDetailsList(dt);
              
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<StandByProductDetailsList> ConvertStandByProductDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new StandByProductDetailsList()
                    {

                        ID_Product = Convert.ToString(dr["ID_FIELD"].ToString()),
                        ProductName = Convert.ToString(dr["ID_Name"].ToString()),
                        ProductCode = Convert.ToString(dr["Code"].ToString()),
                        MRP = Convert.ToString(dr["MRPs"].ToString()),
                        SalesPrice = Convert.ToString(dr["SalePrice"].ToString()),
                        CurrentStock = Convert.ToString(dr["CurrentStock1R"].ToString()),
                        StandByStock = Convert.ToString(dr["StandbyStock"].ToString())


                    }).ToList();

        }
        public static UpdatePickUpAndDelivery ConvertUpdatePickUpAndDelivery(DataTable dt)
        {
            UpdatePickUpAndDelivery log = new UpdatePickUpAndDelivery();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Successfully Updated";
                if (Convert.ToInt64(dt.Rows[0][0].ToString())>0)
                        log.FK_ProductDeliveryFollowUp = Convert.ToInt64(dt.Rows[0][0].ToString());
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static BillType ConvertBillType(DataTable dt)
        {
            BillType log = new BillType();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.BillTypeList = ConvertBillTypeList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<BillTypeList> ConvertBillTypeList(DataTable dt)
        {
            List<BillTypeList> objList = new List<BillTypeList>();
            foreach (DataRow dr in dt.Rows)
            {
                BillTypeList obj = new BillTypeList();
                obj.ID_BillType = Convert.ToString(dr["ID_BillType"].ToString());
                obj.BTName = Convert.ToString(dr["BTName"].ToString());
                objList.Add(obj);
            }
            return objList;
        }
        public static DeliveryProductInformationDetails ConvertDeliveryProductInformationDetails(DataTable dt)
        {
            DeliveryProductInformationDetails log = new DeliveryProductInformationDetails();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.DeliveryProductInformationDetailsList = ConvertDeliveryProductInformationDetailsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<DeliveryProductInformationDetailsList> ConvertDeliveryProductInformationDetailsList(DataTable dt)
        {
            List<DeliveryProductInformationDetailsList> lst = new List<DeliveryProductInformationDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                DeliveryProductInformationDetailsList obj = new DeliveryProductInformationDetailsList();
                if (dt.Rows[0]["TransMode"].ToString() == "INSBR")
                {
                  
                    obj.FK_Product = Convert.ToString(dr["ID_Product"].ToString());                 
                    obj.Product = Convert.ToString(dr["ProdName"]);
                    obj.ProvideStandBy = Convert.ToString(dr["ProvideStandBy"]);
                    obj.Quantity = Convert.ToString(dr["Quantity"]);
                    obj.StandByProduct = Convert.ToString(dr["Product"]);
                    obj.FK_StandByProduct = Convert.ToString(dr["FK_StandByProduct"]);
                    obj.SPQuantity = Convert.ToString(dr["SPQuantity"]);
                    obj.SPAmount = Convert.ToString(dr["SPAmount"]);
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                    obj.TransMode = Convert.ToString(dr["TransMode"]);
                    obj.FK_StandByStock = Convert.ToString(dr["FK_StandByStock"]);
                    obj.ID_ProductDelivery = "";
                    obj.FK_Category = "";
                }
                else if (dt.Rows[0]["TransMode"].ToString() == "CUSA")
                {

                    obj.FK_Product = Convert.ToString(dr["ID_Product"].ToString());
                    obj.Product = Convert.ToString(dr["ProdName"]);
                    obj.ProvideStandBy = Convert.ToString(dr["ProvideStandBy"]);
                    obj.Quantity = Convert.ToString(dr["Quantity"]);
                    obj.StandByProduct = Convert.ToString(dr["Product"]);
                    obj.FK_StandByProduct = Convert.ToString(dr["FK_StandByProduct"]);
                    obj.SPQuantity = Convert.ToString(dr["SPQuantity"]);
                    obj.SPAmount = Convert.ToString(dr["SPAmount"]);
                    obj.Remarks = Convert.ToString(dr["Remarks"]);
                    obj.TransMode = Convert.ToString(dr["TransMode"]);
                    obj.FK_StandByStock = "";
                    obj.ID_ProductDelivery = "";
                    obj.FK_Category = "";
                }
                else
                {
                    obj.ID_ProductDelivery = Convert.ToString(dr["ID_ProductDelivery"].ToString());
                    obj.FK_Product = Convert.ToString(dr["FK_Product"].ToString());
                    obj.FK_Category = Convert.ToString(dr["FK_Category"]);
                    obj.Product = Convert.ToString(dr["Product"]);
                    obj.Quantity = Convert.ToString(dr["Quantity"]);
                    obj.TransMode = Convert.ToString(dr["TransMode"]);
                }
                lst.Add(obj);
            }
            return lst;

        }

        public static ProductComplaintList ConvertProductCompalintList(DataTable dt)
        {
            ProductComplaintList log = new ProductComplaintList();
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductComplaintsList = ConvertProductCompalintsList(dt);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductComplaintListDetails> ConvertProductCompalintsList(DataTable dt)
        {
            List<ProductComplaintListDetails> lst = new List<ProductComplaintListDetails>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductComplaintListDetails obj = new ProductComplaintListDetails();
                obj.ID_ComplaintList = Convert.ToString(dr["ID_ComplaintList"].ToString());
                obj.ComplaintName = Convert.ToString(dr["ComplaintName"].ToString());
                
                lst.Add(obj);
            }
            return lst;

        }
    }
}
