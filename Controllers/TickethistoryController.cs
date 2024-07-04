using PerfectWebERP.Filters;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class TickethistoryController : Controller
    {
        // GET: Tickethistory




        //public ActionResult Tickethistory(string TicketNo)
        //{
        //    //pretend i tested for a real user
        //    TempData["TicketNo"] = TicketNo;
        //    var r = new { result = "Tickethistory1", Urls = Url.Action("Tickethistory1") };
        //    return Json(r);
        //}

        //public ActionResult Tickethistory1()
        //{
        //    if (!TempData.ContainsKey("TicketNo"))
        //    {
        //        //no temp data email.. maybe redirect.. who knows!!
        //        return RedirectToAction("Index");
        //    }

        //    //read the temp data entry.. 
        //    string TicketNo = TempData["TicketNo"].ToString();

        //    //reset the temp data entry
        //    TempData["TicketNo"] = TicketNo;

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    UserAcssRightInfo pageAccess = new UserAcssRightInfo();
        //    pageAccess = _userLoginInfo.PageAccessRights;
        //    ViewBag.Username = _userLoginInfo.UserName;
        //    ViewBag.UserRole = _userLoginInfo.UserRole;
        //    ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
        //    ViewBag.PagedAccessRights = pageAccess;
        //    ViewBag.TicketNo = TicketNo;
        //    return View();
        //}
        public ActionResult Tickethistory(string TicketNo)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.TicketNo = TicketNo;
            return View();
        }


        public ActionResult LoadTicketForm()
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

         
            return PartialView("_AddTicketForm");
        }

        public ActionResult GetTicket(TickethistoryModel.GetTicket dt)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            TickethistoryModel objfld = new TickethistoryModel();


            var data = objfld.GetTicketlist(input: new TickethistoryModel.GetTicket
            {
                TicketNo = dt.TicketNo,
               EntrBy= _userLoginInfo.EntrBy,
               FK_Machine=_userLoginInfo.FK_Machine
              
            },
             companyKey: _userLoginInfo.CompanyKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}