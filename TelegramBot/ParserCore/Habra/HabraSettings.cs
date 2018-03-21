namespace TelegramBot.ParserCore.Habra
{
    class HabraSettings : IParserSettings
    {
        public HabraSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }
        //public string BaseUrl { get; set; } = "https://habrahabr.ru";
        //public string BaseUrl { get; set; } = "https://nekdo.ru";
        //public string BaseUrl { get; set; } = "https://2ch.hk/news/";
        //public string BaseUrl { get; set; } = "https://m.lenta.ru/";
        public string BaseUrl { get; set; } = "http://stavklass.ru/images";
        //public string Prefix { get; set; } = "page{CurrentId}";   //habr
        //public string Prefix { get; set; } = "page/{CurrentId}";  //nekdo
        public string Prefix { get; set; } = "";    //2ch, lenta.ru, pikabu, stavklass
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
