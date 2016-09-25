using ReadRssFeeds.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadRssFeeds.WebUI.Models
{
    public class FeedsRSSList
    {
        public IEnumerable<ResurseRSS> feedsRss { get; set; }
        public SelectList resurseRSS { get; set; }
    }
}