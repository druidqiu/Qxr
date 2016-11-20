using System;
using System.Web.Mvc;

namespace Qxr.MvcAssist.Filters
{
    public class VisitorLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controllerName = filterContext.RouteData.GetRequiredString("controller");
            var actionName = filterContext.RouteData.GetRequiredString("action");
            LogTxt.Debug(string.Format("visit at: {0}, controller: {1}, action: {2}", DateTime.Now, controllerName, actionName));

            base.OnActionExecuted(filterContext);
        }
    }
}
