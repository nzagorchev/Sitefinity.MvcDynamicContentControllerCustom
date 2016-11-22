using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Frontend.Mvc.Infrastructure.Controllers;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Mvc.Store;

namespace SitefinityWebApp
{
    public class MvcWidgetToolboxRegistrationHelper
    {
        public static void Register()
        {
            var helper = new MvcWidgetToolboxRegistrationHelper();
            var infos = helper.GetControllerInfos();
            bool saveRequired = false;
            ConfigManager configManager = ConfigManager.GetManager();
            using (new ElevatedConfigModeRegion())
            {
                var section = configManager.GetSection<ToolboxesConfig>();
                foreach (var info in infos)
                {
                    bool needSave = helper.AddControllerToPageToolbox(info, section);
                    if (needSave)
                    {
                        saveRequired = true;
                    }
                }

                if (saveRequired)
                {
                    configManager.SaveSection(section);
                }
            }
        }

        private List<ControllerInfoExtended> GetControllerInfos()
        {
            var assembly = Assembly.GetCallingAssembly();
            var types = assembly.GetTypes();
            var controllerInfos = new List<ControllerInfoExtended>();
            foreach (var type in types)
            {
                if (typeof(System.Web.Mvc.IController).IsAssignableFrom(type))
                {
                    // attribute excluding inheriting one
                    var attributes = type.GetCustomAttributes(typeof(MvcWidgetControllerToolboxItemAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                    {
                        var toolboxItemAttribute = attributes.OfType<MvcWidgetControllerToolboxItemAttribute>()
                            .FirstOrDefault();
                        if (toolboxItemAttribute != null)
                        {
                            var info = new ControllerInfoExtended();
                            info.ControllerType = type;

                            info.IncludeInToolbox = true;
                            info.DefaultToolboxItemName = toolboxItemAttribute.Name;
                            info.DefaultToolboxItemTitle = toolboxItemAttribute.Title;
                            info.DefaultToolboxSectionName = toolboxItemAttribute.SectionName;
                            //info.DefaultToolboxModuleName = toolboxItemAttribute.ModuleName;
                            //info.DefaultToolboxCssClass = toolboxItemAttribute.CssClass;
                            //info.Toolbox = toolboxItemAttribute.Toolbox;
                            info.WidgetName = toolboxItemAttribute.WidgetName;

                            controllerInfos.Add(info);
                        }
                    }
                }
            }

            return controllerInfos;
        }

        private bool AddControllerToPageToolbox(ControllerInfoExtended controllerInfo, ToolboxesConfig toolboxConfig)
        {
            if (!controllerInfo.IncludeInToolbox)
                return false;

            bool saveRequired = false;

            // get the pages toolbox
            var toolboxName = string.IsNullOrEmpty(controllerInfo.Toolbox) ? "PageControls" : controllerInfo.Toolbox;
            var toolbox = toolboxConfig.Toolboxes[toolboxName];

            // create the new toolbox section
            var controllerSection = toolbox.Sections
                .Where<ToolboxSection>(s => s.Name == controllerInfo.DefaultToolboxSectionName).SingleOrDefault();

            if (controllerSection == null)
            {
                saveRequired = true;
                controllerSection = new ToolboxSection(toolbox.Sections);
                controllerSection.Name = controllerInfo.DefaultToolboxSectionName;
                controllerSection.Title = controllerInfo.DefaultToolboxSectionName;
                toolbox.Sections.Add(controllerSection);
            }

            // create the controller toolbox item
            ToolboxItem toolboxItem = controllerSection.Tools.Where<ToolboxItem>(t => t.Name == controllerInfo.DefaultToolboxItemName).SingleOrDefault();
            if (toolboxItem == null)
            {
                saveRequired = true;
                string mvcWidgetProxyFullName = this.GetMvcWidgetProxyFullName();

                toolboxItem = new ToolboxItem(controllerSection.Tools);
                toolboxItem.Title = controllerInfo.DefaultToolboxItemTitle;
                toolboxItem.Name = controllerInfo.DefaultToolboxItemName;
                toolboxItem.ControlType = mvcWidgetProxyFullName;
                toolboxItem.ControllerType = controllerInfo.ControllerType.FullName;
                toolboxItem.ModuleName = controllerInfo.DefaultToolboxModuleName;
                toolboxItem.CssClass = controllerInfo.DefaultToolboxCssClass;

                toolboxItem.Parameters.Add("ControllerName", controllerInfo.ControllerType.FullName);
                toolboxItem.Parameters.Add("WidgetName", controllerInfo.WidgetName);

                controllerSection.Tools.Add(toolboxItem);
            }

            return saveRequired;
        }

        protected virtual string GetMvcWidgetProxyFullName()
        {
            return typeof(MvcWidgetProxy).FullName;
        }
    }

    public class ControllerInfoExtended : ControllerInfo
    {
        public string WidgetName { get; set; }
    }
}