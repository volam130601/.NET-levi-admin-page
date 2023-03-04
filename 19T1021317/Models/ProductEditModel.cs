using _19T1021317.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _19T1021317.Webs.Models
{
    /// <summary>
    ///     Product Edit Model
    /// </summary>
    public class ProductEditModel
    {
        /// <summary>
        ///     Product
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        ///     Product Attributes
        /// </summary>
        public List<ProductAttribute> Attributes { get; set; }

        /// <summary>
        ///     Product Photos
        /// </summary>
        public List<ProductPhoto> Photos { get; set; }
    }
}