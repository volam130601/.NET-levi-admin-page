using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021317.DomainModels;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    /// Output data of category search 
    /// </summary>
    public class CategorySearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// List of suppliers
        /// </summary>
        public List<Category> Data { get; set; }

        /// <summary>
        /// From
        /// </summary>
        public int From => (Page - 1) * PageSize + 1;

        /// <summary>
        /// From
        /// </summary>
        public int To => (Page - 1) * PageSize + Data.Count;
    }
}