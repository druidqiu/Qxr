using Qxr.MvcAssist.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qxr.MvcApplication.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;
        public AccountController()
        {
            authProvider = new FormsAuthProvider();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string pwd)
        {
            authProvider.Authenticate(username, () =>
            {
                return username == "admin" && pwd == "Win2003@";
            });

            Session[AppConfig.UserSessionKey] = new UserSession(username, "admin");

            return RedirectToAction("LoginInfo");
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            authProvider.SignOut();
            return Redirect(authProvider.LoginUrl);
        }

        public ActionResult LoginInfo(UserSession user)
        {
            return Content(string.Format("username: {0}, role: {1}", user.Username, user.Role));
        }
    }
}