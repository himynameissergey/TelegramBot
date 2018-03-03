using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class BredCommand : ICommand
    {
        public string Name { get; set; } = "/bred";
        public int CountArgs { get; set; } = 1;
        public async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            DateTime now = DateTime.Now;
            BredCommand bm = new BredCommand();

            if (message.Text.Length > 6)
            {
                string resp = message.Text.Substring(6);
                //string resp = model.Args.FirstOrDefault();
                if (resp.Length == 0) { this.NumMessages = 0; }
                try
                {
                    int nm = Convert.ToInt32(resp);
                    //if (nm == 0) { this.NumMessages = 0;  }
                    if (nm <= 0 | nm > 10)
                    {
                        await client.SendTextMessageAsync(chatId, "Число должно быть от 1 до 10", replyToMessageId: messageId);
                        return;
                    }
                    this.NumMessages = nm;
                }
                catch (Exception ex)
                {
                    await client.SendTextMessageAsync(chatId, "Неверный формат числа", replyToMessageId: messageId);
                    return;
                }

                string s = bm.Generate(this.NumMessages);
                await client.SendTextMessageAsync(chatId, s);//, replyToMessageId: messageId);
                Bot.ConsoleWriteLog(message);
            }
        }
        public async void OnError(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, @"Введите ""/bred"" и через пробел количество дебильных фраз");
            Bot.ConsoleWriteLog(message);
        }
        public int NumMessages = 0;
        public string Generate(int numMessages)
        {
            string outStr = "";
            Random rnd = new Random();
            for (int i = 0; i < numMessages; i++)
            {
                int n1 = rnd.Next(s1.Length);
                int n2 = rnd.Next(s2.Length);
                int n3 = rnd.Next(s3.Length);
                int n4 = rnd.Next(s4.Length);
                int n5 = rnd.Next(s5.Length);
                outStr = outStr + s1[n1] + s2[n2] + s3[n3] + s4[n4] + s5[n5];
            }
            return outStr;
        }

        public string[] s1 = {
            "Равным образом ",
            "Задача организации, в особенности же укрепление и развитие структуры ",
            "Не следует, однако забывать, что ",
            "Идейные соображения высшего порядка, а также ",
            "Товарищи! ",
            "Значимость этих проблем настолько очевидна, что ",
            "Таким образом, ",
            "Не следует, однако забывать, что ",
            "Задача организации, в особенности же укрепление и развитие структуры "
        };
        public string[] s2 = {
            "постоянное информационно-пропагандистское обеспечение нашей деятельности ",
            "дальнейшее развитие различных форм деятельности ",
            "реализация намеченных плановых заданий ",
            "консультация с широким активом ",
            "постоянный количественный рост и сфера нашей активности "
        };
        public string[] s3 = {
            "позволяет выполнять важные задания по ",
            "в значительной степени обуславливает ",
            "требуют от нас ",
            "позволяет оценить ",
            "обеспечивает широкому кругу (специалистов) ",
            "способствует ",
            "требуют ",
        };
        public string[] s4 = {
            "разработке ",
            "создание ",
            "анализа ",
            "значение ",
            "участие в ",
            "подготовке и реализации ",
            "определения и уточнения "
        };
        public string[] s5 = {
            "форм развития. \n",
            "дальнейших направлений развития. \n",
            "существенных финансовых и административных условий. \n",
            "формировании направлений прогрессивного развития. \n",
            "соответствующих условий активизации. \n",
            "позиций, занимаемых участниками. \n",
        };
    }
}




