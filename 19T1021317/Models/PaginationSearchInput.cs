using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    ///     Representation of input data for general search and pagination
    /// </summary>
    public class PaginationSearchInput
    {
        /// <summary>
        ///     Page number
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        ///     Number of items per page
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        ///     Search value
        /// </summary>
        public string SearchValue { get; set; } = "";
    }
}