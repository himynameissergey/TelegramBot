using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class TalkCommand : ICommand
    {
        public string Name { get; set; } = "але";
        public int CountArgs { get; set; } = 0;
        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            DateTime now = DateTime.Now;
            string phrase = Generate(1);
            await client.SendTextMessageAsync(chatId, phrase);
            Bot.ConsoleWriteLog(message);
        }
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Неверное количество аргументов!");
            Bot.ConsoleWriteLog(message);
        }
        public string Generate(int numMessages)
        {
            Random rnd = new Random();
                int n = rnd.Next(s.Length);
            return s[n];
        }

        public string[] s = {
            "Ну привет",
            "Я с тобой не разговариваю",
            "Нет, я не обиделся",
            "Да пошел ты",
            "Отвали",
            "Что случилось?",
            "Ублюдок, мать твою, а ну иди сюда, говно собачье",
            "Я занят",
            "Займись делом",
            "Мне сейчас типа некогда",
            "Я слушаю",
            "Внимательно",
            "Че надо?",
            "Отстань, старуха, я в печали",
            "Как же вы меня достали!",
            "Не надоело еще?",
            "Как же тяжело работать с кретинами!",
            "Ты спятил?",
            "Я твой дом труба шатал",
            "Я устал с тобой общаться"
        };
    }

}

