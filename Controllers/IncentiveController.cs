using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class IncentiveController : Controller
    {
        // GET: Incentive
        public ActionResult Index(string mgrp)
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

            return View();
        }

        #region[LoadIncentiveform]
        public ActionResult LoadIncentiveform()
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


            IncentiveModel model = new IncentiveModel();
            IncentiveModel.IncentiveInitView inc = new IncentiveModel.IncentiveInitView();

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );
            inc.PaymentView = PaymentView.Data;

            return PartialView("_AddIncentiveform", inc);
        }
        #endregion


        #region[GetDatafill]
        [HttpPost]
        public ActionResult GetDatafill(IncentiveModel.IncentiveView data)
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

            IncentiveModel incentive = new IncentiveModel();

            var result = incentive.GetfillData(input: new IncentiveModel.Modedata
            {
                Mode = data.IncModule,
                FK_Master = data.FK_Master

            }, companyKey: _userLoginInfo.CompanyKey);

            //get id
             var moduleList=result.Data.Select(a => a.Module).Distinct().ToList();
            List<IncentiveModel.IncentiveViewTable> inct = new List<IncentiveModel.IncentiveViewTable>();
            foreach (var i in moduleList)
            {

                var module = result.Data.Where(a => a.Module == i).FirstOrDefault();

                inct.Add(new IncentiveModel.IncentiveViewTable
                {

                    Module = module.Module,
                    IncModule = module.IncModule,
                    Modoutputs = result.Data
                                    .Where(a => a.Module == i)
                                    .Select(b => new IncentiveModel.IncentiveSubViewOutputTable
                                    {
                                        IncTrDetIncAmount = b.Amount,
                                        IncTrDetRemarks = b.Description,
                                        IncTrDetDescription = b.Module,
                                        IModule=b.IncModule,
                                        Party = b.Party,
                                        IncTrDetIncPercentage = b.Percentage,
                                        MasterType = b.MasterType,
                                        FK_MasterID = b.FK_MasterID
                                    }).ToList()

                });

            }

            //create variable

            APIGetRecordsDynamic<IncentiveModel.IncentiveViewTable> output = new APIGetRecordsDynamic<IncentiveModel.IncentiveViewTable>();
            output.Process = result.Process;
            output.Data = inct;
            return Json(output, JsonRequestBehavior.AllowGet);

            //List<IncentiveModel.IncentiveView> inct = result.Data.Select(r => new IncentiveModel.IncentiveView
            //{
            //    Module = r.Module,
            //    Description = r.Description,
            //    Percentage = r.Percentage,
            //    Party = r.Party,
            //    Amount = r.Amount
            //}).ToList();
        }



        #endregion

        #region[AddIncentivedata]
        public ActionResult AddIncentivedata(IncentiveModel.IncentiveView view)
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

            IncentiveModel incentiveModel = new IncentiveModel();

            var datares = incentiveModel.UpdateIncentivedetails(input: new IncentiveModel.UpdateIncentive
            {
                UserAction = 1,
                TransMode = view.TransMode,
                Debug = 0,
                ID_IncentiveTransaction = 0,
                TransDate = view.TransDate,
                IncTrType= view.IncTrType,
                FK_Master = view.FK_Master,
                EntrBy = _userLoginInfo.EntrBy,
                FK_IncentiveTransaction = 0,
                IncTrProfitAmount = view.IncTrProfitAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                //IncModule = view.IncModule,
                //ReferenceNo = view.ReferenceNo,
                IncNetAmount = view.IncNetAmount,
                IncTrTotalAmount = view.IncTrTotalAmount,
                IncTrDividentPercent = view.IncTrDividentPercent,
                IncentiveTransactionDetails = view.Modoutputs is null ? "" : Common.xmlTostring(view.Modoutputs),
                PaymentDetail = view.PaymentDetail is null ? "": Common.xmlTostring(view.PaymentDetail)

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetIncentiveformList]
        public ActionResult GetIncentiveformList(int pageSize, int pageIndex, string Name, string TransMode)
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

            var data = model.GetIncentiveData(input: new IncentiveModel.GetIncentivedetails
            {
                TransMode = TransMode,
                FK_IncentiveTransaction = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                Name = Name,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 0

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[GetIncentiveReasonList]
        public ActionResult GetIncentiveReasonList()
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
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[DeleteIncentivePay]
        public ActionResult DeleteIncentivePay(IncentiveModel.IncentiveInputDel del)
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
            IncentiveModel incentiveModel = new IncentiveModel();

            var datares = incentiveModel.DelIncentiveData(input: new IncentiveModel.DeleteIncentiveData
            {
                TransMode= del.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_IncentiveTransaction = del.ID_IncentiveTransaction,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = del.ReasonID,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[GetIncentiveInfoByID]
        [HttpPost]
        public ActionResult GetIncentiveInfoByID(IncentiveModel.IncentiveView inputDel)
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

            IncentiveModel model = new IncentiveModel();

            var data = model.GetIncentiveDataGrid(input: new IncentiveModel.GetIncentivedetails
            {
                TransMode =inputDel.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_IncentiveTransaction = inputDel.ID_IncentiveTransaction,
                Detailed = 1
            }, companyKey: _userLoginInfo.CompanyKey);

            var paymentdetail = model.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveModel.GetPaymentin
            {
                FK_Master = inputDel.ID_IncentiveTransaction,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = inputDel.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });



            var moduleList = data.Data.Select(a => a.IncTrDetDescription).Distinct().ToList();
            List<IncentiveModel.IncentiveViewTable> inct = new List<IncentiveModel.IncentiveViewTable>();
            foreach (var i in moduleList)
            {

                var module = data.Data.Where(a => a.IncTrDetDescription == i).FirstOrDefault();

                inct.Add(new IncentiveModel.IncentiveViewTable
                {

                    Module = module.IncTrDetDescription,
                    IncModule = module.FK_IncentiveTransaction,
                    Modoutputs = data.Data
                                    .Where(a => a.IncTrDetDescription == i)
                                    .Select(b => new IncentiveModel.IncentiveSubViewOutputTable
                                    {
                                        IncTrDetIncAmount = b.IncTrDetIncAmount,
                                        IncTrDetRemarks = b.IncTrDetRemarks,
                                        IncTrDetDescription = b.IncTrDetDescription,
                                        IModule = b.FK_IncentiveTransaction,
                                        Party = b.Party,
                                        IncTrDetPayWithSalalry = b.IncTrDetPayWithSalalry,
                                        IncTrDetPayAsCash = b.IncTrDetPayAsCash,

                                        IncTrDetIncPercentage = b.IncTrDetIncPercentage,
                                        MasterType = b.IncTrDetMasterType,
                                        FK_MasterID = b.FK_MasterID
                                    }).ToList()

                });

            }

            APIGetRecordsDynamic<IncentiveModel.IncentiveViewTable> output = new APIGetRecordsDynamic<IncentiveModel.IncentiveViewTable>();
            output.Process = data.Process;
            output.Data = inct;

            return Json(new { output, paymentdetail }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}