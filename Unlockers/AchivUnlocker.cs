namespace AchivUnlocker.Unlockers
{
    internal class AchivUnlocker : IUnlocker
    {
        public AchivUnlocker(Achiv achiv)
        {
            _achiv = achiv;
        }

        public async Task Unlock()
        {
            var parameters = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("achiv_id", _achiv.id),
                new KeyValuePair<string, string>("achiv_hash", _achiv.hash),
                new KeyValuePair<string, string>("the_login_hash", Achiv.the_login_hash)
            });

            var result = await Client.GetClient().PostAsync("https://jut.su/engine/ajax/get_achievement.php", parameters);
            _achiv.isUnlocked = result.IsSuccessStatusCode;
            Logger.Log(_achiv);
        }

        private Achiv _achiv;
    }
}
