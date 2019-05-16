using Agilisium.TalentManager.Web.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Helpers
{
    public static partial class PagingHtmlHelperExtension
    {
        public static MvcHtmlString AddPagination(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageURL, bool isPagingEnabled = true)
        {
            return AddDropDownPagination(html, pagingInfo, pageURL);
            //if (isPagingEnabled == false) return MvcHtmlString.Create(string.Empty);

            //StringBuilder result = new StringBuilder();

            //for (int i = 1; i <= pagingInfo.TotalPageCount; i++)
            //{
            //    TagBuilder tag = new TagBuilder("a");
            //    tag.MergeAttribute("href", pageURL(i));
            //    tag.InnerHtml = i.ToString();
            //    if (i == pagingInfo.CurentPageNo)
            //    {
            //        tag.AddCssClass("selected");
            //        tag.AddCssClass("btn-primary");
            //    }
            //    tag.AddCssClass("btn btn-default");
            //    result.Append(tag.ToString());
            //}

            //return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString AddDropDownPagination(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> url, bool isPagingEnabled = true)
        {
            if (isPagingEnabled == false || pagingInfo == null || pagingInfo.TotalPageCount <= 1)
            {
                return MvcHtmlString.Create(string.Empty);
            }

            StringBuilder htmlStr = new StringBuilder();
            TagBuilder firstLink = new TagBuilder("a");
            firstLink.MergeAttribute("href", url(1));
            if (pagingInfo.CurentPageNo == 1)
            {
                firstLink.AddCssClass("selected");
                firstLink.AddCssClass("btn-primary");
            }
            firstLink.InnerHtml = "First";
            firstLink.AddCssClass("btn btn-default");
            if (pagingInfo.TotalPageCount <= 1 || pagingInfo.CurentPageNo == 1)
            {
                firstLink.MergeAttribute("disabled", "disabled");
            }

            htmlStr.Append(firstLink.ToString());

            TagBuilder previousLink = new TagBuilder("a");
            if (pagingInfo.CurentPageNo == 1)
            {
                previousLink.MergeAttribute("href", url(1));
            }
            else
            {
                previousLink.MergeAttribute("href", url(pagingInfo.CurentPageNo - 1));
            }

            previousLink.InnerHtml = "Prev.";
            previousLink.AddCssClass("btn btn-default");
            if (pagingInfo.TotalPageCount <= 1 || pagingInfo.CurentPageNo == 1)
            {
                previousLink.MergeAttribute("disabled", "disabled");
            }

            htmlStr.Append(previousLink.ToString());

            TagBuilder currentLink = new TagBuilder("text");
            int pageCount = pagingInfo.TotalPageCount == 0 ? 1 : pagingInfo.TotalPageCount;
            currentLink.InnerHtml = $"Page {pagingInfo.CurentPageNo} of {pageCount} pages";
            //currentLink.AddCssClass("btn btn-default");
            currentLink.AddCssClass("btn btn-primary");
            currentLink.MergeAttribute("style", "background-color:lightgray; color:black");
            htmlStr.Append(currentLink.ToString());

            TagBuilder nextLink = new TagBuilder("a");
            if (pagingInfo.CurentPageNo == pagingInfo.TotalPageCount)
            {
                nextLink.MergeAttribute("href", url(pagingInfo.TotalPageCount));
            }
            else
            {
                nextLink.MergeAttribute("href", url(pagingInfo.CurentPageNo + 1));
            }
            nextLink.InnerHtml = "Next";
            nextLink.AddCssClass("btn btn-default");
            if (pagingInfo.TotalPageCount <= 1 || pagingInfo.CurentPageNo == pagingInfo.TotalPageCount)
            {
                nextLink.MergeAttribute("disabled", "disabled");
            }

            htmlStr.Append(nextLink.ToString());

            TagBuilder lastLink = new TagBuilder("a");
            lastLink.MergeAttribute("href", url(pagingInfo.TotalPageCount));
            if (pagingInfo.CurentPageNo == pagingInfo.TotalPageCount)
            {
                lastLink.AddCssClass("selected");
                lastLink.AddCssClass("btn-primary");
            }
            lastLink.InnerHtml = "Last";
            lastLink.AddCssClass("btn btn-default");
            if (pagingInfo.TotalPageCount <= 1 || pagingInfo.CurentPageNo == pagingInfo.TotalPageCount)
            {
                lastLink.MergeAttribute("disabled", "disabled");
            }

            htmlStr.Append(lastLink.ToString());

            return MvcHtmlString.Create(htmlStr.ToString());
        }
    }
}