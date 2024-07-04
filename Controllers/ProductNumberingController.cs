using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ProductNumberingController : Controller
    {       
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
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadProductNumberingList(string TransMode, string mtd)
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

            ProductNumberingModel objProNum = new ProductNumberingModel();
            ProductNumberingModel.ProductNumbering objProNumNew = new ProductNumberingModel.ProductNumbering();

            var importFrom = objProNum.GetModeList(input: new ProductNumberingModel.GetModeData { Mode = 66 }, companyKey: _userLoginInfo.CompanyKey);
            objProNumNew.ImportFromList = importFrom.Data.AsEnumerable();
            ViewBag.TransMode = TransMode;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_LoadProductNumbering", objProNumNew);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddProductNumbering(ProductNumberingModel.ProductNumberingNew data)
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
            ProductNumberingModel objProductNumber = new ProductNumberingModel();

            var datresponse = objProductNumber.UpdateProductNumberingData(input: new ProductNumberingModel.ProductNumberingUpdate
            {
                UserAction = 1,
                TransMode = data.TransMode,
                ProdNumModule = data.ProdNumModule,
                FK_Master = data.FK_Master,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,                
                ProductNumberingDetails = data.ProductNumberingDetails is null ? "" : Common.xmlTostring(data.ProductNumberingDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductNumberingData(int pageSize, int pageIndex, string Name)
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
            ProductNumberingModel ObjMo = new ProductNumberingModel();
            var ProductNumberInfo = ObjMo.GetProductNumberingData(companyKey: _userLoginInfo.CompanyKey, input: new ProductNumberingModel.GetProductNumbering
            {
                ProdNumGroupID = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detailed = 0
            });
            return Json(new { ProductNumberInfo.Process, ProductNumberInfo.Data, pageSize, pageIndex, totalrecord = (ProductNumberInfo.Data is null) ? 0 : ProductNumberInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProductNumberingInfo(ProductNumberingModel.GetProductNumbering obj)
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
            ProductNumberingModel ObjMo = new ProductNumberingModel();
            var ProductNumberInfo = ObjMo.GetProductNumberingData(companyKey: _userLoginInfo.CompanyKey, input: new ProductNumberingModel.GetProductNumbering
            {
                ProdNumGroupID = obj.ProdNumGroupID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,               
                Detailed = 0
            });

            var ProductNumberInfoDtls = ObjMo.GetProductNumberingDtlData(companyKey: _userLoginInfo.CompanyKey, input: new ProductNumberingModel.GetProductNumbering
            {
                ProdNumGroupID = obj.ProdNumGroupID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1
            });


            return Json(new { ProductNumberInfo, ProductNumberInfoDtls }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductNumberingReasonList()
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
                EntrBy = _userLoginInfo.EntrBy
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
        public ActionResult DeleteProductNumbering(ProductNumberingModel.DeleteProductNumbering data)
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

            ProductNumberingModel objProductNu = new ProductNumberingModel();

            var datresponse = objProductNu.DeleteProductNumberingData(input: new ProductNumberingModel.DeleteProductNumbering
            {
                ProdNumGroupID = data.ProdNumGroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductSerialNumbers(ProductNumberingModel.GetProductSerialNumbers obj)
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
            ProductNumberingModel ObjMo = new ProductNumberingModel();
            var ProductSerialNumberInfo = ObjMo.GetProductSerialNumber(companyKey: _userLoginInfo.CompanyKey, input: new ProductNumberingModel.GetProductSerialNumbers
            {     
                FK_Company = _userLoginInfo.FK_Company,
                FK_Product= obj.FK_Product,
                FK_Stock = obj.FK_Stock
            });           

            return Json(new { ProductSerialNumberInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}