using Newtonsoft.Json;
using PerfectWebERPAPI.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using static PerfectWebERPAPI.Business.BlCustomerPortalAPIModel;

namespace PerfectWebERPAPI.Business
{
    class BlCustomerPortalAPIFormat
    {

        public static CustomerInfoMobileModel ConvertToCustomerLoginOutput(DataTable dt)
        {

            CustomerInfoMobileModel output = new CustomerInfoMobileModel();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    output.BranchID = DBNull.Value.Equals(dr["BranchID"]) ? default(Int64) : Convert.ToInt64(dr["BranchID"]);
                    output.BranchUserCode = DBNull.Value.Equals(dr["BranchUserCode"]) ? default(Int64) : Convert.ToInt64(dr["BranchUserCode"]);
                    output.CustomerID = DBNull.Value.Equals(dr["CustomerID"]) ? default(Int64) : Convert.ToInt64(dr["CustomerID"]);
                    output.CustomerRoleID = DBNull.Value.Equals(dr["CustomerRoleID"]) ? default(byte) : Convert.ToByte(dr["CustomerRoleID"]);
                    output.MPIN = DBNull.Value.Equals(dr["MPIN"]) ? "" : Convert.ToString(dr["MPIN"]);
                    output.Role = DBNull.Value.Equals(dr["Role"]) ? "" : Convert.ToString(dr["Role"]);
                }
            }



            return output;


        }
        public static CustomerManagePIN ConvertToCustomerManagePIN(DataTable dt)
        {

            CustomerManagePIN output = new CustomerManagePIN();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    output.OTP = DBNull.Value.Equals(dr["OTP"]) ? "" : Convert.ToString(dr["OTP"]);
                    output.MPIN = DBNull.Value.Equals(dr["MPIN"]) ? "" : Convert.ToString(dr["MPIN"]);
                    output.Process = DBNull.Value.Equals(dr["Process"]) ? default(bool) : Convert.ToBoolean(dr["Process"]);
                    output.HasMPIN = DBNull.Value.Equals(dr["HasMPIN"]) ? default(bool) : Convert.ToBoolean(dr["HasMPIN"]);
                    output.ID_User = DBNull.Value.Equals(dr["ID_User"]) ? 0 : Convert.ToInt64(dr["ID_User"]);

                }
            }

            return output;


        }

        public static CompanyInfoOutput ConvertToCompanyInfoOutput(DataTable dt)
        {
            CompanyInfoOutput output = new CompanyInfoOutput();

            CompanyInfoModel model = new CompanyInfoModel();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    model.ID_Company = DBNull.Value.Equals(dr["ID_Company"]) ? default(Int64) : Convert.ToInt64(dr["ID_Company"]);
                    model.CompName = DBNull.Value.Equals(dr["CompName"]) ? "" : Convert.ToString(dr["CompName"]);
                    model.CompContactPerson = DBNull.Value.Equals(dr["CompContactPerson"]) ? "" : Convert.ToString(dr["CompContactPerson"]);
                    model.CompContactPMobile = DBNull.Value.Equals(dr["CompContactPMobile"]) ? "" : Convert.ToString(dr["CompContactPMobile"]);
                    model.CompContactPEmail = DBNull.Value.Equals(dr["CompContactPEmail"]) ? "" : Convert.ToString(dr["CompContactPEmail"]);

                    model.CompAddress1 = DBNull.Value.Equals(dr["CompAddress1"]) ? "" : Convert.ToString(dr["CompAddress1"]);
                    model.CompAddress2 = DBNull.Value.Equals(dr["CompAddress2"]) ? "" : Convert.ToString(dr["CompAddress2"]);
                    model.CompAddress3 = DBNull.Value.Equals(dr["CompAddress3"]) ? "" : Convert.ToString(dr["CompAddress3"]);

                    model.CompEmail = DBNull.Value.Equals(dr["CompEmail"]) ? "" : Convert.ToString(dr["CompEmail"]);
                    model.CompMobile = DBNull.Value.Equals(dr["CompMobile"]) ? "" : Convert.ToString(dr["CompMobile"]);
                    model.CompPhone = DBNull.Value.Equals(dr["CompPhone"]) ? "" : Convert.ToString(dr["CompPhone"]);
                    model.CompFax = DBNull.Value.Equals(dr["CompFax"]) ? "" : Convert.ToString(dr["CompFax"]);

                    model.CompWebSite = DBNull.Value.Equals(dr["CompWebSite"]) ? "" : Convert.ToString(dr["CompWebSite"]);
                    model.CompSocialMediaWebsite1 = DBNull.Value.Equals(dr["CompSocialMediaWebsite1"]) ? "" : Convert.ToString(dr["CompSocialMediaWebsite1"]);
                    model.CompSocialMediaWebsite2 = DBNull.Value.Equals(dr["CompSocialMediaWebsite2"]) ? "" : Convert.ToString(dr["CompSocialMediaWebsite2"]);
                    model.CompLogo = DBNull.Value.Equals(dr["CompLogo"]) ? "" : "data:image /; base64," + Convert.ToBase64String((byte[])dr["CompLogo"]);

                }
            }

            output.Data = model;

            return output;
        }

        public static CustomerInfoOutput ConvertToCustomerInfoOutput(DataTable dt)
        {
            CustomerInfoOutput output = new CustomerInfoOutput();

            CustomerInfoModel model = new CustomerInfoModel();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    model.CustomerID = DBNull.Value.Equals(dr["CustomerID"]) ? default(Int64) : Convert.ToInt64(dr["CustomerID"]);
                    model.CustomerName = DBNull.Value.Equals(dr["CustomerName"]) ? "" : Convert.ToString(dr["CustomerName"]);
                    model.CustomerNumber = DBNull.Value.Equals(dr["CustomerNumber"]) ? "" : Convert.ToString(dr["CustomerNumber"]);
                    model.CustomerAddress1 = DBNull.Value.Equals(dr["CustomerAddress1"]) ? "" : Convert.ToString(dr["CustomerAddress1"]);
                    model.CustomerMobile = DBNull.Value.Equals(dr["CustomerMobile"]) ? "" : Convert.ToString(dr["CustomerMobile"]);

                    model.CustomerPhone = DBNull.Value.Equals(dr["CustomerPhone"]) ? "" : Convert.ToString(dr["CustomerPhone"]);
                    model.CustomerEmail = DBNull.Value.Equals(dr["CustomerEmail"]) ? "" : Convert.ToString(dr["CustomerEmail"]);
                    model.CustomerRoleID = DBNull.Value.Equals(dr["CustomerRoleID"]) ? default(Byte) : Convert.ToByte(dr["CustomerRoleID"]);

                    model.CustomerRole = DBNull.Value.Equals(dr["CustomerRole"]) ? "" : Convert.ToString(dr["CustomerRole"]);
                    model.FK_Company = DBNull.Value.Equals(dr["FK_Company"]) ? default(Int64) : Convert.ToInt64(dr["FK_Company"]);
                    model.FK_BranchCodeUser = DBNull.Value.Equals(dr["FK_BranchCodeUser"]) ? default(Int64) : Convert.ToInt64(dr["FK_BranchCodeUser"]);
                    model.FK_Branch = DBNull.Value.Equals(dr["FK_Branch"]) ? default(Int64) : Convert.ToInt64(dr["FK_Branch"]);
                    model.FK_Machine = DBNull.Value.Equals(dr["FK_Machine"]) ? default(Int64) : Convert.ToInt64(dr["FK_Machine"]);

                }
               
            }
           
            output.Data = model;
            return output;
        }

        public static TicketStatusCountOutput ConvertToTicketStatusCountOutput(DataTable dt)
        {
            TicketStatusCountOutput output = new TicketStatusCountOutput();

            List<TicketStatusModel> ticketStatusList = new List<TicketStatusModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TicketStatusModel ticketStatus = new TicketStatusModel();


                    ticketStatus.StatusID = DBNull.Value.Equals(dr["StatusID"]) ? default(Int64) : Convert.ToInt64(dr["StatusID"]);
                    ticketStatus.StatusText = DBNull.Value.Equals(dr["StatusText"]) ? "" : Convert.ToString(dr["StatusText"]);
                    ticketStatus.StatusCount = DBNull.Value.Equals(dr["StatusCount"]) ? default(Int64) : Convert.ToInt64(dr["StatusCount"]);
                    ticketStatusList.Add(ticketStatus);
                }
            }
            output.Data = ticketStatusList;
            return output;
        }
        public static TicketListOutput ConvertToTicketListOutput(DataTable dt)
        {
            TicketListOutput output = new TicketListOutput();

            List<TicketInfoModel> ticketInfoList = new List<TicketInfoModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TicketInfoModel ticketInfo = new TicketInfoModel();

                    ticketInfo.TicketID = DBNull.Value.Equals(dr["TicketID"]) ? default(Int64) : Convert.ToInt64(dr["TicketID"]);
                    ticketInfo.TicketNumber = DBNull.Value.Equals(dr["TicketNumber"]) ? "" : Convert.ToString(dr["TicketNumber"]);
                    ticketInfo.CustomerID = DBNull.Value.Equals(dr["CustomerID"]) ? default(Int64) : Convert.ToInt64(dr["CustomerID"]);

                    ticketInfo.CustomerName = DBNull.Value.Equals(dr["CustomerName"]) ? "" : Convert.ToString(dr["CustomerName"]);
                    ticketInfo.RegistrationOn = DBNull.Value.Equals(dr["RegistrationOn"]) ? "" : Convert.ToString(dr["RegistrationOn"]);
                    ticketInfo.Status = DBNull.Value.Equals(dr["Status"]) ? default(byte) : Convert.ToByte(dr["Status"]);

                    ticketInfo.StatusText = DBNull.Value.Equals(dr["StatusText"]) ? "" : Convert.ToString(dr["StatusText"]);
                    ticketInfo.Priority = DBNull.Value.Equals(dr["Priority"]) ? default(byte) : Convert.ToByte(dr["Priority"]);
                    ticketInfo.PriorityText = DBNull.Value.Equals(dr["PriorityText"]) ? "" : Convert.ToString(dr["PriorityText"]);

                    ticketInfo.ClosedDate = DBNull.Value.Equals(dr["ClosedDate"]) ? "" : Convert.ToString(dr["ClosedDate"]);
                    ticketInfo.TicketDescription = DBNull.Value.Equals(dr["TicketDescription"]) ? "" : Convert.ToString(dr["TicketDescription"]);
                    ticketInfo.RequestType = DBNull.Value.Equals(dr["RequestType"]) ? default(byte) : Convert.ToByte(dr["RequestType"]);

                    ticketInfo.ProductID = DBNull.Value.Equals(dr["ProductID"]) ? default(Int64) : Convert.ToInt64(dr["ProductID"]);
                    ticketInfo.ProductName = DBNull.Value.Equals(dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);

                    ticketInfo.ServiceID = DBNull.Value.Equals(dr["ServiceID"]) ? default(Int64) : Convert.ToInt64(dr["ServiceID"]);
                    ticketInfo.ServiceName = DBNull.Value.Equals(dr["ServiceName"]) ? "" : Convert.ToString(dr["ServiceName"]);

                    ticketInfo.ComplaintID = DBNull.Value.Equals(dr["ComplaintID"]) ? default(Int64) : Convert.ToInt64(dr["ComplaintID"]);
                    ticketInfo.ComplaintName = DBNull.Value.Equals(dr["ComplaintName"]) ? "" : Convert.ToString(dr["ComplaintName"]);
                    ticketInfo.ComplaintPriority = DBNull.Value.Equals(dr["ComplaintPriority"]) ? "" : Convert.ToString(dr["ComplaintPriority"]);

                    ticketInfo.CategoryID = DBNull.Value.Equals(dr["CategoryID"]) ? default(Int64) : Convert.ToInt64(dr["CategoryID"]);
                    ticketInfo.CategoryName = DBNull.Value.Equals(dr["CategoryName"]) ? "" : Convert.ToString(dr["CategoryName"]);


                    ticketInfo.VisitDate = DBNull.Value.Equals(dr["VisitDate"]) ? "" : Convert.ToString(dr["VisitDate"]);
                    ticketInfo.VisitTime = DBNull.Value.Equals(dr["VisitTime"]) ? "" : Convert.ToString(dr["VisitTime"]);
                    ticketInfo.VisitPriority = DBNull.Value.Equals(dr["VisitPriority"]) ? "" : Convert.ToString(dr["VisitPriority"]);
                    ticketInfo.VisitRemark = DBNull.Value.Equals(dr["VisitRemark"]) ? "" : Convert.ToString(dr["VisitRemark"]);

                    ticketInfoList.Add(ticketInfo);

                }
            }
            output.Data = ticketInfoList;
            return output;
        }
        public static TicketInfoOutput ConvertToTicketInfoOutput(DataTable dt)
        {
            TicketInfoOutput output = new TicketInfoOutput();

            TicketDetailedInfoModel model = new TicketDetailedInfoModel();
            PortalFeedbackSubmitDetails feedbackSubmitDetails = new PortalFeedbackSubmitDetails();
            PortalTicketEmployee portalTicketEmployee = new PortalTicketEmployee();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    model.TicketID = DBNull.Value.Equals(dr["TicketID"]) ? default(Int64) : Convert.ToInt64(dr["TicketID"]);
                    model.TicketNumber = DBNull.Value.Equals(dr["TicketNumber"]) ? "" : Convert.ToString(dr["TicketNumber"]);
                    model.CustomerID = DBNull.Value.Equals(dr["CustomerID"]) ? default(Int64) : Convert.ToInt64(dr["CustomerID"]);
                    model.CustomerRoleID = DBNull.Value.Equals(dr["CustomerRoleID"]) ? default(byte) : Convert.ToByte(dr["CustomerRoleID"]);
                    model.CustomerName = DBNull.Value.Equals(dr["CustomerName"]) ? "" : Convert.ToString(dr["CustomerName"]);
                    model.RegistrationOn = DBNull.Value.Equals(dr["RegistrationOn"]) ? "" : Convert.ToString(dr["RegistrationOn"]);
                    model.CustomerContactNumber = DBNull.Value.Equals(dr["CustomerContactNumber"]) ? "" : Convert.ToString(dr["CustomerContactNumber"]);
                    model.CustomerLandmark = DBNull.Value.Equals(dr["CustomerLandmark"]) ? "" : Convert.ToString(dr["CustomerLandmark"]);
                    model.Status = DBNull.Value.Equals(dr["Status"]) ? default(byte) : Convert.ToByte(dr["Status"]);
                    model.StatusText = DBNull.Value.Equals(dr["StatusText"]) ? "" : Convert.ToString(dr["StatusText"]);
                    model.Priority = DBNull.Value.Equals(dr["Priority"]) ? default(byte) : Convert.ToByte(dr["Priority"]);
                    model.PriorityText = DBNull.Value.Equals(dr["PriorityText"]) ? "" : Convert.ToString(dr["PriorityText"]);
                    model.ClosedDate = DBNull.Value.Equals(dr["ClosedDate"]) ? "" : Convert.ToString(dr["ClosedDate"]);
                    model.TicketDescription = DBNull.Value.Equals(dr["TicketDescription"]) ? "" : Convert.ToString(dr["TicketDescription"]);
                    model.ProductID = DBNull.Value.Equals(dr["ProductID"]) ? default(Int64) : Convert.ToInt64(dr["ProductID"]);
                    model.ProductName = DBNull.Value.Equals(dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);
                    model.ServiceID = DBNull.Value.Equals(dr["ServiceID"]) ? default(Int64) : Convert.ToInt64(dr["ServiceID"]);
                    model.ServiceName = DBNull.Value.Equals(dr["ServiceName"]) ? "" : Convert.ToString(dr["ServiceName"]);
                    model.ComplaintID = DBNull.Value.Equals(dr["ComplaintID"]) ? default(Int64) : Convert.ToInt64(dr["ComplaintID"]);
                    model.ComplaintName = DBNull.Value.Equals(dr["ComplaintName"]) ? "" : Convert.ToString(dr["ComplaintName"]);
                    model.ComplaintPriority = DBNull.Value.Equals(dr["ComplaintPriority"]) ? "" : Convert.ToString(dr["ComplaintPriority"]);
                    model.CategoryID = DBNull.Value.Equals(dr["CategoryID"]) ? default(Int64) : Convert.ToInt64(dr["CategoryID"]);
                    model.CategoryName = DBNull.Value.Equals(dr["CategoryName"]) ? "" : Convert.ToString(dr["CategoryName"]);
                    model.VisitDate = DBNull.Value.Equals(dr["VisitDate"]) ? "" : Convert.ToString(dr["VisitDate"]);
                    model.VisitTime = DBNull.Value.Equals(dr["VisitTime"]) ? "" : Convert.ToString(dr["VisitTime"]);
                    model.VisitRemark = DBNull.Value.Equals(dr["VisitRemark"]) ? "" : Convert.ToString(dr["VisitRemark"]);
                    model.VisitPriority = DBNull.Value.Equals(dr["VisitPriority"]) ? "" : Convert.ToString(dr["VisitPriority"]);
                    model.ID_Customerserviceassign = DBNull.Value.Equals(dr["ID_Customerserviceassign"]) ? default(Int64) : Convert.ToInt64(dr["ID_Customerserviceassign"]);
                    model.ComplaintCategory = DBNull.Value.Equals(dr["ComplaintCategory"]) ? default(byte) : Convert.ToByte(dr["ComplaintCategory"]);
                    model.OtherCompanyName = DBNull.Value.Equals(dr["OtherCompanyName"]) ? "" : Convert.ToString(dr["OtherCompanyName"]);
                    model.RequestType = DBNull.Value.Equals(dr["RequestType"]) ? "" : Convert.ToString(dr["RequestType"]);
                    model.ProductType = DBNull.Value.Equals(dr["ProductType"]) ? "" : Convert.ToString(dr["ProductType"]);

                    if (DBNull.Value.Equals(dr["AssignList"]))
                    {
                        model.AssignList = null;
                    }
                    else
                    {
                        model.AssignList = JsonConvert.DeserializeObject<List<PortalTicketEmployee>>(Convert.ToString(dr["AssignList"]));
                    }


                    if (DBNull.Value.Equals(dr["FeedbackSubmitJSON"]))
                    {
                        model.FeedbackSubmit = null;
                    }
                    else
                    {
                        model.FeedbackSubmit = JsonConvert.DeserializeObject<List<PortalFeedbackSubmitDetails>>(Convert.ToString(dr["FeedbackSubmitJSON"]));
                    }
                    model.ShowFeedbackForm = DBNull.Value.Equals(dr["ShowFeedbackForm"]) ? default(bool) : Convert.ToBoolean(dr["ShowFeedbackForm"]);
                    model.OpenFeedbackForm = DBNull.Value.Equals(dr["OpenFeedbackForm"]) ? default(bool) : Convert.ToBoolean(dr["OpenFeedbackForm"]);


                }
            }
            output.Data = model;

            return output;
        }
        public static List<TimeLineChartList> ConvertToTimeLineChartList(DataTable dt)
        {
            List <TimeLineChartList> output = new List<TimeLineChartList>();

            List<TicketStatusModel> ticketStatusList = new List<TicketStatusModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TimeLineChartList model = new TimeLineChartList();

                    model.SLNo = DBNull.Value.Equals(dr["SLNo"]) ? default(Int64) : Convert.ToInt64(dr["SLNo"]);
                    model.Title1 = DBNull.Value.Equals(dr["Title1"]) ? "" : Convert.ToString(dr["Title1"]);
                    model.Title2 = DBNull.Value.Equals(dr["Title2"]) ? "" : Convert.ToString(dr["Title2"]);
                    model.Description = DBNull.Value.Equals(dr["Description"]) ? "" : Convert.ToString(dr["Description"]);
                    model.EntrOn = DBNull.Value.Equals(dr["EntrOn"]) ? "" : Convert.ToString(dr["EntrOn"]);
                    model.MoreData = DBNull.Value.Equals(dr["MoreData"]) ? "" : Convert.ToString(dr["MoreData"]);

                    output.Add(model);
                }
            }

            return output;
        }

        //test commit

        public static ProductEnqueryListOutput ConvertToProductEnqueryListOutput(DataTable dt)
        {
            ProductEnqueryListOutput output = new ProductEnqueryListOutput();

            List<ProductEnqueryInfo> outputList = new List<ProductEnqueryInfo>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProductEnqueryInfo outputOne = new ProductEnqueryInfo();


                    outputOne.ID_Category = DBNull.Value.Equals(dr["ID_Category"]) ? default(Int64) : Convert.ToInt64(dr["ID_Category"]);
                    outputOne.Code = DBNull.Value.Equals(dr["Code"]) ? "" : Convert.ToString(dr["Code"]);
                    outputOne.Name = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                    outputOne.Description = DBNull.Value.Equals(dr["Description"]) ? "" : Convert.ToString(dr["Description"]);
                    outputOne.MRP = DBNull.Value.Equals(dr["MRP"]) ? default(decimal) : Convert.ToDecimal(dr["MRP"]);
                    outputOne.SalPrice = DBNull.Value.Equals(dr["SalPrice"]) ? default(decimal) : Convert.ToDecimal(dr["SalPrice"]);
                    outputOne.ID_Product = DBNull.Value.Equals(dr["ID_Product"]) ? default(Int64) : Convert.ToInt64(dr["ID_Product"]);
                    outputOne.CurrentQuantity = DBNull.Value.Equals(dr["CurrentQuantity"]) ? default(Int64) : Convert.ToInt64(dr["CurrentQuantity"]);
                    outputOne.OFFER = DBNull.Value.Equals(dr["OFFER"]) ? default(bool) : Convert.ToBoolean(dr["OFFER"]);
                    outputOne.OfrName = DBNull.Value.Equals(dr["OfrName"]) ? "" : Convert.ToString(dr["OfrName"]);
                    outputOne.OfrDescription = DBNull.Value.Equals(dr["OfrDescription"]) ? "" : Convert.ToString(dr["OfrDescription"]);
                    outputOne.OfrExpireDate = DBNull.Value.Equals(dr["OfrExpireDate"]) ? "" : Convert.ToString(dr["OfrExpireDate"]);
                    outputOne.HasLeadOfferAmount = DBNull.Value.Equals(dr["HasLeadOfferAmount"]) ? default(bool) : Convert.ToBoolean(dr["HasLeadOfferAmount"]);
                    outputOne.LgpMRP = DBNull.Value.Equals(dr["LgpMRP"]) ? default(decimal) : Convert.ToDecimal(dr["LgpMRP"]);
                    outputOne.LgpSalesPrice = DBNull.Value.Equals(dr["LgpSalesPrice"]) ? default(decimal) : Convert.ToDecimal(dr["LgpSalesPrice"]);


                    outputList.Add(outputOne);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static CustomerPortalSliderOutput ConvertToCustomerPortalSliderOutput(DataTable dt)
        {
            CustomerPortalSliderOutput output = new CustomerPortalSliderOutput();
            
            List<CustomerPortalSlider> outputList = new List<CustomerPortalSlider>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CustomerPortalSlider outputOne = new CustomerPortalSlider();

                   // outputOne.SilderImage = DBNull.Value.Equals(dr["SilderImage"]) ? "" : Convert.ToString(dr["SilderImage"]);
                    outputOne.SilderImage = ConvertBase64ToImageURL(dr);
                    outputOne.SilderImageRedirect = DBNull.Value.Equals(dr["SilderImageRedirect"]) ? "" : Convert.ToString(dr["SilderImageRedirect"]);
                    outputOne.SliderTitle = DBNull.Value.Equals(dr["SliderTitle"]) ? "" : Convert.ToString(dr["SliderTitle"]);
                    outputOne.SliderSubTitle = DBNull.Value.Equals(dr["SliderSubTitle"]) ? "" : Convert.ToString(dr["SliderSubTitle"]);


                    outputList.Add(outputOne);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }

        public static string ConvertBase64ToImageURL(DataRow dr)
        {
            string folder = "CustomerPortalSlider";
            string imageURL = "";
            var baseUri = new Uri(System.Web.HttpContext.Current.Request.Url, "/");

            if (dr != null)
            {
                long companyID = 0;
                long sliderID = 0;
                string fileName = "";// Convert.ToString(dr["SliderTitle"]);

                if (dr["FK_Company"] != DBNull.Value)
                {
                    companyID = Convert.ToInt64(dr["FK_Company"]);
                }


                folder = $"{folder}/company_{companyID}";

                if (dr["ImageFileName"] != DBNull.Value)
                {
                    fileName = Convert.ToString(dr["ImageFileName"]);
                }


                if (dr["SilderImage"] != DBNull.Value)
                {

                    String path = System.Web.HttpContext.Current.Server.MapPath("~/" + folder); //Path

                    //Check if directory exist
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }


                    //    byte[] bytes = Convert.FromBase64String(Convert.ToString(dr["SilderImage"]));

                    string imgbase = Convert.ToString(dr["SilderImage"]);

                    string result = System.Text.RegularExpressions.Regex.Replace(imgbase, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                    byte[] bytes = Convert.FromBase64String(result);

                    //byte[] bytes = (byte[])(dr["SilderImage2"]);
                    if (bytes.Length > 0)
                    {
                        if (String.IsNullOrWhiteSpace(fileName))
                        {
                            if (dr["ID_CusPortalSlider"] != DBNull.Value)
                            {
                                sliderID = Convert.ToInt64(dr["ID_CusPortalSlider"]);
                            }

                            string fileExtension = GetFileExtension(bytes);
                            fileName = $"sliderImage_{sliderID}.{fileExtension}";
                        }

                        string filePath = "~/" + folder + "/" + fileName;

                        using (var stream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                            //imageURL = baseUri + folder + "/" + fileName;
                            imageURL = folder + "/" + fileName;
                        }
                    }
                }
                else
                {
                    imageURL = "";
                }
            }
    


            return imageURL;

        }
        static string GetFileExtension(byte[] byteArray)
        {
            #region code kinda works but not sure
            // Define a dictionary of file signatures (magic numbers) and their corresponding file extensions
            //    Dictionary<string, string> fileSignatures = new Dictionary<string, string>
            //{
            //    { "25504446", "pdf" }, // PDF file signature
            //    { "47494638", "gif" }, // GIF file signature
            //    { "89504E47", "png" }, // PNG file signature
            //    { "FFD8FFDB", "jpg" }, // JPEG file signature
            //    // Add more file signatures and extensions as needed
            //};

            //    // Convert the first few bytes of the byte array to a hexadecimal string
            //    string hexSignature = BitConverter.ToString(byteArray.Take(4).ToArray()).Replace("-", "");

            //    // Search for the matching file extension in the dictionary
            //    if (fileSignatures.TryGetValue(hexSignature, out string extension))
            //    {
            //        return extension;
            //    }



            // If the file signature is not found, you can also use a regular expression
            // or other methods to determine the file type based on its content.

            //return "unknown"; // Default extension if not recognized
            #endregion

            if (byteArray[0] == 0xFF && byteArray[1] == 0xD8 && byteArray[2] == 0xFF)
            {
                return "jpg"; // JPEG format
            }
            else if (byteArray[0] == 0x89 && byteArray[1] == 0x50 && byteArray[2] == 0x4E && byteArray[3] == 0x47)
            {
                return "png"; // PNG format
            }
            else if (byteArray[0] == 0x47 && byteArray[1] == 0x49 && byteArray[2] == 0x46)
            {
                return "gif"; // GIF format
            }
            else if (byteArray[0] == 0x42 && byteArray[1] == 0x4D)
            {
                return "bmp"; // BMP format
            }
            else
            {
                return null; // Unknown format or unsupported format
            }
            
        }
        /*
          public static List<ProductEnquiryImageData> ConvertProductImageData(DataTable dt)
        {
            List<ProductEnquiryImageData> lst = new List<ProductEnquiryImageData>();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProductEnquiryImageData obj = new ProductEnquiryImageData();                   
                    string fileName = Convert.ToString(dr["FileName"]);
                    string filePath = "~/ProductDetails/" + fileName;
                    if (dr["ImageData"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])dr["ImageData"];
                        if (bytes.Length > 0)
                        {
                            using (var stream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Create))
                            {
                                stream.Write(bytes, 0, bytes.Length);
                                stream.Flush();
                                obj.Image = "/ProductDetails/" + fileName;
                            }
                        }
                    }
                    else
                    {
                        obj.Image = "";
                    }
                    lst.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }

            return lst;

        }
         
         */
      


        
        public static CustomerProductInfoOutput ConvertToCustomerProductInfoOutput(DataTable dt)
        {
            CustomerProductInfoOutput output = new CustomerProductInfoOutput();

            List<CustomerProductInfoModel> outputList = new List<CustomerProductInfoModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                 

                    CustomerProductInfoModel model = new CustomerProductInfoModel();
                    model.ProductID = DBNull.Value.Equals(dr["ProductID"]) ? default(Int64) : Convert.ToInt64(dr["ProductID"]);
                    model.ProductName = DBNull.Value.Equals(dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);
                    model.CategoryID = DBNull.Value.Equals(dr["CategoryID"]) ? default(Int64) : Convert.ToInt64(dr["CategoryID"]);
                    model.Category = DBNull.Value.Equals(dr["Category"]) ? "" : Convert.ToString(dr["Category"]);
                    model.SalBillNo = DBNull.Value.Equals(dr["SalBillNo"]) ? "" : Convert.ToString(dr["SalBillNo"]);
                    model.SalBillDate = DBNull.Value.Equals(dr["SalBillDate"]) ? "" : Convert.ToString(dr["SalBillDate"]);
                    model.ID_SalesProductDetails = DBNull.Value.Equals(dr["ID_SalesProductDetails"]) ? default(Int64) : Convert.ToInt64(dr["ID_SalesProductDetails"]);

                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static CompanyListOutput ConvertToCompanyListOutput(DataTable dt)
        {
            CompanyListOutput output = new CompanyListOutput();

            List<CompanyListModel> outputList = new List<CompanyListModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    CompanyListModel model = new CompanyListModel();
                    model.CompanyID = DBNull.Value.Equals(dr["ModeID"]) ? default(Int64) : Convert.ToInt64(dr["ModeID"]);
                    model.CompanyName = DBNull.Value.Equals(dr["ModeValue"]) ? "" : Convert.ToString(dr["ModeValue"]);
                  

                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static CategoryListOutput ConvertToCategoryListOutput(DataTable dt)
        {
            CategoryListOutput output = new CategoryListOutput();

            List<CategoryListModel> outputList = new List<CategoryListModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    CategoryListModel model = new CategoryListModel();
                    model.CategoryID = DBNull.Value.Equals(dr["ModeID"]) ? default(Int64) : Convert.ToInt64(dr["ModeID"]);
                    model.CategoryName = DBNull.Value.Equals(dr["ModeValue"]) ? "" : Convert.ToString(dr["ModeValue"]);


                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static ProductListOutput ConvertToProductListOutput(DataTable dt)
        {
            ProductListOutput output = new ProductListOutput();

            List<ProductListModel> outputList = new List<ProductListModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    ProductListModel model = new ProductListModel();
                    model.ProductID = DBNull.Value.Equals(dr["ModeID"]) ? default(Int64) : Convert.ToInt64(dr["ModeID"]);
                    model.ProductName = DBNull.Value.Equals(dr["ModeValue"]) ? "" : Convert.ToString(dr["ModeValue"]);


                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static GetRequestTypeOutput ConvertToGetRequestTypeOutput(DataTable dt)
        {
            GetRequestTypeOutput output = new GetRequestTypeOutput();

            List<RequestTypeModel> outputList = new List<RequestTypeModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    RequestTypeModel model = new RequestTypeModel();
                    model.RequestTypeID = DBNull.Value.Equals(dr["ModeID"]) ? default(Int64) : Convert.ToInt64(dr["ModeID"]);
                    model.RequestTypeName = DBNull.Value.Equals(dr["ModeValue"]) ? "" : Convert.ToString(dr["ModeValue"]);


                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static GetServiceListOutput ConvertToGetServiceListOutput(DataTable dt)
        {
            GetServiceListOutput output = new GetServiceListOutput();

            List<ServiceListModel> outputList = new List<ServiceListModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    ServiceListModel model = new ServiceListModel();
                    model.ServiceListID = DBNull.Value.Equals(dr["ModeID"]) ? default(Int64) : Convert.ToInt64(dr["ModeID"]);
                    model.ServiceListName = DBNull.Value.Equals(dr["ModeValue"]) ? "" : Convert.ToString(dr["ModeValue"]);


                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static GetComplaintListOutput ConvertToGetComplaintListOutput(DataTable dt)
        {
            GetComplaintListOutput output = new GetComplaintListOutput();

            List<ComplaintListModel> outputList = new List<ComplaintListModel>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    ComplaintListModel model = new ComplaintListModel();
                    model.ComplaintListID = DBNull.Value.Equals(dr["ModeID"]) ? default(Int64) : Convert.ToInt64(dr["ModeID"]);
                    model.ComplaintListName = DBNull.Value.Equals(dr["ModeValue"]) ? "" : Convert.ToString(dr["ModeValue"]);


                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
        public static GetProductDetailsOutput ConvertToGetProductDetailsOutput(DataTable dt)
        {
            GetProductDetailsOutput output = new GetProductDetailsOutput();

            List<ProductDetailsModel> outputList = new List<ProductDetailsModel>();
            

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {


                    ProductDetailsModel model = new ProductDetailsModel();
                    model.ProductID = DBNull.Value.Equals(dr["ProductID"]) ? default(Int64) : Convert.ToInt64(dr["ProductID"]);
                    model.ProdctName = DBNull.Value.Equals(dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);
                    model.CategoryID = DBNull.Value.Equals(dr["CategoryID"]) ? default(Int64) : Convert.ToInt64(dr["CategoryID"]);
                    model.AMC = DBNull.Value.Equals(dr["AMCDueDate"]) ? "" : Convert.ToString(dr["AMCDueDate"]);
                    model.Address = DBNull.Value.Equals(dr["BrAddress2"]) ? "" : Convert.ToString(dr["BrAddress2"]);
                    model.Name = DBNull.Value.Equals(dr["BrName"]) ? "" : Convert.ToString(dr["BrName"]);
                    model.Phone = DBNull.Value.Equals(dr["BrPhone"]) ? "" : Convert.ToString(dr["BrPhone"]);
                    model.Mobile = DBNull.Value.Equals(dr["BrMobile"]) ? "" : Convert.ToString(dr["BrMobile"]);
                    model.Email = DBNull.Value.Equals(dr["BrEmail"]) ? "" : Convert.ToString(dr["BrEmail"]);
                    model.GSTIN = DBNull.Value.Equals(dr["BrGSTINNo"]) ? "" : Convert.ToString(dr["BrGSTINNo"]);
                    model.Description = DBNull.Value.Equals(dr["ManufDescription"]) ? "" : Convert.ToString(dr["ManufDescription"]);
                    model.InvoiceNo = DBNull.Value.Equals(dr["SalBillNo"]) ? "" : Convert.ToString(dr["SalBillNo"]);
                    //model.InvoiceDate = DBNull.Value.Equals(dr["SalBillDate"]) ? "" : Convert.ToString(dr["SalBillDate"]);
                    model.InvoiceDate = DBNull.Value.Equals(dr["SalBillDate"])  ? ""  : Convert.ToDateTime(dr["SalBillDate"]).ToString("MM-dd-yyyy");
                    model.Quantity = DBNull.Value.Equals(dr["SpdSalActualQuantity"]) ? "" : Convert.ToString(dr["SpdSalActualQuantity"]);
                    model.InvoiceAmount = DBNull.Value.Equals(dr["SalBillTotal"]) ? default(Int64) : Convert.ToInt64(dr["SalBillTotal"]);
                    model.ReplacementWarrantyExpireDate = DBNull.Value.Equals(dr["WDReplacementWarrantyExpireDate"]) ? "" : Convert.ToString(dr["WDReplacementWarrantyExpireDate"]);
                    model.ServiceWarrantyExpireDate = DBNull.Value.Equals(dr["WDServiceWarrantyExpireDate"]) ? "" : Convert.ToString(dr["WDServiceWarrantyExpireDate"]);

                    model.ManufName = DBNull.Value.Equals(dr["ManufName"]) ? "" : Convert.ToString(dr["ManufName"]);
                    model.ManufPContact = DBNull.Value.Equals(dr["ManufPContact"]) ? "" : Convert.ToString(dr["ManufPContact"]);
                    model.ManufMobile = DBNull.Value.Equals(dr["ManufMobile"]) ? "" : Convert.ToString(dr["ManufMobile"]);
                    model.ManufPhone = DBNull.Value.Equals(dr["ManufPhone"]) ? "" : Convert.ToString(dr["ManufPhone"]);
                    model.ManufAddress = DBNull.Value.Equals(dr["ManufAddress"]) ? "" : Convert.ToString(dr["ManufAddress"]);
                    model.ManufEmail = DBNull.Value.Equals(dr["ManufEmail"]) ? "" : Convert.ToString(dr["ManufEmail"]);
                    model.ManufGSTIN = DBNull.Value.Equals(dr["ManufGSTIN"]) ? "" : Convert.ToString(dr["ManufGSTIN"]);
                    model.ManufDescription = DBNull.Value.Equals(dr["ManufDescription"]) ? "" : Convert.ToString(dr["ManufDescription"]);
                    outputList.Add(model);
                }
                output.StatusCode = 0;
                output.EXMessage = "Success";
            }
            else
            {
                output.StatusCode = 0;
                output.EXMessage = "No data";
            }
            output.Data = outputList;
            return output;
        }
    }
}
