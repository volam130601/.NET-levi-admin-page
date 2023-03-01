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
    [Authorize]
    public class SupplierController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SUPPLIER_SEARCH = "SupplierCondition";
       
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
        public ActionResult Save(Supplier supplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                    ModelState.AddModelError("SupplierName", "Supplier Name is required");
                if (string.IsNullOrWhiteSpace(supplier.Address))
                    ModelState.AddModelError("Address", "Address is required");
                if (string.IsNullOrWhiteSpace(supplier.Phone))
                    ModelState.AddModelError("Phone", "Phone is required");
                if (string.IsNullOrWhiteSpace(supplier.ContactName))
                    ModelState.AddModelError("ContactName", "Contact Name is required");
                if (string.IsNullOrWhiteSpace(supplier.Country))
                    ModelState.AddModelError("Country", "Please select a country");
                if (string.IsNullOrWhiteSpace(supplier.City))
                    ModelState.AddModelError("City", "City is required");
                if (string.IsNullOrWhiteSpace(supplier.PostalCode))
                    ModelState.AddModelError("PostalCode", "Postal Code is required");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = supplier.SupplierID == 0 ? "Supplier | Create" : "Supplier | Edit";
                    return View("Edit", supplier);
                }

                if (supplier.SupplierID == 0)
                    CommonDataService.AddSupplier(supplier);
                else
                    CommonDataService.UpdateSupplier(supplier);
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