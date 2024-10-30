using AchivUnlocker.Unlockers;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;


namespace AchivUnlocker.HtmlParsers
{
    internal class AchivParser : IHtmlParser
    {
        public List<IUnlocker>? Parse(HtmlDocument doc)
        {
            HtmlNode videoBlockDocument = doc.DocumentNode.SelectSingleNode("//div[@class='videoBlockWrapp']");
            if (videoBlockDocument == null)
            {
                return null;
            }

            HtmlNodeCollection EncodedScripts = videoBlockDocument.SelectNodes("script");
            if (EncodedScripts.Count < 2)
            {
                return null;
            }

            string script = DecodeJsScript(EncodedScripts[1].InnerText);

            return GetAchivListFromJsScript(script);
        }

        private string DecodeJsScript(string encodedScript)
        {
            string result = encodedScript;

            //may look weird
            for (int i = 0; i < 2; i++)
            {
                Match base64match = scriptRegex.Match(result);
                byte[] base64DecodedBytes = Convert.FromBase64String(base64match.Value);
                string decodedString = Encoding.UTF8.GetString(base64DecodedBytes);
                result = decodedString;
            }

            return result;
        }

        private List<IUnlocker> GetAchivListFromJsScript(string script)
        {
            MatchCollection matches = achivRegex.Matches(script);

            List<IUnlocker> list = new List<IUnlocker>();
            foreach (Match match in matches)
            {
                Achiv achiv = new Achiv
                {
                    title = match.Groups["title"].Value,
                    id = match.Groups["id"].Value,
                    hash = match.Groups["hash"].Value
                };

                list.Add(new Unlockers.AchivUnlocker(achiv));
            }
            return list;
        }

        private static readonly Regex scriptRegex = new Regex("(?<=\")[^\"]*(?=\")");
        private static readonly Regex achivRegex = new Regex(@"this_anime_achievements\.push\(\{.*?title:\s*""(?<title>[^""]+)"",.*?id:\s*""(?<id>\d+)"",.*?hash:\s*""(?<hash>[a-fA-F0-9]+)""", RegexOptions.Singleline);
    }
}
