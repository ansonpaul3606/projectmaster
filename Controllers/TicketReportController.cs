using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class TicketReportController : Controller
    {
        // GET: TicketReport
        public ActionResult TicketReport()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();
        }

        public ActionResult LoadTicketForm()
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

            return PartialView("_AddTicketReportForm");
        }
    }
}