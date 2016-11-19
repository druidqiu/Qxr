using System.Web.Mvc;

namespace Qxr.MvcAssist.Binders
{
    internal class UserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            UserSession user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (UserSession)controllerContext.HttpContext.Session[AppConfig.UserSessionKey];
            }

            return user;
        }
    }
}
