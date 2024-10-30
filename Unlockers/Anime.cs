using AchivUnlocker.HtmlParsers;


namespace AchivUnlocker.Unlockers
{
    internal class Anime : WebEntity
    {
        public Anime(string url) : base(url, new SeriesParser()) { }
    }
}
