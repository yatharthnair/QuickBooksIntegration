using Intuit.Ipp.OAuth2PlatformClient;
using System.Security.Claims;
using Intuit.Ipp.Core;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using QuickbookIntegration1.Models;
using Intuit.Ipp.Data;
using Intuit.Ipp.Core.Configuration;
using System.Security.Cryptography.X509Certificates;
using Intuit.Ipp.DataService;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QuickbookIntegration1.Controllers
{
    public class AppController : Controller

    {

        NewDBContext db = new NewDBContext();

        public static string clientId = "ABmvAWjedrPeDs7xZZm7OmVtskZ7yxg4S3wvlKq2sbOzClLvf9";
        public static string clientSecret = "vVxCwSHNZXitBtNjYfDbcGZaDJxY33M6vJpdrZBm";
        public static string redirectUrl = "https://localhost:7092/callback";
        public static string environment = "sandbox";

        public static OAuth2Client oauthClient = new OAuth2Client(clientId, clientSecret, redirectUrl, environment);



        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        public IActionResult Initiateoauth2()
        {
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            string authUrl = oauthClient.GetAuthorizationURL(scopes);
            return Redirect(authUrl);

        }


        public ActionResult ApiCallService()
        {
            string access_token = TempData["Access_token"] as string;
            string refresh_token = TempData["Refresh_token"] as string;
            string realmId = TempData["RealmId"] as string;
            var vendorId = 59;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Vendor vendor = new Vendor();

            //List <VendorList> vendorList = 
            var data = db.Vendors.ToList();

            foreach (var item in data)
            {
                vendor.SyncToken = item.SyncToken.ToString();
                /*vendor.PrimaryEmailAddr.Address= item.PrimaryEmailAddress;*/
                vendor.DisplayName = item.DisplayName;
                vendor.GSTIN = item.Gstin;
                vendor.BusinessNumber = item.BusinessNumber.ToString();
                /* vendor.CurrencyRef.Value = "USD";*/
                /*  vendor.CompanyName = item.CompanyName;*/
                /* vendor.AcctNum = item.AcctNum.ToString();
                 vendor.Balance = item.Balance;*/
                dataService.Add<Vendor>(vendor);
            }
            return View();
        }
    }
}
       /* public ActionResult ()
        {
            string access_token = TempData["Access_token"] as string;
            string refresh_token = TempData["Refresh_token"] as string;
            string realmId = TempData["RealmId"] as string;
            var vendorId = 59;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Vendor vendor = new Vendor();

            //List <VendorList> vendorList = 
            var data = db.Vendors.ToList();

            foreach (var item in data)
            {
                vendor.SyncToken = item.SyncToken.ToString();
                *//*vendor.PrimaryEmailAddr.Address= item.PrimaryEmailAddress;*//*
                vendor.DisplayName = item.DisplayName;
                vendor.GSTIN = item.Gstin;
                vendor.BusinessNumber = item.BusinessNumber.ToString();
                *//* vendor.CurrencyRef.Value = "USD";*/
                /*  vendor.CompanyName = item.CompanyName;*/
                /* vendor.AcctNum = item.AcctNum.ToString();
                 vendor.Balance = item.Balance;*//*
                dataService.Add<Vendor>(vendor);
            }
            return View();
        }*/
           /* var vendor = dataService.FindById(new Vendor(),vendorId) as Vendor;

            if (vendor != null)
            {
                Console.WriteLine($"Vendor Name: {vendor.DisplayName}");
                Console.WriteLine($"Vendor Email: {vendor.PrimaryEmailAddr.Address}");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Vendor with ID {vendorId} not found.");
                Console.ReadLine();
            }
            return View();  
        }
         */

            /*  if (HttpContext.Session.Get("realmId") != null)
              {
                  string realmId = "realmId";
                  *//*try
                  {*//*
                  var principal = User as ClaimsPrincipal;
                  OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("access_token");


                  // Create a ServiceContext with Auth tokens and realmId
                  ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
                  serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
                  serviceContext.IppConfiguration.MinorVersion.Qbo = "65";

              }*/


                    /*// Create a QuickBooks QueryService using ServiceContext
                    QueryService<VendorList> querySvc = new QueryService<VendorList>(serviceContext);
                    VendorList vendorlist = querySvc.ExecuteIdsQuery().
                        querySvc.ExecuteIdsQuery("SELECT * FROM VendorList").FirstOrDefault();

                    string output = "Suffix: " + vendorList.Suffix + "DisplayName: " + vendorList.DisplayName + " PrimaryEmailAddress: " + vendorList.PrimaryEmailAddress + "PanID " + vendorList.PanId + " GSTNo" + vendorList.Gstno + " Contact " + vendorList.Contact + "VendorBusiness" + vendorList.VendorBusiness;
                    return View("ApiCallService", (object)("QBO API call Successful!! Response: " + output));
                }
                catch (Exception ex)
                {
                    return View("ApiCallService", (object)("QBO API call Failed!" + " Error message: " + ex.Message));
                }
            }
            else
                return View("ApiCallService", (object)"QBO API call Failed!");
*/
/*        public ActionResult Tokens()
        {
            return View("Tokens");
        }
    }
}*/