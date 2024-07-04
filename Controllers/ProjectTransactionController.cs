using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.ProjectTransactionModel;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ProjectTransactionController : Controller
    {

        public ActionResult Index(string mtd, string mgrp)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole; 
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod cmnmethd = new CommonMethod();
            string mGrp = cmnmethd.DecryptString(mgrp);
            ViewBag.mtd = mtd;
            ViewBag.TransMode = mGrp;
            Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
           
        }
        public ActionResult LoadProjectTransaction(string mtd, string TransMode)
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
            var FK_Company = _userLoginInfo.FK_Company;
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;


            ProjectTransactionModel model = new ProjectTransactionModel();
            ProjectTransactionModel.ProjectTransactionView inc = new ProjectTransactionModel.ProjectTransactionView();

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND PMMode<>2 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );
            inc.PaymentView = PaymentView.Data;
            //var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            //{
            //    TableName = "BillType",
            //    SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
            //    Criteria = "BTBillType=2 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + " ORDER BY SortOrder asc",
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            // companyKey: _userLoginInfo.CompanyKey
            // );
            var BillTypeListView = model.GetBillTypeInfo(input: new ProjectTransactionModel.GetBillType { Mode = 0,FK_Project=0 , FK_Company = FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            inc.BillTypeListView = BillTypeListView.Data;

            var PettyCashier = model.GetBillTypeInfo(input: new ProjectTransactionModel.GetBillType { Mode = 1, FK_Project = 0 ,FK_Company= FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            
            inc.PettyCashierView = PettyCashier.Data;
            var TrasactionType = model.GetTransactionType(input: new ProjectTransactionModel.ModeLead { Mode = 111 }, companyKey: _userLoginInfo.CompanyKey);
            inc.TransactionTypeList = TrasactionType.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            Common.ClearOtherCharges(TransMode);
            return PartialView("_AddProjectTransaction", inc);
        }

        public void ClearOtherCharge(string TransMode)
        {
            Common.ClearOtherCharges(TransMode);
        }

        #region GetStage
        [HttpPost]
        public ActionResult GetStages(getStagesIP input)
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


            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
                SelectFields = "distinct [ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 And FK_Project ='" + input.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialRequestReportsModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);


            // var Stages = Common.GetDataViaQuery<ProjectTransactionModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);


            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region
        public ActionResult LoadProjectTransactionList(int pageSize, int pageIndex, string Name, string TransMode)
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
            IncentiveModel model = new IncentiveModel();

            ProjectTransactionModel modal = new ProjectTransactionModel();
            var data = modal.LoadProjectTransactionList(companyKey: _userLoginInfo.CompanyKey, input: new ProjectTransactionModel.SelectProjectTransactionIp
            {

                FK_Company = _userLoginInfo.FK_Company,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransMode,
                FK_ProjectTransaction = 0


            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            // return null;
        }
        #endregion

        #region[SelectProjectTransactionbyId]
        [HttpPost]
        public ActionResult SelectProjectTransactionbyId(SelectProjectTransactionIp inputDel)
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

            ProjectTransactionModel model = new ProjectTransactionModel();

            var data = model.LoadProjectTransactionList(input: new ProjectTransactionModel.SelectProjectTransactionIp
            {

                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_ProjectTransaction = inputDel.FK_ProjectTransaction,
                Name = "",
                TransMode = inputDel.TransMode

            }, companyKey: _userLoginInfo.CompanyKey);

            SaleModel Sales = new SaleModel();
            var OtherCharge = Sales.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSubTableSales { FK_Transaction = inputDel.FK_ProjectTransaction, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = "PRTP" });
            var paymentdetail = model.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new ProjectTransactionModel.GetPaymentin
            {
                FK_Master = inputDel.FK_ProjectTransaction,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = inputDel.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data, OtherCharge, paymentdetail }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[DeleteProjectTransaction]
        [HttpPost]
        public ActionResult DeleteProjectTransaction(DeleteCommonPrintingSettingsIP del)
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
            ProjectTransactionModel incentiveModel = new ProjectTransactionModel();

            var datares = incentiveModel.DeleteProjectTransaction(input: new ProjectTransactionModel.DeleteCommonPrintingSettingsIP
            {

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = del.FK_Reason,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = _userLoginInfo.FK_Branch,
                TransMode = del.TransMode,
                FK_ProjectTransaction = del.FK_ProjectTransaction

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datares, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SaveProjectTransaction
        [HttpPost]
        public ActionResult SaveProjectTransaction(UpdateProjectTransactionIp ip)
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
            ProjectTransactionModel incentiveModel = new ProjectTransactionModel();
            var otherCharge = Common.GetOtherCharges(ip.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(ip.TransMode);
            var datares = incentiveModel.SaveProjectTransaction(input: new ProjectTransactionModel.UpdateProjectTransactionDTO
            {

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                Date = ip.Date,
                FK_Project = ip.FK_Project,
                PTPrjTransType = ip.PTPrjTransType,
                FK_PettyCashier= ip.FK_PettyCashier,
                FK_BillType = ip.FK_BillType,
                FK_Employee = ip.FK_Employee,
                FK_ProjectTransaction = ip.FK_ProjectTransaction,
                FK_Stage = ip.FK_Stage,
                NetAmount = ip.NetAmount,
                PTRoundOff = ip.PTRoundOff,
                PTAmount = ip.PTAmount,
                // OtherCharge=ip.OtherCharge,
                Remark = ip.Remark,
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                PaymentDetail = ip.PaymentDetail is null ? "" : Common.xmlTostring(ip.PaymentDetail),
                TransMode = ip.TransMode,
                UserAction = 1,
                Debug = 0,
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                LastID = (ip.LastID.HasValue) ? ip.LastID.Value : 0,
                ID_ProjectTransaction = 0,
                //PaymentType=ip.PaymentType

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datares, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPaymentInfo(ProjectTransactionModel.InputPaymentinfo PaymenyInfo)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectTransactionModel projectTransact = new ProjectTransactionModel();
            var FK_Company = _userLoginInfo.FK_Company;
        //  var PaymentInfo = projectTransact.GetPaymentInformation(companyKey: _userLoginInfo.CompanyKey, input: new ProjectTransactionModel.InputPaymentinfo { , FK_Company = _userLoginInfo.FK_Company });
        var PaymentInfo = projectTransact.GetPaymentInformation(input: new ProjectTransactionModel.InputPaymentinfo { FK_Project = PaymenyInfo.FK_Project, FK_Employee = PaymenyInfo.FK_Employee, FK_Stage = PaymenyInfo.FK_Stage, AsOnDate= PaymenyInfo.AsOnDate, FK_PetyCashier= PaymenyInfo.FK_PetyCashier, FK_Company = FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            var BillType = projectTransact.GetBillTypeInfo(input: new ProjectTransactionModel.GetBillType { Mode = 0, FK_Project = PaymenyInfo.FK_Project , FK_Company = FK_Company, FK_Employee = PaymenyInfo.FK_Employee }, companyKey: _userLoginInfo.CompanyKey);
            var PettyCashier = projectTransact.GetBillTypeInfo(input: new ProjectTransactionModel.GetBillType { Mode = 1, FK_Project = PaymenyInfo.FK_Project , FK_Company = FK_Company, FK_Employee = PaymenyInfo.FK_Employee }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { PaymentInfo, BillType, PettyCashier }, JsonRequestBehavior.AllowGet);
        }
    }

    }