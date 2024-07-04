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
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class AMCSettingsController : Controller
    {
        // GET: AMCSettings
        public ActionResult AMCSettingsIndex(string mtd)
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
            CommonMethod objCmnMethod = new CommonMethod();
            string mTd = objCmnMethod.DecryptString(mtd);
            TempData["mTd"] = mTd.ToString();
            return View();
        }

        public ActionResult loadAMCSettingsForm() { 
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

            AMCSettingsModel.AMCSettingsViewList AMCNameList = new AMCSettingsModel.AMCSettingsViewList();
            var AMCName = Common.GetDataViaQuery<AMCSettingsModel.AMCTypeNameList>(parameters: new APIParameters
            {
                TableName = "AMCType AT",
                SelectFields = "AT.ID_AMCType AS AMCTypeID,AT.AMCName AS[AMCName]",
                Criteria = "AT.Cancelled=0 AND AT.Passed=1 AND AT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
          );

            AMCNameList.AMCType = AMCName.Data;
            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<AMCSettingsModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            AMCNameList.TaxgroupList = taxgroup.Data;
            ViewBag.PageTitle = TempData["mTd"] as string;
            return PartialView("_AddAMCSettings", AMCNameList);
        }

        [HttpPost]
        public ActionResult SaveAMCSettings(AMCSettingsModel.AMCSettingsView data)
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

            AMCSettingsModel SaveAMCSettings = new AMCSettingsModel();

            var datresponse = SaveAMCSettings.UpdateAMCSettingsData(input: new AMCSettingsModel.UpdateAMCSettings
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                EffectDate=data.EffectDate,
                ID_AMCSettings = data.ID_AMCSettings,
                FK_AMCType=data.FK_AMCType,
                
                FK_AccountHead = (data.FK_AccountHead.HasValue) ? data.FK_AccountHead.Value : 0,
                FK_AccountHeadSub = (data.FK_AccountHeadSub.HasValue) ? data.FK_AccountHeadSub.Value : 0,
              
                FK_TaxGroup = (data.TaxGroupID.HasValue) ? data.TaxGroupID.Value : 0,
                AMCAmountIncludeTax = data.IncludeTax,
                FK_AMCSettings =data.ID_AMCSettings,
                FK_AMCSettingsDetails = data.AMCSettingsDetails is null ? "" : Common.xmlTostring(data.AMCSettingsDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAMCSettings(AMCSettingsModel.AMCSettingsViewList data)
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

            AMCSettingsModel UpdateAMCSettings = new AMCSettingsModel();

            var datresponse = UpdateAMCSettings.UpdateAMCSettingsData(input: new AMCSettingsModel.UpdateAMCSettings
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                EffectDate = data.EffectDate,
                ID_AMCSettings = data.ID_AMCSettings,
                FK_AMCType = data.FK_AMCType,
                FK_AccountHead = (data.FK_AccountHead.HasValue) ? data.FK_AccountHead.Value : 0,
                FK_AccountHeadSub = (data.FK_AccountHeadSub.HasValue) ? data.FK_AccountHeadSub.Value : 0,
                FK_TaxGroup = (data.TaxGroupID.HasValue) ? data.TaxGroupID.Value : 0,
                AMCAmountIncludeTax = data.IncludeTax,
                FK_AMCSettings = data.ID_AMCSettings,
                FK_AMCSettingsDetails = data.AMCSettingsDetails is null ? "" : Common.xmlTostring(data.AMCSettingsDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAMCSettingsList(int pageSize, int pageIndex, string Name, string TransMode)
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

            AMCSettingsModel AMCSetting = new AMCSettingsModel();

       
            var data = AMCSetting.GetAMCSettings(input: new AMCSettingsModel.AMCSettingsID
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_AMCSettings =0,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);

            // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult GetAMCSettingsInfo(AMCSettingsModel.AMCSettingsDetailsID data)
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

            AMCSettingsModel AMCSettingsdata = new AMCSettingsModel();
            var AMCSettingsInfo = AMCSettingsdata.GetAMCSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new AMCSettingsModel.AMCSettingsID
            {
                FK_AMCSettings = data.ID_AMCSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                
            });

            var AMCSettingsdetails = AMCSettingsdata.GetAMCSettingsDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new AMCSettingsModel.AMCSettingsViewDet
            {
                FK_AMCSettings = data.ID_AMCSettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1
            });

            return Json(new { AMCSettingsInfo, AMCSettingsdetails }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAMCSettingsReasonList()
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
        
        public ActionResult DeleteAMCSettingsInfo(AMCSettingsModel.DeleteAMCSettings data)

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


            AMCSettingsModel.DeleteAMCSettings AMCSettingsdelete = new AMCSettingsModel.DeleteAMCSettings
            {

                TransMode = "",
                FK_AMCSettings = data.FK_AMCSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = data.FK_Reason,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            };

            Output dataresponse = Common.UpdateTableData<AMCSettingsModel.DeleteAMCSettings>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProAMCSettingsDelete", parameter: AMCSettingsdelete);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}