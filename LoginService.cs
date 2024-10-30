using HtmlAgilityPack;
using System.Data;
using System.Text.RegularExpressions;


namespace AchivUnlocker
{
    internal static class LoginService
    {
        public async static Task<bool> TryToLogIn(string login, string password)
        {
            var startUrl = "https://jut.su/";

            var parameters = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("login_name", login),
                    new KeyValuePair<string, string>("login_password", password),
                    new KeyValuePair<string, string>("login", "submit")
                });

            var response = await Client.GetClient().PostAsync(startUrl, parameters);
            string responseStr = await response.Content.ReadAsStringAsync();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseStr);

            return TryToExtractLoginHash(doc);
        }

        private static bool TryToExtractLoginHash(HtmlDocument doc)
        {
            var scriptNodes = doc.DocumentNode.SelectNodes("//script");

            if (scriptNodes != null)
            {
                foreach (var scriptNode in scriptNodes)
                {
                    string scriptContent = scriptNode.InnerText;

                    var match = loginRegex.Match(scriptContent);

                    if (match.Success)
                    {
                        string theLoginHash = match.Groups[1].Value;

                        Achiv.the_login_hash = theLoginHash;
                        return true;
                    }
                }
            }
            return false;
        }

        private static readonly Regex loginRegex = new Regex(@"var the_login_hash = '([^']+)'");
    }
}
