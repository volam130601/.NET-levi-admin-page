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
        /// <summary>
        /// Đổi 1 giá trị sang giá trị để tương thích với dữ liệu được lưu cơ sở dữ liệu
        /// (Giải thích: vì giá trị null muốn lưu vào CSDL phải chuyển thành DBNull.Value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected object ToDBValue(object value)
        {
            if (value != null)
                return value;
            return DBNull.Value;
        }
        /// <summary>
        /// Chuyển giá trị từ trong CSDL sang kiểu Nullable int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected int? DBValueToNullableInt(object value)
        {
            try
            {
                if (value == DBNull.Value)
                    return null;
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Chuyển giá trị từ trong CSDL sang kiểu Nullable DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected DateTime? DBValueToNullableDateTime(object value)
        {
            try
            {
                if (value == DBNull.Value)
                    return null;
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
    }
}
