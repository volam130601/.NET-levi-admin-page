using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;

namespace _19T1021317.Webs.Codes
{
    /// <summary>
    /// Provide functions convert datatypes
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Convert string to DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? DMYStringToDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }

    }
}