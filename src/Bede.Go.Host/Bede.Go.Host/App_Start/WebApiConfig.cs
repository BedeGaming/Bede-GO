using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bede.Go.Host
{
    public static class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            // Web API configuration and services
            config.SuppressHostPrincipal();
            config.Filters.Add(new InsecureBearerTokenAuthFilter("Bearer"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
