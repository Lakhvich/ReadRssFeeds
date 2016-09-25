using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using System.Xml;
using System.Net;
using ReadRssFeeds.DataProvider.Abstract;
using ReadRssFeeds.DataProvider.Entities;

namespace ReadRssFeeds.ConsoleControl
{
    public class RssReader
    {
        private IResurseRSSRepository repository;

        public RssReader(IResurseRSSRepository itemRepository)
        {
            this.repository = itemRepository;
        }

        public List<ResurseRSS> ReadRssFeeds()
        {
            List<ResurseRSS> items = new List<ResurseRSS>();
            try
            {
                foreach (var rss in repository.GetResourceRSS)
                {
                    var doc = System.Xml.Linq.XDocument.Load(rss.Url);
                    XElement chanel = doc.Elements("rss").Elements("channel").FirstOrDefault();
                    if (chanel != null)
                    {
                        var newsItem = (from el in chanel.Elements("item")
                                        select new NewsItem
                                        {
                                            Title = el.Element("title").Value,
                                            PublicDate = ConversionsToDateTime(el.Element("pubDate").Value),
                                            Url = el.Element("link").Value,
                                            Description = el.Element("description").Value,
                                            ResurseRSSId = rss.Id
                                        }).ToList();

                        var rssFedds = new ResurseRSS
                        {
                            Id = rss.Id,
                            Name = rss.Name,
                            Url = rss.Url,
                            NewsItems = newsItem
                        };
                        items.Add(rssFedds);
                    }
                    else
                    {
                        throw new InvalidOperationException("Ошибка в XML. Описание канала не найдено!");
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception)
            {
                throw new Exception("Невозможно соединиться с указаным источником.\r\n");
            }
            return items;
        }

        public void SaveRssFeeds(List<ResurseRSS> rssFeedsList)
        {
            string status = "";
            foreach (var items in rssFeedsList)
            {
                int countUpdate = repository.InsertNewsItems(items.NewsItems.ToList());
                status += String.Format("{0} : {1} прочитано : {2} обновлено\n", items.Name, items.NewsItems.Count, countUpdate);
            }
            Console.WriteLine(status);
        }

        public void PrintToConsole(List<ResurseRSS> rss)
        {
            string status = "";
            if (rss.FirstOrDefault() != null)
                foreach (var r in rss)
                {
                    foreach (var item in r.NewsItems)
                        Console.WriteLine("{0}\n{1}\n{2}\n{3}\n", r.Name, item.Title, item.Description, item.PublicDate);
                    status += String.Format("{0} : {1} прочитано\n", r.Name, r.NewsItems.Count);
                }
            Console.WriteLine(status);
        }

        private static DateTime ConversionsToDateTime(string dateOfset)
        {
            return DateTimeOffset.ParseExact(ConvertZoneToLocalDifferential(dateOfset),
                "ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture).UtcDateTime;
        }

        private static string ConvertZoneToLocalDifferential(string pubDate)
        {
            string zoneLocalDifferential = String.Empty;
            if (pubDate.EndsWith(" GMT", StringComparison.OrdinalIgnoreCase))
                zoneLocalDifferential = String.Concat(pubDate.Substring(0, (pubDate.LastIndexOf(" GMT") + 1)), "+00:00");
            else
                zoneLocalDifferential = pubDate;
            return zoneLocalDifferential;
        }
    }
}