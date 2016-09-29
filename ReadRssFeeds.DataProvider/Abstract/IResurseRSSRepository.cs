using ReadRssFeeds.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.DataProvider.Abstract
{
    public interface IResurseRSSRepository
    {
        List<ResurseRSS> GetResourceRSS { get; }
        List<NewsItem> GetItemsAll();
        int InsertNewsItems(List<NewsItem> newItemsList);
    }
}
