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
    public class ExtraWorkTypeController : Controller
    {
        // GET: ExtraWorkType
        public ActionResult ExtraWorkIndex()
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

        public ActionResult ExtraworkForm()
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

            ExtraWorkTypeModel.ExtWkList output = new ExtraWorkTypeModel.ExtWkList();
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "ExtraWorkType",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            output.SortOrder = a.Data[0].NextNo;


            return PartialView("_AddExtraWorkType", output);
        }

        public ActionResult GetAccoundlist()
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
    

            var data = Common.GetDataViaQuery<ExtraWorkTypeModel.AccountName>(parameters: new APIParameters
            {
                TableName = "AccountHead A",
                SelectFields = "A.AHName as AccoundHead,A.ID_AccountHead as AccountHeadID",
                Criteria = "A.Cancelled=0 ",
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddWorkType(ExtraWorkTypeModel.ExtraWorkTypeView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ModelState.Remove("ExtraWorkTypeID");
            ModelState.Remove("SortOrder");
            ModelState.Remove("WorkName");
            ModelState.Remove("ShortName");
            ModelState.Remove("AccountHeadID");

            #region :: Model validation  ::
            //--- Model validation 
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

            #endregion :: Model validation  ::
            

            ExtraWorkTypeModel wrktyp = new ExtraWorkTypeModel();

            var datresponse = wrktyp.UpdateExtraWorkTypeData(input: new ExtraWorkTypeModel.UpdateExtraWorkType
            {

                UserAction = 1,
                ExtraWorkTypeID = 0,
                BackupId = data.ExtraWorkTypeID,
                FK_AccountHead = data.AccountHeadID,
                FK_ExtwrkType = data.ExtraWorkTypeID,
                ExtwrktyName = data.WorkName,
                ExtwrktyShortName = data.ShortName,
                EWTExtraWorkType = data.ExtraWork,
                SortOrder = data.SortOrder,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                Debug = 0,
              



            },

            companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetWorkList()
        //{
        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    ExtraWorkTypeModel workllist = new ExtraWorkTypeModel();

        //    var outputList = workllist.GeExtraWorkTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ExtraWorkTypeModel.ExtraWorkTypeID
        //    {
        //        FK_ExtraWorkType = 0,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Machine = _userLoginInfo.FK_Machine,
        //        EntrBy = _userLoginInfo.EntrBy,
        //        PageIndex = pageIndex + 1,
        //        PageSize = pageSize,
        //        Name = Name,
        //        TransMode = transMode
        //    });
        //    return Json(outputList, JsonRequestBehavior.AllowGet);


        //}

        [HttpPost]
        public ActionResult GetWorkList(int pageSize, int pageIndex, string Name)
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
            ExtraWorkTypeModel workllist = new ExtraWorkTypeModel();

            var data = workllist.GeExtraWorkTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ExtraWorkTypeModel.ExtraWorkTypeID
            {
                FK_ExtraWorkType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetWorkInfo(ExtraWorkTypeModel.ExtraWorkTypeView data)
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
            ModelState.Remove("ExtraWorkTypeID");
            ModelState.Remove("WorkName");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("AccountHeadID");
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


            //var UnitInfo = Common.GetDataViaProcedure<UnitModel.UpdateUnit, UnitModel.Unit_ID>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proUnitSelect", parameter: new UnitModel.Unit_ID { UnitID = data.UnitID });

            //return Json(UnitInfo, JsonRequestBehavior.AllowGet);

            ExtraWorkTypeModel work= new ExtraWorkTypeModel();

            var workInfo = work.GeExtraWorkTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ExtraWorkTypeModel.ExtraWorkTypeID
            {
                FK_ExtraWorkType = data.ExtraWorkTypeID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(workInfo, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public ActionResult UpdateWork(ExtraWorkTypeModel.ExtraWorkTypeView data)
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


            ModelState.Remove("SortOrder");
            ModelState.Remove("ExtraWorkTypeID");
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

            ExtraWorkTypeModel model = new ExtraWorkTypeModel();

            var datresponse = model.UpdateExtraWorkTypeData(input: new ExtraWorkTypeModel.UpdateExtraWorkType
            {


                UserAction = 2,
                ExtwrktyName = data.WorkName,
                ExtwrktyShortName = data.ShortName,
                EWTExtraWorkType = data.ExtraWork,
                FK_AccountHead = data.AccountHeadID,
                SortOrder = data.SortOrder,
                ExtraWorkTypeID =data.ExtraWorkTypeID,
                FK_ExtwrkType =data.ExtraWorkTypeID,
                BackupId = data.ExtraWorkTypeID,

                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                Debug=0,
                TransMode = data.TransMode,



            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteWork(ExtraWorkTypeModel.DeleteView data)
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

            ExtraWorkTypeModel mdl = new ExtraWorkTypeModel();



            Output datresponse = mdl.DeleteExtraWorkTypeData(input: new ExtraWorkTypeModel.DeleteExtraWorkType
            {
                FK_ExtraWorkType = data.ExtraWorkTypeID,
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


        public ActionResult GetExtraworkDeleteReasonList()
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