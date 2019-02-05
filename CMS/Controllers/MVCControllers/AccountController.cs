using Antlr.Runtime;
using CMS.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.MVCControllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserSession _userSession;

        public AccountController(IUserSession userSession)
        {
            _userSession = userSession;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Login(LoginModel model, string returnUrl)
        [AllowAnonymous]
        public ActionResult Login()
        {
            var getTokenUrl = string.Format(ConfigurationManager.AppSettings["ApiBaseUri"].ToString()+ "/oauth/token");
            LoginModel model = new LoginModel()
            {
                Username = "SuperPowerUser",
                Password = "MySuperP@ssword!"
            };
            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.Username),
                    new KeyValuePair<string, string>("password", model.Password)
                });

                HttpResponseMessage result = httpClient.PostAsync(getTokenUrl, content).Result;

                string resultContent = result.Content.ReadAsStringAsync().Result;

                var token = JsonConvert.DeserializeObject<Token>(resultContent);

                AuthenticationProperties options = new AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim("AcessToken", string.Format("Bearer {0}", token.access_token)),
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(options, identity);

            }
            return View();
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }
    }

    internal class Token
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }

        public string token_type { get; set; }
    }
}