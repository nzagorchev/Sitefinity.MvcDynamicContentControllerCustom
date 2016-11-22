using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitefinityWebApp
{
    public class MyViewEngine : RazorViewEngine
    {
        public MyViewEngine()
        {
            var viewLocations = this.ViewLocationFormats.ToList();
            viewLocations.Add("~/Frontend-Assembly/Telerik.Sitefinity.Frontend.DynamicContent/Mvc/Views/{1}/{0}.cshtml");
            this.ViewLocationFormats = viewLocations.ToArray();
        }
    }
}