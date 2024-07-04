using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class StandByRecoveryController : Controller
    {
        // GET: StandByRecovery
        #region[Index]
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
        #endregion

        #region[LoadStandbyRecovery]
        public ActionResult LoadStandbyRecovery(string mtd, string TransMode)
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
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;

            StandByRecoveryModel product = new StandByRecoveryModel();
            StandByRecoveryModel.StandByRecoveryView view = new StandByRecoveryModel.StandByRecoveryView();
            StandByRecoveryModel.DropdownList list = new StandByRecoveryModel.DropdownList(); 

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            var DepartmentList = Common.GetDataViaQuery<StandByRecoveryModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS Department",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            list.DepartmentList = DepartmentList.Data;

            return PartialView("_AddStandByRecovery", list);

        }
        #endregion

        #region[GetStandbyStockData]
        public ActionResult GetStandbyStockData(StandByRecoveryModel.StandByRecoveryView view)
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
            StandByRecoveryModel model = new StandByRecoveryModel();

            var result = model.GetStandbyStockDetails(new StandByRecoveryModel.StandByRecoveryView
            {
                FK_Customer = view.FK_Customer,
                StbyRecoveryDate = view.StbyRecoveryDate

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { result, data = result.Data, result.Process }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[AddStandbyRecoverydata]
        public ActionResult AddStandbyRecoverydata(StandByRecoveryModel.InsertInput view)
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
            StandByRecoveryModel model = new StandByRecoveryModel();

            var datares = model.UpdateStandbyRecoverydetails(input: new StandByRecoveryModel.UpdateStandbyRecovery
            {
                UserAction = 1,
                TransMode = view.TransMode,
                ID_StandbyRecovery = 0,
                EntrBy = _userLoginInfo.EntrBy,
                //FK_ProductRecovery = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Customer = view.FK_Customer,
                StbyRecoveryDate = view.StbyRecoveryDate,
                Debug = 0,
                DeliveryDate = view.DeliveryDate,
                DeliveryTime = view.DeliveryTime,
                FK_Employee = view.FK_Employee,
                VehicleDet = view.VehicleDet,
                ProdRecNarration = view.ProdRecNarration,
                LastID = (view.LastID.HasValue) ? view.LastID.Value : 0,
                StandbyRecoveryDetails = view.StandbyRecoveryDetails is null ? "" : Common.xmlTostring(view.StandbyRecoveryDetails),
                ReplaceProductDetails = view.ReplaceProductDetails is null ? "" : Common.xmlTostring(view.ReplaceProductDetails)
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetStandbyRecoveryList]
        public ActionResult GetStandbyRecoveryList(int pageSize, int pageIndex, string Name, string TransMode)
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
            StandByRecoveryModel model = new StandByRecoveryModel();

            var data = model.GetStandRecoList(input: new StandByRecoveryModel.StandbyRecoveryInput
            {

                TransMode = TransMode,
                GroupID = 0,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
                Detailed = 0

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetStandbyRecoveryInfoByID]
        public ActionResult GetStandbyRecoveryInfoByID(StandByRecoveryModel.StandbyRecoveryInput info)
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
            StandByRecoveryModel item = new StandByRecoveryModel();

            var datares = item.GetProRecoveryGrid(input: new StandByRecoveryModel.StandbyRecoveryInput
            {
                TransMode = info.TransMode,
                PageSize = 0,
                PageIndex = 0,
                GroupID = info.GroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetStandbyRecoReasonList]
        public ActionResult GetStandbyRecoReasonList()
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

        #region[DeleteStandbyData]
        public ActionResult DeleteStandbyData(StandByRecoveryModel.DeleteInput delete)
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

            StandByRecoveryModel model = new StandByRecoveryModel();

            var dltdata = model.DeletePStandbyRecoveryData(input: new StandByRecoveryModel.DeleteStandRecovery
            {
                TransMode = delete.TransMode,
                StandbyRecGroupID = delete.StandbyRecGroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = delete.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = dltdata }, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}