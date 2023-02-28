using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021317.DomainModels;
using _19T1021317.BusinessLayers;
using _19T1021317.Webs.Models;

namespace _19T1021317.Webs.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CATEGORY_SEARCH = "CategoryCondition";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var condition = Session[CATEGORY_SEARCH] as PaginationSearchInput ?? new PaginationSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
            };
            return View(condition);
        }

        /// <summary>
        /// Search a category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input)
        {
            var data = CommonDataService.ListOfCategories(input.Page, input.PageSize, input.SearchValue, out var totalItems);
            var result = new CategorySearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                TotalItems = totalItems,
                Data = data
            };
            Session[CATEGORY_SEARCH] = input;

            return View(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Category Create New";
            var data = new Category()
            {
                CategoryID = 0
            };
            return View("Edit", data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(String ID)
        {
            ViewBag.Title = "Category Edit";
            int CategoryID = Convert.ToInt32(ID);
            var data = CommonDataService.GetCategoryByID(CategoryID);
            return View(data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(String ID)
        {
            int CategoryID = Convert.ToInt32(ID);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCategoryByID(CategoryID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCategory(CategoryID);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Save data of categories
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // atribute kiểm tra antitoken
        public ActionResult Save(Category category)
        {
            try
            {
                if (string.IsNullOrEmpty(category.CategoryName))
                    ModelState.AddModelError("CategoryName",
                                             "Category Name is required"
                    );
                if (string.IsNullOrEmpty(category.Description))
                    ModelState.AddModelError("Description",
                                             "Description is required"
                    );
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = category.CategoryID == 0 ? "Category | Create" : "Category | Edit";
                    return View("Edit", category);
                }

                if (category.CategoryID == 0)
                    CommonDataService.AddCategory(category);
                else
                    CommonDataService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Title = "Supplier | Error";
                return PartialView("Error", e.Message);
            }


        }
    }
}