using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

namespace WEB_to_MSE2._1
{
    public class Stats
    {
        public string   Power,
                        Loyalty,
                        Toughness;
    }

    public class Img
    {
        public string   Http,
                        Local;
    }
    
    public class Card
    {
        public string   Name,
                        Cost,
                        Type,
                        Rarity,
                        TextOracle,
                        TextFlavor,
                        Illustrator,
                        NumberOfSet;
        public Stats    stats;
        public Img      Img;

        public void Print()
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Стоимость: " + Cost);
            Console.WriteLine("Ссылка на изображение: " + Img.Http);
            Console.WriteLine("Тип: " + Type);
            Console.WriteLine("Редкость: " + Rarity);
            Console.WriteLine("Oracle текст: " + TextOracle);
            Console.WriteLine("Художественный текст: " + TextFlavor);
            Console.WriteLine("Сила: " + stats.Power);
            Console.WriteLine("Прочность: " + stats.Toughness);
            Console.WriteLine("Лояльность: " + stats.Loyalty);
            Console.WriteLine("Художник: " + Illustrator);
            Console.WriteLine("Номер: " + NumberOfSet);
            Console.WriteLine("");
        }
    }

    class Program
    {
        public static string Name(HtmlDocument htmlDocument)
        {
            string Name;
            Name = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='card-text-title']").InnerHtml.Trim();
            return Regex.Split(Name, "<span")[0].Trim();
        }

        public static string Cost(HtmlDocument htmlDocument)
        {
            string Cost = "";

            if (htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='card-text-title']/span[@class='card-text-mana-cost']") != null)
            {
                Cost = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='card-text-title']/span[@class='card-text-mana-cost']").InnerText.Trim();
                Cost = Cost.Replace("{", null);
                Cost = Cost.Replace("}", null);
            }
            else
            {
                Cost = null;
            }
            
            return Cost;
        }
        
        public static string Type (HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectSingleNode("//p[@class='card-text-type-line']").InnerText.Trim();
        }

        public static string TextOracle(HtmlDocument htmlDocument)
        {
            string Text = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-oracle']").InnerText.Trim();
            Text = Text.Replace("{", "<sym>");
            Text = Text.Replace("}", "</sym>");
            return Text;
        }

        public static string TextFlavor(HtmlDocument htmlDocument)
        {
            if (htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-flavor']") != null)
            {
                return htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-flavor']").InnerText.Trim();
            } else
            {
                return null;
            }
            
        }

        public static Stats Stats(HtmlDocument htmlDocument)
        {
            var Stats = new Stats();
            Stats.Power = null;
            Stats.Toughness = null;
            Stats.Loyalty = null;

            if (htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-stats']") != null)
            {
                if (htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-stats']").InnerText.IndexOf("Loyalty") > -1)
                {
                    Stats.Loyalty = Regex.Split(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-stats']").InnerText, ":")[1].Trim();
                } else
                {
                    Stats.Power = Regex.Split(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-stats']").InnerText, "/")[0].Trim();
                    Stats.Toughness = Regex.Split(htmlDocument.DocumentNode.SelectSingleNode("//div[@class='card-text-stats']").InnerText, "/")[1].Trim();
                }
            };
            return Stats;
        }

        public static string Illustrator(HtmlDocument htmlDocument)
        {
            if (htmlDocument.DocumentNode.SelectSingleNode("//p[@class='card-text-artist']") != null)
            {
                return htmlDocument.DocumentNode.SelectSingleNode("//p[@class='card-text-artist']/a").InnerText.Trim();
            }
            else
            {
                return null;
            }
        }

        public static string NumberOfSet(HtmlDocument htmlDocument)
        {
            if (htmlDocument.DocumentNode.SelectSingleNode("//span[@class='prints-current-set-details']") != null)
            {
                return Regex.Split(htmlDocument.DocumentNode.SelectSingleNode("//span[@class='prints-current-set-details']").InnerText, "·")[0].Replace("#", "").Trim();
            }
            else
            {
                return null;
            }
        }

        public static string Rarity(HtmlDocument htmlDocument)
        {
            if (htmlDocument.DocumentNode.SelectSingleNode("//span[@class='prints-current-set-details']") != null)
            {
                return Regex.Split(htmlDocument.DocumentNode.SelectSingleNode("//span[@class='prints-current-set-details']").InnerText, "·")[1].Trim();
            }
            else
            {
                return null;
            }
        }

        public static Img Img(HtmlDocument htmlDocument)
        {
            var Img = new Img();
            var WebClient = new WebClient();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(WebClient.DownloadString(htmlDocument.DocumentNode.SelectSingleNode("//a[@title='Show version in English']").GetAttributeValue("href", "google.com")));
            Img.Http = document.DocumentNode.SelectNodes("//a[@class='button-n']")[0].GetAttributeValue("href", "google.com");
            return Img;
        }

        public static Card GetRuCard(string href)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            htmlDocument.LoadHtml(webClient.DownloadString(href));
            var Cards = new Card();

            Cards.Name          = Name(htmlDocument);
            Cards.Cost          = Cost(htmlDocument); 
            Cards.Img           = Img(htmlDocument); 
            Cards.Type          = Type(htmlDocument);
            Cards.Rarity        = Rarity(htmlDocument);
            Cards.TextOracle    = TextOracle(htmlDocument);
            Cards.TextFlavor    = TextFlavor(htmlDocument);
            Cards.stats         = Stats(htmlDocument);
            Cards.Illustrator   = Illustrator(htmlDocument);
            Cards.NumberOfSet   = NumberOfSet(htmlDocument);

            return Cards;
        }

        static void Main(string[] args)
        {
            GetRuCard("https://scryfall.com/card/hou/56/ru/%D0%BF%D1%80%D0%BE%D0%BA%D0%BB%D1%8F%D1%82%D0%B0%D1%8F-%D0%BE%D1%80%D0%B4%D0%B0").Print();
            GetRuCard("https://scryfall.com/card/hou/1/ru/%D0%B3%D0%B5%D1%80%D0%BE%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9-%D0%BF%D0%BE%D0%B4%D0%B2%D0%B8%D0%B3").Print();
            GetRuCard("https://scryfall.com/card/hou/165/ru/%D0%B7%D0%B5%D1%80%D0%BA%D0%B0%D0%BB%D0%BE-%D0%BC%D0%B8%D1%80%D0%B0%D0%B6%D0%B5%D0%B9").Print();
            GetRuCard("https://scryfall.com/card/hou/195/ru/%D0%B1%D0%BE%D0%BB%D0%BE%D1%82%D0%BE").Print();
            GetRuCard("https://scryfall.com/card/hou/144/ru/%D1%81%D0%B0%D0%BC%D1%83%D1%82-%D0%B8%D1%81%D0%BF%D1%8B%D1%82%D0%B0%D0%BD%D0%BD%D0%B0%D1%8F").Print();
        }
    }
}
