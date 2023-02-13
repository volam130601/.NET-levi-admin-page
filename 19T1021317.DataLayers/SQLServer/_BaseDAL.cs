using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _19T1021317.DataLayers.SQLServer
{

    /// <summary>
    /// Base class for classes setting action access data on SQL Server
    /// </summary>
    public abstract class _BaseDAL
    {

        /// <summary>
        /// Paramater string connect CSDL
        /// </summary>
        protected string _connectionString;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>
        /// Create and open connection to CSDL
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
