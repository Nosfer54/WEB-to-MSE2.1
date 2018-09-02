using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

//public static void gfsf()
//{
//
//}

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
        static void Main(string[] args)
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
    }
}
