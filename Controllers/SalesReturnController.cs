/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 23/01/2023
Purpose		: Sales Return
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
    public class SalesReturnController : Controller
    {
        // GET: SalesReturn

        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;

           // Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
        }
        public ActionResult SalesReturn(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            var unitset = Common.GetDataViaQuery<ProductModel.MultipleUnitSettings>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT003'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
                   companyKey: _userLoginInfo.CompanyKey

        );



            ViewBag.Multiunitsettings = unitset.Data[0].GsValue;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }
        [HttpGet]
        public ActionResult LoadSalesReturnForm(string mtd)
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
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
          SalesReturnModel.SalesReturnView sortno = new SalesReturnModel.SalesReturnView();
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var UnitList = Common.GetDataViaQuery<SalesReturnModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);
            sortno.UnitList = UnitList.Data;
            
            return PartialView("_AddSalesReturn", sortno);
        }

        public ActionResult GetSalesInvoiceSearch(string TransMode)
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
            SalesReturnModel objSal = new SalesReturnModel();

            var data = objSal.GetSalesSelectForSalesReturn(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.GetSalesData
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Branch = _userLoginInfo.FK_Branch,
                TransMode = TransMode
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInvoiceDetails(SalesReturnModel.SalesGetInfo data)
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

            SalesReturnModel salesReturnModel = new SalesReturnModel();
            var datresponse = salesReturnModel.FillSales(input: new SalesReturnModel.SalesGetInfo
            {
                InvoiceNo = data.InvoiceNo,              
                FK_Company = _userLoginInfo.FK_Company,
               FK_Branch = _userLoginInfo.FK_Branch,
               TransMode = data.TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSalesReturnFill(SalesReturnModel.SalesReturnfill Data)
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

            SalesReturnModel salesReturnModel = new SalesReturnModel();

            var datresponse = salesReturnModel.FilSales(input: new SalesReturnModel.SalesReturnfil
            {
                FK_Master = Data.SalesID,
                Mode = Data.Mode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
            }, companyKey: _userLoginInfo.CompanyKey);

            var TaxDetails = salesReturnModel.FilSales(input: new SalesReturnModel.SalesReturnfil
            {
                FK_Master = Data.FK_Master,
                Mode = 4,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
            }, companyKey: _userLoginInfo.CompanyKey);

            var MasterUnit = salesReturnModel.FilSales(input: new SalesReturnModel.SalesReturnfil
            {
                FK_Master = Data.FK_Master,
                Mode = 5,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datresponse, MasterUnit }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalesReturnTaxFill(SalesReturnModel.SalesReturnfill Data)
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

            SalesReturnModel salesReturnModel = new SalesReturnModel();

            var TaxDetails = salesReturnModel.FilSalesTax(input: new SalesReturnModel.SalesReturnfil
            {
                FK_Master = Data.FK_Master,
                Mode = 4,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { TaxDetails }, JsonRequestBehavior.AllowGet);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddSalesReturn(SalesReturnModel.SalesReturnView data)
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


            SalesReturnModel SalesReturn = new SalesReturnModel();

            var datresponse = SalesReturn.UpdateSalesReturnData(input: new SalesReturnModel.UpdateSalesReturn
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_SalesReturn = 0,
                Mode =  data.Mode,
                SrBillNo = data.SrBillNo,
                SrReturnDate = data.SrReturnDate,
                SrReferenceNo = data.SrReferenceNo,
                SrBillDate = data.SrBillDate,
                SrBillTotal = data.SrBillTotal,
                SrDiscount = data.SrDiscount,
                SrOthercharges = data.SrOthercharges,
                SrRoundoff = data.SrRoundoff,
                SrNetAmount = data.SrNetAmount,
                SrRemarks = data.SrRemarks,
                TTIntrastate = 1/*data.TaxtyIntrastate*/,
                FK_Customer = data.FK_Customer,
                FK_BillType = data.BillType,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Sales = data.FK_Sales,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,            
                SalesReturnDetails = data.SalesReturnDetails is null ? "" : Common.xmlTostring(data.SalesReturnDetails),
                TaxDetails =        data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                FK_SalesReturn=0,
                CustomerName=data.CustomerName,
                SrReturnType=data.SrReturnType
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSalesReturn(SalesReturnModel.SalesReturnView data)
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


            SalesReturnModel SalesReturn = new SalesReturnModel();

            var datresponse = SalesReturn.UpdateSalesReturnData(input: new SalesReturnModel.UpdateSalesReturn
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_SalesReturn = data.ID_SalesReturn,
                Mode = data.Mode,
                SrBillNo = data.SrBillNo,
                SrReferenceNo = data.SrReferenceNo,
                SrBillDate = data.SrBillDate,
                SrReturnDate = data.SrReturnDate,
                SrBillTotal = data.SrBillTotal,
                SrDiscount = data.SrDiscount,
                SrOthercharges = data.SrOthercharges,
                SrRoundoff = data.SrRoundoff,
                SrNetAmount = data.SrNetAmount,
                SrRemarks = data.SrRemarks,
                TTIntrastate = 1/*data.TaxtyIntrastate*/,
                FK_Customer = data.FK_Customer,
                FK_BillType = data.BillType,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Sales = data.FK_Sales,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                SalesReturnDetails = data.SalesReturnDetails is null ? "" : Common.xmlTostring(data.SalesReturnDetails),
                TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                FK_SalesReturn = data.ID_SalesReturn,
                CustomerName = data.CustomerName,
                SrReturnType = data.SrReturnType
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSalesReturnList(int pageSize, int pageIndex, string Name, string TransMode)
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
            SalesReturnModel SalesReturn = new SalesReturnModel();

            var SalesInfo = SalesReturn.GetSalesReturnDataForList(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.GetSalesReturn
            {
                FK_SalesReturn = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                Details = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            return Json(new { SalesInfo.Process, SalesInfo.Data, pageSize, pageIndex, totalrecord = (SalesInfo.Data is null) ? 0 : SalesInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSalesReturnInfo(SalesReturnModel.SalesReturnView salesreturnInfo)
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



            SalesReturnModel salesreturn = new SalesReturnModel();

            var SalRetInfo = salesreturn.GetSalesReturnData(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.GetSalesReturn
            {
                FK_SalesReturn = salesreturnInfo.ID_SalesReturn,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = salesreturnInfo.TransMode,
                Details = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            var SrRetProducts = salesreturn.GetSalesReturnProductData(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.GetSalesReturn
            {
                FK_SalesReturn = salesreturnInfo.ID_SalesReturn,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = salesreturnInfo.TransMode,
                Details = 1,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            var OtherCharge = salesreturn.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.GetOtherchargeDetails
            {
                FK_Transaction = salesreturnInfo.ID_SalesReturn,
                TransMode = salesreturnInfo.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            });
            var TaxDetails = salesreturn.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.SalesTaxGet
            {
                ID_Transaction = salesreturnInfo.ID_SalesReturn,
                Mode = "P",
                TransMode = salesreturnInfo.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            });

            //var Data = salesreturn.GetMultiunit(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.ProductWiseUnit
            //{

            //    Mode = "P",
            //    TransMode = salesreturnInfo.TransMode,
            //    ProductID = salesreturnInfo.ProductID,
            //    FK_Company = _userLoginInfo.FK_Company,
            //    EntrBy = _userLoginInfo.EntrBy,
            //    FK_Machine = _userLoginInfo.FK_Machine,
            //    FK_Transaction = salesreturnInfo.FK_Transaction,
            //});



            return Json(new { SalRetInfo, SrRetProducts, OtherCharge, TaxDetails }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSalesReturn(SalesReturnModel.SalesReturnView data)
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

            SalesReturnModel SalesReturn = new SalesReturnModel();

            var datresponse = SalesReturn.DeleteSalesReturnData(input: new SalesReturnModel.DeleteSalesReturn
            {
                FK_SalesReturn = data.ID_SalesReturn,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = data.TransMode
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSalesReturnUnit(SalesReturnModel.ProductWiseUnit data)
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

            SalesReturnModel salesReturnModel = new SalesReturnModel();

            //APIParameters apiParaUnit = new APIParameters
            //{
            //    TableName = "[Unit]",
            //    SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName",
            //    Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
            //    GroupByFileds = "",
            //    SortFields = ""
            //};
            //var UnitList = Common.GetDataViaQuery<SalesReturnModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);
            var Data = salesReturnModel.GetMultiunit(companyKey: _userLoginInfo.CompanyKey, input: new SalesReturnModel.ProductWiseUnit
            {
               
                Mode = "P",
                TransMode = data.TransMode,
                ProductID=data.ProductID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Transaction = data.FK_Transaction,
                SalesReturnMode=data.SalesReturnMode
            });

            //return Json(new { MuultipleUnitList }, JsonRequestBehavior.AllowGet);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        #region Buyback
        public ActionResult BuyBackIndex(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }


        [HttpGet]
        public ActionResult LoadBuyBackForm(string mtd)
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
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            SalesReturnModel.SalesReturnView sortno = new SalesReturnModel.SalesReturnView();
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddBuyBack", sortno);
        }
        #endregion
    }
}