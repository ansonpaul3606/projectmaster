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
    public class DistrictController : Controller
    {
        // GET: District
        public ActionResult DistrictIndex(string mtd)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 


            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }

        public ActionResult LoadFormDistrict(string mtd)
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

            // DistrictModel.DistrictViewList districtViewList = new DistrictModel.DistrictViewList();
            var stateslist = Common.GetDataViaQuery<DistrictModel.States>(parameters: new APIParameters
            {
                TableName = "States",
                SelectFields = "ID_States AS StatesID,StName AS StatesName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );

            DistrictModel.DistrictViewList districtviewlist = new DistrictModel.DistrictViewList
            {
                StatesList = stateslist.Data

            };
            var MenuList = Common.GetDataViaQuery<QuotationModel.Menulist>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "MnuLstName TransMode",
                Criteria = "Cancelled=0 AND Passed=1 AND ControllerName='State' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MenuList",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            CommonMethod objCmnMethod = new CommonMethod();


            ViewBag.SharePagetransmode = objCmnMethod.EncryptString(MenuList.Data[0].Transmode); 
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
               companyKey: _userLoginInfo.CompanyKey,
               procedureName: "ProGetNextNo",
               parameter: new NextSortOrder
               {
                   TableName = "District",
                   FieldName = "SortOrder",
                   Debug = 0
               });

            districtviewlist.SortOrder = a.Data[0].NextNo;
           
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddDistrictForm", districtviewlist);
        }

        [HttpGet]
        public ActionResult GetStateList()
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
            var data = Common.GetDataViaQuery<DistrictModel.States>(parameters: new APIParameters
            {
                TableName = "States",
                SelectFields = "ID_States AS StatesID,StName AS StatesName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        //slide window show district details//

        public ActionResult GetDistrictList(int pageSize, int pageIndex, string Name)
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
            DistrictModel district = new DistrictModel();

           var data = district.GetDistrictData(companyKey: _userLoginInfo.CompanyKey, input: new DistrictModel.InputDistrictID { ID_District = 0, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy ,
               PageIndex = pageIndex + 1,
               PageSize = pageSize,
               Name = Name,
               TransMode = transMode
               
           });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);




        }


        [HttpPost] 
        [ValidateAntiForgeryToken()]
        //Add New district//
        public ActionResult AddNewDistrictDetails(DistrictModel.DistrictInputView newDistrict)
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



            ModelState.Remove("ID");
            ModelState.Remove("States");
          //  ModelState.Remove("StatesID");
          ModelState.Remove("ReasonID");
            ModelState.Remove("SortOrder");
            ModelState.Remove("TotalCount");

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

            //DistrictModel states = new DistrictModel();
            DistrictModel district= new DistrictModel();

            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;
          //  DistrictModel.DistrictView district = new DistrictModel.DistrictView
             var dataresponse = district.UpdateDData(input: new DistrictModel.UpdateDistrict
            {
                 UserAction = userAction,
                 FK_Machine = FK_Machine,
                 FK_BranchCodeUser = branchUserCode,

                 FK_Company = _userLoginInfo.FK_Company,
               
                 EntrBy = entrBy,

                 TransMode = newDistrict.TransMode,
                 Debug = 0,

                 ID_District = 0,
                DtName = newDistrict.DistrictName,
                DtShortName= newDistrict.DistrictShortName,
                FK_States = newDistrict.StatesID,
                SortOrder=newDistrict.SortOrder

             }, companyKey: _userLoginInfo.CompanyKey);

           // Output datresponse = Common.UpdateTableData<DistrictModel.DistrictView>(companyKey: companyKey, procedureName: "proAreaUpdate", parameter: areas);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        //update district
        public ActionResult UpdateDistrictDetails(DistrictModel.DistrictInputView data)
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


            ModelState.Remove("States");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ReasonID");
            ModelState.Remove("TotalCount");

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

            DistrictModel district = new DistrictModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
           // int backupId = 0;

            var dataresponse = district.UpdateDData(input: new DistrictModel.UpdateDistrict
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
             
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,

                ID_District = data.ID,
                DtName = data.DistrictName,
                DtShortName = data.DistrictShortName,
                FK_States = data.StatesID,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete district
        public ActionResult DeleteDistrictInfo(DistrictModel.DistrictInputView data)
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

            ModelState.Remove("DistrictName");
            ModelState.Remove("DistrictShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("TotalCount");


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

            DistrictModel.DeleteDistrict district = new DistrictModel.DeleteDistrict
            {
                ID_District = data.ID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug =0,
                FK_Company=_userLoginInfo.FK_Company,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Reason= data.ReasonID,
                TransMode =""
            };

            Output dataresponse = Common.UpdateTableData<DistrictModel.DeleteDistrict>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proDistrictDelete", parameter:district);
           
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        
       [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetDistrictInfoByID(DistrictModel.DistrictInputView data)
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

            ModelState.Remove("StatesID");
            ModelState.Remove("States");
            ModelState.Remove("SortOrder");
            ModelState.Remove("DistrictShortName");
            ModelState.Remove("DistrictName");
            ModelState.Remove("ReasonID");

            ModelState.Remove("TotalCount");



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

            DistrictModel district = new DistrictModel();
            var districtInfo = district.GetDistrictData(companyKey: _userLoginInfo.CompanyKey, input: new DistrictModel.InputDistrictID { ID_District = data.ID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy,FK_Machine=_userLoginInfo.FK_Machine });

            return Json(districtInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictReasonList()
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