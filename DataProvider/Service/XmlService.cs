using System;
using System.Collections.Generic;
using System.Xml;
using DataProvider.Model;
using DataProvider.ServiceAbstraction;
using Microsoft.Extensions.Options;

namespace DataProvider.Service
{
    public class XmlService : IXmlService
    {
        private readonly string _xmlDataFilePath;
        public XmlService(IOptions<AppSettings> appsettings)
        {
            _xmlDataFilePath = appsettings.Value.XmlDataFile;
        }
        public List<Horse> GetHorsesFromXmlService()
        {
            Dictionary<string, string> horseDict = new Dictionary<string, string>();
            List<Horse> horseList = new List<Horse>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_xmlDataFilePath);

                var rootNode = doc.DocumentElement;
                var horseNodes = rootNode.SelectNodes("races/race/horses/horse");

                if (horseNodes.Count == 0) return null;

                foreach (XmlNode horse in horseNodes)
                {
                    var horseName = horse.Attributes["name"].InnerText;
                    var horseNumber = horse.SelectSingleNode("number").InnerText;
                    horseDict.Add(horseNumber, horseName);
                }

                var priceNodes = rootNode.SelectNodes("races/race/prices/price/horses/horse");

                if (priceNodes.Count == 0) return null;
                foreach (XmlNode horseWithPrice in priceNodes)
                {
                    var horseNumber = horseWithPrice.Attributes["number"].InnerText;
                    var horsePrice = horseWithPrice.Attributes["Price"].InnerText;
                    var horse = new Horse
                    {
                        Name = horseDict[horseNumber],
                        Price = Convert.ToDouble(horsePrice)
                    };
                    horseList.Add(horse);
                }
                return horseList;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error retreiving data from XmlService: {e}");
                throw;
            }
        }
    }
}
