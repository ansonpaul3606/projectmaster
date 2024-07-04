/*----------------------------------------------------------------------
Created By	: Kavya
Created On	: 17/10/2022
Purpose		: AccountGroup
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

using System.Reflection;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class AccountGroupController : Controller
    {
        public ActionResult AccountGroupIndex(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
           
            ViewBag.mtd = mtd;
           
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }

        public ActionResult LoadFormAccountGroup(string mtd)
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

            AccountGroupModel.AccountGroupView FinacAccountTypeList = new AccountGroupModel.AccountGroupView();
            AccountGroupModel objAccountType = new AccountGroupModel();


            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "AccountGroup",
                FieldName = "SortOrder",
                Debug = 0
            });

            FinacAccountTypeList.SortOrder = a.Data[0].NextNo;

            var FinalAccountType = objAccountType.GetAccountTypeList(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 40 },companyKey: _userLoginInfo.CompanyKey);
            var FinalAccountGroup = objAccountType.GetAccountGroupList(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 41 }, companyKey: _userLoginInfo.CompanyKey);
            var FinalAccountSubGroupType = objAccountType.GetAccountSubGroupList(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 87 }, companyKey: _userLoginInfo.CompanyKey);

            FinacAccountTypeList.FinalAccountType = FinalAccountType.Data;
            FinacAccountTypeList.FinalAccountGroup = FinalAccountGroup.Data;
            FinacAccountTypeList.SubGroupType = FinalAccountSubGroupType.Data;



            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddAccountGroup", FinacAccountTypeList);
        }

        [HttpPost]
        public ActionResult FillAccountGroupType(AccountGroupModel.AccountGroupTypeView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountGroupModel.AccountGroupTypeView AccountGroupTypeList = new AccountGroupModel.AccountGroupTypeView();
            AccountGroupModel objAccountGrp = new AccountGroupModel();

            var AccountGroupType = objAccountGrp.FillAccountGroup(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 41 }, companyKey: _userLoginInfo.CompanyKey);
            object result;
            switch (data.FK_AccType)
            {
                case 2:
                    result = AccountGroupType.Data.Where(a => a.ModeParent == 2).ToList();
                    break;
                case 3:
                    result = AccountGroupType.Data.Where(a => a.ModeParent == 3).ToList();
                    break;
                default:
                    result = AccountGroupType;
                    break;
            }

            
            AccountGroupTypeList.AccountGroupType = data.AccountGroupType;          
             
            return Json(new { AccountGroupType, result }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public ActionResult GetAccountGroupList(int pageSize, int pageIndex, string Name,string Transmode)
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

            string transMode = Transmode;
            ModelState.Remove("ReasonID");
            ModelState.Remove("TotalCount");

            AccountGroupModel AccountGroup = new AccountGroupModel();

            var data = AccountGroup.GetAccountGroupData(companyKey: _userLoginInfo.CompanyKey, input: new AccountGroupModel.GetAccountGroup
            {
                ID_AccountGroup = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_AccountGroup = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            //  return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountGroupInfo(AccountGroupModel.AccountGroupViewData AccountGroupInfo)
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


            AccountGroupModel AccountGroup = new AccountGroupModel();


            var data = AccountGroup.GetAccountGroupData(companyKey: _userLoginInfo.CompanyKey, input: new AccountGroupModel.GetAccountGroup
               {
                ID_AccountGroup = AccountGroupInfo.ID_AccountGroup,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveAccountGroup(AccountGroupModel.AccountGroupViewData data)
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
            ModelState.Remove("AGFinalAccType");
            ModelState.Remove("SortOrder");
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

       

            AccountGroupModel AccountGroup = new AccountGroupModel();

            var datresponse = AccountGroup.UpdateAccountGroupData(input: new AccountGroupModel.UpdateAccountGroup
            {


                AGName = data.AGName,
                AGShortName = data.AGShortName,
                AGFinalAccType = data.AGFinalAccType,
                AGFinalAccGroupType = data.AGFinalAccGroupType,

                SortOrder = data.SortOrder,
                UserAction = 1,
                ID_AccountGroup = 0,
                TransMode = data.TransMode,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AccountGroup=0,
                Trading = data.Trading,
                ProfitLoss = data.ProfitLoss,
                BalanceSheet = data.BalanceSheet,
                AGFinalAccGroupSubType = (data.AGFinalAccGroupSubType.HasValue) ? data.AGFinalAccGroupSubType.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateAccountGroup(AccountGroupModel.AccountGroupViewData data)
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
            ModelState.Remove("AGFinalAccType");
            ModelState.Remove("SortOrder");
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

            AccountGroupModel AccountGroup = new AccountGroupModel();
            var datresponse = AccountGroup.UpdateAccountGroupData(input: new AccountGroupModel.UpdateAccountGroup
            {
                AGName = data.AGName,
                AGShortName = data.AGShortName,
                AGFinalAccType = data.AGFinalAccType,
                AGFinalAccGroupType = data.AGFinalAccGroupType,

                SortOrder = data.SortOrder,
                UserAction = 2,
                ID_AccountGroup = data.ID_AccountGroup,
                TransMode = data.TransMode,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AccountGroup = data.ID_AccountGroup,
                Trading=data.Trading,
                ProfitLoss=data.ProfitLoss,
                BalanceSheet=data.BalanceSheet,
                AGFinalAccGroupSubType = (data.AGFinalAccGroupSubType.HasValue) ? data.AGFinalAccGroupSubType.Value : 0,

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        /*Trading,ProfitLoss,BalanceSheet*/
        [HttpPost]
        [ValidateAntiForgeryToken()]       
        public ActionResult DeleteAccountGroup(AccountGroupModel.AccountGroupInfoView data)
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

            AccountGroupModel AccountGroup = new AccountGroupModel();

            var datresponse = AccountGroup.DeleteAccountGroupData(input: new AccountGroupModel.DeleteAccountGroup
            {

                FK_AccountGroup = data.FK_AccountGroup,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode="",
                Debug=0,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountGroupDeleteReasonList()
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



        [HttpPost]
        public ActionResult FillAccountGroupTypebalancesheet(AccountGroupModel.AccountGroupTypeView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountGroupModel.AccountGroupTypeView AccountGroupTypeList = new AccountGroupModel.AccountGroupTypeView();
            AccountGroupModel objAccountGrp = new AccountGroupModel();

            var AccountGroupType = objAccountGrp.FillAccountGroup(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 41 }, companyKey: _userLoginInfo.CompanyKey);
            object result;
           
           result = AccountGroupType.Data.Where(a => a.ModeParent == 3).ToList();
               
            AccountGroupTypeList.AccountGroupType = data.AccountGroupType;

            return Json(new { AccountGroupType, result }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult FillAccountGroupTypeotherthanbalancesheet(AccountGroupModel.AccountGroupTypeView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountGroupModel.AccountGroupTypeView AccountGroupTypeList = new AccountGroupModel.AccountGroupTypeView();
            AccountGroupModel objAccountGrp = new AccountGroupModel();

            var AccountGroupType = objAccountGrp.FillAccountGroup(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 41 }, companyKey: _userLoginInfo.CompanyKey);
            object result;

            result = AccountGroupType.Data.Where(a => a.ModeParent == 2).ToList();

            AccountGroupTypeList.AccountGroupType = data.AccountGroupType;

            return Json(new { AccountGroupType, result }, JsonRequestBehavior.AllowGet);

        }
    }

}


