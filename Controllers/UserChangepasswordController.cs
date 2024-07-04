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
using System.Text.RegularExpressions;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class UserChangepasswordController : Controller
    {
        // GET: UserChangepassword
        public ActionResult UserchangepasswordIndex()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();
        }
        [HttpGet]
        public ActionResult LoadUserchangepassword()
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
            UserchangePasswordModel.Userchangepwdlist usercpswd = new UserchangePasswordModel.Userchangepwdlist();
            usercpswd.UserCode = _userLoginInfo.EntrBy;
            return PartialView("_AddUserChangePassword", usercpswd);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewUserChangepassword(UserchangePasswordModel.UserChangePassword data)
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



            else
            {
                UserchangePasswordModel model = new UserchangePasswordModel();
                BlFunctions blfun = new BlFunctions();
                string EncPassword = blfun.Encrypt(Convert.ToString(data.UserCode[0].ToString() + data.PresentPassword.ToString()));
                var dataUserpwdcheck = model.GetUserpasswordcheck(input:
                  new UserchangePasswordModel.UserpswdcheckView
                  {
                      UserCode = _userLoginInfo.EntrBy,
                      FK_Company = _userLoginInfo.FK_Company,

                  },


                companyKey: _userLoginInfo.CompanyKey
                );



                string Encpassword = (dataUserpwdcheck.Data[0].PresentPassword);

                if (EncPassword != Encpassword)
                {
                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "Incorrect Present Password " }, Status = "Incorrect Present Password " } }, JsonRequestBehavior.AllowGet);
                }


                int passwordcombination = 0;
                string strRegex = @"(^[0-9])";
                Regex re = new Regex(strRegex);
                string strRegexAlpha = @"(^[A-Za-z]+$)";
                Regex realpha = new Regex(strRegexAlpha);
                string strRegexAlphanumeric = @"(^[a-zA-Z0-9]+$)";
                Regex realphanum = new Regex(strRegexAlphanumeric);
                string strRegexComplex = @"^(?=.*[A-Z])(?=.*[0-9])(?=.*[~!@#$%^&*()\-_=+{}[\]:;""'<,>.?/\\|]).*$";
                Regex regexComplex = new Regex(strRegexComplex);

                //var password = data.NewPassword;

                //var uppercaseValid = password.Any(char.IsUpper);
                //var numberValid = password.Any(char.IsDigit);

                //string specialCharacters = @"@/[~!'#$%^&*()_\-`\[\]{};':""<>?,.\/]+";

                //bool hasSpecialCharacter = password.Any(ch => specialCharacters.Contains(ch));




                //  var allValid =  uppercaseValid && numberValid && hasSpecialCharacter;


                if (re.IsMatch(data.NewPassword))
                {
                    passwordcombination = 2;
                }
               
               else if (realpha.IsMatch(data.NewPassword))
                {
                    passwordcombination = 3;
                }

               else if (realphanum.IsMatch(data.NewPassword))
                {
                    passwordcombination = 1;
                }
                else if (regexComplex.IsMatch(data.NewPassword))
                {
                    passwordcombination = 4;
                }
            //else if (allValid)
            //    {
            //        passwordcombination = 4;
            //    }


                var dataUserpasswordalphachecklist = model.GetUserpolicysettingchecklist(input:
                new UserchangePasswordModel.Userpolicysettinglistcheck
                {
                    UsersettingsMode = 1,
                    FK_Company = _userLoginInfo.FK_Company,

                },
              companyKey: _userLoginInfo.CompanyKey
              );
                long Passwordcombinationnumber = (dataUserpasswordalphachecklist.Data[0].Passminimumlength);
                if (Passwordcombinationnumber != passwordcombination)
                {
                    if (Passwordcombinationnumber == 1)
                        return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password should include only Alphanumeric values" }, Status = "New Password should include only Alphanumeric values" } }, JsonRequestBehavior.AllowGet);
                    if (Passwordcombinationnumber == 2)
                        return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password should include only Numeric values" }, Status = "New Password should include only Numeric values" } }, JsonRequestBehavior.AllowGet);
                    if (Passwordcombinationnumber == 3)
                        return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password should include only Alphabet values" }, Status = "New Password should include only Alphabet values" } }, JsonRequestBehavior.AllowGet);
                    if (Passwordcombinationnumber == 4)
                        return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password should include Alphanumeric values with <br/> Special Characters <br/>and at least 1 Uppercase Letter <br/>" }, Status = "New Password should include Alphanumeric values <br/> Special Characters <br/> at least 1 Uppercase Letter" } }, JsonRequestBehavior.AllowGet);
                    //if (Passwordcombinationnumber == 4)
                    //    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password " }, Status = "New Password should include only Alphabet values" } }, JsonRequestBehavior.AllowGet);


                }
            
                var dataUserpasswordminichecklist = model.GetUserpolicysettingchecklist(input:
               new UserchangePasswordModel.Userpolicysettinglistcheck
               {
                   UsersettingsMode = 2,
                   FK_Company = _userLoginInfo.FK_Company,

               },
             companyKey: _userLoginInfo.CompanyKey
             );
                long Passwordlenght = data.NewPassword.Length;
                long Passwordminlength = (dataUserpasswordminichecklist.Data[0].Passminimumlength);
                if (Passwordlenght < Passwordminlength)
                {

                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password does not met Minimum Length " }, Status = "New Password does not met Minimum Length" } }, JsonRequestBehavior.AllowGet);
                }
                var dataUserpasswordmaxchecklist = model.GetUserpolicysettingchecklist(input:
                 new UserchangePasswordModel.Userpolicysettinglistcheck
                 {
                     UsersettingsMode = 3,
                     FK_Company = _userLoginInfo.FK_Company,

                 },
               companyKey: _userLoginInfo.CompanyKey
               );
                long Passwordmaxlength = (dataUserpasswordmaxchecklist.Data[0].Passminimumlength);
                if (Passwordlenght > Passwordmaxlength)
                {
                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "New Password Length Exceeded" }, Status = "New Password Length Exceeded" } }, JsonRequestBehavior.AllowGet);
                }


                else if (data.NewPassword == data.PresentPassword)
                {
                    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "Present and New Password Are Same " }, Status = "Present and New Password Are Same" } }, JsonRequestBehavior.AllowGet);
                }
                else if (data.NewPassword != data.ConfirmPassword)
                {
                  
                    List<string> _ErrorMessage = new List<string>();

                    _ErrorMessage.Add("Confirm password and  New password Should be Same");

                    return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);

                  
                }
                else
                {
                    UserchangePasswordModel passwordModel = new UserchangePasswordModel();
                    string EncPassword1 = blfun.Encrypt(Convert.ToString(data.UserCode[0].ToString() + data.NewPassword.ToString()));
                    var datresponse = passwordModel.UpdateuserchangepasswordData(input: new UserchangePasswordModel.UpdateUserChangePassword
                    {
                        ID_Users = _userLoginInfo.ID_Users,
                        UserCode = data.UserCode,
                        UserPassword = EncPassword1,
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                        EntrBy = _userLoginInfo.EntrBy,
                        FK_Machine = _userLoginInfo.FK_Machine


                    }, companyKey: _userLoginInfo.CompanyKey);

                    return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        //public ActionResult Userpasswordcheckcondition(UserchangePasswordModel.UserChangePassword data)
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


        //    UserchangePasswordModel model = new UserchangePasswordModel();


        //    var dataUserpasswordminichecklist = model.GetUserpolicysettingchecklist(input:
        //   new UserchangePasswordModel.Userpolicysettinglistcheck
        //   {
        //       UsersettingsMode = 2,
        //       FK_Company = _userLoginInfo.FK_Company,

        //   },
        // companyKey: _userLoginInfo.CompanyKey
        // );
        //    long Passwordlenght = data.NewPassword.Length;
        //    long Passwordminlength = (dataUserpasswordminichecklist.Data[0].Passminimumlength);
        //    if (Passwordlenght < Passwordminlength)
        //    {

        //        return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "Password does not met Minimum Lenght " }, Status = "Password does not met Minimum Lenght" } }, JsonRequestBehavior.AllowGet);
        //    }
        //    var dataUserpasswordmaxchecklist = model.GetUserpolicysettingchecklist(input:
        //     new UserchangePasswordModel.Userpolicysettinglistcheck
        //     {
        //         UsersettingsMode = 3,
        //         FK_Company = _userLoginInfo.FK_Company,

        //     },
        //   companyKey: _userLoginInfo.CompanyKey
        //   );
        //    long Passwordmaxlength = (dataUserpasswordmaxchecklist.Data[0].Passminimumlength);
        //    if (Passwordlenght > Passwordmaxlength)
        //    {
        //        return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "Password Lenght Exceeded" }, Status = "Password Lenght Exceeded" } }, JsonRequestBehavior.AllowGet);
        //    }




        //    return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "Password Invalid" }, Status = "Password Invalid" } }, JsonRequestBehavior.AllowGet);
        //}
    }
}






