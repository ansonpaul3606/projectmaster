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
using System;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class IssueOutController : Controller
    {
        // GET: DamageMarking
        public ActionResult IssueOutIndex(string mgrp)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
           // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
           
        }

        [HttpGet]
        public ActionResult LoadIssueOutForm()
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
            IssueOutModel.IssueOutViewList list = new IssueOutModel.IssueOutViewList();

            var OpDepartmentList = Common.GetDataViaQuery<IssueOutModel.Department>(parameters: new APIParameters
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

            var ChangeMod = Common.GetDataViaProcedure<IssueOutModel.IssueMode, IssueOutModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new IssueOutModel.ChangeModeInput { Mode = 29 });
            

            list.IssueModeList = ChangeMod.Data;

         

            return PartialView("_AddIssueOut",list);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewIssueOut(IssueOutModel.IssueOutView data)
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

          

            IssueOutModel IssueOut = new IssueOutModel();

            var datresponse = IssueOut.UpdateIssueOutData(input: new IssueOutModel.UpdateIssueOut
            {
                UserAction = 1
,
                IsoutDate = Convert.ToDateTime( data.ISOutDate),
                IsoutType = data.ISOutType,
                IsOutRemarks = data.ISOutRemarks,
                FK_Department = data.DepartmentID,
                FK_Branch = _userLoginInfo.FK_Branch,
                IssueOutDetails = data.IssueOutDetails is null ? "" : Common.xmlTostring(data.IssueOutDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode=data.TransMode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_IssueOut = 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateIssueOut(IssueOutModel.IssueOutView data)
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

        

            IssueOutModel IssueOut = new IssueOutModel();
            var datresponse = IssueOut.UpdateIssueOutData(input: new IssueOutModel.UpdateIssueOut
            {
                UserAction = 2
    ,
                ID_IssueOut = data.IssueOutID,
                IsoutDate = Convert.ToDateTime(data.ISOutDate),
                IsoutType = data.ISOutType,
                IsOutRemarks = data.ISOutRemarks,
                FK_Department = data.DepartmentID,
                FK_Branch = _userLoginInfo.FK_Branch,
                IssueOutDetails = data.IssueOutDetails is null ? "" : Common.xmlTostring(data.IssueOutDetails),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetIssueOutList(int pageSize, int pageIndex, string Name,string TransModes)
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
           IssueOutModel issueout = new IssueOutModel();
          
            var IssueoutInfo = issueout.GetIssueOutData(companyKey: _userLoginInfo.CompanyKey, input: new IssueOutModel.IssueOutID

            {
                FK_IssueOut = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
              
                TransMode = TransModes
            });

         
            return Json(new { IssueoutInfo.Process, IssueoutInfo.Data, pageSize, pageIndex, totalrecord = (IssueoutInfo.Data is null) ? 0 : IssueoutInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]

        public ActionResult GetIssueOutInfo(IssueOutModel.IssueOutView data)
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


            IssueOutModel objprd = new IssueOutModel();

            var mptableInfo = objprd.GetIssueOutData(companyKey: _userLoginInfo.CompanyKey, input: new IssueOutModel.IssueOutID
            {
                FK_IssueOut = data.IssueOutID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode=data.TransMode
            });

            var subtable = objprd.GetSubTableIssueOutData(companyKey: _userLoginInfo.CompanyKey, input: new IssueOutModel.IssueOutSubSelect
            { FK_IssueOut = data.IssueOutID, Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode=data.TransMode
            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].IssueOutDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteIssueOut(IssueOutModel.IssueOutInfoView data)
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

            IssueOutModel issueOut = new IssueOutModel();


            Output datresponse = issueOut.DeleteIssueOutData(input: new IssueOutModel.DeleteIssueOut
            {
                FK_IssueOut = data.IssueOutID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = "",
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetIssueOutReasonList()
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

  
       // public ActionResult GetProductQty(long ProductID1)
       // {
       //     UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

       //     var data = Common.GetDataViaQuery<IssueOutModel>(parameters: new APIParameters
       //     {
       //         TableName = "Stock",
       //         SelectFields = "CurrentQuantity As CurrentQuantity",
       //         Criteria = "Cancelled =0 AND Passed=1 AND FK_Product='" + ProductID1 + "' AND FK_Company=" + _userLoginInfo.FK_Company,
       //         SortFields = "",
       //         GroupByFileds = ""
       //     },

       //   companyKey: _userLoginInfo.CompanyKey
       //);

       //     return Json(data, JsonRequestBehavior.AllowGet);
       // }

    }
}