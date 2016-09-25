using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.Domain.Entities
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime PublicDate { get; set; }

        public int ResurseRSSId { get; set; }
        public virtual ResurseRSS ResurseRSS { get; set; }
    }
}
