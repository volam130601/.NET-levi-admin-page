using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainModels
{
    /// <summary>
    ///     Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        ///     Customer ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        ///     Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        ///     Contact Name
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        ///     Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Postal Code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        ///     Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Password
        /// </summary>
        public string Password { get; set; }
    }
}
