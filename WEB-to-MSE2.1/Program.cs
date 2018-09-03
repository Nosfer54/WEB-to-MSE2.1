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
    public class Card
    {
        public string   Name,
                        Cost,
                        Type,
                        TextOracle,
                        TextFlavor,

            id, href,   Img,  NumberOfSet, Stats, Rarity, NameENG, stylesheet, FileImg, Color,
                          loyalty, Illustrator, Power, Toughness, TextBoxes, NumberCoordinates, DividerCoordinates, SpecialText;
        public Int32 Number;
        public List<string> LoyaltyCost = new List<string>();
        public List<string> LevelText = new List<string>();

        public void Print()
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Стоимость: " + Cost);
            Console.WriteLine("Тип: " + Type);
            Console.WriteLine("Oracle текст: " + TextOracle);
            Console.WriteLine("Художественный текст: " + TextFlavor);
            Console.WriteLine("");
        }
    }

    class Program
    {
        public static void Dadada()
        {
            int SetCount = 0;
            string SetCode = "HOU";
            string SetLocation = "D:/sets/" + SetCode + "/set";
            List<Card> Cards = new List<Card>();

            WebClient MTG = new WebClient();
            HtmlDocument scryfall = new HtmlDocument();

            scryfall.LoadHtml(MTG.DownloadString("https://scryfall.com/sets/hou?order=set"));


            SetCount = scryfall.DocumentNode.SelectNodes("//a[@class='card-grid-item']").Count;

            for (var i = 0; i < SetCount; i++)
            {
                Cards.Add(new Card());
                Cards[i].href = scryfall.DocumentNode.SelectNodes("//a[@class='card-grid-item']")[i].GetAttributeValue("href", "");

                HtmlDocument cards = new HtmlDocument();
                cards.LoadHtml(MTG.DownloadString(Cards[i].href));
                string aaa = cards.DocumentNode.SelectSingleNode("//div[@class='print-langs']/a[@title='Show version in Russian']").GetAttributeValue("href", "");
                //Console.WriteLine(cards.DocumentNode.SelectSingleNode("//div[@class='print-langs']/a[@title='Show version in Russian']").InnerHtml);

                HtmlDocument CardRu = new HtmlDocument();
                CardRu.LoadHtml(MTG.DownloadString(aaa));
                //string bbb = 


                //HtmlDocument SingleCardRu = new HtmlDocument();
                //SingleCardRu.LoadHtml(CardRu.LoadHtml(cards.DocumentNode.SelectSingleNode("//div[@class='print-langs']/a[@title='Show version in Russian']").GetAttributeValue("href", "")));

                Console.WriteLine(CardRu.Text);

                //Cards[i].id = scryfall.DocumentNode.SelectNodes("//a[@class='card-grid-item']")[i].GetAttributeValue("data-id","");
                //

                //

                //
                //Cards[i].Img = (cards.DocumentNode.SelectNodes("//a[@class='button-n']")[13].GetAttributeValue("href", ""));

                //Console.WriteLine(cards.DocumentNode.SelectNodes("//a[@class='button-n']")[13].GetAttributeValue("href", ""));

                //Console.WriteLine(Cards[i].id);
                //Console.WriteLine(Cards[i].href);

                Console.ReadLine();
            }

            //Console.WriteLine(scryfall.DocumentNode.SelectNodes("//a[@class='card-grid-item']").Count);

            //            Console.WriteLine(scryfall.Text);

        }

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

        public static Card GetRuCard(string href)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            htmlDocument.LoadHtml(webClient.DownloadString(href));
            var Cards = new Card();

            Cards.Name          = Name(htmlDocument);
            Cards.Cost          = Cost(htmlDocument); 
            Cards.Type          = Type(htmlDocument);
            Cards.TextOracle    = TextOracle(htmlDocument);
            Cards.TextFlavor    = TextFlavor(htmlDocument);

            return Cards;
        }

        static void Main(string[] args)
        {
            GetRuCard("https://scryfall.com/card/hou/56/ru/%D0%BF%D1%80%D0%BE%D0%BA%D0%BB%D1%8F%D1%82%D0%B0%D1%8F-%D0%BE%D1%80%D0%B4%D0%B0").Print();
            GetRuCard("https://scryfall.com/card/hou/1/ru/%D0%B3%D0%B5%D1%80%D0%BE%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9-%D0%BF%D0%BE%D0%B4%D0%B2%D0%B8%D0%B3").Print();
            GetRuCard("https://scryfall.com/card/hou/165/ru/%D0%B7%D0%B5%D1%80%D0%BA%D0%B0%D0%BB%D0%BE-%D0%BC%D0%B8%D1%80%D0%B0%D0%B6%D0%B5%D0%B9").Print();
            GetRuCard("https://scryfall.com/card/hou/195/ru/%D0%B1%D0%BE%D0%BB%D0%BE%D1%82%D0%BE").Print();
        }
    }
}
