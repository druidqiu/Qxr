using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
namespace Qxr.MvcAssist
{
    public static class MvcExtensions
    {
        public static string RenderPartialViewToString(this ControllerContext context, string viewName, object model)
        {
            var controllerName = context.RouteData.Values["controller"].ToString();
            string viewUrl = string.Format(@"~/Views/{0}/{1}.cshtml", controllerName, viewName);

            return RenderViewToString(context, viewUrl, model);
        }

        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(
                    context, viewResult.View,
                    context.Controller.ViewData,
                    context.Controller.TempData, sw);

                try
                {
                    viewResult.View.Render(viewContext, sw);
                }
                catch (Exception ex)
                {
                    return string.Empty;
                }

                return sw.GetStringBuilder().ToString();
            }
        }

        public static string GetModelStateErrors(this ModelStateDictionary modelState)
        {
            var errorMsgs = modelState.Values.SelectMany(t => t.Errors.Select(a => a.ErrorMessage));
            return string.Join(";", errorMsgs);
        }
    }
}
