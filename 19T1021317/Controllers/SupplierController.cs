using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021317.Controllers
{
    public class SupplierController : Controller
    {
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Supplier Create New";
            return View("Edit");
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.Title = "Supplier Edit";
            return View();
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return View();
        }
    }
}