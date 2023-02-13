using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021317.DomainModels;
using _19T1021317.DataLayers;
using System.Configuration;

namespace _19T1021317.BusinessLayers
{

    /// <summary>
    /// Provide professional function handle to common data related to:
    /// Country , Supplier , Customer , Shipper , Employee , Category
    /// </summary>
    public static class CommonDataService
    {
        private static ICountryDAL countryDB;

        /// <summary>
        /// Constructor
        /// </summary>
        static CommonDataService ()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);

        }

        #region Handle related to country
        /// <summary>
        /// Get list of Country
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }

        #endregion
    }
}
