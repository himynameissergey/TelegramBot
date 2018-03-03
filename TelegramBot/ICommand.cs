using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    interface ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        string Name { get; set; }
        int CountArgs { get; set; }

        /// <summary>
        /// Вызывает команду
        /// </summary>
        /// <param name="message">принимает сообщение</param>
        /// <param name="client">Ссылка на экземпляр бота</param>
        void Execute(Message message, TelegramBotClient client);
        void OnError(Message message, TelegramBotClient client);
    }
}
