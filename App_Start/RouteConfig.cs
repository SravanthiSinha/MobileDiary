using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http.WebHost;
using System.Web.SessionState;

namespace NewMObileDiary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var route = routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            route.RouteHandler = new SessionHttpControllerRouteHandler();
        }

        public class SessionHttpControllerRouteHandler : HttpControllerRouteHandler
            {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
                {
                return new SessionHttpControllerHandler(requestContext.RouteData);
                }
            }

        public class SessionHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
            {
            public SessionHttpControllerHandler(RouteData routeData)
                : base(routeData)
                {
                }
            }
    }

    public static class AppConstants
    {
        public static string SessionSupport = "Enabled";
    }
}