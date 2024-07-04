
using ClosedXML.Excel;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Net.Http;
using System.Web;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Services;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class SalesOrderController : Controller
    {
        public ActionResult Index(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            Common.ClearOtherCharges(ViewBag.TransMode);
            if (Common.GetCorrectionData(mGrp) != "")
            {
                ViewBag.MasterID = Common.GetCorrectionData(mGrp);
                Common.ClearCorrectionData(mGrp);
            }
            return View();
        }

        public ActionResult LoadFormSalesOrder(string mtd, string TransMode)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.isAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            SalesOrderModel salesOrderModel = new SalesOrderModel();
            SalesOrderModel.SalesOrderView salesOrder = new SalesOrderModel.SalesOrderView();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "SalesOrder",
                FieldName = "SortOrder",
                Debug = 0
            });
            salesOrder.SortOrder = a.Data[0].NextNo;

            APIParameters apiParaModule = new APIParameters
            {
                TableName = "[ModuleType]",
                SelectFields = "[ID_ModuleType] AS ID_ModuleType,[ModuleName] AS ModuleName,[Mode] as Mode ",
                Criteria = "Mode='P'",
                GroupByFileds = "",
                SortFields = ""
            };
            var Module = Common.GetDataViaQuery<SalesOrderModel.ModuleType>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaModule);
            salesOrder.ModuleTypeList = Module.Data;

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=2 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey );
           salesOrder.BillTypeListView = BillTypeListView.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name, PMDefault AS PMDefault",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey );
            salesOrder.PaymentView = PaymentView.Data;

            var ProductLocationList = Common.GetDataViaQuery<SalesOrderModel.ProductLocationList>(parameters: new APIParameters
            {
                TableName = "ProductLocation",
                SelectFields = "ID_ProductLocation as FK_ProductLocation,LocationName as ProductLocation",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);
            salesOrder.ProductLocationList = ProductLocationList.Data;

            var CostCenterData=salesOrderModel.GetCostCenterDropdownData(input: new SalesOrderModel.Fk_companyModel { FK_Company= _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            salesOrder.CostCenterList = CostCenterData.Data;

            CommonMethod objCmnMethod = new CommonMethod();           
            ViewBag.PageTilte= objCmnMethod.DecryptString(mtd);

            CommonModel objComm = new CommonModel();
            var genSettings = objComm.GetGeneralSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.GetGeneralSettings
            {
                FK_Company = _userLoginInfo.FK_Company,
                GsModule = "INSOMR"
            });
            ViewBag.INSOMR = !genSettings.Data[0].GsValue;
           
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            //Common.ClearOtherCharges(TransMode);
            Common.ClearOtherCharges("");
            return PartialView("_AddSalesOrderForm", salesOrder);
        }

        [HttpPost]
        public ActionResult GetSalesOrderList(int pageSize, int pageIndex, string Name, string TransModes)
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

            SalesOrderModel SalesOrder = new SalesOrderModel();
            var SaleInfo = SalesOrder.GetSalesOrderData(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.GetSalesOrder
            {
                ID_SalesOrder = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = TransModes,
            });

            return Json(new { SaleInfo.Process, SaleInfo.Data, pageSize, pageIndex, totalrecord = (SaleInfo.Data is null) ? 0 : SaleInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSalesOrderInfo(SalesOrderModel.SalesOrderID objSoID)
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

            SalesOrderModel objSalesOrder = new SalesOrderModel();

            Common.fillOtherCharges("INSO", objSoID.ID_SalesOrder);

            var soInfo = objSalesOrder.GetSalesOrderSelectDetails(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.SalesOrderSelectDetails
            {
                ID_SalesOrder = objSoID.ID_SalesOrder,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var soItem = objSalesOrder.GetSalesOrderItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.SalesOrderItemDetails
            {
                ID_SalesOrder = objSoID.ID_SalesOrder,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var OtherCharge = objSalesOrder.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.GetOtherCharge
            {
                TransMode = "INSO",
                FK_Transaction = objSoID.ID_SalesOrder,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            var EmiItems = objSalesOrder.GetEMIDetailsSelects(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.GetEMIDetailsSelect
            {
                TransMode = "INSO",
                FK_Master = objSoID.ID_SalesOrder,
                Mode = 1,
            });
            var EmiData = objSalesOrder.GetEMIInstallmentDetailsSelects(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.GetEMIDetailsSelect
            {
                TransMode = "INSO",
                FK_Master = objSoID.ID_SalesOrder,
                Mode = 0,
            });
            var paymentdetail = objSalesOrder.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.GetPaymentin
            {   FK_Master = objSoID.ID_SalesOrder,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "INSO",
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
           
            return Json(new { soInfo, soItem, OtherCharge, EmiItems, EmiData, paymentdetail }, JsonRequestBehavior.AllowGet);
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                      
                    pro.SetValue(obj,dr.IsNull(column)?"": dr[column.ColumnName], null);
                   
                    else
                        continue;
                }
            }
            return obj;
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
                QuoMode = 1
            });

            var quoItems = objQu.GetSalesOrderItemDetailsSelectForSalesOrderImport(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.QuotationItemDetails
            {
                ID_Quotation = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuoMode = 1
            });
           

            var OtherCharge = objQu.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetOtherCharge
            {
                TransMode = "INQU",
                FK_Transaction = objQuo.ID_Quotation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(new { quoInfo, quoItems, OtherCharge }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewSalesOrder(SalesOrderModel.SalesOrderViewNew data)
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
            SalesOrderModel SalesOrder = new SalesOrderModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = SalesOrder.UpdateSalesOrderData(input: new SalesOrderModel.UpdateSalesOrder
            {

                UserAction = 1,
                TransMode = data.TransMode,
                SoReferenceNo = data.SoReferenceNo,
                SoDate = data.SoDate,
                SoDeliverydate = data.SoDeliverydate,
                SoAdvcAmount = data.SoAdvcAmount,
                SoBillTotal = data.SoBillTotal,
                SoDiscount = data.SoDiscount,
                SoOthercharges = data.SoOthercharges,
                SoRoundoff = data.SoRoundoff,
                SoNetAmount = data.SoNetAmount,
                TaxtyIntrastate = data.TaxtyIntrastate,
                FK_Customer = data.FK_Customer,
                FK_LeadGenerate = data.FK_LeadGenerate,
                FK_Quotation = data.FK_Quotation,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_SalesOrder = 0,
                SalesOrderDetails = data.SalesOrderDetail is null ? "" : Common.xmlTostring(data.SalesOrderDetail),

               
                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                InstallmenetDetails = data.InstallmentDetails is null ? "" : Common.xmlTostring(data.InstallmentDetails),
                //FK_FinancePlan = data.FK_FinancePlan,
                //EMIDate = data.EMIDate,
                //ProductWise = data.ProductWise,
                AdditionalAmount=data.AdditionalAmount,
                DownPayment=data.DownPayment,
                MobileNo=data.MobileNo,
                FK_AssignedTo = data.FK_AssignedTo,
                FK_BillType = data.BillType,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                FK_CostCenterDetails = (data.CodeCenter_ID.HasValue) ? data.CodeCenter_ID.Value : 0,
                SODescription =data.SODescription,
                FK_Employee = data.FK_Employee

            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSalesOrder(SalesOrderModel.SalesOrderViewNew data)
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


            SalesOrderModel SalesOrder = new SalesOrderModel();

            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            var datresponse = SalesOrder.UpdateSalesOrderData(input: new SalesOrderModel.UpdateSalesOrder
            {
                UserAction = 2,
                TransMode = data.TransMode,
                SoReferenceNo = data.SoReferenceNo,
                SoDate = data.SoDate,
                SoDeliverydate = data.SoDeliverydate,
                SoAdvcAmount = data.SoAdvcAmount,
                SoBillTotal = data.SoBillTotal,
                SoDiscount = data.SoDiscount,
                SoOthercharges = data.SoOthercharges,
                SoRoundoff = data.SoRoundoff,
                SoNetAmount = data.SoNetAmount,
                TaxtyIntrastate = data.TaxtyIntrastate,
                FK_Customer = data.FK_Customer,
                FK_LeadGenerate = data.FK_LeadGenerate,
                FK_Quotation = data.FK_Quotation,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_SalesOrder = data.ID_SalesOrder,
                SalesOrderDetails = data.SalesOrderDetail is null ? "" : Common.xmlTostring(data.SalesOrderDetail),
              
                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),

                ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                InstallmenetDetails = data.InstallmentDetails is null ? "" : Common.xmlTostring(data.InstallmentDetails),
                //FK_FinancePlan = data.FK_FinancePlan,
                //EMIDate = data.EMIDate,
                //ProductWise = data.ProductWise,
                AdditionalAmount = data.AdditionalAmount,
                DownPayment = data.DownPayment,
                MobileNo = data.MobileNo,
                FK_AssignedTo = data.FK_AssignedTo,
                FK_BillType = data.BillType,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                FK_CostCenterDetails = (data.CodeCenter_ID.HasValue) ? data.CodeCenter_ID.Value : 0,
                SODescription = data.SODescription,
                FK_Employee = data.FK_Employee
            }, companyKey: _userLoginInfo.CompanyKey);
            if (datresponse.IsProcess)
            {

                Common.ClearOtherCharges(data.TransMode);
            }
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSalesOrder(SalesOrderModel.SalesOrderInfoView data)
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

            SalesOrderModel SalesOrder = new SalesOrderModel();

            var datresponse = SalesOrder.DeleteSalesOrderData(input: new SalesOrderModel.DeleteSalesOrder
            {
                ID_SalesOrder = data.SalesOrderID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                TransMode=data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSalesOrderReasonList()
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

        public ActionResult GetOtherCharges(SalesOrderModel.BindOtherCharge Data)
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
            SalesOrderModel salesOrder = new SalesOrderModel();
            var OtherCharges = salesOrder.FillOtherCharges(input: new SalesOrderModel.BindOtherCharge
            {
                TransMode = Data.TransMode,

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

        [HttpGet]
        public ActionResult GetQuotationList()
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
            var QuotationInfo = objQu.GetQuotationPopupData(companyKey: _userLoginInfo.CompanyKey, input: new QuotationModel.GetQuotationPopup
            {
                ID_Quotation = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                QuoMode = 1
            });



            return Json(QuotationInfo, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult GetProductEMI(SalesOrderModel.ProductDetails data)
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

            SalesOrderModel objproduct = new SalesOrderModel();
            var EMIInfo = objproduct.GetProductEMIData(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.GetProductEMIPlans
            {
                ProductID = data.ID_Product,
                Amount=data.Amount,
                FK_Company = _userLoginInfo.FK_Company,                
            });
            return Json(EMIInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EMICalculate(SalesOrderModel.ProductEMICalc data)
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

            SalesOrderModel objproduct = new SalesOrderModel();
            var EMIInfo = objproduct.EMICalculate(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.CalculateEMI
            {
                FK_FinancePlan = data.FK_FinancePlan,
                Date=data.EMIDate,
                Downpayment=data.Downpayment,
                AdditionalAmount=data.AdditionalAmount,
                Installment=data.Installment,
                BillAmount=data.BillAmount,
            });
            return Json(EMIInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOtherCharges(PurchaseModel.BindOtherCharge Data)
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

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=1",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLeadFill(SalesOrderModel.Leadfill Data)
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
            SalesOrderModel purchaseModel = new SalesOrderModel();

            SalesOrderModel.LeadFillSalesOrder objsalesorder = new SalesOrderModel.LeadFillSalesOrder();
       
            
            var datresponse = purchaseModel.Fillead(input: new SalesOrderModel.Leadfill
            {
                FK_Master = Data.FK_Master,
                IsLead = Data.IsLead,
            }, companyKey: _userLoginInfo.CompanyKey);
            List<SalesOrderModel.LeadFillReSalesOrder> objresult = new List<SalesOrderModel.LeadFillReSalesOrder>();
           if(datresponse.Data.Count>0)
            {
                foreach(var temp in datresponse.Data)
                {
                    SalesOrderModel.LeadFillReSalesOrder objresalesorder = new SalesOrderModel.LeadFillReSalesOrder();
                    objresalesorder.SlNo = temp.SLNo;
                    objresalesorder.FK_Product = temp.ProductID;
                    objresalesorder.ProdName = temp.ProName;
                    objresalesorder.SodSalQuantity = temp.SpdSalQuantity;
                    objresalesorder.SodSalDiscount = temp.Discamt;                    
                    objresalesorder.SodMRP = temp.MRPs;
                    objresalesorder.StockId = temp.StockId;
                    objresalesorder.SodSalDiscountPercent = temp.Discp;
                    objresalesorder.SodSalTaxAmount = temp.TaxAmount;
                    objresalesorder.SodSalTotalAmount = temp.NetAmt;
                    objresalesorder.Sprice = temp.SalePrice;
                    objresalesorder.SodSalPrice = temp.SalePrice;
                    objresalesorder.FK_ProductLocation = temp.FK_ProductLocation;

                    objresult.Add(objresalesorder);
                }
           }

           return Json(objresult, JsonRequestBehavior.AllowGet);
        }
        #region[ExportSalesOrder]
        [HttpPost]
        public ActionResult ExportSalesOrder(int ID_SalesOrder)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            SalesOrderModel objSalesOrder = new SalesOrderModel();
            var soInfo = objSalesOrder.GetSalesOrderSelectDetails(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.SalesOrderSelectDetails
            {
                ID_SalesOrder = ID_SalesOrder,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var soItem = objSalesOrder.GetSalesOrderItemDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new SalesOrderModel.SalesOrderItemDetails
            {
                ID_SalesOrder = ID_SalesOrder,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            DataTable custTable = new DataTable("Sales Order");
            DataColumn dtColumn;
            DataRow myDataRow;

          
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "SoNo";
            dtColumn.Caption = "Sales Order No#";

            custTable.Columns.Add(dtColumn);

           
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "SoDate";
            dtColumn.Caption = " Sales Order Date ";

            custTable.Columns.Add(dtColumn);

  
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Costcenter";
            dtColumn.Caption = "Cost center/Salesman";

            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "CusName";
            dtColumn.Caption = "Customer Name";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "MobileNo";
            dtColumn.Caption = "Mobile No";
            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Address";
            dtColumn.Caption = "Address";
            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "SoDeliverydate";
            dtColumn.Caption = "Delivery Date";

            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "ProdCode";
            dtColumn.Caption = "Product Code";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "ProdName";
            dtColumn.Caption = "Product";
            custTable.Columns.Add(dtColumn);           


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Floor";
            dtColumn.Caption = "Floor";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SodSalQuantity";
            dtColumn.Caption = "Quantity";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SodMRP";
            dtColumn.Caption = "MRP ";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SodSalPrice";
            dtColumn.Caption = "Sale Price";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SodSalDiscountPercent";
            dtColumn.Caption = "Disc %";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SodSalDiscount";
            dtColumn.Caption = "Disc Amount";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SodSalTotalAmount";
            dtColumn.Caption = "Total";

            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "SodRemarks";
            dtColumn.Caption = "Description";
            custTable.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SoBillTotal";
            dtColumn.Caption = "Bill Total ";

            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SoOthercharges";
            dtColumn.Caption = "Other Charges ";

            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SoDiscount";
            dtColumn.Caption = "Discount";

            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SoAdvcAmount";
            dtColumn.Caption = "Adv. Amount ";

            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(Decimal);
            dtColumn.ColumnName = "SoNetAmount";
            dtColumn.Caption = "Net Amount ";
            custTable.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(String);
            dtColumn.ColumnName = "Narration";
            dtColumn.Caption = "Narration";
            custTable.Columns.Add(dtColumn);

            myDataRow = custTable.NewRow();
            //myDataRow["SoNo"] = soInfo.Data[0].SoNo;
            //myDataRow["SoDate"] = soInfo.Data[0].FSoDate;
            //myDataRow["Costcenter"] = soInfo.Data[0].CCName;
            //myDataRow["CusName"] = soInfo.Data[0].Customer;           
            //myDataRow["MobileNo"] = soInfo.Data[0].Mobile;
            //myDataRow["Address"] = soInfo.Data[0].Address;
            //myDataRow["SoDeliverydate"] = soInfo.Data[0].FSoDeliverydate;
            //myDataRow["SoBillTotal"] = soInfo.Data[0].SoBillTotal;
            //myDataRow["SoOthercharges"] = soInfo.Data[0].SoOthercharges;
            //myDataRow["SoDiscount"] = soInfo.Data[0].SoDiscount;
            //myDataRow["SoAdvcAmount"] = soInfo.Data[0].SoAdvcAmount;
            //myDataRow["SoNetAmount"] = soInfo.Data[0].SoNetAmount;
            //myDataRow["Narration"] = soInfo.Data[0].SODescription;
            
            var num = 0;
            foreach (var item in soItem.Data)
            {
                if (num != 0)
                {
                    myDataRow = custTable.NewRow();
                }
                myDataRow["ProdCode"] = item.ProdCode;
                myDataRow["ProdName"] = item.ProdName;               
                myDataRow["Floor"] = item.Location;
                myDataRow["SodSalQuantity"] = item.SodSalQuantity;
                myDataRow["SodMRP"] = item.SodMRP;
                myDataRow["SodSalPrice"] = item.SodSalPrice;
                myDataRow["SodSalDiscountPercent"] = item.SodSalDiscountPercent;
                myDataRow["SodSalDiscount"] = item.SodSalDiscount;
                myDataRow["SodSalTotalAmount"] = item.SodSalTotalAmount;
                myDataRow["SodRemarks"] = item.SodRemarks;

                myDataRow["SoNo"] = soInfo.Data[0].SoNo;
                myDataRow["SoDate"] = soInfo.Data[0].FSoDate;
                myDataRow["Costcenter"] = soInfo.Data[0].CCName;
                myDataRow["CusName"] = soInfo.Data[0].Customer;
                myDataRow["MobileNo"] = soInfo.Data[0].Mobile;
                myDataRow["Address"] = soInfo.Data[0].Address;
                myDataRow["SoDeliverydate"] = soInfo.Data[0].FSoDeliverydate;
                myDataRow["SoBillTotal"] = soInfo.Data[0].SoBillTotal;
                myDataRow["SoOthercharges"] = soInfo.Data[0].SoOthercharges;
                myDataRow["SoDiscount"] = soInfo.Data[0].SoDiscount;
                myDataRow["SoAdvcAmount"] = soInfo.Data[0].SoAdvcAmount;
                myDataRow["SoNetAmount"] = soInfo.Data[0].SoNetAmount;
                myDataRow["Narration"] = soInfo.Data[0].SODescription;

                custTable.Rows.Add(myDataRow);
                num++;
            }
            
            using (XLWorkbook wb = new XLWorkbook())
            {
               var ws= wb.Worksheets.Add(custTable);  
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {                   
                    wb.SaveAs(MyMemoryStream);
                    MemoryStream copyStream = new MemoryStream(MyMemoryStream.ToArray());
                    copyStream.Position = 0;
                    return File(copyStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalesOrder.xlsx");
                }
            }        
         }
        #endregion
    }
}