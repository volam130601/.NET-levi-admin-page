using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainModels
{
    /// <summary>
    ///     Category
    /// </summary>
    public class Category
    {
        /// <summary>
        ///     Category ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        ///     Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Parent Category ID
        /// </summary>
        public int ParentCategoryId { get; set; }
    }
}
