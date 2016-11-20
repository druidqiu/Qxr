using Qxr.MvcAssist.Filters;
using System.Web.Mvc;

namespace Qxr.MvcApplication.Controllers
{
    [VisitorLog]
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected void AlertMessage(string message)
        {
            if(!string.IsNullOrWhiteSpace(message))
            {
                TempData[AppConfig.PageMessageKey] = message;
            }
        }
    }
}