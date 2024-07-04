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
    public class UserPasswordresetController : Controller
    {
        // GET: UserPasswordreset
       
        public ActionResult Index()

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();
        }
        public ActionResult Userpasswordindex()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();
        }

        public ActionResult LoadFormUsersPasswordReset()
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


            return PartialView("_AddUserPasswordReset");
        }

        public ActionResult GetUserCodeList()
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

            var data = Common.GetDataViaQuery<UserPasswordresetModel.UserCodeList>(parameters: new APIParameters
            {
                TableName = "Users AS U LEFT JOIN Branch AS B On B.ID_Branch=U.FK_Branch LEFT JOIN BranchType AS BT ON BT.ID_BranchType =U.FK_BranchType",
                SelectFields = "U.ID_Users AS UserID,CASE WHEN BT.FK_BranchMode IN(1,2) THEN (SELECT CONCAT(E.EmpFName,E.EmpLName) AS UserName From Employee AS E WHERE E.ID_Employee =U.FK_Employee AND E.EmpRelieved=0) ELSE (SELECT C.CusName AS UserName From CustomerOthers AS C WHERE C.ID_CustomerOthers =U.FK_Employee) END UserName,U.UserCode AS UserCode,B.BrName AS Branch",
                Criteria = "U.cancelled=0 AND U.Passed =1 AND U.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



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
        public ActionResult Adduserpasswordreset(UserPasswordresetModel.UserpasswordResetView data)
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

            ModelState.Remove("UserCode");

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

            UserPasswordresetModel users = new UserPasswordresetModel();


            string password = GeneratePassword();
            BlFunctions blfun = new BlFunctions();
           
            string EncPassword = blfun.Encrypt(Convert.ToString(data.UserCode[0].ToString() + password.ToString()));
            var datresponse = users.UpdateUserpasswordresetData(input: new UserPasswordresetModel.Updateuserpaswordreset
            {
                //UserAction = 2,
               ID_Users=data.UserID,
                UserCode = data.UserCode,
                UserPassword = EncPassword,
              
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            }, companyKey: _userLoginInfo.CompanyKey);


            if (datresponse.IsProcess)

                //string Decryptpassword = blfun.Decrypt(blfun.ConvertHexToString(EncPassword);
                datresponse.Message.Add($"{password}");

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }
    }
}