using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using System.Web.Http;
using System.Web.Http.Routing;

namespace IoTTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.AddApiVersioning();
            //DefaultInlineConstraintResolver constraintResolver = new DefaultInlineConstraintResolver()
            //{
            //    ConstraintMap =
            //    {
            //        ["apiVersion"] = typeof( ApiVersionRouteConstraint )
            //    }
            //};

            // Web API routes
            //config.MapHttpAttributeRoutes(constraintResolver);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
