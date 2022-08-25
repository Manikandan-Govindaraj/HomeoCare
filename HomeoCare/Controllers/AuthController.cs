using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeoCare.Models;
using Microsoft.AspNetCore.Identity;
using HomeoCare.Data;
using System.ComponentModel.DataAnnotations;
using HomeoCare.Models.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using HomeoCare.Utility;
using Microsoft.AspNetCore.Authentication;

namespace HomeoCare.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(
           UserManager<ApplicationUser> userManager,
           ILogger<RegisterModel> logger,
           IEmailSender emailSender,
           ApplicationDbContext db,
           SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            _signInManager = signInManager;
        }

        public IActionResult Index() {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            RegisterModel registerModel = new RegisterModel();
            return View(registerModel);
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //save identity
                var user = new ApplicationUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber,
                    DisplayName = registerModel.FirstName
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    //save personal details
                    PersonalDetails personal = new PersonalDetails()
                    {
                        UserID = user.Id,
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        PhoneNumber = registerModel.PhoneNumber,
                        Email = registerModel.Email
                    };
                    _db.PersonalDetails.Add(personal);

                    //add user to role
                    if (User.IsInRole(WC.AdminRole))
                        await _userManager.AddToRoleAsync(user, WC.AdminRole);
                    else
                        await _userManager.AddToRoleAsync(user, WC.CustomerRole);


                    //generate confirmation code
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    ViewBag.ConfirmationLink =
                        Url.Action(
                        "ConfirmEMail", "Auth",
                        new { userId = user.Id, code = code, returnUrl = Url.Action("Products", "Home") },
                        Request.Scheme);

                    //create html for email
                    var html = await this.RenderViewToStringAsync("~/Views/Shared/EmailTemplates/EmailConfirmation.cshtml");

                    //send email
                    await _emailSender.SendEmailAsync(registerModel.Email, "Confirm your email", html);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToAction("RegisterSuccess", "Auth");
                        //return RedirectToPage("RegisterConfirmation", new { userId = user.Id, returnUrl = Url.Action("Products", "Home") });
                    }

                    return RedirectToAction("Products", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEMail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Error", "Home", new { StatusCode = 404 });
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["isEmailConfirmed"] = true;
                TempData.Keep();
                return RedirectToAction("Products", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Home",
                    new { StatusCode = 404, ErrorMsg = "Sorry, cannot verify the email, pleace contact support." });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }

        [ActionName("Login")]
        [HttpPost]
        public async Task<IActionResult> LoginPost(LoginModel loginModel, string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/Home/Products");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete(WC.CartCookie);
            HttpContext.Session.Clear();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction();
            }
        }
    }
}
