using System.Web.Mvc;

namespace SitefinityWebApp.Mvc.Attributes
{
    public class OwnActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var name = filterContext.Controller.GetType().Name;
            // using convention {MyControllerName}Controller
            // Example: MyDynamicContentController -> MyDynamicContent
            var controllerName = name.Substring(0, name.Length - "Controller".Length);
            filterContext.Controller.ControllerContext.RouteData.Values["controller"] = controllerName;
            base.OnActionExecuting(filterContext);
        }
    }
}