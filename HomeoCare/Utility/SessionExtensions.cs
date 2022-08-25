using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Claims;

using HomeoCare.Models;
using HomeoCare.Data;

namespace HomeoCare.Utility
{


    public static class CookieExtensions
    {
        public static void Set<T>(this IResponseCookies Cookies, string key, T value, DateTime? Expires)
        {
            Cookies.Append(key,
                JsonSerializer.Serialize(value),
                       new CookieOptions() { Expires = Expires != null ? Expires : DateTime.Now.AddDays(30) });
        }

        public static T Get<T>(this IRequestCookieCollection Cookies, string key)
        {
            var value = Cookies[key];
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }



        public static bool? GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToBoolean(data, 0);
        }

        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

    }
    public static class CartExtensions
    {
        public static List<ShoppingCart> GetShoppingCart<T>(this IRequestCookieCollection Cookies)
        {
            if (Cookies[WC.CartCookie] != null)
            {
                List<ShoppingCart> shoppingCart = JsonSerializer.Deserialize<List<ShoppingCart>>(Cookies[WC.CartCookie]);
                return shoppingCart;
            }
            return new List<ShoppingCart>();
        }


        public static int GetCartItemCount(this IRequestCookieCollection cookies,
            ApplicationDbContext _db, HttpContext httpContext)
        {
            if (cookies[WC.CartCookie] == null)
            {
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    IEnumerable<ShoppingCart> cartlist = _db.ShoppingCart.Where(u => u.UserID == new Guid(userId));
                    httpContext.Response.Cookies.Set(WC.CartCookie, cartlist, null);
                    return cartlist.Count();
                }
                return 0;
            }
            else
            {
                List<ShoppingCart> cartlist = httpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
                return cartlist.Count();
            }
        }

    }
}

public static class FormatExtensions
{
    public static string FormatCurrency(this decimal i)
    {
        return string.Format("{0:C}", i).Replace(" ","");
    }
}