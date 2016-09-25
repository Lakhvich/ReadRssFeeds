using ReadRssFeeds.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.DataProvider.Concrete
{
    public partial class EFDbContext : DataContext
    {
        public Table<NewsItem> NewsItems;
        public Table<ResurseRSS> ResurseRSSes;
        public EFDbContext(string connection) : base(connection) {}
    }
}
