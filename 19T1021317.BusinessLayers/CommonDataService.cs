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
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Employee> employeeDB;
        private static ICommonDAL<Category> categoryDB;

        /// <summary>
        /// Constructor
        /// </summary>
        static CommonDataService ()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
        }

        #region The following fucnions are realted to Country
        /// <summary>
        /// Get list of Country
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }

        #endregion

        #region The following functions are related to Supplier

        /// <summary>
        ///     Get list of suppliers
        /// </summary>
        /// <param name="page"> Page number (Default: 1) </param>
        /// <param name="pageSize"> Number of items per page (Default: 10) </param>
        /// <param name="searchValue"> Search value (Default: "") </param>
        /// <param name="rowCount"> Total number of rows </param>
        /// <returns> List of suppliers </returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Get list of suppliers not navigation
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers()
        {
            return supplierDB.List().ToList();
        }

        /// <summary>
        ///     Get supplier by id
        /// </summary>
        /// <param name="id"> SupplierID </param>
        /// <returns> Supplier </returns>
        public static Supplier GetSupplierById(int id)
        {
            return supplierDB.Get(id);
        }

        /// <summary>
        ///     Add new supplier
        /// </summary>
        /// <param name="data"> Supplier data </param>
        /// <returns> SupplierID </returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        ///     Update supplier
        /// </summary>
        /// <param name="data"> Supplier data </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        ///     Delete supplier
        /// </summary>
        /// <param name="supplierId"> SupplierID </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool DeleteSupplier(int supplierId)
        {
            return supplierDB.Delete(supplierId);
        }

        /// <summary>
        ///     Check if supplier is used
        /// </summary>
        /// <param name="supplierId"> SupplierID </param>
        /// <returns> True if used, otherwise false </returns>
        public static bool IsUsedSupplier(int supplierId)
        {
            return supplierDB.IsUsed(supplierId);
        }

        #endregion

        #region The following functions are related to Category

        /// <summary>
        ///  Get lsit of categories
        /// </summary>
        /// <param name="page"> Page number (Default: 1) </param>
        /// <param name="pageSize"> Number of items per page (Default: 10) </param>
        /// <param name="searchValue"> Search value (Default: "") </param>
        /// <param name="rowCount"> Total number of rows </param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page , int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        ///   Search and get list of categories(don't pagination)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(string searchValue)
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        ///   Get list of categories(don't pagination)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories()
        {
            return categoryDB.List().ToList();
        }

        /// <summary>
        ///     Get category by id
        /// </summary>
        /// <param name="id"> CategoryID </param>
        /// <returns> Supplier </returns>
        public static Category GetCategoryByID(int id)
        {
            return categoryDB.Get(id);
        }

        /// <summary>
        ///     Add new category
        /// </summary>
        /// <param name="data"> Category data </param>
        /// <returns> SupplierID </returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        ///     Update Category
        /// </summary>
        /// <param name="data"> Category data </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        ///     Delete Category
        /// </summary>
        /// <param name="categoryId"> CategoryID </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool DeleteCategory(int categoryId)
        {
            return categoryDB.Delete(categoryId);
        }

        /// <summary>
        ///     Check if category is used
        /// </summary>
        /// <param name="categoryId"> categoryID </param>
        /// <returns> True if used, otherwise false </returns>
        public static bool IsUsedCategoryId(int categoryID)
        {
            return categoryDB.IsUsed(categoryID);
        }

        #endregion

        #region The following functions are related to Customer

        /// <summary>
        ///  Get lsit of Customers
        /// </summary>
        /// <param name="page"> Page number (Default: 1) </param>
        /// <param name="pageSize"> Number of items per page (Default: 10) </param>
        /// <param name="searchValue"> Search value (Default: "") </param>
        /// <param name="rowCount"> Total number of rows </param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        ///     Get Customer by id
        /// </summary>
        /// <param name="id"> CustomerID </param>
        /// <returns> Supplier </returns>
        public static Customer GetCustomerByID(int id)
        {
            return customerDB.Get(id);
        }

        /// <summary>
        ///     Add new Customer
        /// </summary>
        /// <param name="data"> Customer data </param>
        /// <returns> CustomerID </returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }

        /// <summary>
        ///     Update Customer
        /// </summary>
        /// <param name="data"> Customer data </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }

        /// <summary>
        ///     Delete Customer
        /// </summary>
        /// <param name="CustomerID"> CustomerID </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool DeleteCustomer(int CustomerID)
        {
            return customerDB.Delete(CustomerID);
        }

        /// <summary>
        ///     Check if Customer is used
        /// </summary>
        /// <param name="CustomerID"> CustomerID </param>
        /// <returns> True if used, otherwise false </returns>
        public static bool IsUsedCustomerID(int CustomerID)
        {
            return customerDB.IsUsed(CustomerID);
        }

        #endregion

        #region The following functions are related to Employee

        /// <summary>
        ///  Get list of Employees
        /// </summary>
        /// <param name="page"> Page number (Default: 1) </param>
        /// <param name="pageSize"> Number of items per page (Default: 10) </param>
        /// <param name="searchValue"> Search value (Default: "") </param>
        /// <param name="rowCount"> Total number of rows </param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        ///     Get Employee by id
        /// </summary>
        /// <param name="id"> EmployeeID </param>
        /// <returns> Supplier </returns>
        public static Employee GetEmployeeByID(int id)
        {
            return employeeDB.Get(id);
        }

        /// <summary>
        ///     Add new Employee
        /// </summary>
        /// <param name="data"> Employee data </param>
        /// <returns> CustomerID </returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        ///     Update Employee
        /// </summary>
        /// <param name="data"> Employee data </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        ///     Delete Employee
        /// </summary>
        /// <param name="EmployeeID"> EmployeeID </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool DeleteEmployee(int EmployeeID)
        {
            return employeeDB.Delete(EmployeeID);
        }

        /// <summary>
        ///     Check if Employee is used
        /// </summary>
        /// <param name="EmployeeID"> EmployeeID </param>
        /// <returns> True if used, otherwise false </returns>
        public static bool IsUsedEmployeeID(int EmployeeID)
        {
            return employeeDB.IsUsed(EmployeeID);
        }

        #endregion

        #region The following functions are related to Shipper

        /// <summary>
        ///  Get list of Shippers
        /// </summary>
        /// <param name="page"> Page number (Default: 1) </param>
        /// <param name="pageSize"> Number of items per page (Default: 10) </param>
        /// <param name="searchValue"> Search value (Default: "") </param>
        /// <param name="rowCount"> Total number of rows </param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        ///     Get Shipper by id
        /// </summary>
        /// <param name="id"> ShipperID </param>
        /// <returns> Supplier </returns>
        public static Shipper GetShipperByID(int id)
        {
            return shipperDB.Get(id);
        }

        /// <summary>
        ///     Add new Shipper
        /// </summary>
        /// <param name="data"> Shipper data </param>
        /// <returns> ShipperID </returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        ///     Update Shipper
        /// </summary>
        /// <param name="data"> Shipper data </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        ///     Delete Shipper
        /// </summary>
        /// <param name="ShipperID"> ShipperID </param>
        /// <returns> True if success, otherwise false </returns>
        public static bool DeleteShipper(int ShipperID)
        {
            return shipperDB.Delete(ShipperID);
        }

        /// <summary>
        ///     Check if Employee is used
        /// </summary>
        /// <param name="ShipperID"> ShipperID </param>
        /// <returns> True if used, otherwise false </returns>
        public static bool IsUsedShipperID(int ShipperID)
        {
            return shipperDB.IsUsed(ShipperID);
        }

        #endregion
    
    }
}
