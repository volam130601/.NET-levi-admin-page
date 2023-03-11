using _19T1021317.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021317.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến đơn hàng
    /// </summary>
    public interface IOrderDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách đơn hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng trên mỗi trang (0 nếu không phân trang)</param>
        /// <param name="status">Trạng thái đơn hàng (0 nếu không tìm theo trạng thái)</param>
        /// <param name="searchValue">tên khách hàng hoặc tên người giao hàng cần tìm (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        IList<Order> List(int page = 1, int pageSize = 0, int status = 0, string searchValue = "");
        /// <summary>
        /// Đếm số lượng đơn hàng thỏa điều kiện tìm kiếm
        /// </summary>
        /// <param name="status">Trạng thái đơn hàng (-99 nếu không tìm theo trạng thái)</param>
        /// <param name="searchValue">tên khách hàng, tên nhân viên hoặc tên người giao hàng cần tìm (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(int status = -99, string searchValue = "");
        /// <summary>
        /// Lấy thông tin đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        Order Get(int orderID);
        /// <summary>
        /// Bổ sung đơn hàng mới
        /// </summary>
        /// <param name="data">Thông tin đơn hàng</param>
        /// <param name="details">Danh sách hàng được bán trong đơn hàng</param>
        /// <returns></returns>
        int Add(Order data, IEnumerable<OrderDetail> details);
        /// <summary>
        /// Cập nhật thông tin của đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Order data);
        /// <summary>
        /// Xóa đơn hàng (khi xóa đơn hàng thì đồng thời xóa cả chi tiết của đơn hàng)
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        bool Delete(int orderID);

        /// <summary>
        /// Lấy danh sách hàng được bán trong đơn hàng (chi tiết đơn hàng)
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        IList<OrderDetail> ListDetails(int orderID);
        /// <summary>
        /// Lấy thông tin của 1 mặt hàng được bán trong đơn hàng (1 chi tiết trong đơn hàng)
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        OrderDetail GetDetail(int orderID, int productID);
        /// <summary>
        /// Lưu thông tin chi tiết của đơn hàng (thêm mặt hàng được bán trong đơn hàng) theo nguyên tắc:
        /// - Nếu mặt hàng chưa có trong chi tiết đơn hàng thì bổ sung
        /// - Nếu mặt hàng đã có trong chi tiết đơn hàng thì cập nhật lại số lượng và giá bán
        /// Hàm trả về mã của chi tiết đơn hàng được bổ sung hoặc được cập nhật
        /// </summary>        
        /// <returns></returns>
        int SaveDetail(int orderID, int productID, int quantity, decimal salePrice);
        /// <summary>
        /// Xóa chi tiết của đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        bool DeleteDetail(int orderID, int productID);
    }
}
