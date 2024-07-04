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
    public class ActionTypeController : Controller
    {
        // GET: ActionType
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            return View();
        }

            public ActionResult LoadActionTypeForm(string mtd)
            {


                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                UserAcssRightInfo pageAccess = new UserAcssRightInfo();
                pageAccess = _userLoginInfo.PageAccessRights;
                ViewBag.PagedAccessRights = pageAccess;

            ActionTypeModel.ActionTypeListModel actionTypeListModel = new ActionTypeModel.ActionTypeListModel();

                var actionTypeList = Common.GetDataViaQuery<ActionTypeModel.ActionModuleModel>(parameters: new APIParameters
                {
                    TableName = "[ActionModule]",
                    SelectFields = "[ID_ActionModule] AS ActionModuleID,ActionModuleName",
                    Criteria = "",
                    GroupByFileds = "",
                    SortFields = ""
                },
                companyKey: _userLoginInfo.CompanyKey

                );

                actionTypeListModel.ActionModuleList = actionTypeList.Data;

                var actionTypeStatusList = Common.GetDataViaQuery<ActionTypeModel.ActionModeModel>(parameters: new APIParameters
                {
                    TableName = "[ActionMode]",
                    SelectFields = "[ID_ActionMode] AS ActionModeID,ActionModeName",
                    Criteria = "",
                    GroupByFileds = "",
                    SortFields = ""
                },
                   companyKey: _userLoginInfo.CompanyKey

                   );
                actionTypeListModel.ActionModeList = actionTypeStatusList.Data;

                var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                    companyKey: _userLoginInfo.CompanyKey,
                    procedureName: "ProGetNextNo",
                    parameter: new NextSortOrder
                    {
                        TableName = "ActionType",
                        FieldName = "SortOrder",
                        Debug = 0
                    });

            actionTypeListModel.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddActionTypeForm", actionTypeListModel);
            }




        [HttpPost]
        public ActionResult GetPgActionTypeList(int pageSize, int pageIndex,String Name)
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

            ActionTypeModel actionType = new ActionTypeModel();
            var data = actionType.GetActionTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ActionTypeModel.GetActionType
            {

                FK_ActionType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                Name =Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = transMode

            });

            //return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            //  return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        //[HttpGet]
        //    public ActionResult GetActionTypeList()
        //    {
        //        #region ::  Check User Session to verifyLogin  ::

        //        if (Session["UserLoginInfo"] is null)
        //        {
        //            return Json(new
        //            {
        //                Process = new Output
        //                {
        //                    IsProcess = false,
        //                    Message = new List<string> { "Please login to continue" },
        //                    Status = "Session Timeout",
        //                }
        //            }, JsonRequestBehavior.AllowGet);
        //        }

        //        #endregion ::  Check User Session to verifyLogin  ::

        //        UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //        ActionTypeModel actionType = new ActionTypeModel();
        //    var data = actionType.GetActionTypeData(input: new ActionTypeModel.GetActionType
        //    {
        //       FK_ActionType = 0,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        EntrBy = _userLoginInfo.EntrBy,
        //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Machine = _userLoginInfo.FK_Machine,
        //        PageIndex = 0,
        //        PageSize = 0,
        //        TransMode = ""
        //    },companyKey:_userLoginInfo.CompanyKey);



        //        return Json(data, JsonRequestBehavior.AllowGet);

        //    }



        [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult GetActionTypeInfoByID(ActionTypeModel.ActionTypeInfoView data)
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

                ActionTypeModel actionType = new ActionTypeModel();
                var actionTypeInfo = actionType.GetActionTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ActionTypeModel.GetActionType { FK_ActionType = data.ActionTypeID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

                return Json(actionTypeInfo, JsonRequestBehavior.AllowGet);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddNewActionTypeDetails(ActionTypeModel.ActionTypeInputFromViewModel data)
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

                ActionTypeModel actionTypes = new ActionTypeModel();


             
                var dataresponse = actionTypes.UpdateActionTypeData(input: new ActionTypeModel.UpdateActionType
                {
                    UserAction = 1,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    FK_ActionType=0,
                    ID_ActionType = 0,
                    ActnTypeName = data.ActionType,
                    ActnTypeShortName = data.ShortName,
                    FK_ActionMode = data.ActionModeID,
                    FK_ActionModule = data.ActionModuleID,
                    SortOrder = data.SortOrder,
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult UpdateActionTypeDetails(ActionTypeModel.ActionTypeInputFromViewModel data)
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

                ActionTypeModel actionTypes = new ActionTypeModel();


            
                var dataresponse = actionTypes.UpdateActionTypeData(input: new ActionTypeModel.UpdateActionType
                {
                    UserAction = 2,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    FK_ActionType = data.ActionTypeID,


                    ID_ActionType = data.ActionTypeID,
                    ActnTypeName = data.ActionType,
                    ActnTypeShortName = data.ShortName,
                    FK_ActionMode = data.ActionModeID,
                    FK_ActionModule = data.ActionModuleID,
                    SortOrder = data.SortOrder,
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }




            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteActionTypeInfo(ActionTypeModel.ActionTypeInfoView data)
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

                ActionTypeModel actionTypeModel = new ActionTypeModel();
                var datresponse = actionTypeModel.DeleteActionTypeData(input: new ActionTypeModel.DeleteActionType { FK_ActionType = data.ActionTypeID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode=""}, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }


        public ActionResult GetActionTypeReasonList()
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