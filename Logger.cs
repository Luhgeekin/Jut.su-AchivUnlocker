using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchivUnlocker
{
    internal static class Logger
    {
        public static void Log(Achiv achiv) 
        {
            Console.Write($"achiv title: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{Truncate(achiv.title, 25),-30}");
            Console.ResetColor();
            Console.Write("achiv status: ");

            if (achiv.isUnlocked)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("unlocked\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("error\n");
            }

            Console.ResetColor();
        }

        private static string Truncate(string str, int maxLength)
        {
            if (str.Length > maxLength)
            {
                return str.Substring(0, maxLength - 3) + "...";
            }

            return str;
        }
    }
}
