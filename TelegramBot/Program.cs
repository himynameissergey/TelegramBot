using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Console.WindowHeight = 10;
            Console.WindowWidth = 100;
            Console.Title = "Бот для Telegram";
            Console.WriteLine(DateTime.Now+" Бот запущен");
            bot.RunAsync().Wait();
            Console.ReadKey();
        }
    }
}
