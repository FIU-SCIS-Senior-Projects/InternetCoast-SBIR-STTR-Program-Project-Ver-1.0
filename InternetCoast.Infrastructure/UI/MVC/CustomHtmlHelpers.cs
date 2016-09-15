using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace InternetCoast.Infrastructure.UI.MVC
{
    public class IsTrueAttribute : ValidationAttribute
    {
        #region Overrides of ValidationAttribute

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("can only be used on boolean properties.");

            return (bool)value;
        }

        #endregion
    }

    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString PagerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> property,
            int totalPages = 1, int currentPage = 1)
        {
            const string pager = @"
                                        <ul class='pagination'>
                                            {0}
                                        </ul>";

            const string link = "<li><a href='#'>{0}</a></li>";
            const string activeLink = "<li class='active'><a href='#'>{0}</a></li>";
            const string activeLinkPrev = "<li><a href='#'><span class='glyphicon glyphicon-backward' aria-label='Previous'></span></a></li>";
            const string activeLinkNext = "<li><a href='#'><span class='glyphicon glyphicon-forward' aria-label='Next'></span></a></li>";
            const string disabledLinkPrev = "<li class='disabled'><a href='#'><span class='glyphicon glyphicon-backward' aria-label='Previous'></span></a></li>";
            const string disabledLlinkNext = "<li class='disabled'><a href='#'><span class='glyphicon glyphicon-forward' aria-label='Next'></span></a></li>";

            var links = "";

            if (currentPage > 1)
                links = links + activeLinkPrev;
            else
                links = links + disabledLinkPrev;

            if (totalPages > 5)
            {
                var start = currentPage - 2;
                var end = currentPage + 2;
                while (end > totalPages)
                {
                    start--;
                    end--;
                }

                while (start < 1)
                {
                    start++;
                    end++;
                }

                for (var i = start; i <= end; i++)
                {
                    if (i == currentPage)
                        links = links + string.Format(activeLink, i);
                    else
                        links = links + string.Format(link, i);
                }

            }
            else
            {
                for (var i = 1; i <= totalPages; i++)
                {
                    if (i == currentPage)
                        links = links + string.Format(activeLink, i);
                    else
                        links = links + string.Format(link, i);
                }
            }

            if (currentPage != totalPages)
                links = links + activeLinkNext;
            else
                links = links + disabledLlinkNext;

            return MvcHtmlString.Create(string.Format(pager, links));
        }
    }
}
