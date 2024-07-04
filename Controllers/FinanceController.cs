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
    public class FinanceController : Controller
    {
        // GET: Finance
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadFinancePlan()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            FinanceModel.FinancePlanList FinanceList = new FinanceModel.FinancePlanList();
            var FinancePlan = Common.GetDataViaQuery<FinanceModel.FinancePlanType>(parameters: new APIParameters
            {
                TableName = "FinancePlanType FPT",
                SelectFields = "FPT.ID_FinancePlanType AS FinancePlanTypeID,FPT.FpName AS[FinanceName]",
                Criteria = "FPT.Cancelled=0 AND FPT.Passed=1 AND FPT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
          );

            FinanceList.FinanceNameList = FinancePlan.Data;
            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<FinanceModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            FinanceList.CategoryList = Category.Data;
            return PartialView("_AddFinance", FinanceList);
        }
      
              public ActionResult GetPlanDetails(string FinancePlanName)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            FinanceModel custinfo = new FinanceModel();

            var data = Common.GetDataViaQuery<FinanceModel.FinancePlanDetails>(parameters: new APIParameters
            {
                TableName = "FinancePlanType",
                SelectFields = "ID_FinancePlanType as FinancePlanTypeID, FpName as FinancePlanName,FpDuration FinanceDuration,FpPeriod FinanceInstallmentPeriod,CASE WHEN FpPeriodType=1 THEN 'Day' WHEN FpPeriodType=2 THEN 'Week'  WHEN FpPeriodType= 3 THEN 'Month' WHEN FpPeriodType=4 THEN 'Year' else '' END FinancePeriodType ",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "  AND FpName='" + FinancePlanName + "'",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult SaveFinancePlan(FinanceModel.UpdateFinancePlanTypeSettings data)
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

          //  ModelState.Remove("ProductID");

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

            FinanceModel FinancePlan = new FinanceModel();
            var datresponse = FinancePlan.UpdateFinancePlanTypeSettingsData(input: new FinanceModel.UpdateFinancePlanTypeSettings
            {
                UserAction = userAction,
                Debug = 0,
                TransMode = data.TransMode,
                ID_FinancePlanTypeSettings = 0,
                FK_FinancePlanType = data.FK_FinancePlanType,
                EffectDate =data.EffectDate,
                FpstAddlCostMethod=data.FpstAddlCostMethod,
                FpstAddlCostValue=data.FpstAddlCostValue,
                FpstDownPayMethod=data.FpstDownPayMethod,
                FpstDownPayValue=data.FpstDownPayValue,
                FpstFineMethod=data.FpstFineMethod,
                FpstFinevalue=data.FpstFinevalue,
                FpstFineCalcMethod=data.FpstFineCalcMethod,
                FpstFineCalcvalue=data.FpstFineCalcvalue,
                FpstFineGracePrd = data.FpstFineGracePrd,
                FpstFineGracePrdvalue=data.FpstFineGracePrdvalue,
                FK_AccountHead = (data.FK_AccountHead.HasValue) ? data.FK_AccountHead.Value : 0,
                FK_AccountHeadAddAmount = (data.FK_AccountHeadAddAmount.HasValue) ? data.FK_AccountHeadAddAmount.Value : 0,
                FK_Product= (data.FK_Product.HasValue) ? data.FK_Product.Value : 0,
                FK_Category = (data.FK_Category.HasValue) ? data.FK_Category.Value : 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = FK_Machine,
                FK_FinancePlanTypeSettings=0,
                FK_AccountHeadFine =(data.FK_AccountHeadFine.HasValue) ? data.FK_AccountHeadFine.Value : 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }


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

            FinanceModel financeplanload = new FinanceModel();

            var data = financeplanload.GetFinancePlanData(companyKey: _userLoginInfo.CompanyKey, input: new FinanceModel.FinancePlanView
            {
                FK_FinancePlanTypeSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = TransMode,               
               
            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFinancePlanInfo(FinanceModel.FinanceViewList data)
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

            FinanceModel financedata = new FinanceModel();
            var financeInfo = financedata.GetFinancePlanData(companyKey: _userLoginInfo.CompanyKey, input: new FinanceModel.FinancePlanView { FK_FinancePlanTypeSettings = data.FK_FinancePlanTypeSettings, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });
            
            return Json(new {financeInfo}, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteFinancePlanInfo(FinanceModel.DeleteFinancePlanTypeSettings data)

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


            FinanceModel.DeleteFinancePlanTypeSettings financeplandelte = new FinanceModel.DeleteFinancePlanTypeSettings
            {

                TransMode = "",
                FK_FinancePlanTypeSettings = data.FK_FinancePlanTypeSettings,
                Debug = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = data.FK_Reason,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            };

            Output dataresponse = Common.UpdateTableData<FinanceModel.DeleteFinancePlanTypeSettings>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProFinancePlanTypeSettingsDelete", parameter: financeplandelte);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateFinancePlanDetails(FinanceModel.FinanceViewList data)
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

            FinanceModel financePlandet = new FinanceModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            // int backupId = 0;

            var datresponse = financePlandet.UpdateFinancePlanTypeSettingsData(input: new FinanceModel.UpdateFinancePlanTypeSettings
            {
                UserAction = userAction,
                Debug = 0,
                TransMode = data.TransMode,
                ID_FinancePlanTypeSettings =data.FK_FinancePlanTypeSettings,
                EffectDate = data.EffectDate,      
                FK_FinancePlanType =data.FinancePlanType,
                FpstAddlCostMethod = data.FinanceAddCostMethod,
                FpstAddlCostValue = data.FinanceAddCostValue,
                FpstDownPayMethod = data.FinanceDownPayMethod,
                FpstDownPayValue = data.FinanceDownPayValue,
                FpstFineMethod = data.FinanceFineMethod,
                FpstFinevalue = data.FinanceFineValue,
                FpstFineCalcMethod = data.FinanceFineCalcMethod,
                FpstFineCalcvalue = data.FinanceFineCalcValue,
                FpstFineGracePrd = data.FinanceFineGracePeriod,
                FpstFineGracePrdvalue = data.FinanceFineGracePeriodValue,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = FK_Machine,
                FK_FinancePlanTypeSettings = data.FK_FinancePlanTypeSettings,
                FK_Product = (data.FK_Product.HasValue) ? data.FK_Product.Value : 0,
                FK_Category = (data.FK_Category.HasValue) ? data.FK_Category.Value : 0,
                FK_AccountHeadFine = data.FK_AccountHeadFine
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);



        }

    }
}





