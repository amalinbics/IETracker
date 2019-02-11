using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace IETracker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
             name: "ApiWithAction",
             routeTemplate: "api/Home/Get/",
             defaults: new { Controller = "Home", Action = "Get" }
           );

            config.Routes.MapHttpRoute(
             name: "CategoryWiseInocmeExpense",
             routeTemplate: "api/Home/GetCategoryWiseInocmeExpense/",
             defaults: new { Controller = "Home", Action = "GetCategoryWiseInocmeExpense" }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional } 


            );




        }
    }
}
