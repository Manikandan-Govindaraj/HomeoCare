using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc;
using HomeoCare.Data;
using HomeoCare.Models;
using HomeoCare.Models.ViewModels;
using HomeoCare.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;

namespace HomeoCare.Controllers
{

    [Authorize]
    [TypeFilter(typeof(CartFilter))]
    public class CartController : Controller
    {
        //LIVE
        //string key = "rzp_live_mQ1Bh4gZyUMU0V";
        //string secret = "cTifvwW02t1gTQtG6lcBcQuu";

        //TEST
        string key = "rzp_test_iUs9BRtwn0CzpZ";
        string secret = "UdI25XRNoPol4bU10jNQczFQ";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
        public CartController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender,
            UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _userManager = userManager;
            _configuration = configuration;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            IEnumerable<Product> prodList = new List<Product>();
            //HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (User.Identity.IsAuthenticated)
            {
                shoppingCartList = _db.ShoppingCart.Where(u => u.UserID == new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();
            }
            else
            {
                shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            }


            if (shoppingCartList.Count > 0)
            {
                List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
                prodList = _db.Product.Where(u => prodInCart.Contains(u.ProductID));

                foreach (Product p in prodList)
                {
                    p.TempQty = shoppingCartList.Find(x => x.ProductId == p.ProductID).Quantity;
                }

                decimal SubTotal = 0;
                foreach (Product p in prodList)
                {
                    p.TempQty = shoppingCartList.Find(x => x.ProductId == p.ProductID).Quantity;
                    SubTotal += p.SellingPrice * p.TempQty;
                }
                ViewBag.SubTotal = SubTotal;
            }

            return View(prodList);
        }


        public IActionResult Checkout()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userId = User.FindFirstValue(ClaimTypes.Name);

            List<ShoppingCart> shoppingCartList = _db.ShoppingCart.Where(u => u.UserID == new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.ProductID));


            decimal SubTotal = 0, DeliveryCharges = 100, TotalPayable = 0;
            foreach (Product p in prodList)
            {
                p.TempQty = shoppingCartList.Find(x => x.ProductId == p.ProductID).Quantity;
                SubTotal += p.SellingPrice * p.TempQty;
            }
            TotalPayable = (DeliveryCharges + SubTotal) + (SubTotal * WC.TaxRate / 100);


            ViewBag.TotalPayable = TotalPayable;
            ViewBag.DeliveryCharges = DeliveryCharges;
            ViewBag.SubTotal = SubTotal;
            ViewBag.TaxRate = WC.TaxRate;
            ViewBag.TaxAmount = SubTotal * WC.TaxRate / 100;


            ProductUserVM = new ProductUserVM()
            {
                addressDetails = _db.AddressDetails.FirstOrDefault(u => u.UserID == new Guid(claim.Value)),
                ProductList = prodList.ToList()
            };


            if (ProductUserVM.addressDetails != null)
            {
                if (string.IsNullOrEmpty(ProductUserVM.addressDetails.ContactName)
                    || string.IsNullOrEmpty(ProductUserVM.addressDetails.Mobile))
                {
                    PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == new Guid(claim.Value));
                    ProductUserVM.addressDetails.ContactName = personalDetails.FirstName;
                    ProductUserVM.addressDetails.Mobile = personalDetails.PhoneNumber;
                }
            }
            else
            {
                ProductUserVM.addressDetails = new AddressDetails();
                ProductUserVM.addressDetails.UserID = new Guid(claim.Value);
                PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == new Guid(claim.Value));
                ProductUserVM.addressDetails.ContactName = personalDetails.FirstName;
                ProductUserVM.addressDetails.Mobile = personalDetails.PhoneNumber;
            }

            return View(ProductUserVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Checkout")]
        public async Task<IActionResult> CheckoutPost(ProductUserVM ProductUserVM)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                //UPDATE ADDRESS DETAILS
                if (ProductUserVM.addressDetails.AddressID == new Guid())
                {
                    ProductUserVM.addressDetails.AddressID = Guid.NewGuid();
                    _db.AddressDetails.Add(ProductUserVM.addressDetails);
                }
                else
                {
                    _db.AddressDetails.Update(ProductUserVM.addressDetails);
                }


                //GET SUBTOTAL FROM THE CART
                List<ShoppingCart> shoppingCartList = _db.ShoppingCart.Where(u => u.UserID == new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();
                List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
                IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.ProductID));
                decimal TotalPayable = 0;
                foreach (Product p in prodList)
                {
                    p.TempQty = shoppingCartList.Find(x => x.ProductId == p.ProductID).Quantity;
                    TotalPayable += p.SellingPrice * p.TempQty;
                }

                //ADD TAX AND SHIPPING CHARGES TO SUBTOTAL
                TotalPayable += (TotalPayable * WC.TaxRate / 100) + WC.ShippingCharges;


                //CREATE OR UPDATE RAZOR CUSTOMER
                RazorpayClient client = new RazorpayClient(key, secret);
                PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == user.Id);
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("name", personalDetails.FirstName);
                data.Add("email", personalDetails.Email);
                data.Add("contact", personalDetails.PhoneNumber);
                if (string.IsNullOrEmpty(personalDetails.RazorPayID))
                {
                    Customer customer = client.Customer.Create(data);
                    personalDetails.RazorPayID = Convert.ToString(customer.Attributes["id"]);
                }
                else
                {
                    Customer customer = client.Customer.Fetch(personalDetails.RazorPayID);

                    //data.Add("id", personalDetails.RazorPayID);
                    //data.Add("entity", "customer");
                    customer.Edit(data);
                }

                _db.SaveChanges();

                //CREATE ORDER UFID
                string OrderUFID = Util.GetOrderUFID();

                //CREATE RAZORPAY ORDER
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", TotalPayable * 100); // amount in the smallest currency unit
                options.Add("receipt", OrderUFID);
                options.Add("currency", "INR");
                Razorpay.Api.Order order = client.Order.Create(options);

                //SET CHECKOUT DETAILS
                TempData["CustomerName"] = personalDetails.FirstName;
                TempData["OrderUFID"] = OrderUFID;
                TempData["OrderAmount"] = Convert.ToString(TotalPayable * 100);
                TempData["RazerOrderID"] = order["id"].ToString();
                TempData.Keep();
                return Json(new
                {
                    key = key,
                    amount = Convert.ToString(TotalPayable * 100),
                    order_id = order["id"].ToString(),
                    customer_id = personalDetails.RazorPayID
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public IActionResult InquiryConfirmation()
        {
            HttpContext.Response.Cookies.Delete(WC.CartCookie);
            Guid UserID = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _db.ShoppingCart.RemoveRange(_db.ShoppingCart.Where(u => u.UserID == UserID));
            _db.SaveChanges();

            ViewBag.OrderID = Convert.ToString(TempData["OrderUFID"]);
            ViewBag.CustomerName = Convert.ToString(TempData["CustomerName"]);
            SendConfirmationMail();

            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete(WC.CartCookie);
            TempData.Clear();
            return View();
        }

        [AllowAnonymous]
        public JsonResult Remove(int ProductID)
        {
            List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            ShoppingCart shoppingCart = shoppingCartList.FirstOrDefault(u => u.ProductId == ProductID);
            shoppingCartList.Remove(shoppingCart);
            if (User.Identity.IsAuthenticated && shoppingCart.RowID != 0)
            {
                _db.ShoppingCart.Remove(shoppingCart);
                _db.SaveChanges();
            }

            Response.Cookies.Set(WC.CartCookie, shoppingCartList, null);
            return Json(new { cartitemcount = shoppingCartList.Count() });
        }
        [AllowAnonymous]
        public JsonResult UpdateCart(int ProductID, int Quantity)
        {
            List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            shoppingCartList.FirstOrDefault(u => u.ProductId == ProductID).Quantity = Quantity;
            ShoppingCart shoppingCart = shoppingCartList.FirstOrDefault(u => u.ProductId == ProductID);
            _db.ShoppingCart.Update(shoppingCart);
            _db.SaveChanges();
            Response.Cookies.Set(WC.CartCookie, shoppingCartList, null);
            return Json(new { status = true });
        }



        public async void SendConfirmationMail()
        {
            Guid UserID = new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            ProductUserVM ProductUserVM = new ProductUserVM();

            ProductUserVM.addressDetails = _db.AddressDetails.FirstOrDefault(u => u.UserID == UserID);

            //List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            //int[] ids = shoppingCartList.Select(x => x.ProductId).ToArray();
            //ProductUserVM.ProductList = _db.Product.Where(u => ids.Contains(u.ProductID)).ToList();


            List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
            decimal SubTotal = 0;
            foreach (var prod in shoppingCartList)
            {
                Product product = _db.Product.FirstOrDefault(x => x.ProductID == prod.ProductId);
                product.TempQty = prod.Quantity;
                SubTotal += product.SellingPrice * product.TempQty;
                ProductUserVM.ProductList.Add(product);
            }
            ViewBag.SubTotal = SubTotal;

            var absUrl = string.Format("{0}://{1}", Request.Scheme, Request.Host);
            ViewBag.absUrl = "https://greencart.reckonspace.com";

            var html = await this.RenderViewToStringAsync("CreateEmailInvoice", ProductUserVM);

            PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == UserID);

            await _emailSender.SendEmailAsync(personalDetails.Email,
                "Your HomeoRightCare.com order #" + ViewBag.OrderID + " of " + shoppingCartList.Count() + " items.", html);
        }


        public JsonResult CapturePaymet(Razorpayresponse response)
        {
            RazorpayClient client = new RazorpayClient(key, secret);
            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", response.razorpay_payment_id);
            attributes.Add("razorpay_order_id", Convert.ToString(TempData["RazerOrderID"]));
            attributes.Add("razorpay_signature", response.razorpay_signature);

            try
            {
                Utils.verifyPaymentSignature(attributes);
                Payment payment = client.Payment.Fetch(response.razorpay_payment_id);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", Convert.ToDecimal(TempData["OrderAmount"]));
                options.Add("currency", "INR");
                if (CreateOrder())
                {
                    Payment paymentCaptured = payment.Capture(options);
                    return Json(new { status = true });
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = "Error while processing payment." });
                throw;
            }
        }

        public bool CreateOrder()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Models.Order _orders = new Models.Order()
                {
                    OrderID = Guid.NewGuid(),
                    UFID = Convert.ToString(TempData["OrderUFID"]),
                    UserID = new Guid(userId),
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending,
                    DeliveredDate = null,
                    ShippingFee = WC.ShippingCharges
                };
                _db.Order.Add(_orders);


                List<ShoppingCart> shoppingCartList = HttpContext.Request.Cookies.GetShoppingCart<List<ShoppingCart>>();
                foreach (var prod in shoppingCartList)
                {
                    Product product = _db.Product.FirstOrDefault(x => x.ProductID == prod.ProductId);

                    OrderList _list = new OrderList();
                    _list.OrderID = _orders.OrderID;
                    _list.Price = product.SellingPrice;
                    _orders.OrderTotal += (_list.Price * prod.Quantity);
                    _list.ProductID = prod.ProductId;
                    _list.Quantity = prod.Quantity;
                    _list.PaymentStatus = PaymentStatus.Paid;
                    _db.OrderList.Add(_list);
                    product.Quantity = product.Quantity - prod.Quantity;
                }
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class Razorpayresponse
    {
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
    }
}
