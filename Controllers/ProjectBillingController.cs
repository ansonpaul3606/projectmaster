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
using System.IO;
using static PerfectWebERP.Models.ProjectBillingModel;

namespace PerfectWebERP.Controllers
{
    public class ProjectBillingController : Controller
    {
        // GET: ProjectBill
        public ActionResult ProjectBillingIndex(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            CommonMethod cmnmethd = new CommonMethod();
            string mGrp = cmnmethd.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = cmnmethd.DecryptString(mtd);
            Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
        }
        public ActionResult LoadFormProjectBilling(string mtd)
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
            ProjectBillingModel.ProjectBillingView projectBilling = new ProjectBillingModel.ProjectBillingView();

            ProjectBillingModel bill = new ProjectBillingModel();

            var BillModeList = bill.GetBillModeList(input: new ProjectBillingModel.ModeValue { Mode = 74 },
            companyKey: _userLoginInfo.CompanyKey);
            projectBilling.PrBillModeList = BillModeList.Data;

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType = 5 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            projectBilling.BillTypeListView = BillTypeListView.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            projectBilling.PaymentView = PaymentView.Data;


            var SettingView = Common.GetDataViaQuery<ProjectBillingModel.SettingNameList>(parameters: new APIParameters
            {
                TableName = "CommonPrintingSettings",
                SelectFields = "ID_CommonPrintingSettings  ,SettingsName ",
                Criteria = "cancelled=0 AND Passed =1 ",
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            projectBilling.SettingNameList = SettingView.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            var perfomadata = Common.GetDataViaQuery<LeadGenerateModel.MRPEdit>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'PROJ' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='PROJ003'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey);
            ViewBag.Performa = perfomadata.Data[0].GsValue;

            return PartialView("_AddProjectBillingForm", projectBilling);
     }

        public ActionResult GetProjectBillingList(int pageSize, int pageIndex, string Name, string TransModes)
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

            ProjectBillingModel Projectbill = new ProjectBillingModel();
            var MakerInfo = Projectbill.GetProjectBillingData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectBillingModel.GetProjectBillingDetails
            {
                FK_ProjectBilling = 0,
                TransMode = TransModes,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

            });

            return Json(new { MakerInfo.Process, MakerInfo.Data, pageSize, pageIndex, totalrecord = (MakerInfo.Data is null) ? 0 : MakerInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProjectBillingInfo(ProjectBillingModel.InputProjectBilling data)
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

            //Vehicles vehic = new Vehicles();
            ProjectBillingModel Projectbill = new ProjectBillingModel();

            var modelInfo = Projectbill.GetProjectBillingData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectBillingModel.GetProjectBillingDetails
            {
                FK_ProjectBilling = data.ID_ProjectBilling,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });
            var OtherCharge = Projectbill.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new ProjectBillingModel.GetSubTableSales
            {
                FK_Transaction = data.ID_ProjectBilling,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });

            decimal amnt = 0; bool IncludeTax = false; long FK_TaxGroup = 0;
            if (modelInfo != null)
            {
                if (modelInfo.Data.Count > 0)
                {
                    amnt = Convert.ToDecimal(modelInfo.Data[0].TaxableAmount);
                    IncludeTax = modelInfo.Data[0].IncludeTax;
                    FK_TaxGroup = Convert.ToInt64(modelInfo.Data[0].FK_TaxGroup);
                }
            }
            var TaxDetails = Projectbill.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new ProjectBillingModel.TaxCalculation
            {
                FK_Product = 0,
                Amount = amnt,
                IncludeTax = IncludeTax,
                Sales = 0,
                Quantity = 0,
                FK_TaxGroup = FK_TaxGroup
            });

        
            var paymentdetail = Projectbill.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new ProjectBillingModel.GetPaymentin
            {
                FK_Master = data.ID_ProjectBilling,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            return Json(new { modelInfo, OtherCharge, TaxDetails, paymentdetail }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewProjectBilling(ProjectBillingModel.InputProjectBilling data)
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
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            ProjectBillingModel projectbilling = new ProjectBillingModel();
            var datresponse = projectbilling.UpdateProjectBillingData(input: new ProjectBillingModel.UpdateProjectBilling
            {
                UserAction = data.UserAction,
                ID_ProjectBilling = data.ID_ProjectBilling,
                FK_Project = data.FK_Project,
                PrBillDate = data.PrBillDate,
                PrBillMode = data.PrBillMode,
                FK_BillType = data.FK_BillType,
                PrBillAmount = data.PrBillAmount,
                PrOtherCharges = data.PrOtherCharges,
                PrAdvAmount = 0,
                PrTaxAmount = data.PrTaxAmount,
                PrNetAmount = data.PrNetAmount,
                TaxList = data.TaxList is null ? "" : Common.xmlTostring(data.TaxList),
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PrBillRoundOff = data.RoundOff,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                PrPerforma = data.PrPerforma,
                PrDownPayment = data.PrDownPayment,
                PrAddAmount = data.PrAddAmount,
            }, companyKey: _userLoginInfo.CompanyKey);
            Common.ClearOtherCharges(data.TransMode);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateProjectBilling(ProjectBillingModel.InputProjectBilling data)
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

            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            ProjectBillingModel projectbilling = new ProjectBillingModel();
            var datresponse = projectbilling.UpdateProjectBillingData(input: new ProjectBillingModel.UpdateProjectBilling
            {
                UserAction = data.UserAction,
                ID_ProjectBilling = data.ID_ProjectBilling,
                FK_Project = data.FK_Project,
                PrBillDate = data.PrBillDate,
                PrBillMode = data.PrBillMode,
                FK_BillType = data.FK_BillType,
                PrBillAmount = data.PrBillAmount,
                PrOtherCharges = data.PrOtherCharges,
                PrAdvAmount = data.PrAdvAmount,
                PrTaxAmount = data.PrTaxAmount,
                PrNetAmount = data.PrNetAmount,
                TaxList = data.TaxList is null ? "" : Common.xmlTostring(data.TaxList),
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PrBillRoundOff = data.RoundOff,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                PrPerforma = data.PrPerforma,
                PrDownPayment = data.PrDownPayment,
                PrAddAmount = data.PrAddAmount,
            }, companyKey: _userLoginInfo.CompanyKey);
            Common.ClearOtherCharges(data.TransMode);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetOtherCharges(SaleModel.BindOtherCharge Data)
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

            ProjectBillingModel projectBill = new ProjectBillingModel();

            var OtherCharges = projectBill.FillOtherCharges(input: new ProjectBillingModel.BindOtherCharge
            {
                TransMode = Data.TransMode,
                FK_Company= _userLoginInfo.FK_Company,
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

        public ActionResult GetTaxAmountNew(ProjectBillingModel.BindTaxAmountNew Data)
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

            ProjectBillingModel getTax = new ProjectBillingModel();

            var datresponse = getTax.GetTaxDetailsNew(input: new ProjectBillingModel.TaxCalculation
            {
                FK_Product = 0,
                Amount = Data.Amount,
                IncludeTax = Data.Includetax,
                Sales = 0,
                Quantity = 0,
                FK_TaxGroup = Data.FK_TaxGroup
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProjectBillingInfo(ProjectBillingModel.InputProjectBilling data)
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

            ProjectBillingModel.DeleteProjectBilling deleteprojectbill = new ProjectBillingModel.DeleteProjectBilling
            {
                FK_ProjectBilling = data.ID_ProjectBilling,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID
            };

            Output dataresponse = Common.UpdateTableData<ProjectBillingModel.DeleteProjectBilling>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProProjectBillingDelete", parameter: deleteprojectbill);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult ShowBuyBack()
        //{
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    ProjectBillingModel BuyBack = new ProjectBillingModel();
        //    var data = Common.GetDataViaQuery<ProjectBillingModel.BuyBack>(parameters: new APIParameters
        //    {
        //        TableName = "GeneralSettings",
        //        SelectFields = "GsValue,GsField",
        //        Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND GsField='BuyBck'",
        //        SortFields = "",
        //        GroupByFileds = ""
        //    },
        //    companyKey: _userLoginInfo.CompanyKey);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetProjectBillingDeleteReasonList()
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
      
        public ActionResult ProjectBilling_Invoice(ProjectBillingModel.ProjectBillingInvoiceIP data)
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

  
            ProjectBillingModel modal = new ProjectBillingModel();
            var data1 = modal.Pro_ProjectBillingInvoice_table1(input: new ProjectBillingModel.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 1

            }, companyKey: _userLoginInfo.CompanyKey);

            var data2 = modal.Pro_ProjectBillingInvoice_table2(input: new ProjectBillingModel.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 2

            }, companyKey: _userLoginInfo.CompanyKey);

            var data3 = modal.Pro_ProjectBillingInvoice_table3(input: new ProjectBillingModel.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 3

            }, companyKey: _userLoginInfo.CompanyKey);


   

             return Json(new { data1, data2, data3 }, JsonRequestBehavior.AllowGet);
           

        }

        [HttpPost]
        public ActionResult GetCommonPrintElements()
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

            CommonPrintSettingsModal Modal = new CommonPrintSettingsModal();

            var BranchList = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingElemets>(parameters: new APIParameters
            {
                TableName = "CommonPrintSettingElemets",
                SelectFields = "ElementName,html,Id",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  IsActive = 1 AND TransMode ='Sales' AND TransType=" + 1,
                SortFields = "",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            var DetailsSection1 = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingElemets>(parameters: new APIParameters
            {
                TableName = "CommonPrintSettingElemets",
                SelectFields = "ElementName,html,Id",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  IsActive = 1 AND TransMode ='Sales' AND TransType=" + 2,
                SortFields = "",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            var DetailsSection2 = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingElemets>(parameters: new APIParameters
            {
                TableName = "CommonPrintSettingElemets",
                SelectFields = "ElementName,html,Id",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  IsActive = 1 AND TransMode ='Sales' AND TransType=" + 3,
                SortFields = "",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { BranchList, DetailsSection1, DetailsSection2 }, JsonRequestBehavior.AllowGet);

        }


    }
}