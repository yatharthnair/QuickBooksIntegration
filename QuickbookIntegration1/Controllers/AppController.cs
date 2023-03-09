using Intuit.Ipp.OAuth2PlatformClient;
using System.Security.Claims;
using Intuit.Ipp.Core;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using QuickbookIntegration1.Models;

namespace QuickbookIntegration1.Controllers
{
    public class AppController : Controller
    {
        public static string clientId = "ABmvAWjedrPeDs7xZZm7OmVtskZ7yxg4S3wvlKq2sbOzClLvf9";
        public static string clientSecret = "vVxCwSHNZXitBtNjYfDbcGZaDJxY33M6vJpdrZBm";
        public static string redirectUrl = "https://localhost:7092/callback";
        public static string environment = "sandbox";

        public static OAuth2Client oauthClient = new OAuth2Client(clientId, clientSecret, redirectUrl, environment);

       

        public ActionResult Index()
        {
            HttpContext.Session.Clear(); // Clear the session data
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // Sign out the authentication cookie
            return View();
        }

        public IActionResult Initiateoauth2()
        {
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            string authUrl = oauthClient.GetAuthorizationURL(scopes);
            return Redirect(authUrl);

        }

        public ActionResult ApiCallService(VendorList vendorList)
        {
            //HttpContext.Session.SetString("key", "value");

            if (HttpContext.Session.Get("realmId") != null)
            {
                string realmId = HttpContext.Session.Get("realmId").ToString();
                try
                {
                    var principal = User as ClaimsPrincipal;
                    OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(principal.FindFirst("access_token").Value);

                    // Create a ServiceContext with Auth tokens and realmId
                    ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
                    serviceContext.IppConfiguration.MinorVersion.Qbo = "23";

                    // Create a QuickBooks QueryService using ServiceContext
                    QueryService<VendorList> querySvc = new QueryService<VendorList>(serviceContext);
                    VendorList vendorlist = querySvc.ExecuteIdsQuery("SELECT * FROM VendorList").FirstOrDefault();

                    string output = "Suffix: " + vendorList.Suffix + "DisplayName: " + vendorList.DisplayName + " PrimaryEmailAddress: " + vendorList.PrimaryEmailAddress + "PanID " + vendorList.PanId + " GSTNo" + vendorList.Gstno + " Contact " + vendorList.Contact + "VendorBusiness" + vendorList.VendorBusiness;
                    return View("ApiCallService", (object)("QBO API call Successful!! Response: " + output));
                }
                catch (Exception ex)
                {
                    return View("ApiCallService", (object)("QBO A PI call Failed!" + " Error message: " + ex.Message));
                }
            }
            else
                return View("ApiCallService", (object)"QBO API call Failed!");
        }
        public ActionResult Tokens()
        {
            return View("Tokens");
        }
    }
}