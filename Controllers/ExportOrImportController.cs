using ExcelDataReader;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    #region[Exensions]
    public static class Extensions
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table   
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
    #endregion
    public class ExportOrImportController : Controller
    {
        // GET: ExportOrImport
        #region[Index]
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        #endregion

        #region[LoadExportImport]
        public ActionResult LoadExportImport(string mtd)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            ExportImportModel.FormateView view = new ExportImportModel.FormateView();
            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            return PartialView("_AddExportImport", view);
        }
        #endregion

        #region[GetDataFromExcel]
        private class ValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; }

            public ValidationResult()
            {
                IsValid = true;
                Errors = new List<string>();
            }
        }
        #region[GetSalesExcel]
        private List<ExportImportModel.FormateView> GetDataFromExcel(Stream stream)
        {
            var validationResult = new ValidationResult();

            var importList = new List<ExportImportModel.FormateView>();
            int slNoCounter = 1; // Initialize the SlNo counter


            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        ExportImportModel.FormateView currentFormateView = null;

                        int rowNumber = 1; // Initialize row number counter
                        DateTime today = DateTime.Today; // Get today's date
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            rowNumber++; // Increment row number
                            if (dr.ItemArray.All(x => string.IsNullOrWhiteSpace(x?.ToString())))
                                continue;

                            decimal salePrice = 0;
                            decimal mrp = 0;
                            decimal discnt = 0;
                            decimal roundof = 0;
                            decimal qnty = 0;


                            // Validate SalePrice and MRP
                            string salePriceStr = dr["SalePrice"].ToString();
                            string mrpStr = dr["MRP"].ToString();
                            string discount = dr["Discount"].ToString();
                            string roundoff = dr["Roundoff"].ToString();


                            #region[Valiidate]

                            // Convert SalePrice and MRP to decimal
                            if (!decimal.TryParse(salePriceStr, out salePrice))
                            {
                                validationResult.IsValid = false;
                                //validationResult.Errors.Add($"Invalid Sale Price in row {rowNumber}. Please provide a valid numeric value.");
                                throw new Exception($"Invalid Sale Price in row {rowNumber}. Please provide a valid numeric value.");
                            }

                            if (!decimal.TryParse(mrpStr, out mrp))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid MRP in row {rowNumber}. Please provide a valid numeric value.");
                            }

                            if (mrp > 0 && salePrice > mrp)
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Sale Price cannot be greater than MRP in row {rowNumber}.");
                            }




                            if (!string.IsNullOrWhiteSpace(discount) && !decimal.TryParse(discount, out discnt))
                            {
                                throw new Exception($"Invalid Discount in row {rowNumber}. Please provide a valid numeric value.");
                            }


                            if (discnt > salePrice)
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Discount cannot be greater than Sales Price in row {rowNumber}.");
                            }


                            if (!string.IsNullOrWhiteSpace(roundoff) && !decimal.TryParse(roundoff, out roundof))
                            {
                                throw new Exception($"Invalid RoundOff in row {rowNumber}. Please provide a valid numeric value.");
                            }
                            else if (roundof < -0.99M || roundof > 0.99M)
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Roundoff in row {rowNumber} should be between -0.99 and 0.99.");
                            }


                            // Check mandatory fields 
                            string billDateStr = dr["BillDate"].ToString();
                            if (string.IsNullOrEmpty(billDateStr))
                            {
                                //validationResult.IsValid = false;
                                //validationResult.Errors.Add("BillDate is mandatory. Please provide a value.");

                                throw new Exception($"BillDate is mandatory. Please provide a value in row {rowNumber}.");
                            }

                            string billNumberStr = dr["BillNumber"].ToString();
                            if (string.IsNullOrEmpty(billNumberStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"BillNumber is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string productStr = dr["Product"].ToString();
                            if (string.IsNullOrEmpty(productStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Product is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }


                            string quantityStr = dr["Quantity"].ToString();
                            if (string.IsNullOrEmpty(quantityStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception("Quantity is mandatory. Please provide a value.");
                                //continue;
                            }
                            string customerStr = dr["CustomerName"].ToString();
                            if (string.IsNullOrEmpty(customerStr))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Customer Name is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string mobileNoStr = dr["MobileNo"].ToString();
                            if (string.IsNullOrEmpty(mobileNoStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Mobile No is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            if (string.IsNullOrEmpty(salePriceStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Sale Price is mandatory. Please provide a value in row {rowNumber}.");
                                // continue;
                            }
                            else if (salePrice <= 0)
                            {
                                throw new Exception($"Sale Price cannot be less than 1 in row {rowNumber}.");
                            }

                            if (string.IsNullOrEmpty(mrpStr))
                            {
                                throw new Exception($"MRP is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            else if (mrp <= 0)
                            {
                                throw new Exception($"MRP cannot be less than 1 in row {rowNumber}.");
                            }

                            // Create a key for SalBillNo and Product combination
                            string key = dr["BillNumber"].ToString() + "_" + dr["Product"].ToString();

                            // Check if the combination already exists in the importList
                            if (importList.Any(x => x.SalBillNo_Product == key))
                            {
                                // Throw an error or handle it appropriately
                                throw new Exception($"Duplicate combination of BillNo and Product in row {rowNumber}.");
                            }


                            if (!decimal.TryParse(quantityStr, out qnty))
                            {

                                throw new Exception($"Invalid Quantity in row {rowNumber}. Please provide a valid numeric value.");
                            }

                            if (qnty <= 0)
                            {
                                throw new Exception($"Quantity cannot be less than 1 in row {rowNumber}.");

                            }

                            #endregion

                            // Validate SaleBillDate

                            DateTime billDate;

                            if (!DateTime.TryParse(billDateStr, out billDate))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Bill Date in row {rowNumber}. Please provide a valid date.");
                            }
                            else if (billDate > today)
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Bill Date in row {rowNumber} cannot be a future date.");
                            }


                            currentFormateView = new ExportImportModel.FormateView
                            {
                                SalBillDate = Convert.ToDateTime(dr["BillDate"]).ToString("yyyy-MM-dd"),
                                SalBillNo = dr["BillNumber"].ToString(),
                                CustomerName = dr["CustomerName"].ToString(),
                                MobileNo = dr["MobileNo"].ToString(),
                                DelAddress1 = dr["DeliveryAddress"].ToString(),
                                DelAddress2 = dr["Place"].ToString(),
                                Area = dr["Area"].ToString(),
                                Post = dr["Post"].ToString(),
                                District = dr["District"].ToString(),
                                State = dr["State"].ToString(),
                                Country = dr["Country"].ToString(),
                                SalDiscount = dr["Discount"].ToString() == "" ? "0" : dr["Discount"].ToString(),
                                SalRoundoff = dr["Roundoff"].ToString() == "" ? "0" : dr["Roundoff"].ToString(),
                                Product = dr["Product"].ToString(),
                                SpdSalQuantity = dr["Quantity"].ToString() == "" ? "0" : dr["Quantity"].ToString(),
                                MRPs = dr["MRP"].ToString() == "" ? "0" : dr["MRP"].ToString(),
                                SalePrice = dr["SalePrice"].ToString() == "" ? "0" : dr["SalePrice"].ToString(),
                                SlNo = slNoCounter, // Set the SlNo value from the counter
                            };


                            // Add the key to the currentFormateView for future comparison
                            currentFormateView.SalBillNo_Product = key;

                            importList.Add(currentFormateView);
                            slNoCounter++; // Increment the SlNo counter for the next iteration
                        }


                        // After the loop, check if there are any errors
                        //if (!validationResult.IsValid)
                        //{
                        //    string errorMessages = string.Join("\n", validationResult.Errors);
                        //    throw new Exception("Data validation errors:\n" + errorMessages);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return importList;

        }
        #endregion

        #endregion

        #region[GetLeadExcel]
        private List<ExportImportModel.LeadDataView> GetLeadDataFromExcel(Stream stream)
        {
            var validationResult = new ValidationResult();

            var importLeadList = new List<ExportImportModel.LeadDataView>();
            int slNoCounter = 1; // Initialize the SlNo counter


            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        ExportImportModel.LeadDataView currentFormateLeadView = null;

                        int rowNumber = 1; // Initialize row number counter
                        DateTime today = DateTime.Today; // Get today's date
                        List<string> test = new List<string>();

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            rowNumber++; // Increment row number
                            if (dr.ItemArray.All(x => string.IsNullOrWhiteSpace(x?.ToString())))
                            //{
                              continue;

                            #region[Validate Excel Data]
                            
                            decimal mrp = 0;
                            decimal offrPrc = 0;
                            
                            string mrpStr = dr["MRP"].ToString();
                            string offrPrcSrt = dr["OfferPrice"].ToString();



                            // Check if Offer Price is not empty and convert it to decimal
                            if (!string.IsNullOrWhiteSpace(offrPrcSrt) && !decimal.TryParse(offrPrcSrt, out offrPrc))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Offer Price in row {rowNumber}. Please provide a valid numeric value.");
                            }

                            // Check if MRP is not empty and convert it to decimal
                            if (!string.IsNullOrWhiteSpace(mrpStr) && !decimal.TryParse(mrpStr, out mrp))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid MRP in row {rowNumber}. Please provide a valid numeric value.");
                            }


                            #region[Check mandatory fields] 
                            string enquiryDateStr = dr["EnquiryDate"].ToString();
                            if (string.IsNullOrWhiteSpace(enquiryDateStr))
                            {
                                // If the string is empty or contains only whitespace characters
                                throw new Exception($"EnquiryDate is mandatory. Please provide a value in row {rowNumber}.");
                            }


                            string customerNmeStr = dr["CustomerName"].ToString();
                            if (string.IsNullOrWhiteSpace(customerNmeStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"CustomerName is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string contactNoStr = dr["ContactNo"].ToString();
                            if (string.IsNullOrWhiteSpace(contactNoStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"ContactNo is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }


                            string CategoryStr = dr["Category"].ToString();
                            if (string.IsNullOrWhiteSpace(CategoryStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Category is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string priorityStr = dr["Priority"].ToString();
                            if (string.IsNullOrWhiteSpace(priorityStr))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Priority  is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string enqNotStr = dr["EnquiryNote"].ToString();
                            if (string.IsNullOrWhiteSpace(enqNotStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Enquiry Note is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string actionStr = dr["Action"].ToString();
                            if (string.IsNullOrWhiteSpace(actionStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Action is mandatory. Please provide a value in row {rowNumber}.");
                                // continue;
                            }

                            string follupStr = dr["FollowUpThrough"].ToString();
                            //if (string.IsNullOrWhiteSpace(follupStr))
                            //{
                            //    throw new Exception($"FollowUp Through is mandatory. Please provide a value in row {rowNumber}.");
                            //}

                            string followupDateStr = dr["FollowUpDate"].ToString();
                            //if (string.IsNullOrWhiteSpace(followupDateStr))
                            //{
                            //    throw new Exception($"FollowUp Date is mandatory. Please provide a value in row {rowNumber}.");
                            //}

                            string assignedToStr = dr["AssignedTo"].ToString();
                            //if (string.IsNullOrWhiteSpace(assignedToStr))
                            //{
                            //    throw new Exception($"AssignedTo is mandatory. Please provide a value in row {rowNumber}.");
                            //}


                            #endregion


                            //Validate Enquiry Date and FollowUp Date
                            DateTime enqDate;
                            DateTime follDate;

                            if (!DateTime.TryParse(enquiryDateStr, out enqDate))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Enquiry Date in row {rowNumber}. Please provide a valid date.");
                            }
                            else if (enqDate > today)
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Enquiry Date in row {rowNumber} cannot be a future date.");
                            }
                            if (!string.IsNullOrWhiteSpace(followupDateStr))
                            {
                                if (!DateTime.TryParse(followupDateStr, out follDate))
                                {
                                    throw new Exception($"Invalid FollowUp Date in row {rowNumber}. Please provide a valid date.");
                                }

                                if(follDate < enqDate)
                                {
                                    throw new Exception($"Follow up date should be greater than or equal to Enquiry date in row {rowNumber}.");
                                }

                            }
                            

                            decimal Prodct = 0;
                            string ProductOrProjectStr = dr["ProductOrProject"].ToString();
                            string ProductQuantityStr = dr["ProductQuantity"].ToString();

                            if (!string.IsNullOrWhiteSpace(ProductOrProjectStr))
                            {

                                if (!decimal.TryParse(ProductQuantityStr, out Prodct))
                                {
                                    throw new Exception($"Invalid Product Quantity in row {rowNumber}. Please provide a valid numeric value.");
                                }

                                if (Prodct <= 0 )
                                {
                                    throw new Exception($"Please enter Product Quantity greater than 0 in row {rowNumber}.");
                                }
                            }



                            #endregion

                            #region[Lenght Check]

                            string mobno = dr["ContactNo"].ToString();
                            string WhatsAppNo1 = dr["WhatsAppNo"].ToString();

                            if (!string.IsNullOrEmpty(mobno) && mobno.Length > 15)
                            {
                                throw new Exception($"Contact No should not exceed 15 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(mobno) && mobno.Length < 9)
                            {
                                throw new Exception($"Contact No Should be More than 9 Characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(WhatsAppNo1) && WhatsAppNo1.Length > 15)
                            {
                                throw new Exception($"WhatsApp No should not exceed 15 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(WhatsAppNo1) && WhatsAppNo1.Length < 9)
                            {
                                throw new Exception($"WhatsApp No Should be More than 9 Characters in row {rowNumber}.");
                            }


                            // Validate Contact No
                            if (!string.IsNullOrEmpty(mobno))
                            {
                                // Check if Contact No contains only numeric digits and '+'
                                if (!Regex.IsMatch(mobno, @"^[0-9+]+$"))
                                {
                                    throw new Exception($"Contact No should only contain numeric digits in row {rowNumber}.");
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(WhatsAppNo1))
                            {
                                if (!Regex.IsMatch(WhatsAppNo1, @"^[0-9+]+$"))
                                {
                                    throw new Exception($"WhatsApp No should only contain numeric digits in row {rowNumber}.");
                                }
                            }
                                int  maxlenght = 150;

                            string CompanyOrContactPerson1 = dr["CompanyOrContactPerson"].ToString();
                            string CustomerName1 = dr["CustomerName"].ToString();
                            string EnquiryNote1 = dr["EnquiryNote"].ToString();
                            string CollectedBy1 = dr["CollectedBy"].ToString();
                            string FollowUpThrough1 = dr["FollowUpThrough"].ToString();
                            string AssignedTo1 = dr["AssignedTo"].ToString();

                            if (!string.IsNullOrEmpty(CompanyOrContactPerson1) && CompanyOrContactPerson1.Length > maxlenght)
                            {
                                throw new Exception($"Company Or ContactPerson  should not exceed 150 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(CustomerName1) && CustomerName1.Length > maxlenght)
                            {
                                throw new Exception($"Customer Name should not exceed 150 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(EnquiryNote1) && EnquiryNote1.Length > maxlenght)
                            {
                                throw new Exception($"Enquiry Note  should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(CollectedBy1) && CollectedBy1.Length > maxlenght)
                            {
                                throw new Exception($"CollectedBy should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(FollowUpThrough1) && FollowUpThrough1.Length > maxlenght)
                            {
                                throw new Exception($"FollowUp Through should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(AssignedTo1) && AssignedTo1.Length > maxlenght)
                            {
                                throw new Exception($"Assigned To should not exceed 150 characters in row {rowNumber}.");
                            }

                            int maxLength500 = 500;

                            string LeadFrom = dr["LeadFrom"].ToString();
                            string HouseName1 = dr["HouseName"].ToString();
                            string Place1 = dr["Place"].ToString();
                            string Country1 = dr["Country"].ToString();
                            string State1 = dr["State"].ToString();
                            string District1 = dr["District"].ToString();
                            string Area1 = dr["Area"].ToString();
                            string Post1 = dr["Post"].ToString();
                            string Floor1 = dr["Floor"].ToString();
                            string Action1 = dr["Action"].ToString();

                            if (!string.IsNullOrEmpty(LeadFrom) && LeadFrom.Length > maxLength500)
                            {
                                throw new Exception("LeadFrom should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(HouseName1) && HouseName1.Length > maxLength500)
                            {
                                throw new Exception("HouseName should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(Place1) && Place1.Length > maxLength500)
                            {
                                throw new Exception("Place should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(Country1) && Country1.Length > maxLength500)
                            {
                                throw new Exception("Country should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(State1) && State1.Length > maxLength500)
                            {
                                throw new Exception("State should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(District1) && District1.Length > maxLength500)
                            {
                                throw new Exception("District should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(Area1) && Area1.Length > maxLength500)
                            {
                                throw new Exception("Area should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(Post1) && Post1.Length > maxLength500)
                            {
                                throw new Exception("Post should not exceed 500 characters.");
                            }

                            if (!string.IsNullOrEmpty(Floor1) && Floor1.Length > maxLength500)
                            {
                                throw new Exception("Floor should not exceed 500 characters.");
                            }
                            if (!string.IsNullOrEmpty(Action1) && Action1.Length > maxLength500)
                            {
                                throw new Exception("Action should not exceed 500 characters.");
                            }



                            #endregion

                            currentFormateLeadView = new ExportImportModel.LeadDataView
                                {
                                    EnquiryDate = Convert.ToDateTime(dr["EnquiryDate"]).ToString("yyyy-MM-dd"),
                                    CollectedBy = dr["CollectedBy"].ToString(),
                                    CustomerName = dr["CustomerName"].ToString(),
                                    ContactNo = dr["ContactNo"].ToString(),
                                    Category = dr["Category"].ToString(),
                                    ProductOrProject = dr["ProductOrProject"].ToString(),
                                    Priority = dr["Priority"].ToString(),
                                    EnquiryNote = dr["EnquiryNote"].ToString(),
                                    Action = dr["Action"].ToString(),
                                    FollowUpThrough = dr["FollowUpThrough"].ToString(),
                                    FollowUpDate = dr["FollowUpDate"] != DBNull.Value ? Convert.ToDateTime(dr["FollowUpDate"]).ToString("yyyy-MM-dd") : "" ,
                                    AssignedTo = dr["AssignedTo"].ToString(),
                                    LeadSource = dr["LeadSource"].ToString(),
                                    LeadFrom = dr["LeadFrom"].ToString(),
                                    WhatsAppNo = dr["WhatsAppNo"].ToString(),
                                    CompanyOrContactPerson = dr["CompanyOrContactPerson"].ToString(),
                                    ContactEmail = dr["ContactEmail"].ToString(),
                                    HouseName = dr["HouseName"].ToString(),
                                    Place = dr["Place"].ToString(),
                                    Country = dr["Country"].ToString(),
                                    State = dr["State"].ToString(),
                                    District = dr["District"].ToString(),
                                    Area = dr["Area"].ToString(),
                                    Post = dr["Post"].ToString(),
                                    ProductQuantity = dr["ProductQuantity"].ToString() == "" ? "0" : dr["ProductQuantity"].ToString(),
                                    MRP = dr["MRP"].ToString() == "" ? "0" : dr["MRP"].ToString(),
                                    OfferPrice = dr["OfferPrice"].ToString() == "" ? "0" : dr["OfferPrice"].ToString(),
                                    Floor = dr["Floor"].ToString(),

                                    SlNo = slNoCounter,
                                };


                                importLeadList.Add(currentFormateLeadView);
                                slNoCounter++;
                           
                      
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return importLeadList;

        }
        #endregion

        #region[GetServiceDataFromExcel]
        private List<ExportImportModel.ServiceDataView> GetServiceDataFromExcel(Stream stream)
        {
            var validationResult = new ValidationResult();

            var importServiceList = new List<ExportImportModel.ServiceDataView>();
            int slNoCounter = 1; // Initialize the SlNo counter


            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });

                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        ExportImportModel.ServiceDataView currentFormateServiceView = null;

                        int rowNumber = 1; // Initialize row number counter
                        DateTime today = DateTime.Today; // Get today's date
                        List<string> test = new List<string>();

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            rowNumber++; // Increment row number
                            if (dr.ItemArray.All(x => string.IsNullOrWhiteSpace(x?.ToString())))
                                //{
                                continue;


                            #region[Check mandatory fields] 

                            string regDateStr = dr["Register Date"].ToString();
                            string reqToDateStr = dr["RequestedDate To"].ToString();
                            string reqFromDateStr = dr["RequestedDate From"].ToString();
                            if (string.IsNullOrWhiteSpace(regDateStr))
                            {
                                // If the string is empty or contains only whitespace characters
                                throw new Exception($"Register Date is mandatory. Please provide a value in row {rowNumber}.");
                            }


                            string registerTimeStr = dr["Register Time"].ToString();
                            if (string.IsNullOrWhiteSpace(regDateStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Register Time is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string customertStr = dr["Customer"].ToString();
                            if (string.IsNullOrWhiteSpace(customertStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Customer is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }


                            string CategoryStr = dr["Category"].ToString();
                            if (string.IsNullOrWhiteSpace(CategoryStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Category is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string priorityStr = dr["Priority"].ToString();
                            if (string.IsNullOrWhiteSpace(priorityStr))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Priority  is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string mobileStr = dr["MobileNo"].ToString();
                            if (string.IsNullOrWhiteSpace(mobileStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Mobile No. is mandatory. Please provide a value in row {rowNumber}.");
                                //continue;
                            }

                            string typeStr = dr["Type"].ToString();
                            if (string.IsNullOrWhiteSpace(typeStr))
                            {
                                //validationResult.IsValid = false;
                                throw new Exception($"Type is mandatory. Please provide a value in row {rowNumber}.");
                                // continue;
                            }

                            string CmSrStr = dr["ComplaintOrService"].ToString();
                            if (string.IsNullOrWhiteSpace(CmSrStr))
                            {
                                throw new Exception($"Compliant/Service is mandatory. Please provide a value in row {rowNumber}.");
                            }

                            string descriptionStr = dr["Description"].ToString();
                            if (string.IsNullOrWhiteSpace(descriptionStr))
                            {
                                throw new Exception($"Description is mandatory. Please provide a value in row {rowNumber}.");
                            }

                            #endregion


                            //Validate Enquiry Date and FollowUp Date
                            #region[DateTime]
                            DateTime regDate;
                            DateTime reqTo;
                            DateTime reqFrom;

                            if (!DateTime.TryParse(regDateStr, out regDate))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Register Date in row {rowNumber}. Please provide a valid date.");
                            }

                            if (!string.IsNullOrWhiteSpace(reqFromDateStr))
                            {
                                if (!DateTime.TryParse(reqFromDateStr, out reqFrom))
                                {
                                    validationResult.IsValid = false;
                                    throw new Exception($"Invalid RequestedDate From in row {rowNumber}. Please provide a valid date.");
                                }
                                else if (reqFrom < today)
                                {
                                    validationResult.IsValid = false;
                                    throw new Exception($"RequestedDate From should be greater than or equals to todays date in row {rowNumber}.");
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(reqToDateStr))
                            {
                                if (!DateTime.TryParse(reqToDateStr, out reqTo))
                                {
                                    validationResult.IsValid = false;
                                    throw new Exception($"Invalid RequestedDate To in row {rowNumber}. Please provide a valid date.");
                                }
                               
                            }


                            if(DateTime.TryParse(reqToDateStr, out reqTo) && DateTime.TryParse(reqFromDateStr, out reqFrom))
                            {
                                if(reqTo < reqFrom)
                                {
                                    validationResult.IsValid = false;
                                    throw new Exception($"Requested Date To should be greater than or equal to Requested Date From in row {rowNumber}.");
                                }
                            }


                            string ReqTimeTo = dr["RequestedTime To"].ToString();
                            string ReqTimeFrom = dr["RequestedTime From"].ToString();

                            TimeSpan TimeTo;
                            TimeSpan TimeFrom;

                            if (string.IsNullOrWhiteSpace(ReqTimeFrom))
                            {
                                // Time is not provided, set it to 00:00
                                TimeFrom = TimeSpan.Zero;
                            }
                            else if (TimeSpan.TryParse(ReqTimeFrom, out TimeFrom))
                            {
                                // Time is provided, parse it
                            }
                            

                            // Similar check for ReqTimeTo
                            if (string.IsNullOrWhiteSpace(ReqTimeTo))
                            {
                                // Time is not provided, set it to 00:00
                                TimeTo = TimeSpan.Zero;
                            }
                            else if (TimeSpan.TryParse(ReqTimeTo, out TimeTo))
                            {
                                // Time is provided, parse it
                            }
                           
                           

                            if (!string.IsNullOrWhiteSpace(ReqTimeTo))
                            {
                                if (TimeTo < TimeFrom)
                                {
                                    validationResult.IsValid = false;
                                    throw new Exception($"Requested Time To should be greater than or equal to Requested Time From  in row {rowNumber}.");
                                }
                            }
                            #endregion

                            #region [length]
                            string Product1 = dr["Product"].ToString();
                            string ContactNo1 = dr["Contact No"].ToString();
                            string MobileNo1 = dr["MobileNo"].ToString();

                            string HouseName = dr["House Name"].ToString();
                            string Place = dr["Place"].ToString();
                            string Post = dr["Post"].ToString();
                            string Area = dr["Area"].ToString();
                            string District = dr["District"].ToString();
                            string Landmark = dr["Landmark"].ToString();
                            string Description = dr["Description"].ToString();

                            if (!string.IsNullOrEmpty(Product1) && Product1.Length > 250)
                            {
                                throw new Exception($"Product should not exceed 150 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(ContactNo1) && ContactNo1.Length > 15)
                            {
                                throw new Exception($"ContactNo should not exceed 15 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(MobileNo1) && MobileNo1.Length > 15)
                            {
                                throw new Exception($"MobileNo should not exceed 15 characters in row {rowNumber}.");
                            }


                            #region[Define the maximum length]

                            int maxLength = 500; // Define the maximum length

                            if (!string.IsNullOrEmpty(HouseName) && HouseName.Length > maxLength)
                            {
                                throw new Exception($"HouseName should not exceed 500 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(Place) && Place.Length > maxLength)
                            {
                                throw new Exception($"Place should not exceed 500 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(Post) && Post.Length > maxLength)
                            {
                                throw new Exception($"Post should not exceed 500 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(Area) && Area.Length > maxLength)
                            {
                                throw new Exception($"Area should not exceed 500 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(District) && District.Length > maxLength)
                            {
                                throw new Exception($"District should not exceed 500 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(Landmark) && Landmark.Length > maxLength)
                            {
                                throw new Exception($"Landmark should not exceed 500 characters in row {rowNumber}.");
                            }

                            if (!string.IsNullOrEmpty(Description) && Description.Length > maxLength)
                            {
                                throw new Exception($"Description should not exceed 500 characters in row {rowNumber}.");
                            }

                            #endregion

                            #endregion


                            var reqTimeToStr = dr["RequestedTime To"].ToString();
                            
                            currentFormateServiceView = new ExportImportModel.ServiceDataView
                            {
                                RegisterDate = Convert.ToDateTime(dr["Register Date"]).ToString("yyyy-MM-dd"),
                                RegisterTime = Convert.ToDateTime(dr["Register Time"]).ToString("HH:mm"),
                                Customer = dr["Customer"].ToString(),   
                                MobileNo = dr["MobileNo"].ToString(),
                                Category = dr["Category"].ToString(),
                                Product = dr["Product"].ToString(),
                                Type = dr["Type"].ToString(),
                                ComplaintOrService = dr["ComplaintOrService"].ToString(),
                                Priority = dr["Priority"].ToString(),
                                Description = dr["Description"].ToString(),
                                ContactNo = dr["Contact No"].ToString(),
                                HouseName = dr["House Name"].ToString(),
                                Place = dr["Place"].ToString(),
                                Post = dr["Post"].ToString(),
                                Area = dr["Area"].ToString(),
                                District = dr["District"].ToString(),
                                Landmark = dr["Landmark"].ToString(),
                                RequestedDateFrom = dr["RequestedDate From"] != DBNull.Value ? Convert.ToDateTime(dr["RequestedDate From"]).ToString("yyyy-MM-dd") : "",
                                RequestedDateTo = dr["RequestedDate To"] != DBNull.Value ? Convert.ToDateTime(dr["RequestedDate To"]).ToString("yyyy-MM-dd") : "",
                                RequestedTimeFrom = dr["RequestedTime From"] != DBNull.Value ? Convert.ToDateTime(dr["RequestedTime From"]).ToString("HH:mm") : "00:00",
                                RequestedTimeTo = dr["RequestedTime To"] != DBNull.Value ? Convert.ToDateTime(dr["RequestedTime To"]).ToString("HH:mm") : "00:00",
                                ComplaintMedia = dr["Complaint Media"].ToString(),
                                SlNo = slNoCounter,
                            };


                            importServiceList.Add(currentFormateServiceView);
                            slNoCounter++;
                            
                        }
                     
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return importServiceList;

        }


        #endregion

        #region[GetProductDataFromExcel]
        private List<ExportImportModel.ProductDataView> GetProductDataFromExcel(Stream stream)
        {

            var validationResult = new ValidationResult();

            var importProductList = new List<ExportImportModel.ProductDataView>();
            int slNoCounter = 1;

            try
            {
                using(var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // To set First Row As Column Names    
                        }
                    });
                    if(dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];
                        ExportImportModel.ProductDataView productDataView = null;

                        int rowNumber = 1;// Initialize row number counter
                        DateTime today = DateTime.Today;

                        foreach(DataRow dr in dataTable.Rows)
                        {
                            rowNumber++; // Increment row number

                            #region[CheckMandatoryFields]

                            string strDep = dr["Department"].ToString();
                            string strCat = dr["Category"].ToString();
                            string strProd = dr["Product"].ToString();
                            string strUnt = dr["Unit"].ToString();
                            string strAltUnt = dr["Alt unit"].ToString();
                            string strTaxPrct = dr["Tax Group"].ToString();

                            if (string.IsNullOrWhiteSpace(strDep))
                            {
                                throw new Exception($"Department is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            if (string.IsNullOrWhiteSpace(strCat))
                            {
                                throw new Exception($"Category is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            if (string.IsNullOrWhiteSpace(strProd))
                            {
                                throw new Exception($"Product is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            if (string.IsNullOrWhiteSpace(strUnt))
                            {
                                throw new Exception($"Unit is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            if (string.IsNullOrWhiteSpace(strAltUnt))
                            {
                                throw new Exception($"Alt Unit is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            if (string.IsNullOrWhiteSpace(strTaxPrct))
                            {
                                throw new Exception($"Tax Group is mandatory. Please provide a value in row {rowNumber}.");
                            }
                            #endregion

                            #region[MaxLength Checking]

                            string strSubCat = dr["Sub Category"].ToString();
                            string strProdDes = dr["Product Description"].ToString();
                            string strManf = dr["Manufacture"].ToString();
                            string strHsn = dr["HSN Code"].ToString();
                            string _mrpStr = dr["MRP"].ToString();
                            string _saleStr = dr["Sale Price"].ToString();
                            string _purStr = dr["Pur Rate"].ToString();
                            string _opnStr = dr["Opening Qty"].ToString();
                            string QRCode = dr["QR Code"].ToString();
                            string BarCode = dr["Bar Code"].ToString();
                            string TaxPercentage = dr["Tax Group"].ToString();
                                

                            if (!string.IsNullOrEmpty(strDep) && strDep.Length>150 )
                            {
                                throw new Exception($"Department should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strCat) && strCat.Length > 150)
                            {
                                throw new Exception($"Category should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strSubCat) && strSubCat.Length > 150)
                            {
                                throw new Exception($"Sub Category should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strProd) && strProd.Length > 150)
                            {
                                throw new Exception($"Product should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strProdDes) && strProdDes.Length > 250)
                            {
                                throw new Exception($"Product Description should not exceed 250 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strManf) && strManf.Length > 150)
                            {
                                throw new Exception($"Manufacture should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strHsn) && strHsn.Length > 15)
                            {
                                throw new Exception($"HSN Code should not exceed 15 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strUnt) && strUnt.Length > 150)
                            {
                                throw new Exception($"Unit should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(strAltUnt) && strAltUnt.Length > 150)
                            {
                                throw new Exception($"Alt Unit should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(QRCode) && QRCode.Length > 15)
                            {
                                throw new Exception($"QR Code should not exceed 15 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(BarCode) && BarCode.Length > 15)
                            {
                                throw new Exception($"Bar Code should not exceed 15 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(TaxPercentage) && TaxPercentage.Length > 150)
                            {
                                throw new Exception($"Tax Group should not exceed 150 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(_mrpStr) && _mrpStr.Length > 12)
                            {
                                throw new Exception($"MRP should not exceed 12 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(_saleStr) && _saleStr.Length > 12)
                            {
                                throw new Exception($"Sale Price should not exceed 12 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(_purStr) && _purStr.Length > 12)
                            {
                                throw new Exception($"Pur Rate should not exceed 12 characters in row {rowNumber}.");
                            }
                            if (!string.IsNullOrEmpty(_opnStr) && _opnStr.Length > 10)
                            {
                                throw new Exception($"Opening Qty should not exceed 10 characters in row {rowNumber}.");
                            }
                            #endregion

                            #region[Validate Excel Data]

                            decimal mrp = 0;
                            decimal salePrc = 0;
                            decimal purRate = 0;
                            decimal opnQty = 0;

                            // Check if MRP is not empty and convert it to decimal
                            if (!string.IsNullOrWhiteSpace(_mrpStr) && !decimal.TryParse(_mrpStr, out mrp))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid MRP in row {rowNumber}. Please provide a valid numeric value.");
                            }
                            if (!string.IsNullOrWhiteSpace(_saleStr) && !decimal.TryParse(_saleStr, out salePrc))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Sale Price in row {rowNumber}. Please provide a valid numeric value.");
                            }
                            if (!string.IsNullOrWhiteSpace(_purStr) && !decimal.TryParse(_purStr, out purRate))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Purchase Rate in row {rowNumber}. Please provide a valid numeric value.");
                            }
                            if (!string.IsNullOrWhiteSpace(_opnStr) && !decimal.TryParse(_opnStr, out opnQty))
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Invalid Opening Quantity in row {rowNumber}. Please provide a valid numeric value.");
                            }

                            if (mrp > 0 && salePrc > mrp)
                            {
                                validationResult.IsValid = false;
                                throw new Exception($"Sale Price cannot be greater than MRP in row {rowNumber}.");
                            }


                            #endregion

                            productDataView = new ExportImportModel.ProductDataView
                            {
                                Department = dr["Department"].ToString(),
                                Category = dr["Category"].ToString(),
                                SubCategory = dr["Sub Category"].ToString(),
                                Product = dr["Product"].ToString(),
                                ProductDescription = dr["Product Description"].ToString(),
                                Manufacture = dr["Manufacture"].ToString(),
                                HSNCode = dr["HSN Code"].ToString(),
                                Unit = dr["Unit"].ToString(),
                                AltUnit = dr["Alt unit"].ToString(),
                                QRCode = dr["QR Code"].ToString(),
                                BarCode = dr["Bar Code"].ToString(),
                                TaxPercentage = dr["Tax Group"].ToString(),
                                MRP = dr["MRP"].ToString(),
                                SalePrice = dr["Sale Price"].ToString(),
                                PurRate = dr["Pur Rate"].ToString(),
                                OpeningQty = dr["Opening Qty"].ToString(),
                                SlNo = slNoCounter,
                            };

                            importProductList.Add(productDataView);
                            slNoCounter++;

                        }


                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return importProductList;

        }
        #endregion

        #region [ImportedFileSales]
        [HttpPost]
        public ActionResult ImportedFile(HttpPostedFileBase file,int ExType)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            if (file == null || file.ContentLength == 0)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "No file was uploaded" },
                        Status = "File Upload Error",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ExportImportModel export = new ExportImportModel();

            try
            {

                if (ExType == 1)
                {
                    var fileData = GetDataFromExcel(file.InputStream);
                    return Json(new { fileData, ExType, Process = new Output { IsProcess = true, Message = new List<string> { } } }, JsonRequestBehavior.AllowGet);
                }
                else if(ExType == 2){
                    var fileData = GetLeadDataFromExcel(file.InputStream);
                    return Json(new { fileData, ExType, Process = new Output { IsProcess = true, Message = new List<string> { } } }, JsonRequestBehavior.AllowGet);
                }
                else if(ExType == 3)
                {
                    var fileData = GetServiceDataFromExcel(file.InputStream);
                    return Json(new { fileData, ExType, Process = new Output { IsProcess = true, Message = new List<string> { } } }, JsonRequestBehavior.AllowGet);
                }
                else if(ExType == 4)
                {
                    var fileData = GetProductDataFromExcel(file.InputStream);
                    return Json(new { fileData, ExType, Process = new Output { IsProcess = true, Message = new List<string> { } } }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> {"Something went wrong" } } }, JsonRequestBehavior.AllowGet);
                }


                
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occurred while processing the Excel file: " + ex.Message;
                return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { errorMessage } } });
            }

        }
        #endregion

        #region[SaveExcelData]
        [HttpPost]
        public ActionResult SaveExcelData(ExportImportModel.SaveExceldata formate)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ExportImportModel export = new ExportImportModel();

            var Tval = formate.TypeForm;
            string itemdata = "";
            if (Tval == 1)
            {
                formate.dataitem.All(x => x.SalBillDate == "");
                itemdata = formate.dataitem is null ? "" : Common.xmlTostring(formate.dataitem);

            }
            else if (Tval ==2)
            {
                itemdata = formate.leadDatas is null ? "" : Common.xmlTostring(formate.leadDatas);
            }
            else if(Tval == 3){
                itemdata = formate.serviceData is null ? "" : Common.xmlTostring(formate.serviceData);
            }
            else
            {
                itemdata = formate.productData is null ? "" : Common.xmlTostring(formate.productData);
            }

            try
            {
                var result = export.UpdateSalesData(

                    new ExportImportModel.ExportUpdate
                    {

                        TransMode = "INSL",
                        UserAction = 1,
                        Debug = 0,
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                        EntrBy = _userLoginInfo.EntrBy,
                        FK_Machine = _userLoginInfo.FK_Machine,
                        FK_Branch = _userLoginInfo.FK_Branch,
                        SalesDataDetails = itemdata,
                        FK_Department = _userLoginInfo.FK_Department,
                        ImportFrom = Tval

                    }, companyKey: _userLoginInfo.CompanyKey);
                if (result.Data[0].ErrCode == "-200")
                {
                    string errMsg = result.Data[0].ErrMsg;
                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { errMsg } } });
                }
                else if (result.Data[0].ErrCode == "-1")
                {
                    string errMsg = "Error occurred while processing the Excel file";/*result.Data[0].ErrMsg;*/
                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { errMsg } } });
                }
                return Json(new { result,data=result.Data, result.Process }, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                string errorMessage = "Error occurred while processing the Excel file: " + ex.Message;
                return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { errorMessage } } });
            }

            
        }
        #endregion


    }
}