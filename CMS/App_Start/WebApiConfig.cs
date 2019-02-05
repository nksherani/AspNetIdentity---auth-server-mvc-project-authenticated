using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace CMS.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // A route that enables RPC requests
            config.Routes.MapHttpRoute(
                name: "RpcApi",
                routeTemplate: "cmsapi/{controller}/{action}",
                defaults: new { action = "Get" }
            );

            //route that considers action name as well
            config.Routes.MapHttpRoute(
                name: "ActionRoute",
                routeTemplate: "cmsapi/{controller}/Action/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "cmsapi/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            //convert response to JSON
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            //convert JSON response to camelCase
            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //var settings = jsonFormatter.SerializerSettings;
            //settings.Formatting = Formatting.Indented;
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var json = config.Formatters.JsonFormatter;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.OfType<JsonMediaTypeFormatter>().First().SerializerSettings.Formatting = Formatting.Indented;

        }
    }
}