using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class UserPolicySettingsController : Controller
    {
        // GET: UserPolicySettings
        public ActionResult UserPolicySettings()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();
        }
        public ActionResult LoadUserPolicySettingsForm()
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
            //var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            //companyKey: _userLoginInfo.CompanyKey,
            //procedureName: "ProGetNextNo",
            //parameter: new NextSortOrder
            //{
            //    TableName = "PaymentMethod",
            //    FieldName = "SortOrder",
            //    Debug = 0
            //});
            ////Paymntobj.SortOrder = a.Data[0].NextNo;
            return PartialView("_AddUserPolicySettings");
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewUserPolicySettings(UserPolicySettingsModel.UserPolicySettingsView data)
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

           


            UserPolicySettingsModel user = new UserPolicySettingsModel();

            //XDocument doc = new XDocument();




            //string sec = string.Empty;
            //var upsec = "";
            //var dts = data.UplSecMods.Zip(data.UplSecVals, (mods, val) => new { uplmod = mods, uplvals = val });
            //foreach (var rowobj in dts)
            //{
            //    //var upsecmod = new XDocument(new XElement("Upsecmoddata",new XElement("Upsecmode", rowobj.uplmod), new XElement("Upsecvals", rowobj.uplvals)));
            //    var upsecdata = new XElement("Upsecmoddata", rowobj.uplmod.Select(x => new XElement("Upsecmode", x)), rowobj.uplvals.Select(x => new XElement("Upsecval", x)));
            //    upsec = Convert.ToString(upsecdata);
            //    sec += upsec;
            //}


            //var upsecval = new XElement("Upsecvaldata",  data.UplSecVals.Select(x => new XElement("Upsecval", x)));
            //var upsecmod = new XDocument(new XElement("Upsecmoddata",/*new XAttribute("version", "1.0"),*/new XElement("Upsecmode", data.UplSecMods)));
            //var upsecval = new XDocument(new XElement("Upsecvaldata",/*new XAttribute("version", "1.0"),*/new XElement("Upsecval", data.UplSecVals)));
            //var dataresp=new Output();
            //var dts = data.UplSecMods.Zip(data.UplSecVals, (mods, val) => new { uplmod = mods, uplvals = val });
            //foreach (var rowobj in dts) { 

            var datresponse = user.UpdateuserPolicySettings(input: new UserPolicySettingsModel.UpdateUserPolicySettings
            {
                UserAction = 1,
                UplSecMode = 0/*rowobj.uplmod*//*data.UplSecMods*/,
                UplSecValue = ""/*rowobj.uplvals*//*data.UplSecVals*/,
                //UplSecMode = /*Convert.ToString(upsecmod)*//*.Document.ToString(SaveOptions.DisableFormatting)*//*,*/ data.UplSecMods is null ? "" : Common.xmlTostring(data.UplSecMods),
                //UplSecValue = /*Convert.ToString(upsecval)*//*.Document.ToString(SaveOptions.DisableFormatting)*/data.UplSecVals is null ? "" : Common.xmlTostring(data.UplSecVals),
                UplSecdata = data.UplSecdata is null ? "" : Common.xmlTostring(data.UplSecdata),
                FK_Company = _userLoginInfo.FK_Company,
                TransMode = "",
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_UserPolicySettings = 0,
                FK_UserPolicySettings = 0,
                
            }, companyKey: _userLoginInfo.CompanyKey);
                //dataresp = datresponse;
        //}
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateUserpolicySettings(UserPolicySettingsModel.UserPolicySettingsView data)
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

            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("ActStatus");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");


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

            

            UserPolicySettingsModel user = new UserPolicySettingsModel();
            var datresponse = user.UpdateuserPolicySettings(input: new UserPolicySettingsModel.UpdateUserPolicySettings
            {
                UserAction = 1,
                UplSecdata = data.UplSecdata is null ? "" : Common.xmlTostring(data.UplSecdata),
                FK_Company = _userLoginInfo.FK_Company,
                TransMode = "",
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_UserPolicySettings = 0,
                FK_UserPolicySettings = 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetUserPolicysettingsList()
        //{
        //    #region ::  Check User Session to verifyLogin  ::

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

        //    #endregion ::  Check User Session to verifyLogin  ::

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    var data = Common.GetDataViaQuery<UserPolicySettingsModel.UplSecListValues>(parameters: new APIParameters
        //    {
        //        TableName = "UserPolicySettings AS U",
        //        SelectFields = "CASE WHEN BT.FK_BranchMode IN(1,2) THEN (SELECT CONCAT(E.EmpFName,E.EmpLName) AS UserName From Employee AS E WHERE E.ID_Employee =U.FK_Employee) ELSE (SELECT C.CusName AS UserName From CustomerOthers AS C WHERE C.ID_CustomerOthers =U.FK_Employee) END UserName,U.UserCode AS UserCode,B.BrName AS Branch",
        //        Criteria = "U.cancelled=0 AND U.Passed =1 AND U.FK_Company=" + _userLoginInfo.FK_Company,
        //        SortFields = "",
        //        GroupByFileds = ""
        //    },
        //     companyKey: _userLoginInfo.CompanyKey
        //    );

        //    return Json(data, JsonRequestBehavior.AllowGet);



        //}

        //public ActionResult Getuserpolicydata(UserPolicySettingsModel.UserPolicyView data)
        //{
        //    #region ::  Check User Session to verifyLogin  ::

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

        //    #endregion ::  Check User Session to verifyLogin  ::

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


        //    //ModelState.Remove("Name");
        //    //ModelState.Remove("ShortName");
        //    //ModelState.Remove("ActStatus");
        //    //ModelState.Remove("AHeadName");
        //    //ModelState.Remove("ASHeadName");
        //    //ModelState.Remove("AccountHead");
        //    //ModelState.Remove("AccountHeadSub");
        //    //ModelState.Remove("AccountCode");
        //    //ModelState.Remove("AccountSHCode");


        //    #region :: Model validation  ::

        //    //--- Model validation 
        //    if (!ModelState.IsValid)
        //    {

        //        // since no need to continue just return
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = ModelState.Values.SelectMany(m => m.Errors)
        //                                .Select(e => e.ErrorMessage)
        //                                .ToList(),
        //                Status = "Validation failed",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion :: Model validation  ::

        //    UserPolicySettingsModel userpolicy = new UserPolicySettingsModel();

        //    var Paymethod = userpolicy.GetuserpolicyData(companyKey: _userLoginInfo.CompanyKey, input: new UserPolicySettingsModel.GetUserpolicy
        //    {
        //        FK_UserPolicySettings = data.FK_UserPolicySettings,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        EntrBy = _userLoginInfo.EntrBy
        //    });

        //    return Json(Paymethod, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetUserPolicydata(UserPolicySettingsModel.UserPolicyView data)
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
            //UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            //pageAccess = _userLoginInfo.PageAccessRights;
            //ViewBag.PagedAccessRights = pageAccess;
            UserPolicySettingsModel userpolicy = new UserPolicySettingsModel();
            var datas = userpolicy.GetuserpolicyData(companyKey: _userLoginInfo.CompanyKey, input: new UserPolicySettingsModel.GetUserpolicy
            {
                FK_UserPolicySettings = data.FK_UserPolicySettings,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy
            });

            return Json(datas, JsonRequestBehavior.AllowGet);



        }
    }

}