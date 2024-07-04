/*----------------------------------------------------------------------
Created By	: Aiswarya
Created On	: 31/01/2022
Purpose		: Purchase
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
    public class PurchaseController : Controller
    {
       
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.Fk_BranchCode1 = _userLoginInfo.FK_BranchCodeUser;
            ViewBag.FK_Branch1 = _userLoginInfo.FK_Branch;
            ViewBag.FK_States = _userLoginInfo.FK_States;
            Common.ClearOtherCharges(ViewBag.TransMode);            
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            //ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }
      
        public ActionResult LoadPurchaseForm(string mtd, string TransMode)
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
            ViewBag.FK_States = _userLoginInfo.FK_States;
            PurchaseModel.DropDownListModel Prd = new PurchaseModel.DropDownListModel();
            APIParameters ApiSupplier = new APIParameters
            {
                TableName = "Supplier",
                SelectFields = "ID_Supplier AS SupplierID,SuppName AS SupplierName,SuppAddress as Address,SuppMobile as Mobile,SuppGSTINNo as GSTIN",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            };
            var Supplier = Common.GetDataViaQuery<PurchaseModel.Supplierlist>(companyKey: _userLoginInfo.CompanyKey, parameters: ApiSupplier);
            Prd.SupplierView = Supplier.Data;
            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "ID_Unit AS ID_Unit,UnitName AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var unit = Common.GetDataViaQuery<PurchaseModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);

            Prd.UnitList = unit.Data;
            APIParameters apiParaModule = new APIParameters
            {
                TableName = "[ModuleType]",
                SelectFields = "[ID_ModuleType] AS ID_ModuleType,[ModuleName] AS ModuleName,[Mode] as Mode ",
                Criteria = "Mode='P'",
                GroupByFileds = "",
                SortFields = ""
            };
            var Module = Common.GetDataViaQuery<PurchaseModel.ModuleType>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaModule);
            Prd.ModuleTypeList = Module.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            Common.ClearOtherCharges(TransMode);
            return PartialView("_AddPurchaseFormNew", Prd);
        }
        public ActionResult LoadFormOtherChargeType()
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

          //  OtherChargeTypeModel.OtherChargeTypeViewList otherchargeviewlist = new OtherChargeTypeModel.OtherChargeTypeViewList();

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
         //   otherchargeviewlist.TransTypeList = Transtypelist.Data;


            return Json(Transtypelist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUnitList(UnitModel.UnitView data)
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

            UnitModel unitlist = new UnitModel();

            var outputList = unitlist.GetUnitData(companyKey: _userLoginInfo.CompanyKey, input: new UnitModel.GetUnitList
            {
                FK_Unit = data.UnitID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
            });



            return Json(outputList, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult GetPurchaseList()
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
            PurchaseModel Purchase = new PurchaseModel();

            var PurcInfo = Purchase.GetPurchaseData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchase { ID_Purchase = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });
           // var PurDetailsInfo=
            return Json(PurcInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPurchaseInfo(PurchaseModel.PurchaseView purInfo)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::

            //--- Model validation 
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::


            PurchaseModel purchase = new PurchaseModel();
            Common.fillOtherCharges(purInfo.TransMode, purInfo.PurchaseID);
            var prInfo = purchase.GetPurchaseData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchase { ID_Purchase = purInfo.PurchaseID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
           var PurchaseDetails = purchase.GetPurchaseDetails(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchase { ID_Purchase = purInfo.PurchaseID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });
            var TaxDetails = purchase.GetTaxDetails(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetSubTablePurchase { ID_Purchase = purInfo.PurchaseID,Mode="P", FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });
            var OtherCharge = purchase.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetSubTablePurchaseNew { FK_Transaction = purInfo.PurchaseID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            // var tax = purchase.GettaxData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchase { ID_Product = purInfo.ProductID, FK_Company = _userLogin"nfo.FK_Company, UserCode = _userLoginInfo.EntrBy });

            return Json(new { prInfo, PurchaseDetails, TaxDetails, OtherCharge }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaxAmount(PurchaseModel.BindTaxAmount Data)
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

            var datresponse = purchaseModel.FillTax(input: new PurchaseModel.BindTaxAmount
            {
                PurRate = Data.PurRate,
                Includetax = Data.Includetax,
                InterState = false,
                Quantity = Data.Quantity,
                FK_Product = Data.FK_Product,

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaxAmountNew(PurchaseModel.BindTaxAmountNew Data)
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


            //if our branches statecode and selected supplier state code are different
          //  Data.TaxtyInterstate=1

            var datresponse = purchaseModel.FillTaxNew(input: new PurchaseModel.BindTaxAmountNew
            {
                Amount = Data.Amount,
                Includetax = Data.Includetax,
                FK_Product = Data.FK_Product,
               TaxtyInterstate=Data.TaxtyInterstate,

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
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

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=1",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSupplierList()
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

            var data = Common.GetDataViaQuery<PurchaseModel.Supplierlist>(parameters: new APIParameters
            {
                TableName = "Supplier",
                SelectFields = "ID_Supplier AS SupplierID,SuppName AS SupplierName,SuppAddress as Address,SuppMobile as Mobile,SuppGSTINNo as GSTIN",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }
        public ActionResult GetDepartmentList()
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

            var data = Common.GetDataViaQuery<PurchaseModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department D join Employee E on E.FK_Department=D.ID_Department join Users U on U.FK_Employee=E.ID_Employee",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName,DeptShortName as ShortName",
                Criteria = "D.cancelled=0 AND D.Passed =1 AND D.FK_Company=" + _userLoginInfo.FK_Company+ "AND U.ID_Users = " + _userLoginInfo.ID_Users,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }
        public ActionResult GetBranchList()
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

            var data = Common.GetDataViaQuery<PurchaseModel.BranchList>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName,BrCode as BranchCode,BrShortName as ShortName,BrAddress1 as Address,BrMobile Mobile",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND ID_Branch = " + _userLoginInfo.FK_Branch,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }
        public ActionResult GetPurchaseOrderList()
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

            var data = Common.GetDataViaQuery<PurchaseModel.Supplierlist>(parameters: new APIParameters
            {
                TableName = "Supplier",
                SelectFields = "ID_Supplier AS SupplierID,SuppName AS SupplierName,SuppAddress as Address,SuppMobile as Mobile,SuppGSTINNo as GSTIN",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewPurchase(PurchaseModel.PurchaseView data)
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

            ModelState.Remove("PurchaseID");
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

            PurchaseModel Purchase = new PurchaseModel();

            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            var datresponse = Purchase.UpdatePurchaseData(input: new PurchaseModel.UpdatePurchase
            {
                UserAction = 1,

                // PurBillNo = data.BillNo,
                TransMode= data.TransMode,
                PurInvoiceNo = data.InvoiceNo,
                PurInvoiceDate = data.InvoiceDate,
                PurBillTotal = data.BillTotal,
                PurDiscount = data.Discount,
                //PurOtherCharge = data.OtherCharge,
                PurRoundOff = data.RoundOff,
                PurNetAmount = data.NetAmount,
                PurPayableAmount = data.PayableAmount,
                TaxtyIntrastate = data.TaxtyIntrastate,
                FK_Quotation = data.Quotation,
                //FK_BillType = data.BillType,
                //FK_PurchaseOrder = data.PurchaseOrder,
                FK_Supplier = data.SupplierID,
                FK_Branch = data.BranchID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.DepartmentID,
                //FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                // EntrBy = _userLoginInfo.EntrBy,
                  FK_Machine = _userLoginInfo.FK_Machine,
                PurchaseDetails = data.PurchaseDetails is null ? "" : Common.xmlTostring(data.PurchaseDetails),

                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PTaxDetails = data.PTaxDetails is null ? "" : Common.xmlTostring(data.PTaxDetails),
                //OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ID_Purchase = 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {
                
                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdatePurchase(PurchaseModel.PurchaseView data)
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

            PurchaseModel Purchase = new PurchaseModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = Purchase.UpdatePurchaseData(input: new PurchaseModel.UpdatePurchase
            {
               /// UserAction = 2
    
              
                
                PurInvoiceNo = data.InvoiceNo,
                PurInvoiceDate = data.InvoiceDate,
                PurBillTotal = data.BillTotal,
                PurDiscount = data.Discount,
                PurOtherCharge = data.OtherCharge,
                PurRoundOff = data.RoundOff,
                PurNetAmount = data.NetAmount,
                PurPayableAmount = data.PayableAmount,
                TaxtyIntrastate = data.TaxtyIntrastate,
              //  FK_Quotation = data.Quotation,
              //  FK_BillType = data.BillType,
               // FK_PurchaseOrder = data.PurchaseOrder,
                FK_Supplier = data.SupplierID,
                FK_Branch = data.BranchID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.DepartmentID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult DeletePurchase(PurchaseModel.PurchaseView data)
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
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = ModelState.Values.SelectMany(m => m.Errors)
        //                .Select(e => e.ErrorMessage)
        //                .ToList(),
        //                Status = "Validation failed",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    PurchaseModel Purchase = new PurchaseModel();

        //    var datresponse = Purchase.DeletePurchaseData(input: new PurchaseModel.DeletePurchase { ID_Purchase =0, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = 0}, companyKey: _userLoginInfo.CompanyKey);
        //    return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult GetPurchaseReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewPurchaseNew(PurchaseModel.PurchaseViewNew data)
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

            ModelState.Remove("PurchaseID");
            ModelState.Remove("InvoiceNo");
            ModelState.Remove("UnitID");
            ModelState.Remove("NetAmount"); 
            ModelState.Remove("FK_PurchaseOrder");
            

            PurchaseModel Purchase = new PurchaseModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = Purchase.UpdatePurchaseDataNew(input: new PurchaseModel.UpdatePurchaseNew
            {
                UserAction = 1,

                // PurBillNo = data.BillNo,
                TransMode = data.TransMode,
                PurInvoiceNo = data.InvoiceNo,
                PurInvoiceDate = data.InvoiceDate,
                PurBillTotal = data.BillTotal,
                PurDiscount = data.Discount,
                PurDiscountPer=data.PurDiscountPer,
                PurOtherCharge = data.PurOtherCharge,
                PurRoundOff = data.RoundOff,
                PurNetAmount = data.NetAmount,
                PurPayableAmount = data.PayableAmount,
                TaxtyIntrastate = data.TaxtyIntrastate,
                FK_Quotation = data.Quotation,
                //FK_BillType = data.BillType,
                FK_PurchaseOrder = data.FK_PurchaseOrder,
                FK_Supplier = data.SupplierID,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.DepartmentID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PurchaseDetails = data.PurchaseDetails is null ? "" : Common.xmlTostring(data.PurchaseDetails),

                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),

                PTaxDetails = data.PTaxDetails is null ? "" : Common.xmlTostring(data.PTaxDetails),
                //OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ID_Purchase = 0,
                PurAdvAmount = data.PurAdvAmount,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPurchaseListNew(int pageSize, int pageIndex, string Name, string TransModes)
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
            string transMode = "";
            PurchaseModel Purchase = new PurchaseModel();

            var PurcInfo = Purchase.GetPurchaseDataNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchaseNew {
                ID_Purchase = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransModes,
                Details=0,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
            });
            // var PurDetailsInfo=
            return Json(new { PurcInfo.Process, PurcInfo.Data, pageSize, pageIndex, totalrecord = (PurcInfo.Data is null) ? 0 : PurcInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPurchaseInfoNew(PurchaseModel.PurchaseViewNew purInfo)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::

            //--- Model validation 
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::

           

            PurchaseModel purchase = new PurchaseModel();
            Common.fillOtherCharges(purInfo.TransMode, purInfo.PurchaseID);
            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            var prInfo = purchase.GetPurchaseDataNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchaseNew
            {
                ID_Purchase = purInfo.PurchaseID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                Details = 0,
                TransMode= purInfo.TransMode,
            });

            var PurchaseDetails = purchase.GetPurchaseDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchaseNew
            {
                ID_Purchase = purInfo.PurchaseID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Details = 1,
                TransMode = purInfo.TransMode,
            });

            var PTaxDetails = purchase.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.PurchaseNewTax
            {
                ID_Transaction = purInfo.PurchaseID,               
                TransMode = purInfo.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var OtherCharge = purchase.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetSubTablePurchaseNew
            {
                FK_Transaction = purInfo.PurchaseID,
                TransMode = purInfo.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            //var tax = purchase.GettaxData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchaseNew
            //{
            //    ID_Product = purInfo.ProductID,
            //    FK_Company = _userLoginInfo.FK_Company,
            //    UserCode = _userLoginInfo.EntrBy });
            

            return Json(new { Transtypelist,prInfo, PurchaseDetails, PTaxDetails, OtherCharge }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdatePurchaseNew(PurchaseModel.PurchaseViewNew data)
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
            ModelState.Remove("NetAmount");
            ModelState.Remove("PurchaseID");
            ModelState.Remove("InvoiceNo");
            ModelState.Remove("UnitID");
            ModelState.Remove("ReferenceNo");
            ModelState.Remove("BranchID");
            ModelState.Remove("ProductID");
            ModelState.Remove("PpdDisper");
            ModelState.Remove("FK_PurchaseOrder");
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

            PurchaseModel Purchase = new PurchaseModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = Purchase.UpdatePurchaseDataNew(input: new PurchaseModel.UpdatePurchaseNew
            {
                UserAction = 2,
                TransMode = data.TransMode,
                ID_Purchase =data.ID_Purchase,
                PurInvoiceNo = data.InvoiceNo,
                PurInvoiceDate = data.InvoiceDate,
                PurBillTotal = data.BillTotal,
                PurDiscount = data.Discount,
                PurDiscountPer=data.PurDiscountPer,
                PurOtherCharge = data.PurOtherCharge,
                PurRoundOff = data.RoundOff,
                PurNetAmount = data.NetAmount,
                PurPayableAmount = data.PayableAmount,
                TaxtyIntrastate = data.TaxtyIntrastate,
                FK_PurchaseOrder=data.FK_PurchaseOrder,
                //  FK_Quotation = data.Quotation,
                //  FK_BillType = data.BillType,
                // FK_PurchaseOrder = data.PurchaseOrder,
                FK_Supplier = data.SupplierID,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.DepartmentID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PurchaseDetails = data.PurchaseDetails is null ? "" : Common.xmlTostring(data.PurchaseDetails),

                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                //TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails),
                //OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                PurAdvAmount =data.PurAdvAmount,
            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeletePurchase(PurchaseModel.PurchaseViewNew data)
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
            ModelState.Remove("PurchaseID");
            ModelState.Remove("InvoiceNo");
            ModelState.Remove("UnitID");
            ModelState.Remove("ReferenceNo");
            ModelState.Remove("BranchID");
            ModelState.Remove("ProductID");
            ModelState.Remove("PpdDisper");
            ModelState.Remove("SupplierID");
            ModelState.Remove("DepartmentID");
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


            PurchaseModel model = new PurchaseModel();

            Output dataresponse = model.DeletePurchaseData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.DeletePurchase
            {
                FK_Purchase = data.PurchaseID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            });

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductUnit(int ProductID)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<PurchaseModel.Unit>(parameters: new APIParameters
            {
                TableName = "Product P LEFT JOIN Unit U ON U.ID_Unit = P.FK_Unit",
                SelectFields = "U.ID_Unit AS ID_Unit,U.UnitName AS UnitName,U.NoOfUnits AS UnitCount",
                Criteria = "P.Cancelled =0 AND P.Passed=1 AND P.ID_Product=" + ProductID + " AND P.FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetPurchaseFill(PurchaseModel.Purchasefill Data)
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

            var datresponse = purchaseModel.FilPurchase(input: new PurchaseModel.Purchasefill
            {
                FK_Master = Data.FK_Master,
                Mode = Data.Mode,


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductTopRate(PurchaseModel.PurchaseProducttopview data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            PurchaseModel purchaseModel = new PurchaseModel();

            var datresponse = purchaseModel.GetProductTopRate(input: new PurchaseModel.PurchaseProducttopview
            {
                BranchID=_userLoginInfo.FK_Branch,
                DepartmentID=data.DepartmentID,
                ProductID=data.ProductID,
                FK_Company=_userLoginInfo.FK_Company


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
    }
}


