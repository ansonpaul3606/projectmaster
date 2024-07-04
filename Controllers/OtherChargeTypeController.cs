
/*----------------------------------------------------------------------
Created By	: Amritha A K
Created On	: 03/02/2022
Purpose		: OtherChargeType
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
    public class OtherChargeTypeController : Controller
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
            return View();
        }

        public ActionResult LoadFormOtherChargeType(string mtd)
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

            OtherChargeTypeModel.OtherChargeTypeViewList otherchargeviewlist = new OtherChargeTypeModel.OtherChargeTypeViewList();

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );
            otherchargeviewlist.TransTypeList = Transtypelist.Data;



            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "OtherChargeType",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            otherchargeviewlist.SortOrder = a.Data[0].NextNo;
            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<OtherChargeTypeModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            otherchargeviewlist.TaxgroupList = taxgroup.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddOtherChargeType", otherchargeviewlist);
        }


      

        [HttpPost]
        public ActionResult GetOtherChargeTypeList(int pageSize, int pageIndex, string Name)
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
            OtherChargeTypeModel OtherChargeType = new OtherChargeTypeModel();
           
            var otherchargeInfo = OtherChargeType.GetOtherChargeTypeData(companyKey: _userLoginInfo.CompanyKey, input: new OtherChargeTypeModel.OtherChargeTypeID

            {
                ID_OtherChargeType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = ""

            });

            //  return Json(EmployeetransferInfo, JsonRequestBehavior.AllowGet);
            return Json(new { otherchargeInfo.Process, otherchargeInfo.Data, pageSize, pageIndex, totalrecord = (otherchargeInfo.Data is null) ? 0 : otherchargeInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOtherChargeTypeInfoByID(OtherChargeTypeModel.OtherChargeTypeView data)
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


            ModelState.Remove("Names");
            ModelState.Remove("ShortName");
            ModelState.Remove("TransTypeID");
            ModelState.Remove("Sales");
            ModelState.Remove("SalesReturn");
            ModelState.Remove("Purchase");
            ModelState.Remove("PurchaseReturn");
            ModelState.Remove("Other");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");


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

            OtherChargeTypeModel otherChargeType = new OtherChargeTypeModel();

            var otherChargeTypeInfo = otherChargeType.GetOtherChargeTypeData(companyKey: _userLoginInfo.CompanyKey, input: new OtherChargeTypeModel.OtherChargeTypeID { ID_OtherChargeType = data.OtherChargeTypeID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            return Json(otherChargeTypeInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewOtherChargeType(OtherChargeTypeModel.OtherChargeTypeView data)
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

            ModelState.Remove("OtherChargeTypeID");
            //ModelState.Remove("TransTypeID");
            ModelState.Remove("Sales");
            ModelState.Remove("SalesReturn");
            ModelState.Remove("Purchase");
            ModelState.Remove("PurchaseReturn");
            ModelState.Remove("Other");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("SortOrder");
            ModelState.Remove("FK_TaxGroup");
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

            OtherChargeTypeModel OtherChargeType = new OtherChargeTypeModel();

            var datresponse = OtherChargeType.UpdateOtherChargeTypeData(input: new OtherChargeTypeModel.UpdateOtherChargeType
            {
                UserAction = 1,
                ID_OtherChargeType = 0,
                OctyName = data.Names,
                OctyShortName = data.ShortName,
                OctyTransType = data.TransTypeID,
                OctySales = data.Sales,
                OctySalesReturn = data.SalesReturn,
                OctyPurchase = data.Purchase,
                OctyPurchaseReturn = data.PurchaseReturn,
                OctyOther = data.Other,
                FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,
                FK_AccountSubHead = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,
                FK_Company = _userLoginInfo.FK_Company,
                SortOrder = data.SortOrder,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode=data.TransMode,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_TaxGroup=data.FK_TaxGroup,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateOtherChargeType(OtherChargeTypeModel.OtherChargeTypeView data)
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

            // ModelState.Remove("OtherChargeTypeID");
            //ModelState.Remove("TransTypeID");
            ModelState.Remove("Sales");
            ModelState.Remove("SalesReturn");
            ModelState.Remove("Purchase");
            ModelState.Remove("PurchaseReturn");
            ModelState.Remove("Other");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("SortOrder");
            ModelState.Remove("FK_TaxGroup");
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

            OtherChargeTypeModel OtherChargeType = new OtherChargeTypeModel();
            var datresponse = OtherChargeType.UpdateOtherChargeTypeData(input: new OtherChargeTypeModel.UpdateOtherChargeType
            {
                UserAction = 2,
                ID_OtherChargeType = data.OtherChargeTypeID,
                OctyName = data.Names,
                OctyShortName = data.ShortName,
                OctyTransType = data.TransTypeID,
                OctySales = data.Sales,
                OctySalesReturn = data.SalesReturn,
                OctyPurchase = data.Purchase,
                OctyPurchaseReturn = data.PurchaseReturn,
                OctyOther = data.Other,
                FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,
                FK_AccountSubHead = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,
                FK_Company = _userLoginInfo.FK_Company,
                SortOrder = data.SortOrder,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_TaxGroup = data.FK_TaxGroup,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteOtherChargeType(OtherChargeTypeModel.OtherChargeTypeInfoView data)
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

           
            OtherChargeTypeModel model = new OtherChargeTypeModel();

            Output dataresponse = model.DeleteOtherChargeTypeData(companyKey: _userLoginInfo.CompanyKey,input: new OtherChargeTypeModel.DeleteOtherChargeType
            {
                ID_OtherChargeType = data.ID_OtherChargeType,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            });

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetOtherChargeTypeReasonList()
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