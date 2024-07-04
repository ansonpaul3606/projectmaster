/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 14/11/2022
Purpose		: AccountHead
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/


using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class AccountHeadController : Controller
    {
        public ActionResult AccountHead(string mtd)
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
            ViewBag.mtd = mtd;
            return View();
        }
     
        public ActionResult LoadFormAccountHead(string mtd)
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

            AccountHeadModel.AccountHeadViewList output = new AccountHeadModel.AccountHeadViewList();
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "AccountHead",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            output.SortOrder = a.Data[0].NextNo;

            var AccountGroup = Common.GetDataViaQuery<AccountHeadModel.AccountGroup>(parameters: new APIParameters
            {
                TableName = "AccountGroup AG",
                SelectFields = "AG.ID_AccountGroup ,AG.AGName ",
                Criteria = "AG.Cancelled=0 AND AG.Passed=1 AND AG.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
      companyKey: _userLoginInfo.CompanyKey
      );

            output.AccountGroup = AccountGroup.Data;

            var AccountSubGroup = Common.GetDataViaQuery<AccountHeadModel.AccountSubGroup>(parameters: new APIParameters
            {
                TableName = "AccountSubGroup ASG",
                SelectFields = "ASG.ID_AccountSubGroup ,ASG.ASGName ",
                Criteria = "ASG.Cancelled=0 AND ASG.Passed=1 AND ASG.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
    companyKey: _userLoginInfo.CompanyKey
    );
            output.AccountSubGroup = AccountSubGroup.Data;

            AccountHeadModel objAccountType = new AccountHeadModel();
            var AccountType = objAccountType.GetAccountTypeList(input: new AccountHeadModel.AccountTypeMode { Mode = 47 }, companyKey: _userLoginInfo.CompanyKey);

            output.AccountType = AccountType.Data;

            AccountHeadModel objAccountVoucher = new AccountHeadModel();
            var AccountVoucher = objAccountVoucher.GetAccountVoucherGrpngList(input: new AccountHeadModel.AccountVoucherGrpMode { Mode = 82 }, companyKey: _userLoginInfo.CompanyKey);

            output.AccountVoucher = AccountVoucher.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<AccountHeadModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            output.TaxgroupList = taxgroup.Data;

            var branchdata = objAccountType.GetBranchHead(input: new AccountHeadModel.InputBranch
            {
                FK_Company = _userLoginInfo.FK_Company,
                
            }, companyKey:_userLoginInfo.CompanyKey);

            output.branchHeads = branchdata.Data;

            return PartialView("_AddAccountHead", output);
        }

        [HttpPost]
        public ActionResult FillAccountSubGroup(AccountHeadModel.AccountHeadViewData obj)
        {
     
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountHeadModel objMl = new AccountHeadModel();
            var AccountSubList = objMl.GetSubGroupList(input: new AccountHeadModel.AccountHeadViewData
            {
                FK_AccountGroup = obj.FK_AccountGroup,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                FK_AccountSubGroup = obj.FK_AccountSubGroup
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(AccountSubList.Data, JsonRequestBehavior.AllowGet);
       
    }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveAccountHead(AccountHeadModel.AccountHeadView data)
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

            AccountHeadModel AccountHead = new AccountHeadModel();

            var datresponse = AccountHead.UpdateAccountHeadData(input: new AccountHeadModel.UpdateAccountHead
            {
                AHName = data.AHName,
                AHShortName = data.AHShortName,
                FK_AccountGroup = data.FK_AccountGroup,
                FK_AccountSubGroup = data.FK_AccountSubGroup,
                SortOrder = data.SortOrder,
                AHVoucherGrouping=data.AHVoucherGrouping,
                AHAccountMode=data.AHAccountMode,
                UserAction = 1,
                ID_AccountHead = 0,
                TransMode = data.TransMode,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AccountHead = 0,
                TaxGroupID = data.TaxGroupID,
                FK_Branch = data.FK_Branch  
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateAccountHead(AccountHeadModel.AccountHeadView data)
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


           


            AccountHeadModel AccountHead = new AccountHeadModel();

            var datresponse = AccountHead.UpdateAccountHeadData(input: new AccountHeadModel.UpdateAccountHead
            {
                AHName = data.AHName,
                AHShortName = data.AHShortName,
                FK_AccountGroup = data.FK_AccountGroup,
                FK_AccountSubGroup = data.FK_AccountSubGroup,
                SortOrder = data.SortOrder,
                AHVoucherGrouping = data.AHVoucherGrouping,
                AHAccountMode = data.AHAccountMode,
                UserAction = 2,
                ID_AccountHead = data.ID_AccountHead,
                TransMode = data.TransMode,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AccountHead = data.FK_AccountHead,
                TaxGroupID = data.TaxGroupID,
                FK_Branch = data.FK_Branch
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAccountHeadList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ModelState.Remove("TotalCount");

            AccountHeadModel AccountHead = new AccountHeadModel();

            var data = AccountHead.GetAccountHeadData(companyKey: _userLoginInfo.CompanyKey, input: new AccountHeadModel.AccountHeadID
            {
                ID_AccountHead = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode= TransMode
            });


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountHeadInfo(AccountHeadModel.AccountHeadView AccountHeadInfo)
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


            AccountHeadModel AccountHead = new AccountHeadModel();


            var data = AccountHead.GetAccountHeadData(companyKey: _userLoginInfo.CompanyKey, input: new AccountHeadModel.AccountHeadID
            {
                ID_AccountHead = AccountHeadInfo.ID_AccountHead,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteAccountHead(AccountHeadModel.DeleteAccount data)
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

            AccountHeadModel AccountHead = new AccountHeadModel();

            var datresponse = AccountHead.DeleteAccountHeadData(input: new AccountHeadModel.DeleteAccountHead
            {

                ID_AccountHead = data.ID_AccountHead,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = "",
                Debug = 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountHeadDeleteReasonList()
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
    }
}


