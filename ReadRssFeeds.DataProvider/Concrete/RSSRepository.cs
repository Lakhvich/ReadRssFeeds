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

        public List<NewsItem> GetItemsAll()
        {
            var query = from item in db.NewsItems
                        from rss in db.ResurseRSSes
                        where item.ResurseRSSId == rss.Id
                        select new { item, rss };

            return query.AsEnumerable().Select(a => new NewsItem
            {
                Id = a.item.Id,
                Title = a.item.Title,
                PublicDate = a.item.PublicDate,
                Url = a.item.Url,
                Description = a.item.Description,
                ResurseRSSId = a.item.ResurseRSSId,
                ResurseRSS = new ResurseRSS
                {
                    Id = a.rss.Id,
                    Name = a.rss.Name,
                    Url = a.rss.Url
                }
            }).ToList();
        }

        public List<ResurseRSS> GetResourceRSS
        {
            get
            {
                var query = from rss in db.ResurseRSSes
                            select rss;

                return query.AsEnumerable().Select(a => new ResurseRSS
                {
                    Id = a.Id,
                    Name = a.Name,
                    Url = a.Url,
                    NewsItems = a.NewsItems
                }).ToList();
            }
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
