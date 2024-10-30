using AchivUnlocker.Unlockers;
using HtmlAgilityPack;


namespace AchivUnlocker.HtmlParsers
{
    internal class SeriesParser : IHtmlParser
    {
        public List<IUnlocker>? Parse(HtmlDocument doc)
        {
            var watchLNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'watch_l')]");
            if (watchLNode == null)
            {
                return null;
            }

            var seriesElements = watchLNode.SelectNodes(".//a");
            List<IUnlocker> list = new List<IUnlocker>();

            foreach (var seriesElement in seriesElements)
            {
                string seriesURL = seriesElement.GetAttributeValue("href", string.Empty);
                if (_checkForValidSeriesURL(seriesURL))
                {
                    list.Add(new Series("https://jut.su" + seriesURL));
                }
            }

            return list;
        }

        private bool _checkForValidSeriesURL(string str)
        {
            return str.EndsWith(".html");
        }
    }
}
