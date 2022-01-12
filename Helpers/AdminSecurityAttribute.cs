using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoChek.Helpers
{
    public class AdminSecurityAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpCookie cookie = HttpContext.Current.Request.Cookies[Helper.adminCookie];

            if (cookie != null)
            {
                //filterContext.Result
                //TODO 
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "action","index"},{"controller","home" } });
            }

            base.OnActionExecuting(filterContext);  
        }
    }
}