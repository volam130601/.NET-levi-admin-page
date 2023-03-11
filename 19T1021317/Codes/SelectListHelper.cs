using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021317.BusinessLayers;
using _19T1021317.DomainModels;

namespace _19T1021317.Webs.Codes
{
    /// <summary>
    /// Provide utility functions for select list
    /// </summary>
    public class SelectListHelper
    {

        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Text = "-- Select a country --",
                Value = "0"
            });

            foreach (var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;

        }

        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Text = "-- Select a supplier --",
                Value = "0"
            });

            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Text = item.SupplierName,
                    Value = item.SupplierID.ToString()
                });
            }
            return list;
        }
        
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Text = "-- Select a category --",
                Value = ""
            });

            foreach (var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Text = item.CategoryName,
                    Value = item.CategoryID.ToString()
                });
            }
            return list;
        }

        /// <summary>
        ///     Get list of Order Statuses
        /// </summary>
        /// <returns> List of Order Statuses </returns>
        public static IEnumerable<SelectListItem> OrderStatuses()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "-- Select a Status -- ",
                    Value = "0"
                },
                new SelectListItem()
                {
                    Text = "Init",
                    Value = "1"
                },
                 new SelectListItem()
                {
                    Text = "Accepted",
                    Value = "2"
                },
                  new SelectListItem()
                {
                    Text = "Shipping",
                    Value = "3"
                },
                   new SelectListItem()
                {
                    Text = "Finished",
                    Value = "4"
                },
                    new SelectListItem()
                {
                    Text = "Cancel",
                    Value = "-1"
                },
                     new SelectListItem()
                {
                    Text = "Rejected",
                    Value = "-2"
                },
            };

            return list;
        }
    }
}