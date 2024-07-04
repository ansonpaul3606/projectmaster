using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class DepreciationSettingController : Controller
    {
        // GET: DepreciationSetting
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
        public ActionResult LoadDepreciationSettings()
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
            DepreciationSettingModel.DepreciationSettingView prdListObj = new DepreciationSettingModel.DepreciationSettingView();
            DepreciationSettingModel objfin = new DepreciationSettingModel();

            var PerdtypList = objfin.GetPerdTypList(input: new DepreciationSettingModel.ModeLead { Mode = 3 },
            companyKey: _userLoginInfo.CompanyKey);
     

            prdListObj.PeriodList = PerdtypList.Data;

            var ModeTypeList = objfin.GetModeTypList(input: new DepreciationSettingModel.Modes { Mode = 67 },
            companyKey: _userLoginInfo.CompanyKey);

            prdListObj.ModeList = ModeTypeList.Data;


       //     var Category = Common.GetDataViaQuery<DepreciationSettingModel.CategoryList>(parameters: new APIParameters
       //     {
       //         TableName = "Category",
       //         SelectFields = "ID_Category AS FK_Category ,CatName AS Category",
       //         Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
       //         SortFields = "ID_Category",
       //         GroupByFileds = ""
       //     },
       //companyKey: _userLoginInfo.CompanyKey

       //   );
       //     prdListObj.CategoryList = Category.Data;

            return PartialView("_AddDepreciationSettings", prdListObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepreciationSettings(DepreciationSettingModel.DepreciationSettingView data)
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

            //ModelState.Remove("BranchtypeID");
            //ModelState.Remove("BranchModeID");
            //ModelState.Remove("ReasonID");
            //ModelState.Remove("SortOrder");
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


            DepreciationSettingModel branchtype = new DepreciationSettingModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            /// string userCode = _userLoginInfo.Us;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = branchtype.AddDepreciationData(input: new DepreciationSettingModel.AddDepreciation
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,

                ID_DepreciationSettings = 0,
                EffectDate = data.EffectDate,
                Mode = data.Mode,
                DepreciationSettingsDetails = data.DepreciationSettingsDetails is null ? "" : Common.xmlTostring(data.DepreciationSettingsDetails),





            }, 
            companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetDepreciationSettingsList(int pageSize, int pageIndex, string Name)
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

            DepreciationSettingModel objPrd = new DepreciationSettingModel();
            var data = objPrd.GetDepreciationsettingsData(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationSettingModel.DepreciationsettingsID
            {

                FK_DepreciationSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Details = 0,
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetDepreciationsettingsInfo(DepreciationSettingModel.DepreciationsettingsID data)
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
            DepreciationSettingModel objprd = new DepreciationSettingModel();

            var mptableInfo = objprd.GetDepreciationsettingsDatainfoid(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationSettingModel.DepreciationsettingsID
            {
                FK_DepreciationSettings = data.FK_DepreciationSettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Details = 1,
                EntrBy = _userLoginInfo.EntrBy
            });

            var subtable = objprd.GetDepreciationsettingsSubtabledata(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationSettingModel.DepreciationSettingDetailsSubSelect
            {
                FK_DepreciationSettings = data.FK_DepreciationSettings,
                Details = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].DepreciationSettingsDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDepreciationsettingsDeleteReasonList()
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
        public ActionResult DeleteDepreciationSettings(DepreciationSettingModel.DepreciationSettingView data)
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

            DepreciationSettingModel.DepreciationsettingsDelete objprddel = new DepreciationSettingModel.DepreciationsettingsDelete
            {
                FK_DepreciationSettings = data.ID_DepreciationSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<DepreciationSettingModel.DepreciationsettingsDelete>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProDepreciationSettingsDelete", parameter: objprddel);
            //test data
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCategorylist(string Id_Mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<DepreciationSettingModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category ",
                SelectFields = "ID_Category FK_Category, CatName Category",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Company='" + _userLoginInfo.FK_Company + "'" + " AND Mode='"+ Id_Mode +"'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}