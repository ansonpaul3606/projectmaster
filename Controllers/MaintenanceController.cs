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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class MaintenanceController : Controller
    {
        // GET: Maintenance
        public ActionResult Index()
        {

                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            return View();
            }

            public ActionResult LoadMaintenanceForm()
            {


                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            MaintenanceModel.MaintenanceListModel maintenanceList = new MaintenanceModel.MaintenanceListModel();


                var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                  companyKey: _userLoginInfo.CompanyKey,
                  procedureName: "ProGetNextNo",
                  parameter: new NextSortOrder
                  {
                      TableName = "Maintenance",
                      FieldName = "SortOrder",
                      Debug = 0
                  });

                maintenanceList.SortOrder = a.Data[0].NextNo;
            var Module = Common.GetDataViaQuery<MaintenanceModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType",
                SelectFields = "ID_ModuleType as IDMode,ModuleName as ModeName,Mode",
                Criteria = "",
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey
           );
            maintenanceList.modeList = Module.Data;



            return PartialView("_AddMaintenanceForm", maintenanceList);
            }


            [HttpGet]
            public ActionResult GetMaintenanceList()
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


                MaintenanceModel maintenance = new MaintenanceModel();
                var data = maintenance.GetMaintenanceData(input: new MaintenanceModel.GetMaintenance {FK_Maintenance = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        [HttpPost]
        public ActionResult GetMaintenanceList(int pageSize, int pageIndex, string Name)
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


          

            MaintenanceModel maintenance = new MaintenanceModel();
            var data = maintenance.GetMaintenanceData(input: new MaintenanceModel.GetMaintenance
            { FK_Maintenance = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = "" }
            
            , companyKey: _userLoginInfo.CompanyKey);

           
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult GetMaintenanceInfoByID(MaintenanceModel.MaintenanceInfoView data)
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

                MaintenanceModel maintenance = new MaintenanceModel();
                var maintenanceInfo = maintenance.GetMaintenanceData(companyKey: _userLoginInfo.CompanyKey, input: new MaintenanceModel.GetMaintenance { FK_Maintenance = data.MaintenanceID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

                return Json(maintenanceInfo, JsonRequestBehavior.AllowGet);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddNewMaintenanceDetails(MaintenanceModel.MaintenanceInputFromViewModel data)
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
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
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

                MaintenanceModel maintenance = new MaintenanceModel();
            

                var dataresponse = maintenance.UpdateMaintenanceData(input: new MaintenanceModel.UpdateMaintenance
                {
                    UserAction = 1,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    FK_Maintenance=0,

                    ID_Maintenance = 0,
                    MaintenanceName = data.Maintenance,
                    MaintenanceShortName = data.ShortName,
                    MaintenanceDescription = data.Description,
                    //FK_AccountHead = data.AccountHead,
                    //FK_AccountHeadSub = data.AccountHeadSub,
                    FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,
                    FK_AccountHeadSub = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,
                    SortOrder = data.SortOrder,
                    Mode=data.Mode
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult UpdateMaintenanceDetails(MaintenanceModel.MaintenanceInputFromViewModel data)
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

                MaintenanceModel maintenance = new MaintenanceModel();


               

               

                var dataresponse = maintenance.UpdateMaintenanceData(input: new MaintenanceModel.UpdateMaintenance
                {
                    UserAction = 2,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    FK_Maintenance = data.MaintenanceID,

                    ID_Maintenance = data.MaintenanceID,
                    MaintenanceName = data.Maintenance,
                    MaintenanceShortName = data.ShortName,   
                    MaintenanceDescription=data.Description,
                    //FK_AccountHead = data.AccountHead,
                    //FK_AccountHeadSub = data.AccountHeadSub,
                    FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,
                    FK_AccountHeadSub = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,
                    SortOrder = data.SortOrder,
                    Mode=data.Mode
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            [ValidateAntiForgeryToken()]
            public ActionResult DeleteMaintenanceInfo(MaintenanceModel.MaintenanceInfoView data)
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

                MaintenanceModel maintenance = new MaintenanceModel();
                var datresponse = maintenance.DeleteMaintenanceData(input: new MaintenanceModel.DeleteMaintenance { FK_Maintenance = data.MaintenanceID,EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode="" }, companyKey: _userLoginInfo.CompanyKey);
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }



        public ActionResult GetMaintenanceDeleteReasonList()
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


        [HttpGet]
        public ActionResult GetAccountHeadDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<MaintenanceModel.AccountHeadView>(parameters: new APIParameters
            {
                TableName = "AccountHead AH",
                SelectFields = "AH.ID_AccountHead AS AccountHead,AH.AHCode AS AccountCode,AH.AHName AS AHeadName",
                Criteria = "AH.Cancelled =0 AND AH.Passed=1",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAccountSubHeadDetails(int AccountCode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<MaintenanceModel.AccountSubHeadView>(parameters: new APIParameters
            {
                TableName = "AccountSubHead ASH",
                SelectFields = "ASH.ID_AccountSubHead AS AccountHeadSub,ASH.AHCode AS AccountSHCode,ASH.ASHName AS ASHeadName",
                Criteria = "ASH.Cancelled =0 AND ASH.Passed=1 AND ASH.AHCode=" + AccountCode,
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

    }
    }