using HomeoCare.Data;
using HomeoCare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HomeoCare.Utility
{
    public class CartFilter : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _db;
        public CartFilter(ApplicationDbContext db)
        {
            _db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isAjaxCall = filterContext.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            filterContext.HttpContext.Session.SetString(WC.CartItemCount, "0");
            bool CartUpdated = filterContext.HttpContext.Session.GetBoolean("CartUpdated") ?? false;
            if (CartUpdated == false && filterContext.HttpContext.User.Identity.IsAuthenticated == true)
            {
                var cookies = filterContext.HttpContext.Request.Cookies;

                Guid UserID = new Guid(filterContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                List<ShoppingCart> dbcart = _db.ShoppingCart.Where
                       (u => u.UserID == UserID).ToList();

                if (cookies[WC.CartCookie] != null)
                {
                    List<ShoppingCart> Cookiescart = filterContext.HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
                    foreach (ShoppingCart shoppingCart in Cookiescart)
                    {
                        if (shoppingCart.RowID == 0)
                        {
                            if (dbcart.FirstOrDefault(u => u.ProductId == shoppingCart.ProductId) == null)
                            {
                                shoppingCart.UserID = UserID;
                                dbcart.Add(shoppingCart);
                                _db.ShoppingCart.Add(shoppingCart);
                                _db.SaveChanges();
                            }
                            else
                            {
                                dbcart.FirstOrDefault(u => u.ProductId == shoppingCart.ProductId)
                                    .Quantity = shoppingCart.Quantity;
                                _db.SaveChanges();
                            }
                        }
                    }
                }
                filterContext.HttpContext.Response.Cookies.Delete(WC.CartCookie);
                filterContext.HttpContext.Response.Cookies.Set(WC.CartCookie, dbcart, null);
                filterContext.HttpContext.Session.SetString(WC.CartItemCount, dbcart.Count().ToString());
                filterContext.HttpContext.Session.SetBoolean("CartUpdated", true);
            }
            else
            {
                var cookies = filterContext.HttpContext.Request.Cookies;
                if (cookies[WC.CartCookie] != null)
                {
                    filterContext.HttpContext.Session.SetString(WC.CartItemCount,
                        cookies.Get<List<ShoppingCart>>(WC.CartCookie).Count().ToString());
                }
            }



            //if (cookies[WC.CartCookie] == null)
            //{
            //    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        var userId = filterContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //        IEnumerable<ShoppingCart> cartlist = _db.ShoppingCart.Where(u => u.UserID == new Guid(userId));
            //        filterContext.HttpContext.Response.Cookies.Set(WC.CartCookie, cartlist, null);
            //    }
            //}


            //if (cookies["CartList"] == null)
            //{
            //    var userId = filterContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //    IEnumerable<ShoppingCart> cartlist = _db.ShoppingCart.Where(u => u.UserID == new Guid(userId));
            //    filterContext.HttpContext.Response.Cookies.Set(WC.CartCookie, cartlist, null);
            //}
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //...work with the filterContext object after executing the method
        }
    }
}
