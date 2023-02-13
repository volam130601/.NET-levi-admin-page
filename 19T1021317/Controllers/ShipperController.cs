using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _19T1021317.Webs.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
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
            return View("Edit");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
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