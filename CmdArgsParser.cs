namespace AchivUnlocker
{
    internal static class CmdArgParser
    {
        public struct Config
        {
            
            public string login { get; set; }
            public string password { get; set; }
            public List<string> URLs { get; set; }
        }

        public static Config ParseArgs(string[] args)
        {
            if (args.Length == 1 && args[0] == "help")
            {
                Console.WriteLine("Usage: <login> <password> <URL> <URL>...<URL>");
                Environment.Exit(0);
            }

            if (args.Length < 3)
            {
                Console.WriteLine("Invalid arguments. Use 'help' for guidance.");
                Environment.Exit(1);
            }


            Config config = new Config();
            config.login = args[0];
            config.password = args[1];

            config.URLs = new List<string>();
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i].StartsWith("https://jut.su/"))
                {
                    config.URLs.Add(args[i]);
                }
            }

            return config;
        }
    }
}
