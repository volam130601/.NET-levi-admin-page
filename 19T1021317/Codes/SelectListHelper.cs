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
    }
}