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
    [CheckSessionTimeOut]
    public class WarrantySettingsController : Controller
    {
        // GET: WarrantySettings
        public ActionResult WarrantySettingIndex()
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
            return View();
        }

        public ActionResult WarrantySettingView()
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

            WarrantySettingModel.WarrantySettingList mdllist = new WarrantySettingModel.WarrantySettingList();

            var Warset = Common.GetDataViaQuery<WarrantySettingModel.WarrantyType>(parameters: new APIParameters
            {
                TableName = "WarrantyType WT",
                SelectFields = "WT.ID_WarrantyType as FK_WarrantyType,WT.WartyName as Warranty",
                Criteria = "WT.Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            mdllist.warrtype = Warset.Data;



            var Module = Common.GetDataViaQuery<WarrantySettingModel.WarrantyList>(parameters: new APIParameters
            {
                TableName = "WarrantyTypeSetting WS",
                SelectFields = " WS.ID_WarrantyTypeSetting,WS.WarrantyTypeSetting ",
                Criteria = "",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            mdllist.warrlist = Module.Data;

            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<WarrantySettingModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            mdllist.TaxgroupList = taxgroup.Data;



            return PartialView("_AddWarrantySetting", mdllist);


        }


        [HttpPost]
        public ActionResult AddWarrantySetting(WarrantySettingModel.WarrantySettingView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("WarrantyType");
            ModelState.Remove("EffectDate");
            ModelState.Remove("RepDurType");
            ModelState.Remove("ReplacementDuration");
            ModelState.Remove("ServiceDuration");
            ModelState.Remove("SerDurPrd");
            ModelState.Remove("FK_WarrantyType");

            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                //errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                        .Select(e => e.ErrorMessage)
                //                        .ToList();

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

            WarrantySettingModel warrtyp = new WarrantySettingModel();

            var datresponse = warrtyp.UpdateWarrantySettingData(input: new WarrantySettingModel.UpdateWarrantySetting
            {

                UserAction = 1,
                FK_WarrantyType = data.FK_WarrantyType,
                WsEffectDate = data.EffectDate,
                WsRepDurPrd=data.ReplacementDuration,
                WsRepDurType=data.RepDurType,
                WsSerDurPrd=data.ServiceDuration,
                WsSerDurtype=data.SerDurType,
                BackupId = data.WarrantySettingID,
                FK_WarrantySetting = data.WarrantySettingID,
                ID_WarrantySetting = data.WarrantySettingID,
                WsAmount = data.WsAmount,
                FK_AccountHead = (data.FK_AccountHead.HasValue) ? data.FK_AccountHead.Value : 0,
                FK_AccountHeadSub = (data.FK_AccountHead.HasValue) ? data.FK_AccountHead.Value : 0,
                FK_TaxGroup = (data.TaxGroupID.HasValue) ? data.TaxGroupID.Value : 0,
               WsAmountIncludeTax = data.IncludeTax,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                Debug = 0

            },

            companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateWarrantySetting(WarrantySettingModel.WarrantySettingView updata)
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

            ModelState.Remove("WarrantyType");
            ModelState.Remove("EffectDate");
            ModelState.Remove("RepDurType");
            ModelState.Remove("ReplacementDuration");
            ModelState.Remove("ServiceDuration");
            ModelState.Remove("SerDurPrd");
            ModelState.Remove("FK_WarrantyType");

            #region :: Model validation  ::
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

            WarrantySettingModel warrtyp = new WarrantySettingModel();

            var datresponse = warrtyp.UpdateWarrantySettingData(input: new WarrantySettingModel.UpdateWarrantySetting
            {
                UserAction = 2,
                FK_WarrantyType = updata.FK_WarrantyType,
                WsEffectDate = updata.EffectDate,
                WsRepDurPrd = updata.ReplacementDuration,
                WsRepDurType = updata.RepDurType,
                WsSerDurPrd = updata.ServiceDuration,
                WsSerDurtype = updata.SerDurType,
                BackupId = updata.WarrantySettingID,
                FK_WarrantySetting = updata.WarrantySettingID,
                ID_WarrantySetting = updata.WarrantySettingID,
                WsAmount = updata.WsAmount,
                FK_AccountHead = (updata.FK_AccountHead.HasValue) ? updata.FK_AccountHead.Value : 0,
                FK_AccountHeadSub = (updata.FK_AccountHead.HasValue) ? updata.FK_AccountHead.Value : 0,
                FK_TaxGroup = (updata.TaxGroupID.HasValue) ? updata.TaxGroupID.Value : 0,
                WsAmountIncludeTax = updata.IncludeTax,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = updata.TransMode,
                Debug = 0


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetWarrantySettingList(int pageSize, int pageIndex, string Name, string Transmode)
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

            WarrantySettingModel Warrantytype = new WarrantySettingModel();

            var outputList = Warrantytype.GetWarrantySettingData(companyKey: _userLoginInfo.CompanyKey, input: new WarrantySettingModel.WarrantySettingID
            {
                FK_WarrantySetting = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode= Transmode,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
            });

          //  return Json(outputList, JsonRequestBehavior.AllowGet);

            return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetWarrantyTypeInfo(WarrantySettingModel.WarrantySettingView warrantyInfo)
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

            ModelState.Remove("WarrantyType");
            ModelState.Remove("EffectDate");
            ModelState.Remove("RepDurType");
            ModelState.Remove("ReplacementDuration");
            ModelState.Remove("ServiceDuration");
            ModelState.Remove("SerDurPrd");
            ModelState.Remove("FK_WarrantyType");


            #region :: Model validation  ::

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


            WarrantySettingModel warrantytype = new WarrantySettingModel();

            var warInfo = warrantytype.GetWarrantySettingData(companyKey: _userLoginInfo.CompanyKey, input: new WarrantySettingModel.WarrantySettingID
            {
                FK_WarrantySetting = warrantyInfo.WarrantySettingID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy
            });

            return Json(warInfo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteWarrantytype(WarrantySettingModel.DeleteView data)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 

            ModelState.Remove("WarrantyTypeID");

            #region :: Model validation  ::
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

            WarrantySettingModel warrantytype = new WarrantySettingModel();




            var datresponse = warrantytype.DeleteWarrantySettingData(input: new WarrantySettingModel.DeleteWarrantySetting
            {
                FK_WarrantySetting = data.WarrantySettingID,
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

        public ActionResult GetWarrantySettingDeleteReasonList()
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