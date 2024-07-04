using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ProductRateSettingsController : Controller
    {
        // GET: ProductRateSettings
        public ActionResult Index()
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadFormProductRateSettings()
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
            ProductRateSettingsModel.Productratesettingsview prdListObj = new ProductRateSettingsModel.Productratesettingsview();
            var Category = Common.GetDataViaQuery<ProductRateSettingsModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS FK_Category ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            prdListObj.CategoryList = Category.Data;

            return PartialView("_AddProductRateSettings", prdListObj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProductRateSettings(ProductRateSettingsModel.Productratesettingsview data)
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

            ModelState.Remove("ProductRateSettingDetails");


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


            ProductRateSettingsModel objprdwise = new ProductRateSettingsModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
           
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateProductratesettingsData(input: new ProductRateSettingsModel.ProductratesettingsUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
               // FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_ProductRateSetting=0,
                EffectDate = data.EffectDate,
                ExpiryDate = data.ExpiryDate,
                FK_PriceFixingType = data.FK_PriceFixingType,
                Active=data.Active,
                ProductRateSettingDetails = data.ProductRateSettingDetails is null ? "" : Common.xmlTostring(data.ProductRateSettingDetails),


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProductRateSettings(ProductRateSettingsModel.Productratesettingsview data)
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

            ModelState.Remove("ProductRateSettingDetails");


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


            ProductRateSettingsModel objprdwise = new ProductRateSettingsModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateProductratesettingsData(input: new ProductRateSettingsModel.ProductratesettingsUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
               // FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_ProductRateSetting=data.ID_ProductRateSetting,
                EffectDate = data.EffectDate,
                ExpiryDate = data.ExpiryDate,
                FK_PriceFixingType = data.FK_PriceFixingType,
                Active=data.Active,
                ProductRateSettingDetails = data.ProductRateSettingDetails is null ? "" : Common.xmlTostring(data.ProductRateSettingDetails),


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetProductRateSettingsList(int pageSize, int pageIndex, string Name)
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

            ProductRateSettingsModel objPrd = new ProductRateSettingsModel();
            var data = objPrd.GetProductratesettingsData(companyKey: _userLoginInfo.CompanyKey, input: new ProductRateSettingsModel.ProductratesettingsID
            {

                FK_Productratesettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Details = 0,
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductratesettingsInfo(ProductRateSettingsModel.ProductratesettingsID data)
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
            ProductRateSettingsModel objprd = new ProductRateSettingsModel();

            var mptableInfo = objprd.GetProductratesettingsDatainfoid(companyKey: _userLoginInfo.CompanyKey, input: new ProductRateSettingsModel.ProductratesettingsID
            {
                FK_Productratesettings = data.FK_Productratesettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Details = 0,
                EntrBy = _userLoginInfo.EntrBy
            });

            var subtable = objprd.GetProductratesettingsSubtabledata(companyKey: _userLoginInfo.CompanyKey, input: new ProductRateSettingsModel.ProductRateSettingDetailsSubSelect {
                FK_Productratesettings = data.FK_Productratesettings,
                Details = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].ProductRateSettingDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPrdratesettingsDeleteReasonList()
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
        [HttpPost]
        [ValidateAntiForgeryToken()]
       
        public ActionResult DeleteProductRateSettings(ProductRateSettingsModel.PrdratesettingsRsnView data)
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

            ProductRateSettingsModel.ProductratesettingsDelete objprddel = new ProductRateSettingsModel.ProductratesettingsDelete
            {
                FK_Productratesettings = data.ID_Productratesettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<ProductRateSettingsModel.ProductratesettingsDelete>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProProductratesettingsDelete", parameter: objprddel);
            //test data
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

    }
}