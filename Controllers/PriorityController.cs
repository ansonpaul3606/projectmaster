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
    public class PriorityController : Controller
    {
        // GET: Priority
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

        public ActionResult LoadPriorityForm()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            PriorityModel.Prioritylist dis = new PriorityModel.Prioritylist();



            PriorityModel  objcmnstmode = new PriorityModel();
            var CmnstmodeList = objcmnstmode.GetModulesList(input: new PriorityModel.ModeLead { Mode = 13 }, companyKey: _userLoginInfo.CompanyKey);
            dis.Modulelist = CmnstmodeList.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "PriorityMaster",
                    FieldName = "SortOrder",
                    Debug = 0
                });
            dis.SortOrder = a.Data[0].NextNo;


            return PartialView("_AddPriority",dis);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
    
        public ActionResult AddPriority(PriorityModel.PriorityViewModel data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("ShortName");
            ModelState.Remove("Name");
            ModelState.Remove("ColorCode");
            ModelState.Remove("ReasonID");
            ModelState.Remove("ID_Mode");
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

            PriorityModel priority = new PriorityModel();
            var dataresponse = priority.UpdatePriorityData(input: new PriorityModel.UpdatePriority
            {
                UserAction = 1,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                FK_Company = _userLoginInfo.FK_Company,

                EntrBy = _userLoginInfo.EntrBy,

                TransMode = data.TransMode,
                Debug = 0,
                ID_Priority = 0,
                PrtyName =data.Name,
                PrtySName =data.ShortName,
                ID_Mode =data.ID_Mode,
                ColorCode =data.ColorCode,
               SortOrder = data.SortOrder

            }, companyKey: _userLoginInfo.CompanyKey);

            // Output datresponse = Common.UpdateTableData<DistrictModel.DistrictView>(companyKey: companyKey, procedureName: "proAreaUpdate", parameter: areas);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult UpdatePriority(PriorityModel.PriorityViewModel data)
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

            PriorityModel priority = new PriorityModel();
            var dataresponse = priority.UpdatePriorityData(input: new PriorityModel.UpdatePriority
            {
                UserAction = 2,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                FK_Company = _userLoginInfo.FK_Company,

                EntrBy = _userLoginInfo.EntrBy,

                TransMode = data.TransMode,
                Debug = 0,
                ID_Priority = data.PriorityID,
                PrtyName = data.Name,
                PrtySName = data.ShortName,
                ID_Mode = data.ID_Mode,
                ColorCode = data.ColorCode,
                SortOrder = data.SortOrder

            }, companyKey: _userLoginInfo.CompanyKey);

            // Output datresponse = Common.UpdateTableData<DistrictModel.DistrictView>(companyKey: companyKey, procedureName: "proAreaUpdate", parameter: areas);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPriorityInfoByID(PriorityModel.PriorityViewModel data)
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

            ModelState.Remove("PriorityID");
           
            ModelState.Remove("SortOrder");
            ModelState.Remove("ShortName");
            ModelState.Remove("Name");
            ModelState.Remove("ColorCode");
            ModelState.Remove("ReasonID");
            ModelState.Remove("ID_Mode");
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

            PriorityModel priority = new PriorityModel();
            var priorityInfo = priority.GetPriorityData(companyKey: _userLoginInfo.CompanyKey, input: new PriorityModel.InputPriorityID
            {
                FK_PriorityMaster = data.PriorityID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            return Json(priorityInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //slide window show district details//

        public ActionResult GetPriorityviewList(int pageSize, int pageIndex, string Name)
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
            PriorityModel Priority = new PriorityModel();

            var data = Priority.GetPriorityData(companyKey: _userLoginInfo.CompanyKey, input: new PriorityModel.InputPriorityID
            {
                FK_PriorityMaster = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser =_userLoginInfo.FK_BranchCodeUser,
        PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);




        }


        public ActionResult DeletePriorityInfo(PriorityModel.PriorityViewModel data)
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

            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ID_Mode");
            ModelState.Remove("ColorCode");
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

            PriorityModel.DeletePriority priority = new PriorityModel.DeletePriority
            {
                FK_PriorityMaster = data.PriorityID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<PriorityModel.DeletePriority>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProPriorityMasterDelete", parameter: priority);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriorityReasonList()
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