using Qxr.MvcAssist;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Qxr.MvcApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private AutofacDependency.Dependency AutoBootstrapper { get; set; }

        protected MvcApplication()
        {
            AutoBootstrapper = new AutofacDependency.Dependency();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureManager.Run();
            
            AutoBootstrapper.Register(Assembly.GetExecutingAssembly());
        }

        protected void Application_End()
        {
            AutoBootstrapper.Shutdown();
        }
    }
}
