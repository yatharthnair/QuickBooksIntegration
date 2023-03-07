
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

        /*public IActionResult ViewVendor()
        {   
            return View();
        }*/



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
        public IActionResult OAuth2Client()
        {
            string clientId = "ABmvAWjedrPeDs7xZZm7OmVtskZ7yxg4S3wvlKq2sbOzClLvf9";
            string clientSecret = "vVxCwSHNZXitBtNjYfDbcGZaDJxY33M6vJpdrZBm";
            string redirectUrl = "https://localhost:7092/Home/ViewVendor";
            string environment = "sandbox";
 
            OAuth2Client oauthClient = new OAuth2Client(clientId, clientSecret, redirectUrl, environment);


            List<OidcScopes>? scopes = null;
            string authUrl = oauthClient.GetAuthorizationURL(scopes);

            string authCode = Request.Query["code"];
            var token = oauthClient.GetBearerTokenAsync(authCode);
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("eyJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiYWxnIjoiZGlyIn0..kzfe1IkaGrWQSUzKYfa4LQ.J2cnU-l_yAeuko85plp0BdHS4SOXtErB7pAkGuvqIBHoI8iHgOyAf4vRVfaCut55gOnM1BZMNAifRwzEWKxrwDXeQ9ZyM2KZDo0sb4FniAiSOMMCQ30MKTBMtvu1I1NrBVc7Kskk41w2-E69rIKlWjwC5t8QH5I4GaO5I-dV0M82pD46TnXbfFUVPqF7VVAVcNsgbYFGa5T39XmguLbTaZiDVdv_ghOAfHNG_kWZTqfT0BRoUts5QZr1qRfeqw2pn-EQUwkJmZWYJXUsN9M9bUT1xDkUly_XqAHbzSXcKz8y1PSfftCnf2c-YqqtDEVoQtivx1X07SWPsPxKoioZ9WwbkeEQ2yRsFGXnxOPSSSA4E52UvWaxu1cc3jnV8nui9aV6Ueqb7P89nE7riTKor_npIhGkG8yldl1g63f-ka6cr3_XLbmHyFB-wxZvPiW9A8dEAlkWh8J0emabMkQMvTMjvlO35mIyITIPlFcVT1X5BIDHC1KHCAFA6PJordDIT_beeOn3Oqmw9nrQKuq2UHQhT-cYQogB5lwIckMmilLZc1gghTL3zo600knSxRwB8a9NW6AGoeNH89t8qEUGFS3OCAR16YI8eAuNvg2dSHx8mGBMKTYzMLYSqHpr5y5Q5tKpKDCIfFBrytTU0ITH2byBUD6KVa2Vjmm8E9hxHfJSGT7bjy1JPxeXvu--Rtq_mehh7J5MGTBTYB6jY6d0cqzXjwz2WjNfeLaT_fgHUC-Q5SLUmmVq8ACKOcHae1G9.b3q8DODnQRQflzOB8sZqRw");
            DataService service = new DataService(oauthValidator);

        }
    }
}
        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    