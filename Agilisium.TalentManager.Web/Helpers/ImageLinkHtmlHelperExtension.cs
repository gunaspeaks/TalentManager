using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Agilisium.TalentManager.Web.Helpers
{
    public static class ImageLinkHtmlHelperExtension
    {
        public static MvcHtmlString ImageLink(this HtmlHelper helper, 
            string action, string controller,
            object routeValues, string imageUrl,
            string alternateText, object linkHtmlAttributes,
            object imageHtmlAttributes)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            TagBuilder imageTag = new TagBuilder("img");
            imageTag.MergeAttribute("src", imageUrl);
            imageTag.MergeAttribute("alt", alternateText);
            imageTag.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            TagBuilder linkTag = new TagBuilder("a");
            linkTag.MergeAttribute("href", url.Action(action, controller, new RouteValueDictionary(routeValues)));
            linkTag.InnerHtml = imageTag.ToString(TagRenderMode.SelfClosing);
            linkTag.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            return MvcHtmlString.Create(linkTag.ToString());
        }
    }
}