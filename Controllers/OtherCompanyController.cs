/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 05/05/2022
Purpose		: OtherCompany
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

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
    public class OtherCompanyController : Controller
    {
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadFormOtherCompany()
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

            OtherCompanyModel.OtherCompanyView sortno = new OtherCompanyModel.OtherCompanyView();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "OtherCompany",
                FieldName = "SortOrder",
                Debug = 0
            });

            sortno.SortOrder = a.Data[0].NextNo;

            return PartialView("_AddOtherCompany", sortno);
        }

        [HttpPost]
        public ActionResult GetOtherCompanyList(int pageSize, int pageIndex, string Name)
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


            OtherCompanyModel otherCompany = new OtherCompanyModel();
           // CompanyModel company = new CompanyModel();

            var data = otherCompany.GetOtherCompanyData(companyKey: _userLoginInfo.CompanyKey, input: new OtherCompanyModel.OtherCompanyID
            {
                FK_OtherCompany = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

           
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetOtherCompanyInfoByID(OtherCompanyModel.OtherCompanyView data)
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
           
            ModelState.Remove("RegisterNo");
            ModelState.Remove("Address");
            ModelState.Remove("Mobile");
           
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

            OtherCompanyModel company = new OtherCompanyModel();

          
            var companyInfo = company.GetOtherCompanyData(companyKey: _userLoginInfo.CompanyKey, input: new OtherCompanyModel.OtherCompanyID { FK_OtherCompany = data.OtherCompanyID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            


            return Json(companyInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewOtherCompany(OtherCompanyModel.OtherCompanyView data)
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

            ModelState.Remove("OtherCompanyID");
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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
            ModelState.Remove("OtherCompanyID");
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

            ModelState.Remove("RegisterNo");
            ModelState.Remove("Address");
            ModelState.Remove("Mobile");

            ModelState.Remove("Email");
            OtherCompanyModel OtherCompany = new OtherCompanyModel();

            var datresponse = OtherCompany.UpdateOtherCompanyData(input: new OtherCompanyModel.UpdateOtherCompany
            {
                UserAction = 1,
              
                OCRegisterNo = data.RegisterNo,
                OCName = data.Name,
                OCShortName = data.ShortName,
                OCAddress1 = data.Address,
                FK_Country = data.CountryID,
                FK_District = (data.DistrictID.HasValue) ? data.DistrictID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_State = (data.StatesID.HasValue) ? data.StatesID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                OCMobileNo = data.Mobile,
                OCEmail = data.Email,
                OCWebsite = data.Website,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode=data.TransMode,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_OtherCompany = 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateOtherCompany(OtherCompanyModel.OtherCompanyView data)
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

            ModelState.Remove("RegisterNo");
            ModelState.Remove("Address");
            ModelState.Remove("Mobile");

            ModelState.Remove("Email");
            OtherCompanyModel OtherCompany = new OtherCompanyModel();
            var datresponse = OtherCompany.UpdateOtherCompanyData(input: new OtherCompanyModel.UpdateOtherCompany
            {
                UserAction = 2
    ,
                ID_OtherCompany = data.OtherCompanyID,
                OCRegisterNo = data.RegisterNo,
                OCName = data.Name,
                OCShortName = data.ShortName,
                OCAddress1 = data.Address,
                FK_Country = data.CountryID,
                FK_District = (data.DistrictID.HasValue) ? data.DistrictID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_State = (data.StatesID.HasValue) ? data.StatesID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                OCMobileNo = data.Mobile,
                OCEmail = data.Email,
                OCWebsite = data.Website,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete Company
        public ActionResult DeleteOtherCompanyInfo(OtherCompanyModel.OtherCompanyInfoView data)
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
           
            ModelState.Remove("RegisterNo");
            ModelState.Remove("Address");
            ModelState.Remove("Mobile");
           
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

            OtherCompanyModel.DeleteOtherCompany deleteCompany = new OtherCompanyModel.DeleteOtherCompany
            {
                FK_OtherCompany = data.ID_OtherCompany,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID
            };

            Output dataresponse = Common.UpdateTableData<OtherCompanyModel.DeleteOtherCompany>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proOtherCompanyDelete", parameter: deleteCompany);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOtherCompanyDeleteReasonList()
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


