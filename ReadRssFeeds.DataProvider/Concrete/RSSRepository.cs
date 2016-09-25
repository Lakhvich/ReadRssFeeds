using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadRssFeeds.DataProvider.Abstract;
using ReadRssFeeds.DataProvider.Entities;
using System.Data.Linq;

namespace ReadRssFeeds.DataProvider.Concrete
{
    public class RSSRepository : IResurseRSSRepository
    {
        private EFDbContext db = new EFDbContext(Properties.Settings.Default.EFDbContextConnectionString);

        public List<ResurseRSS> GetResourceRSSAndRelationshipItems()
        {
            var query = (from rss in db.ResurseRSSes
                         from item in db.NewsItems
                         where item.ResurseRSSId == rss.Id
                         select new
                         {
                             Id = rss.Id,
                             Name = rss.Name,
                             UrlRss = rss.Url,
                             NewsItems = rss.NewsItems,
                             Title = item.Title,
                             Description = item.Description,
                             PublicDate = item.PublicDate,
                             UrlArticle = item.Url,
                             ResurseRSSId = item.ResurseRSSId
                         });

            var queryGroup = from rss in query
                             group rss by rss.Id into newGroup
                             orderby newGroup.Key
                             select newGroup;

            List<ResurseRSS> RssFeeds = new List<ResurseRSS>();
            foreach (var nameGroup in queryGroup)
            {
                var items = (from el in nameGroup
                             select new NewsItem
                             {
                                 Title = el.Title,
                                 PublicDate = el.PublicDate,
                                 Url = el.UrlArticle,
                                 Description = el.Description
                             }).ToList();

                var rss = new ResurseRSS
                {
                    Id = nameGroup.First().Id,
                    Name = nameGroup.First().Name,
                    Url = nameGroup.First().UrlRss,
                    NewsItems = items
                };
                RssFeeds.Add(rss);
            }
            return RssFeeds;
        }

        public List<ResurseRSS> GetResourceRSS
        {
            get
            {
                var query = from rss in db.ResurseRSSes
                            select new
                            {
                                Id = rss.Id,
                                Name = rss.Name,
                                Url = rss.Url
                            };

                return query.AsEnumerable().Select(a => new ResurseRSS
                {
                    Id = a.Id,
                    Name = a.Name,
                    Url = a.Url
                }).ToList();
            }
        }

        public List<ResurseRSS> GetResourceRSSAndRelationshipItemsById(int? rssId)
        {
            var quew = from rss in db.ResurseRSSes
                       from item in db.NewsItems
                       where item.ResurseRSSId == rssId && rss.Id == rssId
                       select new
                       {
                           Id = rss.Id,
                           Name = rss.Name,
                           UrlRss = rss.Url,
                           NewsItems = rss.NewsItems,
                           Title = item.Title,
                           Description = item.Description,
                           PublicDate = item.PublicDate,
                           UrlArticle = item.Url,
                           ResurseRSSId = item.ResurseRSSId
                       };

            if (quew.FirstOrDefault() != null)
            {
                var items = (from el in quew.AsEnumerable()
                             select new NewsItem
                             {
                                 Title = el.Title,
                                 PublicDate = el.PublicDate,
                                 Url = el.UrlArticle,
                                 Description = el.Description
                             }).ToList();

                return (new List<ResurseRSS>
                {
                    new ResurseRSS
                    {
                        Id = quew.First().Id,
                        Name = quew.First().Name,
                        Url = quew.First().UrlRss,
                        NewsItems = items
                    }
                });
            }
            return null;
        }

        public List<ResurseRSS> GetResourceRSSAndRelationshipItemsByIdOrderByDes(int? rssId)
        {
            var quew = from rss in db.ResurseRSSes
                       from item in db.NewsItems
                       where item.ResurseRSSId == rssId && rss.Id == rssId
                       orderby item.PublicDate descending
                       select new
                       {
                           Id = rss.Id,
                           Name = rss.Name,
                           UrlRss = rss.Url,
                           NewsItems = rss.NewsItems,
                           Title = item.Title,
                           Description = item.Description,
                           PublicDate = item.PublicDate,
                           UrlArticle = item.Url,
                           ResurseRSSId = item.ResurseRSSId
                       };

            if (quew.FirstOrDefault() != null)
            {
                var items = (from el in quew.AsEnumerable()
                             select new NewsItem
                             {
                                 Title = el.Title,
                                 PublicDate = el.PublicDate,
                                 Url = el.UrlArticle,
                                 Description = el.Description
                             }).ToList();

                return (new List<ResurseRSS>
                {
                    new ResurseRSS
                    {
                        Id = quew.First().Id,
                        Name = quew.First().Name,
                        Url = quew.First().UrlRss,
                        NewsItems = items
                    }
                });
            }
            return null;
        }

        public int InsertNewsItems(List<NewsItem> newItemsList)
        {
            int countUpdate = 0;
            var result = from item in db.NewsItems
                         select item;
            try
            {
                foreach (var r in newItemsList)
                    if (
                        !result.Any(
                            f =>
                                String.Compare(r.Title, f.Title) == 0 &&
                                DateTime.Compare(r.PublicDate, f.PublicDate) == 0))
                    {
                        db.NewsItems.InsertOnSubmit(r);
                        countUpdate++;
                    }

                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return countUpdate;
        }
    }
}
