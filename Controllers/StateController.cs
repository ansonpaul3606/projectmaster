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
using static PerfectWebERP.Models.StateModel;
using PerfectWebERP.Filters;



//using static PerfectWebERP.Models.SampleTable;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class StateController : Controller
    {
     
       
        // GET: State
        public ActionResult Index(string mtd)
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
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadStateForm(string mtd)
        {
          

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;

            ViewBag.PagedAccessRights = pageAccess;

           
            StateModel.StateListModel stateListModel = new StateModel.StateListModel();

            var stateList= Common.GetDataViaQuery<Country>(parameters: new APIParameters
            {
                TableName = "[Country]",
                SelectFields = "[ID_Country] AS CountryID,[CntryName] AS CountryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company="+_userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );



            var menuSettings = Common.GetDataViaQuery<MenuDeatils>(parameters: new APIParameters
            {
                TableName = "[MenuList]",
                SelectFields = "[MnuLstName] AS MnuLstName",
                Criteria = "Cancelled=0 AND Passed=1 AND ControllerName='Country' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            stateListModel.CountryList = stateList.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "States",
                FieldName = "SortOrder",
                Debug = 0
            });
            CommonMethod objCmnMethod = new CommonMethod();

            ViewBag.mtdcont = objCmnMethod.EncryptString(menuSettings.Data.FirstOrDefault().MnuLstName);

            stateListModel.SortOrder = a.Data[0].NextNo;
     
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddStateForm", stateListModel);
        }


        [HttpGet]
        public ActionResult GetCountryList()
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
            var data = Common.GetDataViaQuery<Country>(parameters: new APIParameters
            {
                TableName = "[Country]",
                SelectFields = "[ID_Country] AS CountryID,[CntryName] AS CountryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
                 companyKey: _userLoginInfo.CompanyKey

                    );
            return Json(data, JsonRequestBehavior.AllowGet);

        }

     

        [HttpPost]
        public ActionResult GetStateList(int pageSize, int pageIndex, string Name)
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
            StateModel state = new StateModel();

            var data = state.GetStateData(companyKey: _userLoginInfo.CompanyKey, input: new StateModel.GetState
            {
                FK_States = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetStateInfoByID(StateModel.StateInfoView data)
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

            StateModel state = new StateModel();
            var stateInfo = state.GetStateData(input: new StateModel.GetState { FK_States = data.StateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);


            return Json(stateInfo, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewStateDetails(StateModel.StatesInputFromViewModel data)
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

            ModelState.Remove("State");
            ModelState.Remove("ShortName");

            ModelState.Remove("GSTINCode");
            ModelState.Remove("CountryID");
            ModelState.Remove("Country");
            ModelState.Remove("SortOrder");

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

            StateModel states = new StateModel();


         
         

            var dataresponse = states.UpdateStateData(input: new StateModel.UpdateState
            {
                UserAction = 1,
                FK_Machine = _userLoginInfo.FK_Machine,
              
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
              
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
             FK_States=0,
                TransMode ="",

                ID_States = 0,
                StName = data.State,
                StShortName = data.ShortName,
                StGSTINCode = data.GSTINCode,
                FK_Country = data.CountryID,
                SortOrder = data.SortOrder
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStateDetails(StateModel.StatesInputFromViewModel data)
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
            ModelState.Remove("State");
            ModelState.Remove("StateID");
            ModelState.Remove("ShortName");

            ModelState.Remove("GSTINCode");
            ModelState.Remove("CountryID");
            ModelState.Remove("Country");
            ModelState.Remove("SortOrder");
            StateModel states = new StateModel();



            var dataresponse = states.UpdateStateData(input: new StateModel.UpdateState
            {
                UserAction = 2,
                FK_Machine = _userLoginInfo.FK_Machine,

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_States = data.StateID,
                TransMode = "",

                ID_States = data.StateID,
                StName = data.State,
                StShortName = data.ShortName,
                StGSTINCode = data.GSTINCode,
                FK_Country = data.CountryID,
                SortOrder = data.SortOrder
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


      
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStateInfo(StateModel.StateInfoView data)
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

            StateModel stateModel = new StateModel();
            var datresponse = stateModel.DeleteStateData(input: new StateModel.DeleteState{ FK_States = data.StateID, EntrBy = _userLoginInfo.EntrBy,FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode="", FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStateDeleteReasonList()
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