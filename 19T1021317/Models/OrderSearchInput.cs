using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderSearchInput : PaginationSearchInput
    {
        /// <summary>
        ///     Order status Id (Default: 0)
        /// </summary>
        public int OrderStatusId { get; set; } = 0;
    }
}