using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021317.DomainModels;
using _19T1021317.BusinessLayers;

namespace _19T1021317.Webs.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(page, PAGE_SIZE, searchValue, out rowCount);

            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0) pageCount += 1;

            ViewBag.Page = page;
            ViewBag.From = (page - 1) * PAGE_SIZE + 1;
            ViewBag.To = (page != pageCount) ? ((page - 1) * PAGE_SIZE + PAGE_SIZE) : rowCount;
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);
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