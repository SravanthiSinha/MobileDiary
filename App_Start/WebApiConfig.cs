using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.SessionState;
using System.Web.Routing;
using System.Web;

namespace NewMObileDiary
    {
    public static class WebApiConfig 
        {
        public static void Register(RouteCollection routes)
            {
            var route = routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
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

    }
