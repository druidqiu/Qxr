using System.Web.Mvc;

namespace Qxr.MvcAssist.Binders
{
    internal class BinderManager
    {
        public static void Bind()
        {
            ModelBinders.Binders.Add(typeof(UserSession), new UserModelBinder());
        }
    }
}
