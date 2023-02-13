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
        private int CustomerID { get; set; }

        /// <summary>
        ///     Customer Name
        /// </summary>
        private string CustomerName { get; set; }

        /// <summary>
        ///     Contact Name
        /// </summary>
        private string ContactName { get; set; }

        /// <summary>
        ///     Address
        /// </summary>
        private string Address { get; set; }

        /// <summary>
        ///     City
        /// </summary>
        private string City { get; set; }

        /// <summary>
        ///     Postal Code
        /// </summary>
        private string PostalCode { get; set; }

        /// <summary>
        ///     Country
        /// </summary>
        private string Country { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        private string Email { get; set; }

        /// <summary>
        ///     Password
        /// </summary>
        private string Password { get; set; }
    }
}
