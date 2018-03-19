using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;

namespace TelegramBot.ParserCore.Habra
{
    class HabraParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            //var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));  /habr
            //var items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("text"));  //nekdo
            //var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("orange")).OfType<IHtmlAnchorElement>();   //2ch
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("b-list-item__link")).OfType<IHtmlAnchorElement>(); //lenta.ru

            //AngleSharp example
            //var document = parser.Parse("<ul><li>First item<li>Second item<li class='blue'>Third item!<li class='blue red'>Last item!</ul>");
            ////Do something with LINQ
            //var blueListItemsLinq = document.All.Where(m => m.LocalName == "li" && m.ClassList.Contains("blue"));

            foreach (var item in items)
            {
                //list.Add(item.TextContent); //habr, nekdo
                //list.Add("https://2ch.hk" + item.PathName);   //2ch
                list.Add("https://m.lenta.ru" + item.PathName);	//lenta.ru
            }
            return list.ToArray();
        }
    }
}

