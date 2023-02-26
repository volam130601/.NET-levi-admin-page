using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    ///     Representation of output data for general search and pagination
    /// </summary>
    public abstract class PaginationSearchOutput
    {
        /// <summary>
        ///     Page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///     Number of items per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Total number of items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        ///     Total number of pages
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;
                var totalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                return totalPages == 0 ? 1 : totalPages;
            }
        }

        /// <summary>
        ///     Search value
        /// </summary>
        public string SearchValue { get; set; }
    }
}