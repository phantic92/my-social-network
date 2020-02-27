using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.ActionFilters
{
    public class PersonFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["person_id"] == null)
            {
                System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
                routeValues.Add("controller", "Profile");
                routeValues.Add("action", "Index");

                filterContext.Result = new RedirectToRouteResult("Default", routeValues);
            }
        }
    }
}