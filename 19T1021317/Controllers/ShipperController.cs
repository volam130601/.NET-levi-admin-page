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
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SHIPPER_SEARCH = "ShipperCondition";
        /// <summary>
        /// Show Index of shipper
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var condition = Session[SHIPPER_SEARCH] as PaginationSearchInput ?? new PaginationSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
            };
            return View(condition);
        }
        /// <summary>
        /// Search a shipper
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input)
        {
            var data = CommonDataService.ListOfShippers(input.Page, input.PageSize, input.SearchValue, out var totalItems);
            var result = new ShipperSearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                TotalItems = totalItems,
                Data = data
            };
            Session[SHIPPER_SEARCH] = input;

            return View(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Shipper Create New";
            var data = new Shipper()
            {
                ShipperId = 0
            };
            return View("Edit", data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(String ID)
        {
            ViewBag.Title = "Shipper Edit";
            int ShipperId = Convert.ToInt32(ID);
            var data = CommonDataService.GetShipperByID(ShipperId);
            return View(data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(String ID)
        {
            int ShipperId = Convert.ToInt32(ID);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetShipperByID(ShipperId);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteShipper(ShipperId);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Save data of Shippers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // atribute kiểm tra antitoken
        public ActionResult Save(Shipper shipper)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(shipper.ShipperName))
                    ModelState.AddModelError("ShipperName", "Shipper Name is required");
                if (string.IsNullOrWhiteSpace(shipper.Phone))
                    ModelState.AddModelError("Phone", "Phone is required");

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = shipper.ShipperId == 0 ? "Shipper | Create" : "Shipper | Edit";
                    return View("Edit", shipper);
                }

                if (shipper.ShipperId == 0)
                    CommonDataService.AddShipper(shipper);
                else
                    CommonDataService.UpdateShipper(shipper);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Title = "Shipper | Error";
                return PartialView("Error", e.Message);
            }

        }
    }
}