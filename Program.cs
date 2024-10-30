using AchivUnlocker.Unlockers;


namespace AchivUnlocker
{
    class Program
    {
        static async Task Main(string[] args)
        {

            CmdArgParser.Config config = CmdArgParser.ParseArgs(args);


            if(!await LoginService.TryToLogIn(config.login, config.password))
            {
                Console.WriteLine("Wrong login or password");
                return;
            }

            foreach(string URL in config.URLs) 
            {
                IUnlocker unlocker = CreateUnlocker(URL);
                await unlocker.Unlock();

            }

            Console.WriteLine($"Achivs unlocked: {Achiv.achivsUnlocked}");
        }

        static IUnlocker CreateUnlocker(string URL)
        {
            if (!URL.EndsWith(".html"))
            {
                return new Anime(URL);
            }
            else
            {
                return new Series(URL);
            }
        }
    }

}