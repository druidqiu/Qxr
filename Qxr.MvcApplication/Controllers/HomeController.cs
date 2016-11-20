using Qxr.Services.Interfaces;
using System;
using System.Web.Mvc;

namespace Qxr.MvcApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            var randomInt = new Random().Next(1,1000000);
            _userService.AddUser(new Services.Messaging.UserService.AddUserRequest
            {
                UserCode = "abc" + randomInt,
                UserName = "ok" + randomInt
            });
            var users = _userService.GetUsers();

            return View(users);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}