using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021317.BusinessLayers;
using _19T1021317.DomainModels;
using _19T1021317.Webs.Codes;
using _19T1021317.Webs.Models;

namespace _19T1021317.Webs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string SHOPPING_CART = "ShoppingCart";
        private const string ERROR_MESSAGE = "ErrorMessage";
        private const string OrderSearch = "OrderSearch";

        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var input = Session[OrderSearch] as OrderSearchInput ?? new OrderSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
                OrderStatusId = 0
            };

            return View(input);
        }

        /// <summary>
        ///     Display list of orders
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(OrderSearchInput input)
        {
            var data = OrderDataService.ListOrders(input.Page, input.PageSize, input.OrderStatusId, input.SearchValue, out var totalItems);

            var result = new OrderSearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                OrderStatusId = input.OrderStatusId,
                TotalItems = totalItems,
                Data = data
            };

            Session[OrderSearch] = input;

            return View(result);
        }

        /// <summary>
        /// Xem thông tin và chi tiết của đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            try
            {
                var order = OrderDataService.GetOrder(id);
                if (order == null) return RedirectToAction("Index");
                var listOfOrderDetails = OrderDataService.ListOrderDetails(id);

                var employeeId = Converter.CookieToUserAccount(User.Identity.Name).UserId;
                var isAuthorized = Convert.ToInt32(employeeId) == order.EmployeeID;

                var data = new OrderView
                {
                    Order = order,
                    OrderDetails = listOfOrderDetails,
                    IsAuthorized = isAuthorized
                };

                return View(data);
            }
            catch (Exception e)
            {
                ViewBag.Title = "Product | Error";
                return PartialView("Error", e.Message);
            }
        }
        /// <summary>
        /// Giao diện Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("EditDetail/{orderID}/{productID}")]
        public ActionResult EditDetail(int orderID = 0, int productID = 0)
        {
            if (orderID == 0 || productID == 0)
                return RedirectToAction("Index");

            var orderDetail = OrderDataService.GetOrderDetail(orderID, productID);

            return View(orderDetail);
        }
        /// <summary>
        /// Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDetail(OrderDetail data)
        {
            if (data.SalePrice > 0 && data.Quantity > 0)
                OrderDataService.SaveOrderDetail(data.OrderID, data.ProductID, data.Quantity, data.SalePrice);

            return RedirectToAction($"Details/{data.OrderID}");
        }
        /// <summary>
        /// Xóa 1 chi tiết trong đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteDetail/{orderID}/{productID}")]
       [ValidateAntiForgeryToken] public ActionResult DeleteDetail(int orderID = 0, int productID = 0)
        {
            if (orderID == 0 || productID == 0)
                return RedirectToAction("Index");

            OrderDataService.DeleteOrderDetail(orderID, productID);
            return RedirectToAction($"Details/{orderID}");
        }
        /// <summary>
        /// Xóa đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            OrderDataService.DeleteOrder(id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Chấp nhận đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Accept(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            OrderDataService.AcceptOrder(id);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Xác nhận chuyển đơn hàng cho người giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Shipping(int id = 0, int shipperID = 0)
        {
            if (Request.HttpMethod == "GET")
                return View(id);

            if (id == 0 || shipperID == 0)
                return RedirectToAction($"Details/{id}");

            OrderDataService.ShipOrder(id, shipperID);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Ghi nhận hoàn tất thành công đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Finish(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            OrderDataService.FinishOrder(id);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Hủy bỏ đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Cancel(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            OrderDataService.CancelOrder(id);
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reject(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            OrderDataService.RejectOrder(id);
            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Sử dụng 1 biến session để lưu tạm giỏ hàng (danh sách các chi tiết của đơn hàng) trong quá trình xử lý.
        /// Hàm này lấy giỏ hàng hiện đang có trong session (nếu chưa có thì tạo mới giỏ hàng rỗng)
        /// </summary>
        /// <returns></returns>
        private List<OrderDetail> GetShoppingCart()
        {
            if (Session[SHOPPING_CART] is List<OrderDetail> shoppingCart)
                return shoppingCart;
            shoppingCart = new List<OrderDetail>();
            Session[SHOPPING_CART] = shoppingCart;
            return shoppingCart;
        }
        /// <summary>
        /// Giao diện lập đơn hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = TempData[ERROR_MESSAGE] ?? "";
            return View(GetShoppingCart());
        }
        /// <summary>
        /// Tìm kiếm mặt hàng để bổ sung vào giỏ hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult SearchProducts(int page = 1, string searchValue = "")
        {
            var data = ProductDataService.ListProducts(page, 4, searchValue, 0, 0, out var totalItems);
            
            var output = new OrderSearchProductOutput
            {
                Page = page,
                PageSize = 4,
                SearchValue = searchValue,
                TotalItems = totalItems,
                Data = data
            };
             
            return View(output);
        }
        /// <summary>
        /// Bổ sung thêm hàng vào giỏ hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(OrderDetail data)
        {
            if (data == null)
            {
                TempData[ERROR_MESSAGE] = "Dữ liệu không hợp lệ";
                return RedirectToAction("Create");
            }
            if (data.SalePrice <= 0 || data.Quantity <= 0)
            {
                TempData[ERROR_MESSAGE] = "Giá bán và số lượng không hợp lệ";
                return RedirectToAction("Create");
            }

            List<OrderDetail> shoppingCart = GetShoppingCart();
            var existsProduct = shoppingCart.FirstOrDefault(m => m.ProductID == data.ProductID);

            if (existsProduct == null) //Nếu mặt hàng cần được bổ sung chưa có trong giỏ hàng thì bổ sung vào giỏ
            {

                shoppingCart.Add(data);
            }
            else //Trường hợp mặt hàng cần bổ sung đã có thì tăng số lượng và thay đổi đơn giá
            {
                existsProduct.Quantity += data.Quantity;
                existsProduct.SalePrice = data.SalePrice;
            }
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ hàng
        /// </summary>
        /// <param name="id">Mã mặt hàng</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int id = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            int index = shoppingCart.FindIndex(m => m.ProductID == id);
            if (index >= 0)
                shoppingCart.RemoveAt(index);
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCart()
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Khởi tạo đơn hàng (với phần thông tin chi tiết của đơn hàng là giỏ hàng đang có trong session)
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Init(int customerID = 0, int employeeID = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                TempData[ERROR_MESSAGE] = "Không thể tạo đơn hàng với giỏ hàng trống";
                return RedirectToAction("Create");
            }

            if (customerID == 0 || employeeID == 0)
            {
                TempData[ERROR_MESSAGE] = "Vui lòng chọn khách hàng và nhân viên phụ trách";
                return RedirectToAction("Create");
            }

            int orderID = OrderDataService.InitOrder(customerID, employeeID, DateTime.Now, shoppingCart);

            Session.Remove(SHOPPING_CART); //Xóa giỏ hàng 

            return RedirectToAction($"Details/{orderID}");
        }
    }
}