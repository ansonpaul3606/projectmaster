using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ServiceBillDetailsController : Controller
    {
        // GET: ServiceBillDetails
        public ActionResult Index(string mgrp)
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
            return View();
        }

        public ActionResult LoadServicebill()
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
            ServiceBillDetailsModel.Servicebillview serbill = new ServiceBillDetailsModel.Servicebillview();

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=3 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );
            serbill.BillTypeListView = BillTypeListView.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),

                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );
            serbill.PaymentView = PaymentView.Data;


            var BankList = Common.GetDataViaQuery<ServiceBillDetailsModel.BankList>(parameters: new APIParameters
            {
                TableName = "Bank",
                SelectFields = "ID_Bank AS BankID,CONCAT(BankName,'/',BranchName) AS BankName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Branch=" + _userLoginInfo.FK_Branch,
                SortFields = "",
                GroupByFileds = ""
            },
                   companyKey: _userLoginInfo.CompanyKey
                   
                   );
            serbill.BankList = BankList.Data;

            return PartialView("_AddServicebillDetails",serbill);
        }


        public ActionResult GetOtherCharges(ServiceBillDetailsModel.BindOtherCharge Data)
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
                Criteria = "ID_TransType=2",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetServiceandProduct(ServiceBillDetailsModel.ServiceProductViewInput inp)
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

            ServiceBillDetailsModel serv = new ServiceBillDetailsModel();

            var data = serv.GetDetailsServiceProduct(input: new ServiceBillDetailsModel.ServiceProductViewInput
            {
                Mode = 1,
                FK_TransactionID = inp.FK_TransactionID,
                EntrBy = _userLoginInfo.EntrBy

            }, companyKey: _userLoginInfo.CompanyKey);

            var dataPro = serv.GetDetailsServiceProduct(input: new ServiceBillDetailsModel.ServiceProductViewInput
            {
                Mode=2,
                FK_TransactionID =inp.FK_TransactionID,
                EntrBy = _userLoginInfo.EntrBy
            }, companyKey: _userLoginInfo.CompanyKey);
 
            return Json(new { data, dataPro }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult AddServicebill(ServiceBillDetailsModel.ServicebillProductview data)
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

            ServiceBillDetailsModel model = new ServiceBillDetailsModel();

            var dtresponse = model.UpdateService(input: new ServiceBillDetailsModel.UpdateServicebill
            {
                UserAction = 1,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BillType = data.BillType,
                SerBillDate = data.TransDate,
                SerEffectDate = data.EffectDate,
                FK_CustomerServiceRegister = data.TicketNo,
                SerCustomerName = data.CustomerName,
                SerBillTotal = data.SalBillTotal,
                SerProductBillTotal = data.SalProductBillTotal,
                SerDiscount = data.SalDiscount,
                SerOtherCharge = data.OtherCharge,
                SerNetAmount = data.SalNetAmount,
                SerRoundoff = data.SalRoundoff,
                Servicelist = data.Servicedetails is null ? "" : Common.xmlTostring(data.Servicedetails),
                Productlist = data.Productdetails is null ? "" : Common.xmlTostring(data.Productdetails),
                OtherChDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                Debug = 0,
                FK_ServiceBill = 0,
                BalCusAmount = data.BalCusAmount,
                FK_Bank = (data.BankID.HasValue) ? data.BankID.Value : 0,
                SecurityAmount = data.SecurityAmount,
                AdvancedAmount=data.AdvancedAmount


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new{ Process = dtresponse}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetServicebillList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ServiceBillDetailsModel model = new ServiceBillDetailsModel();

            var data = model.GetbillData(input: new ServiceBillDetailsModel.GetServicebilldetails
            {
                TransMode = TransMode,
                FK_ServiceBill = 0,
                FK_Company =  _userLoginInfo.FK_Company,
                BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            },companyKey:_userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetServiceReasonList()
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

        public ActionResult DeleteService(ServiceBillDetailsModel.ServiceInputDel data)
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

            ServiceBillDetailsModel ser = new ServiceBillDetailsModel();


            var datresponse = ser.DeleteServiceData(input: new ServiceBillDetailsModel.DeleteService
            {
                TransMode = data.TransMode,
                FK_ServiceBill = data.ID_ServiceBill,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServiceInfoByID(ServiceBillDetailsModel.Getbillinput bill)
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

            ServiceBillDetailsModel model = new ServiceBillDetailsModel();

            var Servicelist = model.GetDetailsServiceProduct(input: new ServiceBillDetailsModel.ServiceProductViewInput
            {
                Mode = 1,
                FK_TransactionID = bill.FK_CustomerServiceRegister,
                EntrBy = _userLoginInfo.EntrBy

            }, companyKey: _userLoginInfo.CompanyKey);

            var Productlist = model.GetDetailsServiceProduct(input: new ServiceBillDetailsModel.ServiceProductViewInput
            {
                Mode = 2,
                FK_TransactionID = bill.FK_CustomerServiceRegister,
                EntrBy = _userLoginInfo.EntrBy
            }, companyKey: _userLoginInfo.CompanyKey);

            var OtherCharge = model.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new ServiceBillDetailsModel.GetSubTableSales
            {
                FK_Transaction = bill.ID_ServiceBill,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = bill.TransMode
            });

            var paymentdetail = model.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new ServiceBillDetailsModel.GetPaymentin
            {
                FK_Master = bill.ID_ServiceBill,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = bill.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            var Amoutndetails = model.GetServiceDetails(input: new ServiceBillDetailsModel.ServiceProductViewInput
            {
                Mode = 3,
                FK_TransactionID = bill.FK_CustomerServiceRegister,
                EntrBy = _userLoginInfo.EntrBy

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { OtherCharge, paymentdetail, Productlist, Servicelist , Amoutndetails }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public ActionResult GetServiceDetails(ServiceBillDetailsModel.ServiceProductViewInput inp)
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

            ServiceBillDetailsModel serv = new ServiceBillDetailsModel();

            var data = serv.GetServiceDetails(input: new ServiceBillDetailsModel.ServiceProductViewInput
            {
                Mode = 3,
                FK_TransactionID = inp.FK_TransactionID,
                EntrBy = _userLoginInfo.EntrBy

            }, companyKey: _userLoginInfo.CompanyKey);

            

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}