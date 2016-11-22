using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Frontend.DynamicContent.Mvc.Models;
using Telerik.Sitefinity.Model;

namespace SitefinityWebApp.Mvc.Models
{
    public class DynamicContentModelCustom : DynamicContentModel
    {
        public int FromYearToFilter { get; set; }

        protected override IEnumerable<Telerik.Sitefinity.Frontend.Mvc.Models.ItemViewModel> ApplyListSettings(int page, IQueryable<Telerik.Sitefinity.Model.IDataItem> query, out int? totalPages)
        {
            string pressReleaseTypeFullName = "Telerik.Sitefinity.DynamicTypes.Model.Pressreleases.PressRelease";
            if (this.ContentType.FullName == pressReleaseTypeFullName && this.FromYearToFilter > 0)
            {
                query = query.Cast<DynamicContent>()
                    .Where(this.FilterByYear);
            }

            return base.ApplyListSettings(page, query, out totalPages);
        }

        protected Expression<Func<DynamicContent, bool>> FilterByYear
        {
            get
            {
                return pr => pr.GetValue<DateTime?>("ReleaseDate").HasValue && pr.GetValue<DateTime?>("ReleaseDate").Value.Year == this.FromYearToFilter;
            }
        }
    }
}
