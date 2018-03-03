using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class rButtonsCommand : ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; set; } = "/rbuttons";
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

            client.OnUpdate += async (object sender, UpdateEventArgs e) =>
            {
                if (e.Update.CallbackQuery != null || e.Update.InlineQuery != null) return; // в этом блоке нам келлбэки и инлайны не нужны
                var update = e.Update;
                var upd_message = update.Message;
                if (upd_message == null) return;
                if (upd_message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
                {
                    //reply кнопки
                    var keyboard = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                            new[] // row 1
                            {
                                new Telegram.Bot.Types.KeyboardButton("Чпоньк"),
                                new Telegram.Bot.Types.KeyboardButton("Жамк"),
                            },
                         },
                        ResizeKeyboard = true
                    };
                    await client.SendTextMessageAsync(upd_message.Chat.Id, "Жамкни внизу!", replyMarkup: keyboard);

                    // обработка reply кнопок
                    if (upd_message.Text == "Чпоньк")
                    {
                        await client.SendTextMessageAsync(upd_message.Chat.Id, "Ты нажал на первую кнопку!", replyToMessageId: upd_message.MessageId);
                    }
                    if (upd_message.Text == "Жамк")
                    {
                        await client.SendTextMessageAsync(upd_message.Chat.Id, "Ты нажал на вторую кнопку!", replyToMessageId: upd_message.MessageId);
                    }
                }
            };
            client.StartReceiving();
        }
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Введите ""/rbuttons"" ");
        }
    }
}
