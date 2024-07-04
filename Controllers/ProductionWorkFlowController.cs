/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 29/11/2022
Purpose		: ProductionWorkFlow
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
17/12/2022     Kavya K         Save , Update ,Select for View
-------------------------------------------------------------------------
19/12/2022     Kavya K         Delete
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
    public class ProductionWorkFlowController : Controller
    {
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadFormProductionWorkFlow()
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

            //ProductionWorkFlowModel.ProductionWorkFlowView sortno = new ProductionWorkFlowModel.ProductionWorkFlowView();

            //var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            //companyKey: _userLoginInfo.CompanyKey,
            //procedureName: "ProGetNextNo",
            //parameter: new NextSortOrder
            //{
            //    TableName = "ProductionWorkFlow",
            //    FieldName = "SortOrder",
            //    Debug = 0
            //});

            //sortno.SortOrder = a.Data[0].NextNo;

            //  return PartialView("_AddProductionWorkFlow", sortno);
            return PartialView("_AddProductionWorkFlow");
        }

        [HttpPost]
        public ActionResult GetProductionWorkFlowList(int pageSize, int pageIndex, string Name, String TransMode)
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
            ProductionWorkFlowModel ProductionWorkFlow = new ProductionWorkFlowModel();

            var ProductionwrkflowInfo = ProductionWorkFlow.GetProductionWorkFlowData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionWorkFlowModel.ProductionWorkFlowID
            {
                FK_ProductionWorkFlow = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                Detailed = 0
            });

            return Json(new { ProductionwrkflowInfo.Process, ProductionwrkflowInfo.Data, pageSize, pageIndex, totalrecord = (ProductionwrkflowInfo.Data is null) ? 0 : ProductionwrkflowInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProductionWorkFlowInfo(ProductionWorkFlowModel.ProductionWorkFlowView data)
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
            ProductionWorkFlowModel ProductionWorkFlow = new ProductionWorkFlowModel();
            var ProductionWorkFlowInfo = ProductionWorkFlow.GetProductionWorkFlowData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionWorkFlowModel.ProductionWorkFlowID
            {
                FK_ProductionWorkFlow = data.FK_ProductionWorkFlow,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Detailed = 0
            });

            var ProductionWorkFlowItem = ProductionWorkFlow.GetProductionWorkFlowDataDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new ProductionWorkFlowModel.ProductionWorkFlowData
            {
                FK_ProductionWorkFlow = data.FK_ProductionWorkFlow,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Detailed = 1
            });

            return Json(new { ProductionWorkFlowInfo, ProductionWorkFlowItem }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewProductionWorkFlow(ProductionWorkFlowModel.ProductionWorkFlowView data)
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

            ModelState.Remove("ProductionWorkFlowID");
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

            ProductionWorkFlowModel ProductionWorkFlow = new ProductionWorkFlowModel();

            var datresponse = ProductionWorkFlow.UpdateProductionWorkFlowData(input: new ProductionWorkFlowModel.UpdateProductionWorkFlow
            {
                UserAction = 1,
                PWDate = data.PWDate,
                FK_Product = data.FK_Product,
                TransMode = data.TransMode,
                ProductionworkflowDetails = data.ProductionworkflowDetails is null ? "" : Common.xmlTostring(data.ProductionworkflowDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_ProductionWorkFlow = 0,
                FK_ProductionWorkFlow = 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateProductionWorkFlow(ProductionWorkFlowModel.ProductionWorkFlowView data)
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

            ProductionWorkFlowModel ProductionWorkFlow = new ProductionWorkFlowModel();
            var datresponse = ProductionWorkFlow.UpdateProductionWorkFlowData(input: new ProductionWorkFlowModel.UpdateProductionWorkFlow
            {
                UserAction = 2,
                PWDate = data.PWDate,
                FK_Product = data.FK_Product,
                TransMode = data.TransMode,
                ProductionworkflowDetails = data.ProductionworkflowDetails is null ? "" : Common.xmlTostring(data.ProductionworkflowDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_ProductionWorkFlow = data.ID_ProductionWorkFlow,
                FK_ProductionWorkFlow = data.ID_ProductionWorkFlow,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProductionWorkFlow(ProductionWorkFlowModel.DeleteProductionWorkFlow data)
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

            ProductionWorkFlowModel ProductionWorkFlow = new ProductionWorkFlowModel();

            var datresponse = ProductionWorkFlow.DeleteProductionWorkFlowData(input: new ProductionWorkFlowModel.DeleteProductionWorkFlow
            {
                FK_ProductionWorkFlow = data.FK_ProductionWorkFlow,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductionWorkFlowReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

    }
}


