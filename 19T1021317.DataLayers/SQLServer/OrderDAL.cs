using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using _19T1021317.DomainModels;
using System.Data;

namespace _19T1021317.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDAL : _BaseDAL, IOrderDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public OrderDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Chuyển dữ liệu từ SqlDataReader thành Order
        /// </summary>
        /// <param name="dbReader"></param>
        /// <returns></returns>
        private Order DataReaderToOrder(SqlDataReader dbReader)
        {
            return new Order()
            {
                OrderID = Convert.ToInt32(dbReader["OrderID"]),
                OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                AcceptTime = DBValueToNullableDateTime(dbReader["AcceptTime"]),
                ShippedTime = DBValueToNullableDateTime(dbReader["ShippedTime"]),
                FinishedTime = DBValueToNullableDateTime(dbReader["FinishedTime"]),
                Status = Convert.ToInt32(dbReader["Status"]),
                CustomerID = DBValueToNullableInt(dbReader["CustomerID"]),
                CustomerName = dbReader["CustomerName"].ToString(),
                CustomerContactName = dbReader["CustomerContactName"].ToString(),
                CustomerAddress = dbReader["CustomerAddress"].ToString(),
                CustomerEmail = dbReader["CustomerEmail"].ToString(),

                EmployeeID = DBValueToNullableInt(dbReader["EmployeeID"]),
                EmployeeFullName = $"{dbReader["EmployeeFirstName"]} {dbReader["EmployeeLastName"]}",

                ShipperID = DBValueToNullableInt(dbReader["ShipperID"]),
                ShipperName = dbReader["ShipperName"].ToString(),
                ShipperPhone = dbReader["ShipperPhone"].ToString()
            };
        }
        /// <summary>
        /// Chuyển dữ liệu từ SqlDataReader thành OrderDetail
        /// </summary>
        /// <param name="dbReader"></param>
        /// <returns></returns>
        private OrderDetail DataReaderToOrderDetail(SqlDataReader dbReader)
        {
            return new OrderDetail()
            {
                OrderDetailID = Convert.ToInt32(dbReader["OrderDetailID"]),
                OrderID = Convert.ToInt32(dbReader["OrderID"]),
                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                ProductName = Convert.ToString(dbReader["ProductName"]),
                Unit = Convert.ToString(dbReader["Unit"]),
                Photo = Convert.ToString(dbReader["Photo"]),
                Quantity = Convert.ToInt32(dbReader["Quantity"]),
                SalePrice = Convert.ToDecimal(dbReader["SalePrice"])
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public int Add(Order data, IEnumerable<OrderDetail> details)
        {
            int orderID = 0;
            using (var connection = OpenConnection())
            {
                //Tạo đơn hàng mới trong CSDL
                var cmdOrder = connection.CreateCommand();
                cmdOrder.CommandText = @"INSERT INTO Orders(CustomerID, OrderTime, EmployeeID, AcceptTime, ShipperID, ShippedTime, FinishedTime, Status)
                                         VALUES(@CustomerID, @OrderTime, @EmployeeID, @AcceptTime, @ShipperID, @ShippedTime, @FinishedTime, @Status);
                                         SELECT SCOPE_IDENTITY()";
                cmdOrder.CommandType = System.Data.CommandType.Text;
                cmdOrder.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmdOrder.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                cmdOrder.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmdOrder.Parameters.Add(new SqlParameter("@AcceptTime", SqlDbType.DateTime) { Value = ToDBValue(data.AcceptTime) });
                cmdOrder.Parameters.Add(new SqlParameter("@ShipperID", SqlDbType.Int) { Value = ToDBValue(data.ShipperID) });
                cmdOrder.Parameters.Add(new SqlParameter("@ShippedTime", SqlDbType.DateTime) { Value = ToDBValue(data.ShippedTime) });
                cmdOrder.Parameters.Add(new SqlParameter("@FinishedTime", SqlDbType.DateTime) { Value = ToDBValue(data.FinishedTime) });
                cmdOrder.Parameters.AddWithValue("@Status", data.Status);

                orderID = Convert.ToInt32(cmdOrder.ExecuteScalar());

                //Bổ sung chi tiết cho đơn hàng có mã là orderID
                var cmdDetail = connection.CreateCommand();
                cmdDetail.CommandText = @"INSERT INTO OrderDetails(OrderID, ProductID, Quantity, SalePrice) " +
                                         "VALUES(@OrderID, @ProductID, @Quantity, @SalePrice)";
                cmdDetail.CommandType = CommandType.Text;
                cmdDetail.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.Int));
                cmdDetail.Parameters.Add(new SqlParameter("@ProductID", SqlDbType.Int));
                cmdDetail.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int));
                cmdDetail.Parameters.Add(new SqlParameter("@SalePrice", SqlDbType.Money));

                foreach (var item in details)
                {
                    cmdDetail.Parameters["@OrderID"].Value = orderID;
                    cmdDetail.Parameters["@ProductID"].Value = item.ProductID;
                    cmdDetail.Parameters["@Quantity"].Value = item.Quantity;
                    cmdDetail.Parameters["@SalePrice"].Value = item.SalePrice;
                    cmdDetail.ExecuteNonQuery();
                }

                connection.Close();
            }
            return orderID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(int status = 0, string searchValue = "")
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT  COUNT(*)
                                    FROM    Orders as o
                                            LEFT JOIN Customers AS c ON o.CustomerID = c.CustomerID
                                            LEFT JOIN Employees AS e ON o.EmployeeID = e.EmployeeID
                                            LEFT JOIN Shippers AS s ON o.ShipperID = s.ShipperID
                                    WHERE   (@Status = 0 OR o.Status = @Status)
                                        AND (@SearchValue = N'' OR c.CustomerName LIKE @SearchValue OR s.ShipperName LIKE @SearchValue)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool Delete(int orderID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM OrderDetails WHERE OrderID = @OrderID;
                                    DELETE FROM Orders WHERE OrderID = @OrderID;";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                result = cmd.ExecuteNonQuery() > 0;

                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool DeleteDetail(int orderID, int productID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM OrderDetails WHERE OrderID = @OrderID AND ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public Order Get(int orderID)
        {
            Order data = null;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT  o.*,
                                            c.CustomerName,
                                            c.ContactName as CustomerContactName,
                                            c.Address as CustomerAddress,
                                            c.Email as CustomerEmail,
                                            e.FirstName as EmployeeFirstName,
                                            e.LastName as EmployeeLastName,
                                            s.ShipperName,
                                            s.Phone as ShipperPhone
                                    FROM    Orders as o
                                            LEFT JOIN Customers AS c ON o.CustomerID = c.CustomerID
                                            LEFT JOIN Employees AS e ON o.EmployeeID = e.EmployeeID
                                            LEFT JOIN Shippers AS s ON o.ShipperID = s.ShipperID
                                    WHERE   o.OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                using (var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = DataReaderToOrder(dbReader);
                    }
                    dbReader.Close();
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public OrderDetail GetDetail(int orderID, int productID)
        {
            OrderDetail data = null;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT	od.*, p.ProductName, p.Unit, p.Photo		
                                    FROM	OrderDetails AS od
		                                    JOIN Products AS p ON od.ProductID = p.ProductID
                                    WHERE	od.OrderID = @OrderID AND od.ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                using (var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = DataReaderToOrderDetail(dbReader);
                    }
                    dbReader.Close();
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="status"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Order> List(int page = 1, int pageSize = 0, int status = 0, string searchValue = "")
        {

            List<Order> data = new List<Order>();
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT  *
                                    FROM    (
                                            SELECT  o.*,
                                                    c.CustomerName,
                                                    c.ContactName as CustomerContactName,
                                                    c.Address as CustomerAddress,
                                                    c.Email as CustomerEmail,
                                                    e.FirstName as EmployeeFirstName,
                                                    e.LastName as EmployeeLastName,
                                                    s.ShipperName,
                                                    s.Phone as ShipperPhone,
                                                    ROW_NUMBER() OVER(ORDER BY o.OrderID DESC) AS RowNumber
                                            FROM    Orders as o
                                                    LEFT JOIN Customers AS c ON o.CustomerID = c.CustomerID
                                                    LEFT JOIN Employees AS e ON o.EmployeeID = e.EmployeeID
                                                    LEFT JOIN Shippers AS s ON o.ShipperID = s.ShipperID
                                            WHERE   (@Status = 0 OR o.Status = @Status)
                                                AND (@SearchValue = N'' OR c.CustomerName LIKE @SearchValue OR s.ShipperName LIKE @SearchValue)
                                            ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN(@Page -1)*@PageSize + 1 AND @Page*@PageSize)
                                    ORDER BY t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                using (var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(DataReaderToOrder(dbReader));
                    }
                    dbReader.Close();
                }

                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public IList<OrderDetail> ListDetails(int orderID)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT	od.*, p.ProductName, p.Unit, p.Photo		
                                    FROM	OrderDetails AS od
		                                    JOIN Products AS p ON od.ProductID = p.ProductID
                                    WHERE	od.OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                using (var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(DataReaderToOrderDetail(dbReader));
                    }
                    dbReader.Close();
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int SaveDetail(int orderID, int productID, int quantity, decimal salePrice)
        {
            int result = 0;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"DECLARE @OrderDetailID int;
                                    SELECT @OrderDetailID = OrderDetailID FROM OrderDetails WHERE OrderID = @OrderID AND ProductID = @ProductID;
                                    IF(@OrderDetailID IS NULL)
                                        BEGIN
                                            INSERT INTO OrderDetails(OrderID, ProductID, Quantity, SalePrice)
                                            VALUES(@OrderID, @ProductID, @Quantity, @SalePrice);
                                            SELECT SCOPE_IDENTITY();
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE OrderDetails SET Quantity = @Quantity, SalePrice = @SalePrice
                                            WHERE OrderDetailID = @OrderDetailID;
                                            SELECT @OrderDetailID;
                                        END";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@SalePrice", salePrice);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Order data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Orders
                                    SET     CustomerID = @CustomerID,
                                            OrderTime = @OrderTime,
                                            EmployeeID = @EmployeeID,
                                            AcceptTime = @AcceptTime,
                                            ShipperID = @ShipperID,
                                            ShippedTime = @ShippedTime,
                                            FinishedTime = @FinishedTime,
                                            Status = @Status
                                    WHERE   OrderID = @OrderID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@OrderID", data.OrderID);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@OrderTime", data.OrderTime);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.Add(new SqlParameter("@AcceptTime", SqlDbType.DateTime) { Value = ToDBValue(data.AcceptTime) });
                cmd.Parameters.Add(new SqlParameter("@ShipperID", SqlDbType.Int) { Value = ToDBValue(data.ShipperID) });
                cmd.Parameters.Add(new SqlParameter("@ShippedTime", SqlDbType.DateTime) { Value = ToDBValue(data.ShippedTime) });
                cmd.Parameters.Add(new SqlParameter("@FinishedTime", SqlDbType.DateTime) { Value = ToDBValue(data.FinishedTime) });
                cmd.Parameters.AddWithValue("@Status", data.Status);

                result = cmd.ExecuteNonQuery() > 0;

                connection.Close();
            }
            return result;
        }
    }
}
