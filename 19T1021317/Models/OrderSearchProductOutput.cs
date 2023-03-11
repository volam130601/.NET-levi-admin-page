using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _19T1021317.DomainModels;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderSearchProductOutput : PaginationSearchOutput
    {
        /// <summary>
        ///     Data
        /// </summary>
        public List<Product> Data { get; set; }

        /// <summary>
        ///     From
        /// </summary>
        public int From => (Page - 1) * PageSize + 1;

        /// <summary>
        ///     To
        /// </summary>
        public int To => (Page - 1) * PageSize + Data.Count;
    }
}