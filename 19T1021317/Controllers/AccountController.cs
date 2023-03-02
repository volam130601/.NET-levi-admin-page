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
        ///     Change password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="username"> Username </param>
        /// <param name="oldPassword"> Password </param>
        /// <param name="newPassword"> Re-Password </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            ViewBag.UserName = username;
            ViewBag.OldPassword = oldPassword;
            ViewBag.NewPassword = newPassword;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("ChangePasswordError", "Username or old-password or new-password is required");
                Console.WriteLine("abc");
                return View();
            }

            if (oldPassword.Equals(newPassword))
            {
                ModelState.AddModelError("ChangePasswordError", "New password and old password must is diff");

                return View();
            }

            var result = UserAccountService.ChangePassword(AccountTypes.Employee, username, oldPassword, newPassword);

            if (result)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("ChangePasswordError", "Username or old password is invalid");
            return View();
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