using Qxr.MvcAssist.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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