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
    public class PriorityConversionController : Controller
    {
        // GET: PriorityConversion
        public ActionResult Index(string mtd)
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

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
          
            ViewBag.mtd = mtd;
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;
            ViewBag.Fk_Branch = _userLoginInfo.FK_Branch;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadFormPriorityConversion(string mtd)
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
            PriorityConversionModel.PriorityConversionModelView list = new PriorityConversionModel.PriorityConversionModelView();

          

            var OpBranchListto = Common.GetDataViaQuery<PriorityConversionModel.BranchList>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS FK_Branch,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                      companyKey: _userLoginInfo.CompanyKey

           );
            list.BranchList = OpBranchListto.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddPriorityConversion", list);
        }

        public ActionResult GetLeadPriorityDetails(PriorityConversionModel.Prioritydetailsinput Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            PriorityConversionModel data = new PriorityConversionModel();
            var Lits = data.GetPriorityLeadDetails(input: new PriorityConversionModel.Prioritydetailsinput { FK_Branch = Data.FK_Branch,AsonDate=Data.AsonDate, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);

          
            var PriorityLists = data.GetPriorityList(input: new PriorityConversionModel.GetModeData { Mode = 88 }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Lits, PriorityLists }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddPriorityConversion(PriorityConversionModel.InputPriorityConversionlist data)
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



            bool IsValid = true;
            List<string> _ErrorMessage = new List<string>();

           
          

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

            PriorityConversionModel List = new PriorityConversionModel();

            if (IsValid == true)
            {
                var datresponse = List.UpdatePriorityListData(input: new PriorityConversionModel.UpdatePriorityConversion
                {
                    UserAction=1,
                    FK_Branch = data.FK_Branch,
                    LpcDate = data.AsonDate,
                    PriorityConversionDetails = data.PriorityConversionDetails is null ? "" : Common.xmlTostring(data.PriorityConversionDetails),
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    LpcMode=0,
                   


                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}