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
        private readonly IHttpContextAccessor _context;


        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context)
        {
            _logger = logger;
            _context = context;
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
                return RedirectToAction("Index");
            }
            var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            _context.HttpContext.Session.SetString("email", email);
            /*HttpContext.Session.SetString("userEmail", email);*/
            return RedirectToAction("AddVendor", "Home");
        }

        public IActionResult dashboard()
        {
            return View();
        }


        public IActionResult AddVendor()
        {
            var userEmail = _context.HttpContext.Session.GetString("email");
            if (userEmail==null || userEmail == "")
            {

               return RedirectToAction("Index");
            }

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
        public IActionResult AddVendor(vendor model)
        {
            using (var context = new NewDBContext())
            {
                context.Vendors.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("ViewVendor");
        }

        public IActionResult logout()
        {
            _context.HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
         public IActionResult AddAccount()
         {
             return View();
         }
         [HttpPost]
         public ActionResult AddAccount(account model)
         {
             using (var context = new NewDBContext())
             {
                 context.Accounts.Add(model);
                 context.SaveChanges();
             }
             return View();
         }

        [HttpGet]
        public IActionResult ViewItem()
        {
            var data = new List<_Item>();
            using (var context = new NewDBContext())
            {
                data = context.Items.ToList();
            }

            return View(data);
        }
        public ActionResult AddItem() 
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult AddItem(_Item model)
        {
            using (var context = new NewDBContext())
            {
                context.Items.Add(model);
                context.SaveChanges();
            }
            return View();
        }
        public IActionResult AddPurchaseOrder()
         {
             return View();
         }
        [HttpGet]
        public IActionResult viewaccount()
        {
            var data = new List<account>();
            using (var context = new NewDBContext())
            {
                data = context.Accounts.ToList();
            }

            return View(data);
        }

        /*[HttpPost]
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