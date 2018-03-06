﻿namespace TelegramBot.ParserCore.Habra
{
    class HabraSettings : IParserSettings
    {
        public HabraSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }
        //public string BaseUrl { get; set; } = "https://habrahabr.ru";
        public string BaseUrl { get; set; } = "http://vse-shutochki.ru/anekdoty";
        //public string Prefix { get; set; } = "page{CurrentId}";
        public string Prefix { get; set; } = "{CurrentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
