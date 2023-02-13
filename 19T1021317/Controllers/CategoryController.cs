using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021317.Webs.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
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
            ViewBag.Title = "Category Create New";
            return View("Edit");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.Title = "Category Edit";
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