using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021317.DomainModels;
using _19T1021317.BusinessLayers;
using _19T1021317.Webs.Models;
using System.Diagnostics;
using System.IO;

namespace _19T1021317.Webs.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private const int PAGE_SIZE = 5;
        private const string PRODUCT_SEARCH = "ProductCondition";
        
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var condition = Session[PRODUCT_SEARCH] as ProductSearchInput ?? new ProductSearchInput
            {
                Page = 1,
                PageSize = PAGE_SIZE,
                SearchValue = "",
                CategoryID = 0,
                SupplierID = 0,
            };
            return View(condition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(ProductSearchInput input)
        {
            var data = ProductDataService.ListProducts(input.Page, input.PageSize, input.SearchValue, input.CategoryID, input.SupplierID, out var totalItems);
            var result = new ProductSearchOutput
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                CategoryID = input.CategoryID,
                SupplierID = input.SupplierID,
                TotalItems = totalItems,
                Data = data,
            };
            Session[PRODUCT_SEARCH] = input;

            return View(result);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var product = new Product
            {
                ProductID = 0
            };

            return View(product);

        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Edit(int id = 0)
        {
            var product = ProductDataService.GetProduct(id);
            var data = new ProductEditModel
            {
                Product = product,
                Attributes = ProductDataService.ListAttributes(id),
                Photos = ProductDataService.ListPhotos(id)
            };

            return View(data);

        }

        /// <summary>
        ///     Save a product information
        /// </summary>
        /// <param name="product"> Product </param>
        /// <param name="uploadPhoto"> Photo </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.ProductName))
                    ModelState.AddModelError("ProductName", "Product Name is required");
                if (product.CategoryID == 0)
                    ModelState.AddModelError("CategoryID", "Category ID is required");
                if (product.SupplierID == 0)
                    ModelState.AddModelError("SupplierID", "Supplier ID is required");
                if (product.Price == 0)
                    ModelState.AddModelError("Price", "Price is required");
                if (string.IsNullOrWhiteSpace(product.Unit))
                    ModelState.AddModelError("Unit", "Unit is required");
                if (uploadPhoto != null)
                {
                    var path = Server.MapPath("~/Photos");
                    var fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    var filePath = Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    product.Photo = fileName;
                }
                if (product.ProductID == 0)
                {
                    if (uploadPhoto == null)
                        ModelState.AddModelError("Photo", "Photo is required");

                    if (!ModelState.IsValid)
                        return View("Create", product);

                    ProductDataService.AddProduct(product);
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        var data = new ProductEditModel
                        {
                            Product = product,
                            Attributes = ProductDataService.ListAttributes(product.ProductID),
                            Photos = ProductDataService.ListPhotos(product.ProductID)
                        };

                        return View("Edit", data);
                    }
                    ProductDataService.UpdateProduct(product);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Title = "Product | Error";
                return PartialView("Error", e.Message);
            }
        }


        /// <summary>
        /// Xóa mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("Index");

            ViewBag.Title = "Product | Delete";

            if (Request.HttpMethod == "GET")
            {
                var product = ProductDataService.GetProduct(id);
                if (product == null)
                    return RedirectToAction("Index");

                return View(product);
            }

            try
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Title = "Product | Error";
                return PartialView("Error", e.Message);
            }

        }

        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method = "add", int productID = 0, long photoID = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Add Photo";

                    var productPhoto = new ProductPhoto
                    {
                        PhotoID = 0,
                        ProductID = productID,
                        DisplayOrder = 1
                    };
                    return View(productPhoto);
                case "edit":
                    ViewBag.Title = "Edit Photo";

                    var photo = ProductDataService.GetPhoto(productID);

                    return View(photo);
                case "delete":
                    ProductDataService.DeletePhoto(productID);
                    return RedirectToAction("Edit", new { id = productID });
                default:
                    return RedirectToAction("Index");
            }

        }

        /// <summary>
        ///     Save a photo of a product
        /// </summary>
        /// <param name="productPhoto"> Product Photo </param>
        /// <param name="uploadPhoto"> Photo </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePhoto(ProductPhoto productPhoto, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productPhoto.Description))
                    ModelState.AddModelError("Description", "Description is required");
                if (productPhoto.DisplayOrder == 0)
                    ModelState.AddModelError("DisplayOrder", "DisplayOrder is required");

                if (uploadPhoto != null)
                {
                    var path = Server.MapPath("~/Photos");
                    var fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    var filePath = Path.Combine(path, fileName);
                    uploadPhoto.SaveAs(filePath);
                    productPhoto.Photo = fileName;
                }

                if (productPhoto.PhotoID == 0)
                {
                    if (uploadPhoto == null)
                        ModelState.AddModelError("Photo", "Photo is required");

                    if (!ModelState.IsValid)
                        return View("Photo", productPhoto);

                    ProductDataService.AddPhoto(productPhoto);
                }
                else
                {
                    if (!ModelState.IsValid)
                        return RedirectToAction("Photo", new { method = "edit", productID = productPhoto.ProductID, photoID = productPhoto.PhotoID });

                    ProductDataService.UpdatePhoto(productPhoto);
                }

                return RedirectToAction("Edit", new { id = productPhoto.ProductID });
            }
            catch (Exception e)
            {
                ViewBag.Title = "Product | Error";
                return PartialView("Error", e.Message);
            }
        }


        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method = "add", int productID = 0, int attributeID = 0)
        {
           switch (method){
                case "add":
                    ViewBag.Title = "Add Attribute";

                    var productAttribute = new ProductAttribute
                    {
                        AttributeID = 0,
                        ProductID = productID,
                        DisplayOrder = 1
                    };
                    return View(productAttribute);
                case "edit":
                    ViewBag.Title = "Edit Attribute";

                    var attribute = ProductDataService.GetAttribute(attributeID);
                    return View(attribute);
                case "delete":
                    ProductDataService.DeleteAttribute(attributeID);
                    return RedirectToAction("Edit", new { id = productID });
                default:
                    return RedirectToAction("Index");
            }

        }

        /// <summary>
        ///     Save a attribute of a product
        /// </summary>
        /// <param name="productAttribute"> Product Attribute </param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAttribute(ProductAttribute productAttribute)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(productAttribute.AttributeName))
                    ModelState.AddModelError("AttributeName", "AttributeName is required");
                if (string.IsNullOrWhiteSpace(productAttribute.AttributeValue))
                    ModelState.AddModelError("AttributeValue", "AttributeValue is required");
                if (productAttribute.DisplayOrder <= 0)
                    ModelState.AddModelError("DisplayOrder", "DisplayOrder is required");

                if (productAttribute.AttributeID == 0)
                {
                    if (!ModelState.IsValid)
                        return View("Attribute", productAttribute);

                    ProductDataService.AddAttribute(productAttribute);
                }
                else
                {
                    if (!ModelState.IsValid)
                        return RedirectToAction("Attribute", new { method = "edit", productID = productAttribute.ProductID, attributeID = productAttribute.AttributeID });

                    ProductDataService.UpdateAttribute(productAttribute);
                }

                return RedirectToAction("Edit", new { id = productAttribute.ProductID });
            }
            catch (Exception e)
            {
                ViewBag.Title = "Product | Error";
                return PartialView("Error", e.Message);
            }
        }

    }
}