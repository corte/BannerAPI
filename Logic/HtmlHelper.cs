using System.Linq;
using HtmlAgilityPack;

namespace BannerApi.Logic
{
    public static class HtmlHelper
    {
        public static bool ValidateHtml(this string html)
        {
            try
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                return !htmlDoc.ParseErrors.Any();
            }
            catch 
            {
                return false;
            }
        }
    }
}