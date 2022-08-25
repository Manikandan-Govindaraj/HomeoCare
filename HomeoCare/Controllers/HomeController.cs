using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using HomeoCare.Data;
using HomeoCare.Models;
using HomeoCare.Models.ViewModels;
using HomeoCare.Utility;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HomeoCare.Controllers
{
    //[CartFilter(typeof(ApplicationDbContext),typeof(HttpContext))]
    [TypeFilter(typeof(CartFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        private const int pageSize = 6;
        public IActionResult Products(int? PageNo, int CatID = 0)
        {
            if (TempData["isEmailConfirmed"] != null)
            {
                ViewBag.isEmailConfirmed = TempData["isEmailConfirmed"];
                TempData["isEmailConfirmed"] = null;
            }

            int pageNumber = PageNo == 0 ? 1 : PageNo ?? 1;

            var TotalRecords = 0;
            HomeVM homeVM = null;
            if (CatID != 0)
            {
                homeVM = new HomeVM()
                {
                    Products = _db.Product.Where(u => u.CategoryId == CatID).Include(u => u.Category).OrderBy(p => p.Category.DisplayOrder)
                  .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                    Categories = _db.Category
                };
                TotalRecords = homeVM.Products.Count();
            }
            else
            {
                homeVM = new HomeVM()
                {
                    Products = _db.Product.Include(u => u.Category).OrderBy(p => p.Category.DisplayOrder)
                  .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                    Categories = _db.Category
                };
                TotalRecords = _db.Product.Count();
            }

            
            TempData["CatID"] = CatID;
            TempData["pageNumber"] = pageNumber;
            TempData["TotalPages"] = (int)Math.Ceiling((double)TotalRecords / pageSize);
            TempData.Keep();

            return View(homeVM);


        }


        public IActionResult Details(int id)
        {

            DetailsVM DetailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(u => u.Category)
                .Where(u => u.ProductID == id).FirstOrDefault(),
                ExistsInCart = CheckProductExistInCart(id)
            };

            return View(DetailsVM);
        }
        public IActionResult DetailsNew(int id)
        {

            DetailsVM DetailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(u => u.Category)
                .Where(u => u.ProductID == id).FirstOrDefault(),
                ExistsInCart = CheckProductExistInCart(id)
            };

            return View(DetailsVM);
        }

        [HttpPost]
        public JsonResult Addtocart(int ProductID)
        {
            return Json(new { cartitemcount = AddtoCard(ProductID) });
        }
        public int AddtoCard(int id)
        {
            List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            ShoppingCart shoppingCart = shoppingCartList.Find(x => x.ProductId == id);
            if (shoppingCart != null)
            {
                shoppingCart.Quantity += 1;
            }
            else
            {
                shoppingCart = new ShoppingCart() { ProductId = id, Quantity = 1 };
                shoppingCartList.Add(shoppingCart);
            }


            if (User.Identity.IsAuthenticated)
            {
                Guid UserID = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                shoppingCart.UserID = UserID;

                if (shoppingCart.RowID != 0)
                    _db.ShoppingCart.Update(shoppingCart);
                else
                    _db.ShoppingCart.Add(shoppingCart);

                _db.SaveChanges();
            }

            Response.Cookies.Set(WC.CartCookie, shoppingCartList, null);
            return shoppingCartList.Count;
        }

        public bool CheckProductExistInCart(int id)
        {
            List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            if (shoppingCartList.Count > 0)
            {
                if (shoppingCartList.Find(x => x.ProductId == id) != null)
                    return true;
                else
                    return false;
            }

            return false;
        }


        [Route("Home/Error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //exceptionDetails.Path;
            //exceptionDetails.Error.Message;
            //exceptionDetails.Error.StackTrace;

            return View(new ErrorViewModel { IsException = true, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Home/Error/{StatusCode}")]
        public IActionResult Error(int StatusCode, string ErrorMsg)
        {
            switch (StatusCode)
            {
                case 404:
                    ViewBag.ErrorMsg = "Sorry, the resource you requested could not be found";
                    break;
                case 401:
                    if (!string.IsNullOrEmpty(ErrorMsg))
                        ViewBag.ErrorMsg = ErrorMsg;
                    else
                        ViewBag.ErrorMsg = "Sorry, the resource you requested is unauthorized.";
                    break;
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
