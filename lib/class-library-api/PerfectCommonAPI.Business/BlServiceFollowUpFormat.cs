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
    public class BlServiceFollowUpFormat
    {
        public static Attendancedetails ConvertAttendancedetails(DataTable dt)
        {
            Attendancedetails log = new Attendancedetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AttendancedetailsList = ConvertAttendancedetailsList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<AttendancedetailsList> ConvertAttendancedetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new AttendancedetailsList()
                    {
                        ID_Employee = Convert.ToInt64(dr["ID_Employee"]),
                        Department = Convert.ToString(dr["Department"].ToString()),
                        Role = Convert.ToString(dr["EmployeeType"].ToString()),
                        EmployeeName = Convert.ToString(dr["Employee"]),
                        ID_CSAEmployeeType = Convert.ToInt64(dr["ID_CSAEmployeeType"].ToString()),
                        Attend = Convert.ToInt32(dr["Attend"].ToString()),
                        Designation = Convert.ToString(dr["Designation"].ToString()),

                    }).ToList();

        }
        public static ServiceHistoryDetails ConvertServiceHistoryDetails(DataTable dt)
        {
            ServiceHistoryDetails log = new ServiceHistoryDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceHistoryDetailsList = ConvertServiceHistoryDetailsList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ServiceHistoryDetailsList> ConvertServiceHistoryDetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ServiceHistoryDetailsList()
                    {
                        TicketNo = Convert.ToString(dr["TicketNo"]),
                        RegOn = Convert.ToString(dr["RegOn"].ToString()),
                        AttendedBy = Convert.ToString(dr["AttendedBy"].ToString()),
                        TransMode = Convert.ToString(dr["TransMode"].ToString()),
                        ID_Master = Convert.ToInt64(dr["ID_Master"].ToString()),
                      //  Service = Convert.ToString(dr["Service"].ToString()),
                        Complaint = Convert.ToString(dr["Complaint"].ToString()),
                        CurrentStatus = Convert.ToString(dr["Status"]),
                      //  ClosedDate = Convert.ToString(dr["ClosedDate"].ToString()),
                        EmployeeNotes = Convert.ToString(dr["EmployeeNotes"].ToString())


                    }).ToList();

        }
        public static ServiceAttendedDetails ConvertServiceAttendedDetails(DataTable dt)
        {
            ServiceAttendedDetails log = new ServiceAttendedDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceAttendedDetailsList = ConvertServiceAttendedDetailsList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ServiceAttendedDetailsList> ConvertServiceAttendedDetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ServiceAttendedDetailsList()
                    {
                        ID_ProductWiseServiceDetails = Convert.ToInt64(dr["ID_ProductWiseServiceDetails"]),
                        SubProduct = Convert.ToString(dr["SubProduct"].ToString()),
                        ID_Product = Convert.ToInt64(dr["ID_Product"].ToString()),
                        ID_Services = Convert.ToInt64(dr["ID_Services"]),
                        Service = Convert.ToString(dr["Service"].ToString()),
                        ServiceCost = Convert.ToString(dr["ServiceCost"].ToString()),
                        ServiceTaxAmount = Convert.ToString(dr["ServiceTaxAmount"].ToString()),
                        ServiceNetAmount = Convert.ToString(dr["ServiceNetAmount"].ToString()),
                        Remarks = Convert.ToString(dr["Remarks"].ToString())
                    }).ToList();

        }
        public static ServiceType ConvertServiceType(DataTable dt)
        {
            ServiceType log = new ServiceType();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceTypeList = ConvertServiceTypeList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ServiceTypeList> ConvertServiceTypeList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ServiceTypeList()
                    {
                        ServiceTypeId = Convert.ToInt32(dr["ID_Mode"]),
                        ServiceTypeName = Convert.ToString(dr["ModeName"].ToString())

                    }).ToList();

        }
        public static AddedService ConvertAddedService(DataTable dt)
        {
            AddedService log = new AddedService();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.AddedServiceList = ConvertAddedServiceList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<AddedServiceList> ConvertAddedServiceList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new AddedServiceList()
                    {
                        ID_Services = Convert.ToInt64(dr["ID_Services"]),
                        Service = Convert.ToString(dr["Service"].ToString()),
                        ServiceChargeIncludeTax = Convert.ToBoolean(dr["ServiceChargeIncludeTax"].ToString()),
                        FK_TaxGroup = Convert.ToInt64(dr["FK_TaxGroup"]),
                        TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"])
                    }).ToList();

        }
        public static ProductListDetails ConvertProductList(DataTable dt)
        {
            ProductListDetails log = new ProductListDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductSearchList = ConvertProductListDetails(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductSearchList> ConvertProductListDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ProductSearchList()
                    {
                        SlNo = Convert.ToInt64(dr["SlNo"]),
                        // FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        ID_Product = Convert.ToInt64(dr["ID_FIELD"].ToString()),
                        Name = Convert.ToString(dr["Name"]),
                        PurRate = Convert.ToDecimal(dr["PurRate"]),
                        ProductAmount = Convert.ToDecimal(dr["ProductAmount"]),
                        CurrentStock = Convert.ToDecimal(dr["CurrentStock"]),
                        Stock = Convert.ToDecimal(dr["Stock"]),
                        StandbyStock = Convert.ToDecimal(dr["StandbyStock"]),
                        StandbyQuantity = Convert.ToDecimal(dr["StandbyQuantity"]),
                        ID_Stock = Convert.ToInt64(dr["ID_Stock"]),


                    }).ToList();

        }
        public static ServiceSerach ConvertSearchList(DataTable dt)
        {
            ServiceSerach log = new ServiceSerach();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceSerachList = ConvertServiceSeractDetails(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ServiceSerachList> ConvertServiceSeractDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ServiceSerachList()
                    {
                        SNo = Convert.ToInt64(dr["SNo"]),
                        // FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        MasterProduct = Convert.ToString(dr["MasterProduct"].ToString()),
                        FK_Product = Convert.ToInt64(dr["FK_Product"]),
                        Product = Convert.ToString(dr["Product"]),
                        BindProduct = Convert.ToString(dr["BindProduct"]),
                       // SerchSerialNo = Convert.ToString(dr["SerchSerialNo"]),
                        // ComplaintProduct = Convert.ToString(dr["ComplaintProduct"]),
                        FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        SLNo = Convert.ToString(dr["SLNo"]),
                        Warranty = Convert.ToString(dr["Warranty"]),
                        ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"]),
                        ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"]),
                        ServiceWarrantyExpired = Convert.ToString(dr["ServiceWarrantyExpired"]),
                        ReplacementWarrantyExpired = Convert.ToString(dr["ReplacementWarrantyExpired"]),
                        ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"])

                    }).ToList();

        }
        public static SerDetails ConvertServiceList(DataSet dt)
        {
            SerDetails log = new SerDetails();
            DataTable dts = dt.Tables[0];
            if (dts.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ServiceAttendedList = ConvertServiceListDetails(dt.Tables[0]);
                


            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ServiceAttendedListDet> ConvertServiceListDetailsValue(DataTable dt,string MasterProduct)
        {

            return (from DataRow dr in dt.Rows
                    where Convert.ToInt64(dr["BindProduct"]) == 1 && Convert.ToString(dr["MasterProduct"].ToString())== MasterProduct
                    select new ServiceAttendedListDet()
                    {
                        SNo = Convert.ToInt64(dr["SNo"]),
                       // FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        MasterProduct = Convert.ToString(dr["MasterProduct"].ToString()),
                        FK_Product = Convert.ToInt64(dr["FK_Product"]),
                        Product = Convert.ToString(dr["Product"]),
                        BindProduct = Convert.ToString(dr["BindProduct"]),
                        SerchSerialNo = Convert.ToString(dr["SerchSerialNo"]),
                        ComplaintProduct = Convert.ToString(dr["ComplaintProduct"]),
                        FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        SLNo = Convert.ToString(dr["SLNo"]),
                        Warranty = Convert.ToString(dr["Warranty"]),
                        ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"]),
                        ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"]),
                        ServiceWarrantyExpired = Convert.ToString(dr["ServiceWarrantyExpired"]),
                        ReplacementWarrantyExpired = Convert.ToString(dr["ReplacementWarrantyExpired"]),
                        ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"])

                    }).ToList();

        }
        public static List<ServiceAttendedList> ConvertServiceListDetails(DataTable dt)
        {
            //var list = new List<ServiceAttendedList>();
            //list = ctx.MemberTable
            //    .Where(c => c.Mode == 1)
            //    .ToList();

            //return (from DataRow dr in dt.Rows
            //        select new ServiceAttendedList()
            //        {
            //            SNo = Convert.ToInt64(dr["SNo"]),
            //            // FK_Category = Convert.ToInt64(dr["FK_Category"]),
            //            MasterProduct = Convert.ToString(dr["MasterProduct"].ToString()),
            //            // FK_Product = Convert.ToInt64(dr["FK_Product"]),
            //            Product = Convert.ToString(dr["Product"]),
            //            Mode = Convert.ToString(dr["BindProduct"])
            //            //  SLNo = Convert.ToString(dr["SLNo"]),
            //            //Warranty = Convert.ToString(dr["Warranty"]),
            //            //ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"]),
            //            //ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"]),
            //            //ServiceWarrantyExpired = Convert.ToString(dr["ServiceWarrantyExpired"]),
            //            //ReplacementWarrantyExpired = Convert.ToString(dr["ReplacementWarrantyExpired"]),
            //            //ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"])


            //        }).ToList();//.Where(dr["Mode"]=1);

            //return (from DataRow dr in dt.Rows
            //                                         where Convert.ToInt64(dr["Mode"]) == 1
            //                                         select new ServiceAttendedList()
            //                                         {
            //                                             SNo = Convert.ToInt64(dr["SNo"]),
            //                                             MasterProduct = Convert.ToString(dr["MasterProduct"]),
            //                                             Product = Convert.ToString(dr["Product"]),
            //                                             Mode = Convert.ToString(dr["BindProduct"])
            //                                             ServiceAttendedListDet= ConvertServiceListDetailsValue()
            //                                             // Add other properties as needed
            //                                         }).ToList();
            List<ServiceAttendedList> root = new List<ServiceAttendedList>();
            foreach (DataRow dr in dt.Rows)
            {
                if(dr["BindProduct"].ToString()=="0")
                { 
                ServiceAttendedList serviceAttended = new ServiceAttendedList
                {
                    SNo = Convert.ToInt64(dr["SNo"]),
                    MasterProduct = Convert.ToString(dr["MasterProduct"]),
                    FK_Product = Convert.ToInt64(dr["FK_Product"]),
                    Product = Convert.ToString(dr["Product"]),
                    BindProduct = Convert.ToString(dr["BindProduct"]),
                    SerchSerialNo = Convert.ToString(dr["SerchSerialNo"]),
                    ComplaintProduct = Convert.ToString(dr["ComplaintProduct"]),
                    FK_Category = Convert.ToInt64(dr["FK_Category"]),
                    Mode = Convert.ToString(dr["BindProduct"]),
                    Warranty = Convert.ToString(dr["Warranty"]),
                    ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"]),
                    ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"]),
                    ServiceWarrantyExpired = Convert.ToString(dr["ServiceWarrantyExpired"]),
                    ReplacementWarrantyExpired = Convert.ToString(dr["ReplacementWarrantyExpired"]),
                    ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"]),
                    ServiceAttendedListDet = ConvertServiceListDetailsValue(dt,dr["Product"].ToString()) // Assuming you have a function to convert details
                };

                root.Add(serviceAttended);
                }
            }
            return root;
        }
        public static ProductInfo ConvertProductInfoList(DataSet dt)
        {
            ProductInfo log = new ProductInfo();
            if (dt.Tables.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ProductInfoList = ConvertProductInfoList(dt.Tables[0]);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ProductInfoList> ConvertProductInfoList(DataTable dt)
        {
            
            List<ProductInfoList> root = new List<ProductInfoList>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["FK_SubProduct"].ToString() == "0")
                {
                    ProductInfoList serviceAttended = new ProductInfoList
                    {
                        SNo = Convert.ToInt64(dr["SNo"]),
                        MasterProduct = Convert.ToString(dr["MasterProduct"]),
                        FK_Product = Convert.ToInt64(dr["FK_Product"]),
                        Product = Convert.ToString(dr["Product"]),
                        BindProduct = Convert.ToString(dr["BindProduct"]),
                        SerchSerialNo = Convert.ToString(dr["SerchSerialNo"]),
                        ComplaintProduct = Convert.ToString(dr["ComplaintProduct"]),
                        FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        Mode = Convert.ToString(dr["BindProduct"]),
                        Warranty = Convert.ToString(dr["Warranty"]),
                        ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"]),
                        ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"]),
                        ServiceWarrantyExpired = Convert.ToString(dr["ServiceWarrantyExpired"]),
                        ReplacementWarrantyExpired = Convert.ToString(dr["ReplacementWarrantyExpired"]),
                        ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"]),
                        ProductInfoListDet = ConvertProductInfoDetailsValue(dt, dr["Product"].ToString()) // Assuming you have a function to convert details
                    };

                    root.Add(serviceAttended);
                }
            }
            return root;
        }
        public static List<ProductInfoListDet> ConvertProductInfoDetailsValue(DataTable dt, string MasterProduct)
        {

            return (from DataRow dr in dt.Rows
                    where Convert.ToInt64(dr["FK_SubProduct"]) >0 
                    select new ProductInfoListDet()
                    {
                        SNo = Convert.ToInt64(dr["SNo"]),
                        // FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        MasterProduct = Convert.ToString(dr["MasterProduct"].ToString()),
                        FK_Product = Convert.ToInt64(dr["FK_Product"]),
                        Product = Convert.ToString(dr["Product"]),
                        BindProduct = Convert.ToString(dr["BindProduct"]),
                        SerchSerialNo = Convert.ToString(dr["SerchSerialNo"]),
                        ComplaintProduct = Convert.ToString(dr["ComplaintProduct"]),
                        FK_Category = Convert.ToInt64(dr["FK_Category"]),
                        SLNo = Convert.ToString(dr["SLNo"]),
                        Warranty = Convert.ToString(dr["Warranty"]),
                        ServiceWarrantyExpireDate = Convert.ToString(dr["ServiceWarrantyExpireDate"]),
                        ReplacementWarrantyExpireDate = Convert.ToString(dr["ReplacementWarrantyExpireDate"]),
                        ServiceWarrantyExpired = Convert.ToString(dr["ServiceWarrantyExpired"]),
                        ReplacementWarrantyExpired = Convert.ToString(dr["ReplacementWarrantyExpired"]),
                        ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"])

                    }).ToList();

        }
        public static ChangemodeDetails ConvertChangemodeDetails(DataTable dt)
        {
            ChangemodeDetails log = new ChangemodeDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ChangemodeDetailsList = ConvertChangemodeDetailsList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ChangemodeDetailsList> ConvertChangemodeDetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ChangemodeDetailsList()
                    {
                        ID_Mode = Convert.ToInt64(dr["ID_Mode"]),
                        ModeName = Convert.ToString(dr["ModeName"].ToString())

                    }).ToList();

        }
        public static ReplaceProductdetails ConvertReplaceProductdetails(DataTable dt)
        {
            ReplaceProductdetails log = new ReplaceProductdetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.ReplaceProductdetailsList = ConvertReplaceProductdetailsList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<ReplaceProductdetailsList> ConvertReplaceProductdetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ReplaceProductdetailsList()
                    {
                        ID_OLD_Product = Convert.ToInt64(dr["ID_OLD_Product"]),
                        OLD_Product = Convert.ToString(dr["OLD_Product"].ToString()),
                        SPDOldQuantity = Convert.ToInt32(dr["SPDOldQuantity"]),
                        Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                        ReplaceAmount = Convert.ToDecimal(dr["ReplaceAmount"]),
                        Remarks = Convert.ToString(dr["Remarks"].ToString()),
                        ID_Product = Convert.ToInt32(dr["ID_Product"]),
                        Product = Convert.ToString(dr["Product"].ToString())

                    }).ToList();

        }
        public static PopUpProductdetails ConverPopUpProductdetails(DataTable dt)
        {
            PopUpProductdetails log = new PopUpProductdetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PopUpProductdetailsList = ConvertPopUpProductdetailsList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PopUpProductdetailsList> ConvertPopUpProductdetailsList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new PopUpProductdetailsList()
                    {
                        ID_Product = Convert.ToInt64(dr["ID_FIELD"]),
                        Code = Convert.ToString(dr["Code"].ToString()),
                        Name = Convert.ToString(dr["Name"]),
                        MRPs = Convert.ToString(dr["MRPs"].ToString()),
                        MRP1R = Convert.ToString(dr["MRP1R"]),
                        SalesPrice1R = Convert.ToString(dr["SalesPrice1R"].ToString()),
                        SalePrice = Convert.ToString(dr["SalePrice"]),
                        CurrentStock1R = Convert.ToString(dr["CurrentStock1R"].ToString()),
                        StockId = Convert.ToString(dr["StockId"].ToString()),
                        TaxAmount = Convert.ToString(dr["TaxAmount"]),
                        StandbyStock = Convert.ToString(dr["StandbyStock"].ToString()),
                        //TotalCount = Convert.ToString(dr["TotalCount"].ToString())

                    }).ToList();

        }
        public static PopUpSerchCritrea ConvertPopUpSerchCritrea(DataTable dt)
        {
            PopUpSerchCritrea log = new PopUpSerchCritrea();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.PopUpSerchCritreaList = ConvertPopUpSerchCritreaList(dt);



            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<PopUpSerchCritreaList> ConvertPopUpSerchCritreaList(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new PopUpSerchCritreaList()
                    {
                        CritreaValue = Convert.ToString(dr["Value"]),
                        Name = Convert.ToString(dr["Name"].ToString())

                    }).ToList();

        }
        public static FollowUpActionDetails ConvertFollowUpActionDetails(DataTable dt)
        {
            FollowUpActionDetails log = new FollowUpActionDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FollowUpActionDetailsList = ConvertFollowUpActionDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<FollowUpActionDetailsList> ConvertFollowUpActionDetailsList(DataTable dt)
        {
          
            List<FollowUpActionDetailsList> objList = new List<FollowUpActionDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                FollowUpActionDetailsList obj = new FollowUpActionDetailsList();
                obj.ID_NextAction = Convert.ToInt64(dr["ID_NextAction"].ToString());
                obj.NxtActnName = Convert.ToString(dr["NxtActnName"].ToString());
                obj.Status = Convert.ToInt32(dr["FK_ActionStatus"].ToString());

                objList.Add(obj);
            }
            return objList;
        }
        public static SubproductDetails ConvertGetSubProduct(DataTable dt)
        {
            SubproductDetails log = new SubproductDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.SubproductDetailsList = ConvertGetSubProdutDetailList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<SubproductDetailsList> ConvertGetSubProdutDetailList(DataTable dt)
        {

            List<SubproductDetailsList> objList = new List<SubproductDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                SubproductDetailsList obj = new SubproductDetailsList();
                obj.ID_MasterProduct = Convert.ToInt64(dr["ID_MasterProduct"].ToString());
                obj.MainProduct = Convert.ToString(dr["MainProduct"].ToString());
                obj.ID_Product = Convert.ToInt64(dr["ID_Product"].ToString());
                obj.Componant = Convert.ToString(dr["Componant"].ToString());
                obj.WarrantyMode = Convert.ToString(dr["WarrantyMode"].ToString());
                obj.ProductAmount = Convert.ToDecimal(dr["ProductAmount"].ToString());
                obj.ReplceMode = Convert.ToString(dr["ReplceMode"].ToString());
                obj.ID_CustomerWiseProductDetails = Convert.ToInt64(dr["ID_CustomerWiseProductDetails"].ToString());


                objList.Add(obj);
            }
            return objList;
        }
        public static FollowUpPaymentMethod ConvertFollowUpPaymentMethod(DataTable dt)
        {
            FollowUpPaymentMethod log = new FollowUpPaymentMethod();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.FollowUpPaymentMethodList = ConvertFollowUpPaymentMethodList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<FollowUpPaymentMethodList> ConvertFollowUpPaymentMethodList(DataTable dt)
        {

            List<FollowUpPaymentMethodList> objList = new List<FollowUpPaymentMethodList>();
            foreach (DataRow dr in dt.Rows)
            {
                FollowUpPaymentMethodList obj = new FollowUpPaymentMethodList();
                obj.ID_PaymentMethod = Convert.ToInt64(dr["ID_PaymentMethod"].ToString());
                obj.PaymentName = Convert.ToString(dr["PMName"].ToString());

                objList.Add(obj);
            }
            return objList;
        }
        public static UpdateServiceFollowUp ConvertUpdateServiceFollowUp(DataTable dt)
        {
            UpdateServiceFollowUp log = new UpdateServiceFollowUp();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "update successfully";
                log.Message = "update successfully";

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static MoreComponentDetails ConvertMoreComponentDetails(DataTable dt)
        {
            MoreComponentDetails log = new MoreComponentDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MoreComponentDetailsList = ConvertMoreComponentDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<MoreComponentDetailsList> ConvertMoreComponentDetailsList(DataTable dt)
        {

            List<MoreComponentDetailsList> objList = new List<MoreComponentDetailsList>();
            foreach (DataRow dr in dt.Rows)
            {
                MoreComponentDetailsList obj = new MoreComponentDetailsList();
                obj.FK_Product = Convert.ToString(dr["FK_Product"].ToString());
                obj.ProductName = Convert.ToString(dr["Product"].ToString());

                objList.Add(obj);
            }
            return objList;
        }
        public static MainProductDetails ConvertMainProductDetails(DataTable dt)
        {
            MainProductDetails log = new MainProductDetails();
            if (dt.Rows.Count > 0)
            {

                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                log.MainProductDetailsList = ConvertMainProductDetailsList(dt);

            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }
        public static List<MainProductDetailsList> ConvertMainProductDetailsList(DataTable dt)
        {
            return (from DataRow dr in dt.Rows
                    select new MainProductDetailsList()
                    {
                        ID_Product = Convert.ToInt64(dr["ID_Product"].ToString()),
                        ProdName = Convert.ToString(dr["ProdName"].ToString()),
                        ProdShortName = Convert.ToString(dr["ProdShortName"].ToString()),

                    }).ToList();
        }
        public static ServiceInvoice ConvertServiceInvoiceList(DataSet dts)
        {
            ServiceInvoice log = new ServiceInvoice();
            DataTable dt = dts.Tables[3];
            if (dt.Rows.Count > 0)
            {
                log.ResponseCode = "0";
                log.ResponseMessage = "Transaction Verified";
                //if (dts.Tables[3].Rows.Count > 0)
                    log.TicketDetails = ConvertServiceTicketDetails(dts.Tables[3]);
               // else
                  // log.TicketDetails = ConvertServiceTicketDetails(dts.Tables[3]);
              // if (dts.Tables[0].Rows.Count > 0)
                    log.ServiceInformation = ConvertServiceInformationDetails(dts.Tables[0]);
              // else
                 //   log.ServiceInformation = ConvertServiceInformationDetails(dts.Tables[0]);
               // if (dts.Tables[1].Rows.Count > 0)
                    log.ProductInformation = ConvertServiceProductInformation(dts.Tables[1]);
              //  if (dts.Tables[2].Rows.Count > 0)
                    log.AmountDetails = ConvertServiceAmountDetails(dts.Tables[2]);
              //  log.NetDetails = ConvertNetDetails(dts.Tables[4]);
            }
            else
            {
                log.ResponseCode = "-2";
                log.ResponseMessage = "No Data Found";
            }
            return log;
        }

        public static List<TicketDetails> ConvertServiceTicketDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new TicketDetails()
                    {
                        TicketNo = Convert.ToString(dr["TicketNo"]),
                        Customer = Convert.ToString(dr["Customer"].ToString()),
                        CusAddress = Convert.ToString(dr["CusAddress"].ToString()),
                         ClosedDate = Convert.ToString(dr["ClosedDate"].ToString()),
                          RegDate = Convert.ToString(dr["RegDate"].ToString()),
                          ID_CustomerServiceRegister= Convert.ToInt64(dr["ID_CustomerServiceRegister"]),
                    }).ToList();

        }
        public static List<ServiceInformation> ConvertServiceInformationDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ServiceInformation()
                    {
                        ServiceId = Convert.ToInt32(dr["ServiceId"]),
                        Service = Convert.ToString(dr["Service"].ToString()),
                        ServiceCost = Convert.ToDecimal(dr["ServiceCost"].ToString()),
                        TaxAmount = Convert.ToDecimal(dr["TaxAmount"].ToString()),
                        NetAmount = Convert.ToDecimal(dr["NetAmount"].ToString())
                    }).ToList();

        }
        public static List<ProductInformation> ConvertServiceProductInformation(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new ProductInformation()
                    {
                        Product = Convert.ToString(dr["Product"]),
                        Quantity = Convert.ToDecimal(dr["Quantity"].ToString()),
                        MRP = Convert.ToDecimal(dr["MRP"].ToString()),
                        SalePrice = Convert.ToDecimal(dr["SalePrice"].ToString()),
                        NetAmount = Convert.ToDecimal(dr["NetAmount"].ToString()),



                    }).ToList();

        }
        public static List<AmountDetails> ConvertServiceAmountDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new AmountDetails()
                    {
                        AdvanceAmount = Convert.ToDecimal(dr["AdvanceAmount"]),
                        SecurityAmount = Convert.ToDecimal(dr["SecurityAmount"].ToString()),
                        ServiceCharge = Convert.ToDecimal(dr["ServiceCharge"].ToString()),
                          DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"].ToString()),
                            ProductCharge = Convert.ToDecimal(dr["ProductsCharge"].ToString()),
                            NetAmount = Convert.ToDecimal(dr["NetAmount"].ToString())
                    }).ToList();

        }
        public static List<NetDetails> ConvertNetDetails(DataTable dt)
        {

            return (from DataRow dr in dt.Rows
                    select new NetDetails()
                    {
                        NetAmount = Convert.ToDecimal(dr["NetAmount"]),
                        ProductCharge = Convert.ToDecimal(dr["ProductCharge"].ToString()),
                        ServiceCharge = Convert.ToDecimal(dr["ServiceCharge"].ToString()),
                        AdvanceAmount = Convert.ToDecimal(dr["AdvanceAmount"]),
                        SecurityAmount = Convert.ToDecimal(dr["SecurityAmount"].ToString()),
                        DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"].ToString())

                    }).ToList();

        }
    }
}

