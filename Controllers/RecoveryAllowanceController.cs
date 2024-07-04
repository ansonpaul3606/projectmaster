using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class RecoveryAllowanceController : Controller
    {
        // GET: RecoveryAllowance
        public ActionResult Index()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadRecoveryAllowanceForm()
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

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;

            ViewBag.PagedAccessRights = pageAccess;
            RecoveryAllowanceModel.RecoveryAllowanceView prdListObj = new RecoveryAllowanceModel.RecoveryAllowanceView();
            //var Customertype = Common.GetDataViaQuery<RecoveryAllowanceModel.TypeList>(parameters: new APIParameters

            //{
            //    TableName = "CustomerType",
            //    SelectFields = "ID_CustomerType AS FK_Master,CustyName as Customertype",
            //    Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //       companyKey: _userLoginInfo.CompanyKey
            //       );
            //prdListObj.TypeList = Customertype.Data;

            return PartialView("_AddRecoveryAllowance", prdListObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetALType(int Mode)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            RecoveryAllowanceModel.RecoveryAllowanceView list = new RecoveryAllowanceModel.RecoveryAllowanceView();
            var ChangeMod = Common.GetDataViaProcedure<RecoveryAllowanceModel.ModeList, RecoveryAllowanceModel.ModeSelect>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new RecoveryAllowanceModel.ModeSelect { Mode = Mode});
          
            list.ModeList = ChangeMod.Data;
            return Json(ChangeMod.Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewRecoveryAllowance(RecoveryAllowanceModel.RecoveryAllowanceView data)
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

            //ModelState.Remove("ID_PriceFixingType");


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


            RecoveryAllowanceModel objprdwise = new RecoveryAllowanceModel();


            byte userAction = 1;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateRecoveryAllowanceData(input: new RecoveryAllowanceModel.RecoveryAllowanceUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_AllowanceType = 0,
                ALWName = data.ALWName,
                ALWShortName = data.ALWShortName,
                ALWMode = data.ALWMode,
                ALWType = data.ALWType,
                FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,

                FK_AccountHeadSub = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecoveryAllowanceInfo(RecoveryAllowanceModel.RecoveryAllowance data)
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

            RecoveryAllowanceModel RecoveryAllowance = new RecoveryAllowanceModel();
            var recoveryallowanceInfo = RecoveryAllowance.GetRecoveryAllowanceData(companyKey: _userLoginInfo.CompanyKey, input: new RecoveryAllowanceModel.RecoveryAllowance { FK_AllowanceType = data.FK_AllowanceType, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, EntrBy = _userLoginInfo.EntrBy });

            return Json(recoveryallowanceInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRecoveryAllowanceDeleteReasonList()
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

        public ActionResult GetRecoveryAllowanceList(int pageSize, int pageIndex, string Name)
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



            //string transMode = "";
            RecoveryAllowanceModel RecoveryAllowance = new RecoveryAllowanceModel();

            var data = RecoveryAllowance.GetRecoveryAllowanceData(companyKey: _userLoginInfo.CompanyKey, input: new RecoveryAllowanceModel.RecoveryAllowance
            {
                FK_AllowanceType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //TransMode = transMode
            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DeleteRecoveryAllowanceInfo(RecoveryAllowanceModel.RecoveryAllowanceView data)

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

            ModelState.Remove("ALWName");
            ModelState.Remove("ALWShortName");


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


            RecoveryAllowanceModel.DeleteRecoveryAllowance RecoveryAllowance = new RecoveryAllowanceModel.DeleteRecoveryAllowance
            {
                FK_AllowanceType = data.FK_AllowanceType,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""

            };

            Output dataresponse = Common.UpdateTableData<RecoveryAllowanceModel.DeleteRecoveryAllowance>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProAllowanceTypeDelete", parameter: RecoveryAllowance);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountHeadDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<RecoveryAllowanceModel.AccountHeadView>(parameters: new APIParameters
            {
                TableName = "AccountHead AH",
                SelectFields = "AH.ID_AccountHead AS AccountHead,AH.AHCode AS AccountCode,AH.AHName AS AHeadName",
                Criteria = "AH.Cancelled =0 AND AH.Passed=1",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAccountSubHeadDetails(int AccountCode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<RecoveryAllowanceModel.AccountSubHeadView>(parameters: new APIParameters
            {
                TableName = "AccountSubHead ASH",
                SelectFields = "ASH.ID_AccountSubHead AS AccountHeadSub,ASH.AHCode AS AccountSHCode,ASH.ASHName AS ASHeadName",
                Criteria = "ASH.Cancelled =0 AND ASH.Passed=1 AND ASH.AHCode=" + AccountCode,
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateRecoveryAllowance(RecoveryAllowanceModel.RecoveryAllowanceView data)
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

            ModelState.Remove("ID_AllowanceType");


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


            RecoveryAllowanceModel objprdwise = new RecoveryAllowanceModel();


            byte userAction = 2;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;

            var dataresponse = objprdwise.UpdateRecoveryAllowanceData(input: new RecoveryAllowanceModel.RecoveryAllowanceUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_AllowanceType = data.ID_AllowanceType,
                ALWName = data.ALWName,
                ALWShortName = data.ALWShortName,
                ALWMode = data.ALWMode,
                ALWType = data.ALWType,
               
                FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,
              
                FK_AccountHeadSub = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,



            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



    }
}