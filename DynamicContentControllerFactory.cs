using SitefinityWebApp.Mvc.Controllers;
using System;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp.Mvc
{
    public class DynamicContentControllerFactory : FrontendControllerFactory
    {     
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            if (controllerName == typeof(DynamicContentControllerCustomController).FullName)
            {
                var routeData = requestContext.RouteData;
                if (routeData == null)
                {
                    routeData = new System.Web.Routing.RouteData();
                }
                if (!routeData.Values.ContainsKey("widgetName"))
                {
                    routeData.Values.Add("widgetName", "DynamicContent");
                }
                requestContext.RouteData = routeData;
            }

            var controller = base.CreateController(requestContext, controllerName);

            return controller;
        }
    }
}