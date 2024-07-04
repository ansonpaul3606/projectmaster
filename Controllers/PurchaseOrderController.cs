/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 23/07/2022
Purpose		: PurchaseOrder
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
    public class PurchaseOrderController : Controller
    {
        public ActionResult Index(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        [HttpGet]
        
        public ActionResult LoadPurchaseOrderForm(string mtd)
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
            PurchaseOrderModel.PurchaseorderViewList list = new PurchaseOrderModel.PurchaseorderViewList();

            var OpDepartmentList = Common.GetDataViaQuery<PurchaseOrderModel.Department>(parameters: new APIParameters
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
            var OpUnitList = Common.GetDataViaQuery<PurchaseOrderModel.Unit>(parameters: new APIParameters
            {
                TableName = "Unit",
                SelectFields = "ID_Unit AS PurordUnitID,UnitName AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
                       companyKey: _userLoginInfo.CompanyKey

            );
            list.UnitList= OpUnitList.Data;

           PurchaseOrderModel model = new PurchaseOrderModel();
            var OrderNo = model.GetPurchaseOrderNo(input: new PurchaseOrderModel.inputGetPurOrdNo { FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            list.PurordNo = OrderNo.Data[0].PurordNo;

            



            var ChangeMod = Common.GetDataViaProcedure<PurchaseOrderModel.DeliveryMode, PurchaseOrderModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new PurchaseOrderModel.ChangeModeInput { Mode = 27 });
            //AssignModel. = Lits.Data;
            list.DeliveryModeList = ChangeMod.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            var NumberGenlist = Common.GetDataViaQuery<PurchaseOrderModel.NumberGen>(parameters: new APIParameters
            {
                TableName = "CommonSettings",
                SelectFields = "ID_CommonSettings AS ID_NumberGen",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Module=4 AND FK_SubModule=46 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
                    companyKey: _userLoginInfo.CompanyKey

         );
           if(NumberGenlist.Data!=null)
            {
                ViewBag.NumberGen = 1;
            }
            else
            {
                ViewBag.NumberGen = 0;
            }

            return PartialView("_AddPurchaseOrder",list);
        }

        public ActionResult GetEmployeedetailsDefault()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadGenerateModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department, BT.BTName,B.BrName,D.DeptName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

);


            //OrganizationModel Organization = new OrganizationModel();

            //var data = Organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID { ID_Organization = 0 });

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetPurchaseDataFill(PurchaseOrderModel.Purchasedatafill Data)
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

            PurchaseOrderModel purchaseModel = new PurchaseOrderModel();

            var datresponse = purchaseModel.PurchaseOrderDataFill(input: new PurchaseOrderModel.Purchasedatafill
            {
                FK_Master = Data.FK_Master,
              Mode=1/*Data.Includetax*/


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductreorderdetails(PurchaseOrderModel.InputProductID datas)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<PurchaseOrderModel.PurchaseOrderView>(parameters: new APIParameters
            {
                TableName = "Product P LEFT JOIN Unit U ON U.ID_Unit = P.FK_Unit",
                SelectFields = "U.ID_Unit AS PurordUnitID,U.UnitName AS UnitName,U.NoOfUnits AS UnitCount",
                Criteria = "P.Cancelled =0 AND P.Passed=1 AND P.ID_Product=" + datas.ProductID +  " AND P.FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetPurchaseOrderList(int pageSize, int pageIndex, string Name,string TransModes)
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
            PurchaseOrderModel purchaseOrder = new PurchaseOrderModel();
            string transMode = "";
            var PurchaseorderInfo = purchaseOrder.GetPurchaseOrderData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.PurchaseOrderViewID

            {
                FK_PurchaseOrder = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = TransModes

            });

            //  return Json(EmployeetransferInfo, JsonRequestBehavior.AllowGet);
            return Json(new { PurchaseorderInfo.Process, PurchaseorderInfo.Data, pageSize, pageIndex, totalrecord = (PurchaseorderInfo.Data is null) ? 0 : PurchaseorderInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]

        public ActionResult GetPurchaseOrderInfo(PurchaseOrderModel.PurchaseOrderView data)
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


            PurchaseOrderModel purchaseOrderModel = new PurchaseOrderModel();

            var mptableInfo = purchaseOrderModel.GetPurchaseOrderData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.PurchaseOrderViewID
            {
                FK_PurchaseOrder = data.PurchaseOrderID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode=data.TransMode,
            });

            var subtable = purchaseOrderModel.GetSubTablePurchaseOrderData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.PurchaseorderSubSelect { FK_PurchaseOrder = data.PurchaseOrderID, Detailed = 1, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].PurchaseOrderDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewPurchaseOrder(PurchaseOrderModel.PurchaseOrderView data)
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

            PurchaseOrderModel PurchaseOrder = new PurchaseOrderModel();

            var datresponse = PurchaseOrder.UpdatePurchaseOrderData(input: new PurchaseOrderModel.UpdatePurchaseOrder
            {
                UserAction = 1,

                // PoNo = data.PurordNo,
                PoNo = data.Number,
                PoReferenceNo = data.PurordQReferenceNo,
                PoDate = data.PurordDate,
                //PoTotalAmount = data.PurordTotalAmount,
            
                PoNetAmount = data.PurordNetAmount,
                PoAdvanceAmount = data.PurordAdvanceAmount,
                PoEstiDeliveryDate = data.PurordEstiDeliveryDate,
                PoDeliveryType = data.PurordDeliveryType,
                PoPurchased = 0,
                PoOthercharges=0,
                TransMode = data.TransMode,
                Debug=0,
                FK_Quotation = (data.QuotationID.HasValue)?data.QuotationID.Value : 0,
                PoRemarks =data.PurordRemark,
                FK_Supplier = data.SupplierID,
                PurchaseOrderDetails = data.PurchaseOrderDetails is null ?"":Common.xmlTostring(data.PurchaseOrderDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.DepartmentID,
                FK_Branch=_userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                LastID= (data.LastID.HasValue) ? data.LastID.Value : 0,

                ID_PurchaseOrder = 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult UpdatePurchaseOrder(PurchaseOrderModel.PurchaseOrderView data)
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
            ModelState.Remove("PurordAdvanceAmount");
            ModelState.Remove("PurchaseOrderID");
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

            PurchaseOrderModel PurchaseOrder = new PurchaseOrderModel();

            var datresponse = PurchaseOrder.UpdatePurchaseOrderData(input: new PurchaseOrderModel.UpdatePurchaseOrder
            {
                UserAction = 2,

                PoNo = data.Number,
                PoReferenceNo = data.PurordQReferenceNo,
                PoDate = data.PurordDate,

                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                PoNetAmount = data.PurordNetAmount,
                PoAdvanceAmount = data.PurordAdvanceAmount,
                PoEstiDeliveryDate = data.PurordEstiDeliveryDate,
                PoDeliveryType = data.PurordDeliveryType,
                TransMode = data.TransMode,
                FK_Quotation = (data.QuotationID.HasValue) ? data.QuotationID.Value : 0,
                //(data.PurordQuotation.HasValue) ? data.PurordQuotation.Value : 0,
                FK_Supplier = data.SupplierID,
                PurchaseOrderDetails = data.PurchaseOrderDetails is null ? "" : Common.xmlTostring(data.PurchaseOrderDetails),
                PoRemarks = data.PurordRemark,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.DepartmentID,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug=0,
                ID_PurchaseOrder = data.PurchaseOrderID,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeletePurchaseOrder(PurchaseOrderModel.PurchaseOrderInfoView data)
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

            PurchaseOrderModel purchaseOrder = new PurchaseOrderModel();

            var datresponse = purchaseOrder.DeletePurchaseOrderData(input: new PurchaseOrderModel.DeletePurchaseOrder
            {
                EntrBy = _userLoginInfo.EntrBy,
               FK_PurchaseOrder=data.PurchaseOrderID,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetPurchaseOrderReasonList()
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
        public ActionResult SmartFill_PurchaseOrder(PurchaseOrderModel.SmartFillInput data)
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
            PurchaseOrderModel purchaseOrderModel = new PurchaseOrderModel();

            var result = purchaseOrderModel.GetPurchaseOrderCollection(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.SmartFillInput
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                FK_Supplier = data.FK_Supplier,
            });
           

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDetailsByQuotationNo(string Quotationno)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
           PurchaseOrderModel model = new PurchaseOrderModel();


            var data = model.GetPurchaseorderByQuotationid(input:
             new PurchaseOrderModel.PurchaseorderbyQutotationView
             {
                 FK_Company = _userLoginInfo.FK_Company,
                 Quotationno = Quotationno,
                 Mode = 1

             },

              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);


        }

    }
}


