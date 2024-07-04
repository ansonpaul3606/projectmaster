/*----------------------------------------------------------------------
Created By	: Amrithaak
Created On	: 11/03/2023
Purpose		: AllowanceSettings
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

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
    public class AllowanceSettingsController : Controller
    {
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

            return View();
        }

        public ActionResult LoadFormAllowanceSettings(string mtd)
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

            AllowanceSettingModel.AllowancesettingsListModel allowancetylist = new AllowanceSettingModel.AllowancesettingsListModel();
            var employeeList = Common.GetDataViaQuery<AllowanceSettingModel.EmployeeTypeList>(parameters: new APIParameters
            {
                TableName = "EmployeeType E",
                SelectFields = "E.ID_EmployeeType AS  EmployeeTypeID,E.EmptyName AS EmployeeType",
                Criteria = "E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
       companyKey: _userLoginInfo.CompanyKey

       );

            allowancetylist.EmployeeTypeList = employeeList.Data;

            var alwList = Common.GetDataViaQuery<AllowanceSettingModel.AllowancetypeList>(parameters: new APIParameters
            {
                TableName = "AllowanceType A",
                SelectFields = "A.ID_AllowanceType AS AllowancetypeID,A.ALWName AS Allowancetype",
                Criteria = "A.Cancelled=0 AND A.Passed=1 AND A.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
    companyKey: _userLoginInfo.CompanyKey

    );

            allowancetylist.AllowanceList = alwList.Data;


            var alwamountList = Common.GetDataViaQuery<AllowanceSettingModel.AllowancetypeamountList>(parameters: new APIParameters
            {
                TableName = "AllowanceType At",
                SelectFields = "At.ID_AllowanceType AS FK_AllowanceType,At.ALWName AS AllowanceTypeName",
                Criteria = "At.Cancelled=0 AND At.Passed=1 AND At.FK_Company=" + _userLoginInfo.FK_Company + "AND At.ALWMode= " + 1,
                GroupByFileds = "",
                SortFields = ""
            },
  companyKey: _userLoginInfo.CompanyKey

  );
            allowancetylist.AllowancetypeamountList = alwamountList.Data;

            var designationList = Common.GetDataViaQuery<AllowanceSettingModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
    companyKey: _userLoginInfo.CompanyKey

    );
            allowancetylist.DesignationList = designationList.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAllowanceSettings", allowancetylist);
        }

       public ActionResult GetEmployeeType()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];           

            AllowanceSettingModel.AllowancesettingsListModel allowancetylist = new AllowanceSettingModel.AllowancesettingsListModel();
            var employeeList = Common.GetDataViaQuery<AllowanceSettingModel.EmployeeTypeList>(parameters: new APIParameters
            {
                TableName = "EmployeeType E",
                SelectFields = "E.ID_EmployeeType AS  EmployeeTypeID,E.EmptyName AS EmployeeType",
                Criteria = "E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Checkbasic(long FK_AllowanceType)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            AllowanceSettingModel.AllowancesettingsListModel allowancetylist = new AllowanceSettingModel.AllowancesettingsListModel();
            var employeeList = Common.GetDataViaQuery<AllowanceSettingModel.AllowancetypeList>(parameters: new APIParameters
            {
                TableName = "AllowanceType A",
                SelectFields = "A.ID_AllowanceType AS FK_AllowanceType,A.ALWType AS AllowanceTypeName",
                Criteria = "A.Cancelled=0 AND A.Passed=1 AND A.ALWType=1 AND  A.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewAllowanceSettings(AllowanceSettingModel.AllowanceSettingView data)
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

            ModelState.Remove("AllowanceSettingsID");
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

            AllowanceSettingModel AllowanceSettings = new AllowanceSettingModel();

            var datresponse = AllowanceSettings.UpdateAllowanceSettingsData(input: new AllowanceSettingModel.AllowanceSettingsUpdate
            {
                UserAction = 1,
                ALWMode = data.ALWMode,
                FK_AllowanceType = data.FK_AllowanceType,
                FK_EmployeeType = data.FK_EmployeeType,
                FK_Designation = data.FK_Designation,
                EffectDate = data.EffectDate,
                AlsAmountCriteria = data.AlsAmountCriteria,
                FK_Company = _userLoginInfo.FK_Company,
                AlsAmount = (data.AlsAmount.HasValue) ? data.AlsAmount.Value : 0,
                FK_Employee = (data.FK_Employee.HasValue) ? data.FK_Employee.Value : 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_AllowanceSettings = 0,
                TransMode=data.TransMode,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                AllowanceSettingsDetails = data.AllowanceSettingsDetails is null ? "" : Common.xmlTostring(data.AllowanceSettingsDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateAllowanceSettings(AllowanceSettingModel.AllowanceSettingView data)
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

            ModelState.Remove("AllowanceSettingsID");
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

            AllowanceSettingModel AllowanceSettings = new AllowanceSettingModel();

            var datresponse = AllowanceSettings.UpdateAllowanceSettingsData(input: new AllowanceSettingModel.AllowanceSettingsUpdate
            {
                UserAction = 2,
                ALWMode= data.ALWMode,
                FK_AllowanceType = data.FK_AllowanceType,
                FK_EmployeeType = data.FK_EmployeeType,
                FK_Designation = data.FK_Designation,
                EffectDate = data.EffectDate,
                AlsAmountCriteria = data.AlsAmountCriteria,
                FK_Company = _userLoginInfo.FK_Company,
                AlsAmount = (data.AlsAmount.HasValue) ? data.AlsAmount.Value : 0,
                FK_Employee = (data.FK_Employee.HasValue) ? data.FK_Employee.Value : 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                ID_AllowanceSettings = data.ID_AllowanceSettings,
                AllowanceSettingsDetails = data.AllowanceSettingsDetails is null ? "" : Common.xmlTostring(data.AllowanceSettingsDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetAllowanceSettingsList(int pageSize, int pageIndex, string Name)
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

            string transMode = "";

            AllowanceSettingModel objPrd = new AllowanceSettingModel();
            var data = objPrd.GetAllowanceSettingData(companyKey: _userLoginInfo.CompanyKey, input: new AllowanceSettingModel.AllowanceSettingsID
            {

                FK_AllowanceSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detailed = 0,
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetDesignationList()
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
            var data = Common.GetDataViaQuery<AllowanceSettingModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey

          );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetAllowanceSettingsInfo(AllowanceSettingModel.AllowanceSettingsID data)
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
            AllowanceSettingModel objprd = new AllowanceSettingModel();

            var mptableInfo = objprd.GetAllowanceSettingData(companyKey: _userLoginInfo.CompanyKey, input: new AllowanceSettingModel.AllowanceSettingsID
            {
                FK_AllowanceSettings = data.FK_AllowanceSettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy
            });

            var subtable = objprd.GetAllowanceSettingsSubtabledata(companyKey: _userLoginInfo.CompanyKey, input: new AllowanceSettingModel.AllowanceSettingsDetailsSubSelect
            {
                FK_AllowanceSettings = data.FK_AllowanceSettings,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].AllowanceSettingsDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteAllowanceSettings(AllowanceSettingModel.AllowanceSettingsRsnView data)
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

            AllowanceSettingModel.AllowanceSettingsDelete objprddel = new AllowanceSettingModel.AllowanceSettingsDelete
            {
                FK_AllowanceSettings = data.ID_AllowanceSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<AllowanceSettingModel.AllowanceSettingsDelete>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProAllowanceSettingsDelete", parameter: objprddel);
            //test data
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetALType(long ALWMode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<AllowanceSettingModel.AllowancetypeList>(parameters: new APIParameters
            {

                TableName = "AllowanceType AL",
                SelectFields = "AL.ID_AllowanceType AllowancetypeID,AL.ALWName Allowancetype",
                Criteria = "AL.Cancelled=0 AND AL.Passed=1 AND AL.FK_Company=" + _userLoginInfo.FK_Company + " AND AL.ALWMode = " + ALWMode,
                GroupByFileds = "",
                SortFields = ""


            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetAllowanceSettingsDeleteReasonList()
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

    }
}


