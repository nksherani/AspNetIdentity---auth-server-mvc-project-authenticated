using CMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers.MVCControllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IUserSession _userSession;

        public HomeController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        // GET: Home
        public ActionResult Index()
        {
            //var getTokenUrl = string.Format(ConfigurationManager.AppSettings["ApiBaseUri"].ToString() + "/oauth/token");
            //LoginModel model = new LoginModel()
            //{
            //    Username = "SuperPowerUser",
            //    Password = "MySuperP@ssword!"
            //};
            //using (HttpClient httpClient = new HttpClient())
            //{
            //    HttpContent content = new FormUrlEncodedContent(new[]
            //    {
            //        new KeyValuePair<string, string>("grant_type", "password"),
            //        new KeyValuePair<string, string>("username", model.Username),
            //        new KeyValuePair<string, string>("password", model.Password)
            //    });

            //    System.Net.Http.HttpResponseMessage result = httpClient.PostAsync(getTokenUrl, content).Result;

            //    string resultContent = result.Content.ReadAsStringAsync().Result;

            //    //var token = JsonConvert.DeserializeObject<Token>(resultContent);

            //}
            ViewBag.Username = _userSession.Username;
            ViewBag.AccessToken = _userSession.BearerToken;

            return View();
        }
    }


    public interface IUserSession
    {
        string Username { get; }
        string BearerToken { get; }
    }

    public class UserSession : IUserSession
    {

        public string Username
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst(ClaimTypes.Name).Value; }
        }

        public string BearerToken
        {
            get { return ((ClaimsPrincipal)HttpContext.Current.User).FindFirst("AcessToken").Value; }
        }

    }
}