using MVCProject_Nazmul.Models;
using MVCProject_Nazmul.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCProject_Nazmul.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PageSize = new SelectList(new List<int> { 5, 10, 20, 50 }, pageSize ?? 10); 

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var orders = from t in db.Orders
                         select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(tr => tr.Customer.CustomerName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderByDescending(tr => tr.Customer.CustomerName);
                    break;
                default:
                    orders = orders.OrderBy(tr => tr.Customer.CustomerName);
                    break;
            }

            int pageNumber = (page ?? 1);
            int pageSizeValue = pageSize ?? 10; 

            return View(orders.ToPagedList(pageNumber, pageSizeValue));

        }


        public ActionResult Order()
        {
            return View();
        }

        public JsonResult getProductCategories()
        {
           var categories = db.Categories
                    .OrderBy(c => c.CategoryName)
                    .Select(c => new
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    })
                    .ToList();
            return new JsonResult { Data = categories, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult getProducts(int categoryId)
        {
            var products = db.Products
                            .Where(p => p.CategoryId == categoryId)
                            .Select(p => new
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                UnitPrice = p.UnitPrice
                            }) 
                            .OrderBy(p => p.ProductName)
                            .ToList();
            return new JsonResult { Data = products, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetUnitPrice(int productId)
        {
            var price = db.Products.Where(p => p.ProductId == productId).Select(p => p.UnitPrice).FirstOrDefault();
            return Json(price, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Customer customer, Order order, HttpPostedFileBase file)
        {
            bool status = false;
            if (file != null)
            {
                string folderPath = Server.MapPath("~/Images/");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(folderPath, fileName);
                file.SaveAs(filePath);
                customer.Image = fileName;
            }
            var isValidModel = TryUpdateModel(customer);
            if (isValidModel)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                order.CustomerId = customer.CustomerId;
                db.Orders.Add(order);

                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        public JsonResult OrderDetails(int id)
        {
            var orderDetails = db.OrderDetails.Where(o => o.OrderId == id).ToList();

            var orderInfo = orderDetails
                    .Select(o => new {
                        id = o.OrderId,
                        productId = o.ProductId,
                        productName = db.Products.FirstOrDefault(p => p.ProductId == o.ProductId)?.ProductName,
                        quantity = o.Quantity.ToString(),
                        unitPrice = o.UnitPrice.ToString(),
                        totalPrice = o.Quantity * o.UnitPrice
                    });
            return Json(orderInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}