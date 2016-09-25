using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadRssFeeds.Domain.Abstract;
using ReadRssFeeds.Domain.Entities;

namespace ReadRssFeeds.Domain.Concrete
{
    public class EFNewsItemRepository : INewsItemRepository
    {
        private EFDbContext context = new EFDbContext();
        public IQueryable<NewsItem> NewsItems
        {
            get { return context.NewsItems; }
        }
    }
}
