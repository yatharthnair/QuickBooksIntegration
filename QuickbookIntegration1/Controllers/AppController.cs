using Intuit.Ipp.OAuth2PlatformClient;
using System.Security.Claims;
using Intuit.Ipp.Core;

using Intuit.Ipp.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using QuickbookIntegration1.Models;
using Intuit.Ipp.Data;

using Intuit.Ipp.DataService;


namespace QuickbookIntegration1.Controllers
{

    public class AppController : Controller

    {
        private readonly IHttpContextAccessor _context;

        public AppController(IHttpContextAccessor context)
        {

            _context = context;

        }

        NewDBContext db = new NewDBContext();

        public static string clientId = "ABmvAWjedrPeDs7xZZm7OmVtskZ7yxg4S3wvlKq2sbOzClLvf9";
        public static string clientSecret = "vVxCwSHNZXitBtNjYfDbcGZaDJxY33M6vJpdrZBm";
        public static string redirectUrl = "https://localhost:7092/callback";
        public static string environment = "sandbox";

        public static OAuth2Client oauthclient = new OAuth2Client(clientId, clientSecret, redirectUrl, environment);



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
            string authUrl = oauthclient.GetAuthorizationURL(scopes);
            return Redirect(authUrl);

        }


        public IActionResult QBvendor()
        {
            /*string access_token = TempData["Access_token"] as string*/
            /*string refresh_token = TempData["Refresh_token"] as string;
            string realmId = TempData["RealmId"] as string;*/
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Vendor vendor = new Vendor();

            //List <VendorList> vendorList = 
            var data = db.Vendors.Where(c => c.QBid == null).ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    /*vendor.SyncToken = item.SyncToken.ToString();*/
                    vendor.PrimaryEmailAddr = new EmailAddress();
                    vendor.PrimaryEmailAddr.Address = item.PrimaryEmailAddress;
                    vendor.DisplayName = item.DisplayName;
                    vendor.GSTIN = item.Gstin;
                    vendor.BusinessNumber = item.BusinessNumber.ToString();
                    /*vendor.Mobile.FreeFormNumber = item.OtherContactInfo;*/
                    /* vendor.CurrencyRef.Value = "USD";*/
                    /*  vendor.CompanyName = item.CompanyName;*/
                    /* vendor.AcctNum = item.AcctNum.ToString();
                     vendor.Balance = item.Balance;*/
                    Vendor addedObj = dataService.Add<Vendor>(vendor);
                    var QbId = addedObj.Id;
                    using (var context = new NewDBContext())
                    {
                        // Retrieve the vendor entity using its primary key value
                        var vendorToUpdate = context.Vendors.Find(item.Id);

                        if (vendorToUpdate != null)
                        {
                            // Update the QBID property of the retrieved entity with the new QBID value
                            vendorToUpdate.QBid = QbId;

                            // Save the changes back to the database
                            context.SaveChanges();

                        }
                    }
                }
                return View();
            }
            return RedirectToAction("Home", "ViewVendor");

        }

        public ActionResult QBAccount()
        {
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Account account = new Account();
            var data = db.Accounts.Where(c => c.QBaccid == null).ToList();

            foreach (var item in data)
            {
                /*account.Id = item.Id.ToString();*/
                account.Name = item.Name;
                account.AccountType = AccountTypeEnum.AccountsReceivable;
                account.AccountSubType = AccountSubTypeEnum.AccountsReceivable.ToString();
                account.AcctNum = item.AcctNum;
                account.CurrentBalance = (decimal)item.CurrentBalance;
                Account addedobj = dataService.Add<Account>(account);
                var QBid = addedobj.Id;
                using (var context = new NewDBContext())
                {
                    // Retrieve the vendor entity using its primary key value
                    //var Accountupdate = context.Accounts.Find(item.Id.ToString());
                    var acct = context.Accounts.Where(x => x.Id == item.Id).FirstOrDefault();

                    if (acct != null)
                    {
                        // Update the QBID property of the retrieved entity with the new QBID value
                        acct.QBaccid = QBid;
                        context.Entry(acct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        // Save the changes back to the database
                        context.SaveChanges();
                    }
                }

            }
            return RedirectToAction("Home", "viewaccount");
        }

        public ActionResult QBitem()
        {
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);
            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Intuit.Ipp.Data.Item i = new Intuit.Ipp.Data.Item();
            var data = db.Items.Where(c => c.QBitem == null).ToList();
            foreach (var item in data)
            {
                i.Name = item.Name;
                i.Description = "This is a test item";
                i.Type = ItemTypeEnum.NonInventory;
                i.InvStartDate = DateTime.Now;
    /*            i.QtyOnHand = 10;*/
                i.TypeSpecified = true;
                i.Active = true;
                /* var Accdata = db.Accounts.Where(x => x.Id == 3).FirstOrDefault();
                 var QBaccid1 = Accdata.QBaccid;
                 var Accdata2 = db.Accounts.Where(x => x.Id == 5).FirstOrDefault();
                 var QBaccid2 = Accdata2.QBaccid;*/
                i.ExpenseAccountRef = new ReferenceType() { Value = item.expenseaccref.ToString() };
                i.IncomeAccountRef = new ReferenceType() { Value = "232" };
                i.Sku=item.SKU.ToString();
                i.PurchaseCost = (decimal)item.cost;
                Intuit.Ipp.Data.Item addeditem = dataService.Add<Intuit.Ipp.Data.Item>(i);
                var QBid = addeditem.Id;
                using (var context = new NewDBContext())
                {
                    var item1 = context.Items.Where(x => x.Id == item.Id).FirstOrDefault();

                    if (item1 != null)
                    {
                        // Update the QBID property of the retrieved entity with the new QBID value
                        item1.QBitem = QBid;
                        context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        // Save the changes back to the database
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Home", "ViewItem");
        }
        public ActionResult QBpurchase()
        {
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            var itemdata = db.Items.ToList();
            var data = db.Pos.Where(c => c.QBid == null).ToList();
            foreach (var item in data)
            {
                purchaseOrder.VendorRef = new ReferenceType()
                {
                    Value = item.VendorRef.ToString()
                };
                Line[] line = new Line[1];
                line[0] = new Line();
                line[0].DetailType = LineDetailTypeEnum.ItemBasedExpenseLineDetail;
                ItemBasedExpenseLineDetail itemBasedExpense = new ItemBasedExpenseLineDetail();
                itemBasedExpense.ItemAccountRef = new ReferenceType()
                {
                    Value = item.itemaccref.ToString()
                };
                itemBasedExpense.ItemRef = new ReferenceType()
                {
                    /*  name= "Laptops",*/
                    Value = item.itemid.ToString() 
                };
                line[0].AnyIntuitObject = itemBasedExpense;
                line[0].DetailTypeSpecified = true;
                var amount = item.qty * item.rate;
                line[0].Amount = amount;
                line[0].AmountSpecified = true;
                purchaseOrder.Line = line;
                purchaseOrder.TxnDate = DateTime.Now;
                purchaseOrder.TxnDateSpecified = true;
                purchaseOrder.DueDate = item.due;
                purchaseOrder.DueDateSpecified = true;
                PurchaseOrder addedobj = dataService.Add<PurchaseOrder>(purchaseOrder);
                var QBid = addedobj.Id;
                using (var context = new NewDBContext())
                {
                    var item1 = context.Pos.Where(x => x.Id == item.Id).FirstOrDefault();

                    if (item1 != null)
                    {  
                        // Update the QBID property of the retrieved entity with the new QBID value
                        item1.QBid = QBid;
                        context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        // Save the changes back to the database
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Home", "ViewPurchaseOrder");
        }


        public ActionResult GenBill()
        {
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Intuit.Ipp.Data.Bill bill = new Intuit.Ipp.Data.Bill();
            var data = db.Bills.Where(c => c.QBbillid == null).ToList();
            foreach (var item in data)
            {
                bill.VendorRef = new ReferenceType() { Value = item.VendorRef.ToString() };
                bill.APAccountRef = new ReferenceType()
                {
                    Value = "230"
                };
                Line[] line = new Line[1];
                line[0] = new Line();
                line[0].DetailType = LineDetailTypeEnum.ItemBasedExpenseLineDetail;
                line[0].DetailTypeSpecified = true;
                ItemBasedExpenseLineDetail itemBasedExpense = new ItemBasedExpenseLineDetail();
                itemBasedExpense.ItemRef = new ReferenceType() { Value = item.itemref.ToString() };
                line[0].Amount = item.Qty * item.rate;
                line[0].AmountSpecified = true;
                line[0].AnyIntuitObject = itemBasedExpense;

                bill.Line = line;

                Intuit.Ipp.Data.Bill addedobj = dataService.Add<Intuit.Ipp.Data.Bill>(bill);
                var QBid = addedobj.Id;
                using (var context = new NewDBContext())
                {
                    var item1 = context.Bills.Where(x => x.Id == item.Id ).FirstOrDefault();

                    if (item1 != null)
                    {
                        // Update the QBID property of the retrieved entity with the new QBID value
                        item1.QBbillid = QBid.ToString();
                        context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        // Save the changes back to the database
                        context.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Home", "ViewBill");
        }
       
        public ActionResult Customer()
        {
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Intuit.Ipp.Data.Customer customer = new Intuit.Ipp.Data.Customer();
            var data = db.cust.Where(c => c.QBcustid == null).ToList();
            foreach (var item in data)
            {
                customer.DisplayName = item.DisplayName;
                customer.PrimaryEmailAddr = new EmailAddress();
                customer.PrimaryEmailAddr.Address = item.Email;
                customer.Mobile = new TelephoneNumber();
                customer.Mobile.FreeFormNumber = item.Mobile;
                Intuit.Ipp.Data.Customer addedcustomer = dataService.Add<Customer>(customer);
                var QBid = addedcustomer.Id;
                using (var context = new NewDBContext())
                {
                    var item1 = context.cust.Where(x => x.Id == item.Id).FirstOrDefault();

                    if (item1 != null)
                    {
                        // Update the QBID property of the retrieved entity with the new QBID value
                        item1.QBcustid = QBid.ToString();
                        context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        // Save the changes back to the database
                        context.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Home","ViewCustomer");
        }
        public ActionResult Invoice() 
        {
            string access_token = _context.HttpContext.Session.GetString("Access_token") as string;
            string refresh_token = _context.HttpContext.Session.GetString("Refresh_token") as string;
            string realmId = _context.HttpContext.Session.GetString("RealmId") as string;
            var principal = User as ClaimsPrincipal;
            OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(access_token);


            // Create a ServiceContext with Auth tokens and realmId
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "https://sandbox-quickbooks.api.intuit.com/";
            serviceContext.IppConfiguration.MinorVersion.Qbo = "65";
            var dataService = new DataService(serviceContext);
            Intuit.Ipp.Data.Invoice so=new Intuit.Ipp.Data.Invoice();
            var data = db.inv.Where(c => c.QBInvid == null).ToList();
            foreach (var item in data)
            {
                so.CustomerRef = new ReferenceType() { Value = item.customerref };
                Line[] line = new Line[1];
                line[0] = new Line();
                line[0].DetailType = LineDetailTypeEnum.SalesItemLineDetail;
                line[0].DetailTypeSpecified = true;
                SalesItemLineDetail salesItemLineDetail = new SalesItemLineDetail();
                salesItemLineDetail.Qty = item.qty;
                salesItemLineDetail.ItemRef = new ReferenceType() { Value = item.itemref };
                salesItemLineDetail.DiscountRate = item.rate;
                /*salesItemLineDetail.DiscountAmt = 150;*/
                salesItemLineDetail.DiscountRateSpecified = true;
                salesItemLineDetail.DiscountAmtSpecified = true;
                line[0].AnyIntuitObject = salesItemLineDetail;
                line[0].Amount = item.qty * item.rate;
                line[0].AmountSpecified = true;
                so.Line = line;
                so.EmailStatus = EmailStatusEnum.EmailSent;
                so.EmailStatusSpecified = true;
                so.BillEmail = new EmailAddress();
                so.BillEmail.Address = item.Email;
                Intuit.Ipp.Data.Invoice addedso = dataService.Add<Intuit.Ipp.Data.Invoice>(so);
                var QBid = addedso.Id;
                using (var context = new NewDBContext())
                {
                    var item1 = context.inv.Where(x => x.id == item.id).FirstOrDefault();

                    if (item1 != null)
                    {
                        // Update the QBID property of the retrieved entity with the new QBID value
                        item1.QBInvid = QBid.ToString();
                        context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        // Save the changes back to the database
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Home", "ViewInvoice");
        }
    }
}
  