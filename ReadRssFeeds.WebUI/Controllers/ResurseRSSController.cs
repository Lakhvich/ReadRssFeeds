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

        public ActionResult List(int? id, string sort = "resurse")
        {
            var feedRssList = (id != null && id != 0) ?
                (sort.Equals("date") ? repository.GetResourceRSSAndRelationshipItemsByIdOrderByDes(id)
                : repository.GetResourceRSSAndRelationshipItemsById(id))
                : repository.GetResourceRSSAndRelationshipItems();
            if (feedRssList == null)
            {
                return HttpNotFound();
            }

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
            var feedRssList = (id != null && id != 0) ?
                (sort.Equals("date") ? repository.GetResourceRSSAndRelationshipItemsByIdOrderByDes(id)
                : repository.GetResourceRSSAndRelationshipItemsById(id))
                : repository.GetResourceRSSAndRelationshipItems();

            if (feedRssList == null)
            {
                return HttpNotFound();
            }

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