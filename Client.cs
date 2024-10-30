using System.Net;
using System.Text;


namespace AchivUnlocker
{
    internal static class Client
    {
        private static readonly Lazy<HttpClient> _client = new Lazy<HttpClient>(CreateHttpClient());

        public static HttpClient GetClient() => _client.Value;

        private static HttpClient CreateHttpClient()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli
            };

            HttpClient client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Accept-Language", "en");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            return client;
        }
    }
}
