using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class LeaveSettingsController : Controller
    {
        // GET: LeaveSettings
        #region[Index]
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            return View();
        }
        #endregion

        #region[LoadLeaveSettings]
        public ActionResult LoadLeaveSettings()
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

            LeaveSettingsModel LSM = new LeaveSettingsModel();
            LeaveSettingsModel.LeaveSettingsView settingsView = new LeaveSettingsModel.LeaveSettingsView();

            var Leavetype = Common.GetDataViaQuery<LeaveSettingsModel.LeaveTypeList>(parameters: new APIParameters
            {
                TableName = "LeaveType",
                SelectFields = "ID_LeaveType as ID_LeaveType, LTName as TypeName",
                Criteria = "cancelled=0 AND Passed = 1  AND FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""

            }, companyKey: _userLoginInfo.CompanyKey);

            settingsView.leaveTypeLists = Leavetype.Data;

            var Allowancetype = Common.GetDataViaQuery<LeaveSettingsModel.AllowanceTypeList>(parameters: new APIParameters
            {
                TableName = "AllowanceType",
                SelectFields = "ID_AllowanceType as ID_AllowanceType, ALWName as AllowName",
                Criteria = "cancelled=0 AND Passed = 1 AND ALWMode=1 AND FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""

            }, companyKey: _userLoginInfo.CompanyKey);

            settingsView.recoveryTypeLists = Allowancetype.Data;



            return PartialView("_AddLeaveSetiings", settingsView);
        }
        #endregion

        #region[AddLeaveSetting]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddLeaveSetting(LeaveSettingsModel.LeaveSettingsInput data)
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

            LeaveSettingsModel leave = new LeaveSettingsModel();

            List<LeaveSettingsModel.LeaveType> FK_Leavetypes = new List<LeaveSettingsModel.LeaveType>();
        
            var datares = leave.UpdateLeaveSettingData(new LeaveSettingsModel.UpdateLeaveSetting
            {
                UserAction = 1,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                EffectDate = data.EffectDate,
                LsSalCalculateDays = data.LsSalCalculateDays,
               // FK_LeaveType = data.FK_LeaveType,
                FK_LeaveSettings = 0,
                LeaveDetails = data.LeaveDetails is null ? "" : Common.xmlTostring(data.LeaveDetails),
                LeaveType = Common.xmlTostring(data.LeaveType.Select(a => new LeaveSettingsModel.LeaveType { FK_LeaveType = a }).ToList())



            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[GetLeaveSettingList]
        public ActionResult GetLeaveSettingList(int pageSize, int pageIndex, string Name, string TransMode)
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
            LeaveSettingsModel leaveSettings = new LeaveSettingsModel();

            var model = leaveSettings.GetLeaveSettinglist(input: new LeaveSettingsModel.LeaveSettingsViewInput
            {
                TransMode = TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
                FK_LeaveSettings =0
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { model.Process,model.Data ,pageIndex, pageSize, totalrecord = (model.Data is null) ? 0 : model.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region[GetLeaveSettingReasonList]
        public ActionResult GetLeaveSettingReasonList()
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

        #region[DeleteLeaveSettings]
        public ActionResult DeleteLeaveSettings(LeaveSettingsModel.DeleteInput deleteInput)
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
            LeaveSettingsModel model = new LeaveSettingsModel();

            var result = model.DeleteLeaveSettings(input: new LeaveSettingsModel.DeleteLeaveSetting
            {
                TransMode = "",
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_LeaveSettings = deleteInput.FK_LeaveSettings,
                Debug = 0,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = deleteInput.ReasonID,
                
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[GetLeaveSettingInfoByID]
        public ActionResult GetLeaveSettingInfoByID(LeaveSettingsModel.LeaveSettingsViewInput data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ReasonID");
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

            LeaveSettingsModel leaveSettings = new LeaveSettingsModel();

            var result = leaveSettings.GetLeaveGrid(input: new LeaveSettingsModel.LeaveSettingsViewInput
            {
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_LeaveSettings = data.FK_LeaveSettings,
                Detailed = 1
            }, companyKey: _userLoginInfo.CompanyKey);

            //get details

            //List<LeaveSettingsModel.LeaveSettingViewOutput> listOfleave = new List<LeaveSettingsModel.LeaveSettingViewOutput>();

            //listOfleave.Add(new LeaveSettingsModel.LeaveSettingViewOutput { FK_LeaveType = 2 });
            //listOfleave.Add(new LeaveSettingsModel.LeaveSettingViewOutput { FK_LeaveType = 5 });
            //listOfleave.Add(new LeaveSettingsModel.LeaveSettingViewOutput { FK_LeaveType = 6 });

            //listOfleave.Select(a => a.LeaveType).ToList();

           
            //string[] leave = listOfleave.Select(a => a.LeaveType).ToArray();

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}