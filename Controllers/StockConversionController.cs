/*----------------------------------------------------------------------
Created By	: NIJI TJ
Created On	: 28/02/2023
Purpose		: StockConversion
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
    public class StockConversionController : Controller
    {
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            return View();
        }

        public ActionResult LoadFormStockConversion(string StockConvTransMode)
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

            StockConversionModel.StockConversionView sortno = new StockConversionModel.StockConversionView();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "StockConversion",
                FieldName = "SortOrder",
                Debug = 0
            });

            sortno.SortOrder = a.Data[0].NextNo;
            StockConversionModel.StockConversionView list = new StockConversionModel.StockConversionView();

            var OpDepartmentList = Common.GetDataViaQuery<StockConversionModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                        companyKey: _userLoginInfo.CompanyKey

             );
            list.DepartmentList = OpDepartmentList.Data;
            var OpBranchListto = Common.GetDataViaQuery<StockConversionModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                     companyKey: _userLoginInfo.CompanyKey

          );
            list.BranchList = OpBranchListto.Data;

            StockConversionModel objpaymode = new StockConversionModel();
            var StockModeList = objpaymode.GeLeadStatusList(input: new StockConversionModel.ModeLead { Mode =58 }, companyKey: _userLoginInfo.CompanyKey);

            list.StockModeList = StockModeList.Data;

            PurchaseOrderModel models = new PurchaseOrderModel();
            var LastIDNo = models.GetPurchaseOrderlastID(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.LastIdIn { FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = StockConvTransMode });
            list.LastID = LastIDNo.Data[0].LastID;
            return PartialView("_AddStockConversion", list);
        }
        public ActionResult GetStockConversionReasonList()
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

        [HttpPost]
        public ActionResult GetStockConversionList(int pageSize, int pageIndex, string Name, string TransMode)
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
            StockConversionModel StockConversion = new StockConversionModel();

            var data = StockConversion.GetStockConversionData(companyKey: _userLoginInfo.CompanyKey, input: new StockConversionModel.StockConversionID
            {
                FK_StockConversion = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode =  TransMode,
                Name = Name
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

           
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteStockConversion(StockConversionModel.StockConversionView data)
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

            StockConversionModel StockConversion = new StockConversionModel();

            var datresponse = StockConversion.DeleteStockConversionData(input: new StockConversionModel.DeleteStockConversion { TransMode=data.TransMode,FK_StockConversion = data.ID_StockConversion, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.ReasonID , FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewStockConversion(StockConversionModel.StockConversion data)
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
            StockConversionModel StockConv = new StockConversionModel();

            var datresponse = StockConv.UpdateStockConversionData(input: new StockConversionModel.StockConversionUpdate
            {
                UserAction = 1,
                TransDate = data.TransDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_StockConversion = data.ID_StockConversion,
                LastID=data.LastID,
                TransMode=data.TransMode,
                FK_Employee=data.FK_Employee,
               StockConversionDetails = data.StockConversionDetails is null ? "" : Common.xmlTostring(data.StockConversionDetails)
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetStockConversionInfo(StockConversionModel.StockConversionView stockConversionInfo)
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
            StockConversionModel stockConversion = new StockConversionModel();
            var alInfo = stockConversion.GetStockConversionData(companyKey: _userLoginInfo.CompanyKey, input: new StockConversionModel.StockConversionID
            {
                FK_StockConversion = stockConversionInfo.ID_StockConversion,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                Detailed=0
            });
           
            var subtable = stockConversion.GetStockConversionDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new StockConversionModel.StockConversionID
            {
                FK_StockConversion = stockConversionInfo.ID_StockConversion,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                Detailed=1
            });
           
            return Json(new { alInfo, subtable }, JsonRequestBehavior.AllowGet);
        }
    }
}
 
