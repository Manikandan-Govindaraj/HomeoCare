using HomeoCare.Data;
using HomeoCare.Models;
using HomeoCare.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace HomeoCare.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Username = user.UserName;
            PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == user.Id);
            return View(personalDetails);
        }

        [HttpPost]
        [ActionName("Profile")]
        public async Task<IActionResult> ProfilePost(PersonalDetails personalDetails)
        {
            _db.PersonalDetails.Update(personalDetails);
            await _db.SaveChangesAsync();
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Username = user.UserName;
            return View(personalDetails);
        }

        public async Task<IActionResult> ManageAddress()
        {
            var user = await _userManager.GetUserAsync(User);
            AddressDetails addressDetails = _db.AddressDetails.FirstOrDefault(u => u.UserID == user.Id);

            if (addressDetails != null)
            {
                if (string.IsNullOrEmpty(addressDetails.ContactName) || string.IsNullOrEmpty(addressDetails.Mobile))
                {
                    PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == user.Id);
                    addressDetails.ContactName = personalDetails.FirstName;
                    addressDetails.Mobile = personalDetails.PhoneNumber;
                }
            }
            else
            {
                addressDetails = new AddressDetails();
                addressDetails.UserID = user.Id;
                PersonalDetails personalDetails = _db.PersonalDetails.FirstOrDefault(u => u.UserID == user.Id);
                addressDetails.ContactName = personalDetails.FirstName;
                addressDetails.Mobile = personalDetails.PhoneNumber;
            }



            return View(addressDetails);
        }

        [HttpPost]
        [ActionName("ManageAddress")]
        public async Task<IActionResult> ManageAddressPost(AddressDetails addressDetails)
        {
            if (ModelState.IsValid)
            {
                if (addressDetails.AddressID == new Guid())
                {
                    addressDetails.AddressID = Guid.NewGuid();
                    _db.AddressDetails.Add(addressDetails);
                }
                else
                {
                    _db.AddressDetails.Update(addressDetails);

                }
                await _db.SaveChangesAsync();
            }
            return View(addressDetails);
        }

        public IActionResult Orders()
        {
            var userID = _userManager.GetUserId(User);
            IEnumerable<Order> orders = _db.Order.Where(u => u.UserID == new Guid(userID)).OrderByDescending(u => u.OrderDate);
            foreach (Order order in orders)
            {
                order.orderList = _db.OrderList.Where(u => u.OrderID == order.OrderID)
                   .Include(u => u.Product).ToList();
            }
            return View(orders);
        }



        public JsonResult Getcutomerorders(List<SortDescription> sort, FilterContainer filter, int skip, int take, int pageSize, int page)
        {
            var userID = _userManager.GetUserId(User);
            IEnumerable<Order> orders = _db.Order.Where(u => u.UserID == new Guid(userID)).Include(u => u.UserDetails).OrderByDescending(u => u.OrderDate);
            return Json(new { total = 1, data = orders });
        }
        public JsonResult Getorderlist(List<SortDescription> sort, FilterContainer filter, int skip, int take, int pageSize, int page)
        {
            IEnumerable<OrderList> orders = _db.OrderList.Where(u => u.OrderID == new Guid(filter.filters[0].value)).Include(u => u.Product);
            return Json(new { total = 1, data = orders });
        }

        [HttpGet]
        public ActionResult OrderDetails(string id)
        {
            Order order = _db.Order.FirstOrDefault(u => u.OrderID == new Guid(id));
            order.orderList = _db.OrderList.Where(u => u.OrderID == new Guid(id))
                .Include(u => u.Product).ToList();

            return View(order);
        }

    }
}
