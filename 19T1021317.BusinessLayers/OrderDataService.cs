using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021317.DataLayers;
using _19T1021317.DomainModels;

namespace _19T1021317.BusinessLayers
{
    /// <summary>
    /// Các chức năng nghiệp vụ liên quan đến xử lý đơn hàng
    /// </summary>
    public static class OrderDataService
    {
        private static IOrderDAL orderDB;
        /// <summary>
        /// 
        /// </summary>
        static OrderDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            orderDB = new DataLayers.SQLServer.OrderDAL(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="status"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Order> ListOrders(int page, int pageSize, int status, string searchValue, out int rowCount)
        {
            rowCount = orderDB.Count(status, searchValue);
            return orderDB.List(page, pageSize, status, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static Order GetOrder(int orderID)
        {
            return orderDB.Get(orderID);
        }
        /// <summary>
        /// Khởi tạo 1 đơn hàng mới (tạo đơn hàng mới ở trạng thái Init). Hàm trả về
        /// mã của đơn hàng được tạo mới
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <param name="employeeID">Mã nhân viên phụ trách đơn hàng</param>
        /// <param name="orderTime">Thời điểm tạo đơn hàng</param>
        /// <param name="details">Danh sách hàng được đặt mua trong đơn hàng</param>
        /// <returns></returns>
        public static int InitOrder(int customerID, int employeeID, DateTime orderTime, IEnumerable<OrderDetail> details)
        {
            Order data = new Order()
            {
                CustomerID = customerID,
                EmployeeID = employeeID,
                OrderTime = orderTime,
                AcceptTime = null,
                ShipperID = null,
                ShippedTime = null,
                FinishedTime = null,
                Status = OrderStatus.INIT
            };
            return orderDB.Add(data, details);
        }
        /// <summary>
        /// Hủy bỏ đơn hàng
        /// </summary>
        /// <param name="orderID">Mã đơn hàng cần hủy</param>
        /// <returns></returns>
        public static bool CancelOrder(int orderID)
        {
            var data = orderDB.Get(orderID);
            if (data == null)
                return false;

            if (data.Status == OrderStatus.FINISHED)
                return false;

            data.Status = OrderStatus.CANCEL;
            data.FinishedTime = DateTime.Now;
            return orderDB.Update(data);
        }
        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static bool RejectOrder(int orderID)
        {
            Order data = orderDB.Get(orderID);
            if (data == null)
                return false;

            if (data.Status == OrderStatus.FINISHED)
                return false;

            data.Status = OrderStatus.REJECTED;
            data.FinishedTime = DateTime.Now;
            return orderDB.Update(data);
        }
        /// <summary>
        /// Chấp nhận đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static bool AcceptOrder(int orderID)
        {
            Order data = orderDB.Get(orderID);
            if (data == null)
                return false;

            if (data.Status != OrderStatus.INIT)
                return false;

            data.Status = OrderStatus.ACCEPTED;
            data.AcceptTime = DateTime.Now;
            return orderDB.Update(data);
        }
        /// <summary>
        /// Xác nhận đã chuyển hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static bool ShipOrder(int orderID, int shipperID)
        {
            Order data = orderDB.Get(orderID);
            if (data == null)
                return false;

            if (data.Status != OrderStatus.ACCEPTED)
                return false;

            data.Status = OrderStatus.SHIPPING;
            data.ShipperID = shipperID;
            data.ShippedTime = DateTime.Now;
            return orderDB.Update(data);
        }
        /// <summary>
        /// Ghi nhận kết thúc quá trình xử lý đơn hàng thành công
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static bool FinishOrder(int orderID)
        {
            Order data = orderDB.Get(orderID);
            if (data == null)
                return false;

            if (data.Status != OrderStatus.SHIPPING)
                return false;

            data.Status = OrderStatus.FINISHED;
            data.FinishedTime = DateTime.Now;
            return orderDB.Update(data);
        }
        /// <summary>
        /// Xóa đơn hàng và toàn bộ chi tiết của đơn hàng
        /// (chỉ cho phép xóa đơn hàng đang ở một trong số các trạng thái: vừa khởi tạo, bị hủy hoặc bị từ chối)
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static bool DeleteOrder(int orderID)
        {
            var data = orderDB.Get(orderID);
            if (data == null)
                return false;

            if (data.Status == OrderStatus.INIT || data.Status == OrderStatus.CANCEL || data.Status == OrderStatus.REJECTED)
                return orderDB.Delete(orderID);

            return false;
        }
        /// <summary>
        /// Lấy danh sách chi tiết của đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static List<OrderDetail> ListOrderDetails(int orderID)
        {
            return orderDB.ListDetails(orderID).ToList();
        }
        /// <summary>
        /// Lấy 1 chi tiết của đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static OrderDetail GetOrderDetail(int orderID, int productID)
        {
            return orderDB.GetDetail(orderID, productID);
        }
        /// <summary>
        /// Lưu thông tin chi tiết của đơn hàng (thêm mặt hàng được bán trong đơn hàng) theo nguyên tắc:
        /// - Nếu mặt hàng chưa có trong chi tiết đơn hàng thì bổ sung
        /// - Nếu mặt hàng đã có trong chi tiết đơn hàng thì cập nhật lại số lượng và giá bán
        /// Hàm trả về mã của chi tiết đơn hàng được bổ sung hoặc được cập nhật
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <param name="quantity"></param>
        /// <param name="salePrice"></param>
        /// <returns></returns>
        public static int SaveOrderDetail(int orderID, int productID, int quantity, decimal salePrice)
        {
            return orderDB.SaveDetail(orderID, productID, quantity, salePrice);
        }
        /// <summary>
        /// Xóa 1 chi tiết trong đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteOrderDetail(int orderID, int productID)
        {
            return orderDB.DeleteDetail(orderID, productID);
        }

        /// <summary>
        ///     Get order status (string)
        /// </summary>
        /// <param name="orderStatus"> Order status </param>
        /// <returns> Order status (string) </returns>
        public static string GetOrderStatus(int orderStatus)
        {
            switch (orderStatus)
            {
                case 1: return "Init";
                case 2: return "Accepted";
                case 3: return "Shipping";
                case 4: return "Finished";
                case -1: return "Cancel";
                case -2: return "Rejected";

                default: return "";
            }
        }
    }
}
