using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    /// Product Search Input
    /// </summary>
    public class ProductSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; } 
        
    }
}