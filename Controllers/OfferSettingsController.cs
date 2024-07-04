using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class OfferSettingsController : Controller
    {
        // GET: OfferSettings
        #region[Index]
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
        #endregion

        #region[LoadOfferSettings]

        public ActionResult LoadOfferSettings()
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

            OfferSettingsModel offerSettingsModel = new OfferSettingsModel();
            OfferSettingsModel.OfferView offerView = new OfferSettingsModel.OfferView();

         

            var Category = Common.GetDataViaQuery<OfferSettingsModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS FK_Category ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey

             );
            offerView.CategoryList = Category.Data;

            return PartialView("_AddOfferSettings", offerView);


        }
        #endregion

        #region[AddOfferSetting]

        public ActionResult AddOfferSetting(OfferSettingsModel.OfferView offerView)
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

            OfferSettingsModel offer = new OfferSettingsModel();

            var data = offer.UpdateOfferdetails(input: new OfferSettingsModel.OfferSettingUpdate
            { 
                UserAction =1,
                TransMode = "",
                Debug = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                ID_Offers = 0,
                OfrName = offerView.OfrName,
                OfrEffectDate = offerView.OfrEffectDate,
                OfrExpireDate = offerView.OfrExpireDate,
                OfrActive = offerView.OfrActive,
                OfrDescription = offerView.OfrDescription,
                OfferDetails = offerView.OfferDetails is null ? "" : Common.xmlTostring(offerView.OfferDetails),
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Offers = 0,
                FK_Type=offerView.FK_Type,
                LastID = (offerView.LastID.HasValue) ? offerView.LastID.Value : 0,


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[UpdateOfferSetting]

        public ActionResult UpdateOfferSetting(OfferSettingsModel.OfferView offer)
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


            OfferSettingsModel offers = new OfferSettingsModel();
            var data = offers.UpdateOfferdetails(input: new OfferSettingsModel.OfferSettingUpdate
            {
                UserAction = 2,
                TransMode = "",
                Debug = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                ID_Offers = offer.FK_Offers,
                OfrName = offer.OfrName,
                OfrEffectDate = offer.OfrEffectDate,
                OfrExpireDate = offer.OfrExpireDate,
                OfrActive = offer.OfrActive,
                OfrDescription = offer.OfrDescription,
                OfferDetails = offer.OfferDetails is null ? "" : Common.xmlTostring(offer.OfferDetails),
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Offers = offer.FK_Offers,
                FK_Type = offer.FK_Type,
                LastID = (offer.LastID.HasValue) ? offer.LastID.Value : 0,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = data }, JsonRequestBehavior.AllowGet);


        }

        #endregion

        #region[GetOfferdetailsList]
        public ActionResult GetOfferdetailsList(int pageSize, int pageIndex, string Name, string TransMode)
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

            OfferSettingsModel offer = new OfferSettingsModel();

            var datares = offer.GetOfferdetails(input: new OfferSettingsModel.OfferInput
            {
                TransMode = "",
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name = Name,
                FK_Offers = 0,
                Details = 0

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares.Process, datares.Data, pageIndex, pageSize, totalrecord = (datares.Data is null) ? 0 : datares.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetOfferReasonList]
        public ActionResult GetOfferReasonList()
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


            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[DeleteOfferSetting]
        public ActionResult DeleteOfferSetting(OfferSettingsModel.DeleteInput data)
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

            OfferSettingsModel model = new OfferSettingsModel();

            var result = model.DeleteOffer(input: new OfferSettingsModel.DeleteOfferSetting
            {
             TransMode = "",
             FK_Offers = data.ID_Offers,
             EntrBy = _userLoginInfo.EntrBy,
             FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
             FK_Company = _userLoginInfo.FK_Company,
             FK_Machine = _userLoginInfo.FK_Machine,
             FK_Reason = data.ReasonID,
             Debug = 0,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetOfferSettingInfoByID]
        public ActionResult GetOfferSettingInfoByID(OfferSettingsModel.OfferInput data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
            #region :: Model validation  ::
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

            OfferSettingsModel offer = new OfferSettingsModel();


            var result = offer.GetOfferGrid(input: new OfferSettingsModel.OfferInput
            {
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Offers = data.FK_Offers,
                Details = 1
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region 
        public ActionResult GetProductCategory(OfferSettingsModel.InputProductID datas)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<OfferSettingsModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "P.FK_Category FK_Category",
                Criteria = "P.Cancelled=0 AND P.Passed=1 AND P.ID_Product=" + datas.ProductID +"AND + P.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "P.FK_Category",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );


            
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }


}