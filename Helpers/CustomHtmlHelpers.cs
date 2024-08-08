using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MVCProject_Nazmul.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static MvcHtmlString SearchForm<TModel>(this HtmlHelper<TModel> htmlHelper, string buttonText = "Search")
        {
            var form = new TagBuilder("form");
            var currentUrl = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            form.Attributes.Add("action", currentUrl.Action(htmlHelper.ViewContext.RouteData.Values["action"].ToString(), htmlHelper.ViewContext.RouteData.Values["controller"].ToString()));
            form.Attributes.Add("method", "get");

            var paragraph = new TagBuilder("p");
            paragraph.InnerHtml = "Find By Name: " + htmlHelper.TextBox("searchString");

            var submitButton = new TagBuilder("input");
            submitButton.Attributes.Add("type", "submit");
            submitButton.Attributes.Add("value", buttonText);
            submitButton.AddCssClass("btn btn-info");

            form.InnerHtml = paragraph.ToString() + submitButton.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(form.ToString());
        }

        public static MvcHtmlString CustomPager(this System.Web.Mvc.HtmlHelper htmlHelper, PagedList.IPagedList model)
        {
            var pageCount = model.PageCount;
            var pageNumber = model.PageNumber;

            var pageText = "Page " + (pageCount < pageNumber ? 0 : pageNumber) + " of " + pageCount;

            var pager = htmlHelper.PagedListPager(model, page =>
                htmlHelper.ViewContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Path) + "?" +
                $"page={page}&sortOrder={htmlHelper.ViewBag.CurrentSort}&currentFilter={htmlHelper.ViewBag.CurrentFilter}");

            return MvcHtmlString.Create(pageText + " " + pager);
        }
    }
}