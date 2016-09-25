﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadRssFeeds.Domain.Entities;

namespace ReadRssFeeds.Domain.Abstract
{
    public interface IResurseRSSRepository
    {
        IQueryable<ResurseRSS> ResurseRSSs { get; }
    }
}