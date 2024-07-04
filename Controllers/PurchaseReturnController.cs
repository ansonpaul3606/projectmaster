/*----------------------------------------------------------------------
Created By	: Anson Paul
Created On	: 01/09/2022
Purpose		: PurchaseReturn
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
    public class PurchaseReturnController : Controller
    {
        public ActionResult PurchaseReturn(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.FK_States = _userLoginInfo.FK_States;
            ViewBag.mtd = mtd;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadFormPurchaseReturn(string mtd)
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
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            PurchaseReturnModel.PurchaseReturnView sortno = new PurchaseReturnModel.PurchaseReturnView();

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "ID_Unit AS ID_Unit,UnitName AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var unit = Common.GetDataViaQuery<PurchaseReturnModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);

            sortno.UnitList = unit.Data;
            return PartialView("_AddPurchaseReturnForm", sortno);
        }

        [HttpPost]
        public ActionResult GetPurchaseReturnList(int pageSize, int pageIndex, string Name, string TransModes)
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
            PurchaseReturnModel PurchaseReturn = new PurchaseReturnModel();

            var PurcInfo = PurchaseReturn.GetPurchaseReturnDataForList(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.GetPurchaseReturn
            {
                FK_PurchaseReturn = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransModes,
                Details = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            return Json(new { PurcInfo.Process, PurcInfo.Data, pageSize, pageIndex, totalrecord = (PurcInfo.Data is null) ? 0 : PurcInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddPurchaseReturn(PurchaseReturnModel.PurchaseReturnView data)
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

            //ModelState.Remove("PurchaseReturnID");
            //if (!ModelState.IsValid)
            //{
            //    List<string> errorList = new List<string>();

            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //            .Select(e => e.ErrorMessage)
            //            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            PurchaseReturnModel PurchaseReturn = new PurchaseReturnModel();

            var datresponse = PurchaseReturn.UpdatePurchaseReturnData(input: new PurchaseReturnModel.UpdatePurchaseReturn
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_PurchaseReturn = 0,
                PrBillNo = "",
                PrReferenceNo = data.PrReferenceNo,
                PrBillDate = data.PrBillDate,
                PrInvoiceNo = data.PrInvoiceNo,
                PrInvoiceDate = data.PrInvoiceDate,
                // TotalTax=data.TotalTax,
                PrBillTotal = data.PrBillTotal,
                PrDiscount = data.PrDiscount,
                PrOthercharges = data.PrOthercharges,
                PrRoundoff = data.PrRoundoff,
                PrNetAmount = data.PrNetAmount,
                PrRemarks = data.PrRemarks,
                TaxtyIntrastate = 1/*data.TaxtyIntrastate*/,
                FK_Supplier = data.FK_Supplier,
                FK_BillType = data.BillType,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Purchase = data.FK_Purchase,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ApprovedBy = "",
                PRReturnType = data.PRReturnType,
                //ApprovedOn= data.PrInvoiceDate,
                PurchaseReturnDetails = data.PurchaseReturnDetails is null ? "" : Common.xmlTostring(data.PurchaseReturnDetails),
                TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                //FK_PurchaseReturn=0,


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdatePurchaseReturn(PurchaseReturnModel.PurchaseReturnView data)
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

            //if (!ModelState.IsValid)
            //{
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //            .Select(e => e.ErrorMessage)
            //            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            PurchaseReturnModel PurchaseReturn = new PurchaseReturnModel();
            var datresponse = PurchaseReturn.UpdatePurchaseReturnData(input: new PurchaseReturnModel.UpdatePurchaseReturn
            {
                UserAction = 2,
                PrBillNo = data.PrBillNo,
                TransMode = data.TransMode,
                PrReferenceNo = data.PrReferenceNo,
                ID_PurchaseReturn = data.ID_PurchaseReturn,
                PrBillDate = data.PrBillDate,
                PrInvoiceNo = data.PrInvoiceNo,
                PrInvoiceDate = data.PrInvoiceDate,
                PrBillTotal = data.PrBillTotal,
                PrDiscount = data.PrDiscount,
                PrOthercharges = data.PrOthercharges,
                PrRoundoff = data.PrRoundoff,
                PrNetAmount = data.PrNetAmount,
                PrRemarks = data.PrRemarks,
                //TaxtyIntrastate = data.TaxtyIntrastate,
                FK_Supplier = data.FK_Supplier,
                FK_BillType = data.BillType,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Purchase = data.FK_Purchase,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ApprovedBy = "",
                //ApprovedOn = data.PrInvoiceDate,
                PurchaseReturnDetails = data.PurchaseReturnDetails is null ? "" : Common.xmlTostring(data.PurchaseReturnDetails),
                TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPurchaseReturnInfo(PurchaseReturnModel.PurchaseReturnView purInfo)
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

            PurchaseReturnModel purchasereturn = new PurchaseReturnModel();

            var PurRetInfo = purchasereturn.GetPurchaseReturnData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.GetPurchaseReturn
            {
                FK_PurchaseReturn = purInfo.ID_PurchaseReturn,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = purInfo.TransMode,
                Details = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            var PrRetProducts = purchasereturn.GetPurchaseReturnProductData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.GetPurchaseReturn
            {
                FK_PurchaseReturn = purInfo.ID_PurchaseReturn,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = purInfo.TransMode,
                Details = 1,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            var OtherCharge = purchasereturn.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.GetOtherchargeDetails
            {
                FK_Transaction = purInfo.ID_PurchaseReturn,
                TransMode = purInfo.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            });
            var TaxDetails = purchasereturn.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.PurchaseTaxGet
            {
                ID_Transaction = purInfo.ID_PurchaseReturn,
                Mode = "P",
                TransMode = purInfo.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            });
            return Json(new { PurRetInfo, PrRetProducts, OtherCharge, TaxDetails }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeletePurchaseReturn(PurchaseReturnModel.PurchaseReturnView data)
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

            PurchaseReturnModel PurchaseReturn = new PurchaseReturnModel();

            var datresponse = PurchaseReturn.DeletePurchaseReturnData(input: new PurchaseReturnModel.DeletePurchaseReturn
            {
                FK_PurchaseReturn = data.ID_PurchaseReturn,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetPurchaseReturnReasonList()
        //{

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    ReasonModel reason = new ReasonModel();

        //    var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { ID_Reason = 0, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.UserCode });

        //    APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
        //    {
        //        Process = outputList.Process,
        //        Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
        //    };
        //    return Json(deleteReason, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetPurchaseReturnFill(PurchaseReturnModel.PurchaseReturnfill Data)
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

            PurchaseReturnModel purchasereturnModel = new PurchaseReturnModel();

            var datresponse = purchasereturnModel.FilPurchaseReturn(input: new PurchaseReturnModel.PurchaseReturnfill
            {
                FK_Master = Data.FK_Master,
                Mode = Data.Mode,
            }, companyKey: _userLoginInfo.CompanyKey);

            var TaxDetails = purchasereturnModel.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.PurchaseTaxGet
            {
                ID_Transaction = Data.FK_Master,
                Mode = "P",
                TransMode = "INPU",
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPurchaseReturnTaxFill(PurchaseReturnModel.PurchaseReturnfill Data)
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

            PurchaseReturnModel purchasereturnModel = new PurchaseReturnModel();

            var TaxDetails = purchasereturnModel.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.PurchaseTaxGet
            {
                ID_Transaction = Data.FK_Master,
                Mode = "P",
                TransMode = "INPU",
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(new { TaxDetails }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOtherCharges(PurchaseModel.BindOtherCharge Data)
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

            PurchaseModel purchaseModel = new PurchaseModel();

            var OtherCharges = purchaseModel.FillOtherCharges(input: new PurchaseModel.BindOtherCharge
            {
                TransMode = Data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=2",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductUnit(long FK_Product)
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
            PurchaseReturnModel ProductUnitData = new PurchaseReturnModel();
            var UnitSelect = ProductUnitData.GetProductUnitData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseReturnModel.GetProductUnitDtls
            {
                FK_Product = FK_Product,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            });
            
            return Json(UnitSelect, JsonRequestBehavior.AllowGet);
        }
    }
}
