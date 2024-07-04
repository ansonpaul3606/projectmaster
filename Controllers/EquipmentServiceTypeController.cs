using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class EquipmentServiceTypeController : Controller
    {
        // GET: ServiceType

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
            return View();
        }
        #endregion

        #region[LoadFormServiceType]
        public ActionResult LoadFormServiceType()
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

            EquipmentServiceTypeModel.ServiceTypeView Stview = new EquipmentServiceTypeModel.ServiceTypeView();
            EquipmentServiceTypeModel eqmodel = new EquipmentServiceTypeModel();

           var srtno = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
           companyKey: _userLoginInfo.CompanyKey,
           procedureName: "ProGetNextNo",
           parameter: new NextSortOrder
           {
               TableName = "EquipmentServiceType",
               FieldName = "SortOrder",
               Debug = 0
           });

            Stview.SortOrder = srtno.Data[0].NextNo;
            var mode = eqmodel.GetServiceModelist(input: new EquipmentServiceTypeModel.ModeLead { Mode = 67 }, companyKey: _userLoginInfo.CompanyKey);
            Stview.ServiceList = mode.Data;

            return PartialView("_AddServiceTypeForm", Stview);

        }
        #endregion

        #region[AddEquipmentServiceType]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddEquipmentServiceType(EquipmentServiceTypeModel.ServiceTypeView data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("ID_EquipmentServiceType");
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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

            EquipmentServiceTypeModel Eqview = new EquipmentServiceTypeModel();

            var datares = Eqview.UpdateEqServiceTypeData(input: new EquipmentServiceTypeModel.UpdateEqService
            {
                UserAction = 1,
                Debug = 0,
                EqServiceName =  data.EqServiceName,
                EqServiceShortName = data.EqServiceShortName,
                EqServiceDescription = data.EqServiceDescription,
                SortOrder = data.SortOrder,
                Mode = data.Mode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                ID_EquipmentServiceType = data.ID_EquipmentServiceType,
                FK_EquipmentServiceType = data.ID_EquipmentServiceType

            }, companyKey:  _userLoginInfo.CompanyKey);
            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region[UpdateEquipmentServiceType]

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateEquipmentServiceType(EquipmentServiceTypeModel.ServiceTypeView data)
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

            ModelState.Remove("SortOrder");
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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

            EquipmentServiceTypeModel Eqtype = new EquipmentServiceTypeModel();

            var result = Eqtype.UpdateEqServiceTypeData(input: new EquipmentServiceTypeModel.UpdateEqService
            {
                UserAction = 2,
                Debug = 0,
                EqServiceName = data.EqServiceName,
                EqServiceShortName = data.EqServiceShortName,
                EqServiceDescription = data.EqServiceDescription,
                SortOrder = data.SortOrder,
                Mode = data.Mode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = " ",
                ID_EquipmentServiceType = data.ID_EquipmentServiceType,
                FK_EquipmentServiceType = data.ID_EquipmentServiceType
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[GetServiceTypeList]

        public ActionResult GetServiceTypeList(int pageSize, int pageIndex, string Name)
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

            EquipmentServiceTypeModel eqtype = new EquipmentServiceTypeModel();

            var Eqinfo = eqtype.GetServiceTypeData(input: new EquipmentServiceTypeModel.GetServiceType
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = "",
                Name = Name,
                FK_EquipmentServiceType = 0,
                EntrBy = _userLoginInfo.EntrBy

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Eqinfo.Process, Eqinfo.Data, pageIndex, pageSize, totalrecord = (Eqinfo.Data is null) ? 0 : Eqinfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetServiceTypeReasonList]
        public ActionResult GetServiceTypeReasonList()
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

        #region[DeleteServiceType]

        public ActionResult DeleteServiceType(EquipmentServiceTypeModel.EqServiceTypeId data)
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
         
            #region :: Model validation  ::
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

            #endregion :: Model validation  :: 

            EquipmentServiceTypeModel type = new EquipmentServiceTypeModel();

            var datares = type.DeleteServiceTypeData(input: new EquipmentServiceTypeModel.DeleteServiceType
            {
                TransMode = "",
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason =  data.ReasonID,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                FK_EquipmentServiceType = data.FK_EquipmentServiceType
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetServiceTypeInfo]
        public ActionResult GetServiceTypeInfo(EquipmentServiceTypeModel.ServiceTypeView info)
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

            EquipmentServiceTypeModel EqSer = new EquipmentServiceTypeModel();

            var result = EqSer.GetServiceTypeData(input: new EquipmentServiceTypeModel.GetServiceType
            {
                FK_EquipmentServiceType = info.FK_EquipmentServiceType,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}