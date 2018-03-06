using AngleSharp.Dom.Html;

namespace TelegramBot.ParserCore
{
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}

