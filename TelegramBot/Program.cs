using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.ParserCore;
using TelegramBot.ParserCore.Habra;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            Console.WindowHeight = 10;
            Console.WindowWidth = 100;
            Console.Title = "Бот Алеша для Telegram";
            Console.WriteLine(DateTime.Now+" Бот Алеша запущен");
            #region For parsing
            ParserWorker<string[]> parser;
            parser = new ParserWorker<string[]>(new HabraParser());
            parser.OnCompleted += ParseCommand.Parser_OnCompleted;
            parser.OnNewData += ParseCommand.Parser_OnNewData;
            parser.Settings = new HabraSettings(1, 5);  // первая страница сайта
            parser.Start();
            #endregion
            bot.RunAsync().Wait();
            Console.ReadKey();
        }
    }
}
