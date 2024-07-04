/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 09/03/2023
Purpose		: LeaveType
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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    public class LeaveTypeController : Controller
    {
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadFormLeaveType()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
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
           

            LeaveTypeModel.Leavetypeviewlist listview = new LeaveTypeModel.Leavetypeviewlist();



            LeaveTypeModel lists = new LeaveTypeModel();

            var leavemodelist = lists.Getleavemodelst(input: new LeaveTypeModel.ModeLeave { Mode = 60 },
                companyKey: _userLoginInfo.CompanyKey);

            listview.LTModelist = leavemodelist.Data;


            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "LeaveType",
                FieldName = "SortOrder",
                Debug = 0
            });

            listview.SortOrder = a.Data[0].NextNo;

            return PartialView("_AddLeaveType", listview);
        }

      
        public ActionResult GetLeavetypeList(int pageSize, int pageIndex, string Name)
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

            LeaveTypeModel leavetype = new LeaveTypeModel();
            var data = leavetype.GetLeaveTypeData(companyKey: _userLoginInfo.CompanyKey, input: new LeaveTypeModel.LeaveTypelistID
            {

                FK_LeaveType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
              
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

       
        public ActionResult GetLeaveTypeInfobyID(LeaveTypeModel.LeaveTypelistID data)
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

            LeaveTypeModel leavet = new LeaveTypeModel();
            var leavetInfo = leavet.GetLeaveTypeData(companyKey: _userLoginInfo.CompanyKey, input: new LeaveTypeModel.LeaveTypelistID { FK_LeaveType = data.FK_LeaveType, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(leavetInfo, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewLeaveType(LeaveTypeModel.LeaveTypeView data)
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

            LeaveTypeModel LeaveType = new LeaveTypeModel();

            var datresponse = LeaveType.UpdateLeaveTypeData(input: new LeaveTypeModel.UpdateLeaveType
            {
                UserAction = 1
,
                LTName = data.LTName,
                LTShortName = data.LTShortName,
                LTMode = data.LTMode,
                SortOrder = data.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_LeaveType = 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateLeaveType(LeaveTypeModel.LeaveTypeView data)
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

            LeaveTypeModel LeaveType = new LeaveTypeModel();
            var datresponse = LeaveType.UpdateLeaveTypeData(input: new LeaveTypeModel.UpdateLeaveType
            {
                UserAction = 2,
                ID_LeaveType = data.ID_LeaveType,
                LTName = data.LTName,
                LTShortName = data.LTShortName,
                LTMode = data.LTMode,
                SortOrder = data.SortOrder,

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    
[HttpPost]
[ValidateAntiForgeryToken()]
public ActionResult DeleteLeaveType(LeaveTypeModel.LeaveTypeRsnView data)
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

    LeaveTypeModel LeaveType = new LeaveTypeModel();

    var datresponse = LeaveType.DeleteLeaveTypeData(input: new LeaveTypeModel.LeaveTypeDelete
    {
        FK_LeaveType = data.ID_LeaveType,
        EntrBy = _userLoginInfo.EntrBy,
        FK_Machine = _userLoginInfo.FK_Machine,
        Debug = 0,
        FK_Company = _userLoginInfo.FK_Company,
        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        FK_Reason = data.ReasonID,
        TransMode = ""

    }, companyKey: _userLoginInfo.CompanyKey);
    return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
}


        public ActionResult GetLeaveTypeDeleteReasonList()
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

