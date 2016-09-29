using ReadRssFeeds.DataProvider.Abstract;
using ReadRssFeeds.DataProvider.Entities;
using ReadRssFeeds.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadRssFeeds.WebUI.Controllers
{
    public class ResurseRSSController : Controller
    {
        private IResurseRSSRepository repository;

        public ResurseRSSController(IResurseRSSRepository itemRepository)
        {
            this.repository = itemRepository;
        }

        public ActionResult List(int? id, string sort)
        {
            List<NewsItem> feedRssList = repository.GetItemsAll().OrderByDescending(w => w.PublicDate).ToList();

            if (id != null && id != 0)
                feedRssList = feedRssList.Where(w => w.ResurseRSSId == id).ToList();

            if (!String.IsNullOrEmpty(sort) && sort.Equals("resurse"))
                feedRssList = feedRssList.OrderByDescending(w => w.ResurseRSSId).ToList();

            List<ResurseRSS> res = repository.GetResourceRSS;
            res.Insert(0, new ResurseRSS { Name = "Все", Id = 0 });

            FeedsRSSList feeds = new FeedsRSSList
            {
                feedsRss = feedRssList,
                resurseRSS = new SelectList(res, "Id", "Name")
            };
            return View(feeds);
        }

        public ActionResult List2(int? id, string sort = "resurse")
        {
            List<NewsItem> feedRssList = repository.GetItemsAll().OrderByDescending(w => w.PublicDate).ToList();

            if (id != null && id != 0)
                feedRssList = feedRssList.Where(w => w.ResurseRSSId == id).ToList();

            if (!String.IsNullOrEmpty(sort) && sort.Equals("resurse"))
                feedRssList = feedRssList.OrderByDescending(w => w.ResurseRSSId).ToList();

            if (Request.IsAjaxRequest())
                return PartialView("_item", feedRssList);

            List<ResurseRSS> res = repository.GetResourceRSS;
            res.Insert(0, new ResurseRSS { Name = "Все", Id = 0 });

            FeedsRSSList feeds = new FeedsRSSList
            {
                feedsRss = feedRssList,
                resurseRSS = new SelectList(res, "Id", "Name")
            };
            return View(feeds);
        }
    }
}