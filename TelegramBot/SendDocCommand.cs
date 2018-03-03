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
                // чтение из файла
                //FileStream fstream = System.IO.File.OpenRead(@"test");
                //{
                    // преобразуем строку в байты
                    //byte[] array = new byte[fstream.Length];
                    // считываем данные
                    //fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    //string textFromFile = System.Text.Encoding.Default.GetString(array);
                    //Console.WriteLine("Текст из файла: {0}", textFromFile);
                    //FileToSend doc = new FileToSend("test", fstream);

                    //await client.SendDocumentAsync(chatId, doc);
                //}

                var FileUrl = @"excel.xlsx";
                using (var stream = System.IO.File.Open(FileUrl, FileMode.Open))
                {
                    FileToSend fts = new FileToSend();
                    fts.Content = stream;
                    fts.Filename = FileUrl.Split('\\').Last();
                    var test = await client.SendDocumentAsync(chatId, fts, "Экселич");
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
        }
    }
}
