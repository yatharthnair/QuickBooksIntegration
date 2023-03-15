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
            var data = new List<vendor>();
            using (var context = new NewDBContext())
            {
                data = context.Vendors.ToList();
            }

            return View(data);
        }


        [HttpPost]
        public ActionResult AddVendor(vendor model)
        {
            using (var context = new NewDBContext())
            {
                context.Vendors.Add(model);
                context.SaveChanges();
            }
            return View();
        }
        /* public IActionResult AddAccount()
         {
             return View();
         }
         [HttpPost]
         public ActionResult AddAccount(accountlist model)
         {
             using (var context = new FinalDBContext())
             {
                 context.AccountLists.Add(model);
                 context.SaveChanges();
             }
             return View();
         }

         public IActionResult AddPurchaseOrder()
         {
             return View();
         }
         [HttpPost]
          public ActionResult AddPurchaseOrder(PurchaseOrder model)
         {
             using (var context = new FinalDBContext())
             {
                 context.PurchaseOrders.Add(model);
                 context.SaveChanges();
             }
             return View();
         }*/


    }
}