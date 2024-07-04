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

namespace PerfectWebERP.Controllers
{
    public class EquipmentLogController : Controller
    {

        #region[LogIndex]
        public ActionResult LogIndex(string mtd,string mgrp)
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
            ViewBag.TransMode = mGrp;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            var headName = ViewBag.TransMode.Substring(0, 2);

            string loghead = "";

            if (headName == "VL")
            {
                loghead = "Driver";
            }
            else if (headName == "TO")
            {
                loghead = "Operator";
            }
            ViewBag.headlog = loghead;

            return View();
        }
        #endregion
        #region[LogView]
        public ActionResult LogView(string mtd,string TransMode)
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
            LogModel.LogListModel LogList = new LogModel.LogListModel();
            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string labelpopup = "";
            string lblpro = "";
            string loghead = "";
            string desc = " ";
            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    labelpopup = "Driver";
                    lblpro = "Vehicle";
                    loghead = "Vehicle Log";
                    desc = "Route";

                    break;
                case "TO":
                    modeData = "'T'";
                    labelpopup = "Operator";
                    lblpro = "Tool";
                    loghead = "Tool Log";
                    desc = "Description";
                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            ViewBag.lblemp = labelpopup;
            ViewBag.lblpro = lblpro;
            ViewBag.headlog = loghead;
            ViewBag.lbldesc = desc;
            ViewBag.tmode = mode;
            var MaintenanceView = Common.GetDataViaQuery<LogModel.TypeList>(parameters: new APIParameters
            {
                TableName = "Maintenance",
                SelectFields = "ID_Maintenance as TypeID,MaintenanceName as TypeName",
                Criteria = "cancelled=0 AND Passed =1 AND Mode=" + modeData + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            LogList.TypeListdata = MaintenanceView.Data;


            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name, PMDefault AS PMDefault",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey
      );
            LogList.PaymentView = PaymentView.Data;
            LogModel log = new LogModel();
            var FuelTypeList = log.GeLeadStatusList(input: new LogModel.ModeLead { Mode = 65 },

            companyKey: _userLoginInfo.CompanyKey);

            LogList.ActionStatusList = FuelTypeList.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddLogView", LogList);
        }
        #endregion

        #region[GetOtherCharges]
        public ActionResult GetOtherCharges(LogModel.BindOtherCharge Data)
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
                Criteria = "ID_TransType=2",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[AddNewVehicleAndToolLog]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewVehicleAndToolLog(LogModel.VehicleAndToolLogView data)
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

            LogModel log = new LogModel();
            var OtherChgDetails = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = log.UpdateVehicleAndToolLogData(input: new LogModel.UpdateVehicleAndToolLog
            {
                UserAction = 1,

                FK_Employee = data.Employee,
                FK_Vehicle = data.Vehicle,
                VtlLogStartDate = data.VtlLogStartDate,
                VtlLogDate=data.VtlLogDate,
                VtlLogStartTime = data.VtlLogStartTime,
                VtlLogEndDate = data.VtlLogEndDate,
                VtlLogEndTime = data.VtlLogEndTime,
                VtlLogDescription = data.VtlLogDescription,
                VtlLogRemarks = data.VtlLogRemarks,
                VtlLogTotalAmount = data.VtlLogTotalAmount,
                VtlLogOtherCharges = data.VtlLogOtherCharges,
                VtlLogNetAmount = data.VtlLogNetAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                //Mode = data.Mode
                TransMode = data.TransMode,
                MaintenanceDetails = data.MaintenanceDetails is null ? "" : Common.xmlTostring(data.MaintenanceDetails),
                ID_VehicleAndToolLog = 0,
               // VtlogFuelAmount = data.VtlogFuelAmount,
                //FK_Fuel = data.FK_FuelType,
                VtLogStartKm = data.VtlLogStartKm,
                VtLogEndKm = data.VtlLogEndKm,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                OtherChDetails = OtherChgDetails is null ? "" : Common.xmlTostring(OtherChgDetails),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateVehicleAndToolLog]
        public ActionResult UpdateVehicleAndToolLog(LogModel.VehicleAndToolLogView data)
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

            LogModel log = new LogModel();
            var OtherChgDetails = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            var datresponse = log.UpdateVehicleAndToolLogData(input: new LogModel.UpdateVehicleAndToolLog
            {
                UserAction = 2,

                FK_Employee = data.Employee,
                FK_Vehicle = data.Vehicle,
                VtlLogStartDate = data.VtlLogStartDate,
                VtlLogDate = data.VtlLogDate,
                VtlLogStartTime = data.VtlLogStartTime,
                VtlLogEndDate = data.VtlLogEndDate,
                VtlLogEndTime = data.VtlLogEndTime,
                VtlLogDescription = data.VtlLogDescription,
                VtlLogRemarks = data.VtlLogRemarks,
                VtlLogTotalAmount = data.VtlLogTotalAmount,
                VtlLogOtherCharges = data.VtlLogOtherCharges,
                VtlLogNetAmount = data.VtlLogNetAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                //Mode = data.Mode
                TransMode = data.TransMode,
                MaintenanceDetails = data.MaintenanceDetails is null ? "" : Common.xmlTostring(data.MaintenanceDetails),
                ID_VehicleAndToolLog = data.ID_VehicleAndToolLog,
                //VtlogFuelAmount = data.VtlogFuelAmount,
                //FK_Fuel = data.FK_FuelType,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                VtLogStartKm = data.VtlLogStartKm,
                VtLogEndKm = data.VtlLogEndKm,
                OtherChDetails = OtherChgDetails is null ? "" : Common.xmlTostring(OtherChgDetails),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[GetLogList]
        [HttpPost]
        public ActionResult GetLogList( int pageSize, int pageIndex,string Name ,string TransMode)
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
            LogModel log = new LogModel();
            var data = log.GetLoglist(input: new LogModel.VtLogViewInput
            {

                TransMode = TransMode,
                FK_VehicleAndToolLog = 0,
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

        #region[GetlogReasonList]

        public ActionResult GetlogReasonList()
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

        #region[DeleteLog]
        public ActionResult DeleteLog(LogModel.DeleteInput data)
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

            LogModel log = new LogModel();


            var datresponse = log.DeleteLogData(input: new LogModel.deleteLog
            {
                TransMode = data.TransMode,
                FK_VehicleAndToolLog = data.ID_VehicleAndToolLog,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetLogInfoByID]
        public ActionResult GetLogInfoByID(LogModel.VtLogViewInput logdata)
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

            LogModel log = new LogModel();
            Common.fillOtherCharges(logdata.TransMode, logdata.FK_VehicleAndToolLog);
            var lgdata = log.GetMainteneceGrid(companyKey: _userLoginInfo.CompanyKey, input: new LogModel.VtLogViewInput
                  {
                      TransMode = logdata.TransMode,
                      PageSize = 0,
                      PageIndex = 0,
                      FK_VehicleAndToolLog = logdata.FK_VehicleAndToolLog,
                      EntrBy = _userLoginInfo.EntrBy,
                      FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                      FK_Company = _userLoginInfo.FK_Company,
                      FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1

            });

            var OtherCharge = log.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new LogModel.GetSubTableSales
            {
                FK_Transaction = logdata.FK_VehicleAndToolLog,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = logdata.TransMode
            });
            var paymentdetail = log.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new LogModel.GetPaymentin {
                FK_Master = logdata.FK_VehicleAndToolLog,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = logdata.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            return Json( new{ lgdata, OtherCharge, paymentdetail }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region[GetStartKm]
        [HttpGet]
        public ActionResult GetStartKm(LogModel.GetStKm data)
        {

           // var getdata1 = "SELECT ISNULL(MAX(VtlogEndKm),0) FROM VehicleAndToolLog WHERE FK_Vehicle = " + data.FK_Vehicle + " AND Cancelled = 0";

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LogModel.GetStKm obj = new LogModel.GetStKm();
            var getdata = Common.GetDataViaQuery<LogModel.GetStKm>(parameters: new APIParameters
            {
                TableName = "VehicleAndToolLog",
                SelectFields = "ISNULL(MAX(VtlogEndKm),0) As VtlogStartKm",
                Criteria = " FK_Vehicle = " + data.FK_Vehicle + " AND Cancelled = 0",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            obj.VtlogStartKm = getdata.Data[0].VtlogStartKm;
            return Json(new { obj }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
