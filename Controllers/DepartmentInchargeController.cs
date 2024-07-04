using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class DepartmentInchargeController : Controller
    {
        // GET: DepartmentIncharge
        public ActionResult Index(string mtd, string mgrp)
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
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.TransMode = mGrp;
            return View();
        }

        #region[LoadDepartmentIncharge]
        public ActionResult LoadDepartmentIncharge(string mtd, string TransMode)
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

            DepartmentInchargeModel model = new DepartmentInchargeModel();
            DepartmentInchargeModel.DepartmentInchargeView view = new DepartmentInchargeModel.DepartmentInchargeView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            var branchlist = Common.GetDataViaQuery<DepartmentInchargeModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            view.Branchlists = branchlist.Data;

            var deptlist = Common.GetDataViaQuery<DepartmentInchargeModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DeptID,DeptName AS DeptName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey
           );
            view.deptlists = deptlist.Data;

            return PartialView("_AddDepartmentIncharge", view);

        }

        #endregion

        #region[AddDepartmentIncharge]
        public ActionResult AddDepartmentIncharge(DepartmentInchargeModel.DepartmentInchargeView view)
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

            DepartmentInchargeModel model = new DepartmentInchargeModel();

            var result = model.UpdateDepartmentIncharge(new DepartmentInchargeModel.UpdateDepIncharge
            {
                UserAction =1 ,
                Debug = 0,
                EffectDate = view.EffectDate,
                FK_Branch = view.FK_Branch,
                FK_Department =  view.FK_Department,
                FK_Incharge = view.FK_Incharge,
                Remarks = view.Remarks,
                FK_DepartmentIncharge = view.ID_DepartmentIncharge,
                TransMode = view.TransMode,
                EntrBy =_userLoginInfo.EntrBy,
                FK_Company =_userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_DepartmentIncharge =  view.ID_DepartmentIncharge
            }, _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateDepartmentIncharge]
        public ActionResult UpdateDepartmentIncharge(DepartmentInchargeModel.DepartmentInchargeView view)
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

            DepartmentInchargeModel model = new DepartmentInchargeModel();

            var result = model.UpdateDepartmentIncharge(new DepartmentInchargeModel.UpdateDepIncharge
            {
                UserAction = 2,
                Debug = 0,
                EffectDate = view.EffectDate,
                FK_Branch = view.FK_Branch,
                FK_Department = view.FK_Department,
                FK_Incharge = view.FK_Incharge,
                Remarks = view.Remarks,
                FK_DepartmentIncharge = view.ID_DepartmentIncharge,
                TransMode = view.TransMode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_DepartmentIncharge = view.ID_DepartmentIncharge
            }, _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetDepInchargeList]
        public ActionResult GetDepInchargeList(int pageSize, int pageIndex, string Name, string TransMode)
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
            DepartmentInchargeModel model = new DepartmentInchargeModel();

            var data = model.GetDeptInchargelist(input: new DepartmentInchargeModel.DIViewInput
            {

                TransMode = TransMode,
                FK_DepartmentIncharge = 0,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region[GetDIReasonList]
        public ActionResult GetDIReasonList()
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

        #region[DeleteDepartmentIncharge]
        public ActionResult DeleteDepartmentIncharge(DepartmentInchargeModel.DeleteInput delete)
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
            DepartmentInchargeModel model = new DepartmentInchargeModel();

            var datresponse = model.DeleteDepartmentInchargeData(input: new DepartmentInchargeModel.DeleteDepartmentIncharge
            {
                TransMode = delete.TransMode,
                FK_DepartmentIncharge = delete.FK_DepartmentIncharge,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = delete.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}