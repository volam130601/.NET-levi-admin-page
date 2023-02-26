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
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SupplierCondition";
       
       /* public ActionResult Index(int page = 1 , string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(page, PAGE_SIZE, searchValue ,out rowCount);

            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0) pageCount += 1;

            ViewBag.Page = page;
            ViewBag.From = (page - 1) * PAGE_SIZE + 1;
            ViewBag.To = (page != pageCount) ? ((page - 1) * PAGE_SIZE + PAGE_SIZE) : rowCount;
            ViewBag.RowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);
        }*/
       /// <summary>
       /// Show Index of supplier
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            var condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput ?? new PaginationSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
            };
            return View(condition);
        }
        /// <summary>
        /// Search a supplier
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input) {
            var data = CommonDataService.ListOfSuppliers(input.Page, input.PageSize, input.SearchValue, out var totalItems);
            var result = new SupplierSearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                TotalItems = totalItems,
                Data = data
            };
            Session[SUPPLIER_SEARCH] = input;

            return View(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Supplier Create New";
            var data = new Supplier()
            {
                SupplierID = 0
            };
            return View("Edit", data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(String ID)
        {
            ViewBag.Title = "Supplier Edit";
            int SupplierID = Convert.ToInt32(ID);
            var data = CommonDataService.GetSupplierById(SupplierID);
            return View(data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(String ID)
        {
            int SupplierID = Convert.ToInt32(ID);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetSupplierById(SupplierID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteSupplier(SupplierID);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Save data of Suppliers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // atribute kiểm tra antitoken
        public ActionResult Save(Supplier data)
        {
            if (data.SupplierID == 0)
            {
                CommonDataService.AddSupplier(data);
            }
            else
            {
                CommonDataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index");

        }

    }
}