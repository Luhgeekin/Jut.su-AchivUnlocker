using AchivUnlocker.HtmlParsers;


namespace AchivUnlocker.Unlockers
{
    internal class Series : WebEntity
    {
        public Series(string url) : base(url, new AchivParser()) { }
    }
}
