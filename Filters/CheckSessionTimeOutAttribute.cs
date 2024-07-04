using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace PerfectWebERP.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CheckSessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)

        {
            string sessionName = "UserLoginInfo";
            string cookieName = "tempPerfectWebErp";

            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;
            string AuthTokeninCookies = "";//System.Web.HttpCookieCollection.this[string].get
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                AuthTokeninCookies = HttpContext.Current.Request.Cookies[cookieName].Value;
            }

            if (controller != null)
            {
                if (session[sessionName] == null || session[cookieName] == null
                       || AuthTokeninCookies == null || AuthTokeninCookies != session[cookieName].ToString())
                {

                    filterContext.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Security" },{ "action", "LogOut" }
                                         });

                }

                base.OnActionExecuting(filterContext);
            }
        }
    }
}