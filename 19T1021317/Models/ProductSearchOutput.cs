using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021317.DomainModels;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    /// Representation of output data for product search
    /// </summary>
    public class ProductSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// List of suppliers
        /// </summary>
        public List<Product> Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
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