using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainModels
{
    /// <summary>
    ///     Supplier class
    /// </summary>
    public class Supplier
    {
        /// <summary>
        ///     Supplier Id
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        ///     Supplier Name
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     Supplier Contact Name
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        ///     Supplier Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Supplier Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Supplier Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Supplier City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Supplier Postal Code
        /// </summary>
        public string PostalCode { get; set; }
    }
}
