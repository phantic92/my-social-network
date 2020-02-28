using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.ActionFilters
{
    public class MessageFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var personId = int.Parse(filterContext.RouteData.Values["id"].ToString());

            if (personId != int.Parse(filterContext.HttpContext.Session["person_id"].ToString()))
            {
                System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
                routeValues.Add("controller", "Profile");
                routeValues.Add("action", "ListMessages");
                routeValues.Add("id", filterContext.HttpContext.Session["person_id"]);

                filterContext.Result = new RedirectToRouteResult("Default", routeValues);
            }
        }
    }
}