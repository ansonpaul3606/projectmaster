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
    [CheckSessionTimeOut]
    public class FinancePlanTypeController : Controller
    {
        // GET: FinancePlanType
        
            public ActionResult FinancePlanType()
            {
                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                UserAcssRightInfo pageAccess = new UserAcssRightInfo();
                pageAccess = _userLoginInfo.PageAccessRights;
                ViewBag.Username = _userLoginInfo.UserName;
                ViewBag.UserRole = _userLoginInfo.UserRole;
                ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
                ViewBag.PagedAccessRights = pageAccess;
                return View();
        }

        public ActionResult LoadFinancePlan()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            FinancePlanTypeModel.FinancePlanList FinanceListname = new FinancePlanTypeModel.FinancePlanList();
            FinancePlanTypeModel objfin = new FinancePlanTypeModel();

            var PerdtypList = objfin.GetPerdTypList(input: new FinancePlanTypeModel.ModeLead { Mode = 3 },
            companyKey: _userLoginInfo.CompanyKey);

            FinanceListname.PerdList = PerdtypList.Data;

            return PartialView("_AddFinancePlanType", FinanceListname);
        }


        [HttpPost]
        public ActionResult AddNewFinancePlan(FinancePlanTypeModel.FinancePlanList data)
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

            ModelState.Remove("ProductID");

            ModelState.Remove("TotalCount");

            ModelState.Remove("FinancePlanTypeID");
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

            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            FinancePlanTypeModel FinancePlan = new FinancePlanTypeModel();
            var datresponse = FinancePlan.UpdateFinancePlanData(input: new FinancePlanTypeModel.FinancePlanUpdate
            {
                UserAction = userAction,
                Debug = 0,
                ID_FinancePlanType = 0,
                TransMode = data.TransMode,
                FpName = data.FinancePlanName,
                FpShortName = data.FinancePlanShortName,
                FpPeriodType = data.FinancePeriodType,
                FpPeriod = data.FinanceInstallmentPeriod,
                FpDuration = data.FinanceDuration,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = FK_Machine

            }, companyKey: _userLoginInfo.CompanyKey);

           
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            
        }


        [HttpPost]
        public ActionResult GetFinancePlanList(int pageSize, int pageIndex, string Name,string TransMode)
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

        
            FinancePlanTypeModel financeplanload = new FinancePlanTypeModel();

            var data = financeplanload.GetFinancePlanData(companyKey: _userLoginInfo.CompanyKey, input: new FinancePlanTypeModel.ID
            {
                FK_FinancePlanType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = TransMode,
                Name=Name,


            });

            //return Json(new { data.Process, data.Data, pageSize, pageIndex }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFinancePlanInfo(FinancePlanTypeModel.ID data)
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


            //ModelState.Remove("FinancePlanShortName");
            //ModelState.Remove("FinancePlanName");
            //ModelState.Remove("ProductID");
            //ModelState.Remove("FinancePeriodType");
            //ModelState.Remove("FinanceInstallmentPeriod");
            //ModelState.Remove("FinanceDuration");
            //ModelState.Remove("TotalCount");



            #region :: Model validation  ::

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

            FinancePlanTypeModel financedata = new FinancePlanTypeModel();
            var financeInfo = financedata.GetFinancePlanData(companyKey: _userLoginInfo.CompanyKey, input: new FinancePlanTypeModel.ID { FK_FinancePlanType = data.FK_FinancePlanType, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(financeInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateFinancePlanDetails(FinancePlanTypeModel.FinancePlanList data)
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

            //ModelState.Remove("ProductID");
            ModelState.Remove("TotalCount");

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

            FinancePlanTypeModel financePlandet = new FinancePlanTypeModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            // int backupId = 0;
           
                var dataresponse = financePlandet.UpdateFinancePlanData(input: new FinancePlanTypeModel.FinancePlanUpdate
                {
                    UserAction = userAction,
                    FK_Machine = FK_Machine,
                    FK_BranchCodeUser = branchUserCode,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = entrBy,
                    TransMode = data.TransMode,
                    Debug = 0,
                    ID_FinancePlanType = data.FinancePlanID,
                    FpName = data.FinancePlanName,
                    FpShortName = data.FinancePlanShortName,
                    FpPeriodType = data.FinancePeriodType,
                    FpPeriod = data.FinanceInstallmentPeriod,
                    FpDuration = data.FinanceDuration
                }, companyKey: _userLoginInfo.CompanyKey);            

                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);

               
            
        }


        public ActionResult GetFinancePlanReasonList()
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
        public ActionResult DeleteFinancePlanInfo(FinancePlanTypeModel.FinancePlanDelete data)

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

            ModelState.Remove("FinancePlanShortName");
            ModelState.Remove("FinancePlanName");
            ModelState.Remove("ProductID");
            ModelState.Remove("FinancePeriodType");
            ModelState.Remove("FinanceInstallmentPeriod");
            ModelState.Remove("FinanceDuration");
            ModelState.Remove("TotalCount");

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


            FinancePlanTypeModel.FinancePlanDelete financeplandelte = new FinancePlanTypeModel.FinancePlanDelete
            {

                TransMode = "",
                FK_FinancePlanType = data.FK_FinancePlanType,
                Debug = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = data.FK_Reason,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            };

            Output dataresponse = Common.UpdateTableData<FinancePlanTypeModel.FinancePlanDelete>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProFinancePlanTypeDelete", parameter: financeplandelte);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductName(FinancePlanTypeModel.FinanceModelView data)
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

            FinancePlanTypeModel view = new FinancePlanTypeModel();



            var datresponse = Common.GetDataViaProcedure<FinancePlanTypeModel.FinanceModelView, FinancePlanTypeModel.FinancePlanList>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProFinancePlanTypeSelect",
               parameter: new FinancePlanTypeModel.FinancePlanList
               {
                   ProductID = data.ProductID
               });

            // return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

    }
}
