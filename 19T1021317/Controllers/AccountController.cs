using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _19T1021317.BusinessLayers;
using _19T1021317.DataLayers;
using Newtonsoft.Json;

namespace _19T1021317.Webs.Controllers
{
    /// <summary>
    ///     Account Controller
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        ///     Login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        ///     Login
        /// </summary>
        /// <param name="username"> Username </param>
        /// <param name="password"> Password </param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ViewBag.Username = username;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("LoginError", "Username or password is required");

                return View();
            }

            var user = UserAccountService.Authenticate(AccountTypes.Employee, username, password);
            if (user == null)
            {
                ModelState.AddModelError("LoginError", "Username or password is incorrect");
                return View();
            }

            var cookie = JsonConvert.SerializeObject(user);
            FormsAuthentication.SetAuthCookie(cookie, false);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        ///     Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }


}