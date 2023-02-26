﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using _19T1021317.DomainModels;
using _19T1021317.BusinessLayers;
using _19T1021317.Webs.Models;
namespace _19T1021317.Webs.Controllers
{
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string EMPLOYEE_SEARCH = "EmployeerCondition";
        /// <summary>
        /// Show Index of employee
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput ?? new PaginationSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
            };
            return View(condition);
        }
        /// <summary>
        /// Search a employee
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput input)
        {
            var data = CommonDataService.ListOfEmployees(input.Page, input.PageSize, input.SearchValue, out var totalItems);
            var result = new EmployeeSearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                TotalItems = totalItems,
                Data = data
            };
            Session[EMPLOYEE_SEARCH] = input;

            return View(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.Title = "Employee Create New";
            var data = new Employee()
            {
                EmployeeID = 0
            };
            return View("Edit", data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(String ID)
        {
            ViewBag.Title = "Employee Edit";
            int EmployeeID = Convert.ToInt32(ID);
            var data = CommonDataService.GetEmployeeByID(EmployeeID);
            return View(data);
        }
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(String ID)
        {
            int EmployeeID = Convert.ToInt32(ID);
            if (Request.HttpMethod == "GET")
            {
                var data = CommonDataService.GetEmployeeByID(EmployeeID);
                return View(data);
            }
            else
            {
                CommonDataService.DeleteEmployee(EmployeeID);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Save data of Employees
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // atribute kiểm tra antitoken
        public ActionResult Save(Employee data)
        {
            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");

        }
    }
}