using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainModels
{
    /// <summary>
    ///     Employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        ///     Employee ID
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        ///     Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Birth Date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        ///     Photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        ///     Notes
        /// </summary>
        public string Notes { get; set; }

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
