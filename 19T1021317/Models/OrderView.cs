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
    public class OrderView
    {
        /// <summary>
        ///     Order
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        ///     List of order details
        /// </summary>
        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        ///     Is authorized
        /// </summary>
        public bool IsAuthorized { get; set; }
    }
}