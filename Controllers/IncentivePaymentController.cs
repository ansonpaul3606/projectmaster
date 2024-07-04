using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class IncentivePaymentController : Controller
    {
        // GET: IncentivePayment
        public ActionResult IncentivePayment(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            return View();
        }

        public ActionResult LoadFormIncentivePayment(string mtd)
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
            CommonMethod objCmnMethod = new CommonMethod();
            IncentivePaymentModel.IncentivePaymentViewlist statusobj = new IncentivePaymentModel.IncentivePaymentViewlist();
            var Category = Common.GetDataViaQuery<IncentivePaymentModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.CategoryList = Category.Data;

            var branch = Common.GetDataViaQuery<IncentivePaymentModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;
            var Department = Common.GetDataViaQuery<IncentivePaymentModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department ,DeptName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company/*+"FK_Branch="+_userLoginInfo.FK_Branch*/,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey );
            statusobj.DepartmentList = Department.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name,PMDefault AS PMDefault",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + " AND PMMode!=2 AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.PaymentView = PaymentView.Data;

            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddIncentivePayment", statusobj);
        }


        public ActionResult GetDepartmentByBranchList(long FK_Branch)
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

            IncentiveSettingsMasterModel objpaymode = new IncentiveSettingsMasterModel();
            var Department = Common.GetDataViaQuery<IncentivePaymentModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department ,DeptName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company /*+ "FK_Branch=" + FK_Branch*/,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(Department, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncentivePaymentDetails(IncentivePaymentModel.IncentivePaymentview data)
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

            IncentivePaymentModel objpaymode = new IncentivePaymentModel();
            var IncentiveAmount = objpaymode.GetIncentivePaymentdata(companyKey: _userLoginInfo.CompanyKey, input: new IncentivePaymentModel.GetIncentivePaymentview
            {
                FK_Employee = data.FK_Employee,
                AsOnDate = data.AsOnDate,
                FK_Company = _userLoginInfo.FK_Company,

            });
            return Json(IncentiveAmount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddIncentivePayment(IncentivePaymentModel.IncentivePaymentviewDetails data)
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
            IncentivePaymentModel incpay = new IncentivePaymentModel();
            var datresponse = incpay.UpdateIncentivePaymentData(input: new IncentivePaymentModel.UpdateIncentivePayment
            {
                UserAction = 1,
                ID_IncentivePayment = data.ID_IncentivePayment,
                AsonDate = data.AsonDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                FK_Employee = data.FK_Employee,
                TransMode = data.TransMode,
                IPAmount = data.IPAmount,
                IncentivePaymentDetails = data.IncentivePaymentDetails is null ? "" : Common.xmlTostring(data.IncentivePaymentDetails),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,                
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetIncentivePaymentList(int pageSize, int pageIndex, string Name, string TransModes)
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

            IncentivePaymentModel incpay = new IncentivePaymentModel();
            var Info = incpay.GetIncentivePaymentData(companyKey: _userLoginInfo.CompanyKey, input: new IncentivePaymentModel.GetIncentivepayment
            {                
                IPGroupID = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = TransModes,
                Mode = 0,
            });
            return Json(new { Info.Process, Info.Data, pageSize, pageIndex, totalrecord = (Info.Data is null) ? 0 : Info.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetIncentivePaymentInfo(IncentivePaymentModel.InputIncentivePaymentDetails data)
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

            IncentivePaymentModel incpay = new IncentivePaymentModel();


            var Maindata = incpay.GetIncentivePaymentDataInfo(companyKey: _userLoginInfo.CompanyKey, input: new IncentivePaymentModel.GetIncentivepayment
            {
                IPGroupID = data.IPGroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Mode = 0

            });

            var Detaildata = incpay.GetPaymentDetailDataFill(companyKey: _userLoginInfo.CompanyKey, input: new IncentivePaymentModel.GetIncentivepayment
            {                
                IPGroupID = data.IPGroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                Mode = 1
            });

            var paymentdetail = incpay.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new IncentivePaymentModel.GetPaymentin
            {
                FK_Master = data.IPGroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            return Json(new { Maindata, Detaildata, paymentdetail }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeleteReasonList()
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
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteIncentivePayment(IncentivePaymentModel.InputIncentivePaymentDetails data)
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


            IncentivePaymentModel incpay = new IncentivePaymentModel();
            var datresponse = incpay.DeleteIncentivePaymentData(input: new IncentivePaymentModel.DeleteIncentivePayment
            {
                IPGroupID = data.IPGroupID,
                TransMode=data.TransMode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}