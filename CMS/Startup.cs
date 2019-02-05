using CMS.App_Start;
using CMS.Controllers;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;



//[assembly: OwinStartup(typeof(CMS.Startup))]
namespace CMS
{
    public class Startup
    {
        /// <summary> Configurations the specified application. </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var httpConfiguration = CreateHttpConfiguration();
            RegisterCustomControllerFactory();
            app.UseWebApi(httpConfiguration);
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Account/Login"),

            });
        }
        private void RegisterCustomControllerFactory()
        {
            IControllerFactory factory = new CustomControllerFactory("CMS.Controllers.MVCControllers");
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        /// <summary> Creates the HTTP configuration. </summary>
        /// <returns> An <see cref="HttpConfiguration"/> to bootstrap the hosted API </returns>
        public static HttpConfiguration CreateHttpConfiguration()
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();

            return httpConfiguration;
        }
    }
}