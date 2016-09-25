using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadRssFeeds.Domain.Entities;

namespace ReadRssFeeds.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbContext") { }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<ResurseRSS> ResurseRSSs { get; set; }
    }
}
