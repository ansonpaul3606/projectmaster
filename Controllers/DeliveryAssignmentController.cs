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
    public class DeliveryAssignmentController : Controller
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
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.AdminManager = false;
            if (_userLoginInfo.UsrrlAdmin == true || _userLoginInfo.UsrrlManager == true)
            {
                ViewBag.AdminManager = true;
            }
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            return View();
        }
       
        public ActionResult LoadDeliveryAssignment(string mtd)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.AdminManager = false;
            if (_userLoginInfo.UsrrlAdmin == true || _userLoginInfo.UsrrlManager == true)
            {
                ViewBag.AdminManager = true;
            }
            DeliveryAssignmentModel.DeliveryAssignmentview Del = new DeliveryAssignmentModel.DeliveryAssignmentview();
            var ChangeMod = Common.GetDataViaProcedure<DeliveryAssignmentModel.TransporttypeMode, DeliveryAssignmentModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new DeliveryAssignmentModel.ChangeModeInput { Mode = 53 });

            Del.TransporttypeModeList = ChangeMod.Data;

            return PartialView("_LoadDeliveryAssignment", Del);
        }
        [HttpPost]
        public ActionResult GetDeliveryAssignList(int pageSize, int pageIndex, string Name,string TransMode)
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

            DeliveryAssignmentModel DelList = new DeliveryAssignmentModel();
            var DeliveryInfo = DelList.GetDeliveryAssignmentData(companyKey: _userLoginInfo.CompanyKey, input: new DeliveryAssignmentModel.GetDeliveryAssignment
            {
                FK_ProductDelivery = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                Detailed = 0,
            });
            return Json(new { DeliveryInfo.Process, DeliveryInfo.Data, pageSize, pageIndex, totalrecord = (DeliveryInfo.Data is null) ? 0 : DeliveryInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetDeliveryAssignByID(DeliveryAssignmentModel.DeliveryAssignmentview data)
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
            ViewBag.AdminManager = false;
            if (_userLoginInfo.UsrrlAdmin == true || _userLoginInfo.UsrrlManager == true)
            {
                ViewBag.AdminManager = true;
            }
            DeliveryAssignmentModel DelList = new DeliveryAssignmentModel();
            var DeliveryInfo = DelList.GetDeliveryAssignmentData(companyKey: _userLoginInfo.CompanyKey, input: new DeliveryAssignmentModel.GetDeliveryAssignment
            {
                FK_ProductDelivery = data.ID_DeliveryAssignment,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                TransMode = data.TransMode,
                Detailed = 0,
            });
            var DeliveryProduct = DelList.GetDeliveryAssignmentData(companyKey: _userLoginInfo.CompanyKey, input: new DeliveryAssignmentModel.GetDeliveryAssignment
            {
                FK_ProductDelivery = data.ID_DeliveryAssignment,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = 0,
                PageSize = 0,
                Name = "",
                TransMode = data.TransMode,
                Detailed = 1,
            });
            return Json(new { DeliveryInfo, DeliveryProduct }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductDetails(DeliveryAssignmentModel.InputProductDetails Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            DeliveryAssignmentModel data = new DeliveryAssignmentModel();
            var Lits = data.GetProductDetails(input: new DeliveryAssignmentModel.InputProductDetails { ImportID = Data.ImportID, ID = Data.ID, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Lits }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDeliveryAssignment(DeliveryAssignmentModel.DeliveryAssignmentview data)
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
            DeliveryAssignmentModel objprdwise = new DeliveryAssignmentModel();

            long IDimport = 0;
            if (data.ddlImportsList == 1){
                IDimport = data.SalesID;
            }
            else{
                IDimport = data.SalesOrderID;
            }
            var dataresponse = objprdwise.UpdateDeliveryAssignmentData(input: new DeliveryAssignmentModel.DeliveryUpdateInput
            {

                UserAction = 1,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_ProductDelivery = 0,
                ImportID = data.ddlImportsList,
                FK_Master = IDimport,
                FK_Vehicle = data.VehicleID,
                FK_Employee = data.EmployeeID,
                ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                AddressCheck=data.AddressCheck,
                ShpContactName=data.ShpContactName,
                Address1=data.Address1,
                Place=data.Place,
                CountryID=data.CountryID,
                StatesID=data.StatesID,
                DistrictID=data.DistrictID,
                AreaID=data.AreaID,
                PostID=data.PostID,
                ShpMobile=data.ShpMobile,
                TransportType = data.TransportType,
                EwayBillNo=data.EwayBillNo,
                Vehicleno=data.Vehicleno,
                DrvName=data.DrvName,
                DrvPhoneno=data.DrvPhoneno,
                PdAssignedDate = data.DeliveryDate,
                PdAssignedTime = data.DeliveryTime is null ? "": data.DeliveryTime,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDeliveryAssignment(DeliveryAssignmentModel.DeliveryAssignmentview data)
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
            DeliveryAssignmentModel objprdwise = new DeliveryAssignmentModel();

            long IDimport = 0;
            if (data.ddlImportsList == 1)
            {
                IDimport = data.SalesID;
            }
            else
            {
                IDimport = data.SalesOrderID;
            }
            var dataresponse = objprdwise.UpdateDeliveryAssignmentData(input: new DeliveryAssignmentModel.DeliveryUpdateInput
            {

                UserAction = 2,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_ProductDelivery = data.ID_DeliveryAssignment,
                ImportID = data.ddlImportsList,
                FK_Master = IDimport,
                FK_Vehicle = data.VehicleID,
                FK_Employee = data.EmployeeID,
                ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                AddressCheck = data.AddressCheck,
                ShpContactName = data.ShpContactName,
                Address1 = data.Address1,
                Place = data.Place,
                CountryID = data.CountryID,
                StatesID = data.StatesID,
                DistrictID = data.DistrictID,
                AreaID = data.AreaID,
                PostID = data.PostID,
                ShpMobile = data.ShpMobile,
                TransportType = data.TransportType,
                EwayBillNo = data.EwayBillNo,
                Vehicleno = data.Vehicleno,
                DrvName = data.DrvName,
                DrvPhoneno = data.DrvPhoneno,
                PdAssignedDate = data.DeliveryDate,
                PdAssignedTime = data.DeliveryTime,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteDeliveryAssignment(DeliveryAssignmentModel.DeliveryAssignmentview data)
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


            DeliveryAssignmentModel Delivery = new DeliveryAssignmentModel();

            var datresponse = Delivery.DeleteDeliveryData(input: new DeliveryAssignmentModel.DeleteDeliveryAssign
            {
                FK_ProductDelivery = data.ID_DeliveryAssignment,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeliveryyDeleteReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
    }
}