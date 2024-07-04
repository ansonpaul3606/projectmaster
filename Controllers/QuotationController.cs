using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static PerfectWebERP.Models.QuotationModel;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class QuotationController : Controller
    {       
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
        }
        public ActionResult Outward(string mtd,string mgrp)
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
            ViewBag.TransMode = mGrp;// TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = "Lead Generation";//TempData["mTd"] as string;
            var MenuList = Common.GetDataViaQuery<QuotationModel.Menulist>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode",
                Criteria = "Cancelled=0 AND Passed=1 AND ControllerName='Quotation'AND Url='OutWard' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MenuList",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            ViewBag.SharePagetransmode = MenuList.Data;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
        }
        public ActionResult LoadOutwardQuotation(string mtd)
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
            QuotationModel.QuotationViewInit objQu = new QuotationModel.QuotationViewInit();
            //Mode Inward=0
            //Mode Outward=1

            objQu.QuoMode = "1";
            var Category = Common.GetDataViaQuery<QuotationModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project,Mode",
                Criteria = "Cancelled=0 AND Passed=1 AND MODE='P' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },companyKey: _userLoginInfo.CompanyKey);
            objQu.CategoryList = Category.Data;

            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            objQu.Warrantytype = warrantytype.Data;
            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
      companyKey: _userLoginInfo.CompanyKey
     );
            objQu.PaymentView = PaymentView.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.FromLead = 0;

            return PartialView("_AddQuotationOutwardForm", objQu);
        }
        [HttpPost]
        public ActionResult GetOutwardQuotationList(int pageSize, int pageIndex, string Name, string TransModes)
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

            QuotationModel objQu = new QuotationModel();
            var QuInfo = objQu.GetQuotationData(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotation
            {
                ID_Quotation = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransModes,
                QuoMode= 1
            });

          
            return Json(new { QuInfo.Process, QuInfo.Data, pageSize, pageIndex, totalrecord = (QuInfo.Data is null) ? 0 : QuInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Inward(string mtd,string mgrp)
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
            ViewBag.TransMode = mGrp;// TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadInwardQuotation(string mtd)
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
            QuotationModel.QuotationViewInit objQu = new QuotationModel.QuotationViewInit();
            //Mode Inward=0
            //Mode Outward=1
            objQu.QuoMode = "0";
            var Category = Common.GetDataViaQuery<QuotationModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            objQu.CategoryList = Category.Data;

            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            objQu.Warrantytype = warrantytype.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddQuotationInwardForm", objQu);
        }
        [HttpPost]
        public ActionResult GetInwardQuotationList(int pageSize, int pageIndex, string Name,string TransModes)
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

            QuotationModel objQu = new QuotationModel();
            var QuInfo = objQu.GetQuotationData(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotation
            {
                ID_Quotation = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransModes,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                QuoMode = 0
            });


            return Json(new { QuInfo.Process, QuInfo.Data, pageSize, pageIndex, totalrecord = (QuInfo.Data is null) ? 0 : QuInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetQuotationInfo(QuotationModel.QuotationGetInfo objQuo)
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
            QuotationModel objQu = new QuotationModel();
            var quoInfo = objQu.GetQuotationSelectDetails(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.QuotationSelectDetails
            {
                ID_Quotation = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuoMode= objQuo.QuoMode
            });

            var quoItem = objQu.GetQuotationItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.QuotationItemDetails
            {
                ID_Quotation = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuoMode = objQuo.QuoMode
            });

            var quoWar = objQu.GetQuotationWarrantySelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.WarrantyListCriteria {
                //TransMode= "INQU",
                TransMode= objQuo.TransMode,
                FK_Product=0,
                FK_Master= objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            // Common.fillOtherCharges(objQuo.TransMode, objQuo.ID_Quotation);
            var OtherCharge = objQu.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetOtherCharge
            {
                //TransMode = "INQU",
                TransMode = objQuo.TransMode,
                FK_Transaction = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = objQuo.QuoMode
            });

            var ProjectQuotationCriteria = objQu.GetQuotationCriteriaDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotationCriteriaByID
            {
                ID_Quotation = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,                
            });
            return Json(new { quoInfo, quoItem, quoWar, OtherCharge, ProjectQuotationCriteria }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductSearch(int FK_Category, string ProdName)
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
            var data = Common.GetDataViaQuery<QuotationModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P LEFT JOIN Stock S ON P.ID_Product=S.FK_Product",
                SelectFields = "P.ID_Product,P.ProdName,P.ProdShortName ,CONVERT(DECIMAL(14,2),ISNULL(S.MRP,0)) ,CONVERT(DECIMAL(14,2),ISNULL(S.SalPrice,0)) AS Rate",
                Criteria = "P.Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND P.FK_Company=" + _userLoginInfo.FK_Company + "  AND P.FK_Category=" + FK_Category + "  AND P.ProdName LIKE +" + "'%" + ProdName + "%'",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProduct(string ProdName)
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
            var data = Common.GetDataViaQuery<QuotationModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P join Stock S ON P.ID_Product=S.FK_Product",
                SelectFields = "P.ID_Product,P.ProdName,P.ProdShortName , S.MRP,S.SalPrice AS Rate,P.FK_Category FK_Category",
                Criteria = "P.Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND P.FK_Company=" + _userLoginInfo.FK_Company + " AND P.ProdName LIKE +" + "'%" + ProdName + "%'",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadFormQuotation()
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
            QuotationModel.QuotationView objQu = new QuotationModel.QuotationView();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "Quotation",
                FieldName = "SortOrder",
                Debug = 0
            });
            objQu.SortOrder = a.Data[0].NextNo;
            return PartialView("_AddQuotationForm", objQu);
        }
        [HttpPost]
        public ActionResult GetQuotationList(int pageSize, int pageIndex, string Name)
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

            QuotationModel objQu = new QuotationModel();
            var QuInfo = objQu.GetQuotationData(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotation
            {
                ID_Quotation = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = ""
            });

            return Json(new { QuInfo.Process, QuInfo.Data, pageSize, pageIndex, totalrecord = (QuInfo.Data is null) ? 0 : QuInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewQuotation(QuotationModel.QuotationNew  data)
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
            QuotationModel objQu = new QuotationModel();

            var QuotationNo = objQu.GetQuotationNo(input: new QuotationModel.InputGetQuotationNo
            {
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            var datresponse = objQu.UpdateQuotationData(input: new QuotationModel.UpdateQuotation
            {
                UserAction = 1,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_Quotation =0,
                QuoMode = data.QuoMode,
                QuoFrom = data.QuoFrom,   
                FK_Master=data.FK_Master,                
                QuoDate = data.QuoDate,               
                QuoExpireDate = data.QuoExpireDate,
                QuoBillTotal = data.QuoBillTotal,
                QuoDiscount = data.QuoDiscount,
                QuoOthercharges = data.QuoOthercharges,
                QuoRoundoff = data.QuoRoundoff,
                QuoNetAmount = data.QuoNetAmount,
                QuoRemarks = data.QuoRemarks,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuotationDetails = data.QuotationDetail is null ? "" : Common.xmlTostring(data.QuotationDetail),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
              
                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
             
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                CustomerName =data.CustomerName,
                CustomerPhone=data.CustomerPhone,
                CustomerAddress=data.CustomerAddress,
                FK_QuotationGen = data.FK_QuotationGen,
                QuoReferenceNo = data.QuoReferenceNo,
                FK_Quotation =0,
                QuoEntrDate = data.QuoEntrDate,
                QuotationCriteriaDetails = data.QuotationCriteriaDetails is null ? "" : Common.xmlTostring(data.QuotationCriteriaDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateQuotation(QuotationModel.QuotationNew data)
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

            QuotationModel objQu = new QuotationModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = objQu.UpdateQuotationData(input: new QuotationModel.UpdateQuotation
            {
                UserAction = 2,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_Quotation = data.ID_Quotation,               
                QuoMode = data.QuoMode,
                QuoFrom = data.QuoFrom,
                FK_Master = data.FK_Master,
                QuoDate = data.QuoDate,
                QuoExpireDate = data.QuoExpireDate,
                QuoBillTotal = data.QuoBillTotal,
                QuoDiscount = data.QuoDiscount,
                QuoOthercharges = data.QuoOthercharges,
                QuoRoundoff = data.QuoRoundoff,
                QuoNetAmount = data.QuoNetAmount,
                QuoRemarks = data.QuoRemarks,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuotationDetails = data.QuotationDetail is null ? "" : Common.xmlTostring(data.QuotationDetail),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),

                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                CustomerName = data.CustomerName,
                CustomerPhone = data.CustomerPhone,
                CustomerAddress = data.CustomerAddress,
                FK_QuotationGen = data.FK_QuotationGen,
                QuoReferenceNo = data.QuoReferenceNo,
                FK_Quotation = 0,
                QuoEntrDate=data.QuoEntrDate,
                QuotationCriteriaDetails = data.QuotationCriteriaDetails is null ? "" : Common.xmlTostring(data.QuotationCriteriaDetails),
            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteQuotation(QuotationModel.DeleteQuotation data)
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

            QuotationModel Quot = new QuotationModel();

            var datresponse = Quot.DeleteQuotationData(input: new QuotationModel.DeleteQuotation
            {
                ID_Quotation = data.ID_Quotation,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                QuoMode=data.QuoMode
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetQuotationReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOtherCharges1(QuotationModel.BindOtherCharge Data)
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
            QuotationModel objQu = new QuotationModel();
            var OtherCharges = objQu.FillOtherCharges(input: new QuotationModel.BindOtherCharge
            {
                Mode = Data.Mode,

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=2",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaxAmountNew(SalesOrderModel.BindTaxAmountNew Data)
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

            PurchaseModel purchaseModel = new PurchaseModel();

            var datresponse = purchaseModel.FillTaxNew(input: new PurchaseModel.BindTaxAmountNew
            {
                Amount = Data.Amount,
                Includetax = 0/*Data.Includetax*/,
                FK_Product = Data.FK_Product,
                Sales = 1,
                Quantity = Data.Quantity

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetQuotationGenInfo(QuotationModel.QuotationGetInfo objQuo)
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
            QuotationModel objQu = new QuotationModel();           

            var quoItem = objQu.GetQuotationGenInwardItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.QuotationItemDetails
            {
                ID_Quotation = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuoMode =0
            });

            return Json(new { quoItem}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuotationGenSearch(string TransMode)
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
             QuotationModel objQu = new QuotationModel();          

            var data = objQu.GetQuotationGenSelectForInwardQuotation(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotationGen
            {              
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Branch=_userLoginInfo.FK_Branch,
                TransMode= TransMode
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Comparison(string mgrp)
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
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            //ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }
        public ActionResult LoadQuotationComparison()
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
            
            return PartialView("_LoadQuotationComparison");
        }
        public ActionResult GetQuotationGenSearchForComparison(string TransModeData)
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
            QuotationModel objQu = new QuotationModel();

            var data = objQu.GetQuotationGenSelectForComparison(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotationGen
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Branch = _userLoginInfo.FK_Branch,
                TransMode= TransModeData
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetQuotationComparisonList(int QuotationGenID,bool isNetAmount=false, bool isBillAmount=false,bool isOtherAmount=false, bool isDiscountAmount = false)
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

            QuotationModel objQu = new QuotationModel();
            var QuInfo = objQu.GetQuotationSelectByQuotationGen(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotationComparison
            {
                FK_QuotationGen = QuotationGenID,
                IsNetAmount= isNetAmount,
                IsBillAmount=isBillAmount,
                IsOtherAmount=isOtherAmount,
                IsDiscountAmount=isDiscountAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser
            });


            return Json(new { QuInfo.Process, QuInfo.Data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateQuotationApproved(QuotationModel.QuotationComparisonData data)
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
            QuotationModel objQu = new QuotationModel();

            var datresponse = objQu.UpdateQuotationComparisonData(input: new QuotationModel.UpdateQuotationComparison
            {
                UserAction = 2,
                TransMode = data.TransMode,
                ID_Quotation = data.ID_Quotation,
                QuoMode = 0,
                Accepted = !data.Approved,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetQuotationInfoComparison(QuotationModel.QuotationGetInfo objQuo)
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
            QuotationModel objQu = new QuotationModel();
            
            var quoItem = objQu.GetQuotationItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.QuotationItemDetails
            {
                ID_Quotation = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuoMode = 0
            });

            var quoWar = objQu.GetQuotationWarrantySelect(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.WarrantyListCriteria
            {
                TransMode = "INQU",
                FK_Product = 0,
                FK_Master = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(new {  quoItem, quoWar}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLeadFill(QuotationModel.Leadfill Data)
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
            QuotationModel purchaseModel = new QuotationModel();

            var datresponse = purchaseModel.Fillead(input: new QuotationModel.Leadfill
            {
                FK_Master = Data.FK_Master,
                IsLead = Data.IsLead,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadOutwardQuotationFromLead(string mtd)
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
            QuotationModel.QuotationViewInit objQu = new QuotationModel.QuotationViewInit();
            //Mode Inward=0
            //Mode Outward=1

            objQu.QuoMode = "1";
            var Category = Common.GetDataViaQuery<QuotationModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project,Mode",
                Criteria = "Cancelled=0 AND Passed=1 AND MODE='P' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            objQu.CategoryList = Category.Data;

            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            objQu.Warrantytype = warrantytype.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.FromLead = 1;

            return PartialView("_AddQuotationOutwardForm", objQu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewQuotationLead(QuotationModel.QuotationNew data)
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
            QuotationModel objQu = new QuotationModel();

            
            QuotationModel.UpdateQuotation objQuo = new QuotationModel.UpdateQuotation();
            objQuo.UserAction = 1;
                objQuo.TransMode = data.TransMode;
            objQuo.LastID = (data.LastID.HasValue) ? data.LastID.Value : 0;
            objQuo.ID_Quotation = 0;
            objQuo.QuoMode = data.QuoMode;
            objQuo.QuoFrom = data.QuoFrom;
            objQuo.FK_Master = data.FK_Master;
            objQuo.QuoDate = data.QuoDate;
            objQuo.QuoExpireDate = data.QuoExpireDate;
            objQuo.QuoBillTotal = data.QuoBillTotal;
            objQuo.QuoDiscount = data.QuoDiscount;
            objQuo.QuoOthercharges = data.QuoOthercharges;
            objQuo.QuoRoundoff = data.QuoRoundoff;
            objQuo.QuoNetAmount = data.QuoNetAmount;
            objQuo.QuoRemarks = data.QuoRemarks;
            objQuo.FK_Company = _userLoginInfo.FK_Company;
            objQuo.FK_Branch = _userLoginInfo.FK_Branch;
            objQuo.FK_Department = _userLoginInfo.FK_Department;
            objQuo.FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser;
            objQuo.EntrBy = _userLoginInfo.EntrBy;
            objQuo.FK_Machine = _userLoginInfo.FK_Machine;
            objQuo.QuotationDetails = data.QuotationDetail is null ? "" : Common.xmlTostring(data.QuotationDetail);
            objQuo.TaxDetails = data.TaxDetails is null ? "" : Common.xmlTostring(data.TaxDetails);
            objQuo.OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails);
            objQuo.WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails);
            objQuo.CustomerName = data.CustomerName;
            objQuo.CustomerPhone = data.CustomerPhone;
            objQuo.CustomerAddress = data.CustomerAddress;
            objQuo.FK_QuotationGen = data.FK_QuotationGen;
            objQuo.QuoReferenceNo = data.QuoReferenceNo;
            objQuo.FK_Quotation = 0;
            objQuo.QuoEntrDate = data.QuoEntrDate;
            Session["UpdateQuotation"] = objQuo;
            return Json(new
            {
                Process = new Output
                {
                    IsProcess = true,
                    Message = new List<string> { "" },
                    Status = "",
                }
            }, JsonRequestBehavior.AllowGet); 
           
        }

        public ActionResult OutwardProject(string mtd, string mgrp)
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
            ViewBag.PageTitle = "Lead Generation";
            var MenuList = Common.GetDataViaQuery<QuotationModel.Menulist>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode",
                Criteria = "Cancelled=0 AND Passed=1 AND ControllerName='Quotation'AND Url='OutWard' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MenuList",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            ViewBag.SharePagetransmode = MenuList.Data;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        public ActionResult LoadOutwardQuotationProject(string mtd)
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
            QuotationModel.QuotationViewInit objQu = new QuotationModel.QuotationViewInit();
            //Mode Inward=0
            //Mode Outward=1

            objQu.QuoMode = "1";
            var Category = Common.GetDataViaQuery<QuotationModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project,Mode",
                Criteria = "Cancelled=0 AND Passed=1 AND MODE='P' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            objQu.CategoryList = Category.Data;

            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            objQu.Warrantytype = warrantytype.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.FromLead = 0;

            return PartialView("_AddQuotationOutwardSunithaForm", objQu);
        }
        public ActionResult GetQuotationCriteriaDetails(QuotationModel.QuotationCriteriaByID data)
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

            QuotationModel objemi = new QuotationModel();
            var QuotationInfos = objemi.GetQuotationCriteriaDetails(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.QuotationCriteriaByID
            {
                FK_Master = data.FK_Master,
                Mode = data.Mode,
            });
            return Json(QuotationInfos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Getpritreportdata(QuatationNumber inputdata)
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

            QuotationModel objQu = new QuotationModel();
            var Mode1data = objQu.Getpritreportdata1(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.Getpritreportinput
            {

                Mode = 1,
                FK_Quotation = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company
            });
            var Mode2data = objQu.Getpritreportdata2(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.Getpritreportinput
            {
                Mode = 2,
                FK_Quotation = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company
            });
            var Mode3data = objQu.Getpritreportdata3(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.Getpritreportinput
            {
                Mode = 3,
                FK_Quotation = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company
            });
            var Mode4data = objQu.Getpritreportdata3(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.Getpritreportinput
            {
                Mode = 4,
                FK_Quotation = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company
            });

            return Json(new { Mode1data, Mode2data, Mode3data, Mode4data }, JsonRequestBehavior.AllowGet);
        }


        #region [GetTemplate]

        /// </summary>
        /// <param name="ID_CommonPrintTemplateData"></param>
        /// <returns></returns>
        public ActionResult GetTemplate(GetTemplateIp inputdata)
        {
            try
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

                CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
                var tepltId = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettings>(parameters: new APIParameters
                {

                    TableName = "CommonPrintingSettings",
                    SelectFields = "TOP(1) ID_CommonPrintingSettings",
                    Criteria = "Cancelled =0 AND Passed=1 AND CommonPrintSettingsMode='" + inputdata.TransMode + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "ID_CommonPrintingSettings DESC",
                    GroupByFileds = ""
                },
                companyKey: _userLoginInfo.CompanyKey);

                if (tepltId.Data != null)
                {
                    QuotationModel modal2 = new QuotationModel();


                    var data = Common.GetDataViaQuery<QuotationModel.GetTemplateCommonPrintOp>(parameters: new APIParameters
                    {

                        TableName = "CommonPrintingSettings",
                        SelectFields = "FrontSideString,FrntImg,PageSize",
                        Criteria = "Cancelled =0 AND CommonPrintSettingsMode='" + inputdata.TransMode + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company + "AND ID_CommonPrintingSettings=" + tepltId.Data[0].ID_CommonPrintingSettings,
                        SortFields = "",
                        GroupByFileds = ""
                    },
                    companyKey: _userLoginInfo.CompanyKey);
                    return Json(data, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    {
                        Process = new Output
                        {
                            IsProcess = false,
                            Message = new List<string> { "No Print Template Found." },
                            Status = "",
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public ActionResult GetComPritReportData(QuatationNumber inputdata)
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

            QuotationModel objQu = new QuotationModel();
            var Mode1data = objQu.GetCompritreportdata1(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetCommPritreportinput
            {

                TransMode = inputdata.TransMode ,
                FK_Master = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company,
                TableCount = 1,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser

            });
            var Mode2data = objQu.GetCompritreportdata2(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetCommPritreportinput
            {
                TransMode = inputdata.TransMode,
                FK_Master = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company,
                TableCount = 2,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser

            });
            var Mode3data = objQu.GetCompritreportdata3(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetCommPritreportinput
            {
                TransMode = inputdata.TransMode,
                FK_Master = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company,
                TableCount = 3,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser

            });
            var Mode4data = objQu.GetCompritreportdata2(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetCommPritreportinput
            {
                TransMode = inputdata.TransMode,
                FK_Master = inputdata.QuatationNum,
                FK_Company = _userLoginInfo.FK_Company,
                TableCount = 4,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser

            });

            return Json(new { Mode1data, Mode2data, Mode3data, Mode4data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOtherCharges(QuotationModel.BindOtherCharges Data)
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

            PurchaseModel purchaseModel = new PurchaseModel();

            var OtherCharges = purchaseModel.FillOtherCharges(input: new PurchaseModel.BindOtherCharge
            {
                TransMode = Data.TransMode,
                FK_Company=_userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=2",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }

    }
}