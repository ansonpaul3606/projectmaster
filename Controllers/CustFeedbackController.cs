/*----------------------------------------------------------------------  Created By : NIJI TJ  Created On : 10/05/2023  Purpose  : CustFeedback  -------------------------------------------------------------------------  Modification  On   By     OMID/Remarks  -------------------------------------------------------------------------  -------------------------------------------------------------------------*/  using System;
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
    public class CustFeedbackController : Controller
    {
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            //CommonMethod objCmnMethod = new CommonMethod();
            //string mTd = objCmnMethod.Decrypt(mtd);
            //TempData["mTd"] = mTd.ToString();
            return View();
        }
        public ActionResult LoadFeedbackInfo(long masterID, string tmode)
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
            CustFeedbackModel CustFeedback = new CustFeedbackModel();
            var data = CustFeedback.GetCustFeedbackData(companyKey: _userLoginInfo.CompanyKey, input: new CustFeedbackModel.GetCustFeedback
            {
                ID_CustFeedback = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
               
            });

           
            return PartialView("_LoadCustFeedback", data);
        }
        public ActionResult LoadFormCustFeedback()
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


            ServiceListModel.ServiceListView Assign = new ServiceListModel.ServiceListView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var BranchList = Common.GetDataViaQuery<ServiceListModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = " ID_Branch,BrName BranchName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );

            var ComplaintList = Common.GetDataViaQuery<ServiceListModel.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList,CompntName ComplaintName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
          );
            var ChangeMod = Common.GetDataViaProcedure<ServiceListModel.ChangeMode, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 28 });
            //AssignModel. = Lits.Data;
            Assign.StatusModeList = ChangeMod.Data;

            Assign.BranchList = BranchList.Data;
            Assign.ComplaintList = ComplaintList.Data;

            //CommonMethod objCmnMethod = new CommonMethod();
            //string mGrp = objCmnMethod.Decrypt(mgrp);
            //ViewBag.TransMode = mGrp;
            //ViewBag.PageTitle = TempData["mTd"] as string;
            return PartialView("_AddCustFeedback", Assign);
        }
        public ActionResult GetSearchResult(ServiceListModel.TicketInput Data, int pageSize, int pageIndex)
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

            CustFeedbackModel Model = new CustFeedbackModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetTicketDetails(input: new ServiceListModel.TicketInput
            {
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Product = Data.FK_Product,
                EntrBy = _userLoginInfo.EntrBy,
                Status = Data.Status,
                CurrentStatus = Data.CurrentStatus,
                FromDate = Data.FromDate == null ? "" : Data.FromDate,

                ToDate = Data.ToDate == null ? "" : Data.ToDate,
                TicketNumber = Data.TicketNumber,
                FK_Employee = _userLoginInfo.FK_Employee,
                Customer = Data.Customer,

                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            return Json(new { data.Process, data.Data, pageIndex, pageSize, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        
        [HttpGet]
        public ActionResult GetCustFeedbackList()
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

            string transMode = "";

            CustFeedbackModel Feedback = new CustFeedbackModel();

            var data = Feedback.GetFeedbackData(companyKey: _userLoginInfo.CompanyKey, input: new CustFeedbackModel.GetFeedback
            {
                FK_Feedback = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,                 
                TransMode = transMode,
                FromDate=DateTime.Now,
                Mode=1
            });      
            
            return PartialView("_LoadCustFeedback", data.Data);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateCustFeedback(CustFeedbackModel.CustFeedbackView data)
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
            CustFeedbackModel CustFeedback = new CustFeedbackModel();
            
            var dataresponse = CustFeedback.UpdateCustFeedbackData(input: new CustFeedbackModel.UpdateCustFeedback
            {
                UserAction = data.ID_CustFeedback==0?1:2,
                ID_CustFeedback = data.ID_CustFeedback,
                CustFeedbackDetails=data.CustFeedbackDetails is null ? "" : Common.xmlTostring(data.CustFeedbackDetails),
                Remarks=data.Remarks==null?"":data.Remarks,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_CustomerServiceRegister = data.FK_CustomerServiceRegister,
                ID_CustomerServiceRegisterProductDetails = data.ID_CustomerServiceRegisterProductDetails,
                EntrBy = _userLoginInfo.EntrBy,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCustFeedback(CustFeedbackModel.DeleteCustFeedback data)
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

            CustFeedbackModel walkingmodel = new CustFeedbackModel();


            Output datresponse = walkingmodel.DeleteCustFeedbackData(input: new CustFeedbackModel.DeleteCustFeedback
            {
                ID_CustFeedback = data.ID_CustFeedback,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = "",
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustFeedbackReasonList()
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
            return Json(deleteReason, JsonRequestBehavior.AllowGet);       }



       

    }
}