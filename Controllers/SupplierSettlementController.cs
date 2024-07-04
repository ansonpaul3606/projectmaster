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
using System.IO;

namespace PerfectWebERP.Controllers
{
    public class SupplierSettlementController : Controller
    {
        // GET: SupplierSettlement
        public ActionResult SupplierSettlementIndex(string mgrp)
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
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;
            //ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadSupplierSettlementForm()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            SupplierSettlementModel.DropDownListModelView ObjSupplierSettlement = new SupplierSettlementModel.DropDownListModelView();

            var OpBranchList = Common.GetDataViaQuery<SupplierSettlementModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                     companyKey: _userLoginInfo.CompanyKey

          );
            ObjSupplierSettlement.BranchList = OpBranchList.Data;

            var OpDepartmentList = Common.GetDataViaQuery<SupplierSettlementModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                      companyKey: _userLoginInfo.CompanyKey

           );
            ObjSupplierSettlement.DepartmentList = OpDepartmentList.Data;

            var PaymentView = Common.GetDataViaQuery < PaymentMethodModel.PaymentMethodView > (parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
     companyKey: _userLoginInfo.CompanyKey
    );
            ObjSupplierSettlement.PaymentView= PaymentView.Data;

           
            var BillTypeList = Common.GetDataViaQuery<SupplierSettlementModel.BillTypes>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=1 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );
            ObjSupplierSettlement.BillTypeListView = BillTypeList.Data;

            var PaymentViewpayment = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company +"AND PMMode!= "+2,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            ObjSupplierSettlement.PaymentViewpayment = PaymentViewpayment.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "SupplierType",
                FieldName = "SortOrder",
                Debug = 0
            });

            ObjSupplierSettlement.SortOrder = a.Data[0].NextNo;
            SupplierSettlementModel SupplierType = new SupplierSettlementModel();

            var SuppInfo = SupplierType.GetSupplierModeData(companyKey: _userLoginInfo.CompanyKey, input: new SupplierSettlementModel.Modetype { Mode = 83 });

            ObjSupplierSettlement.listMode = SuppInfo.Data;
            return PartialView("_AddSupplierSettlement", ObjSupplierSettlement);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetOtherchargeInfoNew(SupplierSettlementModel.DropDownListModelView Info)
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

            
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::


            #endregion :: Model validation  ::



            SupplierSettlementModel purchase = new SupplierSettlementModel();

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            var OtherCharge = purchase.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SupplierSettlementModel.Getotherchargelist
            {
                InvoiceType = Info.ddlimportfrom,
                FK_Master = Info.ImportID,
                FK_Company = _userLoginInfo.FK_Company,
             
               
            });

            

            return Json(new { Transtypelist, OtherCharge }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewSuppSettlement(SupplierSettlementModel.DropDownListModelView data)
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

            ModelState.Remove("SalesID");


            SupplierSettlementModel SuppS = new SupplierSettlementModel();
            string warimage = (string)Session["WarProductImage"];


            var datresponse = SuppS.UpdateSuppStlmentData(input: new SupplierSettlementModel.UpdateSupplierSettlement
            {

                UserAction = 1,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_SuppSettlement = 0,

                TransMode = data.TransMode,
                StInvoiceType = data.BillTypeID,
                StImportfrom = data.ddlimportfrom,
                TransDate = data.TransDate,
                StOtherCharge = data.StOtherCharge,
                FK_Supplier = data.SuppID,
                FK_Master = data.ImportID,
                StBillTotal = data.NetAmount,
                StDiscount = data.Discount,
                StRoundOff = data.Supproundoff,
                StPaymentMode = data.ddltransType,

                StAdvanceamount = data.AdvanceAmount,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company=_userLoginInfo.FK_Company,
                StNetAmount=data.SuppNetAmount,


                OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),

                SupplierPaymentDetails=data.SupplierPaymentDetails is null ? "": Common.xmlTostring(data.SupplierPaymentDetails),
                TotalAmount=data.TotalAmount,
                TotalDiscount=data.TotalDiscount,
                FK_PaymentMethod=data.FK_PaymentMethod


            }, companyKey: _userLoginInfo.CompanyKey);
            

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPayemntMethod(SupplierSettlementModel.DropDownListModelView input)
        {
            var PayMethod = "";
            if (input.ddlimportfrom == 1)
                PayMethod = "AND PMMode<>2";
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 " + PayMethod + "  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
 );

            return Json(PaymentView, JsonRequestBehavior.AllowGet);
        }
     

        public ActionResult GetSupplierPaymentdataFill(SupplierSettlementModel.PaymentdatafillbySuppID Data)

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

            SupplierSettlementModel supplierSettlement = new SupplierSettlementModel();

            var datresponse = supplierSettlement.FillSupplierpaymentdata(input: new SupplierSettlementModel.PaymentdatafillbySuppID
            {
               FK_Supplier =Data.FK_Supplier,
               FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
               FK_Company=_userLoginInfo.FK_Company,
               FK_Machine=_userLoginInfo.FK_Machine,
               EntrBy=_userLoginInfo.EntrBy,
               PayAmount = Data.PayAmount

            }, companyKey: _userLoginInfo.CompanyKey);

         
           

            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSupplierSettlemetList(int pageSize, int pageIndex, string Name, string TransModes)
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
           SupplierSettlementModel purchaseOrder = new SupplierSettlementModel();
           
            var SuppSettleInfo = purchaseOrder.GetSupplierSettlementData(companyKey: _userLoginInfo.CompanyKey, input: new SupplierSettlementModel.SupplierSettlementViewID

            {
                GroupID = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detailed=0,
                TransMode = TransModes

            });

           
            return Json(new { SuppSettleInfo.Process, SuppSettleInfo.Data, pageSize, pageIndex, totalrecord = (SuppSettleInfo.Data is null) ? 0 : SuppSettleInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSupplierSettlementInfo(SupplierSettlementModel.ViewInfo data)
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

            SupplierSettlementModel SuppSettlModel = new SupplierSettlementModel();

            var mptableInfo = SuppSettlModel.GetSupplierSettlementData(companyKey: _userLoginInfo.CompanyKey, input: new SupplierSettlementModel.SupplierSettlementViewID
            {
                GroupID = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed=1,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
            });
           

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);



            var OtherCharge = SuppSettlModel.GetOthrChargeDetails(input: new SupplierSettlementModel.Getotherchargelist
            {
                InvoiceType = 0,
                FK_Master = (mptableInfo != null)?mptableInfo.Data[0].SupplierSettlementID:0/*data.GroupID*/,
               // TransMode=data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,


            }, companyKey: _userLoginInfo.CompanyKey);
          
            var paymentdetail = SuppSettlModel.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new SupplierSettlementModel.GetPaymentin
            {
                FK_Master = mptableInfo.Data[0].SupplierSettlementID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            return Json( new { mptableInfo, OtherCharge, paymentdetail/*, Transtypelist,*/ }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSupplierSettlement(SupplierSettlementModel.SupplierSettlementInfoView data)
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

            SupplierSettlementModel suppsetl = new SupplierSettlementModel();

            var datresponse = suppsetl.DeleteSupplierSettlementData(input: new SupplierSettlementModel.DeleteSupplierSettlement
            {
                EntrBy = _userLoginInfo.EntrBy,
                GroupID = data.GroupID,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSupplierSettlementReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = ""});


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
    }
}