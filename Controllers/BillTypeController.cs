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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class BillTypeController : Controller
    {
        public ActionResult BillType()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 


            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            //CommonMethod objCmnMethod = new CommonMethod();
            //string mTd = objCmnMethod.Decrypt(mtd);
            //TempData["mTd"] = mTd.ToString();

            return View();
        }
        public ActionResult LoadBillTypeForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            BillTypeModel.BillTypeListModel BillTypeList = new BillTypeModel.BillTypeListModel();


            var BillList = Common.GetDataViaQuery<BillTypeModel.ModeList>(parameters: new APIParameters // Add Cash mode list Also..........
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey


            );

            var CashList = Common.GetDataViaQuery<BillTypeModel.CashModeList>(parameters: new APIParameters
            {
                TableName = "CashMode C",
                SelectFields = "C.ID_CashMode as CashModeID, C.Cmname AS CashModeName, C.CmMode AS CashMode",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            BillTypeList.CashModeList = CashList.Data;
            BillTypeList.ModeList = BillList.Data;
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "BillType",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            BillTypeList.SortOrder = a.Data[0].NextNo;


            BillTypeModel cstrmdl = new BillTypeModel();

            var billist = cstrmdl.Gettypelst(input: new BillTypeModel.ModeLead { Mode = 23 },
                companyKey: _userLoginInfo.CompanyKey);

            BillTypeList.Billtyps = billist.Data;
           // ViewBag.PageTitle = TempData["mTd"] as string;
            return PartialView("_AddBillType", BillTypeList);
        }


        [HttpPost]
        public ActionResult GetPgBillTypeList(int pageSize, int pageIndex, string Name)
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

            string transMode = "";

            BillTypeModel billType = new BillTypeModel();
            var data = billType.GetBillTypeData(companyKey: _userLoginInfo.CompanyKey, input: new BillTypeModel.GetBillType
            {

                FK_BillType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode

            });

           // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GetBillTypeList(int pageIndex)
        //{
        //    #region ::  Check User Session to verifyLogin  ::

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

        //    #endregion ::  Check User Session to verifyLogin  ::

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


        //    BillTypeModel BillType = new BillTypeModel();
        //    var data = BillType.GetBillTypeData(input: new BillTypeModel.GetBillType
        //    {
        //        FK_BillType = 0,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        EntrBy = _userLoginInfo.EntrBy,
        //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Machine = _userLoginInfo.FK_Machine,
        //        PageIndex = pageIndex,
        //        PageSize = 100,
        //        TransMode = ""

        //    },
        //        companyKey: _userLoginInfo.CompanyKey);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetBillTypeInfoByID(BillTypeModel.BillTypeInfoView data)
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

            BillTypeModel BillType = new BillTypeModel();
            var BillTypeInfo = BillType.GetBillTypeData(companyKey: _userLoginInfo.CompanyKey, input: new BillTypeModel.GetBillType
            {
                FK_BillType = data.BillTypeID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 1,
                PageSize = 10,
                TransMode = ""
            });

            return Json(BillTypeInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewBillTypeDetails(BillTypeModel.BillTypeInputFromViewModel data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("AccountHeadID");
            ModelState.Remove("AccountHeadSubID");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");

            #region :: Model validation  ::



            //removing a node in model while validating
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
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

            BillTypeModel BillType = new BillTypeModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = BillType.UpdateBillTypeData(input: new BillTypeModel.UpdateBillType
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                BackupId = backupId,
                FK_Company = OrgnCode,
                EntrBy = entrBy,
                ID_BillType = 0,
                BTName = data.BillType,
                BTShortName = data.ShortName,
                //FK_AccountHead = data.AccountHeadID,
                //FK_AccountHeadSub = data.AccountHeadID,
                FK_AccountHead = (data.AccountHeadID.HasValue) ? data.AccountHeadID.Value : 0,
                FK_AccountHeadSub = (data.AccountHeadSubID.HasValue) ? data.AccountHeadSubID.Value : 0,
                SortOrder = data.SortOrder,
                Mode = data.Mode,
                TransMode=data.TransMode,
                CashMode = data.CashMode,
                BTBillType= data.BTBillType,
                FK_BillType = 0,
                FK_Branch = 0,
                FK_AccountHeadDisc = (data.DiscHeadID.HasValue) ? data.DiscHeadID.Value : 0,
                FK_AccountHeadSubDisc = (data.DiscHeadSubID.HasValue) ? data.DiscHeadSubID.Value : 0,
                FK_AccountHeadRoundOff = (data.RoundHeadID.HasValue) ? data.RoundHeadID.Value : 0,
                FK_AccountHeadSubRoundOff = (data.RoundHeadSubID.HasValue) ? data.RoundHeadSubID.Value : 0,
                FK_AccountHeadAdv = (data.AdvHeadID.HasValue) ? data.AdvHeadID.Value : 0,
                FK_AccountHeadSubAdv = (data.AdvHeadSubID.HasValue) ? data.AdvHeadSubID.Value : 0,
                FK_AccountHeadRet = (data.RetHeadID.HasValue) ? data.RetHeadID.Value : 0,
                FK_AccountHeadSubRet = (data.RetHeadSubID.HasValue) ? data.RetHeadSubID.Value : 0,
                FK_SecAmtHead = (data.SecAmtHeadID.HasValue) ? data.SecAmtHeadID.Value : 0,
                FK_SecAmtHeadSub = (data.SecAmtHeadSubID.HasValue) ? data.SecAmtHeadSubID.Value : 0,
                FK_ServiceAmtHead = (data.ServiceAmtHeadID.HasValue) ? data.ServiceAmtHeadID.Value : 0,
                FK_ServiceAmtHeadSub = (data.ServiceAmtSubHeadID.HasValue) ? data.ServiceAmtSubHeadID.Value : 0,
                FK_BuyBackHead = (data.BuyBackAmtHeadID.HasValue) ? data.BuyBackAmtHeadID.Value : 0,
                FK_BuyBackHeadSub = (data.BuyBackAmtSubHeadID.HasValue) ? data.BuyBackAmtSubHeadID.Value : 0,
                FK_FundHead = (data.FundHeadID.HasValue) ? data.FundHeadID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBillTypeDetails(BillTypeModel.BillTypeInputFromViewModel data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("AccountHeadID");
            ModelState.Remove("AccountHeadSubID");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");

            #region :: Model validation  ::



            //removing a node in model while validating
            ModelState.Remove("Interstate");
            ModelState.Remove("Intrastate");
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

            BillTypeModel BillType = new BillTypeModel();
            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = BillType.UpdateBillTypeData(input: new BillTypeModel.UpdateBillType
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Branch = branchCode,
                BackupId = backupId,
                FK_Company = OrgnCode,
                //FK_Reason = 0,
                EntrBy = entrBy,
                // Cancelled = false,
                ID_BillType = data.BillTypeID,
                BTName = data.BillType,
                BTShortName = data.ShortName,
                Mode = data.Mode,
                CashMode = data.CashMode,
                FK_AccountHead = (data.AccountHeadID.HasValue) ? data.AccountHeadID.Value : 0,
                FK_AccountHeadSub = (data.AccountHeadSubID.HasValue) ? data.AccountHeadSubID.Value : 0,
                //FK_AccountHead = data.AccountHeadID,
                //FK_AccountHeadSub = data.AccountHeadSubID,
                TransMode = data.TransMode,
                SortOrder = data.SortOrder,
                BTBillType = data.BTBillType,
                FK_BillType = data.BillTypeID,
                FK_AccountHeadDisc = (data.DiscHeadID.HasValue) ? data.DiscHeadID.Value : 0,
                FK_AccountHeadSubDisc = (data.DiscHeadSubID.HasValue) ? data.DiscHeadSubID.Value : 0,
                FK_AccountHeadRoundOff = (data.RoundHeadID.HasValue) ? data.RoundHeadID.Value : 0,
                FK_AccountHeadSubRoundOff = (data.RoundHeadSubID.HasValue) ? data.RoundHeadSubID.Value : 0,
                FK_AccountHeadAdv = (data.AdvHeadID.HasValue) ? data.AdvHeadID.Value : 0,
                FK_AccountHeadSubAdv = (data.AdvHeadSubID.HasValue) ? data.AdvHeadSubID.Value : 0,
                FK_AccountHeadRet = (data.RetHeadID.HasValue) ? data.RetHeadID.Value : 0,
                FK_AccountHeadSubRet = (data.RetHeadSubID.HasValue) ? data.RetHeadSubID.Value : 0,
                FK_SecAmtHead = (data.SecAmtHeadID.HasValue) ? data.SecAmtHeadID.Value : 0,
                FK_SecAmtHeadSub = (data.SecAmtHeadSubID.HasValue) ? data.SecAmtHeadSubID.Value : 0,
                FK_ServiceAmtHead = (data.ServiceAmtHeadID.HasValue) ? data.ServiceAmtHeadID.Value : 0,
                FK_ServiceAmtHeadSub = (data.ServiceAmtSubHeadID.HasValue) ? data.ServiceAmtSubHeadID.Value : 0,
                FK_BuyBackHead = (data.BuyBackAmtHeadID.HasValue) ? data.BuyBackAmtHeadID.Value : 0,
                FK_BuyBackHeadSub = (data.BuyBackAmtSubHeadID.HasValue) ? data.BuyBackAmtSubHeadID.Value : 0,
                FK_FundHead = (data.FundHeadID.HasValue) ? data.FundHeadID.Value : 0,
                //Passed = true


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteBillTypeInfo(BillTypeModel.BillTypeInfoView data)
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

            BillTypeModel BillType = new BillTypeModel();
            var datresponse = BillType.DeleteBillTypeData(input: new BillTypeModel.DeleteBillType
            {
                FK_BillType = data.BillTypeID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountHeadDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<BillTypeModel.AccountHeadView>(parameters: new APIParameters
            {
                TableName = "AccountHead AH",
                SelectFields = "AH.ID_AccountHead AS AccountHeadID,AH.AHName AS AHeadName",
                Criteria = "AH.Cancelled =0 AND AH.Passed=1",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAccountSubHeadDetails(int AccountHeadID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<BillTypeModel.AccountSubHeadView>(parameters: new APIParameters
            {
                TableName = "AccountSubHead ASH",
                SelectFields = "ASH.ID_AccountSubHead AS AccountHeadSubID,ASH.ASHName AS ASHeadName",
                Criteria = "ASH.Cancelled =0 AND ASH.Passed=1 AND ASH.AHCode=" + AccountHeadID,
                SortFields = "",
                GroupByFileds = ""
            },


        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetBillTypeReasonList()
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