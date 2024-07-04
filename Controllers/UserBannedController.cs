/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 26/03/2022
Purpose		: UserBanned
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
    [CheckSessionTimeOut]
    public class UserBannedController : Controller
    {
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadFormUserBanned()
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


            var branchtypelist = Common.GetDataViaQuery<UserBannedModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey
          );
            var banmethodlist = Common.GetDataViaQuery<UserBannedModel.BanMethod>(parameters: new APIParameters
            {
                TableName = "BanMethod",
                SelectFields = "ID_BanMethod AS BanMethodID,BanMethodName AS BanMethodName",
                Criteria = "",

                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );

       //     var reasonbanlist = Common.GetDataViaQuery<UserBannedModel.ReasonBan>(parameters: new APIParameters
       //     {
       //         TableName = "Reason",
       //         SelectFields = "ID_Reason AS ReasonBanID,ResnName AS ReasonBanName",
       //         //Criteria = "FK_ReasonMode= " +" 4 '"+ "FK_Company='" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",
       //         Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND Cancelled =0 AND Passed=1 AND FK_ReasonMode='" + 4 + "'",
       //         SortFields = "",
       //         GroupByFileds = ""
       //     },
       // companyKey: _userLoginInfo.CompanyKey
       //);

            UserBannedModel.UserbanViewList bannedView = new UserBannedModel.UserbanViewList
            {
                BranchList = branchtypelist.Data,
                BanMethodList =banmethodlist.Data,
               // ReasonBanList=reasonbanlist.Data
                

            };

            return PartialView("_AddUserBanned", bannedView);
        }

      

        public ActionResult getUserList(Int32 Branchid)
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
            var data = Common.GetDataViaQuery<UserBannedModel.Users>(parameters: new APIParameters
            {

                TableName = "Users AS U LEFT JOIN Branch AS B On B.ID_Branch=U.FK_Branch LEFT JOIN BranchType AS BT ON BT.ID_BranchType =U.FK_BranchType",
                SelectFields = "U.ID_Users AS UsersID,CASE WHEN BT.FK_BranchMode IN(1,2) THEN (SELECT CONCAT(E.EmpFName,E.EmpLName) AS UserName From Employee AS E WHERE E.ID_Employee =U.FK_Employee) ELSE (SELECT C.CusName AS UserName From CustomerOthers AS C WHERE C.ID_CustomerOthers =U.FK_Employee) END UserName,U.UserCode AS UserCode,BT.BTName AS BranchType",

                Criteria = "U.Cancelled =0 AND U.Passed=1 AND U.FK_Branch='" + Branchid + "'" + " AND U.FK_Company=" + _userLoginInfo.FK_Company,
                //Criteria = "U.cancelled=0 AND U.Passed =1 AND U.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

           

        companyKey: _userLoginInfo.CompanyKey
        );

             return Json(data, JsonRequestBehavior.AllowGet);

    }
       

        [HttpPost]
        public ActionResult GetUserBannedList(int pageSize, int pageIndex,string Name)
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

            ModelState.Remove("ReasonID");

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
            string transMode = "";

            UserBannedModel users = new UserBannedModel();


            var userInfo = users.GetUserBannedData(companyKey: _userLoginInfo.CompanyKey, input: new UserBannedModel.UserBannedID
            {
                ID_UserBanned = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser =_userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });
            return Json(new { userInfo.Process, userInfo.Data, pageSize, pageIndex, totalrecord = (userInfo.Data is null) ? 0 : userInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

           
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetUsersBanInfoByID(UserBannedModel.UserBannedView data)
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

           
            //ModelState.Remove("UserBannedID");
            ModelState.Remove("BranchID");
            ModelState.Remove("Branch");
            ModelState.Remove("UsersID");
            ModelState.Remove("UserCode");
            ModelState.Remove("BanMethodID");
            ModelState.Remove("BanMethodName");
            ModelState.Remove("UbFromDate");
            ModelState.Remove("UbToDate");
            // ModelState.Remove("ReasonBanID");
            ModelState.Remove("UserReasonBan");

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

            UserBannedModel users = new UserBannedModel();

            var usersInfo = users.GetUserBannedData(companyKey: _userLoginInfo.CompanyKey, input: new UserBannedModel.UserBannedID { ID_UserBanned = data.UserBannedID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(usersInfo, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewUserBanned(UserBannedModel.UserBannedView data)
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

            ModelState.Remove("UserBannedID");
            ModelState.Remove("BranchID");
            ModelState.Remove("Branch");
            ModelState.Remove("UsersID");
            ModelState.Remove("UserCode");
            ModelState.Remove("BanMethodID");
            ModelState.Remove("BanMethodName");
            ModelState.Remove("UbFromDate");
            ModelState.Remove("UbToDate");
            // ModelState.Remove("ReasonBanID");
            ModelState.Remove("UserReasonBan");
            //ModelState.Remove("ReasonBan");
            //ModelState.Remove("ReasonBan");


            bool IsValid = true;
            List<string> _ErrorMessage = new List<string>();

            ModelState.Clear();

            if (data.BanMethodID == 1 && data.UbFromDate != null && data.UbToDate != null)
            {
                
                if (data.UbFromDate > data.UbToDate)
                {
                    _ErrorMessage.Add("To Date  should greater than From Date"); IsValid = false;
                }

                else
                if (data.UbFromDate < DateTime.Now.Date)
                    {
                        _ErrorMessage.Add("From Date should be greater than or equal to today"); IsValid = false;
                    }
                }
            
            else
            if(data.BanMethodID ==2 && data.UbFromDate!=null)
            {

                //string fdate = Convert.ToString(data.UbFromDate);
                //string fdate = Convert.ToString(data.UbFromDate);

                if ((data.UbFromDate) < (DateTime.Now.Date))
                {
                    _ErrorMessage.Add("From Date should be greater than or equal to today"); IsValid = false;
                }

            }

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

            UserBannedModel UserBanned = new UserBannedModel();

            if (IsValid == true)
            {
                var datresponse = UserBanned.UpdateUserBannedData(input: new UserBannedModel.UpdateUserBanned
                {
                    UserAction = 1
,
                    FK_User = data.UsersID,
                    FK_Branch = data.BranchID,
                    UbMethod = data.BanMethodID,
                    UbFromDate = data.UbFromDate,
                    // UbToDate= string.IsNullOrEmpty(data.UbToDate) ? null : Convert.ToDateTime(data.UbToDate),
                    UbToDate = data.UbToDate,
                    // UbToDate = (newCompany.AreaID.HasValue) ? newCompany.AreaID.Value : 0,
                    UbBanned = true,
                    UbReasonBan = data.UserReasonBan,
                    UbBanBy = _userLoginInfo.EntrBy,
                    UbReasonUnBan = "",
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,

                    ID_UserBanned = 0,
                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetUsersBanList(int pageSize, int pageIndex, string Name)
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

            string transMode = "";
          
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
            //usersModel users = new usersModel();
            UserBannedModel users = new UserBannedModel();


            var userInfo = users.GetUserBannedData(companyKey: _userLoginInfo.CompanyKey, input: new UserBannedModel.UserBannedID
            {
                ID_UserBanned = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });


            //return Json(new { userInfo.Process, userInfo.Data, pageSize, pageIndex, totalrecord = userInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { userInfo.Process, userInfo.Data, pageSize, pageIndex, totalrecord = (userInfo.Data is null) ? 0 : userInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteusersBan(UserBannedModel.UserBannInfoView data)
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

            //usersModel users = new usersModel();
            UserBannedModel users = new UserBannedModel();

            var datresponse = users.DeleteUserBannedData(input: new UserBannedModel.DeleteUserBanned
            {
                FK_UserBanned = data.ID_UserBanned,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUsersBanDeleteReasonList()
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


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateUserBanned(UserBannedModel.UserBannedView data)
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

            UserBannedModel UserBanned = new UserBannedModel();
            var datresponse = UserBanned.UpdateUserBannedData(input: new UserBannedModel.UpdateUserBanned
            {
                UserAction = 2,
                ID_UserBanned = data.UserBannedID,
                FK_User = data.UsersID,
                FK_Branch = data.BranchID,
                UbMethod = data.BanMethodID,
                UbFromDate = data.UbFromDate,
                // UbToDate= string.IsNullOrEmpty(data.UbToDate) ? null : Convert.ToDateTime(data.UbToDate),
                UbToDate = data.UbToDate,
                // UbToDate = (newCompany.AreaID.HasValue) ? newCompany.AreaID.Value : 0,
                UbBanned = false,
                UbReasonBan = data.UserReasonBan,
                UbBanBy = _userLoginInfo.EntrBy,
                UbReasonUnBan = data.UserReasonUnBan,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


    }
}


