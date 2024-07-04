/*----------------------------------------------------------------------
    Created By	: Kavya 
    Created On	: 31/10/2022
    Purpose		: EMI Collection
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
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    public class EMICollectionController : Controller
    {
        // GET: EMICollection
        public ActionResult EMICollectionIndex(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.mtd = mtd;
            ViewBag.TransMode = mGrp;
           // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadEMICollectionForm(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            EMICollectionModel.DropDownListModel ObjPayment = new EMICollectionModel.DropDownListModel();

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND PMMode<>2 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            ObjPayment.PaymentView = PaymentView.Data;

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            ObjPayment.Transtypelist = Transtypelist.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddEMICollection", ObjPayment);
        }
        public ActionResult GetEMIDetails(EMICollectionModel.EMICollectionview data)
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

            EMICollectionModel objemi = new EMICollectionModel();
            var EMIInfos = objemi.GetEMIDetails(companyKey: _userLoginInfo.CompanyKey, input: new EMICollectionModel.GetEMICollectionview
            {
                FK_Master = data.FK_Master,
                AccountMode = data.AccountMode,
                CollectionDate = data.CollectionDate,
                Advance = data.Advance,

            });
            return Json(EMIInfos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewEMICollection(EMICollectionModel.EMICollectionviewDetails data)
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
            EMICollectionModel EMICollection = new EMICollectionModel();
            var datresponse = EMICollection.UpdateEMICollectionData(input: new EMICollectionModel.UpdateEMICollection
            {
                UserAction = 1,
                ID_EMICollection = data.ID_EMICollection,
                AccountMode = data.CusTrCusType,
                FK_Master=data.FK_Master,
                TransMode = data.TransMode,
                TransDate = data.TransDate,
                CollectDate = data.CollectDate,
                CusTrCollectedBy = data.CusTrCollectedBy,
                TransType = data.TransType,
                CusTrAmount = data.CusTrAmount,
                CusTrFineAmount = data.CusTrFineAmount,
                CusTrFineWaiveAmount = data.CusTrFineWaiveAmount,
                NetAmount = data.NetAmount,
                FK_PaymentMethod = data.FK_PaymentMethod,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                EMIDetails = data.EMIDetail is null ? "" : Common.xmlTostring(data.EMIDetail),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetEMICollectionList(int pageSize, int pageIndex, string Name, string TransModes)
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

            EMICollectionModel emicollection = new EMICollectionModel();
            var SaleInfo = emicollection.GetEMICollectionData(companyKey: _userLoginInfo.CompanyKey, input: new EMICollectionModel.GetEMICollection
            {
                FK_CustomerTransaction = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = TransModes,
            });

            return Json(new { SaleInfo.Process, SaleInfo.Data, pageSize, pageIndex, totalrecord = (SaleInfo.Data is null) ? 0 : SaleInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteEMICollection(EMICollectionModel.EMiCollectionInfoView data)
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

            EMICollectionModel SalesOrder = new EMICollectionModel();
            var datresponse = SalesOrder.DeleteSalesOrderData(input: new EMICollectionModel.DeleteEMICollection
            {
                FK_CustomerTransaction = data.CustomerTransactionID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEMICollectionInfo(EMICollectionModel.EmiDataFetch ID)
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

            EMICollectionModel Emiget = new EMICollectionModel();


            var Fielddata = Emiget.GetEMICollectionDataFillFields(companyKey: _userLoginInfo.CompanyKey, input: new EMICollectionModel.GetEMICollection
            {
                FK_CustomerTransaction = ID.FK_CustomerTransaction,
                Detailed = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = ID.TransMode,
            });

            var Billdata = Emiget.GetEMICollectionDataFill(companyKey: _userLoginInfo.CompanyKey, input: new EMICollectionModel.GetEMICollection
            {
                FK_CustomerTransaction = ID.FK_CustomerTransaction,
                Detailed =1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,                
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = ID.TransMode,
            });

            var paymentdetail = Emiget.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new EMICollectionModel.GetPaymentin
            {
                FK_Master = ID.FK_CustomerTransaction,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = ID.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            return Json(new { Fielddata, Billdata, paymentdetail }, JsonRequestBehavior.AllowGet);
        }
    }
}