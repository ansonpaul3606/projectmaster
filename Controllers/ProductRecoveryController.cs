using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ProductRecoveryController : Controller
    {
        // GET: ProductRecovery
        #region[Index]
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.TransMode = mGrp;

            return View();
        }
        #endregion

        #region[LoadProductRecovery]
        public ActionResult LoadProductRecovery(string mtd, string TransMode)
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

            ProductRecoveryModel product = new ProductRecoveryModel();
            ProductRecoveryModel.ProductRecoveryView view = new ProductRecoveryModel.ProductRecoveryView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            return PartialView("_AddProductRecovery", view);
        }
        #endregion

        #region[GetCustomerPrEmiDue]
        public ActionResult GetCustomerTransactionDue(ProductRecoveryModel.ProductRecoveryView view)
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
            ProductRecoveryModel product = new ProductRecoveryModel();

            var result = product.GetCustomerProductDetails(new ProductRecoveryModel.ProductRecoveryView
            {
                FK_Customer = view.FK_Customer,
                CollectionDate = view.CollectionDate

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { result, data = result.Data, result.Process }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[AddProductRecoverydata]
        public ActionResult AddProductRecoverydata(ProductRecoveryModel.InsertInputView view)
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
            ProductRecoveryModel model = new ProductRecoveryModel();

            var datares = model.UpdateProductRecoverydetails(input: new ProductRecoveryModel.UpdateProductRecovery
            {
                UserAction = 1,
                TransMode = view.TransMode,
                ID_ProductRecovery = 0,
                EntrBy = _userLoginInfo.EntrBy,
                //FK_ProductRecovery = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                //FK_Customer = view.FK_Customer,
                ProdRecoveryDate = view.ProdRecoveryDate,
                Debug = 0,
                PickUpDate = view.PickUpDate,
                PickUpTime = view.PickUpTime,
                FK_Employee = view.FK_Employee,
                VehicleDet = view.VehicleDet,
                ProdRecNarration = view.ProdRecNarration,
                LastID= (view.LastID.HasValue) ? view.LastID.Value : 0,
                ProductRecoveryDetails = view.ProductRecoveryDetails is null ? "" : Common.xmlTostring(view.ProductRecoveryDetails)
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetProductRecoveryList]
        public ActionResult GetProductRecoveryList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ProductRecoveryModel model = new ProductRecoveryModel();

            var data = model.GetProRecoList(input: new ProductRecoveryModel.ProductRecoveryInput
            {

                TransMode = TransMode,
                ProdGroupID = 0,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
                Detailed = 0

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetProductRecoveryInfoByID]
        public ActionResult GetProductRecoveryInfoByID(ProductRecoveryModel.ProductRecoveryInput info)
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
            ProductRecoveryModel item = new ProductRecoveryModel();

            var datares = item.GetProRecoveryGrid(input: new ProductRecoveryModel.ProductRecoveryInput
            {
                TransMode = info.TransMode,
                PageSize = 0,
                PageIndex = 0,
                ProdGroupID = info.ProdGroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetProRecoReasonList]
        public ActionResult GetProRecoReasonList()
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

        #region[DeleteProdData]
        public ActionResult DeleteProdData(ProductRecoveryModel.DeleteInput delete)
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

            ProductRecoveryModel model = new ProductRecoveryModel();

            var dltdata = model.DeleteProRecoveryData(input: new ProductRecoveryModel.DeleteProRecovery
            {
                TransMode = delete.TransMode,
                ProdGroupID = delete.ProdGroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = delete.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

             return Json(new { Process = dltdata }, JsonRequestBehavior.AllowGet);       
        }
        #endregion
    }
}