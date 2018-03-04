using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class SendDocCommand : ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; set; } = "/doc";
        public int CountArgs { get; set; } = 0;
        /// <summary>
        /// Вызывает команду
        /// </summary>
        /// <param name="message">принимает сообщение</param>
        /// <param name="client">Ссылка на экземпляр бота</param>
        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            try
            {
                string FileUrl = @"excel.xlsx";
                using (var stream = System.IO.File.OpenRead(FileUrl))
                {
                    await client.SendDocumentAsync(chatId, new FileToSend(stream.Name, stream), "Экселич");
                    Bot.ConsoleWriteLog(message);
                }
            }
            catch (Exception exept)
            {
                Console.WriteLine(exept.Message);
            }
}
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Введите ""/doc"" ");
            Bot.ConsoleWriteLog(message);
        }
    }
}
