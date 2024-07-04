using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;

using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using PerfectWebERP.Filters;
using System.Configuration; // Namespace for ConfigurationManager

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult CompanyIndex(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 


            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar; 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
          
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            string clientType = ConfigurationManager.AppSettings["ClientType"] ?? "0";
            ViewBag.ClientType = clientType;
            return View();
        }

        public ActionResult LoadFormCompany(string mtd)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CompanyModel.CompanyViewList sortno = new CompanyModel.CompanyViewList();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "Company",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            sortno.SortOrder = a.Data[0].NextNo;

            CompanyModel objcmp = new CompanyModel();
            var imgmodelst = objcmp.GetImgModelist(input: new CompanyModel.ModeLead { Mode = 12 }, companyKey: _userLoginInfo.CompanyKey);
            sortno.ImgModeList = imgmodelst.Data;
            var companyCategory= objcmp.GetImgModelist(input: new CompanyModel.ModeLead { Mode = 54 }, companyKey: _userLoginInfo.CompanyKey);
            sortno.Category = companyCategory.Data;
            ViewBag.mtd = mtd;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            string clientType = ConfigurationManager.AppSettings["ClientType"] ?? "0";
            ViewBag.ClientType = clientType;
            return PartialView("_AddCompany", sortno);
        }

        [HttpPost]
        public ActionResult GetCompanyList(int pageSize, int pageIndex, string Name)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            string transMode = "";
            ViewBag.PageTitle = TempData["mTd"] as string;


            CompanyModel company = new CompanyModel();

          var data = company.GetCompanyData(companyKey: _userLoginInfo.CompanyKey, input: new CompanyModel.CompanyID { ID_Company = 0, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy=_userLoginInfo.EntrBy, PageIndex = pageIndex + 1,
               PageSize = pageSize,
              Name = Name,
              TransMode = transMode
            });

          //  return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

            
        [HttpPost]
        [ValidateAntiForgeryToken()]
        //Add New Company//
        public ActionResult AddNewCompanyDetails(CompanyModel.CompanyView newCompany)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            #region :: Model validation  ::

            ModelState.Remove("CompanyID");
            ModelState.Remove("Organization");
            ModelState.Remove("OrganizationID");
            ModelState.Remove("Country");
            ModelState.Remove("CountryID");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("District");
            ModelState.Remove("DistrictID");
           // ModelState.Remove("AreaID");
           // ModelState.Remove("Area");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
            ModelState.Remove("PostID");
            ModelState.Remove("Post");

            //removing a node in model while validating
            //-- - Model validation
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::


            CompanyModel company = new CompanyModel();

            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            //string userCode = _userLoginInfo.UserCode;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            //List<CompanyModel.CompanyImage2> imagelist = new List<CompanyModel.CompanyImage2>();
            //if (newCompany.CompanyImageList != null)
            //{
            //    foreach (var itm in newCompany.CompanyImageList)
            //    {
            //        itm.ComImgValue = itm.ComImgValue.Split(';')[1].Replace("base64,", "");
            //        imagelist.Add(new CompanyModel.CompanyImage2 { ComImg = Convert.FromBase64String(itm.ComImgValue), ComImgMode =itm.ComImgMode, FK_Company =0, ID_CompanyImage =0});


            //       // itm.ComImg = Convert.FromBase64String(itm.ComImgValue);

            //    }
            //}
            string image = (string)Session["CompanyImage"];
            //if (newCompany.CompanyImageList != null)
            //{
            //    foreach (var itm in newCompany.CompanyImageList)
            //    {
                   
            //            itm.ComImg = itm.ComImg.Split(';')[1].Replace("base64,", "");                    
            //            itm.ComImgValue = Convert.FromBase64String(itm.ComImg);
            //            itm.ComImg = "";                    
            //    }
            //}
            //  DistrictModel.DistrictView district = new DistrictModel.DistrictView
            var dataresponse = company.UpdateCompanyData(input: new CompanyModel.UpdateCompany
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,              
                FK_Company =_userLoginInfo.FK_Company,
                FK_Reason = 0,
                EntrBy = entrBy,                
                TransMode =newCompany.TransMode,
                Debug = 0,
                ID_Company = 0,
                CompCode = newCompany.Code,
                CompName = newCompany.Name,
                CompShortName = newCompany.ShortName,
                CompContactPerson = newCompany.ContactPerson,
                CompContactPMobile = newCompany.ContactPMobile,
                CompContactPEmail = newCompany.ContactPEmail,
                CompRegisterNo = newCompany.RegisterNo,
                CompAddress1 = newCompany.Address1,
                CompAddress2 = newCompany.Address2,
                CompAddress3 = newCompany.Address3,
                CompEmail = newCompany.Email,
                CompWebSite = newCompany.WebSite,
                CompMobile = newCompany.Mobile,
                CompPhone = newCompany.Phone,
                CompFax = newCompany.Fax,
                CompStartDate = newCompany.StartDate,
                CompSoftinstalled= newCompany.Softinstalled,
                CompSocialMediaWebsite1 = newCompany.SocialMediaWebsite1,
                CompSocialMediaWebsite2 = newCompany.SocialMediaWebsite2,
                FK_Organization= newCompany.OrganizationID,
                FK_Country = newCompany.CountryID,
                FK_District = newCompany.DistrictID,
                FK_Area = (newCompany.AreaID.HasValue) ? newCompany.AreaID.Value : 0,
                FK_Place = (newCompany.PlaceID.HasValue) ? newCompany.PlaceID.Value : 0,
                FK_State = newCompany.StatesID,
                //FK_Post = newCompany.PostID,
                FK_Post = (newCompany.PostID.HasValue) ? newCompany.PostID.Value : 0,
                SortOrder = newCompany.SortOrder,
                //CompanyImageList = newCompany.CompanyImageList is null ? "" : Common.xmlTostring(newCompany.CompanyImageList),
                CompanyImageList = image,
                SMSShortname = newCompany.SMSShortname,
                CompCategory = newCompany.CompCategory
                //  CompanyImageList = newCompany.CompanyImageList is null ? "" : Common.xmlTostring(newCompany.CompanyImageList),
            }, companyKey: _userLoginInfo.CompanyKey);
            Session["CompanyImage"] = "";

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        

     

        public ActionResult GetCompanyInfoByID(CompanyModel.CompanyView data)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("Organization");
            ModelState.Remove("OrganizationID");
            ModelState.Remove("Country");
            ModelState.Remove("CountryID");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("District");
            ModelState.Remove("DistrictID");
            ModelState.Remove("AreaID");
            ModelState.Remove("Area");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
            ModelState.Remove("PostID");
            ModelState.Remove("Post");
            ModelState.Remove("Code");
            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("ContactPerson");
            ModelState.Remove("ContactPMobile");
            ModelState.Remove("ContactPEmail");
            ModelState.Remove("RegisterNo");
            ModelState.Remove("Address1");
            ModelState.Remove("Mobile");
            ModelState.Remove("StartDate");
            ModelState.Remove("Softinstalled");
            ModelState.Remove("Email");
        
           



            #region :: Model validation  ::

            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            CompanyModel company = new CompanyModel();

           // var prInfo = product.GetProductData(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProduct { ID_Product = productInfo.ProductID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.UserCode });
            var companyInfo = company.GetCompanyData(companyKey: _userLoginInfo.CompanyKey, input: new CompanyModel.CompanyID { ID_Company = data.CompanyID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy});
            // var companyInfo = company.GetCompanyData(companyKey: _userLoginInfo.CompanyKey, input: new CompanyModel.CompanyID { ID_Company = data.ID_Company });
            var subimg = company.GetSubImage(companyKey: _userLoginInfo.CompanyKey, input: new CompanyModel.CompanyImgId { ID_Company = data.CompanyID, EntrBy = _userLoginInfo.EntrBy });

            return Json(new { subimg, companyInfo }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOrganizationList()
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];



            var data = Common.GetDataViaQuery<CompanyModel.Organizationlist>(parameters: new APIParameters
            {
                TableName = "Organization",
                SelectFields = "ID_Organization AS OrganizationID,OrgName AS Organization",
                Criteria = "cancelled=0 AND Passed =1 and FK_Company=" + _userLoginInfo.FK_Company,             
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCompanyDetails(CompanyModel.CompanyView data)
        {

            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            string image = (string)Session["CompanyImage"];
            //Session["CompanyImage"]
            #region :: Model validation  ::



            ModelState.Remove("CompanyID");
            ModelState.Remove("OrganizationID");
            ModelState.Remove("Organization");
            ModelState.Remove("CountryID");
            ModelState.Remove("Country");
            ModelState.Remove("StatesID");
            ModelState.Remove("States");
            ModelState.Remove("DistrictID");
            ModelState.Remove("District");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
            ModelState.Remove("PostID");
            ModelState.Remove("Post");
            ModelState.Remove("AreaID");
            ModelState.Remove("Area");
            ModelState.Remove("PinCode");
            ModelState.Remove("SortOrder");
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            CompanyModel company = new CompanyModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
           // string userCode = _userLoginInfo.UserCode;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;
          
            //if (data.CompanyImageList != null)
            //{
            //    foreach (var itm in data.CompanyImageList)
            //    {
            //        itm.ComImg = itm.ComImg.Split(';')[1].Replace("base64,", "");
            //        itm.ComImgValue = Convert.FromBase64String(itm.ComImg);
            //        itm.ComImg = "";

            //    }
            //}
            var dataresponse = company.UpdateCompanyData(input: new CompanyModel.UpdateCompany
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,                
                FK_BranchCodeUser = branchUserCode,             
                FK_Company = OrgnCode,
                TransMode = data.TransMode,
                Debug =0,
                FK_Reason = 0,
                EntrBy = entrBy,
                ID_Company = data.CompanyID,
                CompCode = data.Code,
                CompName = data.Name,
                CompShortName = data.ShortName,
                CompContactPerson = data.ContactPerson,
                CompContactPMobile = data.ContactPMobile,
                CompContactPEmail = data.ContactPEmail,
                CompRegisterNo = data.RegisterNo,
                CompAddress1 = data.Address1,
                CompAddress2 = data.Address2,
                CompAddress3 = data.Address3,
                CompEmail = data.Email,
                CompWebSite = data.WebSite,
                CompMobile = data.Mobile,
                CompPhone = data.Phone,
                CompFax = data.Fax,
                CompStartDate = data.StartDate,
                CompSoftinstalled = data.Softinstalled,
                CompSocialMediaWebsite1 = data.SocialMediaWebsite1,
                CompSocialMediaWebsite2 = data.SocialMediaWebsite2,
                FK_Organization = data.OrganizationID,
                FK_Country = data.CountryID,
                FK_District = data.DistrictID,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value :0 ,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_State = data.StatesID,
                //FK_Post = data.PostID,
                SortOrder = data.SortOrder,
                 CompanyImageList = image,
                SMSShortname = data.SMSShortname,
                CompCategory = data.CompCategory
            }, companyKey: _userLoginInfo.CompanyKey);

            Session["CompanyImage"] = "";
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete Company
        public ActionResult DeleteCompanyInfo(CompanyModel.CompanyInfoView data)
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("Organization");
            ModelState.Remove("OrganizationID");
            ModelState.Remove("Country");
            ModelState.Remove("CountryID");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("District");
            ModelState.Remove("DistrictID");
            ModelState.Remove("AreaID");
            ModelState.Remove("Area");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
            ModelState.Remove("PostID");
            ModelState.Remove("Post");
            ModelState.Remove("Code");
            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("ContactPerson");
            ModelState.Remove("ContactPMobile");
            ModelState.Remove("ContactPEmail");
            ModelState.Remove("RegisterNo");
            ModelState.Remove("Address1");
            ModelState.Remove("Mobile");
            ModelState.Remove("StartDate");
            ModelState.Remove("Softinstalled");
            ModelState.Remove("Email");
            #region :: Model validation  ::

            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion :: Model validation  ::

            CompanyModel.DeleteCompany deleteCompany = new CompanyModel.DeleteCompany
            {
                ID_Company = data.ID_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                Debug = 0,
                FK_Company =_userLoginInfo.FK_Company,
                FK_BranchCodeUser =_userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID
            };

            Output dataresponse = Common.UpdateTableData<CompanyModel.DeleteCompany>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCompanyDelete", parameter: deleteCompany);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCompanyDeleteReasonList()
        {
            #region ::  Check User Session to verifyLogin  ::

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

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ReasonModel reason = new ReasonModel();

             var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }


    }
}