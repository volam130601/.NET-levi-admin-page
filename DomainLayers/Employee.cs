using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DomainLayers
{
    /// <summary>
    ///     Employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        ///     Employee ID
        /// </summary>
        private int EmployeeID { get; set; }

        /// <summary>
        ///     Last Name
        /// </summary>
        private string LastName { get; set; }

        /// <summary>
        ///     First Name
        /// </summary>
        private string FirstName { get; set; }

        /// <summary>
        ///     Birth Date
        /// </summary>
        private DateTime BirthDate { get; set; }

        /// <summary>
        ///     Photo
        /// </summary>
        private string Photo { get; set; }

        /// <summary>
        ///     Notes
        /// </summary>
        private string Notes { get; set; }

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
