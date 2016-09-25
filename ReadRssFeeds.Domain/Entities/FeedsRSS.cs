﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRssFeeds.Domain.Entities
{
    public class FeedsRSS
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public ICollection<NewsItem> NewsItems { get; set; }
    }
}