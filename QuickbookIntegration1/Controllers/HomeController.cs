
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QuickbookIntegration1.Models;

namespace IntegrationWithQuickbooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(SignInCallback)),
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet]
        public async Task<IActionResult> SignInCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return RedirectToAction("Privacy", "Home");
            }
            var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;

            return RedirectToAction("AddVendor", "Home");
        }
        public IActionResult dashboard()
        {
            return View();
        }


        public IActionResult AddVendor()
        {
            return View();
        }
        public IActionResult AddVendors()
        {
            return View();
        }

        public IActionResult ViewVendor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVendor(VendorList model)
        {
            using (var context = new QuickBooksContext())
            {
                context.VendorLists.Add(model);
                context.SaveChanges();
            }
            return View();
        }
    }
}
        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    