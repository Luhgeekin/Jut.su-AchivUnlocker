namespace AchivUnlocker
{
    internal class Achiv
    {
        public string title { get; set; } = "";

        public string id { get; set; } = "";

        public string hash { get; set; } = "";

        public bool isUnlocked 
        { 
            get => _isUnlocked;
            set
            {
                if(value == _isUnlocked)
                {
                    return;
                }

                if (value)
                {
                    achivsUnlocked++;
                }
                else
                {
                    achivsUnlocked--;
                }

                _isUnlocked = value;
            } 
        }

        private bool _isUnlocked = false;


        static public string the_login_hash { get; set; } = "";

        static public int achivsUnlocked { get; set; } = 0;
    }
}
