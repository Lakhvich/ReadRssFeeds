using ReadRssFeeds.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.DataProvider.Abstract
{
    public interface INewsItemRepository
    {
        IQueryable<NewsItem> NewsItems { get; }
    }
}
