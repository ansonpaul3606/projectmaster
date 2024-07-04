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
using static PerfectWebERP.Models.NextActionModel;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class NextActionController : Controller
    {
        // GET: NextAction
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
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return View();
        }
            public ActionResult LoadNextActionForm(string mtd)
            {


                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                UserAcssRightInfo pageAccess = new UserAcssRightInfo();
                pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            NextActionModel.NextActionListModel nextActionListModel = new NextActionModel.NextActionListModel();

                var nextActionList = Common.GetDataViaQuery<ActionModuleModel>(parameters: new APIParameters
                {
                    TableName = "[ActionModule]",
                    SelectFields = "[ID_ActionModule] AS ActionModuleID,ActionModuleName",
                    Criteria = "",
                    GroupByFileds = "",
                    SortFields = ""
                },
                companyKey: _userLoginInfo.CompanyKey

                );

                nextActionListModel.ActionModuleList = nextActionList.Data;

            var nextActionStatusList = Common.GetDataViaQuery<ActionStatusModel>(parameters: new APIParameters
            {
                TableName = "[ActionStatus]",
                SelectFields = "[ID_ActionStatus] AS ActionStatusID,ActionStatusName",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
               companyKey: _userLoginInfo.CompanyKey

             );

            nextActionListModel.ActionStatusList = nextActionStatusList.Data;
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "NextAction",
                    FieldName = "SortOrder",
                    Debug = 0,
                    FK_Company=_userLoginInfo.FK_Company
                });

                nextActionListModel.SortOrder = a.Data[0].NextNo;

            NextActionModel objnewstatus = new NextActionModel();
            var AcStatusList = objnewstatus.GeLeadStatusList(input: new NextActionModel.ModeLead { Mode = 9 }, companyKey: _userLoginInfo.CompanyKey);

            nextActionListModel.ActionStatusNewList = AcStatusList.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddNextActionForm", nextActionListModel);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetActionStatusList(short ActionModuleID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var nextActionStatusList = Common.GetDataViaQuery<ActionStatusModel>(parameters: new APIParameters
            {
                TableName = "[ActionStatus]",
                SelectFields = "[ID_ActionStatus] AS ActionStatusID,[ActionStatusName] AS ActionStatusName",
                Criteria = "FK_ActionModule=" + ActionModuleID,
                GroupByFileds = "",
                SortFields = ""
            },
              companyKey: _userLoginInfo.CompanyKey

              );
     
            return Json(nextActionStatusList, JsonRequestBehavior.AllowGet);

        }

       

        public ActionResult GetNextActionList(int pageSize, int pageIndex, string Name)
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

            NextActionModel nextAction = new NextActionModel();
            var data = nextAction.GetNextActionDataList(input: new NextActionModel.GetNextAction {
                FK_NextAction = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = "",
                Name=Name,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult GetNextActionInfoByID(NextActionModel.NextActionInfoView data)
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

                NextActionModel nextAction = new NextActionModel();
                var nextActionInfo = nextAction.GetNextActionData(companyKey: _userLoginInfo.CompanyKey, input: new NextActionModel.GetNextAction { FK_NextAction = data.NextActionID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

                return Json(nextActionInfo, JsonRequestBehavior.AllowGet);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddNewNextActionDetails(NextActionModel.NextActionInputFromViewModel data)
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

                NextActionModel nextActions = new NextActionModel();


              

                var dataresponse = nextActions.UpdateNextActionData(input: new NextActionModel.UpdateNextAction
                {
                    UserAction = 1,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = "",

                    FK_NextAction=0,
                    ID_NextAction = 0,
                    NxtActnName = data.NextAction,
                    NxtActnShortName = data.ShortName,
                    FK_ActionStatus = data.ActionStatusID,
                    FK_ActionModule = data.ActionModuleID,
                    SortOrder = data.SortOrder,
                    NxtActnStage=data.ActionStageID,
                }, companyKey: _userLoginInfo.CompanyKey);


            Output output = new Output();

            if (dataresponse.Data.FirstOrDefault().Column1 > 0)
            {

                output.IsProcess = true;
                output.Message = new List<string> { "Saved Successfully" };

            }
            else
            {
                output.Message = new List<string> { dataresponse.Data.FirstOrDefault().ErrMsg };
                output.code = dataresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult UpdateNextActionDetails(NextActionModel.NextActionInputFromViewModel data)
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

                NextActionModel nextActions = new NextActionModel();


                var dataresponse = nextActions.UpdateNextActionData(input: new NextActionModel.UpdateNextAction
                {
                    UserAction = 2,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = "",

                    FK_NextAction= data.NextActionID,
                    ID_NextAction = data.NextActionID,
                    NxtActnName = data.NextAction,
                    NxtActnShortName = data.ShortName,
                    FK_ActionStatus = data.ActionStatusID,
                    FK_ActionModule = data.ActionModuleID,
                    SortOrder = data.SortOrder,
                    NxtActnStage = data.ActionStageID,
                }, companyKey: _userLoginInfo.CompanyKey);


            Output output = new Output();

            if (dataresponse.Data.FirstOrDefault().Column1 > 0)
            {

                output.IsProcess = true;
                output.Message = new List<string> { "Updated Successfully" };

            }
            else
            {
                output.Message = new List<string> { dataresponse.Data.FirstOrDefault().ErrMsg };
                output.code = dataresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);
        }




            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteNextActionInfo(NextActionModel.NextActionInfoView data)
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

                NextActionModel nextActionModel = new NextActionModel();
                var datresponse = nextActionModel.DeleteNextActionData(input: new NextActionModel.DeleteNextAction { FK_NextAction = data.NextActionID,EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode=""}, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }



        public ActionResult GetNextActionDeleteReasonList()
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