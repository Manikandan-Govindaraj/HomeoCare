using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeoCare.Data;
using HomeoCare.Models;
using HomeoCare.Utility;

namespace HomeoCare.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Getcutomerorders(List<SortDescription> sort, FilterContainer filter, int skip, int take, int pageSize, int page)
        {
            IEnumerable<Order> orders = _db.Order.Include(u => u.UserDetails).OrderByDescending(u => u.OrderDate);

            foreach (Order ord in orders)
            {
                ord.drpdown = new dropdownhelper()
                {
                    Key = Convert.ToInt32(ord.Status).ToString(),
                    Value = ord.Status.ToString()
                };
            }

            return Json(new { total = 1, data = orders });
        }
        public JsonResult Getorderlist(List<SortDescription> sort, FilterContainer filter, int skip, int take, int pageSize, int page)
        {
            IEnumerable<OrderList> orders = _db.OrderList.Where(u => u.OrderID == new Guid(filter.filters[0].value)).Include(u => u.Product);
            return Json(new { total = 1, data = orders });
        }

        public JsonResult GetOrderStatusList()
        {
            List<dropdownhelper> drpdown = new List<dropdownhelper>();
            foreach (var name in Enum.GetNames(typeof(OrderStatus)))
            {
                drpdown.Add(new dropdownhelper()
                {
                    Key = ((int)Enum.Parse(typeof(OrderStatus), name)).ToString(),
                    Value = name
                });
            }
            return Json(new { total = 1, data = drpdown });
        }

        [HttpPut]
        public JsonResult UpdateGrid(Order orderrr)
        {
            Order s1 = _db.Order.FirstOrDefault(u => u.OrderID == orderrr.OrderID);
            //s1.OrderID = orderrr.OrderID;
            //_db.Order.Attach(s1); // State = Unchanged
            s1.Status = (OrderStatus)Convert.ToInt32(orderrr.drpdown.Key);

            if ((OrderStatus)Convert.ToInt32(orderrr.drpdown.Key) == OrderStatus.Deliverd)
            {
                s1.DeliveredDate = DateTime.Now;
            }
            else if ((OrderStatus)Convert.ToInt32(orderrr.drpdown.Key) == OrderStatus.Shipped)
            {
                IEnumerable<OrderList> orders = _db.OrderList.Where(u => u.OrderID == orderrr.OrderID).Include(u => u.Product);
                foreach (OrderList order in orders)
                {
                    order.Product.QuantityOnHand = order.Product.QuantityOnHand - order.Quantity;
                }
            }
            else if ((OrderStatus)Convert.ToInt32(orderrr.drpdown.Key) == OrderStatus.Returned)
            {
                IEnumerable<OrderList> orders = _db.OrderList.Where(u => u.OrderID == orderrr.OrderID).Include(u => u.Product);
                foreach (OrderList order in orders)
                {
                    s1.DeliveredDate = null;
                    order.Product.QuantityOnHand = order.Product.QuantityOnHand + order.Quantity;
                    order.Product.Quantity = order.Product.Quantity + order.Quantity;
                }
            }
            _db.SaveChanges();
            return Getcutomerorders(null, null, 0, 0, 0, 0);

            //return Json(new { total = "test" });
        }

        [HttpPost]
        public ActionResult delete(string ufid)
        {
            return Json(new { test = "" });
        }



        [HttpGet]
        public ActionResult OrderDetails()
        {
            Order order = _db.Order.FirstOrDefault(u => u.OrderID == new Guid("F9086427-F286-40B1-A194-2607D9414442"));
            order.orderList = _db.OrderList.Where(u => u.OrderID == new Guid("F9086427-F286-40B1-A194-2607D9414442"))
                .Include(u => u.Product).ToList();

            return View(order);
        }

    }

}
