//----------------------------------------------------------------------
//    Created By    : Kavya K
//    Created On    : 30/01/2023
//    Purpose	    : AMC Warranty Mapping
//    ------------------------------------------------------------------------- 
//    Modification On
//    By
//    -------------------------------------------------------------------------

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
    public class AMCWarrantyMappingController : Controller
    {
        // GET: AMCWarrantyMapping
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();
        }

        public ActionResult AMCWarrantyMapping(string mgrp, string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            string mTd = objCmnMethod.DecryptString(mtd);
            TempData["mTd"] = mTd.ToString();
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadFormAMCWarranty(string mtd)
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

            AMCWarrantyMappingModel.DropDownListModel AMCWT = new AMCWarrantyMappingModel.DropDownListModel();

            var Warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
        );
            AMCWT.Warrantytype = Warrantytype.Data;


            var AMCName = Common.GetDataViaQuery<AMCWarrantyMappingModel.AMCType>(parameters: new APIParameters
            {
                TableName = "AMCType AT",
                SelectFields = "AT.ID_AMCType AS AMCTypeID,AT.AMCName AS[AMCName]",
                Criteria = "AT.Cancelled=0 AND AT.Passed=1 AND AT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
          );

            AMCWT.AMCType = AMCName.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
        );
            AMCWT.PaymentView = PaymentView.Data;
            ViewBag.PageTitle = TempData["mTd"] as string;
            CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.DecryptString(mgrp);
            //ViewBag.TransMode = mGrp;
            string mTd = objCmnMethod.DecryptString(mtd);
            TempData["mTd"] = mTd.ToString();
            return PartialView("_AddAMCWarrantyMapping", AMCWT);
        }

        public ActionResult AMCWarrantyFillInvoiceCustomer(long FK_Customer, string TransMode, Int16 Mode)
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
            AMCWarrantyMappingModel objAMC = new AMCWarrantyMappingModel();

            var CusData = objAMC.GetAMCWarrantyInvoiceCustomer(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetAMCWarrantyCustomer
            {
                Mode = Mode,
                FK_Customer = FK_Customer,
                FK_Company = _userLoginInfo.FK_Company,
                TransMode = TransMode
            });


            return Json(CusData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AMCWarrantyFillAMCData(AMCWarrantyMappingModel.AMCDetailsView AMCData)
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
            AMCWarrantyMappingModel objAMC = new AMCWarrantyMappingModel();

            var AMCDetails = objAMC.GetAMCWarrantyAMCDetails(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetAMCDetails
            {
                Mode = AMCData.Mode,
                FK_Type = AMCData.FK_AMCType,
                Date = AMCData.EffectDate,
                ProductAmount = AMCData.AmcAmount,
                Quantity = AMCData.Quantity
            });


            return Json(AMCDetails, JsonRequestBehavior.AllowGet);

        }


        public ActionResult AMCWarrantyFillingAMCData(AMCWarrantyMappingModel.AMCDetailsView AMCData)
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
            AMCWarrantyMappingModel objAMC = new AMCWarrantyMappingModel();

            var AMCDetails = objAMC.GetAMCWarrantyAMCDetails(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetAMCDetails
            {
                Mode = AMCData.Mode,
                FK_Type = AMCData.FK_AMCType,
                Date = AMCData.EffectDate,
                ProductAmount = AMCData.AmcAmount,
                Quantity = AMCData.Quantity
            });


            return Json(AMCDetails, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AMCWarrantyFillWarrantyData(AMCWarrantyMappingModel.WarrantyDetailsView WarrantyData)
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
            AMCWarrantyMappingModel objAMC = new AMCWarrantyMappingModel();

            var WarrantyDetails = objAMC.GetAMCWarrantyWarrantyDetails(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetWarDetails
            {
                Mode = WarrantyData.Mode,
                FK_Type = WarrantyData.FK_WarrantyType,
                Date = WarrantyData.EffectDate,
                ProductAmount = WarrantyData.Amount,
                Quantity = WarrantyData.Quantity
            });


            return Json(WarrantyDetails, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddAMCDetails(AMCWarrantyMappingModel.AMCDetailsViewData data)
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


            AMCWarrantyMappingModel AMCdata = new AMCWarrantyMappingModel();
            List<AMCWarrantyMappingModel.WarrantyDetails> lst = new List<AMCWarrantyMappingModel.WarrantyDetails>();
            if (data.AMCMode==0)            {
              
                if (data.WarrantyDetails.Count > 0)
                {
                    foreach (var tem in data.WarrantyDetails)
                    {
                        AMCWarrantyMappingModel.WarrantyDetails obj = tem;
                        obj.FK_Master = data.FK_Master;
                        lst.Add(obj);
                    }

                }
            }
         

            var datresponse = AMCdata.UpdateAMCData(input: new AMCWarrantyMappingModel.UpdateAMCDetails
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                ID_AMCDetails = 0,
                FK_Customer = data.FK_Customer,
                EffectDate = data.EffectDate,
                TransDate = data.TransDate,
                AMCDetails = data.AMCDetails is null ? "" : Common.xmlTostring(data.AMCDetails),
                PaymentDetails = data.PaymentDetails is null ? "" : Common.xmlTostring(data.PaymentDetails),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(lst),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AMCDetails = 0,
                AMCMode=data.AMCMode,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateAMCDetails(AMCWarrantyMappingModel.AMCDetailsViewData data)
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


            AMCWarrantyMappingModel AMCdata = new AMCWarrantyMappingModel();

            var datresponse = AMCdata.UpdateAMCData(input: new AMCWarrantyMappingModel.UpdateAMCDetails
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_AMCDetails = data.ID_AMCDetails,
                FK_Customer = data.FK_Customer,
                EffectDate = data.EffectDate,
                TransDate = data.TransDate,
                AMCDetails = data.AMCDetails is null ? "" : Common.xmlTostring(data.AMCDetails),
                PaymentDetails = data.PaymentDetails is null ? "" : Common.xmlTostring(data.PaymentDetails),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AMCDetails = data.ID_AMCDetails,
                AMCMode = data.AMCMode,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAMCWarrantyList(int pageSize, int pageIndex, string Name, int Details, string TransMode)
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


            AMCWarrantyMappingModel AMCWarrantyData = new AMCWarrantyMappingModel();

            var data = AMCWarrantyData.GetAMCWarrantyDetails(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetAMCWarrantyData
            {
                TransMode = TransMode,
                FK_AMCDetails = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Details = Details,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                EntrBy = _userLoginInfo.EntrBy
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAMCWarrantyReasonList()
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


        public ActionResult DeleteAMCWarranty(AMCWarrantyMappingModel.DeleteAMCWarranty data)

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

        AMCWarrantyMappingModel AMCWarrantydelete = new AMCWarrantyMappingModel();


            var datresponse = AMCWarrantydelete.DeleteAMCWarrantyData(input: new AMCWarrantyMappingModel.DeleteAMCWarranty
        {
            TransMode = data.TransMode,
            FK_AMCDetails = data.FK_AMCDetails,
            Debug = 0,
            EntrBy = _userLoginInfo.EntrBy,
            FK_Reason = data.FK_Reason,
            FK_Company = _userLoginInfo.FK_Company,
            FK_Machine = _userLoginInfo.FK_Machine,
            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            Mode = data.Mode
        },
             companyKey: _userLoginInfo.CompanyKey);



            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAMCWarrantyInfo(AMCWarrantyMappingModel.AMCWarrantyViewList data)
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

            AMCWarrantyMappingModel amcwarrantydata = new AMCWarrantyMappingModel();
            var amcInfo = amcwarrantydata.GetAMCDetailedData(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetAMCWarrantyData
            {
                TransMode = data.TransMode,
                FK_AMCDetails = data.ID_AMCDetails,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Details = 0,
                PageIndex = data.PageIndex + 1,
                PageSize = data.PageSize,
                Name = data.Name,
                EntrBy = _userLoginInfo.EntrBy,
            });

            var warrantyInfo = amcwarrantydata.GetWarrantyDetailedData(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetWarrantyData
            {
                TransMode = data.TransMode,
                FK_AMCDetails = data.ID_AMCDetails,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Details = 1,
                PageIndex = data.PageIndex + 1,
                PageSize = data.PageSize,
                Name = data.Name,
                EntrBy = _userLoginInfo.EntrBy,
            });

            var OtherCharge = amcwarrantydata.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetSubTable
            {
                FK_Transaction = data.ID_AMCDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode });

            var paymentdetail = amcwarrantydata.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetPaymentin
            {
                FK_Master = data.ID_AMCDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });

            return Json(new { amcInfo, warrantyInfo , OtherCharge, paymentdetail }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AMCRenewalDetails(long FK_AMCDetails, string TransMode, Int16 Mode, DateTime RenewDate)
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
            AMCWarrantyMappingModel objAMC = new AMCWarrantyMappingModel();

            var CusData = objAMC.GetRenewalDetail(companyKey: _userLoginInfo.CompanyKey, input: new AMCWarrantyMappingModel.GetRenwalData
            {
                Mode = Mode,
                FK_AMCDetails = FK_AMCDetails,
                FK_Company = _userLoginInfo.FK_Company,
                TransMode = TransMode,
                RenewDate = RenewDate
            });


            return Json(CusData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddRenewalDetails(AMCWarrantyMappingModel.RenewalDetailsViewData data)
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


            AMCWarrantyMappingModel AMCdata = new AMCWarrantyMappingModel();
          


            var datresponse = AMCdata.UpdateRenewalData(input: new AMCWarrantyMappingModel.UpdateRenewalDetails
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                ID_AMCTransaction = 0,
                FK_AMCDetails = data.FK_AMCDetails,
                EffectDate = data.EffectDate,
                TransDate = data.TransDate,
                AMCMode = data.AMCMode,

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AMCTransaction = 0,
          
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}