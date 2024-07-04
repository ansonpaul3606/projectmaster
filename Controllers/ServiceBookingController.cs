using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ServiceBookingController : Controller
    {
        // GET: ServiceBooking
        #region[Index]
        public ActionResult Index(string mtd,string mgrp)
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
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            var headName = ViewBag.TransMode.Substring(0, 2);

            string loghead = "";

            if (headName == "VL")
            {
                loghead = "Vehicle";
            }
            else if(headName == "TO")
            {
                loghead = "Tool";
            }
             ViewBag.headlog = loghead;
            return View();
        }
        #endregion

        #region[LoadServiceBooking]
        public ActionResult LoadServiceBooking(string mtd,string TransMode)
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

            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string lblpro = "";
            //string loghead = "";

            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    //labelpopup = "Driver";
                    lblpro = "Vehicle";
                    //loghead = "Vehicle";
                    //desc = "Route";

                    break;
                case "TO":
                    modeData = "'T'";
                    //labelpopup = "Operator";
                    lblpro = "Tool";
                    //loghead = "Tool Log";
                    //desc = "Description";
                    break;
                
                default:
                    modeData = "'P'";
                    break;
            }

            ViewBag.tmode = mode;
            ViewBag.lblpro = lblpro;
           // ViewBag.headlog = loghead;
            ServiceBookingModel.ServiceBookingView serv = new ServiceBookingModel.ServiceBookingView();

            ServiceBookingModel Servdata = new ServiceBookingModel();

            var Servicetype = Common.GetDataViaQuery<ServiceBookingModel.ServiceTypeList>(parameters: new APIParameters
            {
                TableName = "EquipmentServiceType",
                SelectFields = "ID_EquipmentServiceType as TypeID, EqServiceName as TypeName",
                Criteria = "cancelled=0 AND Passed = 1 AND Mode=" + modeData + " AND FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""

            }, companyKey: _userLoginInfo.CompanyKey);

            serv.serviceTypeLists = Servicetype.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddServiceBooking",serv);
        }
        #endregion

        #region[AddServiceBooking]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddServiceBooking(ServiceBookingModel.ServiceBookingViewList data)
        {
            #region[UserSession]
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
            #endregion
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ServiceBookingModel ServBook = new ServiceBookingModel();

            var datares = ServBook.UpdateServiceBookingData(input: new ServiceBookingModel.UpdateServiceBook
            {
                UserAction =1,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                SerBookingDate = data.SerBookingDate,
                SerBookingDescription = data.SerBookingDescription,
                SerBookingNo = data.SerBookingNo,
                SerServiceCentre = data.SerServiceCentre,
                SerServiceDate = data.SerServiceDate,
                SerServiceTime = data.SerServiceTime,
                FK_EquipmentServiceType = data.FK_EquipmentServiceType,
                ID_ServiceBooking = 0,
                FK_Master = data.FK_Master,
                Debug = 0,
                FK_ServiceBooking = 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                SerPickDel = data.SerPickDel



            },companyKey: _userLoginInfo.CompanyKey);

            return Json(new {Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateServiceBooking]
        [HttpPost]
        public ActionResult UpdateServiceBooking(ServiceBookingModel.ServiceBookingViewList model)
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

            ServiceBookingModel Servdta = new ServiceBookingModel();

            var dta = Servdta.UpdateServiceBookingData(input: new ServiceBookingModel.UpdateServiceBook
            {
                UserAction = 2,
                TransMode = model.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

                SerBookingDate = model.SerBookingDate,
                SerBookingDescription = model.SerBookingDescription,
                SerBookingNo = model.SerBookingNo,
                SerServiceCentre = model.SerServiceCentre,
                SerServiceDate = model.SerServiceDate,
                SerServiceTime = model.SerServiceTime,
                FK_EquipmentServiceType = model.FK_EquipmentServiceType,
                ID_ServiceBooking = model.ID_ServiceBooking,
                FK_Master = model.FK_Master,
                Debug = 0,
                FK_ServiceBooking = 0,
                LastID = (model.LastID.HasValue) ? model.LastID.Value : 0,
                SerPickDel = model.SerPickDel

            },companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = dta }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[GetServiceBookingList]
        [HttpPost]
        public ActionResult GetServiceBookingList(int pageSize, int pageIndex, string Name, string TransMode)
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
            ServiceBookingModel ServBook = new ServiceBookingModel();

            var model = ServBook.GetServiceBooklist(input: new ServiceBookingModel.ServBookViewInput
            {
                TransMode = TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
                FK_ServiceBooking = 0

            },companyKey: _userLoginInfo.CompanyKey);

            return Json(new { model.Process, model.Data, pageIndex, pageSize, totalrecord = (model.Data is null) ? 0 : model.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region[GetServiceReasonList]
        public ActionResult GetServiceReasonList()
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

        #region[DeleteServiceBooking]
        public ActionResult DeleteServiceBooking(ServiceBookingModel.DeleteInput data)
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

            ServiceBookingModel model = new ServiceBookingModel();

            var datares = model.DeleteServiceBookData(input: new ServiceBookingModel.DeleteServiceBook
            {
                TransMode = data.TransMode,
                FK_ServiceBooking = data.ID_ServiceBooking,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                FK_Company = _userLoginInfo.FK_Company,
                Debug = 0

            },companyKey:_userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetServiceInfoByID]
        public ActionResult GetServiceInfoByID(ServiceBookingModel.ServBookViewInput data)
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

            ServiceBookingModel ServBook = new ServiceBookingModel();

            var result = ServBook.GetServiceBooklist(input: new ServiceBookingModel.ServBookViewInput
            {
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_ServiceBooking = data.FK_ServiceBooking
            },companyKey: _userLoginInfo.CompanyKey);

            return Json(new { result }, JsonRequestBehavior.AllowGet);

        }
        #endregion


       

    }
}


