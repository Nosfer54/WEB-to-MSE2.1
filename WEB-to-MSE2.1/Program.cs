using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace WEB_to_MSE2._1
{
    public class Card
    {
        public string id, href, Name, Cost, Img, Type, NumberOfSet, Stats, Rarity, NameENG, stylesheet, FileImg, Color,
                        RuleText, FlavorText, loyalty, Illustrator, Power, Toughness, TextBoxes, NumberCoordinates, DividerCoordinates, SpecialText;
        public Int32 Number;
        public List<string> LoyaltyCost = new List<string>();
        public List<string> LevelText = new List<string>();
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

        public static Card GetRuCard(string href)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            var aa = new WebClient();
            aa.Encoding = Encoding.UTF8;
            htmlDocument.LoadHtml(aa.DownloadString(href));
            var Cards = new Card();

            Cards.Name = htmlDocument.DocumentNode.SelectSingleNode("//h1[@class='card-text-title']").InnerText;
            Cards.id = "dfsdfsfsdfsdfsd";

            return Cards;
        }

        static void Main(string[] args)
        {
            //Console.WriteLine(GetRuCard("https://scryfall.com/card/hou/1/ru/%D0%B3%D0%B5%D1%80%D0%BE%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9-%D0%BF%D0%BE%D0%B4%D0%B2%D0%B8%D0%B3"));
            Console.WriteLine(GetRuCard("https://scryfall.com/card/hou/1/ru/%D0%B3%D0%B5%D1%80%D0%BE%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D0%B9-%D0%BF%D0%BE%D0%B4%D0%B2%D0%B8%D0%B3").Name);
        }
    }
}
