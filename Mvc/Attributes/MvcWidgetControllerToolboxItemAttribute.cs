using System;

namespace SitefinityWebApp
{
    public class MvcWidgetControllerToolboxItemAttribute : Attribute
    {
        // [ControllerToolboxItem(Name = "DynamicContentControllerCustom", Title = "DynamicContentControllerCustom", SectionName = "MvcWidgets")]
        // <add enabled="True" type="Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers.MvcWidgetProxy" controllerType="SitefinityWebApp.Mvc.Controllers.DynamicContentControllerCustomController" title="DynamicContentControllerCustom" ControllerName="SitefinityWebApp.Mvc.Controllers.DynamicContentControllerCustomController" WidgetName="Speaker" visibilityMode="None" name="DynamicContentControllerCustom" />

        public string Name { get; set; }

        public string Title { get; set; }

        public string SectionName { get; set; }

        public string WidgetName { get; set; }
    }
}