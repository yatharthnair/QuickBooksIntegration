using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using QuickbookIntegration1.Models;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.OAuth2PlatformClient;
using Intuit.Ipp.Security;
using NuGet.Common;
using System;
using Intuit.Ipp.Core;
using static System.Net.WebRequestMethods;
using Microsoft.CodeAnalysis.Diagnostics;

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


        [HttpGet]
        public IActionResult ViewVendor()
        {
            var data = new List<VendorList>();
            using (var context = new QuickBooksContext())
            {
                data = context.VendorLists.ToList();
            }

            return View(data);
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
        //public static string clientId = "ABmvAWjedrPeDs7xZZm7OmVtskZ7yxg4S3wvlKq2sbOzClLvf9";
        //public static string clientSecret = "vVxCwSHNZXitBtNjYfDbcGZaDJxY33M6vJpdrZBm";
        //public static string redirectUrl = "https://localhost:7092/callback";
        //public static string environment = "sandbox";

        //public static OAuth2Client oauthClient = new OAuth2Client(clientId, clientSecret, redirectUrl, environment);
        //public IActionResult Initiateoauth2()
        //{
        //    List<OidcScopes> scopes = new List<OidcScopes>();
        //    scopes.Add(OidcScopes.Accounting);
        //    string authUrl = oauthClient.GetAuthorizationURL(scopes);
        //    return Redirect(authUrl);

        //}

       

    }
}