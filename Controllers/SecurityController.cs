using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using PerfectWebERPAPI.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Configuration; // Namespace for ConfigurationManager

namespace PerfectWebERP.Controllers
{

    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            string clientType = ConfigurationManager.AppSettings["ClientType"] ?? "0";
            ViewBag.ClientType = clientType;
            return View();
        }
        public ActionResult ForgotPassword()
        {
            string clientType = ConfigurationManager.AppSettings["ClientType"] ?? "0";
            ViewBag.ClientType = clientType;
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPasswordSubmit(User.Login login)
        {
            StringBuilder Passsword = new StringBuilder();
            BlFunctions blfunlog = new BlFunctions();
            Passsword.Append(blfunlog.RandomString(4, true));
            Passsword.Append(blfunlog.RandomNumber(1000, 9999));
            Passsword.Append(blfunlog.RandomString(2, false));
            string companyKey = System.Web.Configuration.WebConfigurationManager.AppSettings["companyKey"]?.ToString();
            
            string Encpasswordlog = blfunlog.Encrypt(Convert.ToString(login.Username[0].ToString() + Passsword));
            User.UserTable userTable = new User.UserTable
            {
                UserCode = login.Username,
                Password = Encpasswordlog,
                FK_Machine = 0,
                FK_Company = 1,
                Mode = 0,

            };
            
            var loginReturndata = Common.GetDataViaProcedure<UserForgotPasswordInfo, User.UserTable>(parameter: userTable, companyKey: companyKey, procedureName: "[ProForgotPassword]");
            List<string> _ErrorMsg = new List<string>();
            string _Status = "";
            bool blnProcess = false;
            if (Convert.ToInt32(loginReturndata.Data[0].ErrCode) == 1)
            {
                string email = loginReturndata.Data[0].UserEmail;
                string maskedEmail = "";
                if(!string.IsNullOrEmpty(email))
                {
                    maskedEmail= string.Format("{0}****{1}", email[0], email.Substring(email.IndexOf('@') - 1));
                }                    
                _ErrorMsg.Add(maskedEmail);
                _Status = "1";
                blnProcess = true;
                sendMail objMail = new sendMail();
           
                 objMail.sendMailDataforgotpassword(loginReturndata.Data[0].ID_Users, "LOGIN", loginReturndata.Data[0].FK_Company, 1, 3, companyKey, Passsword.ToString(), login.Username, loginReturndata.Data[0].UserEmail, loginReturndata.Data[0].EmployeeName);
            }
            else
            {
                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);
            }
            return Json(new { Process = new Output { IsProcess = blnProcess, Message = _ErrorMsg,  Status = _Status } }, JsonRequestBehavior.AllowGet);
        }
    
  
        public ActionResult LoginCheck(User.Login login)
        {
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
            string companyKey = System.Web.Configuration.WebConfigurationManager.AppSettings["companyKey"]?.ToString();
            string sessionName = "FK_Customer";
            string cookieName = "tempPerfectWebErp";

            BlFunctions blfunlog = new BlFunctions();
            string Encpasswordlog = blfunlog.Encrypt(Convert.ToString(login.Username[0].ToString() + login.Passsword));
            
            User.UserTable userTable = new User.UserTable
            {
                UserCode = login.Username,
                Password = Encpasswordlog,
                FK_Machine = 0,
                FK_Company = 1,
                Mode=0,

            };

           

            #region Check User credentials

            BlFunctions blfun = new BlFunctions();
            string EncPassword = blfun.Encrypt(Convert.ToString(login.Username[0].ToString() + login.Passsword));
            //var loginReturndata = Common.GetDataViaProcedure<UserLoginInfo, User.UserTable>(parameter: userTable, companyKey: "", procedureName: "[ProUserLoginCheck]");
            var loginReturndata = Common.GetDataViaProcedure<UserLoginInfo, User.UserTable>(parameter: userTable, companyKey: companyKey, procedureName: "[ProUserLoginCheck]");

            if (loginReturndata.Data[0].ErrCode=="-13")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 1,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg); 

               
                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = "" } }, JsonRequestBehavior.AllowGet);

                //return Json(new { Process = loginAttempt }, JsonRequestBehavior.AllowGet);
            }
            else if (loginReturndata.Data[0].ErrCode == "-91")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = "" } }, JsonRequestBehavior.AllowGet);

                //return Json(new { Process = loginAttempt }, JsonRequestBehavior.AllowGet);
            }
            else if (loginReturndata.Data[0].ErrCode == "-12")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = "" } }, JsonRequestBehavior.AllowGet);

                //return Json(new { Process = loginAttempt }, JsonRequestBehavior.AllowGet);
            }
            else if (loginReturndata.Data[0].ErrCode == "-14")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

               _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = "" } }, JsonRequestBehavior.AllowGet);

                
            }
         
            else  if (loginReturndata.Data[0].ErrCode == "-16")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = "" } }, JsonRequestBehavior.AllowGet);

                //return Json(new { Process = loginAttempt }, JsonRequestBehavior.AllowGet);
            }
            else if (loginReturndata.Data[0].ErrCode == "-17")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = "" } }, JsonRequestBehavior.AllowGet);

               
            }
            else if (loginReturndata.Data[0].ErrCode == "-140")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = loginReturndata.Data[0].ErrCode } }, JsonRequestBehavior.AllowGet);


            }
            else if (loginReturndata.Data[0].ErrCode == "-141")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = loginReturndata.Data[0].ErrCode } }, JsonRequestBehavior.AllowGet);


            }
            else if (loginReturndata.Data[0].ErrCode == "-142")
            {

                User.UserTable userTableLoginAttempt = new User.UserTable
                {
                    UserCode = login.Username,
                    Password = Encpasswordlog,
                    FK_Machine = 0,
                    FK_Company = 1,
                    Mode = 0,

                };

                List<string> _ErrorMsg = new List<string>();

                _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);


                var loginAttempt = Common.UpdateTableData<User.UserTable>(parameter: userTableLoginAttempt, companyKey: "", procedureName: "[ProUserLoginCheck]");

                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMsg, Status = loginReturndata.Data[0].ErrCode } }, JsonRequestBehavior.AllowGet);


            }
            #endregion Check User credentials.
            string guid = Guid.NewGuid().ToString();
            Session[cookieName] = guid;
            Response.Cookies.Add(new HttpCookie(cookieName, guid));
            Session[sessionName] = 1;
            if (loginReturndata.Data != null)
            {
                if (loginReturndata.Data[0].ID_Users > 0)
                {
                    UserLoginInfo setUserLoginInfo = new UserLoginInfo();
                    UserAcssRightInfo accessRights = new UserAcssRightInfo();//get value 
                                                                             //   bool ExpiryDate = false;
                    
                    setUserLoginInfo = new UserLoginInfo
                    {
                        EntrBy = loginReturndata.Data[0].EntrBy,
                        FK_Machine = loginReturndata.Data[0].FK_Machine,
                        CompanyKey = companyKey,
                        //CompanyKey = "",
                        FK_Company = loginReturndata.Data[0].FK_Company,
                        FK_Branch = loginReturndata.Data[0].FK_Branch,
                        FK_Department = loginReturndata.Data[0].FK_Department,
                        FK_BranchCodeUser = loginReturndata.Data[0].FK_BranchCodeUser,
                        UserName = loginReturndata.Data[0].UserName,
                        UserRole = loginReturndata.Data[0].UserRole,
                        UsrrlAdmin = loginReturndata.Data[0].UsrrlAdmin,
                        UsrrlManager = loginReturndata.Data[0].UsrrlManager,
                        UserImage = loginReturndata.Data[0].UserImage,
                        // UserAvatar = "/assets/images/profile/17.jpg",
                        FK_Employee = loginReturndata.Data[0].FK_Employee,
                        FK_UserRole = loginReturndata.Data[0].FK_UserRole,
                        Password = loginReturndata.Data[0].Password,
                        ID_Users = loginReturndata.Data[0].ID_Users,
                        UsrrlMsAdd = loginReturndata.Data[0].UsrrlMsAdd,
                        UsrrlStAdd = loginReturndata.Data[0].UsrrlStAdd,
                        CompanyLogo= loginReturndata.Data[0].CompanyLogo,
                        Company = loginReturndata.Data[0].Company,
                        Branch = loginReturndata.Data[0].Branch,
                        FK_Country= loginReturndata.Data[0].FK_Country,
                        CntryName= loginReturndata.Data[0].CntryName,
                        FK_District = loginReturndata.Data[0].FK_District,
                        DtName= loginReturndata.Data[0].DtName,
                        FK_States = loginReturndata.Data[0].FK_States,
                        StName= loginReturndata.Data[0].StName,
                        CompCategory= loginReturndata.Data[0].CompCategory,
                        UserMrpEdit = loginReturndata.Data[0].UserMrpEdit,
                        UserPriceEdit = loginReturndata.Data[0].UserPriceEdit,
                        RemainingDays = loginReturndata.Data[0].RemainingDays,
                        PageAccessRights = new UserAcssRightInfo
                        {
                            UsrrlTyAdd = loginReturndata.Data[0].UsrrlTyAdd,
                            UsrrlTyEdt = loginReturndata.Data[0].UsrrlTyEdt,
                            UsrrlTyDel = loginReturndata.Data[0].UsrrlTyDel,
                            UsrrlTyView = loginReturndata.Data[0].UsrrlTyView,
                            UsrrlStAdd = loginReturndata.Data[0].UsrrlStAdd,
                            UsrrlStEdt = loginReturndata.Data[0].UsrrlStEdt,
                            UsrrlStDel = loginReturndata.Data[0].UsrrlStDel,
                            UsrrlStView = loginReturndata.Data[0].UsrrlStView,
                            UsrrlMsAdd = loginReturndata.Data[0].UsrrlMsAdd,
                            UsrrlMsEdt = loginReturndata.Data[0].UsrrlMsEdt,
                            UsrrlMsDel = loginReturndata.Data[0].UsrrlMsDel,
                            UsrrlMsth = loginReturndata.Data[0].UsrrlMsth,
                            UsrrlMsView = loginReturndata.Data[0].UsrrlMsView,
                            UsrrlTrAdd = loginReturndata.Data[0].UsrrlTrAdd,
                            UsrrlTrEdt = loginReturndata.Data[0].UsrrlTrEdt,
                            UsrrlTrDel = loginReturndata.Data[0].UsrrlTrDel,
                            UsrrlTrView = loginReturndata.Data[0].UsrrlTrView,
                            UsrrlAppUser = loginReturndata.Data[0].UsrrlAppUser,
                            UsrrlViewRpt = loginReturndata.Data[0].UsrrlViewRpt,
                            UsrrlSecurUser = loginReturndata.Data[0].UsrrlSecurUser,
                            UsrrlSecurBackUp = loginReturndata.Data[0].UsrrlSecurBackUp,
                            UsrrlSecurAuth = loginReturndata.Data[0].UsrrlSecurAuth,
                            UsrrlDayBeg = loginReturndata.Data[0].UsrrlDayBeg,
                            UsrrlPtRpt = loginReturndata.Data[0].UsrrlPtRpt,
                            UsrrlPtVoucher = loginReturndata.Data[0].UsrrlPtVoucher,
                            UsrrlAdmin = loginReturndata.Data[0].UsrrlAdmin,
                            UserEditSalprice = loginReturndata.Data[0].UserEditSalprice

                        }
                    };

                    Session["UserLoginInfo"] = setUserLoginInfo;


                    if (loginReturndata.Data[0].ErrCode == "-15")
                    {
                        List<string> _ErrorMsg = new List<string>();
                        _ErrorMsg.Add(loginReturndata.Data[0].ErrMsg);
                        return Json(new { Process = new Output { IsProcess = true, Message = _ErrorMsg, Status = "2" }, redircetURL = Url.Action("UserchangepasswordIndex", "UserChangepassword") }, JsonRequestBehavior.AllowGet);
                    }

                    else if (loginReturndata.Data[0].UserExpDate != null)
                    {
                        return Json(new { Process = new Output { IsProcess = true, Message = new List<string> { "" }, Status = "2" }, redircetURL = Url.Action("UserchangepasswordIndex", "UserChangepassword") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                      //  var loginAttemptDate = Common.UpdateTableData<User.UserTable>(parameter: userTable, companyKey: "", procedureName: "[ProUserLoginCheck]");

                       

                    }
                }

                return Json(new { Process = new Output { IsProcess = true, Message = new List<string> { "" }, Status = "1" }, redircetURL = Url.Action("Dashboard", "Home") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { "" }, Status = "" } }, JsonRequestBehavior.AllowGet);
            }
        }
       
    }

}
