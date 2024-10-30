using AchivUnlocker.Unlockers;
using HtmlAgilityPack;

namespace AchivUnlocker.HtmlParsers
{
    internal interface IHtmlParser
    {
        List<IUnlocker>? Parse(HtmlDocument doc);
    }

}
