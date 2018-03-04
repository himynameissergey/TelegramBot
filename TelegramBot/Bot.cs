using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class Bot
    {
        private TelegramBotClient bot;
        /// <summary>
        /// Создание списка команд для бота
        /// </summary>
        private static List<ICommand> commands = new List<ICommand>();
        /// <summary>
        /// Получить список команд
        /// </summary>
        public static List<ICommand> GetCommands
        {
            get
            {
                return commands;
            }
        }
        /// <summary>
        /// Создание бот-клиента, инициализация новых команд
        /// </summary>
        public Bot()
        {
            bot = new TelegramBotClient(BotSettings.Key);
            commands.Add(new HelloCommand());
            commands.Add(new HelpCommand());
            commands.Add(new ShowCommand());
            commands.Add(new MyCommand());
            commands.Add(new BredCommand());
            //commands.Add(new iButtonsCommand());
            //commands.Add(new rButtonsCommand());
            commands.Add(new ZabbixCommand());
            commands.Add(new TalkCommand());
            commands.Add(new PowerShellCommand());
            commands.Add(new SendDocCommand());
        }
        /// <summary>
        /// Функция возвращает строку с заглавным первым символом
        /// </summary>
        //string FirstSymbolUp(string str)
        //{
        //    return str.Substring(0, 1).ToUpper() + (str.Length > 1 ? str.Substring(1) : "");
        //}
        public static void ConsoleWriteLog(Message message)
        {
            Console.WriteLine("" + DateTime.Now + " >> " + message.From.LastName + " " + message.From.FirstName + " >> " + message.Text);
        }
        public BotCommandModel Parse(string text)
        {
            //if (text.StartsWith("/"))
            //{
                var splits = text.Split(' ');
                var name = splits?.FirstOrDefault();
                var args = splits.Skip(1).Take(splits.Count()).ToArray();

                return new BotCommandModel
                {
                    Command = name,
                    Args = args,
                };
            //}
            //return null;
        }
        /// <summary>
        /// Запуск бота
        /// </summary>
        public async Task RunAsync()
        {
            int offset = 0;
            while (true)
            {
                var updates = await bot.GetUpdatesAsync(offset);

                foreach (var update in updates)
                {
                    if (update.Message != null && update.Message.Text != null)
                    {
                        var model = Parse(update.Message.Text);
                        //if (model != null)
                        //{
                            foreach (var command in commands)
                            {
                                if (update.Message.Text.ToLower().Contains(command.Name)) 
                                {
                                    if ((command.Name == "але") || (command.Name == "/showpls"))
                                    {
                                        command.Execute(update.Message, bot);
                                        break;
                                    }
                                    else if (command.CountArgs == model.Args.Length)
                                    {
                                        command.Execute(update.Message, bot);
                                        break;
                                    }
                                    else
                                    {
                                        command.OnError(update.Message, bot);
                                        break;
                                    }
                                //------------------------- без обработки параметра
                                //if (update.Message.Text != null && update.Message.Text.ToLower().Contains(command.Name))
                                //{
                                //    command.Execute(update.Message, bot);
                                //    break;
                                //}
                                //------------------------- без обработки параметра
                                //}
                            }
                        }
                        //}
                    }
                    offset = update.Id + 1;
                }
            }
        }
    }
}
