using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.DataProvider.Entities
{
    [Table(Name = "ResurseRSSes")]
    public class ResurseRSS
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "Url")]
        public string Url { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }

        public virtual ICollection<NewsItem> NewsItems { get; set; }
    }
}
