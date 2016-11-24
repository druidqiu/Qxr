﻿using Qxr.MvcAssist;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Qxr.MvcApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureManager.Run();
            Ioc.UnityInject.UnityWebActivator.Start();
        }

        protected void Application_End()
        {
            Ioc.UnityInject.UnityWebActivator.Shutdown();
        }
    }
}
