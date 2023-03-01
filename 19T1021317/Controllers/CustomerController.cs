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
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string CUSTOMER_SEARCH = "CustomerCondition";
        /// <summary>
        /// Show Index of supplier
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput ?? new PaginationSearchInput
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
        public ActionResult Search(PaginationSearchInput input)
        {
            var data = CommonDataService.ListOfCustomers(input.Page, input.PageSize, input.SearchValue, out var totalItems);
            var result = new CustomerSearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                TotalItems = totalItems,
                Data = data
            };
            Session[CUSTOMER_SEARCH] = input;

            return View(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Customer Create New";
            var data = new Customer()
            {
                CustomerID = 0
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
            int CustomerID = Convert.ToInt32(ID);
            var data = CommonDataService.GetCustomerByID(CustomerID);
            return View(data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(String ID)
        {
            int CustomerID = Convert.ToInt32(ID);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetCustomerByID(CustomerID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteCustomer(CustomerID);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Save data of Customers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // atribute kiểm tra antitoken
        public ActionResult Save(Customer customer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customer.CustomerName))
                    ModelState.AddModelError("CustomerName", "Customer Name is required");
                if (string.IsNullOrWhiteSpace(customer.Address))
                    ModelState.AddModelError("Address", "Address is required");
                if (string.IsNullOrWhiteSpace(customer.Email))
                    ModelState.AddModelError("Email", "Email is required");
                if (string.IsNullOrWhiteSpace(customer.ContactName))
                    ModelState.AddModelError("ContactName", "Contact Name is required");
                if (string.IsNullOrWhiteSpace(customer.Country))
                    ModelState.AddModelError("Country", "Please select a country");
                if (string.IsNullOrWhiteSpace(customer.City))
                    ModelState.AddModelError("City", "City is required");
                if (string.IsNullOrWhiteSpace(customer.PostalCode))
                    ModelState.AddModelError("PostalCode", "Postal Code is required");
                if (string.IsNullOrWhiteSpace(customer.Password))
                    ModelState.AddModelError("Password", "Password is required");

                return RedirectToAction("Index");
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = customer.CustomerID == 0 ? "Customer | Create" : "Customer | Edit";
                    return View("Edit", customer);
                }

                if (customer.CustomerID == 0)
                    CommonDataService.AddCustomer(customer);
                else
                    CommonDataService.UpdateCustomer(customer);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Title = "Customer | Error";
                return PartialView("Error", e.Message);
            }


        }
    }
}