using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.DataProvider.Entities
{
    [Table(Name = "NewsItems")]
    public class NewsItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "Title")]
        public string Title { get; set; }
        [Column(Name = "Description")]
        public string Description { get; set; }
        [Column(Name = "Url")]
        public string Url { get; set; }
        [Column(Name = "PublicDate")]
        public DateTime PublicDate { get; set; }

        [Column(Name = "ResurseRSSId")]
        public int ResurseRSSId { get; set; }
        public virtual ResurseRSS ResurseRSS { get; set; }
    }
}
