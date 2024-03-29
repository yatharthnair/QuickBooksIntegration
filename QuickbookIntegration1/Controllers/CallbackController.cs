﻿using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using IntegrationWithQuickbooks.Controllers;

namespace QuickbookIntegration1.Controllers
{
    public class CallbackController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<string> list = new List<string>(); 
            var state = HttpContext.Request.Query["state"];
            var realmId = HttpContext.Request.Query["realmId"];
            var code = HttpContext.Request.Query["code"];
            list=await GetAuthTokensAsync(code, realmId);
            list.Add(realmId);
            TempData["Access_token"] = list[0];
            TempData["Refresh_token"] = list[1];
            TempData["RealmId"] = list[2];

            return RedirectToAction("ApiCallService","App");
        }
        private async Task<List<string>> GetAuthTokensAsync(string code, string realmId)
        {

            var tokenResponse = await AppController.oauthClient.GetBearerTokenAsync(code);

            var access_token = tokenResponse.AccessToken;
            var access_token_expires_at = tokenResponse.AccessTokenExpiresIn;

            var refresh_token = tokenResponse.RefreshToken;
            var refresh_token_expires_at = tokenResponse.RefreshTokenExpiresIn;
            List<string> myList = new List<string>();
            myList.Add(access_token);
            myList.Add(refresh_token);
            return (myList);
        }

    }
}