using HomeoCare.Data.Interface;
using HomeoCare.Models;
using HomeoCare.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeoCare.Data.EFRepository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpContext _httpContext;

        public ShoppingCartRepository(ApplicationDbContext db,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public bool Add(ShoppingCart shoppingCart)
        {
            _db.ShoppingCart.Add(shoppingCart);
            _db.SaveChanges();
            return true;
        }
        public bool Update(ShoppingCart shoppingCart)
        {
            _db.ShoppingCart.Update(shoppingCart);
            _db.SaveChanges();
            return true;
        }

        public int AddtoCart(int ProductID)
        {
            List<ShoppingCart> shoppingCartList = _httpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            ShoppingCart sp = shoppingCartList.Find(x => x.ProductId == ProductID);
            if (sp != null)
                sp.Quantity += 1;
            else
                shoppingCartList.Add(new ShoppingCart { ProductId = ProductID, Quantity = 1 });

            //_httpContext.Response.Cookies.SetShoppingCart<List<ShoppingCart>>(WC.CartCookie,
            //    shoppingCartList, _db, _httpContext.User);

            return shoppingCartList.Count;
        }

        public int RemovefromCart(int ProductID)
        {
            List<ShoppingCart> shoppingCartList = _httpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            ShoppingCart shoppingCart = shoppingCartList.FirstOrDefault(u => u.ProductId == ProductID);
            shoppingCartList.Remove(shoppingCart);
            if (_httpContext.User.Identity.IsAuthenticated && shoppingCart.RowID != 0)
            {
                _db.ShoppingCart.Remove(shoppingCart);
                _db.SaveChanges();
            }

            _httpContext.Response.Cookies.Set(WC.CartCookie, shoppingCartList, null);
            return shoppingCartList.Count();
        }

        public bool UpdateCart(int ProductID, int Quantity)
        {
            List<ShoppingCart> shoppingCartList = _httpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            shoppingCartList.FirstOrDefault(u => u.ProductId == ProductID).Quantity = Quantity;
            //_httpContext.Response.Cookies.SetShoppingCart<List<ShoppingCart>>(WC.CartCookie, 
            //    shoppingCartList, _db, _httpContext.User);
            return true;
        }
    }
}
