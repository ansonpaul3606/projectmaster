/*----------------------------------------------------------------------
Created By	: NIJI TJ
Created On	: 23/11/2023
Purpose		: EMIPlan
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
    public class EMIPlanController : Controller
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
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadFormEMIPlan(string mtd)
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

            EMIPlanModel.EMIPlanView EMIPlan = new EMIPlanModel.EMIPlanView();

            EMIPlan.EMIPlanList = new List<EMIPlanModel.EMIPlan>();

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddEMIPlan", EMIPlan);
        }

        [HttpGet]
        public ActionResult GetEMIPlanList(long FK_Category)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var EMIPlanList = Common.GetDataViaQuery<EMIPlanModel.EMIPlan>(parameters: new APIParameters
            {
                TableName = "FinancePlanTypeSettings FS JOIN FinancePlanType FT ON FS.FK_FinancePlanType = FT.ID_FinancePlanType",
                SelectFields = "FT.ID_FinancePlanType,FT.FpName",
                Criteria = "FS.Cancelled = 0 AND FS.Passed =1 AND  FS.FK_Category =  " + FK_Category,
                GroupByFileds = "FT.ID_FinancePlanType,FT.FpName",
                SortFields = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(EMIPlanList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetEMIPlanDetails(EMIPlanModel.ProjectEMIInput data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            EMIPlanModel Emi = new EMIPlanModel();
            var EMIPlanDetails = Emi.FillProjectEMI(input: data, companyKey: _userLoginInfo.CompanyKey);
            return Json(EMIPlanDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProjectEMIPlanList(int pageSize, int pageIndex, string Name, string TransMode)
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



            EMIPlanModel Emi = new EMIPlanModel();
            var data = Emi.GetEMIPlanData(input: new EMIPlanModel.ProjectEMIPlanID
            {

                FK_CustomerWiseEMI = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                Detailed = 0
            }
            , companyKey: _userLoginInfo.CompanyKey);

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GetEMIPlanInfoByID(EMIPlanModel.EMIPlanView data)
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

            EMIPlanModel EMIMdl = new EMIPlanModel();
            var EMIPlans  = EMIMdl.GetEMIPlanData(companyKey: _userLoginInfo.CompanyKey, input: new EMIPlanModel.ProjectEMIPlanID { FK_CustomerWiseEMI = data.ID_CustomerWiseEMI, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode });


            var EMIPlansDetails = EMIMdl.GetEMIPlanSlabDetails(companyKey: _userLoginInfo.CompanyKey, input: new EMIPlanModel.ProjectEMIPlanID { FK_CustomerWiseEMI = data.ID_CustomerWiseEMI, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, Detailed = 1 });


            return Json(new { EMIPlans, EMIPlansDetails }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GetEMISlabDetails(EMIPlanModel.ProjectEMISlabInput data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            EMIPlanModel Emi = new EMIPlanModel();
            var EMISlabDetails = Emi.GetEMISlabDetails(input: data, companyKey: _userLoginInfo.CompanyKey);
            return Json(EMISlabDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewEMIPlan(EMIPlanModel.EMIPlanView data)
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

            ModelState.Remove("EMIPlanID");
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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

            EMIPlanModel EMIPlan = new EMIPlanModel();

            var datresponse = EMIPlan.UpdateEMIPlanData(input: new EMIPlanModel.UpdateEMIPlan
            {
                UserAction = 1,
                TransMode = data.TransMode,
                FK_Master = data.FK_Master,
                FK_FinancePlanType = data.FK_FinancePlanType,
                CedEMINo = data.CedEMINo,
                CedTotalAmount = data.CedTotalAmount,
                CedDownPayment = data.CedDownPayment,
                CedAddAmount = data.CedAddAmount,
                CedInstAmount = Convert.ToDecimal(data.CedInstAmount),
                CedFirstInstDate = data.CedFirstInstDate,
                FK_AssignedTo = data.FK_AssignedTo,             
               
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                EMIDetails = data.EMIPlanDetailsList is null ? "" : Common.xmlTostring(data.EMIPlanDetailsList),

                //ID_EMIPlan = 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult UpdateEMIPlan(EMIPlanModel.EMIPlanView data)
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

        //    EMIPlanModel EMIPlan = new EMIPlanModel();
        //    var datresponse = EMIPlan.UpdateEMIPlanData(input: new EMIPlanModel.UpdateEMIPlan
        //    {
        //        //UserAction = 2, EMIPName = data.EMIPName,
        //        EMIPShortName = data.EMIPShortName,
        //        FK_Product = data.Product,
        //        EMIPPeriodType = data.EMIPPeriodType,
        //        EMIPPeriod = data.EMIPPeriod,
        //        EMIPDuration = data.EMIPDuration,
        //        EMIPExtraCostPercentage = data.EMIPExtraCostPercentage,
        //        EMIPExtraCost = data.EMIPExtraCost,
        //        EMIPDownPaymentPercentage = data.EMIPDownPaymentPercentage,
        //        EMIPDownPayment = data.EMIPDownPayment,
        //        EMIPFinePercentage = data.EMIPFinePercentage,
        //        EMIPFineAmount = data.EMIPFineAmount,
        //        EMIPFineGracePeriod = data.EMIPFineGracePeriod,
        //        EMIPFineCalculationMethod = data.EMIPFineCalculationMethod,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //        EntrBy = _userLoginInfo.EntrBy,
        //        FK_Machine = _userLoginInfo.FK_Machine
        //    }, companyKey: _userLoginInfo.CompanyKey);
        //    return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteEMIPlan(EMIPlanModel.DeleteEMIPlan data)
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

            EMIPlanModel EMIPlan = new EMIPlanModel();

            var datresponse = EMIPlan.DeleteEMIPlanData(input: new EMIPlanModel.DeleteEMIPlan {TransMode=data.TransMode, FK_CustomerWiseEMI = data.FK_CustomerWiseEMI, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.FK_Reason ,FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser}, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetEMIPlanReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID {  FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

    }
}
 
