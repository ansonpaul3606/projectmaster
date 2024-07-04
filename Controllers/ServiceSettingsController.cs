using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ServiceSettingsController : Controller
    {
        // GET: ServiceSettings
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
        public ActionResult LoadServiceSettingsList()
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

            ServiceSettingsModel objSer = new ServiceSettingsModel();
            ServiceSettingsModel.ServiceSettings objSerNew = new ServiceSettingsModel.ServiceSettings();

            var Category = Common.GetDataViaQuery<ServiceSettingsModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
                companyKey: _userLoginInfo.CompanyKey
           );
            var Servicelist = Common.GetDataViaQuery<ServiceSettingsModel.Services>(parameters: new APIParameters
            {
                TableName = "Services",
                SelectFields = "ID_Services AS ServiceID,ServicesName AS ServiceList",
                Criteria = "cancelled=0 AND Passed=1 and FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            var PeriodType = Common.GetDataViaProcedure<ServiceSettingsModel.PeriodType, ServiceSettingsModel.PeriodTypeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceSettingsModel.PeriodTypeInput { Mode = 3 });

            objSerNew.CategoryLists = Category.Data;
            objSerNew.ServiceList = Servicelist.Data;
            objSerNew.PeriodTypes = PeriodType.Data;
            return PartialView("_LoadServiceSettings", objSerNew);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddServiceSettings(ServiceSettingsModel.ServiceSettingsNew data)
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
            ServiceSettingsModel ServiceSettings = new ServiceSettingsModel();

            var datresponse = ServiceSettings.UpdateServiceSettingsData(input: new ServiceSettingsModel.ServiceSettingsUpdate
            {
                UserAction = 1,
                EffectDate = data.EffectDate,
                FK_Category = data.FK_Category,
                FK_Product = data.FK_Product,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_PeriodicServiceSettings = 0,
                PeriodicServiceSettingsDetails = data.PeriodicServiceSettingsDetails is null ? "" : Common.xmlTostring(data.PeriodicServiceSettingsDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetServiceSettingsData(int pageSize, int pageIndex, string Name)
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
            ServiceSettingsModel ObjMo = new ServiceSettingsModel();
            var ServiceInfo = ObjMo.GetServiceSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceSettingsModel.GetServiceSettings
            {
                FK_PeriodicServiceSettings = 0,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detailed=0
            });
            return Json(new { ServiceInfo.Process, ServiceInfo.Data, pageSize, pageIndex, totalrecord = (ServiceInfo.Data is null) ? 0 : ServiceInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetServiceSettingsInfo(ServiceSettingsModel.GetServiceSettings obj)
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
            ServiceSettingsModel objServ = new ServiceSettingsModel();
            var serInfo = objServ.GetServiceSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceSettingsModel.GetServiceSettings
            {
                FK_PeriodicServiceSettings = obj.FK_PeriodicServiceSettings,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,              
                Detailed = 0
            });
            var serDtlInfo = objServ.GetServiceSettingsDtlsData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceSettingsModel.GetServiceSettings
            {
                FK_PeriodicServiceSettings = obj.FK_PeriodicServiceSettings,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1
            });
            
           
            return Json(new { serInfo, serDtlInfo }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetServiceSettingReasonList()
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
                EntrBy = _userLoginInfo.EntrBy
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteServiceSettings(ServiceSettingsModel.DeleteServiceSettings data)
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

            ServiceSettingsModel objServ = new ServiceSettingsModel();

            var datresponse = objServ.DeleteServiceSettingsData(input: new ServiceSettingsModel.DeleteServiceSettings
            {
                FK_PeriodicServiceSettings = data.FK_PeriodicServiceSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.FK_Reason,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}