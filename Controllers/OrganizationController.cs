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

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class OrganizationController : Controller
    {
        // GET: Organization
       // public static string _transMode = "org";

        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadFormOrganization()
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

            OrganizationModel.OrganizationView sortno = new OrganizationModel.OrganizationView();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "Organization",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            sortno.SortOrder = a.Data[0].NextNo;

            return PartialView("_AddOrganizationForm", sortno);
        }

       

        [HttpPost]
        public ActionResult GetOrganizationList(int pageSize, int pageIndex, string Name)
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

          
            ModelState.Remove("ReasonID");

            if (!ModelState.IsValid)
            {
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

            OrganizationModel Organization = new OrganizationModel();

            var data = Organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID {
                ID_Organization = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
               
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
            });



            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        //Add New Organization//
        public ActionResult AddNewOrganizationDetails(OrganizationModel.OrganizationView newOrganization)
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

            ModelState.Remove("OrganizationID");
            ModelState.Remove("Organization");
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

            //DistrictModel states = new DistrictModel();
            OrganizationModel organization = new OrganizationModel();

            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
           // string transmode = _transMode;
            int backupId = 0;
            //  DistrictModel.DistrictView district = new DistrictModel.DistrictView
            var dataresponse = organization.UpdateOrganizationData(input: new OrganizationModel.UpdateOrganization
            {
                UserAction = userAction,
               
                ID_Organization = 0,
                OrgCode = newOrganization.Code,
                OrgName = newOrganization.Name,
                OrgShortName = newOrganization.ShortName,
                OrgContactPerson = newOrganization.ContactPerson,
                OrgContactPMobile = newOrganization.ContactPMobile,
                OrgContactPEmail = newOrganization.ContactPEmail,
                OrgRegisterNo = newOrganization.RegisterNo,
                OrgAddress1 = newOrganization.Address1,
                OrgAddress2 = newOrganization.Address2,
                OrgAddress3 = newOrganization.Address3,
                OrgEmail = newOrganization.Email,
                OrgWebSite = newOrganization.WebSite,
                OrgMobile = newOrganization.Mobile,
                OrgPhone = newOrganization.Phone,
                OrgFax = newOrganization.Fax,
                OrgStartDate = newOrganization.StartDate,
                OrgSocialMediaWebsite1 = newOrganization.SocialMediaWebsite1,
                OrgSocialMediaWebsite2 = newOrganization.SocialMediaWebsite2,
                FK_Country = newOrganization.CountryID,
                FK_District = newOrganization.DistrictID,
                FK_Area = (newOrganization.AreaID.HasValue) ? newOrganization.AreaID.Value : 0,
                FK_Place = (newOrganization.PlaceID.HasValue) ? newOrganization.PlaceID.Value : 0,
                FK_States = newOrganization.StatesID,
               // FK_Post = newOrganization.PostID,
                FK_Post = (newOrganization.PostID.HasValue) ? newOrganization.PostID.Value : 0,
                SortOrder = newOrganization.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode=newOrganization.TransMode,
                FK_Machine = _userLoginInfo.FK_Machine

            }, companyKey: _userLoginInfo.CompanyKey);

            //_transMode
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetOrganizationInfoByID(OrganizationModel.OrganizationInfoView data)
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
            ModelState.Remove("Code");
            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove(" ContactPerson");
            ModelState.Remove("ContactPMobile");
            ModelState.Remove(" ContactPEmail");
            ModelState.Remove("RegisterNo");
            ModelState.Remove(" Address1");
            ModelState.Remove(" Address2");
            ModelState.Remove("Address3");
            ModelState.Remove("Email");
            ModelState.Remove(" WebSite");
            ModelState.Remove("Mobile");
            ModelState.Remove("Phone");
            ModelState.Remove("Fax");
            ModelState.Remove("StartDate");
            ModelState.Remove("SocialMediaWebsite1");
            ModelState.Remove("SocialMediaWebsite2");
            ModelState.Remove("OrganizationID");
            ModelState.Remove("Organization");
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

            OrganizationModel organization = new OrganizationModel();
            var organizationInfo = organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID { ID_Organization = data.ID_Organization, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy });

            return Json(organizationInfo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPinCodedetails(string Pincode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
           
            var data = Common.GetDataViaQuery<OrganizationModel.searchPinModel>(parameters: new APIParameters
            {
                TableName = "Post AS P JOIN Area AS A ON A.ID_Area=P.FK_Area JOIN District As D ON D.ID_District=A.FK_District   JOIN States AS ST ON ST.ID_States=D.FK_States JOIN Country AS C ON C.ID_Country=ST.FK_Country",
                SelectFields = " P.PostName AS Post ,P.ID_Post AS PostID,A.AreaName AS Area,A.ID_Area AS AreaID,D.DtName AS District,D.ID_District AS DistrictID,ST.StName AS States,ST.ID_States AS StatesID,C.CntryName AS Country,C.ID_Country AS CountryID ",
                Criteria = "P.Cancelled =0 AND P.Passed=1 AND P.PinCode='" + Pincode + "'" + " AND P.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        /*LEFT JOIN Area AS A  ON D.ID_District =A.FK_District; A.AreaName AS Area,A.ID_Area AS AreaID,*/

        public ActionResult GetCountryList()
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

            var data = Common.GetDataViaQuery<OrganizationModel.Countrylist>(parameters: new APIParameters
            {
                TableName = "Country",
                SelectFields = "ID_Country AS CountryID,CntryName AS Country",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }



        public ActionResult GetStateList(Int32 countryid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<OrganizationModel.Statelist>(parameters: new APIParameters
            {

                TableName = "States",
                SelectFields = " StName AS States,ID_States AS StatesID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Country='" + countryid + "'" +" AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetDistrictList(Int32 statesid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<OrganizationModel.Districtlist>(parameters: new APIParameters
            {

                TableName = "District",
                SelectFields = " DtName AS District,ID_District AS DistrictID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_States='" + statesid + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GetAreaList(Int32 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<OrganizationModel.Arealist>(parameters: new APIParameters
            {

                TableName = "Area",
                SelectFields = " AreaName AS Area,ID_Area AS AreaID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPlaceList(Int32 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<OrganizationModel.Placelist>(parameters: new APIParameters
            {

                TableName = "Place",
                SelectFields = " PlcName AS Place,ID_Place AS PlaceID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPostList(OrganizationModel.Postlistrelated datas)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<OrganizationModel.PostList>(parameters: new APIParameters
            {

                TableName = "Post",
                SelectFields = "PostName AS Post,ID_Post AS PostID,PinCode AS PinCode",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District=" + datas.DistrictID +"AND FK_Place=" + datas.PlaceID + "" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateOrganizationDetails(OrganizationModel.OrganizationView data)
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



            //ModelState.Remove("OrganizationID");
            //ModelState.Remove("Code");
            //ModelState.Remove("Name");
            //ModelState.Remove("ShortName");
            //ModelState.Remove(" ContactPerson");
            //ModelState.Remove("ContactPMobile");
            //ModelState.Remove(" ContactPEmail");
            //ModelState.Remove("RegisterNo");
            //ModelState.Remove(" Address1");
            ModelState.Remove(" Address2");
            ModelState.Remove("Address3");
            //ModelState.Remove("Email");
            //ModelState.Remove(" WebSite");
            //ModelState.Remove("Mobile");
            //ModelState.Remove("Phone");
            //ModelState.Remove("Fax");
            //ModelState.Remove("StartDate");
            //ModelState.Remove("SocialMediaWebsite1");
            //ModelState.Remove("SocialMediaWebsite2");
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
            //ModelState.Remove("SortOrder");
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

            OrganizationModel organization = new OrganizationModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = organization.UpdateOrganizationData(input: new OrganizationModel.UpdateOrganization
            {
                UserAction = userAction,
               
                ID_Organization = data.OrganizationID,
                OrgCode = data.Code,
                OrgName = data.Name,
                OrgShortName = data.ShortName,
                OrgContactPerson = data.ContactPerson,
                OrgContactPMobile = data.ContactPMobile,
                OrgContactPEmail = data.ContactPEmail,
                OrgRegisterNo = data.RegisterNo,
                OrgAddress1 = data.Address1,
                OrgAddress2 = data.Address2,
                OrgAddress3 = data.Address3,
                OrgEmail = data.Email,
                OrgWebSite = data.WebSite,
                OrgMobile = data.Mobile,
                OrgPhone = data.Phone,
                OrgFax = data.Fax,
                OrgStartDate = data.StartDate,
                OrgSocialMediaWebsite1 = data.SocialMediaWebsite1,
                OrgSocialMediaWebsite2 = data.SocialMediaWebsite2,
                FK_Country = data.CountryID,
                FK_District = data.DistrictID,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_States = data.StatesID,
               // FK_Post = data.PostID,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                TransMode=data.TransMode,
                SortOrder = data.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine



            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete organization
        public ActionResult DeleteOrganizationInfo(OrganizationModel.OrganizationInfoView data)
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

            OrganizationModel.DeleteOrganization org = new OrganizationModel.DeleteOrganization
            {
                ID_Organization = data.ID_Organization,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID
            };

            Output dataresponse = Common.UpdateTableData<OrganizationModel.DeleteOrganization>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proOrganizationDelete", parameter: org);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOrganizationdDeleteReasonList()
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