using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DataLayers
{

    /// <summary>
    /// Definition of common datas access layer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Search value and get list of T
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="searchValue">Search Value</param>
        /// <returns>List of T</returns>
        IList<T> List(int page = 1 , int pageSize =  0 , string searchValue = "");


        /// <summary>
        /// Count of items
        /// </summary>
        /// <param name="searchValue">Search Value</param>
        /// <returns>
        ///     Number of items
        /// </returns>
        int Count(string searchValue = "");

        /// <summary>
        /// Get data of T
        /// </summary>
        /// <param name="id">T ID</param>
        /// <returns>Data of T</returns>
        T Get(int id);

        /// <summary>
        /// Add new data of T
        /// </summary>
        /// <param name="data">T Data</param>
        /// <returns>ID of T</returns>
        int Add(T data);

        /// <summary>
        ///     Update T
        /// </summary>
        /// <param name="data">T data</param>
        /// <returns>
        ///     Success or not
        /// </returns>
        bool Update(T data);

        /// <summary>
        ///     Delete T
        /// </summary>
        /// <param name="id">T Id</param>
        /// <returns>
        ///     Success or not
        /// </returns>
        bool Delete(int id);


        /// <summary>
        ///     Check if T is used
        /// </summary>
        /// <param name="id">T id</param>
        /// <returns>
        ///     Used or not
        /// </returns>
        bool IsUsed(int id);

    }
}
