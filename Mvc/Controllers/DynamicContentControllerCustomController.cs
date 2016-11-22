using SitefinityWebApp.Mvc.Attributes;
using SitefinityWebApp.Mvc.Models;
using System.ComponentModel;
using System.Web.Mvc;
using Telerik.Sitefinity.Frontend.Designers;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Controllers;

namespace SitefinityWebApp.Mvc.Controllers
{
    //[ControllerToolboxItem(Name = "DynamicContentControllerCustom", Title = "DynamicContentControllerCustom", SectionName = "MvcWidgets")]
    [MvcWidgetControllerToolboxItemAttribute(Name = "DynamicContentControllerCustom", Title = "DynamicContentControllerCustom",
        SectionName = "MvcWidgets", WidgetName = "PressRelease")]
    [DesignerUrlAttribute("~/Telerik.Sitefinity.Frontend/Designer/Master/DynamicContent")]
    public class DynamicContentControllerCustomController : DynamicContentController
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Category("String Properties")]
        public string Message { get; set; }

        /// <summary>
        /// This is the default Action.
        /// </summary>
        /// using the attribute is the same as:
        /// this.ControllerContext.RouteData.Values["controller"] = "DynamicContentControllerCustom";
        [OwnActionAttribute]
        public ActionResult ShowMessage()
        {
            var model = new MessageModel();
            model.Message = this.Message;

            return View("Default", model);
        }

        public ActionResult FilterByYear(int year)
        {
            ((DynamicContentModelCustom)this.Model).FromYearToFilter = year;

            return base.Index(null);
        }
    }
}