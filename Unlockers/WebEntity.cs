using AchivUnlocker.HtmlParsers;
using HtmlAgilityPack;


namespace AchivUnlocker.Unlockers
{
    internal abstract class WebEntity : IUnlocker
    {
        public WebEntity(string url, IHtmlParser parser)
        {
            _url = url;
            _parser = parser;
        }

        public async Task Unlock()
        {
            try
            {
                _parsedItems = _parser.Parse(await GetHtmlDoc());
                await UnlockAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        protected async Task<HtmlDocument> GetHtmlDoc()
        {
            var response = await Client.GetClient().GetAsync(_url);
            string responseStr = await response.Content.ReadAsStringAsync();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseStr);

            return doc;
        }

        protected async Task UnlockAll()
        {
            if (_parsedItems == null)
            {
                return;
            }

            foreach (IUnlocker unlocker in _parsedItems)
            {
                await unlocker.Unlock();
            }
        }

        protected string _url;
        protected IHtmlParser _parser;
        protected List<IUnlocker>? _parsedItems;
    }
}
