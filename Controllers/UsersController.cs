

/*----------------------------------------------------------------------
Created By	: Amritha Ak
Created On	: 21/02/2022
Purpose		: users
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
using PerfectWebERPAPI.Business;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class usersController : Controller
    {
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadFormusers(string mtd)
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
            var branchtypelist = Common.GetDataViaQuery<usersModel.BranchTypes>(parameters: new APIParameters
            {
                TableName = "BranchType",
                SelectFields = "ID_BranchType AS BranchTypeID,BTName AS BranchType,FK_BranchMode AS BranchModeID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            var userrolelist = Common.GetDataViaQuery<usersModel.Userrole>(parameters: new APIParameters
            {
                TableName = "UserRole",
                SelectFields = "ID_UserRole AS UserRoleID,UsrrlName AS UserRole",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );
            usersModel.UsersViewList branchViewList = new usersModel.UsersViewList
            {

                BranchTypelists = branchtypelist.Data,
                UserRolelists = userrolelist.Data
            };
            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddusersForm", branchViewList);
        }


        public ActionResult GetBranchList(Int32 branchtypeid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<usersModel.Branchs>(parameters: new APIParameters
            {

                TableName = "Branch",
                SelectFields = " BrName AS Branch,ID_Branch AS BranchID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND Cancelled =0 AND Passed=1 AND FK_BranchType='" + branchtypeid + "'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
      


        public ActionResult GetEmployeeCustomerList(usersModel.userrelateddata data)
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

            usersModel users = new usersModel();

         

            var datresponse = Common.GetDataViaProcedure<usersModel.Employees,usersModel.Userlistrelated>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProUsersEmployeeCustomeroOthrlistusingBranchmode",
               parameter: new usersModel.Userlistrelated
               {
                   FK_BranchMode = data.BranchModeID,
                   FK_Branch = data.BranchID,
                   FK_Company = _userLoginInfo.FK_Company
               });

            // return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetusersList(int pageSize, int pageIndex, string Name)
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
            ModelState.Remove("ReasonID");
            ModelState.Remove("TotalCount");
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
            usersModel users = new usersModel();


            var data = users.GetusersData(companyKey: _userLoginInfo.CompanyKey, input: new usersModel.usersID
            {
                FK_users = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            //return Json(new { userInfo.Process, userInfo.Data, pageSize, pageIndex, totalrecord = userInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetUsersInfoByID(usersModel.usersView data)
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

            ModelState.Remove("UserRole");
            ModelState.Remove("UserRoleID");
            ModelState.Remove("BranchType");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("Branch");
            ModelState.Remove("BranchID");
            ModelState.Remove("CashLimit");
            ModelState.Remove("Employee");
            ModelState.Remove("EmployeeID");
            ModelState.Remove("ReasonID");
            ModelState.Remove("UserCode");


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

            usersModel users = new usersModel();




            var usersInfo = users.GetusersData(companyKey: _userLoginInfo.CompanyKey, input: new usersModel.usersID { FK_users = data.UsersID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(usersInfo, JsonRequestBehavior.AllowGet);


        }
        public string GeneratePassword()
        {
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string nums = "123456789";
            string chars = small_alphabets + nums + alphabets;
            string pwd = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                string str = string.Empty;
                do
                {
                    int index = new Random().Next(0, chars.Length);
                    str = chars.ToCharArray()[index].ToString();
                } while (pwd.IndexOf(str) != -1);
                pwd += str;
            }
            return pwd;
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewusers(usersModel.usersView data)
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

            ModelState.Remove("UsersID");

            ModelState.Remove("UserRole");
            ModelState.Remove("BranchType");
            ModelState.Remove("Branch");
            ModelState.Remove("CashLimit");
            ModelState.Remove("Employee");
            ModelState.Remove("UserPassword");


            //if (!ModelState.IsValid)
            //{
            //    List<string> errorList = new List<string>();

            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //            .Select(e => e.ErrorMessage)
            //            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            usersModel users = new usersModel();

            
            string password = GeneratePassword();
            BlFunctions blfun = new BlFunctions();
            // string EncPassword = blfun.Encrypt(Convert.ToString(login.Username[0].ToString() + login.Passswor));

            

            string EncPassword = blfun.Encrypt(Convert.ToString(data.UserCode[0].ToString()+password.ToString()));
           // string EncPassword = blfun.Encrypt(Convert.ToString(data.UserCode.ToString()+password));
           // string passwordEnc = EncPassword;
            var datresponse = users.UpdateusersData(input: new usersModel.Updateusers
            {
                UserAction = 1
,
                UserCode = data.UserCode,
                UserPassword = EncPassword,
                UserCashlimit = data.CashLimit,
                FK_BranchType = data.BranchTypeID,
                FK_UserRole = data.UserRoleID,
                FK_Employee = (data.EmployeeID.HasValue) ? data.EmployeeID.Value : 0,
                FK_Branch = data.BranchID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserMrpEdit = data.UserMrpEdit,
                UserPriceEdit = data.UserPriceEdit,

                BranchModeID=data.BranchModeID,
                Employee=data.Employee,
                Mobile=data.Mobile,
                Email=data.Email,
                Address1=data.Address1,
                Address2=data.Address2,
                CountryID=data.CountryID,
                StatesID=data.StatesID,
                DistrictID=data.DistrictID,
                AreaID=data.AreaID,
                PostID=data.PostID,



                ID_Users = 0,
            }, companyKey: _userLoginInfo.CompanyKey);


            if (datresponse.IsProcess)

                //string Decryptpassword = blfun.Decrypt(blfun.ConvertHexToString(EncPassword);
               datresponse.Message.Add($"{password}");

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Updateusers(usersModel.usersView data)
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
            // ModelState.Remove("UsersID");

            ModelState.Remove("BranchType");
            ModelState.Remove("Branch");
            ModelState.Remove("UserRole");
            ModelState.Remove("Employee");
            ModelState.Remove("CashLimit");
            ModelState.Remove("UserPassword");
            //if (!ModelState.IsValid)
            //{
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //            .Select(e => e.ErrorMessage)
            //            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            usersModel users = new usersModel();
            var datresponse = users.UpdateusersData(input: new usersModel.Updateusers
            {
                UserAction = 2,
                ID_Users = data.UsersID,

                UserCode = data.UserCode,
              //  UserPassword = data.UserPassword,
                UserCashlimit = data.CashLimit,
                FK_BranchType = data.BranchTypeID,
                FK_UserRole = data.UserRoleID,
                FK_Employee = (data.EmployeeID.HasValue) ? data.EmployeeID.Value : 0,
                FK_Branch = data.BranchID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserMrpEdit = data.UserMrpEdit,
                UserPriceEdit = data.UserPriceEdit,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Deleteusers(usersModel.UsersInfoView data)
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

            usersModel users = new usersModel();

            var datresponse = users.DeleteusersData(input: new usersModel.Deleteusers
            {
                FK_users = data.ID_Users,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetUsersDeleteReasonList()
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


