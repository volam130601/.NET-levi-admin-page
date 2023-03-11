using _19T1021317.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        ///     Data
        /// </summary>
        public List<Order> Data { get; set; }

        /// <summary>
        ///     Order status id
        /// </summary>
        public int OrderStatusId { get; set; }

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