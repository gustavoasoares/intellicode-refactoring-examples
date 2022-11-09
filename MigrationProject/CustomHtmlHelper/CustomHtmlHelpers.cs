using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using CimenaCityProject.Models;
using CimenaCityProject.ViewModels;
using System.Web.Routing;

namespace CimenaCityProject.CustomHtmlHelper
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlString QRCode(this HtmlHelper htmlHelper, string data, int size = 130,
            int margin = 4, QRCodeErrorCorrectionLevel errorCorrectionLevel = QRCodeErrorCorrectionLevel.High, object htmlAttributes = null)
        {
            if (data == null)
                return null;

            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chld={2}|{3}&chs={0}x{0}&chl={1}",
                size, HttpUtility.UrlEncode(data), errorCorrectionLevel.ToString()[0], margin);

            var tag = new TagBuilder("img");
            if (htmlAttributes != null)
                tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tag.Attributes.Add("src", url);
            tag.Attributes.Add("width", size.ToString());
            tag.Attributes.Add("height", size.ToString());

            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString Image(this HtmlHelper helper, string src, string alt)
        {
            if (src == null)
            {
                src = "~/Image/TempImage.png";
                alt = "Temp";

            }
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("alt", alt);


            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
    }


    public enum QRCodeErrorCorrectionLevel
    {
        /// <summary>Recovers from up to 7% erroneous data.</summary>
        Low,
        /// <summary>Recovers from up to 15% erroneous data.</summary>
        Medium,
        /// <summary>Recovers from up to 25% erroneous data.</summary>
        QuiteGood,
        /// <summary>Recovers from up to 30% erroneous data.</summary>
        High
    }
}